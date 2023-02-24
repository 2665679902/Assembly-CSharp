using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D8C RID: 3468
	public class MeteorShowerEvent : GameplayEvent<MeteorShowerEvent.StatesInstance>
	{
		// Token: 0x060069BE RID: 27070 RVA: 0x0029258C File Offset: 0x0029078C
		public MeteorShowerEvent(string id, float duration, float secondsPerMeteor, MathUtil.MinMax secondsBombardmentOff = default(MathUtil.MinMax), MathUtil.MinMax secondsBombardmentOn = default(MathUtil.MinMax))
			: base(id, 0, 0)
		{
			this.duration = duration;
			this.secondsPerMeteor = secondsPerMeteor;
			this.secondsBombardmentOff = secondsBombardmentOff;
			this.secondsBombardmentOn = secondsBombardmentOn;
			this.bombardmentInfo = new List<MeteorShowerEvent.BombardmentInfo>();
			this.tags.Add(GameTags.SpaceDanger);
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x002925E8 File Offset: 0x002907E8
		public MeteorShowerEvent AddMeteor(string prefab, float weight)
		{
			this.bombardmentInfo.Add(new MeteorShowerEvent.BombardmentInfo
			{
				prefab = prefab,
				weight = weight
			});
			return this;
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x0029261A File Offset: 0x0029081A
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new MeteorShowerEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F8C RID: 20364
		private List<MeteorShowerEvent.BombardmentInfo> bombardmentInfo;

		// Token: 0x04004F8D RID: 20365
		private MathUtil.MinMax secondsBombardmentOff;

		// Token: 0x04004F8E RID: 20366
		private MathUtil.MinMax secondsBombardmentOn;

		// Token: 0x04004F8F RID: 20367
		private float secondsPerMeteor = 0.33f;

		// Token: 0x04004F90 RID: 20368
		private float duration;

		// Token: 0x02001E61 RID: 7777
		private struct BombardmentInfo
		{
			// Token: 0x04008887 RID: 34951
			public string prefab;

			// Token: 0x04008888 RID: 34952
			public float weight;
		}

		// Token: 0x02001E62 RID: 7778
		public class States : GameplayEventStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, MeteorShowerEvent>
		{
			// Token: 0x06009B7B RID: 39803 RVA: 0x003379E4 File Offset: 0x00335BE4
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				base.InitializeStates(out default_state);
				default_state = this.planning;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.planning.Enter(delegate(MeteorShowerEvent.StatesInstance smi)
				{
					this.runTimeRemaining.Set(smi.gameplayEvent.duration, smi, false);
					this.bombardTimeRemaining.Set(smi.gameplayEvent.secondsBombardmentOn.Get(), smi, false);
					this.snoozeTimeRemaining.Set(smi.gameplayEvent.secondsBombardmentOff.Get(), smi, false);
				}).GoTo(this.running);
				this.running.DefaultState(this.running.snoozing).Update(delegate(MeteorShowerEvent.StatesInstance smi, float dt)
				{
					this.runTimeRemaining.Delta(-dt, smi);
				}, UpdateRate.SIM_200ms, false).ParamTransition<float>(this.runTimeRemaining, this.finished, GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.IsLTEZero);
				this.running.bombarding.Enter(delegate(MeteorShowerEvent.StatesInstance smi)
				{
					smi.StartBackgroundEffects();
				}).Exit(delegate(MeteorShowerEvent.StatesInstance smi)
				{
					smi.StopBackgroundEffects();
				}).Exit(delegate(MeteorShowerEvent.StatesInstance smi)
				{
					this.bombardTimeRemaining.Set(smi.gameplayEvent.secondsBombardmentOn.Get(), smi, false);
				})
					.Update(delegate(MeteorShowerEvent.StatesInstance smi, float dt)
					{
						this.bombardTimeRemaining.Delta(-dt, smi);
					}, UpdateRate.SIM_200ms, false)
					.ParamTransition<float>(this.bombardTimeRemaining, this.running.snoozing, GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.IsLTEZero)
					.Update(delegate(MeteorShowerEvent.StatesInstance smi, float dt)
					{
						smi.Bombarding(dt);
					}, UpdateRate.SIM_200ms, false);
				this.running.snoozing.Exit(delegate(MeteorShowerEvent.StatesInstance smi)
				{
					this.snoozeTimeRemaining.Set(smi.gameplayEvent.secondsBombardmentOff.Get(), smi, false);
				}).Update(delegate(MeteorShowerEvent.StatesInstance smi, float dt)
				{
					this.snoozeTimeRemaining.Delta(-dt, smi);
				}, UpdateRate.SIM_200ms, false).ParamTransition<float>(this.snoozeTimeRemaining, this.running.bombarding, GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.IsLTEZero);
				this.finished.ReturnSuccess();
			}

			// Token: 0x04008889 RID: 34953
			public GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.State planning;

			// Token: 0x0400888A RID: 34954
			public MeteorShowerEvent.States.RunningStates running;

			// Token: 0x0400888B RID: 34955
			public GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.State finished;

			// Token: 0x0400888C RID: 34956
			public StateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.FloatParameter runTimeRemaining;

			// Token: 0x0400888D RID: 34957
			public StateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.FloatParameter bombardTimeRemaining;

			// Token: 0x0400888E RID: 34958
			public StateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.FloatParameter snoozeTimeRemaining;

			// Token: 0x02002D97 RID: 11671
			public class RunningStates : GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.State
			{
				// Token: 0x0400BA1E RID: 47646
				public GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.State bombarding;

				// Token: 0x0400BA1F RID: 47647
				public GameStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, object>.State snoozing;
			}
		}

		// Token: 0x02001E63 RID: 7779
		public class StatesInstance : GameplayEventStateMachine<MeteorShowerEvent.States, MeteorShowerEvent.StatesInstance, GameplayEventManager, MeteorShowerEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009B83 RID: 39811 RVA: 0x00337C5C File Offset: 0x00335E5C
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, MeteorShowerEvent meteorShowerEvent)
				: base(master, eventInstance, meteorShowerEvent)
			{
				this.timeRemaining = this.gameplayEvent.duration;
				this.timeBetweenMeteors = this.gameplayEvent.secondsPerMeteor;
				this.m_worldId = eventInstance.worldId;
				Game.Instance.Subscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
			}

			// Token: 0x06009B84 RID: 39812 RVA: 0x00337CBC File Offset: 0x00335EBC
			private void OnActiveWorldChanged(object data)
			{
				int first = ((global::Tuple<int, int>)data).first;
				if (this.activeMeteorBackground != null)
				{
					this.activeMeteorBackground.GetComponent<ParticleSystemRenderer>().enabled = first == this.m_worldId;
				}
			}

			// Token: 0x06009B85 RID: 39813 RVA: 0x00337CFC File Offset: 0x00335EFC
			public override void StopSM(string reason)
			{
				this.StopBackgroundEffects();
				base.StopSM(reason);
			}

			// Token: 0x06009B86 RID: 39814 RVA: 0x00337D0B File Offset: 0x00335F0B
			protected override void OnCleanUp()
			{
				Game.Instance.Unsubscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
				base.OnCleanUp();
			}

			// Token: 0x06009B87 RID: 39815 RVA: 0x00337D30 File Offset: 0x00335F30
			public void StartBackgroundEffects()
			{
				if (this.activeMeteorBackground == null)
				{
					this.activeMeteorBackground = Util.KInstantiate(EffectPrefabs.Instance.MeteorBackground, null, null);
					WorldContainer world = ClusterManager.Instance.GetWorld(this.m_worldId);
					float num = (world.maximumBounds.x + world.minimumBounds.x) / 2f;
					float y = world.maximumBounds.y;
					float num2 = 25f;
					this.activeMeteorBackground.transform.SetPosition(new Vector3(num, y, num2));
					this.activeMeteorBackground.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
				}
			}

			// Token: 0x06009B88 RID: 39816 RVA: 0x00337DE4 File Offset: 0x00335FE4
			public void StopBackgroundEffects()
			{
				if (this.activeMeteorBackground != null)
				{
					ParticleSystem component = this.activeMeteorBackground.GetComponent<ParticleSystem>();
					component.main.stopAction = ParticleSystemStopAction.Destroy;
					component.Stop();
					if (!component.IsAlive())
					{
						UnityEngine.Object.Destroy(this.activeMeteorBackground);
					}
					this.activeMeteorBackground = null;
				}
			}

			// Token: 0x06009B89 RID: 39817 RVA: 0x00337E38 File Offset: 0x00336038
			public float TimeUntilNextShower()
			{
				if (base.IsInsideState(base.sm.running.bombarding))
				{
					return 0f;
				}
				return base.sm.snoozeTimeRemaining.Get(this);
			}

			// Token: 0x06009B8A RID: 39818 RVA: 0x00337E6C File Offset: 0x0033606C
			public void Bombarding(float dt)
			{
				this.nextMeteorTime -= dt;
				while (this.nextMeteorTime < 0f)
				{
					this.DoBombardment(this.gameplayEvent.bombardmentInfo);
					this.nextMeteorTime += this.timeBetweenMeteors;
				}
			}

			// Token: 0x06009B8B RID: 39819 RVA: 0x00337EBC File Offset: 0x003360BC
			private void DoBombardment(List<MeteorShowerEvent.BombardmentInfo> bombardment_info)
			{
				float num = 0f;
				foreach (MeteorShowerEvent.BombardmentInfo bombardmentInfo in bombardment_info)
				{
					num += bombardmentInfo.weight;
				}
				num = UnityEngine.Random.Range(0f, num);
				MeteorShowerEvent.BombardmentInfo bombardmentInfo2 = bombardment_info[0];
				int num2 = 0;
				while (num - bombardmentInfo2.weight > 0f)
				{
					num -= bombardmentInfo2.weight;
					bombardmentInfo2 = bombardment_info[++num2];
				}
				Game.Instance.Trigger(-84771526, null);
				this.SpawnBombard(bombardmentInfo2.prefab);
			}

			// Token: 0x06009B8C RID: 39820 RVA: 0x00337F70 File Offset: 0x00336170
			private GameObject SpawnBombard(string prefab)
			{
				WorldContainer world = ClusterManager.Instance.GetWorld(this.m_worldId);
				float num = (float)world.Width * UnityEngine.Random.value + (float)world.WorldOffset.x;
				float num2 = (float)(world.Height + world.WorldOffset.y - 1);
				float layerZ = Grid.GetLayerZ(Grid.SceneLayer.FXFront);
				Vector3 vector = new Vector3(num, num2, layerZ);
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(prefab), vector, Quaternion.identity, null, null, true, 0);
				gameObject.SetActive(true);
				return gameObject;
			}

			// Token: 0x0400888F RID: 34959
			public GameObject activeMeteorBackground;

			// Token: 0x04008890 RID: 34960
			[Serialize]
			private float nextMeteorTime;

			// Token: 0x04008891 RID: 34961
			[Serialize]
			private float timeRemaining;

			// Token: 0x04008892 RID: 34962
			[Serialize]
			private float timeBetweenMeteors;

			// Token: 0x04008893 RID: 34963
			[Serialize]
			private int m_worldId;
		}
	}
}
