using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C9A RID: 3226
	public class MiscStatusItems : StatusItems
	{
		// Token: 0x0600659F RID: 26015 RVA: 0x0026C6E5 File Offset: 0x0026A8E5
		public MiscStatusItems(ResourceSet parent)
			: base("MiscStatusItems", parent)
		{
			this.CreateStatusItems();
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x0026C6FC File Offset: 0x0026A8FC
		private StatusItem CreateStatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, bool showWorldIcon = true, int status_overlays = 129022)
		{
			return base.Add(new StatusItem(id, prefix, icon, icon_type, notification_type, allow_multiples, render_overlay, showWorldIcon, status_overlays, null));
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0026C724 File Offset: 0x0026A924
		private StatusItem CreateStatusItem(string id, string name, string tooltip, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, int status_overlays = 129022)
		{
			return base.Add(new StatusItem(id, name, tooltip, icon, icon_type, notification_type, allow_multiples, render_overlay, status_overlays, true, null));
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0026C750 File Offset: 0x0026A950
		private void CreateStatusItems()
		{
			this.AttentionRequired = this.CreateStatusItem("AttentionRequired", "MISC", "status_item_doubleexclamation", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Edible = this.CreateStatusItem("Edible", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Edible.resolveStringCallback = delegate(string str, object data)
			{
				Edible edible = (Edible)data;
				str = string.Format(str, GameUtil.GetFormattedCalories(edible.Calories, GameUtil.TimeSlice.None, true));
				return str;
			};
			this.PendingClear = this.CreateStatusItem("PendingClear", "MISC", "status_item_pending_clear", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingClearNoStorage = this.CreateStatusItem("PendingClearNoStorage", "MISC", "status_item_pending_clear", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.MarkedForCompost = this.CreateStatusItem("MarkedForCompost", "MISC", "status_item_pending_compost", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.MarkedForCompostInStorage = this.CreateStatusItem("MarkedForCompostInStorage", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.MarkedForDisinfection = this.CreateStatusItem("MarkedForDisinfection", "MISC", "status_item_disinfect", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.Disease.ID, true, 129022);
			this.NoClearLocationsAvailable = this.CreateStatusItem("NoClearLocationsAvailable", "MISC", "status_item_no_filter_set", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.WaitingForDig = this.CreateStatusItem("WaitingForDig", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.WaitingForMop = this.CreateStatusItem("WaitingForMop", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.OreMass = this.CreateStatusItem("OreMass", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.OreMass.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject = (GameObject)data;
				str = str.Replace("{Mass}", GameUtil.GetFormattedMass(gameObject.GetComponent<PrimaryElement>().Mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.OreTemp = this.CreateStatusItem("OreTemp", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.OreTemp.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject2 = (GameObject)data;
				str = str.Replace("{Temp}", GameUtil.GetFormattedTemperature(gameObject2.GetComponent<PrimaryElement>().Temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.ElementalState = this.CreateStatusItem("ElementalState", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ElementalState.resolveStringCallback = delegate(string str, object data)
			{
				Element element = ((Func<Element>)data)();
				str = str.Replace("{State}", element.GetStateString());
				return str;
			};
			this.ElementalCategory = this.CreateStatusItem("ElementalCategory", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ElementalCategory.resolveStringCallback = delegate(string str, object data)
			{
				Element element2 = ((Func<Element>)data)();
				str = str.Replace("{Category}", element2.GetMaterialCategoryTag().ProperName());
				return str;
			};
			this.ElementalTemperature = this.CreateStatusItem("ElementalTemperature", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ElementalTemperature.resolveStringCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject = (CellSelectionObject)data;
				str = str.Replace("{Temp}", GameUtil.GetFormattedTemperature(cellSelectionObject.temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.ElementalMass = this.CreateStatusItem("ElementalMass", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ElementalMass.resolveStringCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject2 = (CellSelectionObject)data;
				str = str.Replace("{Mass}", GameUtil.GetFormattedMass(cellSelectionObject2.Mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.ElementalDisease = this.CreateStatusItem("ElementalDisease", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ElementalDisease.resolveStringCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject3 = (CellSelectionObject)data;
				str = str.Replace("{Disease}", GameUtil.GetFormattedDisease(cellSelectionObject3.diseaseIdx, cellSelectionObject3.diseaseCount, false));
				return str;
			};
			this.ElementalDisease.resolveTooltipCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject4 = (CellSelectionObject)data;
				str = str.Replace("{Disease}", GameUtil.GetFormattedDisease(cellSelectionObject4.diseaseIdx, cellSelectionObject4.diseaseCount, true));
				return str;
			};
			this.TreeFilterableTags = this.CreateStatusItem("TreeFilterableTags", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.TreeFilterableTags.resolveStringCallback = delegate(string str, object data)
			{
				TreeFilterable treeFilterable = (TreeFilterable)data;
				str = str.Replace("{Tags}", treeFilterable.GetTagsAsStatus(6));
				return str;
			};
			this.SublimationEmitting = this.CreateStatusItem("SublimationEmitting", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SublimationEmitting.resolveStringCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject5 = (CellSelectionObject)data;
				if (cellSelectionObject5.element.sublimateId == (SimHashes)0)
				{
					return str;
				}
				str = str.Replace("{Element}", GameUtil.GetElementNameByElementHash(cellSelectionObject5.element.sublimateId));
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(cellSelectionObject5.FlowRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.SublimationEmitting.resolveTooltipCallback = this.SublimationEmitting.resolveStringCallback;
			this.SublimationBlocked = this.CreateStatusItem("SublimationBlocked", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SublimationBlocked.resolveStringCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject6 = (CellSelectionObject)data;
				if (cellSelectionObject6.element.sublimateId == (SimHashes)0)
				{
					return str;
				}
				str = str.Replace("{Element}", cellSelectionObject6.element.name);
				str = str.Replace("{SubElement}", GameUtil.GetElementNameByElementHash(cellSelectionObject6.element.sublimateId));
				return str;
			};
			this.SublimationBlocked.resolveTooltipCallback = this.SublimationBlocked.resolveStringCallback;
			this.SublimationOverpressure = this.CreateStatusItem("SublimationOverpressure", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SublimationOverpressure.resolveTooltipCallback = delegate(string str, object data)
			{
				CellSelectionObject cellSelectionObject7 = (CellSelectionObject)data;
				if (cellSelectionObject7.element.sublimateId == (SimHashes)0)
				{
					return str;
				}
				str = str.Replace("{Element}", cellSelectionObject7.element.name);
				str = str.Replace("{SubElement}", GameUtil.GetElementNameByElementHash(cellSelectionObject7.element.sublimateId));
				return str;
			};
			this.Space = this.CreateStatusItem("Space", "MISC", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022);
			this.BuriedItem = this.CreateStatusItem("BuriedItem", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpoutOverPressure = this.CreateStatusItem("SpoutOverPressure", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpoutOverPressure.resolveStringCallback = delegate(string str, object data)
			{
				Geyser.StatesInstance statesInstance = (Geyser.StatesInstance)data;
				Studyable component = statesInstance.GetComponent<Studyable>();
				if (statesInstance != null && component != null && component.Studied)
				{
					str = str.Replace("{StudiedDetails}", MISC.STATUSITEMS.SPOUTOVERPRESSURE.STUDIED.text.Replace("{Time}", GameUtil.GetFormattedCycles(statesInstance.master.RemainingEruptTime(), "F1", false)));
				}
				else
				{
					str = str.Replace("{StudiedDetails}", "");
				}
				return str;
			};
			this.SpoutEmitting = this.CreateStatusItem("SpoutEmitting", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpoutEmitting.resolveStringCallback = delegate(string str, object data)
			{
				Geyser.StatesInstance statesInstance2 = (Geyser.StatesInstance)data;
				Studyable component2 = statesInstance2.GetComponent<Studyable>();
				if (statesInstance2 != null && component2 != null && component2.Studied)
				{
					str = str.Replace("{StudiedDetails}", MISC.STATUSITEMS.SPOUTEMITTING.STUDIED.text.Replace("{Time}", GameUtil.GetFormattedCycles(statesInstance2.master.RemainingEruptTime(), "F1", false)));
				}
				else
				{
					str = str.Replace("{StudiedDetails}", "");
				}
				return str;
			};
			this.SpoutPressureBuilding = this.CreateStatusItem("SpoutPressureBuilding", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpoutPressureBuilding.resolveStringCallback = delegate(string str, object data)
			{
				Geyser.StatesInstance statesInstance3 = (Geyser.StatesInstance)data;
				Studyable component3 = statesInstance3.GetComponent<Studyable>();
				if (statesInstance3 != null && component3 != null && component3.Studied)
				{
					str = str.Replace("{StudiedDetails}", MISC.STATUSITEMS.SPOUTPRESSUREBUILDING.STUDIED.text.Replace("{Time}", GameUtil.GetFormattedCycles(statesInstance3.master.RemainingNonEruptTime(), "F1", false)));
				}
				else
				{
					str = str.Replace("{StudiedDetails}", "");
				}
				return str;
			};
			this.SpoutIdle = this.CreateStatusItem("SpoutIdle", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpoutIdle.resolveStringCallback = delegate(string str, object data)
			{
				Geyser.StatesInstance statesInstance4 = (Geyser.StatesInstance)data;
				Studyable component4 = statesInstance4.GetComponent<Studyable>();
				if (statesInstance4 != null && component4 != null && component4.Studied)
				{
					str = str.Replace("{StudiedDetails}", MISC.STATUSITEMS.SPOUTIDLE.STUDIED.text.Replace("{Time}", GameUtil.GetFormattedCycles(statesInstance4.master.RemainingNonEruptTime(), "F1", false)));
				}
				else
				{
					str = str.Replace("{StudiedDetails}", "");
				}
				return str;
			};
			this.SpoutDormant = this.CreateStatusItem("SpoutDormant", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpicedFood = this.CreateStatusItem("SpicedFood", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpicedFood.resolveTooltipCallback = delegate(string baseString, object data)
			{
				string text = baseString;
				string text2 = "\n    • ";
				foreach (SpiceInstance spiceInstance in ((List<SpiceInstance>)data))
				{
					string text3 = "STRINGS.ITEMS.SPICES.";
					Tag id = spiceInstance.Id;
					string text4 = text3 + id.Name.ToUpper() + ".NAME";
					StringEntry stringEntry;
					Strings.TryGet(text4, out stringEntry);
					string text5 = ((stringEntry == null) ? ("MISSING " + text4) : stringEntry.String);
					text = text + text2 + text5;
					string text6 = "\n        • ";
					if (spiceInstance.StatBonus != null)
					{
						text += Effect.CreateTooltip(spiceInstance.StatBonus, false, text6, false);
					}
				}
				return text;
			};
			this.OrderAttack = this.CreateStatusItem("OrderAttack", "MISC", "status_item_attack", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.OrderCapture = this.CreateStatusItem("OrderCapture", "MISC", "status_item_capture", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingHarvest = this.CreateStatusItem("PendingHarvest", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NotMarkedForHarvest = this.CreateStatusItem("NotMarkedForHarvest", "MISC", "status_item_building_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NotMarkedForHarvest.conditionalOverlayCallback = (HashedString viewMode, object o) => !(viewMode != OverlayModes.None.ID);
			this.PendingUproot = this.CreateStatusItem("PendingUproot", "MISC", "status_item_pending_uproot", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PickupableUnreachable = this.CreateStatusItem("PickupableUnreachable", "MISC", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Prioritized = this.CreateStatusItem("Prioritized", "MISC", "status_item_prioritized", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Using = this.CreateStatusItem("Using", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Using.resolveStringCallback = delegate(string str, object data)
			{
				Workable workable = (Workable)data;
				if (workable != null)
				{
					KSelectable component5 = workable.GetComponent<KSelectable>();
					if (component5 != null)
					{
						str = str.Replace("{Target}", component5.GetName());
					}
				}
				return str;
			};
			this.Operating = this.CreateStatusItem("Operating", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Cleaning = this.CreateStatusItem("Cleaning", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RegionIsBlocked = this.CreateStatusItem("RegionIsBlocked", "MISC", "status_item_solids_blocking", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.AwaitingStudy = this.CreateStatusItem("AwaitingStudy", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Studied = this.CreateStatusItem("Studied", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.HighEnergyParticleCount = this.CreateStatusItem("HighEnergyParticleCount", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.HighEnergyParticleCount.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject3 = (GameObject)data;
				return GameUtil.GetFormattedHighEnergyParticles(gameObject3.IsNullOrDestroyed() ? 0f : gameObject3.GetComponent<HighEnergyParticle>().payload, GameUtil.TimeSlice.None, true);
			};
			this.Durability = this.CreateStatusItem("Durability", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Durability.resolveStringCallback = delegate(string str, object data)
			{
				Durability component6 = ((GameObject)data).GetComponent<Durability>();
				str = str.Replace("{durability}", GameUtil.GetFormattedPercent(component6.GetDurability() * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.StoredItemDurability = this.CreateStatusItem("StoredItemDurability", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.StoredItemDurability.resolveStringCallback = delegate(string str, object data)
			{
				Durability component7 = ((GameObject)data).GetComponent<Durability>();
				float num = ((component7 != null) ? (component7.GetDurability() * 100f) : 100f);
				str = str.Replace("{durability}", GameUtil.GetFormattedPercent(num, GameUtil.TimeSlice.None));
				return str;
			};
			this.ArtifactEntombed = this.CreateStatusItem("ArtifactEntombed", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.TearOpen = this.CreateStatusItem("TearOpen", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.TearClosed = this.CreateStatusItem("TearClosed", "MISC", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
		}

		// Token: 0x0400494D RID: 18765
		public StatusItem AttentionRequired;

		// Token: 0x0400494E RID: 18766
		public StatusItem MarkedForDisinfection;

		// Token: 0x0400494F RID: 18767
		public StatusItem MarkedForCompost;

		// Token: 0x04004950 RID: 18768
		public StatusItem MarkedForCompostInStorage;

		// Token: 0x04004951 RID: 18769
		public StatusItem PendingClear;

		// Token: 0x04004952 RID: 18770
		public StatusItem PendingClearNoStorage;

		// Token: 0x04004953 RID: 18771
		public StatusItem Edible;

		// Token: 0x04004954 RID: 18772
		public StatusItem WaitingForDig;

		// Token: 0x04004955 RID: 18773
		public StatusItem WaitingForMop;

		// Token: 0x04004956 RID: 18774
		public StatusItem OreMass;

		// Token: 0x04004957 RID: 18775
		public StatusItem OreTemp;

		// Token: 0x04004958 RID: 18776
		public StatusItem ElementalCategory;

		// Token: 0x04004959 RID: 18777
		public StatusItem ElementalState;

		// Token: 0x0400495A RID: 18778
		public StatusItem ElementalTemperature;

		// Token: 0x0400495B RID: 18779
		public StatusItem ElementalMass;

		// Token: 0x0400495C RID: 18780
		public StatusItem ElementalDisease;

		// Token: 0x0400495D RID: 18781
		public StatusItem TreeFilterableTags;

		// Token: 0x0400495E RID: 18782
		public StatusItem SublimationOverpressure;

		// Token: 0x0400495F RID: 18783
		public StatusItem SublimationEmitting;

		// Token: 0x04004960 RID: 18784
		public StatusItem SublimationBlocked;

		// Token: 0x04004961 RID: 18785
		public StatusItem BuriedItem;

		// Token: 0x04004962 RID: 18786
		public StatusItem SpoutOverPressure;

		// Token: 0x04004963 RID: 18787
		public StatusItem SpoutEmitting;

		// Token: 0x04004964 RID: 18788
		public StatusItem SpoutPressureBuilding;

		// Token: 0x04004965 RID: 18789
		public StatusItem SpoutIdle;

		// Token: 0x04004966 RID: 18790
		public StatusItem SpoutDormant;

		// Token: 0x04004967 RID: 18791
		public StatusItem SpicedFood;

		// Token: 0x04004968 RID: 18792
		public StatusItem OrderAttack;

		// Token: 0x04004969 RID: 18793
		public StatusItem OrderCapture;

		// Token: 0x0400496A RID: 18794
		public StatusItem PendingHarvest;

		// Token: 0x0400496B RID: 18795
		public StatusItem NotMarkedForHarvest;

		// Token: 0x0400496C RID: 18796
		public StatusItem PendingUproot;

		// Token: 0x0400496D RID: 18797
		public StatusItem PickupableUnreachable;

		// Token: 0x0400496E RID: 18798
		public StatusItem Prioritized;

		// Token: 0x0400496F RID: 18799
		public StatusItem Using;

		// Token: 0x04004970 RID: 18800
		public StatusItem Operating;

		// Token: 0x04004971 RID: 18801
		public StatusItem Cleaning;

		// Token: 0x04004972 RID: 18802
		public StatusItem RegionIsBlocked;

		// Token: 0x04004973 RID: 18803
		public StatusItem NoClearLocationsAvailable;

		// Token: 0x04004974 RID: 18804
		public StatusItem AwaitingStudy;

		// Token: 0x04004975 RID: 18805
		public StatusItem Studied;

		// Token: 0x04004976 RID: 18806
		public StatusItem StudiedGeyserTimeRemaining;

		// Token: 0x04004977 RID: 18807
		public StatusItem Space;

		// Token: 0x04004978 RID: 18808
		public StatusItem HighEnergyParticleCount;

		// Token: 0x04004979 RID: 18809
		public StatusItem Durability;

		// Token: 0x0400497A RID: 18810
		public StatusItem StoredItemDurability;

		// Token: 0x0400497B RID: 18811
		public StatusItem ArtifactEntombed;

		// Token: 0x0400497C RID: 18812
		public StatusItem TearOpen;

		// Token: 0x0400497D RID: 18813
		public StatusItem TearClosed;
	}
}
