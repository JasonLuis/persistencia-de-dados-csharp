using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace ScreenSound.Web.Test
{
    public class TestandoLogin
    {

        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;


        public TestandoLogin()
        {
            this.driver = new ChromeDriver(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location));

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));  // Tempo de espera máximo de 10 segundos

        }

        [Fact]
        public void VerificandoAPaginaHome()
        {
            //Arrange
            //Act
            driver.Navigate().GoToUrl("https://localhost:7277/");


            wait.Until(d => (d.Title.Contains("Home")));

            //Assert
            Assert.Contains("Hello", driver.PageSource);
        }
    }
}
