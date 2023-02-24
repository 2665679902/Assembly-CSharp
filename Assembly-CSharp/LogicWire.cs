using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005FE RID: 1534
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/LogicWire")]
public class LogicWire : KMonoBehaviour, IFirstFrameCallback, IHaveUtilityNetworkMgr, IBridgedNetworkItem, IBitRating, IDisconnectable
{
	// Token: 0x060027D2 RID: 10194 RVA: 0x000D4375 File Offset: 0x000D2575
	public static int GetBitDepthAsInt(LogicWire.BitDepth rating)
	{
		if (rating == LogicWire.BitDepth.OneBit)
		{
			return 1;
		}
		if (rating != LogicWire.BitDepth.FourBit)
		{
			return 0;
		}
		return 4;
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x000D4388 File Offset: 0x000D2588
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(base.transform.GetPosition());
		Game.Instance.logicCircuitSystem.AddToNetworks(num, this, false);
		base.Subscribe<LogicWire>(774203113, LogicWire.OnBuildingBrokenDelegate);
		base.Subscribe<LogicWire>(-1735440190, LogicWire.OnBuildingFullyRepairedDelegate);
		this.Connect();
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(LogicWire.OutlineSymbol, false);
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x000D43F8 File Offset: 0x000D25F8
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			Game.Instance.logicCircuitSystem.RemoveFromNetworks(num, this, false);
		}
		base.Unsubscribe<LogicWire>(774203113, LogicWire.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<LogicWire>(-1735440190, LogicWire.OnBuildingFullyRepairedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000D4484 File Offset: 0x000D2684
	public bool IsConnected
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Game.Instance.logicCircuitSystem.GetNetworkForCell(num) is LogicCircuitNetwork;
		}
	}

	// Token: 0x060027D6 RID: 10198 RVA: 0x000D44BA File Offset: 0x000D26BA
	public bool IsDisconnected()
	{
		return this.disconnected;
	}

	// Token: 0x060027D7 RID: 10199 RVA: 0x000D44C4 File Offset: 0x000D26C4
	public bool Connect()
	{
		BuildingHP component = base.GetComponent<BuildingHP>();
		if (component == null || component.HitPoints > 0)
		{
			this.disconnected = false;
			Game.Instance.logicCircuitSystem.ForceRebuildNetworks();
		}
		return !this.disconnected;
	}

	// Token: 0x060027D8 RID: 10200 RVA: 0x000D450C File Offset: 0x000D270C
	public void Disconnect()
	{
		this.disconnected = true;
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.WireDisconnected, null);
		Game.Instance.logicCircuitSystem.ForceRebuildNetworks();
	}

	// Token: 0x060027D9 RID: 10201 RVA: 0x000D455C File Offset: 0x000D275C
	public UtilityConnections GetWireConnections()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return Game.Instance.logicCircuitSystem.GetConnections(num, true);
	}

	// Token: 0x060027DA RID: 10202 RVA: 0x000D458C File Offset: 0x000D278C
	public string GetWireConnectionsString()
	{
		UtilityConnections wireConnections = this.GetWireConnections();
		return Game.Instance.logicCircuitSystem.GetVisualizerString(wireConnections);
	}

	// Token: 0x060027DB RID: 10203 RVA: 0x000D45B0 File Offset: 0x000D27B0
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x060027DC RID: 10204 RVA: 0x000D45B8 File Offset: 0x000D27B8
	private void OnBuildingFullyRepaired(object data)
	{
		this.Connect();
	}

	// Token: 0x060027DD RID: 10205 RVA: 0x000D45C1 File Offset: 0x000D27C1
	public void SetFirstFrameCallback(System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x000D45D7 File Offset: 0x000D27D7
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x000D45E6 File Offset: 0x000D27E6
	public LogicWire.BitDepth GetMaxBitRating()
	{
		return this.MaxBitDepth;
	}

	// Token: 0x060027E0 RID: 10208 RVA: 0x000D45EE File Offset: 0x000D27EE
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.logicCircuitSystem;
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x000D45FC File Offset: 0x000D27FC
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.logicCircuitSystem.GetNetworkForCell(num);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x000D4638 File Offset: 0x000D2838
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.logicCircuitSystem.GetNetworkForCell(num);
		return networks.Contains(networkForCell);
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x000D466E File Offset: 0x000D286E
	public int GetNetworkCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x04001771 RID: 6001
	[SerializeField]
	public LogicWire.BitDepth MaxBitDepth;

	// Token: 0x04001772 RID: 6002
	[SerializeField]
	private bool disconnected = true;

	// Token: 0x04001773 RID: 6003
	public static readonly KAnimHashedString OutlineSymbol = new KAnimHashedString("outline");

	// Token: 0x04001774 RID: 6004
	private static readonly EventSystem.IntraObjectHandler<LogicWire> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<LogicWire>(delegate(LogicWire component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x04001775 RID: 6005
	private static readonly EventSystem.IntraObjectHandler<LogicWire> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<LogicWire>(delegate(LogicWire component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});

	// Token: 0x04001776 RID: 6006
	private System.Action firstFrameCallback;

	// Token: 0x0200125E RID: 4702
	public enum BitDepth
	{
		// Token: 0x04005DA3 RID: 23971
		OneBit,
		// Token: 0x04005DA4 RID: 23972
		FourBit,
		// Token: 0x04005DA5 RID: 23973
		NumRatings
	}
}
