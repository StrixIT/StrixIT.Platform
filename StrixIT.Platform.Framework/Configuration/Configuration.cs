using StrixIT.Platform.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace StrixIT.Platform.Framework
{
    public class Configuration : IConfiguration
    {
        #region Private Fields

        private static Dictionary<string, IDictionary<string, string>> _combinedAppSettings;
        private static ConnectionStringSettingsCollection _combinedConnections;
        private static ConcurrentDictionary<string, object> _configurationCache = new ConcurrentDictionary<string, object>();
        private static bool _configurationsLoaded = false;
        private static object _loadConfigLock = new object();

        private bool _isLocalRequest;

        #endregion Private Fields

        #region Public Constructors

        public Configuration() : this(true)
        {
        }

        public Configuration(bool isLocalRequest)
        {
            _isLocalRequest = isLocalRequest;

            if (!_configurationsLoaded)
            {
                LoadConfigurations();
            }
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the connection strings from all loaded config files.
        /// </summary>
        /// <returns>A dictionary of connection string names and connection strings</returns>
        public System.Configuration.ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (!_configurationsLoaded)
                {
                    Logger.Log("Load module configurations start.");
                    LoadConfigurations();
                    Logger.Log("Load module configurations done.");
                }

                return _combinedConnections;
            }
        }

        public bool CustomErrorsEnabled
        {
            get
            {
                var settings = GetConfigSectionGroup<SystemWebSectionGroup>("system.web");
                return settings.CustomErrors.Mode != CustomErrorsMode.Off && !(settings.CustomErrors.Mode == CustomErrorsMode.RemoteOnly && _isLocalRequest);
            }
        }

        public string FromAddress
        {
            get
            {
                var mailSettings = GetConfigSectionGroup<MailSettingsSectionGroup>("system.net/mailSettings");
                var from = mailSettings != null && mailSettings.Smtp != null ? mailSettings.Smtp.From : null;
                return from;
            }
        }

        public string MailPickupDirectory
        {
            get
            {
                var mailSettings = GetConfigSectionGroup<MailSettingsSectionGroup>("system.net/mailSettings");
                var pickupDir = mailSettings != null &&
                                mailSettings.Smtp != null &&
                                mailSettings.Smtp.SpecifiedPickupDirectory != null ?
                                mailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation : null;
                return pickupDir;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public T GetConfiguration<T>(string key) where T : class, new()
        {
            if (_configurationCache.ContainsKey(key))
            {
                return (T)_configurationCache[key];
            }

            IDictionary<string, string> settings;

            if (_combinedAppSettings.TryGetValue(key, out settings))
            {
                var configuration = new T();

                try
                {
                    foreach (var setting in settings.Keys)
                    {
                        var propertyName = setting.Substring(0, 1).ToUpper() + setting.Substring(1);

                        if (configuration.HasProperty(propertyName))
                        {
                            var property = configuration.GetType().GetProperty(propertyName);
                            configuration.SetPropertyValue(propertyName, Convert.ChangeType(settings[setting], property.PropertyType));
                        }
                    }

                    _configurationCache.GetOrAdd(key, configuration);

                    return configuration;
                }
                catch (FormatException ex)
                {
                    throw new StrixConfigurationException(string.Format("Error in configuration with key {0}. {1}", key, ex.Message));
                }
            }
            else
            {
                throw new StrixConfigurationException(string.Format("No configuration with key {0} found.", key));
            }
        }

        public T GetConfiguration<T>() where T : class, new()
        {
            var key = typeof(T).Name.Replace("Configuration", string.Empty);
            return GetConfiguration<T>(key);
        }

        public string GetConnectionString(string connectionStringName)
        {
            return _combinedConnections[connectionStringName].ConnectionString;
        }

        public T GetSetting<T>(string module, string key)
        {
            IDictionary<string, string> settings;

            if (_combinedAppSettings.TryGetValue(module, out settings))
            {
                string setting;

                if (settings.TryGetValue(key, out setting))
                {
                    T value = default(T);

                    if (setting != null)
                    {
                        try
                        {
                            value = (T)Convert.ChangeType(setting, typeof(T));
                        }
                        catch (FormatException ex)
                        {
                            throw new StrixConfigurationException(string.Format("Error converting configuration setting {0}. {1}", key, ex.Message));
                        }
                    }

                    return value;
                }
                else
                {
                    throw new StrixConfigurationException(string.Format("No setting with key {0} found in configuration {1}.", module, key));
                }
            }
            else
            {
                throw new StrixConfigurationException(string.Format("No configuration with key {0} found.", module));
            }
        }

        public void LoadConfigurations()
        {
            if (!_configurationsLoaded)
            {
                lock (_loadConfigLock)
                {
                    if (!_configurationsLoaded)
                    {
                        _combinedConnections = new System.Configuration.ConnectionStringSettingsCollection();
                        _combinedAppSettings = new Dictionary<string, IDictionary<string, string>>();
                        _combinedAppSettings.Add("Platform", new Dictionary<string, string>());
                        var platformSettings = System.Configuration.ConfigurationManager.AppSettings;

                        foreach (string key in platformSettings.Keys)
                        {
                            _combinedAppSettings["Platform"].Add(key, platformSettings[key].ToLower());
                        }

                        foreach (System.Configuration.ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
                        {
                            _combinedConnections.Add(connection);
                        }

                        var moduleDirectory = Path.Combine(WorkingDirectory, "Areas");

                        if (Directory.Exists(moduleDirectory))
                        {
                            string[] configFilePaths = Directory.GetFiles(moduleDirectory, "web.config", SearchOption.AllDirectories);

                            foreach (string configFilePath in configFilePaths)
                            {
                                System.Configuration.ExeConfigurationFileMap configMap = new System.Configuration.ExeConfigurationFileMap();
                                configMap.ExeConfigFilename = configFilePath;
                                var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configMap, System.Configuration.ConfigurationUserLevel.None);
                                LoadConnectionStrings(config);
                                LoadAppSettings(config);
                            }
                        }

                        _configurationsLoaded = true;
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Properties

        private string WorkingDirectory
        {
            get
            {
                if (HttpRuntime.AppDomainAppVirtualPath != null)
                {
                    return HttpRuntime.AppDomainAppPath;
                }

                return Helpers.GetWorkingDirectory();
            }
        }

        #endregion Private Properties

        #region Private Methods

        private static void LoadAppSettings(System.Configuration.Configuration config)
        {
            System.Configuration.AppSettingsSection section = config.GetSection("appSettings") as System.Configuration.AppSettingsSection;

            if (section == null)
            {
                return;
            }

            var pathParts = config.FilePath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            var moduleName = pathParts.ElementAt(pathParts.Length - 2);

            if (moduleName.ToLower() == "views")
            {
                return;
            }

            if (!_combinedAppSettings.ContainsKey(moduleName))
            {
                _combinedAppSettings.Add(moduleName, new Dictionary<string, string>());

                foreach (System.Configuration.KeyValueConfigurationElement s in section.Settings)
                {
                    _combinedAppSettings[moduleName].Add(s.Key, s.Value);
                }
            }
        }

        private static void LoadConnectionStrings(System.Configuration.Configuration config)
        {
            System.Configuration.ConnectionStringsSection section = null;

            try
            {
                section = config.GetSection("connectionStrings") as System.Configuration.ConnectionStringsSection;
            }
            catch (System.Configuration.ConfigurationErrorsException ex)
            {
                string duplicateName = Regex.Match(ex.BareMessage, "'([^']*')").ToString();
                var message = string.Format("While reading the module connection strings, multiple entries with the name {0} were found. Please make sure the connection strings of the platform and the modules all have unique names.", duplicateName);
                throw new System.Configuration.ConfigurationErrorsException(message);
            }

            if (section == null)
            {
                return;
            }

            foreach (System.Configuration.ConnectionStringSettings cs in section.ConnectionStrings)
            {
                if (_combinedConnections[cs.Name.ToLower()] == null)
                {
                    _combinedConnections.Add(new System.Configuration.ConnectionStringSettings(cs.Name.ToLower(), cs.ConnectionString));
                }
            }
        }

        private T GetConfigSectionGroup<T>(string group) where T : class
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = Path.Combine(WorkingDirectory, "web.config");
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            T settings = config.GetSectionGroup(group) as T;
            return settings;
        }

        #endregion Private Methods
    }
}