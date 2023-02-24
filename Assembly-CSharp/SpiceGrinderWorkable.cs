using System;
using System.Linq;
using TUNING;
using UnityEngine;

// Token: 0x02000649 RID: 1609
public class SpiceGrinderWorkable : Workable, IConfigurableConsumer
{
	// Token: 0x06002AC1 RID: 10945 RVA: 0x000E196C File Offset: 0x000DFB6C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanSpiceGrinder.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Spicing;
		this.attributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_spice_grinder_kanim") };
		base.SetWorkTime(5f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x000E1A2C File Offset: 0x000DFC2C
	protected override void OnStartWork(Worker worker)
	{
		if (this.Grinder.CurrentFood != null)
		{
			float num = this.Grinder.CurrentFood.Calories * 0.001f / 1000f;
			base.SetWorkTime(num * 5f);
		}
		else
		{
			global::Debug.LogWarning("SpiceGrider attempted to start spicing with no food");
			base.StopWork(worker, true);
		}
		this.Grinder.UpdateFoodSymbol();
	}

	// Token: 0x06002AC3 RID: 10947 RVA: 0x000E1A95 File Offset: 0x000DFC95
	protected override void OnAbortWork(Worker worker)
	{
		if (this.Grinder.CurrentFood == null)
		{
			return;
		}
		this.Grinder.UpdateFoodSymbol();
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x000E1AB6 File Offset: 0x000DFCB6
	protected override void OnCompleteWork(Worker worker)
	{
		if (this.Grinder.CurrentFood == null)
		{
			return;
		}
		this.Grinder.SpiceFood();
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x000E1AD8 File Offset: 0x000DFCD8
	public IConfigurableConsumerOption[] GetSettingOptions()
	{
		return SpiceGrinder.SettingOptions.Values.ToArray<SpiceGrinder.Option>();
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x000E1AF6 File Offset: 0x000DFCF6
	public IConfigurableConsumerOption GetSelectedOption()
	{
		return this.Grinder.SelectedOption;
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x000E1B03 File Offset: 0x000DFD03
	public void SetSelectedOption(IConfigurableConsumerOption option)
	{
		this.Grinder.OnOptionSelected(option as SpiceGrinder.Option);
	}

	// Token: 0x04001953 RID: 6483
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04001954 RID: 6484
	[SerializeField]
	public Vector3 finishedSeedDropOffset;

	// Token: 0x04001955 RID: 6485
	private Notification notification;

	// Token: 0x04001956 RID: 6486
	public SpiceGrinder.StatesInstance Grinder;
}
