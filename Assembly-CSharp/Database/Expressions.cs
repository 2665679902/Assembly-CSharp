using System;

namespace Database
{
	// Token: 0x02000C96 RID: 3222
	public class Expressions : ResourceSet<Expression>
	{
		// Token: 0x06006598 RID: 26008 RVA: 0x0026B17C File Offset: 0x0026937C
		public Expressions(ResourceSet parent)
			: base("Expressions", parent)
		{
			Faces faces = Db.Get().Faces;
			this.Angry = new Expression("Angry", this, faces.Angry);
			this.Suffocate = new Expression("Suffocate", this, faces.Suffocate);
			this.RecoverBreath = new Expression("RecoverBreath", this, faces.Uncomfortable);
			this.RedAlert = new Expression("RedAlert", this, faces.Hot);
			this.Hungry = new Expression("Hungry", this, faces.Hungry);
			this.Radiation1 = new Expression("Radiation1", this, faces.Radiation1);
			this.Radiation2 = new Expression("Radiation2", this, faces.Radiation2);
			this.Radiation3 = new Expression("Radiation3", this, faces.Radiation3);
			this.Radiation4 = new Expression("Radiation4", this, faces.Radiation4);
			this.SickSpores = new Expression("SickSpores", this, faces.SickSpores);
			this.Zombie = new Expression("Zombie", this, faces.Zombie);
			this.SickFierySkin = new Expression("SickFierySkin", this, faces.SickFierySkin);
			this.SickCold = new Expression("SickCold", this, faces.SickCold);
			this.Pollen = new Expression("Pollen", this, faces.Pollen);
			this.Sick = new Expression("Sick", this, faces.Sick);
			this.Cold = new Expression("Cold", this, faces.Cold);
			this.Hot = new Expression("Hot", this, faces.Hot);
			this.FullBladder = new Expression("FullBladder", this, faces.Uncomfortable);
			this.Tired = new Expression("Tired", this, faces.Tired);
			this.Unhappy = new Expression("Unhappy", this, faces.Uncomfortable);
			this.Uncomfortable = new Expression("Uncomfortable", this, faces.Uncomfortable);
			this.Productive = new Expression("Productive", this, faces.Productive);
			this.Determined = new Expression("Determined", this, faces.Determined);
			this.Sticker = new Expression("Sticker", this, faces.Sticker);
			this.Balloon = new Expression("Sticker", this, faces.Balloon);
			this.Sparkle = new Expression("Sticker", this, faces.Sparkle);
			this.Music = new Expression("Music", this, faces.Music);
			this.Tickled = new Expression("Tickled", this, faces.Tickled);
			this.Happy = new Expression("Happy", this, faces.Happy);
			this.Relief = new Expression("Relief", this, faces.Happy);
			this.Neutral = new Expression("Neutral", this, faces.Neutral);
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.resources[i].priority = 100 * (this.Count - i);
			}
		}

		// Token: 0x040048E8 RID: 18664
		public Expression Neutral;

		// Token: 0x040048E9 RID: 18665
		public Expression Happy;

		// Token: 0x040048EA RID: 18666
		public Expression Uncomfortable;

		// Token: 0x040048EB RID: 18667
		public Expression Cold;

		// Token: 0x040048EC RID: 18668
		public Expression Hot;

		// Token: 0x040048ED RID: 18669
		public Expression FullBladder;

		// Token: 0x040048EE RID: 18670
		public Expression Tired;

		// Token: 0x040048EF RID: 18671
		public Expression Hungry;

		// Token: 0x040048F0 RID: 18672
		public Expression Angry;

		// Token: 0x040048F1 RID: 18673
		public Expression Unhappy;

		// Token: 0x040048F2 RID: 18674
		public Expression RedAlert;

		// Token: 0x040048F3 RID: 18675
		public Expression Suffocate;

		// Token: 0x040048F4 RID: 18676
		public Expression RecoverBreath;

		// Token: 0x040048F5 RID: 18677
		public Expression Sick;

		// Token: 0x040048F6 RID: 18678
		public Expression SickSpores;

		// Token: 0x040048F7 RID: 18679
		public Expression Zombie;

		// Token: 0x040048F8 RID: 18680
		public Expression SickFierySkin;

		// Token: 0x040048F9 RID: 18681
		public Expression SickCold;

		// Token: 0x040048FA RID: 18682
		public Expression Pollen;

		// Token: 0x040048FB RID: 18683
		public Expression Relief;

		// Token: 0x040048FC RID: 18684
		public Expression Productive;

		// Token: 0x040048FD RID: 18685
		public Expression Determined;

		// Token: 0x040048FE RID: 18686
		public Expression Sticker;

		// Token: 0x040048FF RID: 18687
		public Expression Balloon;

		// Token: 0x04004900 RID: 18688
		public Expression Sparkle;

		// Token: 0x04004901 RID: 18689
		public Expression Music;

		// Token: 0x04004902 RID: 18690
		public Expression Tickled;

		// Token: 0x04004903 RID: 18691
		public Expression Radiation1;

		// Token: 0x04004904 RID: 18692
		public Expression Radiation2;

		// Token: 0x04004905 RID: 18693
		public Expression Radiation3;

		// Token: 0x04004906 RID: 18694
		public Expression Radiation4;
	}
}
