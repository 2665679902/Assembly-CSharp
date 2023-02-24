using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Delaunay.Geo;
using Klei;
using KSerialization;
using ProcGen;
using ProcGenGame;
using TemplateClasses;
using TUNING;
using UnityEngine;

// Token: 0x020009D5 RID: 2517
[SerializationConfig(MemberSerialization.OptIn)]
public class WorldContainer : KMonoBehaviour
{
	// Token: 0x17000588 RID: 1416
	// (get) Token: 0x06004ADB RID: 19163 RVA: 0x001A2AE9 File Offset: 0x001A0CE9
	// (set) Token: 0x06004ADC RID: 19164 RVA: 0x001A2AF1 File Offset: 0x001A0CF1
	[Serialize]
	public WorldInventory worldInventory { get; private set; }

	// Token: 0x17000589 RID: 1417
	// (get) Token: 0x06004ADD RID: 19165 RVA: 0x001A2AFA File Offset: 0x001A0CFA
	// (set) Token: 0x06004ADE RID: 19166 RVA: 0x001A2B02 File Offset: 0x001A0D02
	public Dictionary<Tag, float> materialNeeds { get; private set; }

	// Token: 0x1700058A RID: 1418
	// (get) Token: 0x06004ADF RID: 19167 RVA: 0x001A2B0B File Offset: 0x001A0D0B
	public bool IsModuleInterior
	{
		get
		{
			return this.isModuleInterior;
		}
	}

	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x001A2B13 File Offset: 0x001A0D13
	public bool IsDiscovered
	{
		get
		{
			return this.isDiscovered || DebugHandler.RevealFogOfWar;
		}
	}

	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x06004AE1 RID: 19169 RVA: 0x001A2B24 File Offset: 0x001A0D24
	public bool IsStartWorld
	{
		get
		{
			return this.isStartWorld;
		}
	}

	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x001A2B2C File Offset: 0x001A0D2C
	public bool IsDupeVisited
	{
		get
		{
			return this.isDupeVisited;
		}
	}

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x001A2B34 File Offset: 0x001A0D34
	public float DupeVisitedTimestamp
	{
		get
		{
			return this.dupeVisitedTimestamp;
		}
	}

	// Token: 0x1700058F RID: 1423
	// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x001A2B3C File Offset: 0x001A0D3C
	public float DiscoveryTimestamp
	{
		get
		{
			return this.discoveryTimestamp;
		}
	}

	// Token: 0x17000590 RID: 1424
	// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x001A2B44 File Offset: 0x001A0D44
	public bool IsRoverVisted
	{
		get
		{
			return this.isRoverVisited;
		}
	}

	// Token: 0x17000591 RID: 1425
	// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x001A2B4C File Offset: 0x001A0D4C
	public bool IsSurfaceRevealed
	{
		get
		{
			return this.isSurfaceRevealed;
		}
	}

	// Token: 0x17000592 RID: 1426
	// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x001A2B54 File Offset: 0x001A0D54
	public Dictionary<string, int> SunlightFixedTraits
	{
		get
		{
			return this.sunlightFixedTraits;
		}
	}

	// Token: 0x17000593 RID: 1427
	// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x001A2B5C File Offset: 0x001A0D5C
	public Dictionary<string, int> CosmicRadiationFixedTraits
	{
		get
		{
			return this.cosmicRadiationFixedTraits;
		}
	}

	// Token: 0x17000594 RID: 1428
	// (get) Token: 0x06004AE9 RID: 19177 RVA: 0x001A2B64 File Offset: 0x001A0D64
	public List<string> Biomes
	{
		get
		{
			return this.m_subworldNames;
		}
	}

	// Token: 0x17000595 RID: 1429
	// (get) Token: 0x06004AEA RID: 19178 RVA: 0x001A2B6C File Offset: 0x001A0D6C
	public List<string> WorldTraitIds
	{
		get
		{
			return this.m_worldTraitIds;
		}
	}

	// Token: 0x17000596 RID: 1430
	// (get) Token: 0x06004AEB RID: 19179 RVA: 0x001A2B74 File Offset: 0x001A0D74
	public List<string> StoryTraitIds
	{
		get
		{
			return this.m_storyTraitIds;
		}
	}

	// Token: 0x17000597 RID: 1431
	// (get) Token: 0x06004AEC RID: 19180 RVA: 0x001A2B7C File Offset: 0x001A0D7C
	public AlertStateManager.Instance AlertManager
	{
		get
		{
			if (this.m_alertManager == null)
			{
				StateMachineController component = base.GetComponent<StateMachineController>();
				this.m_alertManager = component.GetSMI<AlertStateManager.Instance>();
			}
			global::Debug.Assert(this.m_alertManager != null, "AlertStateManager should never be null.");
			return this.m_alertManager;
		}
	}

	// Token: 0x06004AED RID: 19181 RVA: 0x001A2BBD File Offset: 0x001A0DBD
	public void AddTopPriorityPrioritizable(Prioritizable prioritizable)
	{
		if (!this.yellowAlertTasks.Contains(prioritizable))
		{
			this.yellowAlertTasks.Add(prioritizable);
		}
		this.RefreshHasTopPriorityChore();
	}

	// Token: 0x06004AEE RID: 19182 RVA: 0x001A2BE0 File Offset: 0x001A0DE0
	public void RemoveTopPriorityPrioritizable(Prioritizable prioritizable)
	{
		for (int i = this.yellowAlertTasks.Count - 1; i >= 0; i--)
		{
			if (this.yellowAlertTasks[i] == prioritizable || this.yellowAlertTasks[i].Equals(null))
			{
				this.yellowAlertTasks.RemoveAt(i);
			}
		}
		this.RefreshHasTopPriorityChore();
	}

	// Token: 0x17000598 RID: 1432
	// (get) Token: 0x06004AEF RID: 19183 RVA: 0x001A2C3F File Offset: 0x001A0E3F
	// (set) Token: 0x06004AF0 RID: 19184 RVA: 0x001A2C47 File Offset: 0x001A0E47
	public int ParentWorldId { get; private set; }

	// Token: 0x06004AF1 RID: 19185 RVA: 0x001A2C50 File Offset: 0x001A0E50
	public Quadrant[] GetQuadrantOfCell(int cell, int depth = 1)
	{
		Vector2 vector = new Vector2((float)this.WorldSize.x * Grid.CellSizeInMeters, (float)this.worldSize.y * Grid.CellSizeInMeters);
		Vector2 vector2 = Grid.CellToPos2D(Grid.XYToCell(this.WorldOffset.x, this.WorldOffset.y));
		Vector2 vector3 = Grid.CellToPos2D(cell);
		Quadrant[] array = new Quadrant[depth];
		Vector2 vector4 = new Vector2(vector2.x, (float)this.worldOffset.y + vector.y);
		Vector2 vector5 = new Vector2(vector2.x + vector.x, (float)this.worldOffset.y);
		for (int i = 0; i < depth; i++)
		{
			float num = vector5.x - vector4.x;
			float num2 = vector4.y - vector5.y;
			float num3 = num * 0.5f;
			float num4 = num2 * 0.5f;
			if (vector3.x >= vector4.x + num3 && vector3.y >= vector5.y + num4)
			{
				array[i] = Quadrant.NE;
			}
			if (vector3.x >= vector4.x + num3 && vector3.y < vector5.y + num4)
			{
				array[i] = Quadrant.SE;
			}
			if (vector3.x < vector4.x + num3 && vector3.y < vector5.y + num4)
			{
				array[i] = Quadrant.SW;
			}
			if (vector3.x < vector4.x + num3 && vector3.y >= vector5.y + num4)
			{
				array[i] = Quadrant.NW;
			}
			switch (array[i])
			{
			case Quadrant.NE:
				vector4.x += num3;
				vector5.y += num4;
				break;
			case Quadrant.NW:
				vector5.x -= num3;
				vector5.y += num4;
				break;
			case Quadrant.SW:
				vector4.y -= num4;
				vector5.x -= num3;
				break;
			case Quadrant.SE:
				vector4.x += num3;
				vector4.y -= num4;
				break;
			}
		}
		return array;
	}

	// Token: 0x06004AF2 RID: 19186 RVA: 0x001A2E7D File Offset: 0x001A107D
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.ParentWorldId = this.id;
	}

	// Token: 0x06004AF3 RID: 19187 RVA: 0x001A2E8B File Offset: 0x001A108B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.worldInventory = base.GetComponent<WorldInventory>();
		this.materialNeeds = new Dictionary<Tag, float>();
		ClusterManager.Instance.RegisterWorldContainer(this);
	}

	// Token: 0x06004AF4 RID: 19188 RVA: 0x001A2EB8 File Offset: 0x001A10B8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.gameObject.AddOrGet<InfoDescription>().DescriptionLocString = this.worldDescription;
		this.RefreshHasTopPriorityChore();
		this.UpgradeFixedTraits();
		this.RefreshFixedTraits();
		if (DlcManager.IsPureVanilla())
		{
			this.isStartWorld = true;
			this.isDupeVisited = true;
		}
	}

	// Token: 0x06004AF5 RID: 19189 RVA: 0x001A2F08 File Offset: 0x001A1108
	protected override void OnCleanUp()
	{
		SaveGame.Instance.materialSelectorSerializer.WipeWorldSelectionData(this.id);
		base.OnCleanUp();
	}

	// Token: 0x06004AF6 RID: 19190 RVA: 0x001A2F28 File Offset: 0x001A1128
	private void UpgradeFixedTraits()
	{
		if (this.sunlightFixedTrait == null || this.sunlightFixedTrait == "")
		{
			new Dictionary<int, string>
			{
				{
					160000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_HIGH
				},
				{
					0,
					FIXEDTRAITS.SUNLIGHT.NAME.NONE
				},
				{
					10000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_LOW
				},
				{
					20000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_LOW
				},
				{
					30000,
					FIXEDTRAITS.SUNLIGHT.NAME.LOW
				},
				{
					35000,
					FIXEDTRAITS.SUNLIGHT.NAME.MED_LOW
				},
				{
					40000,
					FIXEDTRAITS.SUNLIGHT.NAME.MED
				},
				{
					50000,
					FIXEDTRAITS.SUNLIGHT.NAME.MED_HIGH
				},
				{
					60000,
					FIXEDTRAITS.SUNLIGHT.NAME.HIGH
				},
				{
					80000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_HIGH
				},
				{
					120000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_HIGH
				}
			}.TryGetValue(this.sunlight, out this.sunlightFixedTrait);
		}
		if (this.cosmicRadiationFixedTrait == null || this.cosmicRadiationFixedTrait == "")
		{
			new Dictionary<int, string>
			{
				{
					0,
					FIXEDTRAITS.COSMICRADIATION.NAME.NONE
				},
				{
					6,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_LOW
				},
				{
					12,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_LOW
				},
				{
					18,
					FIXEDTRAITS.COSMICRADIATION.NAME.LOW
				},
				{
					21,
					FIXEDTRAITS.COSMICRADIATION.NAME.MED_LOW
				},
				{
					25,
					FIXEDTRAITS.COSMICRADIATION.NAME.MED
				},
				{
					31,
					FIXEDTRAITS.COSMICRADIATION.NAME.MED_HIGH
				},
				{
					37,
					FIXEDTRAITS.COSMICRADIATION.NAME.HIGH
				},
				{
					50,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_HIGH
				},
				{
					75,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_HIGH
				}
			}.TryGetValue(this.cosmicRadiation, out this.cosmicRadiationFixedTrait);
		}
	}

	// Token: 0x06004AF7 RID: 19191 RVA: 0x001A30C9 File Offset: 0x001A12C9
	private void RefreshFixedTraits()
	{
		this.sunlight = this.GetSunlightValueFromFixedTrait();
		this.cosmicRadiation = this.GetCosmicRadiationValueFromFixedTrait();
	}

	// Token: 0x06004AF8 RID: 19192 RVA: 0x001A30E3 File Offset: 0x001A12E3
	private void RefreshHasTopPriorityChore()
	{
		if (this.AlertManager != null)
		{
			this.AlertManager.SetHasTopPriorityChore(this.yellowAlertTasks.Count > 0);
		}
	}

	// Token: 0x06004AF9 RID: 19193 RVA: 0x001A3106 File Offset: 0x001A1306
	public List<string> GetSeasonIds()
	{
		return this.m_seasonIds;
	}

	// Token: 0x06004AFA RID: 19194 RVA: 0x001A310E File Offset: 0x001A130E
	public bool IsRedAlert()
	{
		return this.m_alertManager.IsRedAlert();
	}

	// Token: 0x06004AFB RID: 19195 RVA: 0x001A311B File Offset: 0x001A131B
	public bool IsYellowAlert()
	{
		return this.m_alertManager.IsYellowAlert();
	}

	// Token: 0x06004AFC RID: 19196 RVA: 0x001A3128 File Offset: 0x001A1328
	public string GetRandomName()
	{
		return GameUtil.GenerateRandomWorldName(this.nameTables);
	}

	// Token: 0x06004AFD RID: 19197 RVA: 0x001A3135 File Offset: 0x001A1335
	public void SetID(int id)
	{
		this.id = id;
		this.ParentWorldId = id;
	}

	// Token: 0x06004AFE RID: 19198 RVA: 0x001A3148 File Offset: 0x001A1348
	public void SetParentIdx(int parentIdx)
	{
		this.parentChangeArgs.lastParentId = this.ParentWorldId;
		this.parentChangeArgs.world = this;
		this.ParentWorldId = parentIdx;
		Game.Instance.Trigger(880851192, this.parentChangeArgs);
		this.parentChangeArgs.lastParentId = (int)ClusterManager.INVALID_WORLD_IDX;
	}

	// Token: 0x17000599 RID: 1433
	// (get) Token: 0x06004AFF RID: 19199 RVA: 0x001A319E File Offset: 0x001A139E
	public Vector2 minimumBounds
	{
		get
		{
			return new Vector2((float)this.worldOffset.x, (float)this.worldOffset.y);
		}
	}

	// Token: 0x1700059A RID: 1434
	// (get) Token: 0x06004B00 RID: 19200 RVA: 0x001A31BD File Offset: 0x001A13BD
	public Vector2 maximumBounds
	{
		get
		{
			return new Vector2((float)(this.worldOffset.x + (this.worldSize.x - 1)), (float)(this.worldOffset.y + (this.worldSize.y - 1)));
		}
	}

	// Token: 0x1700059B RID: 1435
	// (get) Token: 0x06004B01 RID: 19201 RVA: 0x001A31F8 File Offset: 0x001A13F8
	public Vector2I WorldSize
	{
		get
		{
			return this.worldSize;
		}
	}

	// Token: 0x1700059C RID: 1436
	// (get) Token: 0x06004B02 RID: 19202 RVA: 0x001A3200 File Offset: 0x001A1400
	public Vector2I WorldOffset
	{
		get
		{
			return this.worldOffset;
		}
	}

	// Token: 0x1700059D RID: 1437
	// (get) Token: 0x06004B03 RID: 19203 RVA: 0x001A3208 File Offset: 0x001A1408
	public bool FullyEnclosedBorder
	{
		get
		{
			return this.fullyEnclosedBorder;
		}
	}

	// Token: 0x1700059E RID: 1438
	// (get) Token: 0x06004B04 RID: 19204 RVA: 0x001A3210 File Offset: 0x001A1410
	public int Height
	{
		get
		{
			return this.worldSize.y;
		}
	}

	// Token: 0x1700059F RID: 1439
	// (get) Token: 0x06004B05 RID: 19205 RVA: 0x001A321D File Offset: 0x001A141D
	public int Width
	{
		get
		{
			return this.worldSize.x;
		}
	}

	// Token: 0x06004B06 RID: 19206 RVA: 0x001A322A File Offset: 0x001A142A
	public void SetDiscovered(bool reveal_surface = false)
	{
		if (!this.isDiscovered)
		{
			this.discoveryTimestamp = GameUtil.GetCurrentTimeInCycles();
		}
		this.isDiscovered = true;
		if (reveal_surface)
		{
			this.LookAtSurface();
		}
		Game.Instance.Trigger(-521212405, this);
	}

	// Token: 0x06004B07 RID: 19207 RVA: 0x001A325F File Offset: 0x001A145F
	public void SetDupeVisited()
	{
		if (!this.isDupeVisited)
		{
			this.dupeVisitedTimestamp = GameUtil.GetCurrentTimeInCycles();
			this.isDupeVisited = true;
			Game.Instance.Trigger(-434755240, this);
		}
	}

	// Token: 0x06004B08 RID: 19208 RVA: 0x001A328B File Offset: 0x001A148B
	public void SetRoverLanded()
	{
		this.isRoverVisited = true;
	}

	// Token: 0x06004B09 RID: 19209 RVA: 0x001A3294 File Offset: 0x001A1494
	public void SetRocketInteriorWorldDetails(int world_id, Vector2I size, Vector2I offset)
	{
		this.SetID(world_id);
		this.fullyEnclosedBorder = true;
		this.worldOffset = offset;
		this.worldSize = size;
		this.isDiscovered = true;
		this.isModuleInterior = true;
		this.m_seasonIds = new List<string>();
	}

	// Token: 0x06004B0A RID: 19210 RVA: 0x001A32CC File Offset: 0x001A14CC
	private static int IsClockwise(Vector2 first, Vector2 second, Vector2 origin)
	{
		if (first == second)
		{
			return 0;
		}
		Vector2 vector = first - origin;
		Vector2 vector2 = second - origin;
		float num = Mathf.Atan2(vector.x, vector.y);
		float num2 = Mathf.Atan2(vector2.x, vector2.y);
		if (num < num2)
		{
			return 1;
		}
		if (num > num2)
		{
			return -1;
		}
		if (vector.sqrMagnitude >= vector2.sqrMagnitude)
		{
			return -1;
		}
		return 1;
	}

	// Token: 0x06004B0B RID: 19211 RVA: 0x001A3338 File Offset: 0x001A1538
	public void PlaceInteriorTemplate(string template_name, System.Action callback)
	{
		TemplateContainer template = TemplateCache.GetTemplate(template_name);
		Vector2 pos = new Vector2((float)(this.worldSize.x / 2 + this.worldOffset.x), (float)(this.worldSize.y / 2 + this.worldOffset.y));
		TemplateLoader.Stamp(template, pos, callback);
		float num = template.info.size.X / 2f;
		float num2 = template.info.size.Y / 2f;
		float num3 = Math.Max(num, num2);
		GridVisibility.Reveal((int)pos.x, (int)pos.y, (int)num3 + 3 + 5, num3 + 3f);
		WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
		this.overworldCell = new WorldDetailSave.OverworldCell();
		List<Vector2> list = new List<Vector2>(template.cells.Count);
		foreach (Prefab prefab in template.buildings)
		{
			if (prefab.id == "RocketWallTile")
			{
				Vector2 vector = new Vector2((float)prefab.location_x + pos.x, (float)prefab.location_y + pos.y);
				if (vector.x > pos.x)
				{
					vector.x += 0.5f;
				}
				if (vector.y > pos.y)
				{
					vector.y += 0.5f;
				}
				list.Add(vector);
			}
		}
		list.Sort((Vector2 v1, Vector2 v2) => WorldContainer.IsClockwise(v1, v2, pos));
		this.overworldCell.poly = new Polygon(list);
		this.overworldCell.zoneType = SubWorld.ZoneType.RocketInterior;
		this.overworldCell.tags = new TagSet { WorldGenTags.RocketInterior };
		clusterDetailSave.overworldCells.Add(this.overworldCell);
		Rect rect = new Rect(pos.x - num + 1f, pos.y - num2 + 1f, template.info.size.X, template.info.size.Y);
		int num4 = (int)rect.yMin;
		while ((float)num4 < rect.yMax)
		{
			int num5 = (int)rect.xMin;
			while ((float)num5 < rect.xMax)
			{
				SimMessages.ModifyCellWorldZone(Grid.XYToCell(num5, num4), 0);
				num5++;
			}
			num4++;
		}
	}

	// Token: 0x06004B0C RID: 19212 RVA: 0x001A3610 File Offset: 0x001A1810
	private string GetSunlightFromFixedTraits(WorldGen world)
	{
		foreach (string text in world.Settings.world.fixedTraits)
		{
			if (this.sunlightFixedTraits.ContainsKey(text))
			{
				return text;
			}
		}
		return FIXEDTRAITS.SUNLIGHT.NAME.DEFAULT;
	}

	// Token: 0x06004B0D RID: 19213 RVA: 0x001A3680 File Offset: 0x001A1880
	private string GetCosmicRadiationFromFixedTraits(WorldGen world)
	{
		foreach (string text in world.Settings.world.fixedTraits)
		{
			if (this.cosmicRadiationFixedTraits.ContainsKey(text))
			{
				return text;
			}
		}
		return FIXEDTRAITS.COSMICRADIATION.NAME.DEFAULT;
	}

	// Token: 0x06004B0E RID: 19214 RVA: 0x001A36F0 File Offset: 0x001A18F0
	private int GetSunlightValueFromFixedTrait()
	{
		if (this.sunlightFixedTrait == null)
		{
			this.sunlightFixedTrait = FIXEDTRAITS.SUNLIGHT.NAME.DEFAULT;
		}
		if (this.sunlightFixedTraits.ContainsKey(this.sunlightFixedTrait))
		{
			return this.sunlightFixedTraits[this.sunlightFixedTrait];
		}
		return FIXEDTRAITS.SUNLIGHT.DEFAULT_VALUE;
	}

	// Token: 0x06004B0F RID: 19215 RVA: 0x001A372F File Offset: 0x001A192F
	private int GetCosmicRadiationValueFromFixedTrait()
	{
		if (this.cosmicRadiationFixedTrait == null)
		{
			this.cosmicRadiationFixedTrait = FIXEDTRAITS.COSMICRADIATION.NAME.DEFAULT;
		}
		if (this.cosmicRadiationFixedTraits.ContainsKey(this.cosmicRadiationFixedTrait))
		{
			return this.cosmicRadiationFixedTraits[this.cosmicRadiationFixedTrait];
		}
		return FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;
	}

	// Token: 0x06004B10 RID: 19216 RVA: 0x001A3770 File Offset: 0x001A1970
	public void SetWorldDetails(WorldGen world)
	{
		if (world != null)
		{
			this.fullyEnclosedBorder = world.Settings.GetBoolSetting("DrawWorldBorder") && world.Settings.GetBoolSetting("DrawWorldBorderOverVacuum");
			this.worldOffset = world.GetPosition();
			this.worldSize = world.GetSize();
			this.isDiscovered = world.isStartingWorld;
			this.isStartWorld = world.isStartingWorld;
			this.worldName = world.Settings.world.filePath;
			this.nameTables = world.Settings.world.nameTables;
			this.worldDescription = world.Settings.world.description;
			this.worldType = world.Settings.world.name;
			this.isModuleInterior = world.Settings.world.moduleInterior;
			this.m_seasonIds = new List<string>(world.Settings.world.seasons);
			this.sunlightFixedTrait = this.GetSunlightFromFixedTraits(world);
			this.cosmicRadiationFixedTrait = this.GetCosmicRadiationFromFixedTraits(world);
			this.sunlight = this.GetSunlightValueFromFixedTrait();
			this.cosmicRadiation = this.GetCosmicRadiationValueFromFixedTrait();
			this.currentCosmicIntensity = (float)this.cosmicRadiation;
			this.m_subworldNames = new List<string>();
			foreach (WeightedSubworldName weightedSubworldName in world.Settings.world.subworldFiles)
			{
				string text = weightedSubworldName.name;
				text = text.Substring(0, text.LastIndexOf('/'));
				text = text.Substring(text.LastIndexOf('/') + 1, text.Length - (text.LastIndexOf('/') + 1));
				this.m_subworldNames.Add(text);
			}
			this.m_worldTraitIds = new List<string>();
			this.m_worldTraitIds.AddRange(world.Settings.GetWorldTraitIDs());
			this.m_storyTraitIds = new List<string>();
			this.m_storyTraitIds.AddRange(world.Settings.GetStoryTraitIDs());
			return;
		}
		this.fullyEnclosedBorder = false;
		this.worldOffset = Vector2I.zero;
		this.worldSize = new Vector2I(Grid.WidthInCells, Grid.HeightInCells);
		this.isDiscovered = true;
		this.isStartWorld = true;
		this.isDupeVisited = true;
		this.m_seasonIds = new List<string> { Db.Get().GameplaySeasons.MeteorShowers.Id };
	}

	// Token: 0x06004B11 RID: 19217 RVA: 0x001A39E4 File Offset: 0x001A1BE4
	public bool ContainsPoint(Vector2 point)
	{
		return point.x >= (float)this.worldOffset.x && point.y >= (float)this.worldOffset.y && point.x < (float)(this.worldOffset.x + this.worldSize.x) && point.y < (float)(this.worldOffset.y + this.worldSize.y);
	}

	// Token: 0x06004B12 RID: 19218 RVA: 0x001A3A5C File Offset: 0x001A1C5C
	public void LookAtSurface()
	{
		if (!this.IsDupeVisited)
		{
			this.RevealSurface();
		}
		Vector3? vector = this.SetSurfaceCameraPos();
		if (ClusterManager.Instance.activeWorldId == this.id && vector != null)
		{
			CameraController.Instance.SnapTo(vector.Value);
		}
	}

	// Token: 0x06004B13 RID: 19219 RVA: 0x001A3AAC File Offset: 0x001A1CAC
	public void RevealSurface()
	{
		if (this.isSurfaceRevealed)
		{
			return;
		}
		this.isSurfaceRevealed = true;
		for (int i = 0; i < this.worldSize.x; i++)
		{
			for (int j = this.worldSize.y - 1; j >= 0; j--)
			{
				int num = Grid.XYToCell(i + this.worldOffset.x, j + this.worldOffset.y);
				if (!Grid.IsValidCell(num) || Grid.IsSolidCell(num) || Grid.IsLiquid(num))
				{
					break;
				}
				GridVisibility.Reveal(i + this.worldOffset.X, j + this.worldOffset.y, 7, 1f);
			}
		}
	}

	// Token: 0x06004B14 RID: 19220 RVA: 0x001A3B58 File Offset: 0x001A1D58
	private Vector3? SetSurfaceCameraPos()
	{
		if (SaveGame.Instance != null)
		{
			int num = (int)this.maximumBounds.y;
			for (int i = 0; i < this.worldSize.X; i++)
			{
				for (int j = this.worldSize.y - 1; j >= 0; j--)
				{
					int num2 = j + this.worldOffset.y;
					int num3 = Grid.XYToCell(i + this.worldOffset.x, num2);
					if (Grid.IsValidCell(num3) && (Grid.Solid[num3] || Grid.IsLiquid(num3)))
					{
						num = Math.Min(num, num2);
						break;
					}
				}
			}
			int num4 = (num + this.worldOffset.y + this.worldSize.y) / 2;
			Vector3 vector = new Vector3((float)(this.WorldOffset.x + this.Width / 2), (float)num4, 0f);
			SaveGame.Instance.GetComponent<UserNavigation>().SetWorldCameraStartPosition(this.id, vector);
			return new Vector3?(vector);
		}
		return null;
	}

	// Token: 0x06004B15 RID: 19221 RVA: 0x001A3C6C File Offset: 0x001A1E6C
	public void EjectAllDupes(Vector3 spawn_pos)
	{
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.GetWorldItems(this.id, false))
		{
			minionIdentity.transform.SetLocalPosition(spawn_pos);
		}
	}

	// Token: 0x06004B16 RID: 19222 RVA: 0x001A3CD0 File Offset: 0x001A1ED0
	public void SpacePodAllDupes(AxialI sourceLocation, SimHashes podElement)
	{
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.GetWorldItems(this.id, false))
		{
			if (!minionIdentity.HasTag(GameTags.Dead))
			{
				Vector3 vector = new Vector3(-1f, -1f, 0f);
				GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab("EscapePod"), vector);
				gameObject.GetComponent<PrimaryElement>().SetElement(podElement, true);
				gameObject.SetActive(true);
				gameObject.GetComponent<MinionStorage>().SerializeMinion(minionIdentity.gameObject);
				TravellingCargoLander.StatesInstance smi = gameObject.GetSMI<TravellingCargoLander.StatesInstance>();
				smi.StartSM();
				smi.Travel(sourceLocation, ClusterUtil.ClosestVisibleAsteroidToLocation(sourceLocation).Location);
			}
		}
	}

	// Token: 0x06004B17 RID: 19223 RVA: 0x001A3DA8 File Offset: 0x001A1FA8
	public void DestroyWorldBuildings(out HashSet<int> noRefundTiles)
	{
		this.TransferBuildingMaterials(out noRefundTiles);
		foreach (ClustercraftInteriorDoor clustercraftInteriorDoor in Components.ClusterCraftInteriorDoors.GetWorldItems(this.id, false))
		{
			clustercraftInteriorDoor.DeleteObject();
		}
		this.ClearWorldZones();
	}

	// Token: 0x06004B18 RID: 19224 RVA: 0x001A3E10 File Offset: 0x001A2010
	public void TransferResourcesToParentWorld(Vector3 spawn_pos, HashSet<int> noRefundTiles)
	{
		this.TransferPickupables(spawn_pos);
		this.TransferLiquidsSolidsAndGases(spawn_pos, noRefundTiles);
	}

	// Token: 0x06004B19 RID: 19225 RVA: 0x001A3E24 File Offset: 0x001A2024
	public void TransferResourcesToDebris(AxialI sourceLocation, HashSet<int> noRefundTiles, SimHashes debrisContainerElement)
	{
		List<Storage> list = new List<Storage>();
		this.TransferPickupablesToDebris(ref list, debrisContainerElement);
		this.TransferLiquidsSolidsAndGasesToDebris(ref list, noRefundTiles, debrisContainerElement);
		foreach (Storage storage in list)
		{
			RailGunPayload.StatesInstance smi = storage.GetSMI<RailGunPayload.StatesInstance>();
			smi.StartSM();
			smi.Travel(sourceLocation, ClusterUtil.ClosestVisibleAsteroidToLocation(sourceLocation).Location);
		}
	}

	// Token: 0x06004B1A RID: 19226 RVA: 0x001A3EA0 File Offset: 0x001A20A0
	private void TransferBuildingMaterials(out HashSet<int> noRefundTiles)
	{
		HashSet<int> retTemplateFoundationCells = new HashSet<int>();
		ListPool<ScenePartitionerEntry, ClusterManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, ClusterManager>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)this.minimumBounds.x, (int)this.minimumBounds.y, this.Width, this.Height, GameScenePartitioner.Instance.completeBuildings, pooledList);
		Action<int> <>9__0;
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			BuildingComplete buildingComplete = scenePartitionerEntry.obj as BuildingComplete;
			if (buildingComplete != null)
			{
				Deconstructable component = buildingComplete.GetComponent<Deconstructable>();
				if (component != null && !buildingComplete.HasTag(GameTags.NoRocketRefund))
				{
					PrimaryElement component2 = buildingComplete.GetComponent<PrimaryElement>();
					float temperature = component2.Temperature;
					byte diseaseIdx = component2.DiseaseIdx;
					int diseaseCount = component2.DiseaseCount;
					int num = 0;
					while (num < component.constructionElements.Length && buildingComplete.Def.Mass.Length > num)
					{
						Element element = ElementLoader.GetElement(component.constructionElements[num]);
						if (element != null)
						{
							element.substance.SpawnResource(buildingComplete.transform.GetPosition(), buildingComplete.Def.Mass[num], temperature, diseaseIdx, diseaseCount, false, false, false);
						}
						else
						{
							GameObject prefab = Assets.GetPrefab(component.constructionElements[num]);
							int num2 = 0;
							while ((float)num2 < buildingComplete.Def.Mass[num])
							{
								GameUtil.KInstantiate(prefab, buildingComplete.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0).SetActive(true);
								num2++;
							}
						}
						num++;
					}
				}
				SimCellOccupier component3 = buildingComplete.GetComponent<SimCellOccupier>();
				if (component3 != null && component3.doReplaceElement)
				{
					Building building = buildingComplete;
					Action<int> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(int cell)
						{
							retTemplateFoundationCells.Add(cell);
						});
					}
					building.RunOnArea(action);
				}
				Storage component4 = buildingComplete.GetComponent<Storage>();
				if (component4 != null)
				{
					component4.DropAll(false, false, default(Vector3), true, null);
				}
				PlantablePlot component5 = buildingComplete.GetComponent<PlantablePlot>();
				if (component5 != null)
				{
					SeedProducer seedProducer = ((component5.Occupant != null) ? component5.Occupant.GetComponent<SeedProducer>() : null);
					if (seedProducer != null)
					{
						seedProducer.DropSeed(null);
					}
				}
				buildingComplete.DeleteObject();
			}
		}
		pooledList.Clear();
		noRefundTiles = retTemplateFoundationCells;
	}

	// Token: 0x06004B1B RID: 19227 RVA: 0x001A4130 File Offset: 0x001A2330
	private void TransferPickupables(Vector3 pos)
	{
		int num = Grid.PosToCell(pos);
		ListPool<ScenePartitionerEntry, ClusterManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, ClusterManager>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)this.minimumBounds.x, (int)this.minimumBounds.y, this.Width, this.Height, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			if (scenePartitionerEntry.obj != null)
			{
				Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
				if (pickupable != null)
				{
					pickupable.gameObject.transform.SetLocalPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.Move));
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06004B1C RID: 19228 RVA: 0x001A41FC File Offset: 0x001A23FC
	private void TransferLiquidsSolidsAndGases(Vector3 pos, HashSet<int> noRefundTiles)
	{
		int num = (int)this.minimumBounds.x;
		while ((float)num <= this.maximumBounds.x)
		{
			int num2 = (int)this.minimumBounds.y;
			while ((float)num2 <= this.maximumBounds.y)
			{
				int num3 = Grid.XYToCell(num, num2);
				if (!noRefundTiles.Contains(num3))
				{
					Element element = Grid.Element[num3];
					if (element != null && !element.IsVacuum)
					{
						element.substance.SpawnResource(pos, Grid.Mass[num3], Grid.Temperature[num3], Grid.DiseaseIdx[num3], Grid.DiseaseCount[num3], false, false, false);
					}
				}
				num2++;
			}
			num++;
		}
	}

	// Token: 0x06004B1D RID: 19229 RVA: 0x001A42B4 File Offset: 0x001A24B4
	private void TransferPickupablesToDebris(ref List<Storage> debrisObjects, SimHashes debrisContainerElement)
	{
		ListPool<ScenePartitionerEntry, ClusterManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, ClusterManager>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)this.minimumBounds.x, (int)this.minimumBounds.y, this.Width, this.Height, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			if (scenePartitionerEntry.obj != null)
			{
				Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
				if (pickupable != null)
				{
					if (pickupable.IsPrefabID(GameTags.Minion))
					{
						global::Util.KDestroyGameObject(pickupable.gameObject);
					}
					else
					{
						pickupable.PrimaryElement.Units = (float)Mathf.Max(1, Mathf.RoundToInt(pickupable.PrimaryElement.Units * 0.5f));
						if ((debrisObjects.Count == 0 || debrisObjects[debrisObjects.Count - 1].RemainingCapacity() == 0f) && pickupable.PrimaryElement.Mass > 0f)
						{
							debrisObjects.Add(CraftModuleInterface.SpawnRocketDebris(" from World Objects", debrisContainerElement));
						}
						Storage storage = debrisObjects[debrisObjects.Count - 1];
						while (pickupable.PrimaryElement.Mass > storage.RemainingCapacity())
						{
							Pickupable pickupable2 = pickupable.Take(storage.RemainingCapacity());
							storage.Store(pickupable2.gameObject, false, false, true, false);
							storage = CraftModuleInterface.SpawnRocketDebris(" from World Objects", debrisContainerElement);
							debrisObjects.Add(storage);
						}
						if (pickupable.PrimaryElement.Mass > 0f)
						{
							storage.Store(pickupable.gameObject, false, false, true, false);
						}
					}
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06004B1E RID: 19230 RVA: 0x001A4488 File Offset: 0x001A2688
	private void TransferLiquidsSolidsAndGasesToDebris(ref List<Storage> debrisObjects, HashSet<int> noRefundTiles, SimHashes debrisContainerElement)
	{
		int num = (int)this.minimumBounds.x;
		while ((float)num <= this.maximumBounds.x)
		{
			int num2 = (int)this.minimumBounds.y;
			while ((float)num2 <= this.maximumBounds.y)
			{
				int num3 = Grid.XYToCell(num, num2);
				if (!noRefundTiles.Contains(num3))
				{
					Element element = Grid.Element[num3];
					if (element != null && !element.IsVacuum)
					{
						float num4 = Grid.Mass[num3];
						num4 *= 0.5f;
						if ((debrisObjects.Count == 0 || debrisObjects[debrisObjects.Count - 1].RemainingCapacity() == 0f) && num4 > 0f)
						{
							debrisObjects.Add(CraftModuleInterface.SpawnRocketDebris(" from World Tiles", debrisContainerElement));
						}
						Storage storage = debrisObjects[debrisObjects.Count - 1];
						while (num4 > 0f)
						{
							float num5 = Mathf.Min(num4, storage.RemainingCapacity());
							num4 -= num5;
							storage.AddOre(element.id, num5, Grid.Temperature[num3], Grid.DiseaseIdx[num3], Grid.DiseaseCount[num3], false, true);
							if (num4 > 0f)
							{
								storage = CraftModuleInterface.SpawnRocketDebris(" from World Tiles", debrisContainerElement);
								debrisObjects.Add(storage);
							}
						}
					}
				}
				num2++;
			}
			num++;
		}
	}

	// Token: 0x06004B1F RID: 19231 RVA: 0x001A45F0 File Offset: 0x001A27F0
	public void CancelChores()
	{
		for (int i = 0; i < 44; i++)
		{
			int num = (int)this.minimumBounds.x;
			while ((float)num <= this.maximumBounds.x)
			{
				int num2 = (int)this.minimumBounds.y;
				while ((float)num2 <= this.maximumBounds.y)
				{
					int num3 = Grid.XYToCell(num, num2);
					GameObject gameObject = Grid.Objects[num3, i];
					if (gameObject != null)
					{
						gameObject.Trigger(2127324410, true);
					}
					num2++;
				}
				num++;
			}
		}
		List<Chore> list;
		GlobalChoreProvider.Instance.choreWorldMap.TryGetValue(this.id, out list);
		int num4 = 0;
		while (list != null && num4 < list.Count)
		{
			Chore chore = list[num4];
			if (chore != null && chore.target != null && !chore.isNull)
			{
				chore.Cancel("World destroyed");
			}
			num4++;
		}
		List<FetchChore> list2;
		GlobalChoreProvider.Instance.fetchMap.TryGetValue(this.id, out list2);
		int num5 = 0;
		while (list2 != null && num5 < list2.Count)
		{
			FetchChore fetchChore = list2[num5];
			if (fetchChore != null && fetchChore.target != null && !fetchChore.isNull)
			{
				fetchChore.Cancel("World destroyed");
			}
			num5++;
		}
	}

	// Token: 0x06004B20 RID: 19232 RVA: 0x001A4748 File Offset: 0x001A2948
	public void ClearWorldZones()
	{
		if (this.overworldCell != null)
		{
			WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
			int num = -1;
			for (int i = 0; i < SaveLoader.Instance.clusterDetailSave.overworldCells.Count; i++)
			{
				WorldDetailSave.OverworldCell overworldCell = SaveLoader.Instance.clusterDetailSave.overworldCells[i];
				if (overworldCell.zoneType == this.overworldCell.zoneType && overworldCell.tags != null && this.overworldCell.tags != null && overworldCell.tags.ContainsAll(this.overworldCell.tags) && overworldCell.poly.bounds == this.overworldCell.poly.bounds)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				clusterDetailSave.overworldCells.RemoveAt(num);
			}
		}
		int num2 = (int)this.minimumBounds.y;
		while ((float)num2 <= this.maximumBounds.y)
		{
			int num3 = (int)this.minimumBounds.x;
			while ((float)num3 <= this.maximumBounds.x)
			{
				SimMessages.ModifyCellWorldZone(Grid.XYToCell(num3, num2), byte.MaxValue);
				num3++;
			}
			num2++;
		}
	}

	// Token: 0x06004B21 RID: 19233 RVA: 0x001A4880 File Offset: 0x001A2A80
	public int GetSafeCell()
	{
		if (this.IsModuleInterior)
		{
			using (List<RocketControlStation>.Enumerator enumerator = Components.RocketControlStations.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RocketControlStation rocketControlStation = enumerator.Current;
					if (rocketControlStation.GetMyWorldId() == this.id)
					{
						return Grid.PosToCell(rocketControlStation);
					}
				}
				goto IL_A2;
			}
		}
		foreach (Telepad telepad in Components.Telepads.Items)
		{
			if (telepad.GetMyWorldId() == this.id)
			{
				return Grid.PosToCell(telepad);
			}
		}
		IL_A2:
		return Grid.XYToCell(this.worldOffset.x + this.worldSize.x / 2, this.worldOffset.y + this.worldSize.y / 2);
	}

	// Token: 0x06004B22 RID: 19234 RVA: 0x001A4984 File Offset: 0x001A2B84
	public string GetStatus()
	{
		return ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResultStatus(this.id);
	}

	// Token: 0x04003121 RID: 12577
	[Serialize]
	public int id = -1;

	// Token: 0x04003122 RID: 12578
	[Serialize]
	public Tag prefabTag;

	// Token: 0x04003125 RID: 12581
	[Serialize]
	private Vector2I worldOffset;

	// Token: 0x04003126 RID: 12582
	[Serialize]
	private Vector2I worldSize;

	// Token: 0x04003127 RID: 12583
	[Serialize]
	private bool fullyEnclosedBorder;

	// Token: 0x04003128 RID: 12584
	[Serialize]
	private bool isModuleInterior;

	// Token: 0x04003129 RID: 12585
	[Serialize]
	private WorldDetailSave.OverworldCell overworldCell;

	// Token: 0x0400312A RID: 12586
	[Serialize]
	private bool isDiscovered;

	// Token: 0x0400312B RID: 12587
	[Serialize]
	private bool isStartWorld;

	// Token: 0x0400312C RID: 12588
	[Serialize]
	private bool isDupeVisited;

	// Token: 0x0400312D RID: 12589
	[Serialize]
	private float dupeVisitedTimestamp;

	// Token: 0x0400312E RID: 12590
	[Serialize]
	private float discoveryTimestamp;

	// Token: 0x0400312F RID: 12591
	[Serialize]
	private bool isRoverVisited;

	// Token: 0x04003130 RID: 12592
	[Serialize]
	private bool isSurfaceRevealed;

	// Token: 0x04003131 RID: 12593
	[Serialize]
	public string worldName;

	// Token: 0x04003132 RID: 12594
	[Serialize]
	public string[] nameTables;

	// Token: 0x04003133 RID: 12595
	[Serialize]
	public string worldType;

	// Token: 0x04003134 RID: 12596
	[Serialize]
	public string worldDescription;

	// Token: 0x04003135 RID: 12597
	[Serialize]
	public int sunlight = FIXEDTRAITS.SUNLIGHT.DEFAULT_VALUE;

	// Token: 0x04003136 RID: 12598
	[Serialize]
	public int cosmicRadiation = FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;

	// Token: 0x04003137 RID: 12599
	[Serialize]
	public float currentSunlightIntensity;

	// Token: 0x04003138 RID: 12600
	[Serialize]
	public float currentCosmicIntensity = (float)FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;

	// Token: 0x04003139 RID: 12601
	[Serialize]
	public string sunlightFixedTrait;

	// Token: 0x0400313A RID: 12602
	[Serialize]
	public string cosmicRadiationFixedTrait;

	// Token: 0x0400313B RID: 12603
	[Serialize]
	public int fixedTraitsUpdateVersion = 1;

	// Token: 0x0400313C RID: 12604
	private Dictionary<string, int> sunlightFixedTraits = new Dictionary<string, int>
	{
		{
			FIXEDTRAITS.SUNLIGHT.NAME.NONE,
			FIXEDTRAITS.SUNLIGHT.NONE
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_LOW,
			FIXEDTRAITS.SUNLIGHT.VERY_VERY_LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_LOW,
			FIXEDTRAITS.SUNLIGHT.VERY_LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.LOW,
			FIXEDTRAITS.SUNLIGHT.LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.MED_LOW,
			FIXEDTRAITS.SUNLIGHT.MED_LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.MED,
			FIXEDTRAITS.SUNLIGHT.MED
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.MED_HIGH,
			FIXEDTRAITS.SUNLIGHT.MED_HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.HIGH,
			FIXEDTRAITS.SUNLIGHT.HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_HIGH,
			FIXEDTRAITS.SUNLIGHT.VERY_HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_HIGH,
			FIXEDTRAITS.SUNLIGHT.VERY_VERY_HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_VERY_HIGH,
			FIXEDTRAITS.SUNLIGHT.VERY_VERY_VERY_HIGH
		}
	};

	// Token: 0x0400313D RID: 12605
	private Dictionary<string, int> cosmicRadiationFixedTraits = new Dictionary<string, int>
	{
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.NONE,
			FIXEDTRAITS.COSMICRADIATION.NONE
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_LOW,
			FIXEDTRAITS.COSMICRADIATION.VERY_VERY_LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_LOW,
			FIXEDTRAITS.COSMICRADIATION.VERY_LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.LOW,
			FIXEDTRAITS.COSMICRADIATION.LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.MED_LOW,
			FIXEDTRAITS.COSMICRADIATION.MED_LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.MED,
			FIXEDTRAITS.COSMICRADIATION.MED
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.MED_HIGH,
			FIXEDTRAITS.COSMICRADIATION.MED_HIGH
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.HIGH,
			FIXEDTRAITS.COSMICRADIATION.HIGH
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_HIGH,
			FIXEDTRAITS.COSMICRADIATION.VERY_HIGH
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_HIGH,
			FIXEDTRAITS.COSMICRADIATION.VERY_VERY_HIGH
		}
	};

	// Token: 0x0400313E RID: 12606
	[Serialize]
	private List<string> m_seasonIds;

	// Token: 0x0400313F RID: 12607
	[Serialize]
	private List<string> m_subworldNames;

	// Token: 0x04003140 RID: 12608
	[Serialize]
	private List<string> m_worldTraitIds;

	// Token: 0x04003141 RID: 12609
	[Serialize]
	private List<string> m_storyTraitIds;

	// Token: 0x04003142 RID: 12610
	private WorldParentChangedEventArgs parentChangeArgs = new WorldParentChangedEventArgs();

	// Token: 0x04003143 RID: 12611
	[MySmiReq]
	private AlertStateManager.Instance m_alertManager;

	// Token: 0x04003144 RID: 12612
	private List<Prioritizable> yellowAlertTasks = new List<Prioritizable>();
}
