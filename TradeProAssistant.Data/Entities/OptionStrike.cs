using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class OptionStrike
	{
		[NotMapped]
		public static OptionStrikePropertyNames PropertyNames = new OptionStrikePropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal StrikePrice { get; set; }

		[ForeignKey("OptionDate")]
		public int? OptionDateIdentifier { get; set; }
		public virtual OptionDate OptionDate { get; set; }
		[ForeignKey("Call")]
		public int? CallIdentifier { get; set; }
		public virtual Call Call { get; set; }
		[ForeignKey("Put")]
		public int? PutIdentifier { get; set; }
		public virtual Put Put { get; set; }

		#region Constructor
		public  OptionStrike()
		{
				}

		public  OptionStrike(OptionStrike source)
		{
			this.StrikePrice = source.StrikePrice;
					if(source.OptionDate != null) this.OptionDate = new OptionDate(source.OptionDate);
			if(source.Call != null) this.Call = new Call(source.Call);
			if(source.Put != null) this.Put = new Put(source.Put);
		}
		#endregion
	}
}
