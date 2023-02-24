using System;

// Token: 0x02000899 RID: 2201
public class SaltPlant : StateMachineComponent<SaltPlant.StatesInstance>
{
	// Token: 0x06003F1C RID: 16156 RVA: 0x0016055D File Offset: 0x0015E75D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SaltPlant>(-724860998, SaltPlant.OnWiltDelegate);
		base.Subscribe<SaltPlant>(712767498, SaltPlant.OnWiltRecoverDelegate);
	}

	// Token: 0x06003F1D RID: 16157 RVA: 0x00160587 File Offset: 0x0015E787
	private void OnWilt(object data = null)
	{
		base.gameObject.GetComponent<ElementConsumer>().EnableConsumption(false);
	}

	// Token: 0x06003F1E RID: 16158 RVA: 0x0016059A File Offset: 0x0015E79A
	private void OnWiltRecover(object data = null)
	{
		base.gameObject.GetComponent<ElementConsumer>().EnableConsumption(true);
	}

	// Token: 0x0400296A RID: 10602
	private static readonly EventSystem.IntraObjectHandler<SaltPlant> OnWiltDelegate = new EventSystem.IntraObjectHandler<SaltPlant>(delegate(SaltPlant component, object data)
	{
		component.OnWilt(data);
	});

	// Token: 0x0400296B RID: 10603
	private static readonly EventSystem.IntraObjectHandler<SaltPlant> OnWiltRecoverDelegate = new EventSystem.IntraObjectHandler<SaltPlant>(delegate(SaltPlant component, object data)
	{
		component.OnWiltRecover(data);
	});

	// Token: 0x02001665 RID: 5733
	public class StatesInstance : GameStateMachine<SaltPlant.States, SaltPlant.StatesInstance, SaltPlant, object>.GameInstance
	{
		// Token: 0x0600879B RID: 34715 RVA: 0x002F33BC File Offset: 0x002F15BC
		public StatesInstance(SaltPlant master)
			: base(master)
		{
		}
	}

	// Token: 0x02001666 RID: 5734
	public class States : GameStateMachine<SaltPlant.States, SaltPlant.StatesInstance, SaltPlant>
	{
		// Token: 0x0600879C RID: 34716 RVA: 0x002F33C5 File Offset: 0x002F15C5
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.alive;
			this.alive.DoNothing();
		}

		// Token: 0x0400698C RID: 27020
		public GameStateMachine<SaltPlant.States, SaltPlant.StatesInstance, SaltPlant, object>.State alive;
	}
}
