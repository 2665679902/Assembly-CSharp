using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008E8 RID: 2280
public class RetiredColonyData
{
	// Token: 0x060041A5 RID: 16805 RVA: 0x0017080E File Offset: 0x0016EA0E
	public RetiredColonyData()
	{
	}

	// Token: 0x060041A6 RID: 16806 RVA: 0x00170818 File Offset: 0x0016EA18
	public RetiredColonyData(string colonyName, int cycleCount, string date, string[] achievements, MinionAssignablesProxy[] minions, BuildingComplete[] buildingCompletes, string startWorld, Dictionary<string, string> worldIdentities)
	{
		this.colonyName = colonyName;
		this.cycleCount = cycleCount;
		this.achievements = achievements;
		this.date = date;
		this.Duplicants = new RetiredColonyData.RetiredDuplicantData[(minions == null) ? 0 : minions.Length];
		int i = 0;
		while (i < this.Duplicants.Length)
		{
			this.Duplicants[i] = new RetiredColonyData.RetiredDuplicantData();
			this.Duplicants[i].name = minions[i].GetProperName();
			this.Duplicants[i].age = (int)Mathf.Floor((float)GameClock.Instance.GetCycle() - minions[i].GetArrivalTime());
			this.Duplicants[i].skillPointsGained = minions[i].GetTotalSkillpoints();
			this.Duplicants[i].accessories = new Dictionary<string, string>();
			if (minions[i].GetTargetGameObject().GetComponent<Accessorizer>() != null)
			{
				using (List<ResourceRef<Accessory>>.Enumerator enumerator = minions[i].GetTargetGameObject().GetComponent<Accessorizer>().GetAccessories()
					.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ResourceRef<Accessory> resourceRef = enumerator.Current;
						if (resourceRef.Get() != null)
						{
							this.Duplicants[i].accessories.Add(resourceRef.Get().slot.Id, resourceRef.Get().Id);
						}
					}
					goto IL_3AF;
				}
				goto IL_14E;
			}
			goto IL_14E;
			IL_3AF:
			i++;
			continue;
			IL_14E:
			StoredMinionIdentity component = minions[i].GetTargetGameObject().GetComponent<StoredMinionIdentity>();
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.Eyes.Id, Db.Get().Accessories.Get(component.bodyData.eyes).Id);
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.Arm.Id, Db.Get().Accessories.Get(component.bodyData.arms).Id);
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.ArmLower.Id, Db.Get().Accessories.Get(component.bodyData.armslower).Id);
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.Body.Id, Db.Get().Accessories.Get(component.bodyData.body).Id);
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.Hair.Id, Db.Get().Accessories.Get(component.bodyData.hair).Id);
			if (component.bodyData.hat != HashedString.Invalid)
			{
				this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.Hat.Id, Db.Get().Accessories.Get(component.bodyData.hat).Id);
			}
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.HeadShape.Id, Db.Get().Accessories.Get(component.bodyData.headShape).Id);
			this.Duplicants[i].accessories.Add(Db.Get().AccessorySlots.Mouth.Id, Db.Get().Accessories.Get(component.bodyData.mouth).Id);
			goto IL_3AF;
		}
		Dictionary<Tag, int> dictionary = new Dictionary<Tag, int>();
		if (buildingCompletes != null)
		{
			foreach (BuildingComplete buildingComplete in buildingCompletes)
			{
				if (!dictionary.ContainsKey(buildingComplete.PrefabID()))
				{
					dictionary[buildingComplete.PrefabID()] = 0;
				}
				Dictionary<Tag, int> dictionary2 = dictionary;
				Tag tag = buildingComplete.PrefabID();
				int num = dictionary2[tag];
				dictionary2[tag] = num + 1;
			}
		}
		this.buildings = new List<global::Tuple<string, int>>();
		foreach (KeyValuePair<Tag, int> keyValuePair in dictionary)
		{
			this.buildings.Add(new global::Tuple<string, int>(keyValuePair.Key.ToString(), keyValuePair.Value));
		}
		this.Stats = null;
		if (ReportManager.Instance != null)
		{
			global::Tuple<float, float>[] array = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[k].day, ReportManager.Instance.reports[k].GetEntry(ReportManager.ReportType.OxygenCreated).accPositive);
			}
			global::Tuple<float, float>[] array2 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int l = 0; l < array2.Length; l++)
			{
				array2[l] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[l].day, ReportManager.Instance.reports[l].GetEntry(ReportManager.ReportType.OxygenCreated).accNegative * -1f);
			}
			global::Tuple<float, float>[] array3 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int m = 0; m < array3.Length; m++)
			{
				array3[m] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[m].day, ReportManager.Instance.reports[m].GetEntry(ReportManager.ReportType.CaloriesCreated).accPositive * 0.001f);
			}
			global::Tuple<float, float>[] array4 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int n = 0; n < array4.Length; n++)
			{
				array4[n] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[n].day, ReportManager.Instance.reports[n].GetEntry(ReportManager.ReportType.CaloriesCreated).accNegative * 0.001f * -1f);
			}
			global::Tuple<float, float>[] array5 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num2 = 0; num2 < array5.Length; num2++)
			{
				array5[num2] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num2].day, ReportManager.Instance.reports[num2].GetEntry(ReportManager.ReportType.EnergyCreated).accPositive * 0.001f);
			}
			global::Tuple<float, float>[] array6 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num3 = 0; num3 < array6.Length; num3++)
			{
				array6[num3] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num3].day, ReportManager.Instance.reports[num3].GetEntry(ReportManager.ReportType.EnergyWasted).accNegative * -1f * 0.001f);
			}
			global::Tuple<float, float>[] array7 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num4 = 0; num4 < array7.Length; num4++)
			{
				array7[num4] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num4].day, ReportManager.Instance.reports[num4].GetEntry(ReportManager.ReportType.WorkTime).accPositive);
			}
			global::Tuple<float, float>[] array8 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num5 = 0; num5 < array7.Length; num5++)
			{
				int num6 = 0;
				float num7 = 0f;
				ReportManager.ReportEntry entry = ReportManager.Instance.reports[num5].GetEntry(ReportManager.ReportType.WorkTime);
				for (int num8 = 0; num8 < entry.contextEntries.Count; num8++)
				{
					num6++;
					num7 += entry.contextEntries[num8].accPositive;
				}
				num7 /= (float)num6;
				num7 /= 600f;
				num7 *= 100f;
				array8[num5] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num5].day, num7);
			}
			global::Tuple<float, float>[] array9 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num9 = 0; num9 < array9.Length; num9++)
			{
				array9[num9] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num9].day, ReportManager.Instance.reports[num9].GetEntry(ReportManager.ReportType.TravelTime).accPositive);
			}
			global::Tuple<float, float>[] array10 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num10 = 0; num10 < array9.Length; num10++)
			{
				int num11 = 0;
				float num12 = 0f;
				ReportManager.ReportEntry entry2 = ReportManager.Instance.reports[num10].GetEntry(ReportManager.ReportType.TravelTime);
				for (int num13 = 0; num13 < entry2.contextEntries.Count; num13++)
				{
					num11++;
					num12 += entry2.contextEntries[num13].accPositive;
				}
				num12 /= (float)num11;
				num12 /= 600f;
				num12 *= 100f;
				array10[num10] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num10].day, num12);
			}
			global::Tuple<float, float>[] array11 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num14 = 0; num14 < array7.Length; num14++)
			{
				array11[num14] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num14].day, (float)ReportManager.Instance.reports[num14].GetEntry(ReportManager.ReportType.WorkTime).contextEntries.Count);
			}
			global::Tuple<float, float>[] array12 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num15 = 0; num15 < array12.Length; num15++)
			{
				int num16 = 0;
				float num17 = 0f;
				ReportManager.ReportEntry entry3 = ReportManager.Instance.reports[num15].GetEntry(ReportManager.ReportType.StressDelta);
				for (int num18 = 0; num18 < entry3.contextEntries.Count; num18++)
				{
					num16++;
					num17 += entry3.contextEntries[num18].accPositive;
				}
				array12[num15] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num15].day, num17 / (float)num16);
			}
			global::Tuple<float, float>[] array13 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num19 = 0; num19 < array13.Length; num19++)
			{
				int num20 = 0;
				float num21 = 0f;
				ReportManager.ReportEntry entry4 = ReportManager.Instance.reports[num19].GetEntry(ReportManager.ReportType.StressDelta);
				for (int num22 = 0; num22 < entry4.contextEntries.Count; num22++)
				{
					num20++;
					num21 += entry4.contextEntries[num22].accNegative;
				}
				num21 *= -1f;
				array13[num19] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num19].day, num21 / (float)num20);
			}
			global::Tuple<float, float>[] array14 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num23 = 0; num23 < array14.Length; num23++)
			{
				array14[num23] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num23].day, ReportManager.Instance.reports[num23].GetEntry(ReportManager.ReportType.DomesticatedCritters).accPositive);
			}
			global::Tuple<float, float>[] array15 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num24 = 0; num24 < array15.Length; num24++)
			{
				array15[num24] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num24].day, ReportManager.Instance.reports[num24].GetEntry(ReportManager.ReportType.WildCritters).accPositive);
			}
			global::Tuple<float, float>[] array16 = new global::Tuple<float, float>[ReportManager.Instance.reports.Count];
			for (int num25 = 0; num25 < array16.Length; num25++)
			{
				array16[num25] = new global::Tuple<float, float>((float)ReportManager.Instance.reports[num25].day, ReportManager.Instance.reports[num25].GetEntry(ReportManager.ReportType.RocketsInFlight).accPositive);
			}
			this.Stats = new RetiredColonyData.RetiredColonyStatistic[]
			{
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.OxygenProduced, array, UI.RETIRED_COLONY_INFO_SCREEN.STATS.OXYGEN_CREATED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.MASS.KILOGRAM),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.OxygenConsumed, array2, UI.RETIRED_COLONY_INFO_SCREEN.STATS.OXYGEN_CONSUMED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.MASS.KILOGRAM),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.CaloriesProduced, array3, UI.RETIRED_COLONY_INFO_SCREEN.STATS.CALORIES_CREATED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.CALORIES.KILOCALORIE),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.CaloriesRemoved, array4, UI.RETIRED_COLONY_INFO_SCREEN.STATS.CALORIES_CONSUMED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.CALORIES.KILOCALORIE),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.PowerProduced, array5, UI.RETIRED_COLONY_INFO_SCREEN.STATS.POWER_CREATED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.ELECTRICAL.KILOJOULE),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.PowerWasted, array6, UI.RETIRED_COLONY_INFO_SCREEN.STATS.POWER_WASTED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.ELECTRICAL.KILOJOULE),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.WorkTime, array7, UI.RETIRED_COLONY_INFO_SCREEN.STATS.WORK_TIME, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.SECONDS),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.AverageWorkTime, array8, UI.RETIRED_COLONY_INFO_SCREEN.STATS.AVERAGE_WORK_TIME, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.PERCENT),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.TravelTime, array9, UI.RETIRED_COLONY_INFO_SCREEN.STATS.TRAVEL_TIME, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.SECONDS),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.AverageTravelTime, array10, UI.RETIRED_COLONY_INFO_SCREEN.STATS.AVERAGE_TRAVEL_TIME, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.PERCENT),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.LiveDuplicants, array11, UI.RETIRED_COLONY_INFO_SCREEN.STATS.LIVE_DUPLICANTS, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.DUPLICANTS),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.RocketsInFlight, array16, UI.RETIRED_COLONY_INFO_SCREEN.STATS.ROCKET_MISSIONS, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.ROCKET_MISSIONS),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.AverageStressCreated, array12, UI.RETIRED_COLONY_INFO_SCREEN.STATS.AVERAGE_STRESS_CREATED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.PERCENT),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.AverageStressRemoved, array13, UI.RETIRED_COLONY_INFO_SCREEN.STATS.AVERAGE_STRESS_REMOVED, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.PERCENT),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.DomesticatedCritters, array14, UI.RETIRED_COLONY_INFO_SCREEN.STATS.NUMBER_DOMESTICATED_CRITTERS, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.CRITTERS),
				new RetiredColonyData.RetiredColonyStatistic(RetiredColonyData.DataIDs.WildCritters, array15, UI.RETIRED_COLONY_INFO_SCREEN.STATS.NUMBER_WILD_CRITTERS, UI.MATH_PICTURES.AXIS_LABELS.CYCLES, UI.UNITSUFFIXES.CRITTERS)
			};
			this.startWorld = startWorld;
			this.worldIdentities = worldIdentities;
		}
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x060041A7 RID: 16807 RVA: 0x00171800 File Offset: 0x0016FA00
	// (set) Token: 0x060041A8 RID: 16808 RVA: 0x00171808 File Offset: 0x0016FA08
	public string colonyName { get; set; }

	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x060041A9 RID: 16809 RVA: 0x00171811 File Offset: 0x0016FA11
	// (set) Token: 0x060041AA RID: 16810 RVA: 0x00171819 File Offset: 0x0016FA19
	public int cycleCount { get; set; }

	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x060041AB RID: 16811 RVA: 0x00171822 File Offset: 0x0016FA22
	// (set) Token: 0x060041AC RID: 16812 RVA: 0x0017182A File Offset: 0x0016FA2A
	public string date { get; set; }

	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x060041AD RID: 16813 RVA: 0x00171833 File Offset: 0x0016FA33
	// (set) Token: 0x060041AE RID: 16814 RVA: 0x0017183B File Offset: 0x0016FA3B
	public string[] achievements { get; set; }

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x060041AF RID: 16815 RVA: 0x00171844 File Offset: 0x0016FA44
	// (set) Token: 0x060041B0 RID: 16816 RVA: 0x0017184C File Offset: 0x0016FA4C
	public RetiredColonyData.RetiredDuplicantData[] Duplicants { get; set; }

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x060041B1 RID: 16817 RVA: 0x00171855 File Offset: 0x0016FA55
	// (set) Token: 0x060041B2 RID: 16818 RVA: 0x0017185D File Offset: 0x0016FA5D
	public List<global::Tuple<string, int>> buildings { get; set; }

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x060041B3 RID: 16819 RVA: 0x00171866 File Offset: 0x0016FA66
	// (set) Token: 0x060041B4 RID: 16820 RVA: 0x0017186E File Offset: 0x0016FA6E
	public RetiredColonyData.RetiredColonyStatistic[] Stats { get; set; }

	// Token: 0x170004A7 RID: 1191
	// (get) Token: 0x060041B5 RID: 16821 RVA: 0x00171877 File Offset: 0x0016FA77
	// (set) Token: 0x060041B6 RID: 16822 RVA: 0x0017187F File Offset: 0x0016FA7F
	public Dictionary<string, string> worldIdentities { get; set; }

	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x060041B7 RID: 16823 RVA: 0x00171888 File Offset: 0x0016FA88
	// (set) Token: 0x060041B8 RID: 16824 RVA: 0x00171890 File Offset: 0x0016FA90
	public string startWorld { get; set; }

	// Token: 0x020016A4 RID: 5796
	public static class DataIDs
	{
		// Token: 0x04006A70 RID: 27248
		public static string OxygenProduced = "oxygenProduced";

		// Token: 0x04006A71 RID: 27249
		public static string OxygenConsumed = "oxygenConsumed";

		// Token: 0x04006A72 RID: 27250
		public static string CaloriesProduced = "caloriesProduced";

		// Token: 0x04006A73 RID: 27251
		public static string CaloriesRemoved = "caloriesRemoved";

		// Token: 0x04006A74 RID: 27252
		public static string PowerProduced = "powerProduced";

		// Token: 0x04006A75 RID: 27253
		public static string PowerWasted = "powerWasted";

		// Token: 0x04006A76 RID: 27254
		public static string WorkTime = "workTime";

		// Token: 0x04006A77 RID: 27255
		public static string TravelTime = "travelTime";

		// Token: 0x04006A78 RID: 27256
		public static string AverageWorkTime = "averageWorkTime";

		// Token: 0x04006A79 RID: 27257
		public static string AverageTravelTime = "averageTravelTime";

		// Token: 0x04006A7A RID: 27258
		public static string LiveDuplicants = "liveDuplicants";

		// Token: 0x04006A7B RID: 27259
		public static string AverageStressCreated = "averageStressCreated";

		// Token: 0x04006A7C RID: 27260
		public static string AverageStressRemoved = "averageStressRemoved";

		// Token: 0x04006A7D RID: 27261
		public static string DomesticatedCritters = "domesticatedCritters";

		// Token: 0x04006A7E RID: 27262
		public static string WildCritters = "wildCritters";

		// Token: 0x04006A7F RID: 27263
		public static string AverageGerms = "averageGerms";

		// Token: 0x04006A80 RID: 27264
		public static string RocketsInFlight = "rocketsInFlight";
	}

	// Token: 0x020016A5 RID: 5797
	public class RetiredColonyStatistic
	{
		// Token: 0x06008821 RID: 34849 RVA: 0x002F4C2F File Offset: 0x002F2E2F
		public RetiredColonyStatistic()
		{
		}

		// Token: 0x06008822 RID: 34850 RVA: 0x002F4C37 File Offset: 0x002F2E37
		public RetiredColonyStatistic(string id, global::Tuple<float, float>[] data, string name, string axisNameX, string axisNameY)
		{
			this.id = id;
			this.value = data;
			this.name = name;
			this.nameX = axisNameX;
			this.nameY = axisNameY;
		}

		// Token: 0x06008823 RID: 34851 RVA: 0x002F4C64 File Offset: 0x002F2E64
		public global::Tuple<float, float> GetByMaxValue()
		{
			if (this.value.Length == 0)
			{
				return new global::Tuple<float, float>(0f, 0f);
			}
			int num = -1;
			float num2 = -1f;
			for (int i = 0; i < this.value.Length; i++)
			{
				if (this.value[i].second > num2)
				{
					num2 = this.value[i].second;
					num = i;
				}
			}
			if (num == -1)
			{
				num = 0;
			}
			return this.value[num];
		}

		// Token: 0x06008824 RID: 34852 RVA: 0x002F4CD4 File Offset: 0x002F2ED4
		public global::Tuple<float, float> GetByMaxKey()
		{
			if (this.value.Length == 0)
			{
				return new global::Tuple<float, float>(0f, 0f);
			}
			int num = -1;
			float num2 = -1f;
			for (int i = 0; i < this.value.Length; i++)
			{
				if (this.value[i].first > num2)
				{
					num2 = this.value[i].first;
					num = i;
				}
			}
			return this.value[num];
		}

		// Token: 0x04006A81 RID: 27265
		public string id;

		// Token: 0x04006A82 RID: 27266
		public global::Tuple<float, float>[] value;

		// Token: 0x04006A83 RID: 27267
		public string name;

		// Token: 0x04006A84 RID: 27268
		public string nameX;

		// Token: 0x04006A85 RID: 27269
		public string nameY;
	}

	// Token: 0x020016A6 RID: 5798
	public class RetiredDuplicantData
	{
		// Token: 0x04006A86 RID: 27270
		public string name;

		// Token: 0x04006A87 RID: 27271
		public int age;

		// Token: 0x04006A88 RID: 27272
		public int skillPointsGained;

		// Token: 0x04006A89 RID: 27273
		public Dictionary<string, string> accessories;
	}
}
