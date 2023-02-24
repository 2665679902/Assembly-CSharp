using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000657 RID: 1623
public class Teleporter : KMonoBehaviour
{
	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000E4915 File Offset: 0x000E2B15
	// (set) Token: 0x06002B77 RID: 11127 RVA: 0x000E491D File Offset: 0x000E2B1D
	[Serialize]
	public int teleporterID { get; private set; }

	// Token: 0x06002B78 RID: 11128 RVA: 0x000E4926 File Offset: 0x000E2B26
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002B79 RID: 11129 RVA: 0x000E492E File Offset: 0x000E2B2E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Teleporters.Add(this);
		this.SetTeleporterID(0);
		base.Subscribe<Teleporter>(-801688580, Teleporter.OnLogicValueChangedDelegate);
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x000E495C File Offset: 0x000E2B5C
	private void OnLogicValueChanged(object data)
	{
		LogicPorts component = base.GetComponent<LogicPorts>();
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		List<int> list = new List<int>();
		int num = 0;
		int num2 = Mathf.Min(this.ID_LENGTH, component.inputPorts.Count);
		for (int i = 0; i < num2; i++)
		{
			int logicUICell = component.inputPorts[i].GetLogicUICell();
			LogicCircuitNetwork networkForCell = logicCircuitManager.GetNetworkForCell(logicUICell);
			int num3 = ((networkForCell != null) ? networkForCell.OutputValue : 1);
			list.Add(num3);
		}
		foreach (int num4 in list)
		{
			num = (num << 1) | num4;
		}
		this.SetTeleporterID(num);
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x000E4A2C File Offset: 0x000E2C2C
	protected override void OnCleanUp()
	{
		Components.Teleporters.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x000E4A3F File Offset: 0x000E2C3F
	public bool HasTeleporterTarget()
	{
		return this.FindTeleportTarget() != null;
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x000E4A4D File Offset: 0x000E2C4D
	public bool IsValidTeleportTarget(Teleporter from_tele)
	{
		return from_tele.teleporterID == this.teleporterID && this.operational.IsOperational;
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x000E4A6C File Offset: 0x000E2C6C
	public Teleporter FindTeleportTarget()
	{
		List<Teleporter> list = new List<Teleporter>();
		foreach (object obj in Components.Teleporters)
		{
			Teleporter teleporter = (Teleporter)obj;
			if (teleporter.IsValidTeleportTarget(this) && teleporter != this)
			{
				list.Add(teleporter);
			}
		}
		Teleporter teleporter2 = null;
		if (list.Count > 0)
		{
			teleporter2 = list.GetRandom<Teleporter>();
		}
		return teleporter2;
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x000E4AF4 File Offset: 0x000E2CF4
	public void SetTeleporterID(int ID)
	{
		this.teleporterID = ID;
		foreach (object obj in Components.Teleporters)
		{
			((Teleporter)obj).Trigger(-1266722732, null);
		}
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x000E4B58 File Offset: 0x000E2D58
	public void SetTeleportTarget(Teleporter target)
	{
		this.teleportTarget.Set(target);
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x000E4B68 File Offset: 0x000E2D68
	public void TeleportObjects()
	{
		Teleporter teleporter = this.teleportTarget.Get();
		int widthInCells = base.GetComponent<Building>().Def.WidthInCells;
		int num = base.GetComponent<Building>().Def.HeightInCells - 1;
		Vector3 position = base.transform.GetPosition();
		if (teleporter != null)
		{
			ListPool<ScenePartitionerEntry, Teleporter>.PooledList pooledList = ListPool<ScenePartitionerEntry, Teleporter>.Allocate();
			GameScenePartitioner.Instance.GatherEntries((int)position.x - widthInCells / 2 + 1, (int)position.y - num / 2 + 1, widthInCells, num, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
			int num2 = Grid.PosToCell(teleporter);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				GameObject gameObject = (scenePartitionerEntry.obj as Pickupable).gameObject;
				Vector3 vector = gameObject.transform.GetPosition() - position;
				MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
				if (component != null)
				{
					new EmoteChore(component.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, "anim_interacts_portal_kanim", Telepad.PortalBirthAnim, null);
				}
				else
				{
					vector += Vector3.up;
				}
				gameObject.transform.SetLocalPosition(Grid.CellToPosCBC(num2, Grid.SceneLayer.Move) + vector);
			}
			pooledList.Recycle();
		}
		TeleportalPad.StatesInstance smi = this.teleportTarget.Get().GetSMI<TeleportalPad.StatesInstance>();
		smi.sm.doTeleport.Trigger(smi);
		this.teleportTarget.Set(null);
	}

	// Token: 0x040019BA RID: 6586
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040019BC RID: 6588
	[Serialize]
	public Ref<Teleporter> teleportTarget = new Ref<Teleporter>();

	// Token: 0x040019BD RID: 6589
	public int ID_LENGTH = 4;

	// Token: 0x040019BE RID: 6590
	private static readonly EventSystem.IntraObjectHandler<Teleporter> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<Teleporter>(delegate(Teleporter component, object data)
	{
		component.OnLogicValueChanged(data);
	});
}
