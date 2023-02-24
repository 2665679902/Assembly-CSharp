using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
[SkipSaveFileSerialization]
public class ReceptacleMonitor : StateMachineComponent<ReceptacleMonitor.StatesInstance>, IGameObjectEffectDescriptor, IWiltCause, ISim1000ms
{
	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06003010 RID: 12304 RVA: 0x000FE197 File Offset: 0x000FC397
	public bool Replanted
	{
		get
		{
			return this.replanted;
		}
	}

	// Token: 0x06003011 RID: 12305 RVA: 0x000FE19F File Offset: 0x000FC39F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003012 RID: 12306 RVA: 0x000FE1B2 File Offset: 0x000FC3B2
	public PlantablePlot GetReceptacle()
	{
		return (PlantablePlot)base.smi.sm.receptacle.Get(base.smi);
	}

	// Token: 0x06003013 RID: 12307 RVA: 0x000FE1D4 File Offset: 0x000FC3D4
	public void SetReceptacle(PlantablePlot plot = null)
	{
		if (plot == null)
		{
			base.smi.sm.receptacle.Set(null, base.smi, false);
			this.replanted = false;
			return;
		}
		base.smi.sm.receptacle.Set(plot, base.smi, false);
		this.replanted = true;
	}

	// Token: 0x06003014 RID: 12308 RVA: 0x000FE238 File Offset: 0x000FC438
	public void Sim1000ms(float dt)
	{
		if (base.smi.sm.receptacle.Get(base.smi) == null)
		{
			base.smi.GoTo(base.smi.sm.wild);
			return;
		}
		Operational component = base.smi.sm.receptacle.Get(base.smi).GetComponent<Operational>();
		if (component == null)
		{
			base.smi.GoTo(base.smi.sm.operational);
			return;
		}
		if (component.IsOperational)
		{
			base.smi.GoTo(base.smi.sm.operational);
			return;
		}
		base.smi.GoTo(base.smi.sm.inoperational);
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06003015 RID: 12309 RVA: 0x000FE309 File Offset: 0x000FC509
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.Receptacle };
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06003016 RID: 12310 RVA: 0x000FE318 File Offset: 0x000FC518
	public string WiltStateString
	{
		get
		{
			string text = "";
			if (base.smi.IsInsideState(base.smi.sm.inoperational))
			{
				text += CREATURES.STATUSITEMS.RECEPTACLEINOPERATIONAL.NAME;
			}
			return text;
		}
	}

	// Token: 0x06003017 RID: 12311 RVA: 0x000FE35A File Offset: 0x000FC55A
	public bool HasReceptacle()
	{
		return !base.smi.IsInsideState(base.smi.sm.wild);
	}

	// Token: 0x06003018 RID: 12312 RVA: 0x000FE37A File Offset: 0x000FC57A
	public bool HasOperationalReceptacle()
	{
		return base.smi.IsInsideState(base.smi.sm.operational);
	}

	// Token: 0x06003019 RID: 12313 RVA: 0x000FE397 File Offset: 0x000FC597
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_RECEPTACLE, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_RECEPTACLE, Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x04001D02 RID: 7426
	private bool replanted;

	// Token: 0x020013F8 RID: 5112
	public class StatesInstance : GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.GameInstance
	{
		// Token: 0x06007FB9 RID: 32697 RVA: 0x002DDA66 File Offset: 0x002DBC66
		public StatesInstance(ReceptacleMonitor master)
			: base(master)
		{
		}
	}

	// Token: 0x020013F9 RID: 5113
	public class States : GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor>
	{
		// Token: 0x06007FBA RID: 32698 RVA: 0x002DDA70 File Offset: 0x002DBC70
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.wild;
			base.serializable = StateMachine.SerializeType.Never;
			this.wild.TriggerOnEnter(GameHashes.ReceptacleOperational, null);
			this.inoperational.TriggerOnEnter(GameHashes.ReceptacleInoperational, null);
			this.operational.TriggerOnEnter(GameHashes.ReceptacleOperational, null);
		}

		// Token: 0x04006228 RID: 25128
		public StateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.ObjectParameter<SingleEntityReceptacle> receptacle;

		// Token: 0x04006229 RID: 25129
		public GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.State wild;

		// Token: 0x0400622A RID: 25130
		public GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.State inoperational;

		// Token: 0x0400622B RID: 25131
		public GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.State operational;
	}
}
