using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
	public class BullPutSpreadDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Int32 Quantity { get; set; }
		public Decimal SellStrike { get; set; }
		public Decimal BuyStrike { get; set; }
		public Decimal Credit { get; set; }
		public Decimal Risk { get; set; }

		public PutDto SellPut { get; set; }
		public PutDto BuyPut { get; set; }
		public SecurityDto Security { get; set; }

		#region Comparisons
		public static bool operator ==(BullPutSpreadDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is BullPutSpreadDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((BullPutSpreadDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(BullPutSpreadDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is BullPutSpreadDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((BullPutSpreadDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is BullPutSpreadDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((BullPutSpreadDto)obj).Identifier);
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
            return Identifier.ToString();
        }
		#endregion
	}
}
