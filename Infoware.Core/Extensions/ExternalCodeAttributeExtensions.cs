using System;
using System.Linq;
using System.Reflection;
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

            return type.GetMember(value.ToString()).FirstOrDefault().GetCustomAttribute<ExternalCodeAttribute>();
        }

        public static ExternalCodeAttribute GetExternalCodeAttribute(PropertyInfo property)
        {
            return property
                .GetCustomAttribute<ExternalCodeAttribute>();
        }
    }
}
