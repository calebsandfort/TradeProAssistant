using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	public partial class EconomicEvent
	{
		[NotMapped]
		public static EconomicEventPropertyNames PropertyNames = new EconomicEventPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Timestamp { get; set; }

		public EconomicEventTypes EventType { get; set; }

		[ForeignKey("EconomicDay")]
		public int? EconomicDayIdentifier { get; set; }
		public virtual EconomicDay EconomicDay { get; set; }


		#region Constructor
		public  EconomicEvent()
		{
				}

		public  EconomicEvent(EconomicEvent source)
		{
			this.Timestamp = source.Timestamp;
			this.EventType = source.EventType;
			if(source.EconomicDay != null) this.EconomicDay = new EconomicDay(source.EconomicDay);
		}
		#endregion
	}
}
