using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000880 RID: 2176
[SerializationConfig(MemberSerialization.OptIn)]
public class Ownable : Assignable, ISaveLoadable, IGameObjectEffectDescriptor
{
	// Token: 0x06003E72 RID: 15986 RVA: 0x0015D544 File Offset: 0x0015B744
	public override void Assign(IAssignableIdentity new_assignee)
	{
		if (new_assignee == this.assignee)
		{
			return;
		}
		if (base.slot != null && new_assignee is MinionIdentity)
		{
			new_assignee = (new_assignee as MinionIdentity).assignableProxy.Get();
		}
		if (base.slot != null && new_assignee is StoredMinionIdentity)
		{
			new_assignee = (new_assignee as StoredMinionIdentity).assignableProxy.Get();
		}
		if (new_assignee is MinionAssignablesProxy)
		{
			AssignableSlotInstance slot = new_assignee.GetSoleOwner().GetComponent<Ownables>().GetSlot(base.slot);
			if (slot != null)
			{
				Assignable assignable = slot.assignable;
				if (assignable != null)
				{
					assignable.Unassign();
				}
			}
		}
		base.Assign(new_assignee);
	}

	// Token: 0x06003E73 RID: 15987 RVA: 0x0015D5E0 File Offset: 0x0015B7E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateTint();
		this.UpdateStatusString();
		base.OnAssign += this.OnNewAssignment;
		if (this.assignee == null)
		{
			MinionStorage component = base.GetComponent<MinionStorage>();
			if (component)
			{
				List<MinionStorage.Info> storedMinionInfo = component.GetStoredMinionInfo();
				if (storedMinionInfo.Count > 0)
				{
					Ref<KPrefabID> serializedMinion = storedMinionInfo[0].serializedMinion;
					if (serializedMinion != null && serializedMinion.GetId() != -1)
					{
						StoredMinionIdentity component2 = serializedMinion.Get().GetComponent<StoredMinionIdentity>();
						component2.ValidateProxy();
						this.Assign(component2);
					}
				}
			}
		}
	}

	// Token: 0x06003E74 RID: 15988 RVA: 0x0015D66A File Offset: 0x0015B86A
	private void OnNewAssignment(IAssignableIdentity assignables)
	{
		this.UpdateTint();
		this.UpdateStatusString();
	}

	// Token: 0x06003E75 RID: 15989 RVA: 0x0015D678 File Offset: 0x0015B878
	private void UpdateTint()
	{
		if (this.tintWhenUnassigned)
		{
			KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
			if (component != null && component.HasBatchInstanceData)
			{
				component.TintColour = ((this.assignee == null) ? this.unownedTint : this.ownedTint);
				return;
			}
			KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
			if (component2 != null && component2.HasBatchInstanceData)
			{
				component2.TintColour = ((this.assignee == null) ? this.unownedTint : this.ownedTint);
			}
		}
	}

	// Token: 0x06003E76 RID: 15990 RVA: 0x0015D700 File Offset: 0x0015B900
	private void UpdateStatusString()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (component == null)
		{
			return;
		}
		StatusItem statusItem;
		if (this.assignee != null)
		{
			if (this.assignee is MinionIdentity)
			{
				statusItem = Db.Get().BuildingStatusItems.AssignedTo;
			}
			else if (this.assignee is Room)
			{
				statusItem = Db.Get().BuildingStatusItems.AssignedTo;
			}
			else
			{
				statusItem = Db.Get().BuildingStatusItems.AssignedTo;
			}
		}
		else
		{
			statusItem = Db.Get().BuildingStatusItems.Unassigned;
		}
		component.SetStatusItem(Db.Get().StatusItemCategories.Ownable, statusItem, this);
	}

	// Token: 0x06003E77 RID: 15991 RVA: 0x0015D7A0 File Offset: 0x0015B9A0
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.ASSIGNEDDUPLICANT, UI.BUILDINGEFFECTS.TOOLTIPS.ASSIGNEDDUPLICANT, Descriptor.DescriptorType.Requirement);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x040028DD RID: 10461
	public bool tintWhenUnassigned = true;

	// Token: 0x040028DE RID: 10462
	private Color unownedTint = Color.gray;

	// Token: 0x040028DF RID: 10463
	private Color ownedTint = Color.white;
}
