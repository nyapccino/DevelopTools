using BreadFactory.Models;
using BreadFactory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadFactory.Logics
{
    public class ValueLogic
    {
        public static List<ValueModel> Execute(List<BreadFactoryModel> breadFactoryModelList)
        {
            List<ValueModel> valueModelList = new List<ValueModel>();
            foreach (var model in breadFactoryModelList.Select((value, index) => new { Index = index, Value = value }))
            {
                if (ValueUtil.IsValid(model.Value.Value))
                {
                    if (ValueUtil.IsValid(model.Value.TypeName))
                    {
                        ValueModel valueModel = new ValueModel
                        {
                            No = model.Index,
                            TypeName = model.Value.TypeName
                        };
                        valueModel.ValueList.Add(model.Value.Value);
                        valueModelList.Add(valueModel);
                    }
                    else
                    {
                        valueModelList.Last().ValueList.Add(model.Value.Value);
                    }
                }
            }

            return valueModelList;
        }
    }
}
