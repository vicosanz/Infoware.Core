using System;

namespace Infoware.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ExternalCodeAttribute : Attribute
    {
        public string Code { get; }
        public string ValidFromDate { get; set; }
        public string ValidToDate { get; set; }

        public ExternalCodeAttribute(string code)
        {
            Code = code;
        }
    }
}