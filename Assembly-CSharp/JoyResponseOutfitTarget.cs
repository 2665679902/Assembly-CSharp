using System;
using UnityEngine;

// Token: 0x020007E8 RID: 2024
public readonly struct JoyResponseOutfitTarget
{
	// Token: 0x06003A53 RID: 14931 RVA: 0x001431A9 File Offset: 0x001413A9
	public JoyResponseOutfitTarget(JoyResponseOutfitTarget.Implementation impl)
	{
		this.impl = impl;
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x001431B2 File Offset: 0x001413B2
	public Option<string> ReadFacadeId()
	{
		return this.impl.ReadFacadeId();
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x001431BF File Offset: 0x001413BF
	public void WriteFacadeId(Option<string> facadeId)
	{
		this.impl.WriteFacadeId(facadeId);
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x001431CD File Offset: 0x001413CD
	public string GetMinionName()
	{
		return this.impl.GetMinionName();
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x001431DA File Offset: 0x001413DA
	public Personality GetPersonality()
	{
		return this.impl.GetPersonality();
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x001431E7 File Offset: 0x001413E7
	public static JoyResponseOutfitTarget FromMinion(GameObject minionInstance)
	{
		return new JoyResponseOutfitTarget(new JoyResponseOutfitTarget.MinionInstanceTarget(minionInstance));
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x001431F9 File Offset: 0x001413F9
	public static JoyResponseOutfitTarget FromPersonality(Personality personality)
	{
		return new JoyResponseOutfitTarget(new JoyResponseOutfitTarget.PersonalityTarget(personality));
	}

	// Token: 0x0400264B RID: 9803
	private readonly JoyResponseOutfitTarget.Implementation impl;

	// Token: 0x02001538 RID: 5432
	public interface Implementation
	{
		// Token: 0x060082FA RID: 33530
		Option<string> ReadFacadeId();

		// Token: 0x060082FB RID: 33531
		void WriteFacadeId(Option<string> permitId);

		// Token: 0x060082FC RID: 33532
		string GetMinionName();

		// Token: 0x060082FD RID: 33533
		Personality GetPersonality();
	}

	// Token: 0x02001539 RID: 5433
	public readonly struct MinionInstanceTarget : JoyResponseOutfitTarget.Implementation
	{
		// Token: 0x060082FE RID: 33534 RVA: 0x002E6C1C File Offset: 0x002E4E1C
		public MinionInstanceTarget(GameObject minionInstance)
		{
			this.minionInstance = minionInstance;
			this.wearableAccessorizer = minionInstance.GetComponent<WearableAccessorizer>();
		}

		// Token: 0x060082FF RID: 33535 RVA: 0x002E6C31 File Offset: 0x002E4E31
		public string GetMinionName()
		{
			return this.minionInstance.GetProperName();
		}

		// Token: 0x06008300 RID: 33536 RVA: 0x002E6C3E File Offset: 0x002E4E3E
		public Personality GetPersonality()
		{
			return Db.Get().Personalities.Get(this.minionInstance.GetComponent<MinionIdentity>().personalityResourceId);
		}

		// Token: 0x06008301 RID: 33537 RVA: 0x002E6C5F File Offset: 0x002E4E5F
		public Option<string> ReadFacadeId()
		{
			return this.wearableAccessorizer.GetJoyResponseId();
		}

		// Token: 0x06008302 RID: 33538 RVA: 0x002E6C6C File Offset: 0x002E4E6C
		public void WriteFacadeId(Option<string> permitId)
		{
			this.wearableAccessorizer.SetJoyResponseId(permitId);
		}

		// Token: 0x040065F4 RID: 26100
		public readonly GameObject minionInstance;

		// Token: 0x040065F5 RID: 26101
		public readonly WearableAccessorizer wearableAccessorizer;
	}

	// Token: 0x0200153A RID: 5434
	public readonly struct PersonalityTarget : JoyResponseOutfitTarget.Implementation
	{
		// Token: 0x06008303 RID: 33539 RVA: 0x002E6C7A File Offset: 0x002E4E7A
		public PersonalityTarget(Personality personality)
		{
			this.personality = personality;
		}

		// Token: 0x06008304 RID: 33540 RVA: 0x002E6C83 File Offset: 0x002E4E83
		public string GetMinionName()
		{
			return this.personality.Name;
		}

		// Token: 0x06008305 RID: 33541 RVA: 0x002E6C90 File Offset: 0x002E4E90
		public Personality GetPersonality()
		{
			return this.personality;
		}

		// Token: 0x06008306 RID: 33542 RVA: 0x002E6C98 File Offset: 0x002E4E98
		public Option<string> ReadFacadeId()
		{
			return this.personality.GetOutfit(ClothingOutfitUtility.OutfitType.JoyResponse);
		}

		// Token: 0x06008307 RID: 33543 RVA: 0x002E6CAB File Offset: 0x002E4EAB
		public void WriteFacadeId(Option<string> facadeId)
		{
			this.personality.SetOutfit(ClothingOutfitUtility.OutfitType.JoyResponse, facadeId);
		}

		// Token: 0x040065F6 RID: 26102
		public readonly Personality personality;
	}
}
