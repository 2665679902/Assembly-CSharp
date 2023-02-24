using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class LeadSuitConfig : IEquipmentConfig
{
	// Token: 0x06000238 RID: 568 RVA: 0x000101F1 File Offset: 0x0000E3F1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000101F8 File Offset: 0x0000E3F8
	public EquipmentDef CreateEquipmentDef()
	{
		List<AttributeModifier> list = new List<AttributeModifier>();
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.ATHLETICS, (float)TUNING.EQUIPMENT.SUITS.LEADSUIT_ATHLETICS, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(Db.Get().Attributes.ScaldingThreshold.Id, (float)TUNING.EQUIPMENT.SUITS.LEADSUIT_SCALDING, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(Db.Get().Attributes.RadiationResistance.Id, TUNING.EQUIPMENT.SUITS.LEADSUIT_RADIATION_SHIELDING, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(Db.Get().Attributes.Strength.Id, (float)TUNING.EQUIPMENT.SUITS.LEADSUIT_STRENGTH, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.INSULATION, (float)TUNING.EQUIPMENT.SUITS.LEADSUIT_INSULATION, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.THERMAL_CONDUCTIVITY_BARRIER, TUNING.EQUIPMENT.SUITS.LEADSUIT_THERMAL_CONDUCTIVITY_BARRIER, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.NAME, false, false, true));
		this.expertAthleticsModifier = new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.ATHLETICS, (float)(-(float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS), Db.Get().Skills.Suits1.Name, false, false, true);
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("Lead_Suit", TUNING.EQUIPMENT.SUITS.SLOT, SimHashes.Dirt, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_MASS, "suit_leadsuit_kanim", "", "body_leadsuit_kanim", 6, list, null, true, EntityTemplates.CollisionShape.CIRCLE, 0.325f, 0.325f, new Tag[]
		{
			GameTags.Suit,
			GameTags.Clothes
		}, null);
		equipmentDef.wornID = "Worn_Lead_Suit";
		equipmentDef.RecipeDescription = STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.RECIPE_DESC;
		equipmentDef.EffectImmunites.Add(Db.Get().effects.Get("SoakingWet"));
		equipmentDef.EffectImmunites.Add(Db.Get().effects.Get("WetFeet"));
		equipmentDef.EffectImmunites.Add(Db.Get().effects.Get("PoppedEarDrums"));
		equipmentDef.OnEquipCallBack = delegate(Equippable eq)
		{
			Ownables soleOwner = eq.assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				Navigator component = targetGameObject.GetComponent<Navigator>();
				if (component != null)
				{
					component.SetFlags(PathFinder.PotentialPath.Flags.HasAtmoSuit);
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
							component3.ClearFlags(PathFinder.PotentialPath.Flags.HasAtmoSuit);
						}
						Effects component4 = targetGameObject2.GetComponent<Effects>();
						if (component4 != null && component4.HasEffect("SoiledSuit"))
						{
							component4.Remove("SoiledSuit");
						}
					}
					Tag elementTag = eq.GetComponent<SuitTank>().elementTag;
					eq.GetComponent<Storage>().DropUnlessHasTag(elementTag);
				}
			}
		};
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "Lead_Suit");
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "Helmet");
		return equipmentDef;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00010448 File Offset: 0x0000E648
	public void DoPostConfigure(GameObject go)
	{
		SuitTank suitTank = go.AddComponent<SuitTank>();
		suitTank.element = "Oxygen";
		suitTank.capacity = 40f;
		suitTank.elementTag = GameTags.Breathable;
		go.AddComponent<LeadSuitTank>().batteryDuration = 200f;
		go.AddComponent<HelmetController>();
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Clothes, false);
		component.AddTag(GameTags.PedestalDisplayable, false);
		component.AddTag(GameTags.AirtightSuit, false);
		Durability durability = go.AddComponent<Durability>();
		durability.wornEquipmentPrefabID = "Worn_Lead_Suit";
		durability.durabilityLossPerCycle = TUNING.EQUIPMENT.SUITS.ATMOSUIT_DECAY;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage.showInUI = true;
		go.AddOrGet<AtmoSuit>();
		go.AddComponent<SuitDiseaseHandler>();
	}

	// Token: 0x04000134 RID: 308
	public const string ID = "Lead_Suit";

	// Token: 0x04000135 RID: 309
	public const string WORN_ID = "Worn_Lead_Suit";

	// Token: 0x04000136 RID: 310
	public static ComplexRecipe recipe;

	// Token: 0x04000137 RID: 311
	private const PathFinder.PotentialPath.Flags suit_flags = PathFinder.PotentialPath.Flags.HasAtmoSuit;

	// Token: 0x04000138 RID: 312
	private AttributeModifier expertAthleticsModifier;
}
