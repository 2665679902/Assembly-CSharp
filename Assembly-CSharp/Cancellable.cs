using System;
using UnityEngine;

// Token: 0x02000457 RID: 1111
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Cancellable")]
public class Cancellable : KMonoBehaviour
{
	// Token: 0x0600186A RID: 6250 RVA: 0x000817E7 File Offset: 0x0007F9E7
	protected override void OnPrefabInit()
	{
		base.Subscribe<Cancellable>(2127324410, Cancellable.OnCancelDelegate);
	}

	// Token: 0x0600186B RID: 6251 RVA: 0x000817FA File Offset: 0x0007F9FA
	protected virtual void OnCancel(object data)
	{
		this.DeleteObject();
	}

	// Token: 0x04000DA5 RID: 3493
	private static readonly EventSystem.IntraObjectHandler<Cancellable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Cancellable>(delegate(Cancellable component, object data)
	{
		component.OnCancel(data);
	});
}
