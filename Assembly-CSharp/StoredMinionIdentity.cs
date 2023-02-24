using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/StoredMinionIdentity")]
public class StoredMinionIdentity : KMonoBehaviour, ISaveLoadable, IAssignableIdentity, IListableOption, IPersonalPriorityManager
{
	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0009C267 File Offset: 0x0009A467
	// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0009C26F File Offset: 0x0009A46F
	[Serialize]
	public string genderStringKey { get; set; }

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0009C278 File Offset: 0x0009A478
	// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0009C280 File Offset: 0x0009A480
	[Serialize]
	public string nameStringKey { get; set; }

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0009C289 File Offset: 0x0009A489
	// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0009C291 File Offset: 0x0009A491
	[Serialize]
	public HashedString personalityResourceId { get; set; }

	// Token: 0x06001D3D RID: 7485 RVA: 0x0009C29C File Offset: 0x0009A49C
	[OnDeserialized]
	[Obsolete]
	private void OnDeserializedMethod()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 7))
		{
			int num = 0;
			foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryByRoleID)
			{
				if (keyValuePair.Value && keyValuePair.Key != "NoRole")
				{
					num++;
				}
			}
			this.TotalExperienceGained = MinionResume.CalculatePreviousExperienceBar(num);
			foreach (KeyValuePair<HashedString, float> keyValuePair2 in this.AptitudeByRoleGroup)
			{
				this.AptitudeBySkillGroup[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 29))
		{
			this.forbiddenTagSet = new HashSet<Tag>(this.forbiddenTags);
			this.forbiddenTags = null;
		}
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 30))
		{
			this.bodyData = Accessorizer.UpdateAccessorySlots(this.nameStringKey, ref this.accessories);
		}
		List<ResourceRef<Accessory>> list = this.accessories.FindAll((ResourceRef<Accessory> acc) => acc.Get() == null);
		if (list.Count > 0)
		{
			List<ClothingItemResource> list2 = new List<ClothingItemResource>();
			foreach (ResourceRef<Accessory> resourceRef in list)
			{
				ClothingItemResource clothingItemResource = Db.Get().Permits.ClothingItems.TryResolveAccessoryResource(resourceRef.Guid);
				if (clothingItemResource != null && !list2.Contains(clothingItemResource))
				{
					list2.Add(clothingItemResource);
				}
			}
			foreach (ClothingItemResource clothingItemResource2 in list2)
			{
				this.clothingItems.Add(new ResourceRef<ClothingItemResource>(clothingItemResource2));
			}
			this.bodyData = Accessorizer.UpdateAccessorySlots(this.nameStringKey, ref this.accessories);
		}
		this.OnDeserializeModifiers();
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x0009C4F8 File Offset: 0x0009A6F8
	public bool HasPerk(SkillPerk perk)
	{
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).perks.Contains(perk))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x0009C578 File Offset: 0x0009A778
	public bool HasMasteredSkill(string skillId)
	{
		return this.MasteryBySkillID.ContainsKey(skillId) && this.MasteryBySkillID[skillId];
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0009C596 File Offset: 0x0009A796
	protected override void OnPrefabInit()
	{
		this.assignableProxy = new Ref<MinionAssignablesProxy>();
		this.minionModifiers = base.GetComponent<MinionModifiers>();
		this.savedAttributeValues = new Dictionary<string, float>();
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x0009C5BC File Offset: 0x0009A7BC
	[OnSerializing]
	private void OnSerialize()
	{
		this.savedAttributeValues.Clear();
		foreach (AttributeInstance attributeInstance in this.minionModifiers.attributes)
		{
			this.savedAttributeValues.Add(attributeInstance.Attribute.Id, attributeInstance.GetTotalValue());
		}
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x0009C630 File Offset: 0x0009A830
	protected override void OnSpawn()
	{
		MinionConfig.AddMinionAmounts(this.minionModifiers);
		MinionConfig.AddMinionTraits(DUPLICANTS.MODIFIERS.BASEDUPLICANT.NAME, this.minionModifiers);
		this.ValidateProxy();
		this.CleanupLimboMinions();
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x0009C65E File Offset: 0x0009A85E
	public void OnHardDelete()
	{
		if (this.assignableProxy.Get() != null)
		{
			Util.KDestroyGameObject(this.assignableProxy.Get().gameObject);
		}
		ScheduleManager.Instance.OnStoredDupeDestroyed(this);
		Components.StoredMinionIdentities.Remove(this);
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x0009C6A0 File Offset: 0x0009A8A0
	private void OnDeserializeModifiers()
	{
		foreach (KeyValuePair<string, float> keyValuePair in this.savedAttributeValues)
		{
			Klei.AI.Attribute attribute = Db.Get().Attributes.TryGet(keyValuePair.Key);
			if (attribute == null)
			{
				attribute = Db.Get().BuildingAttributes.TryGet(keyValuePair.Key);
			}
			if (attribute != null)
			{
				if (this.minionModifiers.attributes.Get(attribute.Id) != null)
				{
					this.minionModifiers.attributes.Get(attribute.Id).Modifiers.Clear();
					this.minionModifiers.attributes.Get(attribute.Id).ClearModifiers();
				}
				else
				{
					this.minionModifiers.attributes.Add(attribute);
				}
				this.minionModifiers.attributes.Add(new AttributeModifier(attribute.Id, keyValuePair.Value, () => DUPLICANTS.ATTRIBUTES.STORED_VALUE, false, false));
			}
		}
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x0009C7D4 File Offset: 0x0009A9D4
	public void ValidateProxy()
	{
		this.assignableProxy = MinionAssignablesProxy.InitAssignableProxy(this.assignableProxy, this);
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x0009C7E8 File Offset: 0x0009A9E8
	public string[] GetClothingItemIds()
	{
		string[] array = new string[this.clothingItems.Count];
		for (int i = 0; i < this.clothingItems.Count; i++)
		{
			array[i] = this.clothingItems[i].Get().Id;
		}
		return array;
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x0009C838 File Offset: 0x0009AA38
	private void CleanupLimboMinions()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		bool flag = false;
		if (component.InstanceID == -1)
		{
			DebugUtil.LogWarningArgs(new object[] { "Stored minion with an invalid kpid! Attempting to recover...", this.storedName });
			flag = true;
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		if (component.conflicted)
		{
			DebugUtil.LogWarningArgs(new object[] { "Minion with a conflicted kpid! Attempting to recover... ", component.InstanceID, this.storedName });
			if (KPrefabIDTracker.Get().GetInstance(component.InstanceID) != null)
			{
				KPrefabIDTracker.Get().Unregister(component);
			}
			component.InstanceID = KPrefabID.GetUniqueID();
			KPrefabIDTracker.Get().Register(component);
			DebugUtil.LogWarningArgs(new object[] { "Restored as:", component.InstanceID });
		}
		this.assignableProxy.Get().SetTarget(this, base.gameObject);
		bool flag2 = false;
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			List<MinionStorage.Info> storedMinionInfo = minionStorage.GetStoredMinionInfo();
			for (int i = 0; i < storedMinionInfo.Count; i++)
			{
				MinionStorage.Info info = storedMinionInfo[i];
				if (flag && info.serializedMinion != null && info.serializedMinion.GetId() == -1 && info.name == this.storedName)
				{
					DebugUtil.LogWarningArgs(new object[]
					{
						"Found a minion storage with an invalid ref, rebinding.",
						component.InstanceID,
						this.storedName,
						minionStorage.gameObject.name
					});
					info = new MinionStorage.Info(this.storedName, new Ref<KPrefabID>(component));
					storedMinionInfo[i] = info;
					minionStorage.GetComponent<Assignable>().Assign(this);
					flag2 = true;
					break;
				}
				if (info.serializedMinion != null && info.serializedMinion.Get() == component)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				break;
			}
		}
		if (!flag2)
		{
			DebugUtil.LogWarningArgs(new object[] { "Found a stored minion that wasn't in any minion storage. Respawning them at the portal.", component.InstanceID, this.storedName });
			GameObject activeTelepad = GameUtil.GetActiveTelepad();
			if (activeTelepad != null)
			{
				MinionStorage.DeserializeMinion(component.gameObject, activeTelepad.transform.GetPosition());
			}
		}
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x0009CB18 File Offset: 0x0009AD18
	public string GetProperName()
	{
		return this.storedName;
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0009CB20 File Offset: 0x0009AD20
	public List<Ownables> GetOwners()
	{
		return this.assignableProxy.Get().ownables;
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x0009CB32 File Offset: 0x0009AD32
	public Ownables GetSoleOwner()
	{
		return this.assignableProxy.Get().GetComponent<Ownables>();
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x0009CB44 File Offset: 0x0009AD44
	public bool HasOwner(Assignables owner)
	{
		return this.GetOwners().Contains(owner as Ownables);
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0009CB57 File Offset: 0x0009AD57
	public int NumOwners()
	{
		return this.GetOwners().Count;
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x0009CB64 File Offset: 0x0009AD64
	public Accessory GetAccessory(AccessorySlot slot)
	{
		for (int i = 0; i < this.accessories.Count; i++)
		{
			if (this.accessories[i].Get() != null && this.accessories[i].Get().slot == slot)
			{
				return this.accessories[i].Get();
			}
		}
		return null;
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x0009CBC6 File Offset: 0x0009ADC6
	public bool IsNull()
	{
		return this == null;
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x0009CBD0 File Offset: 0x0009ADD0
	public string GetStorageReason()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			using (List<MinionStorage.Info>.Enumerator enumerator2 = minionStorage.GetStoredMinionInfo().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.serializedMinion.Get() == component)
					{
						return minionStorage.GetProperName();
					}
				}
			}
		}
		return "";
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x0009CC88 File Offset: 0x0009AE88
	public bool IsPermittedToConsume(string consumable)
	{
		return !this.forbiddenTagSet.Contains(consumable);
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x0009CCA0 File Offset: 0x0009AEA0
	public bool IsChoreGroupDisabled(ChoreGroup chore_group)
	{
		foreach (string text in this.traitIDs)
		{
			if (Db.Get().traits.Exists(text))
			{
				Trait trait = Db.Get().traits.Get(text);
				if (trait.disabledChoreGroups != null)
				{
					ChoreGroup[] disabledChoreGroups = trait.disabledChoreGroups;
					for (int i = 0; i < disabledChoreGroups.Length; i++)
					{
						if (disabledChoreGroups[i].IdHash == chore_group.IdHash)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x0009CD50 File Offset: 0x0009AF50
	public int GetPersonalPriority(ChoreGroup chore_group)
	{
		ChoreConsumer.PriorityInfo priorityInfo;
		if (this.choreGroupPriorities.TryGetValue(chore_group.IdHash, out priorityInfo))
		{
			return priorityInfo.priority;
		}
		return 0;
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x0009CD7A File Offset: 0x0009AF7A
	public int GetAssociatedSkillLevel(ChoreGroup group)
	{
		return 0;
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x0009CD7D File Offset: 0x0009AF7D
	public void SetPersonalPriority(ChoreGroup group, int value)
	{
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x0009CD7F File Offset: 0x0009AF7F
	public void ResetPersonalPriorities()
	{
	}

	// Token: 0x04001073 RID: 4211
	[Serialize]
	public string storedName;

	// Token: 0x04001074 RID: 4212
	[Serialize]
	public string gender;

	// Token: 0x04001078 RID: 4216
	[Serialize]
	[ReadOnly]
	public float arrivalTime;

	// Token: 0x04001079 RID: 4217
	[Serialize]
	public int voiceIdx;

	// Token: 0x0400107A RID: 4218
	[Serialize]
	public KCompBuilder.BodyData bodyData;

	// Token: 0x0400107B RID: 4219
	[Serialize]
	public List<Ref<KPrefabID>> assignedItems;

	// Token: 0x0400107C RID: 4220
	[Serialize]
	public List<Ref<KPrefabID>> equippedItems;

	// Token: 0x0400107D RID: 4221
	[Serialize]
	public List<string> traitIDs;

	// Token: 0x0400107E RID: 4222
	[Serialize]
	public List<ResourceRef<Accessory>> accessories;

	// Token: 0x0400107F RID: 4223
	[Serialize]
	public List<ResourceRef<ClothingItemResource>> clothingItems = new List<ResourceRef<ClothingItemResource>>();

	// Token: 0x04001080 RID: 4224
	[Obsolete("Deprecated, use forbiddenTagSet")]
	[Serialize]
	public List<Tag> forbiddenTags;

	// Token: 0x04001081 RID: 4225
	[Serialize]
	public HashSet<Tag> forbiddenTagSet;

	// Token: 0x04001082 RID: 4226
	[Serialize]
	public Ref<MinionAssignablesProxy> assignableProxy;

	// Token: 0x04001083 RID: 4227
	[Serialize]
	public List<Effects.SaveLoadEffect> saveLoadEffects;

	// Token: 0x04001084 RID: 4228
	[Serialize]
	public List<Effects.SaveLoadImmunities> saveLoadImmunities;

	// Token: 0x04001085 RID: 4229
	[Serialize]
	public Dictionary<string, bool> MasteryByRoleID = new Dictionary<string, bool>();

	// Token: 0x04001086 RID: 4230
	[Serialize]
	public Dictionary<string, bool> MasteryBySkillID = new Dictionary<string, bool>();

	// Token: 0x04001087 RID: 4231
	[Serialize]
	public List<string> grantedSkillIDs = new List<string>();

	// Token: 0x04001088 RID: 4232
	[Serialize]
	public Dictionary<HashedString, float> AptitudeByRoleGroup = new Dictionary<HashedString, float>();

	// Token: 0x04001089 RID: 4233
	[Serialize]
	public Dictionary<HashedString, float> AptitudeBySkillGroup = new Dictionary<HashedString, float>();

	// Token: 0x0400108A RID: 4234
	[Serialize]
	public float TotalExperienceGained;

	// Token: 0x0400108B RID: 4235
	[Serialize]
	public string currentHat;

	// Token: 0x0400108C RID: 4236
	[Serialize]
	public string targetHat;

	// Token: 0x0400108D RID: 4237
	[Serialize]
	public Dictionary<HashedString, ChoreConsumer.PriorityInfo> choreGroupPriorities = new Dictionary<HashedString, ChoreConsumer.PriorityInfo>();

	// Token: 0x0400108E RID: 4238
	[Serialize]
	public List<AttributeLevels.LevelSaveLoad> attributeLevels;

	// Token: 0x0400108F RID: 4239
	[Serialize]
	public Dictionary<string, float> savedAttributeValues;

	// Token: 0x04001090 RID: 4240
	public MinionModifiers minionModifiers;
}
