using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	// Token: 0x02000217 RID: 535
	public class TagDirectiveCollection : KeyedCollection<string, TagDirective>
	{
		// Token: 0x0600108F RID: 4239 RVA: 0x00043D6A File Offset: 0x00041F6A
		public TagDirectiveCollection()
		{
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00043D74 File Offset: 0x00041F74
		public TagDirectiveCollection(IEnumerable<TagDirective> tagDirectives)
		{
			foreach (TagDirective tagDirective in tagDirectives)
			{
				base.Add(tagDirective);
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00043DC4 File Offset: 0x00041FC4
		protected override string GetKeyForItem(TagDirective item)
		{
			return item.Handle;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00043DCC File Offset: 0x00041FCC
		public new bool Contains(TagDirective directive)
		{
			return base.Contains(this.GetKeyForItem(directive));
		}
	}
}
