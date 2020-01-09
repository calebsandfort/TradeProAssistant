using System;

namespace Entities.Dtos
{
	public class DayCandlestickDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime Date { get; set; }
		public Decimal Open { get; set; }
		public Decimal High { get; set; }
		public Decimal Low { get; set; }
		public Decimal Close { get; set; }
		public Int32 Volume { get; set; }

		#region Comparisons
		public static bool operator ==(DayCandlestickDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is DayCandlestickDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((DayCandlestickDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(DayCandlestickDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is DayCandlestickDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((DayCandlestickDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is DayCandlestickDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((DayCandlestickDto)obj).Identifier);
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
	}
}
