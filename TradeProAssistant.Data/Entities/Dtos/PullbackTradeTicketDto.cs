using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class PullbackTradeTicketDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public DateTime Timestamp { get; set; }
		public bool ZoneQualified { get; set; }
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

		public TradeQualifiers Qualifier1 { get; set; }

		public TradeQualifiers Qualifier2 { get; set; }

		public TradeQualifiers Qualifier3 { get; set; }

		public TradeQualifiers Qualifier4 { get; set; }

		#region Comparisons
		public static bool operator ==(PullbackTradeTicketDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is PullbackTradeTicketDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((PullbackTradeTicketDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(PullbackTradeTicketDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is PullbackTradeTicketDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((PullbackTradeTicketDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is PullbackTradeTicketDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((PullbackTradeTicketDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion

		#region ToString
		public override string ToString()
        {
            return Timestamp.ToString();
        }
		#endregion
	}
}
