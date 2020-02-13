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
        #region Settings
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

        #region MinStrikeDiff
        private Decimal minStrikeDiff = 1m;
        [NotMapped]
        public Decimal MinStrikeDiff
        {
            get
            {
                return this.minStrikeDiff;
            }
            set
            {
                this.minStrikeDiff = value;
            }
        }
        #endregion

        #region StrikePadding
        private int strikePadding = 0;
        [NotMapped]
        public int StrikePadding
        {
            get
            {
                return this.strikePadding;
            }
            set
            {
                this.strikePadding = value;
            }
        }
        #endregion
        #endregion

        #region Latest Option Entities
        #region LatestOptionChain
        private OptionChain latestOptionChain = null;
        [NotMapped]
        public OptionChain LatestOptionChain
        {
            get
            {
                if (this.latestOptionChain == null && this.OptionChains.Count > 0)
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
                    this.latestOptionStrikes = this.LatestOptionDate.Strikes.OrderBy(x => x.StrikePrice).ToList();
                }
                else if (this.latestOptionStrikes == null)
                {
                    this.latestOptionStrikes = new List<OptionStrike>();
                }

                return this.latestOptionStrikes;
            }
        }
        #endregion 
        #endregion

        #region Expected Move Props
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

        #region ExpectedMovePtcg
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
        #endregion

        #region Pair Condor
        #region PcLowerBoundStrikeIndex
        private int pcLowerBoundStrikeIndex = -1;
        [NotMapped]
        public int PcLowerBoundStrikeIndex
        {
            get
            {
                if (pcLowerBoundStrikeIndex < 0)
                {
                    OptionStrike strike = this.LatestOptionStrikes.Where(x => x.StrikePrice < this.ExpectedLowerMove).OrderBy(x => x.StrikePrice).Last();
                    pcLowerBoundStrikeIndex = this.LatestOptionStrikes.IndexOf(strike) - this.StrikePadding;
                }

                return pcLowerBoundStrikeIndex;
            }
        }
        #endregion

        #region PcLowerBoundStrike
        private Decimal pcLowerBoundStrike = -1m;
        [NotMapped]
        public Decimal PcLowerBoundStrike
        {
            get
            {
                if (pcLowerBoundStrike < 0m)
                {
                    pcLowerBoundStrike = this.LatestOptionStrikes[this.PcLowerBoundStrikeIndex].StrikePrice;
                }

                return pcLowerBoundStrike;
            }
        }
        #endregion

        #region PcUpperBoundStrikeIndex
        private int pcUpperBoundStrikeIndex = -1;
        [NotMapped]
        public int PcUpperBoundStrikeIndex
        {
            get
            {
                if (pcUpperBoundStrikeIndex < 0)
                {
                    OptionStrike strike = this.LatestOptionStrikes.Where(x => x.StrikePrice > this.ExpectedUpperMove).OrderBy(x => x.StrikePrice).First();
                    pcUpperBoundStrikeIndex = this.LatestOptionStrikes.IndexOf(strike) + this.StrikePadding;
                }

                return pcUpperBoundStrikeIndex;
            }
        }
        #endregion

        #region PcUpperBoundStrike
        private Decimal pcUpperBoundStrike = -1m;
        [NotMapped]
        public Decimal PcUpperBoundStrike
        {
            get
            {
                if (pcUpperBoundStrike < 0m)
                {
                    pcUpperBoundStrike = this.LatestOptionStrikes[this.PcUpperBoundStrikeIndex].StrikePrice;
                }

                return pcUpperBoundStrike;
            }
        }
        #endregion

        #region PcBullPutSpread
        private BullPutSpread pcBullPutSpread = null;
        [NotMapped]
        public BullPutSpread PcBullPutSpread
        {
            get
            {
                if (this.pcBullPutSpread == null && this.OptionChains.Count > 0)
                {
                    this.pcBullPutSpread = new BullPutSpread();
                    this.pcBullPutSpread.SecurityIdentifier = this.Identifier;
                    this.pcBullPutSpread.SellPut = this.LatestOptionStrikes[this.PcLowerBoundStrikeIndex].Put;
                    this.pcBullPutSpread.SellStrike = this.LatestOptionStrikes[this.PcLowerBoundStrikeIndex].StrikePrice;

                    int buyIndex = this.PcLowerBoundStrikeIndex - 1;
                    Decimal strikeDiff = this.LatestOptionStrikes[this.PcLowerBoundStrikeIndex].StrikePrice
                        - this.LatestOptionStrikes[buyIndex].StrikePrice;

                    while (strikeDiff < this.MinStrikeDiff)
                    {
                        buyIndex -= 1;
                        strikeDiff = this.LatestOptionStrikes[this.PcLowerBoundStrikeIndex].StrikePrice
                        - this.LatestOptionStrikes[buyIndex].StrikePrice;
                    }

                    this.pcBullPutSpread.Quantity = (int)Math.Ceiling(this.MaxRisk / strikeDiff);

                    this.pcBullPutSpread.BuyPut = this.LatestOptionStrikes[buyIndex].Put;
                    this.pcBullPutSpread.BuyStrike = this.LatestOptionStrikes[buyIndex].StrikePrice;
                }
                else if (this.pcBullPutSpread == null)
                {
                    this.pcBullPutSpread = new BullPutSpread();
                }

                return this.pcBullPutSpread;
            }

        }
        #endregion

        #region PcBearCallSpread
        private BearCallSpread pcBearCallSpread = null;
        [NotMapped]
        public BearCallSpread PcBearCallSpread
        {
            get
            {
                if (this.pcBearCallSpread == null && this.OptionChains.Count > 0)
                {
                    this.pcBearCallSpread = new BearCallSpread();
                    this.pcBearCallSpread.SecurityIdentifier = this.Identifier;
                    this.pcBearCallSpread.SellCall = this.LatestOptionStrikes[this.PcUpperBoundStrikeIndex].Call;
                    this.pcBearCallSpread.SellStrike = this.LatestOptionStrikes[this.PcUpperBoundStrikeIndex].StrikePrice;

                    int buyIndex = this.PcUpperBoundStrikeIndex + 1;
                    Decimal strikeDiff = this.LatestOptionStrikes[buyIndex].StrikePrice
                        - this.LatestOptionStrikes[this.PcUpperBoundStrikeIndex].StrikePrice;

                    while (strikeDiff < this.MinStrikeDiff)
                    {
                        buyIndex += 1;
                        strikeDiff = this.LatestOptionStrikes[buyIndex].StrikePrice
                            - this.LatestOptionStrikes[this.PcUpperBoundStrikeIndex].StrikePrice;
                    }

                    this.pcBearCallSpread.Quantity = (int)Math.Ceiling(this.MaxRisk / strikeDiff);

                    this.pcBearCallSpread.BuyCall = this.LatestOptionStrikes[buyIndex].Call;
                    this.pcBearCallSpread.BuyStrike = this.LatestOptionStrikes[buyIndex].StrikePrice;
                }
                else if (this.pcBearCallSpread == null)
                {
                    this.pcBearCallSpread = new BearCallSpread();
                }

                return this.pcBearCallSpread;
            }

        }
        #endregion
        #endregion

        #region IC Options Exists
        public bool IcOptionsExist
        {
            get {

                try
                {
                    if(this.IcLowerBoundStrikeIndex > 0
                        && this.IcUpperBoundStrikeIndex > 0)
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        #endregion

        #region Iron Condor
        #region IcLowerBoundStrikeIndex
        private int icLowerBoundStrikeIndex = -1;
        [NotMapped]
        public int IcLowerBoundStrikeIndex
        {
            get
            {
                if (icLowerBoundStrikeIndex < 0)
                {
                    OptionStrike strike = this.LatestOptionStrikes.Where(x => x.StrikePrice <= this.Support).OrderBy(x => x.StrikePrice).Last();
                    icLowerBoundStrikeIndex = this.LatestOptionStrikes.IndexOf(strike) - this.StrikePadding;
                }

                return icLowerBoundStrikeIndex;
            }
        }
        #endregion

        #region IcLowerBoundStrike
        private Decimal icLowerBoundStrike = -1m;
        [NotMapped]
        public Decimal IcLowerBoundStrike
        {
            get
            {
                if (icLowerBoundStrike < 0m)
                {
                    icLowerBoundStrike = this.LatestOptionStrikes[this.IcLowerBoundStrikeIndex].StrikePrice;
                }

                return icLowerBoundStrike;
            }
        }
        #endregion

        #region IcUpperBoundStrikeIndex
        private int icUpperBoundStrikeIndex = -1;
        [NotMapped]
        public int IcUpperBoundStrikeIndex
        {
            get
            {
                if (icUpperBoundStrikeIndex < 0)
                {
                    OptionStrike strike = this.LatestOptionStrikes.Where(x => x.StrikePrice >= this.Resistance).OrderBy(x => x.StrikePrice).First();
                    icUpperBoundStrikeIndex = this.LatestOptionStrikes.IndexOf(strike) + this.StrikePadding;
                }

                return icUpperBoundStrikeIndex;
            }
        }
        #endregion

        #region IcUpperBoundStrike
        private Decimal icUpperBoundStrike = -1m;
        [NotMapped]
        public Decimal IcUpperBoundStrike
        {
            get
            {
                if (icUpperBoundStrike < 0m)
                {
                    icUpperBoundStrike = this.LatestOptionStrikes[this.IcUpperBoundStrikeIndex].StrikePrice;
                }

                return icUpperBoundStrike;
            }
        }
        #endregion

        #region IcBullPutSpread
        private BullPutSpread icBullPutSpread = null;
        [NotMapped]
        public BullPutSpread IcBullPutSpread
        {
            get
            {
                if (this.icBullPutSpread == null && this.OptionChains.Count > 0 && this.IronCondorEligible && this.Support > 0)
                {
                    this.icBullPutSpread = new BullPutSpread();
                    this.icBullPutSpread.SecurityIdentifier = this.Identifier;
                    this.icBullPutSpread.SellPut = this.LatestOptionStrikes[this.IcLowerBoundStrikeIndex].Put;
                    this.icBullPutSpread.SellStrike = this.LatestOptionStrikes[this.IcLowerBoundStrikeIndex].StrikePrice;

                    int buyIndex = this.IcLowerBoundStrikeIndex - 1;
                    Decimal strikeDiff = this.LatestOptionStrikes[this.IcLowerBoundStrikeIndex].StrikePrice
                        - this.LatestOptionStrikes[buyIndex].StrikePrice;

                    while (strikeDiff < this.MinStrikeDiff)
                    {
                        buyIndex -= 1;
                        strikeDiff = this.LatestOptionStrikes[this.IcLowerBoundStrikeIndex].StrikePrice
                        - this.LatestOptionStrikes[buyIndex].StrikePrice;
                    }

                    this.icBullPutSpread.Quantity = (int)Math.Ceiling((this.MaxRisk * 2) / strikeDiff);

                    this.icBullPutSpread.BuyPut = this.LatestOptionStrikes[buyIndex].Put;
                    this.icBullPutSpread.BuyStrike = this.LatestOptionStrikes[buyIndex].StrikePrice;
                }
                else if (this.icBullPutSpread == null)
                {
                    this.icBullPutSpread = new BullPutSpread();
                }

                return this.icBullPutSpread;
            }

        }
        #endregion

        #region IcBearCallSpread
        private BearCallSpread icBearCallSpread = null;
        [NotMapped]
        public BearCallSpread IcBearCallSpread
        {
            get
            {
                if (this.icBearCallSpread == null && this.OptionChains.Count > 0 && this.IronCondorEligible && this.Resistance > 0)
                {
                    this.icBearCallSpread = new BearCallSpread();
                    this.icBearCallSpread.SecurityIdentifier = this.Identifier;
                    this.icBearCallSpread.SellCall = this.LatestOptionStrikes[this.IcUpperBoundStrikeIndex].Call;
                    this.icBearCallSpread.SellStrike = this.LatestOptionStrikes[this.IcUpperBoundStrikeIndex].StrikePrice;

                    int buyIndex = this.IcUpperBoundStrikeIndex + 1;
                    Decimal strikeDiff = this.LatestOptionStrikes[buyIndex].StrikePrice
                        - this.LatestOptionStrikes[this.IcUpperBoundStrikeIndex].StrikePrice;

                    while (strikeDiff < this.MinStrikeDiff)
                    {
                        buyIndex += 1;
                        strikeDiff = this.LatestOptionStrikes[buyIndex].StrikePrice
                            - this.LatestOptionStrikes[this.IcUpperBoundStrikeIndex].StrikePrice;
                    }

                    this.icBearCallSpread.Quantity = (int)Math.Ceiling((this.MaxRisk * 2) / strikeDiff);

                    this.icBearCallSpread.BuyCall = this.LatestOptionStrikes[buyIndex].Call;
                    this.icBearCallSpread.BuyStrike = this.LatestOptionStrikes[buyIndex].StrikePrice;
                }
                else if (this.icBearCallSpread == null)
                {
                    this.icBearCallSpread = new BearCallSpread();
                }

                return this.icBearCallSpread;
            }

        }
        #endregion 
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
