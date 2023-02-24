using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000857 RID: 2135
public class Personality : Resource
{
	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x06003D4E RID: 15694 RVA: 0x00156E9B File Offset: 0x0015509B
	public string description
	{
		get
		{
			return this.GetDescription();
		}
	}

	// Token: 0x06003D4F RID: 15695 RVA: 0x00156EA4 File Offset: 0x001550A4
	[Obsolete("Modders: Use constructor with isStartingMinion parameter")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, string description)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, 0, 0, 0, 0, 0, 0, description, true)
	{
	}

	// Token: 0x06003D50 RID: 15696 RVA: 0x00156EDC File Offset: 0x001550DC
	[Obsolete("Modders: Added additional body part customization to duplicant personalities")]
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, string description, bool isStartingMinion)
		: this(name_string_key, name, Gender, PersonalityType, StressTrait, JoyTrait, StickerType, CongenitalTrait, headShape, mouth, neck, eyes, hair, body, 0, 0, 0, 0, 0, 0, description, true)
	{
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x00156F14 File Offset: 0x00155114
	public Personality(string name_string_key, string name, string Gender, string PersonalityType, string StressTrait, string JoyTrait, string StickerType, string CongenitalTrait, int headShape, int mouth, int neck, int eyes, int hair, int body, int belt, int cuff, int foot, int hand, int pelvis, int leg, string description, bool isStartingMinion)
		: base(name_string_key, name)
	{
		this.nameStringKey = name_string_key;
		this.genderStringKey = Gender;
		this.personalityType = PersonalityType;
		this.stresstrait = StressTrait;
		this.joyTrait = JoyTrait;
		this.stickerType = StickerType;
		this.congenitaltrait = CongenitalTrait;
		this.unformattedDescription = description;
		this.headShape = headShape;
		this.mouth = mouth;
		this.neck = neck;
		this.eyes = eyes;
		this.hair = hair;
		this.body = body;
		this.belt = belt;
		this.cuff = cuff;
		this.foot = foot;
		this.hand = hand;
		this.pelvis = pelvis;
		this.leg = leg;
		this.startingMinion = isStartingMinion;
		this.outfitIds = new Dictionary<ClothingOutfitUtility.OutfitType, string>();
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x00156FF0 File Offset: 0x001551F0
	public string GetDescription()
	{
		this.unformattedDescription = this.unformattedDescription.Replace("{0}", this.Name);
		return this.unformattedDescription;
	}

	// Token: 0x06003D53 RID: 15699 RVA: 0x00157014 File Offset: 0x00155214
	public void SetAttribute(Klei.AI.Attribute attribute, int value)
	{
		Personality.StartingAttribute startingAttribute = new Personality.StartingAttribute(attribute, value);
		this.attributes.Add(startingAttribute);
	}

	// Token: 0x06003D54 RID: 15700 RVA: 0x00157035 File Offset: 0x00155235
	public void AddTrait(Trait trait)
	{
		this.traits.Add(trait);
	}

	// Token: 0x06003D55 RID: 15701 RVA: 0x00157043 File Offset: 0x00155243
	public void SetOutfit(ClothingOutfitUtility.OutfitType outfitType, Option<string> outfit)
	{
		Db.Get().Permits.ClothingOutfits.SetDuplicantPersonalityOutfit(this.Id, outfit, outfitType);
	}

	// Token: 0x06003D56 RID: 15702 RVA: 0x00157061 File Offset: 0x00155261
	public void Internal_SetOutfit(ClothingOutfitUtility.OutfitType outfitType, Option<string> outfit)
	{
		if (outfit.HasValue)
		{
			this.outfitIds[outfitType] = outfit.Unwrap();
			return;
		}
		this.outfitIds.Remove(outfitType);
	}

	// Token: 0x06003D57 RID: 15703 RVA: 0x0015708D File Offset: 0x0015528D
	public string GetOutfit(ClothingOutfitUtility.OutfitType outfitType)
	{
		if (this.outfitIds.ContainsKey(outfitType))
		{
			return this.outfitIds[outfitType];
		}
		return null;
	}

	// Token: 0x06003D58 RID: 15704 RVA: 0x001570AC File Offset: 0x001552AC
	public Sprite GetMiniIcon()
	{
		if (string.IsNullOrWhiteSpace(this.nameStringKey))
		{
			return Assets.GetSprite("unknown");
		}
		string text;
		if (this.nameStringKey == "MIMA")
		{
			text = "Mi-Ma";
		}
		else
		{
			text = this.nameStringKey[0].ToString() + this.nameStringKey.Substring(1).ToLower();
		}
		return Assets.GetSprite("dreamIcon_" + text);
	}

	// Token: 0x0400282C RID: 10284
	public List<Personality.StartingAttribute> attributes = new List<Personality.StartingAttribute>();

	// Token: 0x0400282D RID: 10285
	public List<Trait> traits = new List<Trait>();

	// Token: 0x0400282E RID: 10286
	public int headShape;

	// Token: 0x0400282F RID: 10287
	public int mouth;

	// Token: 0x04002830 RID: 10288
	public int neck;

	// Token: 0x04002831 RID: 10289
	public int eyes;

	// Token: 0x04002832 RID: 10290
	public int hair;

	// Token: 0x04002833 RID: 10291
	public int body;

	// Token: 0x04002834 RID: 10292
	public int belt;

	// Token: 0x04002835 RID: 10293
	public int cuff;

	// Token: 0x04002836 RID: 10294
	public int foot;

	// Token: 0x04002837 RID: 10295
	public int hand;

	// Token: 0x04002838 RID: 10296
	public int pelvis;

	// Token: 0x04002839 RID: 10297
	public int leg;

	// Token: 0x0400283A RID: 10298
	public Dictionary<ClothingOutfitUtility.OutfitType, string> outfitIds;

	// Token: 0x0400283B RID: 10299
	public string nameStringKey;

	// Token: 0x0400283C RID: 10300
	public string genderStringKey;

	// Token: 0x0400283D RID: 10301
	public string personalityType;

	// Token: 0x0400283E RID: 10302
	public string stresstrait;

	// Token: 0x0400283F RID: 10303
	public string joyTrait;

	// Token: 0x04002840 RID: 10304
	public string stickerType;

	// Token: 0x04002841 RID: 10305
	public string congenitaltrait;

	// Token: 0x04002842 RID: 10306
	public string unformattedDescription;

	// Token: 0x04002843 RID: 10307
	public bool startingMinion;

	// Token: 0x0200160F RID: 5647
	public class StartingAttribute
	{
		// Token: 0x060086AA RID: 34474 RVA: 0x002EF8E4 File Offset: 0x002EDAE4
		public StartingAttribute(Klei.AI.Attribute attribute, int value)
		{
			this.attribute = attribute;
			this.value = value;
		}

		// Token: 0x040068E5 RID: 26853
		public Klei.AI.Attribute attribute;

		// Token: 0x040068E6 RID: 26854
		public int value;
	}
}
