using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000068 RID: 104
[AddComponentMenu("KMonoBehaviour/Plugins/KTabMenuHeader")]
public class KTabMenuHeader : KMonoBehaviour
{
	// Token: 0x06000443 RID: 1091 RVA: 0x000158C6 File Offset: 0x00013AC6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ActivateTabArtwork(0);
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x000158D8 File Offset: 0x00013AD8
	public void Add(string name, KTabMenuHeader.OnClick onClick, int id)
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab.gameObject, null, false);
		gameObject.SetActive(true);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.transform.SetParent(base.transform, false);
		component.name = name;
		Text componentInChildren = component.GetComponentInChildren<Text>();
		if (componentInChildren != null)
		{
			componentInChildren.text = name.ToUpper();
		}
		this.ActivateTabArtwork(id);
		gameObject.GetComponent<KButton>().onClick += delegate
		{
			onClick(id);
		};
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00015970 File Offset: 0x00013B70
	public void Add(Sprite icon, string name, KTabMenuHeader.OnClick onClick, int id, string tooltip = "")
	{
		GameObject gameObject = Util.KInstantiateUI(this.prefab.gameObject, null, false);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.transform.SetParent(base.transform, false);
		component.name = name;
		if (tooltip == "")
		{
			component.GetComponent<ToolTip>().toolTip = name;
		}
		else
		{
			component.GetComponent<ToolTip>().toolTip = tooltip;
		}
		this.ActivateTabArtwork(id);
		TabHeaderIcon componentInChildren = component.GetComponentInChildren<TabHeaderIcon>();
		if (componentInChildren)
		{
			componentInChildren.TitleText.text = name;
		}
		KToggle component2 = gameObject.GetComponent<KToggle>();
		if (component2 && component2.fgImage)
		{
			component2.fgImage.sprite = icon;
		}
		component2.group = base.GetComponent<ToggleGroup>();
		component2.onClick += delegate
		{
			onClick(id);
		};
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00015A5C File Offset: 0x00013C5C
	public void Activate(int itemIdx, int previouslyActiveTabIdx)
	{
		int childCount = base.transform.childCount;
		if (itemIdx >= childCount)
		{
			return;
		}
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child.gameObject.activeSelf)
			{
				KButton componentInChildren = child.GetComponentInChildren<KButton>();
				if (componentInChildren != null && componentInChildren.GetComponentInChildren<Text>() != null && i == itemIdx)
				{
					this.ActivateTabArtwork(itemIdx);
				}
				KToggle component = child.GetComponent<KToggle>();
				if (component != null)
				{
					this.ActivateTabArtwork(itemIdx);
					if (i == itemIdx)
					{
						component.Select();
					}
					else
					{
						component.Deselect();
					}
				}
			}
		}
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00015AF6 File Offset: 0x00013CF6
	public void SetTabEnabled(int tabIdx, bool enabled)
	{
		if (tabIdx < base.transform.childCount)
		{
			base.transform.GetChild(tabIdx).gameObject.SetActive(enabled);
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00015B20 File Offset: 0x00013D20
	public virtual void ActivateTabArtwork(int tabIdx)
	{
		if (tabIdx >= base.transform.childCount)
		{
			return;
		}
		for (int i = 0; i < base.transform.childCount; i++)
		{
			ImageToggleState component = base.transform.GetChild(i).GetComponent<ImageToggleState>();
			if (component != null)
			{
				if (i == tabIdx)
				{
					component.SetActive();
				}
				else
				{
					component.SetInactive();
				}
			}
			Canvas componentInChildren = base.transform.GetChild(i).GetComponentInChildren<Canvas>(true);
			if (componentInChildren != null)
			{
				componentInChildren.overrideSorting = tabIdx == i;
			}
			SetTextStyleSetting componentInChildren2 = base.transform.GetChild(i).GetComponentInChildren<SetTextStyleSetting>();
			if (componentInChildren2 != null && this.TextStyle_Active != null && this.TextStyle_Inactive != null)
			{
				if (i == tabIdx)
				{
					componentInChildren2.SetStyle(this.TextStyle_Active);
				}
				else
				{
					componentInChildren2.SetStyle(this.TextStyle_Inactive);
				}
			}
		}
	}

	// Token: 0x040004A6 RID: 1190
	[SerializeField]
	private RectTransform prefab;

	// Token: 0x040004A7 RID: 1191
	public TextStyleSetting TextStyle_Active;

	// Token: 0x040004A8 RID: 1192
	public TextStyleSetting TextStyle_Inactive;

	// Token: 0x020009BA RID: 2490
	// (Invoke) Token: 0x06005360 RID: 21344
	public delegate void OnClick(int id);
}
