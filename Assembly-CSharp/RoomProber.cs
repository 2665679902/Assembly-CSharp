using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F6 RID: 2294
public class RoomProber : ISim1000ms
{
	// Token: 0x0600423E RID: 16958 RVA: 0x00175560 File Offset: 0x00173760
	public RoomProber()
	{
		this.CellCavityID = new HandleVector<int>.Handle[Grid.CellCount];
		this.floodFiller = new RoomProber.CavityFloodFiller(this.CellCavityID);
		for (int i = 0; i < this.CellCavityID.Length; i++)
		{
			this.solidChanges.Add(i);
		}
		this.ProcessSolidChanges();
		this.RefreshRooms();
		World instance = World.Instance;
		instance.OnSolidChanged = (Action<int>)Delegate.Combine(instance.OnSolidChanged, new Action<int>(this.SolidChangedEvent));
		GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingsChanged));
		GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[2], new Action<int, object>(this.OnBuildingsChanged));
	}

	// Token: 0x0600423F RID: 16959 RVA: 0x00175683 File Offset: 0x00173883
	public void Refresh()
	{
		this.ProcessSolidChanges();
		this.RefreshRooms();
	}

	// Token: 0x06004240 RID: 16960 RVA: 0x00175691 File Offset: 0x00173891
	private void SolidChangedEvent(int cell)
	{
		this.SolidChangedEvent(cell, true);
	}

	// Token: 0x06004241 RID: 16961 RVA: 0x0017569B File Offset: 0x0017389B
	private void OnBuildingsChanged(int cell, object building)
	{
		if (this.GetCavityForCell(cell) != null)
		{
			this.solidChanges.Add(cell);
			this.dirty = true;
		}
	}

	// Token: 0x06004242 RID: 16962 RVA: 0x001756BA File Offset: 0x001738BA
	public void SolidChangedEvent(int cell, bool ignoreDoors)
	{
		if (ignoreDoors && Grid.HasDoor[cell])
		{
			return;
		}
		this.solidChanges.Add(cell);
		this.dirty = true;
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x001756E4 File Offset: 0x001738E4
	private CavityInfo CreateNewCavity()
	{
		CavityInfo cavityInfo = new CavityInfo();
		cavityInfo.handle = this.cavityInfos.Allocate(cavityInfo);
		return cavityInfo;
	}

	// Token: 0x06004244 RID: 16964 RVA: 0x0017570C File Offset: 0x0017390C
	private unsafe void ProcessSolidChanges()
	{
		int* ptr = stackalloc int[(UIntPtr)20];
		*ptr = 0;
		ptr[1] = -Grid.WidthInCells;
		ptr[2] = -1;
		ptr[3] = 1;
		ptr[4] = Grid.WidthInCells;
		foreach (int num in this.solidChanges)
		{
			for (int i = 0; i < 5; i++)
			{
				int num2 = num + ptr[i];
				if (Grid.IsValidCell(num2))
				{
					this.floodFillSet.Add(num2);
					HandleVector<int>.Handle handle = this.CellCavityID[num2];
					if (handle.IsValid())
					{
						this.CellCavityID[num2] = HandleVector<int>.InvalidHandle;
						this.releasedIDs.Add(handle);
					}
				}
			}
		}
		CavityInfo cavityInfo = this.CreateNewCavity();
		foreach (int num3 in this.floodFillSet)
		{
			if (!this.visitedCells.Contains(num3))
			{
				HandleVector<int>.Handle handle2 = this.CellCavityID[num3];
				if (!handle2.IsValid())
				{
					CavityInfo cavityInfo2 = cavityInfo;
					this.floodFiller.Reset(cavityInfo2.handle);
					GameUtil.FloodFillConditional(num3, new Func<int, bool>(this.floodFiller.ShouldContinue), this.visitedCells, null);
					if (this.floodFiller.NumCells > 0)
					{
						cavityInfo2.numCells = this.floodFiller.NumCells;
						cavityInfo2.minX = this.floodFiller.MinX;
						cavityInfo2.minY = this.floodFiller.MinY;
						cavityInfo2.maxX = this.floodFiller.MaxX;
						cavityInfo2.maxY = this.floodFiller.MaxY;
						cavityInfo = this.CreateNewCavity();
					}
				}
			}
		}
		if (cavityInfo.numCells == 0)
		{
			this.releasedIDs.Add(cavityInfo.handle);
		}
		foreach (HandleVector<int>.Handle handle3 in this.releasedIDs)
		{
			CavityInfo data = this.cavityInfos.GetData(handle3);
			this.releasedCritters.AddRange(data.creatures);
			if (data.room != null)
			{
				this.ClearRoom(data.room);
			}
			this.cavityInfos.Free(handle3);
		}
		this.RebuildDirtyCavities(this.visitedCells);
		this.releasedIDs.Clear();
		this.visitedCells.Clear();
		this.solidChanges.Clear();
		this.floodFillSet.Clear();
	}

	// Token: 0x06004245 RID: 16965 RVA: 0x001759E0 File Offset: 0x00173BE0
	private void RebuildDirtyCavities(ICollection<int> visited_cells)
	{
		int maxRoomSize = TuningData<RoomProber.Tuning>.Get().maxRoomSize;
		foreach (int num in visited_cells)
		{
			HandleVector<int>.Handle handle = this.CellCavityID[num];
			if (handle.IsValid())
			{
				CavityInfo data = this.cavityInfos.GetData(handle);
				if (0 < data.numCells && data.numCells <= maxRoomSize)
				{
					GameObject gameObject = Grid.Objects[num, 1];
					if (gameObject != null)
					{
						KPrefabID component = gameObject.GetComponent<KPrefabID>();
						bool flag = false;
						foreach (KPrefabID kprefabID in data.buildings)
						{
							if (component.InstanceID == kprefabID.InstanceID)
							{
								flag = true;
								break;
							}
						}
						foreach (KPrefabID kprefabID2 in data.plants)
						{
							if (component.InstanceID == kprefabID2.InstanceID)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (component.GetComponent<Deconstructable>())
							{
								data.AddBuilding(component);
							}
							else if (component.HasTag(GameTags.Plant) && !component.IsPrefabID("ForestTreeBranch".ToTag()))
							{
								data.AddPlants(component);
							}
						}
					}
				}
			}
		}
		visited_cells.Clear();
	}

	// Token: 0x06004246 RID: 16966 RVA: 0x00175BB4 File Offset: 0x00173DB4
	public void Sim1000ms(float dt)
	{
		if (this.dirty)
		{
			this.ProcessSolidChanges();
			this.RefreshRooms();
		}
	}

	// Token: 0x06004247 RID: 16967 RVA: 0x00175BCC File Offset: 0x00173DCC
	private void CreateRoom(CavityInfo cavity)
	{
		global::Debug.Assert(cavity.room == null);
		Room room = new Room();
		room.cavity = cavity;
		cavity.room = room;
		this.rooms.Add(room);
		room.roomType = Db.Get().RoomTypes.GetRoomType(room);
		this.AssignBuildingsToRoom(room);
	}

	// Token: 0x06004248 RID: 16968 RVA: 0x00175C24 File Offset: 0x00173E24
	private void ClearRoom(Room room)
	{
		this.UnassignBuildingsToRoom(room);
		room.CleanUp();
		this.rooms.Remove(room);
	}

	// Token: 0x06004249 RID: 16969 RVA: 0x00175C40 File Offset: 0x00173E40
	private void RefreshRooms()
	{
		int maxRoomSize = TuningData<RoomProber.Tuning>.Get().maxRoomSize;
		foreach (CavityInfo cavityInfo in this.cavityInfos.GetDataList())
		{
			if (cavityInfo.dirty)
			{
				global::Debug.Assert(cavityInfo.room == null, "I expected info.room to always be null by this point");
				if (cavityInfo.numCells > 0)
				{
					if (cavityInfo.numCells <= maxRoomSize)
					{
						this.CreateRoom(cavityInfo);
					}
					foreach (KPrefabID kprefabID in cavityInfo.buildings)
					{
						kprefabID.Trigger(144050788, cavityInfo.room);
					}
					foreach (KPrefabID kprefabID2 in cavityInfo.plants)
					{
						kprefabID2.Trigger(144050788, cavityInfo.room);
					}
				}
				cavityInfo.dirty = false;
			}
		}
		foreach (KPrefabID kprefabID3 in this.releasedCritters)
		{
			if (kprefabID3 != null)
			{
				OvercrowdingMonitor.Instance smi = kprefabID3.GetSMI<OvercrowdingMonitor.Instance>();
				if (smi != null)
				{
					smi.RoomRefreshUpdateCavity();
				}
			}
		}
		this.releasedCritters.Clear();
		this.dirty = false;
	}

	// Token: 0x0600424A RID: 16970 RVA: 0x00175DE4 File Offset: 0x00173FE4
	private void AssignBuildingsToRoom(Room room)
	{
		global::Debug.Assert(room != null);
		RoomType roomType = room.roomType;
		if (roomType == Db.Get().RoomTypes.Neutral)
		{
			return;
		}
		foreach (KPrefabID kprefabID in room.buildings)
		{
			if (!(kprefabID == null) && !kprefabID.HasTag(GameTags.NotRoomAssignable))
			{
				Assignable component = kprefabID.GetComponent<Assignable>();
				if (component != null && (roomType.primary_constraint == null || !roomType.primary_constraint.building_criteria(kprefabID.GetComponent<KPrefabID>())))
				{
					component.Assign(room);
				}
			}
		}
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x00175EA0 File Offset: 0x001740A0
	private void UnassignKPrefabIDs(Room room, List<KPrefabID> list)
	{
		foreach (KPrefabID kprefabID in list)
		{
			if (!(kprefabID == null))
			{
				kprefabID.Trigger(144050788, null);
				Assignable component = kprefabID.GetComponent<Assignable>();
				if (component != null && component.assignee == room)
				{
					component.Unassign();
				}
			}
		}
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x00175F1C File Offset: 0x0017411C
	private void UnassignBuildingsToRoom(Room room)
	{
		global::Debug.Assert(room != null);
		this.UnassignKPrefabIDs(room, room.buildings);
		this.UnassignKPrefabIDs(room, room.plants);
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x00175F44 File Offset: 0x00174144
	public void UpdateRoom(CavityInfo cavity)
	{
		if (cavity == null)
		{
			return;
		}
		if (cavity.room != null)
		{
			this.ClearRoom(cavity.room);
			cavity.room = null;
		}
		this.CreateRoom(cavity);
		foreach (KPrefabID kprefabID in cavity.buildings)
		{
			if (kprefabID != null)
			{
				kprefabID.Trigger(144050788, cavity.room);
			}
		}
		foreach (KPrefabID kprefabID2 in cavity.plants)
		{
			if (kprefabID2 != null)
			{
				kprefabID2.Trigger(144050788, cavity.room);
			}
		}
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x00176028 File Offset: 0x00174228
	public Room GetRoomOfGameObject(GameObject go)
	{
		if (go == null)
		{
			return null;
		}
		int num = Grid.PosToCell(go);
		if (!Grid.IsValidCell(num))
		{
			return null;
		}
		CavityInfo cavityForCell = this.GetCavityForCell(num);
		if (cavityForCell == null)
		{
			return null;
		}
		return cavityForCell.room;
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x00176064 File Offset: 0x00174264
	public bool IsInRoomType(GameObject go, RoomType checkType)
	{
		Room roomOfGameObject = this.GetRoomOfGameObject(go);
		if (roomOfGameObject != null)
		{
			RoomType roomType = roomOfGameObject.roomType;
			return checkType == roomType;
		}
		return false;
	}

	// Token: 0x06004250 RID: 16976 RVA: 0x0017608C File Offset: 0x0017428C
	private CavityInfo GetCavityInfo(HandleVector<int>.Handle id)
	{
		CavityInfo cavityInfo = null;
		if (id.IsValid())
		{
			cavityInfo = this.cavityInfos.GetData(id);
		}
		return cavityInfo;
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x001760B4 File Offset: 0x001742B4
	public CavityInfo GetCavityForCell(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return null;
		}
		HandleVector<int>.Handle handle = this.CellCavityID[cell];
		return this.GetCavityInfo(handle);
	}

	// Token: 0x04002C3F RID: 11327
	public List<Room> rooms = new List<Room>();

	// Token: 0x04002C40 RID: 11328
	private KCompactedVector<CavityInfo> cavityInfos = new KCompactedVector<CavityInfo>(1024);

	// Token: 0x04002C41 RID: 11329
	private HandleVector<int>.Handle[] CellCavityID;

	// Token: 0x04002C42 RID: 11330
	private bool dirty = true;

	// Token: 0x04002C43 RID: 11331
	private HashSet<int> solidChanges = new HashSet<int>();

	// Token: 0x04002C44 RID: 11332
	private HashSet<int> visitedCells = new HashSet<int>();

	// Token: 0x04002C45 RID: 11333
	private HashSet<int> floodFillSet = new HashSet<int>();

	// Token: 0x04002C46 RID: 11334
	private HashSet<HandleVector<int>.Handle> releasedIDs = new HashSet<HandleVector<int>.Handle>();

	// Token: 0x04002C47 RID: 11335
	private RoomProber.CavityFloodFiller floodFiller;

	// Token: 0x04002C48 RID: 11336
	private List<KPrefabID> releasedCritters = new List<KPrefabID>();

	// Token: 0x020016C3 RID: 5827
	public class Tuning : TuningData<RoomProber.Tuning>
	{
		// Token: 0x04006AE4 RID: 27364
		public int maxRoomSize;
	}

	// Token: 0x020016C4 RID: 5828
	private class CavityFloodFiller
	{
		// Token: 0x060088A1 RID: 34977 RVA: 0x002F6279 File Offset: 0x002F4479
		public CavityFloodFiller(HandleVector<int>.Handle[] grid)
		{
			this.grid = grid;
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x002F6288 File Offset: 0x002F4488
		public void Reset(HandleVector<int>.Handle search_id)
		{
			this.cavityID = search_id;
			this.numCells = 0;
			this.minX = int.MaxValue;
			this.minY = int.MaxValue;
			this.maxX = 0;
			this.maxY = 0;
		}

		// Token: 0x060088A3 RID: 34979 RVA: 0x002F62BC File Offset: 0x002F44BC
		private static bool IsWall(int cell)
		{
			return (Grid.BuildMasks[cell] & (Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation)) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor) || Grid.HasDoor[cell];
		}

		// Token: 0x060088A4 RID: 34980 RVA: 0x002F62DC File Offset: 0x002F44DC
		public bool ShouldContinue(int flood_cell)
		{
			if (RoomProber.CavityFloodFiller.IsWall(flood_cell))
			{
				this.grid[flood_cell] = HandleVector<int>.InvalidHandle;
				return false;
			}
			this.grid[flood_cell] = this.cavityID;
			int num;
			int num2;
			Grid.CellToXY(flood_cell, out num, out num2);
			this.minX = Math.Min(num, this.minX);
			this.minY = Math.Min(num2, this.minY);
			this.maxX = Math.Max(num, this.maxX);
			this.maxY = Math.Max(num2, this.maxY);
			this.numCells++;
			return true;
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060088A5 RID: 34981 RVA: 0x002F6377 File Offset: 0x002F4577
		public int NumCells
		{
			get
			{
				return this.numCells;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060088A6 RID: 34982 RVA: 0x002F637F File Offset: 0x002F457F
		public int MinX
		{
			get
			{
				return this.minX;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060088A7 RID: 34983 RVA: 0x002F6387 File Offset: 0x002F4587
		public int MinY
		{
			get
			{
				return this.minY;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060088A8 RID: 34984 RVA: 0x002F638F File Offset: 0x002F458F
		public int MaxX
		{
			get
			{
				return this.maxX;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060088A9 RID: 34985 RVA: 0x002F6397 File Offset: 0x002F4597
		public int MaxY
		{
			get
			{
				return this.maxY;
			}
		}

		// Token: 0x04006AE5 RID: 27365
		private HandleVector<int>.Handle[] grid;

		// Token: 0x04006AE6 RID: 27366
		private HandleVector<int>.Handle cavityID;

		// Token: 0x04006AE7 RID: 27367
		private int numCells;

		// Token: 0x04006AE8 RID: 27368
		private int minX;

		// Token: 0x04006AE9 RID: 27369
		private int minY;

		// Token: 0x04006AEA RID: 27370
		private int maxX;

		// Token: 0x04006AEB RID: 27371
		private int maxY;
	}
}
