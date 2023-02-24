using System;
using UnityEngine;

// Token: 0x0200057D RID: 1405
[AddComponentMenu("KMonoBehaviour/scripts/BubbleSpawner")]
public class BubbleSpawner : KMonoBehaviour
{
	// Token: 0x0600222E RID: 8750 RVA: 0x000B99D7 File Offset: 0x000B7BD7
	protected override void OnSpawn()
	{
		this.emitMass += (UnityEngine.Random.value - 0.5f) * this.emitVariance * this.emitMass;
		base.OnSpawn();
		base.Subscribe<BubbleSpawner>(-1697596308, BubbleSpawner.OnStorageChangedDelegate);
	}

	// Token: 0x0600222F RID: 8751 RVA: 0x000B9A18 File Offset: 0x000B7C18
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = this.storage.FindFirst(ElementLoader.FindElementByHash(this.element).tag);
		if (gameObject == null)
		{
			return;
		}
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		if (component.Mass >= this.emitMass)
		{
			gameObject.GetComponent<PrimaryElement>().Mass -= this.emitMass;
			BubbleManager.instance.SpawnBubble(base.transform.GetPosition(), this.initialVelocity, component.ElementID, this.emitMass, component.Temperature);
		}
	}

	// Token: 0x040013BE RID: 5054
	public SimHashes element;

	// Token: 0x040013BF RID: 5055
	public float emitMass;

	// Token: 0x040013C0 RID: 5056
	public float emitVariance;

	// Token: 0x040013C1 RID: 5057
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x040013C2 RID: 5058
	public Vector2 initialVelocity;

	// Token: 0x040013C3 RID: 5059
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040013C4 RID: 5060
	private static readonly EventSystem.IntraObjectHandler<BubbleSpawner> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<BubbleSpawner>(delegate(BubbleSpawner component, object data)
	{
		component.OnStorageChanged(data);
	});
}
