using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class WeeklyIncomeComboCountDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Int32 Count { get; set; }

		public Sectors SectorEnum { get; set; }


		#region Comparisons
		public static bool operator ==(WeeklyIncomeComboCountDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is WeeklyIncomeComboCountDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((WeeklyIncomeComboCountDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(WeeklyIncomeComboCountDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is WeeklyIncomeComboCountDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((WeeklyIncomeComboCountDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is WeeklyIncomeComboCountDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((WeeklyIncomeComboCountDto)obj).Identifier);
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
            return Count.ToString();
        }
		#endregion
	}
}
