using System;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x020008F4 RID: 2292
public static class RoomConstraints
{
	// Token: 0x06004239 RID: 16953 RVA: 0x00174740 File Offset: 0x00172940
	public static string RoomCriteriaString(Room room)
	{
		string text = "";
		RoomType roomType = room.roomType;
		if (roomType != Db.Get().RoomTypes.Neutral)
		{
			text = text + "<b>" + ROOMS.CRITERIA.HEADER + "</b>";
			text = text + "\n    • " + roomType.primary_constraint.name;
			if (roomType.additional_constraints != null)
			{
				foreach (RoomConstraints.Constraint constraint in roomType.additional_constraints)
				{
					if (constraint.isSatisfied(room))
					{
						text = text + "\n    • " + constraint.name;
					}
					else
					{
						text = text + "\n<color=#F44A47FF>    • " + constraint.name + "</color>";
					}
				}
			}
			return text;
		}
		RoomTypes.RoomTypeQueryResult[] possibleRoomTypes = Db.Get().RoomTypes.GetPossibleRoomTypes(room);
		text += ((possibleRoomTypes.Length > 1) ? ("<b>" + ROOMS.CRITERIA.POSSIBLE_TYPES_HEADER + "</b>") : "");
		foreach (RoomTypes.RoomTypeQueryResult roomTypeQueryResult in possibleRoomTypes)
		{
			RoomType type = roomTypeQueryResult.Type;
			if (type != Db.Get().RoomTypes.Neutral)
			{
				if (text != "")
				{
					text += "\n";
				}
				text = string.Concat(new string[]
				{
					text,
					"<b><color=#BCBCBC>    • ",
					type.Name,
					"</b> (",
					type.primary_constraint.name,
					")</color>"
				});
				if (roomTypeQueryResult.SatisfactionRating == RoomType.RoomIdentificationResult.all_satisfied)
				{
					bool flag = false;
					RoomTypes.RoomTypeQueryResult[] array3 = possibleRoomTypes;
					for (int j = 0; j < array3.Length; j++)
					{
						RoomType type2 = array3[j].Type;
						if (type2 != type && type2 != Db.Get().RoomTypes.Neutral && Db.Get().RoomTypes.HasAmbiguousRoomType(room, type, type2))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						text += string.Format("\n<color=#F44A47FF>{0}{1}{2}</color>", "    ", "    • ", ROOMS.CRITERIA.NO_TYPE_CONFLICTS);
					}
				}
				else
				{
					foreach (RoomConstraints.Constraint constraint2 in type.additional_constraints)
					{
						if (!constraint2.isSatisfied(room))
						{
							string text2 = string.Empty;
							if (constraint2.building_criteria != null)
							{
								text2 = string.Format(ROOMS.CRITERIA.CRITERIA_FAILED.MISSING_BUILDING, constraint2.name);
							}
							else
							{
								text2 = string.Format(ROOMS.CRITERIA.CRITERIA_FAILED.FAILED, constraint2.name);
							}
							text = text + "\n<color=#F44A47FF>        • " + text2 + "</color>";
						}
					}
				}
			}
		}
		return text;
	}

	// Token: 0x04002C08 RID: 11272
	public static RoomConstraints.Constraint CEILING_HEIGHT_4 = new RoomConstraints.Constraint(null, (Room room) => 1 + room.cavity.maxY - room.cavity.minY >= 4, 1, string.Format(ROOMS.CRITERIA.CEILING_HEIGHT.NAME, "4"), string.Format(ROOMS.CRITERIA.CEILING_HEIGHT.DESCRIPTION, "4"), null);

	// Token: 0x04002C09 RID: 11273
	public static RoomConstraints.Constraint CEILING_HEIGHT_6 = new RoomConstraints.Constraint(null, (Room room) => 1 + room.cavity.maxY - room.cavity.minY >= 6, 1, string.Format(ROOMS.CRITERIA.CEILING_HEIGHT.NAME, "6"), string.Format(ROOMS.CRITERIA.CEILING_HEIGHT.DESCRIPTION, "6"), null);

	// Token: 0x04002C0A RID: 11274
	public static RoomConstraints.Constraint MINIMUM_SIZE_12 = new RoomConstraints.Constraint(null, (Room room) => room.cavity.numCells >= 12, 1, string.Format(ROOMS.CRITERIA.MINIMUM_SIZE.NAME, "12"), string.Format(ROOMS.CRITERIA.MINIMUM_SIZE.DESCRIPTION, "12"), null);

	// Token: 0x04002C0B RID: 11275
	public static RoomConstraints.Constraint MINIMUM_SIZE_24 = new RoomConstraints.Constraint(null, (Room room) => room.cavity.numCells >= 24, 1, string.Format(ROOMS.CRITERIA.MINIMUM_SIZE.NAME, "24"), string.Format(ROOMS.CRITERIA.MINIMUM_SIZE.DESCRIPTION, "24"), null);

	// Token: 0x04002C0C RID: 11276
	public static RoomConstraints.Constraint MINIMUM_SIZE_32 = new RoomConstraints.Constraint(null, (Room room) => room.cavity.numCells >= 32, 1, string.Format(ROOMS.CRITERIA.MINIMUM_SIZE.NAME, "32"), string.Format(ROOMS.CRITERIA.MINIMUM_SIZE.DESCRIPTION, "32"), null);

	// Token: 0x04002C0D RID: 11277
	public static RoomConstraints.Constraint MAXIMUM_SIZE_64 = new RoomConstraints.Constraint(null, (Room room) => room.cavity.numCells <= 64, 1, string.Format(ROOMS.CRITERIA.MAXIMUM_SIZE.NAME, "64"), string.Format(ROOMS.CRITERIA.MAXIMUM_SIZE.DESCRIPTION, "64"), null);

	// Token: 0x04002C0E RID: 11278
	public static RoomConstraints.Constraint MAXIMUM_SIZE_96 = new RoomConstraints.Constraint(null, (Room room) => room.cavity.numCells <= 96, 1, string.Format(ROOMS.CRITERIA.MAXIMUM_SIZE.NAME, "96"), string.Format(ROOMS.CRITERIA.MAXIMUM_SIZE.DESCRIPTION, "96"), null);

	// Token: 0x04002C0F RID: 11279
	public static RoomConstraints.Constraint MAXIMUM_SIZE_120 = new RoomConstraints.Constraint(null, (Room room) => room.cavity.numCells <= 120, 1, string.Format(ROOMS.CRITERIA.MAXIMUM_SIZE.NAME, "120"), string.Format(ROOMS.CRITERIA.MAXIMUM_SIZE.DESCRIPTION, "120"), null);

	// Token: 0x04002C10 RID: 11280
	public static RoomConstraints.Constraint NO_INDUSTRIAL_MACHINERY = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		using (List<KPrefabID>.Enumerator enumerator = room.buildings.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasTag(RoomConstraints.ConstraintTags.IndustrialMachinery))
				{
					return false;
				}
			}
		}
		return true;
	}, 1, ROOMS.CRITERIA.NO_INDUSTRIAL_MACHINERY.NAME, ROOMS.CRITERIA.NO_INDUSTRIAL_MACHINERY.DESCRIPTION, null);

	// Token: 0x04002C11 RID: 11281
	public static RoomConstraints.Constraint NO_COTS = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		foreach (KPrefabID kprefabID in room.buildings)
		{
			if (kprefabID.HasTag(RoomConstraints.ConstraintTags.BedType) && !kprefabID.HasTag(RoomConstraints.ConstraintTags.LuxuryBedType))
			{
				return false;
			}
		}
		return true;
	}, 1, ROOMS.CRITERIA.NO_COTS.NAME, ROOMS.CRITERIA.NO_COTS.DESCRIPTION, null);

	// Token: 0x04002C12 RID: 11282
	public static RoomConstraints.Constraint NO_LUXURY_BEDS = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		using (List<KPrefabID>.Enumerator enumerator3 = room.buildings.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				if (enumerator3.Current.HasTag(RoomConstraints.ConstraintTags.LuxuryBedType))
				{
					return false;
				}
			}
		}
		return true;
	}, 1, ROOMS.CRITERIA.NO_COTS.NAME, ROOMS.CRITERIA.NO_COTS.DESCRIPTION, null);

	// Token: 0x04002C13 RID: 11283
	public static RoomConstraints.Constraint NO_OUTHOUSES = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		foreach (KPrefabID kprefabID2 in room.buildings)
		{
			if (kprefabID2.HasTag(RoomConstraints.ConstraintTags.ToiletType) && !kprefabID2.HasTag(RoomConstraints.ConstraintTags.FlushToiletType))
			{
				return false;
			}
		}
		return true;
	}, 1, ROOMS.CRITERIA.NO_OUTHOUSES.NAME, ROOMS.CRITERIA.NO_OUTHOUSES.DESCRIPTION, null);

	// Token: 0x04002C14 RID: 11284
	public static RoomConstraints.Constraint NO_MESS_STATION = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		bool flag = false;
		int num = 0;
		while (!flag && num < room.buildings.Count)
		{
			flag = room.buildings[num].HasTag(RoomConstraints.ConstraintTags.MessTable);
			num++;
		}
		return !flag;
	}, 1, ROOMS.CRITERIA.NO_MESS_STATION.NAME, ROOMS.CRITERIA.NO_MESS_STATION.DESCRIPTION, null);

	// Token: 0x04002C15 RID: 11285
	public static RoomConstraints.Constraint HAS_LUXURY_BED = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.LuxuryBedType), null, 1, ROOMS.CRITERIA.HAS_LUXURY_BED.NAME, ROOMS.CRITERIA.HAS_LUXURY_BED.DESCRIPTION, null);

	// Token: 0x04002C16 RID: 11286
	public static RoomConstraints.Constraint HAS_BED = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.BedType) && !bc.HasTag(RoomConstraints.ConstraintTags.Clinic), null, 1, ROOMS.CRITERIA.HAS_BED.NAME, ROOMS.CRITERIA.HAS_BED.DESCRIPTION, null);

	// Token: 0x04002C17 RID: 11287
	public static RoomConstraints.Constraint SCIENCE_BUILDINGS = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.ScienceBuilding), null, 2, ROOMS.CRITERIA.SCIENCE_BUILDINGS.NAME, ROOMS.CRITERIA.SCIENCE_BUILDINGS.DESCRIPTION, null);

	// Token: 0x04002C18 RID: 11288
	public static RoomConstraints.Constraint BED_SINGLE = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.BedType) && !bc.HasTag(RoomConstraints.ConstraintTags.Clinic), delegate(Room room)
	{
		short num2 = 0;
		int num3 = 0;
		while (num2 < 2 && num3 < room.buildings.Count)
		{
			if (room.buildings[num3].HasTag(RoomConstraints.ConstraintTags.BedType))
			{
				num2 += 1;
			}
			num3++;
		}
		return num2 == 1;
	}, 1, ROOMS.CRITERIA.BED_SINGLE.NAME, ROOMS.CRITERIA.BED_SINGLE.DESCRIPTION, null);

	// Token: 0x04002C19 RID: 11289
	public static RoomConstraints.Constraint LUXURY_BED_SINGLE = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.LuxuryBedType), delegate(Room room)
	{
		short num4 = 0;
		int num5 = 0;
		while (num4 <= 2 && num5 < room.buildings.Count)
		{
			if (room.buildings[num5].HasTag(RoomConstraints.ConstraintTags.LuxuryBedType))
			{
				num4 += 1;
			}
			num5++;
		}
		return num4 == 1;
	}, 1, ROOMS.CRITERIA.LUXURY_BED_SINGLE.NAME, ROOMS.CRITERIA.LUXURY_BED_SINGLE.DESCRIPTION, null);

	// Token: 0x04002C1A RID: 11290
	public static RoomConstraints.Constraint BUILDING_DECOR_POSITIVE = new RoomConstraints.Constraint(delegate(KPrefabID bc)
	{
		DecorProvider component = bc.GetComponent<DecorProvider>();
		return component != null && component.baseDecor > 0f;
	}, null, 1, ROOMS.CRITERIA.BUILDING_DECOR_POSITIVE.NAME, ROOMS.CRITERIA.BUILDING_DECOR_POSITIVE.DESCRIPTION, null);

	// Token: 0x04002C1B RID: 11291
	public static RoomConstraints.Constraint DECORATIVE_ITEM = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(GameTags.Decoration), null, 1, string.Format(ROOMS.CRITERIA.DECORATIVE_ITEM.NAME, 1), string.Format(ROOMS.CRITERIA.DECORATIVE_ITEM.DESCRIPTION, 1), null);

	// Token: 0x04002C1C RID: 11292
	public static RoomConstraints.Constraint DECORATIVE_ITEM_2 = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(GameTags.Decoration), null, 2, string.Format(ROOMS.CRITERIA.DECORATIVE_ITEM.NAME, 2), string.Format(ROOMS.CRITERIA.DECORATIVE_ITEM.DESCRIPTION, 2), null);

	// Token: 0x04002C1D RID: 11293
	public static RoomConstraints.Constraint DECORATIVE_ITEM_SCORE_20 = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(GameTags.Decoration) && bc.HasTag(RoomConstraints.ConstraintTags.Decor20), null, 1, string.Format(ROOMS.CRITERIA.DECORATIVE_ITEM_N.NAME, "20"), string.Format(ROOMS.CRITERIA.DECORATIVE_ITEM_N.DESCRIPTION, "20"), null);

	// Token: 0x04002C1E RID: 11294
	public static RoomConstraints.Constraint POWER_STATION = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.PowerStation), null, 1, ROOMS.CRITERIA.POWER_STATION.NAME, ROOMS.CRITERIA.POWER_STATION.DESCRIPTION, null);

	// Token: 0x04002C1F RID: 11295
	public static RoomConstraints.Constraint FARM_STATION = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.FarmStationType), null, 1, ROOMS.CRITERIA.FARM_STATION.NAME, ROOMS.CRITERIA.FARM_STATION.DESCRIPTION, null);

	// Token: 0x04002C20 RID: 11296
	public static RoomConstraints.Constraint RANCH_STATION = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.RanchStationType), null, 1, ROOMS.CRITERIA.RANCH_STATION.NAME, ROOMS.CRITERIA.RANCH_STATION.DESCRIPTION, null);

	// Token: 0x04002C21 RID: 11297
	public static RoomConstraints.Constraint SPICE_STATION = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.SpiceStation), null, 1, ROOMS.CRITERIA.SPICE_STATION.NAME, ROOMS.CRITERIA.SPICE_STATION.DESCRIPTION, null);

	// Token: 0x04002C22 RID: 11298
	public static RoomConstraints.Constraint COOK_TOP = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.CookTop), null, 1, ROOMS.CRITERIA.COOK_TOP.NAME, ROOMS.CRITERIA.COOK_TOP.DESCRIPTION, null);

	// Token: 0x04002C23 RID: 11299
	public static RoomConstraints.Constraint REFRIGERATOR = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.Refrigerator), null, 1, ROOMS.CRITERIA.REFRIGERATOR.NAME, ROOMS.CRITERIA.REFRIGERATOR.DESCRIPTION, null);

	// Token: 0x04002C24 RID: 11300
	public static RoomConstraints.Constraint REC_BUILDING = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.RecBuilding), null, 1, ROOMS.CRITERIA.REC_BUILDING.NAME, ROOMS.CRITERIA.REC_BUILDING.DESCRIPTION, null);

	// Token: 0x04002C25 RID: 11301
	public static RoomConstraints.Constraint MACHINE_SHOP = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.MachineShopType), null, 1, ROOMS.CRITERIA.MACHINE_SHOP.NAME, ROOMS.CRITERIA.MACHINE_SHOP.DESCRIPTION, null);

	// Token: 0x04002C26 RID: 11302
	public static RoomConstraints.Constraint LIGHT = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		foreach (KPrefabID kprefabID3 in room.cavity.creatures)
		{
			if (kprefabID3 != null && kprefabID3.GetComponent<Light2D>() != null)
			{
				return true;
			}
		}
		foreach (KPrefabID kprefabID4 in room.buildings)
		{
			if (!(kprefabID4 == null))
			{
				Light2D component2 = kprefabID4.GetComponent<Light2D>();
				if (component2 != null)
				{
					RequireInputs component3 = kprefabID4.GetComponent<RequireInputs>();
					if (component2.enabled || (component3 != null && component3.RequirementsMet))
					{
						return true;
					}
				}
			}
		}
		return false;
	}, 1, ROOMS.CRITERIA.LIGHT.NAME, ROOMS.CRITERIA.LIGHT.DESCRIPTION, null);

	// Token: 0x04002C27 RID: 11303
	public static RoomConstraints.Constraint DESTRESSING_BUILDING = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.DeStressingBuilding), null, 1, ROOMS.CRITERIA.DESTRESSING_BUILDING.NAME, ROOMS.CRITERIA.DESTRESSING_BUILDING.DESCRIPTION, null);

	// Token: 0x04002C28 RID: 11304
	public static RoomConstraints.Constraint MASSAGE_TABLE = new RoomConstraints.Constraint((KPrefabID bc) => bc.IsPrefabID(RoomConstraints.ConstraintTags.MassageTable), null, 1, ROOMS.CRITERIA.MASSAGE_TABLE.NAME, ROOMS.CRITERIA.MASSAGE_TABLE.DESCRIPTION, null);

	// Token: 0x04002C29 RID: 11305
	public static RoomConstraints.Constraint MESS_STATION_SINGLE = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.MessTable), null, 1, ROOMS.CRITERIA.MESS_STATION_SINGLE.NAME, ROOMS.CRITERIA.MESS_STATION_SINGLE.DESCRIPTION, new List<RoomConstraints.Constraint> { RoomConstraints.REC_BUILDING });

	// Token: 0x04002C2A RID: 11306
	public static RoomConstraints.Constraint TOILET = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.ToiletType), null, 1, ROOMS.CRITERIA.TOILET.NAME, ROOMS.CRITERIA.TOILET.DESCRIPTION, null);

	// Token: 0x04002C2B RID: 11307
	public static RoomConstraints.Constraint FLUSH_TOILET = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.FlushToiletType), null, 1, ROOMS.CRITERIA.FLUSH_TOILET.NAME, ROOMS.CRITERIA.FLUSH_TOILET.DESCRIPTION, null);

	// Token: 0x04002C2C RID: 11308
	public static RoomConstraints.Constraint WASH_STATION = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.WashStation), null, 1, ROOMS.CRITERIA.WASH_STATION.NAME, ROOMS.CRITERIA.WASH_STATION.DESCRIPTION, null);

	// Token: 0x04002C2D RID: 11309
	public static RoomConstraints.Constraint ADVANCED_WASH_STATION = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.AdvancedWashStation), null, 1, ROOMS.CRITERIA.ADVANCED_WASH_STATION.NAME, ROOMS.CRITERIA.ADVANCED_WASH_STATION.DESCRIPTION, null);

	// Token: 0x04002C2E RID: 11310
	public static RoomConstraints.Constraint CLINIC = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.Clinic), null, 1, ROOMS.CRITERIA.CLINIC.NAME, ROOMS.CRITERIA.CLINIC.DESCRIPTION, new List<RoomConstraints.Constraint>
	{
		RoomConstraints.TOILET,
		RoomConstraints.FLUSH_TOILET,
		RoomConstraints.MESS_STATION_SINGLE
	});

	// Token: 0x04002C2F RID: 11311
	public static RoomConstraints.Constraint PARK_BUILDING = new RoomConstraints.Constraint((KPrefabID bc) => bc.HasTag(RoomConstraints.ConstraintTags.Park), null, 1, ROOMS.CRITERIA.PARK_BUILDING.NAME, ROOMS.CRITERIA.PARK_BUILDING.DESCRIPTION, null);

	// Token: 0x04002C30 RID: 11312
	public static RoomConstraints.Constraint ORIGINALTILES = new RoomConstraints.Constraint(null, (Room room) => 1 + room.cavity.maxY - room.cavity.minY >= 4, 1, "", "", null);

	// Token: 0x04002C31 RID: 11313
	public static RoomConstraints.Constraint IS_BACKWALLED = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		bool flag2 = true;
		int num6 = (room.cavity.maxX - room.cavity.minX + 1) / 2 + 1;
		int num7 = 0;
		while (flag2 && num7 < num6)
		{
			int num8 = room.cavity.minX + num7;
			int num9 = room.cavity.maxX - num7;
			int num10 = room.cavity.minY;
			while (flag2 && num10 <= room.cavity.maxY)
			{
				int num11 = Grid.XYToCell(num8, num10);
				int num12 = Grid.XYToCell(num9, num10);
				if (Game.Instance.roomProber.GetCavityForCell(num11) == room.cavity)
				{
					GameObject gameObject = Grid.Objects[num11, 2];
					flag2 &= gameObject != null && !gameObject.HasTag(GameTags.UnderConstruction);
				}
				if (Game.Instance.roomProber.GetCavityForCell(num12) == room.cavity)
				{
					GameObject gameObject2 = Grid.Objects[num12, 2];
					flag2 &= gameObject2 != null && !gameObject2.HasTag(GameTags.UnderConstruction);
				}
				if (!flag2)
				{
					return false;
				}
				num10++;
			}
			num7++;
		}
		return flag2;
	}, 1, ROOMS.CRITERIA.IS_BACKWALLED.NAME, ROOMS.CRITERIA.IS_BACKWALLED.DESCRIPTION, null);

	// Token: 0x04002C32 RID: 11314
	public static RoomConstraints.Constraint WILDANIMAL = new RoomConstraints.Constraint(null, (Room room) => room.cavity.creatures.Count + room.cavity.eggs.Count > 0, 1, ROOMS.CRITERIA.WILDANIMAL.NAME, ROOMS.CRITERIA.WILDANIMAL.DESCRIPTION, null);

	// Token: 0x04002C33 RID: 11315
	public static RoomConstraints.Constraint WILDANIMALS = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		int num13 = 0;
		using (List<KPrefabID>.Enumerator enumerator6 = room.cavity.creatures.GetEnumerator())
		{
			while (enumerator6.MoveNext())
			{
				if (enumerator6.Current.HasTag(GameTags.Creatures.Wild))
				{
					num13++;
				}
			}
		}
		return num13 >= 2;
	}, 1, ROOMS.CRITERIA.WILDANIMALS.NAME, ROOMS.CRITERIA.WILDANIMALS.DESCRIPTION, null);

	// Token: 0x04002C34 RID: 11316
	public static RoomConstraints.Constraint WILDPLANT = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		int num14 = 0;
		foreach (KPrefabID kprefabID5 in room.cavity.plants)
		{
			if (kprefabID5 != null)
			{
				BasicForagePlantPlanted component4 = kprefabID5.GetComponent<BasicForagePlantPlanted>();
				ReceptacleMonitor component5 = kprefabID5.GetComponent<ReceptacleMonitor>();
				if (component5 != null && !component5.Replanted)
				{
					num14++;
				}
				else if (component4 != null)
				{
					num14++;
				}
			}
		}
		return num14 >= 2;
	}, 1, ROOMS.CRITERIA.WILDPLANT.NAME, ROOMS.CRITERIA.WILDPLANT.DESCRIPTION, null);

	// Token: 0x04002C35 RID: 11317
	public static RoomConstraints.Constraint WILDPLANTS = new RoomConstraints.Constraint(null, delegate(Room room)
	{
		int num15 = 0;
		foreach (KPrefabID kprefabID6 in room.cavity.plants)
		{
			if (kprefabID6 != null)
			{
				BasicForagePlantPlanted component6 = kprefabID6.GetComponent<BasicForagePlantPlanted>();
				ReceptacleMonitor component7 = kprefabID6.GetComponent<ReceptacleMonitor>();
				if (component7 != null && !component7.Replanted)
				{
					num15++;
				}
				else if (component6 != null)
				{
					num15++;
				}
			}
		}
		return num15 >= 4;
	}, 1, ROOMS.CRITERIA.WILDPLANTS.NAME, ROOMS.CRITERIA.WILDPLANTS.DESCRIPTION, null);

	// Token: 0x020016BE RID: 5822
	public static class ConstraintTags
	{
		// Token: 0x04006AC1 RID: 27329
		public static Tag BedType = "BedType".ToTag();

		// Token: 0x04006AC2 RID: 27330
		public static Tag LuxuryBedType = "LuxuryBedType".ToTag();

		// Token: 0x04006AC3 RID: 27331
		public static Tag ToiletType = "ToiletType".ToTag();

		// Token: 0x04006AC4 RID: 27332
		public static Tag FlushToiletType = "FlushToiletType".ToTag();

		// Token: 0x04006AC5 RID: 27333
		public static Tag MessTable = "MessTable".ToTag();

		// Token: 0x04006AC6 RID: 27334
		public static Tag Clinic = "Clinic".ToTag();

		// Token: 0x04006AC7 RID: 27335
		public static Tag WashStation = "WashStation".ToTag();

		// Token: 0x04006AC8 RID: 27336
		public static Tag AdvancedWashStation = "AdvancedWashStation".ToTag();

		// Token: 0x04006AC9 RID: 27337
		public static Tag ScienceBuilding = "ScienceBuilding".ToTag();

		// Token: 0x04006ACA RID: 27338
		public static Tag LightSource = "LightSource".ToTag();

		// Token: 0x04006ACB RID: 27339
		public static Tag MassageTable = "MassageTable".ToTag();

		// Token: 0x04006ACC RID: 27340
		public static Tag DeStressingBuilding = "DeStressingBuilding".ToTag();

		// Token: 0x04006ACD RID: 27341
		public static Tag IndustrialMachinery = "IndustrialMachinery".ToTag();

		// Token: 0x04006ACE RID: 27342
		public static Tag PowerStation = "PowerStation".ToTag();

		// Token: 0x04006ACF RID: 27343
		public static Tag FarmStationType = "FarmStationType".ToTag();

		// Token: 0x04006AD0 RID: 27344
		public static Tag CreatureRelocator = "CreatureRelocator".ToTag();

		// Token: 0x04006AD1 RID: 27345
		public static Tag RanchStationType = "RanchStationType".ToTag();

		// Token: 0x04006AD2 RID: 27346
		public static Tag SpiceStation = "SpiceStation".ToTag();

		// Token: 0x04006AD3 RID: 27347
		public static Tag CookTop = "CookTop".ToTag();

		// Token: 0x04006AD4 RID: 27348
		public static Tag Refrigerator = "Refrigerator".ToTag();

		// Token: 0x04006AD5 RID: 27349
		public static Tag RecBuilding = "RecBuilding".ToTag();

		// Token: 0x04006AD6 RID: 27350
		public static Tag MachineShopType = "MachineShopType".ToTag();

		// Token: 0x04006AD7 RID: 27351
		public static Tag Park = "Park".ToTag();

		// Token: 0x04006AD8 RID: 27352
		public static Tag NatureReserve = "NatureReserve".ToTag();

		// Token: 0x04006AD9 RID: 27353
		public static Tag Decor20 = "Decor20".ToTag();

		// Token: 0x04006ADA RID: 27354
		public static Tag RocketInterior = "RocketInterior".ToTag();
	}

	// Token: 0x020016BF RID: 5823
	public class Constraint
	{
		// Token: 0x06008860 RID: 34912 RVA: 0x002F5597 File Offset: 0x002F3797
		public Constraint(Func<KPrefabID, bool> building_criteria, Func<Room, bool> room_criteria, int times_required = 1, string name = "", string description = "", List<RoomConstraints.Constraint> stomp_in_conflict = null)
		{
			this.room_criteria = room_criteria;
			this.building_criteria = building_criteria;
			this.times_required = times_required;
			this.name = name;
			this.description = description;
			this.stomp_in_conflict = stomp_in_conflict;
		}

		// Token: 0x06008861 RID: 34913 RVA: 0x002F55D4 File Offset: 0x002F37D4
		public bool isSatisfied(Room room)
		{
			int num = 0;
			if (this.room_criteria != null && !this.room_criteria(room))
			{
				return false;
			}
			if (this.building_criteria != null)
			{
				int num2 = 0;
				while (num < this.times_required && num2 < room.buildings.Count)
				{
					KPrefabID kprefabID = room.buildings[num2];
					if (!(kprefabID == null) && this.building_criteria(kprefabID))
					{
						num++;
					}
					num2++;
				}
				int num3 = 0;
				while (num < this.times_required && num3 < room.plants.Count)
				{
					KPrefabID kprefabID2 = room.plants[num3];
					if (!(kprefabID2 == null) && this.building_criteria(kprefabID2))
					{
						num++;
					}
					num3++;
				}
				return num >= this.times_required;
			}
			return true;
		}

		// Token: 0x04006ADB RID: 27355
		public string name;

		// Token: 0x04006ADC RID: 27356
		public string description;

		// Token: 0x04006ADD RID: 27357
		public int times_required = 1;

		// Token: 0x04006ADE RID: 27358
		public Func<Room, bool> room_criteria;

		// Token: 0x04006ADF RID: 27359
		public Func<KPrefabID, bool> building_criteria;

		// Token: 0x04006AE0 RID: 27360
		public List<RoomConstraints.Constraint> stomp_in_conflict;
	}
}
