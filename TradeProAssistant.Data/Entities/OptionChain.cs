using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class OptionChain
	{
		[NotMapped]
		public static OptionChainPropertyNames PropertyNames = new OptionChainPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Date { get; set; }

		[ForeignKey("Security")]
		public int? SecurityIdentifier { get; set; }
		public virtual Security Security { get; set; }


		public virtual List<OptionDate> Dates { get; set; }

		#region Constructor
		public  OptionChain()
		{
			Dates = new List<OptionDate>();
		}

		public  OptionChain(OptionChain source)
		{
			this.Date = source.Date;
					this.SecurityIdentifier = source.SecurityIdentifier;
			this.Dates = source.Dates.Select(x => new OptionDate(x)).ToList();
		}
		#endregion
	}
}
