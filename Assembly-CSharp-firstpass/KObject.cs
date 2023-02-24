using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class KObject
{
	// Token: 0x06000712 RID: 1810 RVA: 0x0001E683 File Offset: 0x0001C883
	public KObject(GameObject go)
	{
		this.id = go.GetInstanceID();
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0001E698 File Offset: 0x0001C898
	~KObject()
	{
		this.OnCleanUp();
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
	public void OnCleanUp()
	{
		if (this.eventSystem != null)
		{
			this.eventSystem.OnCleanUp();
			this.eventSystem = null;
		}
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
	public EventSystem GetEventSystem()
	{
		if (this.eventSystem == null)
		{
			this.eventSystem = new EventSystem();
		}
		return this.eventSystem;
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001E6FB File Offset: 0x0001C8FB
	// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001E703 File Offset: 0x0001C903
	public int id { get; private set; }

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001E70C File Offset: 0x0001C90C
	public bool hasEventSystem
	{
		get
		{
			return this.eventSystem != null;
		}
	}

	// Token: 0x040005DA RID: 1498
	private EventSystem eventSystem;
}
