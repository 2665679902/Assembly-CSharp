using System;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
[AddComponentMenu("KMonoBehaviour/scripts/ElementDropper")]
public class ElementDropper : KMonoBehaviour
{
	// Token: 0x06002420 RID: 9248 RVA: 0x000C363E File Offset: 0x000C183E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ElementDropper>(-1697596308, ElementDropper.OnStorageChangedDelegate);
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x000C3657 File Offset: 0x000C1857
	private void OnStorageChanged(object data)
	{
		if (this.storage.GetMassAvailable(this.emitTag) >= this.emitMass)
		{
			this.storage.DropSome(this.emitTag, this.emitMass, false, false, this.emitOffset, true, true);
		}
	}

	// Token: 0x040014C3 RID: 5315
	[SerializeField]
	public Tag emitTag;

	// Token: 0x040014C4 RID: 5316
	[SerializeField]
	public float emitMass;

	// Token: 0x040014C5 RID: 5317
	[SerializeField]
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x040014C6 RID: 5318
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040014C7 RID: 5319
	private static readonly EventSystem.IntraObjectHandler<ElementDropper> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<ElementDropper>(delegate(ElementDropper component, object data)
	{
		component.OnStorageChanged(data);
	});
}
