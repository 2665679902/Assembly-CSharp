using System;
using System.Collections.Generic;
using Delaunay.Geo;
using KSerialization;
using ProcGen;
using ProcGen.Map;
using UnityEngine;
using VoronoiTree;

namespace ProcGenGame
{
	// Token: 0x02000C44 RID: 3140
	[SerializationConfig(MemberSerialization.OptIn)]
	public class TerrainCell
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06006356 RID: 25430 RVA: 0x0024F50D File Offset: 0x0024D70D
		public Polygon poly
		{
			get
			{
				return this.site.poly;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06006357 RID: 25431 RVA: 0x0024F51A File Offset: 0x0024D71A
		// (set) Token: 0x06006358 RID: 25432 RVA: 0x0024F522 File Offset: 0x0024D722
		public Cell node { get; private set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06006359 RID: 25433 RVA: 0x0024F52B File Offset: 0x0024D72B
		// (set) Token: 0x0600635A RID: 25434 RVA: 0x0024F533 File Offset: 0x0024D733
		public Diagram.Site site { get; private set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600635B RID: 25435 RVA: 0x0024F53C File Offset: 0x0024D73C
		// (set) Token: 0x0600635C RID: 25436 RVA: 0x0024F544 File Offset: 0x0024D744
		public Dictionary<Tag, int> distancesToTags { get; private set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600635D RID: 25437 RVA: 0x0024F54D File Offset: 0x0024D74D
		public bool HasMobs
		{
			get
			{
				return this.mobs != null && this.mobs.Count > 0;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600635E RID: 25438 RVA: 0x0024F567 File Offset: 0x0024D767
		// (set) Token: 0x0600635F RID: 25439 RVA: 0x0024F56F File Offset: 0x0024D76F
		public List<KeyValuePair<int, Tag>> mobs { get; private set; }

		// Token: 0x06006360 RID: 25440 RVA: 0x0024F578 File Offset: 0x0024D778
		protected TerrainCell()
		{
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x0024F58B File Offset: 0x0024D78B
		protected TerrainCell(Cell node, Diagram.Site site, Dictionary<Tag, int> distancesToTags)
		{
			this.node = node;
			this.site = site;
			this.distancesToTags = distancesToTags;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x0024F5B3 File Offset: 0x0024D7B3
		public virtual void LogInfo(string evt, string param, float value)
		{
			global::Debug.Log(string.Concat(new string[]
			{
				evt,
				":",
				param,
				"=",
				value.ToString()
			}));
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x0024F5E8 File Offset: 0x0024D7E8
		public void InitializeCells(HashSet<int> claimedCells)
		{
			if (this.allCells != null)
			{
				return;
			}
			this.allCells = new List<int>();
			this.availableTerrainPoints = new HashSet<int>();
			this.availableSpawnPoints = new HashSet<int>();
			for (int i = 0; i < Grid.HeightInCells; i++)
			{
				for (int j = 0; j < Grid.WidthInCells; j++)
				{
					if (this.poly.Contains(new Vector2((float)j, (float)i)))
					{
						int num = Grid.XYToCell(j, i);
						this.availableTerrainPoints.Add(num);
						this.availableSpawnPoints.Add(num);
						if (claimedCells.Add(num))
						{
							this.allCells.Add(num);
						}
					}
				}
			}
			this.LogInfo("Initialise cells", "", (float)this.allCells.Count);
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x0024F6A8 File Offset: 0x0024D8A8
		public List<int> GetAllCells()
		{
			return new List<int>(this.allCells);
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x0024F6B8 File Offset: 0x0024D8B8
		public List<int> GetAvailableSpawnCellsAll()
		{
			List<int> list = new List<int>();
			foreach (int num in this.availableSpawnPoints)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x0024F714 File Offset: 0x0024D914
		public List<int> GetAvailableSpawnCellsFeature()
		{
			List<int> list = new List<int>();
			HashSet<int> hashSet = new HashSet<int>(this.availableSpawnPoints);
			hashSet.ExceptWith(this.availableTerrainPoints);
			foreach (int num in hashSet)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x0024F780 File Offset: 0x0024D980
		public List<int> GetAvailableSpawnCellsBiome()
		{
			List<int> list = new List<int>();
			HashSet<int> hashSet = new HashSet<int>(this.availableSpawnPoints);
			hashSet.ExceptWith(this.featureSpawnPoints);
			foreach (int num in hashSet)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x0024F7EC File Offset: 0x0024D9EC
		private bool RemoveFromAvailableSpawnCells(int cell)
		{
			return this.availableSpawnPoints.Remove(cell);
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x0024F7FC File Offset: 0x0024D9FC
		public void AddMobs(IEnumerable<KeyValuePair<int, Tag>> newMobs)
		{
			foreach (KeyValuePair<int, Tag> keyValuePair in newMobs)
			{
				this.AddMob(keyValuePair);
			}
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x0024F844 File Offset: 0x0024DA44
		private void AddMob(int cellIdx, string tag)
		{
			this.AddMob(new KeyValuePair<int, Tag>(cellIdx, new Tag(tag)));
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x0024F858 File Offset: 0x0024DA58
		public void AddMob(KeyValuePair<int, Tag> mob)
		{
			if (this.mobs == null)
			{
				this.mobs = new List<KeyValuePair<int, Tag>>();
			}
			this.mobs.Add(mob);
			bool flag = this.RemoveFromAvailableSpawnCells(mob.Key);
			this.LogInfo("\t\t\tRemoveFromAvailableCells", mob.Value.Name + ": " + (flag ? "success" : "failed"), (float)mob.Key);
			if (!flag)
			{
				if (!this.allCells.Contains(mob.Key))
				{
					global::Debug.Assert(false, string.Concat(new string[]
					{
						"Couldnt find cell [",
						mob.Key.ToString(),
						"] we dont own, to remove for mob [",
						mob.Value.Name,
						"]"
					}));
					return;
				}
				global::Debug.Assert(false, string.Concat(new string[]
				{
					"Couldnt find cell [",
					mob.Key.ToString(),
					"] to remove for mob [",
					mob.Value.Name,
					"]"
				}));
			}
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x0024F984 File Offset: 0x0024DB84
		protected string GetSubWorldType(WorldGen worldGen)
		{
			Vector2I vector2I = new Vector2I((int)this.site.poly.Centroid().x, (int)this.site.poly.Centroid().y);
			return worldGen.GetSubWorldType(vector2I);
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x0024F9CC File Offset: 0x0024DBCC
		protected Temperature.Range GetTemperatureRange(WorldGen worldGen)
		{
			string subWorldType = this.GetSubWorldType(worldGen);
			if (subWorldType == null)
			{
				return Temperature.Range.Mild;
			}
			if (!worldGen.Settings.HasSubworld(subWorldType))
			{
				return Temperature.Range.Mild;
			}
			return worldGen.Settings.GetSubWorld(subWorldType).temperatureRange;
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x0024FA08 File Offset: 0x0024DC08
		protected void GetTemperatureRange(WorldGen worldGen, ref float min, ref float range)
		{
			Temperature.Range temperatureRange = this.GetTemperatureRange(worldGen);
			min = SettingsCache.temperatures[temperatureRange].min;
			range = SettingsCache.temperatures[temperatureRange].max - min;
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x0024FA44 File Offset: 0x0024DC44
		protected float GetDensityMassForCell(Chunk world, int cellIdx, float mass)
		{
			if (!Grid.IsValidCell(cellIdx))
			{
				return 0f;
			}
			global::Debug.Assert(world.density[cellIdx] >= 0f && world.density[cellIdx] <= 1f, "Density [" + world.density[cellIdx].ToString() + "] out of range [0-1]");
			float num = 0.2f * (world.density[cellIdx] - 0.5f);
			float num2 = mass + mass * num;
			if (num2 > 10000f)
			{
				num2 = 10000f;
			}
			return num2;
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x0024FAD4 File Offset: 0x0024DCD4
		private void HandleSprinkleOfElement(WorldGenSettings settings, Tag targetTag, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd)
		{
			Element element = ElementLoader.FindElementByName(settings.GetFeature(targetTag.Name).GetOneWeightedSimHash("SprinkleOfElementChoices", rnd).element);
			ProcGen.Room room = null;
			SettingsCache.rooms.TryGetValue(targetTag.Name, out room);
			SampleDescriber sampleDescriber = room;
			Sim.PhysicsData defaultValues = element.defaultValues;
			Sim.DiseaseCell invalid = Sim.DiseaseCell.Invalid;
			for (int i = 0; i < this.terrainPositions.Count; i++)
			{
				if (!(this.terrainPositions[i].Value != targetTag))
				{
					float num = rnd.RandomRange(sampleDescriber.blobSize.min, sampleDescriber.blobSize.max);
					List<Vector2I> filledCircle = ProcGen.Util.GetFilledCircle(Grid.CellToPos2D(this.terrainPositions[i].Key), num);
					for (int j = 0; j < filledCircle.Count; j++)
					{
						int num2 = Grid.XYToCell(filledCircle[j].x, filledCircle[j].y);
						if (Grid.IsValidCell(num2))
						{
							defaultValues.mass = this.GetDensityMassForCell(world, num2, element.defaultValues.mass);
							defaultValues.temperature = temperatureMin + world.heatOffset[num2] * temperatureRange;
							SetValues(num2, element, defaultValues, invalid);
						}
					}
				}
			}
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0024FC3C File Offset: 0x0024DE3C
		private HashSet<Vector2I> DigFeature(ProcGen.Room.Shape shape, float size, List<int> bordersWidths, SeededRandom rnd, out List<Vector2I> featureCenterPoints, out List<List<Vector2I>> featureBorders)
		{
			HashSet<Vector2I> hashSet = new HashSet<Vector2I>();
			featureCenterPoints = new List<Vector2I>();
			featureBorders = new List<List<Vector2I>>();
			if (size < 1f)
			{
				return hashSet;
			}
			Vector2 vector = this.site.poly.Centroid();
			this.finalSize = size;
			switch (shape)
			{
			case ProcGen.Room.Shape.Circle:
				featureCenterPoints = ProcGen.Util.GetFilledCircle(vector, this.finalSize);
				break;
			case ProcGen.Room.Shape.Blob:
				featureCenterPoints = ProcGen.Util.GetBlob(vector, this.finalSize, rnd.RandomSource());
				break;
			case ProcGen.Room.Shape.Square:
				featureCenterPoints = ProcGen.Util.GetFilledRectangle(vector, this.finalSize, this.finalSize, rnd, 2f, 2f);
				break;
			case ProcGen.Room.Shape.TallThin:
				featureCenterPoints = ProcGen.Util.GetFilledRectangle(vector, this.finalSize / 4f, this.finalSize, rnd, 2f, 2f);
				break;
			case ProcGen.Room.Shape.ShortWide:
				featureCenterPoints = ProcGen.Util.GetFilledRectangle(vector, this.finalSize, this.finalSize / 4f, rnd, 2f, 2f);
				break;
			case ProcGen.Room.Shape.Splat:
				featureCenterPoints = ProcGen.Util.GetSplat(vector, this.finalSize, rnd.RandomSource());
				break;
			}
			hashSet.UnionWith(featureCenterPoints);
			if (featureCenterPoints.Count == 0)
			{
				global::Debug.LogWarning(string.Concat(new string[]
				{
					"Room has no centerpoints. Terrain Cell [ shape: ",
					shape.ToString(),
					" size: ",
					this.finalSize.ToString(),
					"] [",
					this.node.NodeId.ToString(),
					" ",
					this.node.type,
					" ",
					this.node.position.ToString(),
					"]"
				}));
			}
			else if (bordersWidths != null && bordersWidths.Count > 0 && bordersWidths[0] > 0)
			{
				int num = 0;
				while (num < bordersWidths.Count && bordersWidths[num] > 0)
				{
					featureBorders.Add(ProcGen.Util.GetBorder(hashSet, bordersWidths[num]));
					hashSet.UnionWith(featureBorders[num]);
					num++;
				}
			}
			return hashSet;
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x0024FE84 File Offset: 0x0024E084
		public static TerrainCell.ElementOverride GetElementOverride(string element, SampleDescriber.Override overrides)
		{
			global::Debug.Assert(element != null && element.Length > 0);
			TerrainCell.ElementOverride elementOverride = new TerrainCell.ElementOverride
			{
				element = ElementLoader.FindElementByName(element)
			};
			global::Debug.Assert(elementOverride.element != null, "Couldn't find an element called " + element);
			elementOverride.pdelement = elementOverride.element.defaultValues;
			elementOverride.dc = Sim.DiseaseCell.Invalid;
			elementOverride.mass = elementOverride.element.defaultValues.mass;
			elementOverride.temperature = elementOverride.element.defaultValues.temperature;
			if (overrides == null)
			{
				return elementOverride;
			}
			elementOverride.overrideMass = false;
			elementOverride.overrideTemperature = false;
			elementOverride.overrideDiseaseIdx = false;
			elementOverride.overrideDiseaseAmount = false;
			if (overrides.massOverride != null)
			{
				elementOverride.mass = overrides.massOverride.Value;
				elementOverride.overrideMass = true;
			}
			if (overrides.massMultiplier != null)
			{
				elementOverride.mass *= overrides.massMultiplier.Value;
				elementOverride.overrideMass = true;
			}
			if (overrides.temperatureOverride != null)
			{
				elementOverride.temperature = overrides.temperatureOverride.Value;
				elementOverride.overrideTemperature = true;
			}
			if (overrides.temperatureMultiplier != null)
			{
				elementOverride.temperature *= overrides.temperatureMultiplier.Value;
				elementOverride.overrideTemperature = true;
			}
			if (overrides.diseaseOverride != null)
			{
				elementOverride.diseaseIdx = WorldGen.diseaseStats.GetIndex(overrides.diseaseOverride);
				elementOverride.overrideDiseaseIdx = true;
			}
			if (overrides.diseaseAmountOverride != null)
			{
				elementOverride.diseaseAmount = overrides.diseaseAmountOverride.Value;
				elementOverride.overrideDiseaseAmount = true;
			}
			if (elementOverride.overrideTemperature)
			{
				elementOverride.pdelement.temperature = elementOverride.temperature;
			}
			if (elementOverride.overrideMass)
			{
				elementOverride.pdelement.mass = elementOverride.mass;
			}
			if (elementOverride.overrideDiseaseIdx)
			{
				elementOverride.dc.diseaseIdx = elementOverride.diseaseIdx;
			}
			if (elementOverride.overrideDiseaseAmount)
			{
				elementOverride.dc.elementCount = elementOverride.diseaseAmount;
			}
			return elementOverride;
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x002500C8 File Offset: 0x0024E2C8
		private bool IsFeaturePointContainedInBorder(Vector2 point, WorldGen worldGen)
		{
			if (!this.node.tags.Contains(WorldGenTags.AllowExceedNodeBorders))
			{
				return true;
			}
			if (!this.poly.Contains(point))
			{
				TerrainCell terrainCell = worldGen.TerrainCells.Find((TerrainCell x) => x.poly.Contains(point));
				if (terrainCell != null)
				{
					SubWorld subWorld = worldGen.Settings.GetSubWorld(this.node.GetSubworld());
					SubWorld subWorld2 = worldGen.Settings.GetSubWorld(terrainCell.node.GetSubworld());
					if (subWorld.zoneType != subWorld2.zoneType)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x00250168 File Offset: 0x0024E368
		private void ApplyPlaceElementForRoom(FeatureSettings feature, string group, List<Vector2I> cells, WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd, HashSet<int> highPriorityClaims)
		{
			if (cells == null || cells.Count == 0)
			{
				return;
			}
			if (!feature.HasGroup(group))
			{
				return;
			}
			switch (feature.ElementChoiceGroups[group].selectionMethod)
			{
			case ProcGen.Room.Selection.Weighted:
			case ProcGen.Room.Selection.WeightedResample:
			{
				for (int i = 0; i < cells.Count; i++)
				{
					int num = Grid.XYToCell(cells[i].x, cells[i].y);
					if (Grid.IsValidCell(num) && !highPriorityClaims.Contains(num) && this.IsFeaturePointContainedInBorder(cells[i], worldGen))
					{
						WeightedSimHash oneWeightedSimHash = feature.GetOneWeightedSimHash(group, rnd);
						TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(oneWeightedSimHash.element, oneWeightedSimHash.overrides);
						if (!elementOverride.overrideTemperature)
						{
							elementOverride.pdelement.temperature = temperatureMin + world.heatOffset[num] * temperatureRange;
						}
						if (!elementOverride.overrideMass)
						{
							elementOverride.pdelement.mass = this.GetDensityMassForCell(world, num, elementOverride.mass);
						}
						SetValues(num, elementOverride.element, elementOverride.pdelement, elementOverride.dc);
					}
				}
				return;
			}
			case ProcGen.Room.Selection.HorizontalSlice:
			{
				int num2 = int.MaxValue;
				int num3 = int.MinValue;
				for (int j = 0; j < cells.Count; j++)
				{
					num2 = Mathf.Min(cells[j].y, num2);
					num3 = Mathf.Max(cells[j].y, num3);
				}
				int num4 = num3 - num2;
				for (int k = 0; k < cells.Count; k++)
				{
					int num5 = Grid.XYToCell(cells[k].x, cells[k].y);
					if (Grid.IsValidCell(num5) && !highPriorityClaims.Contains(num5) && this.IsFeaturePointContainedInBorder(cells[k], worldGen))
					{
						float num6 = 1f - (float)(cells[k].y - num2) / (float)num4;
						WeightedSimHash weightedSimHashAtChoice = feature.GetWeightedSimHashAtChoice(group, num6);
						TerrainCell.ElementOverride elementOverride2 = TerrainCell.GetElementOverride(weightedSimHashAtChoice.element, weightedSimHashAtChoice.overrides);
						if (!elementOverride2.overrideTemperature)
						{
							elementOverride2.pdelement.temperature = temperatureMin + world.heatOffset[num5] * temperatureRange;
						}
						if (!elementOverride2.overrideMass)
						{
							elementOverride2.pdelement.mass = this.GetDensityMassForCell(world, num5, elementOverride2.mass);
						}
						SetValues(num5, elementOverride2.element, elementOverride2.pdelement, elementOverride2.dc);
					}
				}
				return;
			}
			}
			WeightedSimHash oneWeightedSimHash2 = feature.GetOneWeightedSimHash(group, rnd);
			for (int l = 0; l < cells.Count; l++)
			{
				int num7 = Grid.XYToCell(cells[l].x, cells[l].y);
				if (Grid.IsValidCell(num7) && !highPriorityClaims.Contains(num7) && this.IsFeaturePointContainedInBorder(cells[l], worldGen))
				{
					TerrainCell.ElementOverride elementOverride3 = TerrainCell.GetElementOverride(oneWeightedSimHash2.element, oneWeightedSimHash2.overrides);
					if (!elementOverride3.overrideTemperature)
					{
						elementOverride3.pdelement.temperature = temperatureMin + world.heatOffset[num7] * temperatureRange;
					}
					if (!elementOverride3.overrideMass)
					{
						elementOverride3.pdelement.mass = this.GetDensityMassForCell(world, num7, elementOverride3.mass);
					}
					SetValues(num7, elementOverride3.element, elementOverride3.pdelement, elementOverride3.dc);
				}
			}
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x00250510 File Offset: 0x0024E710
		private int GetIndexForLocation(List<Vector2I> points, Mob.Location location, SeededRandom rnd)
		{
			int num = -1;
			if (points == null || points.Count == 0)
			{
				return num;
			}
			if (location == Mob.Location.Air || location == Mob.Location.Solid)
			{
				return rnd.RandomRange(0, points.Count);
			}
			for (int i = 0; i < points.Count; i++)
			{
				if (Grid.IsValidCell(Grid.XYToCell(points[i].x, points[i].y)))
				{
					if (num == -1)
					{
						num = i;
					}
					else if (location != Mob.Location.Floor)
					{
						if (location == Mob.Location.Ceiling && points[i].y > points[num].y)
						{
							num = i;
						}
					}
					else if (points[i].y < points[num].y)
					{
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x002505C4 File Offset: 0x0024E7C4
		private void PlaceMobsInRoom(WorldGenSettings settings, List<MobReference> mobTags, List<Vector2I> points, SeededRandom rnd)
		{
			if (points == null)
			{
				return;
			}
			if (this.mobs == null)
			{
				this.mobs = new List<KeyValuePair<int, Tag>>();
			}
			for (int i = 0; i < mobTags.Count; i++)
			{
				if (!settings.HasMob(mobTags[i].type))
				{
					global::Debug.LogError("Missing sample description for tag [" + mobTags[i].type + "]");
				}
				else
				{
					Mob mob = settings.GetMob(mobTags[i].type);
					int num = Mathf.RoundToInt(mobTags[i].count.GetRandomValueWithinRange(rnd));
					for (int j = 0; j < num; j++)
					{
						int indexForLocation = this.GetIndexForLocation(points, mob.location, rnd);
						if (indexForLocation == -1)
						{
							break;
						}
						if (points.Count <= indexForLocation)
						{
							return;
						}
						int num2 = Grid.XYToCell(points[indexForLocation].x, points[indexForLocation].y);
						points.RemoveAt(indexForLocation);
						this.AddMob(num2, mobTags[i].type);
					}
				}
			}
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x002506D8 File Offset: 0x0024E8D8
		private int[] ConvertNoiseToPoints(float[] basenoise, float minThreshold = 0.9f, float maxThreshold = 1f)
		{
			if (basenoise == null)
			{
				return null;
			}
			List<int> list = new List<int>();
			float width = this.site.poly.bounds.width;
			float height = this.site.poly.bounds.height;
			for (float num = this.site.position.y - height / 2f; num < this.site.position.y + height / 2f; num += 1f)
			{
				for (float num2 = this.site.position.x - width / 2f; num2 < this.site.position.x + width / 2f; num2 += 1f)
				{
					int num3 = Grid.PosToCell(new Vector2(num2, num));
					if (this.site.poly.Contains(new Vector2(num2, num)))
					{
						float num4 = (float)((int)basenoise[num3]);
						if (num4 >= minThreshold && num4 <= maxThreshold && !list.Contains(num3))
						{
							list.Add(Grid.PosToCell(new Vector2(num2, num)));
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x00250810 File Offset: 0x0024EA10
		private void ApplyForeground(WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd)
		{
			this.LogInfo("Apply foregreound", (this.node.tags != null).ToString(), (float)((this.node.tags != null) ? this.node.tags.Count : 0));
			if (this.node.tags != null)
			{
				FeatureSettings featureSettings = worldGen.Settings.TryGetFeature(this.node.type);
				this.LogInfo("\tFeature?", (featureSettings != null).ToString(), 0f);
				if (featureSettings == null && this.node.tags != null)
				{
					List<Tag> list = new List<Tag>();
					foreach (Tag tag in this.node.tags)
					{
						if (worldGen.Settings.HasFeature(tag.Name))
						{
							list.Add(tag);
						}
					}
					this.LogInfo("\tNo feature, checking possible feature tags, found", "", (float)list.Count);
					if (list.Count > 0)
					{
						Tag tag2 = list[rnd.RandomSource().Next(list.Count)];
						featureSettings = worldGen.Settings.GetFeature(tag2.Name);
						this.LogInfo("\tPicked feature", tag2.Name, 0f);
					}
				}
				if (featureSettings != null)
				{
					this.LogInfo("APPLY FOREGROUND", this.node.type, 0f);
					float num = featureSettings.blobSize.GetRandomValueWithinRange(rnd);
					float num2 = this.poly.DistanceToClosestEdge(null);
					if (!this.node.tags.Contains(WorldGenTags.AllowExceedNodeBorders) && num2 < num)
					{
						if (this.debugMode)
						{
							global::Debug.LogWarning(string.Concat(new string[]
							{
								this.node.type,
								" ",
								featureSettings.shape.ToString(),
								"  blob size too large to fit in node. Size reduced. ",
								num.ToString(),
								"->",
								(num2 - 6f).ToString()
							}));
						}
						num = num2 - 6f;
					}
					if (num <= 0f)
					{
						return;
					}
					List<Vector2I> list2;
					List<List<Vector2I>> list3;
					HashSet<Vector2I> hashSet = this.DigFeature(featureSettings.shape, num, featureSettings.borders, rnd, out list2, out list3);
					this.featureSpawnPoints = new HashSet<int>();
					foreach (Vector2I vector2I in hashSet)
					{
						this.featureSpawnPoints.Add(Grid.XYToCell(vector2I.x, vector2I.y));
					}
					this.LogInfo("\t\t", "claimed points", (float)this.featureSpawnPoints.Count);
					this.availableTerrainPoints.ExceptWith(this.featureSpawnPoints);
					this.ApplyPlaceElementForRoom(featureSettings, "RoomCenterElements", list2, worldGen, world, SetValues, temperatureMin, temperatureRange, rnd, worldGen.HighPriorityClaimedCells);
					if (list3 != null)
					{
						for (int i = 0; i < list3.Count; i++)
						{
							this.ApplyPlaceElementForRoom(featureSettings, "RoomBorderChoices" + i.ToString(), list3[i], worldGen, world, SetValues, temperatureMin, temperatureRange, rnd, worldGen.HighPriorityClaimedCells);
						}
					}
					if (featureSettings.tags.Contains(WorldGenTags.HighPriorityFeature.Name))
					{
						worldGen.AddHighPriorityCells(this.featureSpawnPoints);
					}
				}
			}
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x00250BA8 File Offset: 0x0024EDA8
		private void ApplyBackground(WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd)
		{
			this.LogInfo("Apply Background", this.node.type, 0f);
			float floatSetting = worldGen.Settings.GetFloatSetting("CaveOverrideMaxValue");
			float floatSetting2 = worldGen.Settings.GetFloatSetting("CaveOverrideSliverValue");
			Leaf leafForTerrainCell = worldGen.GetLeafForTerrainCell(this);
			bool flag = leafForTerrainCell.tags.Contains(WorldGenTags.IgnoreCaveOverride);
			bool flag2 = leafForTerrainCell.tags.Contains(WorldGenTags.CaveVoidSliver);
			bool flag3 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToCentroid);
			bool flag4 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToCentroidInv);
			bool flag5 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToEdge);
			bool flag6 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToEdgeInv);
			bool flag7 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToBorder);
			bool flag8 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToBorderWeak);
			bool flag9 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToBorderInv);
			bool flag10 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToWorldTop);
			bool flag11 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToWorldTopOrSide);
			bool flag12 = leafForTerrainCell.tags.Contains(WorldGenTags.DistFunctionPointCentroid);
			bool flag13 = leafForTerrainCell.tags.Contains(WorldGenTags.DistFunctionPointEdge);
			this.LogInfo("Getting Element Bands", this.node.type, 0f);
			ElementBandConfiguration elementBandConfiguration = worldGen.Settings.GetElementBandForBiome(this.node.type);
			if (elementBandConfiguration == null && this.node.biomeSpecificTags != null)
			{
				this.LogInfo("\tType is not a biome, checking tags", "", (float)this.node.tags.Count);
				List<ElementBandConfiguration> list = new List<ElementBandConfiguration>();
				foreach (Tag tag in this.node.biomeSpecificTags)
				{
					ElementBandConfiguration elementBandForBiome = worldGen.Settings.GetElementBandForBiome(tag.Name);
					if (elementBandForBiome != null)
					{
						list.Add(elementBandForBiome);
						this.LogInfo("\tFound biome", tag.Name, 0f);
					}
				}
				if (list.Count > 0)
				{
					int num = rnd.RandomSource().Next(list.Count);
					elementBandConfiguration = list[num];
					this.LogInfo("\tPicked biome", "", (float)num);
				}
			}
			DebugUtil.Assert(elementBandConfiguration != null, "A node didn't get assigned a biome! ", this.node.type);
			foreach (int num2 in this.availableTerrainPoints)
			{
				Vector2I vector2I = Grid.CellToXY(num2);
				if (!worldGen.HighPriorityClaimedCells.Contains(num2))
				{
					float num3 = world.overrides[num2];
					if (!flag && num3 >= 100f)
					{
						if (num3 >= 300f)
						{
							SetValues(num2, WorldGen.voidElement, WorldGen.voidElement.defaultValues, Sim.DiseaseCell.Invalid);
						}
						else if (num3 >= 200f)
						{
							SetValues(num2, WorldGen.unobtaniumElement, WorldGen.unobtaniumElement.defaultValues, Sim.DiseaseCell.Invalid);
						}
						else
						{
							SetValues(num2, WorldGen.katairiteElement, WorldGen.katairiteElement.defaultValues, Sim.DiseaseCell.Invalid);
						}
					}
					else
					{
						float num4 = 1f;
						Vector2 vector = new Vector2((float)vector2I.x, (float)vector2I.y);
						if (flag3 || flag4)
						{
							float num5 = 15f;
							if (flag13)
							{
								float num6 = 0f;
								MathUtil.Pair<Vector2, Vector2> closestEdge = this.poly.GetClosestEdge(vector, ref num6);
								num5 = Vector2.Distance(closestEdge.First + (closestEdge.Second - closestEdge.First) * num6, vector);
							}
							num4 = Vector2.Distance(this.poly.Centroid(), vector) / num5;
							num4 = Mathf.Max(0f, Mathf.Min(1f, num4));
							if (flag4)
							{
								num4 = 1f - num4;
							}
						}
						if (flag6 || flag5)
						{
							float num7 = 0f;
							MathUtil.Pair<Vector2, Vector2> closestEdge2 = this.poly.GetClosestEdge(vector, ref num7);
							Vector2 vector2 = closestEdge2.First + (closestEdge2.Second - closestEdge2.First) * num7;
							float num8 = 15f;
							if (flag12)
							{
								num8 = Vector2.Distance(this.poly.Centroid(), vector);
							}
							num4 = Vector2.Distance(vector2, vector) / num8;
							num4 = Mathf.Max(0f, Mathf.Min(1f, num4));
							if (flag6)
							{
								num4 = 1f - num4;
							}
						}
						if (flag9 || flag7)
						{
							List<Edge> edgesWithTag = worldGen.WorldLayout.overworldGraph.GetEdgesWithTag(WorldGenTags.EdgeClosed);
							float num9 = float.MaxValue;
							foreach (Edge edge in edgesWithTag)
							{
								MathUtil.Pair<Vector2, Vector2> pair = new MathUtil.Pair<Vector2, Vector2>(edge.corner0.position, edge.corner1.position);
								float num10 = 0f;
								num9 = Mathf.Min(Mathf.Abs(MathUtil.GetClosestPointBetweenPointAndLineSegment(pair, vector, ref num10)), num9);
							}
							float num11 = (flag8 ? 7f : 20f);
							if (flag12)
							{
								num11 = Vector2.Distance(this.poly.Centroid(), vector);
							}
							num4 = num9 / num11;
							num4 = Mathf.Max(0f, Mathf.Min(1f, num4));
							if (flag9)
							{
								num4 = 1f - num4;
							}
						}
						if (flag10)
						{
							float y = (float)worldGen.WorldSize.y;
							float num12 = 38f;
							float num13 = 58f;
							float num14 = y - vector.y;
							if (num14 < num12)
							{
								num4 = 0f;
							}
							else if (num14 < num13)
							{
								num4 = Mathf.Clamp01((num14 - num12) / (num13 - num12));
							}
							else
							{
								num4 = 1f;
							}
						}
						if (flag11)
						{
							float y2 = (float)worldGen.WorldSize.y;
							int x = worldGen.WorldSize.x;
							float num15 = 2f;
							float num16 = 10f;
							float num17 = y2 - vector.y;
							float x2 = vector.x;
							float num18 = (float)x - vector.x;
							float num19 = Mathf.Min(new float[] { num17, x2, num18 });
							if (num19 < num15)
							{
								num4 = 0f;
							}
							else if (num19 < num16)
							{
								num4 = Mathf.Clamp01((num19 - num15) / (num16 - num15));
							}
							else
							{
								num4 = 1f;
							}
						}
						Element element;
						Sim.PhysicsData defaultValues;
						Sim.DiseaseCell diseaseCell;
						worldGen.GetElementForBiomePoint(world, elementBandConfiguration, vector2I, out element, out defaultValues, out diseaseCell, num4);
						defaultValues.mass += defaultValues.mass * 0.2f * (world.density[vector2I.x + world.size.x * vector2I.y] - 0.5f);
						if (!element.IsVacuum && element.id != SimHashes.Katairite && element.id != SimHashes.Unobtanium)
						{
							float num20 = temperatureMin;
							if (element.lowTempTransition != null && temperatureMin < element.lowTemp)
							{
								num20 = element.lowTemp;
							}
							defaultValues.temperature = num20 + world.heatOffset[num2] * temperatureRange;
						}
						if (element.IsSolid && !flag && num3 > floatSetting && num3 < 100f)
						{
							if (flag2 && num3 > floatSetting2)
							{
								element = WorldGen.voidElement;
							}
							else
							{
								element = WorldGen.vacuumElement;
							}
							defaultValues = element.defaultValues;
						}
						SetValues(num2, element, defaultValues, diseaseCell);
					}
				}
			}
			if (this.node.tags.Contains(WorldGenTags.SprinkleOfOxyRock))
			{
				this.HandleSprinkleOfElement(worldGen.Settings, WorldGenTags.SprinkleOfOxyRock, world, SetValues, temperatureMin, temperatureRange, rnd);
			}
			if (this.node.tags.Contains(WorldGenTags.SprinkleOfMetal))
			{
				this.HandleSprinkleOfElement(worldGen.Settings, WorldGenTags.SprinkleOfMetal, world, SetValues, temperatureMin, temperatureRange, rnd);
			}
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x002513C4 File Offset: 0x0024F5C4
		private void GenerateActionCells(WorldGenSettings settings, Tag tag, HashSet<int> possiblePoints, SeededRandom rnd)
		{
			ProcGen.Room room = null;
			SettingsCache.rooms.TryGetValue(tag.Name, out room);
			SampleDescriber sampleDescriber = room;
			if (sampleDescriber == null && settings.HasMob(tag.Name))
			{
				sampleDescriber = settings.GetMob(tag.Name);
			}
			if (sampleDescriber == null)
			{
				return;
			}
			HashSet<int> hashSet = new HashSet<int>();
			float randomValueWithinRange = sampleDescriber.density.GetRandomValueWithinRange(rnd);
			SampleDescriber.PointSelectionMethod selectMethod = sampleDescriber.selectMethod;
			List<Vector2> list;
			if (selectMethod != SampleDescriber.PointSelectionMethod.RandomPoints)
			{
				if (selectMethod != SampleDescriber.PointSelectionMethod.Centroid)
				{
				}
				list = new List<Vector2>();
				list.Add(this.node.position);
			}
			else
			{
				list = PointGenerator.GetRandomPoints(this.poly, randomValueWithinRange, 0f, null, sampleDescriber.sampleBehaviour, true, rnd, true, true);
			}
			foreach (Vector2 vector in list)
			{
				int num = Grid.XYToCell((int)vector.x, (int)vector.y);
				if (possiblePoints.Contains(num))
				{
					hashSet.Add(num);
				}
			}
			if (room != null && room.mobselection == ProcGen.Room.Selection.None)
			{
				if (this.terrainPositions == null)
				{
					this.terrainPositions = new List<KeyValuePair<int, Tag>>();
				}
				foreach (int num2 in hashSet)
				{
					if (Grid.IsValidCell(num2))
					{
						this.terrainPositions.Add(new KeyValuePair<int, Tag>(num2, tag));
					}
				}
			}
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x0025154C File Offset: 0x0024F74C
		private void DoProcess(WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, SeededRandom rnd)
		{
			float num = 265f;
			float num2 = 30f;
			this.InitializeCells(worldGen.ClaimedCells);
			this.GetTemperatureRange(worldGen, ref num, ref num2);
			this.ApplyForeground(worldGen, world, SetValues, num, num2, rnd);
			for (int i = 0; i < this.node.tags.Count; i++)
			{
				this.GenerateActionCells(worldGen.Settings, this.node.tags[i], this.availableTerrainPoints, rnd);
			}
			this.ApplyBackground(worldGen, world, SetValues, num, num2, rnd);
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x002515D8 File Offset: 0x0024F7D8
		public void Process(WorldGen worldGen, Sim.Cell[] cells, float[] bgTemp, Sim.DiseaseCell[] dcs, Chunk world, SeededRandom rnd)
		{
			TerrainCell.SetValuesFunction setValuesFunction = delegate(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc)
			{
				if (Grid.IsValidCell(index))
				{
					if (pd.temperature == 0f || (elem as Element).HasTag(GameTags.Special))
					{
						bgTemp[index] = -1f;
					}
					cells[index].SetValues(elem as Element, pd, ElementLoader.elements);
					dcs[index] = dc;
					return;
				}
				global::Debug.LogError(string.Concat(new string[]
				{
					"Process::SetValuesFunction Index [",
					index.ToString(),
					"] is not valid. cells.Length [",
					cells.Length.ToString(),
					"]"
				}));
			};
			this.DoProcess(worldGen, world, setValuesFunction, rnd);
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x00251618 File Offset: 0x0024F818
		public void Process(WorldGen worldGen, Chunk world, SeededRandom rnd)
		{
			TerrainCell.SetValuesFunction setValuesFunction = delegate(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc)
			{
				SimMessages.ModifyCell(index, (elem as Element).idx, pd.temperature, pd.mass, dc.diseaseIdx, dc.elementCount, SimMessages.ReplaceType.Replace, false, -1);
			};
			this.DoProcess(worldGen, world, setValuesFunction, rnd);
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x00251650 File Offset: 0x0024F850
		public int DistanceToTag(Tag tag)
		{
			int num;
			if (!this.distancesToTags.TryGetValue(tag, out num))
			{
				DebugUtil.DevLogError(string.Format("DistanceToTag could not find tag '{0}', did forget to include a start template?", tag));
			}
			return num;
		}

		// Token: 0x0600637F RID: 25471 RVA: 0x00251683 File Offset: 0x0024F883
		public bool IsSafeToSpawnPOI(List<TerrainCell> allCells, bool log = true)
		{
			return this.IsSafeToSpawnPOI(allCells, TerrainCell.noPOISpawnTags, TerrainCell.noPOISpawnTagSet, log);
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x00251697 File Offset: 0x0024F897
		public bool IsSafeToSpawnPOIRelaxed(List<TerrainCell> allCells, bool log = true)
		{
			return this.IsSafeToSpawnPOI(allCells, TerrainCell.relaxedNoPOISpawnTags, TerrainCell.relaxedNoPOISpawnTagSet, log);
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x002516AB File Offset: 0x0024F8AB
		private bool IsSafeToSpawnPOI(List<TerrainCell> allCells, Tag[] noSpawnTags, TagSet noSpawnTagSet, bool log)
		{
			return !this.node.tags.ContainsOne(noSpawnTagSet);
		}

		// Token: 0x040044EC RID: 17644
		private const float MASS_VARIATION = 0.2f;

		// Token: 0x040044F1 RID: 17649
		public List<KeyValuePair<int, Tag>> terrainPositions;

		// Token: 0x040044F2 RID: 17650
		public List<KeyValuePair<int, Tag>> poi;

		// Token: 0x040044F3 RID: 17651
		public List<int> neighbourTerrainCells = new List<int>();

		// Token: 0x040044F4 RID: 17652
		private float finalSize;

		// Token: 0x040044F5 RID: 17653
		private bool debugMode;

		// Token: 0x040044F6 RID: 17654
		private List<int> allCells;

		// Token: 0x040044F7 RID: 17655
		private HashSet<int> availableTerrainPoints;

		// Token: 0x040044F8 RID: 17656
		private HashSet<int> featureSpawnPoints;

		// Token: 0x040044F9 RID: 17657
		private HashSet<int> availableSpawnPoints;

		// Token: 0x040044FA RID: 17658
		public const int DONT_SET_TEMPERATURE_DEFAULTS = -1;

		// Token: 0x040044FB RID: 17659
		private static readonly Tag[] noPOISpawnTags = new Tag[]
		{
			WorldGenTags.StartLocation,
			WorldGenTags.AtStart,
			WorldGenTags.NearStartLocation,
			WorldGenTags.POI,
			WorldGenTags.Feature
		};

		// Token: 0x040044FC RID: 17660
		private static readonly TagSet noPOISpawnTagSet = new TagSet(TerrainCell.noPOISpawnTags);

		// Token: 0x040044FD RID: 17661
		private static readonly Tag[] relaxedNoPOISpawnTags = new Tag[]
		{
			WorldGenTags.StartLocation,
			WorldGenTags.AtStart,
			WorldGenTags.NearStartLocation,
			WorldGenTags.POI
		};

		// Token: 0x040044FE RID: 17662
		private static readonly TagSet relaxedNoPOISpawnTagSet = new TagSet(TerrainCell.relaxedNoPOISpawnTags);

		// Token: 0x02001AE4 RID: 6884
		// (Invoke) Token: 0x06009453 RID: 37971
		public delegate void SetValuesFunction(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc);

		// Token: 0x02001AE5 RID: 6885
		public struct ElementOverride
		{
			// Token: 0x040078FF RID: 30975
			public Element element;

			// Token: 0x04007900 RID: 30976
			public Sim.PhysicsData pdelement;

			// Token: 0x04007901 RID: 30977
			public Sim.DiseaseCell dc;

			// Token: 0x04007902 RID: 30978
			public float mass;

			// Token: 0x04007903 RID: 30979
			public float temperature;

			// Token: 0x04007904 RID: 30980
			public byte diseaseIdx;

			// Token: 0x04007905 RID: 30981
			public int diseaseAmount;

			// Token: 0x04007906 RID: 30982
			public bool overrideMass;

			// Token: 0x04007907 RID: 30983
			public bool overrideTemperature;

			// Token: 0x04007908 RID: 30984
			public bool overrideDiseaseIdx;

			// Token: 0x04007909 RID: 30985
			public bool overrideDiseaseAmount;
		}
	}
}
