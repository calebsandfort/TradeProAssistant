using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class SimpleWeeklyIncomePlaySheetDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime TimeStamp { get; set; }
		public DateTime Expiry { get; set; }

        public String TimeStampDisplay
        {
            get
            {
                return this.TimeStamp.ToString("g");
            }
        }

        #region Comparisons
        public static bool operator ==(SimpleWeeklyIncomePlaySheetDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is SimpleWeeklyIncomePlaySheetDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((SimpleWeeklyIncomePlaySheetDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(SimpleWeeklyIncomePlaySheetDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is SimpleWeeklyIncomePlaySheetDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((SimpleWeeklyIncomePlaySheetDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is SimpleWeeklyIncomePlaySheetDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((SimpleWeeklyIncomePlaySheetDto)obj).Identifier);
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
