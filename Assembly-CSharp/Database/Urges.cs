using System;

namespace Database
{
	// Token: 0x02000CBF RID: 3263
	public class Urges : ResourceSet<Urge>
	{
		// Token: 0x0600661E RID: 26142 RVA: 0x002754E4 File Offset: 0x002736E4
		public Urges()
		{
			this.HealCritical = base.Add(new Urge("HealCritical"));
			this.BeIncapacitated = base.Add(new Urge("BeIncapacitated"));
			this.PacifyEat = base.Add(new Urge("PacifyEat"));
			this.PacifySleep = base.Add(new Urge("PacifySleep"));
			this.PacifyIdle = base.Add(new Urge("PacifyIdle"));
			this.EmoteHighPriority = base.Add(new Urge("EmoteHighPriority"));
			this.RecoverBreath = base.Add(new Urge("RecoverBreath"));
			this.Aggression = base.Add(new Urge("Aggression"));
			this.MoveToQuarantine = base.Add(new Urge("MoveToQuarantine"));
			this.WashHands = base.Add(new Urge("WashHands"));
			this.Shower = base.Add(new Urge("Shower"));
			this.Eat = base.Add(new Urge("Eat"));
			this.Pee = base.Add(new Urge("Pee"));
			this.RestDueToDisease = base.Add(new Urge("RestDueToDisease"));
			this.Sleep = base.Add(new Urge("Sleep"));
			this.Narcolepsy = base.Add(new Urge("Narcolepsy"));
			this.Doctor = base.Add(new Urge("Doctor"));
			this.Heal = base.Add(new Urge("Heal"));
			this.Feed = base.Add(new Urge("Feed"));
			this.PacifyRelocate = base.Add(new Urge("PacifyRelocate"));
			this.Emote = base.Add(new Urge("Emote"));
			this.MoveToSafety = base.Add(new Urge("MoveToSafety"));
			this.WarmUp = base.Add(new Urge("WarmUp"));
			this.CoolDown = base.Add(new Urge("CoolDown"));
			this.LearnSkill = base.Add(new Urge("LearnSkill"));
			this.EmoteIdle = base.Add(new Urge("EmoteIdle"));
		}

		// Token: 0x04004A9D RID: 19101
		public Urge BeIncapacitated;

		// Token: 0x04004A9E RID: 19102
		public Urge Sleep;

		// Token: 0x04004A9F RID: 19103
		public Urge Narcolepsy;

		// Token: 0x04004AA0 RID: 19104
		public Urge Eat;

		// Token: 0x04004AA1 RID: 19105
		public Urge WashHands;

		// Token: 0x04004AA2 RID: 19106
		public Urge Shower;

		// Token: 0x04004AA3 RID: 19107
		public Urge Pee;

		// Token: 0x04004AA4 RID: 19108
		public Urge MoveToQuarantine;

		// Token: 0x04004AA5 RID: 19109
		public Urge HealCritical;

		// Token: 0x04004AA6 RID: 19110
		public Urge RecoverBreath;

		// Token: 0x04004AA7 RID: 19111
		public Urge Emote;

		// Token: 0x04004AA8 RID: 19112
		public Urge Feed;

		// Token: 0x04004AA9 RID: 19113
		public Urge Doctor;

		// Token: 0x04004AAA RID: 19114
		public Urge Flee;

		// Token: 0x04004AAB RID: 19115
		public Urge Heal;

		// Token: 0x04004AAC RID: 19116
		public Urge PacifyIdle;

		// Token: 0x04004AAD RID: 19117
		public Urge PacifyEat;

		// Token: 0x04004AAE RID: 19118
		public Urge PacifySleep;

		// Token: 0x04004AAF RID: 19119
		public Urge PacifyRelocate;

		// Token: 0x04004AB0 RID: 19120
		public Urge RestDueToDisease;

		// Token: 0x04004AB1 RID: 19121
		public Urge EmoteHighPriority;

		// Token: 0x04004AB2 RID: 19122
		public Urge Aggression;

		// Token: 0x04004AB3 RID: 19123
		public Urge MoveToSafety;

		// Token: 0x04004AB4 RID: 19124
		public Urge WarmUp;

		// Token: 0x04004AB5 RID: 19125
		public Urge CoolDown;

		// Token: 0x04004AB6 RID: 19126
		public Urge LearnSkill;

		// Token: 0x04004AB7 RID: 19127
		public Urge EmoteIdle;
	}
}
