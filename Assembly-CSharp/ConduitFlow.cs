using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x020006AF RID: 1711
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{conduitType}")]
public class ConduitFlow : IConduitFlow
{
	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06002E62 RID: 11874 RVA: 0x000F47F4 File Offset: 0x000F29F4
	// (remove) Token: 0x06002E63 RID: 11875 RVA: 0x000F482C File Offset: 0x000F2A2C
	public event System.Action onConduitsRebuilt;

	// Token: 0x06002E64 RID: 11876 RVA: 0x000F4864 File Offset: 0x000F2A64
	public void AddConduitUpdater(Action<float> callback, ConduitFlowPriority priority = ConduitFlowPriority.Default)
	{
		this.conduitUpdaters.Add(new ConduitFlow.ConduitUpdater
		{
			priority = priority,
			callback = callback
		});
		this.dirtyConduitUpdaters = true;
	}

	// Token: 0x06002E65 RID: 11877 RVA: 0x000F489C File Offset: 0x000F2A9C
	public void RemoveConduitUpdater(Action<float> callback)
	{
		for (int i = 0; i < this.conduitUpdaters.Count; i++)
		{
			if (this.conduitUpdaters[i].callback == callback)
			{
				this.conduitUpdaters.RemoveAt(i);
				this.dirtyConduitUpdaters = true;
				return;
			}
		}
	}

	// Token: 0x06002E66 RID: 11878 RVA: 0x000F48EC File Offset: 0x000F2AEC
	private static ConduitFlow.FlowDirections ComputeFlowDirection(int index)
	{
		return (ConduitFlow.FlowDirections)(1 << index);
	}

	// Token: 0x06002E67 RID: 11879 RVA: 0x000F48F5 File Offset: 0x000F2AF5
	private static int ComputeIndex(ConduitFlow.FlowDirections flow)
	{
		switch (flow)
		{
		case ConduitFlow.FlowDirections.Down:
			return 0;
		case ConduitFlow.FlowDirections.Left:
			return 1;
		case ConduitFlow.FlowDirections.Down | ConduitFlow.FlowDirections.Left:
			break;
		case ConduitFlow.FlowDirections.Right:
			return 2;
		default:
			if (flow == ConduitFlow.FlowDirections.Up)
			{
				return 3;
			}
			break;
		}
		global::Debug.Assert(false, "multiple bits are set in 'flow'...can't compute refuted index");
		return -1;
	}

	// Token: 0x06002E68 RID: 11880 RVA: 0x000F4929 File Offset: 0x000F2B29
	private static ConduitFlow.FlowDirections ComputeNextFlowDirection(ConduitFlow.FlowDirections current)
	{
		if (current != ConduitFlow.FlowDirections.None)
		{
			return ConduitFlow.ComputeFlowDirection((ConduitFlow.ComputeIndex(current) + 1) % 4);
		}
		return ConduitFlow.FlowDirections.Down;
	}

	// Token: 0x06002E69 RID: 11881 RVA: 0x000F493F File Offset: 0x000F2B3F
	public static ConduitFlow.FlowDirections Invert(ConduitFlow.FlowDirections directions)
	{
		return ConduitFlow.FlowDirections.All & ~directions;
	}

	// Token: 0x06002E6A RID: 11882 RVA: 0x000F4948 File Offset: 0x000F2B48
	public static ConduitFlow.FlowDirections Opposite(ConduitFlow.FlowDirections directions)
	{
		ConduitFlow.FlowDirections flowDirections = ConduitFlow.FlowDirections.None;
		if ((directions & ConduitFlow.FlowDirections.Left) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Right;
		}
		else if ((directions & ConduitFlow.FlowDirections.Right) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Left;
		}
		else if ((directions & ConduitFlow.FlowDirections.Up) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Down;
		}
		else if ((directions & ConduitFlow.FlowDirections.Down) != ConduitFlow.FlowDirections.None)
		{
			flowDirections = ConduitFlow.FlowDirections.Up;
		}
		return flowDirections;
	}

	// Token: 0x06002E6B RID: 11883 RVA: 0x000F497C File Offset: 0x000F2B7C
	public ConduitFlow(ConduitType conduit_type, int num_cells, IUtilityNetworkMgr network_mgr, float max_conduit_mass, float initial_elapsed_time)
	{
		this.elapsedTime = initial_elapsed_time;
		this.conduitType = conduit_type;
		this.networkMgr = network_mgr;
		this.MaxMass = max_conduit_mass;
		this.Initialize(num_cells);
		network_mgr.AddNetworksRebuiltListener(new Action<IList<UtilityNetwork>, ICollection<int>>(this.OnUtilityNetworksRebuilt));
	}

	// Token: 0x06002E6C RID: 11884 RVA: 0x000F4A2C File Offset: 0x000F2C2C
	public void Initialize(int num_cells)
	{
		this.grid = new ConduitFlow.GridNode[num_cells];
		for (int i = 0; i < num_cells; i++)
		{
			this.grid[i].conduitIdx = -1;
			this.grid[i].contents.element = SimHashes.Vacuum;
			this.grid[i].contents.diseaseIdx = byte.MaxValue;
		}
	}

	// Token: 0x06002E6D RID: 11885 RVA: 0x000F4A9C File Offset: 0x000F2C9C
	private void OnUtilityNetworksRebuilt(IList<UtilityNetwork> networks, ICollection<int> root_nodes)
	{
		this.RebuildConnections(root_nodes);
		int num = this.networks.Count - networks.Count;
		if (0 < this.networks.Count - networks.Count)
		{
			this.networks.RemoveRange(networks.Count, num);
		}
		global::Debug.Assert(this.networks.Count <= networks.Count);
		for (int num2 = 0; num2 != networks.Count; num2++)
		{
			if (num2 < this.networks.Count)
			{
				this.networks[num2] = new ConduitFlow.Network
				{
					network = (FlowUtilityNetwork)networks[num2],
					cells = this.networks[num2].cells
				};
				this.networks[num2].cells.Clear();
			}
			else
			{
				this.networks.Add(new ConduitFlow.Network
				{
					network = (FlowUtilityNetwork)networks[num2],
					cells = new List<int>()
				});
			}
		}
		this.build_network_job.Reset(this);
		foreach (ConduitFlow.Network network in this.networks)
		{
			this.build_network_job.Add(new ConduitFlow.BuildNetworkTask(network, this.soaInfo.NumEntries));
		}
		GlobalJobManager.Run(this.build_network_job);
		for (int num3 = 0; num3 != this.build_network_job.Count; num3++)
		{
			this.build_network_job.GetWorkItem(num3).Finish();
		}
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x000F4C5C File Offset: 0x000F2E5C
	private void RebuildConnections(IEnumerable<int> root_nodes)
	{
		ConduitFlow.ConnectContext connectContext = new ConduitFlow.ConnectContext(this);
		this.soaInfo.Clear(this);
		this.replacements.ExceptWith(root_nodes);
		ObjectLayer objectLayer = ((this.conduitType == ConduitType.Gas) ? ObjectLayer.GasConduit : ObjectLayer.LiquidConduit);
		foreach (int num in root_nodes)
		{
			GameObject gameObject = Grid.Objects[num, (int)objectLayer];
			if (!(gameObject == null))
			{
				global::Conduit component = gameObject.GetComponent<global::Conduit>();
				if (!(component != null) || !component.IsDisconnected())
				{
					int num2 = this.soaInfo.AddConduit(this, gameObject, num);
					this.grid[num].conduitIdx = num2;
					connectContext.cells.Add(num);
				}
			}
		}
		Game.Instance.conduitTemperatureManager.Sim200ms(0f);
		this.connect_job.Reset(connectContext);
		int num3 = 256;
		for (int i = 0; i < connectContext.cells.Count; i += num3)
		{
			this.connect_job.Add(new ConduitFlow.ConnectTask(i, Mathf.Min(i + num3, connectContext.cells.Count)));
		}
		GlobalJobManager.Run(this.connect_job);
		connectContext.Finish();
		if (this.onConduitsRebuilt != null)
		{
			this.onConduitsRebuilt();
		}
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x000F4DC4 File Offset: 0x000F2FC4
	private ConduitFlow.FlowDirections GetDirection(ConduitFlow.Conduit conduit, ConduitFlow.Conduit target_conduit)
	{
		global::Debug.Assert(conduit.idx != -1);
		global::Debug.Assert(target_conduit.idx != -1);
		ConduitFlow.ConduitConnections conduitConnections = this.soaInfo.GetConduitConnections(conduit.idx);
		if (conduitConnections.up == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Up;
		}
		if (conduitConnections.down == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Down;
		}
		if (conduitConnections.left == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Left;
		}
		if (conduitConnections.right == target_conduit.idx)
		{
			return ConduitFlow.FlowDirections.Right;
		}
		return ConduitFlow.FlowDirections.None;
	}

	// Token: 0x06002E70 RID: 11888 RVA: 0x000F4E48 File Offset: 0x000F3048
	public int ComputeUpdateOrder(int cell)
	{
		foreach (ConduitFlow.Network network in this.networks)
		{
			int num = network.cells.IndexOf(cell);
			if (num != -1)
			{
				return num;
			}
		}
		return -1;
	}

	// Token: 0x06002E71 RID: 11889 RVA: 0x000F4EAC File Offset: 0x000F30AC
	public ConduitFlow.ConduitContents GetContents(int cell)
	{
		ConduitFlow.ConduitContents conduitContents = this.grid[cell].contents;
		ConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			conduitContents = this.soaInfo.GetConduit(gridNode.conduitIdx).GetContents(this);
		}
		if (conduitContents.mass > 0f && conduitContents.temperature <= 0f)
		{
			global::Debug.LogError(string.Format("unexpected temperature {0}", conduitContents.temperature));
		}
		return conduitContents;
	}

	// Token: 0x06002E72 RID: 11890 RVA: 0x000F4F34 File Offset: 0x000F3134
	public void SetContents(int cell, ConduitFlow.ConduitContents contents)
	{
		ConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			this.soaInfo.GetConduit(gridNode.conduitIdx).SetContents(this, contents);
			return;
		}
		this.grid[cell].contents = contents;
	}

	// Token: 0x06002E73 RID: 11891 RVA: 0x000F4F85 File Offset: 0x000F3185
	public static int GetCellFromDirection(int cell, ConduitFlow.FlowDirections direction)
	{
		switch (direction)
		{
		case ConduitFlow.FlowDirections.Down:
			return Grid.CellBelow(cell);
		case ConduitFlow.FlowDirections.Left:
			return Grid.CellLeft(cell);
		case ConduitFlow.FlowDirections.Down | ConduitFlow.FlowDirections.Left:
			break;
		case ConduitFlow.FlowDirections.Right:
			return Grid.CellRight(cell);
		default:
			if (direction == ConduitFlow.FlowDirections.Up)
			{
				return Grid.CellAbove(cell);
			}
			break;
		}
		return -1;
	}

	// Token: 0x06002E74 RID: 11892 RVA: 0x000F4FC4 File Offset: 0x000F31C4
	public void Sim200ms(float dt)
	{
		if (dt <= 0f)
		{
			return;
		}
		this.elapsedTime += dt;
		if (this.elapsedTime < 1f)
		{
			return;
		}
		this.elapsedTime -= 1f;
		float num = 1f;
		this.lastUpdateTime = Time.time;
		this.soaInfo.BeginFrame(this);
		ListPool<ConduitFlow.UpdateNetworkTask, ConduitFlow>.PooledList pooledList = ListPool<ConduitFlow.UpdateNetworkTask, ConduitFlow>.Allocate();
		pooledList.Capacity = Mathf.Max(pooledList.Capacity, this.networks.Count);
		foreach (ConduitFlow.Network network in this.networks)
		{
			pooledList.Add(new ConduitFlow.UpdateNetworkTask(network));
		}
		int num2 = 0;
		while (num2 != 4 && pooledList.Count != 0)
		{
			this.update_networks_job.Reset(this);
			foreach (ConduitFlow.UpdateNetworkTask updateNetworkTask in pooledList)
			{
				this.update_networks_job.Add(updateNetworkTask);
			}
			GlobalJobManager.Run(this.update_networks_job);
			pooledList.Clear();
			for (int num3 = 0; num3 != this.update_networks_job.Count; num3++)
			{
				ConduitFlow.UpdateNetworkTask workItem = this.update_networks_job.GetWorkItem(num3);
				if (workItem.continue_updating && num2 != 3)
				{
					pooledList.Add(workItem);
				}
				else
				{
					workItem.Finish(this);
				}
			}
			num2++;
		}
		pooledList.Recycle();
		if (this.dirtyConduitUpdaters)
		{
			this.conduitUpdaters.Sort((ConduitFlow.ConduitUpdater a, ConduitFlow.ConduitUpdater b) => a.priority - b.priority);
		}
		this.soaInfo.EndFrame(this);
		for (int i = 0; i < this.conduitUpdaters.Count; i++)
		{
			this.conduitUpdaters[i].callback(num);
		}
	}

	// Token: 0x06002E75 RID: 11893 RVA: 0x000F51D4 File Offset: 0x000F33D4
	private float ComputeMovableMass(ConduitFlow.GridNode grid_node, Dictionary<int, ConduitFlow.Sink> sinks)
	{
		ConduitFlow.ConduitContents contents = grid_node.contents;
		if (contents.element == SimHashes.Vacuum)
		{
			return 0f;
		}
		ConduitFlow.Sink sink;
		if (!sinks.TryGetValue(grid_node.conduitIdx, out sink) || !(sink.consumer != null))
		{
			return contents.movable_mass;
		}
		return Mathf.Max(0f, contents.movable_mass - sink.space_remaining);
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x000F523C File Offset: 0x000F343C
	private bool UpdateConduit(ConduitFlow.Conduit conduit, Dictionary<int, ConduitFlow.Sink> sinks)
	{
		bool flag = false;
		int cell = this.soaInfo.GetCell(conduit.idx);
		ConduitFlow.GridNode gridNode = this.grid[cell];
		float num = this.ComputeMovableMass(gridNode, sinks);
		ConduitFlow.FlowDirections permittedFlowDirections = this.soaInfo.GetPermittedFlowDirections(conduit.idx);
		ConduitFlow.FlowDirections flowDirections = this.soaInfo.GetTargetFlowDirection(conduit.idx);
		if (num <= 0f)
		{
			for (int num2 = 0; num2 != 4; num2++)
			{
				flowDirections = ConduitFlow.ComputeNextFlowDirection(flowDirections);
				if ((permittedFlowDirections & flowDirections) != ConduitFlow.FlowDirections.None)
				{
					ConduitFlow.Conduit conduitFromDirection = this.soaInfo.GetConduitFromDirection(conduit.idx, flowDirections);
					global::Debug.Assert(conduitFromDirection.idx != -1);
					if ((this.soaInfo.GetSrcFlowDirection(conduitFromDirection.idx) & ConduitFlow.Opposite(flowDirections)) > ConduitFlow.FlowDirections.None)
					{
						this.soaInfo.SetPullDirection(conduitFromDirection.idx, flowDirections);
					}
				}
			}
		}
		else
		{
			for (int num3 = 0; num3 != 4; num3++)
			{
				flowDirections = ConduitFlow.ComputeNextFlowDirection(flowDirections);
				if ((permittedFlowDirections & flowDirections) != ConduitFlow.FlowDirections.None)
				{
					ConduitFlow.Conduit conduitFromDirection2 = this.soaInfo.GetConduitFromDirection(conduit.idx, flowDirections);
					global::Debug.Assert(conduitFromDirection2.idx != -1);
					ConduitFlow.FlowDirections srcFlowDirection = this.soaInfo.GetSrcFlowDirection(conduitFromDirection2.idx);
					bool flag2 = (srcFlowDirection & ConduitFlow.Opposite(flowDirections)) > ConduitFlow.FlowDirections.None;
					if (srcFlowDirection != ConduitFlow.FlowDirections.None && !flag2)
					{
						flag = true;
					}
					else
					{
						int cell2 = this.soaInfo.GetCell(conduitFromDirection2.idx);
						global::Debug.Assert(cell2 != -1);
						ConduitFlow.ConduitContents contents = this.grid[cell2].contents;
						bool flag3 = contents.element == SimHashes.Vacuum || contents.element == gridNode.contents.element;
						float effectiveCapacity = contents.GetEffectiveCapacity(this.MaxMass);
						bool flag4 = flag3 && effectiveCapacity > 0f;
						float num4 = Mathf.Min(num, effectiveCapacity);
						if (flag2 && flag4)
						{
							this.soaInfo.SetPullDirection(conduitFromDirection2.idx, flowDirections);
						}
						if (num4 > 0f && flag4)
						{
							this.soaInfo.SetTargetFlowDirection(conduit.idx, flowDirections);
							global::Debug.Assert(gridNode.contents.temperature > 0f);
							contents.temperature = GameUtil.GetFinalTemperature(gridNode.contents.temperature, num4, contents.temperature, contents.mass);
							contents.AddMass(num4);
							contents.element = gridNode.contents.element;
							int num5 = (int)(num4 / gridNode.contents.mass * (float)gridNode.contents.diseaseCount);
							if (num5 != 0)
							{
								SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(gridNode.contents.diseaseIdx, num5, contents.diseaseIdx, contents.diseaseCount);
								contents.diseaseIdx = diseaseInfo.idx;
								contents.diseaseCount = diseaseInfo.count;
							}
							this.grid[cell2].contents = contents;
							global::Debug.Assert(num4 <= gridNode.contents.mass);
							float num6 = gridNode.contents.mass - num4;
							num -= num4;
							if (num6 <= 0f)
							{
								global::Debug.Assert(num <= 0f);
								this.soaInfo.SetLastFlowInfo(conduit.idx, flowDirections, ref gridNode.contents);
								gridNode.contents = ConduitFlow.ConduitContents.Empty;
							}
							else
							{
								int num7 = (int)(num6 / gridNode.contents.mass * (float)gridNode.contents.diseaseCount);
								global::Debug.Assert(num7 >= 0);
								ConduitFlow.ConduitContents contents2 = gridNode.contents;
								contents2.RemoveMass(num6);
								contents2.diseaseCount -= num7;
								gridNode.contents.RemoveMass(num4);
								gridNode.contents.diseaseCount = num7;
								if (num7 == 0)
								{
									gridNode.contents.diseaseIdx = byte.MaxValue;
								}
								this.soaInfo.SetLastFlowInfo(conduit.idx, flowDirections, ref contents2);
							}
							this.grid[cell].contents = gridNode.contents;
							flag = 0f < this.ComputeMovableMass(gridNode, sinks);
							break;
						}
					}
				}
			}
		}
		ConduitFlow.FlowDirections srcFlowDirection2 = this.soaInfo.GetSrcFlowDirection(conduit.idx);
		ConduitFlow.FlowDirections pullDirection = this.soaInfo.GetPullDirection(conduit.idx);
		if (srcFlowDirection2 == ConduitFlow.FlowDirections.None || (ConduitFlow.Opposite(srcFlowDirection2) & pullDirection) != ConduitFlow.FlowDirections.None)
		{
			this.soaInfo.SetPullDirection(conduit.idx, ConduitFlow.FlowDirections.None);
			this.soaInfo.SetSrcFlowDirection(conduit.idx, ConduitFlow.FlowDirections.None);
			for (int num8 = 0; num8 != 2; num8++)
			{
				ConduitFlow.FlowDirections flowDirections2 = srcFlowDirection2;
				for (int num9 = 0; num9 != 4; num9++)
				{
					flowDirections2 = ConduitFlow.ComputeNextFlowDirection(flowDirections2);
					ConduitFlow.Conduit conduitFromDirection3 = this.soaInfo.GetConduitFromDirection(conduit.idx, flowDirections2);
					if (conduitFromDirection3.idx != -1 && (this.soaInfo.GetPermittedFlowDirections(conduitFromDirection3.idx) & ConduitFlow.Opposite(flowDirections2)) != ConduitFlow.FlowDirections.None)
					{
						int cell3 = this.soaInfo.GetCell(conduitFromDirection3.idx);
						ConduitFlow.ConduitContents contents3 = this.grid[cell3].contents;
						float num10 = ((num8 == 0) ? contents3.movable_mass : contents3.mass);
						if (0f < num10)
						{
							this.soaInfo.SetSrcFlowDirection(conduit.idx, flowDirections2);
							break;
						}
					}
				}
				if (this.soaInfo.GetSrcFlowDirection(conduit.idx) != ConduitFlow.FlowDirections.None)
				{
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000F579D File Offset: 0x000F399D
	public float ContinuousLerpPercent
	{
		get
		{
			return Mathf.Clamp01((Time.time - this.lastUpdateTime) / 1f);
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000F57B6 File Offset: 0x000F39B6
	public float DiscreteLerpPercent
	{
		get
		{
			return Mathf.Clamp01(this.elapsedTime / 1f);
		}
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x000F57CC File Offset: 0x000F39CC
	public float AddElement(int cell_idx, SimHashes element, float mass, float temperature, byte disease_idx, int disease_count)
	{
		if (this.grid[cell_idx].conduitIdx == -1)
		{
			return 0f;
		}
		ConduitFlow.ConduitContents contents = this.GetConduit(cell_idx).GetContents(this);
		if (contents.element != element && contents.element != SimHashes.Vacuum && mass > 0f)
		{
			return 0f;
		}
		float num = Mathf.Min(mass, this.MaxMass - contents.mass);
		float num2 = num / mass;
		if (num <= 0f)
		{
			return 0f;
		}
		contents.temperature = GameUtil.GetFinalTemperature(temperature, num, contents.temperature, contents.mass);
		contents.AddMass(num);
		contents.element = element;
		contents.ConsolidateMass();
		int num3 = (int)(num2 * (float)disease_count);
		if (num3 > 0)
		{
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(disease_idx, num3, contents.diseaseIdx, contents.diseaseCount);
			contents.diseaseIdx = diseaseInfo.idx;
			contents.diseaseCount = diseaseInfo.count;
		}
		this.SetContents(cell_idx, contents);
		return num;
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x000F58CC File Offset: 0x000F3ACC
	public ConduitFlow.ConduitContents RemoveElement(int cell, float delta)
	{
		ConduitFlow.Conduit conduit = this.GetConduit(cell);
		if (conduit.idx == -1)
		{
			return ConduitFlow.ConduitContents.Empty;
		}
		return this.RemoveElement(conduit, delta);
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x000F58F8 File Offset: 0x000F3AF8
	public ConduitFlow.ConduitContents RemoveElement(ConduitFlow.Conduit conduit, float delta)
	{
		ConduitFlow.ConduitContents contents = conduit.GetContents(this);
		float num = Mathf.Min(contents.mass, delta);
		float num2 = contents.mass - num;
		if (num2 <= 0f)
		{
			conduit.SetContents(this, ConduitFlow.ConduitContents.Empty);
			return contents;
		}
		ConduitFlow.ConduitContents conduitContents = contents;
		conduitContents.RemoveMass(num2);
		int num3 = (int)(num2 / contents.mass * (float)contents.diseaseCount);
		conduitContents.diseaseCount = contents.diseaseCount - num3;
		ConduitFlow.ConduitContents conduitContents2 = contents;
		conduitContents2.RemoveMass(num);
		conduitContents2.diseaseCount = num3;
		if (num3 <= 0)
		{
			conduitContents2.diseaseIdx = byte.MaxValue;
			conduitContents2.diseaseCount = 0;
		}
		conduit.SetContents(this, conduitContents2);
		return conduitContents;
	}

	// Token: 0x06002E7C RID: 11900 RVA: 0x000F59A8 File Offset: 0x000F3BA8
	public ConduitFlow.FlowDirections GetPermittedFlow(int cell)
	{
		ConduitFlow.Conduit conduit = this.GetConduit(cell);
		if (conduit.idx == -1)
		{
			return ConduitFlow.FlowDirections.None;
		}
		return this.soaInfo.GetPermittedFlowDirections(conduit.idx);
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x000F59D9 File Offset: 0x000F3BD9
	public bool HasConduit(int cell)
	{
		return this.grid[cell].conduitIdx != -1;
	}

	// Token: 0x06002E7E RID: 11902 RVA: 0x000F59F4 File Offset: 0x000F3BF4
	public ConduitFlow.Conduit GetConduit(int cell)
	{
		int conduitIdx = this.grid[cell].conduitIdx;
		if (conduitIdx == -1)
		{
			return ConduitFlow.Conduit.Invalid;
		}
		return this.soaInfo.GetConduit(conduitIdx);
	}

	// Token: 0x06002E7F RID: 11903 RVA: 0x000F5A2C File Offset: 0x000F3C2C
	private void DumpPipeContents(int cell, ConduitFlow.ConduitContents contents)
	{
		if (contents.element != SimHashes.Vacuum && contents.mass > 0f)
		{
			SimMessages.AddRemoveSubstance(cell, contents.element, CellEventLogger.Instance.ConduitFlowEmptyConduit, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount, true, -1);
			this.SetContents(cell, ConduitFlow.ConduitContents.Empty);
		}
	}

	// Token: 0x06002E80 RID: 11904 RVA: 0x000F5A91 File Offset: 0x000F3C91
	public void EmptyConduit(int cell)
	{
		if (this.replacements.Contains(cell))
		{
			return;
		}
		this.DumpPipeContents(cell, this.grid[cell].contents);
	}

	// Token: 0x06002E81 RID: 11905 RVA: 0x000F5ABA File Offset: 0x000F3CBA
	public void MarkForReplacement(int cell)
	{
		this.replacements.Add(cell);
	}

	// Token: 0x06002E82 RID: 11906 RVA: 0x000F5AC9 File Offset: 0x000F3CC9
	public void DeactivateCell(int cell)
	{
		this.grid[cell].conduitIdx = -1;
		this.SetContents(cell, ConduitFlow.ConduitContents.Empty);
	}

	// Token: 0x06002E83 RID: 11907 RVA: 0x000F5AE9 File Offset: 0x000F3CE9
	[Conditional("CHECK_NAN")]
	private void Validate(ConduitFlow.ConduitContents contents)
	{
		if (contents.mass > 0f && contents.temperature <= 0f)
		{
			global::Debug.LogError("zero degree pipe contents");
		}
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x000F5B10 File Offset: 0x000F3D10
	[OnSerializing]
	private void OnSerializing()
	{
		int numEntries = this.soaInfo.NumEntries;
		if (numEntries > 0)
		{
			this.versionedSerializedContents = new ConduitFlow.SerializedContents[numEntries];
			this.serializedIdx = new int[numEntries];
			for (int i = 0; i < numEntries; i++)
			{
				ConduitFlow.Conduit conduit = this.soaInfo.GetConduit(i);
				ConduitFlow.ConduitContents contents = conduit.GetContents(this);
				this.serializedIdx[i] = this.soaInfo.GetCell(conduit.idx);
				this.versionedSerializedContents[i] = new ConduitFlow.SerializedContents(contents);
			}
			return;
		}
		this.serializedContents = null;
		this.versionedSerializedContents = null;
		this.serializedIdx = null;
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x000F5BA8 File Offset: 0x000F3DA8
	[OnSerialized]
	private void OnSerialized()
	{
		this.versionedSerializedContents = null;
		this.serializedContents = null;
		this.serializedIdx = null;
	}

	// Token: 0x06002E86 RID: 11910 RVA: 0x000F5BC0 File Offset: 0x000F3DC0
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.serializedContents != null)
		{
			this.versionedSerializedContents = new ConduitFlow.SerializedContents[this.serializedContents.Length];
			for (int i = 0; i < this.serializedContents.Length; i++)
			{
				this.versionedSerializedContents[i] = new ConduitFlow.SerializedContents(this.serializedContents[i]);
			}
			this.serializedContents = null;
		}
		if (this.versionedSerializedContents == null)
		{
			return;
		}
		for (int j = 0; j < this.versionedSerializedContents.Length; j++)
		{
			int num = this.serializedIdx[j];
			ConduitFlow.SerializedContents serializedContents = this.versionedSerializedContents[j];
			ConduitFlow.ConduitContents conduitContents = ((serializedContents.mass <= 0f) ? ConduitFlow.ConduitContents.Empty : new ConduitFlow.ConduitContents(serializedContents.element, Math.Min(this.MaxMass, serializedContents.mass), serializedContents.temperature, byte.MaxValue, 0));
			if (0 < serializedContents.diseaseCount || serializedContents.diseaseHash != 0)
			{
				conduitContents.diseaseIdx = Db.Get().Diseases.GetIndex(serializedContents.diseaseHash);
				conduitContents.diseaseCount = ((conduitContents.diseaseIdx == byte.MaxValue) ? 0 : serializedContents.diseaseCount);
			}
			if (float.IsNaN(conduitContents.temperature) || (conduitContents.temperature <= 0f && conduitContents.element != SimHashes.Vacuum) || 10000f < conduitContents.temperature)
			{
				Vector2I vector2I = Grid.CellToXY(num);
				DeserializeWarnings.Instance.PipeContentsTemperatureIsNan.Warn(string.Format("Invalid pipe content temperature of {0} detected. Resetting temperature. (x={1}, y={2}, cell={3})", new object[] { conduitContents.temperature, vector2I.x, vector2I.y, num }), null);
				conduitContents.temperature = ElementLoader.FindElementByHash(conduitContents.element).defaultValues.temperature;
			}
			this.SetContents(num, conduitContents);
		}
		this.versionedSerializedContents = null;
		this.serializedContents = null;
		this.serializedIdx = null;
	}

	// Token: 0x06002E87 RID: 11911 RVA: 0x000F5DB4 File Offset: 0x000F3FB4
	public UtilityNetwork GetNetwork(ConduitFlow.Conduit conduit)
	{
		int cell = this.soaInfo.GetCell(conduit.idx);
		return this.networkMgr.GetNetworkForCell(cell);
	}

	// Token: 0x06002E88 RID: 11912 RVA: 0x000F5DDF File Offset: 0x000F3FDF
	public void ForceRebuildNetworks()
	{
		this.networkMgr.ForceRebuildNetworks();
	}

	// Token: 0x06002E89 RID: 11913 RVA: 0x000F5DEC File Offset: 0x000F3FEC
	public bool IsConduitFull(int cell_idx)
	{
		ConduitFlow.ConduitContents contents = this.grid[cell_idx].contents;
		return this.MaxMass - contents.mass <= 0f;
	}

	// Token: 0x06002E8A RID: 11914 RVA: 0x000F5E24 File Offset: 0x000F4024
	public bool IsConduitEmpty(int cell_idx)
	{
		ConduitFlow.ConduitContents contents = this.grid[cell_idx].contents;
		return contents.mass <= 0f;
	}

	// Token: 0x06002E8B RID: 11915 RVA: 0x000F5E54 File Offset: 0x000F4054
	public void FreezeConduitContents(int conduit_idx)
	{
		GameObject conduitGO = this.soaInfo.GetConduitGO(conduit_idx);
		if (conduitGO != null && this.soaInfo.GetConduit(conduit_idx).GetContents(this).mass > this.MaxMass * 0.1f)
		{
			conduitGO.Trigger(-700727624, null);
		}
	}

	// Token: 0x06002E8C RID: 11916 RVA: 0x000F5EB0 File Offset: 0x000F40B0
	public void MeltConduitContents(int conduit_idx)
	{
		GameObject conduitGO = this.soaInfo.GetConduitGO(conduit_idx);
		if (conduitGO != null && this.soaInfo.GetConduit(conduit_idx).GetContents(this).mass > this.MaxMass * 0.1f)
		{
			conduitGO.Trigger(-1152799878, null);
		}
	}

	// Token: 0x04001C06 RID: 7174
	public const float MAX_LIQUID_MASS = 10f;

	// Token: 0x04001C07 RID: 7175
	public const float MAX_GAS_MASS = 1f;

	// Token: 0x04001C08 RID: 7176
	public ConduitType conduitType;

	// Token: 0x04001C09 RID: 7177
	private float MaxMass = 10f;

	// Token: 0x04001C0A RID: 7178
	private const float PERCENT_MAX_MASS_FOR_STATE_CHANGE_DAMAGE = 0.1f;

	// Token: 0x04001C0B RID: 7179
	public const float TickRate = 1f;

	// Token: 0x04001C0C RID: 7180
	public const float WaitTime = 1f;

	// Token: 0x04001C0D RID: 7181
	private float elapsedTime;

	// Token: 0x04001C0E RID: 7182
	private float lastUpdateTime = float.NegativeInfinity;

	// Token: 0x04001C0F RID: 7183
	public ConduitFlow.SOAInfo soaInfo = new ConduitFlow.SOAInfo();

	// Token: 0x04001C11 RID: 7185
	private bool dirtyConduitUpdaters;

	// Token: 0x04001C12 RID: 7186
	private List<ConduitFlow.ConduitUpdater> conduitUpdaters = new List<ConduitFlow.ConduitUpdater>();

	// Token: 0x04001C13 RID: 7187
	private ConduitFlow.GridNode[] grid;

	// Token: 0x04001C14 RID: 7188
	[Serialize]
	public int[] serializedIdx;

	// Token: 0x04001C15 RID: 7189
	[Serialize]
	public ConduitFlow.ConduitContents[] serializedContents;

	// Token: 0x04001C16 RID: 7190
	[Serialize]
	public ConduitFlow.SerializedContents[] versionedSerializedContents;

	// Token: 0x04001C17 RID: 7191
	private IUtilityNetworkMgr networkMgr;

	// Token: 0x04001C18 RID: 7192
	private HashSet<int> replacements = new HashSet<int>();

	// Token: 0x04001C19 RID: 7193
	private const int FLOW_DIRECTION_COUNT = 4;

	// Token: 0x04001C1A RID: 7194
	private List<ConduitFlow.Network> networks = new List<ConduitFlow.Network>();

	// Token: 0x04001C1B RID: 7195
	private WorkItemCollection<ConduitFlow.BuildNetworkTask, ConduitFlow> build_network_job = new WorkItemCollection<ConduitFlow.BuildNetworkTask, ConduitFlow>();

	// Token: 0x04001C1C RID: 7196
	private WorkItemCollection<ConduitFlow.ConnectTask, ConduitFlow.ConnectContext> connect_job = new WorkItemCollection<ConduitFlow.ConnectTask, ConduitFlow.ConnectContext>();

	// Token: 0x04001C1D RID: 7197
	private WorkItemCollection<ConduitFlow.UpdateNetworkTask, ConduitFlow> update_networks_job = new WorkItemCollection<ConduitFlow.UpdateNetworkTask, ConduitFlow>();

	// Token: 0x02001376 RID: 4982
	[DebuggerDisplay("{NumEntries}")]
	public class SOAInfo
	{
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06007DFB RID: 32251 RVA: 0x002D7368 File Offset: 0x002D5568
		public int NumEntries
		{
			get
			{
				return this.conduits.Count;
			}
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x002D7378 File Offset: 0x002D5578
		public int AddConduit(ConduitFlow manager, GameObject conduit_go, int cell)
		{
			int count = this.conduitConnections.Count;
			ConduitFlow.Conduit conduit = new ConduitFlow.Conduit(count);
			this.conduits.Add(conduit);
			this.conduitConnections.Add(new ConduitFlow.ConduitConnections
			{
				left = -1,
				right = -1,
				up = -1,
				down = -1
			});
			ConduitFlow.ConduitContents contents = manager.grid[cell].contents;
			this.initialContents.Add(contents);
			this.lastFlowInfo.Add(ConduitFlow.ConduitFlowInfo.DEFAULT);
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(conduit_go);
			HandleVector<int>.Handle handle2 = Game.Instance.conduitTemperatureManager.Allocate(manager.conduitType, count, handle, ref contents);
			HandleVector<int>.Handle handle3 = Game.Instance.conduitDiseaseManager.Allocate(handle2, ref contents);
			this.cells.Add(cell);
			this.diseaseContentsVisible.Add(false);
			this.structureTemperatureHandles.Add(handle);
			this.temperatureHandles.Add(handle2);
			this.diseaseHandles.Add(handle3);
			this.conduitGOs.Add(conduit_go);
			this.permittedFlowDirections.Add(ConduitFlow.FlowDirections.None);
			this.srcFlowDirections.Add(ConduitFlow.FlowDirections.None);
			this.pullDirections.Add(ConduitFlow.FlowDirections.None);
			this.targetFlowDirections.Add(ConduitFlow.FlowDirections.None);
			return count;
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x002D74C0 File Offset: 0x002D56C0
		public void Clear(ConduitFlow manager)
		{
			if (this.clearJob.Count == 0)
			{
				this.clearJob.Reset(this);
				this.clearJob.Add<ConduitFlow.SOAInfo.PublishTemperatureToSim>(this.publishTemperatureToSim);
				this.clearJob.Add<ConduitFlow.SOAInfo.PublishDiseaseToSim>(this.publishDiseaseToSim);
				this.clearJob.Add<ConduitFlow.SOAInfo.ResetConduit>(this.resetConduit);
			}
			this.clearPermanentDiseaseContainer.Initialize(this.conduits.Count, manager);
			this.publishTemperatureToSim.Initialize(this.conduits.Count, manager);
			this.publishDiseaseToSim.Initialize(this.conduits.Count, manager);
			this.resetConduit.Initialize(this.conduits.Count, manager);
			this.clearPermanentDiseaseContainer.Run(this);
			GlobalJobManager.Run(this.clearJob);
			for (int num = 0; num != this.conduits.Count; num++)
			{
				Game.Instance.conduitDiseaseManager.Free(this.diseaseHandles[num]);
			}
			for (int num2 = 0; num2 != this.conduits.Count; num2++)
			{
				Game.Instance.conduitTemperatureManager.Free(this.temperatureHandles[num2]);
			}
			this.cells.Clear();
			this.diseaseContentsVisible.Clear();
			this.permittedFlowDirections.Clear();
			this.srcFlowDirections.Clear();
			this.pullDirections.Clear();
			this.targetFlowDirections.Clear();
			this.conduitGOs.Clear();
			this.diseaseHandles.Clear();
			this.temperatureHandles.Clear();
			this.structureTemperatureHandles.Clear();
			this.initialContents.Clear();
			this.lastFlowInfo.Clear();
			this.conduitConnections.Clear();
			this.conduits.Clear();
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x002D7689 File Offset: 0x002D5889
		public ConduitFlow.Conduit GetConduit(int idx)
		{
			return this.conduits[idx];
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x002D7697 File Offset: 0x002D5897
		public ConduitFlow.ConduitConnections GetConduitConnections(int idx)
		{
			return this.conduitConnections[idx];
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x002D76A5 File Offset: 0x002D58A5
		public void SetConduitConnections(int idx, ConduitFlow.ConduitConnections data)
		{
			this.conduitConnections[idx] = data;
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x002D76B4 File Offset: 0x002D58B4
		public float GetConduitTemperature(int idx)
		{
			HandleVector<int>.Handle handle = this.temperatureHandles[idx];
			float temperature = Game.Instance.conduitTemperatureManager.GetTemperature(handle);
			global::Debug.Assert(!float.IsNaN(temperature));
			return temperature;
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x002D76EC File Offset: 0x002D58EC
		public void SetConduitTemperatureData(int idx, ref ConduitFlow.ConduitContents contents)
		{
			HandleVector<int>.Handle handle = this.temperatureHandles[idx];
			Game.Instance.conduitTemperatureManager.SetData(handle, ref contents);
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x002D7718 File Offset: 0x002D5918
		public ConduitDiseaseManager.Data GetDiseaseData(int idx)
		{
			HandleVector<int>.Handle handle = this.diseaseHandles[idx];
			return Game.Instance.conduitDiseaseManager.GetData(handle);
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x002D7744 File Offset: 0x002D5944
		public void SetDiseaseData(int idx, ref ConduitFlow.ConduitContents contents)
		{
			HandleVector<int>.Handle handle = this.diseaseHandles[idx];
			Game.Instance.conduitDiseaseManager.SetData(handle, ref contents);
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x002D776F File Offset: 0x002D596F
		public GameObject GetConduitGO(int idx)
		{
			return this.conduitGOs[idx];
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x002D7780 File Offset: 0x002D5980
		public void ForcePermanentDiseaseContainer(int idx, bool force_on)
		{
			if (this.diseaseContentsVisible[idx] != force_on)
			{
				this.diseaseContentsVisible[idx] = force_on;
				GameObject gameObject = this.conduitGOs[idx];
				if (gameObject == null)
				{
					return;
				}
				gameObject.GetComponent<PrimaryElement>().ForcePermanentDiseaseContainer(force_on);
			}
		}

		// Token: 0x06007E07 RID: 32263 RVA: 0x002D77CC File Offset: 0x002D59CC
		public ConduitFlow.Conduit GetConduitFromDirection(int idx, ConduitFlow.FlowDirections direction)
		{
			ConduitFlow.ConduitConnections conduitConnections = this.conduitConnections[idx];
			switch (direction)
			{
			case ConduitFlow.FlowDirections.Down:
				if (conduitConnections.down == -1)
				{
					return ConduitFlow.Conduit.Invalid;
				}
				return this.conduits[conduitConnections.down];
			case ConduitFlow.FlowDirections.Left:
				if (conduitConnections.left == -1)
				{
					return ConduitFlow.Conduit.Invalid;
				}
				return this.conduits[conduitConnections.left];
			case ConduitFlow.FlowDirections.Down | ConduitFlow.FlowDirections.Left:
				break;
			case ConduitFlow.FlowDirections.Right:
				if (conduitConnections.right == -1)
				{
					return ConduitFlow.Conduit.Invalid;
				}
				return this.conduits[conduitConnections.right];
			default:
				if (direction == ConduitFlow.FlowDirections.Up)
				{
					if (conduitConnections.up == -1)
					{
						return ConduitFlow.Conduit.Invalid;
					}
					return this.conduits[conduitConnections.up];
				}
				break;
			}
			return ConduitFlow.Conduit.Invalid;
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x002D7890 File Offset: 0x002D5A90
		public void BeginFrame(ConduitFlow manager)
		{
			if (this.beginFrameJob.Count == 0)
			{
				this.beginFrameJob.Reset(this);
				this.beginFrameJob.Add<ConduitFlow.SOAInfo.InitializeContentsTask>(this.initializeContents);
				this.beginFrameJob.Add<ConduitFlow.SOAInfo.InvalidateLastFlow>(this.invalidateLastFlow);
			}
			this.initializeContents.Initialize(this.conduits.Count, manager);
			this.invalidateLastFlow.Initialize(this.conduits.Count, manager);
			GlobalJobManager.Run(this.beginFrameJob);
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x002D7914 File Offset: 0x002D5B14
		public void EndFrame(ConduitFlow manager)
		{
			if (this.endFrameJob.Count == 0)
			{
				this.endFrameJob.Reset(this);
				this.endFrameJob.Add<ConduitFlow.SOAInfo.PublishDiseaseToGame>(this.publishDiseaseToGame);
			}
			this.publishTemperatureToGame.Initialize(this.conduits.Count, manager);
			this.publishDiseaseToGame.Initialize(this.conduits.Count, manager);
			this.publishTemperatureToGame.Run(this);
			GlobalJobManager.Run(this.endFrameJob);
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x002D7990 File Offset: 0x002D5B90
		public void UpdateFlowDirection(ConduitFlow manager)
		{
			if (this.updateFlowDirectionJob.Count == 0)
			{
				this.updateFlowDirectionJob.Reset(this);
				this.updateFlowDirectionJob.Add<ConduitFlow.SOAInfo.FlowThroughVacuum>(this.flowThroughVacuum);
			}
			this.flowThroughVacuum.Initialize(this.conduits.Count, manager);
			GlobalJobManager.Run(this.updateFlowDirectionJob);
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x002D79E9 File Offset: 0x002D5BE9
		public void ResetLastFlowInfo(int idx)
		{
			this.lastFlowInfo[idx] = ConduitFlow.ConduitFlowInfo.DEFAULT;
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x002D79FC File Offset: 0x002D5BFC
		public void SetLastFlowInfo(int idx, ConduitFlow.FlowDirections direction, ref ConduitFlow.ConduitContents contents)
		{
			if (this.lastFlowInfo[idx].direction == ConduitFlow.FlowDirections.None)
			{
				this.lastFlowInfo[idx] = new ConduitFlow.ConduitFlowInfo
				{
					direction = direction,
					contents = contents
				};
			}
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x002D7A46 File Offset: 0x002D5C46
		public ConduitFlow.ConduitContents GetInitialContents(int idx)
		{
			return this.initialContents[idx];
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x002D7A54 File Offset: 0x002D5C54
		public ConduitFlow.ConduitFlowInfo GetLastFlowInfo(int idx)
		{
			return this.lastFlowInfo[idx];
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x002D7A62 File Offset: 0x002D5C62
		public ConduitFlow.FlowDirections GetPermittedFlowDirections(int idx)
		{
			return this.permittedFlowDirections[idx];
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x002D7A70 File Offset: 0x002D5C70
		public void SetPermittedFlowDirections(int idx, ConduitFlow.FlowDirections permitted)
		{
			this.permittedFlowDirections[idx] = permitted;
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x002D7A80 File Offset: 0x002D5C80
		public ConduitFlow.FlowDirections AddPermittedFlowDirections(int idx, ConduitFlow.FlowDirections delta)
		{
			List<ConduitFlow.FlowDirections> list = this.permittedFlowDirections;
			return list[idx] |= delta;
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x002D7AAC File Offset: 0x002D5CAC
		public ConduitFlow.FlowDirections RemovePermittedFlowDirections(int idx, ConduitFlow.FlowDirections delta)
		{
			List<ConduitFlow.FlowDirections> list = this.permittedFlowDirections;
			return list[idx] &= ~delta;
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x002D7AD7 File Offset: 0x002D5CD7
		public ConduitFlow.FlowDirections GetTargetFlowDirection(int idx)
		{
			return this.targetFlowDirections[idx];
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x002D7AE5 File Offset: 0x002D5CE5
		public void SetTargetFlowDirection(int idx, ConduitFlow.FlowDirections directions)
		{
			this.targetFlowDirections[idx] = directions;
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x002D7AF4 File Offset: 0x002D5CF4
		public ConduitFlow.FlowDirections GetSrcFlowDirection(int idx)
		{
			return this.srcFlowDirections[idx];
		}

		// Token: 0x06007E16 RID: 32278 RVA: 0x002D7B02 File Offset: 0x002D5D02
		public void SetSrcFlowDirection(int idx, ConduitFlow.FlowDirections directions)
		{
			this.srcFlowDirections[idx] = directions;
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x002D7B11 File Offset: 0x002D5D11
		public ConduitFlow.FlowDirections GetPullDirection(int idx)
		{
			return this.pullDirections[idx];
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x002D7B1F File Offset: 0x002D5D1F
		public void SetPullDirection(int idx, ConduitFlow.FlowDirections directions)
		{
			this.pullDirections[idx] = directions;
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x002D7B2E File Offset: 0x002D5D2E
		public int GetCell(int idx)
		{
			return this.cells[idx];
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x002D7B3C File Offset: 0x002D5D3C
		public void SetCell(int idx, int cell)
		{
			this.cells[idx] = cell;
		}

		// Token: 0x0400609E RID: 24734
		private List<ConduitFlow.Conduit> conduits = new List<ConduitFlow.Conduit>();

		// Token: 0x0400609F RID: 24735
		private List<ConduitFlow.ConduitConnections> conduitConnections = new List<ConduitFlow.ConduitConnections>();

		// Token: 0x040060A0 RID: 24736
		private List<ConduitFlow.ConduitFlowInfo> lastFlowInfo = new List<ConduitFlow.ConduitFlowInfo>();

		// Token: 0x040060A1 RID: 24737
		private List<ConduitFlow.ConduitContents> initialContents = new List<ConduitFlow.ConduitContents>();

		// Token: 0x040060A2 RID: 24738
		private List<GameObject> conduitGOs = new List<GameObject>();

		// Token: 0x040060A3 RID: 24739
		private List<bool> diseaseContentsVisible = new List<bool>();

		// Token: 0x040060A4 RID: 24740
		private List<int> cells = new List<int>();

		// Token: 0x040060A5 RID: 24741
		private List<ConduitFlow.FlowDirections> permittedFlowDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x040060A6 RID: 24742
		private List<ConduitFlow.FlowDirections> srcFlowDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x040060A7 RID: 24743
		private List<ConduitFlow.FlowDirections> pullDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x040060A8 RID: 24744
		private List<ConduitFlow.FlowDirections> targetFlowDirections = new List<ConduitFlow.FlowDirections>();

		// Token: 0x040060A9 RID: 24745
		private List<HandleVector<int>.Handle> structureTemperatureHandles = new List<HandleVector<int>.Handle>();

		// Token: 0x040060AA RID: 24746
		private List<HandleVector<int>.Handle> temperatureHandles = new List<HandleVector<int>.Handle>();

		// Token: 0x040060AB RID: 24747
		private List<HandleVector<int>.Handle> diseaseHandles = new List<HandleVector<int>.Handle>();

		// Token: 0x040060AC RID: 24748
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ClearPermanentDiseaseContainer> clearPermanentDiseaseContainer = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ClearPermanentDiseaseContainer>();

		// Token: 0x040060AD RID: 24749
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToSim> publishTemperatureToSim = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToSim>();

		// Token: 0x040060AE RID: 24750
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToSim> publishDiseaseToSim = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToSim>();

		// Token: 0x040060AF RID: 24751
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ResetConduit> resetConduit = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.ResetConduit>();

		// Token: 0x040060B0 RID: 24752
		private ConduitFlow.SOAInfo.ConduitJob clearJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x040060B1 RID: 24753
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InitializeContentsTask> initializeContents = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InitializeContentsTask>();

		// Token: 0x040060B2 RID: 24754
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InvalidateLastFlow> invalidateLastFlow = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.InvalidateLastFlow>();

		// Token: 0x040060B3 RID: 24755
		private ConduitFlow.SOAInfo.ConduitJob beginFrameJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x040060B4 RID: 24756
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToGame> publishTemperatureToGame = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishTemperatureToGame>();

		// Token: 0x040060B5 RID: 24757
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToGame> publishDiseaseToGame = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.PublishDiseaseToGame>();

		// Token: 0x040060B6 RID: 24758
		private ConduitFlow.SOAInfo.ConduitJob endFrameJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x040060B7 RID: 24759
		private ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.FlowThroughVacuum> flowThroughVacuum = new ConduitFlow.SOAInfo.ConduitTaskDivision<ConduitFlow.SOAInfo.FlowThroughVacuum>();

		// Token: 0x040060B8 RID: 24760
		private ConduitFlow.SOAInfo.ConduitJob updateFlowDirectionJob = new ConduitFlow.SOAInfo.ConduitJob();

		// Token: 0x02002028 RID: 8232
		private abstract class ConduitTask : DivisibleTask<ConduitFlow.SOAInfo>
		{
			// Token: 0x0600A2B0 RID: 41648 RVA: 0x00344B99 File Offset: 0x00342D99
			public ConduitTask(string name)
				: base(name)
			{
			}

			// Token: 0x04008F79 RID: 36729
			public ConduitFlow manager;
		}

		// Token: 0x02002029 RID: 8233
		private class ConduitTaskDivision<Task> : TaskDivision<Task, ConduitFlow.SOAInfo> where Task : ConduitFlow.SOAInfo.ConduitTask, new()
		{
			// Token: 0x0600A2B1 RID: 41649 RVA: 0x00344BA4 File Offset: 0x00342DA4
			public void Initialize(int conduitCount, ConduitFlow manager)
			{
				base.Initialize(conduitCount);
				Task[] tasks = this.tasks;
				for (int i = 0; i < tasks.Length; i++)
				{
					tasks[i].manager = manager;
				}
			}
		}

		// Token: 0x0200202A RID: 8234
		private class ConduitJob : WorkItemCollection<ConduitFlow.SOAInfo.ConduitTask, ConduitFlow.SOAInfo>
		{
			// Token: 0x0600A2B3 RID: 41651 RVA: 0x00344BE8 File Offset: 0x00342DE8
			public void Add<Task>(ConduitFlow.SOAInfo.ConduitTaskDivision<Task> taskDivision) where Task : ConduitFlow.SOAInfo.ConduitTask, new()
			{
				foreach (Task task in taskDivision.tasks)
				{
					base.Add(task);
				}
			}
		}

		// Token: 0x0200202B RID: 8235
		private class ClearPermanentDiseaseContainer : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2B5 RID: 41653 RVA: 0x00344C26 File Offset: 0x00342E26
			public ClearPermanentDiseaseContainer()
				: base("ClearPermanentDiseaseContainer")
			{
			}

			// Token: 0x0600A2B6 RID: 41654 RVA: 0x00344C34 File Offset: 0x00342E34
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					soaInfo.ForcePermanentDiseaseContainer(num, false);
				}
			}
		}

		// Token: 0x0200202C RID: 8236
		private class PublishTemperatureToSim : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2B7 RID: 41655 RVA: 0x00344C5F File Offset: 0x00342E5F
			public PublishTemperatureToSim()
				: base("PublishTemperatureToSim")
			{
			}

			// Token: 0x0600A2B8 RID: 41656 RVA: 0x00344C6C File Offset: 0x00342E6C
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					HandleVector<int>.Handle handle = soaInfo.temperatureHandles[num];
					if (handle.IsValid())
					{
						float temperature = Game.Instance.conduitTemperatureManager.GetTemperature(handle);
						this.manager.grid[soaInfo.cells[num]].contents.temperature = temperature;
					}
				}
			}
		}

		// Token: 0x0200202D RID: 8237
		private class PublishDiseaseToSim : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2B9 RID: 41657 RVA: 0x00344CDD File Offset: 0x00342EDD
			public PublishDiseaseToSim()
				: base("PublishDiseaseToSim")
			{
			}

			// Token: 0x0600A2BA RID: 41658 RVA: 0x00344CEC File Offset: 0x00342EEC
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					HandleVector<int>.Handle handle = soaInfo.diseaseHandles[num];
					if (handle.IsValid())
					{
						ConduitDiseaseManager.Data data = Game.Instance.conduitDiseaseManager.GetData(handle);
						int num2 = soaInfo.cells[num];
						this.manager.grid[num2].contents.diseaseIdx = data.diseaseIdx;
						this.manager.grid[num2].contents.diseaseCount = data.diseaseCount;
					}
				}
			}
		}

		// Token: 0x0200202E RID: 8238
		private class ResetConduit : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2BB RID: 41659 RVA: 0x00344D88 File Offset: 0x00342F88
			public ResetConduit()
				: base("ResetConduitTask")
			{
			}

			// Token: 0x0600A2BC RID: 41660 RVA: 0x00344D98 File Offset: 0x00342F98
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					this.manager.grid[soaInfo.cells[num]].conduitIdx = -1;
				}
			}
		}

		// Token: 0x0200202F RID: 8239
		private class InitializeContentsTask : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2BD RID: 41661 RVA: 0x00344DDD File Offset: 0x00342FDD
			public InitializeContentsTask()
				: base("SetInitialContents")
			{
			}

			// Token: 0x0600A2BE RID: 41662 RVA: 0x00344DEC File Offset: 0x00342FEC
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					int num2 = soaInfo.cells[num];
					ConduitFlow.ConduitContents conduitContents = soaInfo.conduits[num].GetContents(this.manager);
					if (conduitContents.mass <= 0f)
					{
						conduitContents = ConduitFlow.ConduitContents.Empty;
					}
					soaInfo.initialContents[num] = conduitContents;
					this.manager.grid[num2].contents = conduitContents;
				}
			}
		}

		// Token: 0x02002030 RID: 8240
		private class InvalidateLastFlow : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2BF RID: 41663 RVA: 0x00344E6F File Offset: 0x0034306F
			public InvalidateLastFlow()
				: base("InvalidateLastFlow")
			{
			}

			// Token: 0x0600A2C0 RID: 41664 RVA: 0x00344E7C File Offset: 0x0034307C
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					soaInfo.lastFlowInfo[num] = ConduitFlow.ConduitFlowInfo.DEFAULT;
				}
			}
		}

		// Token: 0x02002031 RID: 8241
		private class PublishTemperatureToGame : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2C1 RID: 41665 RVA: 0x00344EB0 File Offset: 0x003430B0
			public PublishTemperatureToGame()
				: base("PublishTemperatureToGame")
			{
			}

			// Token: 0x0600A2C2 RID: 41666 RVA: 0x00344EC0 File Offset: 0x003430C0
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					Game.Instance.conduitTemperatureManager.SetData(soaInfo.temperatureHandles[num], ref this.manager.grid[soaInfo.cells[num]].contents);
				}
			}
		}

		// Token: 0x02002032 RID: 8242
		private class PublishDiseaseToGame : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2C3 RID: 41667 RVA: 0x00344F1F File Offset: 0x0034311F
			public PublishDiseaseToGame()
				: base("PublishDiseaseToGame")
			{
			}

			// Token: 0x0600A2C4 RID: 41668 RVA: 0x00344F2C File Offset: 0x0034312C
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					Game.Instance.conduitDiseaseManager.SetData(soaInfo.diseaseHandles[num], ref this.manager.grid[soaInfo.cells[num]].contents);
				}
			}
		}

		// Token: 0x02002033 RID: 8243
		private class FlowThroughVacuum : ConduitFlow.SOAInfo.ConduitTask
		{
			// Token: 0x0600A2C5 RID: 41669 RVA: 0x00344F8B File Offset: 0x0034318B
			public FlowThroughVacuum()
				: base("FlowThroughVacuum")
			{
			}

			// Token: 0x0600A2C6 RID: 41670 RVA: 0x00344F98 File Offset: 0x00343198
			protected override void RunDivision(ConduitFlow.SOAInfo soaInfo)
			{
				for (int num = this.start; num != this.end; num++)
				{
					ConduitFlow.Conduit conduit = soaInfo.conduits[num];
					int cell = conduit.GetCell(this.manager);
					if (this.manager.grid[cell].contents.element == SimHashes.Vacuum)
					{
						soaInfo.srcFlowDirections[conduit.idx] = ConduitFlow.FlowDirections.None;
					}
				}
			}
		}
	}

	// Token: 0x02001377 RID: 4983
	[DebuggerDisplay("{priority} {callback.Target.name} {callback.Target} {callback.Method}")]
	public struct ConduitUpdater
	{
		// Token: 0x040060B9 RID: 24761
		public ConduitFlowPriority priority;

		// Token: 0x040060BA RID: 24762
		public Action<float> callback;
	}

	// Token: 0x02001378 RID: 4984
	[DebuggerDisplay("conduit {conduitIdx}:{contents.element}")]
	public struct GridNode
	{
		// Token: 0x040060BB RID: 24763
		public int conduitIdx;

		// Token: 0x040060BC RID: 24764
		public ConduitFlow.ConduitContents contents;
	}

	// Token: 0x02001379 RID: 4985
	public struct SerializedContents
	{
		// Token: 0x06007E1C RID: 32284 RVA: 0x002D7C88 File Offset: 0x002D5E88
		public SerializedContents(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count)
		{
			this.element = element;
			this.mass = mass;
			this.temperature = temperature;
			this.diseaseHash = ((disease_idx != byte.MaxValue) ? Db.Get().Diseases[(int)disease_idx].id.GetHashCode() : 0);
			this.diseaseCount = disease_count;
			if (this.diseaseCount <= 0)
			{
				this.diseaseHash = 0;
			}
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x002D7CF5 File Offset: 0x002D5EF5
		public SerializedContents(ConduitFlow.ConduitContents src)
		{
			this = new ConduitFlow.SerializedContents(src.element, src.mass, src.temperature, src.diseaseIdx, src.diseaseCount);
		}

		// Token: 0x040060BD RID: 24765
		public SimHashes element;

		// Token: 0x040060BE RID: 24766
		public float mass;

		// Token: 0x040060BF RID: 24767
		public float temperature;

		// Token: 0x040060C0 RID: 24768
		public int diseaseHash;

		// Token: 0x040060C1 RID: 24769
		public int diseaseCount;
	}

	// Token: 0x0200137A RID: 4986
	[Flags]
	public enum FlowDirections : byte
	{
		// Token: 0x040060C3 RID: 24771
		None = 0,
		// Token: 0x040060C4 RID: 24772
		Down = 1,
		// Token: 0x040060C5 RID: 24773
		Left = 2,
		// Token: 0x040060C6 RID: 24774
		Right = 4,
		// Token: 0x040060C7 RID: 24775
		Up = 8,
		// Token: 0x040060C8 RID: 24776
		All = 15
	}

	// Token: 0x0200137B RID: 4987
	[DebuggerDisplay("conduits l:{left}, r:{right}, u:{up}, d:{down}")]
	public struct ConduitConnections
	{
		// Token: 0x040060C9 RID: 24777
		public int left;

		// Token: 0x040060CA RID: 24778
		public int right;

		// Token: 0x040060CB RID: 24779
		public int up;

		// Token: 0x040060CC RID: 24780
		public int down;

		// Token: 0x040060CD RID: 24781
		public static readonly ConduitFlow.ConduitConnections DEFAULT = new ConduitFlow.ConduitConnections
		{
			left = -1,
			right = -1,
			up = -1,
			down = -1
		};
	}

	// Token: 0x0200137C RID: 4988
	[DebuggerDisplay("{direction}:{contents.element}")]
	public struct ConduitFlowInfo
	{
		// Token: 0x040060CE RID: 24782
		public ConduitFlow.FlowDirections direction;

		// Token: 0x040060CF RID: 24783
		public ConduitFlow.ConduitContents contents;

		// Token: 0x040060D0 RID: 24784
		public static readonly ConduitFlow.ConduitFlowInfo DEFAULT = new ConduitFlow.ConduitFlowInfo
		{
			direction = ConduitFlow.FlowDirections.None,
			contents = ConduitFlow.ConduitContents.Empty
		};
	}

	// Token: 0x0200137D RID: 4989
	[DebuggerDisplay("conduit {idx}")]
	[Serializable]
	public struct Conduit : IEquatable<ConduitFlow.Conduit>
	{
		// Token: 0x06007E20 RID: 32288 RVA: 0x002D7D87 File Offset: 0x002D5F87
		public Conduit(int idx)
		{
			this.idx = idx;
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x002D7D90 File Offset: 0x002D5F90
		public ConduitFlow.FlowDirections GetPermittedFlowDirections(ConduitFlow manager)
		{
			return manager.soaInfo.GetPermittedFlowDirections(this.idx);
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x002D7DA3 File Offset: 0x002D5FA3
		public void SetPermittedFlowDirections(ConduitFlow.FlowDirections permitted, ConduitFlow manager)
		{
			manager.soaInfo.SetPermittedFlowDirections(this.idx, permitted);
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x002D7DB7 File Offset: 0x002D5FB7
		public ConduitFlow.FlowDirections GetTargetFlowDirection(ConduitFlow manager)
		{
			return manager.soaInfo.GetTargetFlowDirection(this.idx);
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x002D7DCA File Offset: 0x002D5FCA
		public void SetTargetFlowDirection(ConduitFlow.FlowDirections directions, ConduitFlow manager)
		{
			manager.soaInfo.SetTargetFlowDirection(this.idx, directions);
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x002D7DE0 File Offset: 0x002D5FE0
		public ConduitFlow.ConduitContents GetContents(ConduitFlow manager)
		{
			int cell = manager.soaInfo.GetCell(this.idx);
			ConduitFlow.ConduitContents contents = manager.grid[cell].contents;
			ConduitFlow.SOAInfo soaInfo = manager.soaInfo;
			contents.temperature = soaInfo.GetConduitTemperature(this.idx);
			ConduitDiseaseManager.Data diseaseData = soaInfo.GetDiseaseData(this.idx);
			contents.diseaseIdx = diseaseData.diseaseIdx;
			contents.diseaseCount = diseaseData.diseaseCount;
			return contents;
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x002D7E54 File Offset: 0x002D6054
		public void SetContents(ConduitFlow manager, ConduitFlow.ConduitContents contents)
		{
			int cell = manager.soaInfo.GetCell(this.idx);
			manager.grid[cell].contents = contents;
			ConduitFlow.SOAInfo soaInfo = manager.soaInfo;
			soaInfo.SetConduitTemperatureData(this.idx, ref contents);
			soaInfo.ForcePermanentDiseaseContainer(this.idx, contents.diseaseIdx != byte.MaxValue);
			soaInfo.SetDiseaseData(this.idx, ref contents);
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x002D7EC2 File Offset: 0x002D60C2
		public ConduitFlow.ConduitFlowInfo GetLastFlowInfo(ConduitFlow manager)
		{
			return manager.soaInfo.GetLastFlowInfo(this.idx);
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x002D7ED5 File Offset: 0x002D60D5
		public ConduitFlow.ConduitContents GetInitialContents(ConduitFlow manager)
		{
			return manager.soaInfo.GetInitialContents(this.idx);
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x002D7EE8 File Offset: 0x002D60E8
		public int GetCell(ConduitFlow manager)
		{
			return manager.soaInfo.GetCell(this.idx);
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x002D7EFB File Offset: 0x002D60FB
		public bool Equals(ConduitFlow.Conduit other)
		{
			return this.idx == other.idx;
		}

		// Token: 0x040060D1 RID: 24785
		public static readonly ConduitFlow.Conduit Invalid = new ConduitFlow.Conduit(-1);

		// Token: 0x040060D2 RID: 24786
		public readonly int idx;
	}

	// Token: 0x0200137E RID: 4990
	[DebuggerDisplay("{element} M:{mass} T:{temperature}")]
	public struct ConduitContents
	{
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06007E2C RID: 32300 RVA: 0x002D7F18 File Offset: 0x002D6118
		public float mass
		{
			get
			{
				return this.initial_mass + this.added_mass - this.removed_mass;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06007E2D RID: 32301 RVA: 0x002D7F2E File Offset: 0x002D612E
		public float movable_mass
		{
			get
			{
				return this.initial_mass - this.removed_mass;
			}
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x002D7F40 File Offset: 0x002D6140
		public ConduitContents(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count)
		{
			global::Debug.Assert(!float.IsNaN(temperature));
			this.element = element;
			this.initial_mass = mass;
			this.added_mass = 0f;
			this.removed_mass = 0f;
			this.temperature = temperature;
			this.diseaseIdx = disease_idx;
			this.diseaseCount = disease_count;
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x002D7F96 File Offset: 0x002D6196
		public void ConsolidateMass()
		{
			this.initial_mass += this.added_mass;
			this.added_mass = 0f;
			this.initial_mass -= this.removed_mass;
			this.removed_mass = 0f;
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x002D7FD4 File Offset: 0x002D61D4
		public float GetEffectiveCapacity(float maximum_capacity)
		{
			float mass = this.mass;
			return Mathf.Max(0f, maximum_capacity - mass);
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x002D7FF5 File Offset: 0x002D61F5
		public void AddMass(float amount)
		{
			global::Debug.Assert(0f <= amount);
			this.added_mass += amount;
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x002D8018 File Offset: 0x002D6218
		public float RemoveMass(float amount)
		{
			global::Debug.Assert(0f <= amount);
			float num = 0f;
			float num2 = this.mass - amount;
			if (num2 < 0f)
			{
				amount += num2;
				num = -num2;
				global::Debug.Assert(false);
			}
			this.removed_mass += amount;
			return num;
		}

		// Token: 0x040060D3 RID: 24787
		public SimHashes element;

		// Token: 0x040060D4 RID: 24788
		private float initial_mass;

		// Token: 0x040060D5 RID: 24789
		private float added_mass;

		// Token: 0x040060D6 RID: 24790
		private float removed_mass;

		// Token: 0x040060D7 RID: 24791
		public float temperature;

		// Token: 0x040060D8 RID: 24792
		public byte diseaseIdx;

		// Token: 0x040060D9 RID: 24793
		public int diseaseCount;

		// Token: 0x040060DA RID: 24794
		public static readonly ConduitFlow.ConduitContents Empty = new ConduitFlow.ConduitContents
		{
			element = SimHashes.Vacuum,
			initial_mass = 0f,
			added_mass = 0f,
			removed_mass = 0f,
			temperature = 0f,
			diseaseIdx = byte.MaxValue,
			diseaseCount = 0
		};
	}

	// Token: 0x0200137F RID: 4991
	[DebuggerDisplay("{network.ConduitType}:{cells.Count}")]
	private struct Network
	{
		// Token: 0x040060DB RID: 24795
		public List<int> cells;

		// Token: 0x040060DC RID: 24796
		public FlowUtilityNetwork network;
	}

	// Token: 0x02001380 RID: 4992
	private struct BuildNetworkTask : IWorkItem<ConduitFlow>
	{
		// Token: 0x06007E34 RID: 32308 RVA: 0x002D80D8 File Offset: 0x002D62D8
		public BuildNetworkTask(ConduitFlow.Network network, int conduit_count)
		{
			this.network = network;
			this.distance_nodes = QueuePool<ConduitFlow.BuildNetworkTask.DistanceNode, ConduitFlow>.Allocate();
			this.distances_via_sources = DictionaryPool<int, int, ConduitFlow>.Allocate();
			this.from_sources = ListPool<KeyValuePair<int, int>, ConduitFlow>.Allocate();
			this.distances_via_sinks = DictionaryPool<int, int, ConduitFlow>.Allocate();
			this.from_sinks = ListPool<KeyValuePair<int, int>, ConduitFlow>.Allocate();
			this.from_sources_graph = new ConduitFlow.BuildNetworkTask.Graph(network.network);
			this.from_sinks_graph = new ConduitFlow.BuildNetworkTask.Graph(network.network);
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x002D8148 File Offset: 0x002D6348
		public void Finish()
		{
			this.distances_via_sinks.Recycle();
			this.distances_via_sources.Recycle();
			this.distance_nodes.Recycle();
			this.from_sources.Recycle();
			this.from_sinks.Recycle();
			this.from_sources_graph.Recycle();
			this.from_sinks_graph.Recycle();
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x002D81A4 File Offset: 0x002D63A4
		private void ComputeFlow(ConduitFlow outer)
		{
			this.from_sources_graph.Build(outer, this.network.network.sources, this.network.network.sinks, true);
			this.from_sinks_graph.Build(outer, this.network.network.sinks, this.network.network.sources, false);
			this.from_sources_graph.Merge(this.from_sinks_graph);
			this.from_sources_graph.BreakCycles();
			this.from_sources_graph.WriteFlow(false);
			this.from_sinks_graph.WriteFlow(true);
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x002D8240 File Offset: 0x002D6440
		private void ComputeOrder(ConduitFlow outer)
		{
			foreach (int num in this.from_sources_graph.sources)
			{
				this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
				{
					cell = num,
					distance = 0
				});
			}
			using (HashSet<int>.Enumerator enumerator = this.from_sources_graph.dead_ends.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = num2,
						distance = 0
					});
				}
				goto IL_21D;
			}
			IL_B3:
			ConduitFlow.BuildNetworkTask.DistanceNode distanceNode = this.distance_nodes.Dequeue();
			int conduitIdx = outer.grid[distanceNode.cell].conduitIdx;
			if (conduitIdx != -1)
			{
				this.distances_via_sources[distanceNode.cell] = distanceNode.distance;
				ConduitFlow.ConduitConnections conduitConnections = outer.soaInfo.GetConduitConnections(conduitIdx);
				ConduitFlow.FlowDirections permittedFlowDirections = outer.soaInfo.GetPermittedFlowDirections(conduitIdx);
				if ((permittedFlowDirections & ConduitFlow.FlowDirections.Up) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections.up),
						distance = distanceNode.distance + 1
					});
				}
				if ((permittedFlowDirections & ConduitFlow.FlowDirections.Down) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections.down),
						distance = distanceNode.distance + 1
					});
				}
				if ((permittedFlowDirections & ConduitFlow.FlowDirections.Left) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections.left),
						distance = distanceNode.distance + 1
					});
				}
				if ((permittedFlowDirections & ConduitFlow.FlowDirections.Right) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections.right),
						distance = distanceNode.distance + 1
					});
				}
			}
			IL_21D:
			if (this.distance_nodes.Count != 0)
			{
				goto IL_B3;
			}
			this.from_sources.AddRange(this.distances_via_sources);
			this.from_sources.Sort((KeyValuePair<int, int> a, KeyValuePair<int, int> b) => b.Value - a.Value);
			this.distance_nodes.Clear();
			foreach (int num3 in this.from_sinks_graph.sources)
			{
				this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
				{
					cell = num3,
					distance = 0
				});
			}
			using (HashSet<int>.Enumerator enumerator = this.from_sinks_graph.dead_ends.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num4 = enumerator.Current;
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = num4,
						distance = 0
					});
				}
				goto IL_508;
			}
			IL_32A:
			ConduitFlow.BuildNetworkTask.DistanceNode distanceNode2 = this.distance_nodes.Dequeue();
			int conduitIdx2 = outer.grid[distanceNode2.cell].conduitIdx;
			if (conduitIdx2 != -1)
			{
				if (!this.distances_via_sources.ContainsKey(distanceNode2.cell))
				{
					this.distances_via_sinks[distanceNode2.cell] = distanceNode2.distance;
				}
				ConduitFlow.ConduitConnections conduitConnections2 = outer.soaInfo.GetConduitConnections(conduitIdx2);
				if (conduitConnections2.up != -1 && (outer.soaInfo.GetPermittedFlowDirections(conduitConnections2.up) & ConduitFlow.FlowDirections.Down) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections2.up),
						distance = distanceNode2.distance + 1
					});
				}
				if (conduitConnections2.down != -1 && (outer.soaInfo.GetPermittedFlowDirections(conduitConnections2.down) & ConduitFlow.FlowDirections.Up) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections2.down),
						distance = distanceNode2.distance + 1
					});
				}
				if (conduitConnections2.left != -1 && (outer.soaInfo.GetPermittedFlowDirections(conduitConnections2.left) & ConduitFlow.FlowDirections.Right) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections2.left),
						distance = distanceNode2.distance + 1
					});
				}
				if (conduitConnections2.right != -1 && (outer.soaInfo.GetPermittedFlowDirections(conduitConnections2.right) & ConduitFlow.FlowDirections.Left) != ConduitFlow.FlowDirections.None)
				{
					this.distance_nodes.Enqueue(new ConduitFlow.BuildNetworkTask.DistanceNode
					{
						cell = outer.soaInfo.GetCell(conduitConnections2.right),
						distance = distanceNode2.distance + 1
					});
				}
			}
			IL_508:
			if (this.distance_nodes.Count == 0)
			{
				this.from_sinks.AddRange(this.distances_via_sinks);
				this.from_sinks.Sort((KeyValuePair<int, int> a, KeyValuePair<int, int> b) => a.Value - b.Value);
				this.network.cells.Capacity = Mathf.Max(this.network.cells.Capacity, this.from_sources.Count + this.from_sinks.Count);
				foreach (KeyValuePair<int, int> keyValuePair in this.from_sources)
				{
					this.network.cells.Add(keyValuePair.Key);
				}
				foreach (KeyValuePair<int, int> keyValuePair2 in this.from_sinks)
				{
					this.network.cells.Add(keyValuePair2.Key);
				}
				return;
			}
			goto IL_32A;
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x002D88B8 File Offset: 0x002D6AB8
		public void Run(ConduitFlow outer)
		{
			this.ComputeFlow(outer);
			this.ComputeOrder(outer);
		}

		// Token: 0x040060DD RID: 24797
		private ConduitFlow.Network network;

		// Token: 0x040060DE RID: 24798
		private QueuePool<ConduitFlow.BuildNetworkTask.DistanceNode, ConduitFlow>.PooledQueue distance_nodes;

		// Token: 0x040060DF RID: 24799
		private DictionaryPool<int, int, ConduitFlow>.PooledDictionary distances_via_sources;

		// Token: 0x040060E0 RID: 24800
		private ListPool<KeyValuePair<int, int>, ConduitFlow>.PooledList from_sources;

		// Token: 0x040060E1 RID: 24801
		private DictionaryPool<int, int, ConduitFlow>.PooledDictionary distances_via_sinks;

		// Token: 0x040060E2 RID: 24802
		private ListPool<KeyValuePair<int, int>, ConduitFlow>.PooledList from_sinks;

		// Token: 0x040060E3 RID: 24803
		private ConduitFlow.BuildNetworkTask.Graph from_sources_graph;

		// Token: 0x040060E4 RID: 24804
		private ConduitFlow.BuildNetworkTask.Graph from_sinks_graph;

		// Token: 0x02002034 RID: 8244
		[DebuggerDisplay("cell {cell}:{distance}")]
		private struct DistanceNode
		{
			// Token: 0x04008F7A RID: 36730
			public int cell;

			// Token: 0x04008F7B RID: 36731
			public int distance;
		}

		// Token: 0x02002035 RID: 8245
		[DebuggerDisplay("vertices:{vertex_cells.Count}, edges:{edges.Count}")]
		private struct Graph
		{
			// Token: 0x0600A2C7 RID: 41671 RVA: 0x0034500C File Offset: 0x0034320C
			public Graph(FlowUtilityNetwork network)
			{
				this.conduit_flow = null;
				this.vertex_cells = HashSetPool<int, ConduitFlow>.Allocate();
				this.edges = ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.Allocate();
				this.cycles = ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.Allocate();
				this.bfs_traversal = QueuePool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.Allocate();
				this.visited = HashSetPool<int, ConduitFlow>.Allocate();
				this.pseudo_sources = ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.Allocate();
				this.sources = HashSetPool<int, ConduitFlow>.Allocate();
				this.sinks = HashSetPool<int, ConduitFlow>.Allocate();
				this.dfs_path = HashSetPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.Allocate();
				this.dfs_traversal = ListPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.Allocate();
				this.dead_ends = HashSetPool<int, ConduitFlow>.Allocate();
				this.cycle_vertices = ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.Allocate();
			}

			// Token: 0x0600A2C8 RID: 41672 RVA: 0x003450A4 File Offset: 0x003432A4
			public void Recycle()
			{
				this.vertex_cells.Recycle();
				this.edges.Recycle();
				this.cycles.Recycle();
				this.bfs_traversal.Recycle();
				this.visited.Recycle();
				this.pseudo_sources.Recycle();
				this.sources.Recycle();
				this.sinks.Recycle();
				this.dfs_path.Recycle();
				this.dfs_traversal.Recycle();
				this.dead_ends.Recycle();
				this.cycle_vertices.Recycle();
			}

			// Token: 0x0600A2C9 RID: 41673 RVA: 0x00345138 File Offset: 0x00343338
			public void Build(ConduitFlow conduit_flow, List<FlowUtilityNetwork.IItem> sources, List<FlowUtilityNetwork.IItem> sinks, bool are_dead_ends_pseudo_sources)
			{
				this.conduit_flow = conduit_flow;
				this.sources.Clear();
				for (int i = 0; i < sources.Count; i++)
				{
					int cell = sources[i].Cell;
					if (conduit_flow.grid[cell].conduitIdx != -1)
					{
						this.sources.Add(cell);
					}
				}
				this.sinks.Clear();
				for (int j = 0; j < sinks.Count; j++)
				{
					int cell2 = sinks[j].Cell;
					if (conduit_flow.grid[cell2].conduitIdx != -1)
					{
						this.sinks.Add(cell2);
					}
				}
				global::Debug.Assert(this.bfs_traversal.Count == 0);
				this.visited.Clear();
				foreach (int num in this.sources)
				{
					this.bfs_traversal.Enqueue(new ConduitFlow.BuildNetworkTask.Graph.Vertex
					{
						cell = num,
						direction = ConduitFlow.FlowDirections.None
					});
					this.visited.Add(num);
				}
				this.pseudo_sources.Clear();
				this.dead_ends.Clear();
				this.cycles.Clear();
				while (this.bfs_traversal.Count != 0)
				{
					ConduitFlow.BuildNetworkTask.Graph.Vertex node = this.bfs_traversal.Dequeue();
					this.vertex_cells.Add(node.cell);
					ConduitFlow.FlowDirections flowDirections = ConduitFlow.FlowDirections.None;
					int num2 = 4;
					if (node.direction != ConduitFlow.FlowDirections.None)
					{
						flowDirections = ConduitFlow.Opposite(node.direction);
						num2 = 3;
					}
					int conduitIdx = conduit_flow.grid[node.cell].conduitIdx;
					for (int num3 = 0; num3 != num2; num3++)
					{
						flowDirections = ConduitFlow.ComputeNextFlowDirection(flowDirections);
						ConduitFlow.Conduit conduitFromDirection = conduit_flow.soaInfo.GetConduitFromDirection(conduitIdx, flowDirections);
						ConduitFlow.BuildNetworkTask.Graph.Vertex new_node = this.WalkPath(conduitIdx, conduitFromDirection.idx, flowDirections, are_dead_ends_pseudo_sources);
						if (new_node.is_valid)
						{
							ConduitFlow.BuildNetworkTask.Graph.Edge edge2 = new ConduitFlow.BuildNetworkTask.Graph.Edge
							{
								vertices = new ConduitFlow.BuildNetworkTask.Graph.Vertex[]
								{
									new ConduitFlow.BuildNetworkTask.Graph.Vertex
									{
										cell = node.cell,
										direction = flowDirections
									},
									new_node
								}
							};
							if (new_node.cell == node.cell)
							{
								this.cycles.Add(edge2);
							}
							else if (!this.edges.Any((ConduitFlow.BuildNetworkTask.Graph.Edge edge) => edge.vertices[0].cell == new_node.cell && edge.vertices[1].cell == node.cell) && !this.edges.Contains(edge2))
							{
								this.edges.Add(edge2);
								if (this.visited.Add(new_node.cell))
								{
									if (this.IsSink(new_node.cell))
									{
										this.pseudo_sources.Add(new_node);
									}
									else
									{
										this.bfs_traversal.Enqueue(new_node);
									}
								}
							}
						}
					}
					if (this.bfs_traversal.Count == 0)
					{
						foreach (ConduitFlow.BuildNetworkTask.Graph.Vertex vertex in this.pseudo_sources)
						{
							this.bfs_traversal.Enqueue(vertex);
						}
						this.pseudo_sources.Clear();
					}
				}
			}

			// Token: 0x0600A2CA RID: 41674 RVA: 0x00345504 File Offset: 0x00343704
			private bool IsEndpoint(int cell)
			{
				global::Debug.Assert(cell != -1);
				return this.conduit_flow.grid[cell].conduitIdx == -1 || this.sources.Contains(cell) || this.sinks.Contains(cell) || this.dead_ends.Contains(cell);
			}

			// Token: 0x0600A2CB RID: 41675 RVA: 0x00345560 File Offset: 0x00343760
			private bool IsSink(int cell)
			{
				return this.sinks.Contains(cell);
			}

			// Token: 0x0600A2CC RID: 41676 RVA: 0x00345570 File Offset: 0x00343770
			private bool IsJunction(int cell)
			{
				global::Debug.Assert(cell != -1);
				ConduitFlow.GridNode gridNode = this.conduit_flow.grid[cell];
				global::Debug.Assert(gridNode.conduitIdx != -1);
				ConduitFlow.ConduitConnections conduitConnections = this.conduit_flow.soaInfo.GetConduitConnections(gridNode.conduitIdx);
				return 2 < this.JunctionValue(conduitConnections.down) + this.JunctionValue(conduitConnections.left) + this.JunctionValue(conduitConnections.up) + this.JunctionValue(conduitConnections.right);
			}

			// Token: 0x0600A2CD RID: 41677 RVA: 0x003455F9 File Offset: 0x003437F9
			private int JunctionValue(int conduit)
			{
				if (conduit != -1)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x0600A2CE RID: 41678 RVA: 0x00345604 File Offset: 0x00343804
			private ConduitFlow.BuildNetworkTask.Graph.Vertex WalkPath(int root_conduit, int conduit, ConduitFlow.FlowDirections direction, bool are_dead_ends_pseudo_sources)
			{
				if (conduit == -1)
				{
					return ConduitFlow.BuildNetworkTask.Graph.Vertex.INVALID;
				}
				int cell;
				for (;;)
				{
					cell = this.conduit_flow.soaInfo.GetCell(conduit);
					if (this.IsEndpoint(cell) || this.IsJunction(cell))
					{
						break;
					}
					direction = ConduitFlow.Opposite(direction);
					bool flag = true;
					for (int num = 0; num != 3; num++)
					{
						direction = ConduitFlow.ComputeNextFlowDirection(direction);
						ConduitFlow.Conduit conduitFromDirection = this.conduit_flow.soaInfo.GetConduitFromDirection(conduit, direction);
						if (conduitFromDirection.idx != -1)
						{
							conduit = conduitFromDirection.idx;
							flag = false;
							break;
						}
					}
					if (flag)
					{
						goto Block_4;
					}
				}
				return new ConduitFlow.BuildNetworkTask.Graph.Vertex
				{
					cell = cell,
					direction = direction
				};
				Block_4:
				if (are_dead_ends_pseudo_sources)
				{
					this.pseudo_sources.Add(new ConduitFlow.BuildNetworkTask.Graph.Vertex
					{
						cell = cell,
						direction = ConduitFlow.ComputeNextFlowDirection(direction)
					});
					this.dead_ends.Add(cell);
					return ConduitFlow.BuildNetworkTask.Graph.Vertex.INVALID;
				}
				ConduitFlow.BuildNetworkTask.Graph.Vertex vertex = default(ConduitFlow.BuildNetworkTask.Graph.Vertex);
				vertex.cell = cell;
				direction = (vertex.direction = ConduitFlow.Opposite(ConduitFlow.ComputeNextFlowDirection(direction)));
				return vertex;
			}

			// Token: 0x0600A2CF RID: 41679 RVA: 0x00345710 File Offset: 0x00343910
			public void Merge(ConduitFlow.BuildNetworkTask.Graph inverted_graph)
			{
				using (List<ConduitFlow.BuildNetworkTask.Graph.Edge>.Enumerator enumerator = inverted_graph.edges.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ConduitFlow.BuildNetworkTask.Graph.Edge inverted_edge2 = enumerator.Current;
						ConduitFlow.BuildNetworkTask.Graph.Edge candidate = inverted_edge2.Invert();
						if (!this.edges.Any((ConduitFlow.BuildNetworkTask.Graph.Edge edge) => edge.Equals(inverted_edge2) || edge.Equals(candidate)))
						{
							this.edges.Add(candidate);
							this.vertex_cells.Add(candidate.vertices[0].cell);
							this.vertex_cells.Add(candidate.vertices[1].cell);
						}
					}
				}
				int num = 1000;
				for (int num2 = 0; num2 != num; num2++)
				{
					global::Debug.Assert(num2 != num - 1);
					bool flag = false;
					using (HashSet<int>.Enumerator enumerator2 = this.vertex_cells.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int cell = enumerator2.Current;
							if (!this.IsSink(cell) && !this.edges.Any((ConduitFlow.BuildNetworkTask.Graph.Edge edge) => edge.vertices[0].cell == cell))
							{
								int num3 = inverted_graph.edges.FindIndex((ConduitFlow.BuildNetworkTask.Graph.Edge inverted_edge) => inverted_edge.vertices[1].cell == cell);
								if (num3 != -1)
								{
									ConduitFlow.BuildNetworkTask.Graph.Edge edge3 = inverted_graph.edges[num3];
									for (int num4 = 0; num4 != this.edges.Count; num4++)
									{
										ConduitFlow.BuildNetworkTask.Graph.Edge edge2 = this.edges[num4];
										if (edge2.vertices[0].cell == edge3.vertices[0].cell && edge2.vertices[1].cell == edge3.vertices[1].cell)
										{
											this.edges[num4] = edge2.Invert();
										}
									}
									flag = true;
									break;
								}
							}
						}
					}
					if (!flag)
					{
						break;
					}
				}
			}

			// Token: 0x0600A2D0 RID: 41680 RVA: 0x00345974 File Offset: 0x00343B74
			public void BreakCycles()
			{
				this.visited.Clear();
				foreach (int num in this.vertex_cells)
				{
					if (!this.visited.Contains(num))
					{
						this.dfs_path.Clear();
						this.dfs_traversal.Clear();
						this.dfs_traversal.Add(new ConduitFlow.BuildNetworkTask.Graph.DFSNode
						{
							cell = num,
							parent = null
						});
						while (this.dfs_traversal.Count != 0)
						{
							ConduitFlow.BuildNetworkTask.Graph.DFSNode dfsnode = this.dfs_traversal[this.dfs_traversal.Count - 1];
							this.dfs_traversal.RemoveAt(this.dfs_traversal.Count - 1);
							bool flag = false;
							for (ConduitFlow.BuildNetworkTask.Graph.DFSNode dfsnode2 = dfsnode.parent; dfsnode2 != null; dfsnode2 = dfsnode2.parent)
							{
								if (dfsnode2.cell == dfsnode.cell)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								for (int num2 = this.edges.Count - 1; num2 != -1; num2--)
								{
									ConduitFlow.BuildNetworkTask.Graph.Edge edge = this.edges[num2];
									if (edge.vertices[0].cell == dfsnode.parent.cell && edge.vertices[1].cell == dfsnode.cell)
									{
										this.cycles.Add(edge);
										this.edges.RemoveAt(num2);
									}
								}
							}
							else if (this.visited.Add(dfsnode.cell))
							{
								foreach (ConduitFlow.BuildNetworkTask.Graph.Edge edge2 in this.edges)
								{
									if (edge2.vertices[0].cell == dfsnode.cell)
									{
										this.dfs_traversal.Add(new ConduitFlow.BuildNetworkTask.Graph.DFSNode
										{
											cell = edge2.vertices[1].cell,
											parent = dfsnode
										});
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600A2D1 RID: 41681 RVA: 0x00345BC4 File Offset: 0x00343DC4
			public void WriteFlow(bool cycles_only = false)
			{
				if (!cycles_only)
				{
					foreach (ConduitFlow.BuildNetworkTask.Graph.Edge edge in this.edges)
					{
						ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator vertexIterator = edge.Iter(this.conduit_flow);
						while (vertexIterator.IsValid())
						{
							this.conduit_flow.soaInfo.AddPermittedFlowDirections(this.conduit_flow.grid[vertexIterator.cell].conduitIdx, vertexIterator.direction);
							vertexIterator.Next();
						}
					}
				}
				foreach (ConduitFlow.BuildNetworkTask.Graph.Edge edge2 in this.cycles)
				{
					this.cycle_vertices.Clear();
					ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator vertexIterator2 = edge2.Iter(this.conduit_flow);
					vertexIterator2.Next();
					while (vertexIterator2.IsValid())
					{
						this.cycle_vertices.Add(new ConduitFlow.BuildNetworkTask.Graph.Vertex
						{
							cell = vertexIterator2.cell,
							direction = vertexIterator2.direction
						});
						vertexIterator2.Next();
					}
					if (this.cycle_vertices.Count > 1)
					{
						int i = 0;
						int num = this.cycle_vertices.Count - 1;
						ConduitFlow.FlowDirections flowDirections = edge2.vertices[0].direction;
						while (i <= num)
						{
							ConduitFlow.BuildNetworkTask.Graph.Vertex vertex = this.cycle_vertices[i];
							this.conduit_flow.soaInfo.AddPermittedFlowDirections(this.conduit_flow.grid[vertex.cell].conduitIdx, ConduitFlow.Opposite(flowDirections));
							flowDirections = vertex.direction;
							i++;
							ConduitFlow.BuildNetworkTask.Graph.Vertex vertex2 = this.cycle_vertices[num];
							this.conduit_flow.soaInfo.AddPermittedFlowDirections(this.conduit_flow.grid[vertex2.cell].conduitIdx, vertex2.direction);
							num--;
						}
						this.dead_ends.Add(this.cycle_vertices[i].cell);
						this.dead_ends.Add(this.cycle_vertices[num].cell);
					}
				}
			}

			// Token: 0x04008F7C RID: 36732
			private ConduitFlow conduit_flow;

			// Token: 0x04008F7D RID: 36733
			private HashSetPool<int, ConduitFlow>.PooledHashSet vertex_cells;

			// Token: 0x04008F7E RID: 36734
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.PooledList edges;

			// Token: 0x04008F7F RID: 36735
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Edge, ConduitFlow>.PooledList cycles;

			// Token: 0x04008F80 RID: 36736
			private QueuePool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.PooledQueue bfs_traversal;

			// Token: 0x04008F81 RID: 36737
			private HashSetPool<int, ConduitFlow>.PooledHashSet visited;

			// Token: 0x04008F82 RID: 36738
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.PooledList pseudo_sources;

			// Token: 0x04008F83 RID: 36739
			public HashSetPool<int, ConduitFlow>.PooledHashSet sources;

			// Token: 0x04008F84 RID: 36740
			private HashSetPool<int, ConduitFlow>.PooledHashSet sinks;

			// Token: 0x04008F85 RID: 36741
			private HashSetPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.PooledHashSet dfs_path;

			// Token: 0x04008F86 RID: 36742
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.DFSNode, ConduitFlow>.PooledList dfs_traversal;

			// Token: 0x04008F87 RID: 36743
			public HashSetPool<int, ConduitFlow>.PooledHashSet dead_ends;

			// Token: 0x04008F88 RID: 36744
			private ListPool<ConduitFlow.BuildNetworkTask.Graph.Vertex, ConduitFlow>.PooledList cycle_vertices;

			// Token: 0x02002DAE RID: 11694
			[DebuggerDisplay("{cell}:{direction}")]
			public struct Vertex : IEquatable<ConduitFlow.BuildNetworkTask.Graph.Vertex>
			{
				// Token: 0x17000A11 RID: 2577
				// (get) Token: 0x0600BEBA RID: 48826 RVA: 0x00391688 File Offset: 0x0038F888
				public bool is_valid
				{
					get
					{
						return this.cell != -1;
					}
				}

				// Token: 0x0600BEBB RID: 48827 RVA: 0x00391696 File Offset: 0x0038F896
				public bool Equals(ConduitFlow.BuildNetworkTask.Graph.Vertex rhs)
				{
					return this.direction == rhs.direction && this.cell == rhs.cell;
				}

				// Token: 0x0400BA4B RID: 47691
				public ConduitFlow.FlowDirections direction;

				// Token: 0x0400BA4C RID: 47692
				public int cell;

				// Token: 0x0400BA4D RID: 47693
				public static ConduitFlow.BuildNetworkTask.Graph.Vertex INVALID = new ConduitFlow.BuildNetworkTask.Graph.Vertex
				{
					direction = ConduitFlow.FlowDirections.None,
					cell = -1
				};
			}

			// Token: 0x02002DAF RID: 11695
			[DebuggerDisplay("{vertices[0].cell}:{vertices[0].direction} -> {vertices[1].cell}:{vertices[1].direction}")]
			public struct Edge : IEquatable<ConduitFlow.BuildNetworkTask.Graph.Edge>
			{
				// Token: 0x17000A12 RID: 2578
				// (get) Token: 0x0600BEBD RID: 48829 RVA: 0x003916E3 File Offset: 0x0038F8E3
				public bool is_valid
				{
					get
					{
						return this.vertices != null;
					}
				}

				// Token: 0x0600BEBE RID: 48830 RVA: 0x003916F0 File Offset: 0x0038F8F0
				public bool Equals(ConduitFlow.BuildNetworkTask.Graph.Edge rhs)
				{
					if (this.vertices == null)
					{
						return rhs.vertices == null;
					}
					return rhs.vertices != null && (this.vertices.Length == rhs.vertices.Length && this.vertices.Length == 2 && this.vertices[0].Equals(rhs.vertices[0])) && this.vertices[1].Equals(rhs.vertices[1]);
				}

				// Token: 0x0600BEBF RID: 48831 RVA: 0x00391774 File Offset: 0x0038F974
				public ConduitFlow.BuildNetworkTask.Graph.Edge Invert()
				{
					return new ConduitFlow.BuildNetworkTask.Graph.Edge
					{
						vertices = new ConduitFlow.BuildNetworkTask.Graph.Vertex[]
						{
							new ConduitFlow.BuildNetworkTask.Graph.Vertex
							{
								cell = this.vertices[1].cell,
								direction = ConduitFlow.Opposite(this.vertices[1].direction)
							},
							new ConduitFlow.BuildNetworkTask.Graph.Vertex
							{
								cell = this.vertices[0].cell,
								direction = ConduitFlow.Opposite(this.vertices[0].direction)
							}
						}
					};
				}

				// Token: 0x0600BEC0 RID: 48832 RVA: 0x00391821 File Offset: 0x0038FA21
				public ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator Iter(ConduitFlow conduit_flow)
				{
					return new ConduitFlow.BuildNetworkTask.Graph.Edge.VertexIterator(conduit_flow, this);
				}

				// Token: 0x0400BA4E RID: 47694
				public ConduitFlow.BuildNetworkTask.Graph.Vertex[] vertices;

				// Token: 0x0400BA4F RID: 47695
				public static readonly ConduitFlow.BuildNetworkTask.Graph.Edge INVALID = new ConduitFlow.BuildNetworkTask.Graph.Edge
				{
					vertices = null
				};

				// Token: 0x02002FE5 RID: 12261
				[DebuggerDisplay("{cell}:{direction}")]
				public struct VertexIterator
				{
					// Token: 0x0600C2D0 RID: 49872 RVA: 0x0039C08B File Offset: 0x0039A28B
					public VertexIterator(ConduitFlow conduit_flow, ConduitFlow.BuildNetworkTask.Graph.Edge edge)
					{
						this.conduit_flow = conduit_flow;
						this.edge = edge;
						this.cell = edge.vertices[0].cell;
						this.direction = edge.vertices[0].direction;
					}

					// Token: 0x0600C2D1 RID: 49873 RVA: 0x0039C0CC File Offset: 0x0039A2CC
					public void Next()
					{
						int conduitIdx = this.conduit_flow.grid[this.cell].conduitIdx;
						ConduitFlow.Conduit conduitFromDirection = this.conduit_flow.soaInfo.GetConduitFromDirection(conduitIdx, this.direction);
						global::Debug.Assert(conduitFromDirection.idx != -1);
						this.cell = conduitFromDirection.GetCell(this.conduit_flow);
						if (this.cell == this.edge.vertices[1].cell)
						{
							return;
						}
						this.direction = ConduitFlow.Opposite(this.direction);
						bool flag = false;
						for (int num = 0; num != 3; num++)
						{
							this.direction = ConduitFlow.ComputeNextFlowDirection(this.direction);
							if (this.conduit_flow.soaInfo.GetConduitFromDirection(conduitFromDirection.idx, this.direction).idx != -1)
							{
								flag = true;
								break;
							}
						}
						global::Debug.Assert(flag);
						if (!flag)
						{
							this.cell = this.edge.vertices[1].cell;
						}
					}

					// Token: 0x0600C2D2 RID: 49874 RVA: 0x0039C1CD File Offset: 0x0039A3CD
					public bool IsValid()
					{
						return this.cell != this.edge.vertices[1].cell;
					}

					// Token: 0x0400C049 RID: 49225
					public int cell;

					// Token: 0x0400C04A RID: 49226
					public ConduitFlow.FlowDirections direction;

					// Token: 0x0400C04B RID: 49227
					private ConduitFlow conduit_flow;

					// Token: 0x0400C04C RID: 49228
					private ConduitFlow.BuildNetworkTask.Graph.Edge edge;
				}
			}

			// Token: 0x02002DB0 RID: 11696
			[DebuggerDisplay("cell:{cell}, parent:{parent == null ? -1 : parent.cell}")]
			private class DFSNode
			{
				// Token: 0x0400BA50 RID: 47696
				public int cell;

				// Token: 0x0400BA51 RID: 47697
				public ConduitFlow.BuildNetworkTask.Graph.DFSNode parent;
			}
		}
	}

	// Token: 0x02001381 RID: 4993
	private struct ConnectContext
	{
		// Token: 0x06007E39 RID: 32313 RVA: 0x002D88C8 File Offset: 0x002D6AC8
		public ConnectContext(ConduitFlow outer)
		{
			this.outer = outer;
			this.cells = ListPool<int, ConduitFlow>.Allocate();
			this.cells.Capacity = Mathf.Max(this.cells.Capacity, outer.soaInfo.NumEntries);
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x002D8902 File Offset: 0x002D6B02
		public void Finish()
		{
			this.cells.Recycle();
		}

		// Token: 0x040060E5 RID: 24805
		public ListPool<int, ConduitFlow>.PooledList cells;

		// Token: 0x040060E6 RID: 24806
		public ConduitFlow outer;
	}

	// Token: 0x02001382 RID: 4994
	private struct ConnectTask : IWorkItem<ConduitFlow.ConnectContext>
	{
		// Token: 0x06007E3B RID: 32315 RVA: 0x002D890F File Offset: 0x002D6B0F
		public ConnectTask(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x002D8920 File Offset: 0x002D6B20
		public void Run(ConduitFlow.ConnectContext context)
		{
			for (int num = this.start; num != this.end; num++)
			{
				int num2 = context.cells[num];
				int conduitIdx = context.outer.grid[num2].conduitIdx;
				if (conduitIdx != -1)
				{
					UtilityConnections connections = context.outer.networkMgr.GetConnections(num2, true);
					if (connections != (UtilityConnections)0)
					{
						ConduitFlow.ConduitConnections @default = ConduitFlow.ConduitConnections.DEFAULT;
						int num3 = num2 - 1;
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Left) != (UtilityConnections)0)
						{
							@default.left = context.outer.grid[num3].conduitIdx;
						}
						num3 = num2 + 1;
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Right) != (UtilityConnections)0)
						{
							@default.right = context.outer.grid[num3].conduitIdx;
						}
						num3 = num2 - Grid.WidthInCells;
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Down) != (UtilityConnections)0)
						{
							@default.down = context.outer.grid[num3].conduitIdx;
						}
						num3 = num2 + Grid.WidthInCells;
						if (Grid.IsValidCell(num3) && (connections & UtilityConnections.Up) != (UtilityConnections)0)
						{
							@default.up = context.outer.grid[num3].conduitIdx;
						}
						context.outer.soaInfo.SetConduitConnections(conduitIdx, @default);
					}
				}
			}
		}

		// Token: 0x040060E7 RID: 24807
		private int start;

		// Token: 0x040060E8 RID: 24808
		private int end;
	}

	// Token: 0x02001383 RID: 4995
	private struct Sink
	{
		// Token: 0x06007E3D RID: 32317 RVA: 0x002D8A74 File Offset: 0x002D6C74
		public Sink(FlowUtilityNetwork.IItem sink)
		{
			this.consumer = ((sink.GameObject != null) ? sink.GameObject.GetComponent<ConduitConsumer>() : null);
			this.space_remaining = ((this.consumer != null && this.consumer.operational.IsOperational) ? this.consumer.space_remaining_kg : 0f);
		}

		// Token: 0x040060E9 RID: 24809
		public ConduitConsumer consumer;

		// Token: 0x040060EA RID: 24810
		public float space_remaining;
	}

	// Token: 0x02001384 RID: 4996
	private class UpdateNetworkTask : IWorkItem<ConduitFlow>
	{
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06007E3E RID: 32318 RVA: 0x002D8ADB File Offset: 0x002D6CDB
		// (set) Token: 0x06007E3F RID: 32319 RVA: 0x002D8AE3 File Offset: 0x002D6CE3
		public bool continue_updating { get; private set; }

		// Token: 0x06007E40 RID: 32320 RVA: 0x002D8AEC File Offset: 0x002D6CEC
		public UpdateNetworkTask(ConduitFlow.Network network)
		{
			this.continue_updating = true;
			this.network = network;
			this.sinks = DictionaryPool<int, ConduitFlow.Sink, ConduitFlow>.Allocate();
			foreach (FlowUtilityNetwork.IItem item in network.network.sinks)
			{
				this.sinks.Add(item.Cell, new ConduitFlow.Sink(item));
			}
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x002D8B74 File Offset: 0x002D6D74
		public void Run(ConduitFlow conduit_flow)
		{
			global::Debug.Assert(this.continue_updating);
			this.continue_updating = false;
			foreach (int num in this.network.cells)
			{
				int conduitIdx = conduit_flow.grid[num].conduitIdx;
				if (conduit_flow.UpdateConduit(conduit_flow.soaInfo.GetConduit(conduitIdx), this.sinks))
				{
					this.continue_updating = true;
				}
			}
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x002D8C0C File Offset: 0x002D6E0C
		public void Finish(ConduitFlow conduit_flow)
		{
			foreach (int num in this.network.cells)
			{
				ConduitFlow.ConduitContents contents = conduit_flow.grid[num].contents;
				contents.ConsolidateMass();
				conduit_flow.grid[num].contents = contents;
			}
			this.sinks.Recycle();
		}

		// Token: 0x040060EB RID: 24811
		private ConduitFlow.Network network;

		// Token: 0x040060EC RID: 24812
		private DictionaryPool<int, ConduitFlow.Sink, ConduitFlow>.PooledDictionary sinks;
	}
}
