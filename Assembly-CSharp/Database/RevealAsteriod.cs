using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CE8 RID: 3304
	public class RevealAsteriod : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066D7 RID: 26327 RVA: 0x00277DFD File Offset: 0x00275FFD
		public RevealAsteriod(float percentToReveal)
		{
			this.percentToReveal = percentToReveal;
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x00277E0C File Offset: 0x0027600C
		public override bool Success()
		{
			this.amountRevealed = 0f;
			float num = 0f;
			WorldContainer startWorld = ClusterManager.Instance.GetStartWorld();
			Vector2 minimumBounds = startWorld.minimumBounds;
			Vector2 maximumBounds = startWorld.maximumBounds;
			int num2 = (int)minimumBounds.x;
			while ((float)num2 <= maximumBounds.x)
			{
				int num3 = (int)minimumBounds.y;
				while ((float)num3 <= maximumBounds.y)
				{
					if (Grid.Visible[Grid.PosToCell(new Vector2((float)num2, (float)num3))] > 0)
					{
						num += 1f;
					}
					num3++;
				}
				num2++;
			}
			this.amountRevealed = num / (float)(startWorld.Width * startWorld.Height);
			return this.amountRevealed > this.percentToReveal;
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x00277EC0 File Offset: 0x002760C0
		public void Deserialize(IReader reader)
		{
			this.percentToReveal = reader.ReadSingle();
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x00277ECE File Offset: 0x002760CE
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.REVEALED, this.amountRevealed * 100f, this.percentToReveal * 100f);
		}

		// Token: 0x04004AE9 RID: 19177
		private float percentToReveal;

		// Token: 0x04004AEA RID: 19178
		private float amountRevealed;
	}
}
