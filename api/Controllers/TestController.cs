using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace react_app.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("/[controller]/test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                Ok = 1
            });
        }

        [HttpGet("/[controller]/selenium")]
        public IActionResult Selenium()
        {
            var cts = new CancellationTokenSource();
            Task t = Task.Factory.StartNew(
            async () => {
                //cts.Token.ThrowIfCancellationRequested();


                RunSelenium();


            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();

            return Ok(new
            {
                Ok = "SELENIUM"
            });
        }


        private void RunSelenium()
        {
            Console.WriteLine("-----------------------------1");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.84 Safari/537.36");
            Console.WriteLine("headless && user-agent set");

            var m_driver = new ChromeDriver(@"/chromedriver", chromeOptions);
            m_driver.Url = "https://kubernetes.io/pl/docs/tutorials/kubernetes-basics/create-cluster/cluster-interactive/";
            m_driver.Manage().Window.Size = new Size(1024, 768);
            //IWebElement hideIntroHide = m_driver.FindElement(By.Id("hide-intro"));
            //hideIntroHide.Click();
            Thread.Sleep(5000);
            m_driver.SwitchTo().Frame(m_driver.FindElement(By.XPath("//iframe[@id='katacoda-container']")));
            Thread.Sleep(5000);
            IWebElement hideIntroHide = m_driver.FindElement(By.XPath("//button[@id='hide-intro']"));
            hideIntroHide.Click();

            Console.WriteLine("-----------------------------2");
            Thread.Sleep(5000);

            var element = m_driver.FindElement(By.XPath("//section[@id='terminal']"));
            //element.Click();
            new Actions(m_driver).MoveToElement(element).MoveByOffset(10, 10).Click().Perform();
            Thread.Sleep(3000);


            Console.WriteLine("-----------------------------3");
            SendKeys("git clone https://github.com/orzech123123/k8snet.git", m_driver);
            Actions actionProvider = new Actions(m_driver);
            actionProvider.KeyDown(Keys.Enter).Build().Perform();

            Thread.Sleep(5000);

            Console.WriteLine("-----------------------------4");
            SendKeys("cd k8snet/api/socket-server/", m_driver);
            actionProvider = new Actions(m_driver);
            actionProvider.KeyDown(Keys.Enter).Build().Perform();
            Thread.Sleep(5000);

            Console.WriteLine("-----------------------------5");
            SendKeys("npm i", m_driver);
            actionProvider = new Actions(m_driver);
            actionProvider.KeyDown(Keys.Enter).Build().Perform();

            Thread.Sleep(10000);

            Console.WriteLine("-----------------------------6");
            SendKeys("npm run client", m_driver);
            actionProvider = new Actions(m_driver);
            actionProvider.KeyDown(Keys.Enter).Build().Perform();

            Thread.Sleep(20000);

            Console.WriteLine("-----------------------------7");
            m_driver.GetScreenshot().SaveAsFile($"{DateTime.Now.Ticks}.png");
            Console.WriteLine("-----------------------------8");

            m_driver.Close();
        }

        private static void SendKeys(string command, IWebDriver driver)
        {
            Console.WriteLine();
            foreach (var letter in command)
            {
                Actions actionProvider = new Actions(driver);
                Console.Write(letter);
                actionProvider.KeyDown(new string(letter, 1)).Build().Perform();
                Thread.Sleep(1500);
            }
            Console.WriteLine();
        }
    }
}
