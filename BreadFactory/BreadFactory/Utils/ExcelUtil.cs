using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace BreadFactory.Utils
{
    public class ExcelUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FileName">ファイル名</param>
        /// <param name="SheetName">シート名</param>
        /// <returns></returns>
        public static List<T> ReadExcel<T>(string FileName, string SheetName) where T : new()
        {
            Console.WriteLine("{0} : [{1}]シート読込開始", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), SheetName);

            List<T> objList = new List<T>();
            using (FileStream stream = File.Open(FileName, FileMode.Open, FileAccess.Read))
            {
                XLWorkbook workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(SheetName);

                var rows = GetCells(worksheet);
                var header = rows.First();
                for (int i = 1; i < rows.Count; i++)
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    for (int j = 0; j < header.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(header[j]))
                        {
                            data.Add(header[j], rows[i][j]);
                        }
                    }
                    objList.Add(CreateObject<T>(data));
                }
            }
            Console.WriteLine("{0} : [{1}]シート読込終了", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), SheetName);

            return objList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private static List<List<string>> GetCells(IXLWorksheet worksheet)
        {
            bool startFlag = false;
            List<List<string>> result = new List<List<string>>();
            List<string> header = null;
            foreach (var row in worksheet.Rows(1, worksheet.LastRowUsed().RowNumber()))
            {
                if (startFlag)
                {
                    List<string> values = new List<string>();
                    for (int i = 1; i <= header.Count; i++)
                    {
                        values.Add(getCellValue(row.Cell(i)));
                    }
                    result.Add(values);
                }
                else
                {
                    List<string> record = new List<string>();
                    foreach (var cell in row.Cells())
                    {
                        string value = getCellValue(cell);
                        record.Add(value);
                    }
                    // ヘッダ行を取得する
                    if (record.Where(x => x == "No").Count() > 0)
                    {
                        header = record;
                        result.Add(header);
                        startFlag = true;
                    }
                }
            }
            return result;
        }

        private static string getCellValue(IXLCell cell)
        {
            try
            {
                return cell.Value.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// [ジェネリック]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static T CreateObject<T>(Dictionary<string, string> data) where T : new()
        {
            T obj = new T();
            foreach (var key in data.Keys)
            {
                var value = data[key];
                setValue(obj, key, value);
            }
            return obj;
        }

        /// <summary>
        /// プロパティの値を設定します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private static void setValue<T>(T obj, string name, string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var prop = typeof(T).GetProperty(name);
            if (prop != null)
            {
                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(obj, value);
                }
                if (prop.PropertyType == typeof(bool))
                {
                    bool parseValue;
                    if (bool.TryParse(value, out parseValue))
                    {
                        prop.SetValue(obj, parseValue);
                    }
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        prop.SetValue(obj, value.Replace("\r\n", "\n").Split('\n').ToList());
                    }
                    else
                    {
                        prop.SetValue(obj, new List<string>());
                    }
                }
            }
        }
    }
}