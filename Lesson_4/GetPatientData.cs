using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Lesson_4
{
    [TestFixture]
    public class GetPatientData
    {
        private IWebDriver driver = new ChromeDriver();
        private string baseURL = "https://login.veloximaging.net/";
        private string registrationPage = "https://w0r4.veloximaging.net/aaa/Reception/Registration";
        ConnectDB ListPat = new ConnectDB();

        public void OpenRegistrationPage()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = false;
            service.Start();
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("ytest.developer");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("No pain no g@in");
            driver.FindElement(By.CssSelector("button.yellow")).Click();
        }

        Patient GetDataOHIP(Patient PatientData)
        {
            string[] validCard = new[] {"Health", "card", "passed", "validation"};
            
            driver.Navigate().GoToUrl(registrationPage);
            driver.FindElement(By.Id("ppHin")).Clear();
            driver.FindElement(By.Id("ppHin")).SendKeys(PatientData.OHIP_Number);
            driver.FindElement(By.CssSelector("input[class^='inp_nh ver field-validation-err']")).SendKeys(PatientData.OHIP_Code);
            driver.FindElement(By.XPath("//div[@id='editableDiv']/div/div[3]/ul/li/a/span")).Click();
            // Надо ожидание сделать норм
            System.Threading.Thread.Sleep(2000);
            string message = driver.FindElement(By.ClassName("info")).Text;
            foreach (var a in message.Split())
            {
                foreach (var b in validCard)
                {
                    if (a==b)
                    { PatientData.Error = "OK"; }
                    else
                    {
                        PatientData.Error = driver.FindElement(By.ClassName("info")).Text.Replace("\r", "").Replace("\n", "")
                            .Replace(PatientData.OHIP_Number, "").Replace("OHIP", "");
                    }

                }
            }
            
            PatientData.LName = driver.FindElement(By.Id("ppLastName")).GetAttribute("value");
            PatientData.FName = driver.FindElement(By.Id("ppFirstName")).GetAttribute("value");
            PatientData.DOB = driver.FindElement(By.Id("DateOfBirth")).GetAttribute("value");

            return PatientData;
        }
        public void SendNumberCart()
        {
            OpenRegistrationPage();
            foreach (var a in ListPat.SelectNumbers())
            {
                GetDataOHIP(a);
                ListPat.SqlUpdate(a);
            }
        }
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
