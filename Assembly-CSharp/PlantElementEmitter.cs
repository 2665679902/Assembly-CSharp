using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200088D RID: 2189
public class PlantElementEmitter : StateMachineComponent<PlantElementEmitter.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06003EBF RID: 16063 RVA: 0x0015EDF3 File Offset: 0x0015CFF3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x0015EE06 File Offset: 0x0015D006
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>();
	}

	// Token: 0x04002911 RID: 10513
	[MyCmpGet]
	private WiltCondition wiltCondition;

	// Token: 0x04002912 RID: 10514
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002913 RID: 10515
	public SimHashes emittedElement;

	// Token: 0x04002914 RID: 10516
	public float emitRate;

	// Token: 0x02001647 RID: 5703
	public class StatesInstance : GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter, object>.GameInstance
	{
		// Token: 0x06008732 RID: 34610 RVA: 0x002F1227 File Offset: 0x002EF427
		public StatesInstance(PlantElementEmitter master)
			: base(master)
		{
		}

		// Token: 0x06008733 RID: 34611 RVA: 0x002F1230 File Offset: 0x002EF430
		public bool IsWilting()
		{
			return !(base.master.wiltCondition == null) && base.master.wiltCondition != null && base.master.wiltCondition.IsWilting();
		}
	}

	// Token: 0x02001648 RID: 5704
	public class States : GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter>
	{
		// Token: 0x06008734 RID: 34612 RVA: 0x002F126C File Offset: 0x002EF46C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.healthy;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.healthy.EventTransition(GameHashes.Wilt, this.wilted, (PlantElementEmitter.StatesInstance smi) => smi.IsWilting()).Update("PlantEmit", delegate(PlantElementEmitter.StatesInstance smi, float dt)
			{
				SimMessages.EmitMass(Grid.PosToCell(smi.master.gameObject), ElementLoader.FindElementByHash(smi.master.emittedElement).idx, smi.master.emitRate * dt, ElementLoader.FindElementByHash(smi.master.emittedElement).defaultValues.temperature, byte.MaxValue, 0, -1);
			}, UpdateRate.SIM_4000ms, false);
			this.wilted.EventTransition(GameHashes.WiltRecover, this.healthy, null);
		}

		// Token: 0x04006952 RID: 26962
		public GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter, object>.State wilted;

		// Token: 0x04006953 RID: 26963
		public GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter, object>.State healthy;
	}
}
