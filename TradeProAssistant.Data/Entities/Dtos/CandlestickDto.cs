using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class CandlestickDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime Timestamp { get; set; }
		public Decimal Open { get; set; }
		public Decimal High { get; set; }
		public Decimal Low { get; set; }
		public Decimal Close { get; set; }
		public Int32 Volume { get; set; }

		public CandlestickIntervals Interval { get; set; }


		#region Comparisons
		public static bool operator ==(CandlestickDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is CandlestickDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((CandlestickDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(CandlestickDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is CandlestickDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((CandlestickDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is CandlestickDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((CandlestickDto)obj).Identifier);
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
