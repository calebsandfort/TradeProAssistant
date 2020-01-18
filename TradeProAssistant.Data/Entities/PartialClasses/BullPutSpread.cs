﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class BullPutSpread
	{
        #region Custom Properties
        #region Bid
        private Decimal bid = -1m;
        [NotMapped]
        public Decimal Bid
        {
            get
            {
                if (bid < 0m)
                {
                    bid = this.SellPut.Bid - this.BuyPut.Ask;
                }

                return bid;
            }
        }
        #endregion

        #region Ask
        private Decimal ask = -1m;
        [NotMapped]
        public Decimal Ask
        {
            get
            {
                if (ask < 0m)
                {
                    ask = this.SellPut.Ask - this.BuyPut.Bid;
                }

                return ask;
            }
        }
        #endregion

        #region Mid
        private Decimal mid = -1m;
        [NotMapped]
        public Decimal Mid
        {
            get
            {
                if (mid < 0m)
                {
                    mid = (this.Bid + this.Ask) / 2;
                }

                return mid;
            }
        }
        #endregion

        #region Credit
        private Decimal credit = -1m;
        [NotMapped]
        public Decimal Credit
        {
            get
            {
                if (credit < 0m)
                {
                    credit = this.Mid * this.Quantity * 100;
                    credit -= (this.Quantity * 2m * .65m);
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
                    risk = ((this.SellStrike - this.BuyStrike) * this.Quantity * 100) - this.Credit;
                }

                return risk;
            }
        }
        #endregion
        #endregion

        #region Custom Methods

        #endregion

        #region Comparisons
        public static bool operator ==(BullPutSpread entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is BullPutSpread && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((BullPutSpread)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(BullPutSpread entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is BullPutSpread && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((BullPutSpread)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is BullPutSpread && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((BullPutSpread)obj).Identifier);
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
            return $"BPS: {this.SellStrike:C}/{this.BuyStrike:C}/{this.Credit:C}/{this.Risk:C}";
        }
        #endregion
    }
}
