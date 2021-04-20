using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Infoware.Core.Attributes;

namespace Infoware.Core.Extensions
{
    public static class EnumExtensions
    {
        #region ExternalCodeAttribute
        public static string GetCode(this Enum enumerate)
        {
            return ExternalCodeAttributeExtensions.GetExternalCodeAttribute(enumerate)?.Code;
        }

        public static List<TEnum> GetValid<TEnum>(DateTime date, Expression<Func<ExternalCodeAttribute, bool>> expression = null) where TEnum : Enum
        {
            Func<ExternalCodeAttribute, bool> expcompiled = null;
            if (expression != null)
                expcompiled = expression.Compile();


            List<TEnum> result = new();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var attr = ExternalCodeAttributeExtensions.GetExternalCodeAttribute(value);
                if (
                    (string.IsNullOrWhiteSpace(attr.ValidFromDate) &&
                        (
                            string.IsNullOrWhiteSpace(attr.ValidToDate) ||
                            date <= DateTime.Parse(attr.ValidToDate)
                        )
                    )
                    ||
                    (date >= DateTime.Parse(attr.ValidFromDate) &&
                        (
                            string.IsNullOrWhiteSpace(attr.ValidToDate) ||
                            date <= DateTime.Parse(attr.ValidToDate)
                        )
                    )
                )
                {
                    if (expcompiled is null || expcompiled.Invoke(attr))
                    {
                        result.Add((TEnum)value);
                    }
                }
            }
            return result;
        }
        #endregion

        #region DisplayAttribute
        public static string GetDisplayName(this Enum enumerate)
        {
            return DisplayAttributeExtensions.GetDisplayAttribute(enumerate)?.GetName();
        }
        #endregion

    }
}
