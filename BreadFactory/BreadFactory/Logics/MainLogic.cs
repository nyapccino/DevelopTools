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

namespace BreadFactory.Logics
{
    public class MainLogic
    {
        /// <summary>
        /// 階層の最大数
        /// </summary>
        private static int MaxBranchLevel = 3;

        public void Execute(string fileName, string sheetName, string outputFileName)
        {
            // Excel読み込み
            var excelData = ExcelUtil.ReadExcel<BreadFactoryModel>(fileName, sheetName);
            BranchModel branchModel = BranchLogic.Execute(excelData, MaxBranchLevel);
            List<ValueModel> valueModelList = ValueLogic.Execute(excelData);

            Dictionary<string, object> jsonObject = CreateLeaf(branchModel, valueModelList) as Dictionary<string, object>;

            List<AppSettingsModel> appSettingsModelList = new List<AppSettingsModel>();
            foreach (var child in branchModel.ChildList)
            {
                AppSettingsModel appSettingsModel = new AppSettingsModel
                {
                    Key = child.PhysicalName,
                    Value = JsonConvert.SerializeObject(jsonObject[child.PhysicalName], Formatting.Indented),
                    Description = child.LogicalName
                };

                appSettingsModelList.Add(appSettingsModel);
            }

            AppSettingsTemplate template = new AppSettingsTemplate
            {
                Version = VersionUtil.GetVersionInfo(),
                ModelList = appSettingsModelList
            };

            File.WriteAllText(outputFileName, template.TransformText());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchModel"></param>
        /// <param name="valueModelList"></param>
        /// <returns></returns>
        private static object CreateLeaf(BranchModel branchModel, List<ValueModel> valueModelList)
        {
            if (branchModel.ChildList.Count == 0)
            {
                // 子がいない場合、対応する値をセットする
                var result = valueModelList.Where(x => x.No == branchModel.No).Select(x => x.ValueList);
                if (result != null && result.Count() >= 1)
                {
                    return ValueUtil.GetValue(result.First());
                }
                else
                {
                    return "";
                }
            }
            else
            {
                Dictionary<string, object> leaf = new Dictionary<string, object>();
                foreach (var child in branchModel.ChildList)
                {
                    // 再起呼び出しで値を取得する
                    object value = CreateLeaf(child, valueModelList);

                    if (leaf.ContainsKey(child.PhysicalName))
                    {
                        if (leaf[child.PhysicalName].GetType() == typeof(List<object>))
                        {
                            // リスト形式の場合
                            List<object> leafList = leaf[child.PhysicalName] as List<object>;
                            leafList.Add(value);
                            leaf[child.PhysicalName] = leafList;
                        }
                        else
                        {
                            List<object> leafList = new List<object>();
                            leafList.Add(leaf[child.PhysicalName]);
                            leafList.Add(value);
                            leaf[child.PhysicalName] = leafList;
                        }
                    }
                    else
                    {
                        leaf.Add(child.PhysicalName, value);
                    }
                }

                return leaf;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private static void AddValue(Dictionary<string, object> dictionary, string key, object value)
        {
            if (dictionary.ContainsKey(key))
            {
                if (dictionary[key].GetType() == typeof(List<string>))
                {
                    List<string> valueList = new List<string>();
                    if (value.GetType() == typeof(List<string>))
                    {
                    }
                }
                else
                {
                    List<string> valueList = new List<string>();
                    valueList.Add(dictionary[key].ToString());
                    if (value.GetType() == typeof(List<string>))
                    {
                        List<string> paramValueList = value as List<string>;
                        valueList.AddRange(paramValueList);
                    }
                    else
                    {
                        valueList.Add(value.ToString());
                    }

                    // 値リストで上書きする
                    dictionary[key] = valueList;
                }
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
