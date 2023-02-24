using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CBB RID: 3259
	public class TechItems : ResourceSet<TechItem>
	{
		// Token: 0x0600660A RID: 26122 RVA: 0x00272A85 File Offset: 0x00270C85
		public TechItems(ResourceSet parent)
			: base("TechItems", parent)
		{
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x00272A94 File Offset: 0x00270C94
		public void Init()
		{
			this.automationOverlay = this.AddTechItem("AutomationOverlay", RESEARCH.OTHER_TECH_ITEMS.AUTOMATION_OVERLAY.NAME, RESEARCH.OTHER_TECH_ITEMS.AUTOMATION_OVERLAY.DESC, this.GetSpriteFnBuilder("overlay_logic"), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.suitsOverlay = this.AddTechItem("SuitsOverlay", RESEARCH.OTHER_TECH_ITEMS.SUITS_OVERLAY.NAME, RESEARCH.OTHER_TECH_ITEMS.SUITS_OVERLAY.DESC, this.GetSpriteFnBuilder("overlay_suit"), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.betaResearchPoint = this.AddTechItem("BetaResearchPoint", RESEARCH.OTHER_TECH_ITEMS.BETA_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.BETA_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_beta_icon"), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.gammaResearchPoint = this.AddTechItem("GammaResearchPoint", RESEARCH.OTHER_TECH_ITEMS.GAMMA_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.GAMMA_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_gamma_icon"), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.orbitalResearchPoint = this.AddTechItem("OrbitalResearchPoint", RESEARCH.OTHER_TECH_ITEMS.ORBITAL_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.ORBITAL_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_orbital_icon"), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.conveyorOverlay = this.AddTechItem("ConveyorOverlay", RESEARCH.OTHER_TECH_ITEMS.CONVEYOR_OVERLAY.NAME, RESEARCH.OTHER_TECH_ITEMS.CONVEYOR_OVERLAY.DESC, this.GetSpriteFnBuilder("overlay_conveyor"), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.jetSuit = this.AddTechItem("JetSuit", RESEARCH.OTHER_TECH_ITEMS.JET_SUIT.NAME, RESEARCH.OTHER_TECH_ITEMS.JET_SUIT.DESC, this.GetPrefabSpriteFnBuilder("Jet_Suit".ToTag()), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.atmoSuit = this.AddTechItem("AtmoSuit", RESEARCH.OTHER_TECH_ITEMS.ATMO_SUIT.NAME, RESEARCH.OTHER_TECH_ITEMS.ATMO_SUIT.DESC, this.GetPrefabSpriteFnBuilder("Atmo_Suit".ToTag()), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.oxygenMask = this.AddTechItem("OxygenMask", RESEARCH.OTHER_TECH_ITEMS.OXYGEN_MASK.NAME, RESEARCH.OTHER_TECH_ITEMS.OXYGEN_MASK.DESC, this.GetPrefabSpriteFnBuilder("Oxygen_Mask".ToTag()), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.deltaResearchPoint = this.AddTechItem("DeltaResearchPoint", RESEARCH.OTHER_TECH_ITEMS.DELTA_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.DELTA_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_delta_icon"), DlcManager.AVAILABLE_EXPANSION1_ONLY);
			this.leadSuit = this.AddTechItem("LeadSuit", RESEARCH.OTHER_TECH_ITEMS.LEAD_SUIT.NAME, RESEARCH.OTHER_TECH_ITEMS.LEAD_SUIT.DESC, this.GetPrefabSpriteFnBuilder("Lead_Suit".ToTag()), DlcManager.AVAILABLE_EXPANSION1_ONLY);
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x00272CFC File Offset: 0x00270EFC
		private Func<string, bool, Sprite> GetSpriteFnBuilder(string spriteName)
		{
			return (string anim, bool centered) => Assets.GetSprite(spriteName);
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x00272D15 File Offset: 0x00270F15
		private Func<string, bool, Sprite> GetPrefabSpriteFnBuilder(Tag prefabTag)
		{
			return (string anim, bool centered) => Def.GetUISprite(prefabTag, "ui", false).first;
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x00272D30 File Offset: 0x00270F30
		public TechItem AddTechItem(string id, string name, string description, Func<string, bool, Sprite> getUISprite, string[] DLCIds)
		{
			if (!DlcManager.IsDlcListValidForCurrentContent(DLCIds))
			{
				return null;
			}
			if (base.TryGet(id) != null)
			{
				DebugUtil.LogWarningArgs(new object[] { "Tried adding a tech item called", id, name, "but it was already added!" });
				return base.Get(id);
			}
			Tech techFromItemID = this.GetTechFromItemID(id);
			if (techFromItemID == null)
			{
				return null;
			}
			TechItem techItem = new TechItem(id, this, name, description, getUISprite, techFromItemID.Id, DLCIds);
			base.Add(techItem);
			techFromItemID.unlockedItems.Add(techItem);
			return techItem;
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x00272DB4 File Offset: 0x00270FB4
		public bool IsTechItemComplete(string id)
		{
			bool flag = true;
			foreach (TechItem techItem in this.resources)
			{
				if (techItem.Id == id)
				{
					flag = techItem.IsComplete();
					break;
				}
			}
			return flag;
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x00272E1C File Offset: 0x0027101C
		private Tech GetTechFromItemID(string itemId)
		{
			if (Db.Get().Techs == null)
			{
				return null;
			}
			return Db.Get().Techs.TryGetTechForTechItem(itemId);
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x00272E3C File Offset: 0x0027103C
		public int GetTechTierForItem(string itemId)
		{
			Tech techFromItemID = this.GetTechFromItemID(itemId);
			if (techFromItemID != null)
			{
				return Techs.GetTier(techFromItemID);
			}
			return 0;
		}

		// Token: 0x04004A70 RID: 19056
		public const string AUTOMATION_OVERLAY_ID = "AutomationOverlay";

		// Token: 0x04004A71 RID: 19057
		public TechItem automationOverlay;

		// Token: 0x04004A72 RID: 19058
		public const string SUITS_OVERLAY_ID = "SuitsOverlay";

		// Token: 0x04004A73 RID: 19059
		public TechItem suitsOverlay;

		// Token: 0x04004A74 RID: 19060
		public const string JET_SUIT_ID = "JetSuit";

		// Token: 0x04004A75 RID: 19061
		public TechItem jetSuit;

		// Token: 0x04004A76 RID: 19062
		public const string ATMO_SUIT_ID = "AtmoSuit";

		// Token: 0x04004A77 RID: 19063
		public TechItem atmoSuit;

		// Token: 0x04004A78 RID: 19064
		public const string OXYGEN_MASK_ID = "OxygenMask";

		// Token: 0x04004A79 RID: 19065
		public TechItem oxygenMask;

		// Token: 0x04004A7A RID: 19066
		public const string LEAD_SUIT_ID = "LeadSuit";

		// Token: 0x04004A7B RID: 19067
		public TechItem leadSuit;

		// Token: 0x04004A7C RID: 19068
		public const string BETA_RESEARCH_POINT_ID = "BetaResearchPoint";

		// Token: 0x04004A7D RID: 19069
		public TechItem betaResearchPoint;

		// Token: 0x04004A7E RID: 19070
		public const string GAMMA_RESEARCH_POINT_ID = "GammaResearchPoint";

		// Token: 0x04004A7F RID: 19071
		public TechItem gammaResearchPoint;

		// Token: 0x04004A80 RID: 19072
		public const string DELTA_RESEARCH_POINT_ID = "DeltaResearchPoint";

		// Token: 0x04004A81 RID: 19073
		public TechItem deltaResearchPoint;

		// Token: 0x04004A82 RID: 19074
		public const string ORBITAL_RESEARCH_POINT_ID = "OrbitalResearchPoint";

		// Token: 0x04004A83 RID: 19075
		public TechItem orbitalResearchPoint;

		// Token: 0x04004A84 RID: 19076
		public const string CONVEYOR_OVERLAY_ID = "ConveyorOverlay";

		// Token: 0x04004A85 RID: 19077
		public TechItem conveyorOverlay;
	}
}
