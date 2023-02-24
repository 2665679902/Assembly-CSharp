using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000897 RID: 2199
[SerializationConfig(MemberSerialization.OptIn)]
public class PlantSubSpeciesCatalog : KMonoBehaviour
{
	// Token: 0x06003F05 RID: 16133 RVA: 0x0015FFCA File Offset: 0x0015E1CA
	public static void DestroyInstance()
	{
		PlantSubSpeciesCatalog.Instance = null;
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x06003F06 RID: 16134 RVA: 0x0015FFD4 File Offset: 0x0015E1D4
	public bool AnyNonOriginalDiscovered
	{
		get
		{
			foreach (KeyValuePair<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> keyValuePair in this.discoveredSubspeciesBySpecies)
			{
				if (keyValuePair.Value.Find((PlantSubSpeciesCatalog.SubSpeciesInfo ss) => !ss.IsOriginal).IsValid)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06003F07 RID: 16135 RVA: 0x0016005C File Offset: 0x0015E25C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		PlantSubSpeciesCatalog.Instance = this;
	}

	// Token: 0x06003F08 RID: 16136 RVA: 0x0016006A File Offset: 0x0015E26A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.EnsureOriginalSubSpecies();
	}

	// Token: 0x06003F09 RID: 16137 RVA: 0x00160078 File Offset: 0x0015E278
	public List<Tag> GetAllDiscoveredSpecies()
	{
		return this.discoveredSubspeciesBySpecies.Keys.ToList<Tag>();
	}

	// Token: 0x06003F0A RID: 16138 RVA: 0x0016008C File Offset: 0x0015E28C
	public List<PlantSubSpeciesCatalog.SubSpeciesInfo> GetAllSubSpeciesForSpecies(Tag speciesID)
	{
		List<PlantSubSpeciesCatalog.SubSpeciesInfo> list;
		if (this.discoveredSubspeciesBySpecies.TryGetValue(speciesID, out list))
		{
			return list;
		}
		return null;
	}

	// Token: 0x06003F0B RID: 16139 RVA: 0x001600AC File Offset: 0x0015E2AC
	public bool GetOriginalSubSpecies(Tag speciesID, out PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo)
	{
		if (!this.discoveredSubspeciesBySpecies.ContainsKey(speciesID))
		{
			subSpeciesInfo = default(PlantSubSpeciesCatalog.SubSpeciesInfo);
			return false;
		}
		subSpeciesInfo = this.discoveredSubspeciesBySpecies[speciesID].Find((PlantSubSpeciesCatalog.SubSpeciesInfo i) => i.IsOriginal);
		return true;
	}

	// Token: 0x06003F0C RID: 16140 RVA: 0x00160108 File Offset: 0x0015E308
	public PlantSubSpeciesCatalog.SubSpeciesInfo GetSubSpecies(Tag speciesID, Tag subSpeciesID)
	{
		return this.discoveredSubspeciesBySpecies[speciesID].Find((PlantSubSpeciesCatalog.SubSpeciesInfo i) => i.ID == subSpeciesID);
	}

	// Token: 0x06003F0D RID: 16141 RVA: 0x00160140 File Offset: 0x0015E340
	public PlantSubSpeciesCatalog.SubSpeciesInfo FindSubSpecies(Tag subSpeciesID)
	{
		Predicate<PlantSubSpeciesCatalog.SubSpeciesInfo> <>9__0;
		foreach (KeyValuePair<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> keyValuePair in this.discoveredSubspeciesBySpecies)
		{
			List<PlantSubSpeciesCatalog.SubSpeciesInfo> value = keyValuePair.Value;
			Predicate<PlantSubSpeciesCatalog.SubSpeciesInfo> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = (PlantSubSpeciesCatalog.SubSpeciesInfo i) => i.ID == subSpeciesID);
			}
			PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = value.Find(predicate);
			if (subSpeciesInfo.ID.IsValid)
			{
				return subSpeciesInfo;
			}
		}
		return default(PlantSubSpeciesCatalog.SubSpeciesInfo);
	}

	// Token: 0x06003F0E RID: 16142 RVA: 0x001601E8 File Offset: 0x0015E3E8
	public void DiscoverSubSpecies(PlantSubSpeciesCatalog.SubSpeciesInfo newSubSpeciesInfo, MutantPlant source)
	{
		if (!this.discoveredSubspeciesBySpecies[newSubSpeciesInfo.speciesID].Contains(newSubSpeciesInfo))
		{
			this.discoveredSubspeciesBySpecies[newSubSpeciesInfo.speciesID].Add(newSubSpeciesInfo);
			Notification notification = new Notification(MISC.NOTIFICATIONS.NEWMUTANTSEED.NAME, NotificationType.Good, new Func<List<Notification>, object, string>(this.NewSubspeciesTooltipCB), newSubSpeciesInfo, true, 0f, null, null, source.transform, true, false, false);
			base.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}
	}

	// Token: 0x06003F0F RID: 16143 RVA: 0x00160270 File Offset: 0x0015E470
	private string NewSubspeciesTooltipCB(List<Notification> notifications, object data)
	{
		PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = (PlantSubSpeciesCatalog.SubSpeciesInfo)data;
		return MISC.NOTIFICATIONS.NEWMUTANTSEED.TOOLTIP.Replace("{Plant}", subSpeciesInfo.speciesID.ProperName());
	}

	// Token: 0x06003F10 RID: 16144 RVA: 0x001602A0 File Offset: 0x0015E4A0
	public void IdentifySubSpecies(Tag subSpeciesID)
	{
		if (this.identifiedSubSpecies.Add(subSpeciesID))
		{
			this.FindSubSpecies(subSpeciesID);
			foreach (object obj in Components.MutantPlants)
			{
				MutantPlant mutantPlant = (MutantPlant)obj;
				if (mutantPlant.HasTag(subSpeciesID))
				{
					mutantPlant.UpdateNameAndTags();
				}
			}
			GeneticAnalysisCompleteMessage geneticAnalysisCompleteMessage = new GeneticAnalysisCompleteMessage(subSpeciesID);
			Messenger.Instance.QueueMessage(geneticAnalysisCompleteMessage);
		}
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x00160328 File Offset: 0x0015E528
	public bool IsSubSpeciesIdentified(Tag subSpeciesID)
	{
		return this.identifiedSubSpecies.Contains(subSpeciesID);
	}

	// Token: 0x06003F12 RID: 16146 RVA: 0x00160336 File Offset: 0x0015E536
	public List<PlantSubSpeciesCatalog.SubSpeciesInfo> GetAllUnidentifiedSubSpecies(Tag speciesID)
	{
		return this.discoveredSubspeciesBySpecies[speciesID].FindAll((PlantSubSpeciesCatalog.SubSpeciesInfo ss) => !this.IsSubSpeciesIdentified(ss.ID));
	}

	// Token: 0x06003F13 RID: 16147 RVA: 0x00160358 File Offset: 0x0015E558
	public bool IsValidPlantableSeed(Tag seedID, Tag subspeciesID)
	{
		if (!seedID.IsValid)
		{
			return false;
		}
		MutantPlant component = Assets.GetPrefab(seedID).GetComponent<MutantPlant>();
		if (component == null)
		{
			return !subspeciesID.IsValid;
		}
		List<PlantSubSpeciesCatalog.SubSpeciesInfo> allSubSpeciesForSpecies = PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(component.SpeciesID);
		return allSubSpeciesForSpecies != null && allSubSpeciesForSpecies.FindIndex((PlantSubSpeciesCatalog.SubSpeciesInfo s) => s.ID == subspeciesID) != -1 && PlantSubSpeciesCatalog.Instance.IsSubSpeciesIdentified(subspeciesID);
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x001603DC File Offset: 0x0015E5DC
	private void EnsureOriginalSubSpecies()
	{
		foreach (GameObject gameObject in Assets.GetPrefabsWithComponent<MutantPlant>())
		{
			MutantPlant component = gameObject.GetComponent<MutantPlant>();
			Tag speciesID = component.SpeciesID;
			if (!this.discoveredSubspeciesBySpecies.ContainsKey(speciesID))
			{
				this.discoveredSubspeciesBySpecies[speciesID] = new List<PlantSubSpeciesCatalog.SubSpeciesInfo>();
				this.discoveredSubspeciesBySpecies[speciesID].Add(component.GetSubSpeciesInfo());
			}
			this.identifiedSubSpecies.Add(component.SubSpeciesID);
		}
	}

	// Token: 0x04002961 RID: 10593
	public static PlantSubSpeciesCatalog Instance;

	// Token: 0x04002962 RID: 10594
	[Serialize]
	private Dictionary<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> discoveredSubspeciesBySpecies = new Dictionary<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>>();

	// Token: 0x04002963 RID: 10595
	[Serialize]
	private HashSet<Tag> identifiedSubSpecies = new HashSet<Tag>();

	// Token: 0x0200165D RID: 5725
	[Serializable]
	public struct SubSpeciesInfo : IEquatable<PlantSubSpeciesCatalog.SubSpeciesInfo>
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600877A RID: 34682 RVA: 0x002F2D06 File Offset: 0x002F0F06
		public bool IsValid
		{
			get
			{
				return this.ID.IsValid;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600877B RID: 34683 RVA: 0x002F2D13 File Offset: 0x002F0F13
		public bool IsOriginal
		{
			get
			{
				return this.mutationIDs == null || this.mutationIDs.Count == 0;
			}
		}

		// Token: 0x0600877C RID: 34684 RVA: 0x002F2D2D File Offset: 0x002F0F2D
		public SubSpeciesInfo(Tag speciesID, List<string> mutationIDs)
		{
			this.speciesID = speciesID;
			this.mutationIDs = ((mutationIDs != null) ? new List<string>(mutationIDs) : new List<string>());
			this.ID = PlantSubSpeciesCatalog.SubSpeciesInfo.SubSpeciesIDFromMutations(speciesID, mutationIDs);
		}

		// Token: 0x0600877D RID: 34685 RVA: 0x002F2D5C File Offset: 0x002F0F5C
		public static Tag SubSpeciesIDFromMutations(Tag speciesID, List<string> mutationIDs)
		{
			if (mutationIDs == null || mutationIDs.Count == 0)
			{
				Tag tag = speciesID;
				return tag.ToString() + "_Original";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(speciesID);
			foreach (string text in mutationIDs)
			{
				stringBuilder.Append("_");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString().ToTag();
		}

		// Token: 0x0600877E RID: 34686 RVA: 0x002F2E00 File Offset: 0x002F1000
		public string GetMutationsNames()
		{
			if (this.mutationIDs == null || this.mutationIDs.Count == 0)
			{
				return CREATURES.PLANT_MUTATIONS.NONE.NAME;
			}
			return string.Join(", ", Db.Get().PlantMutations.GetNamesForMutations(this.mutationIDs));
		}

		// Token: 0x0600877F RID: 34687 RVA: 0x002F2E4C File Offset: 0x002F104C
		public string GetNameWithMutations(string properName, bool identified, bool cleanOriginal)
		{
			string text;
			if (this.mutationIDs == null || this.mutationIDs.Count == 0)
			{
				if (cleanOriginal)
				{
					text = properName;
				}
				else
				{
					text = CREATURES.PLANT_MUTATIONS.PLANT_NAME_FMT.Replace("{PlantName}", properName).Replace("{MutationList}", CREATURES.PLANT_MUTATIONS.NONE.NAME);
				}
			}
			else if (!identified)
			{
				text = CREATURES.PLANT_MUTATIONS.PLANT_NAME_FMT.Replace("{PlantName}", properName).Replace("{MutationList}", CREATURES.PLANT_MUTATIONS.UNIDENTIFIED);
			}
			else
			{
				text = CREATURES.PLANT_MUTATIONS.PLANT_NAME_FMT.Replace("{PlantName}", properName).Replace("{MutationList}", string.Join(", ", Db.Get().PlantMutations.GetNamesForMutations(this.mutationIDs)));
			}
			return text;
		}

		// Token: 0x06008780 RID: 34688 RVA: 0x002F2F04 File Offset: 0x002F1104
		public static bool operator ==(PlantSubSpeciesCatalog.SubSpeciesInfo obj1, PlantSubSpeciesCatalog.SubSpeciesInfo obj2)
		{
			return obj1.Equals(obj2);
		}

		// Token: 0x06008781 RID: 34689 RVA: 0x002F2F0E File Offset: 0x002F110E
		public static bool operator !=(PlantSubSpeciesCatalog.SubSpeciesInfo obj1, PlantSubSpeciesCatalog.SubSpeciesInfo obj2)
		{
			return !(obj1 == obj2);
		}

		// Token: 0x06008782 RID: 34690 RVA: 0x002F2F1A File Offset: 0x002F111A
		public override bool Equals(object other)
		{
			return other is PlantSubSpeciesCatalog.SubSpeciesInfo && this == (PlantSubSpeciesCatalog.SubSpeciesInfo)other;
		}

		// Token: 0x06008783 RID: 34691 RVA: 0x002F2F37 File Offset: 0x002F1137
		public bool Equals(PlantSubSpeciesCatalog.SubSpeciesInfo other)
		{
			return this.ID == other.ID;
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x002F2F4A File Offset: 0x002F114A
		public override int GetHashCode()
		{
			return this.ID.GetHashCode();
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x002F2F60 File Offset: 0x002F1160
		public string GetMutationsTooltip()
		{
			if (this.mutationIDs == null || this.mutationIDs.Count == 0)
			{
				return CREATURES.STATUSITEMS.ORIGINALPLANTMUTATION.TOOLTIP;
			}
			if (!PlantSubSpeciesCatalog.Instance.IsSubSpeciesIdentified(this.ID))
			{
				return CREATURES.STATUSITEMS.UNKNOWNMUTATION.TOOLTIP;
			}
			string text = this.mutationIDs[0];
			PlantMutation plantMutation = Db.Get().PlantMutations.Get(text);
			return CREATURES.STATUSITEMS.SPECIFICPLANTMUTATION.TOOLTIP.Replace("{MutationName}", plantMutation.Name) + "\n" + plantMutation.GetTooltip();
		}

		// Token: 0x0400697C RID: 27004
		public Tag speciesID;

		// Token: 0x0400697D RID: 27005
		public Tag ID;

		// Token: 0x0400697E RID: 27006
		public List<string> mutationIDs;

		// Token: 0x0400697F RID: 27007
		private const string ORIGINAL_SUFFIX = "_Original";
	}
}
