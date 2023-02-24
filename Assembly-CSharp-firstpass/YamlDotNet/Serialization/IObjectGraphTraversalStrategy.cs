using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000189 RID: 393
	public interface IObjectGraphTraversalStrategy
	{
		// Token: 0x06000CF8 RID: 3320
		void Traverse<TContext>(IObjectDescriptor graph, IObjectGraphVisitor<TContext> visitor, TContext context);
	}
}
