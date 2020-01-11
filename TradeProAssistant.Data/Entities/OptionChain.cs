using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class OptionChain
	{
		[NotMapped]
		public static OptionChainPropertyNames PropertyNames = new OptionChainPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime Date { get; set; }

		public int? SecurityIdentifier { get; set; }
		public virtual Security Security { get; set; }

		public virtual List<OptionDate> Dates { get; set; }

		#region Constructor
		public  OptionChain()
		{
			Dates = new List<OptionDate>();
		}
		#endregion

		#region Comparisons
		public static bool operator ==(OptionChain entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is OptionChain && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((OptionChain)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(OptionChain entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is OptionChain && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((OptionChain)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionChain && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((OptionChain)obj).Identifier);
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
