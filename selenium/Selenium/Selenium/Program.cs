using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            var m_driver = new ChromeDriver(@"C:\Projects\k8snet\selenium\Selenium");
            m_driver.Url = "https://kubernetes.io/pl/docs/tutorials/kubernetes-basics/create-cluster/cluster-interactive/";
            m_driver.Manage().Window.Size = new Size(1024, 768);
            //IWebElement hideIntroHide = m_driver.FindElement(By.Id("hide-intro"));
            //hideIntroHide.Click();
            Thread.Sleep(5000);
            m_driver.SwitchTo().Frame(m_driver.FindElement(By.XPath("//iframe[@id='katacoda-container']")));
            Thread.Sleep(5000);
            IWebElement hideIntroHide = m_driver.FindElement(By.XPath("//button[@id='hide-intro']"));
            hideIntroHide.Click();

            Thread.Sleep(5000);

            var element = m_driver.FindElement(By.XPath("//section[@id='terminal']"));
            //element.Click();
            new Actions(m_driver).MoveToElement(element).MoveByOffset(10, 10).Click().Perform();
            Thread.Sleep(3000);


            SendKeys("ls -la", m_driver);
            Actions actionProvider = new Actions(m_driver);
            actionProvider.KeyDown(Keys.Enter).Build().Perform();
            
            Thread.Sleep(5000);
            m_driver.GetScreenshot().SaveAsFile($"{DateTime.Now.Ticks}.png");
            
            m_driver.Close();
        }

        private static void SendKeys(string command, IWebDriver driver)
        {
            foreach (var letter in command)
            {
                Actions actionProvider = new Actions(driver);
                actionProvider.KeyDown(new string(letter, 1)).Build().Perform();
                Thread.Sleep(3000);
            }
        }
    }
}
