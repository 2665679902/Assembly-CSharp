using System;
using KSerialization;
using STRINGS;

// Token: 0x0200095C RID: 2396
[SerializationConfig(MemberSerialization.OptIn)]
public class RocketEngine : StateMachineComponent<RocketEngine.StatesInstance>
{
	// Token: 0x060046CD RID: 18125 RVA: 0x0018E89C File Offset: 0x0018CA9C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		if (this.mainEngine)
		{
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new RequireAttachedComponent(base.gameObject.GetComponent<AttachableBuilding>(), typeof(FuelTank), UI.STARMAP.COMPONENT.FUEL_TANK));
		}
	}

	// Token: 0x04002EDB RID: 11995
	public float exhaustEmitRate = 50f;

	// Token: 0x04002EDC RID: 11996
	public float exhaustTemperature = 1500f;

	// Token: 0x04002EDD RID: 11997
	public SpawnFXHashes explosionEffectHash;

	// Token: 0x04002EDE RID: 11998
	public SimHashes exhaustElement = SimHashes.CarbonDioxide;

	// Token: 0x04002EDF RID: 11999
	public Tag fuelTag;

	// Token: 0x04002EE0 RID: 12000
	public float efficiency = 1f;

	// Token: 0x04002EE1 RID: 12001
	public bool requireOxidizer = true;

	// Token: 0x04002EE2 RID: 12002
	public bool mainEngine = true;

	// Token: 0x0200175A RID: 5978
	public class StatesInstance : GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.GameInstance
	{
		// Token: 0x06008AAB RID: 35499 RVA: 0x002FD93A File Offset: 0x002FBB3A
		public StatesInstance(RocketEngine smi)
			: base(smi)
		{
		}
	}

	// Token: 0x0200175B RID: 5979
	public class States : GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine>
	{
		// Token: 0x06008AAC RID: 35500 RVA: 0x002FD944 File Offset: 0x002FBB44
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("grounded", KAnim.PlayMode.Loop).EventTransition(GameHashes.IgniteEngine, this.burning, null);
			this.burning.EventTransition(GameHashes.RocketLanded, this.burnComplete, null).PlayAnim("launch_pre").QueueAnim("launch_loop", true, null)
				.Update(delegate(RocketEngine.StatesInstance smi, float dt)
				{
					int num = Grid.PosToCell(smi.master.gameObject.transform.GetPosition() + smi.master.GetComponent<KBatchedAnimController>().Offset);
					if (Grid.IsValidCell(num))
					{
						SimMessages.EmitMass(num, ElementLoader.GetElementIndex(smi.master.exhaustElement), dt * smi.master.exhaustEmitRate, smi.master.exhaustTemperature, 0, 0, -1);
					}
					int num2 = 10;
					for (int i = 1; i < num2; i++)
					{
						int num3 = Grid.OffsetCell(num, -1, -i);
						int num4 = Grid.OffsetCell(num, 0, -i);
						int num5 = Grid.OffsetCell(num, 1, -i);
						if (Grid.IsValidCell(num3))
						{
							SimMessages.ModifyEnergy(num3, smi.master.exhaustTemperature / (float)(i + 1), 3200f, SimMessages.EnergySourceID.Burner);
						}
						if (Grid.IsValidCell(num4))
						{
							SimMessages.ModifyEnergy(num4, smi.master.exhaustTemperature / (float)i, 3200f, SimMessages.EnergySourceID.Burner);
						}
						if (Grid.IsValidCell(num5))
						{
							SimMessages.ModifyEnergy(num5, smi.master.exhaustTemperature / (float)(i + 1), 3200f, SimMessages.EnergySourceID.Burner);
						}
					}
				}, UpdateRate.SIM_200ms, false);
			this.burnComplete.PlayAnim("grounded", KAnim.PlayMode.Loop).EventTransition(GameHashes.IgniteEngine, this.burning, null);
		}

		// Token: 0x04006CD2 RID: 27858
		public GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.State idle;

		// Token: 0x04006CD3 RID: 27859
		public GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.State burning;

		// Token: 0x04006CD4 RID: 27860
		public GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.State burnComplete;
	}
}
