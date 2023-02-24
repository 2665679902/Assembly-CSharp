using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200067A RID: 1658
public class CargoDropperMinion : GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>
{
	// Token: 0x06002CB5 RID: 11445 RVA: 0x000EA670 File Offset: 0x000E8870
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.notLanded;
		this.root.ParamTransition<bool>(this.hasLanded, this.complete, GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.IsTrue);
		this.notLanded.EventHandlerTransition(GameHashes.JettisonCargo, this.landed, (CargoDropperMinion.StatesInstance smi, object obj) => true);
		this.landed.Enter(delegate(CargoDropperMinion.StatesInstance smi)
		{
			smi.JettisonCargo(null);
			smi.GoTo(this.exiting);
		});
		this.exiting.Update(delegate(CargoDropperMinion.StatesInstance smi, float dt)
		{
			if (!smi.SyncMinionExitAnimation())
			{
				smi.GoTo(this.complete);
			}
		}, UpdateRate.SIM_200ms, false);
		this.complete.Enter(delegate(CargoDropperMinion.StatesInstance smi)
		{
			this.hasLanded.Set(true, smi, false);
		});
	}

	// Token: 0x04001AA1 RID: 6817
	private GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.State notLanded;

	// Token: 0x04001AA2 RID: 6818
	private GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.State landed;

	// Token: 0x04001AA3 RID: 6819
	private GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.State exiting;

	// Token: 0x04001AA4 RID: 6820
	private GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.State complete;

	// Token: 0x04001AA5 RID: 6821
	public StateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.BoolParameter hasLanded = new StateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.BoolParameter(false);

	// Token: 0x0200133E RID: 4926
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005FF5 RID: 24565
		public Vector3 dropOffset;

		// Token: 0x04005FF6 RID: 24566
		public string kAnimName;

		// Token: 0x04005FF7 RID: 24567
		public string animName;

		// Token: 0x04005FF8 RID: 24568
		public Grid.SceneLayer animLayer = Grid.SceneLayer.Move;

		// Token: 0x04005FF9 RID: 24569
		public bool notifyOnJettison;
	}

	// Token: 0x0200133F RID: 4927
	public class StatesInstance : GameStateMachine<CargoDropperMinion, CargoDropperMinion.StatesInstance, IStateMachineTarget, CargoDropperMinion.Def>.GameInstance
	{
		// Token: 0x06007D1C RID: 32028 RVA: 0x002D305C File Offset: 0x002D125C
		public StatesInstance(IStateMachineTarget master, CargoDropperMinion.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x002D3068 File Offset: 0x002D1268
		public void JettisonCargo(object data = null)
		{
			Vector3 vector = base.master.transform.GetPosition() + base.def.dropOffset;
			MinionStorage component = base.GetComponent<MinionStorage>();
			if (component != null)
			{
				List<MinionStorage.Info> storedMinionInfo = component.GetStoredMinionInfo();
				for (int i = storedMinionInfo.Count - 1; i >= 0; i--)
				{
					MinionStorage.Info info = storedMinionInfo[i];
					GameObject gameObject = component.DeserializeMinion(info.id, vector);
					this.escapingMinion = gameObject.GetComponent<MinionIdentity>();
					gameObject.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
					ChoreProvider component2 = gameObject.GetComponent<ChoreProvider>();
					if (component2 != null)
					{
						this.exitAnimChore = new EmoteChore(component2, Db.Get().ChoreTypes.EmoteHighPriority, base.def.kAnimName, new HashedString[] { base.def.animName }, KAnim.PlayMode.Once, false);
						Vector3 position = gameObject.transform.GetPosition();
						position.z = Grid.GetLayerZ(base.def.animLayer);
						gameObject.transform.SetPosition(position);
						gameObject.GetMyWorld().SetDupeVisited();
					}
					if (base.def.notifyOnJettison)
					{
						gameObject.GetComponent<Notifier>().Add(this.CreateCrashLandedNotification(), "");
					}
				}
			}
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x002D31C4 File Offset: 0x002D13C4
		public bool SyncMinionExitAnimation()
		{
			if (this.escapingMinion != null && this.exitAnimChore != null && !this.exitAnimChore.isComplete)
			{
				KBatchedAnimController component = this.escapingMinion.GetComponent<KBatchedAnimController>();
				KBatchedAnimController component2 = base.master.GetComponent<KBatchedAnimController>();
				if (component2.CurrentAnim.name == base.def.animName)
				{
					component.SetElapsedTime(component2.GetElapsedTime());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x002D3238 File Offset: 0x002D1438
		public Notification CreateCrashLandedNotification()
		{
			return new Notification(MISC.NOTIFICATIONS.DUPLICANT_CRASH_LANDED.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.DUPLICANT_CRASH_LANDED.TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
		}

		// Token: 0x04005FFA RID: 24570
		public MinionIdentity escapingMinion;

		// Token: 0x04005FFB RID: 24571
		public Chore exitAnimChore;
	}
}
