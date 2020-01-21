using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class OptionDate
	{
		[NotMapped]
		public static OptionDatePropertyNames PropertyNames = new OptionDatePropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime ExpiryDate { get; set; }

		public virtual List<OptionStrike> Strikes { get; set; }

		#region Constructor
		public  OptionDate()
		{
			Strikes = new List<OptionStrike>();
		}

		public  OptionDate(OptionDate source)
		{
			this.ExpiryDate = source.ExpiryDate;
					this.Strikes = source.Strikes.Select(x => new OptionStrike(x)).ToList();
		}
		#endregion
	}
}
