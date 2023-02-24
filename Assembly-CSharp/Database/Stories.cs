using System;
using ProcGen;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CBA RID: 3258
	public class Stories : ResourceSet<Story>
	{
		// Token: 0x06006605 RID: 26117 RVA: 0x002728F8 File Offset: 0x00270AF8
		public Stories(ResourceSet parent)
			: base("Stories", parent)
		{
			this.MegaBrainTank = base.Add(new Story("MegaBrainTank", "storytraits/MegaBrainTank", 0, 1, 43).SetKeepsake("keepsake_megabrain"));
			this.CreatureManipulator = base.Add(new Story("CreatureManipulator", "storytraits/CritterManipulator", 1, 2, 43).SetKeepsake("keepsake_crittermanipulator"));
			this.LonelyMinion = base.Add(new Story("LonelyMinion", "storytraits/LonelyMinion", 2, 3, 44).SetKeepsake("keepsake_lonelyminion"));
			this.resources.Sort();
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x00272997 File Offset: 0x00270B97
		public void AddStoryMod(Story mod)
		{
			mod.kleiUseOnlyCoordinateOffset = -1;
			base.Add(mod);
			this.resources.Sort();
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x002729B4 File Offset: 0x00270BB4
		public int GetHighestCoordinateOffset()
		{
			int num = 0;
			foreach (Story story in this.resources)
			{
				num = Mathf.Max(num, story.kleiUseOnlyCoordinateOffset);
			}
			return num;
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x00272A10 File Offset: 0x00270C10
		public WorldTrait GetStoryTrait(string id, bool assertMissingTrait = false)
		{
			Story story = this.resources.Find((Story x) => x.Id == id);
			if (story != null)
			{
				return SettingsCache.GetCachedStoryTrait(story.worldgenStoryTraitKey, assertMissingTrait);
			}
			return null;
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x00272A54 File Offset: 0x00270C54
		public Story GetStoryFromStoryTrait(string storyTraitTemplate)
		{
			return this.resources.Find((Story x) => x.worldgenStoryTraitKey == storyTraitTemplate);
		}

		// Token: 0x04004A6D RID: 19053
		public Story MegaBrainTank;

		// Token: 0x04004A6E RID: 19054
		public Story CreatureManipulator;

		// Token: 0x04004A6F RID: 19055
		public Story LonelyMinion;
	}
}
