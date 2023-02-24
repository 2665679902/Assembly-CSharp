using System;

// Token: 0x02000850 RID: 2128
public class ToiletMonitor : GameStateMachine<ToiletMonitor, ToiletMonitor.Instance>
{
	// Token: 0x06003D36 RID: 15670 RVA: 0x00156400 File Offset: 0x00154600
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.EventHandler(GameHashes.ToiletSensorChanged, delegate(ToiletMonitor.Instance smi)
		{
			smi.RefreshStatusItem();
		}).Exit("ClearStatusItem", delegate(ToiletMonitor.Instance smi)
		{
			smi.ClearStatusItem();
		});
	}

	// Token: 0x04002819 RID: 10265
	public GameStateMachine<ToiletMonitor, ToiletMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x0400281A RID: 10266
	public GameStateMachine<ToiletMonitor, ToiletMonitor.Instance, IStateMachineTarget, object>.State unsatisfied;

	// Token: 0x02001600 RID: 5632
	public new class Instance : GameStateMachine<ToiletMonitor, ToiletMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008652 RID: 34386 RVA: 0x002EEAC8 File Offset: 0x002ECCC8
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.toiletSensor = base.GetComponent<Sensors>().GetSensor<ToiletSensor>();
		}

		// Token: 0x06008653 RID: 34387 RVA: 0x002EEAE4 File Offset: 0x002ECCE4
		public void RefreshStatusItem()
		{
			StatusItem statusItem = null;
			if (!this.toiletSensor.AreThereAnyToilets())
			{
				statusItem = Db.Get().DuplicantStatusItems.NoToilets;
			}
			else if (!this.toiletSensor.AreThereAnyUsableToilets())
			{
				statusItem = Db.Get().DuplicantStatusItems.NoUsableToilets;
			}
			else if (this.toiletSensor.GetNearestUsableToilet() == null)
			{
				statusItem = Db.Get().DuplicantStatusItems.ToiletUnreachable;
			}
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Toilet, statusItem, null);
		}

		// Token: 0x06008654 RID: 34388 RVA: 0x002EEB6B File Offset: 0x002ECD6B
		public void ClearStatusItem()
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Toilet, null, null);
		}

		// Token: 0x040068A6 RID: 26790
		private ToiletSensor toiletSensor;
	}
}
