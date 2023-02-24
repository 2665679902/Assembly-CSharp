using System;
using UnityEngine;

// Token: 0x02000667 RID: 1639
public class UraniumCentrifuge : ComplexFabricator
{
	// Token: 0x06002C27 RID: 11303 RVA: 0x000E7F62 File Offset: 0x000E6162
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<UraniumCentrifuge>(-1697596308, UraniumCentrifuge.DropEnrichedProductDelegate);
		base.Subscribe<UraniumCentrifuge>(-2094018600, UraniumCentrifuge.CheckPipesDelegate);
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x000E7F8C File Offset: 0x000E618C
	private void DropEnrichedProducts(object data)
	{
		Storage[] components = base.GetComponents<Storage>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].Drop(ElementLoader.FindElementByHash(SimHashes.EnrichedUranium).tag);
		}
	}

	// Token: 0x06002C29 RID: 11305 RVA: 0x000E7FC8 File Offset: 0x000E61C8
	private void CheckPipes(object data)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		int num = Grid.OffsetCell(Grid.PosToCell(this), UraniumCentrifugeConfig.outPipeOffset);
		GameObject gameObject = Grid.Objects[num, 16];
		if (!(gameObject != null))
		{
			component.RemoveStatusItem(this.statusHandle, false);
			return;
		}
		if (gameObject.GetComponent<PrimaryElement>().Element.highTemp > ElementLoader.FindElementByHash(SimHashes.MoltenUranium).lowTemp)
		{
			component.RemoveStatusItem(this.statusHandle, false);
			return;
		}
		this.statusHandle = component.AddStatusItem(Db.Get().BuildingStatusItems.PipeMayMelt, null);
	}

	// Token: 0x04001A3F RID: 6719
	private Guid statusHandle;

	// Token: 0x04001A40 RID: 6720
	private static readonly EventSystem.IntraObjectHandler<UraniumCentrifuge> CheckPipesDelegate = new EventSystem.IntraObjectHandler<UraniumCentrifuge>(delegate(UraniumCentrifuge component, object data)
	{
		component.CheckPipes(data);
	});

	// Token: 0x04001A41 RID: 6721
	private static readonly EventSystem.IntraObjectHandler<UraniumCentrifuge> DropEnrichedProductDelegate = new EventSystem.IntraObjectHandler<UraniumCentrifuge>(delegate(UraniumCentrifuge component, object data)
	{
		component.DropEnrichedProducts(data);
	});
}
