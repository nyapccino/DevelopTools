using BreadFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Logics
{
    public class BranchLogic
    {
        public static BranchModel Execute(List<BreadFactoryModel> breadFactoryModelList, int maxLevel)
        {
            // Dictionaryの初期化
            Dictionary<int, List<BranchModel>> branchModelDictionary = InitBranch(maxLevel);

            foreach (var model in breadFactoryModelList.Select((value, index) => new { Index = index, Value = value }))
            {
                for (int level = 1; level <= maxLevel; level++)
                {
                    BranchModel branchModel = new BranchModel
                    {
                        Level = level,
                        No = model.Index,
                        PhysicalName = GetStringValue(model.Value, "PhysicalName", level),
                        LogicalName = GetStringValue(model.Value, "LogicalName", level)
                    };

                    if (IsValid(branchModel))
                    {
                        var branchModelList = branchModelDictionary[level];
                        branchModelList.Add(branchModel);
                        if (level > 1)
                        {
                            branchModelDictionary[level - 1].Last().ChildList.Add(branchModel);
                        }
                    }
                }
            }

            BranchModel result = new BranchModel
            {
                ChildList = branchModelDictionary[1]
            };

            return result;
        }

        /// <summary>
        /// BranchModelの有効無効チェック
        /// </summary>
        /// <param name="branchModel"></param>
        /// <returns></returns>
        private static bool IsValid(BranchModel branchModel)
        {
            return (
                !string.IsNullOrEmpty(branchModel.PhysicalName) &&
                branchModel.PhysicalName != "-");
        }

        /// <summary>
        /// Dictionaryの初期化
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        private static Dictionary<int, List<BranchModel>> InitBranch(int maxLevel)
        {
            Dictionary<int, List<BranchModel>> branchModelDictionary = new Dictionary<int, List<BranchModel>>();
            for (int level = 1; level <= maxLevel; level++)
            {
                branchModelDictionary.Add(level, new List<BranchModel>());
            }

            return branchModelDictionary;
        }

        /// <summary>
        /// ジェネリックを使用して変数名の末尾を添え字として値を取得します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string GetStringValue<T>(T obj, string key, int index)
        {
            var porp = typeof(T).GetProperty(string.Format("{0}{1}", key, index));
            return porp.GetValue(obj).ToString();
        }
    }
}
