using System;
using System.Collections.Generic;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A1 RID: 417
	public sealed class TagMappings
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x0003879B File Offset: 0x0003699B
		public TagMappings()
		{
			this.mappings = new Dictionary<string, Type>();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000387AE File Offset: 0x000369AE
		public TagMappings(IDictionary<string, Type> mappings)
		{
			this.mappings = new Dictionary<string, Type>(mappings);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000387C2 File Offset: 0x000369C2
		public void Add(string tag, Type mapping)
		{
			this.mappings.Add(tag, mapping);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000387D4 File Offset: 0x000369D4
		internal Type GetMapping(string tag)
		{
			Type type;
			if (this.mappings.TryGetValue(tag, out type))
			{
				return type;
			}
			return null;
		}

		// Token: 0x0400080A RID: 2058
		private readonly IDictionary<string, Type> mappings;
	}
}
