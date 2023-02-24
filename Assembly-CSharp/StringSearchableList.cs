using System;
using System.Collections.Generic;

// Token: 0x02000366 RID: 870
public class StringSearchableList<T>
{
	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0005DF12 File Offset: 0x0005C112
	// (set) Token: 0x060011B6 RID: 4534 RVA: 0x0005DF1A File Offset: 0x0005C11A
	public bool didUseFilter { get; private set; }

	// Token: 0x060011B7 RID: 4535 RVA: 0x0005DF23 File Offset: 0x0005C123
	public StringSearchableList(List<T> allValues, StringSearchableList<T>.ShouldFilterOutFn shouldFilterOutFn)
	{
		this.allValues = allValues;
		this.shouldFilterOutFn = shouldFilterOutFn;
		this.filteredValues = new List<T>();
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x0005DF4F File Offset: 0x0005C14F
	public StringSearchableList(StringSearchableList<T>.ShouldFilterOutFn shouldFilterOutFn)
	{
		this.shouldFilterOutFn = shouldFilterOutFn;
		this.allValues = new List<T>();
		this.filteredValues = new List<T>();
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0005DF80 File Offset: 0x0005C180
	public void Refilter()
	{
		if (StringSearchableListUtil.ShouldUseFilter(this.filter))
		{
			this.filteredValues.Clear();
			foreach (T t in this.allValues)
			{
				if (!this.shouldFilterOutFn(t, this.filter))
				{
					this.filteredValues.Add(t);
				}
			}
			this.didUseFilter = true;
			return;
		}
		if (this.filteredValues.Count != this.allValues.Count)
		{
			this.filteredValues.Clear();
			this.filteredValues.AddRange(this.allValues);
		}
		this.didUseFilter = false;
	}

	// Token: 0x0400098A RID: 2442
	public string filter = "";

	// Token: 0x0400098B RID: 2443
	public List<T> allValues;

	// Token: 0x0400098C RID: 2444
	public List<T> filteredValues;

	// Token: 0x0400098E RID: 2446
	public readonly StringSearchableList<T>.ShouldFilterOutFn shouldFilterOutFn;

	// Token: 0x02000F2C RID: 3884
	// (Invoke) Token: 0x06006E3D RID: 28221
	public delegate bool ShouldFilterOutFn(T candidateValue, in string filter);
}
