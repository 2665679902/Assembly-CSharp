using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000A21 RID: 2593
public class UserMenuScreen : KIconButtonMenu
{
	// Token: 0x06004E75 RID: 20085 RVA: 0x001BDE1C File Offset: 0x001BC01C
	protected override void OnPrefabInit()
	{
		this.keepMenuOpen = true;
		base.OnPrefabInit();
		this.priorityScreen = Util.KInstantiateUI<PriorityScreen>(this.priorityScreenPrefab.gameObject, this.priorityScreenParent, false);
		this.priorityScreen.InstantiateButtons(new Action<PrioritySetting>(this.OnPriorityClicked), true);
		this.buttonParent.transform.SetAsLastSibling();
	}

	// Token: 0x06004E76 RID: 20086 RVA: 0x001BDE7B File Offset: 0x001BC07B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.Subscribe(1980521255, new Action<object>(this.OnUIRefresh));
		KInputManager.InputChange.AddListener(new UnityAction(base.RefreshButtonTooltip));
	}

	// Token: 0x06004E77 RID: 20087 RVA: 0x001BDEB5 File Offset: 0x001BC0B5
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(base.RefreshButtonTooltip));
		base.OnForcedCleanUp();
	}

	// Token: 0x06004E78 RID: 20088 RVA: 0x001BDED3 File Offset: 0x001BC0D3
	public void SetSelected(GameObject go)
	{
		this.ClearPrioritizable();
		this.selected = go;
		this.RefreshPrioritizable();
	}

	// Token: 0x06004E79 RID: 20089 RVA: 0x001BDEE8 File Offset: 0x001BC0E8
	private void ClearPrioritizable()
	{
		if (this.selected != null)
		{
			Prioritizable component = this.selected.GetComponent<Prioritizable>();
			if (component != null)
			{
				Prioritizable prioritizable = component;
				prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Remove(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnPriorityChanged));
			}
		}
	}

	// Token: 0x06004E7A RID: 20090 RVA: 0x001BDF3C File Offset: 0x001BC13C
	private void RefreshPrioritizable()
	{
		if (this.selected != null)
		{
			Prioritizable component = this.selected.GetComponent<Prioritizable>();
			if (component != null && component.IsPrioritizable())
			{
				Prioritizable prioritizable = component;
				prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnPriorityChanged));
				this.priorityScreen.gameObject.SetActive(true);
				this.priorityScreen.SetScreenPriority(component.GetMasterPriority(), false);
				return;
			}
			this.priorityScreen.gameObject.SetActive(false);
		}
	}

	// Token: 0x06004E7B RID: 20091 RVA: 0x001BDFCC File Offset: 0x001BC1CC
	public void Refresh(GameObject go)
	{
		if (go != this.selected)
		{
			return;
		}
		this.buttonInfos.Clear();
		this.slidersInfos.Clear();
		Game.Instance.userMenu.AppendToScreen(go, this);
		base.SetButtons(this.buttonInfos);
		base.RefreshButtons();
		this.RefreshSliders();
		this.ClearPrioritizable();
		this.RefreshPrioritizable();
		if ((this.sliders == null || this.sliders.Count == 0) && (this.buttonInfos == null || this.buttonInfos.Count == 0) && !this.priorityScreen.gameObject.activeSelf)
		{
			base.transform.parent.gameObject.SetActive(false);
			return;
		}
		base.transform.parent.gameObject.SetActive(true);
	}

	// Token: 0x06004E7C RID: 20092 RVA: 0x001BE09C File Offset: 0x001BC29C
	public void AddSliders(IList<UserMenu.SliderInfo> sliders)
	{
		this.slidersInfos.AddRange(sliders);
	}

	// Token: 0x06004E7D RID: 20093 RVA: 0x001BE0AA File Offset: 0x001BC2AA
	public void AddButtons(IList<KIconButtonMenu.ButtonInfo> buttons)
	{
		this.buttonInfos.AddRange(buttons);
	}

	// Token: 0x06004E7E RID: 20094 RVA: 0x001BE0B8 File Offset: 0x001BC2B8
	private void OnUIRefresh(object data)
	{
		this.Refresh(data as GameObject);
	}

	// Token: 0x06004E7F RID: 20095 RVA: 0x001BE0C8 File Offset: 0x001BC2C8
	public void RefreshSliders()
	{
		if (this.sliders != null)
		{
			for (int i = 0; i < this.sliders.Count; i++)
			{
				UnityEngine.Object.Destroy(this.sliders[i].gameObject);
			}
			this.sliders = null;
		}
		if (this.slidersInfos == null || this.slidersInfos.Count == 0)
		{
			return;
		}
		this.sliders = new List<MinMaxSlider>();
		for (int j = 0; j < this.slidersInfos.Count; j++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.sliderPrefab.gameObject, Vector3.zero, Quaternion.identity);
			this.slidersInfos[j].sliderGO = gameObject;
			MinMaxSlider component = gameObject.GetComponent<MinMaxSlider>();
			this.sliders.Add(component);
			Transform transform = ((this.sliderParent != null) ? this.sliderParent.transform : base.transform);
			gameObject.transform.SetParent(transform, false);
			gameObject.SetActive(true);
			gameObject.name = "Slider";
			if (component.toolTip)
			{
				component.toolTip.toolTip = this.slidersInfos[j].toolTip;
			}
			component.lockType = this.slidersInfos[j].lockType;
			component.interactable = this.slidersInfos[j].interactable;
			component.minLimit = this.slidersInfos[j].minLimit;
			component.maxLimit = this.slidersInfos[j].maxLimit;
			component.currentMinValue = this.slidersInfos[j].currentMinValue;
			component.currentMaxValue = this.slidersInfos[j].currentMaxValue;
			component.onMinChange = this.slidersInfos[j].onMinChange;
			component.onMaxChange = this.slidersInfos[j].onMaxChange;
			component.direction = this.slidersInfos[j].direction;
			component.SetMode(this.slidersInfos[j].mode);
			component.SetMinMaxValue(this.slidersInfos[j].currentMinValue, this.slidersInfos[j].currentMaxValue, this.slidersInfos[j].minLimit, this.slidersInfos[j].maxLimit);
		}
	}

	// Token: 0x06004E80 RID: 20096 RVA: 0x001BE32C File Offset: 0x001BC52C
	private void OnPriorityClicked(PrioritySetting priority)
	{
		if (this.selected != null)
		{
			Prioritizable component = this.selected.GetComponent<Prioritizable>();
			if (component != null)
			{
				component.SetMasterPriority(priority);
			}
		}
	}

	// Token: 0x06004E81 RID: 20097 RVA: 0x001BE363 File Offset: 0x001BC563
	private void OnPriorityChanged(PrioritySetting priority)
	{
		this.priorityScreen.SetScreenPriority(priority, false);
	}

	// Token: 0x040033E4 RID: 13284
	private GameObject selected;

	// Token: 0x040033E5 RID: 13285
	public MinMaxSlider sliderPrefab;

	// Token: 0x040033E6 RID: 13286
	public GameObject sliderParent;

	// Token: 0x040033E7 RID: 13287
	public PriorityScreen priorityScreenPrefab;

	// Token: 0x040033E8 RID: 13288
	public GameObject priorityScreenParent;

	// Token: 0x040033E9 RID: 13289
	private List<MinMaxSlider> sliders = new List<MinMaxSlider>();

	// Token: 0x040033EA RID: 13290
	private List<UserMenu.SliderInfo> slidersInfos = new List<UserMenu.SliderInfo>();

	// Token: 0x040033EB RID: 13291
	private List<KIconButtonMenu.ButtonInfo> buttonInfos = new List<KIconButtonMenu.ButtonInfo>();

	// Token: 0x040033EC RID: 13292
	private PriorityScreen priorityScreen;
}
