using NUnit.Framework;
using System;
using System.IO;

namespace MetroTest {
    [SetUpFixture]
    public class SetupFixture {
        public const string path = @"c:\tmp\MyTest.cfg";

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            // TODO: Create sample config file
            if (!File.Exists(path)) {
                using (StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine("2 N N E E S S W W");
                    sw.WriteLine("3 N E E S S W W N");
                }
            } else {
                OneTimeTearDown();
                OneTimeSetUp();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            if (File.Exists(path)) {
                File.Delete(path);
            }
        }
    }
}