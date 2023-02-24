using System;
using KSerialization;

// Token: 0x0200059D RID: 1437
[SerializationConfig(MemberSerialization.OptIn)]
public class ConduitElementSensor : ConduitSensor
{
	// Token: 0x06002361 RID: 9057 RVA: 0x000BF00E File Offset: 0x000BD20E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.filterable.onFilterChanged += this.OnFilterChanged;
		this.OnFilterChanged(this.filterable.SelectedTag);
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x000BF040 File Offset: 0x000BD240
	private void OnFilterChanged(Tag tag)
	{
		if (!tag.IsValid)
		{
			return;
		}
		bool flag = tag == GameTags.Void;
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, flag, null);
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x000BF080 File Offset: 0x000BD280
	protected override void ConduitUpdate(float dt)
	{
		Tag tag;
		bool flag;
		this.GetContentsElement(out tag, out flag);
		if (!base.IsSwitchedOn)
		{
			if (tag == this.filterable.SelectedTag && flag)
			{
				this.Toggle();
				return;
			}
		}
		else if (tag != this.filterable.SelectedTag || !flag)
		{
			this.Toggle();
		}
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x000BF0D8 File Offset: 0x000BD2D8
	private void GetContentsElement(out Tag element, out bool hasMass)
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			ConduitFlow.ConduitContents contents = Conduit.GetFlowManager(this.conduitType).GetContents(num);
			element = contents.element.CreateTag();
			hasMass = contents.mass > 0f;
			return;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents2.pickupableHandle);
		KPrefabID kprefabID = ((pickupable != null) ? pickupable.GetComponent<KPrefabID>() : null);
		if (kprefabID != null && pickupable.PrimaryElement.Mass > 0f)
		{
			element = kprefabID.PrefabTag;
			hasMass = true;
			return;
		}
		element = GameTags.Void;
		hasMass = false;
	}

	// Token: 0x0400144E RID: 5198
	[MyCmpGet]
	private Filterable filterable;
}
