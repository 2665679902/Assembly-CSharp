using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000488 RID: 1160
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/InOrbitRequired")]
public class InOrbitRequired : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x060019E9 RID: 6633 RVA: 0x0008AB2C File Offset: 0x00088D2C
	protected override void OnSpawn()
	{
		WorldContainer myWorld = this.GetMyWorld();
		this.craftModuleInterface = myWorld.GetComponent<CraftModuleInterface>();
		base.OnSpawn();
		bool flag = this.craftModuleInterface.HasTag(GameTags.RocketNotOnGround);
		this.UpdateFlag(flag);
		this.craftModuleInterface.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
	}

	// Token: 0x060019EA RID: 6634 RVA: 0x0008AB87 File Offset: 0x00088D87
	protected override void OnCleanUp()
	{
		if (this.craftModuleInterface != null)
		{
			this.craftModuleInterface.Unsubscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		}
	}

	// Token: 0x060019EB RID: 6635 RVA: 0x0008ABB4 File Offset: 0x00088DB4
	private void OnTagsChanged(object data)
	{
		TagChangedEventData tagChangedEventData = (TagChangedEventData)data;
		if (tagChangedEventData.tag == GameTags.RocketNotOnGround)
		{
			this.UpdateFlag(tagChangedEventData.added);
		}
	}

	// Token: 0x060019EC RID: 6636 RVA: 0x0008ABE6 File Offset: 0x00088DE6
	private void UpdateFlag(bool newInOrbit)
	{
		this.operational.SetFlag(InOrbitRequired.inOrbitFlag, newInOrbit);
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.InOrbitRequired, !newInOrbit, this);
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x0008AC19 File Offset: 0x00088E19
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.BUILDINGEFFECTS.IN_ORBIT_REQUIRED, UI.BUILDINGEFFECTS.TOOLTIPS.IN_ORBIT_REQUIRED, Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x04000E80 RID: 3712
	[MyCmpReq]
	private Building building;

	// Token: 0x04000E81 RID: 3713
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04000E82 RID: 3714
	public static readonly Operational.Flag inOrbitFlag = new Operational.Flag("in_orbit", Operational.Flag.Type.Requirement);

	// Token: 0x04000E83 RID: 3715
	private CraftModuleInterface craftModuleInterface;
}
