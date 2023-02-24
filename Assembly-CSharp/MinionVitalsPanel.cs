﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B30 RID: 2864
[AddComponentMenu("KMonoBehaviour/scripts/MinionVitalsPanel")]
public class MinionVitalsPanel : KMonoBehaviour
{
	// Token: 0x0600586C RID: 22636 RVA: 0x00200824 File Offset: 0x001FEA24
	public void Init()
	{
		this.AddAmountLine(Db.Get().Amounts.HitPoints, null);
		this.AddAttributeLine(Db.Get().CritterAttributes.Happiness, null);
		this.AddAmountLine(Db.Get().Amounts.Wildness, null);
		this.AddAmountLine(Db.Get().Amounts.Incubation, null);
		this.AddAmountLine(Db.Get().Amounts.Viability, null);
		this.AddAmountLine(Db.Get().Amounts.PowerCharge, null);
		this.AddAmountLine(Db.Get().Amounts.Fertility, null);
		this.AddAmountLine(Db.Get().Amounts.Age, null);
		this.AddAmountLine(Db.Get().Amounts.Stress, null);
		this.AddAttributeLine(Db.Get().Attributes.QualityOfLife, null);
		this.AddAmountLine(Db.Get().Amounts.Bladder, null);
		this.AddAmountLine(Db.Get().Amounts.Breath, null);
		this.AddAmountLine(Db.Get().Amounts.Stamina, null);
		this.AddAmountLine(Db.Get().Amounts.Calories, null);
		this.AddAmountLine(Db.Get().Amounts.ScaleGrowth, null);
		this.AddAmountLine(Db.Get().Amounts.ElementGrowth, null);
		this.AddAmountLine(Db.Get().Amounts.Temperature, null);
		this.AddAmountLine(Db.Get().Amounts.Decor, null);
		this.AddAmountLine(Db.Get().Amounts.InternalBattery, null);
		this.AddAmountLine(Db.Get().Amounts.InternalChemicalBattery, null);
		if (DlcManager.FeatureRadiationEnabled())
		{
			this.AddAmountLine(Db.Get().Amounts.RadiationBalance, null);
		}
		this.AddCheckboxLine(Db.Get().Amounts.AirPressure, this.conditionsContainerNormal, (GameObject go) => this.GetAirPressureLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<PressureVulnerable>() != null && go.GetComponent<PressureVulnerable>().pressure_sensitive)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_pressure(go), (GameObject go) => this.GetAirPressureTooltip(go));
		this.AddCheckboxLine(null, this.conditionsContainerNormal, (GameObject go) => this.GetAtmosphereLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<PressureVulnerable>() != null && go.GetComponent<PressureVulnerable>().safe_atmospheres.Count > 0)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_atmosphere(go), (GameObject go) => this.GetAtmosphereTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Temperature, this.conditionsContainerNormal, (GameObject go) => this.GetInternalTemperatureLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<TemperatureVulnerable>() != null)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_temperature(go), (GameObject go) => this.GetInternalTemperatureTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Fertilization, this.conditionsContainerAdditional, (GameObject go) => this.GetFertilizationLabel(go), delegate(GameObject go)
		{
			if (go.GetComponent<ReceptacleMonitor>() == null)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
			}
			if (go.GetComponent<ReceptacleMonitor>().Replanted)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Diminished;
		}, (GameObject go) => this.check_fertilizer(go), (GameObject go) => this.GetFertilizationTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Irrigation, this.conditionsContainerAdditional, (GameObject go) => this.GetIrrigationLabel(go), delegate(GameObject go)
		{
			ReceptacleMonitor component = go.GetComponent<ReceptacleMonitor>();
			if (!(component != null) || !component.Replanted)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Diminished;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
		}, (GameObject go) => this.check_irrigation(go), (GameObject go) => this.GetIrrigationTooltip(go));
		this.AddCheckboxLine(Db.Get().Amounts.Illumination, this.conditionsContainerNormal, (GameObject go) => this.GetIlluminationLabel(go), (GameObject go) => MinionVitalsPanel.CheckboxLineDisplayType.Normal, (GameObject go) => this.check_illumination(go), (GameObject go) => this.GetIlluminationTooltip(go));
		this.AddCheckboxLine(null, this.conditionsContainerNormal, (GameObject go) => this.GetRadiationLabel(go), delegate(GameObject go)
		{
			AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MaxRadiationThreshold);
			if (attributeInstance != null && attributeInstance.GetTotalValue() > 0f)
			{
				return MinionVitalsPanel.CheckboxLineDisplayType.Normal;
			}
			return MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
		}, (GameObject go) => this.check_radiation(go), (GameObject go) => this.GetRadiationTooltip(go));
	}

	// Token: 0x0600586D RID: 22637 RVA: 0x00200C7C File Offset: 0x001FEE7C
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		SimAndRenderScheduler.instance.Add(this, false);
	}

	// Token: 0x0600586E RID: 22638 RVA: 0x00200C90 File Offset: 0x001FEE90
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x0600586F RID: 22639 RVA: 0x00200CA4 File Offset: 0x001FEEA4
	private void AddAmountLine(Amount amount, Func<AmountInstance, string> tooltip_func = null)
	{
		GameObject gameObject = Util.KInstantiateUI(this.LineItemPrefab, base.gameObject, false);
		gameObject.GetComponentInChildren<Image>().sprite = Assets.GetSprite(amount.uiSprite);
		gameObject.GetComponent<ToolTip>().refreshWhileHovering = true;
		gameObject.SetActive(true);
		MinionVitalsPanel.AmountLine amountLine = default(MinionVitalsPanel.AmountLine);
		amountLine.amount = amount;
		amountLine.go = gameObject;
		amountLine.locText = gameObject.GetComponentInChildren<LocText>();
		amountLine.toolTip = gameObject.GetComponentInChildren<ToolTip>();
		amountLine.imageToggle = gameObject.GetComponentInChildren<ValueTrendImageToggle>();
		amountLine.toolTipFunc = ((tooltip_func != null) ? tooltip_func : new Func<AmountInstance, string>(amount.GetTooltip));
		this.amountsLines.Add(amountLine);
	}

	// Token: 0x06005870 RID: 22640 RVA: 0x00200D58 File Offset: 0x001FEF58
	private void AddAttributeLine(Klei.AI.Attribute attribute, Func<AttributeInstance, string> tooltip_func = null)
	{
		GameObject gameObject = Util.KInstantiateUI(this.LineItemPrefab, base.gameObject, false);
		gameObject.GetComponentInChildren<Image>().sprite = Assets.GetSprite(attribute.uiSprite);
		gameObject.GetComponent<ToolTip>().refreshWhileHovering = true;
		gameObject.SetActive(true);
		MinionVitalsPanel.AttributeLine attributeLine = default(MinionVitalsPanel.AttributeLine);
		attributeLine.attribute = attribute;
		attributeLine.go = gameObject;
		attributeLine.locText = gameObject.GetComponentInChildren<LocText>();
		attributeLine.toolTip = gameObject.GetComponentInChildren<ToolTip>();
		gameObject.GetComponentInChildren<ValueTrendImageToggle>().gameObject.SetActive(false);
		attributeLine.toolTipFunc = ((tooltip_func != null) ? tooltip_func : new Func<AttributeInstance, string>(attribute.GetTooltip));
		this.attributesLines.Add(attributeLine);
	}

	// Token: 0x06005871 RID: 22641 RVA: 0x00200E10 File Offset: 0x001FF010
	private void AddCheckboxLine(Amount amount, Transform parentContainer, Func<GameObject, string> label_text_func, Func<GameObject, MinionVitalsPanel.CheckboxLineDisplayType> display_condition, Func<GameObject, bool> checkbox_value_func, Func<GameObject, string> tooltip_func = null)
	{
		GameObject gameObject = Util.KInstantiateUI(this.CheckboxLinePrefab, base.gameObject, false);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		gameObject.GetComponent<ToolTip>().refreshWhileHovering = true;
		gameObject.SetActive(true);
		MinionVitalsPanel.CheckboxLine checkboxLine = default(MinionVitalsPanel.CheckboxLine);
		checkboxLine.go = gameObject;
		checkboxLine.parentContainer = parentContainer;
		checkboxLine.amount = amount;
		checkboxLine.locText = component.GetReference("Label") as LocText;
		checkboxLine.get_value = checkbox_value_func;
		checkboxLine.display_condition = display_condition;
		checkboxLine.label_text_func = label_text_func;
		checkboxLine.go.name = "Checkbox_";
		if (amount != null)
		{
			GameObject go = checkboxLine.go;
			go.name += amount.Name;
		}
		else
		{
			GameObject go2 = checkboxLine.go;
			go2.name += "Unnamed";
		}
		if (tooltip_func != null)
		{
			checkboxLine.tooltip = tooltip_func;
			ToolTip tt = checkboxLine.go.GetComponent<ToolTip>();
			tt.refreshWhileHovering = true;
			tt.OnToolTip = delegate
			{
				tt.ClearMultiStringTooltip();
				tt.AddMultiStringTooltip(tooltip_func(this.selectedEntity), null);
				return "";
			};
		}
		this.checkboxLines.Add(checkboxLine);
	}

	// Token: 0x06005872 RID: 22642 RVA: 0x00200F54 File Offset: 0x001FF154
	public void Refresh()
	{
		if (this.selectedEntity == null)
		{
			return;
		}
		if (this.selectedEntity.gameObject == null)
		{
			return;
		}
		Amounts amounts = this.selectedEntity.GetAmounts();
		Attributes attributes = this.selectedEntity.GetAttributes();
		if (amounts == null || attributes == null)
		{
			return;
		}
		WiltCondition component = this.selectedEntity.GetComponent<WiltCondition>();
		if (component == null)
		{
			this.conditionsContainerNormal.gameObject.SetActive(false);
			this.conditionsContainerAdditional.gameObject.SetActive(false);
			foreach (MinionVitalsPanel.AmountLine amountLine in this.amountsLines)
			{
				bool flag = amountLine.TryUpdate(amounts);
				if (amountLine.go.activeSelf != flag)
				{
					amountLine.go.SetActive(flag);
				}
			}
			foreach (MinionVitalsPanel.AttributeLine attributeLine in this.attributesLines)
			{
				bool flag2 = attributeLine.TryUpdate(attributes);
				if (attributeLine.go.activeSelf != flag2)
				{
					attributeLine.go.SetActive(flag2);
				}
			}
		}
		bool flag3 = false;
		for (int i = 0; i < this.checkboxLines.Count; i++)
		{
			MinionVitalsPanel.CheckboxLine checkboxLine = this.checkboxLines[i];
			MinionVitalsPanel.CheckboxLineDisplayType checkboxLineDisplayType = MinionVitalsPanel.CheckboxLineDisplayType.Hidden;
			if (this.checkboxLines[i].amount != null)
			{
				for (int j = 0; j < amounts.Count; j++)
				{
					AmountInstance amountInstance = amounts[j];
					if (checkboxLine.amount == amountInstance.amount)
					{
						checkboxLineDisplayType = checkboxLine.display_condition(this.selectedEntity.gameObject);
						break;
					}
				}
			}
			else
			{
				checkboxLineDisplayType = checkboxLine.display_condition(this.selectedEntity.gameObject);
			}
			if (checkboxLineDisplayType != MinionVitalsPanel.CheckboxLineDisplayType.Hidden)
			{
				checkboxLine.locText.SetText(checkboxLine.label_text_func(this.selectedEntity.gameObject));
				if (!checkboxLine.go.activeSelf)
				{
					checkboxLine.go.SetActive(true);
				}
				GameObject gameObject = checkboxLine.go.GetComponent<HierarchyReferences>().GetReference("Check").gameObject;
				gameObject.SetActive(checkboxLine.get_value(this.selectedEntity.gameObject));
				if (checkboxLine.go.transform.parent != checkboxLine.parentContainer)
				{
					checkboxLine.go.transform.SetParent(checkboxLine.parentContainer);
					checkboxLine.go.transform.localScale = Vector3.one;
				}
				if (checkboxLine.parentContainer == this.conditionsContainerAdditional)
				{
					flag3 = true;
				}
				if (checkboxLineDisplayType == MinionVitalsPanel.CheckboxLineDisplayType.Normal)
				{
					if (checkboxLine.get_value(this.selectedEntity.gameObject))
					{
						checkboxLine.locText.color = Color.black;
						gameObject.transform.parent.GetComponent<Image>().color = Color.black;
					}
					else
					{
						Color color = new Color(0.99215686f, 0f, 0.101960786f);
						checkboxLine.locText.color = color;
						gameObject.transform.parent.GetComponent<Image>().color = color;
					}
				}
				else
				{
					checkboxLine.locText.color = Color.grey;
					gameObject.transform.parent.GetComponent<Image>().color = Color.grey;
				}
			}
			else if (checkboxLine.go.activeSelf)
			{
				checkboxLine.go.SetActive(false);
			}
		}
		if (component != null)
		{
			UnityEngine.Object component2 = component.GetComponent<Growing>();
			bool flag4 = component.HasTag(GameTags.Decoration);
			this.conditionsContainerNormal.gameObject.SetActive(true);
			this.conditionsContainerAdditional.gameObject.SetActive(!flag4);
			if (component2 == null)
			{
				float num = 1f;
				LocText reference = this.conditionsContainerNormal.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				reference.text = "";
				reference.text = (flag4 ? string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD_DECOR.BASE, Array.Empty<object>()) : string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD_INSTANT.BASE, Util.FormatTwoDecimalPlace(num * 0.25f * 100f)));
				reference.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD_INSTANT.TOOLTIP, Array.Empty<object>()));
				LocText reference2 = this.conditionsContainerAdditional.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				ReceptacleMonitor component3 = this.selectedEntity.GetComponent<ReceptacleMonitor>();
				reference2.color = ((component3 == null || component3.Replanted) ? Color.black : Color.grey);
				reference2.text = string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC_INSTANT.BASE, Util.FormatTwoDecimalPlace(num * 100f));
				reference2.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC_INSTANT.TOOLTIP, Array.Empty<object>()));
			}
			else
			{
				LocText reference3 = this.conditionsContainerNormal.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				reference3.text = "";
				reference3.text = string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD.BASE, GameUtil.GetFormattedCycles(component.GetComponent<Growing>().WildGrowthTime(), "F1", false));
				reference3.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.WILD.TOOLTIP, GameUtil.GetFormattedCycles(component.GetComponent<Growing>().WildGrowthTime(), "F1", false)));
				LocText reference4 = this.conditionsContainerAdditional.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
				reference4.color = (this.selectedEntity.GetComponent<ReceptacleMonitor>().Replanted ? Color.black : Color.grey);
				reference4.text = "";
				reference4.text = (flag3 ? string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC.BASE, GameUtil.GetFormattedCycles(component.GetComponent<Growing>().DomesticGrowthTime(), "F1", false)) : string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.DOMESTIC.BASE, GameUtil.GetFormattedCycles(component.GetComponent<Growing>().DomesticGrowthTime(), "F1", false)));
				reference4.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.VITALSSCREEN.CONDITIONS_GROWING.ADDITIONAL_DOMESTIC.TOOLTIP, GameUtil.GetFormattedCycles(component.GetComponent<Growing>().DomesticGrowthTime(), "F1", false)));
			}
			foreach (MinionVitalsPanel.AmountLine amountLine2 in this.amountsLines)
			{
				amountLine2.go.SetActive(false);
			}
			foreach (MinionVitalsPanel.AttributeLine attributeLine2 in this.attributesLines)
			{
				attributeLine2.go.SetActive(false);
			}
		}
	}

	// Token: 0x06005873 RID: 22643 RVA: 0x00201648 File Offset: 0x001FF848
	private string GetAirPressureTooltip(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		if (component == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_PRESSURE.text.Replace("{pressure}", GameUtil.GetFormattedMass(component.GetExternalPressure(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06005874 RID: 22644 RVA: 0x00201694 File Offset: 0x001FF894
	private string GetInternalTemperatureTooltip(GameObject go)
	{
		TemperatureVulnerable component = go.GetComponent<TemperatureVulnerable>();
		if (component == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_TEMPERATURE.text.Replace("{temperature}", GameUtil.GetFormattedTemperature(component.InternalTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
	}

	// Token: 0x06005875 RID: 22645 RVA: 0x002016DC File Offset: 0x001FF8DC
	private string GetFertilizationTooltip(GameObject go)
	{
		FertilizationMonitor.Instance smi = go.GetSMI<FertilizationMonitor.Instance>();
		if (smi == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_FERTILIZER.text.Replace("{mass}", GameUtil.GetFormattedMass(smi.total_fertilizer_available, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06005876 RID: 22646 RVA: 0x00201720 File Offset: 0x001FF920
	private string GetIrrigationTooltip(GameObject go)
	{
		IrrigationMonitor.Instance smi = go.GetSMI<IrrigationMonitor.Instance>();
		if (smi == null)
		{
			return "";
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_IRRIGATION.text.Replace("{mass}", GameUtil.GetFormattedMass(smi.total_fertilizer_available, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06005877 RID: 22647 RVA: 0x00201764 File Offset: 0x001FF964
	private string GetIlluminationTooltip(GameObject go)
	{
		IlluminationVulnerable component = go.GetComponent<IlluminationVulnerable>();
		if (component == null)
		{
			return "";
		}
		if ((component.prefersDarkness && component.IsComfortable()) || (!component.prefersDarkness && !component.IsComfortable()))
		{
			return UI.TOOLTIPS.VITALS_CHECKBOX_ILLUMINATION_DARK;
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_ILLUMINATION_LIGHT;
	}

	// Token: 0x06005878 RID: 22648 RVA: 0x002017BC File Offset: 0x001FF9BC
	private string GetRadiationTooltip(GameObject go)
	{
		int num = Grid.PosToCell(go);
		float num2 = (Grid.IsValidCell(num) ? Grid.Radiation[num] : 0f);
		AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MinRadiationThreshold);
		AttributeInstance attributeInstance2 = go.GetAttributes().Get(Db.Get().PlantAttributes.MaxRadiationThreshold);
		MutantPlant component = go.GetComponent<MutantPlant>();
		bool flag = component != null && component.IsOriginal;
		string text;
		if (attributeInstance.GetTotalValue() == 0f)
		{
			text = UI.TOOLTIPS.VITALS_CHECKBOX_RADIATION_NO_MIN.Replace("{rads}", GameUtil.GetFormattedRads(num2, GameUtil.TimeSlice.None)).Replace("{maxRads}", attributeInstance2.GetFormattedValue());
		}
		else
		{
			text = UI.TOOLTIPS.VITALS_CHECKBOX_RADIATION.Replace("{rads}", GameUtil.GetFormattedRads(num2, GameUtil.TimeSlice.None)).Replace("{minRads}", attributeInstance.GetFormattedValue()).Replace("{maxRads}", attributeInstance2.GetFormattedValue());
		}
		if (flag)
		{
			text += UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_SEED_TOOLTIP;
		}
		return text;
	}

	// Token: 0x06005879 RID: 22649 RVA: 0x002018C4 File Offset: 0x001FFAC4
	private string GetReceptacleTooltip(GameObject go)
	{
		ReceptacleMonitor component = go.GetComponent<ReceptacleMonitor>();
		if (component == null)
		{
			return "";
		}
		if (component.HasOperationalReceptacle())
		{
			return UI.TOOLTIPS.VITALS_CHECKBOX_RECEPTACLE_OPERATIONAL;
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_RECEPTACLE_INOPERATIONAL;
	}

	// Token: 0x0600587A RID: 22650 RVA: 0x00201904 File Offset: 0x001FFB04
	private string GetAtmosphereTooltip(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		if (component != null && component.currentAtmoElement != null)
		{
			return UI.TOOLTIPS.VITALS_CHECKBOX_ATMOSPHERE.text.Replace("{element}", component.currentAtmoElement.name);
		}
		return UI.TOOLTIPS.VITALS_CHECKBOX_ATMOSPHERE;
	}

	// Token: 0x0600587B RID: 22651 RVA: 0x00201954 File Offset: 0x001FFB54
	private string GetAirPressureLabel(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		return string.Concat(new string[]
		{
			Db.Get().Amounts.AirPressure.Name,
			"\n    • ",
			GameUtil.GetFormattedMass(component.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Gram, false, "{0:0.#}"),
			" - ",
			GameUtil.GetFormattedMass(component.pressureWarning_High, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Gram, true, "{0:0.#}")
		});
	}

	// Token: 0x0600587C RID: 22652 RVA: 0x002019C8 File Offset: 0x001FFBC8
	private string GetInternalTemperatureLabel(GameObject go)
	{
		TemperatureVulnerable component = go.GetComponent<TemperatureVulnerable>();
		return string.Concat(new string[]
		{
			Db.Get().Amounts.Temperature.Name,
			"\n    • ",
			GameUtil.GetFormattedTemperature(component.TemperatureWarningLow, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, false, false),
			" - ",
			GameUtil.GetFormattedTemperature(component.TemperatureWarningHigh, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)
		});
	}

	// Token: 0x0600587D RID: 22653 RVA: 0x00201A34 File Offset: 0x001FFC34
	private string GetFertilizationLabel(GameObject go)
	{
		StateMachine<FertilizationMonitor, FertilizationMonitor.Instance, IStateMachineTarget, FertilizationMonitor.Def>.GenericInstance smi = go.GetSMI<FertilizationMonitor.Instance>();
		string text = Db.Get().Amounts.Fertilization.Name;
		float totalValue = go.GetAttributes().Get(Db.Get().PlantAttributes.FertilizerUsageMod).GetTotalValue();
		foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in smi.def.consumedElements)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n    • ",
				ElementLoader.GetElement(consumeInfo.tag).name,
				" ",
				GameUtil.GetFormattedMass(consumeInfo.massConsumptionRate * totalValue, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
			});
		}
		return text;
	}

	// Token: 0x0600587E RID: 22654 RVA: 0x00201AEC File Offset: 0x001FFCEC
	private string GetIrrigationLabel(GameObject go)
	{
		StateMachine<IrrigationMonitor, IrrigationMonitor.Instance, IStateMachineTarget, IrrigationMonitor.Def>.GenericInstance smi = go.GetSMI<IrrigationMonitor.Instance>();
		string text = Db.Get().Amounts.Irrigation.Name;
		float totalValue = go.GetAttributes().Get(Db.Get().PlantAttributes.FertilizerUsageMod).GetTotalValue();
		foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in smi.def.consumedElements)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n    • ",
				ElementLoader.GetElement(consumeInfo.tag).name,
				": ",
				GameUtil.GetFormattedMass(consumeInfo.massConsumptionRate * totalValue, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
			});
		}
		return text;
	}

	// Token: 0x0600587F RID: 22655 RVA: 0x00201BA4 File Offset: 0x001FFDA4
	private string GetIlluminationLabel(GameObject go)
	{
		IlluminationVulnerable component = go.GetComponent<IlluminationVulnerable>();
		return Db.Get().Amounts.Illumination.Name + "\n    • " + (component.prefersDarkness ? UI.GAMEOBJECTEFFECTS.DARKNESS.ToString() : GameUtil.GetFormattedLux(component.LightIntensityThreshold));
	}

	// Token: 0x06005880 RID: 22656 RVA: 0x00201BF8 File Offset: 0x001FFDF8
	private string GetAtmosphereLabel(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		string text = UI.VITALSSCREEN.ATMOSPHERE_CONDITION;
		foreach (Element element in component.safe_atmospheres)
		{
			text = text + "\n    • " + element.name;
		}
		return text;
	}

	// Token: 0x06005881 RID: 22657 RVA: 0x00201C68 File Offset: 0x001FFE68
	private string GetRadiationLabel(GameObject go)
	{
		AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MinRadiationThreshold);
		AttributeInstance attributeInstance2 = go.GetAttributes().Get(Db.Get().PlantAttributes.MaxRadiationThreshold);
		if (attributeInstance.GetTotalValue() == 0f)
		{
			return UI.GAMEOBJECTEFFECTS.AMBIENT_RADIATION + "\n    • " + UI.GAMEOBJECTEFFECTS.AMBIENT_NO_MIN_RADIATION_FMT.Replace("{maxRads}", attributeInstance2.GetFormattedValue());
		}
		return UI.GAMEOBJECTEFFECTS.AMBIENT_RADIATION + "\n    • " + UI.GAMEOBJECTEFFECTS.AMBIENT_RADIATION_FMT.Replace("{minRads}", attributeInstance.GetFormattedValue()).Replace("{maxRads}", attributeInstance2.GetFormattedValue());
	}

	// Token: 0x06005882 RID: 22658 RVA: 0x00201D1C File Offset: 0x001FFF1C
	private bool check_pressure(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		return !(component != null) || component.ExternalPressureState == PressureVulnerable.PressureState.Normal;
	}

	// Token: 0x06005883 RID: 22659 RVA: 0x00201D44 File Offset: 0x001FFF44
	private bool check_temperature(GameObject go)
	{
		TemperatureVulnerable component = go.GetComponent<TemperatureVulnerable>();
		return !(component != null) || component.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.Normal;
	}

	// Token: 0x06005884 RID: 22660 RVA: 0x00201D6C File Offset: 0x001FFF6C
	private bool check_irrigation(GameObject go)
	{
		IrrigationMonitor.Instance smi = go.GetSMI<IrrigationMonitor.Instance>();
		return smi == null || (!smi.IsInsideState(smi.sm.replanted.starved) && !smi.IsInsideState(smi.sm.wild));
	}

	// Token: 0x06005885 RID: 22661 RVA: 0x00201DB4 File Offset: 0x001FFFB4
	private bool check_illumination(GameObject go)
	{
		IlluminationVulnerable component = go.GetComponent<IlluminationVulnerable>();
		return !(component != null) || component.IsComfortable();
	}

	// Token: 0x06005886 RID: 22662 RVA: 0x00201DDC File Offset: 0x001FFFDC
	private bool check_radiation(GameObject go)
	{
		AttributeInstance attributeInstance = go.GetAttributes().Get(Db.Get().PlantAttributes.MinRadiationThreshold);
		if (attributeInstance != null && attributeInstance.GetTotalValue() != 0f)
		{
			int num = Grid.PosToCell(go);
			return (Grid.IsValidCell(num) ? Grid.Radiation[num] : 0f) >= attributeInstance.GetTotalValue();
		}
		return true;
	}

	// Token: 0x06005887 RID: 22663 RVA: 0x00201E44 File Offset: 0x00200044
	private bool check_receptacle(GameObject go)
	{
		ReceptacleMonitor component = go.GetComponent<ReceptacleMonitor>();
		return !(component == null) && component.HasOperationalReceptacle();
	}

	// Token: 0x06005888 RID: 22664 RVA: 0x00201E6C File Offset: 0x0020006C
	private bool check_fertilizer(GameObject go)
	{
		FertilizationMonitor.Instance smi = go.GetSMI<FertilizationMonitor.Instance>();
		return smi == null || smi.sm.hasCorrectFertilizer.Get(smi);
	}

	// Token: 0x06005889 RID: 22665 RVA: 0x00201E98 File Offset: 0x00200098
	private bool check_atmosphere(GameObject go)
	{
		PressureVulnerable component = go.GetComponent<PressureVulnerable>();
		return !(component != null) || component.testAreaElementSafe;
	}

	// Token: 0x04003BDA RID: 15322
	public GameObject LineItemPrefab;

	// Token: 0x04003BDB RID: 15323
	public GameObject CheckboxLinePrefab;

	// Token: 0x04003BDC RID: 15324
	public GameObject selectedEntity;

	// Token: 0x04003BDD RID: 15325
	public List<MinionVitalsPanel.AmountLine> amountsLines = new List<MinionVitalsPanel.AmountLine>();

	// Token: 0x04003BDE RID: 15326
	public List<MinionVitalsPanel.AttributeLine> attributesLines = new List<MinionVitalsPanel.AttributeLine>();

	// Token: 0x04003BDF RID: 15327
	public List<MinionVitalsPanel.CheckboxLine> checkboxLines = new List<MinionVitalsPanel.CheckboxLine>();

	// Token: 0x04003BE0 RID: 15328
	public Transform conditionsContainerNormal;

	// Token: 0x04003BE1 RID: 15329
	public Transform conditionsContainerAdditional;

	// Token: 0x020019C7 RID: 6599
	[DebuggerDisplay("{amount.Name}")]
	public struct AmountLine
	{
		// Token: 0x06009135 RID: 37173 RVA: 0x0031410C File Offset: 0x0031230C
		public bool TryUpdate(Amounts amounts)
		{
			foreach (AmountInstance amountInstance in amounts)
			{
				if (this.amount == amountInstance.amount && !amountInstance.hide)
				{
					this.locText.SetText(this.amount.GetDescription(amountInstance));
					this.toolTip.toolTip = this.toolTipFunc(amountInstance);
					this.imageToggle.SetValue(amountInstance);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400752B RID: 29995
		public Amount amount;

		// Token: 0x0400752C RID: 29996
		public GameObject go;

		// Token: 0x0400752D RID: 29997
		public ValueTrendImageToggle imageToggle;

		// Token: 0x0400752E RID: 29998
		public LocText locText;

		// Token: 0x0400752F RID: 29999
		public ToolTip toolTip;

		// Token: 0x04007530 RID: 30000
		public Func<AmountInstance, string> toolTipFunc;
	}

	// Token: 0x020019C8 RID: 6600
	[DebuggerDisplay("{attribute.Name}")]
	public struct AttributeLine
	{
		// Token: 0x06009136 RID: 37174 RVA: 0x003141A4 File Offset: 0x003123A4
		public bool TryUpdate(Attributes attributes)
		{
			foreach (AttributeInstance attributeInstance in attributes)
			{
				if (this.attribute == attributeInstance.modifier && !attributeInstance.hide)
				{
					this.locText.SetText(this.attribute.GetDescription(attributeInstance));
					this.toolTip.toolTip = this.toolTipFunc(attributeInstance);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04007531 RID: 30001
		public Klei.AI.Attribute attribute;

		// Token: 0x04007532 RID: 30002
		public GameObject go;

		// Token: 0x04007533 RID: 30003
		public LocText locText;

		// Token: 0x04007534 RID: 30004
		public ToolTip toolTip;

		// Token: 0x04007535 RID: 30005
		public Func<AttributeInstance, string> toolTipFunc;
	}

	// Token: 0x020019C9 RID: 6601
	public struct CheckboxLine
	{
		// Token: 0x04007536 RID: 30006
		public Amount amount;

		// Token: 0x04007537 RID: 30007
		public GameObject go;

		// Token: 0x04007538 RID: 30008
		public LocText locText;

		// Token: 0x04007539 RID: 30009
		public Func<GameObject, string> tooltip;

		// Token: 0x0400753A RID: 30010
		public Func<GameObject, bool> get_value;

		// Token: 0x0400753B RID: 30011
		public Func<GameObject, MinionVitalsPanel.CheckboxLineDisplayType> display_condition;

		// Token: 0x0400753C RID: 30012
		public Func<GameObject, string> label_text_func;

		// Token: 0x0400753D RID: 30013
		public Transform parentContainer;
	}

	// Token: 0x020019CA RID: 6602
	public enum CheckboxLineDisplayType
	{
		// Token: 0x0400753F RID: 30015
		Normal,
		// Token: 0x04007540 RID: 30016
		Diminished,
		// Token: 0x04007541 RID: 30017
		Hidden
	}
}
