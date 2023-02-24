using System;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.Utilities
{
	// Token: 0x020001AB RID: 427
	internal sealed class ObjectAnchorCollection
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00038F34 File Offset: 0x00037134
		public void Add(string anchor, object @object)
		{
			this.objectsByAnchor.Add(anchor, @object);
			if (@object != null)
			{
				this.anchorsByObject.Add(@object, anchor);
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00038F53 File Offset: 0x00037153
		public bool TryGetAnchor(object @object, out string anchor)
		{
			return this.anchorsByObject.TryGetValue(@object, out anchor);
		}

		// Token: 0x1700016E RID: 366
		public object this[string anchor]
		{
			get
			{
				object obj;
				if (this.objectsByAnchor.TryGetValue(anchor, out obj))
				{
					return obj;
				}
				throw new AnchorNotFoundException(string.Format(CultureInfo.InvariantCulture, "The anchor '{0}' does not exists", anchor));
			}
		}

		// Token: 0x04000818 RID: 2072
		private readonly IDictionary<string, object> objectsByAnchor = new Dictionary<string, object>();

		// Token: 0x04000819 RID: 2073
		private readonly IDictionary<object, string> anchorsByObject = new Dictionary<object, string>();
	}
}
