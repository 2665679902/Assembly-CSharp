using System;
using UnityEngine;

// Token: 0x020005C8 RID: 1480
public class GlassForge : ComplexFabricator
{
	// Token: 0x060024D4 RID: 9428 RVA: 0x000C72B8 File Offset: 0x000C54B8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<GlassForge>(-2094018600, GlassForge.CheckPipesDelegate);
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x000C72D4 File Offset: 0x000C54D4
	private void CheckPipes(object data)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		int num = Grid.OffsetCell(Grid.PosToCell(this), GlassForgeConfig.outPipeOffset);
		GameObject gameObject = Grid.Objects[num, 16];
		if (!(gameObject != null))
		{
			component.RemoveStatusItem(this.statusHandle, false);
			return;
		}
		if (gameObject.GetComponent<PrimaryElement>().Element.highTemp > ElementLoader.FindElementByHash(SimHashes.MoltenGlass).lowTemp)
		{
			component.RemoveStatusItem(this.statusHandle, false);
			return;
		}
		this.statusHandle = component.AddStatusItem(Db.Get().BuildingStatusItems.PipeMayMelt, null);
	}

	// Token: 0x04001538 RID: 5432
	private Guid statusHandle;

	// Token: 0x04001539 RID: 5433
	private static readonly EventSystem.IntraObjectHandler<GlassForge> CheckPipesDelegate = new EventSystem.IntraObjectHandler<GlassForge>(delegate(GlassForge component, object data)
	{
		component.CheckPipes(data);
	});
}
