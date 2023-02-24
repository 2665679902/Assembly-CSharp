using System;
using System.Collections.Generic;

// Token: 0x02000686 RID: 1670
public class CustomClothingOutfits
{
	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000ED19A File Offset: 0x000EB39A
	public OutfitData OutfitData
	{
		get
		{
			return this.outfitData;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000ED1A2 File Offset: 0x000EB3A2
	public static CustomClothingOutfits Instance
	{
		get
		{
			if (CustomClothingOutfits._instance == null)
			{
				CustomClothingOutfits._instance = new CustomClothingOutfits();
			}
			return CustomClothingOutfits._instance;
		}
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x000ED1BA File Offset: 0x000EB3BA
	public void Internal_EditOutfit(string outfit_name, string[] outfit_items)
	{
		this.outfitData.CustomOutfits[outfit_name] = outfit_items;
		ClothingOutfitUtility.SaveClothingOutfitData();
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x000ED1D4 File Offset: 0x000EB3D4
	public void Internal_RenameOutfit(string old_outfit_name, string new_outfit_name)
	{
		if (!this.outfitData.CustomOutfits.ContainsKey(old_outfit_name))
		{
			throw new ArgumentException(string.Concat(new string[] { "Can't rename outfit \"", old_outfit_name, "\" to \"", new_outfit_name, "\": missing \"", old_outfit_name, "\" entry" }));
		}
		if (this.outfitData.CustomOutfits.ContainsKey(new_outfit_name))
		{
			throw new ArgumentException(string.Concat(new string[] { "Can't rename outfit \"", old_outfit_name, "\" to \"", new_outfit_name, "\": entry \"", new_outfit_name, "\" already exists" }));
		}
		this.outfitData.CustomOutfits.Add(new_outfit_name, this.outfitData.CustomOutfits[old_outfit_name]);
		foreach (KeyValuePair<string, Dictionary<ClothingOutfitUtility.OutfitType, string>> keyValuePair in this.outfitData.DuplicantOutfits)
		{
			string text;
			Dictionary<ClothingOutfitUtility.OutfitType, string> dictionary;
			keyValuePair.Deconstruct(out text, out dictionary);
			string text2 = text;
			Dictionary<ClothingOutfitUtility.OutfitType, string> dictionary2 = dictionary;
			if (dictionary2 != null)
			{
				using (ListPool<ClothingOutfitUtility.OutfitType, CustomClothingOutfits>.PooledList pooledList = PoolsFor<CustomClothingOutfits>.AllocateList<ClothingOutfitUtility.OutfitType>())
				{
					foreach (KeyValuePair<ClothingOutfitUtility.OutfitType, string> keyValuePair2 in dictionary2)
					{
						ClothingOutfitUtility.OutfitType outfitType;
						keyValuePair2.Deconstruct(out outfitType, out text);
						ClothingOutfitUtility.OutfitType outfitType2 = outfitType;
						if (text == old_outfit_name)
						{
							pooledList.Add(outfitType2);
						}
					}
					foreach (ClothingOutfitUtility.OutfitType outfitType3 in pooledList)
					{
						dictionary2[outfitType3] = new_outfit_name;
						Personality personalityFromNameStringKey = Db.Get().Personalities.GetPersonalityFromNameStringKey(text2);
						if (personalityFromNameStringKey.IsNullOrDestroyed())
						{
							DebugUtil.DevAssert(false, string.Concat(new string[] { "<Renaming Outfit Error> Couldn't find personality \"", text2, "\" to switch their outfit preference from \"", old_outfit_name, "\" to \"", new_outfit_name, "\"" }), null);
						}
						else
						{
							personalityFromNameStringKey.Internal_SetOutfit(outfitType3, new_outfit_name);
						}
					}
				}
			}
		}
		this.outfitData.CustomOutfits.Remove(old_outfit_name);
		ClothingOutfitUtility.SaveClothingOutfitData();
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x000ED46C File Offset: 0x000EB66C
	public void Internal_RemoveOutfit(string outfit_name)
	{
		if (this.outfitData.CustomOutfits.Remove(outfit_name))
		{
			foreach (KeyValuePair<string, Dictionary<ClothingOutfitUtility.OutfitType, string>> keyValuePair in this.outfitData.DuplicantOutfits)
			{
				string text;
				Dictionary<ClothingOutfitUtility.OutfitType, string> dictionary;
				keyValuePair.Deconstruct(out text, out dictionary);
				string text2 = text;
				Dictionary<ClothingOutfitUtility.OutfitType, string> dictionary2 = dictionary;
				if (dictionary2 != null)
				{
					using (ListPool<ClothingOutfitUtility.OutfitType, CustomClothingOutfits>.PooledList pooledList = PoolsFor<CustomClothingOutfits>.AllocateList<ClothingOutfitUtility.OutfitType>())
					{
						foreach (KeyValuePair<ClothingOutfitUtility.OutfitType, string> keyValuePair2 in dictionary2)
						{
							ClothingOutfitUtility.OutfitType outfitType;
							keyValuePair2.Deconstruct(out outfitType, out text);
							ClothingOutfitUtility.OutfitType outfitType2 = outfitType;
							if (text == outfit_name)
							{
								pooledList.Add(outfitType2);
							}
						}
						foreach (ClothingOutfitUtility.OutfitType outfitType3 in pooledList)
						{
							dictionary2.Remove(outfitType3);
							Personality personalityFromNameStringKey = Db.Get().Personalities.GetPersonalityFromNameStringKey(text2);
							if (personalityFromNameStringKey.IsNullOrDestroyed())
							{
								DebugUtil.DevAssert(false, "<Deleting Outfit Error> Couldn't find personality \"" + text2 + "\" to clear their outfit preference", null);
							}
							else
							{
								personalityFromNameStringKey.Internal_SetOutfit(outfitType3, Option.None);
							}
						}
					}
				}
			}
			ClothingOutfitUtility.SaveClothingOutfitData();
		}
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x000ED620 File Offset: 0x000EB820
	public void Internal_SetDuplicantPersonalityOutfit(string personalityId, Option<string> outfit_id, ClothingOutfitUtility.OutfitType outfit_type)
	{
		Dictionary<ClothingOutfitUtility.OutfitType, string> dictionary;
		if (outfit_id.HasValue)
		{
			if (!this.outfitData.DuplicantOutfits.ContainsKey(personalityId))
			{
				this.outfitData.DuplicantOutfits.Add(personalityId, new Dictionary<ClothingOutfitUtility.OutfitType, string>());
			}
			this.outfitData.DuplicantOutfits[personalityId][outfit_type] = outfit_id.Value;
		}
		else if (this.outfitData.DuplicantOutfits.TryGetValue(personalityId, out dictionary))
		{
			dictionary.Remove(outfit_type);
			if (dictionary.Count == 0)
			{
				this.outfitData.DuplicantOutfits.Remove(personalityId);
			}
		}
		ClothingOutfitUtility.SaveClothingOutfitData();
	}

	// Token: 0x04001AEA RID: 6890
	private OutfitData outfitData = new OutfitData();

	// Token: 0x04001AEB RID: 6891
	private static CustomClothingOutfits _instance;
}
