using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services;

namespace Entities
{
	public partial class Security
	{
        #region Custom Properties
        #region MaxRisk
        private Decimal maxRisk = 5m;
        [NotMapped]
        public Decimal MaxRisk
        {
            get
            {
                return this.maxRisk;
            }
            set
            {
                this.maxRisk = value;
            }
        }
        #endregion

        #region LatestOptionChain
        private OptionChain latestOptionChain = null;
        [NotMapped]
        public OptionChain LatestOptionChain
        {
            get
            {
                if(this.latestOptionChain == null && this.OptionChains.Count > 0)
                {
                    this.latestOptionChain = this.OptionChains.OrderByDescending(x => x.Identifier).First();
                }
                else if (this.latestOptionChain == null)
                {
                    this.latestOptionChain = new OptionChain();
                }

                return this.latestOptionChain;
            }

        }
        #endregion

        #region LatestOptionDate
        private OptionDate latestOptionDate = null;
        [NotMapped]
        public OptionDate LatestOptionDate
        {
            get
            {
                if (this.latestOptionDate == null && this.LatestOptionChain.Dates.Count > 0)
                {
                    this.latestOptionDate = this.LatestOptionChain.Dates.OrderByDescending(x => x.ExpiryDate).First();
                }
                else if (this.latestOptionDate == null)
                {
                    this.latestOptionDate = new OptionDate();
                }

                return this.latestOptionDate;
            }

        }
        #endregion

        #region LatestOptionStrikes
        private List<OptionStrike> latestOptionStrikes = null;
        [NotMapped]
        public List<OptionStrike> LatestOptionStrikes
        {
            get
            {
                if (this.latestOptionStrikes == null && this.LatestOptionDate.Strikes.Count > 0)
                {
                    this.latestOptionStrikes = this.LatestOptionDate.Strikes;
                }
                else if (this.latestOptionStrikes == null)
                {
                    this.latestOptionStrikes = new List<OptionStrike>();
                }

                return this.latestOptionStrikes;
            }
        }
        #endregion

        #region ExpectedMove
        private Decimal expectedMove = -1m;
        [NotMapped]
        public Decimal ExpectedMove
        {
            get
            {
                if (expectedMove < 0m)
                {
                    if (this.LatestOptionStrikes.Count > 0)
                    {
                        if (this.LatestOptionStrikes.Any(x => x.StrikePrice == this.CurrentPrice))
                        {
                            OptionStrike optionStrike = this.LatestOptionStrikes.First(x => x.StrikePrice == this.CurrentPrice);
                            expectedMove = (optionStrike.Call.Mid + optionStrike.Put.Mid + optionStrike.Call.Mid + optionStrike.Put.Mid) / 2;
                        }
                        else
                        {
                            OptionStrike lowStrike = this.LatestOptionStrikes.Where(x => x.StrikePrice < this.CurrentPrice).OrderBy(x => x.StrikePrice).Last();
                            OptionStrike highStrike = this.LatestOptionStrikes.Where(x => x.StrikePrice > this.CurrentPrice).OrderBy(x => x.StrikePrice).First();
                            expectedMove = (lowStrike.Call.Mid + lowStrike.Put.Mid + highStrike.Call.Mid + highStrike.Put.Mid) / 2;
                        }
                    }
                }

                return expectedMove;
            }
        }
        #endregion

        #region ExpectedMove
        private Decimal expectedMovePtcg = -1m;
        [NotMapped]
        public Decimal ExpectedMovePtcg
        {
            get
            {
                if (expectedMovePtcg < 0m)
                {
                    expectedMovePtcg = this.ExpectedMove / this.CurrentPrice;
                }

                return expectedMovePtcg;
            }
        }
        #endregion

        #region ExpectedLowerMove
        private Decimal expectedLowerMove = -1m;
        [NotMapped]
        public Decimal ExpectedLowerMove
        {
            get
            {
                if (expectedLowerMove < 0m)
                {
                    expectedLowerMove = this.CurrentPrice - this.ExpectedMove;
                }

                return expectedLowerMove;
            }
        }
        #endregion

        #region LowerBoundStrikeIndex
        private int lowerBoundStrikeIndex = -1;
        [NotMapped]
        public int LowerBoundStrikeIndex
        {
            get
            {
                if (lowerBoundStrikeIndex < 0)
                {
                    OptionStrike strike = this.LatestOptionStrikes.Where(x => x.StrikePrice < this.ExpectedLowerMove).OrderBy(x => x.StrikePrice).Last();
                    lowerBoundStrikeIndex = this.LatestOptionStrikes.IndexOf(strike);
                }

                return lowerBoundStrikeIndex;
            }
        }
        #endregion

        #region LowerBoundStrike
        private Decimal lowerBoundStrike = -1m;
        [NotMapped]
        public Decimal LowerBoundStrike
        {
            get
            {
                if (lowerBoundStrike < 0m)
                {
                    lowerBoundStrike = this.LatestOptionStrikes[this.LowerBoundStrikeIndex].StrikePrice;
                }

                return lowerBoundStrike;
            }
        }
        #endregion

        #region ExpectedUpperMove
        private Decimal expectedUpperMove = -1m;
        [NotMapped]
        public Decimal ExpectedUpperMove
        {
            get
            {
                if (expectedUpperMove < 0m)
                {
                    expectedUpperMove = this.CurrentPrice + this.ExpectedMove;
                }

                return expectedUpperMove;
            }
        }
        #endregion

        #region UpperBoundStrikeIndex
        private int upperBoundStrikeIndex = -1;
        [NotMapped]
        public int UpperBoundStrikeIndex
        {
            get
            {
                if (upperBoundStrikeIndex < 0)
                {
                    OptionStrike strike = this.LatestOptionStrikes.Where(x => x.StrikePrice > this.ExpectedUpperMove).OrderBy(x => x.StrikePrice).First();
                    upperBoundStrikeIndex = this.LatestOptionStrikes.IndexOf(strike);
                }

                return upperBoundStrikeIndex;
            }
        }
        #endregion

        #region UpperBoundStrike
        private Decimal upperBoundStrike = -1m;
        [NotMapped]
        public Decimal UpperBoundStrike
        {
            get
            {
                if (upperBoundStrike < 0m)
                {
                    upperBoundStrike = this.LatestOptionStrikes[this.UpperBoundStrikeIndex].StrikePrice;
                }

                return upperBoundStrike;
            }
        }
        #endregion

        #region BullPutSpread
        private BullPutSpread bullPutSpread = null;
        [NotMapped]
        public BullPutSpread BullPutSpread
        {
            get
            {
                if (this.bullPutSpread == null && this.OptionChains.Count > 0)
                {
                    this.bullPutSpread = new BullPutSpread();
                    this.bullPutSpread.SecurityIdentifier = this.Identifier;
                    this.bullPutSpread.SellPut = this.LatestOptionStrikes[this.LowerBoundStrikeIndex].Put;
                    this.bullPutSpread.SellStrike = this.LatestOptionStrikes[this.LowerBoundStrikeIndex].StrikePrice;

                    int buyIndex = this.LowerBoundStrikeIndex - 1;
                    Decimal strikeDiff = this.LatestOptionStrikes[this.LowerBoundStrikeIndex].StrikePrice
                        - this.LatestOptionStrikes[buyIndex].StrikePrice;

                    while(strikeDiff < 1m)
                    {
                        buyIndex -= 1;
                        strikeDiff = this.LatestOptionStrikes[this.LowerBoundStrikeIndex].StrikePrice
                        - this.LatestOptionStrikes[buyIndex].StrikePrice;
                    }

                    this.bullPutSpread.Quantity = (int)Math.Ceiling(this.MaxRisk / strikeDiff);

                    this.bullPutSpread.BuyPut = this.LatestOptionStrikes[buyIndex].Put;
                    this.bullPutSpread.BuyStrike = this.LatestOptionStrikes[buyIndex].StrikePrice;
                }
                else if (this.bullPutSpread == null)
                {
                    this.bullPutSpread = new BullPutSpread();
                }

                return this.bullPutSpread;
            }

        }
        #endregion

        #region BearCallSpread
        private BearCallSpread bearCallSpread = null;
        [NotMapped]
        public BearCallSpread BearCallSpread
        {
            get
            {
                if (this.bearCallSpread == null && this.OptionChains.Count > 0)
                {
                    this.bearCallSpread = new BearCallSpread();
                    this.bearCallSpread.SecurityIdentifier = this.Identifier;
                    this.bearCallSpread.SellCall = this.LatestOptionStrikes[this.UpperBoundStrikeIndex].Call;
                    this.bearCallSpread.SellStrike = this.LatestOptionStrikes[this.UpperBoundStrikeIndex].StrikePrice;

                    int buyIndex = this.UpperBoundStrikeIndex + 1;
                    Decimal strikeDiff = this.LatestOptionStrikes[buyIndex].StrikePrice
                        - this.LatestOptionStrikes[this.UpperBoundStrikeIndex].StrikePrice;

                    while (strikeDiff < 1m)
                    {
                        buyIndex += 1;
                        strikeDiff = this.LatestOptionStrikes[buyIndex].StrikePrice
                            - this.LatestOptionStrikes[this.UpperBoundStrikeIndex].StrikePrice;
                    }

                    this.bearCallSpread.Quantity = (int)Math.Ceiling(this.MaxRisk / strikeDiff);

                    this.bearCallSpread.BuyCall= this.LatestOptionStrikes[buyIndex].Call;
                    this.bearCallSpread.BuyStrike = this.LatestOptionStrikes[buyIndex].StrikePrice;
                }
                else if (this.bearCallSpread == null)
                {
                    this.bearCallSpread = new BearCallSpread();
                }

                return this.bearCallSpread;
            }

        }
        #endregion
        #endregion

        #region Custom Methods

        #endregion

        #region Comparisons
        public static bool operator ==(Security entity, object obj)
        {
            if ((object)entity == null && obj == null)
            {
                return true;
            }
            else if ((object)entity != null && obj is Security && entity.GetType() == obj.GetType())
            {
                return (entity.Identifier == ((Security)obj).Identifier);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Security entity, object obj)
        {
            if ((object)entity == null && obj == null)
            {
                return false;
            }
            else if ((object)entity != null && obj is Security && entity.GetType() == obj.GetType())
            {
                return (entity.Identifier != ((Security)obj).Identifier);
            }
            else
            {
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Security && this.GetType() == obj.GetType())
            {
                return (this.Identifier == ((Security)obj).Identifier);
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
            return Name.ToString();
        }
        #endregion
    }
}
