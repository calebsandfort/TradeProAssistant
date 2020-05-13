using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class TradingSettings
	{
		[NotMapped]
		public static TradingSettingsPropertyNames PropertyNames = new TradingSettingsPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime MonthStart { get; set; }

		[ForeignKey("RiskParameters")]
		public int? RiskParametersIdentifier { get; set; }
		public virtual RiskParameters RiskParameters { get; set; }


		#region Constructor
		public  TradingSettings()
		{
				}

		public  TradingSettings(TradingSettings source)
		{
			this.MonthStart = source.MonthStart;
					if(source.RiskParameters != null) this.RiskParameters = new RiskParameters(source.RiskParameters);
		}
		#endregion
	}
}
