using System;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
[Serializable]
public class DefComponent<T> where T : Component
{
	// Token: 0x060014F9 RID: 5369 RVA: 0x0006DCB0 File Offset: 0x0006BEB0
	public DefComponent(T cmp)
	{
		this.cmp = cmp;
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x0006DCC0 File Offset: 0x0006BEC0
	public T Get(StateMachine.Instance smi)
	{
		T[] components = this.cmp.GetComponents<T>();
		int num = 0;
		while (num < components.Length && !(components[num] == this.cmp))
		{
			num++;
		}
		return smi.gameObject.GetComponents<T>()[num];
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x0006DD1B File Offset: 0x0006BF1B
	public static implicit operator DefComponent<T>(T cmp)
	{
		return new DefComponent<T>(cmp);
	}

	// Token: 0x04000BBF RID: 3007
	[SerializeField]
	private T cmp;
}
