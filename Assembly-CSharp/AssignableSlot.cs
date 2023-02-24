using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000552 RID: 1362
[DebuggerDisplay("{Id}")]
[Serializable]
public class AssignableSlot : Resource
{
	// Token: 0x060020A9 RID: 8361 RVA: 0x000B24F1 File Offset: 0x000B06F1
	public AssignableSlot(string id, string name, bool showInUI = true)
		: base(id, name)
	{
		this.showInUI = showInUI;
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x000B250C File Offset: 0x000B070C
	public AssignableSlotInstance Lookup(GameObject go)
	{
		Assignables component = go.GetComponent<Assignables>();
		if (component != null)
		{
			return component.GetSlot(this);
		}
		return null;
	}

	// Token: 0x040012DF RID: 4831
	public bool showInUI = true;
}
