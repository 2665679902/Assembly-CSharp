using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class EatChore : Chore<EatChore.StatesInstance>
{
	// Token: 0x06001236 RID: 4662 RVA: 0x00061428 File Offset: 0x0005F628
	public EatChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.Eat, master, master.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		base.smi = new EatChore.StatesInstance(this);
		this.showAvailabilityInHoverText = false;
		base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		base.AddPrecondition(EatChore.EdibleIsNotNull, null);
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x00061490 File Offset: 0x0005F690
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("EATCHORE null context.consumer");
			return;
		}
		RationMonitor.Instance smi = context.consumerState.consumer.GetSMI<RationMonitor.Instance>();
		if (smi == null)
		{
			global::Debug.LogError("EATCHORE null RationMonitor.Instance");
			return;
		}
		Edible edible = smi.GetEdible();
		if (edible.gameObject == null)
		{
			global::Debug.LogError("EATCHORE null edible.gameObject");
			return;
		}
		if (base.smi == null)
		{
			global::Debug.LogError("EATCHORE null smi");
			return;
		}
		if (base.smi.sm == null)
		{
			global::Debug.LogError("EATCHORE null smi.sm");
			return;
		}
		if (base.smi.sm.ediblesource == null)
		{
			global::Debug.LogError("EATCHORE null smi.sm.ediblesource");
			return;
		}
		base.smi.sm.ediblesource.Set(edible.gameObject, base.smi, false);
		KCrashReporter.Assert(edible.FoodInfo.CaloriesPerUnit > 0f, edible.GetProperName() + " has invalid calories per unit. Will result in NaNs");
		AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(this.gameObject);
		float num = (amountInstance.GetMax() - amountInstance.value) / edible.FoodInfo.CaloriesPerUnit;
		KCrashReporter.Assert(num > 0f, "EatChore is requesting an invalid amount of food");
		base.smi.sm.requestedfoodunits.Set(num, base.smi, false);
		base.smi.sm.eater.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x040009E8 RID: 2536
	public static readonly Chore.Precondition EdibleIsNotNull = new Chore.Precondition
	{
		id = "EdibleIsNotNull",
		description = DUPLICANTS.CHORES.PRECONDITIONS.EDIBLE_IS_NOT_NULL,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return null != context.consumerState.consumer.GetSMI<RationMonitor.Instance>().GetEdible();
		}
	};

	// Token: 0x02000F5A RID: 3930
	public class StatesInstance : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.GameInstance
	{
		// Token: 0x06006F3B RID: 28475 RVA: 0x0029FE78 File Offset: 0x0029E078
		public StatesInstance(EatChore master)
			: base(master)
		{
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x0029FE84 File Offset: 0x0029E084
		public void UpdateMessStation()
		{
			Ownables soleOwner = base.sm.eater.Get(base.smi).GetComponent<MinionIdentity>().GetSoleOwner();
			List<Assignable> list = Game.Instance.assignmentManager.GetPreferredAssignables(soleOwner, Db.Get().AssignableSlots.MessStation);
			if (list.Count == 0)
			{
				soleOwner.AutoAssignSlot(Db.Get().AssignableSlots.MessStation);
				list = Game.Instance.assignmentManager.GetPreferredAssignables(soleOwner, Db.Get().AssignableSlots.MessStation);
			}
			Assignable assignable = ((list.Count > 0) ? list[0] : null);
			base.smi.sm.messstation.Set(assignable, base.smi);
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x0029FF40 File Offset: 0x0029E140
		public bool UseSalt()
		{
			if (base.smi.sm.messstation != null && base.smi.sm.messstation.Get(base.smi) != null)
			{
				MessStation component = base.smi.sm.messstation.Get(base.smi).GetComponent<MessStation>();
				return component != null && component.HasSalt;
			}
			return false;
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0029FFB8 File Offset: 0x0029E1B8
		public void CreateLocator()
		{
			int num = base.sm.eater.Get<Sensors>(base.smi).GetSensor<SafeCellSensor>().GetCellQuery();
			if (num == Grid.InvalidCell)
			{
				num = Grid.PosToCell(base.sm.eater.Get<Transform>(base.smi).GetPosition());
			}
			Vector3 vector = Grid.CellToPosCBC(num, Grid.SceneLayer.Move);
			Grid.Reserved[num] = true;
			GameObject gameObject = ChoreHelpers.CreateLocator("EatLocator", vector);
			base.sm.locator.Set(gameObject, this, false);
			this.locatorCell = num;
		}

		// Token: 0x06006F3F RID: 28479 RVA: 0x002A004B File Offset: 0x0029E24B
		public void DestroyLocator()
		{
			Grid.Reserved[this.locatorCell] = false;
			ChoreHelpers.DestroyLocator(base.sm.locator.Get(this));
			base.sm.locator.Set(null, this);
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x002A0088 File Offset: 0x0029E288
		public void SetZ(GameObject go, float z)
		{
			Vector3 position = go.transform.GetPosition();
			position.z = z;
			go.transform.SetPosition(position);
		}

		// Token: 0x06006F41 RID: 28481 RVA: 0x002A00B8 File Offset: 0x0029E2B8
		public void ApplyRoomEffects()
		{
			Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.sm.messstation.Get(base.smi).gameObject);
			if (roomOfGameObject != null)
			{
				roomOfGameObject.roomType.TriggerRoomEffects(base.sm.messstation.Get(base.smi).gameObject.GetComponent<KPrefabID>(), base.sm.eater.Get(base.smi).gameObject.GetComponent<Effects>());
			}
		}

		// Token: 0x06006F42 RID: 28482 RVA: 0x002A0140 File Offset: 0x0029E340
		public void ApplySaltEffect()
		{
			Storage component = base.sm.messstation.Get(base.smi).gameObject.GetComponent<Storage>();
			if (component != null && component.Has(TableSaltConfig.ID.ToTag()))
			{
				component.ConsumeIgnoringDisease(TableSaltConfig.ID.ToTag(), TableSaltTuning.CONSUMABLE_RATE);
				base.sm.eater.Get(base.smi).gameObject.GetComponent<Worker>().GetComponent<Effects>().Add("MessTableSalt", true);
				base.sm.messstation.Get(base.smi).gameObject.Trigger(1356255274, null);
			}
		}

		// Token: 0x0400542B RID: 21547
		private int locatorCell;
	}

	// Token: 0x02000F5B RID: 3931
	public class States : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore>
	{
		// Token: 0x06006F43 RID: 28483 RVA: 0x002A01F8 File Offset: 0x0029E3F8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetch;
			base.Target(this.eater);
			this.root.Enter("SetMessStation", delegate(EatChore.StatesInstance smi)
			{
				smi.UpdateMessStation();
			}).EventHandler(GameHashes.AssignablesChanged, delegate(EatChore.StatesInstance smi)
			{
				smi.UpdateMessStation();
			});
			this.fetch.InitializeStates(this.eater, this.ediblesource, this.ediblechunk, this.requestedfoodunits, this.actualfoodunits, this.eatatmessstation, null);
			this.eatatmessstation.DefaultState(this.eatatmessstation.moveto).ParamTransition<GameObject>(this.messstation, this.eatonfloorstate, (EatChore.StatesInstance smi, GameObject p) => p == null).ParamTransition<GameObject>(this.messstation, this.eatonfloorstate, (EatChore.StatesInstance smi, GameObject p) => p != null && !p.GetComponent<Operational>().IsOperational);
			this.eatatmessstation.moveto.InitializeStates(this.eater, this.messstation, this.eatatmessstation.eat, this.eatonfloorstate, null, null);
			this.eatatmessstation.eat.Enter("AnimOverride", delegate(EatChore.StatesInstance smi)
			{
				smi.GetComponent<KAnimControllerBase>().AddAnimOverrides(Assets.GetAnim("anim_eat_table_kanim"), 0f);
			}).DoEat(this.ediblechunk, this.actualfoodunits, null, null).Enter(delegate(EatChore.StatesInstance smi)
			{
				smi.SetZ(this.eater.Get(smi), Grid.GetLayerZ(Grid.SceneLayer.BuildingFront));
				smi.ApplyRoomEffects();
				smi.ApplySaltEffect();
			})
				.Exit(delegate(EatChore.StatesInstance smi)
				{
					smi.SetZ(this.eater.Get(smi), Grid.GetLayerZ(Grid.SceneLayer.Move));
					smi.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(Assets.GetAnim("anim_eat_table_kanim"));
				});
			this.eatonfloorstate.DefaultState(this.eatonfloorstate.moveto).Enter("CreateLocator", delegate(EatChore.StatesInstance smi)
			{
				smi.CreateLocator();
			}).Exit("DestroyLocator", delegate(EatChore.StatesInstance smi)
			{
				smi.DestroyLocator();
			});
			this.eatonfloorstate.moveto.InitializeStates(this.eater, this.locator, this.eatonfloorstate.eat, this.eatonfloorstate.eat, null, null);
			this.eatonfloorstate.eat.ToggleAnims("anim_eat_floor_kanim", 0f, "").DoEat(this.ediblechunk, this.actualfoodunits, null, null);
		}

		// Token: 0x0400542C RID: 21548
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter eater;

		// Token: 0x0400542D RID: 21549
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter ediblesource;

		// Token: 0x0400542E RID: 21550
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter ediblechunk;

		// Token: 0x0400542F RID: 21551
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter messstation;

		// Token: 0x04005430 RID: 21552
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.FloatParameter requestedfoodunits;

		// Token: 0x04005431 RID: 21553
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.FloatParameter actualfoodunits;

		// Token: 0x04005432 RID: 21554
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter locator;

		// Token: 0x04005433 RID: 21555
		public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.FetchSubState fetch;

		// Token: 0x04005434 RID: 21556
		public EatChore.States.EatOnFloorState eatonfloorstate;

		// Token: 0x04005435 RID: 21557
		public EatChore.States.EatAtMessStationState eatatmessstation;

		// Token: 0x02001EA9 RID: 7849
		public class EatOnFloorState : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State
		{
			// Token: 0x0400895B RID: 35163
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.ApproachSubState<IApproachable> moveto;

			// Token: 0x0400895C RID: 35164
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State eat;
		}

		// Token: 0x02001EAA RID: 7850
		public class EatAtMessStationState : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State
		{
			// Token: 0x0400895D RID: 35165
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.ApproachSubState<MessStation> moveto;

			// Token: 0x0400895E RID: 35166
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State eat;
		}
	}
}
