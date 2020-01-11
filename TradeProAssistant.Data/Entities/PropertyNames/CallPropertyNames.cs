using System;

namespace Entities
{
	public class CallPropertyNames : PropertyNamesBase
	{
		public CallPropertyNames() : base(String.Empty) {}
		public CallPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Bid { get { return ResolvePropertyName("Bid"); } }
		public String Ask { get { return ResolvePropertyName("Ask"); } }
	}
}
