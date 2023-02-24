using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003BC RID: 956
public class ModifierSet : ScriptableObject
{
	// Token: 0x060013BB RID: 5051 RVA: 0x00068860 File Offset: 0x00066A60
	public virtual void Initialize()
	{
		this.ResourceTable = new List<Resource>();
		this.Root = new ResourceSet<Resource>("Root", null);
		this.modifierInfos = new ModifierSet.ModifierInfos();
		this.modifierInfos.Load(this.modifiersFile);
		this.Attributes = new Database.Attributes(this.Root);
		this.BuildingAttributes = new BuildingAttributes(this.Root);
		this.CritterAttributes = new CritterAttributes(this.Root);
		this.PlantAttributes = new PlantAttributes(this.Root);
		this.effects = new ResourceSet<Effect>("Effects", this.Root);
		this.traits = new ModifierSet.TraitSet();
		this.traitGroups = new ModifierSet.TraitGroupSet();
		this.FertilityModifiers = new FertilityModifiers();
		this.Amounts = new Database.Amounts();
		this.Amounts.Load();
		this.AttributeConverters = new Database.AttributeConverters();
		this.LoadEffects();
		this.LoadFertilityModifiers();
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x0006894D File Offset: 0x00066B4D
	public static float ConvertValue(float value, Units units)
	{
		if (Units.PerDay == units)
		{
			return value * 0.0016666667f;
		}
		return value;
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x0006895C File Offset: 0x00066B5C
	private void LoadEffects()
	{
		foreach (ModifierSet.ModifierInfo modifierInfo in this.modifierInfos)
		{
			if (!this.effects.Exists(modifierInfo.Id) && (modifierInfo.Type == "Effect" || modifierInfo.Type == "Base" || modifierInfo.Type == "Need"))
			{
				string text = Strings.Get(string.Format("STRINGS.DUPLICANTS.MODIFIERS.{0}.NAME", modifierInfo.Id.ToUpper()));
				string text2 = Strings.Get(string.Format("STRINGS.DUPLICANTS.MODIFIERS.{0}.TOOLTIP", modifierInfo.Id.ToUpper()));
				Effect effect = new Effect(modifierInfo.Id, text, text2, modifierInfo.Duration * 600f, modifierInfo.ShowInUI && modifierInfo.Type != "Need", modifierInfo.TriggerFloatingText, modifierInfo.IsBad, modifierInfo.EmoteAnim, modifierInfo.EmoteCooldown, modifierInfo.StompGroup, modifierInfo.CustomIcon);
				foreach (ModifierSet.ModifierInfo modifierInfo2 in this.modifierInfos)
				{
					if (modifierInfo2.Id == modifierInfo.Id)
					{
						effect.Add(new AttributeModifier(modifierInfo2.Attribute, ModifierSet.ConvertValue(modifierInfo2.Value, modifierInfo2.Units), text, modifierInfo2.Multiplier, false, true));
					}
				}
				this.effects.Add(effect);
			}
		}
		Effect effect2 = new Effect("Ranched", STRINGS.CREATURES.MODIFIERS.RANCHED.NAME, STRINGS.CREATURES.MODIFIERS.RANCHED.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect2.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 5f, STRINGS.CREATURES.MODIFIERS.RANCHED.NAME, false, false, true));
		effect2.Add(new AttributeModifier(Db.Get().Amounts.Wildness.deltaAttribute.Id, -0.09166667f, STRINGS.CREATURES.MODIFIERS.RANCHED.NAME, false, false, true));
		this.effects.Add(effect2);
		Effect effect3 = new Effect("EggSong", STRINGS.CREATURES.MODIFIERS.INCUBATOR_SONG.NAME, STRINGS.CREATURES.MODIFIERS.INCUBATOR_SONG.TOOLTIP, 600f, true, false, false, null, -1f, 0f, null, "");
		effect3.Add(new AttributeModifier(Db.Get().Amounts.Incubation.deltaAttribute.Id, 4f, STRINGS.CREATURES.MODIFIERS.INCUBATOR_SONG.NAME, true, false, true));
		this.effects.Add(effect3);
		Effect effect4 = new Effect("EggHug", STRINGS.CREATURES.MODIFIERS.EGGHUG.NAME, STRINGS.CREATURES.MODIFIERS.EGGHUG.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect4.Add(new AttributeModifier(Db.Get().Amounts.Incubation.deltaAttribute.Id, 1f, STRINGS.CREATURES.MODIFIERS.EGGHUG.NAME, true, false, true));
		this.effects.Add(effect4);
		Effect effect5 = new Effect("HuggingFrenzy", STRINGS.CREATURES.MODIFIERS.HUGGINGFRENZY.NAME, STRINGS.CREATURES.MODIFIERS.HUGGINGFRENZY.TOOLTIP, 600f, true, false, false, null, -1f, 0f, null, "");
		this.effects.Add(effect5);
		Reactable.ReactablePrecondition reactablePrecondition = delegate(GameObject go, Navigator.ActiveTransition n)
		{
			int num = Grid.PosToCell(go);
			return Grid.IsValidCell(num) && Grid.IsGas(num);
		};
		this.effects.Get("WetFeet").AddEmotePrecondition(reactablePrecondition);
		this.effects.Get("SoakingWet").AddEmotePrecondition(reactablePrecondition);
		Effect effect6 = new Effect("DivergentCropTended", STRINGS.CREATURES.MODIFIERS.DIVERGENTPLANTTENDED.NAME, STRINGS.CREATURES.MODIFIERS.DIVERGENTPLANTTENDED.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect6.Add(new AttributeModifier(Db.Get().Amounts.Maturity.deltaAttribute.Id, 0.05f, STRINGS.CREATURES.MODIFIERS.DIVERGENTPLANTTENDED.NAME, true, false, true));
		this.effects.Add(effect6);
		Effect effect7 = new Effect("DivergentCropTendedWorm", STRINGS.CREATURES.MODIFIERS.DIVERGENTPLANTTENDEDWORM.NAME, STRINGS.CREATURES.MODIFIERS.DIVERGENTPLANTTENDEDWORM.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect7.Add(new AttributeModifier(Db.Get().Amounts.Maturity.deltaAttribute.Id, 0.5f, STRINGS.CREATURES.MODIFIERS.DIVERGENTPLANTTENDEDWORM.NAME, true, false, true));
		this.effects.Add(effect7);
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x00068E84 File Offset: 0x00067084
	public Trait CreateTrait(string id, string name, string description, string group_name, bool should_save, ChoreGroup[] disabled_chore_groups, bool positive_trait, bool is_valid_starter_trait)
	{
		Trait trait = new Trait(id, name, description, 0f, should_save, disabled_chore_groups, positive_trait, is_valid_starter_trait);
		this.traits.Add(trait);
		if (group_name == "" || group_name == null)
		{
			group_name = "Default";
		}
		TraitGroup traitGroup = this.traitGroups.TryGet(group_name);
		if (traitGroup == null)
		{
			traitGroup = new TraitGroup(group_name, group_name, group_name != "Default");
			this.traitGroups.Add(traitGroup);
		}
		traitGroup.Add(trait);
		return trait;
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x00068F0C File Offset: 0x0006710C
	public FertilityModifier CreateFertilityModifier(string id, Tag targetTag, string name, string description, Func<string, string> tooltipCB, FertilityModifier.FertilityModFn applyFunction)
	{
		FertilityModifier fertilityModifier = new FertilityModifier(id, targetTag, name, description, tooltipCB, applyFunction);
		this.FertilityModifiers.Add(fertilityModifier);
		return fertilityModifier;
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x00068F36 File Offset: 0x00067136
	protected void LoadTraits()
	{
		TRAITS.TRAIT_CREATORS.ForEach(delegate(System.Action action)
		{
			action();
		});
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x00068F61 File Offset: 0x00067161
	protected void LoadFertilityModifiers()
	{
		TUNING.CREATURES.EGG_CHANCE_MODIFIERS.MODIFIER_CREATORS.ForEach(delegate(System.Action action)
		{
			action();
		});
	}

	// Token: 0x04000AC1 RID: 2753
	public TextAsset modifiersFile;

	// Token: 0x04000AC2 RID: 2754
	public ModifierSet.ModifierInfos modifierInfos;

	// Token: 0x04000AC3 RID: 2755
	public ModifierSet.TraitSet traits;

	// Token: 0x04000AC4 RID: 2756
	public ResourceSet<Effect> effects;

	// Token: 0x04000AC5 RID: 2757
	public ModifierSet.TraitGroupSet traitGroups;

	// Token: 0x04000AC6 RID: 2758
	public FertilityModifiers FertilityModifiers;

	// Token: 0x04000AC7 RID: 2759
	public Database.Attributes Attributes;

	// Token: 0x04000AC8 RID: 2760
	public BuildingAttributes BuildingAttributes;

	// Token: 0x04000AC9 RID: 2761
	public CritterAttributes CritterAttributes;

	// Token: 0x04000ACA RID: 2762
	public PlantAttributes PlantAttributes;

	// Token: 0x04000ACB RID: 2763
	public Database.Amounts Amounts;

	// Token: 0x04000ACC RID: 2764
	public Database.AttributeConverters AttributeConverters;

	// Token: 0x04000ACD RID: 2765
	public ResourceSet Root;

	// Token: 0x04000ACE RID: 2766
	public List<Resource> ResourceTable;

	// Token: 0x02000FE5 RID: 4069
	public class ModifierInfo : Resource
	{
		// Token: 0x040055BE RID: 21950
		public string Type;

		// Token: 0x040055BF RID: 21951
		public string Attribute;

		// Token: 0x040055C0 RID: 21952
		public float Value;

		// Token: 0x040055C1 RID: 21953
		public Units Units;

		// Token: 0x040055C2 RID: 21954
		public bool Multiplier;

		// Token: 0x040055C3 RID: 21955
		public float Duration;

		// Token: 0x040055C4 RID: 21956
		public bool ShowInUI;

		// Token: 0x040055C5 RID: 21957
		public string StompGroup;

		// Token: 0x040055C6 RID: 21958
		public bool IsBad;

		// Token: 0x040055C7 RID: 21959
		public string CustomIcon;

		// Token: 0x040055C8 RID: 21960
		public bool TriggerFloatingText;

		// Token: 0x040055C9 RID: 21961
		public string EmoteAnim;

		// Token: 0x040055CA RID: 21962
		public float EmoteCooldown;
	}

	// Token: 0x02000FE6 RID: 4070
	[Serializable]
	public class ModifierInfos : ResourceLoader<ModifierSet.ModifierInfo>
	{
	}

	// Token: 0x02000FE7 RID: 4071
	[Serializable]
	public class TraitSet : ResourceSet<Trait>
	{
	}

	// Token: 0x02000FE8 RID: 4072
	[Serializable]
	public class TraitGroupSet : ResourceSet<TraitGroup>
	{
	}
}
