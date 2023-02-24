using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CCB RID: 3275
	public class CollectedArtifacts : VictoryColonyAchievementRequirement
	{
		// Token: 0x06006657 RID: 26199 RVA: 0x00275E28 File Offset: 0x00274028
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.COLLECT_ARTIFACTS.Replace("{collectedCount}", this.GetStudiedArtifactCount().ToString()).Replace("{neededCount}", 10.ToString());
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x00275E6B File Offset: 0x0027406B
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x00275E79 File Offset: 0x00274079
		public override bool Success()
		{
			return ArtifactSelector.Instance.AnalyzedArtifactCount >= 10;
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x00275E8C File Offset: 0x0027408C
		private int GetStudiedArtifactCount()
		{
			return ArtifactSelector.Instance.AnalyzedArtifactCount;
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x00275E98 File Offset: 0x00274098
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.REQUIREMENTS.STUDY_ARTIFACTS.Replace("{artifactCount}", 10.ToString());
		}

		// Token: 0x04004AC7 RID: 19143
		private const int REQUIRED_ARTIFACT_COUNT = 10;
	}
}
