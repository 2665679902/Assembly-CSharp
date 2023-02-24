using System;
using UnityEngine;

// Token: 0x02000962 RID: 2402
public class SolidBooster : RocketEngine
{
	// Token: 0x060046FF RID: 18175 RVA: 0x0018FC13 File Offset: 0x0018DE13
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SolidBooster>(-887025858, SolidBooster.OnRocketLandedDelegate);
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x0018FC2C File Offset: 0x0018DE2C
	[ContextMenu("Fill Tank")]
	public void FillTank()
	{
		Element element = ElementLoader.GetElement(this.fuelTag);
		GameObject gameObject = element.substance.SpawnResource(base.gameObject.transform.GetPosition(), this.fuelStorage.capacityKg / 2f, element.defaultValues.temperature, byte.MaxValue, 0, false, false, false);
		this.fuelStorage.Store(gameObject, false, false, true, false);
		element = ElementLoader.GetElement(GameTags.OxyRock);
		gameObject = element.substance.SpawnResource(base.gameObject.transform.GetPosition(), this.fuelStorage.capacityKg / 2f, element.defaultValues.temperature, byte.MaxValue, 0, false, false, false);
		this.fuelStorage.Store(gameObject, false, false, true, false);
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x0018FCF4 File Offset: 0x0018DEF4
	private void OnRocketLanded(object data)
	{
		if (this.fuelStorage != null && this.fuelStorage.items != null)
		{
			for (int i = this.fuelStorage.items.Count - 1; i >= 0; i--)
			{
				Util.KDestroyGameObject(this.fuelStorage.items[i]);
			}
			this.fuelStorage.items.Clear();
		}
	}

	// Token: 0x04002F0B RID: 12043
	public Storage fuelStorage;

	// Token: 0x04002F0C RID: 12044
	private static readonly EventSystem.IntraObjectHandler<SolidBooster> OnRocketLandedDelegate = new EventSystem.IntraObjectHandler<SolidBooster>(delegate(SolidBooster component, object data)
	{
		component.OnRocketLanded(data);
	});
}
