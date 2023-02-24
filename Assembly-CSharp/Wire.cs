using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000673 RID: 1651
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Wire")]
public class Wire : KMonoBehaviour, IDisconnectable, IFirstFrameCallback, IWattageRating, IHaveUtilityNetworkMgr, IBridgedNetworkItem
{
	// Token: 0x06002C7B RID: 11387 RVA: 0x000E9760 File Offset: 0x000E7960
	public static float GetMaxWattageAsFloat(Wire.WattageRating rating)
	{
		switch (rating)
		{
		case Wire.WattageRating.Max500:
			return 500f;
		case Wire.WattageRating.Max1000:
			return 1000f;
		case Wire.WattageRating.Max2000:
			return 2000f;
		case Wire.WattageRating.Max20000:
			return 20000f;
		case Wire.WattageRating.Max50000:
			return 50000f;
		default:
			return 0f;
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000E97AC File Offset: 0x000E79AC
	public bool IsConnected
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Game.Instance.electricalConduitSystem.GetNetworkForCell(num) is ElectricalUtilityNetwork;
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000E97E4 File Offset: 0x000E79E4
	public ushort NetworkID
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			ElectricalUtilityNetwork electricalUtilityNetwork = Game.Instance.electricalConduitSystem.GetNetworkForCell(num) as ElectricalUtilityNetwork;
			if (electricalUtilityNetwork == null)
			{
				return ushort.MaxValue;
			}
			return (ushort)electricalUtilityNetwork.id;
		}
	}

	// Token: 0x06002C7E RID: 11390 RVA: 0x000E9828 File Offset: 0x000E7A28
	protected override void OnSpawn()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		Game.Instance.electricalConduitSystem.AddToNetworks(num, this, false);
		this.InitializeSwitchState();
		base.Subscribe<Wire>(774203113, Wire.OnBuildingBrokenDelegate);
		base.Subscribe<Wire>(-1735440190, Wire.OnBuildingFullyRepairedDelegate);
		base.GetComponent<KSelectable>().AddStatusItem(Wire.WireCircuitStatus, this);
		base.GetComponent<KSelectable>().AddStatusItem(Wire.WireMaxWattageStatus, this);
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(Wire.OutlineSymbol, false);
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x000E98B8 File Offset: 0x000E7AB8
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			Game.Instance.electricalConduitSystem.RemoveFromNetworks(num, this, false);
		}
		base.Unsubscribe<Wire>(774203113, Wire.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<Wire>(-1735440190, Wire.OnBuildingFullyRepairedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x000E9944 File Offset: 0x000E7B44
	private void InitializeSwitchState()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		bool flag = false;
		GameObject gameObject = Grid.Objects[num, 1];
		if (gameObject != null)
		{
			CircuitSwitch component = gameObject.GetComponent<CircuitSwitch>();
			if (component != null)
			{
				flag = true;
				component.AttachWire(this);
			}
		}
		if (!flag)
		{
			this.Connect();
		}
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x000E99A0 File Offset: 0x000E7BA0
	public UtilityConnections GetWireConnections()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return Game.Instance.electricalConduitSystem.GetConnections(num, true);
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x000E99D0 File Offset: 0x000E7BD0
	public string GetWireConnectionsString()
	{
		UtilityConnections wireConnections = this.GetWireConnections();
		return Game.Instance.electricalConduitSystem.GetVisualizerString(wireConnections);
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x000E99F4 File Offset: 0x000E7BF4
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x000E99FC File Offset: 0x000E7BFC
	private void OnBuildingFullyRepaired(object data)
	{
		this.InitializeSwitchState();
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x000E9A04 File Offset: 0x000E7C04
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<KPrefabID>().AddTag(GameTags.Wires, false);
		if (Wire.WireCircuitStatus == null)
		{
			Wire.WireCircuitStatus = new StatusItem("WireCircuitStatus", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null).SetResolveStringCallback(delegate(string str, object data)
			{
				Wire wire = (Wire)data;
				int num = Grid.PosToCell(wire.transform.GetPosition());
				CircuitManager circuitManager = Game.Instance.circuitManager;
				ushort circuitID = circuitManager.GetCircuitID(num);
				float wattsUsedByCircuit = circuitManager.GetWattsUsedByCircuit(circuitID);
				GameUtil.WattageFormatterUnit wattageFormatterUnit = GameUtil.WattageFormatterUnit.Watts;
				if (wire.MaxWattageRating >= Wire.WattageRating.Max20000)
				{
					wattageFormatterUnit = GameUtil.WattageFormatterUnit.Kilowatts;
				}
				float maxWattageAsFloat = Wire.GetMaxWattageAsFloat(wire.MaxWattageRating);
				float wattsNeededWhenActive = circuitManager.GetWattsNeededWhenActive(circuitID);
				string wireLoadColor = GameUtil.GetWireLoadColor(wattsUsedByCircuit, maxWattageAsFloat, wattsNeededWhenActive);
				str = str.Replace("{CurrentLoadAndColor}", (wireLoadColor == Color.white.ToHexString()) ? GameUtil.GetFormattedWattage(wattsUsedByCircuit, wattageFormatterUnit, true) : string.Concat(new string[]
				{
					"<color=#",
					wireLoadColor,
					">",
					GameUtil.GetFormattedWattage(wattsUsedByCircuit, wattageFormatterUnit, true),
					"</color>"
				}));
				str = str.Replace("{MaxLoad}", GameUtil.GetFormattedWattage(maxWattageAsFloat, wattageFormatterUnit, true));
				str = str.Replace("{WireType}", this.GetProperName());
				return str;
			});
		}
		if (Wire.WireMaxWattageStatus == null)
		{
			Wire.WireMaxWattageStatus = new StatusItem("WireMaxWattageStatus", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null).SetResolveStringCallback(delegate(string str, object data)
			{
				Wire wire2 = (Wire)data;
				GameUtil.WattageFormatterUnit wattageFormatterUnit2 = GameUtil.WattageFormatterUnit.Watts;
				if (wire2.MaxWattageRating >= Wire.WattageRating.Max20000)
				{
					wattageFormatterUnit2 = GameUtil.WattageFormatterUnit.Kilowatts;
				}
				int num2 = Grid.PosToCell(wire2.transform.GetPosition());
				CircuitManager circuitManager2 = Game.Instance.circuitManager;
				ushort circuitID2 = circuitManager2.GetCircuitID(num2);
				float wattsNeededWhenActive2 = circuitManager2.GetWattsNeededWhenActive(circuitID2);
				float maxWattageAsFloat2 = Wire.GetMaxWattageAsFloat(wire2.MaxWattageRating);
				str = str.Replace("{TotalPotentialLoadAndColor}", (wattsNeededWhenActive2 > maxWattageAsFloat2) ? string.Concat(new string[]
				{
					"<color=#",
					new Color(0.9843137f, 0.6901961f, 0.23137255f).ToHexString(),
					">",
					GameUtil.GetFormattedWattage(wattsNeededWhenActive2, wattageFormatterUnit2, true),
					"</color>"
				}) : GameUtil.GetFormattedWattage(wattsNeededWhenActive2, wattageFormatterUnit2, true));
				str = str.Replace("{MaxLoad}", GameUtil.GetFormattedWattage(maxWattageAsFloat2, wattageFormatterUnit2, true));
				return str;
			});
		}
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x000E9ABB File Offset: 0x000E7CBB
	public Wire.WattageRating GetMaxWattageRating()
	{
		return this.MaxWattageRating;
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x000E9AC3 File Offset: 0x000E7CC3
	public bool IsDisconnected()
	{
		return this.disconnected;
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x000E9ACC File Offset: 0x000E7CCC
	public bool Connect()
	{
		BuildingHP component = base.GetComponent<BuildingHP>();
		if (component == null || component.HitPoints > 0)
		{
			this.disconnected = false;
			Game.Instance.electricalConduitSystem.ForceRebuildNetworks();
		}
		return !this.disconnected;
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x000E9B14 File Offset: 0x000E7D14
	public void Disconnect()
	{
		this.disconnected = true;
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.WireDisconnected, null);
		Game.Instance.electricalConduitSystem.ForceRebuildNetworks();
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x000E9B62 File Offset: 0x000E7D62
	public void SetFirstFrameCallback(System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x000E9B78 File Offset: 0x000E7D78
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

	// Token: 0x06002C8C RID: 11404 RVA: 0x000E9B87 File Offset: 0x000E7D87
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.electricalConduitSystem;
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x000E9B94 File Offset: 0x000E7D94
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.electricalConduitSystem.GetNetworkForCell(num);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x000E9BD0 File Offset: 0x000E7DD0
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		UtilityNetwork networkForCell = Game.Instance.electricalConduitSystem.GetNetworkForCell(num);
		return networks.Contains(networkForCell);
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x000E9C06 File Offset: 0x000E7E06
	public int GetNetworkCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x04001A7A RID: 6778
	[SerializeField]
	public Wire.WattageRating MaxWattageRating;

	// Token: 0x04001A7B RID: 6779
	[SerializeField]
	private bool disconnected = true;

	// Token: 0x04001A7C RID: 6780
	public static readonly KAnimHashedString OutlineSymbol = new KAnimHashedString("outline");

	// Token: 0x04001A7D RID: 6781
	public float circuitOverloadTime;

	// Token: 0x04001A7E RID: 6782
	private static readonly EventSystem.IntraObjectHandler<Wire> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<Wire>(delegate(Wire component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x04001A7F RID: 6783
	private static readonly EventSystem.IntraObjectHandler<Wire> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<Wire>(delegate(Wire component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});

	// Token: 0x04001A80 RID: 6784
	private static StatusItem WireCircuitStatus = null;

	// Token: 0x04001A81 RID: 6785
	private static StatusItem WireMaxWattageStatus = null;

	// Token: 0x04001A82 RID: 6786
	private System.Action firstFrameCallback;

	// Token: 0x0200133A RID: 4922
	public enum WattageRating
	{
		// Token: 0x04005FE9 RID: 24553
		Max500,
		// Token: 0x04005FEA RID: 24554
		Max1000,
		// Token: 0x04005FEB RID: 24555
		Max2000,
		// Token: 0x04005FEC RID: 24556
		Max20000,
		// Token: 0x04005FED RID: 24557
		Max50000,
		// Token: 0x04005FEE RID: 24558
		NumRatings
	}
}
