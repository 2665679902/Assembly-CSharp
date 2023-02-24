using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000845 RID: 2117
public class SneezeMonitor : GameStateMachine<SneezeMonitor, SneezeMonitor.Instance>
{
	// Token: 0x06003D04 RID: 15620 RVA: 0x00154CF8 File Offset: 0x00152EF8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.ParamTransition<bool>(this.isSneezy, this.sneezy, (SneezeMonitor.Instance smi, bool p) => p);
		this.sneezy.ParamTransition<bool>(this.isSneezy, this.idle, (SneezeMonitor.Instance smi, bool p) => !p).ToggleReactable((SneezeMonitor.Instance smi) => smi.GetReactable());
	}

	// Token: 0x040027E8 RID: 10216
	public StateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.BoolParameter isSneezy = new StateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.BoolParameter(false);

	// Token: 0x040027E9 RID: 10217
	public GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x040027EA RID: 10218
	public GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.State taking_medicine;

	// Token: 0x040027EB RID: 10219
	public GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.State sneezy;

	// Token: 0x040027EC RID: 10220
	public const float SINGLE_SNEEZE_TIME_MINOR = 140f;

	// Token: 0x040027ED RID: 10221
	public const float SINGLE_SNEEZE_TIME_MAJOR = 70f;

	// Token: 0x040027EE RID: 10222
	public const float SNEEZE_TIME_VARIANCE = 0.3f;

	// Token: 0x040027EF RID: 10223
	public const float SHORT_SNEEZE_THRESHOLD = 5f;

	// Token: 0x020015E1 RID: 5601
	public new class Instance : GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085BD RID: 34237 RVA: 0x002ED064 File Offset: 0x002EB264
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.sneezyness = Db.Get().Attributes.Sneezyness.Lookup(master.gameObject);
			this.OnSneezyChange();
			AttributeInstance attributeInstance = this.sneezyness;
			attributeInstance.OnDirty = (System.Action)Delegate.Combine(attributeInstance.OnDirty, new System.Action(this.OnSneezyChange));
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x002ED0C5 File Offset: 0x002EB2C5
		public override void StopSM(string reason)
		{
			AttributeInstance attributeInstance = this.sneezyness;
			attributeInstance.OnDirty = (System.Action)Delegate.Remove(attributeInstance.OnDirty, new System.Action(this.OnSneezyChange));
			base.StopSM(reason);
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x002ED0F8 File Offset: 0x002EB2F8
		public float NextSneezeInterval()
		{
			if (this.sneezyness.GetTotalValue() <= 0f)
			{
				return 70f;
			}
			float num = (this.IsMinorSneeze() ? 140f : 70f) / this.sneezyness.GetTotalValue();
			return UnityEngine.Random.Range(num * 0.7f, num * 1.3f);
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x002ED151 File Offset: 0x002EB351
		public bool IsMinorSneeze()
		{
			return this.sneezyness.GetTotalValue() <= 5f;
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x002ED168 File Offset: 0x002EB368
		private void OnSneezyChange()
		{
			base.smi.sm.isSneezy.Set(this.sneezyness.GetTotalValue() > 0f, base.smi, false);
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x002ED19C File Offset: 0x002EB39C
		public Reactable GetReactable()
		{
			float num = this.NextSneezeInterval();
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "Sneeze", Db.Get().ChoreTypes.Cough, 0f, num, float.PositiveInfinity, 0f);
			string text = "sneeze";
			string text2 = "sneeze_pst";
			Emote emote = Db.Get().Emotes.Minion.Sneeze;
			if (this.IsMinorSneeze())
			{
				text = "sneeze_short";
				text2 = "sneeze_short_pst";
				emote = Db.Get().Emotes.Minion.Sneeze_Short;
			}
			selfEmoteReactable.SetEmote(emote);
			return selfEmoteReactable.RegisterEmoteStepCallbacks(text, new Action<GameObject>(this.TriggerDisurbance), null).RegisterEmoteStepCallbacks(text2, null, new Action<GameObject>(this.ResetSneeze));
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x002ED26B File Offset: 0x002EB46B
		private void TriggerDisurbance(GameObject go)
		{
			if (this.IsMinorSneeze())
			{
				AcousticDisturbance.Emit(go, 2);
				return;
			}
			AcousticDisturbance.Emit(go, 3);
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x002ED284 File Offset: 0x002EB484
		private void ResetSneeze(GameObject go)
		{
			base.smi.GoTo(base.sm.idle);
		}

		// Token: 0x0400682C RID: 26668
		private AttributeInstance sneezyness;

		// Token: 0x0400682D RID: 26669
		private StatusItem statusItem;
	}
}
