using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public interface IStateMachineTarget
{
	// Token: 0x060008A3 RID: 2211
	int Subscribe(int hash, Action<object> handler);

	// Token: 0x060008A4 RID: 2212
	void Unsubscribe(int hash, Action<object> handler);

	// Token: 0x060008A5 RID: 2213
	void Unsubscribe(int id);

	// Token: 0x060008A6 RID: 2214
	void Trigger(int hash, object data = null);

	// Token: 0x060008A7 RID: 2215
	ComponentType GetComponent<ComponentType>();

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060008A8 RID: 2216
	GameObject gameObject { get; }

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060008A9 RID: 2217
	Transform transform { get; }

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060008AA RID: 2218
	string name { get; }

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060008AB RID: 2219
	bool isNull { get; }
}
