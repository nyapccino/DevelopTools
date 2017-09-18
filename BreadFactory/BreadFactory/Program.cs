using BreadFactory.Logics;
using BreadFactory.Models;
using BreadFactory.Templates;
using BreadFactory.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // 引数の個数チェック
            if (args.Length < 3)
            {
                ShowUsage();
                return;
            }

            //string fileName = "D:\\RVR\\Tool\\RVR-UI-0006_アプリ設定情報.xlsx";
            //string sheetName = "アプリ設定情報一覧";
            string fileName = args[0];
            string sheetName = args[1];
            string outputFileName = args[2];

            if (!File.Exists(fileName))
            {
                Console.WriteLine("入力ファイルが存在しません。");
                Console.WriteLine("  入力ファイル：" + fileName);
                ShowUsage();
                return;
            }
            if (!Directory.Exists(outputFileName))
            {
                Console.WriteLine("出力先フォルダが存在しません。");
                Console.WriteLine("出力先フォルダ：" + outputFileName);
                ShowUsage();
                return;
            }

            Console.WriteLine("入力ファイルパス：" + Path.GetDirectoryName(fileName));
            Console.WriteLine("    入力ファイル：" + Path.GetFileName(fileName));
            Console.WriteLine("  出力先フォルダ：" + outputFileName);

            MainLogic mainLogic = new MainLogic();
            mainLogic.Execute(fileName, sheetName, outputFileName);
        }

        /// <summary>
        /// usageの表示
        /// </summary>
        private static void ShowUsage()
        {
            var template = new UsageTemplate { Version = VersionUtil.GetVersionInfo() };
            Console.WriteLine(template.TransformText());
        }
    }
}
