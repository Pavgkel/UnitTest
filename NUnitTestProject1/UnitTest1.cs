using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
            string c;
            bool result=true;
            using (var wc = new WebClient())
            {
                c = wc.DownloadString("http://demo.macroscop.com:8080/command?type=gettime&login=root&password=");
            }

            Regex x = new Regex(@"\d\d?\:\d\d\:\d\d\s?");
            Match m = x.Match(c);
            DateTime time =  DateTime.Parse(m.ToString());
            var second = DateTime.Now.Second - time.Second;
            if (second<15) result = true;
            else result = false;
            Assert.IsTrue(result,"false");
        }
        [Test]
        public void Test2()
        {
            string c;
            bool result = true;
            using (var wc = new WebClient())
            {
                c = wc.DownloadString("http://demo.macroscop.com:8080/command?type=gettime&login=root&password=&responsetype=json");
            }

            Regex x = new Regex(@"\d\d?\:\d\d\:\d\d\s?");
            Match m = x.Match(c);
            DateTime time = DateTime.Parse(m.ToString());
            var second = DateTime.Now.Second - time.Second;
            if (second < 15) result = true;
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