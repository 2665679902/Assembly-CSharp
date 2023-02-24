using System;
using System.Collections.Generic;
using System.Linq;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000792 RID: 1938
public class Geyser : StateMachineComponent<Geyser.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x06003636 RID: 13878 RVA: 0x0012C686 File Offset: 0x0012A886
	// (set) Token: 0x06003635 RID: 13877 RVA: 0x0012C67D File Offset: 0x0012A87D
	public float timeShift { get; private set; }

	// Token: 0x06003637 RID: 13879 RVA: 0x0012C68E File Offset: 0x0012A88E
	public float GetCurrentLifeTime()
	{
		return GameClock.Instance.GetTime() + this.timeShift;
	}

	// Token: 0x06003638 RID: 13880 RVA: 0x0012C6A4 File Offset: 0x0012A8A4
	public void AlterTime(float timeOffset)
	{
		this.timeShift = Mathf.Max(timeOffset, -GameClock.Instance.GetTime());
		float num = this.RemainingEruptTime();
		float num2 = this.RemainingNonEruptTime();
		float num3 = this.RemainingActiveTime();
		float num4 = this.RemainingDormantTime();
		this.configuration.GetYearLength();
		if (num2 == 0f)
		{
			if ((num4 == 0f && this.configuration.GetYearOnDuration() - num3 < this.configuration.GetOnDuration() - num) | (num3 == 0f && this.configuration.GetYearOffDuration() - num4 >= this.configuration.GetOnDuration() - num))
			{
				base.smi.GoTo(base.smi.sm.dormant);
				return;
			}
			base.smi.GoTo(base.smi.sm.erupt);
			return;
		}
		else
		{
			bool flag = (num4 == 0f && this.configuration.GetYearOnDuration() - num3 < this.configuration.GetIterationLength() - num2) | (num3 == 0f && this.configuration.GetYearOffDuration() - num4 >= this.configuration.GetIterationLength() - num2);
			float num5 = this.RemainingEruptPreTime();
			if (flag && num5 <= 0f)
			{
				base.smi.GoTo(base.smi.sm.dormant);
				return;
			}
			if (num5 <= 0f)
			{
				base.smi.GoTo(base.smi.sm.idle);
				return;
			}
			float num6 = this.PreDuration() - num5;
			if ((num3 == 0f) ? (this.configuration.GetYearOffDuration() - num4 > num6) : (num6 > this.configuration.GetYearOnDuration() - num3))
			{
				base.smi.GoTo(base.smi.sm.dormant);
				return;
			}
			base.smi.GoTo(base.smi.sm.pre_erupt);
			return;
		}
	}

	// Token: 0x06003639 RID: 13881 RVA: 0x0012C8A0 File Offset: 0x0012AAA0
	public void ShiftTimeTo(Geyser.TimeShiftStep step)
	{
		float num = this.RemainingEruptTime();
		float num2 = this.RemainingNonEruptTime();
		float num3 = this.RemainingActiveTime();
		float num4 = this.RemainingDormantTime();
		float yearLength = this.configuration.GetYearLength();
		switch (step)
		{
		case Geyser.TimeShiftStep.ActiveState:
		{
			float num5 = ((num3 > 0f) ? (this.configuration.GetYearOnDuration() - num3) : (yearLength - num4));
			this.AlterTime(this.timeShift - num5);
			return;
		}
		case Geyser.TimeShiftStep.DormantState:
		{
			float num6 = ((num3 > 0f) ? num3 : (-(this.configuration.GetYearOffDuration() - num4)));
			this.AlterTime(this.timeShift + num6);
			return;
		}
		case Geyser.TimeShiftStep.NextIteration:
		{
			float num7 = ((num > 0f) ? (num + this.configuration.GetOffDuration()) : num2);
			this.AlterTime(this.timeShift + num7);
			return;
		}
		case Geyser.TimeShiftStep.PreviousIteration:
		{
			float num8 = ((num > 0f) ? (-(this.configuration.GetOnDuration() - num)) : (-(this.configuration.GetIterationLength() - num2)));
			if (num > 0f && Mathf.Abs(num8) < this.configuration.GetOnDuration() * 0.05f)
			{
				num8 -= this.configuration.GetIterationLength();
			}
			this.AlterTime(this.timeShift + num8);
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x0600363A RID: 13882 RVA: 0x0012C9D8 File Offset: 0x0012ABD8
	public void AddModification(Geyser.GeyserModification modification)
	{
		this.modifications.Add(modification);
		this.UpdateModifier();
	}

	// Token: 0x0600363B RID: 13883 RVA: 0x0012C9EC File Offset: 0x0012ABEC
	public void RemoveModification(Geyser.GeyserModification modification)
	{
		this.modifications.Remove(modification);
		this.UpdateModifier();
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x0012CA04 File Offset: 0x0012AC04
	private void UpdateModifier()
	{
		this.modifier.Clear();
		foreach (Geyser.GeyserModification geyserModification in this.modifications)
		{
			this.modifier.AddValues(geyserModification);
		}
		this.configuration.SetModifier(this.modifier);
		this.ApplyConfigurationEmissionValues(this.configuration);
		this.RefreshGeotunerFeedback();
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x0012CA8C File Offset: 0x0012AC8C
	public void RefreshGeotunerFeedback()
	{
		this.RefreshGeotunerStatusItem();
		this.RefreshStudiedMeter();
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x0012CA9C File Offset: 0x0012AC9C
	private void RefreshGeotunerStatusItem()
	{
		KSelectable component = base.gameObject.GetComponent<KSelectable>();
		if (this.GetAmountOfGeotunersPointingThisGeyser() > 0)
		{
			component.AddStatusItem(Db.Get().BuildingStatusItems.GeyserGeotuned, this);
			return;
		}
		component.RemoveStatusItem(Db.Get().BuildingStatusItems.GeyserGeotuned, this);
	}

	// Token: 0x0600363F RID: 13887 RVA: 0x0012CAF4 File Offset: 0x0012ACF4
	private void RefreshStudiedMeter()
	{
		if (this.studyable.Studied)
		{
			bool flag = this.GetAmountOfGeotunersPointingThisGeyser() > 0;
			GeyserConfig.TrackerMeterAnimNames trackerMeterAnimNames = GeyserConfig.TrackerMeterAnimNames.tracker;
			if (flag)
			{
				trackerMeterAnimNames = GeyserConfig.TrackerMeterAnimNames.geotracker;
				int amountOfGeotunersAffectingThisGeyser = this.GetAmountOfGeotunersAffectingThisGeyser();
				if (amountOfGeotunersAffectingThisGeyser > 0)
				{
					trackerMeterAnimNames = GeyserConfig.TrackerMeterAnimNames.geotracker_minor;
				}
				if (amountOfGeotunersAffectingThisGeyser >= 5)
				{
					trackerMeterAnimNames = GeyserConfig.TrackerMeterAnimNames.geotracker_major;
				}
			}
			this.studyable.studiedIndicator.meterController.Play(trackerMeterAnimNames.ToString(), KAnim.PlayMode.Loop, 1f, 0f);
		}
	}

	// Token: 0x06003640 RID: 13888 RVA: 0x0012CB60 File Offset: 0x0012AD60
	public int GetAmountOfGeotunersPointingThisGeyser()
	{
		return Components.GeoTuners.GetItems(base.gameObject.GetMyWorldId()).Count((GeoTuner.Instance x) => x.GetAssignedGeyser() == this);
	}

	// Token: 0x06003641 RID: 13889 RVA: 0x0012CB88 File Offset: 0x0012AD88
	public int GetAmountOfGeotunersPointingOrWillPointAtThisGeyser()
	{
		return Components.GeoTuners.GetItems(base.gameObject.GetMyWorldId()).Count((GeoTuner.Instance x) => x.GetAssignedGeyser() == this || x.GetFutureGeyser() == this);
	}

	// Token: 0x06003642 RID: 13890 RVA: 0x0012CBB0 File Offset: 0x0012ADB0
	public int GetAmountOfGeotunersAffectingThisGeyser()
	{
		int num = 0;
		for (int i = 0; i < this.modifications.Count; i++)
		{
			if (this.modifications[i].originID.Contains("GeoTuner"))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06003643 RID: 13891 RVA: 0x0012CBF7 File Offset: 0x0012ADF7
	private void OnGeotunerChanged(object o)
	{
		this.RefreshGeotunerFeedback();
	}

	// Token: 0x06003644 RID: 13892 RVA: 0x0012CC00 File Offset: 0x0012AE00
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
		if (this.configuration == null || this.configuration.typeId == HashedString.Invalid)
		{
			this.configuration = base.GetComponent<GeyserConfigurator>().MakeConfiguration();
		}
		else
		{
			PrimaryElement component = base.gameObject.GetComponent<PrimaryElement>();
			if (this.configuration.geyserType.geyserTemperature - component.Temperature != 0f)
			{
				SimTemperatureTransfer component2 = base.gameObject.GetComponent<SimTemperatureTransfer>();
				component2.onSimRegistered = (Action<SimTemperatureTransfer>)Delegate.Combine(component2.onSimRegistered, new Action<SimTemperatureTransfer>(this.OnSimRegistered));
			}
		}
		this.ApplyConfigurationEmissionValues(this.configuration);
		this.GenerateName();
		base.smi.StartSM();
		Workable component3 = base.GetComponent<Studyable>();
		if (component3 != null)
		{
			component3.alwaysShowProgressBar = true;
		}
		Components.Geysers.Add(base.gameObject.GetMyWorldId(), this);
		base.gameObject.Subscribe(1763323737, new Action<object>(this.OnGeotunerChanged));
		this.RefreshStudiedMeter();
		this.UpdateModifier();
	}

	// Token: 0x06003645 RID: 13893 RVA: 0x0012CD1C File Offset: 0x0012AF1C
	private void GenerateName()
	{
		StringKey stringKey = new StringKey("STRINGS.CREATURES.SPECIES.GEYSER." + this.configuration.geyserType.id.ToUpper() + ".NAME");
		if (this.nameable.savedName == Strings.Get(stringKey))
		{
			int num = Grid.PosToCell(base.gameObject);
			Quadrant[] quadrantOfCell = base.gameObject.GetMyWorld().GetQuadrantOfCell(num, 2);
			int num2 = (int)quadrantOfCell[0];
			string text = num2.ToString();
			num2 = (int)quadrantOfCell[1];
			string text2 = text + num2.ToString();
			string[] array = NAMEGEN.GEYSER_IDS.IDs.ToString().Split(new char[] { '\n' });
			string text3 = array[UnityEngine.Random.Range(0, array.Length)];
			string text4 = string.Concat(new string[]
			{
				UI.StripLinkFormatting(base.gameObject.GetProperName()),
				" ",
				text3,
				text2,
				"‑",
				UnityEngine.Random.Range(0, 10).ToString()
			});
			this.nameable.SetName(text4);
		}
	}

	// Token: 0x06003646 RID: 13894 RVA: 0x0012CE38 File Offset: 0x0012B038
	public void ApplyConfigurationEmissionValues(GeyserConfigurator.GeyserInstanceConfiguration config)
	{
		this.emitter.emitRange = 2;
		this.emitter.maxPressure = config.GetMaxPressure();
		this.emitter.outputElement = new ElementConverter.OutputElement(config.GetEmitRate(), config.GetElement(), config.GetTemperature(), false, false, (float)this.outputOffset.x, (float)this.outputOffset.y, 1f, config.GetDiseaseIdx(), Mathf.RoundToInt((float)config.GetDiseaseCount() * config.GetEmitRate()), true);
		if (this.emitter.IsSimActive)
		{
			this.emitter.SetSimActive(true);
		}
	}

	// Token: 0x06003647 RID: 13895 RVA: 0x0012CED6 File Offset: 0x0012B0D6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		base.gameObject.Unsubscribe(1763323737, new Action<object>(this.OnGeotunerChanged));
		Components.Geysers.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x06003648 RID: 13896 RVA: 0x0012CF10 File Offset: 0x0012B110
	private void OnSimRegistered(SimTemperatureTransfer stt)
	{
		PrimaryElement component = base.gameObject.GetComponent<PrimaryElement>();
		if (this.configuration.geyserType.geyserTemperature - component.Temperature != 0f)
		{
			component.Temperature = this.configuration.geyserType.geyserTemperature;
		}
		stt.onSimRegistered = (Action<SimTemperatureTransfer>)Delegate.Remove(stt.onSimRegistered, new Action<SimTemperatureTransfer>(this.OnSimRegistered));
	}

	// Token: 0x06003649 RID: 13897 RVA: 0x0012CF80 File Offset: 0x0012B180
	public float RemainingPhaseTimeFrom2(float onDuration, float offDuration, float time, Geyser.Phase expectedPhase)
	{
		float num = onDuration + offDuration;
		float num2 = time % num;
		float num3;
		Geyser.Phase phase;
		if (num2 < onDuration)
		{
			num3 = Mathf.Max(onDuration - num2, 0f);
			phase = Geyser.Phase.On;
		}
		else
		{
			num3 = Mathf.Max(onDuration + offDuration - num2, 0f);
			phase = Geyser.Phase.Off;
		}
		if (expectedPhase != Geyser.Phase.Any && phase != expectedPhase)
		{
			return 0f;
		}
		return num3;
	}

	// Token: 0x0600364A RID: 13898 RVA: 0x0012CFD0 File Offset: 0x0012B1D0
	public float RemainingPhaseTimeFrom4(float onDuration, float pstDuration, float offDuration, float preDuration, float time, Geyser.Phase expectedPhase)
	{
		float num = onDuration + pstDuration + offDuration + preDuration;
		float num2 = time % num;
		float num3;
		Geyser.Phase phase;
		if (num2 < onDuration)
		{
			num3 = onDuration - num2;
			phase = Geyser.Phase.On;
		}
		else if (num2 < onDuration + pstDuration)
		{
			num3 = onDuration + pstDuration - num2;
			phase = Geyser.Phase.Pst;
		}
		else if (num2 < onDuration + pstDuration + offDuration)
		{
			num3 = onDuration + pstDuration + offDuration - num2;
			phase = Geyser.Phase.Off;
		}
		else
		{
			num3 = onDuration + pstDuration + offDuration + preDuration - num2;
			phase = Geyser.Phase.Pre;
		}
		if (expectedPhase != Geyser.Phase.Any && phase != expectedPhase)
		{
			return 0f;
		}
		return num3;
	}

	// Token: 0x0600364B RID: 13899 RVA: 0x0012D039 File Offset: 0x0012B239
	private float IdleDuration()
	{
		return this.configuration.GetOffDuration() * 0.84999996f;
	}

	// Token: 0x0600364C RID: 13900 RVA: 0x0012D04C File Offset: 0x0012B24C
	private float PreDuration()
	{
		return this.configuration.GetOffDuration() * 0.1f;
	}

	// Token: 0x0600364D RID: 13901 RVA: 0x0012D05F File Offset: 0x0012B25F
	private float PostDuration()
	{
		return this.configuration.GetOffDuration() * 0.05f;
	}

	// Token: 0x0600364E RID: 13902 RVA: 0x0012D072 File Offset: 0x0012B272
	private float EruptDuration()
	{
		return this.configuration.GetOnDuration();
	}

	// Token: 0x0600364F RID: 13903 RVA: 0x0012D07F File Offset: 0x0012B27F
	public bool ShouldGoDormant()
	{
		return this.RemainingActiveTime() <= 0f;
	}

	// Token: 0x06003650 RID: 13904 RVA: 0x0012D091 File Offset: 0x0012B291
	public float RemainingIdleTime()
	{
		return this.RemainingPhaseTimeFrom4(this.EruptDuration(), this.PostDuration(), this.IdleDuration(), this.PreDuration(), this.GetCurrentLifeTime(), Geyser.Phase.Off);
	}

	// Token: 0x06003651 RID: 13905 RVA: 0x0012D0B8 File Offset: 0x0012B2B8
	public float RemainingEruptPreTime()
	{
		return this.RemainingPhaseTimeFrom4(this.EruptDuration(), this.PostDuration(), this.IdleDuration(), this.PreDuration(), this.GetCurrentLifeTime(), Geyser.Phase.Pre);
	}

	// Token: 0x06003652 RID: 13906 RVA: 0x0012D0DF File Offset: 0x0012B2DF
	public float RemainingEruptTime()
	{
		return this.RemainingPhaseTimeFrom2(this.configuration.GetOnDuration(), this.configuration.GetOffDuration(), this.GetCurrentLifeTime(), Geyser.Phase.On);
	}

	// Token: 0x06003653 RID: 13907 RVA: 0x0012D104 File Offset: 0x0012B304
	public float RemainingEruptPostTime()
	{
		return this.RemainingPhaseTimeFrom4(this.EruptDuration(), this.PostDuration(), this.IdleDuration(), this.PreDuration(), this.GetCurrentLifeTime(), Geyser.Phase.Pst);
	}

	// Token: 0x06003654 RID: 13908 RVA: 0x0012D12B File Offset: 0x0012B32B
	public float RemainingNonEruptTime()
	{
		return this.RemainingPhaseTimeFrom2(this.configuration.GetOnDuration(), this.configuration.GetOffDuration(), this.GetCurrentLifeTime(), Geyser.Phase.Off);
	}

	// Token: 0x06003655 RID: 13909 RVA: 0x0012D150 File Offset: 0x0012B350
	public float RemainingDormantTime()
	{
		return this.RemainingPhaseTimeFrom2(this.configuration.GetYearOnDuration(), this.configuration.GetYearOffDuration(), this.GetCurrentLifeTime(), Geyser.Phase.Off);
	}

	// Token: 0x06003656 RID: 13910 RVA: 0x0012D175 File Offset: 0x0012B375
	public float RemainingActiveTime()
	{
		return this.RemainingPhaseTimeFrom2(this.configuration.GetYearOnDuration(), this.configuration.GetYearOffDuration(), this.GetCurrentLifeTime(), Geyser.Phase.On);
	}

	// Token: 0x06003657 RID: 13911 RVA: 0x0012D19C File Offset: 0x0012B39C
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = ElementLoader.FindElementByHash(this.configuration.GetElement()).tag.ProperName();
		List<GeoTuner.Instance> items = Components.GeoTuners.GetItems(base.gameObject.GetMyWorldId());
		GeoTuner.Instance instance = items.Find((GeoTuner.Instance g) => g.GetAssignedGeyser() == this);
		int num = items.Count((GeoTuner.Instance x) => x.GetAssignedGeyser() == this);
		bool flag = num > 0;
		string text2 = string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_PRODUCTION, ElementLoader.FindElementByHash(this.configuration.GetElement()).name, GameUtil.GetFormattedMass(this.configuration.GetEmitRate(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.configuration.GetTemperature(), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		if (flag)
		{
			Func<float, float> func = delegate(float emissionPerCycleModifier)
			{
				float num8 = 600f / this.configuration.GetIterationLength();
				return emissionPerCycleModifier / num8 / this.configuration.GetOnDuration();
			};
			int amountOfGeotunersAffectingThisGeyser = this.GetAmountOfGeotunersAffectingThisGeyser();
			float num2 = ((Geyser.temperatureModificationMethod == Geyser.ModificationMethod.Percentages) ? (instance.currentGeyserModification.temperatureModifier * this.configuration.geyserType.temperature) : instance.currentGeyserModification.temperatureModifier);
			float num3 = func((Geyser.massModificationMethod == Geyser.ModificationMethod.Percentages) ? (instance.currentGeyserModification.massPerCycleModifier * this.configuration.scaledRate) : instance.currentGeyserModification.massPerCycleModifier);
			float num4 = (float)amountOfGeotunersAffectingThisGeyser * num2;
			float num5 = (float)amountOfGeotunersAffectingThisGeyser * num3;
			string text3 = ((num4 > 0f) ? "+" : "") + GameUtil.GetFormattedTemperature(num4, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false);
			string text4 = ((num5 > 0f) ? "+" : "") + GameUtil.GetFormattedMass(num5, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}");
			string text5 = ((num2 > 0f) ? "+" : "") + GameUtil.GetFormattedTemperature(num2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false);
			string text6 = ((num3 > 0f) ? "+" : "") + GameUtil.GetFormattedMass(num3, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}");
			text2 = string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_PRODUCTION_GEOTUNED, ElementLoader.FindElementByHash(this.configuration.GetElement()).name, GameUtil.GetFormattedMass(this.configuration.GetEmitRate(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.configuration.GetTemperature(), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			text2 += "\n";
			text2 = text2 + "\n" + string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_PRODUCTION_GEOTUNED_COUNT, amountOfGeotunersAffectingThisGeyser.ToString(), num.ToString());
			text2 += "\n";
			text2 = text2 + "\n" + string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_PRODUCTION_GEOTUNED_TOTAL, text4, text3);
			for (int i = 0; i < amountOfGeotunersAffectingThisGeyser; i++)
			{
				string text7 = "\n    • " + UI.UISIDESCREENS.GEOTUNERSIDESCREEN.STUDIED_TOOLTIP_GEOTUNER_MODIFIER_ROW_TITLE.ToString();
				text7 = text7 + text6 + " " + text5;
				text2 += text7;
			}
		}
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_PRODUCTION, text, GameUtil.GetFormattedMass(this.configuration.GetEmitRate(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.configuration.GetTemperature(), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), text2, Descriptor.DescriptorType.Effect, false));
		if (this.configuration.GetDiseaseIdx() != 255)
		{
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_DISEASE, GameUtil.GetFormattedDiseaseName(this.configuration.GetDiseaseIdx(), false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_DISEASE, GameUtil.GetFormattedDiseaseName(this.configuration.GetDiseaseIdx(), false)), Descriptor.DescriptorType.Effect, false));
		}
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_PERIOD, GameUtil.GetFormattedTime(this.configuration.GetOnDuration(), "F0"), GameUtil.GetFormattedTime(this.configuration.GetIterationLength(), "F0")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_PERIOD, GameUtil.GetFormattedTime(this.configuration.GetOnDuration(), "F0"), GameUtil.GetFormattedTime(this.configuration.GetIterationLength(), "F0")), Descriptor.DescriptorType.Effect, false));
		Studyable component = base.GetComponent<Studyable>();
		if (component && !component.Studied)
		{
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_YEAR_UNSTUDIED, Array.Empty<object>()), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_UNSTUDIED, Array.Empty<object>()), Descriptor.DescriptorType.Effect, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_YEAR_AVR_OUTPUT_UNSTUDIED, Array.Empty<object>()), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_AVR_OUTPUT_UNSTUDIED, Array.Empty<object>()), Descriptor.DescriptorType.Effect, false));
		}
		else
		{
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_YEAR_PERIOD, GameUtil.GetFormattedCycles(this.configuration.GetYearOnDuration(), "F1", false), GameUtil.GetFormattedCycles(this.configuration.GetYearLength(), "F1", false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_PERIOD, GameUtil.GetFormattedCycles(this.configuration.GetYearOnDuration(), "F1", false), GameUtil.GetFormattedCycles(this.configuration.GetYearLength(), "F1", false)), Descriptor.DescriptorType.Effect, false));
			if (base.smi.IsInsideState(base.smi.sm.dormant))
			{
				list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_YEAR_NEXT_ACTIVE, GameUtil.GetFormattedCycles(this.RemainingDormantTime(), "F1", false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_NEXT_ACTIVE, GameUtil.GetFormattedCycles(this.RemainingDormantTime(), "F1", false)), Descriptor.DescriptorType.Effect, false));
			}
			else
			{
				list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_YEAR_NEXT_DORMANT, GameUtil.GetFormattedCycles(this.RemainingActiveTime(), "F1", false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_NEXT_DORMANT, GameUtil.GetFormattedCycles(this.RemainingActiveTime(), "F1", false)), Descriptor.DescriptorType.Effect, false));
			}
			string text8 = UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_AVR_OUTPUT.Replace("{average}", GameUtil.GetFormattedMass(this.configuration.GetAverageEmission(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{element}", this.configuration.geyserType.element.CreateTag().ProperName());
			if (flag)
			{
				text8 += "\n";
				text8 = text8 + "\n" + UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_AVR_OUTPUT_BREAKDOWN_TITLE;
				int amountOfGeotunersAffectingThisGeyser2 = this.GetAmountOfGeotunersAffectingThisGeyser();
				float num6 = ((Geyser.massModificationMethod == Geyser.ModificationMethod.Percentages) ? (instance.currentGeyserModification.massPerCycleModifier * 100f) : (instance.currentGeyserModification.massPerCycleModifier * 100f / this.configuration.scaledRate));
				float num7 = num6 * (float)amountOfGeotunersAffectingThisGeyser2;
				text8 = text8 + GameUtil.AddPositiveSign(num7.ToString("0.0"), num7 > 0f) + "%";
				for (int j = 0; j < amountOfGeotunersAffectingThisGeyser2; j++)
				{
					string text9 = "\n    • " + UI.BUILDINGEFFECTS.TOOLTIPS.GEYSER_YEAR_AVR_OUTPUT_BREAKDOWN_ROW.ToString();
					text9 = text9 + GameUtil.AddPositiveSign(num6.ToString("0.0"), num6 > 0f) + "%";
					text8 += text9;
				}
			}
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.GEYSER_YEAR_AVR_OUTPUT, GameUtil.GetFormattedMass(this.configuration.GetAverageEmission(), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), text8, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x0400242F RID: 9263
	public static Geyser.ModificationMethod massModificationMethod = Geyser.ModificationMethod.Percentages;

	// Token: 0x04002430 RID: 9264
	public static Geyser.ModificationMethod temperatureModificationMethod = Geyser.ModificationMethod.Values;

	// Token: 0x04002431 RID: 9265
	public static Geyser.ModificationMethod IterationDurationModificationMethod = Geyser.ModificationMethod.Percentages;

	// Token: 0x04002432 RID: 9266
	public static Geyser.ModificationMethod IterationPercentageModificationMethod = Geyser.ModificationMethod.Percentages;

	// Token: 0x04002433 RID: 9267
	public static Geyser.ModificationMethod yearDurationModificationMethod = Geyser.ModificationMethod.Percentages;

	// Token: 0x04002434 RID: 9268
	public static Geyser.ModificationMethod yearPercentageModificationMethod = Geyser.ModificationMethod.Percentages;

	// Token: 0x04002435 RID: 9269
	public static Geyser.ModificationMethod maxPressureModificationMethod = Geyser.ModificationMethod.Percentages;

	// Token: 0x04002436 RID: 9270
	[MyCmpAdd]
	private ElementEmitter emitter;

	// Token: 0x04002437 RID: 9271
	[MyCmpAdd]
	private UserNameable nameable;

	// Token: 0x04002438 RID: 9272
	[MyCmpGet]
	private Studyable studyable;

	// Token: 0x04002439 RID: 9273
	[Serialize]
	public GeyserConfigurator.GeyserInstanceConfiguration configuration;

	// Token: 0x0400243A RID: 9274
	public Vector2I outputOffset;

	// Token: 0x0400243B RID: 9275
	public List<Geyser.GeyserModification> modifications = new List<Geyser.GeyserModification>();

	// Token: 0x0400243C RID: 9276
	private Geyser.GeyserModification modifier;

	// Token: 0x0400243E RID: 9278
	private const float PRE_PCT = 0.1f;

	// Token: 0x0400243F RID: 9279
	private const float POST_PCT = 0.05f;

	// Token: 0x020014C5 RID: 5317
	public enum ModificationMethod
	{
		// Token: 0x040064B2 RID: 25778
		Values,
		// Token: 0x040064B3 RID: 25779
		Percentages
	}

	// Token: 0x020014C6 RID: 5318
	public struct GeyserModification
	{
		// Token: 0x060081E9 RID: 33257 RVA: 0x002E3890 File Offset: 0x002E1A90
		public void Clear()
		{
			this.massPerCycleModifier = 0f;
			this.temperatureModifier = 0f;
			this.iterationDurationModifier = 0f;
			this.iterationPercentageModifier = 0f;
			this.yearDurationModifier = 0f;
			this.yearPercentageModifier = 0f;
			this.maxPressureModifier = 0f;
			this.modifyElement = false;
			this.newElement = (SimHashes)0;
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x002E38F8 File Offset: 0x002E1AF8
		public void AddValues(Geyser.GeyserModification modification)
		{
			this.massPerCycleModifier += modification.massPerCycleModifier;
			this.temperatureModifier += modification.temperatureModifier;
			this.iterationDurationModifier += modification.iterationDurationModifier;
			this.iterationPercentageModifier += modification.iterationPercentageModifier;
			this.yearDurationModifier += modification.yearDurationModifier;
			this.yearPercentageModifier += modification.yearPercentageModifier;
			this.maxPressureModifier += modification.maxPressureModifier;
			this.modifyElement |= modification.modifyElement;
			this.newElement = ((modification.newElement == (SimHashes)0) ? this.newElement : modification.newElement);
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x002E39B9 File Offset: 0x002E1BB9
		public bool IsNewElementInUse()
		{
			return this.modifyElement && this.newElement > (SimHashes)0;
		}

		// Token: 0x040064B4 RID: 25780
		public string originID;

		// Token: 0x040064B5 RID: 25781
		public float massPerCycleModifier;

		// Token: 0x040064B6 RID: 25782
		public float temperatureModifier;

		// Token: 0x040064B7 RID: 25783
		public float iterationDurationModifier;

		// Token: 0x040064B8 RID: 25784
		public float iterationPercentageModifier;

		// Token: 0x040064B9 RID: 25785
		public float yearDurationModifier;

		// Token: 0x040064BA RID: 25786
		public float yearPercentageModifier;

		// Token: 0x040064BB RID: 25787
		public float maxPressureModifier;

		// Token: 0x040064BC RID: 25788
		public bool modifyElement;

		// Token: 0x040064BD RID: 25789
		public SimHashes newElement;
	}

	// Token: 0x020014C7 RID: 5319
	public class StatesInstance : GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.GameInstance
	{
		// Token: 0x060081EC RID: 33260 RVA: 0x002E39CE File Offset: 0x002E1BCE
		public StatesInstance(Geyser smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020014C8 RID: 5320
	public class States : GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser>
	{
		// Token: 0x060081ED RID: 33261 RVA: 0x002E39D8 File Offset: 0x002E1BD8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.DefaultState(this.idle).Enter(delegate(Geyser.StatesInstance smi)
			{
				smi.master.emitter.SetEmitting(false);
			});
			this.dormant.PlayAnim("inactive", KAnim.PlayMode.Loop).ToggleMainStatusItem(Db.Get().MiscStatusItems.SpoutDormant, null).ScheduleGoTo((Geyser.StatesInstance smi) => smi.master.RemainingDormantTime(), this.pre_erupt);
			this.idle.PlayAnim("inactive", KAnim.PlayMode.Loop).ToggleMainStatusItem(Db.Get().MiscStatusItems.SpoutIdle, null).Enter(delegate(Geyser.StatesInstance smi)
			{
				if (smi.master.ShouldGoDormant())
				{
					smi.GoTo(this.dormant);
				}
			})
				.ScheduleGoTo((Geyser.StatesInstance smi) => smi.master.RemainingIdleTime(), this.pre_erupt);
			this.pre_erupt.PlayAnim("shake", KAnim.PlayMode.Loop).ToggleMainStatusItem(Db.Get().MiscStatusItems.SpoutPressureBuilding, null).ScheduleGoTo((Geyser.StatesInstance smi) => smi.master.RemainingEruptPreTime(), this.erupt);
			this.erupt.TriggerOnEnter(GameHashes.GeyserEruption, (Geyser.StatesInstance smi) => true).TriggerOnExit(GameHashes.GeyserEruption, (Geyser.StatesInstance smi) => false).DefaultState(this.erupt.erupting)
				.ScheduleGoTo((Geyser.StatesInstance smi) => smi.master.RemainingEruptTime(), this.post_erupt)
				.Enter(delegate(Geyser.StatesInstance smi)
				{
					smi.master.emitter.SetEmitting(true);
				})
				.Exit(delegate(Geyser.StatesInstance smi)
				{
					smi.master.emitter.SetEmitting(false);
				});
			this.erupt.erupting.EventTransition(GameHashes.EmitterBlocked, this.erupt.overpressure, (Geyser.StatesInstance smi) => smi.GetComponent<ElementEmitter>().isEmitterBlocked).PlayAnim("erupt", KAnim.PlayMode.Loop);
			this.erupt.overpressure.EventTransition(GameHashes.EmitterUnblocked, this.erupt.erupting, (Geyser.StatesInstance smi) => !smi.GetComponent<ElementEmitter>().isEmitterBlocked).ToggleMainStatusItem(Db.Get().MiscStatusItems.SpoutOverPressure, null).PlayAnim("inactive", KAnim.PlayMode.Loop);
			this.post_erupt.PlayAnim("shake", KAnim.PlayMode.Loop).ToggleMainStatusItem(Db.Get().MiscStatusItems.SpoutIdle, null).ScheduleGoTo((Geyser.StatesInstance smi) => smi.master.RemainingEruptPostTime(), this.idle);
		}

		// Token: 0x040064BE RID: 25790
		public GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State dormant;

		// Token: 0x040064BF RID: 25791
		public GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State idle;

		// Token: 0x040064C0 RID: 25792
		public GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State pre_erupt;

		// Token: 0x040064C1 RID: 25793
		public Geyser.States.EruptState erupt;

		// Token: 0x040064C2 RID: 25794
		public GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State post_erupt;

		// Token: 0x0200205F RID: 8287
		public class EruptState : GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State
		{
			// Token: 0x0400903A RID: 36922
			public GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State erupting;

			// Token: 0x0400903B RID: 36923
			public GameStateMachine<Geyser.States, Geyser.StatesInstance, Geyser, object>.State overpressure;
		}
	}

	// Token: 0x020014C9 RID: 5321
	public enum TimeShiftStep
	{
		// Token: 0x040064C4 RID: 25796
		ActiveState,
		// Token: 0x040064C5 RID: 25797
		DormantState,
		// Token: 0x040064C6 RID: 25798
		NextIteration,
		// Token: 0x040064C7 RID: 25799
		PreviousIteration
	}

	// Token: 0x020014CA RID: 5322
	public enum Phase
	{
		// Token: 0x040064C9 RID: 25801
		Pre,
		// Token: 0x040064CA RID: 25802
		On,
		// Token: 0x040064CB RID: 25803
		Pst,
		// Token: 0x040064CC RID: 25804
		Off,
		// Token: 0x040064CD RID: 25805
		Any
	}
}
