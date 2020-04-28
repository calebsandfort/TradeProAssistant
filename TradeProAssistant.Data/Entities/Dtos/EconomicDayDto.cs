using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class EconomicDayDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime Date { get; set; }

		public List<EconomicEventDto> Events { get; set; }

		#region Comparisons
		public static bool operator ==(EconomicDayDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is EconomicDayDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((EconomicDayDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(EconomicDayDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is EconomicDayDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((EconomicDayDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is EconomicDayDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((EconomicDayDto)obj).Identifier);
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
            return Date.ToString();
        }
		#endregion
	}
}
