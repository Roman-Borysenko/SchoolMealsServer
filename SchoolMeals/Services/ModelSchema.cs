using Microsoft.Extensions.Logging;
using SchoolMeals.DataAnnotations;
using SchoolMeals.Enums;
using SchoolMeals.Extensions;
using SchoolMeals.Interfaces;
using SchoolMeals.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SchoolMeals.Services
{
    public class ModelSchema : IModelScheme
    {
        private ILogger<ModelSchema> _logger;
        public ModelSchema(ILogger<ModelSchema> logger) 
        {
            _logger = logger;
        }
        public Dictionary<string, Dictionary<ImageSize, List<int>>> ImagesSize = new Dictionary<string, Dictionary<ImageSize, List<int>>>
        {
            {
                "Article", new Dictionary<ImageSize, List<int>>
                {
                    { ImageSize.Min, new List<int> { 420, 500 } },
                    { ImageSize.Max, new List<int> { 1200, 800} }
                }
            },
            {
                "Slide", new Dictionary<ImageSize, List<int>>
                {
                    { ImageSize.Max, new List<int> { 1800, 830 } }
                }
            },
            {
                "Dish", new Dictionary<ImageSize, List<int>>
                {
                    { ImageSize.Min, new List<int> { 238, 238 } },
                    { ImageSize.Midd, new List<int> { 480, 585 } },
                    { ImageSize.Max, new List<int> { 656, 800 } }
                }
            }
        };
        public List<Menu> Schemes => new List<Menu> 
        {
            new Menu 
            {
                Name = "Категорії",
                Slug = "Category",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.Nutritionist },
                Urls = new Dictionary<string, Dictionary<string, string>> 
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/category/getforadmin" }, { "Delete", "api/category/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/category/get" }, { "Send", "api/category/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/category/get" }, { "Send", "api/category/create" } } }
                }
            },
            new Menu
            {
                Name = "Інградієнти",
                Slug = "Ingredient",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.Nutritionist },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/ingredient/getforadmin" }, { "Delete", "api/ingredient/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/ingredient/get" }, { "Send", "api/ingredient/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/ingredient/get" }, { "Send", "api/ingredient/create" } } }
                }
            },
            new Menu
            {
                Name = "Теги",
                Slug = "Tag",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.Nutritionist },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/tag/getforadmin" }, { "Delete", "api/tag/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/tag/get" }, { "Send", "api/tag/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/tag/get" }, { "Send", "api/tag/create" } } }
                }
            },
            new Menu
            {
                Name = "Блог",
                Slug = "Article",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.HeadTeacher },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/blog/getforadmin" }, { "Delete", "api/blog/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/blog/get" }, { "Send", "api/blog/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/blog/get" }, { "Send", "api/blog/create" } } }
                }
            },
            new Menu
            {
                Name = "Слайдер",
                Slug = "Slide",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.HeadTeacher },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/slider/getforadmin" }, { "Delete", "api/slider/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/slider/get" }, { "Send", "api/slider/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/slider/get" }, { "Send", "api/slider/create" } } }
                }
            },
            new Menu
            {
                Name = "Страви",
                Slug = "Dish",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.Nutritionist },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/dish/getforadmin" }, { "Delete", "api/dish/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/dish/get" }, { "Send", "api/dish/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/dish/get" }, { "Send", "api/dish/create" } } }
                }
            },
            new Menu
            {
                Name = "Захворювання",
                Slug = "Disease",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.HeadTeacher, RolesTypes.Nutritionist },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/disease/getforadmin" }, { "Delete", "api/disease/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/disease/get" }, { "Send", "api/disease/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/disease/get" }, { "Send", "api/disease/create" } } }
                }
            },
            new Menu
            {
                Name = "Статуси замовлення",
                Slug = "OrderStatus",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.HeadTeacher, RolesTypes.CookingService },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/orderstatus/getforadmin" }, { "Delete", "api/orderstatus/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/orderstatus/get" }, { "Send", "api/orderstatus/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/orderstatus/get" }, { "Send", "api/orderstatus/create" } } }
                }
            },
            new Menu
            {
                Name = "Користувачі",
                Slug = "User",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.HeadTeacher },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/user/getforadmin" }, { "Delete", "api/user/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/user/get" }, { "Send", "api/user/update" } } },
                    { AdminOperations.Add, new Dictionary<string, string> { { "Get", "api/user/get" }, { "Send", "api/user/create" } } }
                }
            },
            new Menu
            {
                Name = "Замовлення",
                Slug = "Order",
                Roles = new List<string> { RolesTypes.Admin, RolesTypes.HeadTeacher, RolesTypes.CookingService },
                Urls = new Dictionary<string, Dictionary<string, string>>
                {
                    { AdminOperations.Show, new Dictionary<string, string> { { "Get", "api/order/getforadmin" }, { "Delete", "api/order/delete" } } },
                    { AdminOperations.Edit, new Dictionary<string, string> { { "Get", "api/order/get" }, { "Send", "api/order/update" } } }
                }
            }
        };
        public Scheme GetScheme(string scheme, string operation)
        {
            string attribute = "DisplayFormAttribute";

            if (operation.Equals(AdminOperations.Show))
                attribute = "DisplayDataGridAttribute";

            if (!Schemes.Any(s => s.Slug.Equals(scheme)))
                return null;

            try 
            {
                Scheme schema = GetBaseScheme(scheme, operation);

                List<PropertyInfo> properties = Type.GetType($"SchoolMeals.Models.{scheme}").GetProperties().Where(p => p.CustomAttributes.Any(a => a.AttributeType.Name.Equals(attribute))).ToList();

                schema.Properties = properties.Select(p =>
                {
                    Dictionary<ImageSize, List<int>> imagesSize = null;

                    CustomAttributeData displayAtr = p.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name.Equals(attribute));
                    CustomAttributeData dataTypeAtr = p.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name.Equals("DataTypeAttribute"));

                    string dataType = string.Empty;

                    if (dataTypeAtr != null)
                    {
                        if (dataTypeAtr.ConstructorArguments[0].ArgumentType.BaseType.Name.Equals("Enum"))
                        {
                            int? dataTypeNum = dataTypeAtr.ConstructorArguments[0].Value as int?;

                            if (dataTypeNum != null)
                            {
                                dataType = ((DataType)dataTypeNum).ToString();
                            }
                        }
                        else if (dataTypeAtr.ConstructorArguments[0].ArgumentType.BaseType.Name.Equals("Object"))
                        {
                            dataType = dataTypeAtr.ConstructorArguments[0].Value as string;
                        }

                        if(dataType.Equals(CustomDataType.Image))
                        {
                            imagesSize = ImagesSize.SingleOrDefault(i => i.Key.Equals(scheme)).Value;
                        }
                    }

                    return new Property
                    {
                        PropName = p.Name,
                        DisplayData = displayAtr != null ? displayAtr.NamedArguments.ToDictionary(a => a.MemberName, a => a.TypedValue.Value as string) : null,
                        Type = dataType != null ? dataType : p.PropertyType.Name,
                        ImagesSize = imagesSize
                    };
                }).ToList();

                return schema;
            } catch(Exception e)
            {
                _logger.LogError(e.ShowError());
                return null;
            }
        }

        private Scheme GetBaseScheme(string scheme, string operation) 
        {
            Scheme schemeObj = new Scheme();
            Menu el = Schemes.SingleOrDefault(m => m.Slug.ToUpper().Equals(scheme.ToUpper()));

            if(el != null) 
            {
                schemeObj.Name = el.Name;
                schemeObj.Slug = el.Slug;
                schemeObj.Url = el.Urls.SingleOrDefault(u => u.Key.ToUpper().Contains(operation.ToUpper())).Value;
            }

            return schemeObj;
        }
    }
}
