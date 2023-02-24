using System;
using System.Collections.Generic;
using Database;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class SpiceGrinder : GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>
{
	// Token: 0x06000FF0 RID: 4080 RVA: 0x000564D0 File Offset: 0x000546D0
	public static void InitializeSpices()
	{
		Spices spices = Db.Get().Spices;
		SpiceGrinder.SettingOptions = new Dictionary<Tag, SpiceGrinder.Option>();
		for (int i = 0; i < spices.Count; i++)
		{
			Spice spice = spices[i];
			if (DlcManager.IsDlcListValidForCurrentContent(spice.DlcIds))
			{
				SpiceGrinder.SettingOptions.Add(spice.Id, new SpiceGrinder.Option(spice));
			}
		}
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x00056534 File Offset: 0x00054734
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.root.Enter(new StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.State.Callback(this.OnEnterRoot)).EventHandler(GameHashes.OnStorageChange, new GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.GameEvent.Callback(this.OnStorageChanged));
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.ready, new StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Transition.ConditionCallback(this.IsOperational)).Enter(delegate(SpiceGrinder.StatesInstance smi)
		{
			smi.Play((smi.SelectedOption != null) ? "off" : "default", KAnim.PlayMode.Once);
			if (smi.SelectedOption == null)
			{
				smi.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoSpiceSelected, null);
			}
		}).Exit(delegate(SpiceGrinder.StatesInstance smi)
		{
			smi.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoSpiceSelected, false);
		});
		this.operational.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Not(new StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Transition.ConditionCallback(this.IsOperational))).ParamTransition<bool>(this.isReady, this.ready, new StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Parameter<bool>.Callback(this.GrinderIsReady)).PlayAnim("on");
		this.ready.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Not(new StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Transition.ConditionCallback(this.IsOperational))).ParamTransition<bool>(this.isReady, this.operational, new StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.Parameter<bool>.Callback(this.GrinderNoLongerReady)).ToggleRecurringChore(new Func<SpiceGrinder.StatesInstance, Chore>(this.CreateChore), null);
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x00056688 File Offset: 0x00054888
	private void OnEnterRoot(SpiceGrinder.StatesInstance smi)
	{
		smi.Initialize();
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x00056690 File Offset: 0x00054890
	private bool GrinderIsReady(SpiceGrinder.StatesInstance smi, bool ready)
	{
		return this.isReady.Get(smi);
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0005669E File Offset: 0x0005489E
	private bool GrinderNoLongerReady(SpiceGrinder.StatesInstance smi, bool ready)
	{
		return !this.isReady.Get(smi);
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000566AF File Offset: 0x000548AF
	private bool IsOperational(SpiceGrinder.StatesInstance smi)
	{
		return smi.IsOperational;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x000566B8 File Offset: 0x000548B8
	private void OnStorageChanged(SpiceGrinder.StatesInstance smi, object data)
	{
		smi.UpdateMeter();
		if (smi.SelectedOption == null)
		{
			return;
		}
		bool flag = smi.AvailableFood > 0f && smi.CanSpice(smi.CurrentFood.Calories);
		smi.sm.isReady.Set(flag, smi, false);
		smi.UpdateFoodSymbol();
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x00056710 File Offset: 0x00054910
	private Chore CreateChore(SpiceGrinder.StatesInstance smi)
	{
		return new WorkChore<SpiceGrinderWorkable>(Db.Get().ChoreTypes.Cook, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x040008B7 RID: 2231
	public static Dictionary<Tag, SpiceGrinder.Option> SettingOptions = null;

	// Token: 0x040008B8 RID: 2232
	public static Operational.Flag spiceSet = new Operational.Flag("spiceSet", Operational.Flag.Type.Requirement);

	// Token: 0x040008B9 RID: 2233
	public GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.State inoperational;

	// Token: 0x040008BA RID: 2234
	public GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.State operational;

	// Token: 0x040008BB RID: 2235
	public GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.State ready;

	// Token: 0x040008BC RID: 2236
	public StateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.BoolParameter isReady;

	// Token: 0x02000F0E RID: 3854
	public class Option : IConfigurableConsumerOption
	{
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06006DBB RID: 28091 RVA: 0x0029ABD0 File Offset: 0x00298DD0
		public Effect StatBonus
		{
			get
			{
				if (this.statBonus == null)
				{
					return null;
				}
				if (string.IsNullOrEmpty(this.spiceDescription))
				{
					this.CreateDescription();
					this.GetName();
				}
				this.statBonus.Name = this.name;
				this.statBonus.description = this.spiceDescription;
				return this.statBonus;
			}
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x0029AC2C File Offset: 0x00298E2C
		public Option(Spice spice)
		{
			this.Id = new Tag(spice.Id);
			this.Spice = spice;
			if (spice.StatBonus != null)
			{
				this.statBonus = new Effect(spice.Id, this.GetName(), this.spiceDescription, 600f, true, false, false, null, -1f, 0f, null, "");
				this.statBonus.Add(spice.StatBonus);
				Db.Get().effects.Add(this.statBonus);
			}
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x0029ACBC File Offset: 0x00298EBC
		public Tag GetID()
		{
			return this.Spice.Id;
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x0029ACD0 File Offset: 0x00298ED0
		public string GetName()
		{
			if (string.IsNullOrEmpty(this.name))
			{
				string text = "STRINGS.ITEMS.SPICES." + this.Spice.Id.ToUpper() + ".NAME";
				StringEntry stringEntry;
				Strings.TryGet(text, out stringEntry);
				this.name = "MISSING " + text;
				if (stringEntry != null)
				{
					this.name = stringEntry;
				}
			}
			return this.name;
		}

		// Token: 0x06006DBF RID: 28095 RVA: 0x0029AD39 File Offset: 0x00298F39
		public string GetDetailedDescription()
		{
			if (string.IsNullOrEmpty(this.fullDescription))
			{
				this.CreateDescription();
			}
			return this.fullDescription;
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x0029AD54 File Offset: 0x00298F54
		public string GetDescription()
		{
			if (!string.IsNullOrEmpty(this.spiceDescription))
			{
				return this.spiceDescription;
			}
			string text = "STRINGS.ITEMS.SPICES." + this.Spice.Id.ToUpper() + ".DESC";
			StringEntry stringEntry;
			Strings.TryGet(text, out stringEntry);
			this.spiceDescription = "MISSING " + text;
			if (stringEntry != null)
			{
				this.spiceDescription = stringEntry.String;
			}
			return this.spiceDescription;
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x0029ADC4 File Offset: 0x00298FC4
		private void CreateDescription()
		{
			string text = "STRINGS.ITEMS.SPICES." + this.Spice.Id.ToUpper() + ".DESC";
			StringEntry stringEntry;
			Strings.TryGet(text, out stringEntry);
			this.spiceDescription = "MISSING " + text;
			if (stringEntry != null)
			{
				this.spiceDescription = stringEntry.String;
			}
			this.ingredientDescriptions = string.Format("\n\n<b>{0}</b>", BUILDINGS.PREFABS.SPICEGRINDER.INGREDIENTHEADER);
			for (int i = 0; i < this.Spice.Ingredients.Length; i++)
			{
				Spice.Ingredient ingredient = this.Spice.Ingredients[i];
				GameObject prefab = Assets.GetPrefab((ingredient.IngredientSet != null && ingredient.IngredientSet.Length != 0) ? ingredient.IngredientSet[0] : null);
				this.ingredientDescriptions += string.Format("\n{0}{1} {2}{3}", new object[]
				{
					"    • ",
					prefab.GetProperName(),
					ingredient.AmountKG,
					GameUtil.GetUnitTypeMassOrUnit(prefab)
				});
			}
			this.fullDescription = this.spiceDescription + this.ingredientDescriptions;
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x0029AEE9 File Offset: 0x002990E9
		public Sprite GetIcon()
		{
			return Assets.GetSprite(this.Spice.Image);
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x0029AF00 File Offset: 0x00299100
		public IConfigurableConsumerIngredient[] GetIngredients()
		{
			return this.Spice.Ingredients;
		}

		// Token: 0x040052FE RID: 21246
		public readonly Tag Id;

		// Token: 0x040052FF RID: 21247
		public readonly Spice Spice;

		// Token: 0x04005300 RID: 21248
		private string name;

		// Token: 0x04005301 RID: 21249
		private string fullDescription;

		// Token: 0x04005302 RID: 21250
		private string spiceDescription;

		// Token: 0x04005303 RID: 21251
		private string ingredientDescriptions;

		// Token: 0x04005304 RID: 21252
		private Effect statBonus;
	}

	// Token: 0x02000F0F RID: 3855
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000F10 RID: 3856
	public class StatesInstance : GameStateMachine<SpiceGrinder, SpiceGrinder.StatesInstance, IStateMachineTarget, SpiceGrinder.Def>.GameInstance
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06006DC5 RID: 28101 RVA: 0x0029AF22 File Offset: 0x00299122
		public bool IsOperational
		{
			get
			{
				return this.operational != null && this.operational.IsOperational;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06006DC6 RID: 28102 RVA: 0x0029AF3F File Offset: 0x0029913F
		public float AvailableFood
		{
			get
			{
				if (!(this.foodStorage == null))
				{
					return this.foodStorage.MassStored();
				}
				return 0f;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06006DC7 RID: 28103 RVA: 0x0029AF60 File Offset: 0x00299160
		public float AvailableSeeds
		{
			get
			{
				if (!(this.seedStorage == null))
				{
					return this.seedStorage.MassStored();
				}
				return 0f;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06006DC8 RID: 28104 RVA: 0x0029AF81 File Offset: 0x00299181
		public SpiceGrinder.Option SelectedOption
		{
			get
			{
				if (!(this.currentSpice.Id == Tag.Invalid))
				{
					return SpiceGrinder.SettingOptions[this.currentSpice.Id];
				}
				return null;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06006DC9 RID: 28105 RVA: 0x0029AFB4 File Offset: 0x002991B4
		public Edible CurrentFood
		{
			get
			{
				GameObject gameObject = this.foodStorage.FindFirst(GameTags.Edible);
				this.currentFood = ((gameObject != null) ? gameObject.GetComponent<Edible>() : null);
				return this.currentFood;
			}
		}

		// Token: 0x06006DCA RID: 28106 RVA: 0x0029AFF0 File Offset: 0x002991F0
		public StatesInstance(IStateMachineTarget master, SpiceGrinder.Def def)
			: base(master, def)
		{
			this.workable.Grinder = this;
			Storage[] components = base.gameObject.GetComponents<Storage>();
			this.foodStorage = components[0];
			this.seedStorage = components[1];
			this.operational = base.GetComponent<Operational>();
			this.seedDelivery = base.GetComponent<ManualDeliveryKG>();
			this.kbac = base.GetComponent<KBatchedAnimController>();
			this.foodStorageFilter = new FilteredStorage(base.GetComponent<KPrefabID>(), this.foodFilter, null, false, Db.Get().ChoreTypes.CookFetch);
			this.foodStorageFilter.SetHasMeter(false);
			this.meter = new MeterController(this.kbac, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_frame", "meter_level" });
			this.SetupFoodSymbol();
			this.UpdateFoodSymbol();
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x0029B0EC File Offset: 0x002992EC
		public void Initialize()
		{
			SpiceGrinder.Option option;
			SpiceGrinder.SettingOptions.TryGetValue(new Tag(this.spiceHash), out option);
			this.OnOptionSelected(option);
			base.sm.OnStorageChanged(this, null);
			this.UpdateMeter();
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x0029B12C File Offset: 0x0029932C
		private void OnCopySettings(object data)
		{
			SpiceGrinderWorkable component = ((GameObject)data).GetComponent<SpiceGrinderWorkable>();
			if (component != null)
			{
				this.currentSpice = component.Grinder.currentSpice;
				SpiceGrinder.Option option;
				SpiceGrinder.SettingOptions.TryGetValue(new Tag(component.Grinder.spiceHash), out option);
				this.OnOptionSelected(option);
			}
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x0029B184 File Offset: 0x00299384
		public void SetupFoodSymbol()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "foodSymbol";
			gameObject.SetActive(false);
			bool flag;
			Vector3 vector = this.kbac.GetSymbolTransform(SpiceGrinder.StatesInstance.HASH_FOOD, out flag).GetColumn(3);
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Building);
			gameObject.transform.SetPosition(vector);
			this.foodKBAC = gameObject.AddComponent<KBatchedAnimController>();
			this.foodKBAC.AnimFiles = new KAnimFile[] { Assets.GetAnim("mushbar_kanim") };
			this.foodKBAC.initialAnim = "object";
			this.kbac.SetSymbolVisiblity(SpiceGrinder.StatesInstance.HASH_FOOD, false);
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x0029B240 File Offset: 0x00299440
		public void UpdateFoodSymbol()
		{
			bool flag = this.AvailableFood > 0f && this.CurrentFood != null;
			this.foodKBAC.gameObject.SetActive(flag);
			if (flag)
			{
				this.foodKBAC.SwapAnims(this.CurrentFood.GetComponent<KBatchedAnimController>().AnimFiles);
				this.foodKBAC.Play("object", KAnim.PlayMode.Loop, 1f, 0f);
			}
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x0029B2B9 File Offset: 0x002994B9
		public void UpdateMeter()
		{
			this.meter.SetPositionPercent(this.seedStorage.MassStored() / this.seedStorage.capacityKg);
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x0029B2E0 File Offset: 0x002994E0
		public void SpiceFood()
		{
			float num = this.CurrentFood.Calories / 1000f;
			foreach (Spice.Ingredient ingredient in SpiceGrinder.SettingOptions[this.currentSpice.Id].Spice.Ingredients)
			{
				float num2 = num * ingredient.AmountKG / 1000f;
				int num3 = ingredient.IngredientSet.Length - 1;
				while (num2 > 0f && num3 >= 0)
				{
					Tag tag = ingredient.IngredientSet[num3];
					float num4;
					SimUtil.DiseaseInfo diseaseInfo;
					float num5;
					this.seedStorage.ConsumeAndGetDisease(tag, num2, out num4, out diseaseInfo, out num5);
					num2 -= num4;
					num3--;
				}
			}
			this.CurrentFood.SpiceEdible(this.currentSpice, SpiceGrinderConfig.SpicedStatus);
			this.foodStorage.Drop(this.CurrentFood.gameObject, true);
			this.currentFood = null;
			this.UpdateFoodSymbol();
			base.sm.isReady.Set(false, this, false);
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x0029B3E0 File Offset: 0x002995E0
		public bool CanSpice(float kcalToSpice)
		{
			bool flag = true;
			float num = kcalToSpice / 1000f;
			Spice.Ingredient[] ingredients = SpiceGrinder.SettingOptions[this.currentSpice.Id].Spice.Ingredients;
			int num2 = 0;
			while (flag && num2 < ingredients.Length)
			{
				Spice.Ingredient ingredient = ingredients[num2];
				float num3 = 0f;
				int num4 = 0;
				while (ingredient.IngredientSet != null && num4 < ingredient.IngredientSet.Length)
				{
					num3 += this.seedStorage.GetMassAvailable(ingredient.IngredientSet[num4]);
					num4++;
				}
				flag = num * ingredient.AmountKG / 1000f <= num3;
				num2++;
			}
			return flag;
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x0029B48C File Offset: 0x0029968C
		public void OnOptionSelected(SpiceGrinder.Option spiceOption)
		{
			base.smi.GetComponent<Operational>().SetFlag(SpiceGrinder.spiceSet, spiceOption != null);
			if (spiceOption == null)
			{
				this.kbac.Play("default", KAnim.PlayMode.Once, 1f, 0f);
				this.kbac.SetSymbolTint("stripe_anim2", Color.white);
			}
			else
			{
				this.kbac.Play(this.IsOperational ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
			}
			this.seedDelivery.Pause(true, "Spice Changed");
			this.seedDelivery.ClearRequests();
			if (this.currentSpice.Id != Tag.Invalid)
			{
				this.seedStorage.DropAll(false, false, default(Vector3), true, null);
				this.UpdateMeter();
				base.sm.isReady.Set(false, this, false);
			}
			if (spiceOption != null)
			{
				this.currentSpice = new SpiceInstance
				{
					Id = spiceOption.Id,
					TotalKG = spiceOption.Spice.TotalKG
				};
				this.SetSpiceSymbolColours(spiceOption.Spice);
				this.spiceHash = this.currentSpice.Id.GetHash();
				this.seedStorage.capacityKg = this.currentSpice.TotalKG * 10f;
				this.seedDelivery.capacity = this.seedStorage.capacityKg;
				this.seedDelivery.refillMass = this.seedStorage.capacityKg * 0.5f;
				foreach (Spice.Ingredient ingredient in spiceOption.Spice.Ingredients)
				{
					this.seedDelivery.RequestItem(ingredient.IngredientSet, ingredient.AmountKG);
				}
				this.foodFilter[0] = this.currentSpice.Id;
				this.foodStorageFilter.FilterChanged();
				this.seedDelivery.Pause(false, "Spice Changed");
			}
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x0029B694 File Offset: 0x00299894
		private void SetSpiceSymbolColours(Spice spice)
		{
			this.kbac.SetSymbolTint("stripe_anim2", spice.PrimaryColor);
			this.kbac.SetSymbolTint("stripe_anim1", spice.SecondaryColor);
			this.kbac.SetSymbolTint("grinder", spice.PrimaryColor);
		}

		// Token: 0x04005305 RID: 21253
		private static string HASH_FOOD = "food";

		// Token: 0x04005306 RID: 21254
		private KBatchedAnimController kbac;

		// Token: 0x04005307 RID: 21255
		private KBatchedAnimController foodKBAC;

		// Token: 0x04005308 RID: 21256
		[MyCmpReq]
		public SpiceGrinderWorkable workable;

		// Token: 0x04005309 RID: 21257
		[Serialize]
		private int spiceHash;

		// Token: 0x0400530A RID: 21258
		private SpiceInstance currentSpice;

		// Token: 0x0400530B RID: 21259
		private Edible currentFood;

		// Token: 0x0400530C RID: 21260
		private Storage seedStorage;

		// Token: 0x0400530D RID: 21261
		private Storage foodStorage;

		// Token: 0x0400530E RID: 21262
		private MeterController meter;

		// Token: 0x0400530F RID: 21263
		private Tag[] foodFilter = new Tag[1];

		// Token: 0x04005310 RID: 21264
		private FilteredStorage foodStorageFilter;

		// Token: 0x04005311 RID: 21265
		private Operational operational;

		// Token: 0x04005312 RID: 21266
		private ManualDeliveryKG seedDelivery;
	}
}
