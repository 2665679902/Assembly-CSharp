using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using UnityEngine;

// Token: 0x0200092E RID: 2350
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidConduitFlow : IConduitFlow
{
	// Token: 0x060044BC RID: 17596 RVA: 0x0018378D File Offset: 0x0018198D
	public SolidConduitFlow.SOAInfo GetSOAInfo()
	{
		return this.soaInfo;
	}

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x060044BD RID: 17597 RVA: 0x00183798 File Offset: 0x00181998
	// (remove) Token: 0x060044BE RID: 17598 RVA: 0x001837D0 File Offset: 0x001819D0
	public event System.Action onConduitsRebuilt;

	// Token: 0x060044BF RID: 17599 RVA: 0x00183808 File Offset: 0x00181A08
	public void AddConduitUpdater(Action<float> callback, ConduitFlowPriority priority = ConduitFlowPriority.Default)
	{
		this.conduitUpdaters.Add(new SolidConduitFlow.ConduitUpdater
		{
			priority = priority,
			callback = callback
		});
		this.dirtyConduitUpdaters = true;
	}

	// Token: 0x060044C0 RID: 17600 RVA: 0x00183840 File Offset: 0x00181A40
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

	// Token: 0x060044C1 RID: 17601 RVA: 0x00183890 File Offset: 0x00181A90
	public static int FlowBit(SolidConduitFlow.FlowDirection direction)
	{
		return 1 << direction - SolidConduitFlow.FlowDirection.Left;
	}

	// Token: 0x060044C2 RID: 17602 RVA: 0x0018389C File Offset: 0x00181A9C
	public SolidConduitFlow(int num_cells, IUtilityNetworkMgr network_mgr, float initial_elapsed_time)
	{
		this.elapsedTime = initial_elapsed_time;
		this.networkMgr = network_mgr;
		this.maskedOverlayLayer = LayerMask.NameToLayer("MaskedOverlay");
		this.Initialize(num_cells);
		network_mgr.AddNetworksRebuiltListener(new Action<IList<UtilityNetwork>, ICollection<int>>(this.OnUtilityNetworksRebuilt));
	}

	// Token: 0x060044C3 RID: 17603 RVA: 0x0018394C File Offset: 0x00181B4C
	public void Initialize(int num_cells)
	{
		this.grid = new SolidConduitFlow.GridNode[num_cells];
		for (int i = 0; i < num_cells; i++)
		{
			this.grid[i].conduitIdx = -1;
			this.grid[i].contents.pickupableHandle = HandleVector<int>.InvalidHandle;
		}
	}

	// Token: 0x060044C4 RID: 17604 RVA: 0x001839A0 File Offset: 0x00181BA0
	private void OnUtilityNetworksRebuilt(IList<UtilityNetwork> networks, ICollection<int> root_nodes)
	{
		this.RebuildConnections(root_nodes);
		foreach (UtilityNetwork utilityNetwork in networks)
		{
			FlowUtilityNetwork flowUtilityNetwork = (FlowUtilityNetwork)utilityNetwork;
			this.ScanNetworkSources(flowUtilityNetwork);
		}
		this.RefreshPaths();
	}

	// Token: 0x060044C5 RID: 17605 RVA: 0x001839FC File Offset: 0x00181BFC
	private void RebuildConnections(IEnumerable<int> root_nodes)
	{
		this.soaInfo.Clear(this);
		this.pathList.Clear();
		ObjectLayer objectLayer = ObjectLayer.SolidConduit;
		foreach (int num in root_nodes)
		{
			if (this.replacements.Contains(num))
			{
				this.replacements.Remove(num);
			}
			GameObject gameObject = Grid.Objects[num, (int)objectLayer];
			if (!(gameObject == null))
			{
				int num2 = this.soaInfo.AddConduit(this, gameObject, num);
				this.grid[num].conduitIdx = num2;
			}
		}
		Game.Instance.conduitTemperatureManager.Sim200ms(0f);
		foreach (int num3 in root_nodes)
		{
			UtilityConnections connections = this.networkMgr.GetConnections(num3, true);
			if (connections != (UtilityConnections)0 && this.grid[num3].conduitIdx != -1)
			{
				int conduitIdx = this.grid[num3].conduitIdx;
				SolidConduitFlow.ConduitConnections conduitConnections = this.soaInfo.GetConduitConnections(conduitIdx);
				int num4 = num3 - 1;
				if (Grid.IsValidCell(num4) && (connections & UtilityConnections.Left) != (UtilityConnections)0)
				{
					conduitConnections.left = this.grid[num4].conduitIdx;
				}
				num4 = num3 + 1;
				if (Grid.IsValidCell(num4) && (connections & UtilityConnections.Right) != (UtilityConnections)0)
				{
					conduitConnections.right = this.grid[num4].conduitIdx;
				}
				num4 = num3 - Grid.WidthInCells;
				if (Grid.IsValidCell(num4) && (connections & UtilityConnections.Down) != (UtilityConnections)0)
				{
					conduitConnections.down = this.grid[num4].conduitIdx;
				}
				num4 = num3 + Grid.WidthInCells;
				if (Grid.IsValidCell(num4) && (connections & UtilityConnections.Up) != (UtilityConnections)0)
				{
					conduitConnections.up = this.grid[num4].conduitIdx;
				}
				this.soaInfo.SetConduitConnections(conduitIdx, conduitConnections);
			}
		}
		if (this.onConduitsRebuilt != null)
		{
			this.onConduitsRebuilt();
		}
	}

	// Token: 0x060044C6 RID: 17606 RVA: 0x00183C44 File Offset: 0x00181E44
	public void ScanNetworkSources(FlowUtilityNetwork network)
	{
		if (network == null)
		{
			return;
		}
		for (int i = 0; i < network.sources.Count; i++)
		{
			FlowUtilityNetwork.IItem item = network.sources[i];
			this.path.Clear();
			this.visited.Clear();
			this.FindSinks(i, item.Cell);
		}
	}

	// Token: 0x060044C7 RID: 17607 RVA: 0x00183C9C File Offset: 0x00181E9C
	public void RefreshPaths()
	{
		foreach (List<SolidConduitFlow.Conduit> list in this.pathList)
		{
			for (int i = 0; i < list.Count - 1; i++)
			{
				SolidConduitFlow.Conduit conduit = list[i];
				SolidConduitFlow.Conduit conduit2 = list[i + 1];
				if (conduit.GetTargetFlowDirection(this) == SolidConduitFlow.FlowDirection.None)
				{
					SolidConduitFlow.FlowDirection direction = this.GetDirection(conduit, conduit2);
					conduit.SetTargetFlowDirection(direction, this);
				}
			}
		}
	}

	// Token: 0x060044C8 RID: 17608 RVA: 0x00183D30 File Offset: 0x00181F30
	private void FindSinks(int source_idx, int cell)
	{
		SolidConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			this.FindSinksInternal(source_idx, gridNode.conduitIdx);
		}
	}

	// Token: 0x060044C9 RID: 17609 RVA: 0x00183D60 File Offset: 0x00181F60
	private void FindSinksInternal(int source_idx, int conduit_idx)
	{
		if (this.visited.Contains(conduit_idx))
		{
			return;
		}
		this.visited.Add(conduit_idx);
		SolidConduitFlow.Conduit conduit = this.soaInfo.GetConduit(conduit_idx);
		if (conduit.GetPermittedFlowDirections(this) == -1)
		{
			return;
		}
		this.path.Add(conduit);
		FlowUtilityNetwork.IItem item = (FlowUtilityNetwork.IItem)this.networkMgr.GetEndpoint(this.soaInfo.GetCell(conduit_idx));
		if (item != null && item.EndpointType == Endpoint.Sink)
		{
			this.FoundSink(source_idx);
		}
		SolidConduitFlow.ConduitConnections conduitConnections = this.soaInfo.GetConduitConnections(conduit_idx);
		if (conduitConnections.down != -1)
		{
			this.FindSinksInternal(source_idx, conduitConnections.down);
		}
		if (conduitConnections.left != -1)
		{
			this.FindSinksInternal(source_idx, conduitConnections.left);
		}
		if (conduitConnections.right != -1)
		{
			this.FindSinksInternal(source_idx, conduitConnections.right);
		}
		if (conduitConnections.up != -1)
		{
			this.FindSinksInternal(source_idx, conduitConnections.up);
		}
		if (this.path.Count > 0)
		{
			this.path.RemoveAt(this.path.Count - 1);
		}
	}

	// Token: 0x060044CA RID: 17610 RVA: 0x00183E6C File Offset: 0x0018206C
	private SolidConduitFlow.FlowDirection GetDirection(SolidConduitFlow.Conduit conduit, SolidConduitFlow.Conduit target_conduit)
	{
		SolidConduitFlow.ConduitConnections conduitConnections = this.soaInfo.GetConduitConnections(conduit.idx);
		if (conduitConnections.up == target_conduit.idx)
		{
			return SolidConduitFlow.FlowDirection.Up;
		}
		if (conduitConnections.down == target_conduit.idx)
		{
			return SolidConduitFlow.FlowDirection.Down;
		}
		if (conduitConnections.left == target_conduit.idx)
		{
			return SolidConduitFlow.FlowDirection.Left;
		}
		if (conduitConnections.right == target_conduit.idx)
		{
			return SolidConduitFlow.FlowDirection.Right;
		}
		return SolidConduitFlow.FlowDirection.None;
	}

	// Token: 0x060044CB RID: 17611 RVA: 0x00183ECC File Offset: 0x001820CC
	private void FoundSink(int source_idx)
	{
		for (int i = 0; i < this.path.Count - 1; i++)
		{
			SolidConduitFlow.FlowDirection direction = this.GetDirection(this.path[i], this.path[i + 1]);
			SolidConduitFlow.FlowDirection flowDirection = SolidConduitFlow.InverseFlow(direction);
			int cellFromDirection = SolidConduitFlow.GetCellFromDirection(this.soaInfo.GetCell(this.path[i].idx), flowDirection);
			SolidConduitFlow.Conduit conduitFromDirection = this.soaInfo.GetConduitFromDirection(this.path[i].idx, flowDirection);
			if (i == 0 || (this.path[i].GetPermittedFlowDirections(this) & SolidConduitFlow.FlowBit(flowDirection)) == 0 || (cellFromDirection != this.soaInfo.GetCell(this.path[i - 1].idx) && (this.soaInfo.GetSrcFlowIdx(this.path[i].idx) == source_idx || (conduitFromDirection.GetPermittedFlowDirections(this) & SolidConduitFlow.FlowBit(flowDirection)) == 0)))
			{
				int permittedFlowDirections = this.path[i].GetPermittedFlowDirections(this);
				this.soaInfo.SetSrcFlowIdx(this.path[i].idx, source_idx);
				this.path[i].SetPermittedFlowDirections(permittedFlowDirections | SolidConduitFlow.FlowBit(direction), this);
				this.path[i].SetTargetFlowDirection(direction, this);
			}
		}
		for (int j = 1; j < this.path.Count; j++)
		{
			SolidConduitFlow.FlowDirection direction2 = this.GetDirection(this.path[j], this.path[j - 1]);
			this.soaInfo.SetSrcFlowDirection(this.path[j].idx, direction2);
		}
		List<SolidConduitFlow.Conduit> list = new List<SolidConduitFlow.Conduit>(this.path);
		list.Reverse();
		this.TryAdd(list);
	}

	// Token: 0x060044CC RID: 17612 RVA: 0x001840BC File Offset: 0x001822BC
	private void TryAdd(List<SolidConduitFlow.Conduit> new_path)
	{
		Predicate<SolidConduitFlow.Conduit> <>9__0;
		Predicate<SolidConduitFlow.Conduit> <>9__1;
		foreach (List<SolidConduitFlow.Conduit> list in this.pathList)
		{
			if (list.Count >= new_path.Count)
			{
				bool flag = false;
				List<SolidConduitFlow.Conduit> list2 = list;
				Predicate<SolidConduitFlow.Conduit> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (SolidConduitFlow.Conduit t) => t.idx == new_path[0].idx);
				}
				int num = list2.FindIndex(predicate);
				List<SolidConduitFlow.Conduit> list3 = list;
				Predicate<SolidConduitFlow.Conduit> predicate2;
				if ((predicate2 = <>9__1) == null)
				{
					predicate2 = (<>9__1 = (SolidConduitFlow.Conduit t) => t.idx == new_path[new_path.Count - 1].idx);
				}
				int num2 = list3.FindIndex(predicate2);
				if (num != -1 && num2 != -1)
				{
					flag = true;
					int i = num;
					int num3 = 0;
					while (i < num2)
					{
						if (list[i].idx != new_path[num3].idx)
						{
							flag = false;
							break;
						}
						i++;
						num3++;
					}
				}
				if (flag)
				{
					return;
				}
			}
		}
		for (int j = this.pathList.Count - 1; j >= 0; j--)
		{
			if (this.pathList[j].Count <= 0)
			{
				this.pathList.RemoveAt(j);
			}
		}
		for (int k = this.pathList.Count - 1; k >= 0; k--)
		{
			List<SolidConduitFlow.Conduit> old_path = this.pathList[k];
			if (new_path.Count >= old_path.Count)
			{
				bool flag2 = false;
				int num4 = new_path.FindIndex((SolidConduitFlow.Conduit t) => t.idx == old_path[0].idx);
				int num5 = new_path.FindIndex((SolidConduitFlow.Conduit t) => t.idx == old_path[old_path.Count - 1].idx);
				if (num4 != -1 && num5 != -1)
				{
					flag2 = true;
					int l = num4;
					int num6 = 0;
					while (l < num5)
					{
						if (new_path[l].idx != old_path[num6].idx)
						{
							flag2 = false;
							break;
						}
						l++;
						num6++;
					}
				}
				if (flag2)
				{
					this.pathList.RemoveAt(k);
				}
			}
		}
		foreach (List<SolidConduitFlow.Conduit> list4 in this.pathList)
		{
			for (int m = new_path.Count - 1; m >= 0; m--)
			{
				SolidConduitFlow.Conduit new_conduit = new_path[m];
				if (list4.FindIndex((SolidConduitFlow.Conduit t) => t.idx == new_conduit.idx) != -1 && Mathf.IsPowerOfTwo(this.soaInfo.GetPermittedFlowDirections(new_conduit.idx)))
				{
					new_path.RemoveAt(m);
				}
			}
		}
		this.pathList.Add(new_path);
	}

	// Token: 0x060044CD RID: 17613 RVA: 0x001843D8 File Offset: 0x001825D8
	public SolidConduitFlow.ConduitContents GetContents(int cell)
	{
		SolidConduitFlow.ConduitContents conduitContents = this.grid[cell].contents;
		SolidConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			conduitContents = this.soaInfo.GetConduit(gridNode.conduitIdx).GetContents(this);
		}
		return conduitContents;
	}

	// Token: 0x060044CE RID: 17614 RVA: 0x0018442C File Offset: 0x0018262C
	private void SetContents(int cell, SolidConduitFlow.ConduitContents contents)
	{
		SolidConduitFlow.GridNode gridNode = this.grid[cell];
		if (gridNode.conduitIdx != -1)
		{
			this.soaInfo.GetConduit(gridNode.conduitIdx).SetContents(this, contents);
			return;
		}
		this.grid[cell].contents = contents;
	}

	// Token: 0x060044CF RID: 17615 RVA: 0x00184480 File Offset: 0x00182680
	public void SetContents(int cell, Pickupable pickupable)
	{
		SolidConduitFlow.ConduitContents conduitContents = new SolidConduitFlow.ConduitContents
		{
			pickupableHandle = HandleVector<int>.InvalidHandle
		};
		if (pickupable != null)
		{
			KBatchedAnimController component = pickupable.GetComponent<KBatchedAnimController>();
			SolidConduitFlow.StoredInfo storedInfo = new SolidConduitFlow.StoredInfo
			{
				kbac = component,
				pickupable = pickupable
			};
			conduitContents.pickupableHandle = this.conveyorPickupables.Allocate(storedInfo);
			KBatchedAnimController component2 = pickupable.GetComponent<KBatchedAnimController>();
			component2.enabled = false;
			component2.enabled = true;
			pickupable.Trigger(856640610, true);
		}
		this.SetContents(cell, conduitContents);
	}

	// Token: 0x060044D0 RID: 17616 RVA: 0x0018450D File Offset: 0x0018270D
	public static int GetCellFromDirection(int cell, SolidConduitFlow.FlowDirection direction)
	{
		switch (direction)
		{
		case SolidConduitFlow.FlowDirection.Left:
			return Grid.CellLeft(cell);
		case SolidConduitFlow.FlowDirection.Right:
			return Grid.CellRight(cell);
		case SolidConduitFlow.FlowDirection.Up:
			return Grid.CellAbove(cell);
		case SolidConduitFlow.FlowDirection.Down:
			return Grid.CellBelow(cell);
		default:
			return -1;
		}
	}

	// Token: 0x060044D1 RID: 17617 RVA: 0x00184546 File Offset: 0x00182746
	public static SolidConduitFlow.FlowDirection InverseFlow(SolidConduitFlow.FlowDirection direction)
	{
		switch (direction)
		{
		case SolidConduitFlow.FlowDirection.Left:
			return SolidConduitFlow.FlowDirection.Right;
		case SolidConduitFlow.FlowDirection.Right:
			return SolidConduitFlow.FlowDirection.Left;
		case SolidConduitFlow.FlowDirection.Up:
			return SolidConduitFlow.FlowDirection.Down;
		case SolidConduitFlow.FlowDirection.Down:
			return SolidConduitFlow.FlowDirection.Up;
		default:
			return SolidConduitFlow.FlowDirection.None;
		}
	}

	// Token: 0x060044D2 RID: 17618 RVA: 0x0018456C File Offset: 0x0018276C
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
		float num = 1f;
		this.elapsedTime -= 1f;
		this.lastUpdateTime = Time.time;
		this.soaInfo.BeginFrame(this);
		foreach (List<SolidConduitFlow.Conduit> list in this.pathList)
		{
			foreach (SolidConduitFlow.Conduit conduit in list)
			{
				this.UpdateConduit(conduit);
			}
		}
		this.soaInfo.UpdateFlowDirection(this);
		if (this.dirtyConduitUpdaters)
		{
			this.conduitUpdaters.Sort((SolidConduitFlow.ConduitUpdater a, SolidConduitFlow.ConduitUpdater b) => a.priority - b.priority);
		}
		this.soaInfo.EndFrame(this);
		for (int i = 0; i < this.conduitUpdaters.Count; i++)
		{
			this.conduitUpdaters[i].callback(num);
		}
	}

	// Token: 0x060044D3 RID: 17619 RVA: 0x001846C4 File Offset: 0x001828C4
	public void RenderEveryTick(float dt)
	{
		for (int i = 0; i < this.GetSOAInfo().NumEntries; i++)
		{
			SolidConduitFlow.Conduit conduit = this.GetSOAInfo().GetConduit(i);
			SolidConduitFlow.ConduitFlowInfo lastFlowInfo = conduit.GetLastFlowInfo(this);
			if (lastFlowInfo.direction != SolidConduitFlow.FlowDirection.None)
			{
				int cell = conduit.GetCell(this);
				int cellFromDirection = SolidConduitFlow.GetCellFromDirection(cell, lastFlowInfo.direction);
				SolidConduitFlow.ConduitContents contents = this.GetContents(cellFromDirection);
				if (contents.pickupableHandle.IsValid())
				{
					Vector3 vector = Grid.CellToPosCCC(cell, Grid.SceneLayer.SolidConduitContents);
					Vector3 vector2 = Grid.CellToPosCCC(cellFromDirection, Grid.SceneLayer.SolidConduitContents);
					Vector3 vector3 = Vector3.Lerp(vector, vector2, this.ContinuousLerpPercent);
					Pickupable pickupable = this.GetPickupable(contents.pickupableHandle);
					if (pickupable != null)
					{
						pickupable.transform.SetPosition(vector3);
					}
				}
			}
		}
	}

	// Token: 0x060044D4 RID: 17620 RVA: 0x00184784 File Offset: 0x00182984
	private void UpdateConduit(SolidConduitFlow.Conduit conduit)
	{
		if (this.soaInfo.GetUpdated(conduit.idx))
		{
			return;
		}
		if (this.soaInfo.GetSrcFlowDirection(conduit.idx) == SolidConduitFlow.FlowDirection.None)
		{
			this.soaInfo.SetSrcFlowDirection(conduit.idx, conduit.GetNextFlowSource(this));
		}
		int cell = this.soaInfo.GetCell(conduit.idx);
		SolidConduitFlow.ConduitContents contents = this.grid[cell].contents;
		if (!contents.pickupableHandle.IsValid())
		{
			return;
		}
		SolidConduitFlow.FlowDirection targetFlowDirection = this.soaInfo.GetTargetFlowDirection(conduit.idx);
		SolidConduitFlow.Conduit conduitFromDirection = this.soaInfo.GetConduitFromDirection(conduit.idx, targetFlowDirection);
		if (conduitFromDirection.idx == -1)
		{
			this.soaInfo.SetTargetFlowDirection(conduit.idx, conduit.GetNextFlowTarget(this));
			return;
		}
		int cell2 = this.soaInfo.GetCell(conduitFromDirection.idx);
		SolidConduitFlow.ConduitContents contents2 = this.grid[cell2].contents;
		if (contents2.pickupableHandle.IsValid())
		{
			this.soaInfo.SetTargetFlowDirection(conduit.idx, conduit.GetNextFlowTarget(this));
			return;
		}
		if ((this.soaInfo.GetPermittedFlowDirections(conduit.idx) & SolidConduitFlow.FlowBit(targetFlowDirection)) != 0)
		{
			bool flag = false;
			for (int i = 0; i < 5; i++)
			{
				SolidConduitFlow.Conduit conduitFromDirection2 = this.soaInfo.GetConduitFromDirection(conduitFromDirection.idx, this.soaInfo.GetSrcFlowDirection(conduitFromDirection.idx));
				if (conduitFromDirection2.idx == conduit.idx)
				{
					flag = true;
					break;
				}
				if (conduitFromDirection2.idx != -1)
				{
					int cell3 = this.soaInfo.GetCell(conduitFromDirection2.idx);
					SolidConduitFlow.ConduitContents contents3 = this.grid[cell3].contents;
					if (contents3.pickupableHandle.IsValid())
					{
						break;
					}
				}
				this.soaInfo.SetSrcFlowDirection(conduitFromDirection.idx, conduitFromDirection.GetNextFlowSource(this));
			}
			if (flag && !contents2.pickupableHandle.IsValid())
			{
				SolidConduitFlow.ConduitContents conduitContents = this.RemoveFromGrid(conduit);
				this.AddToGrid(cell2, conduitContents);
				this.soaInfo.SetLastFlowInfo(conduit.idx, this.soaInfo.GetTargetFlowDirection(conduit.idx));
				this.soaInfo.SetUpdated(conduitFromDirection.idx, true);
				this.soaInfo.SetSrcFlowDirection(conduitFromDirection.idx, conduitFromDirection.GetNextFlowSource(this));
			}
		}
		this.soaInfo.SetTargetFlowDirection(conduit.idx, conduit.GetNextFlowTarget(this));
	}

	// Token: 0x170004E7 RID: 1255
	// (get) Token: 0x060044D5 RID: 17621 RVA: 0x001849ED File Offset: 0x00182BED
	public float ContinuousLerpPercent
	{
		get
		{
			return Mathf.Clamp01((Time.time - this.lastUpdateTime) / 1f);
		}
	}

	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x060044D6 RID: 17622 RVA: 0x00184A06 File Offset: 0x00182C06
	public float DiscreteLerpPercent
	{
		get
		{
			return Mathf.Clamp01(this.elapsedTime / 1f);
		}
	}

	// Token: 0x060044D7 RID: 17623 RVA: 0x00184A19 File Offset: 0x00182C19
	private void AddToGrid(int cell_idx, SolidConduitFlow.ConduitContents contents)
	{
		this.grid[cell_idx].contents = contents;
	}

	// Token: 0x060044D8 RID: 17624 RVA: 0x00184A30 File Offset: 0x00182C30
	private SolidConduitFlow.ConduitContents RemoveFromGrid(SolidConduitFlow.Conduit conduit)
	{
		int cell = this.soaInfo.GetCell(conduit.idx);
		SolidConduitFlow.ConduitContents contents = this.grid[cell].contents;
		SolidConduitFlow.ConduitContents conduitContents = SolidConduitFlow.ConduitContents.EmptyContents();
		this.grid[cell].contents = conduitContents;
		return contents;
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x00184A78 File Offset: 0x00182C78
	public void AddPickupable(int cell_idx, Pickupable pickupable)
	{
		if (this.grid[cell_idx].conduitIdx == -1)
		{
			global::Debug.LogWarning("No conduit in cell: " + cell_idx.ToString());
			this.DumpPickupable(pickupable);
			return;
		}
		SolidConduitFlow.ConduitContents contents = this.GetConduit(cell_idx).GetContents(this);
		if (contents.pickupableHandle.IsValid())
		{
			global::Debug.LogWarning("Conduit already full: " + cell_idx.ToString());
			this.DumpPickupable(pickupable);
			return;
		}
		KBatchedAnimController component = pickupable.GetComponent<KBatchedAnimController>();
		SolidConduitFlow.StoredInfo storedInfo = new SolidConduitFlow.StoredInfo
		{
			kbac = component,
			pickupable = pickupable
		};
		contents.pickupableHandle = this.conveyorPickupables.Allocate(storedInfo);
		if (this.viewingConduits)
		{
			this.ApplyOverlayVisualization(component);
		}
		if (pickupable.storage)
		{
			pickupable.storage.Remove(pickupable.gameObject, true);
		}
		pickupable.Trigger(856640610, true);
		this.SetContents(cell_idx, contents);
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x00184B70 File Offset: 0x00182D70
	public Pickupable RemovePickupable(int cell_idx)
	{
		Pickupable pickupable = null;
		SolidConduitFlow.Conduit conduit = this.GetConduit(cell_idx);
		if (conduit.idx != -1)
		{
			SolidConduitFlow.ConduitContents conduitContents = this.RemoveFromGrid(conduit);
			if (conduitContents.pickupableHandle.IsValid())
			{
				SolidConduitFlow.StoredInfo data = this.conveyorPickupables.GetData(conduitContents.pickupableHandle);
				this.ClearOverlayVisualization(data.kbac);
				pickupable = data.pickupable;
				if (pickupable)
				{
					pickupable.Trigger(856640610, false);
				}
				this.freedHandles.Add(conduitContents.pickupableHandle);
			}
		}
		return pickupable;
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x00184BF8 File Offset: 0x00182DF8
	public int GetPermittedFlow(int cell)
	{
		SolidConduitFlow.Conduit conduit = this.GetConduit(cell);
		if (conduit.idx == -1)
		{
			return 0;
		}
		return this.soaInfo.GetPermittedFlowDirections(conduit.idx);
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x00184C29 File Offset: 0x00182E29
	public bool HasConduit(int cell)
	{
		return this.grid[cell].conduitIdx != -1;
	}

	// Token: 0x060044DD RID: 17629 RVA: 0x00184C44 File Offset: 0x00182E44
	public SolidConduitFlow.Conduit GetConduit(int cell)
	{
		int conduitIdx = this.grid[cell].conduitIdx;
		if (conduitIdx == -1)
		{
			return SolidConduitFlow.Conduit.Invalid();
		}
		return this.soaInfo.GetConduit(conduitIdx);
	}

	// Token: 0x060044DE RID: 17630 RVA: 0x00184C7C File Offset: 0x00182E7C
	private void DumpPipeContents(int cell)
	{
		Pickupable pickupable = this.RemovePickupable(cell);
		if (pickupable)
		{
			pickupable.transform.parent = null;
		}
	}

	// Token: 0x060044DF RID: 17631 RVA: 0x00184CA5 File Offset: 0x00182EA5
	private void DumpPickupable(Pickupable pickupable)
	{
		if (pickupable)
		{
			pickupable.transform.parent = null;
		}
	}

	// Token: 0x060044E0 RID: 17632 RVA: 0x00184CBB File Offset: 0x00182EBB
	public void EmptyConduit(int cell)
	{
		if (this.replacements.Contains(cell))
		{
			return;
		}
		this.DumpPipeContents(cell);
	}

	// Token: 0x060044E1 RID: 17633 RVA: 0x00184CD3 File Offset: 0x00182ED3
	public void MarkForReplacement(int cell)
	{
		this.replacements.Add(cell);
	}

	// Token: 0x060044E2 RID: 17634 RVA: 0x00184CE4 File Offset: 0x00182EE4
	public void DeactivateCell(int cell)
	{
		this.grid[cell].conduitIdx = -1;
		SolidConduitFlow.ConduitContents conduitContents = SolidConduitFlow.ConduitContents.EmptyContents();
		this.SetContents(cell, conduitContents);
	}

	// Token: 0x060044E3 RID: 17635 RVA: 0x00184D14 File Offset: 0x00182F14
	public UtilityNetwork GetNetwork(SolidConduitFlow.Conduit conduit)
	{
		int cell = this.soaInfo.GetCell(conduit.idx);
		return this.networkMgr.GetNetworkForCell(cell);
	}

	// Token: 0x060044E4 RID: 17636 RVA: 0x00184D3F File Offset: 0x00182F3F
	public void ForceRebuildNetworks()
	{
		this.networkMgr.ForceRebuildNetworks();
	}

	// Token: 0x060044E5 RID: 17637 RVA: 0x00184D4C File Offset: 0x00182F4C
	public bool IsConduitFull(int cell_idx)
	{
		SolidConduitFlow.ConduitContents contents = this.grid[cell_idx].contents;
		return contents.pickupableHandle.IsValid();
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x00184D78 File Offset: 0x00182F78
	public bool IsConduitEmpty(int cell_idx)
	{
		SolidConduitFlow.ConduitContents contents = this.grid[cell_idx].contents;
		return !contents.pickupableHandle.IsValid();
	}

	// Token: 0x060044E7 RID: 17639 RVA: 0x00184DA8 File Offset: 0x00182FA8
	public void Initialize()
	{
		if (OverlayScreen.Instance != null)
		{
			OverlayScreen instance = OverlayScreen.Instance;
			instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
			OverlayScreen instance2 = OverlayScreen.Instance;
			instance2.OnOverlayChanged = (Action<HashedString>)Delegate.Combine(instance2.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
		}
	}

	// Token: 0x060044E8 RID: 17640 RVA: 0x00184E10 File Offset: 0x00183010
	private void OnOverlayChanged(HashedString mode)
	{
		bool flag = mode == OverlayModes.SolidConveyor.ID;
		if (flag == this.viewingConduits)
		{
			return;
		}
		this.viewingConduits = flag;
		int num = (this.viewingConduits ? this.maskedOverlayLayer : Game.PickupableLayer);
		Color32 color = (this.viewingConduits ? SolidConduitFlow.OverlayColour : SolidConduitFlow.NormalColour);
		List<SolidConduitFlow.StoredInfo> dataList = this.conveyorPickupables.GetDataList();
		for (int i = 0; i < dataList.Count; i++)
		{
			SolidConduitFlow.StoredInfo storedInfo = dataList[i];
			if (storedInfo.kbac != null)
			{
				storedInfo.kbac.SetLayer(num);
				storedInfo.kbac.TintColour = color;
			}
		}
	}

	// Token: 0x060044E9 RID: 17641 RVA: 0x00184EB9 File Offset: 0x001830B9
	private void ApplyOverlayVisualization(KBatchedAnimController kbac)
	{
		if (kbac == null)
		{
			return;
		}
		kbac.SetLayer(this.maskedOverlayLayer);
		kbac.TintColour = SolidConduitFlow.OverlayColour;
	}

	// Token: 0x060044EA RID: 17642 RVA: 0x00184EDC File Offset: 0x001830DC
	private void ClearOverlayVisualization(KBatchedAnimController kbac)
	{
		if (kbac == null)
		{
			return;
		}
		kbac.SetLayer(Game.PickupableLayer);
		kbac.TintColour = SolidConduitFlow.NormalColour;
	}

	// Token: 0x060044EB RID: 17643 RVA: 0x00184F00 File Offset: 0x00183100
	public Pickupable GetPickupable(HandleVector<int>.Handle h)
	{
		Pickupable pickupable = null;
		if (h.IsValid())
		{
			pickupable = this.conveyorPickupables.GetData(h).pickupable;
		}
		return pickupable;
	}

	// Token: 0x04002DD7 RID: 11735
	public const float MAX_SOLID_MASS = 20f;

	// Token: 0x04002DD8 RID: 11736
	public const float TickRate = 1f;

	// Token: 0x04002DD9 RID: 11737
	public const float WaitTime = 1f;

	// Token: 0x04002DDA RID: 11738
	private float elapsedTime;

	// Token: 0x04002DDB RID: 11739
	private float lastUpdateTime = float.NegativeInfinity;

	// Token: 0x04002DDC RID: 11740
	private KCompactedVector<SolidConduitFlow.StoredInfo> conveyorPickupables = new KCompactedVector<SolidConduitFlow.StoredInfo>(0);

	// Token: 0x04002DDD RID: 11741
	private List<HandleVector<int>.Handle> freedHandles = new List<HandleVector<int>.Handle>();

	// Token: 0x04002DDE RID: 11742
	private SolidConduitFlow.SOAInfo soaInfo = new SolidConduitFlow.SOAInfo();

	// Token: 0x04002DE0 RID: 11744
	private bool dirtyConduitUpdaters;

	// Token: 0x04002DE1 RID: 11745
	private List<SolidConduitFlow.ConduitUpdater> conduitUpdaters = new List<SolidConduitFlow.ConduitUpdater>();

	// Token: 0x04002DE2 RID: 11746
	private SolidConduitFlow.GridNode[] grid;

	// Token: 0x04002DE3 RID: 11747
	public IUtilityNetworkMgr networkMgr;

	// Token: 0x04002DE4 RID: 11748
	private HashSet<int> visited = new HashSet<int>();

	// Token: 0x04002DE5 RID: 11749
	private HashSet<int> replacements = new HashSet<int>();

	// Token: 0x04002DE6 RID: 11750
	private List<SolidConduitFlow.Conduit> path = new List<SolidConduitFlow.Conduit>();

	// Token: 0x04002DE7 RID: 11751
	private List<List<SolidConduitFlow.Conduit>> pathList = new List<List<SolidConduitFlow.Conduit>>();

	// Token: 0x04002DE8 RID: 11752
	public static readonly SolidConduitFlow.ConduitContents emptyContents = new SolidConduitFlow.ConduitContents
	{
		pickupableHandle = HandleVector<int>.InvalidHandle
	};

	// Token: 0x04002DE9 RID: 11753
	private int maskedOverlayLayer;

	// Token: 0x04002DEA RID: 11754
	private bool viewingConduits;

	// Token: 0x04002DEB RID: 11755
	private static readonly Color32 NormalColour = Color.white;

	// Token: 0x04002DEC RID: 11756
	private static readonly Color32 OverlayColour = new Color(0.25f, 0.25f, 0.25f, 0f);

	// Token: 0x0200170C RID: 5900
	private struct StoredInfo
	{
		// Token: 0x04006BD1 RID: 27601
		public KBatchedAnimController kbac;

		// Token: 0x04006BD2 RID: 27602
		public Pickupable pickupable;
	}

	// Token: 0x0200170D RID: 5901
	public class SOAInfo
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06008959 RID: 35161 RVA: 0x002F89E5 File Offset: 0x002F6BE5
		public int NumEntries
		{
			get
			{
				return this.conduits.Count;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600895A RID: 35162 RVA: 0x002F89F2 File Offset: 0x002F6BF2
		public List<int> Cells
		{
			get
			{
				return this.cells;
			}
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x002F89FC File Offset: 0x002F6BFC
		public int AddConduit(SolidConduitFlow manager, GameObject conduit_go, int cell)
		{
			int count = this.conduitConnections.Count;
			SolidConduitFlow.Conduit conduit = new SolidConduitFlow.Conduit(count);
			this.conduits.Add(conduit);
			this.conduitConnections.Add(new SolidConduitFlow.ConduitConnections
			{
				left = -1,
				right = -1,
				up = -1,
				down = -1
			});
			SolidConduitFlow.ConduitContents contents = manager.grid[cell].contents;
			this.initialContents.Add(contents);
			this.lastFlowInfo.Add(new SolidConduitFlow.ConduitFlowInfo
			{
				direction = SolidConduitFlow.FlowDirection.None
			});
			this.cells.Add(cell);
			this.updated.Add(false);
			this.diseaseContentsVisible.Add(false);
			this.conduitGOs.Add(conduit_go);
			this.srcFlowIdx.Add(-1);
			this.permittedFlowDirections.Add(0);
			this.srcFlowDirections.Add(SolidConduitFlow.FlowDirection.None);
			this.targetFlowDirections.Add(SolidConduitFlow.FlowDirection.None);
			return count;
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x002F8AFC File Offset: 0x002F6CFC
		public void Clear(SolidConduitFlow manager)
		{
			for (int i = 0; i < this.conduits.Count; i++)
			{
				this.ForcePermanentDiseaseContainer(i, false);
				int num = this.cells[i];
				SolidConduitFlow.ConduitContents contents = manager.grid[num].contents;
				manager.grid[num].contents = contents;
				manager.grid[num].conduitIdx = -1;
			}
			this.cells.Clear();
			this.updated.Clear();
			this.diseaseContentsVisible.Clear();
			this.srcFlowIdx.Clear();
			this.permittedFlowDirections.Clear();
			this.srcFlowDirections.Clear();
			this.targetFlowDirections.Clear();
			this.conduitGOs.Clear();
			this.initialContents.Clear();
			this.lastFlowInfo.Clear();
			this.conduitConnections.Clear();
			this.conduits.Clear();
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x002F8BEE File Offset: 0x002F6DEE
		public SolidConduitFlow.Conduit GetConduit(int idx)
		{
			return this.conduits[idx];
		}

		// Token: 0x0600895E RID: 35166 RVA: 0x002F8BFC File Offset: 0x002F6DFC
		public GameObject GetConduitGO(int idx)
		{
			return this.conduitGOs[idx];
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x002F8C0A File Offset: 0x002F6E0A
		public SolidConduitFlow.ConduitConnections GetConduitConnections(int idx)
		{
			return this.conduitConnections[idx];
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x002F8C18 File Offset: 0x002F6E18
		public void SetConduitConnections(int idx, SolidConduitFlow.ConduitConnections data)
		{
			this.conduitConnections[idx] = data;
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x002F8C28 File Offset: 0x002F6E28
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

		// Token: 0x06008962 RID: 35170 RVA: 0x002F8C74 File Offset: 0x002F6E74
		public SolidConduitFlow.Conduit GetConduitFromDirection(int idx, SolidConduitFlow.FlowDirection direction)
		{
			SolidConduitFlow.Conduit conduit = SolidConduitFlow.Conduit.Invalid();
			SolidConduitFlow.ConduitConnections conduitConnections = this.conduitConnections[idx];
			switch (direction)
			{
			case SolidConduitFlow.FlowDirection.Left:
				conduit = ((conduitConnections.left != -1) ? this.conduits[conduitConnections.left] : SolidConduitFlow.Conduit.Invalid());
				break;
			case SolidConduitFlow.FlowDirection.Right:
				conduit = ((conduitConnections.right != -1) ? this.conduits[conduitConnections.right] : SolidConduitFlow.Conduit.Invalid());
				break;
			case SolidConduitFlow.FlowDirection.Up:
				conduit = ((conduitConnections.up != -1) ? this.conduits[conduitConnections.up] : SolidConduitFlow.Conduit.Invalid());
				break;
			case SolidConduitFlow.FlowDirection.Down:
				conduit = ((conduitConnections.down != -1) ? this.conduits[conduitConnections.down] : SolidConduitFlow.Conduit.Invalid());
				break;
			}
			return conduit;
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x002F8D40 File Offset: 0x002F6F40
		public void BeginFrame(SolidConduitFlow manager)
		{
			for (int i = 0; i < this.conduits.Count; i++)
			{
				this.updated[i] = false;
				SolidConduitFlow.ConduitContents contents = this.conduits[i].GetContents(manager);
				this.initialContents[i] = contents;
				this.lastFlowInfo[i] = new SolidConduitFlow.ConduitFlowInfo
				{
					direction = SolidConduitFlow.FlowDirection.None
				};
				int num = this.cells[i];
				manager.grid[num].contents = contents;
			}
			for (int j = 0; j < manager.freedHandles.Count; j++)
			{
				HandleVector<int>.Handle handle = manager.freedHandles[j];
				manager.conveyorPickupables.Free(handle);
			}
			manager.freedHandles.Clear();
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x002F8E12 File Offset: 0x002F7012
		public void EndFrame(SolidConduitFlow manager)
		{
		}

		// Token: 0x06008965 RID: 35173 RVA: 0x002F8E14 File Offset: 0x002F7014
		public void UpdateFlowDirection(SolidConduitFlow manager)
		{
			for (int i = 0; i < this.conduits.Count; i++)
			{
				SolidConduitFlow.Conduit conduit = this.conduits[i];
				if (!this.updated[i])
				{
					int cell = conduit.GetCell(manager);
					SolidConduitFlow.ConduitContents contents = manager.grid[cell].contents;
					if (!contents.pickupableHandle.IsValid())
					{
						this.srcFlowDirections[conduit.idx] = conduit.GetNextFlowSource(manager);
					}
				}
			}
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x002F8E94 File Offset: 0x002F7094
		public void MarkConduitEmpty(int idx, SolidConduitFlow manager)
		{
			if (this.lastFlowInfo[idx].direction != SolidConduitFlow.FlowDirection.None)
			{
				this.lastFlowInfo[idx] = new SolidConduitFlow.ConduitFlowInfo
				{
					direction = SolidConduitFlow.FlowDirection.None
				};
				SolidConduitFlow.Conduit conduit = this.conduits[idx];
				this.targetFlowDirections[idx] = conduit.GetNextFlowTarget(manager);
				int num = this.cells[idx];
				manager.grid[num].contents = SolidConduitFlow.ConduitContents.EmptyContents();
			}
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x002F8F18 File Offset: 0x002F7118
		public void SetLastFlowInfo(int idx, SolidConduitFlow.FlowDirection direction)
		{
			this.lastFlowInfo[idx] = new SolidConduitFlow.ConduitFlowInfo
			{
				direction = direction
			};
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x002F8F42 File Offset: 0x002F7142
		public SolidConduitFlow.ConduitContents GetInitialContents(int idx)
		{
			return this.initialContents[idx];
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x002F8F50 File Offset: 0x002F7150
		public SolidConduitFlow.ConduitFlowInfo GetLastFlowInfo(int idx)
		{
			return this.lastFlowInfo[idx];
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x002F8F5E File Offset: 0x002F715E
		public int GetPermittedFlowDirections(int idx)
		{
			return this.permittedFlowDirections[idx];
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x002F8F6C File Offset: 0x002F716C
		public void SetPermittedFlowDirections(int idx, int permitted)
		{
			this.permittedFlowDirections[idx] = permitted;
		}

		// Token: 0x0600896C RID: 35180 RVA: 0x002F8F7B File Offset: 0x002F717B
		public SolidConduitFlow.FlowDirection GetTargetFlowDirection(int idx)
		{
			return this.targetFlowDirections[idx];
		}

		// Token: 0x0600896D RID: 35181 RVA: 0x002F8F89 File Offset: 0x002F7189
		public void SetTargetFlowDirection(int idx, SolidConduitFlow.FlowDirection directions)
		{
			this.targetFlowDirections[idx] = directions;
		}

		// Token: 0x0600896E RID: 35182 RVA: 0x002F8F98 File Offset: 0x002F7198
		public int GetSrcFlowIdx(int idx)
		{
			return this.srcFlowIdx[idx];
		}

		// Token: 0x0600896F RID: 35183 RVA: 0x002F8FA6 File Offset: 0x002F71A6
		public void SetSrcFlowIdx(int idx, int new_src_idx)
		{
			this.srcFlowIdx[idx] = new_src_idx;
		}

		// Token: 0x06008970 RID: 35184 RVA: 0x002F8FB5 File Offset: 0x002F71B5
		public SolidConduitFlow.FlowDirection GetSrcFlowDirection(int idx)
		{
			return this.srcFlowDirections[idx];
		}

		// Token: 0x06008971 RID: 35185 RVA: 0x002F8FC3 File Offset: 0x002F71C3
		public void SetSrcFlowDirection(int idx, SolidConduitFlow.FlowDirection directions)
		{
			this.srcFlowDirections[idx] = directions;
		}

		// Token: 0x06008972 RID: 35186 RVA: 0x002F8FD2 File Offset: 0x002F71D2
		public int GetCell(int idx)
		{
			return this.cells[idx];
		}

		// Token: 0x06008973 RID: 35187 RVA: 0x002F8FE0 File Offset: 0x002F71E0
		public void SetCell(int idx, int cell)
		{
			this.cells[idx] = cell;
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x002F8FEF File Offset: 0x002F71EF
		public bool GetUpdated(int idx)
		{
			return this.updated[idx];
		}

		// Token: 0x06008975 RID: 35189 RVA: 0x002F8FFD File Offset: 0x002F71FD
		public void SetUpdated(int idx, bool is_updated)
		{
			this.updated[idx] = is_updated;
		}

		// Token: 0x04006BD3 RID: 27603
		private List<SolidConduitFlow.Conduit> conduits = new List<SolidConduitFlow.Conduit>();

		// Token: 0x04006BD4 RID: 27604
		private List<SolidConduitFlow.ConduitConnections> conduitConnections = new List<SolidConduitFlow.ConduitConnections>();

		// Token: 0x04006BD5 RID: 27605
		private List<SolidConduitFlow.ConduitFlowInfo> lastFlowInfo = new List<SolidConduitFlow.ConduitFlowInfo>();

		// Token: 0x04006BD6 RID: 27606
		private List<SolidConduitFlow.ConduitContents> initialContents = new List<SolidConduitFlow.ConduitContents>();

		// Token: 0x04006BD7 RID: 27607
		private List<GameObject> conduitGOs = new List<GameObject>();

		// Token: 0x04006BD8 RID: 27608
		private List<bool> diseaseContentsVisible = new List<bool>();

		// Token: 0x04006BD9 RID: 27609
		private List<bool> updated = new List<bool>();

		// Token: 0x04006BDA RID: 27610
		private List<int> cells = new List<int>();

		// Token: 0x04006BDB RID: 27611
		private List<int> permittedFlowDirections = new List<int>();

		// Token: 0x04006BDC RID: 27612
		private List<int> srcFlowIdx = new List<int>();

		// Token: 0x04006BDD RID: 27613
		private List<SolidConduitFlow.FlowDirection> srcFlowDirections = new List<SolidConduitFlow.FlowDirection>();

		// Token: 0x04006BDE RID: 27614
		private List<SolidConduitFlow.FlowDirection> targetFlowDirections = new List<SolidConduitFlow.FlowDirection>();
	}

	// Token: 0x0200170E RID: 5902
	[DebuggerDisplay("{priority} {callback.Target.name} {callback.Target} {callback.Method}")]
	public struct ConduitUpdater
	{
		// Token: 0x04006BDF RID: 27615
		public ConduitFlowPriority priority;

		// Token: 0x04006BE0 RID: 27616
		public Action<float> callback;
	}

	// Token: 0x0200170F RID: 5903
	public struct GridNode
	{
		// Token: 0x04006BE1 RID: 27617
		public int conduitIdx;

		// Token: 0x04006BE2 RID: 27618
		public SolidConduitFlow.ConduitContents contents;
	}

	// Token: 0x02001710 RID: 5904
	public enum FlowDirection
	{
		// Token: 0x04006BE4 RID: 27620
		Blocked = -1,
		// Token: 0x04006BE5 RID: 27621
		None,
		// Token: 0x04006BE6 RID: 27622
		Left,
		// Token: 0x04006BE7 RID: 27623
		Right,
		// Token: 0x04006BE8 RID: 27624
		Up,
		// Token: 0x04006BE9 RID: 27625
		Down,
		// Token: 0x04006BEA RID: 27626
		Num
	}

	// Token: 0x02001711 RID: 5905
	public struct ConduitConnections
	{
		// Token: 0x04006BEB RID: 27627
		public int left;

		// Token: 0x04006BEC RID: 27628
		public int right;

		// Token: 0x04006BED RID: 27629
		public int up;

		// Token: 0x04006BEE RID: 27630
		public int down;
	}

	// Token: 0x02001712 RID: 5906
	public struct ConduitFlowInfo
	{
		// Token: 0x04006BEF RID: 27631
		public SolidConduitFlow.FlowDirection direction;
	}

	// Token: 0x02001713 RID: 5907
	[Serializable]
	public struct Conduit : IEquatable<SolidConduitFlow.Conduit>
	{
		// Token: 0x06008977 RID: 35191 RVA: 0x002F90A3 File Offset: 0x002F72A3
		public static SolidConduitFlow.Conduit Invalid()
		{
			return new SolidConduitFlow.Conduit(-1);
		}

		// Token: 0x06008978 RID: 35192 RVA: 0x002F90AB File Offset: 0x002F72AB
		public Conduit(int idx)
		{
			this.idx = idx;
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x002F90B4 File Offset: 0x002F72B4
		public int GetPermittedFlowDirections(SolidConduitFlow manager)
		{
			return manager.soaInfo.GetPermittedFlowDirections(this.idx);
		}

		// Token: 0x0600897A RID: 35194 RVA: 0x002F90C7 File Offset: 0x002F72C7
		public void SetPermittedFlowDirections(int permitted, SolidConduitFlow manager)
		{
			manager.soaInfo.SetPermittedFlowDirections(this.idx, permitted);
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x002F90DB File Offset: 0x002F72DB
		public SolidConduitFlow.FlowDirection GetTargetFlowDirection(SolidConduitFlow manager)
		{
			return manager.soaInfo.GetTargetFlowDirection(this.idx);
		}

		// Token: 0x0600897C RID: 35196 RVA: 0x002F90EE File Offset: 0x002F72EE
		public void SetTargetFlowDirection(SolidConduitFlow.FlowDirection directions, SolidConduitFlow manager)
		{
			manager.soaInfo.SetTargetFlowDirection(this.idx, directions);
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x002F9104 File Offset: 0x002F7304
		public SolidConduitFlow.ConduitContents GetContents(SolidConduitFlow manager)
		{
			int cell = manager.soaInfo.GetCell(this.idx);
			return manager.grid[cell].contents;
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x002F9134 File Offset: 0x002F7334
		public void SetContents(SolidConduitFlow manager, SolidConduitFlow.ConduitContents contents)
		{
			int cell = manager.soaInfo.GetCell(this.idx);
			manager.grid[cell].contents = contents;
			if (contents.pickupableHandle.IsValid())
			{
				Pickupable pickupable = manager.GetPickupable(contents.pickupableHandle);
				if (pickupable != null)
				{
					pickupable.transform.parent = null;
					Vector3 vector = Grid.CellToPosCCC(cell, Grid.SceneLayer.SolidConduitContents);
					pickupable.transform.SetPosition(vector);
					pickupable.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.SolidConduitContents);
				}
			}
		}

		// Token: 0x0600897F RID: 35199 RVA: 0x002F91B8 File Offset: 0x002F73B8
		public SolidConduitFlow.FlowDirection GetNextFlowSource(SolidConduitFlow manager)
		{
			if (manager.soaInfo.GetPermittedFlowDirections(this.idx) == -1)
			{
				return SolidConduitFlow.FlowDirection.Blocked;
			}
			SolidConduitFlow.FlowDirection flowDirection = manager.soaInfo.GetSrcFlowDirection(this.idx);
			if (flowDirection == SolidConduitFlow.FlowDirection.None)
			{
				flowDirection = SolidConduitFlow.FlowDirection.Down;
			}
			for (int i = 0; i < 5; i++)
			{
				SolidConduitFlow.FlowDirection flowDirection2 = (flowDirection + i - 1 + 1) % SolidConduitFlow.FlowDirection.Num + 1;
				SolidConduitFlow.Conduit conduitFromDirection = manager.soaInfo.GetConduitFromDirection(this.idx, flowDirection2);
				if (conduitFromDirection.idx != -1)
				{
					SolidConduitFlow.ConduitContents contents = manager.grid[conduitFromDirection.GetCell(manager)].contents;
					if (contents.pickupableHandle.IsValid())
					{
						int permittedFlowDirections = manager.soaInfo.GetPermittedFlowDirections(conduitFromDirection.idx);
						if (permittedFlowDirections != -1)
						{
							SolidConduitFlow.FlowDirection flowDirection3 = SolidConduitFlow.InverseFlow(flowDirection2);
							if (manager.soaInfo.GetConduitFromDirection(conduitFromDirection.idx, flowDirection3).idx != -1 && (permittedFlowDirections & SolidConduitFlow.FlowBit(flowDirection3)) != 0)
							{
								return flowDirection2;
							}
						}
					}
				}
			}
			for (int j = 0; j < 5; j++)
			{
				SolidConduitFlow.FlowDirection flowDirection4 = (manager.soaInfo.GetTargetFlowDirection(this.idx) + j - 1 + 1) % SolidConduitFlow.FlowDirection.Num + 1;
				SolidConduitFlow.FlowDirection flowDirection5 = SolidConduitFlow.InverseFlow(flowDirection4);
				SolidConduitFlow.Conduit conduitFromDirection2 = manager.soaInfo.GetConduitFromDirection(this.idx, flowDirection4);
				if (conduitFromDirection2.idx != -1)
				{
					int permittedFlowDirections2 = manager.soaInfo.GetPermittedFlowDirections(conduitFromDirection2.idx);
					if (permittedFlowDirections2 != -1 && (permittedFlowDirections2 & SolidConduitFlow.FlowBit(flowDirection5)) != 0)
					{
						return flowDirection4;
					}
				}
			}
			return SolidConduitFlow.FlowDirection.None;
		}

		// Token: 0x06008980 RID: 35200 RVA: 0x002F931C File Offset: 0x002F751C
		public SolidConduitFlow.FlowDirection GetNextFlowTarget(SolidConduitFlow manager)
		{
			int permittedFlowDirections = manager.soaInfo.GetPermittedFlowDirections(this.idx);
			if (permittedFlowDirections == -1)
			{
				return SolidConduitFlow.FlowDirection.Blocked;
			}
			for (int i = 0; i < 5; i++)
			{
				int num = (manager.soaInfo.GetTargetFlowDirection(this.idx) + i - SolidConduitFlow.FlowDirection.Left + 1) % 5 + 1;
				if (manager.soaInfo.GetConduitFromDirection(this.idx, (SolidConduitFlow.FlowDirection)num).idx != -1 && (permittedFlowDirections & SolidConduitFlow.FlowBit((SolidConduitFlow.FlowDirection)num)) != 0)
				{
					return (SolidConduitFlow.FlowDirection)num;
				}
			}
			return SolidConduitFlow.FlowDirection.Blocked;
		}

		// Token: 0x06008981 RID: 35201 RVA: 0x002F9390 File Offset: 0x002F7590
		public SolidConduitFlow.ConduitFlowInfo GetLastFlowInfo(SolidConduitFlow manager)
		{
			return manager.soaInfo.GetLastFlowInfo(this.idx);
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x002F93A3 File Offset: 0x002F75A3
		public SolidConduitFlow.ConduitContents GetInitialContents(SolidConduitFlow manager)
		{
			return manager.soaInfo.GetInitialContents(this.idx);
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x002F93B6 File Offset: 0x002F75B6
		public int GetCell(SolidConduitFlow manager)
		{
			return manager.soaInfo.GetCell(this.idx);
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x002F93C9 File Offset: 0x002F75C9
		public bool Equals(SolidConduitFlow.Conduit other)
		{
			return this.idx == other.idx;
		}

		// Token: 0x04006BF0 RID: 27632
		public int idx;
	}

	// Token: 0x02001714 RID: 5908
	[DebuggerDisplay("{pickupable}")]
	public struct ConduitContents
	{
		// Token: 0x06008985 RID: 35205 RVA: 0x002F93D9 File Offset: 0x002F75D9
		public ConduitContents(HandleVector<int>.Handle pickupable_handle)
		{
			this.pickupableHandle = pickupable_handle;
		}

		// Token: 0x06008986 RID: 35206 RVA: 0x002F93E4 File Offset: 0x002F75E4
		public static SolidConduitFlow.ConduitContents EmptyContents()
		{
			return new SolidConduitFlow.ConduitContents
			{
				pickupableHandle = HandleVector<int>.InvalidHandle
			};
		}

		// Token: 0x04006BF1 RID: 27633
		public HandleVector<int>.Handle pickupableHandle;
	}
}
