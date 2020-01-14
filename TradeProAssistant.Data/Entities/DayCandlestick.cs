using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class DayCandlestick
	{
		[NotMapped]
		public static DayCandlestickPropertyNames PropertyNames = new DayCandlestickPropertyNames();

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
		public  DayCandlestick()
		{
				}
		#endregion
	}
}
