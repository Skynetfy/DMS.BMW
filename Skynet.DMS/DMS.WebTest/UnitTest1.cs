using System;
using System.Collections.Generic;
using System.Linq;
using DMS.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DMS.WebTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new List<MyClass>();
            list.Add(new MyClass()
            {
                A="1",
                B="2"
            });
            list.Add(new MyClass()
            {
                A = "1",
                B = "5"
            });
            list.Add(new MyClass()
            {
                A = "3",
                B = "4"
            });

            var q = list.GroupBy(x => x.A).Select(x => x.Key).ToList();
        }

        public class MyClass
        {
            public string A { get; set; }
            public string B { get; set; }
        }
        [TestMethod]
        public void TestRetry()
        {
            RetryHelper.Execute(() =>
            {
                //throw new Exception("test");
            },TimeSpan.FromSeconds(3));
        }
    }
}
