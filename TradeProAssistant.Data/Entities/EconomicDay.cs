using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class EconomicDay
	{
		[NotMapped]
		public static EconomicDayPropertyNames PropertyNames = new EconomicDayPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Date { get; set; }

		public virtual List<EconomicEvent> Events { get; set; }

		#region Constructor
		public  EconomicDay()
		{
			Events = new List<EconomicEvent>();
		}

		public  EconomicDay(EconomicDay source)
		{
			this.Date = source.Date;
					this.Events = source.Events.Select(x => new EconomicEvent(x)).ToList();
		}
		#endregion
	}
}
