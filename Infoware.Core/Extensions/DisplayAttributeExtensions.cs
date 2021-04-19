using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infoware.Core.Extensions
{
    public static class DisplayAttributeExtensions
    {
        public static DisplayAttribute GetDisplayAttribute(object value)
        {
            return GetDisplayAttribute(value.GetType(), value);
        }

        public static DisplayAttribute GetDisplayAttribute(Type type, object value)
        {
            var typeNullable = Nullable.GetUnderlyingType(type);
            type = typeNullable ?? type;

            return type
                .GetMember(value.ToString())
                .OfType<DisplayAttribute>()?
                .FirstOrDefault();
        }

        public static DisplayAttribute GetDisplayAttribute(PropertyInfo property)
        {
            return property
                .GetCustomAttribute<DisplayAttribute>();
        }
    }
}
