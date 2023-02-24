using System;
using System.Collections.Generic;

namespace Klei.AI
{
	// Token: 0x02000D9A RID: 3482
	public class Emote : Resource
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06006A08 RID: 27144 RVA: 0x002932F5 File Offset: 0x002914F5
		public int StepCount
		{
			get
			{
				if (this.emoteSteps != null)
				{
					return this.emoteSteps.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06006A09 RID: 27145 RVA: 0x0029330C File Offset: 0x0029150C
		public KAnimFile AnimSet
		{
			get
			{
				if (this.animSetName != HashedString.Invalid && this.animSet == null)
				{
					this.animSet = Assets.GetAnim(this.animSetName);
				}
				return this.animSet;
			}
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x00293345 File Offset: 0x00291545
		public Emote(ResourceSet parent, string emoteId, EmoteStep[] defaultSteps, string animSetName = null)
			: base(emoteId, parent, null)
		{
			this.emoteSteps.AddRange(defaultSteps);
			this.animSetName = animSetName;
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x00293380 File Offset: 0x00291580
		public bool IsValidForController(KBatchedAnimController animController)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < this.StepCount)
			{
				flag = animController.HasAnimation(this.emoteSteps[num].anim);
				num++;
			}
			KAnimFileData kanimFileData = ((this.animSet == null) ? null : this.animSet.GetData());
			int num2 = 0;
			while (kanimFileData != null && flag && num2 < this.StepCount)
			{
				bool flag2 = false;
				int num3 = 0;
				while (!flag2 && num3 < kanimFileData.animCount)
				{
					flag2 = kanimFileData.GetAnim(num2).id == this.emoteSteps[num2].anim;
					num3++;
				}
				flag = flag2;
				num2++;
			}
			return flag;
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x00293438 File Offset: 0x00291638
		public void ApplyAnimOverrides(KBatchedAnimController animController, KAnimFile overrideSet)
		{
			KAnimFile kanimFile = ((overrideSet != null) ? overrideSet : this.AnimSet);
			if (kanimFile == null || animController == null)
			{
				return;
			}
			animController.AddAnimOverrides(kanimFile, 0f);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x00293478 File Offset: 0x00291678
		public void RemoveAnimOverrides(KBatchedAnimController animController, KAnimFile overrideSet)
		{
			KAnimFile kanimFile = ((overrideSet != null) ? overrideSet : this.AnimSet);
			if (kanimFile == null || animController == null)
			{
				return;
			}
			animController.RemoveAnimOverrides(kanimFile);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x002934B4 File Offset: 0x002916B4
		public void CollectStepAnims(out HashedString[] emoteAnims, int iterations)
		{
			emoteAnims = new HashedString[this.emoteSteps.Count * iterations];
			for (int i = 0; i < emoteAnims.Length; i++)
			{
				emoteAnims[i] = this.emoteSteps[i % this.emoteSteps.Count].anim;
			}
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x00293509 File Offset: 0x00291709
		public bool IsValidStep(int stepIdx)
		{
			return stepIdx >= 0 && stepIdx < this.emoteSteps.Count;
		}

		// Token: 0x17000793 RID: 1939
		public EmoteStep this[int stepIdx]
		{
			get
			{
				if (!this.IsValidStep(stepIdx))
				{
					return null;
				}
				return this.emoteSteps[stepIdx];
			}
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x00293538 File Offset: 0x00291738
		public int GetStepIndex(HashedString animName)
		{
			int i = 0;
			bool flag = false;
			while (i < this.emoteSteps.Count)
			{
				if (this.emoteSteps[i].anim == animName)
				{
					flag = true;
					break;
				}
				i++;
			}
			Debug.Assert(flag, string.Format("Could not find emote step {0} for emote {1}!", animName, this.Id));
			return i;
		}

		// Token: 0x04004FAF RID: 20399
		private HashedString animSetName = null;

		// Token: 0x04004FB0 RID: 20400
		private KAnimFile animSet;

		// Token: 0x04004FB1 RID: 20401
		private List<EmoteStep> emoteSteps = new List<EmoteStep>();
	}
}
