using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
[AddComponentMenu("KMonoBehaviour/Workable/ResearchCenter")]
public class ResearchCenter : Workable, IGameObjectEffectDescriptor, ISim200ms, IResearchCenter
{
	// Token: 0x06004172 RID: 16754 RVA: 0x0016EB18 File Offset: 0x0016CD18
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Researching;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.ALL_DAY_EXPERIENCE;
		ElementConverter elementConverter = this.elementConverter;
		elementConverter.onConvertMass = (Action<float>)Delegate.Combine(elementConverter.onConvertMass, new Action<float>(this.ConvertMassToResearchPoints));
	}

	// Token: 0x06004173 RID: 16755 RVA: 0x0016EBAC File Offset: 0x0016CDAC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ResearchCenter>(-1914338957, ResearchCenter.UpdateWorkingStateDelegate);
		base.Subscribe<ResearchCenter>(-125623018, ResearchCenter.UpdateWorkingStateDelegate);
		base.Subscribe<ResearchCenter>(187661686, ResearchCenter.UpdateWorkingStateDelegate);
		base.Subscribe<ResearchCenter>(-1697596308, ResearchCenter.CheckHasMaterialDelegate);
		Components.ResearchCenters.Add(this);
		this.UpdateWorkingState(null);
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x0016EC18 File Offset: 0x0016CE18
	private void ConvertMassToResearchPoints(float mass_consumed)
	{
		this.remainder_mass_points += mass_consumed / this.mass_per_point - (float)Mathf.FloorToInt(mass_consumed / this.mass_per_point);
		int num = Mathf.FloorToInt(mass_consumed / this.mass_per_point);
		num += Mathf.FloorToInt(this.remainder_mass_points);
		this.remainder_mass_points -= (float)Mathf.FloorToInt(this.remainder_mass_points);
		ResearchType researchType = Research.Instance.GetResearchType(this.research_point_type_id);
		if (num > 0)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, researchType.name, base.transform, 1.5f, false);
			for (int i = 0; i < num; i++)
			{
				Research.Instance.AddResearchPoints(this.research_point_type_id, 1f);
			}
		}
	}

	// Token: 0x06004175 RID: 16757 RVA: 0x0016ECDC File Offset: 0x0016CEDC
	public void Sim200ms(float dt)
	{
		if (!this.operational.IsActive && this.operational.IsOperational && this.chore == null && this.HasMaterial())
		{
			this.chore = this.CreateChore();
			base.SetWorkTime(float.PositiveInfinity);
		}
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x0016ED2C File Offset: 0x0016CF2C
	protected virtual Chore CreateChore()
	{
		return new WorkChore<ResearchCenter>(Db.Get().ChoreTypes.Research, this, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true)
		{
			preemption_cb = new Func<Chore.Precondition.Context, bool>(ResearchCenter.CanPreemptCB)
		};
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x0016ED74 File Offset: 0x0016CF74
	private static bool CanPreemptCB(Chore.Precondition.Context context)
	{
		Worker component = context.chore.driver.GetComponent<Worker>();
		float num = Db.Get().AttributeConverters.ResearchSpeed.Lookup(component).Evaluate();
		Worker worker = context.consumerState.worker;
		return Db.Get().AttributeConverters.ResearchSpeed.Lookup(worker).Evaluate() > num && context.chore.gameObject.GetComponent<ResearchCenter>().GetPercentComplete() < 1f;
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x0016EDF4 File Offset: 0x0016CFF4
	public override float GetPercentComplete()
	{
		if (Research.Instance.GetActiveResearch() == null)
		{
			return 0f;
		}
		float num = Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID[this.research_point_type_id];
		float num2 = 0f;
		if (!Research.Instance.GetActiveResearch().tech.costsByResearchTypeID.TryGetValue(this.research_point_type_id, out num2))
		{
			return 1f;
		}
		return num / num2;
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x0016EE65 File Offset: 0x0016D065
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.operational.SetActive(true, false);
	}

	// Token: 0x0600417A RID: 16762 RVA: 0x0016EE7C File Offset: 0x0016D07C
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		float efficiencyMultiplier = this.GetEfficiencyMultiplier(worker);
		float num = 2f + efficiencyMultiplier;
		if (Game.Instance.FastWorkersModeActive)
		{
			num *= 2f;
		}
		this.elementConverter.SetWorkSpeedMultiplier(num);
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x0600417B RID: 16763 RVA: 0x0016EEC1 File Offset: 0x0016D0C1
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.ShowProgressBar(false);
		this.operational.SetActive(false, false);
	}

	// Token: 0x0600417C RID: 16764 RVA: 0x0016EEE0 File Offset: 0x0016D0E0
	protected bool ResearchComponentCompleted()
	{
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch != null)
		{
			float num = 0f;
			float num2 = 0f;
			activeResearch.progressInventory.PointsByTypeID.TryGetValue(this.research_point_type_id, out num);
			activeResearch.tech.costsByResearchTypeID.TryGetValue(this.research_point_type_id, out num2);
			if (num >= num2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600417D RID: 16765 RVA: 0x0016EF40 File Offset: 0x0016D140
	protected bool IsAllResearchComplete()
	{
		using (List<Tech>.Enumerator enumerator = Db.Get().Techs.resources.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsComplete())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600417E RID: 16766 RVA: 0x0016EFA4 File Offset: 0x0016D1A4
	protected virtual void UpdateWorkingState(object data)
	{
		bool flag = false;
		bool flag2 = false;
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch != null)
		{
			flag = true;
			if (activeResearch.tech.costsByResearchTypeID.ContainsKey(this.research_point_type_id) && Research.Instance.Get(activeResearch.tech).progressInventory.PointsByTypeID[this.research_point_type_id] < activeResearch.tech.costsByResearchTypeID[this.research_point_type_id])
			{
				flag2 = true;
			}
		}
		if (this.operational.GetFlag(EnergyConsumer.PoweredFlag) && !this.IsAllResearchComplete())
		{
			if (flag)
			{
				base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
				if (!flag2 && !this.ResearchComponentCompleted())
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
					base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, null);
				}
				else
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, false);
				}
			}
			else
			{
				base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, null);
				base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, false);
			}
		}
		else
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoApplicableResearchSelected, false);
		}
		this.operational.SetFlag(ResearchCenter.ResearchSelectedFlag, flag && flag2);
		if ((!flag || !flag2) && base.worker)
		{
			base.StopWork(base.worker, true);
		}
	}

	// Token: 0x0600417F RID: 16767 RVA: 0x0016F169 File Offset: 0x0016D369
	private void ClearResearchScreen()
	{
		Game.Instance.Trigger(-1974454597, null);
	}

	// Token: 0x06004180 RID: 16768 RVA: 0x0016F17B File Offset: 0x0016D37B
	public string GetResearchType()
	{
		return this.research_point_type_id;
	}

	// Token: 0x06004181 RID: 16769 RVA: 0x0016F183 File Offset: 0x0016D383
	private void CheckHasMaterial(object o = null)
	{
		if (!this.HasMaterial() && this.chore != null)
		{
			this.chore.Cancel("No material remaining");
			this.chore = null;
		}
	}

	// Token: 0x06004182 RID: 16770 RVA: 0x0016F1AC File Offset: 0x0016D3AC
	private bool HasMaterial()
	{
		return this.storage.MassStored() > 0f;
	}

	// Token: 0x06004183 RID: 16771 RVA: 0x0016F1C0 File Offset: 0x0016D3C0
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Research.Instance.Unsubscribe(-1914338957, new Action<object>(this.UpdateWorkingState));
		Research.Instance.Unsubscribe(-125623018, new Action<object>(this.UpdateWorkingState));
		base.Unsubscribe(-1852328367, new Action<object>(this.UpdateWorkingState));
		Components.ResearchCenters.Remove(this);
		this.ClearResearchScreen();
	}

	// Token: 0x06004184 RID: 16772 RVA: 0x0016F234 File Offset: 0x0016D434
	public string GetStatusString()
	{
		string text = RESEARCH.MESSAGING.NORESEARCHSELECTED;
		if (Research.Instance.GetActiveResearch() != null)
		{
			text = "<b>" + Research.Instance.GetActiveResearch().tech.Name + "</b>";
			int num = 0;
			foreach (KeyValuePair<string, float> keyValuePair in Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID)
			{
				if (Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair.Key] != 0f)
				{
					num++;
				}
			}
			foreach (KeyValuePair<string, float> keyValuePair2 in Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID)
			{
				if (Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair2.Key] != 0f && keyValuePair2.Key == this.research_point_type_id)
				{
					text = text + "\n   - " + Research.Instance.researchTypes.GetResearchType(keyValuePair2.Key).name;
					text = string.Concat(new string[]
					{
						text,
						": ",
						keyValuePair2.Value.ToString(),
						"/",
						Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair2.Key].ToString()
					});
				}
			}
			foreach (KeyValuePair<string, float> keyValuePair3 in Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID)
			{
				if (Research.Instance.GetActiveResearch().tech.costsByResearchTypeID[keyValuePair3.Key] != 0f && !(keyValuePair3.Key == this.research_point_type_id))
				{
					if (num > 1)
					{
						text = text + "\n   - " + string.Format(RESEARCH.MESSAGING.RESEARCHTYPEALSOREQUIRED, Research.Instance.researchTypes.GetResearchType(keyValuePair3.Key).name);
					}
					else
					{
						text = text + "\n   - " + string.Format(RESEARCH.MESSAGING.RESEARCHTYPEREQUIRED, Research.Instance.researchTypes.GetResearchType(keyValuePair3.Key).name);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x06004185 RID: 16773 RVA: 0x0016F514 File Offset: 0x0016D714
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.RESEARCH_MATERIALS, this.inputMaterial.ProperName(), GameUtil.GetFormattedByTag(this.inputMaterial, this.mass_per_point, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.RESEARCH_MATERIALS, this.inputMaterial.ProperName(), GameUtil.GetFormattedByTag(this.inputMaterial, this.mass_per_point, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Requirement, false));
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.PRODUCES_RESEARCH_POINTS, Research.Instance.researchTypes.GetResearchType(this.research_point_type_id).name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.PRODUCES_RESEARCH_POINTS, Research.Instance.researchTypes.GetResearchType(this.research_point_type_id).name), Descriptor.DescriptorType.Effect, false));
		return descriptors;
	}

	// Token: 0x06004186 RID: 16774 RVA: 0x0016F5EC File Offset: 0x0016D7EC
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x04002BA9 RID: 11177
	private Chore chore;

	// Token: 0x04002BAA RID: 11178
	[MyCmpAdd]
	protected Notifier notifier;

	// Token: 0x04002BAB RID: 11179
	[MyCmpAdd]
	protected Operational operational;

	// Token: 0x04002BAC RID: 11180
	[MyCmpAdd]
	protected Storage storage;

	// Token: 0x04002BAD RID: 11181
	[MyCmpGet]
	private ElementConverter elementConverter;

	// Token: 0x04002BAE RID: 11182
	[SerializeField]
	public string research_point_type_id;

	// Token: 0x04002BAF RID: 11183
	[SerializeField]
	public Tag inputMaterial;

	// Token: 0x04002BB0 RID: 11184
	[SerializeField]
	public float mass_per_point;

	// Token: 0x04002BB1 RID: 11185
	[SerializeField]
	private float remainder_mass_points;

	// Token: 0x04002BB2 RID: 11186
	public static readonly Operational.Flag ResearchSelectedFlag = new Operational.Flag("researchSelected", Operational.Flag.Type.Requirement);

	// Token: 0x04002BB3 RID: 11187
	private static readonly EventSystem.IntraObjectHandler<ResearchCenter> UpdateWorkingStateDelegate = new EventSystem.IntraObjectHandler<ResearchCenter>(delegate(ResearchCenter component, object data)
	{
		component.UpdateWorkingState(data);
	});

	// Token: 0x04002BB4 RID: 11188
	private static readonly EventSystem.IntraObjectHandler<ResearchCenter> CheckHasMaterialDelegate = new EventSystem.IntraObjectHandler<ResearchCenter>(delegate(ResearchCenter component, object data)
	{
		component.CheckHasMaterial(data);
	});
}
