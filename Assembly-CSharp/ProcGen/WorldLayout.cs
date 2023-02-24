using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Delaunay.Geo;
using KSerialization;
using ObjectCloner;
using ProcGen.Map;
using ProcGenGame;
using UnityEngine;
using VoronoiTree;

namespace ProcGen
{
	// Token: 0x02000C3B RID: 3131
	[SerializationConfig(MemberSerialization.OptIn)]
	public class WorldLayout
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060062FE RID: 25342 RVA: 0x002499DF File Offset: 0x00247BDF
		// (set) Token: 0x060062FF RID: 25343 RVA: 0x002499E7 File Offset: 0x00247BE7
		[Serialize]
		public int mapWidth { get; private set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06006300 RID: 25344 RVA: 0x002499F0 File Offset: 0x00247BF0
		// (set) Token: 0x06006301 RID: 25345 RVA: 0x002499F8 File Offset: 0x00247BF8
		[Serialize]
		public int mapHeight { get; private set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06006302 RID: 25346 RVA: 0x00249A01 File Offset: 0x00247C01
		// (set) Token: 0x06006303 RID: 25347 RVA: 0x00249A09 File Offset: 0x00247C09
		public bool layoutOK { get; private set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06006304 RID: 25348 RVA: 0x00249A12 File Offset: 0x00247C12
		// (set) Token: 0x06006305 RID: 25349 RVA: 0x00249A19 File Offset: 0x00247C19
		public static LevelLayer levelLayerGradient { get; private set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06006306 RID: 25350 RVA: 0x00249A21 File Offset: 0x00247C21
		// (set) Token: 0x06006307 RID: 25351 RVA: 0x00249A29 File Offset: 0x00247C29
		public WorldGen worldGen { get; private set; }

		// Token: 0x06006308 RID: 25352 RVA: 0x00249A32 File Offset: 0x00247C32
		public WorldLayout(WorldGen worldGen, int seed)
		{
			this.worldGen = worldGen;
			this.localGraph = new MapGraph(seed);
			this.overworldGraph = new MapGraph(seed);
			this.SetSeed(seed);
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00249A60 File Offset: 0x00247C60
		public WorldLayout(WorldGen worldGen, int width, int height, int seed)
			: this(worldGen, seed)
		{
			this.mapWidth = width;
			this.mapHeight = height;
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00249A79 File Offset: 0x00247C79
		public void SetSeed(int seed)
		{
			this.myRandom = new SeededRandom(seed);
			this.localGraph.SetSeed(seed);
			this.overworldGraph.SetSeed(seed);
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x00249A9F File Offset: 0x00247C9F
		public Tree GetVoronoiTree()
		{
			return this.voronoiTree;
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x00249AA7 File Offset: 0x00247CA7
		public static void SetLayerGradient(LevelLayer newGradient)
		{
			WorldLayout.levelLayerGradient = newGradient;
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x00249AB0 File Offset: 0x00247CB0
		public static string GetNodeTypeFromLayers(Vector2 point, float mapHeight, SeededRandom rnd)
		{
			string text = WorldGenTags.TheVoid.Name;
			int num = rnd.RandomRange(0, WorldLayout.levelLayerGradient[WorldLayout.levelLayerGradient.Count - 1].content.Count);
			text = WorldLayout.levelLayerGradient[WorldLayout.levelLayerGradient.Count - 1].content[num];
			for (int i = 0; i < WorldLayout.levelLayerGradient.Count; i++)
			{
				if (point.y < WorldLayout.levelLayerGradient[i].maxValue * mapHeight)
				{
					int num2 = rnd.RandomRange(0, WorldLayout.levelLayerGradient[i].content.Count);
					text = WorldLayout.levelLayerGradient[i].content[num2];
					break;
				}
			}
			return text;
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x00249B80 File Offset: 0x00247D80
		public Tree GenerateOverworld(bool usePD, bool isRunningDebugGen)
		{
			global::Debug.Assert(this.mapWidth != 0 && this.mapHeight != 0, "Map size has not been set");
			global::Debug.Assert(this.worldGen.Settings.world != null, "You need to set a world");
			Diagram.Site site = new Diagram.Site(0U, new Vector2((float)(this.mapWidth / 2), (float)(this.mapHeight / 2)), 1f);
			this.topEdge = new LineSegment(new Vector2?(new Vector2(0f, (float)(this.mapHeight - 5))), new Vector2?(new Vector2((float)this.mapWidth, (float)(this.mapHeight - 5))));
			this.bottomEdge = new LineSegment(new Vector2?(new Vector2(0f, 5f)), new Vector2?(new Vector2((float)this.mapWidth, 5f)));
			this.leftEdge = new LineSegment(new Vector2?(new Vector2(5f, 0f)), new Vector2?(new Vector2(5f, (float)this.mapHeight)));
			this.rightEdge = new LineSegment(new Vector2?(new Vector2((float)(this.mapWidth - 5), 0f)), new Vector2?(new Vector2((float)(this.mapWidth - 5), (float)this.mapHeight)));
			site.poly = new Polygon(new Rect(0f, 0f, (float)this.mapWidth, (float)this.mapHeight));
			this.voronoiTree = new Tree(site, null, this.myRandom.seed);
			Node.maxIndex = 0U;
			float floatSetting = this.worldGen.Settings.GetFloatSetting("OverworldDensityMin");
			float floatSetting2 = this.worldGen.Settings.GetFloatSetting("OverworldDensityMax");
			float num = this.myRandom.RandomRange(floatSetting, floatSetting2);
			float floatSetting3 = this.worldGen.Settings.GetFloatSetting("OverworldAvoidRadius");
			PointGenerator.SampleBehaviour enumSetting = this.worldGen.Settings.GetEnumSetting<PointGenerator.SampleBehaviour>("OverworldSampleBehaviour");
			Cell cell = null;
			if (!string.IsNullOrEmpty(this.worldGen.Settings.world.startSubworldName))
			{
				WeightedSubworldName weightedSubworldName = this.worldGen.Settings.world.subworldFiles.Find((WeightedSubworldName x) => x.name == this.worldGen.Settings.world.startSubworldName);
				global::Debug.Assert(weightedSubworldName != null, "The start subworld must be listed in the subworld files for a world.");
				Vector2 vector = new Vector2((float)this.mapWidth * this.worldGen.Settings.world.startingBasePositionHorizontal.GetRandomValueWithinRange(this.myRandom), (float)this.mapHeight * this.worldGen.Settings.world.startingBasePositionVertical.GetRandomValueWithinRange(this.myRandom));
				cell = this.overworldGraph.AddNode(weightedSubworldName.name, vector);
				SubWorld subWorld = this.worldGen.Settings.GetSubWorld(weightedSubworldName.name);
				float num2 = ((weightedSubworldName.overridePower > 0f) ? weightedSubworldName.overridePower : subWorld.pdWeight);
				Node node = this.voronoiTree.AddSite(new Diagram.Site((uint)cell.NodeId, cell.position, num2), Node.NodeType.Internal);
				node.AddTag(WorldGenTags.AtStart);
				this.ApplySubworldToNode(node, subWorld, num2);
			}
			List<Vector2> list = new List<Vector2>();
			if (cell != null)
			{
				list.Add(cell.position);
			}
			List<Vector2> randomPoints = PointGenerator.GetRandomPoints(site.poly, num, floatSetting3, list, enumSetting, false, this.myRandom, false, true);
			int intSetting = this.worldGen.Settings.GetIntSetting("OverworldMinNodes");
			int intSetting2 = this.worldGen.Settings.GetIntSetting("OverworldMaxNodes");
			if (randomPoints.Count > intSetting2)
			{
				randomPoints.ShuffleSeeded(this.myRandom.RandomSource());
				randomPoints.RemoveRange(intSetting2, randomPoints.Count - intSetting2);
			}
			if (randomPoints.Count < intSetting)
			{
				throw new Exception(string.Format("World layout with fewer than {0} points.", intSetting));
			}
			for (int i = 0; i < randomPoints.Count; i++)
			{
				Cell cell2 = this.overworldGraph.AddNode(WorldGenTags.UnassignedNode.Name, randomPoints[i]);
				this.voronoiTree.AddSite(new Diagram.Site((uint)cell2.NodeId, cell2.position, 1f), Node.NodeType.Internal).tags.Add(WorldGenTags.UnassignedNode);
				cell2.tags.Add(WorldGenTags.UnassignedNode);
			}
			List<Diagram.Site> list2 = new List<Diagram.Site>();
			for (int j = 0; j < this.voronoiTree.ChildCount(); j++)
			{
				list2.Add(this.voronoiTree.GetChild(j).site);
			}
			if (usePD)
			{
				this.voronoiTree.ComputeNode(list2);
				this.voronoiTree.ComputeNodePD(list2, 500, 0.2f);
			}
			else
			{
				this.voronoiTree.ComputeChildren(this.myRandom.seed + 1, false, false);
			}
			this.voronoiTree.VisitAll(delegate(Node n)
			{
				global::Debug.Assert(n.site.poly != null, string.Format("Node {0} had a null poly after initial overworld compute!!", n.site.id));
			});
			this.voronoiTree.AddTagToChildren(WorldGenTags.Overworld);
			this.TagTopAndBottomSites(WorldGenTags.AtSurface, WorldGenTags.AtDepths);
			this.TagEdgeSites(WorldGenTags.AtEdge, WorldGenTags.AtEdge);
			WorldLayout.ResetMapGraphFromVoronoiTree(this.voronoiTree.ImmediateChildren(), this.overworldGraph, true);
			this.PropagateDistanceTags(this.voronoiTree, WorldGenTags.DistanceTags);
			this.ConvertUnknownCells(this.myRandom, isRunningDebugGen);
			if (this.worldGen.Settings.GetOverworldAddTags() != null)
			{
				foreach (string text in this.worldGen.Settings.GetOverworldAddTags())
				{
					int num3 = this.myRandom.RandomSource().Next(this.voronoiTree.ChildCount());
					this.voronoiTree.GetChild(num3).AddTag(new Tag(text));
				}
			}
			if (usePD)
			{
				this.voronoiTree.ComputeNodePD(list2, 500, 0.2f);
			}
			this.voronoiTree.VisitAll(delegate(Node n)
			{
				global::Debug.Assert(n.site.poly != null, string.Format("Node {0} had a null poly after final overworld compute!!", n.site.id));
			});
			this.FlattenOverworld();
			return this.voronoiTree;
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x0024A1EC File Offset: 0x002483EC
		public static void ResetMapGraphFromVoronoiTree(List<Node> nodes, MapGraph graph, bool clear)
		{
			if (clear)
			{
				graph.ClearEdgesAndCorners();
			}
			for (int i = 0; i < nodes.Count; i++)
			{
				Node node = nodes[i];
				Cell cell = graph.FindNodeByID(node.site.id);
				cell.tags.Union(node.tags);
				cell.SetPosition(node.site.position);
				foreach (Node node2 in node.GetNeighbors())
				{
					Cell cell2 = graph.FindNodeByID(node2.site.id);
					if (graph.GetArc(cell, cell2) == null)
					{
						int num = -1;
						LineSegment lineSegment;
						if (node.site.poly.SharesEdge(node2.site.poly, ref num, out lineSegment) == Polygon.Commonality.Edge)
						{
							Corner corner = graph.AddOrGetCorner(lineSegment.p0.Value);
							Corner corner2 = graph.AddOrGetCorner(lineSegment.p1.Value);
							graph.AddOrGetEdge(cell, cell2, corner, corner2);
						}
					}
				}
			}
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x0024A318 File Offset: 0x00248518
		public void PopulateSubworlds()
		{
			this.AddSubworldChildren();
			this.GetStartLocation();
			this.PropagateStartTag();
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x0024A330 File Offset: 0x00248530
		private void PropagateDistanceTags(Tree tree, TagSet tags)
		{
			foreach (Tag tag in tags)
			{
				Dictionary<uint, int> distanceToTag = this.overworldGraph.GetDistanceToTag(tag);
				if (distanceToTag != null)
				{
					int num = 0;
					for (int i = 0; i < tree.ChildCount(); i++)
					{
						Node child = tree.GetChild(i);
						uint id = child.site.id;
						if (distanceToTag.ContainsKey(id))
						{
							child.minDistanceToTag.Add(tag, distanceToTag[id]);
							num++;
							if (distanceToTag[id] > 0)
							{
								child.AddTag(new Tag(tag.Name + "_Distance" + distanceToTag[id].ToString()));
							}
						}
					}
				}
			}
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x0024A418 File Offset: 0x00248618
		private HashSet<WeightedSubWorld> GetNameFilterSet(Node vn, World.AllowedCellsFilter filter, List<WeightedSubWorld> subworlds)
		{
			HashSet<WeightedSubWorld> hashSet = new HashSet<WeightedSubWorld>();
			switch (filter.tagcommand)
			{
			case World.AllowedCellsFilter.TagCommand.Default:
			{
				int num;
				int j;
				for (j = 0; j < filter.subworldNames.Count; j = num + 1)
				{
					hashSet.UnionWith(subworlds.FindAll((WeightedSubWorld f) => f.subWorld.name == filter.subworldNames[j]));
					num = j;
				}
				break;
			}
			case World.AllowedCellsFilter.TagCommand.AtTag:
				if (vn.tags.Contains(filter.tag))
				{
					int num;
					int k;
					for (k = 0; k < filter.subworldNames.Count; k = num + 1)
					{
						hashSet.UnionWith(subworlds.FindAll((WeightedSubWorld f) => f.subWorld.name == filter.subworldNames[k]));
						num = k;
					}
				}
				break;
			case World.AllowedCellsFilter.TagCommand.NotAtTag:
				if (!vn.tags.Contains(filter.tag))
				{
					int num;
					int l;
					for (l = 0; l < filter.subworldNames.Count; l = num + 1)
					{
						hashSet.UnionWith(subworlds.FindAll((WeightedSubWorld f) => f.subWorld.name == filter.subworldNames[l]));
						num = l;
					}
				}
				break;
			case World.AllowedCellsFilter.TagCommand.DistanceFromTag:
			{
				bool flag = vn.minDistanceToTag.ContainsKey(filter.tag.ToTag());
				global::Debug.Assert(flag || filter.optional, "DistanceFromTag is missing tag " + filter.tag + ", consider marking the filter optional.");
				if (flag && vn.minDistanceToTag[filter.tag.ToTag()] >= filter.minDistance && vn.minDistanceToTag[filter.tag.ToTag()] <= filter.maxDistance)
				{
					int num;
					int i;
					for (i = 0; i < filter.subworldNames.Count; i = num + 1)
					{
						hashSet.UnionWith(subworlds.FindAll((WeightedSubWorld f) => f.subWorld.name == filter.subworldNames[i]));
						num = i;
					}
				}
				break;
			}
			}
			return hashSet;
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x0024A6E0 File Offset: 0x002488E0
		private HashSet<WeightedSubWorld> GetZoneTypeFilterSet(Node vn, World.AllowedCellsFilter filter, Dictionary<string, List<WeightedSubWorld>> subworldsByZoneType)
		{
			HashSet<WeightedSubWorld> hashSet = new HashSet<WeightedSubWorld>();
			switch (filter.tagcommand)
			{
			case World.AllowedCellsFilter.TagCommand.Default:
			{
				for (int i = 0; i < filter.zoneTypes.Count; i++)
				{
					hashSet.UnionWith(subworldsByZoneType[filter.zoneTypes[i].ToString()]);
				}
				break;
			}
			case World.AllowedCellsFilter.TagCommand.AtTag:
				if (vn.tags.Contains(filter.tag))
				{
					for (int j = 0; j < filter.zoneTypes.Count; j++)
					{
						hashSet.UnionWith(subworldsByZoneType[filter.zoneTypes[j].ToString()]);
					}
				}
				break;
			case World.AllowedCellsFilter.TagCommand.NotAtTag:
				if (!vn.tags.Contains(filter.tag))
				{
					for (int k = 0; k < filter.zoneTypes.Count; k++)
					{
						hashSet.UnionWith(subworldsByZoneType[filter.zoneTypes[k].ToString()]);
					}
				}
				break;
			case World.AllowedCellsFilter.TagCommand.DistanceFromTag:
				global::Debug.Assert(vn.minDistanceToTag.ContainsKey(filter.tag.ToTag()), filter.tag);
				if (vn.minDistanceToTag[filter.tag.ToTag()] >= filter.minDistance && vn.minDistanceToTag[filter.tag.ToTag()] <= filter.maxDistance)
				{
					for (int l = 0; l < filter.zoneTypes.Count; l++)
					{
						hashSet.UnionWith(subworldsByZoneType[filter.zoneTypes[l].ToString()]);
					}
				}
				break;
			}
			return hashSet;
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x0024A8B8 File Offset: 0x00248AB8
		private HashSet<WeightedSubWorld> GetTemperatureFilterSet(Node vn, World.AllowedCellsFilter filter, Dictionary<string, List<WeightedSubWorld>> subworldsByTemperature)
		{
			HashSet<WeightedSubWorld> hashSet = new HashSet<WeightedSubWorld>();
			switch (filter.tagcommand)
			{
			case World.AllowedCellsFilter.TagCommand.Default:
			{
				for (int i = 0; i < filter.temperatureRanges.Count; i++)
				{
					hashSet.UnionWith(subworldsByTemperature[filter.temperatureRanges[i].ToString()]);
				}
				break;
			}
			case World.AllowedCellsFilter.TagCommand.AtTag:
				if (vn.tags.Contains(filter.tag))
				{
					for (int j = 0; j < filter.temperatureRanges.Count; j++)
					{
						hashSet.UnionWith(subworldsByTemperature[filter.temperatureRanges[j].ToString()]);
					}
				}
				break;
			case World.AllowedCellsFilter.TagCommand.NotAtTag:
				if (!vn.tags.Contains(filter.tag))
				{
					for (int k = 0; k < filter.temperatureRanges.Count; k++)
					{
						hashSet.UnionWith(subworldsByTemperature[filter.temperatureRanges[k].ToString()]);
					}
				}
				break;
			case World.AllowedCellsFilter.TagCommand.DistanceFromTag:
				global::Debug.Assert(vn.minDistanceToTag.ContainsKey(filter.tag.ToTag()), filter.tag);
				if (vn.minDistanceToTag[filter.tag.ToTag()] >= filter.minDistance && vn.minDistanceToTag[filter.tag.ToTag()] <= filter.maxDistance)
				{
					for (int l = 0; l < filter.temperatureRanges.Count; l++)
					{
						hashSet.UnionWith(subworldsByTemperature[filter.temperatureRanges[l].ToString()]);
					}
				}
				break;
			}
			return hashSet;
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x0024AA90 File Offset: 0x00248C90
		private void RunFilterClearCommand(Node vn, World.AllowedCellsFilter filter, HashSet<WeightedSubWorld> allowedSubworldsSet)
		{
			switch (filter.tagcommand)
			{
			case World.AllowedCellsFilter.TagCommand.Default:
				allowedSubworldsSet.Clear();
				return;
			case World.AllowedCellsFilter.TagCommand.AtTag:
				if (vn.tags.Contains(filter.tag))
				{
					allowedSubworldsSet.Clear();
					return;
				}
				break;
			case World.AllowedCellsFilter.TagCommand.NotAtTag:
				if (!vn.tags.Contains(filter.tag))
				{
					allowedSubworldsSet.Clear();
					return;
				}
				break;
			case World.AllowedCellsFilter.TagCommand.DistanceFromTag:
				global::Debug.Assert(vn.minDistanceToTag.ContainsKey(filter.tag.ToTag()), filter.tag);
				if (vn.minDistanceToTag[filter.tag.ToTag()] >= filter.minDistance && vn.minDistanceToTag[filter.tag.ToTag()] <= filter.maxDistance)
				{
					allowedSubworldsSet.Clear();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0024AB68 File Offset: 0x00248D68
		private HashSet<WeightedSubWorld> Filter(Node vn, List<WeightedSubWorld> allSubWorlds, Dictionary<string, List<WeightedSubWorld>> subworldsByTemperature, Dictionary<string, List<WeightedSubWorld>> subworldsByZoneType)
		{
			HashSet<WeightedSubWorld> hashSet = new HashSet<WeightedSubWorld>();
			World world = this.worldGen.Settings.world;
			string text = "";
			foreach (KeyValuePair<Tag, int> keyValuePair in vn.minDistanceToTag)
			{
				text = string.Concat(new string[]
				{
					text,
					keyValuePair.Key.Name,
					":",
					keyValuePair.Value.ToString(),
					", "
				});
			}
			foreach (World.AllowedCellsFilter allowedCellsFilter in world.unknownCellsAllowedSubworlds)
			{
				HashSet<WeightedSubWorld> hashSet2 = new HashSet<WeightedSubWorld>();
				if (allowedCellsFilter.subworldNames != null && allowedCellsFilter.subworldNames.Count > 0)
				{
					hashSet2.UnionWith(this.GetNameFilterSet(vn, allowedCellsFilter, allSubWorlds));
				}
				if (allowedCellsFilter.temperatureRanges != null && allowedCellsFilter.temperatureRanges.Count > 0)
				{
					hashSet2.UnionWith(this.GetTemperatureFilterSet(vn, allowedCellsFilter, subworldsByTemperature));
				}
				if (allowedCellsFilter.zoneTypes != null && allowedCellsFilter.zoneTypes.Count > 0)
				{
					hashSet2.UnionWith(this.GetZoneTypeFilterSet(vn, allowedCellsFilter, subworldsByZoneType));
				}
				switch (allowedCellsFilter.command)
				{
				case World.AllowedCellsFilter.Command.Clear:
					this.RunFilterClearCommand(vn, allowedCellsFilter, hashSet);
					break;
				case World.AllowedCellsFilter.Command.Replace:
					if (hashSet2.Count > 0)
					{
						hashSet.Clear();
						hashSet.UnionWith(hashSet2);
					}
					break;
				case World.AllowedCellsFilter.Command.UnionWith:
					hashSet.UnionWith(hashSet2);
					break;
				case World.AllowedCellsFilter.Command.IntersectWith:
					hashSet.IntersectWith(hashSet2);
					break;
				case World.AllowedCellsFilter.Command.ExceptWith:
					hashSet.ExceptWith(hashSet2);
					break;
				case World.AllowedCellsFilter.Command.SymmetricExceptWith:
					hashSet.SymmetricExceptWith(hashSet2);
					break;
				case World.AllowedCellsFilter.Command.All:
					global::Debug.LogError("Command.All is unsupported for unknownCellsAllowedSubworlds.");
					break;
				}
			}
			return hashSet;
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x0024AD80 File Offset: 0x00248F80
		private void ConvertUnknownCells(SeededRandom myRandom, bool isRunningDebugGen)
		{
			List<Node> list = new List<Node>();
			this.voronoiTree.GetNodesWithTag(WorldGenTags.UnassignedNode, list);
			list.ShuffleSeeded(myRandom.RandomSource());
			List<WeightedSubworldName> list2 = new List<WeightedSubworldName>(this.worldGen.Settings.world.subworldFiles);
			List<WeightedSubWorld> subworldsForWorld = this.worldGen.Settings.GetSubworldsForWorld(list2);
			Dictionary<string, List<WeightedSubWorld>> dictionary = new Dictionary<string, List<WeightedSubWorld>>();
			using (IEnumerator enumerator = Enum.GetValues(typeof(Temperature.Range)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Temperature.Range range = (Temperature.Range)enumerator.Current;
					dictionary.Add(range.ToString(), subworldsForWorld.FindAll((WeightedSubWorld sw) => sw.subWorld.temperatureRange == range));
				}
			}
			Dictionary<string, List<WeightedSubWorld>> dictionary2 = new Dictionary<string, List<WeightedSubWorld>>();
			using (IEnumerator enumerator = Enum.GetValues(typeof(SubWorld.ZoneType)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SubWorld.ZoneType zt = (SubWorld.ZoneType)enumerator.Current;
					dictionary2.Add(zt.ToString(), subworldsForWorld.FindAll((WeightedSubWorld sw) => sw.subWorld.zoneType == zt));
				}
			}
			foreach (Node node in list)
			{
				Node node2 = this.overworldGraph.FindNodeByID(node.site.id);
				node.tags.Remove(WorldGenTags.UnassignedNode);
				node2.tags.Remove(WorldGenTags.UnassignedNode);
				List<WeightedSubWorld> list3 = new List<WeightedSubWorld>(this.Filter(node, subworldsForWorld, dictionary, dictionary2));
				List<WeightedSubWorld> list4 = list3.FindAll((WeightedSubWorld x) => x.minCount > 0);
				WeightedSubWorld weightedSubWorld;
				if (list4.Count > 0)
				{
					weightedSubWorld = list4[0];
					int num = weightedSubWorld.priority;
					foreach (WeightedSubWorld weightedSubWorld2 in list4)
					{
						if (weightedSubWorld2.priority > num || (weightedSubWorld2.priority == num && weightedSubWorld2.minCount > weightedSubWorld.minCount))
						{
							weightedSubWorld = weightedSubWorld2;
							num = weightedSubWorld2.priority;
						}
					}
					WeightedSubWorld weightedSubWorld3 = weightedSubWorld;
					int num2 = weightedSubWorld3.minCount;
					weightedSubWorld3.minCount = num2 - 1;
				}
				else
				{
					weightedSubWorld = WeightedRandom.Choose<WeightedSubWorld>(list3, myRandom);
				}
				if (weightedSubWorld != null)
				{
					this.ApplySubworldToNode(node, weightedSubWorld.subWorld, weightedSubWorld.overridePower);
					WeightedSubWorld weightedSubWorld4 = weightedSubWorld;
					int num2 = weightedSubWorld4.maxCount;
					weightedSubWorld4.maxCount = num2 - 1;
					if (weightedSubWorld.maxCount <= 0)
					{
						subworldsForWorld.Remove(weightedSubWorld);
					}
				}
				else
				{
					string text = "";
					foreach (KeyValuePair<Tag, int> keyValuePair in node.minDistanceToTag)
					{
						text = string.Concat(new string[]
						{
							text,
							keyValuePair.Key.Name,
							":",
							keyValuePair.Value.ToString(),
							", "
						});
					}
					DebugUtil.LogWarningArgs(new object[]
					{
						"No allowed Subworld types. Using default. ",
						node2.tags.ToString(),
						"Distances:",
						text
					});
					node2.SetType("Default");
				}
			}
			foreach (WeightedSubWorld weightedSubWorld5 in subworldsForWorld)
			{
				if (weightedSubWorld5.minCount > 0)
				{
					if (!isRunningDebugGen)
					{
						throw new Exception(string.Format("Could not guarantee minCount of Subworld {0}, {1} remaining on world {2}.", weightedSubWorld5.subWorld.name, weightedSubWorld5.minCount, this.worldGen.Settings.world.filePath));
					}
					DebugUtil.DevLogError(string.Format("Could not guarantee minCount of Subworld {0}, {1} remaining on world {2}.", weightedSubWorld5.subWorld.name, weightedSubWorld5.minCount, this.worldGen.Settings.world.filePath));
				}
			}
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x0024B284 File Offset: 0x00249484
		private Node ApplySubworldToNode(Node vn, SubWorld subWorld, float overridePower = -1f)
		{
			Node node = this.overworldGraph.FindNodeByID(vn.site.id);
			node.SetType(subWorld.name);
			vn.site.weight = ((overridePower > 0f) ? overridePower : subWorld.pdWeight);
			foreach (string text in subWorld.tags)
			{
				vn.AddTag(new Tag(text));
			}
			vn.AddTag(subWorld.zoneType.ToString());
			return node;
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x0024B33C File Offset: 0x0024953C
		private void FlattenOverworld()
		{
			try
			{
				WorldLayout.ResetMapGraphFromVoronoiTree(this.voronoiTree.ImmediateChildren(), this.overworldGraph, true);
				foreach (Edge edge in this.overworldGraph.arcs)
				{
					List<Cell> nodes = this.overworldGraph.GetNodes(edge);
					Cell cell = nodes[0];
					Cell cell2 = nodes[1];
					SubWorld subWorld = this.worldGen.Settings.GetSubWorld(cell.type);
					global::Debug.Assert(subWorld != null, "SubWorld is null: " + cell.type);
					SubWorld subWorld2 = this.worldGen.Settings.GetSubWorld(cell2.type);
					global::Debug.Assert(subWorld2 != null, "other SubWorld is null: " + cell2.type);
					if (cell.type == cell2.type || subWorld.zoneType == subWorld2.zoneType)
					{
						edge.tags.Add(WorldGenTags.EdgeOpen);
					}
					else if (subWorld.borderOverride == "NONE" || subWorld2.borderOverride == "NONE")
					{
						edge.tags.Add(WorldGenTags.EdgeOpen);
					}
					else
					{
						edge.tags.Add(WorldGenTags.EdgeClosed);
					}
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				global::Debug.LogError("ex: " + message + " " + stackTrace);
			}
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x0024B4F8 File Offset: 0x002496F8
		public static bool TestEdgeConsistency(MapGraph graph, Cell cell, out Edge problemEdge)
		{
			List<Edge> arcs = graph.GetArcs(cell);
			foreach (Edge edge in arcs)
			{
				int num = 0;
				int num2 = 0;
				foreach (Edge edge2 in arcs)
				{
					if (edge2.corner0 == edge.corner0 || edge2.corner1 == edge.corner0)
					{
						num++;
					}
					if (edge2.corner1 == edge.corner1 || edge2.corner1 == edge.corner1)
					{
						num2++;
					}
				}
				if (num != 2 || num2 != 2)
				{
					problemEdge = edge;
					return false;
				}
			}
			problemEdge = null;
			return true;
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x0024B5E8 File Offset: 0x002497E8
		private void AddSubworldChildren()
		{
			new TagSet().Add(WorldGenTags.Overworld);
			List<string> defaultMoveTags = this.worldGen.Settings.GetDefaultMoveTags();
			if (defaultMoveTags != null)
			{
				new TagSet(defaultMoveTags);
			}
			List<Feature> list = new List<Feature>();
			foreach (KeyValuePair<string, int> keyValuePair in this.worldGen.Settings.world.globalFeatures)
			{
				for (int i = 0; i < keyValuePair.Value; i++)
				{
					list.Add(new Feature
					{
						type = keyValuePair.Key
					});
				}
			}
			Dictionary<uint, List<Feature>> dictionary = new Dictionary<uint, List<Feature>>();
			List<Node> list2 = new List<Node>();
			this.voronoiTree.GetNodesWithoutTag(WorldGenTags.NoGlobalFeatureSpawning, list2);
			list2.ShuffleSeeded(this.myRandom.RandomSource());
			foreach (Feature feature in list)
			{
				if (list2.Count == 0)
				{
					break;
				}
				Node node = list2[0];
				list2.RemoveAt(0);
				if (!dictionary.ContainsKey(node.site.id))
				{
					dictionary[node.site.id] = new List<Feature>();
				}
				dictionary[node.site.id].Add(feature);
			}
			this.localGraph.ClearEdgesAndCorners();
			for (int j = 0; j < this.voronoiTree.ChildCount(); j++)
			{
				Node child2 = this.voronoiTree.GetChild(j);
				if (child2.type == Node.NodeType.Internal)
				{
					Tree child = child2 as Tree;
					Node node2 = this.overworldGraph.FindNodeByID(child.site.id);
					SubWorld subWorld = SerializingCloner.Copy<SubWorld>(this.worldGen.Settings.GetSubWorld(node2.type));
					child.AddTag(new Tag(node2.type));
					child.AddTag(new Tag(subWorld.temperatureRange.ToString()));
					child.AddTag(new Tag(subWorld.zoneType.ToString()));
					if (dictionary.ContainsKey(child2.site.id))
					{
						subWorld.features.AddRange(dictionary[child2.site.id]);
					}
					this.GenerateChildren(subWorld, child, this.localGraph, (float)this.mapHeight, j + this.myRandom.seed);
					child.RelaxRecursive(0, 10, 1f, this.worldGen.Settings.world.layoutMethod == World.LayoutMethod.PowerTree);
					child.VisitAll(delegate(Node n)
					{
						global::Debug.Assert(n.site.poly != null, string.Format("Node {0}, child of {1} had a null poly after final subworld relax!!", n.site.id, child.site.id));
					});
				}
			}
			Node.maxDepth = this.voronoiTree.MaxDepth(0);
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x0024B91C File Offset: 0x00249B1C
		private List<Vector2> GetPoints(string name, LoggerSSF log, int minPointCount, int maxPointCount, Polygon boundingArea, float density, float avoidRadius, List<Vector2> avoidPoints, PointGenerator.SampleBehaviour sampleBehaviour, bool testInsideBounds, SeededRandom rnd, bool doShuffle = true, bool testAvoidPoints = true)
		{
			int num = 0;
			List<Vector2> randomPoints;
			do
			{
				randomPoints = PointGenerator.GetRandomPoints(boundingArea, density, avoidRadius, avoidPoints, sampleBehaviour, testInsideBounds, rnd, doShuffle, testAvoidPoints);
				if (randomPoints.Count < minPointCount)
				{
					density *= 0.8f;
					avoidRadius *= 0.8f;
					bool isRunningDebugGen = this.worldGen.isRunningDebugGen;
				}
				num++;
			}
			while (randomPoints.Count < minPointCount && randomPoints.Count <= maxPointCount && num < 10);
			if (randomPoints.Count > maxPointCount)
			{
				randomPoints.RemoveRange(maxPointCount, randomPoints.Count - maxPointCount);
			}
			return randomPoints;
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x0024B9A8 File Offset: 0x00249BA8
		public void GenerateChildren(SubWorld sw, Tree node, MapGraph graph, float worldHeight, int seed)
		{
			SeededRandom seededRandom = new SeededRandom(seed);
			List<string> defaultMoveTags = this.worldGen.Settings.GetDefaultMoveTags();
			TagSet tagSet = ((defaultMoveTags != null) ? new TagSet(defaultMoveTags) : null);
			TagSet tagSet2 = new TagSet();
			if (tagSet != null)
			{
				for (int i = 0; i < tagSet.Count; i++)
				{
					Tag tag = tagSet[i];
					if (node.tags.Contains(tag))
					{
						node.tags.Remove(tag);
						tagSet2.Add(tag);
					}
				}
			}
			TagSet tagSet3 = new TagSet(node.tags);
			tagSet3.Remove(WorldGenTags.Overworld);
			for (int j = 0; j < sw.tags.Count; j++)
			{
				tagSet3.Add(new Tag(sw.tags[j]));
			}
			float randomValueWithinRange = sw.density.GetRandomValueWithinRange(seededRandom);
			List<Vector2> list = new List<Vector2>();
			if (sw.centralFeature != null)
			{
				list.Add(node.site.poly.Centroid());
				this.CreateTreeNodeWithFeatureAndBiome(this.worldGen.Settings, sw, node, graph, sw.centralFeature, node.site.poly.Centroid(), tagSet3, -1).AddTag(WorldGenTags.CenteralFeature);
			}
			node.dontRelaxChildren = sw.dontRelaxChildren;
			int num = Mathf.Max(sw.features.Count + sw.extraBiomeChildren, sw.minChildCount);
			int num2 = int.MaxValue;
			if (sw.singleChildCount)
			{
				num = 1;
				num2 = 1;
			}
			List<Vector2> points = this.GetPoints(sw.name, node.log, num, num2, node.site.poly, randomValueWithinRange, sw.avoidRadius, list, sw.sampleBehaviour, true, seededRandom, true, sw.doAvoidPoints);
			global::Debug.Assert(points.Count >= num, string.Format("Overworld node {0} of subworld {1} generated {2} points of an expected minimum {3}\nThis probably means that either:\n* sampler density is too large (lower the number for tighter samples)\n* avoid radius is too large (only applies if there is a central feature, especialy if you get 0 points generated)\n* min point count is just plain too large.", new object[]
			{
				node.site.id,
				sw.name,
				points.Count,
				num
			}));
			for (int k = 0; k < sw.samplers.Count; k++)
			{
				list.AddRange(points);
				float randomValueWithinRange2 = sw.samplers[k].density.GetRandomValueWithinRange(seededRandom);
				List<Vector2> randomPoints = PointGenerator.GetRandomPoints(node.site.poly, randomValueWithinRange2, sw.samplers[k].avoidRadius, list, sw.samplers[k].sampleBehaviour, true, seededRandom, true, sw.samplers[k].doAvoidPoints);
				points.AddRange(randomPoints);
			}
			if (points.Count > 200)
			{
				points.RemoveRange(200, points.Count - 200);
			}
			if (points.Count < num)
			{
				string text = "";
				for (int l = 0; l < node.site.poly.Vertices.Count; l++)
				{
					text = text + node.site.poly.Vertices[l].ToString() + ", ";
				}
				if (this.worldGen.isRunningDebugGen)
				{
					global::Debug.Assert(points.Count >= num, "Error not enough points " + sw.name + " in node " + node.site.id.ToString());
				}
				return;
			}
			int count = sw.features.Count;
			int count2 = points.Count;
			for (int m = 0; m < points.Count; m++)
			{
				Feature feature = null;
				if (m < sw.features.Count)
				{
					feature = sw.features[m];
				}
				this.CreateTreeNodeWithFeatureAndBiome(this.worldGen.Settings, sw, node, graph, feature, points[m], tagSet3, m);
			}
			node.ComputeChildren(seededRandom.seed + 1, false, false);
			node.VisitAll(delegate(Node n)
			{
				global::Debug.Assert(n.site.poly != null, string.Format("Node {0}, child of {1} had a null poly after final subworld compute!!", n.site.id, node.site.id));
			});
			if (node.ChildCount() > 0)
			{
				for (int n2 = 0; n2 < tagSet2.Count; n2++)
				{
					global::Debug.Log(string.Format("Applying Moved Tag {0} to {1}", tagSet2[n2].Name, node.site.id));
					node.GetChild(seededRandom.RandomSource().Next(node.ChildCount())).AddTag(tagSet2[n2]);
				}
			}
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x0024BEB0 File Offset: 0x0024A0B0
		private Node CreateTreeNodeWithFeatureAndBiome(WorldGenSettings settings, SubWorld sw, Tree node, MapGraph graph, Feature feature, Vector2 pos, TagSet newTags, int i)
		{
			string text = null;
			bool flag = false;
			TagSet tagSet = new TagSet();
			TagSet tagSet2 = new TagSet();
			if (feature != null)
			{
				FeatureSettings feature2 = settings.GetFeature(feature.type);
				text = feature.type;
				tagSet2.Union(new TagSet(feature2.tags));
				if (feature.tags != null && feature.tags.Count > 0)
				{
					tagSet2.Union(new TagSet(feature.tags));
				}
				if (feature.excludesTags != null && feature.excludesTags.Count > 0)
				{
					tagSet2.Remove(new TagSet(feature.excludesTags));
				}
				tagSet2.Add(new Tag(feature.type));
				tagSet2.Add(WorldGenTags.Feature);
				if (feature2.forceBiome != null)
				{
					tagSet.Add(feature2.forceBiome);
					flag = true;
				}
				if (feature2.biomeTags != null)
				{
					tagSet.Union(new TagSet(feature2.biomeTags));
				}
			}
			if (!flag && sw.biomes.Count > 0)
			{
				WeightedBiome weightedBiome = WeightedRandom.Choose<WeightedBiome>(sw.biomes, this.myRandom);
				if (text == null)
				{
					text = weightedBiome.name;
				}
				tagSet.Add(weightedBiome.name);
				if (weightedBiome.tags != null && weightedBiome.tags.Count > 0)
				{
					tagSet.Union(new TagSet(weightedBiome.tags));
				}
				flag = true;
			}
			if (!flag)
			{
				text = "UNKNOWN";
				global::Debug.LogError("Couldn't get a biome for a cell in " + sw.name + ". Maybe it doesn't have any biomes configured?");
			}
			Cell cell = graph.AddNode(text, pos);
			cell.biomeSpecificTags = new TagSet(tagSet);
			cell.featureSpecificTags = new TagSet(tagSet2);
			Node node2 = node.AddSite(new Diagram.Site((uint)cell.NodeId, cell.position, 1f), Node.NodeType.Internal);
			node2.tags = new TagSet(newTags);
			node2.tags.Add(text);
			node2.tags.Union(tagSet);
			node2.tags.Union(tagSet2);
			return node2;
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x0024C0B4 File Offset: 0x0024A2B4
		private void TagTopAndBottomSites(Tag topTag, Tag bottomTag)
		{
			List<Diagram.Site> list = new List<Diagram.Site>();
			List<Diagram.Site> list2 = new List<Diagram.Site>();
			this.voronoiTree.GetIntersectingLeafSites(this.topEdge, list);
			this.voronoiTree.GetIntersectingLeafSites(this.bottomEdge, list2);
			for (int i = 0; i < list.Count; i++)
			{
				this.voronoiTree.GetNodeForSite(list[i]).AddTag(topTag);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				this.voronoiTree.GetNodeForSite(list2[j]).AddTag(bottomTag);
			}
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x0024C144 File Offset: 0x0024A344
		private void TagEdgeSites(Tag leftTag, Tag rightTag)
		{
			List<Diagram.Site> list = new List<Diagram.Site>();
			List<Diagram.Site> list2 = new List<Diagram.Site>();
			this.voronoiTree.GetIntersectingLeafSites(this.leftEdge, list);
			this.voronoiTree.GetIntersectingLeafSites(this.rightEdge, list2);
			for (int i = 0; i < list.Count; i++)
			{
				this.voronoiTree.GetNodeForSite(list[i]).AddTag(leftTag);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				this.voronoiTree.GetNodeForSite(list2[j]).AddTag(rightTag);
			}
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x0024C1D3 File Offset: 0x0024A3D3
		private bool StartAreaTooLarge(Node node)
		{
			return node.tags.Contains(WorldGenTags.AtStart) && node.site.poly.Area() > 2000f;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x0024C200 File Offset: 0x0024A400
		private void PropagateStartTag()
		{
			foreach (Node node in this.GetStartNodes())
			{
				node.AddTagToNeighbors(WorldGenTags.NearStartLocation);
				node.AddTag(WorldGenTags.IgnoreCaveOverride);
			}
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x0024C260 File Offset: 0x0024A460
		public List<Node> GetStartNodes()
		{
			return this.GetLeafNodesWithTag(WorldGenTags.StartLocation);
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x0024C270 File Offset: 0x0024A470
		public List<Node> GetLeafNodesWithTag(Tag tag)
		{
			List<Node> list = new List<Node>();
			this.voronoiTree.GetLeafNodes(list, (Node node) => node.tags != null && node.tags.Contains(tag));
			return list;
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x0024C2AC File Offset: 0x0024A4AC
		public List<Node> GetTerrainNodesForTag(Tag tag)
		{
			List<Node> list = new List<Node>();
			foreach (Node node in this.GetLeafNodesWithTag(tag))
			{
				Node node2 = this.localGraph.FindNodeByID(node.site.id);
				if (node2 != null)
				{
					list.Add(node2);
				}
			}
			return list;
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x0024C324 File Offset: 0x0024A524
		private Node FindFirstNode(string nodeType)
		{
			return this.localGraph.FindNode((Cell node) => node.type == nodeType);
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x0024C358 File Offset: 0x0024A558
		private Node FindFirstNodeWithTag(Tag tag)
		{
			return this.localGraph.FindNode((Cell node) => node.tags != null && node.tags.Contains(tag));
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x0024C38C File Offset: 0x0024A58C
		public Vector2I GetStartLocation()
		{
			if (string.IsNullOrEmpty(this.worldGen.Settings.world.startSubworldName))
			{
				return new Vector2I(this.mapWidth / 2, this.mapHeight / 2);
			}
			Node node2 = this.FindFirstNodeWithTag(WorldGenTags.StartLocation);
			if (node2 == null)
			{
				List<Node> nodes = this.GetStartNodes();
				if (nodes == null || nodes.Count == 0)
				{
					global::Debug.LogWarning("Couldnt find start node");
					return new Vector2I(this.mapWidth / 2, this.mapHeight / 2);
				}
				node2 = this.localGraph.FindNode((Cell node) => (uint)node.NodeId == nodes[0].site.id);
				node2.tags.Add(WorldGenTags.StartLocation);
			}
			if (node2 == null)
			{
				global::Debug.LogWarning("Couldnt find start node");
				return new Vector2I(this.mapWidth / 2, this.mapHeight / 2);
			}
			return new Vector2I((int)node2.position.x, (int)node2.position.y);
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x0024C48C File Offset: 0x0024A68C
		private List<Diagram.Site> GetIntersectingSites(Node intersectingSiteSource, Tree sitesSource)
		{
			List<Diagram.Site> list = new List<Diagram.Site>();
			list = new List<Diagram.Site>();
			LineSegment lineSegment;
			for (int i = 1; i < intersectingSiteSource.site.poly.Vertices.Count - 1; i++)
			{
				lineSegment = new LineSegment(new Vector2?(intersectingSiteSource.site.poly.Vertices[i - 1]), new Vector2?(intersectingSiteSource.site.poly.Vertices[i]));
				sitesSource.GetIntersectingLeafSites(lineSegment, list);
			}
			lineSegment = new LineSegment(new Vector2?(intersectingSiteSource.site.poly.Vertices[intersectingSiteSource.site.poly.Vertices.Count - 1]), new Vector2?(intersectingSiteSource.site.poly.Vertices[0]));
			sitesSource.GetIntersectingLeafSites(lineSegment, list);
			return list;
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x0024C568 File Offset: 0x0024A768
		public void GetEdgeOfMapSites(Tree vt, List<Diagram.Site> topSites, List<Diagram.Site> bottomSites, List<Diagram.Site> leftSites, List<Diagram.Site> rightSites)
		{
			vt.GetIntersectingLeafSites(this.topEdge, topSites);
			vt.GetIntersectingLeafSites(this.bottomEdge, bottomSites);
			vt.GetIntersectingLeafSites(this.leftEdge, leftSites);
			vt.GetIntersectingLeafSites(this.rightEdge, rightSites);
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x0024C5A0 File Offset: 0x0024A7A0
		[OnSerializing]
		internal void OnSerializingMethod()
		{
			try
			{
				this.extra = new WorldLayout.ExtraIO();
				if (this.voronoiTree != null)
				{
					this.extra.internals.Add(this.voronoiTree);
					this.voronoiTree.GetInternalNodes(this.extra.internals);
					List<Node> list = new List<Node>();
					this.voronoiTree.GetLeafNodes(list, null);
					using (List<Node>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Leaf ln = (Leaf)enumerator.Current;
							if (ln != null)
							{
								this.extra.leafInternalParent.Add(new KeyValuePair<int, int>(this.extra.leafs.Count, this.extra.internals.FindIndex(0, (Tree n) => n == ln.parent)));
								this.extra.leafs.Add(ln);
							}
						}
					}
					for (int i = 0; i < this.extra.internals.Count; i++)
					{
						Tree vt = this.extra.internals[i];
						if (vt.parent != null)
						{
							int num = this.extra.internals.FindIndex(0, (Tree n) => n == vt.parent);
							if (num >= 0)
							{
								this.extra.internalInternalParent.Add(new KeyValuePair<int, int>(i, num));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				WorldGenLogger.LogException(message, stackTrace);
				global::Debug.Log("Error deserialising " + ex.Message);
			}
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x0024C790 File Offset: 0x0024A990
		[OnSerialized]
		internal void OnSerializedMethod()
		{
			this.extra = null;
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x0024C799 File Offset: 0x0024A999
		[OnDeserializing]
		internal void OnDeserializingMethod()
		{
			this.extra = new WorldLayout.ExtraIO();
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0024C7A8 File Offset: 0x0024A9A8
		[OnDeserialized]
		internal void OnDeserializedMethod()
		{
			try
			{
				this.voronoiTree = this.extra.internals[0];
				for (int i = 0; i < this.extra.internalInternalParent.Count; i++)
				{
					KeyValuePair<int, int> keyValuePair = this.extra.internalInternalParent[i];
					Tree tree = this.extra.internals[keyValuePair.Key];
					this.extra.internals[keyValuePair.Value].AddChild(tree);
				}
				for (int j = 0; j < this.extra.leafInternalParent.Count; j++)
				{
					KeyValuePair<int, int> keyValuePair2 = this.extra.leafInternalParent[j];
					Node node = this.extra.leafs[keyValuePair2.Key];
					this.extra.internals[keyValuePair2.Value].AddChild(node);
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				WorldGenLogger.LogException(message, stackTrace);
				global::Debug.Log("Error deserialising " + ex.Message);
			}
			this.extra = null;
		}

		// Token: 0x040044CC RID: 17612
		private Tree voronoiTree;

		// Token: 0x040044CD RID: 17613
		[Serialize]
		public MapGraph localGraph;

		// Token: 0x040044CE RID: 17614
		[Serialize]
		public MapGraph overworldGraph;

		// Token: 0x040044CF RID: 17615
		[EnumFlags]
		public static WorldLayout.DebugFlags drawOptions;

		// Token: 0x040044D1 RID: 17617
		private LineSegment topEdge;

		// Token: 0x040044D2 RID: 17618
		private LineSegment bottomEdge;

		// Token: 0x040044D3 RID: 17619
		private LineSegment leftEdge;

		// Token: 0x040044D4 RID: 17620
		private LineSegment rightEdge;

		// Token: 0x040044D6 RID: 17622
		private SeededRandom myRandom;

		// Token: 0x040044D8 RID: 17624
		[Serialize]
		private WorldLayout.ExtraIO extra;

		// Token: 0x02001ACB RID: 6859
		[Flags]
		public enum DebugFlags
		{
			// Token: 0x040078CF RID: 30927
			LocalGraph = 1,
			// Token: 0x040078D0 RID: 30928
			OverworldGraph = 2,
			// Token: 0x040078D1 RID: 30929
			VoronoiTree = 4,
			// Token: 0x040078D2 RID: 30930
			PowerDiagram = 8
		}

		// Token: 0x02001ACC RID: 6860
		[SerializationConfig(MemberSerialization.OptOut)]
		private class ExtraIO
		{
			// Token: 0x0600941A RID: 37914 RVA: 0x0031B455 File Offset: 0x00319655
			[OnDeserializing]
			internal void OnDeserializingMethod()
			{
				this.leafs = new List<Leaf>();
				this.internals = new List<Tree>();
				this.leafInternalParent = new List<KeyValuePair<int, int>>();
				this.internalInternalParent = new List<KeyValuePair<int, int>>();
			}

			// Token: 0x040078D3 RID: 30931
			public List<Leaf> leafs = new List<Leaf>();

			// Token: 0x040078D4 RID: 30932
			public List<Tree> internals = new List<Tree>();

			// Token: 0x040078D5 RID: 30933
			public List<KeyValuePair<int, int>> leafInternalParent = new List<KeyValuePair<int, int>>();

			// Token: 0x040078D6 RID: 30934
			public List<KeyValuePair<int, int>> internalInternalParent = new List<KeyValuePair<int, int>>();
		}
	}
}
