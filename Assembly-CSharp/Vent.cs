using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009C6 RID: 2502
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Vent")]
public class Vent : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x06004A61 RID: 19041 RVA: 0x001A0A3B File Offset: 0x0019EC3B
	// (set) Token: 0x06004A62 RID: 19042 RVA: 0x001A0A43 File Offset: 0x0019EC43
	public int SortKey
	{
		get
		{
			return this.sortKey;
		}
		set
		{
			this.sortKey = value;
		}
	}

	// Token: 0x06004A63 RID: 19043 RVA: 0x001A0A4C File Offset: 0x0019EC4C
	public void UpdateVentedMass(SimHashes element, float mass)
	{
		if (!this.lifeTimeVentMass.ContainsKey(element))
		{
			this.lifeTimeVentMass.Add(element, mass);
			return;
		}
		Dictionary<SimHashes, float> dictionary = this.lifeTimeVentMass;
		dictionary[element] += mass;
	}

	// Token: 0x06004A64 RID: 19044 RVA: 0x001A0A8E File Offset: 0x0019EC8E
	public float GetVentedMass(SimHashes element)
	{
		if (this.lifeTimeVentMass.ContainsKey(element))
		{
			return this.lifeTimeVentMass[element];
		}
		return 0f;
	}

	// Token: 0x06004A65 RID: 19045 RVA: 0x001A0AB0 File Offset: 0x0019ECB0
	public bool Closed()
	{
		bool flag = false;
		return (this.operational.Flags.TryGetValue(LogicOperationalController.LogicOperationalFlag, out flag) && !flag) || (this.operational.Flags.TryGetValue(BuildingEnabledButton.EnabledFlag, out flag) && !flag);
	}

	// Token: 0x06004A66 RID: 19046 RVA: 0x001A0AFC File Offset: 0x0019ECFC
	protected override void OnSpawn()
	{
		Building component = base.GetComponent<Building>();
		this.cell = component.GetUtilityOutputCell();
		this.smi = new Vent.StatesInstance(this);
		this.smi.StartSM();
	}

	// Token: 0x06004A67 RID: 19047 RVA: 0x001A0B34 File Offset: 0x0019ED34
	public Vent.State GetEndPointState()
	{
		Vent.State state = Vent.State.Invalid;
		Endpoint endpoint = this.endpointType;
		if (endpoint != Endpoint.Source)
		{
			if (endpoint == Endpoint.Sink)
			{
				state = Vent.State.Ready;
				int num = this.cell;
				if (!this.IsValidOutputCell(num))
				{
					state = (Grid.Solid[num] ? Vent.State.Blocked : Vent.State.OverPressure);
				}
			}
		}
		else
		{
			state = (this.IsConnected() ? Vent.State.Ready : Vent.State.Blocked);
		}
		return state;
	}

	// Token: 0x06004A68 RID: 19048 RVA: 0x001A0B88 File Offset: 0x0019ED88
	public bool IsConnected()
	{
		UtilityNetwork networkForCell = Conduit.GetNetworkManager(this.conduitType).GetNetworkForCell(this.cell);
		return networkForCell != null && (networkForCell as FlowUtilityNetwork).HasSinks;
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06004A69 RID: 19049 RVA: 0x001A0BBC File Offset: 0x0019EDBC
	public bool IsBlocked
	{
		get
		{
			return this.GetEndPointState() != Vent.State.Ready;
		}
	}

	// Token: 0x06004A6A RID: 19050 RVA: 0x001A0BCC File Offset: 0x0019EDCC
	private bool IsValidOutputCell(int output_cell)
	{
		bool flag = false;
		if ((this.structure == null || !this.structure.IsEntombed() || !this.Closed()) && !Grid.Solid[output_cell])
		{
			flag = Grid.Mass[output_cell] < this.overpressureMass;
		}
		return flag;
	}

	// Token: 0x06004A6B RID: 19051 RVA: 0x001A0C20 File Offset: 0x0019EE20
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		string formattedMass = GameUtil.GetFormattedMass(this.overpressureMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.BUILDINGEFFECTS.OVER_PRESSURE_MASS, formattedMass), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.OVER_PRESSURE_MASS, formattedMass), Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x040030C6 RID: 12486
	private int cell = -1;

	// Token: 0x040030C7 RID: 12487
	private int sortKey;

	// Token: 0x040030C8 RID: 12488
	[Serialize]
	public Dictionary<SimHashes, float> lifeTimeVentMass = new Dictionary<SimHashes, float>();

	// Token: 0x040030C9 RID: 12489
	private Vent.StatesInstance smi;

	// Token: 0x040030CA RID: 12490
	[SerializeField]
	public ConduitType conduitType = ConduitType.Gas;

	// Token: 0x040030CB RID: 12491
	[SerializeField]
	public Endpoint endpointType;

	// Token: 0x040030CC RID: 12492
	[SerializeField]
	public float overpressureMass = 1f;

	// Token: 0x040030CD RID: 12493
	[NonSerialized]
	public bool showConnectivityIcons = true;

	// Token: 0x040030CE RID: 12494
	[MyCmpGet]
	[NonSerialized]
	public Structure structure;

	// Token: 0x040030CF RID: 12495
	[MyCmpGet]
	[NonSerialized]
	public Operational operational;

	// Token: 0x020017C3 RID: 6083
	public enum State
	{
		// Token: 0x04006DF3 RID: 28147
		Invalid,
		// Token: 0x04006DF4 RID: 28148
		Ready,
		// Token: 0x04006DF5 RID: 28149
		Blocked,
		// Token: 0x04006DF6 RID: 28150
		OverPressure,
		// Token: 0x04006DF7 RID: 28151
		Closed
	}

	// Token: 0x020017C4 RID: 6084
	public class StatesInstance : GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.GameInstance
	{
		// Token: 0x06008BCF RID: 35791 RVA: 0x00300491 File Offset: 0x002FE691
		public StatesInstance(Vent master)
			: base(master)
		{
			this.exhaust = master.GetComponent<Exhaust>();
		}

		// Token: 0x06008BD0 RID: 35792 RVA: 0x003004A6 File Offset: 0x002FE6A6
		public bool NeedsExhaust()
		{
			return this.exhaust != null && base.master.GetEndPointState() != Vent.State.Ready && base.master.endpointType == Endpoint.Source;
		}

		// Token: 0x06008BD1 RID: 35793 RVA: 0x003004D4 File Offset: 0x002FE6D4
		public bool Blocked()
		{
			return base.master.GetEndPointState() == Vent.State.Blocked && base.master.endpointType > Endpoint.Source;
		}

		// Token: 0x06008BD2 RID: 35794 RVA: 0x003004F4 File Offset: 0x002FE6F4
		public bool OverPressure()
		{
			return this.exhaust != null && base.master.GetEndPointState() == Vent.State.OverPressure && base.master.endpointType > Endpoint.Source;
		}

		// Token: 0x06008BD3 RID: 35795 RVA: 0x00300524 File Offset: 0x002FE724
		public void CheckTransitions()
		{
			if (this.NeedsExhaust())
			{
				base.smi.GoTo(base.sm.needExhaust);
				return;
			}
			if (base.master.Closed())
			{
				base.smi.GoTo(base.sm.closed);
				return;
			}
			if (this.Blocked())
			{
				base.smi.GoTo(base.sm.open.blocked);
				return;
			}
			if (this.OverPressure())
			{
				base.smi.GoTo(base.sm.open.overPressure);
				return;
			}
			base.smi.GoTo(base.sm.open.idle);
		}

		// Token: 0x06008BD4 RID: 35796 RVA: 0x003005D7 File Offset: 0x002FE7D7
		public StatusItem SelectStatusItem(StatusItem gas_status_item, StatusItem liquid_status_item)
		{
			if (base.master.conduitType != ConduitType.Gas)
			{
				return liquid_status_item;
			}
			return gas_status_item;
		}

		// Token: 0x04006DF8 RID: 28152
		private Exhaust exhaust;
	}

	// Token: 0x020017C5 RID: 6085
	public class States : GameStateMachine<Vent.States, Vent.StatesInstance, Vent>
	{
		// Token: 0x06008BD5 RID: 35797 RVA: 0x003005EC File Offset: 0x002FE7EC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.open.idle;
			this.root.Update("CheckTransitions", delegate(Vent.StatesInstance smi, float dt)
			{
				smi.CheckTransitions();
			}, UpdateRate.SIM_200ms, false);
			this.open.TriggerOnEnter(GameHashes.VentOpen, null);
			this.closed.TriggerOnEnter(GameHashes.VentClosed, null);
			this.open.blocked.ToggleStatusItem((Vent.StatesInstance smi) => smi.SelectStatusItem(Db.Get().BuildingStatusItems.GasVentObstructed, Db.Get().BuildingStatusItems.LiquidVentObstructed), null);
			this.open.overPressure.ToggleStatusItem((Vent.StatesInstance smi) => smi.SelectStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure, Db.Get().BuildingStatusItems.LiquidVentOverPressure), null);
		}

		// Token: 0x04006DF9 RID: 28153
		public Vent.States.OpenState open;

		// Token: 0x04006DFA RID: 28154
		public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State closed;

		// Token: 0x04006DFB RID: 28155
		public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State needExhaust;

		// Token: 0x020020D5 RID: 8405
		public class OpenState : GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State
		{
			// Token: 0x04009239 RID: 37433
			public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State idle;

			// Token: 0x0400923A RID: 37434
			public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State blocked;

			// Token: 0x0400923B RID: 37435
			public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State overPressure;
		}
	}
}
