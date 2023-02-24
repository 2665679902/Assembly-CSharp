using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE3 RID: 3299
	public class CreateMasterPainting : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066C3 RID: 26307 RVA: 0x00277A30 File Offset: 0x00275C30
		public override bool Success()
		{
			foreach (Painting painting in Components.Paintings.Items)
			{
				if (painting != null)
				{
					ArtableStage artableStage = Db.GetArtableStages().TryGet(painting.CurrentStage);
					if (artableStage != null && artableStage.statusItem == Db.Get().ArtableStatuses.LookingGreat)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x00277ABC File Offset: 0x00275CBC
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x00277ABE File Offset: 0x00275CBE
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CREATE_A_PAINTING;
		}
	}
}
