using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Infoware.Core.Extensions
{
    public static class DisplayAttributeExtensions
    {
		public static DisplayAttribute GetDisplayAttribute(object value) => GetDisplayAttribute(value.GetType(), value);

		public static DisplayAttribute GetDisplayAttribute(Type type, object value)
        {
            var typeNullable = Nullable.GetUnderlyingType(type);
            type = typeNullable ?? type;

            return type.GetMember(value.ToString()).FirstOrDefault().GetCustomAttribute<DisplayAttribute>();
        }

		public static DisplayAttribute GetDisplayAttribute(PropertyInfo property) => property
				.GetCustomAttribute<DisplayAttribute>();
	}
}
