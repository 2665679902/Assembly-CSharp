using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei.AI;
using STRINGS;

// Token: 0x02000738 RID: 1848
[DebuggerDisplay("{name}")]
[Serializable]
public class Element : IComparable<Element>
{
	// Token: 0x06003287 RID: 12935 RVA: 0x00110DD8 File Offset: 0x0010EFD8
	public float PressureToMass(float pressure)
	{
		return pressure / this.defaultValues.pressure;
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06003288 RID: 12936 RVA: 0x00110DE7 File Offset: 0x0010EFE7
	public bool IsUnstable
	{
		get
		{
			return this.HasTag(GameTags.Unstable);
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06003289 RID: 12937 RVA: 0x00110DF4 File Offset: 0x0010EFF4
	public bool IsLiquid
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Liquid;
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x0600328A RID: 12938 RVA: 0x00110E01 File Offset: 0x0010F001
	public bool IsGas
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Gas;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x0600328B RID: 12939 RVA: 0x00110E0E File Offset: 0x0010F00E
	public bool IsSolid
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Solid;
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x0600328C RID: 12940 RVA: 0x00110E1B File Offset: 0x0010F01B
	public bool IsVacuum
	{
		get
		{
			return (this.state & Element.State.Solid) == Element.State.Vacuum;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x0600328D RID: 12941 RVA: 0x00110E28 File Offset: 0x0010F028
	public bool IsTemperatureInsulated
	{
		get
		{
			return (this.state & Element.State.TemperatureInsulated) > Element.State.Vacuum;
		}
	}

	// Token: 0x0600328E RID: 12942 RVA: 0x00110E36 File Offset: 0x0010F036
	public bool IsState(Element.State expected_state)
	{
		return (this.state & Element.State.Solid) == expected_state;
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x0600328F RID: 12943 RVA: 0x00110E43 File Offset: 0x0010F043
	public bool HasTransitionUp
	{
		get
		{
			return this.highTempTransitionTarget != (SimHashes)0 && this.highTempTransitionTarget != SimHashes.Unobtanium && this.highTempTransition != null && this.highTempTransition != this;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06003290 RID: 12944 RVA: 0x00110E70 File Offset: 0x0010F070
	// (set) Token: 0x06003291 RID: 12945 RVA: 0x00110E78 File Offset: 0x0010F078
	public string name { get; set; }

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06003292 RID: 12946 RVA: 0x00110E81 File Offset: 0x0010F081
	// (set) Token: 0x06003293 RID: 12947 RVA: 0x00110E89 File Offset: 0x0010F089
	public string nameUpperCase { get; set; }

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06003294 RID: 12948 RVA: 0x00110E92 File Offset: 0x0010F092
	// (set) Token: 0x06003295 RID: 12949 RVA: 0x00110E9A File Offset: 0x0010F09A
	public string description { get; set; }

	// Token: 0x06003296 RID: 12950 RVA: 0x00110EA3 File Offset: 0x0010F0A3
	public string GetStateString()
	{
		return Element.GetStateString(this.state);
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x00110EB0 File Offset: 0x0010F0B0
	public static string GetStateString(Element.State state)
	{
		if ((state & Element.State.Solid) == Element.State.Solid)
		{
			return ELEMENTS.STATE.SOLID;
		}
		if ((state & Element.State.Solid) == Element.State.Liquid)
		{
			return ELEMENTS.STATE.LIQUID;
		}
		if ((state & Element.State.Solid) == Element.State.Gas)
		{
			return ELEMENTS.STATE.GAS;
		}
		return ELEMENTS.STATE.VACUUM;
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x00110EF0 File Offset: 0x0010F0F0
	public string FullDescription(bool addHardnessColor = true)
	{
		string text = this.Description();
		if (this.IsSolid)
		{
			text += "\n\n";
			text += string.Format(ELEMENTS.ELEMENTDESCSOLID, this.GetMaterialCategoryTag().ProperName(), GameUtil.GetFormattedTemperature(this.highTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetHardnessString(this, addHardnessColor));
		}
		else if (this.IsLiquid)
		{
			text += "\n\n";
			text += string.Format(ELEMENTS.ELEMENTDESCLIQUID, this.GetMaterialCategoryTag().ProperName(), GameUtil.GetFormattedTemperature(this.lowTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(this.highTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		}
		else if (!this.IsVacuum)
		{
			text += "\n\n";
			text += string.Format(ELEMENTS.ELEMENTDESCGAS, this.GetMaterialCategoryTag().ProperName(), GameUtil.GetFormattedTemperature(this.lowTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		}
		string text2 = ELEMENTS.THERMALPROPERTIES;
		text2 = text2.Replace("{SPECIFIC_HEAT_CAPACITY}", GameUtil.GetFormattedSHC(this.specificHeatCapacity));
		text2 = text2.Replace("{THERMAL_CONDUCTIVITY}", GameUtil.GetFormattedThermalConductivity(this.thermalConductivity));
		text = text + "\n" + text2;
		if (DlcManager.FeatureRadiationEnabled())
		{
			text = text + "\n" + string.Format(ELEMENTS.RADIATIONPROPERTIES, this.radiationAbsorptionFactor, GameUtil.GetFormattedRads(this.radiationPer1000Mass * 1.1f / 600f, GameUtil.TimeSlice.PerCycle));
		}
		if (this.oreTags.Length != 0 && !this.IsVacuum)
		{
			text += "\n\n";
			string text3 = "";
			for (int i = 0; i < this.oreTags.Length; i++)
			{
				Tag tag = new Tag(this.oreTags[i]);
				text3 += tag.ProperName();
				if (i < this.oreTags.Length - 1)
				{
					text3 += ", ";
				}
			}
			text += string.Format(ELEMENTS.ELEMENTPROPERTIES, text3);
		}
		if (this.attributeModifiers.Count > 0)
		{
			foreach (AttributeModifier attributeModifier in this.attributeModifiers)
			{
				string name = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId).Name;
				string formattedString = attributeModifier.GetFormattedString();
				text = text + "\n" + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name, formattedString);
			}
		}
		return text;
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x00111194 File Offset: 0x0010F394
	public string Description()
	{
		return this.description;
	}

	// Token: 0x0600329A RID: 12954 RVA: 0x0011119C File Offset: 0x0010F39C
	public bool HasTag(Tag search_tag)
	{
		return this.tag == search_tag || Array.IndexOf<Tag>(this.oreTags, search_tag) != -1;
	}

	// Token: 0x0600329B RID: 12955 RVA: 0x001111C0 File Offset: 0x0010F3C0
	public Tag GetMaterialCategoryTag()
	{
		return this.materialCategory;
	}

	// Token: 0x0600329C RID: 12956 RVA: 0x001111C8 File Offset: 0x0010F3C8
	public int CompareTo(Element other)
	{
		return this.id - other.id;
	}

	// Token: 0x04001EEA RID: 7914
	public const int INVALID_ID = 0;

	// Token: 0x04001EEB RID: 7915
	public SimHashes id;

	// Token: 0x04001EEC RID: 7916
	public Tag tag;

	// Token: 0x04001EED RID: 7917
	public ushort idx;

	// Token: 0x04001EEE RID: 7918
	public float specificHeatCapacity;

	// Token: 0x04001EEF RID: 7919
	public float thermalConductivity = 1f;

	// Token: 0x04001EF0 RID: 7920
	public float molarMass = 1f;

	// Token: 0x04001EF1 RID: 7921
	public float strength;

	// Token: 0x04001EF2 RID: 7922
	public float flow;

	// Token: 0x04001EF3 RID: 7923
	public float maxCompression;

	// Token: 0x04001EF4 RID: 7924
	public float viscosity;

	// Token: 0x04001EF5 RID: 7925
	public float minHorizontalFlow = float.PositiveInfinity;

	// Token: 0x04001EF6 RID: 7926
	public float minVerticalFlow = float.PositiveInfinity;

	// Token: 0x04001EF7 RID: 7927
	public float maxMass = 10000f;

	// Token: 0x04001EF8 RID: 7928
	public float solidSurfaceAreaMultiplier;

	// Token: 0x04001EF9 RID: 7929
	public float liquidSurfaceAreaMultiplier;

	// Token: 0x04001EFA RID: 7930
	public float gasSurfaceAreaMultiplier;

	// Token: 0x04001EFB RID: 7931
	public Element.State state;

	// Token: 0x04001EFC RID: 7932
	public byte hardness;

	// Token: 0x04001EFD RID: 7933
	public float lowTemp;

	// Token: 0x04001EFE RID: 7934
	public SimHashes lowTempTransitionTarget;

	// Token: 0x04001EFF RID: 7935
	public Element lowTempTransition;

	// Token: 0x04001F00 RID: 7936
	public float highTemp;

	// Token: 0x04001F01 RID: 7937
	public SimHashes highTempTransitionTarget;

	// Token: 0x04001F02 RID: 7938
	public Element highTempTransition;

	// Token: 0x04001F03 RID: 7939
	public SimHashes highTempTransitionOreID = SimHashes.Vacuum;

	// Token: 0x04001F04 RID: 7940
	public float highTempTransitionOreMassConversion;

	// Token: 0x04001F05 RID: 7941
	public SimHashes lowTempTransitionOreID = SimHashes.Vacuum;

	// Token: 0x04001F06 RID: 7942
	public float lowTempTransitionOreMassConversion;

	// Token: 0x04001F07 RID: 7943
	public SimHashes sublimateId;

	// Token: 0x04001F08 RID: 7944
	public SimHashes convertId;

	// Token: 0x04001F09 RID: 7945
	public SpawnFXHashes sublimateFX;

	// Token: 0x04001F0A RID: 7946
	public float sublimateRate;

	// Token: 0x04001F0B RID: 7947
	public float sublimateEfficiency;

	// Token: 0x04001F0C RID: 7948
	public float sublimateProbability;

	// Token: 0x04001F0D RID: 7949
	public float offGasPercentage;

	// Token: 0x04001F0E RID: 7950
	public float lightAbsorptionFactor;

	// Token: 0x04001F0F RID: 7951
	public float radiationAbsorptionFactor;

	// Token: 0x04001F10 RID: 7952
	public float radiationPer1000Mass;

	// Token: 0x04001F11 RID: 7953
	public Sim.PhysicsData defaultValues;

	// Token: 0x04001F12 RID: 7954
	public float toxicity;

	// Token: 0x04001F13 RID: 7955
	public Substance substance;

	// Token: 0x04001F14 RID: 7956
	public Tag materialCategory;

	// Token: 0x04001F15 RID: 7957
	public int buildMenuSort;

	// Token: 0x04001F16 RID: 7958
	public ElementLoader.ElementComposition[] elementComposition;

	// Token: 0x04001F17 RID: 7959
	public Tag[] oreTags = new Tag[0];

	// Token: 0x04001F18 RID: 7960
	public List<AttributeModifier> attributeModifiers = new List<AttributeModifier>();

	// Token: 0x04001F19 RID: 7961
	public bool disabled;

	// Token: 0x04001F1A RID: 7962
	public string dlcId;

	// Token: 0x04001F1B RID: 7963
	public const byte StateMask = 3;

	// Token: 0x0200143E RID: 5182
	[Serializable]
	public enum State : byte
	{
		// Token: 0x040062E4 RID: 25316
		Vacuum,
		// Token: 0x040062E5 RID: 25317
		Gas,
		// Token: 0x040062E6 RID: 25318
		Liquid,
		// Token: 0x040062E7 RID: 25319
		Solid,
		// Token: 0x040062E8 RID: 25320
		Unbreakable,
		// Token: 0x040062E9 RID: 25321
		Unstable = 8,
		// Token: 0x040062EA RID: 25322
		TemperatureInsulated = 16
	}
}
