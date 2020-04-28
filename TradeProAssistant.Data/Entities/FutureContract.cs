using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class FutureContract
	{
		[NotMapped]
		public static FutureContractPropertyNames PropertyNames = new FutureContractPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		[MaxLength(255)]
		public String Name { get; set; }

		[MaxLength(2)]
		public String Symbol { get; set; }

		public Decimal TickValue { get; set; }

		public Decimal TickSize { get; set; }

		public virtual List<Candlestick> Candlesticks { get; set; }

		#region Constructor
		public  FutureContract()
		{
			Candlesticks = new List<Candlestick>();
		}

		public  FutureContract(FutureContract source)
		{
			this.Name = source.Name;
			this.Symbol = source.Symbol;
			this.TickValue = source.TickValue;
			this.TickSize = source.TickSize;
					this.Candlesticks = source.Candlesticks.Select(x => new Candlestick(x)).ToList();
		}
		#endregion
	}
}
