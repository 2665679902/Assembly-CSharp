using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000957 RID: 2391
[SerializationConfig(MemberSerialization.OptIn)]
public class PodLander : StateMachineComponent<PodLander.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060046AD RID: 18093 RVA: 0x0018DEDE File Offset: 0x0018C0DE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060046AE RID: 18094 RVA: 0x0018DEF4 File Offset: 0x0018C0F4
	public void ReleaseAstronaut()
	{
		if (this.releasingAstronaut)
		{
			return;
		}
		this.releasingAstronaut = true;
		MinionStorage component = base.GetComponent<MinionStorage>();
		List<MinionStorage.Info> storedMinionInfo = component.GetStoredMinionInfo();
		for (int i = storedMinionInfo.Count - 1; i >= 0; i--)
		{
			MinionStorage.Info info = storedMinionInfo[i];
			component.DeserializeMinion(info.id, Grid.CellToPos(Grid.PosToCell(base.smi.master.transform.GetPosition())));
		}
		this.releasingAstronaut = false;
	}

	// Token: 0x060046AF RID: 18095 RVA: 0x0018DF6D File Offset: 0x0018C16D
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x04002EBB RID: 11963
	[Serialize]
	private int landOffLocation;

	// Token: 0x04002EBC RID: 11964
	[Serialize]
	private float flightAnimOffset;

	// Token: 0x04002EBD RID: 11965
	private float rocketSpeed;

	// Token: 0x04002EBE RID: 11966
	public float exhaustEmitRate = 2f;

	// Token: 0x04002EBF RID: 11967
	public float exhaustTemperature = 1000f;

	// Token: 0x04002EC0 RID: 11968
	public SimHashes exhaustElement = SimHashes.CarbonDioxide;

	// Token: 0x04002EC1 RID: 11969
	private GameObject soundSpeakerObject;

	// Token: 0x04002EC2 RID: 11970
	private bool releasingAstronaut;

	// Token: 0x02001753 RID: 5971
	public class StatesInstance : GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander, object>.GameInstance
	{
		// Token: 0x06008A8D RID: 35469 RVA: 0x002FCF33 File Offset: 0x002FB133
		public StatesInstance(PodLander master)
			: base(master)
		{
		}
	}

	// Token: 0x02001754 RID: 5972
	public class States : GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander>
	{
		// Token: 0x06008A8E RID: 35470 RVA: 0x002FCF3C File Offset: 0x002FB13C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.landing;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.landing.PlayAnim("launch_loop", KAnim.PlayMode.Loop).Enter(delegate(PodLander.StatesInstance smi)
			{
				smi.master.flightAnimOffset = 50f;
			}).Update(delegate(PodLander.StatesInstance smi, float dt)
			{
				float num = 10f;
				smi.master.rocketSpeed = num - Mathf.Clamp(Mathf.Pow(smi.timeinstate / 3.5f, 4f), 0f, num - 2f);
				smi.master.flightAnimOffset -= dt * smi.master.rocketSpeed;
				KBatchedAnimController component = smi.master.GetComponent<KBatchedAnimController>();
				component.Offset = Vector3.up * smi.master.flightAnimOffset;
				Vector3 positionIncludingOffset = component.PositionIncludingOffset;
				int num2 = Grid.PosToCell(smi.master.gameObject.transform.GetPosition() + smi.master.GetComponent<KBatchedAnimController>().Offset);
				if (Grid.IsValidCell(num2))
				{
					SimMessages.EmitMass(num2, ElementLoader.GetElementIndex(smi.master.exhaustElement), dt * smi.master.exhaustEmitRate, smi.master.exhaustTemperature, 0, 0, -1);
				}
				if (component.Offset.y <= 0f)
				{
					smi.GoTo(this.crashed);
				}
			}, UpdateRate.SIM_33ms, false);
			this.crashed.PlayAnim("grounded").Enter(delegate(PodLander.StatesInstance smi)
			{
				smi.master.GetComponent<KBatchedAnimController>().Offset = Vector3.zero;
				smi.master.rocketSpeed = 0f;
				smi.master.ReleaseAstronaut();
			});
		}

		// Token: 0x04006CBE RID: 27838
		public GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander, object>.State landing;

		// Token: 0x04006CBF RID: 27839
		public GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander, object>.State crashed;
	}
}
