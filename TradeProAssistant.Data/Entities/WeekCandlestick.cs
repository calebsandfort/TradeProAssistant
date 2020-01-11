using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class WeekCandlestick
	{
		[NotMapped]
		public static WeekCandlestickPropertyNames PropertyNames = new WeekCandlestickPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Date { get; set; }

		public Decimal Open { get; set; }

		public Decimal High { get; set; }

		public Decimal Low { get; set; }

		public Decimal Close { get; set; }

		public Int32 Volume { get; set; }

		#region Constructor
		public  WeekCandlestick()
		{
				}
		#endregion

		#region Comparisons
		public static bool operator ==(WeekCandlestick entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is WeekCandlestick && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((WeekCandlestick)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(WeekCandlestick entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is WeekCandlestick && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((WeekCandlestick)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is WeekCandlestick && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((WeekCandlestick)obj).Identifier);
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
