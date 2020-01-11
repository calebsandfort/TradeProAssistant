using System;

namespace Entities
{
	public class PutPropertyNames : PropertyNamesBase
	{
		public PutPropertyNames() : base(String.Empty) {}
		public PutPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Bid { get { return ResolvePropertyName("Bid"); } }
		public String Ask { get { return ResolvePropertyName("Ask"); } }
	}
}
