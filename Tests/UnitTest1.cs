using FluentAssertions;
using Infoware.Core.Attributes;
using Infoware.Core.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
	public class ExternalCodeAttributeTest
	{
		private enum exampleEnum
		{
			[Display(Name = "name1")]
			[ExternalCode("1st")]
			First,
			[ExternalCode("2nd")]
			Second
		}

		[Fact]
		public void ExternalCodeValid()
		{
			var attrJustFrom = new ExternalCodeAttribute("X")
			{
				ValidFromDate = "2010/06/01"
			};
			var attrFromTo = new ExternalCodeAttribute("X")
			{
				ValidFromDate = "2010/06/01",
				ValidToDate = "2024/03/05"
			};
			var attrAlways = new ExternalCodeAttribute("X");
			var attrInPast = new ExternalCodeAttribute("X")
			{
				ValidFromDate = "2010/06/01",
				ValidToDate = "2014/03/05"
			};

			DateTime dateNow = DateTime.Parse("2024/03/05");
			attrJustFrom.IsCurrent(dateNow).Should().BeTrue();
			attrFromTo.IsCurrent(dateNow).Should().BeTrue();
			attrAlways.IsCurrent(dateNow).Should().BeTrue();
			attrInPast.IsCurrent(dateNow).Should().BeFalse();

			attrJustFrom.IsCurrent().Should().BeTrue();
			attrFromTo.IsCurrent().Should().BeTrue();
			attrAlways.IsCurrent().Should().BeTrue();
			attrInPast.IsCurrent().Should().BeTrue();

			var list = ExternalCodeAttributeExtensions.GetAllValid<exampleEnum>(dateNow, null);
			list.Should().HaveCount(2);

			var list1 = ExternalCodeAttributeExtensions.GetAllValid<exampleEnum>(dateNow, (element) => element.Code.Contains("st"));
			list1.Should().HaveCount(1);
		}

		[Fact]
		public void DisplayAttributeValid()
		{
			exampleEnum.First.GetDisplayName().Should().Be("name1");
			exampleEnum.Second.GetDisplayName().Should().Be(null);
		}
	}
}