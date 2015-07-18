﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was not generated by a tool. but for stylecop suppression.
// </auto-generated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Drawing;
using StrixIT.Platform.Core;
using Moq;
using System.Reflection;

namespace StrixIT.Platform.Core.Tests
{
    [TestClass()]
    public class FileSystemWrapperTests
    {
        [TestInitialize]
        public void Init()
        {
            StrixPlatform.CurrentCultureCode = "en";
        }

        [TestCleanup]
        public void Cleanup()
        {
            StrixPlatform.CurrentCultureCode = null;
        }

        [TestMethod()]
        public void DeleteFileShouldRemoveFileFromDisk()
        {
            var wrapper = new FileSystemWrapper();
            var date = DateTime.Now;
            string destinationPath = string.Format("{0}\\TestResults\\{1}\\{2}", StrixPlatform.Environment.WorkingDirectory, date.Year, date.Month);
            string fullPath = destinationPath + "\\Strix_losuiltje.png";
            string originalFullPath = StrixPlatform.Environment.WorkingDirectory + "\\TestFiles\\Strix_losuiltje.png";

            if (!System.IO.Directory.Exists(destinationPath))
            {
                System.IO.Directory.CreateDirectory(destinationPath);
            }

            if (!System.IO.File.Exists(fullPath))
            {
                System.IO.File.Copy(originalFullPath, fullPath);
            }

            string root = Path.Combine(StrixPlatform.Environment.WorkingDirectory, "TestResults");
            string fileName = "Strix_losuiltje";
            string fileExtension = "png";
            wrapper.DeleteFile(string.Format("{0}\\{1}.{2}", System.IO.Path.Combine(root, date.Year.ToString(), date.Month.ToString()), fileName, fileExtension));
            wrapper.ProcessDeleteQueue();
            bool result = System.IO.File.Exists(fullPath);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void SaveImageFromByteArrayShouldSaveImageToDisk()
        {
            var wrapper = new FileSystemWrapper();
            var date = DateTime.Now;
            string fileName = "Strix_losuiltje_from_bytearray";
            string fileExtension = "png";
            string destinationPath = string.Format("{0}\\TestResults\\{1}\\{2}", StrixPlatform.Environment.WorkingDirectory, date.Year, date.Month);
            string originalFullPath = StrixPlatform.Environment.WorkingDirectory + "\\TestFiles\\Strix_losuiltje_2.png";
            Stream inputStream = new FileStream(originalFullPath, FileMode.Open);
            byte[] bytes = new byte[inputStream.Length];
            inputStream.Read(bytes, 0, (int)inputStream.Length);
            bool expected = true;
            bool actual;
            actual = wrapper.SaveFile(string.Format("{0}\\{1}.{2}", destinationPath, fileName, fileExtension), bytes);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SaveDocumentFromByteArrayShouldSaveDocumentToDisk()
        {
            var wrapper = new FileSystemWrapper();
            var date = DateTime.Now;
            string fileName = "test_from_bytearray";
            string fileExtension = "pdf";
            string destinationPath = string.Format("{0}\\TestResults\\{1}\\{2}", StrixPlatform.Environment.WorkingDirectory, date.Year, date.Month);
            string originalFullPath = StrixPlatform.Environment.WorkingDirectory + "\\TestFiles\\test.pdf";
            Stream inputStream = new FileStream(originalFullPath, FileMode.Open);
            byte[] bytes = new byte[inputStream.Length];
            inputStream.Read(bytes, 0, (int)inputStream.Length);
            bool expected = true;
            bool actual;
            actual = wrapper.SaveFile(string.Format("{0}\\{1}.{2}", destinationPath, fileName, fileExtension), bytes);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHtmlTemplateDataForSpecificCultureShouldGetTemplateForCulture()
        {
            var wrapper = new FileSystemWrapper();
            var result = wrapper.GetHtmlTemplate(Path.Combine(StrixPlatform.Environment.WorkingDirectory, "TestFiles"), "AccountInformationMail", "en");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Your account for [[SITENAME]]", result[0].Subject);
            Assert.AreEqual(@"<h1>Your account for [[SITENAME]]</h1>
<p>Dear [[USERNAME]]</p>
<p>
    A new user account for [[SITENAME]] has been created for you. When you are ready to select a password, please click the link below. You'll then receive a
    new link that will allow you to enter a password.
</p>
<a href=""[[BASEURL]]/Account/SendPasswordLink/[[USERID]]"">Select a password</a>", result[0].Body);
            Assert.AreEqual("en", result[0].Culture);
        }

        [TestMethod()]
        public void GetHtmlTemplateDataForUnknownCultureShouldGetTemplateForDefaultCulture()
        {
            var wrapper = new FileSystemWrapper();
            var result = wrapper.GetHtmlTemplate(Path.Combine(StrixPlatform.Environment.WorkingDirectory, "TestFiles"), "AccountInformationMail", "fr");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Your account for [[SITENAME]]", result[0].Subject);
            Assert.AreEqual("en", result[0].Culture);
        }

        [TestMethod()]
        public void GetHtmlTemplateDataShouldGetTemplatesForAllCultures()
        {
            var wrapper = new FileSystemWrapper();
            var result = wrapper.GetHtmlTemplate(Path.Combine(StrixPlatform.Environment.WorkingDirectory, "TestFiles"), "AccountInformationMail", null);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Your account for [[SITENAME]]", result[0].Subject);
            Assert.AreEqual(@"<h1>Your account for [[SITENAME]]</h1>
<p>Dear [[USERNAME]]</p>
<p>
    A new user account for [[SITENAME]] has been created for you. When you are ready to select a password, please click the link below. You'll then receive a
    new link that will allow you to enter a password.
</p>
<a href=""[[BASEURL]]/Account/SendPasswordLink/[[USERID]]"">Select a password</a>", result[0].Body);
            Assert.AreEqual("en", result[0].Culture);
            Assert.AreEqual("Uw account op [[SITENAME]]", result[1].Subject);
            Assert.AreEqual(@"<h1>Uw account op [[SITENAME]]</h1>
<p>Beste [[USERNAME]]</p>
<p>
    Er is een nieuw account op [[SITENAME]] voor u aangemaakt. Wanneer u uw wachtwoord wilt kiezen, gebruik dan de link hieronder. U krijgt dan een nieuwe link toegestuurd waarmee u uw wachtwoord kunt instellen.
</p>
<a href=""[[BASEURL]]/Account/SendPasswordLink/[[USERID]]"">Kies uw wachtwoord</a>", result[1].Body);
            Assert.AreEqual("nl", result[1].Culture);
        }
    }
}