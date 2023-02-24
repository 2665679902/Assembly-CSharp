using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x0200068A RID: 1674
public readonly struct ClothingOutfitTarget : IEquatable<ClothingOutfitTarget>
{
	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000EDD2B File Offset: 0x000EBF2B
	public string Id
	{
		get
		{
			return this.impl.OutfitId;
		}
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x000EDD38 File Offset: 0x000EBF38
	public string[] ReadItems()
	{
		return this.impl.ReadItems().Where(new Func<string, bool>(ClothingOutfitTarget.DoesClothingItemExist)).ToArray<string>();
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x000EDD5B File Offset: 0x000EBF5B
	public void WriteItems(string[] items)
	{
		this.impl.WriteItems(items);
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000EDD69 File Offset: 0x000EBF69
	public bool CanWriteItems
	{
		get
		{
			return this.impl.CanWriteItems;
		}
	}

	// Token: 0x06002D3F RID: 11583 RVA: 0x000EDD76 File Offset: 0x000EBF76
	public string ReadName()
	{
		return this.impl.ReadName();
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x000EDD83 File Offset: 0x000EBF83
	public void WriteName(string name)
	{
		this.impl.WriteName(name);
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000EDD91 File Offset: 0x000EBF91
	public bool CanWriteName
	{
		get
		{
			return this.impl.CanWriteName;
		}
	}

	// Token: 0x06002D42 RID: 11586 RVA: 0x000EDD9E File Offset: 0x000EBF9E
	public void Delete()
	{
		this.impl.Delete();
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000EDDAB File Offset: 0x000EBFAB
	public bool CanDelete
	{
		get
		{
			return this.impl.CanDelete;
		}
	}

	// Token: 0x06002D44 RID: 11588 RVA: 0x000EDDB8 File Offset: 0x000EBFB8
	public bool DoesExist()
	{
		return this.impl.DoesExist();
	}

	// Token: 0x06002D45 RID: 11589 RVA: 0x000EDDC5 File Offset: 0x000EBFC5
	public ClothingOutfitTarget(ClothingOutfitTarget.Implementation impl)
	{
		this.impl = impl;
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x000EDDCE File Offset: 0x000EBFCE
	public bool DoesContainNonOwnedItems()
	{
		return ClothingOutfitTarget.DoesContainNonOwnedItems(this.ReadItems());
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x000EDDDC File Offset: 0x000EBFDC
	public static bool DoesContainNonOwnedItems(IList<string> itemIds)
	{
		foreach (string text in itemIds)
		{
			PermitResource permitResource = Db.Get().Permits.TryGet(text);
			if (permitResource != null && permitResource.IsOwnable() && PermitItems.GetOwnedCount(permitResource) <= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x000EDE4C File Offset: 0x000EC04C
	public IEnumerable<ClothingItemResource> ReadItemValues()
	{
		return from i in this.ReadItems()
			select Db.Get().Permits.ClothingItems.Get(i);
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x000EDE78 File Offset: 0x000EC078
	public static bool DoesClothingItemExist(string clothingItemId)
	{
		return !Db.Get().Permits.ClothingItems.TryGet(clothingItemId).IsNullOrDestroyed();
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x000EDE97 File Offset: 0x000EC097
	public bool Is<T>() where T : ClothingOutfitTarget.Implementation
	{
		return this.impl is T;
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x000EDEA8 File Offset: 0x000EC0A8
	public bool Is<T>(out T value) where T : ClothingOutfitTarget.Implementation
	{
		ClothingOutfitTarget.Implementation implementation = this.impl;
		if (implementation is T)
		{
			T t = (T)((object)implementation);
			value = t;
			return true;
		}
		value = default(T);
		return false;
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x000EDEDC File Offset: 0x000EC0DC
	public bool IsTemplateOutfit()
	{
		return this.Is<ClothingOutfitTarget.KleiAuthored>() || this.Is<ClothingOutfitTarget.UserAuthored>();
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x000EDEEE File Offset: 0x000EC0EE
	public static ClothingOutfitTarget ForNewOutfit()
	{
		return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthored(ClothingOutfitTarget.GetUniqueNameIdFrom(UI.OUTFIT_NAME.NEW)));
	}

	// Token: 0x06002D4E RID: 11598 RVA: 0x000EDF0E File Offset: 0x000EC10E
	public static ClothingOutfitTarget ForNewOutfit(string id)
	{
		if (ClothingOutfitTarget.DoesExist(id))
		{
			throw new ArgumentException("Can not create a new target with id " + id + ", an outfit with that id already exists");
		}
		return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthored(id));
	}

	// Token: 0x06002D4F RID: 11599 RVA: 0x000EDF3E File Offset: 0x000EC13E
	public static ClothingOutfitTarget ForCopyOf(ClothingOutfitTarget sourceTarget)
	{
		return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthored(ClothingOutfitTarget.GetUniqueNameIdFrom(UI.OUTFIT_NAME.COPY_OF.Replace("{OutfitName}", sourceTarget.ReadName()))));
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x000EDF6A File Offset: 0x000EC16A
	public static ClothingOutfitTarget FromMinion(GameObject minionInstance)
	{
		return new ClothingOutfitTarget(new ClothingOutfitTarget.MinionInstance(minionInstance));
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x000EDF7C File Offset: 0x000EC17C
	public static ClothingOutfitTarget FromId(string outfitId)
	{
		return ClothingOutfitTarget.TryFromId(outfitId).Value;
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x000EDF98 File Offset: 0x000EC198
	public static Option<ClothingOutfitTarget> TryFromId(string outfitId)
	{
		if (outfitId == null)
		{
			return Option.None;
		}
		if (CustomClothingOutfits.Instance.OutfitData.CustomOutfits.ContainsKey(outfitId))
		{
			return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthored(outfitId));
		}
		if (Db.Get().Permits.ClothingOutfits.TryGet(outfitId) != null)
		{
			return new ClothingOutfitTarget(new ClothingOutfitTarget.KleiAuthored(outfitId));
		}
		return Option.None;
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x000EE017 File Offset: 0x000EC217
	public static bool DoesExist(string outfitId)
	{
		return Db.Get().Permits.ClothingOutfits.TryGet(outfitId) != null || CustomClothingOutfits.Instance.OutfitData.CustomOutfits.ContainsKey(outfitId);
	}

	// Token: 0x06002D54 RID: 11604 RVA: 0x000EE04C File Offset: 0x000EC24C
	public static IEnumerable<ClothingOutfitTarget> GetAll()
	{
		foreach (ClothingOutfitResource clothingOutfitResource in Db.Get().Permits.ClothingOutfits.resources)
		{
			yield return new ClothingOutfitTarget(new ClothingOutfitTarget.KleiAuthored(clothingOutfitResource));
		}
		List<ClothingOutfitResource>.Enumerator enumerator = default(List<ClothingOutfitResource>.Enumerator);
		foreach (KeyValuePair<string, string[]> keyValuePair in CustomClothingOutfits.Instance.OutfitData.CustomOutfits)
		{
			string text;
			string[] array;
			keyValuePair.Deconstruct(out text, out array);
			string text2 = text;
			yield return new ClothingOutfitTarget(new ClothingOutfitTarget.UserAuthored(text2));
		}
		Dictionary<string, string[]>.Enumerator enumerator2 = default(Dictionary<string, string[]>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x000EE055 File Offset: 0x000EC255
	public static ClothingOutfitTarget GetRandom()
	{
		return ClothingOutfitTarget.GetAll().GetRandom<ClothingOutfitTarget>();
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x000EE064 File Offset: 0x000EC264
	public static string GetUniqueNameIdFrom(string preferredName)
	{
		if (!ClothingOutfitTarget.DoesExist(preferredName))
		{
			return preferredName;
		}
		string text = "testOutfit";
		string text2 = UI.OUTFIT_NAME.RESOLVE_CONFLICT.Replace("{OutfitName}", text).Replace("{ConflictNumber}", 1.ToString());
		string text3 = UI.OUTFIT_NAME.RESOLVE_CONFLICT.Replace("{OutfitName}", text).Replace("{ConflictNumber}", 2.ToString());
		string text4;
		if (text2 != text3)
		{
			text4 = UI.OUTFIT_NAME.RESOLVE_CONFLICT;
		}
		else
		{
			text4 = "{OutfitName} ({ConflictNumber})";
		}
		for (int i = 1; i < 10000; i++)
		{
			string text5 = text4.Replace("{OutfitName}", preferredName).Replace("{ConflictNumber}", i.ToString());
			if (!ClothingOutfitTarget.DoesExist(text5))
			{
				return text5;
			}
		}
		throw new Exception("Couldn't get a unique name for preferred name: " + preferredName);
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x000EE132 File Offset: 0x000EC332
	public static bool operator ==(ClothingOutfitTarget a, ClothingOutfitTarget b)
	{
		return a.Equals(b);
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x000EE13C File Offset: 0x000EC33C
	public static bool operator !=(ClothingOutfitTarget a, ClothingOutfitTarget b)
	{
		return !a.Equals(b);
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x000EE14C File Offset: 0x000EC34C
	public override bool Equals(object obj)
	{
		if (obj is ClothingOutfitTarget)
		{
			ClothingOutfitTarget clothingOutfitTarget = (ClothingOutfitTarget)obj;
			return this.Equals(clothingOutfitTarget);
		}
		return false;
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x000EE171 File Offset: 0x000EC371
	public bool Equals(ClothingOutfitTarget other)
	{
		if (this.impl == null || other.impl == null)
		{
			return this.impl == null == (other.impl == null);
		}
		return this.Id == other.Id;
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x000EE1AA File Offset: 0x000EC3AA
	public override int GetHashCode()
	{
		return Hash.SDBMLower(this.impl.OutfitId);
	}

	// Token: 0x04001AF1 RID: 6897
	public readonly ClothingOutfitTarget.Implementation impl;

	// Token: 0x02001350 RID: 4944
	public interface Implementation
	{
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06007D48 RID: 32072
		string OutfitId { get; }

		// Token: 0x06007D49 RID: 32073
		string[] ReadItems();

		// Token: 0x06007D4A RID: 32074
		void WriteItems(string[] items);

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06007D4B RID: 32075
		bool CanWriteItems { get; }

		// Token: 0x06007D4C RID: 32076
		string ReadName();

		// Token: 0x06007D4D RID: 32077
		void WriteName(string name);

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06007D4E RID: 32078
		bool CanWriteName { get; }

		// Token: 0x06007D4F RID: 32079
		void Delete();

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06007D50 RID: 32080
		bool CanDelete { get; }

		// Token: 0x06007D51 RID: 32081
		bool DoesExist();
	}

	// Token: 0x02001351 RID: 4945
	public readonly struct MinionInstance : ClothingOutfitTarget.Implementation
	{
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06007D52 RID: 32082 RVA: 0x002D3DB2 File Offset: 0x002D1FB2
		public bool CanWriteItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06007D53 RID: 32083 RVA: 0x002D3DB5 File Offset: 0x002D1FB5
		public bool CanWriteName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06007D54 RID: 32084 RVA: 0x002D3DB8 File Offset: 0x002D1FB8
		public bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x002D3DBB File Offset: 0x002D1FBB
		public bool DoesExist()
		{
			return !this.minionInstance.IsNullOrDestroyed();
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06007D56 RID: 32086 RVA: 0x002D3DCC File Offset: 0x002D1FCC
		public string OutfitId
		{
			get
			{
				return this.minionInstance.GetInstanceID().ToString() + "_outfit";
			}
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x002D3DF6 File Offset: 0x002D1FF6
		public MinionInstance(GameObject minionInstance)
		{
			this.minionInstance = minionInstance;
			this.accessorizer = minionInstance.GetComponent<WearableAccessorizer>();
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x002D3E0B File Offset: 0x002D200B
		public string[] ReadItems()
		{
			return this.accessorizer.GetClothingItemIds();
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x002D3E18 File Offset: 0x002D2018
		public void WriteItems(string[] items)
		{
			this.accessorizer.ApplyClothingItems(items.Select((string i) => Db.Get().Permits.ClothingItems.Get(i)));
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x002D3E4A File Offset: 0x002D204A
		public string ReadName()
		{
			return UI.OUTFIT_NAME.MINIONS_OUTFIT.Replace("{MinionName}", this.minionInstance.GetProperName());
		}

		// Token: 0x06007D5B RID: 32091 RVA: 0x002D3E66 File Offset: 0x002D2066
		public void WriteName(string name)
		{
			throw new InvalidOperationException("Can not change change the outfit id for a minion instance");
		}

		// Token: 0x06007D5C RID: 32092 RVA: 0x002D3E72 File Offset: 0x002D2072
		public void Delete()
		{
			throw new InvalidOperationException("Can not delete a minion instance outfit");
		}

		// Token: 0x0400602E RID: 24622
		public readonly GameObject minionInstance;

		// Token: 0x0400602F RID: 24623
		public readonly WearableAccessorizer accessorizer;
	}

	// Token: 0x02001352 RID: 4946
	public readonly struct UserAuthored : ClothingOutfitTarget.Implementation
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06007D5D RID: 32093 RVA: 0x002D3E7E File Offset: 0x002D207E
		public bool CanWriteItems
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06007D5E RID: 32094 RVA: 0x002D3E81 File Offset: 0x002D2081
		public bool CanWriteName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06007D5F RID: 32095 RVA: 0x002D3E84 File Offset: 0x002D2084
		public bool CanDelete
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x002D3E87 File Offset: 0x002D2087
		public bool DoesExist()
		{
			return CustomClothingOutfits.Instance.OutfitData.CustomOutfits.ContainsKey(this.OutfitId);
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06007D61 RID: 32097 RVA: 0x002D3EA3 File Offset: 0x002D20A3
		public string OutfitId
		{
			get
			{
				return this.m_outfitId[0];
			}
		}

		// Token: 0x06007D62 RID: 32098 RVA: 0x002D3EAD File Offset: 0x002D20AD
		public UserAuthored(string outfitId)
		{
			this.m_outfitId = new string[] { outfitId };
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x002D3EC0 File Offset: 0x002D20C0
		public string[] ReadItems()
		{
			string[] array;
			if (CustomClothingOutfits.Instance.OutfitData.CustomOutfits.TryGetValue(this.OutfitId, out array))
			{
				return array;
			}
			return ClothingOutfitTargetExtensions.NO_ITEMS;
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x002D3EF2 File Offset: 0x002D20F2
		public void WriteItems(string[] items)
		{
			CustomClothingOutfits.Instance.Internal_EditOutfit(this.OutfitId, items);
		}

		// Token: 0x06007D65 RID: 32101 RVA: 0x002D3F05 File Offset: 0x002D2105
		public string ReadName()
		{
			return this.OutfitId;
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x002D3F10 File Offset: 0x002D2110
		public void WriteName(string name)
		{
			if (this.OutfitId == name)
			{
				return;
			}
			if (ClothingOutfitTarget.DoesExist(name))
			{
				throw new Exception(string.Concat(new string[] { "Can not change outfit name from \"", this.OutfitId, "\" to \"", name, "\", \"", name, "\" already exists" }));
			}
			if (CustomClothingOutfits.Instance.OutfitData.CustomOutfits.ContainsKey(this.OutfitId))
			{
				CustomClothingOutfits.Instance.Internal_RenameOutfit(this.OutfitId, name);
			}
			else
			{
				CustomClothingOutfits.Instance.Internal_EditOutfit(name, ClothingOutfitTargetExtensions.NO_ITEMS);
			}
			this.m_outfitId[0] = name;
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x002D3FBE File Offset: 0x002D21BE
		public void Delete()
		{
			CustomClothingOutfits.Instance.Internal_RemoveOutfit(this.OutfitId);
		}

		// Token: 0x04006030 RID: 24624
		private readonly string[] m_outfitId;
	}

	// Token: 0x02001353 RID: 4947
	public readonly struct KleiAuthored : ClothingOutfitTarget.Implementation
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06007D68 RID: 32104 RVA: 0x002D3FD0 File Offset: 0x002D21D0
		public bool CanWriteItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06007D69 RID: 32105 RVA: 0x002D3FD3 File Offset: 0x002D21D3
		public bool CanWriteName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06007D6A RID: 32106 RVA: 0x002D3FD6 File Offset: 0x002D21D6
		public bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x002D3FD9 File Offset: 0x002D21D9
		public bool DoesExist()
		{
			return true;
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x002D3FDC File Offset: 0x002D21DC
		public string OutfitId
		{
			get
			{
				return this.m_outfitId;
			}
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x002D3FE4 File Offset: 0x002D21E4
		public KleiAuthored(string outfitId)
		{
			this.m_outfitId = outfitId;
			this.resource = Db.Get().Permits.ClothingOutfits.Get(outfitId);
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x002D4008 File Offset: 0x002D2208
		public KleiAuthored(ClothingOutfitResource outfit)
		{
			this.m_outfitId = outfit.Id;
			this.resource = outfit;
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x002D401D File Offset: 0x002D221D
		public string[] ReadItems()
		{
			return this.resource.itemsInOutfit;
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x002D402A File Offset: 0x002D222A
		public void WriteItems(string[] items)
		{
			throw new InvalidOperationException("Can not set items on a Db authored outfit");
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x002D4036 File Offset: 0x002D2236
		public string ReadName()
		{
			return this.resource.Name;
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x002D4043 File Offset: 0x002D2243
		public void WriteName(string name)
		{
			throw new InvalidOperationException("Can not set name on a Db authored outfit");
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x002D404F File Offset: 0x002D224F
		public void Delete()
		{
			throw new InvalidOperationException("Can not delete a Db authored outfit");
		}

		// Token: 0x04006031 RID: 24625
		public readonly ClothingOutfitResource resource;

		// Token: 0x04006032 RID: 24626
		private readonly string m_outfitId;
	}
}
