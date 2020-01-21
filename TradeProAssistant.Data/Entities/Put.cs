using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class Put
	{
		[NotMapped]
		public static PutPropertyNames PropertyNames = new PutPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal Bid { get; set; }

		public Decimal Ask { get; set; }

		#region Constructor
		public  Put()
		{
				}

		public  Put(Put source)
		{
			this.Bid = source.Bid;
			this.Ask = source.Ask;
				}
		#endregion
	}
}
