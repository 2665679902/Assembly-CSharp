using System;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000591 RID: 1425
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Conduit")]
public class Conduit : KMonoBehaviour, IFirstFrameCallback, IHaveUtilityNetworkMgr, IBridgedNetworkItem, IDisconnectable, FlowUtilityNetwork.IItem
{
	// Token: 0x060022EE RID: 8942 RVA: 0x000BDB20 File Offset: 0x000BBD20
	public void SetFirstFrameCallback(System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x000BDB36 File Offset: 0x000BBD36
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

	// Token: 0x060022F0 RID: 8944 RVA: 0x000BDB48 File Offset: 0x000BBD48
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Conduit>(-1201923725, Conduit.OnHighlightedDelegate);
		base.Subscribe<Conduit>(-700727624, Conduit.OnConduitFrozenDelegate);
		base.Subscribe<Conduit>(-1152799878, Conduit.OnConduitBoilingDelegate);
		base.Subscribe<Conduit>(-1555603773, Conduit.OnStructureTemperatureRegisteredDelegate);
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x000BDB9F File Offset: 0x000BBD9F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Conduit>(774203113, Conduit.OnBuildingBrokenDelegate);
		base.Subscribe<Conduit>(-1735440190, Conduit.OnBuildingFullyRepairedDelegate);
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x000BDBCC File Offset: 0x000BBDCC
	protected virtual void OnStructureTemperatureRegistered(object data)
	{
		int num = Grid.PosToCell(this);
		this.GetNetworkManager().AddToNetworks(num, this, false);
		this.Connect();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Pipe, this);
		BuildingDef def = base.GetComponent<Building>().Def;
		if (def != null && def.ThermalConductivity != 1f)
		{
			this.GetFlowVisualizer().AddThermalConductivity(Grid.PosToCell(base.transform.GetPosition()), def.ThermalConductivity);
		}
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x000BDC64 File Offset: 0x000BBE64
	protected override void OnCleanUp()
	{
		base.Unsubscribe<Conduit>(774203113, Conduit.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<Conduit>(-1735440190, Conduit.OnBuildingFullyRepairedDelegate, false);
		BuildingDef def = base.GetComponent<Building>().Def;
		if (def != null && def.ThermalConductivity != 1f)
		{
			this.GetFlowVisualizer().RemoveThermalConductivity(Grid.PosToCell(base.transform.GetPosition()), def.ThermalConductivity);
		}
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.GetNetworkManager().RemoveFromNetworks(num, this, false);
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			this.GetNetworkManager().RemoveFromNetworks(num, this, false);
			this.GetFlowManager().EmptyConduit(Grid.PosToCell(base.transform.GetPosition()));
		}
		base.OnCleanUp();
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x000BDD58 File Offset: 0x000BBF58
	protected ConduitFlowVisualizer GetFlowVisualizer()
	{
		if (this.type != ConduitType.Gas)
		{
			return Game.Instance.liquidFlowVisualizer;
		}
		return Game.Instance.gasFlowVisualizer;
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x000BDD78 File Offset: 0x000BBF78
	public IUtilityNetworkMgr GetNetworkManager()
	{
		if (this.type != ConduitType.Gas)
		{
			return Game.Instance.liquidConduitSystem;
		}
		return Game.Instance.gasConduitSystem;
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x000BDD98 File Offset: 0x000BBF98
	public ConduitFlow GetFlowManager()
	{
		if (this.type != ConduitType.Gas)
		{
			return Game.Instance.liquidConduitFlow;
		}
		return Game.Instance.gasConduitFlow;
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x000BDDB8 File Offset: 0x000BBFB8
	public static ConduitFlow GetFlowManager(ConduitType type)
	{
		if (type != ConduitType.Gas)
		{
			return Game.Instance.liquidConduitFlow;
		}
		return Game.Instance.gasConduitFlow;
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x000BDDD3 File Offset: 0x000BBFD3
	public static IUtilityNetworkMgr GetNetworkManager(ConduitType type)
	{
		if (type != ConduitType.Gas)
		{
			return Game.Instance.liquidConduitSystem;
		}
		return Game.Instance.gasConduitSystem;
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x000BDDF0 File Offset: 0x000BBFF0
	public virtual void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(Grid.PosToCell(this));
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x000BDE1C File Offset: 0x000BC01C
	public virtual bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(Grid.PosToCell(this));
		return networks.Contains(networkForCell);
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x000BDE42 File Offset: 0x000BC042
	public virtual int GetNetworkCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x000BDE4C File Offset: 0x000BC04C
	private void OnHighlighted(object data)
	{
		int num = (((bool)data) ? Grid.PosToCell(base.transform.GetPosition()) : (-1));
		this.GetFlowVisualizer().SetHighlightedCell(num);
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x000BDE84 File Offset: 0x000BC084
	private void OnConduitFrozen(object data)
	{
		base.Trigger(-794517298, new BuildingHP.DamageSourceInfo
		{
			damage = 1,
			source = BUILDINGS.DAMAGESOURCES.CONDUIT_CONTENTS_FROZE,
			popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.CONDUIT_CONTENTS_FROZE,
			takeDamageEffect = ((this.ConduitType == ConduitType.Gas) ? SpawnFXHashes.BuildingLeakLiquid : SpawnFXHashes.BuildingFreeze),
			fullDamageEffectName = ((this.ConduitType == ConduitType.Gas) ? "water_damage_kanim" : "ice_damage_kanim")
		});
		this.GetFlowManager().EmptyConduit(Grid.PosToCell(base.transform.GetPosition()));
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x000BDF28 File Offset: 0x000BC128
	private void OnConduitBoiling(object data)
	{
		base.Trigger(-794517298, new BuildingHP.DamageSourceInfo
		{
			damage = 1,
			source = BUILDINGS.DAMAGESOURCES.CONDUIT_CONTENTS_BOILED,
			popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.CONDUIT_CONTENTS_BOILED,
			takeDamageEffect = SpawnFXHashes.BuildingLeakGas,
			fullDamageEffectName = "gas_damage_kanim"
		});
		this.GetFlowManager().EmptyConduit(Grid.PosToCell(base.transform.GetPosition()));
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x000BDFAB File Offset: 0x000BC1AB
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x000BDFB3 File Offset: 0x000BC1B3
	private void OnBuildingFullyRepaired(object data)
	{
		this.Connect();
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x000BDFBC File Offset: 0x000BC1BC
	public bool IsDisconnected()
	{
		return this.disconnected;
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x000BDFC4 File Offset: 0x000BC1C4
	public bool Connect()
	{
		BuildingHP component = base.GetComponent<BuildingHP>();
		if (component == null || component.HitPoints > 0)
		{
			this.disconnected = false;
			this.GetNetworkManager().ForceRebuildNetworks();
		}
		return !this.disconnected;
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x000BE005 File Offset: 0x000BC205
	public void Disconnect()
	{
		this.disconnected = true;
		this.GetNetworkManager().ForceRebuildNetworks();
	}

	// Token: 0x170001CD RID: 461
	// (set) Token: 0x06002304 RID: 8964 RVA: 0x000BE019 File Offset: 0x000BC219
	public FlowUtilityNetwork Network
	{
		set
		{
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06002305 RID: 8965 RVA: 0x000BE01B File Offset: 0x000BC21B
	public int Cell
	{
		get
		{
			return Grid.PosToCell(this);
		}
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06002306 RID: 8966 RVA: 0x000BE023 File Offset: 0x000BC223
	public Endpoint EndpointType
	{
		get
		{
			return Endpoint.Conduit;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06002307 RID: 8967 RVA: 0x000BE026 File Offset: 0x000BC226
	public ConduitType ConduitType
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06002308 RID: 8968 RVA: 0x000BE02E File Offset: 0x000BC22E
	public GameObject GameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x04001421 RID: 5153
	[MyCmpReq]
	private KAnimGraphTileVisualizer graphTileDependency;

	// Token: 0x04001422 RID: 5154
	[SerializeField]
	private bool disconnected = true;

	// Token: 0x04001423 RID: 5155
	public ConduitType type;

	// Token: 0x04001424 RID: 5156
	private System.Action firstFrameCallback;

	// Token: 0x04001425 RID: 5157
	protected static readonly EventSystem.IntraObjectHandler<Conduit> OnHighlightedDelegate = new EventSystem.IntraObjectHandler<Conduit>(delegate(Conduit component, object data)
	{
		component.OnHighlighted(data);
	});

	// Token: 0x04001426 RID: 5158
	protected static readonly EventSystem.IntraObjectHandler<Conduit> OnConduitFrozenDelegate = new EventSystem.IntraObjectHandler<Conduit>(delegate(Conduit component, object data)
	{
		component.OnConduitFrozen(data);
	});

	// Token: 0x04001427 RID: 5159
	protected static readonly EventSystem.IntraObjectHandler<Conduit> OnConduitBoilingDelegate = new EventSystem.IntraObjectHandler<Conduit>(delegate(Conduit component, object data)
	{
		component.OnConduitBoiling(data);
	});

	// Token: 0x04001428 RID: 5160
	protected static readonly EventSystem.IntraObjectHandler<Conduit> OnStructureTemperatureRegisteredDelegate = new EventSystem.IntraObjectHandler<Conduit>(delegate(Conduit component, object data)
	{
		component.OnStructureTemperatureRegistered(data);
	});

	// Token: 0x04001429 RID: 5161
	protected static readonly EventSystem.IntraObjectHandler<Conduit> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<Conduit>(delegate(Conduit component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x0400142A RID: 5162
	protected static readonly EventSystem.IntraObjectHandler<Conduit> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<Conduit>(delegate(Conduit component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});
}
