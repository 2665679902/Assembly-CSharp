using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace YamlDotNet.Serialization.ObjectGraphTraversalStrategies
{
	// Token: 0x020001BF RID: 447
	public class RoundtripObjectGraphTraversalStrategy : FullObjectGraphTraversalStrategy
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003A296 File Offset: 0x00038496
		public RoundtripObjectGraphTraversalStrategy(IEnumerable<IYamlTypeConverter> converters, ITypeInspector typeDescriptor, ITypeResolver typeResolver, int maxRecursion)
			: base(typeDescriptor, typeResolver, maxRecursion, null)
		{
			this.converters = converters;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003A2AC File Offset: 0x000384AC
		protected override void TraverseProperties<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, int currentDepth, TContext context)
		{
			if (!value.Type.HasDefaultConstructor() && !this.converters.Any((IYamlTypeConverter c) => c.Accepts(value.Type)))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type '{0}' cannot be deserialized because it does not have a default constructor or a type converter.", value.Type));
			}
			base.TraverseProperties<TContext>(value, visitor, currentDepth, context);
		}

		// Token: 0x04000830 RID: 2096
		private readonly IEnumerable<IYamlTypeConverter> converters;
	}
}
