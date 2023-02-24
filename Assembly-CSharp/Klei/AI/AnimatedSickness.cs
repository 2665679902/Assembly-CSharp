using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D7E RID: 3454
	public class AnimatedSickness : Sickness.SicknessComponent
	{
		// Token: 0x06006967 RID: 26983 RVA: 0x00290764 File Offset: 0x0028E964
		public AnimatedSickness(HashedString[] kanim_filenames, Expression expression)
		{
			this.kanims = new KAnimFile[kanim_filenames.Length];
			for (int i = 0; i < kanim_filenames.Length; i++)
			{
				this.kanims[i] = Assets.GetAnim(kanim_filenames[i]);
			}
			this.expression = expression;
		}

		// Token: 0x06006968 RID: 26984 RVA: 0x002907B0 File Offset: 0x0028E9B0
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			for (int i = 0; i < this.kanims.Length; i++)
			{
				go.GetComponent<KAnimControllerBase>().AddAnimOverrides(this.kanims[i], 10f);
			}
			if (this.expression != null)
			{
				go.GetComponent<FaceGraph>().AddExpression(this.expression);
			}
			return null;
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x00290804 File Offset: 0x0028EA04
		public override void OnCure(GameObject go, object instace_data)
		{
			if (this.expression != null)
			{
				go.GetComponent<FaceGraph>().RemoveExpression(this.expression);
			}
			for (int i = 0; i < this.kanims.Length; i++)
			{
				go.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(this.kanims[i]);
			}
		}

		// Token: 0x04004F46 RID: 20294
		private KAnimFile[] kanims;

		// Token: 0x04004F47 RID: 20295
		private Expression expression;
	}
}
