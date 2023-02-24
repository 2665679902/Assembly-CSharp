using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200055D RID: 1373
[SerializationConfig(MemberSerialization.OptIn)]
public class BottleEmptier : StateMachineComponent<BottleEmptier.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x0600210C RID: 8460 RVA: 0x000B40D3 File Offset: 0x000B22D3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		base.Subscribe<BottleEmptier>(493375141, BottleEmptier.OnRefreshUserMenuDelegate);
		base.Subscribe<BottleEmptier>(-905833192, BottleEmptier.OnCopySettingsDelegate);
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x000B4108 File Offset: 0x000B2308
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x000B410B File Offset: 0x000B230B
	private void OnChangeAllowManualPumpingStationFetching()
	{
		this.allowManualPumpingStationFetching = !this.allowManualPumpingStationFetching;
		base.smi.RefreshChore();
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x000B4128 File Offset: 0x000B2328
	private void OnRefreshUserMenu(object data)
	{
		if (this.isGasEmptier)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = (this.allowManualPumpingStationFetching ? new KIconButtonMenu.ButtonInfo("action_bottler_delivery", UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.DENIED_GAS.NAME, new System.Action(this.OnChangeAllowManualPumpingStationFetching), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.DENIED_GAS.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_bottler_delivery", UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.ALLOWED_GAS.NAME, new System.Action(this.OnChangeAllowManualPumpingStationFetching), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.ALLOWED_GAS.TOOLTIP, true));
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 0.4f);
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo2 = (this.allowManualPumpingStationFetching ? new KIconButtonMenu.ButtonInfo("action_bottler_delivery", UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.DENIED.NAME, new System.Action(this.OnChangeAllowManualPumpingStationFetching), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.DENIED.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_bottler_delivery", UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.ALLOWED.NAME, new System.Action(this.OnChangeAllowManualPumpingStationFetching), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_PUMP_DELIVERY.ALLOWED.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo2, 0.4f);
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x000B425C File Offset: 0x000B245C
	private void OnCopySettings(object data)
	{
		BottleEmptier component = ((GameObject)data).GetComponent<BottleEmptier>();
		this.allowManualPumpingStationFetching = component.allowManualPumpingStationFetching;
		base.smi.RefreshChore();
	}

	// Token: 0x04001302 RID: 4866
	public float emptyRate = 10f;

	// Token: 0x04001303 RID: 4867
	[Serialize]
	public bool allowManualPumpingStationFetching;

	// Token: 0x04001304 RID: 4868
	public bool isGasEmptier;

	// Token: 0x04001305 RID: 4869
	private static readonly EventSystem.IntraObjectHandler<BottleEmptier> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<BottleEmptier>(delegate(BottleEmptier component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04001306 RID: 4870
	private static readonly EventSystem.IntraObjectHandler<BottleEmptier> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<BottleEmptier>(delegate(BottleEmptier component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001186 RID: 4486
	public class StatesInstance : GameStateMachine<BottleEmptier.States, BottleEmptier.StatesInstance, BottleEmptier, object>.GameInstance
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060076D9 RID: 30425 RVA: 0x002B9257 File Offset: 0x002B7457
		// (set) Token: 0x060076DA RID: 30426 RVA: 0x002B925F File Offset: 0x002B745F
		public MeterController meter { get; private set; }

		// Token: 0x060076DB RID: 30427 RVA: 0x002B9268 File Offset: 0x002B7468
		public StatesInstance(BottleEmptier smi)
			: base(smi)
		{
			TreeFilterable component = base.master.GetComponent<TreeFilterable>();
			component.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Combine(component.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
			this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
			base.Subscribe(-1697596308, new Action<object>(this.OnStorageChange));
			base.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		}

		// Token: 0x060076DC RID: 30428 RVA: 0x002B9314 File Offset: 0x002B7514
		public void CreateChore()
		{
			HashSet<Tag> tags = base.GetComponent<TreeFilterable>().GetTags();
			Tag[] array;
			if (!base.master.allowManualPumpingStationFetching)
			{
				array = new Tag[]
				{
					GameTags.LiquidSource,
					GameTags.GasSource
				};
			}
			else
			{
				array = new Tag[0];
			}
			Storage component = base.GetComponent<Storage>();
			this.chore = new FetchChore(Db.Get().ChoreTypes.StorageFetch, component, component.Capacity(), tags, FetchChore.MatchCriteria.MatchID, Tag.Invalid, array, null, true, null, null, null, Operational.State.Operational, 0);
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x002B9399 File Offset: 0x002B7599
		public void CancelChore()
		{
			if (this.chore != null)
			{
				this.chore.Cancel("Storage Changed");
				this.chore = null;
			}
		}

		// Token: 0x060076DE RID: 30430 RVA: 0x002B93BA File Offset: 0x002B75BA
		public void RefreshChore()
		{
			this.GoTo(base.sm.unoperational);
		}

		// Token: 0x060076DF RID: 30431 RVA: 0x002B93CD File Offset: 0x002B75CD
		private void OnFilterChanged(HashSet<Tag> tags)
		{
			this.RefreshChore();
		}

		// Token: 0x060076E0 RID: 30432 RVA: 0x002B93D8 File Offset: 0x002B75D8
		private void OnStorageChange(object data)
		{
			Storage component = base.GetComponent<Storage>();
			this.meter.SetPositionPercent(Mathf.Clamp01(component.RemainingCapacity() / component.capacityKg));
		}

		// Token: 0x060076E1 RID: 30433 RVA: 0x002B9409 File Offset: 0x002B7609
		private void OnOnlyFetchMarkedItemsSettingChanged(object data)
		{
			this.RefreshChore();
		}

		// Token: 0x060076E2 RID: 30434 RVA: 0x002B9414 File Offset: 0x002B7614
		public void StartMeter()
		{
			PrimaryElement firstPrimaryElement = this.GetFirstPrimaryElement();
			if (firstPrimaryElement == null)
			{
				return;
			}
			this.meter.SetSymbolTint(new KAnimHashedString("meter_fill"), firstPrimaryElement.Element.substance.colour);
			this.meter.SetSymbolTint(new KAnimHashedString("water1"), firstPrimaryElement.Element.substance.colour);
			base.GetComponent<KBatchedAnimController>().SetSymbolTint(new KAnimHashedString("leak_ceiling"), firstPrimaryElement.Element.substance.colour);
		}

		// Token: 0x060076E3 RID: 30435 RVA: 0x002B94A8 File Offset: 0x002B76A8
		private PrimaryElement GetFirstPrimaryElement()
		{
			Storage component = base.GetComponent<Storage>();
			for (int i = 0; i < component.Count; i++)
			{
				GameObject gameObject = component[i];
				if (!(gameObject == null))
				{
					PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
					if (!(component2 == null))
					{
						return component2;
					}
				}
			}
			return null;
		}

		// Token: 0x060076E4 RID: 30436 RVA: 0x002B94F4 File Offset: 0x002B76F4
		public void Emit(float dt)
		{
			PrimaryElement firstPrimaryElement = this.GetFirstPrimaryElement();
			if (firstPrimaryElement == null)
			{
				return;
			}
			Storage component = base.GetComponent<Storage>();
			float num = Mathf.Min(firstPrimaryElement.Mass, base.master.emptyRate * dt);
			if (num <= 0f)
			{
				return;
			}
			Tag prefabTag = firstPrimaryElement.GetComponent<KPrefabID>().PrefabTag;
			float num2;
			SimUtil.DiseaseInfo diseaseInfo;
			float num3;
			component.ConsumeAndGetDisease(prefabTag, num, out num2, out diseaseInfo, out num3);
			Vector3 position = base.transform.GetPosition();
			position.y += 1.8f;
			bool flag = base.GetComponent<Rotatable>().GetOrientation() == Orientation.FlipH;
			position.x += (flag ? (-0.2f) : 0.2f);
			int num4 = Grid.PosToCell(position) + (flag ? (-1) : 1);
			if (Grid.Solid[num4])
			{
				num4 += (flag ? 1 : (-1));
			}
			Element element = firstPrimaryElement.Element;
			ushort idx = element.idx;
			if (element.IsLiquid)
			{
				FallingWater.instance.AddParticle(num4, idx, num2, num3, diseaseInfo.idx, diseaseInfo.count, true, false, false, false);
				return;
			}
			SimMessages.ModifyCell(num4, idx, num3, num2, diseaseInfo.idx, diseaseInfo.count, SimMessages.ReplaceType.None, false, -1);
		}

		// Token: 0x04005B17 RID: 23319
		private FetchChore chore;
	}

	// Token: 0x02001187 RID: 4487
	public class States : GameStateMachine<BottleEmptier.States, BottleEmptier.StatesInstance, BottleEmptier>
	{
		// Token: 0x060076E5 RID: 30437 RVA: 0x002B9624 File Offset: 0x002B7824
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.waitingfordelivery;
			this.statusItem = new StatusItem("BottleEmptier", "", "", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItem.resolveStringCallback = delegate(string str, object data)
			{
				BottleEmptier bottleEmptier = (BottleEmptier)data;
				if (bottleEmptier == null)
				{
					return str;
				}
				if (bottleEmptier.allowManualPumpingStationFetching)
				{
					return bottleEmptier.isGasEmptier ? BUILDING.STATUSITEMS.CANISTER_EMPTIER.ALLOWED.NAME : BUILDING.STATUSITEMS.BOTTLE_EMPTIER.ALLOWED.NAME;
				}
				return bottleEmptier.isGasEmptier ? BUILDING.STATUSITEMS.CANISTER_EMPTIER.DENIED.NAME : BUILDING.STATUSITEMS.BOTTLE_EMPTIER.DENIED.NAME;
			};
			this.statusItem.resolveTooltipCallback = delegate(string str, object data)
			{
				BottleEmptier bottleEmptier2 = (BottleEmptier)data;
				if (bottleEmptier2 == null)
				{
					return str;
				}
				if (bottleEmptier2.allowManualPumpingStationFetching)
				{
					if (bottleEmptier2.isGasEmptier)
					{
						return BUILDING.STATUSITEMS.CANISTER_EMPTIER.ALLOWED.TOOLTIP;
					}
					return BUILDING.STATUSITEMS.BOTTLE_EMPTIER.ALLOWED.TOOLTIP;
				}
				else
				{
					if (bottleEmptier2.isGasEmptier)
					{
						return BUILDING.STATUSITEMS.CANISTER_EMPTIER.DENIED.TOOLTIP;
					}
					return BUILDING.STATUSITEMS.BOTTLE_EMPTIER.DENIED.TOOLTIP;
				}
			};
			this.root.ToggleStatusItem(this.statusItem, (BottleEmptier.StatesInstance smi) => smi.master);
			this.unoperational.TagTransition(GameTags.Operational, this.waitingfordelivery, false).PlayAnim("off");
			this.waitingfordelivery.TagTransition(GameTags.Operational, this.unoperational, true).EventTransition(GameHashes.OnStorageChange, this.emptying, (BottleEmptier.StatesInstance smi) => !smi.GetComponent<Storage>().IsEmpty()).Enter("CreateChore", delegate(BottleEmptier.StatesInstance smi)
			{
				smi.CreateChore();
			})
				.Exit("CancelChore", delegate(BottleEmptier.StatesInstance smi)
				{
					smi.CancelChore();
				})
				.PlayAnim("on");
			this.emptying.TagTransition(GameTags.Operational, this.unoperational, true).EventTransition(GameHashes.OnStorageChange, this.waitingfordelivery, (BottleEmptier.StatesInstance smi) => smi.GetComponent<Storage>().IsEmpty()).Enter("StartMeter", delegate(BottleEmptier.StatesInstance smi)
			{
				smi.StartMeter();
			})
				.Update("Emit", delegate(BottleEmptier.StatesInstance smi, float dt)
				{
					smi.Emit(dt);
				}, UpdateRate.SIM_200ms, false)
				.PlayAnim("working_loop", KAnim.PlayMode.Loop);
		}

		// Token: 0x04005B19 RID: 23321
		private StatusItem statusItem;

		// Token: 0x04005B1A RID: 23322
		public GameStateMachine<BottleEmptier.States, BottleEmptier.StatesInstance, BottleEmptier, object>.State unoperational;

		// Token: 0x04005B1B RID: 23323
		public GameStateMachine<BottleEmptier.States, BottleEmptier.StatesInstance, BottleEmptier, object>.State waitingfordelivery;

		// Token: 0x04005B1C RID: 23324
		public GameStateMachine<BottleEmptier.States, BottleEmptier.StatesInstance, BottleEmptier, object>.State emptying;
	}
}
