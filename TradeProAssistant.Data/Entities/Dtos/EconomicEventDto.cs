using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class EconomicEventDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime Timestamp { get; set; }

		public EconomicEventTypes EventType { get; set; }


		#region Comparisons
		public static bool operator ==(EconomicEventDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is EconomicEventDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((EconomicEventDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(EconomicEventDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is EconomicEventDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((EconomicEventDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is EconomicEventDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((EconomicEventDto)obj).Identifier);
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
            return Timestamp.ToString();
        }
		#endregion
	}
}
