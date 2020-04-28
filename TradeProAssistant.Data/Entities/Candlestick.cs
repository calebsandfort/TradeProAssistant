using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	public partial class Candlestick
	{
		[NotMapped]
		public static CandlestickPropertyNames PropertyNames = new CandlestickPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Timestamp { get; set; }

		public Decimal Open { get; set; }

		public Decimal High { get; set; }

		public Decimal Low { get; set; }

		public Decimal Close { get; set; }

		public Int32 Volume { get; set; }

		public CandlestickIntervals Interval { get; set; }

		[ForeignKey("FutureContract")]
		public int? FutureContractIdentifier { get; set; }
		public virtual FutureContract FutureContract { get; set; }


		#region Constructor
		public  Candlestick()
		{
				}

		public  Candlestick(Candlestick source)
		{
			this.Timestamp = source.Timestamp;
			this.Open = source.Open;
			this.High = source.High;
			this.Low = source.Low;
			this.Close = source.Close;
			this.Volume = source.Volume;
			this.Interval = source.Interval;
			if(source.FutureContract != null) this.FutureContract = new FutureContract(source.FutureContract);
		}
		#endregion
	}
}
