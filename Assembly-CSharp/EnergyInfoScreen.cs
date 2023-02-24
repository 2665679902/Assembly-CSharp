using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000A95 RID: 2709
public class EnergyInfoScreen : TargetScreen
{
	// Token: 0x06005319 RID: 21273 RVA: 0x001E19E0 File Offset: 0x001DFBE0
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ICircuitConnected>() != null || target.GetComponent<Wire>() != null;
	}

	// Token: 0x0600531A RID: 21274 RVA: 0x001E19F8 File Offset: 0x001DFBF8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overviewPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.overviewPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.ENERGYGENERATOR.CIRCUITOVERVIEW;
		this.generatorsPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.generatorsPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.ENERGYGENERATOR.GENERATORS;
		this.consumersPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.consumersPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.ENERGYGENERATOR.CONSUMERS;
		this.batteriesPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.batteriesPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.ENERGYGENERATOR.BATTERIES;
	}

	// Token: 0x0600531B RID: 21275 RVA: 0x001E1AF8 File Offset: 0x001DFCF8
	private GameObject AddOrGetLabel(Dictionary<string, GameObject> labels, GameObject panel, string id)
	{
		GameObject gameObject;
		if (labels.ContainsKey(id))
		{
			gameObject = labels[id];
		}
		else
		{
			gameObject = Util.KInstantiate(this.labelTemplate, panel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject, null);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			labels[id] = gameObject;
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	// Token: 0x0600531C RID: 21276 RVA: 0x001E1B66 File Offset: 0x001DFD66
	private void LateUpdate()
	{
		this.Refresh();
	}

	// Token: 0x0600531D RID: 21277 RVA: 0x001E1B70 File Offset: 0x001DFD70
	private void Refresh()
	{
		if (this.selectedTarget == null)
		{
			return;
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.overviewLabels)
		{
			keyValuePair.Value.SetActive(false);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.generatorsLabels)
		{
			keyValuePair2.Value.SetActive(false);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair3 in this.consumersLabels)
		{
			keyValuePair3.Value.SetActive(false);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair4 in this.batteriesLabels)
		{
			keyValuePair4.Value.SetActive(false);
		}
		CircuitManager circuitManager = Game.Instance.circuitManager;
		ushort num = ushort.MaxValue;
		ICircuitConnected component = this.selectedTarget.GetComponent<ICircuitConnected>();
		if (component != null)
		{
			num = circuitManager.GetCircuitID(component);
		}
		else if (this.selectedTarget.GetComponent<Wire>() != null)
		{
			int num2 = Grid.PosToCell(this.selectedTarget.transform.GetPosition());
			num = Game.Instance.circuitManager.GetCircuitID(num2);
		}
		if (num != 65535)
		{
			this.overviewPanel.SetActive(true);
			this.generatorsPanel.SetActive(true);
			this.consumersPanel.SetActive(true);
			this.batteriesPanel.SetActive(true);
			float joulesAvailableOnCircuit = circuitManager.GetJoulesAvailableOnCircuit(num);
			GameObject gameObject = this.AddOrGetLabel(this.overviewLabels, this.overviewPanel, "joulesAvailable");
			gameObject.GetComponent<LocText>().text = string.Format(UI.DETAILTABS.ENERGYGENERATOR.AVAILABLE_JOULES, GameUtil.GetFormattedJoules(joulesAvailableOnCircuit, "F1", GameUtil.TimeSlice.None));
			gameObject.GetComponent<ToolTip>().toolTip = UI.DETAILTABS.ENERGYGENERATOR.AVAILABLE_JOULES_TOOLTIP;
			gameObject.SetActive(true);
			float wattsGeneratedByCircuit = circuitManager.GetWattsGeneratedByCircuit(num);
			float potentialWattsGeneratedByCircuit = circuitManager.GetPotentialWattsGeneratedByCircuit(num);
			gameObject = this.AddOrGetLabel(this.overviewLabels, this.overviewPanel, "wattageGenerated");
			string text;
			if (wattsGeneratedByCircuit == potentialWattsGeneratedByCircuit)
			{
				text = GameUtil.GetFormattedWattage(wattsGeneratedByCircuit, GameUtil.WattageFormatterUnit.Automatic, true);
			}
			else
			{
				text = string.Format("{0} / {1}", GameUtil.GetFormattedWattage(wattsGeneratedByCircuit, GameUtil.WattageFormatterUnit.Automatic, true), GameUtil.GetFormattedWattage(potentialWattsGeneratedByCircuit, GameUtil.WattageFormatterUnit.Automatic, true));
			}
			gameObject.GetComponent<LocText>().text = string.Format(UI.DETAILTABS.ENERGYGENERATOR.WATTAGE_GENERATED, text);
			gameObject.GetComponent<ToolTip>().toolTip = UI.DETAILTABS.ENERGYGENERATOR.WATTAGE_GENERATED_TOOLTIP;
			gameObject.SetActive(true);
			gameObject = this.AddOrGetLabel(this.overviewLabels, this.overviewPanel, "wattageConsumed");
			gameObject.GetComponent<LocText>().text = string.Format(UI.DETAILTABS.ENERGYGENERATOR.WATTAGE_CONSUMED, GameUtil.GetFormattedWattage(circuitManager.GetWattsUsedByCircuit(num), GameUtil.WattageFormatterUnit.Automatic, true));
			gameObject.GetComponent<ToolTip>().toolTip = UI.DETAILTABS.ENERGYGENERATOR.WATTAGE_CONSUMED_TOOLTIP;
			gameObject.SetActive(true);
			gameObject = this.AddOrGetLabel(this.overviewLabels, this.overviewPanel, "potentialWattageConsumed");
			gameObject.GetComponent<LocText>().text = string.Format(UI.DETAILTABS.ENERGYGENERATOR.POTENTIAL_WATTAGE_CONSUMED, GameUtil.GetFormattedWattage(circuitManager.GetWattsNeededWhenActive(num), GameUtil.WattageFormatterUnit.Automatic, true));
			gameObject.GetComponent<ToolTip>().toolTip = UI.DETAILTABS.ENERGYGENERATOR.POTENTIAL_WATTAGE_CONSUMED_TOOLTIP;
			gameObject.SetActive(true);
			gameObject = this.AddOrGetLabel(this.overviewLabels, this.overviewPanel, "maxSafeWattage");
			gameObject.GetComponent<LocText>().text = string.Format(UI.DETAILTABS.ENERGYGENERATOR.MAX_SAFE_WATTAGE, GameUtil.GetFormattedWattage(circuitManager.GetMaxSafeWattageForCircuit(num), GameUtil.WattageFormatterUnit.Automatic, true));
			gameObject.GetComponent<ToolTip>().toolTip = UI.DETAILTABS.ENERGYGENERATOR.MAX_SAFE_WATTAGE_TOOLTIP;
			gameObject.SetActive(true);
			ReadOnlyCollection<Generator> generatorsOnCircuit = circuitManager.GetGeneratorsOnCircuit(num);
			ReadOnlyCollection<IEnergyConsumer> consumersOnCircuit = circuitManager.GetConsumersOnCircuit(num);
			List<Battery> batteriesOnCircuit = circuitManager.GetBatteriesOnCircuit(num);
			ReadOnlyCollection<Battery> transformersOnCircuit = circuitManager.GetTransformersOnCircuit(num);
			if (generatorsOnCircuit.Count > 0)
			{
				using (IEnumerator<Generator> enumerator2 = generatorsOnCircuit.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Generator generator = enumerator2.Current;
						if (generator != null && generator.GetComponent<Battery>() == null)
						{
							gameObject = this.AddOrGetLabel(this.generatorsLabels, this.generatorsPanel, generator.gameObject.GetInstanceID().ToString());
							if (generator.IsProducingPower())
							{
								gameObject.GetComponent<LocText>().text = string.Format("{0}: {1}", generator.GetComponent<KSelectable>().entityName, GameUtil.GetFormattedWattage(generator.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true));
							}
							else
							{
								gameObject.GetComponent<LocText>().text = string.Format("{0}: {1} / {2}", generator.GetComponent<KSelectable>().entityName, GameUtil.GetFormattedWattage(0f, GameUtil.WattageFormatterUnit.Automatic, true), GameUtil.GetFormattedWattage(generator.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true));
							}
							gameObject.SetActive(true);
							gameObject.GetComponent<LocText>().fontStyle = ((generator.gameObject == this.selectedTarget) ? FontStyles.Bold : FontStyles.Normal);
						}
					}
					goto IL_54A;
				}
			}
			gameObject = this.AddOrGetLabel(this.generatorsLabels, this.generatorsPanel, "nogenerators");
			gameObject.GetComponent<LocText>().text = UI.DETAILTABS.ENERGYGENERATOR.NOGENERATORS;
			gameObject.SetActive(true);
			IL_54A:
			if (consumersOnCircuit.Count > 0 || transformersOnCircuit.Count > 0)
			{
				foreach (IEnergyConsumer energyConsumer in consumersOnCircuit)
				{
					this.AddConsumerInfo(energyConsumer, gameObject);
				}
				using (IEnumerator<Battery> enumerator4 = transformersOnCircuit.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						IEnergyConsumer energyConsumer2 = enumerator4.Current;
						this.AddConsumerInfo(energyConsumer2, gameObject);
					}
					goto IL_5FF;
				}
			}
			gameObject = this.AddOrGetLabel(this.consumersLabels, this.consumersPanel, "noconsumers");
			gameObject.GetComponent<LocText>().text = UI.DETAILTABS.ENERGYGENERATOR.NOCONSUMERS;
			gameObject.SetActive(true);
			IL_5FF:
			if (batteriesOnCircuit.Count > 0)
			{
				using (List<Battery>.Enumerator enumerator5 = batteriesOnCircuit.GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						Battery battery = enumerator5.Current;
						if (battery != null)
						{
							gameObject = this.AddOrGetLabel(this.batteriesLabels, this.batteriesPanel, battery.gameObject.GetInstanceID().ToString());
							gameObject.GetComponent<LocText>().text = string.Format("{0}: {1}", battery.GetComponent<KSelectable>().entityName, GameUtil.GetFormattedJoules(battery.JoulesAvailable, "F1", GameUtil.TimeSlice.None));
							gameObject.SetActive(true);
							gameObject.GetComponent<LocText>().fontStyle = ((battery.gameObject == this.selectedTarget) ? FontStyles.Bold : FontStyles.Normal);
						}
					}
					return;
				}
			}
			gameObject = this.AddOrGetLabel(this.batteriesLabels, this.batteriesPanel, "nobatteries");
			gameObject.GetComponent<LocText>().text = UI.DETAILTABS.ENERGYGENERATOR.NOBATTERIES;
			gameObject.SetActive(true);
			return;
		}
		this.overviewPanel.SetActive(true);
		this.generatorsPanel.SetActive(false);
		this.consumersPanel.SetActive(false);
		this.batteriesPanel.SetActive(false);
		GameObject gameObject2 = this.AddOrGetLabel(this.overviewLabels, this.overviewPanel, "nocircuit");
		gameObject2.GetComponent<LocText>().text = UI.DETAILTABS.ENERGYGENERATOR.DISCONNECTED;
		gameObject2.SetActive(true);
	}

	// Token: 0x0600531E RID: 21278 RVA: 0x001E23B4 File Offset: 0x001E05B4
	private void AddConsumerInfo(IEnergyConsumer consumer, GameObject label)
	{
		KMonoBehaviour kmonoBehaviour = consumer as KMonoBehaviour;
		if (kmonoBehaviour != null)
		{
			label = this.AddOrGetLabel(this.consumersLabels, this.consumersPanel, kmonoBehaviour.gameObject.GetInstanceID().ToString());
			float wattsUsed = consumer.WattsUsed;
			float wattsNeededWhenActive = consumer.WattsNeededWhenActive;
			string text;
			if (wattsUsed == wattsNeededWhenActive)
			{
				text = GameUtil.GetFormattedWattage(wattsUsed, GameUtil.WattageFormatterUnit.Automatic, true);
			}
			else
			{
				text = string.Format("{0} / {1}", GameUtil.GetFormattedWattage(wattsUsed, GameUtil.WattageFormatterUnit.Automatic, true), GameUtil.GetFormattedWattage(wattsNeededWhenActive, GameUtil.WattageFormatterUnit.Automatic, true));
			}
			label.GetComponent<LocText>().text = string.Format("{0}: {1}", consumer.Name, text);
			label.SetActive(true);
			label.GetComponent<LocText>().fontStyle = ((kmonoBehaviour.gameObject == this.selectedTarget) ? FontStyles.Bold : FontStyles.Normal);
		}
	}

	// Token: 0x04003841 RID: 14401
	public GameObject labelTemplate;

	// Token: 0x04003842 RID: 14402
	private GameObject overviewPanel;

	// Token: 0x04003843 RID: 14403
	private GameObject generatorsPanel;

	// Token: 0x04003844 RID: 14404
	private GameObject consumersPanel;

	// Token: 0x04003845 RID: 14405
	private GameObject batteriesPanel;

	// Token: 0x04003846 RID: 14406
	private Dictionary<string, GameObject> overviewLabels = new Dictionary<string, GameObject>();

	// Token: 0x04003847 RID: 14407
	private Dictionary<string, GameObject> generatorsLabels = new Dictionary<string, GameObject>();

	// Token: 0x04003848 RID: 14408
	private Dictionary<string, GameObject> consumersLabels = new Dictionary<string, GameObject>();

	// Token: 0x04003849 RID: 14409
	private Dictionary<string, GameObject> batteriesLabels = new Dictionary<string, GameObject>();
}
