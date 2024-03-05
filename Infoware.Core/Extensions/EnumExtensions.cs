using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Infoware.Core.Attributes;

namespace Infoware.Core.Extensions
{
    public static class EnumExtensions
    {
		#region ExternalCodeAttribute
		public static string GetCode(this Enum enumerate, DateTime? validInDate = null) => ExternalCodeAttributeExtensions.GetExternalCodeAttribute(enumerate, validInDate)?.Code;

		public static List<TEnum> GetValid<TEnum>(DateTime validInDate, Expression<Func<ExternalCodeAttribute, bool>> expression = null) where TEnum : Enum
        {
            Func<ExternalCodeAttribute, bool> expcompiled = null;
            if (expression != null)
                expcompiled = expression.Compile();


            List<TEnum> result = new();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
				var attr = ExternalCodeAttributeExtensions.GetExternalCodeAttribute(value, validInDate);
				if (attr != null && (expcompiled?.Invoke(attr) ?? true))
				{
					result.Add((TEnum)value);
				}
			}
            return result;
        }
		#endregion

		#region DisplayAttribute
		public static string GetDisplayName(this Enum enumerate) => DisplayAttributeExtensions.GetDisplayAttribute(enumerate)?.GetName();
		#endregion

	}
}
