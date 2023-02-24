using System;
using Database;
using KSerialization;
using TUNING;

// Token: 0x02000755 RID: 1877
public class EquippableBalloon : StateMachineComponent<EquippableBalloon.StatesInstance>
{
	// Token: 0x060033AD RID: 13229 RVA: 0x0011667B File Offset: 0x0011487B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.smi.transitionTime = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.JOY_REACTION_DURATION;
	}

	// Token: 0x060033AE RID: 13230 RVA: 0x0011669E File Offset: 0x0011489E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.ApplyBalloonOverrideToBalloonFx();
	}

	// Token: 0x060033AF RID: 13231 RVA: 0x001166B7 File Offset: 0x001148B7
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x060033B0 RID: 13232 RVA: 0x001166BF File Offset: 0x001148BF
	public void SetBalloonOverride(BalloonOverrideSymbol balloonOverride)
	{
		base.smi.facadeAnim = balloonOverride.animFileID;
		base.smi.symbolID = balloonOverride.animFileSymbolID;
		this.ApplyBalloonOverrideToBalloonFx();
	}

	// Token: 0x060033B1 RID: 13233 RVA: 0x001166EC File Offset: 0x001148EC
	public void ApplyBalloonOverrideToBalloonFx()
	{
		Equippable component = base.GetComponent<Equippable>();
		if (!component.IsNullOrDestroyed() && !component.assignee.IsNullOrDestroyed())
		{
			Ownables soleOwner = component.assignee.GetSoleOwner();
			if (soleOwner.IsNullOrDestroyed())
			{
				return;
			}
			BalloonFX.Instance smi = ((KMonoBehaviour)soleOwner.GetComponent<MinionAssignablesProxy>().target).GetSMI<BalloonFX.Instance>();
			if (!smi.IsNullOrDestroyed())
			{
				new BalloonOverrideSymbol(base.smi.facadeAnim, base.smi.symbolID).ApplyTo(smi);
			}
		}
	}

	// Token: 0x02001456 RID: 5206
	public class StatesInstance : GameStateMachine<EquippableBalloon.States, EquippableBalloon.StatesInstance, EquippableBalloon, object>.GameInstance
	{
		// Token: 0x060080D3 RID: 32979 RVA: 0x002DFDF2 File Offset: 0x002DDFF2
		public StatesInstance(EquippableBalloon master)
			: base(master)
		{
		}

		// Token: 0x04006326 RID: 25382
		[Serialize]
		public float transitionTime;

		// Token: 0x04006327 RID: 25383
		[Serialize]
		public string facadeAnim;

		// Token: 0x04006328 RID: 25384
		[Serialize]
		public string symbolID;
	}

	// Token: 0x02001457 RID: 5207
	public class States : GameStateMachine<EquippableBalloon.States, EquippableBalloon.StatesInstance, EquippableBalloon>
	{
		// Token: 0x060080D4 RID: 32980 RVA: 0x002DFDFC File Offset: 0x002DDFFC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Transition(this.destroy, (EquippableBalloon.StatesInstance smi) => GameClock.Instance.GetTime() >= smi.transitionTime, UpdateRate.SIM_200ms);
			this.destroy.Enter(delegate(EquippableBalloon.StatesInstance smi)
			{
				smi.master.GetComponent<Equippable>().Unassign();
			});
		}

		// Token: 0x04006329 RID: 25385
		public GameStateMachine<EquippableBalloon.States, EquippableBalloon.StatesInstance, EquippableBalloon, object>.State destroy;
	}
}
