using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class WeeklyIncomePlaySheetDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime TimeStamp { get; set; }
		public DateTime Expiry { get; set; }
		public bool Used { get; set; }

		public StrategyTypes Strategy { get; set; }

		public List<WeeklyIncomeActionPlanDto> ActionPlans { get; set; }
		public List<WeeklyIncomeComboCountDto> ComboCounts { get; set; }

		#region Comparisons
		public static bool operator ==(WeeklyIncomePlaySheetDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is WeeklyIncomePlaySheetDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((WeeklyIncomePlaySheetDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(WeeklyIncomePlaySheetDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is WeeklyIncomePlaySheetDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((WeeklyIncomePlaySheetDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is WeeklyIncomePlaySheetDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((WeeklyIncomePlaySheetDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion

		#region ToString
		public override string ToString()
        {
            return Expiry.ToString();
        }
		#endregion
	}
}
