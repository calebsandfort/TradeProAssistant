using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class OptionStrike
	{
		[NotMapped]
		public static OptionStrikePropertyNames PropertyNames = new OptionStrikePropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal StrikePrice { get; set; }

		public int? OptionDateIdentifier { get; set; }
		public virtual OptionDate OptionDate { get; set; }
		public int? CallIdentifier { get; set; }
		public virtual Call Call { get; set; }
		public int? PutIdentifier { get; set; }
		public virtual Put Put { get; set; }

		#region Constructor
		public  OptionStrike()
		{
				}
		#endregion

		#region Comparisons
		public static bool operator ==(OptionStrike entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is OptionStrike && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((OptionStrike)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(OptionStrike entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is OptionStrike && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((OptionStrike)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionStrike && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((OptionStrike)obj).Identifier);
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
