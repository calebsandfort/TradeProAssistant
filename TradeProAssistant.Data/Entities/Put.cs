using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class Put
	{
		[NotMapped]
		public static PutPropertyNames PropertyNames = new PutPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal Bid { get; set; }

		public Decimal Ask { get; set; }

		#region Constructor
		public  Put()
		{
				}
		#endregion

		#region Comparisons
		public static bool operator ==(Put entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is Put && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((Put)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(Put entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is Put && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((Put)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is Put && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((Put)obj).Identifier);
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
