using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Klei.AI
{
	// Token: 0x02000D8A RID: 3466
	[DebuggerDisplay("{base.Id}")]
	public class GameplaySeason : Resource
	{
		// Token: 0x060069B4 RID: 27060 RVA: 0x0029219C File Offset: 0x0029039C
		public GameplaySeason(string id, GameplaySeason.Type type, string dlcId, float period, bool synchronizedToPeriod, float randomizedEventStartTime = -1f, bool startActive = false, int finishAfterNumEvents = -1, float minCycle = 0f, float maxCycle = float.PositiveInfinity, int numEventsToStartEachPeriod = 1)
			: base(id, null, null)
		{
			this.type = type;
			this.dlcId = dlcId;
			this.period = period;
			this.synchronizedToPeriod = synchronizedToPeriod;
			global::Debug.Assert(period > 0f, "Season " + id + "'s Period cannot be 0 or negative");
			if (randomizedEventStartTime == -1f)
			{
				this.randomizedEventStartTime = new MathUtil.MinMax(-0.05f * period, 0.05f * period);
			}
			else
			{
				this.randomizedEventStartTime = new MathUtil.MinMax(-randomizedEventStartTime, randomizedEventStartTime);
				DebugUtil.DevAssert((this.randomizedEventStartTime.max - this.randomizedEventStartTime.min) * 0.4f < period, string.Format("Season {0} randomizedEventStartTime is greater than {1}% of its period.", id, 0.4f), null);
			}
			this.startActive = startActive;
			this.finishAfterNumEvents = finishAfterNumEvents;
			this.minCycle = minCycle;
			this.maxCycle = maxCycle;
			this.events = new List<GameplayEvent>();
			this.numEventsToStartEachPeriod = numEventsToStartEachPeriod;
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x0029229A File Offset: 0x0029049A
		public GameplaySeason AddEvent(GameplayEvent evt)
		{
			this.events.Add(evt);
			return this;
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x002922A9 File Offset: 0x002904A9
		public GameplaySeasonInstance Instantiate(int worldId)
		{
			return new GameplaySeasonInstance(this, worldId);
		}

		// Token: 0x04004F75 RID: 20341
		public const float DEFAULT_PERCENTAGE_RANDOMIZED_EVENT_START = 0.05f;

		// Token: 0x04004F76 RID: 20342
		public const float PERCENTAGE_WARNING = 0.4f;

		// Token: 0x04004F77 RID: 20343
		public const float USE_DEFAULT = -1f;

		// Token: 0x04004F78 RID: 20344
		public const int INFINITE = -1;

		// Token: 0x04004F79 RID: 20345
		public float period;

		// Token: 0x04004F7A RID: 20346
		public bool synchronizedToPeriod;

		// Token: 0x04004F7B RID: 20347
		public MathUtil.MinMax randomizedEventStartTime;

		// Token: 0x04004F7C RID: 20348
		public int finishAfterNumEvents = -1;

		// Token: 0x04004F7D RID: 20349
		public bool startActive;

		// Token: 0x04004F7E RID: 20350
		public int numEventsToStartEachPeriod;

		// Token: 0x04004F7F RID: 20351
		public float minCycle;

		// Token: 0x04004F80 RID: 20352
		public float maxCycle;

		// Token: 0x04004F81 RID: 20353
		public List<GameplayEvent> events;

		// Token: 0x04004F82 RID: 20354
		public GameplaySeason.Type type;

		// Token: 0x04004F83 RID: 20355
		public string dlcId;

		// Token: 0x02001E5F RID: 7775
		public enum Type
		{
			// Token: 0x04008882 RID: 34946
			World,
			// Token: 0x04008883 RID: 34947
			Cluster
		}
	}
}
