using System;

// Token: 0x020006C0 RID: 1728
[SkipSaveFileSerialization]
public class BlightVulnerable : StateMachineComponent<BlightVulnerable.StatesInstance>
{
	// Token: 0x06002F06 RID: 12038 RVA: 0x000F8D3F File Offset: 0x000F6F3F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x000F8D47 File Offset: 0x000F6F47
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06002F08 RID: 12040 RVA: 0x000F8D5A File Offset: 0x000F6F5A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002F09 RID: 12041 RVA: 0x000F8D62 File Offset: 0x000F6F62
	public void MakeBlighted()
	{
		Debug.Log("Blighting plant", this);
		base.smi.sm.isBlighted.Set(true, base.smi, false);
	}

	// Token: 0x04001C4C RID: 7244
	private SchedulerHandle handle;

	// Token: 0x04001C4D RID: 7245
	public bool prefersDarkness;

	// Token: 0x0200139D RID: 5021
	public class StatesInstance : GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.GameInstance
	{
		// Token: 0x06007E6F RID: 32367 RVA: 0x002D9372 File Offset: 0x002D7572
		public StatesInstance(BlightVulnerable master)
			: base(master)
		{
		}
	}

	// Token: 0x0200139E RID: 5022
	public class States : GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable>
	{
		// Token: 0x06007E70 RID: 32368 RVA: 0x002D937C File Offset: 0x002D757C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.comfortable;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.comfortable.ParamTransition<bool>(this.isBlighted, this.blighted, GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.IsTrue);
			this.blighted.TriggerOnEnter(GameHashes.BlightChanged, (BlightVulnerable.StatesInstance smi) => true).Enter(delegate(BlightVulnerable.StatesInstance smi)
			{
				smi.GetComponent<SeedProducer>().seedInfo.seedId = RotPileConfig.ID;
			}).ToggleTag(GameTags.Blighted)
				.Exit(delegate(BlightVulnerable.StatesInstance smi)
				{
					GameplayEventManager.Instance.Trigger(-1425542080, smi.gameObject);
				});
		}

		// Token: 0x0400612E RID: 24878
		public StateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.BoolParameter isBlighted;

		// Token: 0x0400612F RID: 24879
		public GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.State comfortable;

		// Token: 0x04006130 RID: 24880
		public GameStateMachine<BlightVulnerable.States, BlightVulnerable.StatesInstance, BlightVulnerable, object>.State blighted;
	}
}
