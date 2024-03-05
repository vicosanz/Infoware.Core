using System;

namespace Infoware.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ExternalCodeAttribute : Attribute
    {
		private string validFromDate;
		private string validToDate;
		private DateTime? validFrom;
		private DateTime? validTo;

		public string Code { get; }
		public string ValidFromDate
		{
			get => validFromDate; 
			set
			{
				validFromDate = value;
				validFrom = string.IsNullOrWhiteSpace(ValidFromDate) ? null : DateTime.Parse(ValidFromDate);
			}
		}
		public string ValidToDate
		{
			get => validToDate; 
			set
			{
				validToDate = value;
				validTo = string.IsNullOrWhiteSpace(ValidToDate) ? null : DateTime.Parse(ValidToDate);
			}
		}

		public ExternalCodeAttribute(string code)
        {
            Code = code;
        }

		public bool IsCurrent(DateTime? validInDate)
		{
			return !validInDate.HasValue || !validFrom.HasValue || (validInDate.Value >= validFrom.Value && validInDate.Value <= (validTo ?? validFrom.Value));
		}
	}
}