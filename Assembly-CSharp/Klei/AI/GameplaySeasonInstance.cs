using System;
using System.Collections.Generic;
using System.Linq;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D8B RID: 3467
	[SerializationConfig(MemberSerialization.OptIn)]
	public class GameplaySeasonInstance : ISaveLoadable
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060069B7 RID: 27063 RVA: 0x002922B2 File Offset: 0x002904B2
		public float NextEventTime
		{
			get
			{
				return this.nextPeriodTime + this.randomizedNextTime;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060069B8 RID: 27064 RVA: 0x002922C1 File Offset: 0x002904C1
		public GameplaySeason Season
		{
			get
			{
				if (this._season == null)
				{
					this._season = Db.Get().GameplaySeasons.TryGet(this.seasonId);
				}
				return this._season;
			}
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x002922EC File Offset: 0x002904EC
		public GameplaySeasonInstance(GameplaySeason season, int worldId)
		{
			this.seasonId = season.Id;
			this.worldId = worldId;
			float currentTimeInCycles = GameUtil.GetCurrentTimeInCycles();
			if (season.synchronizedToPeriod)
			{
				float seasonPeriod = this.GetSeasonPeriod();
				this.nextPeriodTime = (Mathf.Floor(currentTimeInCycles / seasonPeriod) + 1f) * seasonPeriod;
			}
			else
			{
				this.nextPeriodTime = currentTimeInCycles;
			}
			this.CalculateNextEventTime();
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x0029234C File Offset: 0x0029054C
		private void CalculateNextEventTime()
		{
			float seasonPeriod = this.GetSeasonPeriod();
			this.randomizedNextTime = UnityEngine.Random.Range(this.Season.randomizedEventStartTime.min, this.Season.randomizedEventStartTime.max);
			float currentTimeInCycles = GameUtil.GetCurrentTimeInCycles();
			float num = this.nextPeriodTime + this.randomizedNextTime;
			while (num < currentTimeInCycles || num < this.Season.minCycle)
			{
				this.nextPeriodTime += seasonPeriod;
				num = this.nextPeriodTime + this.randomizedNextTime;
			}
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x002923D0 File Offset: 0x002905D0
		public bool StartEvent(bool ignorePreconditions = false)
		{
			bool flag = false;
			this.CalculateNextEventTime();
			this.numStartEvents++;
			List<GameplayEvent> list;
			if (!ignorePreconditions)
			{
				list = this.Season.events.Where((GameplayEvent x) => x.IsAllowed()).ToList<GameplayEvent>();
			}
			else
			{
				list = this.Season.events;
			}
			List<GameplayEvent> list2 = list;
			if (list2.Count > 0)
			{
				list2.ForEach(delegate(GameplayEvent x)
				{
					x.CalculatePriority();
				});
				list2.Sort();
				int num = Mathf.Min(list2.Count, 5);
				GameplayEvent gameplayEvent = list2[UnityEngine.Random.Range(0, num)];
				GameplayEventManager.Instance.StartNewEvent(gameplayEvent, this.worldId);
				flag = true;
			}
			this.allEventWillNotRunAgain = true;
			using (List<GameplayEvent>.Enumerator enumerator = this.Season.events.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.WillNeverRunAgain())
					{
						this.allEventWillNotRunAgain = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x002924F8 File Offset: 0x002906F8
		private float GetSeasonPeriod()
		{
			return this.Season.period;
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x00292508 File Offset: 0x00290708
		public bool ShouldGenerateEvents()
		{
			WorldContainer world = ClusterManager.Instance.GetWorld(this.worldId);
			if (!world.IsDupeVisited && !world.IsRoverVisted)
			{
				return false;
			}
			if ((this.Season.finishAfterNumEvents != -1 && this.numStartEvents >= this.Season.finishAfterNumEvents) || this.allEventWillNotRunAgain)
			{
				return false;
			}
			float currentTimeInCycles = GameUtil.GetCurrentTimeInCycles();
			return currentTimeInCycles > this.Season.minCycle && currentTimeInCycles < this.Season.maxCycle;
		}

		// Token: 0x04004F84 RID: 20356
		public const int LIMIT_SELECTION = 5;

		// Token: 0x04004F85 RID: 20357
		[Serialize]
		public int numStartEvents;

		// Token: 0x04004F86 RID: 20358
		[Serialize]
		public int worldId;

		// Token: 0x04004F87 RID: 20359
		[Serialize]
		private readonly string seasonId;

		// Token: 0x04004F88 RID: 20360
		[Serialize]
		private float nextPeriodTime;

		// Token: 0x04004F89 RID: 20361
		[Serialize]
		private float randomizedNextTime;

		// Token: 0x04004F8A RID: 20362
		private bool allEventWillNotRunAgain;

		// Token: 0x04004F8B RID: 20363
		private GameplaySeason _season;
	}
}
