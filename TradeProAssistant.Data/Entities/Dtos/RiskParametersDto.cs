using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class RiskParametersDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public String Name { get; set; }
		public bool Active { get; set; }
		public Decimal DailyTarget { get; set; }
		public Decimal WeeklyTarget { get; set; }
		public Decimal MonthlyTarget { get; set; }
		public Decimal DailyStop { get; set; }
		public Decimal WeeklyStop { get; set; }
		public Decimal MonthlyStop { get; set; }

		#region Comparisons
		public static bool operator ==(RiskParametersDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is RiskParametersDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((RiskParametersDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(RiskParametersDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is RiskParametersDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((RiskParametersDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is RiskParametersDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((RiskParametersDto)obj).Identifier);
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
