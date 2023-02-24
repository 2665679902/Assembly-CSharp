using System;
using UnityEngine;

// Token: 0x020005A8 RID: 1448
public class DevPump : Filterable, ISim1000ms
{
	// Token: 0x060023B3 RID: 9139 RVA: 0x000C1160 File Offset: 0x000BF360
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.elementState == Filterable.ElementState.Liquid)
		{
			base.SelectedTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
			return;
		}
		if (this.elementState == Filterable.ElementState.Gas)
		{
			base.SelectedTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		}
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x000C11B0 File Offset: 0x000BF3B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.filterElementState = this.elementState;
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x000C11C4 File Offset: 0x000BF3C4
	public void Sim1000ms(float dt)
	{
		if (!base.SelectedTag.IsValid)
		{
			return;
		}
		float num = 10f - this.storage.GetAmountAvailable(base.SelectedTag);
		if (num <= 0f)
		{
			return;
		}
		Element element = ElementLoader.GetElement(base.SelectedTag);
		GameObject gameObject = Assets.TryGetPrefab(base.SelectedTag);
		if (element != null)
		{
			this.storage.AddElement(element.id, num, element.defaultValues.temperature, byte.MaxValue, 0, false, false);
			return;
		}
		if (gameObject != null)
		{
			Grid.SceneLayer sceneLayer = gameObject.GetComponent<KBatchedAnimController>().sceneLayer;
			GameObject gameObject2 = GameUtil.KInstantiate(gameObject, sceneLayer, null, 0);
			gameObject2.GetComponent<PrimaryElement>().Units = num;
			gameObject2.SetActive(true);
			this.storage.Store(gameObject2, true, false, true, false);
		}
	}

	// Token: 0x04001479 RID: 5241
	public Filterable.ElementState elementState = Filterable.ElementState.Liquid;

	// Token: 0x0400147A RID: 5242
	[MyCmpReq]
	private Storage storage;
}
