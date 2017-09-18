using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// ファイルを出力します。
        /// </summary>
        /// <param name="NameSpace"></param>
        /// <param name="OutputPath"></param>
        /// <param name="FileName"></param>
        /// <param name="outputText"></param>
        public static void WriteAllText(string NameSpace, string OutputPath, string FileName, string outputText)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                string subPath = NameSpace.Replace(".", "\\");
                string outputFilePath = OutputPath + "\\" + subPath + "\\" + FileName;
                string outputDirPath = Path.GetDirectoryName(outputFilePath);

                if (!Directory.Exists(outputDirPath))
                {
                    Directory.CreateDirectory(outputDirPath);
                }

                File.WriteAllText(outputFilePath, outputText);
            }
        }
    }
}
