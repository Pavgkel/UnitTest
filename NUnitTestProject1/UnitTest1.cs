using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://demo.macroscop.com:8080/command?type=gettime&login=root&password=");
            bool result = true;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            if (timeTaken.Seconds < 15) result = true;
            else result = false;
            Assert.IsTrue(result,"false");
        }
        [Test]
        public void Test2()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://demo.macroscop.com:8080/command?type=gettime&login=root&password=&responsetype=json");
            bool result = true;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            if (timeTaken.Seconds < 15) result = true;
            else result = false;
            Assert.IsTrue(result, "false");
        }
        [Test] 
        public void Test3()
        {
            string HTML;
            bool result = true;
            using (var wc = new WebClient())
            {
                HTML = wc.DownloadString("http://demo.macroscop.com:8080/configex?login=root&password=");
            }

            var count = XDocument.Parse(HTML)
            .Descendants("ChannelInfo")
            .Count();
            if (count < 6) result = false;
            else result = true;
            Assert.IsTrue(result, "false");
        }
    }
}