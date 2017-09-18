using Microsoft.VisualStudio.TestTools.UnitTesting;
using BreadFactory.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Logics.Tests
{
    [TestClass()]
    public class MainLogicTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            string fileName = "D:\\RVR\\Tool\\RVR-UI-0006_アプリ設定情報.xlsx";
            string sheetName = "アプリ設定情報一覧";
            string outputFileName = "D:\\RVR\\Tool\\App.config";

            MainLogic mainLogic = new MainLogic();
            mainLogic.Execute(fileName, sheetName, outputFileName);
        }
    }
}