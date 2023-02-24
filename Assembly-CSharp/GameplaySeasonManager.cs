using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using KSerialization;

// Token: 0x0200078A RID: 1930
public class GameplaySeasonManager : GameStateMachine<GameplaySeasonManager, GameplaySeasonManager.Instance, IStateMachineTarget, GameplaySeasonManager.Def>
{
	// Token: 0x060035FF RID: 13823 RVA: 0x0012B960 File Offset: 0x00129B60
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.root;
		this.root.Enter(delegate(GameplaySeasonManager.Instance smi)
		{
			smi.Initialize();
		}).Update(delegate(GameplaySeasonManager.Instance smi, float dt)
		{
			smi.Update(dt);
		}, UpdateRate.SIM_4000ms, false);
	}

	// Token: 0x020014B8 RID: 5304
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020014B9 RID: 5305
	public new class Instance : GameStateMachine<GameplaySeasonManager, GameplaySeasonManager.Instance, IStateMachineTarget, GameplaySeasonManager.Def>.GameInstance
	{
		// Token: 0x060081B4 RID: 33204 RVA: 0x002E2CC2 File Offset: 0x002E0EC2
		public Instance(IStateMachineTarget master, GameplaySeasonManager.Def def)
			: base(master, def)
		{
			this.activeSeasons = new List<GameplaySeasonInstance>();
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x002E2CD8 File Offset: 0x002E0ED8
		public void Initialize()
		{
			this.activeSeasons.RemoveAll((GameplaySeasonInstance item) => item.Season == null);
			List<GameplaySeason> list = new List<GameplaySeason>();
			if (this.m_worldContainer != null)
			{
				ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
				using (List<string>.Enumerator enumerator = this.m_worldContainer.GetSeasonIds().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						GameplaySeason gameplaySeason = Db.Get().GameplaySeasons.TryGet(text);
						if (gameplaySeason == null)
						{
							Debug.LogWarning("world " + component.name + " has invalid season " + text);
						}
						else
						{
							if (gameplaySeason.type != GameplaySeason.Type.World)
							{
								Debug.LogWarning(string.Concat(new string[] { "world ", component.name, " has specified season ", text, ", which is not a world type season" }));
							}
							list.Add(gameplaySeason);
						}
					}
					goto IL_146;
				}
			}
			Debug.Assert(base.GetComponent<SaveGame>() != null);
			list = Db.Get().GameplaySeasons.resources.Where((GameplaySeason season) => season.type == GameplaySeason.Type.Cluster).ToList<GameplaySeason>();
			IL_146:
			foreach (GameplaySeason gameplaySeason2 in list)
			{
				if (DlcManager.IsContentActive(gameplaySeason2.dlcId) && gameplaySeason2.startActive && !this.SeasonExists(gameplaySeason2))
				{
					this.activeSeasons.Add(gameplaySeason2.Instantiate(this.GetWorldId()));
				}
			}
			foreach (GameplaySeasonInstance gameplaySeasonInstance in new List<GameplaySeasonInstance>(this.activeSeasons))
			{
				if (!list.Contains(gameplaySeasonInstance.Season) || !DlcManager.IsContentActive(gameplaySeasonInstance.Season.dlcId))
				{
					this.activeSeasons.Remove(gameplaySeasonInstance);
				}
			}
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x002E2F20 File Offset: 0x002E1120
		private int GetWorldId()
		{
			if (this.m_worldContainer != null)
			{
				return this.m_worldContainer.id;
			}
			return -1;
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x002E2F40 File Offset: 0x002E1140
		public void Update(float dt)
		{
			foreach (GameplaySeasonInstance gameplaySeasonInstance in this.activeSeasons)
			{
				if (gameplaySeasonInstance.ShouldGenerateEvents() && GameUtil.GetCurrentTimeInCycles() > gameplaySeasonInstance.NextEventTime)
				{
					int num = 0;
					while (num < gameplaySeasonInstance.Season.numEventsToStartEachPeriod && gameplaySeasonInstance.StartEvent(false))
					{
						num++;
					}
				}
			}
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x002E2FC0 File Offset: 0x002E11C0
		public void StartNewSeason(GameplaySeason seasonType)
		{
			if (DlcManager.IsContentActive(seasonType.dlcId))
			{
				this.activeSeasons.Add(seasonType.Instantiate(this.GetWorldId()));
			}
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x002E2FE8 File Offset: 0x002E11E8
		public bool SeasonExists(GameplaySeason seasonType)
		{
			return this.activeSeasons.Find((GameplaySeasonInstance e) => e.Season.IdHash == seasonType.IdHash) != null;
		}

		// Token: 0x0400648A RID: 25738
		[Serialize]
		public List<GameplaySeasonInstance> activeSeasons;

		// Token: 0x0400648B RID: 25739
		[MyCmpGet]
		private WorldContainer m_worldContainer;
	}
}
