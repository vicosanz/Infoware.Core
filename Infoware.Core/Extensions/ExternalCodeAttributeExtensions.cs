using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public static string GetCode(this Enum enumerate, DateTime? validInDate = null) => GetExternalCodeAttribute(enumerate, validInDate)?.Code;

		public static List<TEnum> GetAllValid<TEnum>(DateTime validInDate, Expression<Func<ExternalCodeAttribute, bool>> expression = null) where TEnum : Enum
		{
			Func<ExternalCodeAttribute, bool> expcompiled = null;
			if (expression != null) 
				expcompiled = expression.Compile();

			List<TEnum> result = new();
			foreach (var value in Enum.GetValues(typeof(TEnum)))
			{
				var attr = GetExternalCodeAttribute(value, validInDate);
				if (attr != null && (expcompiled?.Invoke(attr) ?? true))
				{
					result.Add((TEnum)value);
				}
			}
			return result;
		}

	}
}
