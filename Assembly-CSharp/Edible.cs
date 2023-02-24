using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000477 RID: 1143
[AddComponentMenu("KMonoBehaviour/Workable/Edible")]
public class Edible : Workable, IGameObjectEffectDescriptor, ISaveLoadable, IExtendSplitting
{
	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06001977 RID: 6519 RVA: 0x00088975 File Offset: 0x00086B75
	// (set) Token: 0x06001978 RID: 6520 RVA: 0x00088982 File Offset: 0x00086B82
	public float Units
	{
		get
		{
			return this.primaryElement.Units;
		}
		set
		{
			this.primaryElement.Units = value;
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06001979 RID: 6521 RVA: 0x00088990 File Offset: 0x00086B90
	public float MassPerUnit
	{
		get
		{
			return this.primaryElement.MassPerUnit;
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x0600197A RID: 6522 RVA: 0x0008899D File Offset: 0x00086B9D
	// (set) Token: 0x0600197B RID: 6523 RVA: 0x000889B1 File Offset: 0x00086BB1
	public float Calories
	{
		get
		{
			return this.Units * this.foodInfo.CaloriesPerUnit;
		}
		set
		{
			this.Units = value / this.foodInfo.CaloriesPerUnit;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x0600197C RID: 6524 RVA: 0x000889C6 File Offset: 0x00086BC6
	// (set) Token: 0x0600197D RID: 6525 RVA: 0x000889CE File Offset: 0x00086BCE
	public EdiblesManager.FoodInfo FoodInfo
	{
		get
		{
			return this.foodInfo;
		}
		set
		{
			this.foodInfo = value;
			this.FoodID = this.foodInfo.Id;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x0600197E RID: 6526 RVA: 0x000889E8 File Offset: 0x00086BE8
	// (set) Token: 0x0600197F RID: 6527 RVA: 0x000889F0 File Offset: 0x00086BF0
	public bool isBeingConsumed { get; private set; }

	// Token: 0x06001980 RID: 6528 RVA: 0x000889FC File Offset: 0x00086BFC
	protected override void OnPrefabInit()
	{
		this.primaryElement = base.GetComponent<PrimaryElement>();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.showProgressBar = false;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.shouldTransferDiseaseWithWorker = false;
		base.OnPrefabInit();
		if (this.foodInfo == null)
		{
			if (this.FoodID == null)
			{
				global::Debug.LogError("No food FoodID");
			}
			this.foodInfo = EdiblesManager.GetFoodInfo(this.FoodID);
		}
		base.Subscribe<Edible>(748399584, Edible.OnCraftDelegate);
		base.Subscribe<Edible>(1272413801, Edible.OnCraftDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Eating;
		this.synchronizeAnims = false;
		Components.Edibles.Add(this);
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x00088AB0 File Offset: 0x00086CB0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ToggleGenericSpicedTag(base.gameObject.HasTag(GameTags.SpicedFood));
		if (this.spices != null)
		{
			for (int i = 0; i < this.spices.Count; i++)
			{
				this.ApplySpiceEffects(this.spices[i], SpiceGrinderConfig.SpicedStatus);
			}
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().MiscStatusItems.Edible, this);
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x00088B3C File Offset: 0x00086D3C
	public override HashedString[] GetWorkAnims(Worker worker)
	{
		EatChore.StatesInstance smi = worker.GetSMI<EatChore.StatesInstance>();
		bool flag = smi != null && smi.UseSalt();
		MinionResume component = worker.GetComponent<MinionResume>();
		if (component != null && component.CurrentHat != null)
		{
			if (!flag)
			{
				return Edible.hatWorkAnims;
			}
			return Edible.saltHatWorkAnims;
		}
		else
		{
			if (!flag)
			{
				return Edible.normalWorkAnims;
			}
			return Edible.saltWorkAnims;
		}
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x00088B94 File Offset: 0x00086D94
	public override HashedString[] GetWorkPstAnims(Worker worker, bool successfully_completed)
	{
		EatChore.StatesInstance smi = worker.GetSMI<EatChore.StatesInstance>();
		bool flag = smi != null && smi.UseSalt();
		MinionResume component = worker.GetComponent<MinionResume>();
		if (component != null && component.CurrentHat != null)
		{
			if (!flag)
			{
				return Edible.hatWorkPstAnim;
			}
			return Edible.saltHatWorkPstAnim;
		}
		else
		{
			if (!flag)
			{
				return Edible.normalWorkPstAnim;
			}
			return Edible.saltWorkPstAnim;
		}
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x00088BEA File Offset: 0x00086DEA
	private void OnCraft(object data)
	{
		RationTracker.Get().RegisterCaloriesProduced(this.Calories);
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00088BFC File Offset: 0x00086DFC
	public float GetFeedingTime(Worker worker)
	{
		float num = this.Calories * 2E-05f;
		if (worker != null)
		{
			BingeEatChore.StatesInstance smi = worker.GetSMI<BingeEatChore.StatesInstance>();
			if (smi != null && smi.IsBingeEating())
			{
				num /= 2f;
			}
		}
		return num;
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x00088C3C File Offset: 0x00086E3C
	protected override void OnStartWork(Worker worker)
	{
		this.totalFeedingTime = this.GetFeedingTime(worker);
		base.SetWorkTime(this.totalFeedingTime);
		this.caloriesConsumed = 0f;
		this.unitsConsumed = 0f;
		this.totalUnits = this.Units;
		worker.GetComponent<KPrefabID>().AddTag(GameTags.AlwaysConverse, false);
		this.totalConsumableCalories = this.Units * this.foodInfo.CaloriesPerUnit;
		this.StartConsuming();
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x00088CB4 File Offset: 0x00086EB4
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.currentlyLit)
		{
			if (this.currentModifier != this.caloriesLitSpaceModifier)
			{
				worker.GetAttributes().Remove(this.currentModifier);
				worker.GetAttributes().Add(this.caloriesLitSpaceModifier);
				this.currentModifier = this.caloriesLitSpaceModifier;
			}
		}
		else if (this.currentModifier != this.caloriesModifier)
		{
			worker.GetAttributes().Remove(this.currentModifier);
			worker.GetAttributes().Add(this.caloriesModifier);
			this.currentModifier = this.caloriesModifier;
		}
		return this.OnTickConsume(worker, dt);
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x00088D4B File Offset: 0x00086F4B
	protected override void OnStopWork(Worker worker)
	{
		if (this.currentModifier != null)
		{
			worker.GetAttributes().Remove(this.currentModifier);
			this.currentModifier = null;
		}
		worker.GetComponent<KPrefabID>().RemoveTag(GameTags.AlwaysConverse);
		this.StopConsuming(worker);
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x00088D84 File Offset: 0x00086F84
	private bool OnTickConsume(Worker worker, float dt)
	{
		if (!this.isBeingConsumed)
		{
			DebugUtil.DevLogError("OnTickConsume while we're not eating, this would set a NaN mass on this Edible");
			return true;
		}
		bool flag = false;
		float num = dt / this.totalFeedingTime;
		float num2 = num * this.totalConsumableCalories;
		if (this.caloriesConsumed + num2 > this.totalConsumableCalories)
		{
			num2 = this.totalConsumableCalories - this.caloriesConsumed;
		}
		this.caloriesConsumed += num2;
		worker.GetAmounts().Get("Calories").value += num2;
		float num3 = this.totalUnits * num;
		if (this.Units - num3 < 0f)
		{
			num3 = this.Units;
		}
		this.Units -= num3;
		this.unitsConsumed += num3;
		if (this.Units <= 0f)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x00088E4D File Offset: 0x0008704D
	public void SpiceEdible(SpiceInstance spice, StatusItem status)
	{
		this.spices.Add(spice);
		this.ApplySpiceEffects(spice, status);
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x00088E64 File Offset: 0x00087064
	protected virtual void ApplySpiceEffects(SpiceInstance spice, StatusItem status)
	{
		base.GetComponent<KPrefabID>().AddTag(spice.Id, true);
		this.ToggleGenericSpicedTag(true);
		base.GetComponent<KSelectable>().AddStatusItem(status, this.spices);
		if (spice.FoodModifier != null)
		{
			base.gameObject.GetAttributes().Add(spice.FoodModifier);
		}
		if (spice.CalorieModifier != null)
		{
			this.Calories += spice.CalorieModifier.Value;
		}
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x00088EE0 File Offset: 0x000870E0
	private void ToggleGenericSpicedTag(bool isSpiced)
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (isSpiced)
		{
			component.RemoveTag(GameTags.UnspicedFood);
			component.AddTag(GameTags.SpicedFood, true);
			return;
		}
		component.RemoveTag(GameTags.SpicedFood);
		component.AddTag(GameTags.UnspicedFood, false);
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x00088F28 File Offset: 0x00087128
	public bool CanAbsorb(Edible other)
	{
		bool flag = this.spices.Count == other.spices.Count;
		int num = 0;
		while (flag && num < this.spices.Count)
		{
			int num2 = 0;
			while (flag && num2 < other.spices.Count)
			{
				flag = this.spices[num].Id == other.spices[num2].Id;
				num2++;
			}
			num++;
		}
		return flag;
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x00088FA9 File Offset: 0x000871A9
	private void StartConsuming()
	{
		DebugUtil.DevAssert(!this.isBeingConsumed, "Can't StartConsuming()...we've already started", null);
		this.isBeingConsumed = true;
		base.worker.Trigger(1406130139, this);
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x00088FD8 File Offset: 0x000871D8
	private void StopConsuming(Worker worker)
	{
		DebugUtil.DevAssert(this.isBeingConsumed, "StopConsuming() called without StartConsuming()", null);
		this.isBeingConsumed = false;
		if (this.primaryElement != null && this.primaryElement.DiseaseCount > 0)
		{
			new EmoteChore(worker.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, Db.Get().Emotes.Minion.FoodPoisoning, 1, null);
		}
		for (int i = 0; i < this.foodInfo.Effects.Count; i++)
		{
			worker.GetComponent<Effects>().Add(this.foodInfo.Effects[i], true);
		}
		ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, -this.caloriesConsumed, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.EATEN, "{0}", this.GetProperName()), worker.GetProperName());
		this.AddOnConsumeEffects(worker);
		worker.Trigger(1121894420, this);
		base.Trigger(-10536414, worker.gameObject);
		this.unitsConsumed = float.NaN;
		this.caloriesConsumed = float.NaN;
		this.totalUnits = float.NaN;
		if (this.Units <= 0f)
		{
			base.gameObject.DeleteObject();
		}
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x00089112 File Offset: 0x00087312
	public static string GetEffectForFoodQuality(int qualityLevel)
	{
		qualityLevel = Mathf.Clamp(qualityLevel, -1, 5);
		return Edible.qualityEffects[qualityLevel];
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x0008912C File Offset: 0x0008732C
	private void AddOnConsumeEffects(Worker worker)
	{
		int num = Mathf.RoundToInt(worker.GetAttributes().Add(Db.Get().Attributes.FoodExpectation).GetTotalValue());
		int num2 = this.FoodInfo.Quality + num;
		Effects component = worker.GetComponent<Effects>();
		component.Add(Edible.GetEffectForFoodQuality(num2), true);
		for (int i = 0; i < this.spices.Count; i++)
		{
			Effect statBonus = this.spices[i].StatBonus;
			if (statBonus != null)
			{
				float duration = statBonus.duration;
				statBonus.duration = this.caloriesConsumed * 0.001f / 1000f * 600f;
				component.Add(statBonus, true);
				statBonus.duration = duration;
			}
		}
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x000891ED File Offset: 0x000873ED
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Edibles.Remove(this);
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x00089200 File Offset: 0x00087400
	public int GetQuality()
	{
		return this.foodInfo.Quality;
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x00089210 File Offset: 0x00087410
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.CALORIES, GameUtil.GetFormattedCalories(this.foodInfo.CaloriesPerUnit, GameUtil.TimeSlice.None, true)), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.CALORIES, GameUtil.GetFormattedCalories(this.foodInfo.CaloriesPerUnit, GameUtil.TimeSlice.None, true)), Descriptor.DescriptorType.Information, false));
		list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(this.foodInfo.Quality)), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(this.foodInfo.Quality)), Descriptor.DescriptorType.Effect, false));
		foreach (string text in this.foodInfo.Effects)
		{
			string text2 = "";
			foreach (AttributeModifier attributeModifier in Db.Get().effects.Get(text).SelfModifiers)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					"\n    • ",
					Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + attributeModifier.AttributeId.ToUpper() + ".NAME"),
					": ",
					attributeModifier.GetFormattedString()
				});
			}
			list.Add(new Descriptor(Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".NAME"), Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".DESCRIPTION") + text2, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x00089418 File Offset: 0x00087618
	public void OnSplitTick(Pickupable thePieceTaken)
	{
		Edible component = thePieceTaken.GetComponent<Edible>();
		if (this.spices != null && component != null)
		{
			for (int i = 0; i < this.spices.Count; i++)
			{
				SpiceInstance spiceInstance = this.spices[i];
				component.SpiceEdible(spiceInstance, SpiceGrinderConfig.SpicedStatus);
			}
		}
	}

	// Token: 0x04000E3C RID: 3644
	private PrimaryElement primaryElement;

	// Token: 0x04000E3D RID: 3645
	public string FoodID;

	// Token: 0x04000E3E RID: 3646
	private EdiblesManager.FoodInfo foodInfo;

	// Token: 0x04000E40 RID: 3648
	public float unitsConsumed = float.NaN;

	// Token: 0x04000E41 RID: 3649
	public float caloriesConsumed = float.NaN;

	// Token: 0x04000E42 RID: 3650
	private float totalFeedingTime = float.NaN;

	// Token: 0x04000E43 RID: 3651
	private float totalUnits = float.NaN;

	// Token: 0x04000E44 RID: 3652
	private float totalConsumableCalories = float.NaN;

	// Token: 0x04000E45 RID: 3653
	[Serialize]
	private List<SpiceInstance> spices = new List<SpiceInstance>();

	// Token: 0x04000E46 RID: 3654
	private AttributeModifier caloriesModifier = new AttributeModifier("CaloriesDelta", 50000f, DUPLICANTS.MODIFIERS.EATINGCALORIES.NAME, false, true, true);

	// Token: 0x04000E47 RID: 3655
	private AttributeModifier caloriesLitSpaceModifier = new AttributeModifier("CaloriesDelta", 57500f, DUPLICANTS.MODIFIERS.EATINGCALORIES.NAME, false, true, true);

	// Token: 0x04000E48 RID: 3656
	private AttributeModifier currentModifier;

	// Token: 0x04000E49 RID: 3657
	private static readonly EventSystem.IntraObjectHandler<Edible> OnCraftDelegate = new EventSystem.IntraObjectHandler<Edible>(delegate(Edible component, object data)
	{
		component.OnCraft(data);
	});

	// Token: 0x04000E4A RID: 3658
	private static readonly HashedString[] normalWorkAnims = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04000E4B RID: 3659
	private static readonly HashedString[] hatWorkAnims = new HashedString[] { "hat_pre", "working_loop" };

	// Token: 0x04000E4C RID: 3660
	private static readonly HashedString[] saltWorkAnims = new HashedString[] { "salt_pre", "salt_loop" };

	// Token: 0x04000E4D RID: 3661
	private static readonly HashedString[] saltHatWorkAnims = new HashedString[] { "salt_hat_pre", "salt_hat_loop" };

	// Token: 0x04000E4E RID: 3662
	private static readonly HashedString[] normalWorkPstAnim = new HashedString[] { "working_pst" };

	// Token: 0x04000E4F RID: 3663
	private static readonly HashedString[] hatWorkPstAnim = new HashedString[] { "hat_pst" };

	// Token: 0x04000E50 RID: 3664
	private static readonly HashedString[] saltWorkPstAnim = new HashedString[] { "salt_pst" };

	// Token: 0x04000E51 RID: 3665
	private static readonly HashedString[] saltHatWorkPstAnim = new HashedString[] { "salt_hat_pst" };

	// Token: 0x04000E52 RID: 3666
	private static Dictionary<int, string> qualityEffects = new Dictionary<int, string>
	{
		{ -1, "EdibleMinus3" },
		{ 0, "EdibleMinus2" },
		{ 1, "EdibleMinus1" },
		{ 2, "Edible0" },
		{ 3, "Edible1" },
		{ 4, "Edible2" },
		{ 5, "Edible3" }
	};

	// Token: 0x020010C2 RID: 4290
	public class EdibleStartWorkInfo : Worker.StartWorkInfo
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600743B RID: 29755 RVA: 0x002B2716 File Offset: 0x002B0916
		// (set) Token: 0x0600743C RID: 29756 RVA: 0x002B271E File Offset: 0x002B091E
		public float amount { get; private set; }

		// Token: 0x0600743D RID: 29757 RVA: 0x002B2727 File Offset: 0x002B0927
		public EdibleStartWorkInfo(Workable workable, float amount)
			: base(workable)
		{
			this.amount = amount;
		}
	}
}
