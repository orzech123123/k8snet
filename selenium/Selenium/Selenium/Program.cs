using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            m_driver.GetScreenshot().SaveAsFile($"{DateTime.Now.Ticks}.png");
            //Thread.Sleep(200000);
            m_driver.Close();
        }
    }
}
