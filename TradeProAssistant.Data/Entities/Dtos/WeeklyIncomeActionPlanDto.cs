using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class WeeklyIncomeActionPlanDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Decimal Credit { get; set; }
		public Decimal Risk { get; set; }

		public WeeklyActionPlanGenerationMethods GenerationMethod { get; set; }


		public List<PairCondorDto> Pairs { get; set; }

		#region Comparisons
		public static bool operator ==(WeeklyIncomeActionPlanDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is WeeklyIncomeActionPlanDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((WeeklyIncomeActionPlanDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(WeeklyIncomeActionPlanDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is WeeklyIncomeActionPlanDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((WeeklyIncomeActionPlanDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is WeeklyIncomeActionPlanDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((WeeklyIncomeActionPlanDto)obj).Identifier);
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
            return Identifier.ToString();
        }
		#endregion
	}
}
