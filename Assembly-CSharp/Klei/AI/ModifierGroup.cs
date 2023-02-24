using System;
using System.Collections.Generic;

namespace Klei.AI
{
	// Token: 0x02000D95 RID: 3477
	public class ModifierGroup<T> : Resource
	{
		// Token: 0x060069E1 RID: 27105 RVA: 0x00292CC0 File Offset: 0x00290EC0
		public IEnumerator<T> GetEnumerator()
		{
			return this.modifiers.GetEnumerator();
		}

		// Token: 0x1700078B RID: 1931
		public T this[int idx]
		{
			get
			{
				return this.modifiers[idx];
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060069E3 RID: 27107 RVA: 0x00292CE0 File Offset: 0x00290EE0
		public int Count
		{
			get
			{
				return this.modifiers.Count;
			}
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x00292CED File Offset: 0x00290EED
		public ModifierGroup(string id, string name)
			: base(id, name)
		{
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x00292D02 File Offset: 0x00290F02
		public void Add(T modifier)
		{
			this.modifiers.Add(modifier);
		}

		// Token: 0x04004FA5 RID: 20389
		public List<T> modifiers = new List<T>();
	}
}
