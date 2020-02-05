using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class WeeklyIncomeActionPlan
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
                    credit = this.Pairs.Sum(x => x.Credit);
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
                    switch (this.PlaySheet.Strategy)
                    {
                        case Enums.StrategyTypes.PairCondor:
                            risk = this.Pairs.Sum(x => x.Risk);
                            break;
                        case Enums.StrategyTypes.IronCondor:
                            risk = this.Pairs.Sum(x => x.Risk) - this.Pairs.Sum(x => x.Credit);
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
                    switch (this.PlaySheet.Strategy)
                    {
                        case Enums.StrategyTypes.PairCondor:
                            requiredCapital = this.Risk + this.Credit;
                            break;
                        case Enums.StrategyTypes.IronCondor:
                            requiredCapital = this.Pairs.Sum(x => x.Risk);
                            break;
                    }
                }

                return requiredCapital;
            }
        }
        #endregion
        #endregion

        #region Custom Methods
        public bool AddPairCondor(PairCondor pairCondor)
        {
            if(this.Pairs.Any(x => x.BullPutSpread.SecurityIdentifier == pairCondor.BullPutSpread.SecurityIdentifier
            || x.BullPutSpread.SecurityIdentifier == pairCondor.BearCallSpread.SecurityIdentifier
            || x.BearCallSpread.SecurityIdentifier == pairCondor.BullPutSpread.SecurityIdentifier
            || x.BearCallSpread.SecurityIdentifier == pairCondor.BearCallSpread.SecurityIdentifier))
            {
                return false;
            }
            else
            {
                this.Pairs.Add(pairCondor);
                return true;
            }
        }
        #endregion

        #region Comparisons
        public static bool operator ==(WeeklyIncomeActionPlan entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is WeeklyIncomeActionPlan && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((WeeklyIncomeActionPlan)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(WeeklyIncomeActionPlan entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is WeeklyIncomeActionPlan && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((WeeklyIncomeActionPlan)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is WeeklyIncomeActionPlan && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((WeeklyIncomeActionPlan)obj).Identifier);
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
