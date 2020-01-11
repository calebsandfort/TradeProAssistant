using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class OptionDate
	{
		[NotMapped]
		public static OptionDatePropertyNames PropertyNames = new OptionDatePropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime ExpiryDate { get; set; }

		public virtual List<OptionStrike> Strikes { get; set; }

		#region Constructor
		public  OptionDate()
		{
			Strikes = new List<OptionStrike>();
		}
		#endregion

		#region Comparisons
		public static bool operator ==(OptionDate entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is OptionDate && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((OptionDate)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(OptionDate entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is OptionDate && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((OptionDate)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionDate && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((OptionDate)obj).Identifier);
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
	}
}
