﻿using System;
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

		public Decimal Credit { get; set; }

		public Decimal Risk { get; set; }

		public Int32 Quantity { get; set; }

		public Decimal SellStrike { get; set; }

		public Decimal BuyStrike { get; set; }

		public int? SecurityIdentifier { get; set; }
		public virtual Security Security { get; set; }

		#region Constructor
		public  BullPutSpread()
		{
				}
		#endregion
	}
}
