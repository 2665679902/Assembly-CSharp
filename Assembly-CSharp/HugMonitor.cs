using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class HugMonitor : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>
{
	// Token: 0x0600190A RID: 6410 RVA: 0x00085CF4 File Offset: 0x00083EF4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.normal;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Update(new Action<HugMonitor.Instance, float>(this.UpdateHugEggCooldownTimer), UpdateRate.SIM_1000ms, false).ToggleBehaviour(GameTags.Creatures.WantsToTendEgg, (HugMonitor.Instance smi) => smi.UpdateHasTarget(), delegate(HugMonitor.Instance smi)
		{
			smi.hugTarget = null;
		});
		this.normal.DefaultState(this.normal.idle).ParamTransition<float>(this.hugFrenzyTimer, this.hugFrenzy, GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.IsGTZero);
		this.normal.idle.ParamTransition<float>(this.wantsHugCooldownTimer, this.normal.hugReady.seekingHug, GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.IsLTEZero).Update(new Action<HugMonitor.Instance, float>(this.UpdateWantsHugCooldownTimer), UpdateRate.SIM_1000ms, false);
		this.normal.hugReady.ToggleReactable(new Func<HugMonitor.Instance, Reactable>(this.GetHugReactable));
		this.normal.hugReady.passiveHug.ParamTransition<float>(this.wantsHugCooldownTimer, this.normal.hugReady.seekingHug, GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.IsLTEZero).Update(new Action<HugMonitor.Instance, float>(this.UpdateWantsHugCooldownTimer), UpdateRate.SIM_1000ms, false).ToggleStatusItem(CREATURES.STATUSITEMS.HUGMINIONWAITING.NAME, CREATURES.STATUSITEMS.HUGMINIONWAITING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.normal.hugReady.seekingHug.ToggleBehaviour(GameTags.Creatures.WantsAHug, (HugMonitor.Instance smi) => true, delegate(HugMonitor.Instance smi)
		{
			this.wantsHugCooldownTimer.Set(smi.def.hugFrenzyCooldownFailed, smi, false);
			smi.GoTo(this.normal.hugReady.passiveHug);
		});
		this.hugFrenzy.ParamTransition<float>(this.hugFrenzyTimer, this.normal, (HugMonitor.Instance smi, float p) => p <= 0f && !smi.IsHugging()).Update(new Action<HugMonitor.Instance, float>(this.UpdateHugFrenzyTimer), UpdateRate.SIM_1000ms, false).ToggleEffect((HugMonitor.Instance smi) => smi.frenzyEffect)
			.ToggleLoopingSound(HugMonitor.soundPath, null, true, true, true)
			.Enter(delegate(HugMonitor.Instance smi)
			{
				smi.hugParticleFx = Util.KInstantiate(EffectPrefabs.Instance.HugFrenzyFX, smi.master.transform.GetPosition() + smi.hugParticleOffset);
				smi.hugParticleFx.transform.SetParent(smi.master.transform);
				smi.hugParticleFx.SetActive(true);
			})
			.Exit(delegate(HugMonitor.Instance smi)
			{
				Util.KDestroyGameObject(smi.hugParticleFx);
				this.wantsHugCooldownTimer.Set(smi.def.hugFrenzyCooldown, smi, false);
			});
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x00085F76 File Offset: 0x00084176
	private Reactable GetHugReactable(HugMonitor.Instance smi)
	{
		return new HugMinionReactable(smi.gameObject);
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x00085F83 File Offset: 0x00084183
	private void UpdateWantsHugCooldownTimer(HugMonitor.Instance smi, float dt)
	{
		this.wantsHugCooldownTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x00085F9E File Offset: 0x0008419E
	private void UpdateHugEggCooldownTimer(HugMonitor.Instance smi, float dt)
	{
		this.hugEggCooldownTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x00085FB9 File Offset: 0x000841B9
	private void UpdateHugFrenzyTimer(HugMonitor.Instance smi, float dt)
	{
		this.hugFrenzyTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
	}

	// Token: 0x04000E02 RID: 3586
	private static string soundPath = GlobalAssets.GetSound("Squirrel_hug_frenzyFX", false);

	// Token: 0x04000E03 RID: 3587
	private StateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.FloatParameter hugFrenzyTimer;

	// Token: 0x04000E04 RID: 3588
	private StateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.FloatParameter wantsHugCooldownTimer;

	// Token: 0x04000E05 RID: 3589
	private StateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.FloatParameter hugEggCooldownTimer;

	// Token: 0x04000E06 RID: 3590
	public HugMonitor.NormalStates normal;

	// Token: 0x04000E07 RID: 3591
	public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State hugFrenzy;

	// Token: 0x020010A6 RID: 4262
	public class HUGTUNING
	{
		// Token: 0x04005852 RID: 22610
		public const float HUG_EGG_TIME = 15f;

		// Token: 0x04005853 RID: 22611
		public const float HUG_DUPE_WAIT = 60f;

		// Token: 0x04005854 RID: 22612
		public const float FRENZY_EGGS_PER_CYCLE = 6f;

		// Token: 0x04005855 RID: 22613
		public const float FRENZY_EGG_TRAVEL_TIME_BUFFER = 5f;

		// Token: 0x04005856 RID: 22614
		public const float HUG_FRENZY_DURATION = 120f;
	}

	// Token: 0x020010A7 RID: 4263
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005857 RID: 22615
		public float hugsPerCycle = 2f;

		// Token: 0x04005858 RID: 22616
		public float scanningInterval = 30f;

		// Token: 0x04005859 RID: 22617
		public float hugFrenzyDuration = 120f;

		// Token: 0x0400585A RID: 22618
		public float hugFrenzyCooldown = 480f;

		// Token: 0x0400585B RID: 22619
		public float hugFrenzyCooldownFailed = 120f;

		// Token: 0x0400585C RID: 22620
		public float scanningIntervalFrenzy = 15f;
	}

	// Token: 0x020010A8 RID: 4264
	public class HugReadyStates : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State
	{
		// Token: 0x0400585D RID: 22621
		public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State passiveHug;

		// Token: 0x0400585E RID: 22622
		public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State seekingHug;
	}

	// Token: 0x020010A9 RID: 4265
	public class NormalStates : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State
	{
		// Token: 0x0400585F RID: 22623
		public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State idle;

		// Token: 0x04005860 RID: 22624
		public HugMonitor.HugReadyStates hugReady;
	}

	// Token: 0x020010AA RID: 4266
	public new class Instance : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.GameInstance
	{
		// Token: 0x060073E4 RID: 29668 RVA: 0x002B1C9C File Offset: 0x002AFE9C
		public Instance(IStateMachineTarget master, HugMonitor.Def def)
			: base(master, def)
		{
			this.frenzyEffect = Db.Get().effects.Get("HuggingFrenzy");
			this.RefreshSearchTime();
			base.smi.sm.wantsHugCooldownTimer.Set(UnityEngine.Random.Range(base.smi.def.hugFrenzyCooldownFailed, base.smi.def.hugFrenzyCooldown), base.smi, false);
		}

		// Token: 0x060073E5 RID: 29669 RVA: 0x002B1D14 File Offset: 0x002AFF14
		private void RefreshSearchTime()
		{
			if (this.hugTarget == null)
			{
				base.smi.sm.hugEggCooldownTimer.Set(this.GetScanningInterval(), base.smi, false);
				return;
			}
			base.smi.sm.hugEggCooldownTimer.Set(this.GetHugInterval(), base.smi, false);
		}

		// Token: 0x060073E6 RID: 29670 RVA: 0x002B1D76 File Offset: 0x002AFF76
		private float GetScanningInterval()
		{
			if (!this.IsHuggingFrenzy())
			{
				return base.def.scanningInterval;
			}
			return base.def.scanningIntervalFrenzy;
		}

		// Token: 0x060073E7 RID: 29671 RVA: 0x002B1D97 File Offset: 0x002AFF97
		private float GetHugInterval()
		{
			if (this.IsHuggingFrenzy())
			{
				return 0f;
			}
			return 600f / base.def.hugsPerCycle;
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x002B1DB8 File Offset: 0x002AFFB8
		public bool IsHuggingFrenzy()
		{
			return base.smi.GetCurrentState() == base.smi.sm.hugFrenzy;
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x002B1DD7 File Offset: 0x002AFFD7
		public bool IsHugging()
		{
			return base.smi.GetSMI<AnimInterruptMonitor.Instance>().anims != null;
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x002B1DEC File Offset: 0x002AFFEC
		public bool UpdateHasTarget()
		{
			if (this.hugTarget == null)
			{
				if (base.smi.sm.hugEggCooldownTimer.Get(base.smi) > 0f)
				{
					return false;
				}
				this.FindEgg();
				this.RefreshSearchTime();
			}
			return this.hugTarget != null;
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x002B1E44 File Offset: 0x002B0044
		public void EnterHuggingFrenzy()
		{
			base.smi.sm.hugFrenzyTimer.Set(base.smi.def.hugFrenzyDuration, base.smi, false);
			base.smi.sm.hugEggCooldownTimer.Set(0f, base.smi, false);
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x002B1EA0 File Offset: 0x002B00A0
		private void FindEgg()
		{
			this.hugTarget = null;
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			ListPool<KMonoBehaviour, SquirrelHugConfig>.PooledList pooledList2 = ListPool<KMonoBehaviour, SquirrelHugConfig>.Allocate();
			Vector3 position = base.master.transform.GetPosition();
			Extents extents = new Extents(Grid.PosToCell(position), 10);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.completeBuildings, pooledList);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
			Navigator component = base.GetComponent<Navigator>();
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				KMonoBehaviour kmonoBehaviour = scenePartitionerEntry.obj as KMonoBehaviour;
				KPrefabID component2 = kmonoBehaviour.GetComponent<KPrefabID>();
				if (!component2.HasTag(GameTags.Creatures.ReservedByCreature))
				{
					int num = Grid.PosToCell(kmonoBehaviour);
					if (component.CanReach(num))
					{
						EggIncubator component3 = kmonoBehaviour.GetComponent<EggIncubator>();
						if (component3 != null)
						{
							if (component3.Occupant == null || component3.Occupant.HasTag(GameTags.Creatures.ReservedByCreature) || !component3.Occupant.HasTag(GameTags.Egg))
							{
								continue;
							}
							if (component3.Occupant.GetComponent<Effects>().HasEffect("EggHug"))
							{
								continue;
							}
						}
						else if (!component2.HasTag(GameTags.Egg) || kmonoBehaviour.GetComponent<Effects>().HasEffect("EggHug"))
						{
							continue;
						}
						pooledList2.Add(kmonoBehaviour);
					}
				}
			}
			if (pooledList2.Count > 0)
			{
				int num2 = UnityEngine.Random.Range(0, pooledList2.Count);
				KMonoBehaviour kmonoBehaviour2 = pooledList2[num2];
				this.hugTarget = kmonoBehaviour2.gameObject;
			}
			pooledList.Recycle();
			pooledList2.Recycle();
		}

		// Token: 0x04005861 RID: 22625
		public GameObject hugParticleFx;

		// Token: 0x04005862 RID: 22626
		public Vector3 hugParticleOffset;

		// Token: 0x04005863 RID: 22627
		public GameObject hugTarget;

		// Token: 0x04005864 RID: 22628
		[MyCmpGet]
		private Effects effects;

		// Token: 0x04005865 RID: 22629
		public Effect frenzyEffect;
	}
}
