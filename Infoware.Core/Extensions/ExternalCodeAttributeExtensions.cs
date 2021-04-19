using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infoware.Core.Attributes;

namespace Infoware.Core.Extensions
{
    public static class ExternalCodeAttributeExtensions
    {
        public static ExternalCodeAttribute GetExternalCodeAttribute(object value)
        {
            return GetExternalCodeAttribute(value.GetType(), value);
        }

        public static ExternalCodeAttribute GetExternalCodeAttribute(Type type, object value)
        {
            var typeNullable = Nullable.GetUnderlyingType(type);
            type = typeNullable ?? type;

            return type
                .GetMember(value.ToString())
                .OfType<ExternalCodeAttribute>()?
                .FirstOrDefault();
        }

        public static ExternalCodeAttribute GetExternalCodeAttribute(PropertyInfo property)
        {
            return property
                .GetCustomAttribute<ExternalCodeAttribute>();
        }
    }
}
