using System;
using System.Collections.Generic;
using FMODUnity;

namespace Database
{
	// Token: 0x02000CFA RID: 3322
	public class ColonyAchievement : Resource
	{
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x0027A259 File Offset: 0x00278459
		// (set) Token: 0x06006712 RID: 26386 RVA: 0x0027A261 File Offset: 0x00278461
		public EventReference victoryNISSnapshot { get; private set; }

		// Token: 0x06006713 RID: 26387 RVA: 0x0027A26C File Offset: 0x0027846C
		public ColonyAchievement(string Id, string platformAchievementId, string Name, string description, bool isVictoryCondition, List<ColonyAchievementRequirement> requirementChecklist, string messageTitle = "", string messageBody = "", string videoDataName = "", string victoryLoopVideo = "", Action<KMonoBehaviour> VictorySequence = null, EventReference victorySnapshot = default(EventReference), string icon = "", string[] dlcIds = null)
			: base(Id, Name)
		{
			this.Id = Id;
			this.platformAchievementId = platformAchievementId;
			this.Name = Name;
			this.description = description;
			this.isVictoryCondition = isVictoryCondition;
			this.requirementChecklist = requirementChecklist;
			this.messageTitle = messageTitle;
			this.messageBody = messageBody;
			this.shortVideoName = videoDataName;
			this.loopVideoName = victoryLoopVideo;
			this.victorySequence = VictorySequence;
			this.victoryNISSnapshot = (victorySnapshot.IsNull ? AudioMixerSnapshots.Get().VictoryNISGenericSnapshot : victorySnapshot);
			this.icon = icon;
			this.dlcIds = dlcIds;
			if (this.dlcIds == null)
			{
				this.dlcIds = DlcManager.AVAILABLE_ALL_VERSIONS;
			}
		}

		// Token: 0x04004B26 RID: 19238
		public string description;

		// Token: 0x04004B27 RID: 19239
		public bool isVictoryCondition;

		// Token: 0x04004B28 RID: 19240
		public string messageTitle;

		// Token: 0x04004B29 RID: 19241
		public string messageBody;

		// Token: 0x04004B2A RID: 19242
		public string shortVideoName;

		// Token: 0x04004B2B RID: 19243
		public string loopVideoName;

		// Token: 0x04004B2C RID: 19244
		public string platformAchievementId;

		// Token: 0x04004B2D RID: 19245
		public string icon;

		// Token: 0x04004B2E RID: 19246
		public List<ColonyAchievementRequirement> requirementChecklist = new List<ColonyAchievementRequirement>();

		// Token: 0x04004B2F RID: 19247
		public Action<KMonoBehaviour> victorySequence;

		// Token: 0x04004B31 RID: 19249
		public string[] dlcIds;
	}
}
