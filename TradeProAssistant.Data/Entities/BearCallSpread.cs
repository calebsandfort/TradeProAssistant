using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class BearCallSpread
	{
		[NotMapped]
		public static BearCallSpreadPropertyNames PropertyNames = new BearCallSpreadPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Int32 Quantity { get; set; }

		public Decimal SellStrike { get; set; }

		public Decimal BuyStrike { get; set; }

		public int? SellCallIdentifier { get; set; }
		public virtual Call SellCall { get; set; }
		public int? BuyCallIdentifier { get; set; }
		public virtual Call BuyCall { get; set; }
		public int? SecurityIdentifier { get; set; }
		public virtual Security Security { get; set; }

		#region Constructor
		public  BearCallSpread()
		{
				}
		#endregion
	}
}
