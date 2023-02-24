using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000754 RID: 1876
[SerializationConfig(MemberSerialization.OptIn)]
public class Equippable : Assignable, ISaveLoadable, IGameObjectEffectDescriptor, IQuality
{
	// Token: 0x0600339F RID: 13215 RVA: 0x00116136 File Offset: 0x00114336
	public global::QualityLevel GetQuality()
	{
		return this.quality;
	}

	// Token: 0x060033A0 RID: 13216 RVA: 0x0011613E File Offset: 0x0011433E
	public void SetQuality(global::QualityLevel level)
	{
		this.quality = level;
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x060033A1 RID: 13217 RVA: 0x00116147 File Offset: 0x00114347
	// (set) Token: 0x060033A2 RID: 13218 RVA: 0x00116154 File Offset: 0x00114354
	public EquipmentDef def
	{
		get
		{
			return this.defHandle.Get<EquipmentDef>();
		}
		set
		{
			this.defHandle.Set<EquipmentDef>(value);
		}
	}

	// Token: 0x060033A3 RID: 13219 RVA: 0x00116164 File Offset: 0x00114364
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.def.AdditionalTags != null)
		{
			foreach (Tag tag in this.def.AdditionalTags)
			{
				base.GetComponent<KPrefabID>().AddTag(tag, false);
			}
		}
	}

	// Token: 0x060033A4 RID: 13220 RVA: 0x001161B4 File Offset: 0x001143B4
	protected override void OnSpawn()
	{
		if (this.isEquipped)
		{
			if (this.assignee != null && this.assignee is MinionIdentity)
			{
				this.assignee = (this.assignee as MinionIdentity).assignableProxy.Get();
				this.assignee_identityRef.Set(this.assignee as KMonoBehaviour);
			}
			if (this.assignee == null && this.assignee_identityRef.Get() != null)
			{
				this.assignee = this.assignee_identityRef.Get().GetComponent<IAssignableIdentity>();
			}
			if (this.assignee != null)
			{
				this.assignee.GetSoleOwner().GetComponent<Equipment>().Equip(this);
			}
			else
			{
				global::Debug.LogWarning("Equippable trying to be equipped to missing prefab");
				this.isEquipped = false;
			}
		}
		base.Subscribe<Equippable>(1969584890, Equippable.SetDestroyedTrueDelegate);
	}

	// Token: 0x060033A5 RID: 13221 RVA: 0x00116288 File Offset: 0x00114488
	public KAnimFile GetBuildOverride()
	{
		EquippableFacade component = base.GetComponent<EquippableFacade>();
		if (component == null || component.BuildOverride == null)
		{
			return this.def.BuildOverride;
		}
		return Assets.GetAnim(component.BuildOverride);
	}

	// Token: 0x060033A6 RID: 13222 RVA: 0x001162CC File Offset: 0x001144CC
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
			AssignableSlotInstance slot = new_assignee.GetSoleOwner().GetComponent<Equipment>().GetSlot(base.slot);
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

	// Token: 0x060033A7 RID: 13223 RVA: 0x00116368 File Offset: 0x00114568
	public override void Unassign()
	{
		if (this.isEquipped)
		{
			((this.assignee is MinionIdentity) ? ((MinionIdentity)this.assignee).assignableProxy.Get().GetComponent<Equipment>() : ((KMonoBehaviour)this.assignee).GetComponent<Equipment>()).Unequip(this);
			this.OnUnequip();
		}
		base.Unassign();
	}

	// Token: 0x060033A8 RID: 13224 RVA: 0x001163C8 File Offset: 0x001145C8
	public void OnEquip(AssignableSlotInstance slot)
	{
		this.isEquipped = true;
		if (SelectTool.Instance.selected == this.selectable)
		{
			SelectTool.Instance.Select(null, false);
		}
		base.GetComponent<KBatchedAnimController>().enabled = false;
		base.GetComponent<KSelectable>().IsSelectable = false;
		string name = base.GetComponent<KPrefabID>().PrefabTag.Name;
		Effects component = slot.gameObject.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().GetComponent<Effects>();
		if (component != null)
		{
			foreach (Effect effect in this.def.EffectImmunites)
			{
				component.AddImmunity(effect, name, true);
			}
		}
		if (this.def.OnEquipCallBack != null)
		{
			this.def.OnEquipCallBack(this);
		}
		base.GetComponent<KPrefabID>().AddTag(GameTags.Equipped, false);
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x001164C4 File Offset: 0x001146C4
	public void OnUnequip()
	{
		this.isEquipped = false;
		if (this.destroyed)
		{
			return;
		}
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.Equipped);
		base.GetComponent<KBatchedAnimController>().enabled = true;
		base.GetComponent<KSelectable>().IsSelectable = true;
		string name = base.GetComponent<KPrefabID>().PrefabTag.Name;
		if (this.assignee != null)
		{
			Ownables soleOwner = this.assignee.GetSoleOwner();
			if (soleOwner)
			{
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject)
				{
					Effects component = targetGameObject.GetComponent<Effects>();
					if (component != null)
					{
						foreach (Effect effect in this.def.EffectImmunites)
						{
							component.RemoveImmunity(effect, name);
						}
					}
				}
			}
		}
		if (this.def.OnUnequipCallBack != null)
		{
			this.def.OnUnequipCallBack(this);
		}
	}

	// Token: 0x060033AA RID: 13226 RVA: 0x001165C8 File Offset: 0x001147C8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		if (this.def != null)
		{
			List<Descriptor> equipmentEffects = GameUtil.GetEquipmentEffects(this.def);
			if (this.def.additionalDescriptors != null)
			{
				foreach (Descriptor descriptor in this.def.additionalDescriptors)
				{
					equipmentEffects.Add(descriptor);
				}
			}
			return equipmentEffects;
		}
		return new List<Descriptor>();
	}

	// Token: 0x04001FB5 RID: 8117
	private global::QualityLevel quality;

	// Token: 0x04001FB6 RID: 8118
	[MyCmpAdd]
	private EquippableWorkable equippableWorkable;

	// Token: 0x04001FB7 RID: 8119
	[MyCmpAdd]
	private EquippableFacade facade;

	// Token: 0x04001FB8 RID: 8120
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001FB9 RID: 8121
	public DefHandle defHandle;

	// Token: 0x04001FBA RID: 8122
	[Serialize]
	public bool isEquipped;

	// Token: 0x04001FBB RID: 8123
	private bool destroyed;

	// Token: 0x04001FBC RID: 8124
	[Serialize]
	public bool unequippable = true;

	// Token: 0x04001FBD RID: 8125
	[Serialize]
	public bool hideInCodex;

	// Token: 0x04001FBE RID: 8126
	private static readonly EventSystem.IntraObjectHandler<Equippable> SetDestroyedTrueDelegate = new EventSystem.IntraObjectHandler<Equippable>(delegate(Equippable component, object data)
	{
		component.destroyed = true;
	});
}
