using SchoolMeals.Interfaces;
using SchoolMeals.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolMeals.Services
{
    public class ModelSchema : IModelSchema
    {
        public Schema GetSchema(string modelName)
        {
            Schema schema = new Schema();

            schema.Name = modelName;

            List<PropertyInfo> properties = Type.GetType($"SchoolMeals.Models.{modelName}").GetProperties().Where(p => p.CustomAttributes.Any(a => a.AttributeType.Name.Equals("DisplayAttribute"))).ToList();

            schema.Properties = properties.Select(p => 
            {
                CustomAttributeData displayAtr = p.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name.Equals("DisplayAttribute"));
                CustomAttributeData dataTypeAtr = p.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name.Equals("DataTypeAttribute"));

                string dataType = string.Empty;

                if(dataType != null)
                {
                    if (dataTypeAtr.ConstructorArguments[0].ArgumentType.BaseType.Name.Equals("Enum"))
                    {
                        int? dataTypeNum = dataTypeAtr.ConstructorArguments[0].Value as int?;

                        if(dataTypeNum != null)
                        {
                            dataType = ((DataType)dataTypeNum).ToString();
                        }
                    }
                    else if (dataTypeAtr.ConstructorArguments[0].ArgumentType.BaseType.Name.Equals("Object"))
                    {
                        dataType = dataTypeAtr.ConstructorArguments[0].Value as string;
                    }
                }

                return new Property
                {
                    PropName = p.Name,
                    DisplayName = displayAtr != null ? displayAtr.NamedArguments[0].TypedValue.Value as string : p.Name,
                    Type = dataType != null ? dataType as string : p.PropertyType.Name,
                };
            }).ToList();

            return schema;
        }
    }
}
