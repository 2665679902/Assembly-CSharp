using System;
using UnityEngine;

// Token: 0x02000858 RID: 2136
public class EntityElementExchanger : StateMachineComponent<EntityElementExchanger.StatesInstance>
{
	// Token: 0x06003D59 RID: 15705 RVA: 0x00157130 File Offset: 0x00155330
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003D5A RID: 15706 RVA: 0x00157138 File Offset: 0x00155338
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003D5B RID: 15707 RVA: 0x0015714B File Offset: 0x0015534B
	public void SetConsumptionRate(float consumptionRate)
	{
		this.consumeRate = consumptionRate;
	}

	// Token: 0x06003D5C RID: 15708 RVA: 0x00157154 File Offset: 0x00155354
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		EntityElementExchanger entityElementExchanger = (EntityElementExchanger)data;
		if (entityElementExchanger != null)
		{
			entityElementExchanger.OnSimConsume(mass_cb_info);
		}
	}

	// Token: 0x06003D5D RID: 15709 RVA: 0x00157178 File Offset: 0x00155378
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		float num = mass_cb_info.mass * base.smi.master.exchangeRatio;
		if (this.reportExchange && base.smi.master.emittedElement == SimHashes.Oxygen)
		{
			string text = base.gameObject.GetProperName();
			ReceptacleMonitor component = base.GetComponent<ReceptacleMonitor>();
			if (component != null && component.GetReceptacle() != null)
			{
				text = text + " (" + component.GetReceptacle().gameObject.GetProperName() + ")";
			}
			ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, num, text, null);
		}
		SimMessages.EmitMass(Grid.PosToCell(base.smi.master.transform.GetPosition() + this.outputOffset), ElementLoader.FindElementByHash(base.smi.master.emittedElement).idx, num, ElementLoader.FindElementByHash(base.smi.master.emittedElement).defaultValues.temperature, byte.MaxValue, 0, -1);
	}

	// Token: 0x04002844 RID: 10308
	public Vector3 outputOffset = Vector3.zero;

	// Token: 0x04002845 RID: 10309
	public bool reportExchange;

	// Token: 0x04002846 RID: 10310
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002847 RID: 10311
	public SimHashes consumedElement;

	// Token: 0x04002848 RID: 10312
	public SimHashes emittedElement;

	// Token: 0x04002849 RID: 10313
	public float consumeRate;

	// Token: 0x0400284A RID: 10314
	public float exchangeRatio;

	// Token: 0x02001610 RID: 5648
	public class StatesInstance : GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger, object>.GameInstance
	{
		// Token: 0x060086AB RID: 34475 RVA: 0x002EF8FA File Offset: 0x002EDAFA
		public StatesInstance(EntityElementExchanger master)
			: base(master)
		{
		}
	}

	// Token: 0x02001611 RID: 5649
	public class States : GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger>
	{
		// Token: 0x060086AC RID: 34476 RVA: 0x002EF904 File Offset: 0x002EDB04
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.exchanging;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.exchanging.Enter(delegate(EntityElementExchanger.StatesInstance smi)
			{
				WiltCondition component = smi.master.gameObject.GetComponent<WiltCondition>();
				if (component != null && component.IsWilting())
				{
					smi.GoTo(smi.sm.paused);
				}
			}).EventTransition(GameHashes.Wilt, this.paused, null).ToggleStatusItem(Db.Get().CreatureStatusItems.ExchangingElementConsume, null)
				.ToggleStatusItem(Db.Get().CreatureStatusItems.ExchangingElementOutput, null)
				.Update("EntityElementExchanger", delegate(EntityElementExchanger.StatesInstance smi, float dt)
				{
					HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(EntityElementExchanger.OnSimConsumeCallback), smi.master, "EntityElementExchanger");
					SimMessages.ConsumeMass(Grid.PosToCell(smi.master.gameObject), smi.master.consumedElement, smi.master.consumeRate * dt, 3, handle.index);
				}, UpdateRate.SIM_1000ms, false);
			this.paused.EventTransition(GameHashes.WiltRecover, this.exchanging, null);
		}

		// Token: 0x040068E7 RID: 26855
		public GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger, object>.State exchanging;

		// Token: 0x040068E8 RID: 26856
		public GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger, object>.State paused;
	}
}
