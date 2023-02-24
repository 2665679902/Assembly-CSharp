using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D7A RID: 3450
	public class SlimeSickness : Sickness
	{
		// Token: 0x06006962 RID: 26978 RVA: 0x0028FDE8 File Offset: 0x0028DFE8
		public SlimeSickness()
			: base("SlimeSickness", Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector> { Sickness.InfectionVector.Inhalation }, 2220f, "SlimeSicknessRecovery")
		{
			base.AddSicknessComponent(new CommonSickEffectSickness());
			base.AddSicknessComponent(new AttributeModifierSickness(new AttributeModifier[]
			{
				new AttributeModifier("BreathDelta", -1.1363636f, DUPLICANTS.DISEASES.SLIMESICKNESS.NAME, false, false, true),
				new AttributeModifier("Athletics", -3f, DUPLICANTS.DISEASES.SLIMESICKNESS.NAME, false, false, true)
			}));
			base.AddSicknessComponent(new AnimatedSickness(new HashedString[] { "anim_idle_sick_kanim" }, Db.Get().Expressions.Sick));
			base.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 50f));
			base.AddSicknessComponent(new SlimeSickness.SlimeLungComponent());
		}

		// Token: 0x04004F3C RID: 20284
		private const float COUGH_FREQUENCY = 20f;

		// Token: 0x04004F3D RID: 20285
		private const float COUGH_MASS = 0.1f;

		// Token: 0x04004F3E RID: 20286
		private const int DISEASE_AMOUNT = 1000;

		// Token: 0x04004F3F RID: 20287
		public const string ID = "SlimeSickness";

		// Token: 0x04004F40 RID: 20288
		public const string RECOVERY_ID = "SlimeSicknessRecovery";

		// Token: 0x02001E4E RID: 7758
		public class SlimeLungComponent : Sickness.SicknessComponent
		{
			// Token: 0x06009B3F RID: 39743 RVA: 0x00336718 File Offset: 0x00334918
			public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
			{
				SlimeSickness.SlimeLungComponent.StatesInstance statesInstance = new SlimeSickness.SlimeLungComponent.StatesInstance(diseaseInstance);
				statesInstance.StartSM();
				return statesInstance;
			}

			// Token: 0x06009B40 RID: 39744 RVA: 0x00336726 File Offset: 0x00334926
			public override void OnCure(GameObject go, object instance_data)
			{
				((SlimeSickness.SlimeLungComponent.StatesInstance)instance_data).StopSM("Cured");
			}

			// Token: 0x06009B41 RID: 39745 RVA: 0x00336738 File Offset: 0x00334938
			public override List<Descriptor> GetSymptoms()
			{
				return new List<Descriptor>
				{
					new Descriptor(DUPLICANTS.DISEASES.SLIMESICKNESS.COUGH_SYMPTOM, DUPLICANTS.DISEASES.SLIMESICKNESS.COUGH_SYMPTOM_TOOLTIP, Descriptor.DescriptorType.SymptomAidable, false)
				};
			}

			// Token: 0x02002D8A RID: 11658
			public class StatesInstance : GameStateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance, object>.GameInstance
			{
				// Token: 0x0600BE56 RID: 48726 RVA: 0x00390C50 File Offset: 0x0038EE50
				public StatesInstance(SicknessInstance master)
					: base(master)
				{
				}

				// Token: 0x0600BE57 RID: 48727 RVA: 0x00390C5C File Offset: 0x0038EE5C
				public Reactable GetReactable()
				{
					Emote cough = Db.Get().Emotes.Minion.Cough;
					SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "SlimeLungCough", Db.Get().ChoreTypes.Cough, 0f, 0f, float.PositiveInfinity, 0f);
					selfEmoteReactable.SetEmote(cough);
					selfEmoteReactable.RegisterEmoteStepCallbacks("react", null, new Action<GameObject>(this.FinishedCoughing));
					return selfEmoteReactable;
				}

				// Token: 0x0600BE58 RID: 48728 RVA: 0x00390CE4 File Offset: 0x0038EEE4
				private void ProduceSlime(GameObject cougher)
				{
					AmountInstance amountInstance = Db.Get().Amounts.Temperature.Lookup(cougher);
					int num = Grid.PosToCell(cougher);
					string id = Db.Get().Diseases.SlimeGerms.Id;
					Equippable equippable = base.master.gameObject.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
					if (equippable != null)
					{
						equippable.GetComponent<Storage>().AddGasChunk(SimHashes.ContaminatedOxygen, 0.1f, amountInstance.value, Db.Get().Diseases.GetIndex(id), 1000, false, true);
					}
					else
					{
						SimMessages.AddRemoveSubstance(num, SimHashes.ContaminatedOxygen, CellEventLogger.Instance.Cough, 0.1f, amountInstance.value, Db.Get().Diseases.GetIndex(id), 1000, true, -1);
					}
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(DUPLICANTS.DISEASES.ADDED_POPFX, base.master.modifier.Name, 1000), cougher.transform, 1.5f, false);
				}

				// Token: 0x0600BE59 RID: 48729 RVA: 0x00390E02 File Offset: 0x0038F002
				private void FinishedCoughing(GameObject cougher)
				{
					this.ProduceSlime(cougher);
					base.sm.coughFinished.Trigger(this);
				}

				// Token: 0x0400B9FA RID: 47610
				public float lastCoughTime;
			}

			// Token: 0x02002D8B RID: 11659
			public class States : GameStateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance>
			{
				// Token: 0x0600BE5A RID: 48730 RVA: 0x00390E1C File Offset: 0x0038F01C
				public override void InitializeStates(out StateMachine.BaseState default_state)
				{
					default_state = this.breathing;
					this.breathing.DefaultState(this.breathing.normal).TagTransition(GameTags.NoOxygen, this.notbreathing, false);
					this.breathing.normal.Enter("SetCoughTime", delegate(SlimeSickness.SlimeLungComponent.StatesInstance smi)
					{
						if (smi.lastCoughTime < Time.time)
						{
							smi.lastCoughTime = Time.time;
						}
					}).Update("Cough", delegate(SlimeSickness.SlimeLungComponent.StatesInstance smi, float dt)
					{
						if (!smi.master.IsDoctored && Time.time - smi.lastCoughTime > 20f)
						{
							smi.GoTo(this.breathing.cough);
						}
					}, UpdateRate.SIM_4000ms, false);
					this.breathing.cough.ToggleReactable((SlimeSickness.SlimeLungComponent.StatesInstance smi) => smi.GetReactable()).OnSignal(this.coughFinished, this.breathing.normal);
					this.notbreathing.TagTransition(new Tag[] { GameTags.NoOxygen }, this.breathing, true);
				}

				// Token: 0x0400B9FB RID: 47611
				public StateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance, object>.Signal coughFinished;

				// Token: 0x0400B9FC RID: 47612
				public SlimeSickness.SlimeLungComponent.States.BreathingStates breathing;

				// Token: 0x0400B9FD RID: 47613
				public GameStateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance, object>.State notbreathing;

				// Token: 0x02002FE3 RID: 12259
				public class BreathingStates : GameStateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance, object>.State
				{
					// Token: 0x0400C044 RID: 49220
					public GameStateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance, object>.State normal;

					// Token: 0x0400C045 RID: 49221
					public GameStateMachine<SlimeSickness.SlimeLungComponent.States, SlimeSickness.SlimeLungComponent.StatesInstance, SicknessInstance, object>.State cough;
				}
			}
		}
	}
}
