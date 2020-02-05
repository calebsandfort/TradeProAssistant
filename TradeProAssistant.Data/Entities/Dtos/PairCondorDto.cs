using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class PairCondorDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public Decimal Credit { get; set; }
		public Decimal Risk { get; set; }
		public Decimal RequiredCapital { get; set; }

		public Sectors SectorEnum { get; set; }

		public StrategyTypes Strategy { get; set; }

		public BullPutSpreadDto BullPutSpread { get; set; }
		public BearCallSpreadDto BearCallSpread { get; set; }

		#region Comparisons
		public static bool operator ==(PairCondorDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is PairCondorDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((PairCondorDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(PairCondorDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is PairCondorDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((PairCondorDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is PairCondorDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((PairCondorDto)obj).Identifier);
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
