using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion

        #region DisplayAttribute
        public static string GetDisplayName(this Enum enumerate)
        {
            return DisplayAttributeExtensions.GetDisplayAttribute(enumerate)?.GetName();
        }
        #endregion

    }
}
