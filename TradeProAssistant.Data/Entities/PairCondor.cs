using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	public partial class PairCondor
	{
		[NotMapped]
		public static PairCondorPropertyNames PropertyNames = new PairCondorPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal Credit { get; set; }

		public Decimal Risk { get; set; }

		public Sectors SectorEnum { get; set; }

		public int? BullPutSpreadIdentifier { get; set; }
		public virtual BullPutSpread BullPutSpread { get; set; }
		public int? BearCallSpreadIdentifier { get; set; }
		public virtual BearCallSpread BearCallSpread { get; set; }

		#region Constructor
		public  PairCondor()
		{
				}
		#endregion
	}
}
