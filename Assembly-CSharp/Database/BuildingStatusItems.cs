using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000C85 RID: 3205
	public class BuildingStatusItems : StatusItems
	{
		// Token: 0x06006550 RID: 25936 RVA: 0x0025FDE2 File Offset: 0x0025DFE2
		public BuildingStatusItems(ResourceSet parent)
			: base("BuildingStatusItems", parent)
		{
			this.CreateStatusItems();
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x0025FDF8 File Offset: 0x0025DFF8
		private StatusItem CreateStatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, bool showWorldIcon = true, int status_overlays = 129022)
		{
			return base.Add(new StatusItem(id, prefix, icon, icon_type, notification_type, allow_multiples, render_overlay, showWorldIcon, status_overlays, null));
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x0025FE20 File Offset: 0x0025E020
		private StatusItem CreateStatusItem(string id, string name, string tooltip, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString render_overlay, int status_overlays = 129022)
		{
			return base.Add(new StatusItem(id, name, tooltip, icon, icon_type, notification_type, allow_multiples, render_overlay, status_overlays, true, null));
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0025FE4C File Offset: 0x0025E04C
		private void CreateStatusItems()
		{
			this.AngerDamage = this.CreateStatusItem("AngerDamage", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022);
			this.AssignedTo = this.CreateStatusItem("AssignedTo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.AssignedTo.resolveStringCallback = delegate(string str, object data)
			{
				IAssignableIdentity assignee = ((Assignable)data).assignee;
				if (!assignee.IsNullOrDestroyed())
				{
					string properName = assignee.GetProperName();
					str = str.Replace("{Assignee}", properName);
				}
				return str;
			};
			this.AssignedToRoom = this.CreateStatusItem("AssignedToRoom", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.AssignedToRoom.resolveStringCallback = delegate(string str, object data)
			{
				IAssignableIdentity assignee2 = ((Assignable)data).assignee;
				if (!assignee2.IsNullOrDestroyed())
				{
					string properName2 = assignee2.GetProperName();
					str = str.Replace("{Assignee}", properName2);
				}
				return str;
			};
			this.Broken = this.CreateStatusItem("Broken", "BUILDING", "status_item_broken", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.Broken.resolveStringCallback = (string str, object data) => str.Replace("{DamageInfo}", ((BuildingHP.SMInstance)data).master.GetDamageSourceInfo().ToString());
			this.Broken.conditionalOverlayCallback = new Func<HashedString, object, bool>(BuildingStatusItems.ShowInUtilityOverlay);
			this.ChangeDoorControlState = this.CreateStatusItem("ChangeDoorControlState", "BUILDING", "status_item_pending_switch_toggle", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ChangeDoorControlState.resolveStringCallback = delegate(string str, object data)
			{
				Door door = (Door)data;
				return str.Replace("{ControlState}", door.RequestedState.ToString());
			};
			this.CurrentDoorControlState = this.CreateStatusItem("CurrentDoorControlState", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CurrentDoorControlState.resolveStringCallback = delegate(string str, object data)
			{
				Door door2 = (Door)data;
				string text = Strings.Get("STRINGS.BUILDING.STATUSITEMS.CURRENTDOORCONTROLSTATE." + door2.CurrentState.ToString().ToUpper());
				return str.Replace("{ControlState}", text);
			};
			this.ClinicOutsideHospital = this.CreateStatusItem("ClinicOutsideHospital", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022);
			this.ConduitBlocked = this.CreateStatusItem("ConduitBlocked", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.OutputPipeFull = this.CreateStatusItem("OutputPipeFull", "BUILDING", "status_item_no_liquid_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.OutputTileBlocked = this.CreateStatusItem("OutputTileBlocked", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ConstructionUnreachable = this.CreateStatusItem("ConstructionUnreachable", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ConduitBlockedMultiples = this.CreateStatusItem("ConduitBlockedMultiples", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, true, OverlayModes.None.ID, true, 129022);
			this.SolidConduitBlockedMultiples = this.CreateStatusItem("SolidConduitBlockedMultiples", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, true, OverlayModes.None.ID, true, 129022);
			this.DigUnreachable = this.CreateStatusItem("DigUnreachable", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.MopUnreachable = this.CreateStatusItem("MopUnreachable", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.StorageUnreachable = this.CreateStatusItem("StorageUnreachable", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.PassengerModuleUnreachable = this.CreateStatusItem("PassengerModuleUnreachable", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.DirectionControl = this.CreateStatusItem("DirectionControl", BUILDING.STATUSITEMS.DIRECTION_CONTROL.NAME, BUILDING.STATUSITEMS.DIRECTION_CONTROL.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.DirectionControl.resolveStringCallback = delegate(string str, object data)
			{
				DirectionControl directionControl = (DirectionControl)data;
				string text2 = BUILDING.STATUSITEMS.DIRECTION_CONTROL.DIRECTIONS.BOTH;
				WorkableReactable.AllowedDirection allowedDirection = directionControl.allowedDirection;
				if (allowedDirection != WorkableReactable.AllowedDirection.Left)
				{
					if (allowedDirection == WorkableReactable.AllowedDirection.Right)
					{
						text2 = BUILDING.STATUSITEMS.DIRECTION_CONTROL.DIRECTIONS.RIGHT;
					}
				}
				else
				{
					text2 = BUILDING.STATUSITEMS.DIRECTION_CONTROL.DIRECTIONS.LEFT;
				}
				str = str.Replace("{Direction}", text2);
				return str;
			};
			this.DeadReactorCoolingOff = this.CreateStatusItem("DeadReactorCoolingOff", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.DeadReactorCoolingOff.resolveStringCallback = delegate(string str, object data)
			{
				Reactor.StatesInstance statesInstance = (Reactor.StatesInstance)data;
				float num = ((Reactor.StatesInstance)data).sm.timeSinceMeltdown.Get(statesInstance);
				str = str.Replace("{CyclesRemaining}", Util.FormatOneDecimalPlace(Mathf.Max(0f, 3000f - num) / 600f));
				return str;
			};
			this.ConstructableDigUnreachable = this.CreateStatusItem("ConstructableDigUnreachable", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.Entombed = this.CreateStatusItem("Entombed", "BUILDING", "status_item_entombed", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.Entombed.AddNotification(null, null, null);
			this.Flooded = this.CreateStatusItem("Flooded", "BUILDING", "status_item_flooded", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.Flooded.AddNotification(null, null, null);
			this.GasVentObstructed = this.CreateStatusItem("GasVentObstructed", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.GasConduits.ID, true, 129022);
			this.GasVentOverPressure = this.CreateStatusItem("GasVentOverPressure", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.GasConduits.ID, true, 129022);
			this.GeneShuffleCompleted = this.CreateStatusItem("GeneShuffleCompleted", "BUILDING", "status_item_pending_upgrade", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.GeneticAnalysisCompleted = this.CreateStatusItem("GeneticAnalysisCompleted", "BUILDING", "status_item_pending_upgrade", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.InvalidBuildingLocation = this.CreateStatusItem("InvalidBuildingLocation", "BUILDING", "status_item_missing_foundation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.LiquidVentObstructed = this.CreateStatusItem("LiquidVentObstructed", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.LiquidVentOverPressure = this.CreateStatusItem("LiquidVentOverPressure", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.MaterialsUnavailable = new MaterialsStatusItem("MaterialsUnavailable", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, true, OverlayModes.None.ID);
			this.MaterialsUnavailable.AddNotification(null, null, null);
			this.MaterialsUnavailable.resolveStringCallback = delegate(string str, object data)
			{
				string text3 = "";
				Dictionary<Tag, float> dictionary = null;
				if (data is IFetchList)
				{
					dictionary = ((IFetchList)data).GetRemainingMinimum();
				}
				else if (data is Dictionary<Tag, float>)
				{
					dictionary = data as Dictionary<Tag, float>;
				}
				if (dictionary.Count > 0)
				{
					bool flag = true;
					foreach (KeyValuePair<Tag, float> keyValuePair in dictionary)
					{
						if (keyValuePair.Value != 0f)
						{
							if (!flag)
							{
								text3 += "\n";
							}
							if (Assets.IsTagCountable(keyValuePair.Key))
							{
								text3 += string.Format(BUILDING.STATUSITEMS.MATERIALSUNAVAILABLE.LINE_ITEM_UNITS, GameUtil.GetUnitFormattedName(keyValuePair.Key.ProperName(), keyValuePair.Value, false));
							}
							else
							{
								text3 += string.Format(BUILDING.STATUSITEMS.MATERIALSUNAVAILABLE.LINE_ITEM_MASS, keyValuePair.Key.ProperName(), GameUtil.GetFormattedMass(keyValuePair.Value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							flag = false;
						}
					}
				}
				str = str.Replace("{ItemsRemaining}", text3);
				return str;
			};
			this.MaterialsUnavailableForRefill = new MaterialsStatusItem("MaterialsUnavailableForRefill", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, true, OverlayModes.None.ID);
			this.MaterialsUnavailableForRefill.resolveStringCallback = delegate(string str, object data)
			{
				IFetchList fetchList = (IFetchList)data;
				string text4 = "";
				Dictionary<Tag, float> remaining = fetchList.GetRemaining();
				if (remaining.Count > 0)
				{
					bool flag2 = true;
					foreach (KeyValuePair<Tag, float> keyValuePair2 in remaining)
					{
						if (keyValuePair2.Value != 0f)
						{
							if (!flag2)
							{
								text4 += "\n";
							}
							text4 += string.Format(BUILDING.STATUSITEMS.MATERIALSUNAVAILABLEFORREFILL.LINE_ITEM, keyValuePair2.Key.ProperName());
							flag2 = false;
						}
					}
				}
				str = str.Replace("{ItemsRemaining}", text4);
				return str;
			};
			Func<string, object, string> func = delegate(string str, object data)
			{
				RoomType roomType = Db.Get().RoomTypes.Get((string)data);
				if (roomType != null)
				{
					return str.Replace("{0}", roomType.Name);
				}
				return str;
			};
			this.NoCoolant = this.CreateStatusItem("NoCoolant", "BUILDING", "status_item_need_supply_in", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NotInAnyRoom = this.CreateStatusItem("NotInAnyRoom", "BUILDING", "status_item_room_required", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NotInRequiredRoom = this.CreateStatusItem("NotInRequiredRoom", "BUILDING", "status_item_room_required", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NotInRequiredRoom.resolveStringCallback = func;
			this.NotInRecommendedRoom = this.CreateStatusItem("NotInRecommendedRoom", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NotInRecommendedRoom.resolveStringCallback = func;
			this.WaitingForRepairMaterials = this.CreateStatusItem("WaitingForRepairMaterials", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Exclamation, NotificationType.Neutral, true, OverlayModes.None.ID, false, 129022);
			this.WaitingForRepairMaterials.resolveStringCallback = delegate(string str, object data)
			{
				KeyValuePair<Tag, float> keyValuePair3 = (KeyValuePair<Tag, float>)data;
				if (keyValuePair3.Value != 0f)
				{
					string text5 = string.Format(BUILDING.STATUSITEMS.WAITINGFORMATERIALS.LINE_ITEM_MASS, keyValuePair3.Key.ProperName(), GameUtil.GetFormattedMass(keyValuePair3.Value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
					str = str.Replace("{ItemsRemaining}", text5);
				}
				return str;
			};
			this.WaitingForMaterials = new MaterialsStatusItem("WaitingForMaterials", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.Neutral, true, OverlayModes.None.ID);
			this.WaitingForMaterials.resolveStringCallback = delegate(string str, object data)
			{
				IFetchList fetchList2 = (IFetchList)data;
				string text6 = "";
				Dictionary<Tag, float> remaining2 = fetchList2.GetRemaining();
				if (remaining2.Count > 0)
				{
					bool flag3 = true;
					foreach (KeyValuePair<Tag, float> keyValuePair4 in remaining2)
					{
						if (keyValuePair4.Value != 0f)
						{
							if (!flag3)
							{
								text6 += "\n";
							}
							if (Assets.IsTagCountable(keyValuePair4.Key))
							{
								text6 += string.Format(BUILDING.STATUSITEMS.WAITINGFORMATERIALS.LINE_ITEM_UNITS, GameUtil.GetUnitFormattedName(keyValuePair4.Key.ProperName(), keyValuePair4.Value, false));
							}
							else
							{
								text6 += string.Format(BUILDING.STATUSITEMS.WAITINGFORMATERIALS.LINE_ITEM_MASS, keyValuePair4.Key.ProperName(), GameUtil.GetFormattedMass(keyValuePair4.Value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
							}
							flag3 = false;
						}
					}
				}
				str = str.Replace("{ItemsRemaining}", text6);
				return str;
			};
			this.WaitingForHighEnergyParticles = new StatusItem("WaitingForRadiation", "BUILDING", "status_item_need_high_energy_particles", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.MeltingDown = this.CreateStatusItem("MeltingDown", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.MissingFoundation = this.CreateStatusItem("MissingFoundation", "BUILDING", "status_item_missing_foundation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NeutroniumUnminable = this.CreateStatusItem("NeutroniumUnminable", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NeedGasIn = this.CreateStatusItem("NeedGasIn", "BUILDING", "status_item_need_supply_in", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.GasConduits.ID, true, 129022);
			this.NeedGasIn.resolveStringCallback = delegate(string str, object data)
			{
				global::Tuple<ConduitType, Tag> tuple = (global::Tuple<ConduitType, Tag>)data;
				string text7 = string.Format(BUILDING.STATUSITEMS.NEEDGASIN.LINE_ITEM, tuple.second.ProperName());
				str = str.Replace("{GasRequired}", text7);
				return str;
			};
			this.NeedGasOut = this.CreateStatusItem("NeedGasOut", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, true, OverlayModes.GasConduits.ID, true, 129022);
			this.NeedLiquidIn = this.CreateStatusItem("NeedLiquidIn", "BUILDING", "status_item_need_supply_in", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.NeedLiquidIn.resolveStringCallback = delegate(string str, object data)
			{
				global::Tuple<ConduitType, Tag> tuple2 = (global::Tuple<ConduitType, Tag>)data;
				string text8 = string.Format(BUILDING.STATUSITEMS.NEEDLIQUIDIN.LINE_ITEM, tuple2.second.ProperName());
				str = str.Replace("{LiquidRequired}", text8);
				return str;
			};
			this.NeedLiquidOut = this.CreateStatusItem("NeedLiquidOut", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, true, OverlayModes.LiquidConduits.ID, true, 129022);
			this.NeedSolidIn = this.CreateStatusItem("NeedSolidIn", "BUILDING", "status_item_need_supply_in", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.SolidConveyor.ID, true, 129022);
			this.NeedSolidOut = this.CreateStatusItem("NeedSolidOut", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, true, OverlayModes.SolidConveyor.ID, true, 129022);
			this.NeedResourceMass = this.CreateStatusItem("NeedResourceMass", "BUILDING", "status_item_need_resource", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NeedResourceMass.resolveStringCallback = delegate(string str, object data)
			{
				string text9 = "";
				EnergyGenerator.Formula formula = (EnergyGenerator.Formula)data;
				if (formula.inputs.Length != 0)
				{
					bool flag4 = true;
					foreach (EnergyGenerator.InputItem inputItem in formula.inputs)
					{
						if (!flag4)
						{
							text9 += "\n";
							flag4 = false;
						}
						text9 += string.Format(BUILDING.STATUSITEMS.NEEDRESOURCEMASS.LINE_ITEM, inputItem.tag.ProperName());
					}
				}
				str = str.Replace("{ResourcesRequired}", text9);
				return str;
			};
			this.LiquidPipeEmpty = this.CreateStatusItem("LiquidPipeEmpty", "BUILDING", "status_item_no_liquid_to_pump", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.LiquidPipeObstructed = this.CreateStatusItem("LiquidPipeObstructed", "BUILDING", "status_item_wrong_resource_in_pipe", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.LiquidConduits.ID, true, 129022);
			this.GasPipeEmpty = this.CreateStatusItem("GasPipeEmpty", "BUILDING", "status_item_no_gas_to_pump", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.GasConduits.ID, true, 129022);
			this.GasPipeObstructed = this.CreateStatusItem("GasPipeObstructed", "BUILDING", "status_item_wrong_resource_in_pipe", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.GasConduits.ID, true, 129022);
			this.SolidPipeObstructed = this.CreateStatusItem("SolidPipeObstructed", "BUILDING", "status_item_wrong_resource_in_pipe", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.SolidConveyor.ID, true, 129022);
			this.NeedPlant = this.CreateStatusItem("NeedPlant", "BUILDING", "status_item_need_plant", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NeedPower = this.CreateStatusItem("NeedPower", "BUILDING", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022);
			this.NotEnoughPower = this.CreateStatusItem("NotEnoughPower", "BUILDING", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022);
			this.PowerLoopDetected = this.CreateStatusItem("PowerLoopDetected", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022);
			this.DispenseRequested = this.CreateStatusItem("DispenseRequested", "BUILDING", "status_item_exclamation", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NewDuplicantsAvailable = this.CreateStatusItem("NewDuplicantsAvailable", "BUILDING", "status_item_new_duplicants_available", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NewDuplicantsAvailable.AddNotification(null, null, null);
			this.NewDuplicantsAvailable.notificationClickCallback = delegate(object data)
			{
				int num2 = 0;
				for (int j = 0; j < Components.Telepads.Items.Count; j++)
				{
					if (Components.Telepads[j].GetComponent<KSelectable>().IsSelected)
					{
						num2 = (j + 1) % Components.Telepads.Items.Count;
						break;
					}
				}
				Telepad targetTelepad = Components.Telepads[num2];
				int myWorldId = targetTelepad.GetMyWorldId();
				CameraController.Instance.ActiveWorldStarWipe(myWorldId, targetTelepad.transform.GetPosition(), 10f, delegate
				{
					SelectTool.Instance.Select(targetTelepad.GetComponent<KSelectable>(), false);
				});
			};
			this.NoStorageFilterSet = this.CreateStatusItem("NoStorageFilterSet", "BUILDING", "status_item_no_filter_set", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoSuitMarker = this.CreateStatusItem("NoSuitMarker", "BUILDING", "status_item_no_filter_set", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.SuitMarkerWrongSide = this.CreateStatusItem("suitMarkerWrongSide", "BUILDING", "status_item_no_filter_set", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.SuitMarkerTraversalAnytime = this.CreateStatusItem("suitMarkerTraversalAnytime", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SuitMarkerTraversalOnlyWhenRoomAvailable = this.CreateStatusItem("suitMarkerTraversalOnlyWhenRoomAvailable", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NoFishableWaterBelow = this.CreateStatusItem("NoFishableWaterBelow", "BUILDING", "status_item_no_fishable_water_below", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoPowerConsumers = this.CreateStatusItem("NoPowerConsumers", "BUILDING", "status_item_no_power_consumers", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022);
			this.NoWireConnected = this.CreateStatusItem("NoWireConnected", "BUILDING", "status_item_no_wire_connected", StatusItem.IconType.Custom, NotificationType.BadMinor, true, OverlayModes.Power.ID, true, 129022);
			this.NoLogicWireConnected = this.CreateStatusItem("NoLogicWireConnected", "BUILDING", "status_item_no_logic_wire_connected", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Logic.ID, true, 129022);
			this.NoTubeConnected = this.CreateStatusItem("NoTubeConnected", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoTubeExits = this.CreateStatusItem("NoTubeExits", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.StoredCharge = this.CreateStatusItem("StoredCharge", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.StoredCharge.resolveStringCallback = delegate(string str, object data)
			{
				TravelTubeEntrance.SMInstance sminstance = (TravelTubeEntrance.SMInstance)data;
				if (sminstance != null)
				{
					str = string.Format(str, GameUtil.GetFormattedRoundedJoules(sminstance.master.AvailableJoules), GameUtil.GetFormattedRoundedJoules(sminstance.master.TotalCapacity), GameUtil.GetFormattedRoundedJoules(sminstance.master.UsageJoules));
				}
				return str;
			};
			this.PendingDeconstruction = this.CreateStatusItem("PendingDeconstruction", "BUILDING", "status_item_pending_deconstruction", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingDeconstruction.conditionalOverlayCallback = new Func<HashedString, object, bool>(BuildingStatusItems.ShowInUtilityOverlay);
			this.PendingDemolition = this.CreateStatusItem("PendingDemolition", "BUILDING", "status_item_pending_deconstruction", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingDemolition.conditionalOverlayCallback = new Func<HashedString, object, bool>(BuildingStatusItems.ShowInUtilityOverlay);
			this.PendingRepair = this.CreateStatusItem("PendingRepair", "BUILDING", "status_item_pending_repair", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.PendingRepair.resolveStringCallback = (string str, object data) => str.Replace("{DamageInfo}", ((Repairable.SMInstance)data).master.GetComponent<BuildingHP>().GetDamageSourceInfo().ToString());
			this.PendingRepair.conditionalOverlayCallback = (HashedString mode, object data) => true;
			this.RequiresSkillPerk = this.CreateStatusItem("RequiresSkillPerk", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RequiresSkillPerk.resolveStringCallback = delegate(string str, object data)
			{
				string text10 = (string)data;
				SkillPerk skillPerk = Db.Get().SkillPerks.Get(text10);
				List<Skill> skillsWithPerk = Db.Get().Skills.GetSkillsWithPerk(skillPerk);
				List<string> list = new List<string>();
				foreach (Skill skill in skillsWithPerk)
				{
					if (!skill.deprecated)
					{
						list.Add(skill.Name);
					}
				}
				str = str.Replace("{Skills}", string.Join(", ", list.ToArray()));
				return str;
			};
			this.DigRequiresSkillPerk = this.CreateStatusItem("DigRequiresSkillPerk", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.DigRequiresSkillPerk.resolveStringCallback = this.RequiresSkillPerk.resolveStringCallback;
			this.ColonyLacksRequiredSkillPerk = this.CreateStatusItem("ColonyLacksRequiredSkillPerk", "BUILDING", "status_item_role_required", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ColonyLacksRequiredSkillPerk.resolveStringCallback = delegate(string str, object data)
			{
				string text11 = (string)data;
				SkillPerk skillPerk2 = Db.Get().SkillPerks.Get(text11);
				List<Skill> skillsWithPerk2 = Db.Get().Skills.GetSkillsWithPerk(skillPerk2);
				List<string> list2 = new List<string>();
				foreach (Skill skill2 in skillsWithPerk2)
				{
					if (!skill2.deprecated)
					{
						list2.Add(skill2.Name);
					}
				}
				str = str.Replace("{Skills}", string.Join(", ", list2.ToArray()));
				return str;
			};
			this.ColonyLacksRequiredSkillPerk.resolveTooltipCallback = delegate(string str, object data)
			{
				string text12 = (string)data;
				SkillPerk skillPerk3 = Db.Get().SkillPerks.Get(text12);
				List<Skill> skillsWithPerk3 = Db.Get().Skills.GetSkillsWithPerk(skillPerk3);
				List<string> list3 = new List<string>();
				foreach (Skill skill3 in skillsWithPerk3)
				{
					if (!skill3.deprecated)
					{
						list3.Add(skill3.Name);
					}
				}
				str = str.Replace("{Skills}", string.Join(", ", list3.ToArray()));
				return str;
			};
			this.ClusterColonyLacksRequiredSkillPerk = this.CreateStatusItem("ClusterColonyLacksRequiredSkillPerk", "BUILDING", "status_item_role_required", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ClusterColonyLacksRequiredSkillPerk.resolveStringCallback = this.ColonyLacksRequiredSkillPerk.resolveStringCallback;
			this.ClusterColonyLacksRequiredSkillPerk.resolveTooltipCallback = this.ColonyLacksRequiredSkillPerk.resolveTooltipCallback;
			this.WorkRequiresMinion = this.CreateStatusItem("WorkRequiresMinion", "BUILDING", "status_item_role_required", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.SwitchStatusActive = this.CreateStatusItem("SwitchStatusActive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SwitchStatusInactive = this.CreateStatusItem("SwitchStatusInactive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.LogicSwitchStatusActive = this.CreateStatusItem("LogicSwitchStatusActive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.LogicSwitchStatusInactive = this.CreateStatusItem("LogicSwitchStatusInactive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.LogicSensorStatusActive = this.CreateStatusItem("LogicSensorStatusActive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.LogicSensorStatusInactive = this.CreateStatusItem("LogicSensorStatusInactive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingFish = this.CreateStatusItem("PendingFish", "BUILDING", "status_item_pending_fish", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingSwitchToggle = this.CreateStatusItem("PendingSwitchToggle", "BUILDING", "status_item_pending_switch_toggle", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingUpgrade = this.CreateStatusItem("PendingUpgrade", "BUILDING", "status_item_pending_upgrade", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PendingWork = this.CreateStatusItem("PendingWork", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.PowerButtonOff = this.CreateStatusItem("PowerButtonOff", "BUILDING", "status_item_power_button_off", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.PressureOk = this.CreateStatusItem("PressureOk", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.Oxygen.ID, true, 129022);
			this.UnderPressure = this.CreateStatusItem("UnderPressure", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.Oxygen.ID, true, 129022);
			this.UnderPressure.resolveTooltipCallback = delegate(string str, object data)
			{
				float num3 = (float)data;
				return str.Replace("{TargetPressure}", GameUtil.GetFormattedMass(num3, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.Unassigned = this.CreateStatusItem("Unassigned", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.Rooms.ID, true, 129022);
			this.AssignedPublic = this.CreateStatusItem("AssignedPublic", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Rooms.ID, true, 129022);
			this.UnderConstruction = this.CreateStatusItem("UnderConstruction", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.UnderConstructionNoWorker = this.CreateStatusItem("UnderConstructionNoWorker", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Normal = this.CreateStatusItem("Normal", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ManualGeneratorChargingUp = this.CreateStatusItem("ManualGeneratorChargingUp", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.ManualGeneratorReleasingEnergy = this.CreateStatusItem("ManualGeneratorReleasingEnergy", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.GeneratorOffline = this.CreateStatusItem("GeneratorOffline", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022);
			this.Pipe = this.CreateStatusItem("Pipe", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.Pipe.resolveStringCallback = delegate(string str, object data)
			{
				Conduit conduit = (Conduit)data;
				int num4 = Grid.PosToCell(conduit);
				ConduitFlow.ConduitContents contents = conduit.GetFlowManager().GetContents(num4);
				string text13 = BUILDING.STATUSITEMS.PIPECONTENTS.EMPTY;
				if (contents.mass > 0f)
				{
					Element element = ElementLoader.FindElementByHash(contents.element);
					text13 = string.Format(BUILDING.STATUSITEMS.PIPECONTENTS.CONTENTS, GameUtil.GetFormattedMass(contents.mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), element.name, GameUtil.GetFormattedTemperature(contents.temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
					if (OverlayScreen.Instance != null && OverlayScreen.Instance.mode == OverlayModes.Disease.ID && contents.diseaseIdx != 255)
					{
						text13 += string.Format(BUILDING.STATUSITEMS.PIPECONTENTS.CONTENTS_WITH_DISEASE, GameUtil.GetFormattedDisease(contents.diseaseIdx, contents.diseaseCount, true));
					}
				}
				str = str.Replace("{Contents}", text13);
				return str;
			};
			this.Conveyor = this.CreateStatusItem("Conveyor", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.SolidConveyor.ID, true, 129022);
			this.Conveyor.resolveStringCallback = delegate(string str, object data)
			{
				int num5 = Grid.PosToCell((SolidConduit)data);
				SolidConduitFlow solidConduitFlow = Game.Instance.solidConduitFlow;
				SolidConduitFlow.ConduitContents contents2 = solidConduitFlow.GetContents(num5);
				string text14 = BUILDING.STATUSITEMS.CONVEYOR_CONTENTS.EMPTY;
				if (contents2.pickupableHandle.IsValid())
				{
					Pickupable pickupable = solidConduitFlow.GetPickupable(contents2.pickupableHandle);
					if (pickupable)
					{
						PrimaryElement component = pickupable.GetComponent<PrimaryElement>();
						float mass = component.Mass;
						if (mass > 0f)
						{
							text14 = string.Format(BUILDING.STATUSITEMS.CONVEYOR_CONTENTS.CONTENTS, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.GetProperName(), GameUtil.GetFormattedTemperature(component.Temperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
							if (OverlayScreen.Instance != null && OverlayScreen.Instance.mode == OverlayModes.Disease.ID && component.DiseaseIdx != 255)
							{
								text14 += string.Format(BUILDING.STATUSITEMS.CONVEYOR_CONTENTS.CONTENTS_WITH_DISEASE, GameUtil.GetFormattedDisease(component.DiseaseIdx, component.DiseaseCount, true));
							}
						}
					}
				}
				str = str.Replace("{Contents}", text14);
				return str;
			};
			this.FabricatorIdle = this.CreateStatusItem("FabricatorIdle", "BUILDING", "status_item_fabricator_select", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FabricatorEmpty = this.CreateStatusItem("FabricatorEmpty", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.FabricatorLacksHEP = this.CreateStatusItem("FabricatorLacksHEP", "BUILDING", "status_item_need_high_energy_particles", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.FabricatorLacksHEP.resolveStringCallback = delegate(string str, object data)
			{
				ComplexFabricator complexFabricator = (ComplexFabricator)data;
				if (complexFabricator != null)
				{
					int num6 = complexFabricator.HighestHEPQueued();
					HighEnergyParticleStorage component2 = complexFabricator.GetComponent<HighEnergyParticleStorage>();
					str = str.Replace("{HEPRequired}", num6.ToString());
					str = str.Replace("{CurrentHEP}", component2.Particles.ToString());
				}
				return str;
			};
			this.Toilet = this.CreateStatusItem("Toilet", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Toilet.resolveStringCallback = delegate(string str, object data)
			{
				Toilet.StatesInstance statesInstance2 = (Toilet.StatesInstance)data;
				if (statesInstance2 != null)
				{
					str = str.Replace("{FlushesRemaining}", statesInstance2.GetFlushesRemaining().ToString());
				}
				return str;
			};
			this.ToiletNeedsEmptying = this.CreateStatusItem("ToiletNeedsEmptying", "BUILDING", "status_item_toilet_needs_emptying", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.DesalinatorNeedsEmptying = this.CreateStatusItem("DesalinatorNeedsEmptying", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.Unusable = this.CreateStatusItem("Unusable", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoResearchSelected = this.CreateStatusItem("NoResearchSelected", "BUILDING", "status_item_no_research_selected", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoResearchSelected.AddNotification(null, null, null);
			StatusItem noResearchSelected = this.NoResearchSelected;
			noResearchSelected.resolveTooltipCallback = (Func<string, object, string>)Delegate.Combine(noResearchSelected.resolveTooltipCallback, new Func<string, object, string>(delegate(string str, object data)
			{
				string text15 = GameInputMapping.FindEntry(global::Action.ManageResearch).mKeyCode.ToString();
				str = str.Replace("{RESEARCH_MENU_KEY}", text15);
				return str;
			}));
			this.NoResearchSelected.notificationClickCallback = delegate(object d)
			{
				ManagementMenu.Instance.OpenResearch();
			};
			this.NoApplicableResearchSelected = this.CreateStatusItem("NoApplicableResearchSelected", "BUILDING", "status_item_no_research_selected", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoApplicableResearchSelected.AddNotification(null, null, null);
			this.NoApplicableAnalysisSelected = this.CreateStatusItem("NoApplicableAnalysisSelected", "BUILDING", "status_item_no_research_selected", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoApplicableAnalysisSelected.AddNotification(null, null, null);
			StatusItem noApplicableAnalysisSelected = this.NoApplicableAnalysisSelected;
			noApplicableAnalysisSelected.resolveTooltipCallback = (Func<string, object, string>)Delegate.Combine(noApplicableAnalysisSelected.resolveTooltipCallback, new Func<string, object, string>(delegate(string str, object data)
			{
				string text16 = GameInputMapping.FindEntry(global::Action.ManageStarmap).mKeyCode.ToString();
				str = str.Replace("{STARMAP_MENU_KEY}", text16);
				return str;
			}));
			this.NoApplicableAnalysisSelected.notificationClickCallback = delegate(object d)
			{
				ManagementMenu.Instance.OpenStarmap();
			};
			this.NoResearchOrDestinationSelected = this.CreateStatusItem("NoResearchOrDestinationSelected", "BUILDING", "status_item_no_research_selected", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			StatusItem noResearchOrDestinationSelected = this.NoResearchOrDestinationSelected;
			noResearchOrDestinationSelected.resolveTooltipCallback = (Func<string, object, string>)Delegate.Combine(noResearchOrDestinationSelected.resolveTooltipCallback, new Func<string, object, string>(delegate(string str, object data)
			{
				string text17 = GameInputMapping.FindEntry(global::Action.ManageStarmap).mKeyCode.ToString();
				str = str.Replace("{STARMAP_MENU_KEY}", text17);
				string text18 = GameInputMapping.FindEntry(global::Action.ManageResearch).mKeyCode.ToString();
				str = str.Replace("{RESEARCH_MENU_KEY}", text18);
				return str;
			}));
			this.NoResearchOrDestinationSelected.AddNotification(null, null, null);
			this.ValveRequest = this.CreateStatusItem("ValveRequest", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.ValveRequest.resolveStringCallback = delegate(string str, object data)
			{
				Valve valve = (Valve)data;
				str = str.Replace("{QueuedMaxFlow}", GameUtil.GetFormattedMass(valve.QueuedMaxFlow, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.EmittingLight = this.CreateStatusItem("EmittingLight", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.EmittingLight.resolveStringCallback = delegate(string str, object data)
			{
				string text19 = GameInputMapping.FindEntry(global::Action.Overlay5).mKeyCode.ToString();
				str = str.Replace("{LightGridOverlay}", text19);
				return str;
			};
			this.RationBoxContents = this.CreateStatusItem("RationBoxContents", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RationBoxContents.resolveStringCallback = delegate(string str, object data)
			{
				RationBox rationBox = (RationBox)data;
				if (rationBox == null)
				{
					return str;
				}
				Storage component3 = rationBox.GetComponent<Storage>();
				if (component3 == null)
				{
					return str;
				}
				float num7 = 0f;
				foreach (GameObject gameObject in component3.items)
				{
					Edible component4 = gameObject.GetComponent<Edible>();
					if (component4)
					{
						num7 += component4.Calories;
					}
				}
				str = str.Replace("{Stored}", GameUtil.GetFormattedCalories(num7, GameUtil.TimeSlice.None, true));
				return str;
			};
			this.EmittingElement = this.CreateStatusItem("EmittingElement", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.EmittingElement.resolveStringCallback = delegate(string str, object data)
			{
				IElementEmitter elementEmitter = (IElementEmitter)data;
				string text20 = ElementLoader.FindElementByHash(elementEmitter.Element).tag.ProperName();
				str = str.Replace("{ElementType}", text20);
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(elementEmitter.AverageEmitRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.EmittingOxygenAvg = this.CreateStatusItem("EmittingOxygenAvg", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.EmittingOxygenAvg.resolveStringCallback = delegate(string str, object data)
			{
				Sublimates sublimates = (Sublimates)data;
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(sublimates.AvgFlowRate(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.EmittingGasAvg = this.CreateStatusItem("EmittingGasAvg", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.EmittingGasAvg.resolveStringCallback = delegate(string str, object data)
			{
				Sublimates sublimates2 = (Sublimates)data;
				str = str.Replace("{Element}", ElementLoader.FindElementByHash(sublimates2.info.sublimatedElement).name);
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(sublimates2.AvgFlowRate(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.EmittingBlockedHighPressure = this.CreateStatusItem("EmittingBlockedHighPressure", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.EmittingBlockedHighPressure.resolveStringCallback = delegate(string str, object data)
			{
				Sublimates sublimates3 = (Sublimates)data;
				str = str.Replace("{Element}", ElementLoader.FindElementByHash(sublimates3.info.sublimatedElement).name);
				return str;
			};
			this.EmittingBlockedLowTemperature = this.CreateStatusItem("EmittingBlockedLowTemperature", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.EmittingBlockedLowTemperature.resolveStringCallback = delegate(string str, object data)
			{
				Sublimates sublimates4 = (Sublimates)data;
				str = str.Replace("{Element}", ElementLoader.FindElementByHash(sublimates4.info.sublimatedElement).name);
				return str;
			};
			this.PumpingLiquidOrGas = this.CreateStatusItem("PumpingLiquidOrGas", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.PumpingLiquidOrGas.resolveStringCallback = delegate(string str, object data)
			{
				HandleVector<int>.Handle handle = (HandleVector<int>.Handle)data;
				float averageRate = Game.Instance.accumulators.GetAverageRate(handle);
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(averageRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.PipeMayMelt = this.CreateStatusItem("PipeMayMelt", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoLiquidElementToPump = this.CreateStatusItem("NoLiquidElementToPump", "BUILDING", "status_item_no_liquid_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.LiquidConduits.ID, true, 129022);
			this.NoGasElementToPump = this.CreateStatusItem("NoGasElementToPump", "BUILDING", "status_item_no_gas_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.GasConduits.ID, true, 129022);
			this.NoFilterElementSelected = this.CreateStatusItem("NoFilterElementSelected", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NoLureElementSelected = this.CreateStatusItem("NoLureElementSelected", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ElementConsumer = this.CreateStatusItem("ElementConsumer", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 129022);
			this.ElementConsumer.resolveStringCallback = delegate(string str, object data)
			{
				ElementConsumer elementConsumer = (ElementConsumer)data;
				string text21 = ElementLoader.FindElementByHash(elementConsumer.elementToConsume).tag.ProperName();
				str = str.Replace("{ElementTypes}", text21);
				str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(elementConsumer.AverageConsumeRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				return str;
			};
			this.ElementEmitterOutput = this.CreateStatusItem("ElementEmitterOutput", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 129022);
			this.ElementEmitterOutput.resolveStringCallback = delegate(string str, object data)
			{
				ElementEmitter elementEmitter2 = (ElementEmitter)data;
				if (elementEmitter2 != null)
				{
					str = str.Replace("{ElementTypes}", elementEmitter2.outputElement.Name);
					str = str.Replace("{FlowRate}", GameUtil.GetFormattedMass(elementEmitter2.outputElement.massGenerationRate / elementEmitter2.emissionFrequency, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				}
				return str;
			};
			this.AwaitingWaste = this.CreateStatusItem("AwaitingWaste", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 129022);
			this.AwaitingCompostFlip = this.CreateStatusItem("AwaitingCompostFlip", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, true, OverlayModes.None.ID, true, 129022);
			this.JoulesAvailable = this.CreateStatusItem("JoulesAvailable", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.JoulesAvailable.resolveStringCallback = delegate(string str, object data)
			{
				Battery battery = (Battery)data;
				str = str.Replace("{JoulesAvailable}", GameUtil.GetFormattedJoules(battery.JoulesAvailable, "F1", GameUtil.TimeSlice.None));
				str = str.Replace("{JoulesCapacity}", GameUtil.GetFormattedJoules(battery.Capacity, "F1", GameUtil.TimeSlice.None));
				return str;
			};
			this.Wattage = this.CreateStatusItem("Wattage", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.Wattage.resolveStringCallback = delegate(string str, object data)
			{
				Generator generator = (Generator)data;
				str = str.Replace("{Wattage}", GameUtil.GetFormattedWattage(generator.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.SolarPanelWattage = this.CreateStatusItem("SolarPanelWattage", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.SolarPanelWattage.resolveStringCallback = delegate(string str, object data)
			{
				SolarPanel solarPanel = (SolarPanel)data;
				str = str.Replace("{Wattage}", GameUtil.GetFormattedWattage(solarPanel.CurrentWattage, GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.ModuleSolarPanelWattage = this.CreateStatusItem("ModuleSolarPanelWattage", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.ModuleSolarPanelWattage.resolveStringCallback = delegate(string str, object data)
			{
				ModuleSolarPanel moduleSolarPanel = (ModuleSolarPanel)data;
				str = str.Replace("{Wattage}", GameUtil.GetFormattedWattage(moduleSolarPanel.CurrentWattage, GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.SteamTurbineWattage = this.CreateStatusItem("SteamTurbineWattage", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.SteamTurbineWattage.resolveStringCallback = delegate(string str, object data)
			{
				SteamTurbine steamTurbine = (SteamTurbine)data;
				str = str.Replace("{Wattage}", GameUtil.GetFormattedWattage(steamTurbine.CurrentWattage, GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.Wattson = this.CreateStatusItem("Wattson", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Wattson.resolveStringCallback = delegate(string str, object data)
			{
				Telepad telepad = (Telepad)data;
				if (GameFlowManager.Instance != null && GameFlowManager.Instance.IsGameOver())
				{
					str = BUILDING.STATUSITEMS.WATTSONGAMEOVER.NAME;
				}
				else if (telepad.GetComponent<Operational>().IsOperational)
				{
					str = str.Replace("{TimeRemaining}", GameUtil.GetFormattedCycles(telepad.GetTimeRemaining(), "F1", false));
				}
				else
				{
					str = str.Replace("{TimeRemaining}", BUILDING.STATUSITEMS.WATTSON.UNAVAILABLE);
				}
				return str;
			};
			this.FlushToilet = this.CreateStatusItem("FlushToilet", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FlushToilet.resolveStringCallback = delegate(string str, object data)
			{
				FlushToilet.SMInstance sminstance2 = (FlushToilet.SMInstance)data;
				return BUILDING.STATUSITEMS.FLUSHTOILET.NAME.Replace("{toilet}", sminstance2.master.GetProperName());
			};
			this.FlushToilet.resolveTooltipCallback = (string str, object Database) => BUILDING.STATUSITEMS.FLUSHTOILET.TOOLTIP;
			this.FlushToiletInUse = this.CreateStatusItem("FlushToiletInUse", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FlushToiletInUse.resolveStringCallback = delegate(string str, object data)
			{
				FlushToilet.SMInstance sminstance3 = (FlushToilet.SMInstance)data;
				return BUILDING.STATUSITEMS.FLUSHTOILETINUSE.NAME.Replace("{toilet}", sminstance3.master.GetProperName());
			};
			this.FlushToiletInUse.resolveTooltipCallback = (string str, object Database) => BUILDING.STATUSITEMS.FLUSHTOILETINUSE.TOOLTIP;
			this.WireNominal = this.CreateStatusItem("WireNominal", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.WireConnected = this.CreateStatusItem("WireConnected", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.WireDisconnected = this.CreateStatusItem("WireDisconnected", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022);
			this.Overheated = this.CreateStatusItem("Overheated", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022);
			this.Overloaded = this.CreateStatusItem("Overloaded", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022);
			this.LogicOverloaded = this.CreateStatusItem("LogicOverloaded", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID, true, 129022);
			this.Cooling = this.CreateStatusItem("Cooling", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			Func<string, object, string> func2 = delegate(string str, object data)
			{
				AirConditioner airConditioner = (AirConditioner)data;
				return string.Format(str, GameUtil.GetFormattedTemperature(airConditioner.lastGasTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.CoolingStalledColdGas = this.CreateStatusItem("CoolingStalledColdGas", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.CoolingStalledColdGas.resolveStringCallback = func2;
			this.CoolingStalledColdLiquid = this.CreateStatusItem("CoolingStalledColdLiquid", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.CoolingStalledColdLiquid.resolveStringCallback = func2;
			Func<string, object, string> func3 = delegate(string str, object data)
			{
				AirConditioner airConditioner2 = (AirConditioner)data;
				return string.Format(str, GameUtil.GetFormattedTemperature(airConditioner2.lastEnvTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(airConditioner2.lastGasTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(airConditioner2.maxEnvironmentDelta, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false));
			};
			this.CoolingStalledHotEnv = this.CreateStatusItem("CoolingStalledHotEnv", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.CoolingStalledHotEnv.resolveStringCallback = func3;
			this.CoolingStalledHotLiquid = this.CreateStatusItem("CoolingStalledHotLiquid", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.CoolingStalledHotLiquid.resolveStringCallback = func3;
			this.MissingRequirements = this.CreateStatusItem("MissingRequirements", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.GettingReady = this.CreateStatusItem("GettingReady", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Working = this.CreateStatusItem("Working", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NeedsValidRegion = this.CreateStatusItem("NeedsValidRegion", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NeedSeed = this.CreateStatusItem("NeedSeed", "BUILDING", "status_item_fabricator_empty", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.AwaitingSeedDelivery = this.CreateStatusItem("AwaitingSeedDelivery", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.AwaitingBaitDelivery = this.CreateStatusItem("AwaitingBaitDelivery", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NoAvailableSeed = this.CreateStatusItem("NoAvailableSeed", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.NeedEgg = this.CreateStatusItem("NeedEgg", "BUILDING", "status_item_fabricator_empty", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.AwaitingEggDelivery = this.CreateStatusItem("AwaitingEggDelivery", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NoAvailableEgg = this.CreateStatusItem("NoAvailableEgg", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.Grave = this.CreateStatusItem("Grave", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Grave.resolveStringCallback = delegate(string str, object data)
			{
				Grave.StatesInstance statesInstance3 = (Grave.StatesInstance)data;
				string text22 = str.Replace("{DeadDupe}", statesInstance3.master.graveName);
				string[] strings = LocString.GetStrings(typeof(NAMEGEN.GRAVE.EPITAPHS));
				int num8 = statesInstance3.master.epitaphIdx % strings.Length;
				return text22.Replace("{Epitaph}", strings[num8]);
			};
			this.GraveEmpty = this.CreateStatusItem("GraveEmpty", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CannotCoolFurther = this.CreateStatusItem("CannotCoolFurther", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CannotCoolFurther.resolveTooltipCallback = delegate(string str, object data)
			{
				float num9 = (float)data;
				return str.Replace("{0}", GameUtil.GetFormattedTemperature(num9, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.BuildingDisabled = this.CreateStatusItem("BuildingDisabled", "BUILDING", "status_item_building_disabled", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.Expired = this.CreateStatusItem("Expired", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PumpingStation = this.CreateStatusItem("PumpingStation", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PumpingStation.resolveStringCallback = delegate(string str, object data)
			{
				LiquidPumpingStation liquidPumpingStation = (LiquidPumpingStation)data;
				if (liquidPumpingStation != null)
				{
					return liquidPumpingStation.ResolveString(str);
				}
				return str;
			};
			this.EmptyPumpingStation = this.CreateStatusItem("EmptyPumpingStation", "BUILDING", "status_item_no_liquid_to_pump", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.WellPressurizing = this.CreateStatusItem("WellPressurizing", BUILDING.STATUSITEMS.WELL_PRESSURIZING.NAME, BUILDING.STATUSITEMS.WELL_PRESSURIZING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.WellPressurizing.resolveStringCallback = delegate(string str, object data)
			{
				OilWellCap.StatesInstance statesInstance4 = (OilWellCap.StatesInstance)data;
				if (statesInstance4 != null)
				{
					return string.Format(str, GameUtil.GetFormattedPercent(100f * statesInstance4.GetPressurePercent(), GameUtil.TimeSlice.None));
				}
				return str;
			};
			this.WellOverpressure = this.CreateStatusItem("WellOverpressure", BUILDING.STATUSITEMS.WELL_OVERPRESSURE.NAME, BUILDING.STATUSITEMS.WELL_OVERPRESSURE.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022);
			this.ReleasingPressure = this.CreateStatusItem("ReleasingPressure", BUILDING.STATUSITEMS.RELEASING_PRESSURE.NAME, BUILDING.STATUSITEMS.RELEASING_PRESSURE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.ReactorMeltdown = this.CreateStatusItem("ReactorMeltdown", BUILDING.STATUSITEMS.REACTORMELTDOWN.NAME, BUILDING.STATUSITEMS.REACTORMELTDOWN.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Bad, false, OverlayModes.None.ID, 129022);
			this.TooCold = this.CreateStatusItem("TooCold", BUILDING.STATUSITEMS.TOO_COLD.NAME, BUILDING.STATUSITEMS.TOO_COLD.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022);
			this.IncubatorProgress = this.CreateStatusItem("IncubatorProgress", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.IncubatorProgress.resolveStringCallback = delegate(string str, object data)
			{
				EggIncubator eggIncubator = (EggIncubator)data;
				str = str.Replace("{Percent}", GameUtil.GetFormattedPercent(eggIncubator.GetProgress() * 100f, GameUtil.TimeSlice.None));
				return str;
			};
			this.HabitatNeedsEmptying = this.CreateStatusItem("HabitatNeedsEmptying", "BUILDING", "status_item_need_supply_out", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.DetectorScanning = this.CreateStatusItem("DetectorScanning", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.IncomingMeteors = this.CreateStatusItem("IncomingMeteors", "BUILDING", "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.HasGantry = this.CreateStatusItem("HasGantry", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.MissingGantry = this.CreateStatusItem("MissingGantry", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.DisembarkingDuplicant = this.CreateStatusItem("DisembarkingDuplicant", "BUILDING", "status_item_new_duplicants_available", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.RocketName = this.CreateStatusItem("RocketName", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RocketName.resolveStringCallback = delegate(string str, object data)
			{
				RocketModule rocketModule = (RocketModule)data;
				if (rocketModule != null)
				{
					return str.Replace("{0}", rocketModule.GetParentRocketName());
				}
				return str;
			};
			this.RocketName.resolveTooltipCallback = delegate(string str, object data)
			{
				RocketModule rocketModule2 = (RocketModule)data;
				if (rocketModule2 != null)
				{
					return str.Replace("{0}", rocketModule2.GetParentRocketName());
				}
				return str;
			};
			this.LandedRocketLacksPassengerModule = this.CreateStatusItem("LandedRocketLacksPassengerModule", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.PathNotClear = new StatusItem("PATH_NOT_CLEAR", "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.PathNotClear.resolveTooltipCallback = delegate(string str, object data)
			{
				ConditionFlightPathIsClear conditionFlightPathIsClear = (ConditionFlightPathIsClear)data;
				if (conditionFlightPathIsClear != null)
				{
					str = string.Format(str, conditionFlightPathIsClear.GetObstruction());
				}
				return str;
			};
			this.InvalidPortOverlap = this.CreateStatusItem("InvalidPortOverlap", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.InvalidPortOverlap.AddNotification(null, null, null);
			this.EmergencyPriority = this.CreateStatusItem("EmergencyPriority", BUILDING.STATUSITEMS.TOP_PRIORITY_CHORE.NAME, BUILDING.STATUSITEMS.TOP_PRIORITY_CHORE.TOOLTIP, "status_item_doubleexclamation", StatusItem.IconType.Custom, NotificationType.Bad, false, OverlayModes.None.ID, 129022);
			this.EmergencyPriority.AddNotification(null, BUILDING.STATUSITEMS.TOP_PRIORITY_CHORE.NOTIFICATION_NAME, BUILDING.STATUSITEMS.TOP_PRIORITY_CHORE.NOTIFICATION_TOOLTIP);
			this.SkillPointsAvailable = this.CreateStatusItem("SkillPointsAvailable", BUILDING.STATUSITEMS.SKILL_POINTS_AVAILABLE.NAME, BUILDING.STATUSITEMS.SKILL_POINTS_AVAILABLE.TOOLTIP, "status_item_jobs", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.Baited = this.CreateStatusItem("Baited", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022);
			this.Baited.resolveStringCallback = delegate(string str, object data)
			{
				Element element2 = ElementLoader.FindElementByName(((CreatureBait.StatesInstance)data).master.baitElement.ToString());
				str = str.Replace("{0}", element2.name);
				return str;
			};
			this.Baited.resolveTooltipCallback = delegate(string str, object data)
			{
				Element element3 = ElementLoader.FindElementByName(((CreatureBait.StatesInstance)data).master.baitElement.ToString());
				str = str.Replace("{0}", element3.name);
				return str;
			};
			this.TanningLightSufficient = this.CreateStatusItem("TanningLightSufficient", BUILDING.STATUSITEMS.TANNINGLIGHTSUFFICIENT.NAME, BUILDING.STATUSITEMS.TANNINGLIGHTSUFFICIENT.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.TanningLightInsufficient = this.CreateStatusItem("TanningLightInsufficient", BUILDING.STATUSITEMS.TANNINGLIGHTINSUFFICIENT.NAME, BUILDING.STATUSITEMS.TANNINGLIGHTINSUFFICIENT.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.HotTubWaterTooCold = this.CreateStatusItem("HotTubWaterTooCold", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022);
			this.HotTubWaterTooCold.resolveStringCallback = delegate(string str, object data)
			{
				HotTub hotTub = (HotTub)data;
				str = str.Replace("{temperature}", GameUtil.GetFormattedTemperature(hotTub.minimumWaterTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.HotTubTooHot = this.CreateStatusItem("HotTubTooHot", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022);
			this.HotTubTooHot.resolveStringCallback = delegate(string str, object data)
			{
				HotTub hotTub2 = (HotTub)data;
				str = str.Replace("{temperature}", GameUtil.GetFormattedTemperature(hotTub2.maxOperatingTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
				return str;
			};
			this.HotTubFilling = this.CreateStatusItem("HotTubFilling", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022);
			this.HotTubFilling.resolveStringCallback = delegate(string str, object data)
			{
				HotTub hotTub3 = (HotTub)data;
				str = str.Replace("{fullness}", GameUtil.GetFormattedPercent(hotTub3.PercentFull, GameUtil.TimeSlice.None));
				return str;
			};
			this.WindTunnelIntake = this.CreateStatusItem("WindTunnelIntake", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.WarpPortalCharging = this.CreateStatusItem("WarpPortalCharging", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022);
			this.WarpPortalCharging.resolveStringCallback = delegate(string str, object data)
			{
				WarpPortal warpPortal = (WarpPortal)data;
				str = str.Replace("{charge}", GameUtil.GetFormattedPercent(100f * (((WarpPortal)data).rechargeProgress / 3000f), GameUtil.TimeSlice.None));
				return str;
			};
			this.WarpPortalCharging.resolveTooltipCallback = delegate(string str, object data)
			{
				WarpPortal warpPortal2 = (WarpPortal)data;
				str = str.Replace("{cycles}", string.Format("{0:0.0}", (3000f - ((WarpPortal)data).rechargeProgress) / 600f));
				return str;
			};
			this.WarpConduitPartnerDisabled = this.CreateStatusItem("WarpConduitPartnerDisabled", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.WarpConduitPartnerDisabled.resolveStringCallback = (string str, object data) => str.Replace("{x}", data.ToString());
			this.CollectingHEP = this.CreateStatusItem("CollectingHEP", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.Radiation.ID, false, 129022);
			this.CollectingHEP.resolveStringCallback = (string str, object data) => str.Replace("{x}", ((HighEnergyParticleSpawner)data).PredictedPerCycleConsumptionRate.ToString());
			this.InOrbit = this.CreateStatusItem("InOrbit", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.InOrbit.resolveStringCallback = delegate(string str, object data)
			{
				ClusterGridEntity clusterGridEntity = (ClusterGridEntity)data;
				return str.Replace("{Destination}", clusterGridEntity.Name);
			};
			this.InFlight = this.CreateStatusItem("InFlight", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.InFlight.resolveStringCallback = delegate(string str, object data)
			{
				ClusterTraveler clusterTraveler = (ClusterTraveler)data;
				ClusterDestinationSelector component5 = clusterTraveler.GetComponent<ClusterDestinationSelector>();
				RocketClusterDestinationSelector rocketClusterDestinationSelector = component5 as RocketClusterDestinationSelector;
				Sprite sprite;
				string text23;
				string text24;
				ClusterGrid.Instance.GetLocationDescription(component5.GetDestination(), out sprite, out text23, out text24);
				if (rocketClusterDestinationSelector != null)
				{
					LaunchPad destinationPad = rocketClusterDestinationSelector.GetDestinationPad();
					string text25 = ((destinationPad != null) ? destinationPad.GetProperName() : UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE.ToString());
					return str.Replace("{Destination_Asteroid}", text23).Replace("{Destination_Pad}", text25).Replace("{ETA}", GameUtil.GetFormattedCycles(clusterTraveler.TravelETA(), "F1", false));
				}
				return str.Replace("{Destination_Asteroid}", text23).Replace("{ETA}", GameUtil.GetFormattedCycles(clusterTraveler.TravelETA(), "F1", false));
			};
			this.DestinationOutOfRange = this.CreateStatusItem("DestinationOutOfRange", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.DestinationOutOfRange.resolveStringCallback = delegate(string str, object data)
			{
				ClusterTraveler clusterTraveler2 = (ClusterTraveler)data;
				str = str.Replace("{Range}", GameUtil.GetFormattedRocketRange(clusterTraveler2.GetComponent<CraftModuleInterface>().Range, GameUtil.TimeSlice.None, false));
				return str.Replace("{Distance}", clusterTraveler2.RemainingTravelNodes().ToString() + " " + UI.CLUSTERMAP.TILES);
			};
			this.RocketStranded = this.CreateStatusItem("RocketStranded", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.MissionControlAssistingRocket = this.CreateStatusItem("MissionControlAssistingRocket", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.MissionControlAssistingRocket.resolveStringCallback = delegate(string str, object data)
			{
				Spacecraft spacecraft = data as Spacecraft;
				Clustercraft clustercraft = data as Clustercraft;
				return str.Replace("{0}", (spacecraft != null) ? spacecraft.rocketName : clustercraft.Name);
			};
			this.NoRocketsToMissionControlBoost = this.CreateStatusItem("NoRocketsToMissionControlBoost", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NoRocketsToMissionControlClusterBoost = this.CreateStatusItem("NoRocketsToMissionControlClusterBoost", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.NoRocketsToMissionControlClusterBoost.resolveStringCallback = delegate(string str, object data)
			{
				if (str.Contains("{0}"))
				{
					str = str.Replace("{0}", 2.ToString());
				}
				return str;
			};
			this.MissionControlBoosted = this.CreateStatusItem("MissionControlBoosted", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.MissionControlBoosted.resolveStringCallback = delegate(string str, object data)
			{
				Spacecraft spacecraft2 = data as Spacecraft;
				Clustercraft clustercraft2 = data as Clustercraft;
				str = str.Replace("{0}", GameUtil.GetFormattedPercent(20.000004f, GameUtil.TimeSlice.None));
				if (str.Contains("{1}"))
				{
					str = str.Replace("{1}", GameUtil.GetFormattedTime((spacecraft2 != null) ? spacecraft2.controlStationBuffTimeRemaining : clustercraft2.controlStationBuffTimeRemaining, "F0"));
				}
				return str;
			};
			this.RailgunpayloadNeedsEmptying = this.CreateStatusItem("RailgunpayloadNeedsEmptying", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.AwaitingEmptyBuilding = this.CreateStatusItem("AwaitingEmptyBuilding", "BUILDING", "action_empty_contents", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.DuplicantActivationRequired = this.CreateStatusItem("DuplicantActivationRequired", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RocketChecklistIncomplete = this.CreateStatusItem("RocketChecklistIncomplete", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.RocketCargoEmptying = this.CreateStatusItem("RocketCargoEmptying", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RocketCargoFilling = this.CreateStatusItem("RocketCargoFilling", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RocketCargoFull = this.CreateStatusItem("RocketCargoFull", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FlightAllCargoFull = this.CreateStatusItem("FlightAllCargoFull", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FlightCargoRemaining = this.CreateStatusItem("FlightCargoRemaining", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FlightCargoRemaining.resolveStringCallback = delegate(string str, object data)
			{
				float num10 = (float)data;
				return str.Replace("{0}", GameUtil.GetFormattedMass(num10, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.PilotNeeded = this.CreateStatusItem("PilotNeeded", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.PilotNeeded.resolveStringCallback = delegate(string str, object data)
			{
				RocketControlStation master = ((RocketControlStation.StatesInstance)data).master;
				return str.Replace("{timeRemaining}", GameUtil.GetFormattedTime(master.TimeRemaining, "F0"));
			};
			this.AutoPilotActive = this.CreateStatusItem("AutoPilotActive", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.InvalidMaskStationConsumptionState = this.CreateStatusItem("InvalidMaskStationConsumptionState", "BUILDING", "status_item_no_gas_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ClusterTelescopeAllWorkComplete = this.CreateStatusItem("ClusterTelescopeAllWorkComplete", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RocketPlatformCloseToCeiling = this.CreateStatusItem("RocketPlatformCloseToCeiling", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.RocketPlatformCloseToCeiling.resolveStringCallback = (string str, object data) => str.Replace("{distance}", data.ToString());
			this.ModuleGeneratorPowered = this.CreateStatusItem("ModuleGeneratorPowered", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.ModuleGeneratorPowered.resolveStringCallback = delegate(string str, object data)
			{
				Generator generator2 = (Generator)data;
				str = str.Replace("{ActiveWattage}", GameUtil.GetFormattedWattage(generator2.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true));
				str = str.Replace("{MaxWattage}", GameUtil.GetFormattedWattage(generator2.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.ModuleGeneratorNotPowered = this.CreateStatusItem("ModuleGeneratorNotPowered", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022);
			this.ModuleGeneratorNotPowered.resolveStringCallback = delegate(string str, object data)
			{
				Generator generator3 = (Generator)data;
				str = str.Replace("{ActiveWattage}", GameUtil.GetFormattedWattage(0f, GameUtil.WattageFormatterUnit.Automatic, true));
				str = str.Replace("{MaxWattage}", GameUtil.GetFormattedWattage(generator3.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.InOrbitRequired = this.CreateStatusItem("InOrbitRequired", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.ReactorRefuelDisabled = this.CreateStatusItem("ReactorRefuelDisabled", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FridgeCooling = this.CreateStatusItem("FridgeCooling", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FridgeCooling.resolveStringCallback = delegate(string str, object data)
			{
				RefrigeratorController.StatesInstance statesInstance5 = (RefrigeratorController.StatesInstance)data;
				str = str.Replace("{UsedPower}", GameUtil.GetFormattedWattage(statesInstance5.GetNormalPower(), GameUtil.WattageFormatterUnit.Automatic, true)).Replace("{MaxPower}", GameUtil.GetFormattedWattage(statesInstance5.GetNormalPower(), GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.FridgeSteady = this.CreateStatusItem("FridgeSteady", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.FridgeSteady.resolveStringCallback = delegate(string str, object data)
			{
				RefrigeratorController.StatesInstance statesInstance6 = (RefrigeratorController.StatesInstance)data;
				str = str.Replace("{UsedPower}", GameUtil.GetFormattedWattage(statesInstance6.GetSaverPower(), GameUtil.WattageFormatterUnit.Automatic, true)).Replace("{MaxPower}", GameUtil.GetFormattedWattage(statesInstance6.GetNormalPower(), GameUtil.WattageFormatterUnit.Automatic, true));
				return str;
			};
			this.RailGunCooldown = this.CreateStatusItem("RailGunCooldown", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.RailGunCooldown.resolveStringCallback = delegate(string str, object data)
			{
				RailGun.StatesInstance statesInstance7 = (RailGun.StatesInstance)data;
				str = str.Replace("{timeleft}", GameUtil.GetFormattedTime(statesInstance7.sm.cooldownTimer.Get(statesInstance7), "F0"));
				return str;
			};
			this.RailGunCooldown.resolveTooltipCallback = delegate(string str, object data)
			{
				RailGun.StatesInstance statesInstance8 = (RailGun.StatesInstance)data;
				str = str.Replace("{x}", 6.ToString());
				return str;
			};
			this.NoSurfaceSight = new StatusItem("NOSURFACESIGHT", "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.LimitValveLimitReached = this.CreateStatusItem("LimitValveLimitReached", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.LimitValveLimitNotReached = this.CreateStatusItem("LimitValveLimitNotReached", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.LimitValveLimitNotReached.resolveStringCallback = delegate(string str, object data)
			{
				LimitValve limitValve = (LimitValve)data;
				string text26;
				if (limitValve.displayUnitsInsteadOfMass)
				{
					text26 = GameUtil.GetFormattedUnits(limitValve.RemainingCapacity, GameUtil.TimeSlice.None, true, LimitValveSideScreen.FLOAT_FORMAT);
				}
				else
				{
					text26 = GameUtil.GetFormattedMass(limitValve.RemainingCapacity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, LimitValveSideScreen.FLOAT_FORMAT);
				}
				return string.Format(BUILDING.STATUSITEMS.LIMITVALVELIMITNOTREACHED.NAME, text26);
			};
			this.LimitValveLimitNotReached.resolveTooltipCallback = (string str, object data) => BUILDING.STATUSITEMS.LIMITVALVELIMITNOTREACHED.TOOLTIP;
			this.SpacePOIHarvesting = this.CreateStatusItem("SpacePOIHarvesting", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.SpacePOIHarvesting.resolveStringCallback = delegate(string str, object data)
			{
				float num11 = (float)data;
				return string.Format(BUILDING.STATUSITEMS.SPACEPOIHARVESTING.NAME, GameUtil.GetFormattedMass(num11, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.SpacePOIWasting = this.CreateStatusItem("SpacePOIWasting", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.SpacePOIWasting.resolveStringCallback = delegate(string str, object data)
			{
				float num12 = (float)data;
				return string.Format(BUILDING.STATUSITEMS.SPACEPOIWASTING.NAME, GameUtil.GetFormattedMass(num12, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			};
			this.RocketRestrictionActive = new StatusItem("ROCKETRESTRICTIONACTIVE", "BUILDING", "status_item_rocket_restricted", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.RocketRestrictionInactive = new StatusItem("ROCKETRESTRICTIONINACTIVE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.NoRocketRestriction = new StatusItem("NOROCKETRESTRICTION", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.BroadcasterOutOfRange = new StatusItem("BROADCASTEROUTOFRANGE", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.LosingRadbolts = new StatusItem("LOSINGRADBOLTS", "BUILDING", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.FabricatorAcceptsMutantSeeds = new StatusItem("FABRICATORACCEPTSMUTANTSEEDS", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.NoSpiceSelected = new StatusItem("SPICEGRINDERNOSPICE", "BUILDING", "status_item_no_filter_set", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.GeoTunerNoGeyserSelected = new StatusItem("GEOTUNER_NEEDGEYSER", "BUILDING", "status_item_fabricator_select", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
			this.GeoTunerResearchNeeded = new StatusItem("GEOTUNER_CHARGE_REQUIRED", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.GeoTunerResearchInProgress = new StatusItem("GEOTUNER_CHARGING", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.GeoTunerBroadcasting = new StatusItem("GEOTUNER_CHARGED", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.GeoTunerBroadcasting.resolveStringCallback = delegate(string str, object data)
			{
				GeoTuner.Instance instance = (GeoTuner.Instance)data;
				str = str.Replace("{0}", ((float)Mathf.CeilToInt(instance.sm.expirationTimer.Get(instance) / instance.enhancementDuration * 100f)).ToString() + "%");
				return str;
			};
			this.GeoTunerBroadcasting.resolveTooltipCallback = delegate(string str, object data)
			{
				GeoTuner.Instance instance2 = (GeoTuner.Instance)data;
				float num13 = instance2.sm.expirationTimer.Get(instance2);
				float num14 = 100f / instance2.enhancementDuration;
				str = str.Replace("{0}", GameUtil.GetFormattedTime(num13, "F0"));
				str = str.Replace("{1}", "-" + num14.ToString("0.00") + "%");
				return str;
			};
			this.GeoTunerGeyserStatus = new StatusItem("GEOTUNER_GEYSER_STATUS", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.GeoTunerGeyserStatus.resolveStringCallback = delegate(string str, object data)
			{
				Geyser assignedGeyser = ((GeoTuner.Instance)data).GetAssignedGeyser();
				bool flag5 = assignedGeyser != null && assignedGeyser.smi.GetCurrentState() != null && assignedGeyser.smi.GetCurrentState().parent == assignedGeyser.smi.sm.erupt;
				bool flag6 = assignedGeyser != null && assignedGeyser.smi.GetCurrentState() == assignedGeyser.smi.sm.dormant;
				if (!flag6)
				{
					bool flag7 = !flag5;
				}
				return flag5 ? BUILDING.STATUSITEMS.GEOTUNER_GEYSER_STATUS.NAME_ERUPTING : (flag6 ? BUILDING.STATUSITEMS.GEOTUNER_GEYSER_STATUS.NAME_DORMANT : BUILDING.STATUSITEMS.GEOTUNER_GEYSER_STATUS.NAME_IDLE);
			};
			this.GeoTunerGeyserStatus.resolveTooltipCallback = delegate(string str, object data)
			{
				Geyser assignedGeyser2 = ((GeoTuner.Instance)data).GetAssignedGeyser();
				if (assignedGeyser2 != null)
				{
					assignedGeyser2.gameObject.GetProperName();
				}
				bool flag8 = assignedGeyser2 != null && assignedGeyser2.smi.GetCurrentState() != null && assignedGeyser2.smi.GetCurrentState().parent == assignedGeyser2.smi.sm.erupt;
				bool flag9 = assignedGeyser2 != null && assignedGeyser2.smi.GetCurrentState() == assignedGeyser2.smi.sm.dormant;
				if (!flag9)
				{
					bool flag10 = !flag8;
				}
				return flag8 ? BUILDING.STATUSITEMS.GEOTUNER_GEYSER_STATUS.TOOLTIP_ERUPTING : (flag9 ? BUILDING.STATUSITEMS.GEOTUNER_GEYSER_STATUS.TOOLTIP_DORMANT : BUILDING.STATUSITEMS.GEOTUNER_GEYSER_STATUS.TOOLTIP_IDLE);
			};
			this.GeyserGeotuned = new StatusItem("GEYSER_GEOTUNED", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.GeyserGeotuned.resolveStringCallback = delegate(string str, object data)
			{
				Geyser geyser = (Geyser)data;
				int num15 = 0;
				int num16 = Components.GeoTuners.GetItems(geyser.GetMyWorldId()).Count((GeoTuner.Instance x) => x.GetAssignedGeyser() == geyser);
				for (int k = 0; k < geyser.modifications.Count; k++)
				{
					if (geyser.modifications[k].originID.Contains("GeoTuner"))
					{
						num15++;
					}
				}
				str = str.Replace("{0}", num15.ToString());
				str = str.Replace("{1}", num16.ToString());
				return str;
			};
			this.GeyserGeotuned.resolveTooltipCallback = delegate(string str, object data)
			{
				Geyser geyser = (Geyser)data;
				int num17 = 0;
				int num18 = Components.GeoTuners.GetItems(geyser.GetMyWorldId()).Count((GeoTuner.Instance x) => x.GetAssignedGeyser() == geyser);
				for (int l = 0; l < geyser.modifications.Count; l++)
				{
					if (geyser.modifications[l].originID.Contains("GeoTuner"))
					{
						num17++;
					}
				}
				str = str.Replace("{0}", num17.ToString());
				str = str.Replace("{1}", num18.ToString());
				return str;
			};
			this.CreatureManipulatorWaiting = this.CreateStatusItem("CreatureManipulatorWaiting", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CreatureManipulatorProgress = this.CreateStatusItem("CreatureManipulatorProgress", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CreatureManipulatorProgress.resolveStringCallback = delegate(string str, object data)
			{
				GravitasCreatureManipulator.Instance instance3 = (GravitasCreatureManipulator.Instance)data;
				return string.Format(str, instance3.ScannedSpecies.Count, instance3.def.numSpeciesToUnlockMorphMode);
			};
			this.CreatureManipulatorProgress.resolveTooltipCallback = delegate(string str, object data)
			{
				GravitasCreatureManipulator.Instance instance4 = (GravitasCreatureManipulator.Instance)data;
				if (instance4.ScannedSpecies.Count == 0)
				{
					str = str + "\n • " + BUILDING.STATUSITEMS.CREATUREMANIPULATORPROGRESS.NO_DATA;
				}
				else
				{
					foreach (Tag tag in instance4.ScannedSpecies)
					{
						str = str + "\n • " + Strings.Get("STRINGS.CREATURES.FAMILY_PLURAL." + tag.ToString().ToUpper());
					}
				}
				return str;
			};
			this.CreatureManipulatorMorphModeLocked = this.CreateStatusItem("CreatureManipulatorMorphModeLocked", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CreatureManipulatorMorphMode = this.CreateStatusItem("CreatureManipulatorMorphMode", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.CreatureManipulatorWorking = this.CreateStatusItem("CreatureManipulatorWorking", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022);
			this.MegaBrainTankActivationProgress = this.CreateStatusItem("MegaBrainTankActivationProgress", BUILDING.STATUSITEMS.MEGABRAINTANK.PROGRESS.PROGRESSIONRATE.NAME, BUILDING.STATUSITEMS.MEGABRAINTANK.PROGRESS.PROGRESSIONRATE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.MegaBrainNotEnoughOxygen = this.CreateStatusItem("MegaBrainNotEnoughOxygen", "BUILDING", "status_item_suit_locker_no_oxygen", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022);
			this.MegaBrainTankActivationProgress.resolveStringCallback = delegate(string str, object data)
			{
				MegaBrainTank.StatesInstance statesInstance9 = (MegaBrainTank.StatesInstance)data;
				return str.Replace("{ActivationProgress}", string.Format("{0}/{1}", statesInstance9.ActivationProgress, 25));
			};
			this.MegaBrainTankDreamAnalysis = this.CreateStatusItem("MegaBrainTankDreamAnalysis", BUILDING.STATUSITEMS.MEGABRAINTANK.PROGRESS.DREAMANALYSIS.NAME, BUILDING.STATUSITEMS.MEGABRAINTANK.PROGRESS.DREAMANALYSIS.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022);
			this.MegaBrainTankDreamAnalysis.resolveStringCallback = delegate(string str, object data)
			{
				MegaBrainTank.StatesInstance statesInstance10 = (MegaBrainTank.StatesInstance)data;
				return str.Replace("{TimeToComplete}", statesInstance10.TimeTilDigested.ToString());
			};
			this.MegaBrainTankComplete = this.CreateStatusItem("MegaBrainTankComplete", BUILDING.STATUSITEMS.MEGABRAINTANK.COMPLETE.NAME, BUILDING.STATUSITEMS.MEGABRAINTANK.COMPLETE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, 129022);
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x00263968 File Offset: 0x00261B68
		private static bool ShowInUtilityOverlay(HashedString mode, object data)
		{
			Transform transform = (Transform)data;
			bool flag = false;
			if (mode == OverlayModes.GasConduits.ID)
			{
				Tag prefabTag = transform.GetComponent<KPrefabID>().PrefabTag;
				flag = OverlayScreen.GasVentIDs.Contains(prefabTag);
			}
			else if (mode == OverlayModes.LiquidConduits.ID)
			{
				Tag prefabTag2 = transform.GetComponent<KPrefabID>().PrefabTag;
				flag = OverlayScreen.LiquidVentIDs.Contains(prefabTag2);
			}
			else if (mode == OverlayModes.Power.ID)
			{
				Tag prefabTag3 = transform.GetComponent<KPrefabID>().PrefabTag;
				flag = OverlayScreen.WireIDs.Contains(prefabTag3);
			}
			else if (mode == OverlayModes.Logic.ID)
			{
				Tag prefabTag4 = transform.GetComponent<KPrefabID>().PrefabTag;
				flag = OverlayModes.Logic.HighlightItemIDs.Contains(prefabTag4);
			}
			else if (mode == OverlayModes.SolidConveyor.ID)
			{
				Tag prefabTag5 = transform.GetComponent<KPrefabID>().PrefabTag;
				flag = OverlayScreen.SolidConveyorIDs.Contains(prefabTag5);
			}
			else if (mode == OverlayModes.Radiation.ID)
			{
				Tag prefabTag6 = transform.GetComponent<KPrefabID>().PrefabTag;
				flag = OverlayScreen.RadiationIDs.Contains(prefabTag6);
			}
			return flag;
		}

		// Token: 0x04004682 RID: 18050
		public StatusItem MissingRequirements;

		// Token: 0x04004683 RID: 18051
		public StatusItem GettingReady;

		// Token: 0x04004684 RID: 18052
		public StatusItem Working;

		// Token: 0x04004685 RID: 18053
		public MaterialsStatusItem MaterialsUnavailable;

		// Token: 0x04004686 RID: 18054
		public MaterialsStatusItem MaterialsUnavailableForRefill;

		// Token: 0x04004687 RID: 18055
		public StatusItem AngerDamage;

		// Token: 0x04004688 RID: 18056
		public StatusItem ClinicOutsideHospital;

		// Token: 0x04004689 RID: 18057
		public StatusItem DigUnreachable;

		// Token: 0x0400468A RID: 18058
		public StatusItem MopUnreachable;

		// Token: 0x0400468B RID: 18059
		public StatusItem StorageUnreachable;

		// Token: 0x0400468C RID: 18060
		public StatusItem PassengerModuleUnreachable;

		// Token: 0x0400468D RID: 18061
		public StatusItem ConstructableDigUnreachable;

		// Token: 0x0400468E RID: 18062
		public StatusItem ConstructionUnreachable;

		// Token: 0x0400468F RID: 18063
		public StatusItem DispenseRequested;

		// Token: 0x04004690 RID: 18064
		public StatusItem NewDuplicantsAvailable;

		// Token: 0x04004691 RID: 18065
		public StatusItem NeedPlant;

		// Token: 0x04004692 RID: 18066
		public StatusItem NeedPower;

		// Token: 0x04004693 RID: 18067
		public StatusItem NotEnoughPower;

		// Token: 0x04004694 RID: 18068
		public StatusItem PowerLoopDetected;

		// Token: 0x04004695 RID: 18069
		public StatusItem NeedLiquidIn;

		// Token: 0x04004696 RID: 18070
		public StatusItem NeedGasIn;

		// Token: 0x04004697 RID: 18071
		public StatusItem NeedResourceMass;

		// Token: 0x04004698 RID: 18072
		public StatusItem NeedSolidIn;

		// Token: 0x04004699 RID: 18073
		public StatusItem NeedLiquidOut;

		// Token: 0x0400469A RID: 18074
		public StatusItem NeedGasOut;

		// Token: 0x0400469B RID: 18075
		public StatusItem NeedSolidOut;

		// Token: 0x0400469C RID: 18076
		public StatusItem InvalidBuildingLocation;

		// Token: 0x0400469D RID: 18077
		public StatusItem PendingDeconstruction;

		// Token: 0x0400469E RID: 18078
		public StatusItem PendingDemolition;

		// Token: 0x0400469F RID: 18079
		public StatusItem PendingSwitchToggle;

		// Token: 0x040046A0 RID: 18080
		public StatusItem GasVentObstructed;

		// Token: 0x040046A1 RID: 18081
		public StatusItem LiquidVentObstructed;

		// Token: 0x040046A2 RID: 18082
		public StatusItem LiquidPipeEmpty;

		// Token: 0x040046A3 RID: 18083
		public StatusItem LiquidPipeObstructed;

		// Token: 0x040046A4 RID: 18084
		public StatusItem GasPipeEmpty;

		// Token: 0x040046A5 RID: 18085
		public StatusItem GasPipeObstructed;

		// Token: 0x040046A6 RID: 18086
		public StatusItem SolidPipeObstructed;

		// Token: 0x040046A7 RID: 18087
		public StatusItem PartiallyDamaged;

		// Token: 0x040046A8 RID: 18088
		public StatusItem Broken;

		// Token: 0x040046A9 RID: 18089
		public StatusItem PendingRepair;

		// Token: 0x040046AA RID: 18090
		public StatusItem PendingUpgrade;

		// Token: 0x040046AB RID: 18091
		public StatusItem RequiresSkillPerk;

		// Token: 0x040046AC RID: 18092
		public StatusItem DigRequiresSkillPerk;

		// Token: 0x040046AD RID: 18093
		public StatusItem ColonyLacksRequiredSkillPerk;

		// Token: 0x040046AE RID: 18094
		public StatusItem ClusterColonyLacksRequiredSkillPerk;

		// Token: 0x040046AF RID: 18095
		public StatusItem WorkRequiresMinion;

		// Token: 0x040046B0 RID: 18096
		public StatusItem PendingWork;

		// Token: 0x040046B1 RID: 18097
		public StatusItem Flooded;

		// Token: 0x040046B2 RID: 18098
		public StatusItem PowerButtonOff;

		// Token: 0x040046B3 RID: 18099
		public StatusItem SwitchStatusActive;

		// Token: 0x040046B4 RID: 18100
		public StatusItem SwitchStatusInactive;

		// Token: 0x040046B5 RID: 18101
		public StatusItem LogicSwitchStatusActive;

		// Token: 0x040046B6 RID: 18102
		public StatusItem LogicSwitchStatusInactive;

		// Token: 0x040046B7 RID: 18103
		public StatusItem LogicSensorStatusActive;

		// Token: 0x040046B8 RID: 18104
		public StatusItem LogicSensorStatusInactive;

		// Token: 0x040046B9 RID: 18105
		public StatusItem ChangeDoorControlState;

		// Token: 0x040046BA RID: 18106
		public StatusItem CurrentDoorControlState;

		// Token: 0x040046BB RID: 18107
		public StatusItem Entombed;

		// Token: 0x040046BC RID: 18108
		public MaterialsStatusItem WaitingForMaterials;

		// Token: 0x040046BD RID: 18109
		public StatusItem WaitingForHighEnergyParticles;

		// Token: 0x040046BE RID: 18110
		public StatusItem WaitingForRepairMaterials;

		// Token: 0x040046BF RID: 18111
		public StatusItem MissingFoundation;

		// Token: 0x040046C0 RID: 18112
		public StatusItem NeutroniumUnminable;

		// Token: 0x040046C1 RID: 18113
		public StatusItem NoStorageFilterSet;

		// Token: 0x040046C2 RID: 18114
		public StatusItem PendingFish;

		// Token: 0x040046C3 RID: 18115
		public StatusItem NoFishableWaterBelow;

		// Token: 0x040046C4 RID: 18116
		public StatusItem GasVentOverPressure;

		// Token: 0x040046C5 RID: 18117
		public StatusItem LiquidVentOverPressure;

		// Token: 0x040046C6 RID: 18118
		public StatusItem NoWireConnected;

		// Token: 0x040046C7 RID: 18119
		public StatusItem NoLogicWireConnected;

		// Token: 0x040046C8 RID: 18120
		public StatusItem NoTubeConnected;

		// Token: 0x040046C9 RID: 18121
		public StatusItem NoTubeExits;

		// Token: 0x040046CA RID: 18122
		public StatusItem StoredCharge;

		// Token: 0x040046CB RID: 18123
		public StatusItem NoPowerConsumers;

		// Token: 0x040046CC RID: 18124
		public StatusItem PressureOk;

		// Token: 0x040046CD RID: 18125
		public StatusItem UnderPressure;

		// Token: 0x040046CE RID: 18126
		public StatusItem AssignedTo;

		// Token: 0x040046CF RID: 18127
		public StatusItem Unassigned;

		// Token: 0x040046D0 RID: 18128
		public StatusItem AssignedPublic;

		// Token: 0x040046D1 RID: 18129
		public StatusItem AssignedToRoom;

		// Token: 0x040046D2 RID: 18130
		public StatusItem RationBoxContents;

		// Token: 0x040046D3 RID: 18131
		public StatusItem ConduitBlocked;

		// Token: 0x040046D4 RID: 18132
		public StatusItem OutputTileBlocked;

		// Token: 0x040046D5 RID: 18133
		public StatusItem OutputPipeFull;

		// Token: 0x040046D6 RID: 18134
		public StatusItem ConduitBlockedMultiples;

		// Token: 0x040046D7 RID: 18135
		public StatusItem SolidConduitBlockedMultiples;

		// Token: 0x040046D8 RID: 18136
		public StatusItem MeltingDown;

		// Token: 0x040046D9 RID: 18137
		public StatusItem DeadReactorCoolingOff;

		// Token: 0x040046DA RID: 18138
		public StatusItem UnderConstruction;

		// Token: 0x040046DB RID: 18139
		public StatusItem UnderConstructionNoWorker;

		// Token: 0x040046DC RID: 18140
		public StatusItem Normal;

		// Token: 0x040046DD RID: 18141
		public StatusItem ManualGeneratorChargingUp;

		// Token: 0x040046DE RID: 18142
		public StatusItem ManualGeneratorReleasingEnergy;

		// Token: 0x040046DF RID: 18143
		public StatusItem GeneratorOffline;

		// Token: 0x040046E0 RID: 18144
		public StatusItem Pipe;

		// Token: 0x040046E1 RID: 18145
		public StatusItem Conveyor;

		// Token: 0x040046E2 RID: 18146
		public StatusItem FabricatorIdle;

		// Token: 0x040046E3 RID: 18147
		public StatusItem FabricatorEmpty;

		// Token: 0x040046E4 RID: 18148
		public StatusItem FabricatorLacksHEP;

		// Token: 0x040046E5 RID: 18149
		public StatusItem FlushToilet;

		// Token: 0x040046E6 RID: 18150
		public StatusItem FlushToiletInUse;

		// Token: 0x040046E7 RID: 18151
		public StatusItem Toilet;

		// Token: 0x040046E8 RID: 18152
		public StatusItem ToiletNeedsEmptying;

		// Token: 0x040046E9 RID: 18153
		public StatusItem DesalinatorNeedsEmptying;

		// Token: 0x040046EA RID: 18154
		public StatusItem Unusable;

		// Token: 0x040046EB RID: 18155
		public StatusItem NoResearchSelected;

		// Token: 0x040046EC RID: 18156
		public StatusItem NoApplicableResearchSelected;

		// Token: 0x040046ED RID: 18157
		public StatusItem NoApplicableAnalysisSelected;

		// Token: 0x040046EE RID: 18158
		public StatusItem NoResearchOrDestinationSelected;

		// Token: 0x040046EF RID: 18159
		public StatusItem Researching;

		// Token: 0x040046F0 RID: 18160
		public StatusItem ValveRequest;

		// Token: 0x040046F1 RID: 18161
		public StatusItem EmittingLight;

		// Token: 0x040046F2 RID: 18162
		public StatusItem EmittingElement;

		// Token: 0x040046F3 RID: 18163
		public StatusItem EmittingOxygenAvg;

		// Token: 0x040046F4 RID: 18164
		public StatusItem EmittingGasAvg;

		// Token: 0x040046F5 RID: 18165
		public StatusItem EmittingBlockedHighPressure;

		// Token: 0x040046F6 RID: 18166
		public StatusItem EmittingBlockedLowTemperature;

		// Token: 0x040046F7 RID: 18167
		public StatusItem PumpingLiquidOrGas;

		// Token: 0x040046F8 RID: 18168
		public StatusItem NoLiquidElementToPump;

		// Token: 0x040046F9 RID: 18169
		public StatusItem NoGasElementToPump;

		// Token: 0x040046FA RID: 18170
		public StatusItem PipeFull;

		// Token: 0x040046FB RID: 18171
		public StatusItem PipeMayMelt;

		// Token: 0x040046FC RID: 18172
		public StatusItem ElementConsumer;

		// Token: 0x040046FD RID: 18173
		public StatusItem ElementEmitterOutput;

		// Token: 0x040046FE RID: 18174
		public StatusItem AwaitingWaste;

		// Token: 0x040046FF RID: 18175
		public StatusItem AwaitingCompostFlip;

		// Token: 0x04004700 RID: 18176
		public StatusItem JoulesAvailable;

		// Token: 0x04004701 RID: 18177
		public StatusItem Wattage;

		// Token: 0x04004702 RID: 18178
		public StatusItem SolarPanelWattage;

		// Token: 0x04004703 RID: 18179
		public StatusItem ModuleSolarPanelWattage;

		// Token: 0x04004704 RID: 18180
		public StatusItem SteamTurbineWattage;

		// Token: 0x04004705 RID: 18181
		public StatusItem Wattson;

		// Token: 0x04004706 RID: 18182
		public StatusItem WireConnected;

		// Token: 0x04004707 RID: 18183
		public StatusItem WireNominal;

		// Token: 0x04004708 RID: 18184
		public StatusItem WireDisconnected;

		// Token: 0x04004709 RID: 18185
		public StatusItem Cooling;

		// Token: 0x0400470A RID: 18186
		public StatusItem CoolingStalledHotEnv;

		// Token: 0x0400470B RID: 18187
		public StatusItem CoolingStalledColdGas;

		// Token: 0x0400470C RID: 18188
		public StatusItem CoolingStalledHotLiquid;

		// Token: 0x0400470D RID: 18189
		public StatusItem CoolingStalledColdLiquid;

		// Token: 0x0400470E RID: 18190
		public StatusItem CannotCoolFurther;

		// Token: 0x0400470F RID: 18191
		public StatusItem NeedsValidRegion;

		// Token: 0x04004710 RID: 18192
		public StatusItem NeedSeed;

		// Token: 0x04004711 RID: 18193
		public StatusItem AwaitingSeedDelivery;

		// Token: 0x04004712 RID: 18194
		public StatusItem AwaitingBaitDelivery;

		// Token: 0x04004713 RID: 18195
		public StatusItem NoAvailableSeed;

		// Token: 0x04004714 RID: 18196
		public StatusItem NeedEgg;

		// Token: 0x04004715 RID: 18197
		public StatusItem AwaitingEggDelivery;

		// Token: 0x04004716 RID: 18198
		public StatusItem NoAvailableEgg;

		// Token: 0x04004717 RID: 18199
		public StatusItem Grave;

		// Token: 0x04004718 RID: 18200
		public StatusItem GraveEmpty;

		// Token: 0x04004719 RID: 18201
		public StatusItem NoFilterElementSelected;

		// Token: 0x0400471A RID: 18202
		public StatusItem NoLureElementSelected;

		// Token: 0x0400471B RID: 18203
		public StatusItem BuildingDisabled;

		// Token: 0x0400471C RID: 18204
		public StatusItem Overheated;

		// Token: 0x0400471D RID: 18205
		public StatusItem Overloaded;

		// Token: 0x0400471E RID: 18206
		public StatusItem LogicOverloaded;

		// Token: 0x0400471F RID: 18207
		public StatusItem Expired;

		// Token: 0x04004720 RID: 18208
		public StatusItem PumpingStation;

		// Token: 0x04004721 RID: 18209
		public StatusItem EmptyPumpingStation;

		// Token: 0x04004722 RID: 18210
		public StatusItem GeneShuffleCompleted;

		// Token: 0x04004723 RID: 18211
		public StatusItem GeneticAnalysisCompleted;

		// Token: 0x04004724 RID: 18212
		public StatusItem DirectionControl;

		// Token: 0x04004725 RID: 18213
		public StatusItem WellPressurizing;

		// Token: 0x04004726 RID: 18214
		public StatusItem WellOverpressure;

		// Token: 0x04004727 RID: 18215
		public StatusItem ReleasingPressure;

		// Token: 0x04004728 RID: 18216
		public StatusItem ReactorMeltdown;

		// Token: 0x04004729 RID: 18217
		public StatusItem NoSuitMarker;

		// Token: 0x0400472A RID: 18218
		public StatusItem SuitMarkerWrongSide;

		// Token: 0x0400472B RID: 18219
		public StatusItem SuitMarkerTraversalAnytime;

		// Token: 0x0400472C RID: 18220
		public StatusItem SuitMarkerTraversalOnlyWhenRoomAvailable;

		// Token: 0x0400472D RID: 18221
		public StatusItem TooCold;

		// Token: 0x0400472E RID: 18222
		public StatusItem NotInAnyRoom;

		// Token: 0x0400472F RID: 18223
		public StatusItem NotInRequiredRoom;

		// Token: 0x04004730 RID: 18224
		public StatusItem NotInRecommendedRoom;

		// Token: 0x04004731 RID: 18225
		public StatusItem IncubatorProgress;

		// Token: 0x04004732 RID: 18226
		public StatusItem HabitatNeedsEmptying;

		// Token: 0x04004733 RID: 18227
		public StatusItem DetectorScanning;

		// Token: 0x04004734 RID: 18228
		public StatusItem IncomingMeteors;

		// Token: 0x04004735 RID: 18229
		public StatusItem HasGantry;

		// Token: 0x04004736 RID: 18230
		public StatusItem MissingGantry;

		// Token: 0x04004737 RID: 18231
		public StatusItem DisembarkingDuplicant;

		// Token: 0x04004738 RID: 18232
		public StatusItem RocketName;

		// Token: 0x04004739 RID: 18233
		public StatusItem PathNotClear;

		// Token: 0x0400473A RID: 18234
		public StatusItem InvalidPortOverlap;

		// Token: 0x0400473B RID: 18235
		public StatusItem EmergencyPriority;

		// Token: 0x0400473C RID: 18236
		public StatusItem SkillPointsAvailable;

		// Token: 0x0400473D RID: 18237
		public StatusItem Baited;

		// Token: 0x0400473E RID: 18238
		public StatusItem NoCoolant;

		// Token: 0x0400473F RID: 18239
		public StatusItem TanningLightSufficient;

		// Token: 0x04004740 RID: 18240
		public StatusItem TanningLightInsufficient;

		// Token: 0x04004741 RID: 18241
		public StatusItem HotTubWaterTooCold;

		// Token: 0x04004742 RID: 18242
		public StatusItem HotTubTooHot;

		// Token: 0x04004743 RID: 18243
		public StatusItem HotTubFilling;

		// Token: 0x04004744 RID: 18244
		public StatusItem WindTunnelIntake;

		// Token: 0x04004745 RID: 18245
		public StatusItem CollectingHEP;

		// Token: 0x04004746 RID: 18246
		public StatusItem ReactorRefuelDisabled;

		// Token: 0x04004747 RID: 18247
		public StatusItem FridgeCooling;

		// Token: 0x04004748 RID: 18248
		public StatusItem FridgeSteady;

		// Token: 0x04004749 RID: 18249
		public StatusItem WarpPortalCharging;

		// Token: 0x0400474A RID: 18250
		public StatusItem WarpConduitPartnerDisabled;

		// Token: 0x0400474B RID: 18251
		public StatusItem InOrbit;

		// Token: 0x0400474C RID: 18252
		public StatusItem InFlight;

		// Token: 0x0400474D RID: 18253
		public StatusItem DestinationOutOfRange;

		// Token: 0x0400474E RID: 18254
		public StatusItem RocketStranded;

		// Token: 0x0400474F RID: 18255
		public StatusItem RailgunpayloadNeedsEmptying;

		// Token: 0x04004750 RID: 18256
		public StatusItem AwaitingEmptyBuilding;

		// Token: 0x04004751 RID: 18257
		public StatusItem DuplicantActivationRequired;

		// Token: 0x04004752 RID: 18258
		public StatusItem RocketChecklistIncomplete;

		// Token: 0x04004753 RID: 18259
		public StatusItem RocketCargoEmptying;

		// Token: 0x04004754 RID: 18260
		public StatusItem RocketCargoFilling;

		// Token: 0x04004755 RID: 18261
		public StatusItem RocketCargoFull;

		// Token: 0x04004756 RID: 18262
		public StatusItem FlightAllCargoFull;

		// Token: 0x04004757 RID: 18263
		public StatusItem FlightCargoRemaining;

		// Token: 0x04004758 RID: 18264
		public StatusItem LandedRocketLacksPassengerModule;

		// Token: 0x04004759 RID: 18265
		public StatusItem PilotNeeded;

		// Token: 0x0400475A RID: 18266
		public StatusItem AutoPilotActive;

		// Token: 0x0400475B RID: 18267
		public StatusItem InvalidMaskStationConsumptionState;

		// Token: 0x0400475C RID: 18268
		public StatusItem ClusterTelescopeAllWorkComplete;

		// Token: 0x0400475D RID: 18269
		public StatusItem RocketPlatformCloseToCeiling;

		// Token: 0x0400475E RID: 18270
		public StatusItem ModuleGeneratorPowered;

		// Token: 0x0400475F RID: 18271
		public StatusItem ModuleGeneratorNotPowered;

		// Token: 0x04004760 RID: 18272
		public StatusItem InOrbitRequired;

		// Token: 0x04004761 RID: 18273
		public StatusItem RailGunCooldown;

		// Token: 0x04004762 RID: 18274
		public StatusItem NoSurfaceSight;

		// Token: 0x04004763 RID: 18275
		public StatusItem LimitValveLimitReached;

		// Token: 0x04004764 RID: 18276
		public StatusItem LimitValveLimitNotReached;

		// Token: 0x04004765 RID: 18277
		public StatusItem SpacePOIHarvesting;

		// Token: 0x04004766 RID: 18278
		public StatusItem SpacePOIWasting;

		// Token: 0x04004767 RID: 18279
		public StatusItem RocketRestrictionActive;

		// Token: 0x04004768 RID: 18280
		public StatusItem RocketRestrictionInactive;

		// Token: 0x04004769 RID: 18281
		public StatusItem NoRocketRestriction;

		// Token: 0x0400476A RID: 18282
		public StatusItem BroadcasterOutOfRange;

		// Token: 0x0400476B RID: 18283
		public StatusItem LosingRadbolts;

		// Token: 0x0400476C RID: 18284
		public StatusItem FabricatorAcceptsMutantSeeds;

		// Token: 0x0400476D RID: 18285
		public StatusItem NoSpiceSelected;

		// Token: 0x0400476E RID: 18286
		public StatusItem MissionControlAssistingRocket;

		// Token: 0x0400476F RID: 18287
		public StatusItem NoRocketsToMissionControlBoost;

		// Token: 0x04004770 RID: 18288
		public StatusItem NoRocketsToMissionControlClusterBoost;

		// Token: 0x04004771 RID: 18289
		public StatusItem MissionControlBoosted;

		// Token: 0x04004772 RID: 18290
		public StatusItem GeoTunerNoGeyserSelected;

		// Token: 0x04004773 RID: 18291
		public StatusItem GeoTunerResearchNeeded;

		// Token: 0x04004774 RID: 18292
		public StatusItem GeoTunerResearchInProgress;

		// Token: 0x04004775 RID: 18293
		public StatusItem GeoTunerBroadcasting;

		// Token: 0x04004776 RID: 18294
		public StatusItem GeoTunerGeyserStatus;

		// Token: 0x04004777 RID: 18295
		public StatusItem GeyserGeotuned;

		// Token: 0x04004778 RID: 18296
		public StatusItem CreatureManipulatorWaiting;

		// Token: 0x04004779 RID: 18297
		public StatusItem CreatureManipulatorProgress;

		// Token: 0x0400477A RID: 18298
		public StatusItem CreatureManipulatorMorphModeLocked;

		// Token: 0x0400477B RID: 18299
		public StatusItem CreatureManipulatorMorphMode;

		// Token: 0x0400477C RID: 18300
		public StatusItem CreatureManipulatorWorking;

		// Token: 0x0400477D RID: 18301
		public StatusItem MegaBrainNotEnoughOxygen;

		// Token: 0x0400477E RID: 18302
		public StatusItem MegaBrainTankActivationProgress;

		// Token: 0x0400477F RID: 18303
		public StatusItem MegaBrainTankDreamAnalysis;

		// Token: 0x04004780 RID: 18304
		public StatusItem MegaBrainTankAllDupesAreDead;

		// Token: 0x04004781 RID: 18305
		public StatusItem MegaBrainTankComplete;
	}
}
