using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008D8 RID: 2264
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Research")]
public class Research : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06004124 RID: 16676 RVA: 0x0016D11E File Offset: 0x0016B31E
	public static void DestroyInstance()
	{
		Research.Instance = null;
	}

	// Token: 0x06004125 RID: 16677 RVA: 0x0016D128 File Offset: 0x0016B328
	public TechInstance GetTechInstance(string techID)
	{
		return this.techs.Find((TechInstance match) => match.tech.Id == techID);
	}

	// Token: 0x06004126 RID: 16678 RVA: 0x0016D159 File Offset: 0x0016B359
	public bool IsBeingResearched(Tech tech)
	{
		return this.activeResearch != null && tech != null && this.activeResearch.tech == tech;
	}

	// Token: 0x06004127 RID: 16679 RVA: 0x0016D176 File Offset: 0x0016B376
	protected override void OnPrefabInit()
	{
		Research.Instance = this;
		this.researchTypes = new ResearchTypes();
	}

	// Token: 0x06004128 RID: 16680 RVA: 0x0016D18C File Offset: 0x0016B38C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.globalPointInventory == null)
		{
			this.globalPointInventory = new ResearchPointInventory();
		}
		base.Subscribe<Research>(-1523247426, Research.OnRolesUpdatedDelegate);
		Components.ResearchCenters.OnAdd += new Action<IResearchCenter>(this.CheckResearchBuildings);
		Components.ResearchCenters.OnRemove += new Action<IResearchCenter>(this.CheckResearchBuildings);
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			IResearchCenter component = kprefabID.GetComponent<IResearchCenter>();
			if (component != null)
			{
				this.researchCenterPrefabs.Add(component);
			}
		}
	}

	// Token: 0x06004129 RID: 16681 RVA: 0x0016D244 File Offset: 0x0016B444
	public ResearchType GetResearchType(string id)
	{
		return this.researchTypes.GetResearchType(id);
	}

	// Token: 0x0600412A RID: 16682 RVA: 0x0016D252 File Offset: 0x0016B452
	public TechInstance GetActiveResearch()
	{
		return this.activeResearch;
	}

	// Token: 0x0600412B RID: 16683 RVA: 0x0016D25A File Offset: 0x0016B45A
	public TechInstance GetTargetResearch()
	{
		if (this.queuedTech != null && this.queuedTech.Count > 0)
		{
			return this.queuedTech[this.queuedTech.Count - 1];
		}
		return null;
	}

	// Token: 0x0600412C RID: 16684 RVA: 0x0016D28C File Offset: 0x0016B48C
	public TechInstance Get(Tech tech)
	{
		foreach (TechInstance techInstance in this.techs)
		{
			if (techInstance.tech == tech)
			{
				return techInstance;
			}
		}
		return null;
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x0016D2E8 File Offset: 0x0016B4E8
	public TechInstance GetOrAdd(Tech tech)
	{
		TechInstance techInstance = this.techs.Find((TechInstance tc) => tc.tech == tech);
		if (techInstance != null)
		{
			return techInstance;
		}
		TechInstance techInstance2 = new TechInstance(tech);
		this.techs.Add(techInstance2);
		return techInstance2;
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x0016D338 File Offset: 0x0016B538
	public void GetNextTech()
	{
		if (this.queuedTech.Count > 0)
		{
			this.queuedTech.RemoveAt(0);
		}
		if (this.queuedTech.Count > 0)
		{
			this.SetActiveResearch(this.queuedTech[this.queuedTech.Count - 1].tech, false);
			return;
		}
		this.SetActiveResearch(null, false);
	}

	// Token: 0x0600412F RID: 16687 RVA: 0x0016D39C File Offset: 0x0016B59C
	private void AddTechToQueue(Tech tech)
	{
		TechInstance orAdd = this.GetOrAdd(tech);
		if (!orAdd.IsComplete())
		{
			this.queuedTech.Add(orAdd);
		}
		orAdd.tech.requiredTech.ForEach(delegate(Tech _tech)
		{
			this.AddTechToQueue(_tech);
		});
	}

	// Token: 0x06004130 RID: 16688 RVA: 0x0016D3E4 File Offset: 0x0016B5E4
	public void CancelResearch(Tech tech, bool clickedEntry = true)
	{
		Research.<>c__DisplayClass26_0 CS$<>8__locals1 = new Research.<>c__DisplayClass26_0();
		CS$<>8__locals1.tech = tech;
		CS$<>8__locals1.ti = this.queuedTech.Find((TechInstance qt) => qt.tech == CS$<>8__locals1.tech);
		if (CS$<>8__locals1.ti == null)
		{
			return;
		}
		if (CS$<>8__locals1.ti == this.queuedTech[this.queuedTech.Count - 1] && clickedEntry)
		{
			this.SetActiveResearch(null, false);
		}
		int i;
		int j;
		for (i = CS$<>8__locals1.ti.tech.unlockedTech.Count - 1; i >= 0; i = j - 1)
		{
			if (this.queuedTech.Find((TechInstance qt) => qt.tech == CS$<>8__locals1.ti.tech.unlockedTech[i]) != null)
			{
				this.CancelResearch(CS$<>8__locals1.ti.tech.unlockedTech[i], false);
			}
			j = i;
		}
		this.queuedTech.Remove(CS$<>8__locals1.ti);
		if (clickedEntry)
		{
			this.NotifyResearchCenters(GameHashes.ActiveResearchChanged, this.queuedTech);
		}
	}

	// Token: 0x06004131 RID: 16689 RVA: 0x0016D500 File Offset: 0x0016B700
	private void NotifyResearchCenters(GameHashes hash, object data)
	{
		foreach (object obj in Components.ResearchCenters)
		{
			((KMonoBehaviour)obj).Trigger(-1914338957, data);
		}
		base.Trigger((int)hash, data);
	}

	// Token: 0x06004132 RID: 16690 RVA: 0x0016D564 File Offset: 0x0016B764
	public void SetActiveResearch(Tech tech, bool clearQueue = false)
	{
		if (clearQueue)
		{
			this.queuedTech.Clear();
		}
		this.activeResearch = null;
		if (tech != null)
		{
			if (this.queuedTech.Count == 0)
			{
				this.AddTechToQueue(tech);
			}
			if (this.queuedTech.Count > 0)
			{
				this.queuedTech.Sort((TechInstance x, TechInstance y) => x.tech.tier.CompareTo(y.tech.tier));
				this.activeResearch = this.queuedTech[0];
			}
		}
		else
		{
			this.queuedTech.Clear();
		}
		this.NotifyResearchCenters(GameHashes.ActiveResearchChanged, this.queuedTech);
		this.CheckBuyResearch();
		this.CheckResearchBuildings(null);
		if (this.NoResearcherRole != null)
		{
			this.notifier.Remove(this.NoResearcherRole);
			this.NoResearcherRole = null;
		}
		if (this.activeResearch != null)
		{
			Skill skill = null;
			if (this.activeResearch.tech.costsByResearchTypeID.ContainsKey("advanced") && this.activeResearch.tech.costsByResearchTypeID["advanced"] > 0f && !MinionResume.AnyMinionHasPerk(Db.Get().SkillPerks.AllowAdvancedResearch.Id, -1))
			{
				skill = Db.Get().Skills.GetSkillsWithPerk(Db.Get().SkillPerks.AllowAdvancedResearch)[0];
			}
			else if (this.activeResearch.tech.costsByResearchTypeID.ContainsKey("space") && this.activeResearch.tech.costsByResearchTypeID["space"] > 0f && !MinionResume.AnyMinionHasPerk(Db.Get().SkillPerks.AllowInterstellarResearch.Id, -1))
			{
				skill = Db.Get().Skills.GetSkillsWithPerk(Db.Get().SkillPerks.AllowInterstellarResearch)[0];
			}
			else if (this.activeResearch.tech.costsByResearchTypeID.ContainsKey("nuclear") && this.activeResearch.tech.costsByResearchTypeID["nuclear"] > 0f && !MinionResume.AnyMinionHasPerk(Db.Get().SkillPerks.AllowNuclearResearch.Id, -1))
			{
				skill = Db.Get().Skills.GetSkillsWithPerk(Db.Get().SkillPerks.AllowNuclearResearch)[0];
			}
			else if (this.activeResearch.tech.costsByResearchTypeID.ContainsKey("orbital") && this.activeResearch.tech.costsByResearchTypeID["orbital"] > 0f && !MinionResume.AnyMinionHasPerk(Db.Get().SkillPerks.AllowOrbitalResearch.Id, -1))
			{
				skill = Db.Get().Skills.GetSkillsWithPerk(Db.Get().SkillPerks.AllowOrbitalResearch)[0];
			}
			if (skill != null)
			{
				this.NoResearcherRole = new Notification(RESEARCH.MESSAGING.NO_RESEARCHER_SKILL, NotificationType.Bad, new Func<List<Notification>, object, string>(this.NoResearcherRoleTooltip), skill, false, 12f, null, null, null, true, false, false);
				this.notifier.Add(this.NoResearcherRole, "");
			}
		}
	}

	// Token: 0x06004133 RID: 16691 RVA: 0x0016D88C File Offset: 0x0016BA8C
	private string NoResearcherRoleTooltip(List<Notification> list, object data)
	{
		Skill skill = (Skill)data;
		return RESEARCH.MESSAGING.NO_RESEARCHER_SKILL_TOOLTIP.Replace("{ResearchType}", skill.Name);
	}

	// Token: 0x06004134 RID: 16692 RVA: 0x0016D8B8 File Offset: 0x0016BAB8
	public void AddResearchPoints(string researchTypeID, float points)
	{
		if (!this.UseGlobalPointInventory && this.activeResearch == null)
		{
			global::Debug.LogWarning("No active research to add research points to. Global research inventory is disabled.");
			return;
		}
		(this.UseGlobalPointInventory ? this.globalPointInventory : this.activeResearch.progressInventory).AddResearchPoints(researchTypeID, points);
		this.CheckBuyResearch();
		this.NotifyResearchCenters(GameHashes.ResearchPointsChanged, null);
	}

	// Token: 0x06004135 RID: 16693 RVA: 0x0016D914 File Offset: 0x0016BB14
	private void CheckBuyResearch()
	{
		if (this.activeResearch != null)
		{
			ResearchPointInventory researchPointInventory = (this.UseGlobalPointInventory ? this.globalPointInventory : this.activeResearch.progressInventory);
			if (this.activeResearch.tech.CanAfford(researchPointInventory))
			{
				foreach (KeyValuePair<string, float> keyValuePair in this.activeResearch.tech.costsByResearchTypeID)
				{
					researchPointInventory.RemoveResearchPoints(keyValuePair.Key, keyValuePair.Value);
				}
				this.activeResearch.Purchased();
				Game.Instance.Trigger(-107300940, this.activeResearch.tech);
				this.GetNextTech();
			}
		}
	}

	// Token: 0x06004136 RID: 16694 RVA: 0x0016D9E4 File Offset: 0x0016BBE4
	protected override void OnCleanUp()
	{
		Components.ResearchCenters.OnAdd -= new Action<IResearchCenter>(this.CheckResearchBuildings);
		Components.ResearchCenters.OnRemove -= new Action<IResearchCenter>(this.CheckResearchBuildings);
		base.OnCleanUp();
	}

	// Token: 0x06004137 RID: 16695 RVA: 0x0016DA18 File Offset: 0x0016BC18
	public void CompleteQueue()
	{
		while (this.queuedTech.Count > 0)
		{
			foreach (KeyValuePair<string, float> keyValuePair in this.activeResearch.tech.costsByResearchTypeID)
			{
				this.AddResearchPoints(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06004138 RID: 16696 RVA: 0x0016DA94 File Offset: 0x0016BC94
	public List<TechInstance> GetResearchQueue()
	{
		return new List<TechInstance>(this.queuedTech);
	}

	// Token: 0x06004139 RID: 16697 RVA: 0x0016DAA4 File Offset: 0x0016BCA4
	[OnSerializing]
	internal void OnSerializing()
	{
		this.saveData = default(Research.SaveData);
		if (this.activeResearch != null)
		{
			this.saveData.activeResearchId = this.activeResearch.tech.Id;
		}
		else
		{
			this.saveData.activeResearchId = "";
		}
		if (this.queuedTech != null && this.queuedTech.Count > 0)
		{
			this.saveData.targetResearchId = this.queuedTech[this.queuedTech.Count - 1].tech.Id;
		}
		else
		{
			this.saveData.targetResearchId = "";
		}
		this.saveData.techs = new TechInstance.SaveData[this.techs.Count];
		for (int i = 0; i < this.techs.Count; i++)
		{
			this.saveData.techs[i] = this.techs[i].Save();
		}
	}

	// Token: 0x0600413A RID: 16698 RVA: 0x0016DB9C File Offset: 0x0016BD9C
	[OnDeserialized]
	internal void OnDeserialized()
	{
		if (this.saveData.techs != null)
		{
			foreach (TechInstance.SaveData saveData in this.saveData.techs)
			{
				Tech tech = Db.Get().Techs.TryGet(saveData.techId);
				if (tech != null)
				{
					this.GetOrAdd(tech).Load(saveData);
				}
			}
		}
		foreach (TechInstance techInstance in this.techs)
		{
			if (this.saveData.targetResearchId == techInstance.tech.Id)
			{
				this.SetActiveResearch(techInstance.tech, false);
				break;
			}
		}
	}

	// Token: 0x0600413B RID: 16699 RVA: 0x0016DC70 File Offset: 0x0016BE70
	private void OnRolesUpdated(object data)
	{
		if (this.activeResearch == null || this.activeResearch.tech.costsByResearchTypeID.Count <= 1)
		{
			this.notifier.Remove(this.NoResearcherRole);
			return;
		}
		if (!MinionResume.AnyMinionHasPerk(Db.Get().SkillPerks.AllowAdvancedResearch.Id, -1))
		{
			this.notifier.Add(this.NoResearcherRole, "");
			return;
		}
		this.notifier.Remove(this.NoResearcherRole);
	}

	// Token: 0x0600413C RID: 16700 RVA: 0x0016DCF4 File Offset: 0x0016BEF4
	public string GetMissingResearchBuildingName()
	{
		foreach (KeyValuePair<string, float> keyValuePair in this.activeResearch.tech.costsByResearchTypeID)
		{
			bool flag = true;
			if (keyValuePair.Value > 0f)
			{
				flag = false;
				using (List<IResearchCenter>.Enumerator enumerator2 = Components.ResearchCenters.Items.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.GetResearchType() == keyValuePair.Key)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				foreach (IResearchCenter researchCenter in this.researchCenterPrefabs)
				{
					if (researchCenter.GetResearchType() == keyValuePair.Key)
					{
						return ((KMonoBehaviour)researchCenter).GetProperName();
					}
				}
				return null;
			}
		}
		return null;
	}

	// Token: 0x0600413D RID: 16701 RVA: 0x0016DE24 File Offset: 0x0016C024
	private void CheckResearchBuildings(object data)
	{
		if (this.activeResearch == null)
		{
			this.notifier.Remove(this.MissingResearchStation);
			return;
		}
		if (string.IsNullOrEmpty(this.GetMissingResearchBuildingName()))
		{
			this.notifier.Remove(this.MissingResearchStation);
			return;
		}
		this.notifier.Add(this.MissingResearchStation, "");
	}

	// Token: 0x04002B80 RID: 11136
	public static Research Instance;

	// Token: 0x04002B81 RID: 11137
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04002B82 RID: 11138
	private List<TechInstance> techs = new List<TechInstance>();

	// Token: 0x04002B83 RID: 11139
	private List<TechInstance> queuedTech = new List<TechInstance>();

	// Token: 0x04002B84 RID: 11140
	private TechInstance activeResearch;

	// Token: 0x04002B85 RID: 11141
	private Notification NoResearcherRole;

	// Token: 0x04002B86 RID: 11142
	private Notification MissingResearchStation = new Notification(RESEARCH.MESSAGING.MISSING_RESEARCH_STATION, NotificationType.Bad, (List<Notification> list, object data) => RESEARCH.MESSAGING.MISSING_RESEARCH_STATION_TOOLTIP.ToString().Replace("{0}", Research.Instance.GetMissingResearchBuildingName()), null, false, 11f, null, null, null, true, false, false);

	// Token: 0x04002B87 RID: 11143
	private List<IResearchCenter> researchCenterPrefabs = new List<IResearchCenter>();

	// Token: 0x04002B88 RID: 11144
	public ResearchTypes researchTypes;

	// Token: 0x04002B89 RID: 11145
	public bool UseGlobalPointInventory;

	// Token: 0x04002B8A RID: 11146
	[Serialize]
	public ResearchPointInventory globalPointInventory;

	// Token: 0x04002B8B RID: 11147
	[Serialize]
	private Research.SaveData saveData;

	// Token: 0x04002B8C RID: 11148
	private static readonly EventSystem.IntraObjectHandler<Research> OnRolesUpdatedDelegate = new EventSystem.IntraObjectHandler<Research>(delegate(Research component, object data)
	{
		component.OnRolesUpdated(data);
	});

	// Token: 0x0200169A RID: 5786
	private struct SaveData
	{
		// Token: 0x04006A59 RID: 27225
		public string activeResearchId;

		// Token: 0x04006A5A RID: 27226
		public string targetResearchId;

		// Token: 0x04006A5B RID: 27227
		public TechInstance.SaveData[] techs;
	}
}
