using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A83 RID: 2691
public class DebugPaintElementScreen : KScreen
{
	// Token: 0x17000625 RID: 1573
	// (get) Token: 0x0600525C RID: 21084 RVA: 0x001DC171 File Offset: 0x001DA371
	// (set) Token: 0x0600525D RID: 21085 RVA: 0x001DC178 File Offset: 0x001DA378
	public static DebugPaintElementScreen Instance { get; private set; }

	// Token: 0x0600525E RID: 21086 RVA: 0x001DC180 File Offset: 0x001DA380
	public static void DestroyInstance()
	{
		DebugPaintElementScreen.Instance = null;
	}

	// Token: 0x0600525F RID: 21087 RVA: 0x001DC188 File Offset: 0x001DA388
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DebugPaintElementScreen.Instance = this;
		this.SetupLocText();
		this.inputFields.Add(this.massPressureInput);
		this.inputFields.Add(this.temperatureInput);
		this.inputFields.Add(this.diseaseCountInput);
		this.inputFields.Add(this.filterInput);
		foreach (KInputTextField kinputTextField in this.inputFields)
		{
			kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
			{
				base.isEditing = true;
			}));
			kinputTextField.onEndEdit.AddListener(delegate(string value)
			{
				base.isEditing = false;
			});
		}
		base.gameObject.SetActive(false);
		this.activateOnSpawn = true;
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005260 RID: 21088 RVA: 0x001DC27C File Offset: 0x001DA47C
	private void SetupLocText()
	{
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		component.GetReference<LocText>("Title").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.TITLE;
		component.GetReference<LocText>("ElementLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.ELEMENT;
		component.GetReference<LocText>("MassLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.MASS_KG;
		component.GetReference<LocText>("TemperatureLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.TEMPERATURE_KELVIN;
		component.GetReference<LocText>("DiseaseLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.DISEASE;
		component.GetReference<LocText>("DiseaseCountLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.DISEASE_COUNT;
		component.GetReference<LocText>("AddFoWMaskLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.ADD_FOW_MASK;
		component.GetReference<LocText>("RemoveFoWMaskLabel").text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.REMOVE_FOW_MASK;
		this.elementButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.ELEMENT;
		this.diseaseButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.DISEASE;
		this.paintButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.PAINT;
		this.fillButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.FILL;
		this.spawnButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.SPAWN_ALL;
		this.sampleButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.SAMPLE;
		this.storeButton.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.STORE;
		this.affectBuildings.transform.parent.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.BUILDINGS;
		this.affectCells.transform.parent.GetComponentsInChildren<LocText>()[0].text = UI.DEBUG_TOOLS.PAINT_ELEMENTS_SCREEN.CELLS;
	}

	// Token: 0x06005261 RID: 21089 RVA: 0x001DC470 File Offset: 0x001DA670
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.element = SimHashes.Ice;
		this.diseaseIdx = byte.MaxValue;
		this.ConfigureElements();
		List<string> list = new List<string>();
		list.Insert(0, "None");
		foreach (Disease disease in Db.Get().Diseases.resources)
		{
			list.Add(disease.Name);
		}
		this.diseasePopup.SetOptions(list.ToArray());
		KPopupMenu kpopupMenu = this.diseasePopup;
		kpopupMenu.OnSelect = (Action<string, int>)Delegate.Combine(kpopupMenu.OnSelect, new Action<string, int>(this.OnSelectDisease));
		this.SelectDiseaseOption((int)this.diseaseIdx);
		this.paintButton.onClick += this.OnClickPaint;
		this.fillButton.onClick += this.OnClickFill;
		this.sampleButton.onClick += this.OnClickSample;
		this.storeButton.onClick += this.OnClickStore;
		if (SaveGame.Instance.worldGenSpawner.SpawnsRemain())
		{
			this.spawnButton.onClick += this.OnClickSpawn;
		}
		KPopupMenu kpopupMenu2 = this.elementPopup;
		kpopupMenu2.OnSelect = (Action<string, int>)Delegate.Combine(kpopupMenu2.OnSelect, new Action<string, int>(this.OnSelectElement));
		this.elementButton.onClick += this.elementPopup.OnClick;
		this.diseaseButton.onClick += this.diseasePopup.OnClick;
	}

	// Token: 0x06005262 RID: 21090 RVA: 0x001DC62C File Offset: 0x001DA82C
	private void FilterElements(string filterValue)
	{
		if (string.IsNullOrEmpty(filterValue))
		{
			foreach (KButtonMenu.ButtonInfo buttonInfo in this.elementPopup.GetButtons())
			{
				buttonInfo.uibutton.gameObject.SetActive(true);
			}
			return;
		}
		filterValue = this.filter.ToLower();
		foreach (KButtonMenu.ButtonInfo buttonInfo2 in this.elementPopup.GetButtons())
		{
			buttonInfo2.uibutton.gameObject.SetActive(buttonInfo2.text.ToLower().Contains(filterValue));
		}
	}

	// Token: 0x06005263 RID: 21091 RVA: 0x001DC6F8 File Offset: 0x001DA8F8
	private void ConfigureElements()
	{
		if (this.filter != null)
		{
			this.filter = this.filter.ToLower();
		}
		List<DebugPaintElementScreen.ElemDisplayInfo> list = new List<DebugPaintElementScreen.ElemDisplayInfo>();
		foreach (Element element in ElementLoader.elements)
		{
			if (element.name != "Element Not Loaded" && element.substance != null && element.substance.showInEditor && (string.IsNullOrEmpty(this.filter) || element.name.ToLower().Contains(this.filter)))
			{
				list.Add(new DebugPaintElementScreen.ElemDisplayInfo
				{
					id = element.id,
					displayStr = element.name + " (" + element.GetStateString() + ")"
				});
			}
		}
		list.Sort((DebugPaintElementScreen.ElemDisplayInfo a, DebugPaintElementScreen.ElemDisplayInfo b) => a.displayStr.CompareTo(b.displayStr));
		if (string.IsNullOrEmpty(this.filter))
		{
			SimHashes[] array = new SimHashes[]
			{
				SimHashes.SlimeMold,
				SimHashes.Vacuum,
				SimHashes.Dirt,
				SimHashes.CarbonDioxide,
				SimHashes.Water,
				SimHashes.Oxygen
			};
			for (int i = 0; i < array.Length; i++)
			{
				Element element2 = ElementLoader.FindElementByHash(array[i]);
				list.Insert(0, new DebugPaintElementScreen.ElemDisplayInfo
				{
					id = element2.id,
					displayStr = element2.name + " (" + element2.GetStateString() + ")"
				});
			}
		}
		this.options_list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (DebugPaintElementScreen.ElemDisplayInfo elemDisplayInfo in list)
		{
			list2.Add(elemDisplayInfo.displayStr);
			this.options_list.Add(elemDisplayInfo.id.ToString());
		}
		this.elementPopup.SetOptions(list2);
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].id == this.element)
			{
				this.elementPopup.SelectOption(list2[j], j);
			}
		}
		this.elementPopup.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0f, 1f);
	}

	// Token: 0x06005264 RID: 21092 RVA: 0x001DC978 File Offset: 0x001DAB78
	private void OnClickSpawn()
	{
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			worldContainer.SetDiscovered(true);
		}
		SaveGame.Instance.worldGenSpawner.SpawnEverything();
		this.spawnButton.GetComponent<KButton>().isInteractable = false;
	}

	// Token: 0x06005265 RID: 21093 RVA: 0x001DC9E8 File Offset: 0x001DABE8
	private void OnClickPaint()
	{
		this.OnChangeMassPressure();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		this.OnChangeFOWReveal();
		DebugTool.Instance.Activate(DebugTool.Type.ReplaceSubstance);
	}

	// Token: 0x06005266 RID: 21094 RVA: 0x001DCA0D File Offset: 0x001DAC0D
	private void OnClickStore()
	{
		this.OnChangeMassPressure();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		this.OnChangeFOWReveal();
		DebugTool.Instance.Activate(DebugTool.Type.StoreSubstance);
	}

	// Token: 0x06005267 RID: 21095 RVA: 0x001DCA32 File Offset: 0x001DAC32
	private void OnClickSample()
	{
		this.OnChangeMassPressure();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		this.OnChangeFOWReveal();
		DebugTool.Instance.Activate(DebugTool.Type.Sample);
	}

	// Token: 0x06005268 RID: 21096 RVA: 0x001DCA57 File Offset: 0x001DAC57
	private void OnClickFill()
	{
		this.OnChangeMassPressure();
		this.OnChangeTemperature();
		this.OnDiseaseCountChange();
		DebugTool.Instance.Activate(DebugTool.Type.FillReplaceSubstance);
	}

	// Token: 0x06005269 RID: 21097 RVA: 0x001DCA76 File Offset: 0x001DAC76
	private void OnSelectElement(string str, int index)
	{
		this.element = (SimHashes)Enum.Parse(typeof(SimHashes), this.options_list[index]);
		this.elementButton.GetComponentInChildren<LocText>().text = str;
	}

	// Token: 0x0600526A RID: 21098 RVA: 0x001DCAAF File Offset: 0x001DACAF
	private void OnSelectElement(SimHashes element)
	{
		this.element = element;
		this.elementButton.GetComponentInChildren<LocText>().text = ElementLoader.FindElementByHash(element).name;
	}

	// Token: 0x0600526B RID: 21099 RVA: 0x001DCAD4 File Offset: 0x001DACD4
	private void OnSelectDisease(string str, int index)
	{
		this.diseaseIdx = byte.MaxValue;
		for (int i = 0; i < Db.Get().Diseases.Count; i++)
		{
			if (Db.Get().Diseases[i].Name == str)
			{
				this.diseaseIdx = (byte)i;
			}
		}
		this.SelectDiseaseOption((int)this.diseaseIdx);
	}

	// Token: 0x0600526C RID: 21100 RVA: 0x001DCB38 File Offset: 0x001DAD38
	private void SelectDiseaseOption(int diseaseIdx)
	{
		if (diseaseIdx == 255)
		{
			this.diseaseButton.GetComponentInChildren<LocText>().text = "None";
			return;
		}
		string name = Db.Get().Diseases[diseaseIdx].Name;
		this.diseaseButton.GetComponentInChildren<LocText>().text = name;
	}

	// Token: 0x0600526D RID: 21101 RVA: 0x001DCB8C File Offset: 0x001DAD8C
	private void OnChangeFOWReveal()
	{
		if (this.paintPreventFOWReveal.isOn)
		{
			this.paintAllowFOWReveal.isOn = false;
		}
		if (this.paintAllowFOWReveal.isOn)
		{
			this.paintPreventFOWReveal.isOn = false;
		}
		this.set_prevent_fow_reveal = this.paintPreventFOWReveal.isOn;
		this.set_allow_fow_reveal = this.paintAllowFOWReveal.isOn;
	}

	// Token: 0x0600526E RID: 21102 RVA: 0x001DCBF0 File Offset: 0x001DADF0
	public void OnChangeMassPressure()
	{
		float num;
		try
		{
			num = Convert.ToSingle(this.massPressureInput.text);
		}
		catch
		{
			num = -1f;
		}
		this.mass = num;
	}

	// Token: 0x0600526F RID: 21103 RVA: 0x001DCC30 File Offset: 0x001DAE30
	public void OnChangeTemperature()
	{
		float num;
		try
		{
			num = Convert.ToSingle(this.temperatureInput.text);
		}
		catch
		{
			num = -1f;
		}
		this.temperature = num;
	}

	// Token: 0x06005270 RID: 21104 RVA: 0x001DCC70 File Offset: 0x001DAE70
	public void OnDiseaseCountChange()
	{
		int num;
		try
		{
			num = Convert.ToInt32(this.diseaseCountInput.text);
		}
		catch
		{
			num = 0;
		}
		this.diseaseCount = num;
	}

	// Token: 0x06005271 RID: 21105 RVA: 0x001DCCAC File Offset: 0x001DAEAC
	public void OnElementsFilterEdited(string new_filter)
	{
		this.filter = (string.IsNullOrEmpty(this.filterInput.text) ? null : this.filterInput.text);
		this.FilterElements(this.filter);
	}

	// Token: 0x06005272 RID: 21106 RVA: 0x001DCCE0 File Offset: 0x001DAEE0
	public void SampleCell(int cell)
	{
		this.massPressureInput.text = (Grid.Pressure[cell] * 0.010000001f).ToString();
		this.temperatureInput.text = Grid.Temperature[cell].ToString();
		this.OnSelectElement(ElementLoader.GetElementID(Grid.Element[cell].tag));
		this.OnChangeMassPressure();
		this.OnChangeTemperature();
	}

	// Token: 0x0400378F RID: 14223
	[Header("Current State")]
	public SimHashes element;

	// Token: 0x04003790 RID: 14224
	[NonSerialized]
	public float mass = 1000f;

	// Token: 0x04003791 RID: 14225
	[NonSerialized]
	public float temperature = -1f;

	// Token: 0x04003792 RID: 14226
	[NonSerialized]
	public bool set_prevent_fow_reveal;

	// Token: 0x04003793 RID: 14227
	[NonSerialized]
	public bool set_allow_fow_reveal;

	// Token: 0x04003794 RID: 14228
	[NonSerialized]
	public int diseaseCount;

	// Token: 0x04003795 RID: 14229
	public byte diseaseIdx;

	// Token: 0x04003796 RID: 14230
	[Header("Popup Buttons")]
	[SerializeField]
	private KButton elementButton;

	// Token: 0x04003797 RID: 14231
	[SerializeField]
	private KButton diseaseButton;

	// Token: 0x04003798 RID: 14232
	[Header("Popup Menus")]
	[SerializeField]
	private KPopupMenu elementPopup;

	// Token: 0x04003799 RID: 14233
	[SerializeField]
	private KPopupMenu diseasePopup;

	// Token: 0x0400379A RID: 14234
	[Header("Value Inputs")]
	[SerializeField]
	private KInputTextField massPressureInput;

	// Token: 0x0400379B RID: 14235
	[SerializeField]
	private KInputTextField temperatureInput;

	// Token: 0x0400379C RID: 14236
	[SerializeField]
	private KInputTextField diseaseCountInput;

	// Token: 0x0400379D RID: 14237
	[SerializeField]
	private KInputTextField filterInput;

	// Token: 0x0400379E RID: 14238
	[Header("Tool Buttons")]
	[SerializeField]
	private KButton paintButton;

	// Token: 0x0400379F RID: 14239
	[SerializeField]
	private KButton fillButton;

	// Token: 0x040037A0 RID: 14240
	[SerializeField]
	private KButton sampleButton;

	// Token: 0x040037A1 RID: 14241
	[SerializeField]
	private KButton spawnButton;

	// Token: 0x040037A2 RID: 14242
	[SerializeField]
	private KButton storeButton;

	// Token: 0x040037A3 RID: 14243
	[Header("Parameter Toggles")]
	public Toggle paintElement;

	// Token: 0x040037A4 RID: 14244
	public Toggle paintMass;

	// Token: 0x040037A5 RID: 14245
	public Toggle paintTemperature;

	// Token: 0x040037A6 RID: 14246
	public Toggle paintDisease;

	// Token: 0x040037A7 RID: 14247
	public Toggle paintDiseaseCount;

	// Token: 0x040037A8 RID: 14248
	public Toggle affectBuildings;

	// Token: 0x040037A9 RID: 14249
	public Toggle affectCells;

	// Token: 0x040037AA RID: 14250
	public Toggle paintPreventFOWReveal;

	// Token: 0x040037AB RID: 14251
	public Toggle paintAllowFOWReveal;

	// Token: 0x040037AC RID: 14252
	private List<KInputTextField> inputFields = new List<KInputTextField>();

	// Token: 0x040037AD RID: 14253
	private List<string> options_list = new List<string>();

	// Token: 0x040037AE RID: 14254
	private string filter;

	// Token: 0x0200190F RID: 6415
	private struct ElemDisplayInfo
	{
		// Token: 0x0400732F RID: 29487
		public SimHashes id;

		// Token: 0x04007330 RID: 29488
		public string displayStr;
	}
}
