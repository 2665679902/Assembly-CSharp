using System;

namespace Database
{
	// Token: 0x02000C97 RID: 3223
	public class Faces : ResourceSet<Face>
	{
		// Token: 0x06006599 RID: 26009 RVA: 0x0026B498 File Offset: 0x00269698
		public Faces()
		{
			this.Neutral = base.Add(new Face("Neutral", null));
			this.Happy = base.Add(new Face("Happy", null));
			this.Uncomfortable = base.Add(new Face("Uncomfortable", null));
			this.Cold = base.Add(new Face("Cold", null));
			this.Hot = base.Add(new Face("Hot", "headfx_sweat"));
			this.Tired = base.Add(new Face("Tired", null));
			this.Sleep = base.Add(new Face("Sleep", null));
			this.Hungry = base.Add(new Face("Hungry", null));
			this.Angry = base.Add(new Face("Angry", null));
			this.Suffocate = base.Add(new Face("Suffocate", null));
			this.Sick = base.Add(new Face("Sick", "headfx_sick"));
			this.SickSpores = base.Add(new Face("Spores", "headfx_spores"));
			this.Zombie = base.Add(new Face("Zombie", null));
			this.SickFierySkin = base.Add(new Face("Fiery", "headfx_fiery"));
			this.SickCold = base.Add(new Face("SickCold", "headfx_sickcold"));
			this.Pollen = base.Add(new Face("Pollen", "headfx_pollen"));
			this.Dead = base.Add(new Face("Death", null));
			this.Productive = base.Add(new Face("Productive", null));
			this.Determined = base.Add(new Face("Determined", null));
			this.Sticker = base.Add(new Face("Sticker", null));
			this.Sparkle = base.Add(new Face("Sparkle", null));
			this.Balloon = base.Add(new Face("Balloon", null));
			this.Tickled = base.Add(new Face("Tickled", null));
			this.Music = base.Add(new Face("Music", null));
			this.Radiation1 = base.Add(new Face("Radiation1", "headfx_radiation1"));
			this.Radiation2 = base.Add(new Face("Radiation2", "headfx_radiation2"));
			this.Radiation3 = base.Add(new Face("Radiation3", "headfx_radiation3"));
			this.Radiation4 = base.Add(new Face("Radiation4", "headfx_radiation4"));
		}

		// Token: 0x04004907 RID: 18695
		public Face Neutral;

		// Token: 0x04004908 RID: 18696
		public Face Happy;

		// Token: 0x04004909 RID: 18697
		public Face Uncomfortable;

		// Token: 0x0400490A RID: 18698
		public Face Cold;

		// Token: 0x0400490B RID: 18699
		public Face Hot;

		// Token: 0x0400490C RID: 18700
		public Face Tired;

		// Token: 0x0400490D RID: 18701
		public Face Sleep;

		// Token: 0x0400490E RID: 18702
		public Face Hungry;

		// Token: 0x0400490F RID: 18703
		public Face Angry;

		// Token: 0x04004910 RID: 18704
		public Face Suffocate;

		// Token: 0x04004911 RID: 18705
		public Face Dead;

		// Token: 0x04004912 RID: 18706
		public Face Sick;

		// Token: 0x04004913 RID: 18707
		public Face SickSpores;

		// Token: 0x04004914 RID: 18708
		public Face Zombie;

		// Token: 0x04004915 RID: 18709
		public Face SickFierySkin;

		// Token: 0x04004916 RID: 18710
		public Face SickCold;

		// Token: 0x04004917 RID: 18711
		public Face Pollen;

		// Token: 0x04004918 RID: 18712
		public Face Productive;

		// Token: 0x04004919 RID: 18713
		public Face Determined;

		// Token: 0x0400491A RID: 18714
		public Face Sticker;

		// Token: 0x0400491B RID: 18715
		public Face Balloon;

		// Token: 0x0400491C RID: 18716
		public Face Sparkle;

		// Token: 0x0400491D RID: 18717
		public Face Tickled;

		// Token: 0x0400491E RID: 18718
		public Face Music;

		// Token: 0x0400491F RID: 18719
		public Face Radiation1;

		// Token: 0x04004920 RID: 18720
		public Face Radiation2;

		// Token: 0x04004921 RID: 18721
		public Face Radiation3;

		// Token: 0x04004922 RID: 18722
		public Face Radiation4;
	}
}
