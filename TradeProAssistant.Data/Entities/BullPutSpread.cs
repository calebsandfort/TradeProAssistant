using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class BullPutSpread
	{
		[NotMapped]
		public static BullPutSpreadPropertyNames PropertyNames = new BullPutSpreadPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Int32 Quantity { get; set; }

		public Decimal SellStrike { get; set; }

		public Decimal BuyStrike { get; set; }

		[ForeignKey("SellPut")]
		public int? SellPutIdentifier { get; set; }
		public virtual Put SellPut { get; set; }

		[ForeignKey("BuyPut")]
		public int? BuyPutIdentifier { get; set; }
		public virtual Put BuyPut { get; set; }

		[ForeignKey("Security")]
		public int? SecurityIdentifier { get; set; }
		public virtual Security Security { get; set; }


		#region Constructor
		public  BullPutSpread()
		{
				}

		public  BullPutSpread(BullPutSpread source)
		{
			this.Quantity = source.Quantity;
			this.SellStrike = source.SellStrike;
			this.BuyStrike = source.BuyStrike;
					if(source.SellPut != null) this.SellPut = new Put(source.SellPut);
			if(source.BuyPut != null) this.BuyPut = new Put(source.BuyPut);
			this.SecurityIdentifier = source.SecurityIdentifier;
		}
		#endregion
	}
}
