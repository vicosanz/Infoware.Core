using System;
using System.Linq;
using System.Reflection;
using Infoware.Core.Attributes;

namespace Infoware.Core.Extensions
{
    public static class ExternalCodeAttributeExtensions
	{
        public static ExternalCodeAttribute GetExternalCodeAttribute(object value, DateTime? validInDate = null)
		{
            return GetExternalCodeAttribute(value.GetType(), value, validInDate);
        }

        public static ExternalCodeAttribute GetExternalCodeAttribute(Type type, object value, DateTime? validInDate = null)
		{
            var typeNullable = Nullable.GetUnderlyingType(type);
            type = typeNullable ?? type;

			return type.GetMember(value.ToString()).FirstOrDefault()
                .GetCustomAttributes<ExternalCodeAttribute>()
                .FirstOrDefault(x => x.IsCurrent(validInDate));
		}

		public static ExternalCodeAttribute GetExternalCodeAttribute(PropertyInfo property, DateTime? validInDate = null)
        {
            return property
                .GetCustomAttributes<ExternalCodeAttribute>()
				.FirstOrDefault(x => x.IsCurrent(validInDate));
		}
    }
}
