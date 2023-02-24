using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class JetSuitConfig : IEquipmentConfig
{
	// Token: 0x06000232 RID: 562 RVA: 0x0000FD0C File Offset: 0x0000DF0C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000FD14 File Offset: 0x0000DF14
	public EquipmentDef CreateEquipmentDef()
	{
		Dictionary<string, float> dictionary = new Dictionary<string, float>();
		dictionary.Add(SimHashes.Steel.ToString(), 200f);
		dictionary.Add(SimHashes.Petroleum.ToString(), 25f);
		List<AttributeModifier> list = new List<AttributeModifier>();
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.INSULATION, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_INSULATION, STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.ATHLETICS, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS, STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.THERMAL_CONDUCTIVITY_BARRIER, TUNING.EQUIPMENT.SUITS.ATMOSUIT_THERMAL_CONDUCTIVITY_BARRIER, STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(Db.Get().Attributes.Digging.Id, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_DIGGING, STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.NAME, false, false, true));
		list.Add(new AttributeModifier(Db.Get().Attributes.ScaldingThreshold.Id, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_SCALDING, STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.NAME, false, false, true));
		this.expertAthleticsModifier = new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.ATHLETICS, (float)(-(float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS), Db.Get().Skills.Suits1.Name, false, false, true);
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("Jet_Suit", TUNING.EQUIPMENT.SUITS.SLOT, SimHashes.Steel, (float)TUNING.EQUIPMENT.SUITS.ATMOSUIT_MASS, "suit_jetpack_kanim", "", "body_jetpack_kanim", 6, list, null, true, EntityTemplates.CollisionShape.CIRCLE, 0.325f, 0.325f, new Tag[]
		{
			GameTags.Suit,
			GameTags.Clothes
		}, "JetSuit");
		equipmentDef.wornID = "Worn_Jet_Suit";
		equipmentDef.RecipeDescription = STRINGS.EQUIPMENT.PREFABS.JET_SUIT.RECIPE_DESC;
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
					component.SetFlags(PathFinder.PotentialPath.Flags.HasJetPack);
				}
				MinionResume component2 = targetGameObject.GetComponent<MinionResume>();
				if (component2 != null && component2.HasPerk(Db.Get().SkillPerks.ExosuitExpertise.Id))
				{
					targetGameObject.GetAttributes().Get(Db.Get().Attributes.Athletics).Add(this.expertAthleticsModifier);
				}
				KAnimControllerBase component3 = targetGameObject.GetComponent<KAnimControllerBase>();
				if (component3)
				{
					component3.AddAnimOverrides(Assets.GetAnim("anim_loco_hover_kanim"), 0f);
				}
			}
		};
		equipmentDef.OnUnequipCallBack = delegate(Equippable eq)
		{
			if (eq.assignee != null)
			{
				Ownables soleOwner2 = eq.assignee.GetSoleOwner();
				if (soleOwner2)
				{
					GameObject targetGameObject2 = soleOwner2.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
					if (targetGameObject2)
					{
						Attributes attributes = targetGameObject2.GetAttributes();
						if (attributes != null)
						{
							attributes.Get(Db.Get().Attributes.Athletics).Remove(this.expertAthleticsModifier);
						}
						Navigator component4 = targetGameObject2.GetComponent<Navigator>();
						if (component4 != null)
						{
							component4.ClearFlags(PathFinder.PotentialPath.Flags.HasJetPack);
						}
						KAnimControllerBase component5 = targetGameObject2.GetComponent<KAnimControllerBase>();
						if (component5)
						{
							component5.RemoveAnimOverrides(Assets.GetAnim("anim_loco_hover_kanim"));
						}
						Effects component6 = targetGameObject2.GetComponent<Effects>();
						if (component6 != null && component6.HasEffect("SoiledSuit"))
						{
							component6.Remove("SoiledSuit");
						}
					}
					Tag elementTag = eq.GetComponent<SuitTank>().elementTag;
					eq.GetComponent<Storage>().DropUnlessHasTag(elementTag);
				}
			}
		};
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "Jet_Suit");
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "Helmet");
		return equipmentDef;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000FF78 File Offset: 0x0000E178
	public void DoPostConfigure(GameObject go)
	{
		SuitTank suitTank = go.AddComponent<SuitTank>();
		suitTank.element = "Oxygen";
		suitTank.capacity = 75f;
		suitTank.elementTag = GameTags.Breathable;
		go.AddComponent<JetSuitTank>();
		go.AddComponent<HelmetController>().has_jets = true;
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Clothes, false);
		component.AddTag(GameTags.PedestalDisplayable, false);
		component.AddTag(GameTags.AirtightSuit, false);
		Durability durability = go.AddComponent<Durability>();
		durability.wornEquipmentPrefabID = "Worn_Jet_Suit";
		durability.durabilityLossPerCycle = TUNING.EQUIPMENT.SUITS.ATMOSUIT_DECAY;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage.showInUI = true;
		go.AddOrGet<AtmoSuit>();
		go.AddComponent<SuitDiseaseHandler>();
	}

	// Token: 0x0400012F RID: 303
	public const string ID = "Jet_Suit";

	// Token: 0x04000130 RID: 304
	public const string WORN_ID = "Worn_Jet_Suit";

	// Token: 0x04000131 RID: 305
	public static ComplexRecipe recipe;

	// Token: 0x04000132 RID: 306
	private const PathFinder.PotentialPath.Flags suit_flags = PathFinder.PotentialPath.Flags.HasJetPack;

	// Token: 0x04000133 RID: 307
	private AttributeModifier expertAthleticsModifier;
}
