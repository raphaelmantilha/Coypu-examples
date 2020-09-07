using System;
using System.IO;
using Coypu;
using Coypu.Drivers.Selenium;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace NinjaPlus.Common
{
    public class BaseTest
    {
        protected BrowserSession Browser;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var sessionConfig = new SessionConfiguration()
            {
                AppHost = "http://192.168.99.100",
                Port = 5000,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                //Browser = Coypu.Drivers.Browser.Chrome,
                Timeout = TimeSpan.FromSeconds(10)
            };

            if (config["browser"].Equals("chrome"))
            {
                sessionConfig.Browser = Coypu.Drivers.Browser.Chrome;
            }

            if (config["browser"].Equals("firefox"))
            {
                sessionConfig.Browser = Coypu.Drivers.Browser.Firefox;
            }

            Browser = new BrowserSession(sessionConfig);
            Browser.MaximiseWindow();
        }

        public string CoverPath()
        {
            var outputPath = Environment.CurrentDirectory;
            return outputPath + "\\Images\\";
        }

        public void TakeScreenshot()
        {
            var resultId = TestContext.CurrentContext.Test.ID;
            var diretorio = "c:\\qaninja\\screenshots";

            if(!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var screenshot = $"{diretorio}\\{resultId}.png";

            Browser.SaveScreenshot(screenshot);
            TestContext.AddTestAttachment(screenshot);
        }

        [TearDown]
        public void Finish()
        {
            try
            {
                TakeScreenshot();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro ao capturar o Screenshot :(");
                throw new Exception(e.Message);
            }
            finally
            {
                 Browser.Dispose();
            }           
        }
    }
}