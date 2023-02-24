using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x0200064A RID: 1610
public class StaterpillarGenerator : Generator
{
	// Token: 0x06002AC9 RID: 10953 RVA: 0x000E1B20 File Offset: 0x000DFD20
	protected override void OnSpawn()
	{
		Staterpillar staterpillar = this.parent.Get();
		if (staterpillar == null || staterpillar.GetGenerator() != this)
		{
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		this.smi = new StaterpillarGenerator.StatesInstance(this);
		this.smi.StartSM();
		base.OnSpawn();
	}

	// Token: 0x06002ACA RID: 10954 RVA: 0x000E1B7C File Offset: 0x000DFD7C
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		if (!this.operational.IsOperational)
		{
			return;
		}
		float num = base.GetComponent<Generator>().WattageRating;
		if (num > 0f)
		{
			num *= dt;
			num = Mathf.Max(num, 1f * dt);
			base.GenerateJoules(num, false);
		}
	}

	// Token: 0x04001957 RID: 6487
	private StaterpillarGenerator.StatesInstance smi;

	// Token: 0x04001958 RID: 6488
	[Serialize]
	public Ref<Staterpillar> parent = new Ref<Staterpillar>();

	// Token: 0x020012F3 RID: 4851
	public class StatesInstance : GameStateMachine<StaterpillarGenerator.States, StaterpillarGenerator.StatesInstance, StaterpillarGenerator, object>.GameInstance
	{
		// Token: 0x06007C01 RID: 31745 RVA: 0x002CDF00 File Offset: 0x002CC100
		public StatesInstance(StaterpillarGenerator master)
			: base(master)
		{
		}

		// Token: 0x04005F17 RID: 24343
		private Attributes attributes;
	}

	// Token: 0x020012F4 RID: 4852
	public class States : GameStateMachine<StaterpillarGenerator.States, StaterpillarGenerator.StatesInstance, StaterpillarGenerator>
	{
		// Token: 0x06007C02 RID: 31746 RVA: 0x002CDF0C File Offset: 0x002CC10C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.EventTransition(GameHashes.OperationalChanged, this.idle, (StaterpillarGenerator.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.idle.EventTransition(GameHashes.OperationalChanged, this.root, (StaterpillarGenerator.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational).Enter(delegate(StaterpillarGenerator.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			});
		}

		// Token: 0x04005F18 RID: 24344
		public GameStateMachine<StaterpillarGenerator.States, StaterpillarGenerator.StatesInstance, StaterpillarGenerator, object>.State idle;
	}
}
