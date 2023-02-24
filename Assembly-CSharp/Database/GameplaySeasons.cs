using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C99 RID: 3225
	public class GameplaySeasons : ResourceSet<GameplaySeason>
	{
		// Token: 0x0600659E RID: 26014 RVA: 0x0026C584 File Offset: 0x0026A784
		public GameplaySeasons(ResourceSet parent)
			: base("GameplaySeasons", parent)
		{
			this.MeteorShowers = base.Add(new GameplaySeason("MeteorShowers", GameplaySeason.Type.World, "", 14f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1).AddEvent(Db.Get().GameplayEvents.MeteorShowerIronEvent).AddEvent(Db.Get().GameplayEvents.MeteorShowerGoldEvent).AddEvent(Db.Get().GameplayEvents.MeteorShowerCopperEvent));
			this.RegolithMoonMeteorShowers = base.Add(new GameplaySeason("RegolithMoonMeteorShowers", GameplaySeason.Type.World, "EXPANSION1_ID", 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1).AddEvent(Db.Get().GameplayEvents.MeteorShowerDustEvent));
			this.TemporalTearMeteorShowers = base.Add(new GameplaySeason("TemporalTearMeteorShowers", GameplaySeason.Type.World, "EXPANSION1_ID", 1f, false, 0f, false, -1, 0f, float.PositiveInfinity, 1).AddEvent(Db.Get().GameplayEvents.MeteorShowerFullereneEvent));
			this.GassyMooteorShowers = base.Add(new GameplaySeason("GassyMooteorShowers", GameplaySeason.Type.World, "EXPANSION1_ID", 20f, false, -1f, true, -1, 0f, float.PositiveInfinity, 1).AddEvent(Db.Get().GameplayEvents.GassyMooteorEvent));
		}

		// Token: 0x04004945 RID: 18757
		public GameplaySeason MeteorShowers;

		// Token: 0x04004946 RID: 18758
		public GameplaySeason GassyMooteorShowers;

		// Token: 0x04004947 RID: 18759
		public GameplaySeason TemporalTearMeteorShowers;

		// Token: 0x04004948 RID: 18760
		public GameplaySeason NaturalRandomEvents;

		// Token: 0x04004949 RID: 18761
		public GameplaySeason DupeRandomEvents;

		// Token: 0x0400494A RID: 18762
		public GameplaySeason PrickleCropSeason;

		// Token: 0x0400494B RID: 18763
		public GameplaySeason BonusEvents;

		// Token: 0x0400494C RID: 18764
		public GameplaySeason RegolithMoonMeteorShowers;
	}
}
