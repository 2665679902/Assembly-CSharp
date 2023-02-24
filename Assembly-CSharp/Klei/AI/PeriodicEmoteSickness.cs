using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D82 RID: 3458
	public class PeriodicEmoteSickness : Sickness.SicknessComponent
	{
		// Token: 0x06006975 RID: 26997 RVA: 0x00290A68 File Offset: 0x0028EC68
		public PeriodicEmoteSickness(Emote emote, float cooldown)
		{
			this.emote = emote;
			this.cooldown = cooldown;
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x00290A7E File Offset: 0x0028EC7E
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			PeriodicEmoteSickness.StatesInstance statesInstance = new PeriodicEmoteSickness.StatesInstance(diseaseInstance, this);
			statesInstance.StartSM();
			return statesInstance;
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x00290A8D File Offset: 0x0028EC8D
		public override void OnCure(GameObject go, object instance_data)
		{
			((PeriodicEmoteSickness.StatesInstance)instance_data).StopSM("Cured");
		}

		// Token: 0x04004F4B RID: 20299
		private Emote emote;

		// Token: 0x04004F4C RID: 20300
		private float cooldown;

		// Token: 0x02001E4F RID: 7759
		public class StatesInstance : GameStateMachine<PeriodicEmoteSickness.States, PeriodicEmoteSickness.StatesInstance, SicknessInstance, object>.GameInstance
		{
			// Token: 0x06009B43 RID: 39747 RVA: 0x00336768 File Offset: 0x00334968
			public StatesInstance(SicknessInstance master, PeriodicEmoteSickness periodicEmoteSickness)
				: base(master)
			{
				this.periodicEmoteSickness = periodicEmoteSickness;
			}

			// Token: 0x06009B44 RID: 39748 RVA: 0x00336778 File Offset: 0x00334978
			public Reactable GetReactable()
			{
				return new SelfEmoteReactable(base.master.gameObject, "PeriodicEmoteSickness", Db.Get().ChoreTypes.Emote, 0f, this.periodicEmoteSickness.cooldown, float.PositiveInfinity, 0f).SetEmote(this.periodicEmoteSickness.emote).SetOverideAnimSet("anim_sneeze_kanim");
			}

			// Token: 0x04008853 RID: 34899
			public PeriodicEmoteSickness periodicEmoteSickness;
		}

		// Token: 0x02001E50 RID: 7760
		public class States : GameStateMachine<PeriodicEmoteSickness.States, PeriodicEmoteSickness.StatesInstance, SicknessInstance>
		{
			// Token: 0x06009B45 RID: 39749 RVA: 0x003367E2 File Offset: 0x003349E2
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.root;
				this.root.ToggleReactable((PeriodicEmoteSickness.StatesInstance smi) => smi.GetReactable());
			}
		}
	}
}
