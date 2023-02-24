using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200060A RID: 1546
[AddComponentMenu("KMonoBehaviour/Workable/MessStation")]
public class MessStation : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x06002851 RID: 10321 RVA: 0x000D5EA9 File Offset: 0x000D40A9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_machine_kanim") };
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x000D5ECF File Offset: 0x000D40CF
	protected override void OnCompleteWork(Worker worker)
	{
		worker.workable.GetComponent<Edible>().CompleteWork(worker);
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x000D5EE2 File Offset: 0x000D40E2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new MessStation.MessStationSM.Instance(this);
		this.smi.StartSM();
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x000D5F04 File Offset: 0x000D4104
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (go.GetComponent<Storage>().Has(TableSaltConfig.ID.ToTag()))
		{
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.MESS_TABLE_SALT, TableSaltTuning.MORALE_MODIFIER), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.MESS_TABLE_SALT, TableSaltTuning.MORALE_MODIFIER), Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06002855 RID: 10325 RVA: 0x000D5F6E File Offset: 0x000D416E
	public bool HasSalt
	{
		get
		{
			return this.smi.HasSalt;
		}
	}

	// Token: 0x040017AF RID: 6063
	private MessStation.MessStationSM.Instance smi;

	// Token: 0x02001276 RID: 4726
	public class MessStationSM : GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation>
	{
		// Token: 0x06007A6F RID: 31343 RVA: 0x002C8048 File Offset: 0x002C6248
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.salt.none;
			this.salt.none.Transition(this.salt.salty, (MessStation.MessStationSM.Instance smi) => smi.HasSalt, UpdateRate.SIM_200ms).PlayAnim("off");
			this.salt.salty.Transition(this.salt.none, (MessStation.MessStationSM.Instance smi) => !smi.HasSalt, UpdateRate.SIM_200ms).PlayAnim("salt").EventTransition(GameHashes.EatStart, this.eating, null);
			this.eating.Transition(this.salt.salty, (MessStation.MessStationSM.Instance smi) => smi.HasSalt && !smi.IsEating(), UpdateRate.SIM_200ms).Transition(this.salt.none, (MessStation.MessStationSM.Instance smi) => !smi.HasSalt && !smi.IsEating(), UpdateRate.SIM_200ms).PlayAnim("off");
		}

		// Token: 0x04005DEA RID: 24042
		public MessStation.MessStationSM.SaltState salt;

		// Token: 0x04005DEB RID: 24043
		public GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State eating;

		// Token: 0x02001FD4 RID: 8148
		public class SaltState : GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State
		{
			// Token: 0x04008DCC RID: 36300
			public GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State none;

			// Token: 0x04008DCD RID: 36301
			public GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State salty;
		}

		// Token: 0x02001FD5 RID: 8149
		public new class Instance : GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.GameInstance
		{
			// Token: 0x0600A108 RID: 41224 RVA: 0x003426BF File Offset: 0x003408BF
			public Instance(MessStation master)
				: base(master)
			{
				this.saltStorage = master.GetComponent<Storage>();
				this.assigned = master.GetComponent<Assignable>();
			}

			// Token: 0x170009FF RID: 2559
			// (get) Token: 0x0600A109 RID: 41225 RVA: 0x003426E0 File Offset: 0x003408E0
			public bool HasSalt
			{
				get
				{
					return this.saltStorage.Has(TableSaltConfig.ID.ToTag());
				}
			}

			// Token: 0x0600A10A RID: 41226 RVA: 0x003426F8 File Offset: 0x003408F8
			public bool IsEating()
			{
				if (this.assigned == null || this.assigned.assignee == null)
				{
					return false;
				}
				Ownables soleOwner = this.assigned.assignee.GetSoleOwner();
				if (soleOwner == null)
				{
					return false;
				}
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject == null)
				{
					return false;
				}
				ChoreDriver component = targetGameObject.GetComponent<ChoreDriver>();
				return component != null && component.HasChore() && component.GetCurrentChore().choreType.urge == Db.Get().Urges.Eat;
			}

			// Token: 0x04008DCE RID: 36302
			private Storage saltStorage;

			// Token: 0x04008DCF RID: 36303
			private Assignable assigned;
		}
	}
}
