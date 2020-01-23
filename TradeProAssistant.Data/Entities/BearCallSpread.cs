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

		[ForeignKey("SellCall")]
		public int? SellCallIdentifier { get; set; }
		public virtual Call SellCall { get; set; }

		[ForeignKey("BuyCall")]
		public int? BuyCallIdentifier { get; set; }
		public virtual Call BuyCall { get; set; }

		[ForeignKey("Security")]
		public int? SecurityIdentifier { get; set; }
		public virtual Security Security { get; set; }


		#region Constructor
		public  BearCallSpread()
		{
				}

		public  BearCallSpread(BearCallSpread source)
		{
			this.Quantity = source.Quantity;
			this.SellStrike = source.SellStrike;
			this.BuyStrike = source.BuyStrike;
					if(source.SellCall != null) this.SellCall = new Call(source.SellCall);
			if(source.BuyCall != null) this.BuyCall = new Call(source.BuyCall);
			this.SecurityIdentifier = source.SecurityIdentifier;
		}
		#endregion
	}
}
