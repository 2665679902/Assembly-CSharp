using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000CA6 RID: 3238
	public class Personalities : ResourceSet<Personality>
	{
		// Token: 0x060065C5 RID: 26053 RVA: 0x0026E7FC File Offset: 0x0026C9FC
		public Personalities()
		{
			foreach (Personalities.PersonalityInfo personalityInfo in AsyncLoadManager<IGlobalAsyncLoader>.AsyncLoader<Personalities.PersonalityLoader>.Get().entries)
			{
				Personality personality = new Personality(personalityInfo.Name.ToUpper(), Strings.Get(string.Format("STRINGS.DUPLICANTS.PERSONALITIES.{0}.NAME", personalityInfo.Name.ToUpper())), personalityInfo.Gender.ToUpper(), personalityInfo.PersonalityType, personalityInfo.StressTrait, personalityInfo.JoyTrait, personalityInfo.StickerType, personalityInfo.CongenitalTrait, personalityInfo.HeadShape, personalityInfo.Mouth, personalityInfo.Neck, personalityInfo.Eyes, personalityInfo.Hair, personalityInfo.Body, personalityInfo.Belt, personalityInfo.Cuff, personalityInfo.Foot, personalityInfo.Hand, personalityInfo.Pelvis, personalityInfo.Leg, Strings.Get(string.Format("STRINGS.DUPLICANTS.PERSONALITIES.{0}.DESC", personalityInfo.Name.ToUpper())), personalityInfo.ValidStarter);
				base.Add(personality);
			}
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x0026E900 File Offset: 0x0026CB00
		private void AddTrait(Personality personality, string trait_name)
		{
			Trait trait = Db.Get().traits.TryGet(trait_name);
			if (trait != null)
			{
				personality.AddTrait(trait);
			}
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x0026E928 File Offset: 0x0026CB28
		private void SetAttribute(Personality personality, string attribute_name, int value)
		{
			Klei.AI.Attribute attribute = Db.Get().Attributes.TryGet(attribute_name);
			if (attribute == null)
			{
				Debug.LogWarning("Attribute does not exist: " + attribute_name);
				return;
			}
			personality.SetAttribute(attribute, value);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0026E962 File Offset: 0x0026CB62
		public List<Personality> GetStartingPersonalities()
		{
			return this.resources.FindAll((Personality x) => x.startingMinion);
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x0026E990 File Offset: 0x0026CB90
		public List<Personality> GetAll(bool onlyEnabledMinions, bool onlyStartingMinions)
		{
			return this.resources.FindAll((Personality x) => (!onlyStartingMinions || x.startingMinion) && (!onlyEnabledMinions || !x.Disabled));
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x0026E9C8 File Offset: 0x0026CBC8
		public Personality GetRandom(bool onlyEnabledMinions, bool onlyStartingMinions)
		{
			return this.GetAll(onlyEnabledMinions, onlyStartingMinions).GetRandom<Personality>();
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x0026E9D8 File Offset: 0x0026CBD8
		public Personality GetPersonalityFromNameStringKey(string name_string_key)
		{
			foreach (Personality personality in Db.Get().Personalities.resources)
			{
				if (personality.nameStringKey.Equals(name_string_key, StringComparison.CurrentCultureIgnoreCase))
				{
					return personality;
				}
			}
			return null;
		}

		// Token: 0x02001B2A RID: 6954
		public class PersonalityLoader : AsyncCsvLoader<Personalities.PersonalityLoader, Personalities.PersonalityInfo>
		{
			// Token: 0x060095B3 RID: 38323 RVA: 0x00321B00 File Offset: 0x0031FD00
			public PersonalityLoader()
				: base(Assets.instance.personalitiesFile)
			{
			}

			// Token: 0x060095B4 RID: 38324 RVA: 0x00321B12 File Offset: 0x0031FD12
			public override void Run()
			{
				base.Run();
			}
		}

		// Token: 0x02001B2B RID: 6955
		public class PersonalityInfo : Resource
		{
			// Token: 0x04007AB0 RID: 31408
			public int HeadShape;

			// Token: 0x04007AB1 RID: 31409
			public int Mouth;

			// Token: 0x04007AB2 RID: 31410
			public int Neck;

			// Token: 0x04007AB3 RID: 31411
			public int Eyes;

			// Token: 0x04007AB4 RID: 31412
			public int Hair;

			// Token: 0x04007AB5 RID: 31413
			public int Body;

			// Token: 0x04007AB6 RID: 31414
			public int Belt;

			// Token: 0x04007AB7 RID: 31415
			public int Cuff;

			// Token: 0x04007AB8 RID: 31416
			public int Foot;

			// Token: 0x04007AB9 RID: 31417
			public int Hand;

			// Token: 0x04007ABA RID: 31418
			public int Pelvis;

			// Token: 0x04007ABB RID: 31419
			public int Leg;

			// Token: 0x04007ABC RID: 31420
			public string Gender;

			// Token: 0x04007ABD RID: 31421
			public string PersonalityType;

			// Token: 0x04007ABE RID: 31422
			public string StressTrait;

			// Token: 0x04007ABF RID: 31423
			public string JoyTrait;

			// Token: 0x04007AC0 RID: 31424
			public string StickerType;

			// Token: 0x04007AC1 RID: 31425
			public string CongenitalTrait;

			// Token: 0x04007AC2 RID: 31426
			public string Design;

			// Token: 0x04007AC3 RID: 31427
			public bool ValidStarter;
		}
	}
}
