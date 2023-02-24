using System;
using UnityEngine;

// Token: 0x0200038C RID: 908
public class MoveToSafetyChore : Chore<MoveToSafetyChore.StatesInstance>
{
	// Token: 0x0600127C RID: 4732 RVA: 0x00062E2C File Offset: 0x0006102C
	public MoveToSafetyChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.MoveToSafety, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.idle, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new MoveToSafetyChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F7C RID: 3964
	public class StatesInstance : GameStateMachine<MoveToSafetyChore.States, MoveToSafetyChore.StatesInstance, MoveToSafetyChore, object>.GameInstance
	{
		// Token: 0x06006FA3 RID: 28579 RVA: 0x002A3094 File Offset: 0x002A1294
		public StatesInstance(MoveToSafetyChore master, GameObject mover)
			: base(master)
		{
			base.sm.mover.Set(mover, base.smi, false);
			this.sensor = base.sm.mover.Get<Sensors>(base.smi).GetSensor<SafeCellSensor>();
			this.targetCell = this.sensor.GetSensorCell();
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x002A30F3 File Offset: 0x002A12F3
		public void UpdateTargetCell()
		{
			this.targetCell = this.sensor.GetSensorCell();
		}

		// Token: 0x040054A6 RID: 21670
		private SafeCellSensor sensor;

		// Token: 0x040054A7 RID: 21671
		public int targetCell;
	}

	// Token: 0x02000F7D RID: 3965
	public class States : GameStateMachine<MoveToSafetyChore.States, MoveToSafetyChore.StatesInstance, MoveToSafetyChore>
	{
		// Token: 0x06006FA5 RID: 28581 RVA: 0x002A3108 File Offset: 0x002A1308
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.move;
			base.Target(this.mover);
			this.root.ToggleTag(GameTags.Idle);
			this.move.Enter("UpdateLocatorPosition", delegate(MoveToSafetyChore.StatesInstance smi)
			{
				smi.UpdateTargetCell();
			}).Update("UpdateLocatorPosition", delegate(MoveToSafetyChore.StatesInstance smi, float dt)
			{
				smi.UpdateTargetCell();
			}, UpdateRate.SIM_200ms, false).MoveTo((MoveToSafetyChore.StatesInstance smi) => smi.targetCell, null, null, true);
		}

		// Token: 0x040054A8 RID: 21672
		public StateMachine<MoveToSafetyChore.States, MoveToSafetyChore.StatesInstance, MoveToSafetyChore, object>.TargetParameter mover;

		// Token: 0x040054A9 RID: 21673
		public GameStateMachine<MoveToSafetyChore.States, MoveToSafetyChore.StatesInstance, MoveToSafetyChore, object>.State move;
	}
}
