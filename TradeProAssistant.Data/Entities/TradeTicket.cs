using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	[Table("TradeTickets")]
	public partial class TradeTicket
	{
		[NotMapped]
		public static TradeTicketPropertyNames PropertyNames = new TradeTicketPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Timestamp { get; set; }

		public bool MarketStructureQualified1 { get; set; }

		public bool MarketStructureQualified2 { get; set; }

		public bool MarketStructureQualified3 { get; set; }

		public bool Qualifier1Disqualified { get; set; }

		public bool Qualifier2Disqualified { get; set; }

		public bool Qualifier3Disqualified { get; set; }

		public bool Qualifier4Disqualified { get; set; }

		public String Notes { get; set; }

		public bool Won { get; set; }

		public bool Scratch { get; set; }

		public Decimal PnL { get; set; }

		public int Quantity { get; set; }

		public TradeProAssets Asset { get; set; }

		public Strategies Strategy { get; set; }

		public TradeQualifiers Qualifier1 { get; set; }

		public TradeQualifiers Qualifier2 { get; set; }

		public TradeQualifiers Qualifier3 { get; set; }

		public TradeQualifiers Qualifier4 { get; set; }

		#region Constructor
		public  TradeTicket()
		{
				}

		public  TradeTicket(TradeTicket source)
		{
			this.Timestamp = source.Timestamp;
			this.MarketStructureQualified1 = source.MarketStructureQualified1;
			this.MarketStructureQualified2 = source.MarketStructureQualified2;
			this.MarketStructureQualified3 = source.MarketStructureQualified3;
			this.Qualifier1Disqualified = source.Qualifier1Disqualified;
			this.Qualifier2Disqualified = source.Qualifier2Disqualified;
			this.Qualifier3Disqualified = source.Qualifier3Disqualified;
			this.Qualifier4Disqualified = source.Qualifier4Disqualified;
			this.Notes = source.Notes;
			this.Won = source.Won;
			this.Scratch = source.Scratch;
			this.PnL = source.PnL;
			this.Quantity = source.Quantity;
			this.Asset = source.Asset;
	this.Strategy = source.Strategy;
	this.Qualifier1 = source.Qualifier1;
	this.Qualifier2 = source.Qualifier2;
	this.Qualifier3 = source.Qualifier3;
	this.Qualifier4 = source.Qualifier4;
		}
		#endregion
	}
}
