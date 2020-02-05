using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class PairCondor
	{
        #region Custom Properties
        #region Credit
        private Decimal credit = -1m;
        [NotMapped]
        public Decimal Credit
        {
            get
            {
                if (credit < 0m)
                {
                    credit = this.BullPutSpread.Credit + this.BearCallSpread.Credit;
                }

                return credit;
            }
        }
        #endregion

        #region Risk
        private Decimal risk = -1m;
        [NotMapped]
        public Decimal Risk
        {
            get
            {
                if (risk < 0m)
                {
                    switch (this.Strategy)
                    {
                        case Enums.StrategyTypes.PairCondor:
                            risk = this.BullPutSpread.Risk + this.BearCallSpread.Risk;
                            break;
                        case Enums.StrategyTypes.IronCondor:
                            risk = Math.Max(this.BullPutSpread.CapitalRequirement, this.BearCallSpread.CapitalRequirement);
                            break;
                    }

                    
                }

                return risk;
            }
        }
        #endregion

        #region RequiredCapital
        private Decimal requiredCapital = -1m;
        [NotMapped]
        public Decimal RequiredCapital
        {
            get
            {
                if (requiredCapital < 0m)
                {
                    switch (this.Strategy)
                    {
                        case Enums.StrategyTypes.PairCondor:
                            requiredCapital = this.Risk + this.Credit;
                            break;
                        case Enums.StrategyTypes.IronCondor:
                            requiredCapital = this.Risk;
                            break;
                    }
                   
                }

                return requiredCapital;
            }
        }
        #endregion
        #endregion

        #region Custom Methods

        #endregion

        #region Comparisons
        public static bool operator ==(PairCondor entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is PairCondor && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((PairCondor)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(PairCondor entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is PairCondor && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((PairCondor)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is PairCondor && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((PairCondor)obj).Identifier);
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
