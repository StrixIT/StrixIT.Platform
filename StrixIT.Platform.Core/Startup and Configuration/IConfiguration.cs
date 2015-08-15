﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrixIT.Platform.Core
{
    public interface IConfiguration
    {
        #region Public Methods

        dynamic GetConfigSectionGroup(string group);

        T GetConfiguration<T>(string key) where T : class, new();

        T GetConfiguration<T>() where T : class, new();

        string GetConnectionString(string connectionStringName);

        T GetSetting<T>(string module, string key);

        #endregion Public Methods
    }
}