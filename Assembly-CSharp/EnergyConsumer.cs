using System;
using System.Collections.Generic;
using System.Diagnostics;
using FMOD.Studio;
using FMODUnity;
using KSerialization;
using UnityEngine;

// Token: 0x02000741 RID: 1857
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name} {WattsUsed}W")]
[AddComponentMenu("KMonoBehaviour/scripts/EnergyConsumer")]
public class EnergyConsumer : KMonoBehaviour, ISaveLoadable, IEnergyConsumer, ICircuitConnected, IGameObjectEffectDescriptor
{
	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x06003311 RID: 13073 RVA: 0x001139AF File Offset: 0x00111BAF
	public int PowerSortOrder
	{
		get
		{
			return this.powerSortOrder;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06003312 RID: 13074 RVA: 0x001139B7 File Offset: 0x00111BB7
	// (set) Token: 0x06003313 RID: 13075 RVA: 0x001139BF File Offset: 0x00111BBF
	public int PowerCell { get; private set; }

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06003314 RID: 13076 RVA: 0x001139C8 File Offset: 0x00111BC8
	public bool HasWire
	{
		get
		{
			return Grid.Objects[this.PowerCell, 26] != null;
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06003315 RID: 13077 RVA: 0x001139E2 File Offset: 0x00111BE2
	// (set) Token: 0x06003316 RID: 13078 RVA: 0x001139F4 File Offset: 0x00111BF4
	public virtual bool IsPowered
	{
		get
		{
			return this.operational.GetFlag(EnergyConsumer.PoweredFlag);
		}
		protected set
		{
			this.operational.SetFlag(EnergyConsumer.PoweredFlag, value);
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06003317 RID: 13079 RVA: 0x00113A07 File Offset: 0x00111C07
	public bool IsConnected
	{
		get
		{
			return this.CircuitID != ushort.MaxValue;
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x06003318 RID: 13080 RVA: 0x00113A19 File Offset: 0x00111C19
	public string Name
	{
		get
		{
			return this.selectable.GetName();
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x06003319 RID: 13081 RVA: 0x00113A26 File Offset: 0x00111C26
	// (set) Token: 0x0600331A RID: 13082 RVA: 0x00113A2E File Offset: 0x00111C2E
	public bool IsVirtual { get; private set; }

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x0600331B RID: 13083 RVA: 0x00113A37 File Offset: 0x00111C37
	// (set) Token: 0x0600331C RID: 13084 RVA: 0x00113A3F File Offset: 0x00111C3F
	public object VirtualCircuitKey { get; private set; }

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x0600331D RID: 13085 RVA: 0x00113A48 File Offset: 0x00111C48
	// (set) Token: 0x0600331E RID: 13086 RVA: 0x00113A50 File Offset: 0x00111C50
	public ushort CircuitID { get; private set; }

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x0600331F RID: 13087 RVA: 0x00113A59 File Offset: 0x00111C59
	// (set) Token: 0x06003320 RID: 13088 RVA: 0x00113A61 File Offset: 0x00111C61
	public float BaseWattageRating
	{
		get
		{
			return this._BaseWattageRating;
		}
		set
		{
			this._BaseWattageRating = value;
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06003321 RID: 13089 RVA: 0x00113A6A File Offset: 0x00111C6A
	public float WattsUsed
	{
		get
		{
			if (this.operational.IsActive)
			{
				return this.BaseWattageRating;
			}
			return 0f;
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06003322 RID: 13090 RVA: 0x00113A85 File Offset: 0x00111C85
	public float WattsNeededWhenActive
	{
		get
		{
			return this.building.Def.EnergyConsumptionWhenActive;
		}
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x00113A97 File Offset: 0x00111C97
	protected override void OnPrefabInit()
	{
		this.CircuitID = ushort.MaxValue;
		this.IsPowered = false;
		this.BaseWattageRating = this.building.Def.EnergyConsumptionWhenActive;
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x00113AC4 File Offset: 0x00111CC4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.EnergyConsumers.Add(this);
		Building component = base.GetComponent<Building>();
		this.PowerCell = component.GetPowerInputCell();
		Game.Instance.circuitManager.Connect(this);
		Game.Instance.energySim.AddEnergyConsumer(this);
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x00113B15 File Offset: 0x00111D15
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveEnergyConsumer(this);
		Game.Instance.circuitManager.Disconnect(this, true);
		Components.EnergyConsumers.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x00113B49 File Offset: 0x00111D49
	public virtual void EnergySim200ms(float dt)
	{
		this.CircuitID = Game.Instance.circuitManager.GetCircuitID(this);
		if (!this.IsConnected)
		{
			this.IsPowered = false;
		}
		this.circuitOverloadTime = Mathf.Max(0f, this.circuitOverloadTime - dt);
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x00113B88 File Offset: 0x00111D88
	public virtual void SetConnectionStatus(CircuitManager.ConnectionStatus connection_status)
	{
		switch (connection_status)
		{
		case CircuitManager.ConnectionStatus.NotConnected:
			this.IsPowered = false;
			return;
		case CircuitManager.ConnectionStatus.Unpowered:
			if (this.IsPowered && base.GetComponent<Battery>() == null)
			{
				this.IsPowered = false;
				this.circuitOverloadTime = 6f;
				this.PlayCircuitSound("overdraw");
				return;
			}
			break;
		case CircuitManager.ConnectionStatus.Powered:
			if (!this.IsPowered && this.circuitOverloadTime <= 0f)
			{
				this.IsPowered = true;
				this.PlayCircuitSound("powered");
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06003328 RID: 13096 RVA: 0x00113C0C File Offset: 0x00111E0C
	protected void PlayCircuitSound(string state)
	{
		EventReference eventReference;
		if (state == "powered")
		{
			eventReference = Sounds.Instance.BuildingPowerOnMigrated;
		}
		else if (state == "overdraw")
		{
			eventReference = Sounds.Instance.ElectricGridOverloadMigrated;
		}
		else
		{
			eventReference = default(EventReference);
			global::Debug.Log("Invalid state for sound in EnergyConsumer.");
		}
		if (!CameraController.Instance.IsAudibleSound(base.transform.GetPosition()))
		{
			return;
		}
		float num;
		if (!this.lastTimeSoundPlayed.TryGetValue(state, out num))
		{
			num = 0f;
		}
		float num2 = (Time.time - num) / this.soundDecayTime;
		Vector3 position = base.transform.GetPosition();
		position.z = 0f;
		FMOD.Studio.EventInstance eventInstance = KFMOD.BeginOneShot(eventReference, CameraController.Instance.GetVerticallyScaledPosition(position, false), 1f);
		eventInstance.setParameterByName("timeSinceLast", num2, false);
		KFMOD.EndOneShot(eventInstance);
		this.lastTimeSoundPlayed[state] = Time.time;
	}

	// Token: 0x06003329 RID: 13097 RVA: 0x00113CFA File Offset: 0x00111EFA
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x04001F66 RID: 8038
	[MyCmpReq]
	private Building building;

	// Token: 0x04001F67 RID: 8039
	[MyCmpGet]
	protected Operational operational;

	// Token: 0x04001F68 RID: 8040
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001F69 RID: 8041
	[SerializeField]
	public int powerSortOrder;

	// Token: 0x04001F6B RID: 8043
	[Serialize]
	protected float circuitOverloadTime;

	// Token: 0x04001F6C RID: 8044
	public static readonly Operational.Flag PoweredFlag = new Operational.Flag("powered", Operational.Flag.Type.Requirement);

	// Token: 0x04001F6D RID: 8045
	private Dictionary<string, float> lastTimeSoundPlayed = new Dictionary<string, float>();

	// Token: 0x04001F6E RID: 8046
	private float soundDecayTime = 10f;

	// Token: 0x04001F72 RID: 8050
	private float _BaseWattageRating;
}
