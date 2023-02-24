using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000630 RID: 1584
public class Reactor : StateMachineComponent<Reactor.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x060029B4 RID: 10676 RVA: 0x000DBF96 File Offset: 0x000DA196
	// (set) Token: 0x060029B5 RID: 10677 RVA: 0x000DBF9E File Offset: 0x000DA19E
	private float ReactionMassTarget
	{
		get
		{
			return this.reactionMassTarget;
		}
		set
		{
			this.fuelDelivery.capacity = value * 2f;
			this.fuelDelivery.refillMass = value * 0.2f;
			this.fuelDelivery.MinimumMass = value * 0.2f;
			this.reactionMassTarget = value;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000DBFDD File Offset: 0x000DA1DD
	public float FuelTemperature
	{
		get
		{
			if (this.reactionStorage.items.Count > 0)
			{
				return this.reactionStorage.items[0].GetComponent<PrimaryElement>().Temperature;
			}
			return -1f;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000DC014 File Offset: 0x000DA214
	public float ReserveCoolantMass
	{
		get
		{
			PrimaryElement storedCoolant = this.GetStoredCoolant();
			if (!(storedCoolant == null))
			{
				return storedCoolant.Mass;
			}
			return 0f;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000DC03D File Offset: 0x000DA23D
	public bool On
	{
		get
		{
			return base.smi.IsInsideState(base.smi.sm.on);
		}
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x000DC05C File Offset: 0x000DA25C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.NuclearReactors.Add(this);
		Storage[] components = base.GetComponents<Storage>();
		this.supplyStorage = components[0];
		this.reactionStorage = components[1];
		this.wasteStorage = components[2];
		this.CreateMeters();
		base.smi.StartSM();
		this.fuelDelivery = base.GetComponent<ManualDeliveryKG>();
		this.CheckLogicInputValueChanged(true);
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x000DC0C0 File Offset: 0x000DA2C0
	protected override void OnCleanUp()
	{
		Components.NuclearReactors.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x000DC0D3 File Offset: 0x000DA2D3
	private void Update()
	{
		this.CheckLogicInputValueChanged(false);
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x000DC0DC File Offset: 0x000DA2DC
	public Notification CreateMeltdownNotification()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		return new Notification(MISC.NOTIFICATIONS.REACTORMELTDOWN.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.REACTORMELTDOWN.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + component.GetProperName(), false, 0f, null, null, null, true, false, false);
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x000DC13B File Offset: 0x000DA33B
	public void SetStorages(Storage supply, Storage reaction, Storage waste)
	{
		this.supplyStorage = supply;
		this.reactionStorage = reaction;
		this.wasteStorage = waste;
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x000DC154 File Offset: 0x000DA354
	private void CheckLogicInputValueChanged(bool onLoad = false)
	{
		int num = 1;
		if (this.logicPorts.IsPortConnected("CONTROL_FUEL_DELIVERY"))
		{
			num = this.logicPorts.GetInputValue("CONTROL_FUEL_DELIVERY");
		}
		if (num == 0 && (this.fuelDeliveryEnabled || onLoad))
		{
			this.fuelDelivery.refillMass = -1f;
			this.fuelDeliveryEnabled = false;
			this.fuelDelivery.AbortDelivery("AutomationDisabled");
			return;
		}
		if (num == 1 && (!this.fuelDeliveryEnabled || onLoad))
		{
			this.fuelDelivery.refillMass = this.reactionMassTarget * 0.2f;
			this.fuelDeliveryEnabled = true;
		}
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x000DC1F4 File Offset: 0x000DA3F4
	private void OnLogicConnectionChanged(int value, bool connection)
	{
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x000DC1F8 File Offset: 0x000DA3F8
	private void CreateMeters()
	{
		this.temperatureMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "temperature_meter_target", "meter_temperature", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "temperature_meter_target" });
		this.waterMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "water_meter_target", "meter_water", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "water_meter_target" });
	}

	// Token: 0x060029C1 RID: 10689 RVA: 0x000DC260 File Offset: 0x000DA460
	private void TransferFuel()
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		PrimaryElement storedFuel = this.GetStoredFuel();
		float num = ((activeFuel != null) ? activeFuel.Mass : 0f);
		float num2 = ((storedFuel != null) ? storedFuel.Mass : 0f);
		float num3 = this.ReactionMassTarget - num;
		num3 = Mathf.Min(num2, num3);
		if (num3 > 0.5f || num3 == num2)
		{
			this.supplyStorage.Transfer(this.reactionStorage, this.fuelTag, num3, false, true);
		}
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x000DC2E8 File Offset: 0x000DA4E8
	private void TransferCoolant()
	{
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		PrimaryElement storedCoolant = this.GetStoredCoolant();
		float num = ((activeCoolant != null) ? activeCoolant.Mass : 0f);
		float num2 = ((storedCoolant != null) ? storedCoolant.Mass : 0f);
		float num3 = 30f - num;
		num3 = Mathf.Min(num2, num3);
		if (num3 > 0f)
		{
			this.supplyStorage.Transfer(this.reactionStorage, this.coolantTag, num3, false, true);
		}
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x000DC364 File Offset: 0x000DA564
	private PrimaryElement GetStoredFuel()
	{
		GameObject gameObject = this.supplyStorage.FindFirst(this.fuelTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x060029C4 RID: 10692 RVA: 0x000DC3A0 File Offset: 0x000DA5A0
	private PrimaryElement GetActiveFuel()
	{
		GameObject gameObject = this.reactionStorage.FindFirst(this.fuelTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x060029C5 RID: 10693 RVA: 0x000DC3DC File Offset: 0x000DA5DC
	private PrimaryElement GetStoredCoolant()
	{
		GameObject gameObject = this.supplyStorage.FindFirst(this.coolantTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x000DC418 File Offset: 0x000DA618
	private PrimaryElement GetActiveCoolant()
	{
		GameObject gameObject = this.reactionStorage.FindFirst(this.coolantTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x060029C7 RID: 10695 RVA: 0x000DC454 File Offset: 0x000DA654
	private bool CanStartReaction()
	{
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		PrimaryElement activeFuel = this.GetActiveFuel();
		return activeCoolant && activeFuel && activeCoolant.Mass >= 30f && activeFuel.Mass >= 0.5f;
	}

	// Token: 0x060029C8 RID: 10696 RVA: 0x000DC49C File Offset: 0x000DA69C
	private void Cool(float dt)
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		if (activeFuel == null)
		{
			return;
		}
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		if (activeCoolant == null)
		{
			return;
		}
		GameUtil.ForceConduction(activeFuel, activeCoolant, dt * 5f);
		if (activeCoolant.Temperature > 673.15f)
		{
			base.smi.sm.doVent.Trigger(base.smi);
		}
	}

	// Token: 0x060029C9 RID: 10697 RVA: 0x000DC504 File Offset: 0x000DA704
	private void React(float dt)
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		if (activeFuel != null && activeFuel.Mass >= 0.25f)
		{
			float num = GameUtil.EnergyToTemperatureDelta(-100f * dt * activeFuel.Mass, activeFuel);
			activeFuel.Temperature += num;
			this.spentFuel += dt * 0.016666668f;
		}
	}

	// Token: 0x060029CA RID: 10698 RVA: 0x000DC565 File Offset: 0x000DA765
	private void SetEmitRads(float rads)
	{
		base.smi.master.radEmitter.emitRads = rads;
		base.smi.master.radEmitter.Refresh();
	}

	// Token: 0x060029CB RID: 10699 RVA: 0x000DC594 File Offset: 0x000DA794
	private bool ReadyToCool()
	{
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		return activeCoolant != null && activeCoolant.Mass > 0f;
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x000DC5C0 File Offset: 0x000DA7C0
	private void DumpSpentFuel()
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		if (activeFuel != null)
		{
			if (this.spentFuel <= 0f)
			{
				return;
			}
			float num = this.spentFuel * 100f;
			if (num > 0f)
			{
				GameObject gameObject = ElementLoader.FindElementByHash(SimHashes.NuclearWaste).substance.SpawnResource(base.transform.position, num, activeFuel.Temperature, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.id), Mathf.RoundToInt(num * 50f), false, false, false);
				gameObject.AddTag(GameTags.Stored);
				this.wasteStorage.Store(gameObject, true, false, true, false);
			}
			if (this.wasteStorage.MassStored() >= 100f)
			{
				this.wasteStorage.DropAll(true, true, default(Vector3), true, null);
			}
			if (this.spentFuel >= activeFuel.Mass)
			{
				Util.KDestroyGameObject(activeFuel.gameObject);
				this.spentFuel = 0f;
				return;
			}
			activeFuel.Mass -= this.spentFuel;
			this.spentFuel = 0f;
		}
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x000DC6E8 File Offset: 0x000DA8E8
	private void UpdateVentStatus()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.ClearToVent())
		{
			if (component.HasStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure))
			{
				base.smi.sm.canVent.Set(true, base.smi, false);
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure, false);
				return;
			}
		}
		else if (!component.HasStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure))
		{
			base.smi.sm.canVent.Set(false, base.smi, false);
			component.AddStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure, null);
		}
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x000DC7A0 File Offset: 0x000DA9A0
	private void UpdateCoolantStatus()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.GetStoredCoolant() != null || base.smi.GetCurrentState() == base.smi.sm.meltdown || base.smi.GetCurrentState() == base.smi.sm.dead)
		{
			if (component.HasStatusItem(Db.Get().BuildingStatusItems.NoCoolant))
			{
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.NoCoolant, false);
				return;
			}
		}
		else if (!component.HasStatusItem(Db.Get().BuildingStatusItems.NoCoolant))
		{
			component.AddStatusItem(Db.Get().BuildingStatusItems.NoCoolant, null);
		}
	}

	// Token: 0x060029CF RID: 10703 RVA: 0x000DC85C File Offset: 0x000DAA5C
	private void InitVentCells()
	{
		if (this.ventCells == null)
		{
			this.ventCells = new int[]
			{
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.zero),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.left),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.right + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.left + Vector3.left),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.left),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.right + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.left + Vector3.left)
			};
		}
	}

	// Token: 0x060029D0 RID: 10704 RVA: 0x000DCAC8 File Offset: 0x000DACC8
	public int GetVentCell()
	{
		this.InitVentCells();
		for (int i = 0; i < this.ventCells.Length; i++)
		{
			if (Grid.Mass[this.ventCells[i]] < 150f && !Grid.Solid[this.ventCells[i]])
			{
				return this.ventCells[i];
			}
		}
		return -1;
	}

	// Token: 0x060029D1 RID: 10705 RVA: 0x000DCB28 File Offset: 0x000DAD28
	private bool ClearToVent()
	{
		this.InitVentCells();
		for (int i = 0; i < this.ventCells.Length; i++)
		{
			if (Grid.Mass[this.ventCells[i]] < 150f && !Grid.Solid[this.ventCells[i]])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060029D2 RID: 10706 RVA: 0x000DCB7E File Offset: 0x000DAD7E
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>();
	}

	// Token: 0x040018B3 RID: 6323
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040018B4 RID: 6324
	[MyCmpGet]
	private RadiationEmitter radEmitter;

	// Token: 0x040018B5 RID: 6325
	[MyCmpGet]
	private ManualDeliveryKG fuelDelivery;

	// Token: 0x040018B6 RID: 6326
	private MeterController temperatureMeter;

	// Token: 0x040018B7 RID: 6327
	private MeterController waterMeter;

	// Token: 0x040018B8 RID: 6328
	private Storage supplyStorage;

	// Token: 0x040018B9 RID: 6329
	private Storage reactionStorage;

	// Token: 0x040018BA RID: 6330
	private Storage wasteStorage;

	// Token: 0x040018BB RID: 6331
	private Tag fuelTag = SimHashes.EnrichedUranium.CreateTag();

	// Token: 0x040018BC RID: 6332
	private Tag coolantTag = GameTags.AnyWater;

	// Token: 0x040018BD RID: 6333
	private Vector3 dumpOffset = new Vector3(0f, 5f, 0f);

	// Token: 0x040018BE RID: 6334
	public static string MELTDOWN_STINGER = "Stinger_Loop_NuclearMeltdown";

	// Token: 0x040018BF RID: 6335
	private static float meterFrameScaleHack = 3f;

	// Token: 0x040018C0 RID: 6336
	[Serialize]
	private float spentFuel;

	// Token: 0x040018C1 RID: 6337
	private float timeSinceMeltdownEmit;

	// Token: 0x040018C2 RID: 6338
	private const float reactorMeltDownBonusMassAmount = 10f;

	// Token: 0x040018C3 RID: 6339
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x040018C4 RID: 6340
	private LogicEventHandler fuelControlPort;

	// Token: 0x040018C5 RID: 6341
	private bool fuelDeliveryEnabled = true;

	// Token: 0x040018C6 RID: 6342
	public Guid refuelStausHandle;

	// Token: 0x040018C7 RID: 6343
	[Serialize]
	public int numCyclesRunning;

	// Token: 0x040018C8 RID: 6344
	private float reactionMassTarget = 60f;

	// Token: 0x040018C9 RID: 6345
	private int[] ventCells;

	// Token: 0x020012BB RID: 4795
	public class StatesInstance : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.GameInstance
	{
		// Token: 0x06007B59 RID: 31577 RVA: 0x002CB3D3 File Offset: 0x002C95D3
		public StatesInstance(Reactor smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020012BC RID: 4796
	public class States : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor>
	{
		// Token: 0x06007B5A RID: 31578 RVA: 0x002CB3DC File Offset: 0x002C95DC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			default_state = this.off;
			this.root.EventHandler(GameHashes.OnStorageChange, delegate(Reactor.StatesInstance smi)
			{
				PrimaryElement storedCoolant = smi.master.GetStoredCoolant();
				if (!storedCoolant)
				{
					smi.master.waterMeter.SetPositionPercent(0f);
					return;
				}
				smi.master.waterMeter.SetPositionPercent(storedCoolant.Mass / 90f);
			});
			this.off_pre.QueueAnim("working_pst", false, null).OnAnimQueueComplete(this.off);
			this.off.PlayAnim("off").Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.master.radEmitter.SetEmitting(false);
				smi.master.SetEmitRads(0f);
			}).ParamTransition<bool>(this.reactionUnderway, this.on, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue)
				.ParamTransition<bool>(this.melted, this.dead, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue)
				.ParamTransition<bool>(this.meltingDown, this.meltdown, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue)
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.master.TransferFuel();
					smi.master.TransferCoolant();
					if (smi.master.CanStartReaction())
					{
						smi.GoTo(this.on);
					}
				}, UpdateRate.SIM_1000ms, false);
			this.on.Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.sm.reactionUnderway.Set(true, smi, false);
				smi.master.operational.SetActive(true, false);
				smi.master.SetEmitRads(2400f);
				smi.master.radEmitter.SetEmitting(true);
			}).EventHandler(GameHashes.NewDay, (Reactor.StatesInstance smi) => GameClock.Instance, delegate(Reactor.StatesInstance smi)
			{
				smi.master.numCyclesRunning++;
			}).Exit(delegate(Reactor.StatesInstance smi)
			{
				smi.sm.reactionUnderway.Set(false, smi, false);
				smi.master.numCyclesRunning = 0;
			})
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.master.TransferFuel();
					smi.master.TransferCoolant();
					smi.master.React(dt);
					smi.master.UpdateCoolantStatus();
					smi.master.UpdateVentStatus();
					smi.master.DumpSpentFuel();
					if (!smi.master.fuelDeliveryEnabled)
					{
						smi.master.refuelStausHandle = smi.master.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ReactorRefuelDisabled, null);
					}
					else
					{
						smi.master.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ReactorRefuelDisabled, false);
						smi.master.refuelStausHandle = Guid.Empty;
					}
					if (smi.master.GetActiveCoolant() != null)
					{
						smi.master.Cool(dt);
					}
					PrimaryElement activeFuel = smi.master.GetActiveFuel();
					if (activeFuel != null)
					{
						smi.master.temperatureMeter.SetPositionPercent(Mathf.Clamp01(activeFuel.Temperature / 3000f) / Reactor.meterFrameScaleHack);
						if (activeFuel.Temperature >= 3000f)
						{
							smi.sm.meltdownMassRemaining.Set(10f + smi.master.supplyStorage.MassStored() + smi.master.reactionStorage.MassStored() + smi.master.wasteStorage.MassStored(), smi, false);
							smi.master.supplyStorage.ConsumeAllIgnoringDisease();
							smi.master.reactionStorage.ConsumeAllIgnoringDisease();
							smi.master.wasteStorage.ConsumeAllIgnoringDisease();
							smi.GoTo(this.meltdown.pre);
							return;
						}
						if (activeFuel.Mass <= 0.25f)
						{
							smi.GoTo(this.off_pre);
							smi.master.temperatureMeter.SetPositionPercent(0f);
							return;
						}
					}
					else
					{
						smi.GoTo(this.off_pre);
						smi.master.temperatureMeter.SetPositionPercent(0f);
					}
				}, UpdateRate.SIM_200ms, false)
				.DefaultState(this.on.pre);
			this.on.pre.PlayAnim("working_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.on.reacting).OnSignal(this.doVent, this.on.venting);
			this.on.reacting.PlayAnim("working_loop", KAnim.PlayMode.Loop).OnSignal(this.doVent, this.on.venting);
			this.on.venting.ParamTransition<bool>(this.canVent, this.on.venting.vent, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue).ParamTransition<bool>(this.canVent, this.on.venting.ventIssue, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsFalse);
			this.on.venting.ventIssue.PlayAnim("venting_issue", KAnim.PlayMode.Loop).ParamTransition<bool>(this.canVent, this.on.venting.vent, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue);
			this.on.venting.vent.PlayAnim("venting").Enter(delegate(Reactor.StatesInstance smi)
			{
				PrimaryElement activeCoolant = smi.master.GetActiveCoolant();
				if (activeCoolant != null)
				{
					activeCoolant.GetComponent<Dumpable>().Dump(Grid.CellToPos(smi.master.GetVentCell()));
				}
			}).OnAnimQueueComplete(this.on.reacting);
			this.meltdown.ToggleStatusItem(Db.Get().BuildingStatusItems.ReactorMeltdown, null).ToggleNotification((Reactor.StatesInstance smi) => smi.master.CreateMeltdownNotification()).ParamTransition<float>(this.meltdownMassRemaining, this.dead, (Reactor.StatesInstance smi, float p) => p <= 0f)
				.ToggleTag(GameTags.DeadReactor)
				.DefaultState(this.meltdown.loop);
			this.meltdown.pre.PlayAnim("almost_meltdown_pre", KAnim.PlayMode.Once).QueueAnim("almost_meltdown_loop", false, null).QueueAnim("meltdown_pre", false, null)
				.OnAnimQueueComplete(this.meltdown.loop);
			this.meltdown.loop.PlayAnim("meltdown_loop", KAnim.PlayMode.Loop).Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.master.radEmitter.SetEmitting(true);
				smi.master.SetEmitRads(4800f);
				smi.master.temperatureMeter.SetPositionPercent(1f / Reactor.meterFrameScaleHack);
				smi.master.UpdateCoolantStatus();
				if (this.meltingDown.Get(smi))
				{
					MusicManager.instance.PlaySong(Reactor.MELTDOWN_STINGER, false);
					MusicManager.instance.StopDynamicMusic(false);
				}
				else
				{
					MusicManager.instance.PlaySong(Reactor.MELTDOWN_STINGER, false);
					MusicManager.instance.SetSongParameter(Reactor.MELTDOWN_STINGER, "Music_PlayStinger", 1f, true);
					MusicManager.instance.StopDynamicMusic(false);
				}
				this.meltingDown.Set(true, smi, false);
			}).Exit(delegate(Reactor.StatesInstance smi)
			{
				this.meltingDown.Set(false, smi, false);
				MusicManager.instance.SetSongParameter(Reactor.MELTDOWN_STINGER, "Music_NuclearMeltdownActive", 0f, true);
			})
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.master.timeSinceMeltdownEmit += dt;
					float num = 0.5f;
					float num2 = 5f;
					if (smi.master.timeSinceMeltdownEmit > num && smi.sm.meltdownMassRemaining.Get(smi) > 0f)
					{
						smi.master.timeSinceMeltdownEmit -= num;
						float num3 = Mathf.Min(smi.sm.meltdownMassRemaining.Get(smi), num2);
						smi.sm.meltdownMassRemaining.Delta(-num3, smi);
						for (int i = 0; i < 3; i++)
						{
							if (num3 >= NuclearWasteCometConfig.MASS)
							{
								GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(NuclearWasteCometConfig.ID), smi.master.transform.position + Vector3.up * 2f, Quaternion.identity, null, null, true, 0);
								gameObject.SetActive(true);
								Comet component = gameObject.GetComponent<Comet>();
								component.ignoreObstacleForDamage.Set(smi.master.gameObject.GetComponent<KPrefabID>());
								component.addTiles = 1;
								int num4 = 270;
								while (num4 > 225 && num4 < 335)
								{
									num4 = UnityEngine.Random.Range(0, 360);
								}
								float num5 = (float)num4 * 3.1415927f / 180f;
								component.Velocity = new Vector2(-Mathf.Cos(num5) * 20f, Mathf.Sin(num5) * 20f);
								component.GetComponent<KBatchedAnimController>().Rotation = (float)(-(float)num4) - 90f;
								num3 -= NuclearWasteCometConfig.MASS;
							}
						}
						for (int j = 0; j < 3; j++)
						{
							if (num3 >= 0.001f)
							{
								SimMessages.AddRemoveSubstance(Grid.PosToCell(smi.master.transform.position + Vector3.up * 3f + Vector3.right * (float)j * 2f), SimHashes.NuclearWaste, CellEventLogger.Instance.ElementEmitted, num3 / 3f, 3000f, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.RoundToInt(50f * (num3 / 3f)), true, -1);
							}
						}
					}
				}, UpdateRate.SIM_200ms, false);
			this.dead.PlayAnim("dead").ToggleTag(GameTags.DeadReactor).Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.master.temperatureMeter.SetPositionPercent(1f / Reactor.meterFrameScaleHack);
				smi.master.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.DeadReactorCoolingOff, smi);
				this.melted.Set(true, smi, false);
			})
				.Exit(delegate(Reactor.StatesInstance smi)
				{
					smi.master.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.DeadReactorCoolingOff, false);
				})
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.sm.timeSinceMeltdown.Delta(dt, smi);
					smi.master.radEmitter.emitRads = Mathf.Lerp(4800f, 0f, smi.sm.timeSinceMeltdown.Get(smi) / 3000f);
					smi.master.radEmitter.Refresh();
				}, UpdateRate.SIM_200ms, false);
		}

		// Token: 0x04005E88 RID: 24200
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.Signal doVent;

		// Token: 0x04005E89 RID: 24201
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter canVent = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter(true);

		// Token: 0x04005E8A RID: 24202
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter reactionUnderway = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter();

		// Token: 0x04005E8B RID: 24203
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter meltdownMassRemaining = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter(0f);

		// Token: 0x04005E8C RID: 24204
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter timeSinceMeltdown = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter(0f);

		// Token: 0x04005E8D RID: 24205
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter meltingDown = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter(false);

		// Token: 0x04005E8E RID: 24206
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter melted = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter(false);

		// Token: 0x04005E8F RID: 24207
		public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State off;

		// Token: 0x04005E90 RID: 24208
		public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State off_pre;

		// Token: 0x04005E91 RID: 24209
		public Reactor.States.ReactingStates on;

		// Token: 0x04005E92 RID: 24210
		public Reactor.States.MeltdownStates meltdown;

		// Token: 0x04005E93 RID: 24211
		public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State dead;

		// Token: 0x02001FE8 RID: 8168
		public class ReactingStates : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State
		{
			// Token: 0x04008E40 RID: 36416
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State pre;

			// Token: 0x04008E41 RID: 36417
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State reacting;

			// Token: 0x04008E42 RID: 36418
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State pst;

			// Token: 0x04008E43 RID: 36419
			public Reactor.States.ReactingStates.VentingStates venting;

			// Token: 0x02002DAD RID: 11693
			public class VentingStates : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State
			{
				// Token: 0x0400BA49 RID: 47689
				public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State ventIssue;

				// Token: 0x0400BA4A RID: 47690
				public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State vent;
			}
		}

		// Token: 0x02001FE9 RID: 8169
		public class MeltdownStates : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State
		{
			// Token: 0x04008E44 RID: 36420
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State almost_pre;

			// Token: 0x04008E45 RID: 36421
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State almost_loop;

			// Token: 0x04008E46 RID: 36422
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State pre;

			// Token: 0x04008E47 RID: 36423
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State loop;
		}
	}
}
