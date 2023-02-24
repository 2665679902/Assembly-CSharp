using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CCC RID: 3276
	public class CollectedSpaceArtifacts : VictoryColonyAchievementRequirement
	{
		// Token: 0x0600665D RID: 26205 RVA: 0x00275ECC File Offset: 0x002740CC
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.COLLECT_SPACE_ARTIFACTS.Replace("{collectedCount}", this.GetStudiedSpaceArtifactCount().ToString()).Replace("{neededCount}", 10.ToString());
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x00275F0F File Offset: 0x0027410F
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x00275F1D File Offset: 0x0027411D
		public override bool Success()
		{
			return ArtifactSelector.Instance.AnalyzedSpaceArtifactCount >= 10;
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x00275F30 File Offset: 0x00274130
		private int GetStudiedSpaceArtifactCount()
		{
			return ArtifactSelector.Instance.AnalyzedSpaceArtifactCount;
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x00275F3C File Offset: 0x0027413C
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.REQUIREMENTS.STUDY_SPACE_ARTIFACTS.Replace("{artifactCount}", 10.ToString());
		}

		// Token: 0x04004AC8 RID: 19144
		private const int REQUIRED_ARTIFACT_COUNT = 10;
	}
}
