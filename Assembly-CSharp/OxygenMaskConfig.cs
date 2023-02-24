using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class OxygenMaskConfig : IEquipmentConfig
{
	// Token: 0x0600023E RID: 574 RVA: 0x0001066C File Offset: 0x0000E86C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00010674 File Offset: 0x0000E874
	public EquipmentDef CreateEquipmentDef()
	{
		List<AttributeModifier> list = new List<AttributeModifier>();
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.ATHLETICS, (float)TUNING.EQUIPMENT.SUITS.OXYGEN_MASK_ATHLETICS, STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.NAME, false, false, true));
		this.expertAthleticsModifier = new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.ATHLETICS, (float)(-(float)TUNING.EQUIPMENT.SUITS.OXYGEN_MASK_ATHLETICS), Db.Get().Skills.Suits1.Name, false, false, true);
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("Oxygen_Mask", TUNING.EQUIPMENT.SUITS.SLOT, SimHashes.Dirt, 15f, "oxygen_mask_kanim", "mask_oxygen", "", 6, list, null, false, EntityTemplates.CollisionShape.CIRCLE, 0.325f, 0.325f, new Tag[]
		{
			GameTags.Suit,
			GameTags.Clothes
		}, null);
		equipmentDef.wornID = "Worn_Oxygen_Mask";
		equipmentDef.RecipeDescription = STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.RECIPE_DESC;
		equipmentDef.OnEquipCallBack = delegate(Equippable eq)
		{
			Ownables soleOwner = eq.assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				Navigator component = targetGameObject.GetComponent<Navigator>();
				if (component != null)
				{
					component.SetFlags(PathFinder.PotentialPath.Flags.HasOxygenMask);
				}
				MinionResume component2 = targetGameObject.GetComponent<MinionResume>();
				if (component2 != null && component2.HasPerk(Db.Get().SkillPerks.ExosuitExpertise.Id))
				{
					targetGameObject.GetAttributes().Get(Db.Get().Attributes.Athletics).Add(this.expertAthleticsModifier);
				}
			}
		};
		equipmentDef.OnUnequipCallBack = delegate(Equippable eq)
		{
			if (eq.assignee != null)
			{
				Ownables soleOwner2 = eq.assignee.GetSoleOwner();
				if (soleOwner2 != null)
				{
					GameObject targetGameObject2 = soleOwner2.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
					if (targetGameObject2)
					{
						Attributes attributes = targetGameObject2.GetAttributes();
						if (attributes != null)
						{
							attributes.Get(Db.Get().Attributes.Athletics).Remove(this.expertAthleticsModifier);
						}
						Navigator component3 = targetGameObject2.GetComponent<Navigator>();
						if (component3 != null)
						{
							component3.ClearFlags(PathFinder.PotentialPath.Flags.HasOxygenMask);
						}
					}
				}
			}
		};
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "Oxygen_Mask");
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "Helmet");
		return equipmentDef;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0001078C File Offset: 0x0000E98C
	public void DoPostConfigure(GameObject go)
	{
		Storage storage = go.AddComponent<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage.showInUI = true;
		SuitTank suitTank = go.AddComponent<SuitTank>();
		suitTank.element = "Oxygen";
		suitTank.capacity = 20f;
		suitTank.elementTag = GameTags.Breathable;
		Durability durability = go.AddComponent<Durability>();
		durability.wornEquipmentPrefabID = "Worn_Oxygen_Mask";
		durability.durabilityLossPerCycle = TUNING.EQUIPMENT.SUITS.OXYGEN_MASK_DECAY;
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Clothes, false);
		component.AddTag(GameTags.PedestalDisplayable, false);
		go.AddComponent<SuitDiseaseHandler>();
	}

	// Token: 0x04000139 RID: 313
	public const string ID = "Oxygen_Mask";

	// Token: 0x0400013A RID: 314
	public const string WORN_ID = "Worn_Oxygen_Mask";

	// Token: 0x0400013B RID: 315
	private const PathFinder.PotentialPath.Flags suit_flags = PathFinder.PotentialPath.Flags.HasOxygenMask;

	// Token: 0x0400013C RID: 316
	private AttributeModifier expertAthleticsModifier;
}
