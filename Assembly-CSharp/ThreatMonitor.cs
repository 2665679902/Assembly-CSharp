using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200084E RID: 2126
public class ThreatMonitor : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>
{
	// Token: 0x06003D2C RID: 15660 RVA: 0x0015601C File Offset: 0x0015421C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.safe;
		this.root.EventHandler(GameHashes.SafeFromThreats, delegate(ThreatMonitor.Instance smi, object d)
		{
			smi.OnSafe(d);
		}).EventHandler(GameHashes.Attacked, delegate(ThreatMonitor.Instance smi, object d)
		{
			smi.OnAttacked(d);
		}).EventHandler(GameHashes.ObjectDestroyed, delegate(ThreatMonitor.Instance smi, object d)
		{
			smi.Cleanup(d);
		});
		this.safe.Enter(delegate(ThreatMonitor.Instance smi)
		{
			smi.revengeThreat.Clear();
			smi.RefreshThreat(null);
		}).Update("safe", delegate(ThreatMonitor.Instance smi, float dt)
		{
			smi.RefreshThreat(null);
		}, UpdateRate.SIM_1000ms, true);
		this.threatened.duplicant.Transition(this.safe, (ThreatMonitor.Instance smi) => !smi.CheckForThreats(), UpdateRate.SIM_200ms);
		this.threatened.duplicant.ShouldFight.ToggleChore(new Func<ThreatMonitor.Instance, Chore>(this.CreateAttackChore), this.safe).Update("DupeUpdateTarget", new Action<ThreatMonitor.Instance, float>(ThreatMonitor.DupeUpdateTarget), UpdateRate.SIM_200ms, false);
		this.threatened.duplicant.ShoudFlee.ToggleChore(new Func<ThreatMonitor.Instance, Chore>(this.CreateFleeChore), this.safe);
		this.threatened.creature.ToggleBehaviour(GameTags.Creatures.Flee, (ThreatMonitor.Instance smi) => !smi.WillFight(), delegate(ThreatMonitor.Instance smi)
		{
			smi.GoTo(this.safe);
		}).ToggleBehaviour(GameTags.Creatures.Attack, (ThreatMonitor.Instance smi) => smi.WillFight(), delegate(ThreatMonitor.Instance smi)
		{
			smi.GoTo(this.safe);
		}).Update("Threatened", new Action<ThreatMonitor.Instance, float>(ThreatMonitor.CritterUpdateThreats), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x06003D2D RID: 15661 RVA: 0x00156236 File Offset: 0x00154436
	private static void DupeUpdateTarget(ThreatMonitor.Instance smi, float dt)
	{
		if (smi.MainThreat == null || !smi.MainThreat.GetComponent<FactionAlignment>().IsPlayerTargeted())
		{
			smi.Trigger(2144432245, null);
		}
	}

	// Token: 0x06003D2E RID: 15662 RVA: 0x00156264 File Offset: 0x00154464
	private static void CritterUpdateThreats(ThreatMonitor.Instance smi, float dt)
	{
		if (smi.isMasterNull)
		{
			return;
		}
		if (smi.revengeThreat.target != null && smi.revengeThreat.Calm(dt, smi.alignment))
		{
			smi.Trigger(-21431934, null);
			return;
		}
		if (!smi.CheckForThreats())
		{
			smi.GoTo(smi.sm.safe);
		}
	}

	// Token: 0x06003D2F RID: 15663 RVA: 0x001562C7 File Offset: 0x001544C7
	private Chore CreateAttackChore(ThreatMonitor.Instance smi)
	{
		return new AttackChore(smi.master, smi.MainThreat);
	}

	// Token: 0x06003D30 RID: 15664 RVA: 0x001562DA File Offset: 0x001544DA
	private Chore CreateFleeChore(ThreatMonitor.Instance smi)
	{
		return new FleeChore(smi.master, smi.MainThreat);
	}

	// Token: 0x04002814 RID: 10260
	private FactionAlignment alignment;

	// Token: 0x04002815 RID: 10261
	private Navigator navigator;

	// Token: 0x04002816 RID: 10262
	public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State safe;

	// Token: 0x04002817 RID: 10263
	public ThreatMonitor.ThreatenedStates threatened;

	// Token: 0x020015F8 RID: 5624
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006886 RID: 26758
		public Health.HealthState fleethresholdState = Health.HealthState.Injured;

		// Token: 0x04006887 RID: 26759
		public Tag[] friendlyCreatureTags;
	}

	// Token: 0x020015F9 RID: 5625
	public class ThreatenedStates : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State
	{
		// Token: 0x04006888 RID: 26760
		public ThreatMonitor.ThreatenedDuplicantStates duplicant;

		// Token: 0x04006889 RID: 26761
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State creature;
	}

	// Token: 0x020015FA RID: 5626
	public class ThreatenedDuplicantStates : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State
	{
		// Token: 0x0400688A RID: 26762
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State ShoudFlee;

		// Token: 0x0400688B RID: 26763
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State ShouldFight;
	}

	// Token: 0x020015FB RID: 5627
	public struct Grudge
	{
		// Token: 0x0600862D RID: 34349 RVA: 0x002EE170 File Offset: 0x002EC370
		public void Reset(FactionAlignment revengeTarget)
		{
			this.target = revengeTarget;
			float num = 10f;
			this.grudgeTime = num;
		}

		// Token: 0x0600862E RID: 34350 RVA: 0x002EE194 File Offset: 0x002EC394
		public bool Calm(float dt, FactionAlignment self)
		{
			if (this.grudgeTime <= 0f)
			{
				return true;
			}
			this.grudgeTime = Mathf.Max(0f, this.grudgeTime - dt);
			if (this.grudgeTime == 0f)
			{
				if (FactionManager.Instance.GetDisposition(self.Alignment, this.target.Alignment) != FactionManager.Disposition.Attack)
				{
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, UI.GAMEOBJECTEFFECTS.FORGAVEATTACKER, self.transform, 2f, true);
				}
				this.Clear();
				return true;
			}
			return false;
		}

		// Token: 0x0600862F RID: 34351 RVA: 0x002EE227 File Offset: 0x002EC427
		public void Clear()
		{
			this.grudgeTime = 0f;
			this.target = null;
		}

		// Token: 0x06008630 RID: 34352 RVA: 0x002EE23C File Offset: 0x002EC43C
		public bool IsValidRevengeTarget(bool isDuplicant)
		{
			return this.target != null && this.target.IsAlignmentActive() && (this.target.health == null || !this.target.health.IsDefeated()) && (!isDuplicant || !this.target.IsPlayerTargeted());
		}

		// Token: 0x0400688C RID: 26764
		public FactionAlignment target;

		// Token: 0x0400688D RID: 26765
		public float grudgeTime;
	}

	// Token: 0x020015FC RID: 5628
	public new class Instance : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.GameInstance
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06008631 RID: 34353 RVA: 0x002EE29E File Offset: 0x002EC49E
		public GameObject MainThreat
		{
			get
			{
				return this.mainThreat;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06008632 RID: 34354 RVA: 0x002EE2A6 File Offset: 0x002EC4A6
		public bool IAmADuplicant
		{
			get
			{
				return this.alignment.Alignment == FactionManager.FactionID.Duplicant;
			}
		}

		// Token: 0x06008633 RID: 34355 RVA: 0x002EE2B8 File Offset: 0x002EC4B8
		public Instance(IStateMachineTarget master, ThreatMonitor.Def def)
			: base(master, def)
		{
			this.alignment = master.GetComponent<FactionAlignment>();
			this.navigator = master.GetComponent<Navigator>();
			this.choreDriver = master.GetComponent<ChoreDriver>();
			this.health = master.GetComponent<Health>();
			this.choreConsumer = master.GetComponent<ChoreConsumer>();
			this.refreshThreatDelegate = new Action<object>(this.RefreshThreat);
		}

		// Token: 0x06008634 RID: 34356 RVA: 0x002EE326 File Offset: 0x002EC526
		public void ClearMainThreat()
		{
			this.SetMainThreat(null);
		}

		// Token: 0x06008635 RID: 34357 RVA: 0x002EE330 File Offset: 0x002EC530
		public void SetMainThreat(GameObject threat)
		{
			if (threat == this.mainThreat)
			{
				return;
			}
			if (this.mainThreat != null)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
				if (threat == null)
				{
					base.Trigger(2144432245, null);
				}
			}
			if (this.mainThreat != null)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
			}
			this.mainThreat = threat;
			if (this.mainThreat != null)
			{
				this.mainThreat.Subscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Subscribe(1969584890, this.refreshThreatDelegate);
			}
		}

		// Token: 0x06008636 RID: 34358 RVA: 0x002EE418 File Offset: 0x002EC618
		public void OnSafe(object data)
		{
			if (this.revengeThreat.target != null)
			{
				if (!this.revengeThreat.target.GetComponent<FactionAlignment>().IsAlignmentActive())
				{
					this.revengeThreat.Clear();
				}
				this.ClearMainThreat();
			}
		}

		// Token: 0x06008637 RID: 34359 RVA: 0x002EE458 File Offset: 0x002EC658
		public void OnAttacked(object data)
		{
			FactionAlignment factionAlignment = (FactionAlignment)data;
			this.revengeThreat.Reset(factionAlignment);
			if (this.mainThreat == null)
			{
				this.SetMainThreat(factionAlignment.gameObject);
				this.GoToThreatened();
			}
			else if (!this.WillFight())
			{
				this.GoToThreatened();
			}
			if (factionAlignment.GetComponent<Bee>())
			{
				Chore chore = ((this.choreDriver != null) ? this.choreDriver.GetCurrentChore() : null);
				if (chore != null && chore.gameObject.GetComponent<HiveWorkableEmpty>() != null)
				{
					chore.gameObject.GetComponent<HiveWorkableEmpty>().wasStung = true;
				}
			}
		}

		// Token: 0x06008638 RID: 34360 RVA: 0x002EE4FC File Offset: 0x002EC6FC
		public bool WillFight()
		{
			if (this.choreConsumer != null)
			{
				if (!this.choreConsumer.IsPermittedByUser(Db.Get().ChoreGroups.Combat))
				{
					return false;
				}
				if (!this.choreConsumer.IsPermittedByTraits(Db.Get().ChoreGroups.Combat))
				{
					return false;
				}
			}
			return this.health.State < base.smi.def.fleethresholdState;
		}

		// Token: 0x06008639 RID: 34361 RVA: 0x002EE578 File Offset: 0x002EC778
		private void GotoThreatResponse()
		{
			Chore currentChore = base.smi.master.GetComponent<ChoreDriver>().GetCurrentChore();
			if (this.WillFight() && this.mainThreat.GetComponent<FactionAlignment>().IsPlayerTargeted())
			{
				base.smi.GoTo(base.smi.sm.threatened.duplicant.ShouldFight);
				return;
			}
			if (currentChore != null && currentChore.target != null && currentChore.target != base.master && currentChore.target.GetComponent<Pickupable>() != null)
			{
				return;
			}
			base.smi.GoTo(base.smi.sm.threatened.duplicant.ShoudFlee);
		}

		// Token: 0x0600863A RID: 34362 RVA: 0x002EE62D File Offset: 0x002EC82D
		public void GoToThreatened()
		{
			if (this.IAmADuplicant)
			{
				this.GotoThreatResponse();
				return;
			}
			base.smi.GoTo(base.sm.threatened.creature);
		}

		// Token: 0x0600863B RID: 34363 RVA: 0x002EE659 File Offset: 0x002EC859
		public void Cleanup(object data)
		{
			if (this.mainThreat)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
			}
		}

		// Token: 0x0600863C RID: 34364 RVA: 0x002EE694 File Offset: 0x002EC894
		public void RefreshThreat(object data)
		{
			if (!base.IsRunning())
			{
				return;
			}
			if (base.smi.CheckForThreats())
			{
				this.GoToThreatened();
				return;
			}
			if (base.smi.GetCurrentState() != base.sm.safe)
			{
				base.Trigger(-21431934, null);
				base.smi.GoTo(base.sm.safe);
			}
		}

		// Token: 0x0600863D RID: 34365 RVA: 0x002EE6F8 File Offset: 0x002EC8F8
		public bool CheckForThreats()
		{
			GameObject gameObject;
			if (this.revengeThreat.IsValidRevengeTarget(this.IAmADuplicant))
			{
				gameObject = this.revengeThreat.target.gameObject;
			}
			else
			{
				gameObject = this.FindThreat();
			}
			this.SetMainThreat(gameObject);
			return gameObject != null;
		}

		// Token: 0x0600863E RID: 34366 RVA: 0x002EE740 File Offset: 0x002EC940
		public GameObject FindThreat()
		{
			this.threats.Clear();
			if (base.isMasterNull)
			{
				return null;
			}
			bool flag = this.WillFight();
			ListPool<ScenePartitionerEntry, ThreatMonitor>.PooledList pooledList = ListPool<ScenePartitionerEntry, ThreatMonitor>.Allocate();
			int num = 20;
			Extents extents = new Extents(Grid.PosToCell(base.gameObject), num);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.attackableEntitiesLayer, pooledList);
			for (int i = 0; i < pooledList.Count; i++)
			{
				FactionAlignment factionAlignment = pooledList[i].obj as FactionAlignment;
				if (!(factionAlignment.transform == null) && !(factionAlignment == this.alignment) && factionAlignment.IsAlignmentActive() && FactionManager.Instance.GetDisposition(this.alignment.Alignment, factionAlignment.Alignment) == FactionManager.Disposition.Attack)
				{
					if (base.def.friendlyCreatureTags != null)
					{
						bool flag2 = false;
						foreach (Tag tag in base.def.friendlyCreatureTags)
						{
							if (factionAlignment.HasTag(tag))
							{
								flag2 = true;
							}
						}
						if (flag2)
						{
							goto IL_127;
						}
					}
					if (this.navigator.CanReach(factionAlignment.attackable))
					{
						this.threats.Add(factionAlignment);
					}
				}
				IL_127:;
			}
			pooledList.Recycle();
			if (this.IAmADuplicant && flag)
			{
				for (int k = 0; k < 6; k++)
				{
					if (k != 0)
					{
						foreach (FactionAlignment factionAlignment2 in FactionManager.Instance.GetFaction((FactionManager.FactionID)k).Members)
						{
							if (factionAlignment2.IsPlayerTargeted() && !factionAlignment2.health.IsDefeated() && !this.threats.Contains(factionAlignment2) && this.navigator.CanReach(factionAlignment2.attackable))
							{
								this.threats.Add(factionAlignment2);
							}
						}
					}
				}
			}
			if (this.threats.Count == 0)
			{
				return null;
			}
			return this.PickBestTarget(this.threats);
		}

		// Token: 0x0600863F RID: 34367 RVA: 0x002EE96C File Offset: 0x002ECB6C
		public GameObject PickBestTarget(List<FactionAlignment> threats)
		{
			float num = 1f;
			Vector2 vector = base.gameObject.transform.GetPosition();
			GameObject gameObject = null;
			float num2 = float.PositiveInfinity;
			for (int i = threats.Count - 1; i >= 0; i--)
			{
				FactionAlignment factionAlignment = threats[i];
				float num3 = Vector2.Distance(vector, factionAlignment.transform.GetPosition()) / num;
				if (num3 < num2)
				{
					num2 = num3;
					gameObject = factionAlignment.gameObject;
				}
			}
			return gameObject;
		}

		// Token: 0x0400688E RID: 26766
		public FactionAlignment alignment;

		// Token: 0x0400688F RID: 26767
		private Navigator navigator;

		// Token: 0x04006890 RID: 26768
		public ChoreDriver choreDriver;

		// Token: 0x04006891 RID: 26769
		private Health health;

		// Token: 0x04006892 RID: 26770
		private ChoreConsumer choreConsumer;

		// Token: 0x04006893 RID: 26771
		public ThreatMonitor.Grudge revengeThreat;

		// Token: 0x04006894 RID: 26772
		private GameObject mainThreat;

		// Token: 0x04006895 RID: 26773
		private List<FactionAlignment> threats = new List<FactionAlignment>();

		// Token: 0x04006896 RID: 26774
		private Action<object> refreshThreatDelegate;
	}
}
