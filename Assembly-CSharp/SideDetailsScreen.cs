using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B89 RID: 2953
public class SideDetailsScreen : KScreen
{
	// Token: 0x06005CBA RID: 23738 RVA: 0x0021EA65 File Offset: 0x0021CC65
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SideDetailsScreen.Instance = this;
		this.Initialize();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005CBB RID: 23739 RVA: 0x0021EA85 File Offset: 0x0021CC85
	protected override void OnForcedCleanUp()
	{
		SideDetailsScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005CBC RID: 23740 RVA: 0x0021EA94 File Offset: 0x0021CC94
	private void Initialize()
	{
		if (this.screens == null)
		{
			return;
		}
		this.rectTransform = base.GetComponent<RectTransform>();
		this.screenMap = new Dictionary<string, SideTargetScreen>();
		List<SideTargetScreen> list = new List<SideTargetScreen>();
		foreach (SideTargetScreen sideTargetScreen in this.screens)
		{
			SideTargetScreen sideTargetScreen2 = Util.KInstantiateUI<SideTargetScreen>(sideTargetScreen.gameObject, this.body.gameObject, false);
			sideTargetScreen2.gameObject.SetActive(false);
			list.Add(sideTargetScreen2);
		}
		list.ForEach(delegate(SideTargetScreen s)
		{
			this.screenMap.Add(s.name, s);
		});
		this.backButton.onClick += delegate
		{
			this.Show(false);
		};
	}

	// Token: 0x06005CBD RID: 23741 RVA: 0x0021EB58 File Offset: 0x0021CD58
	public void SetTitle(string newTitle)
	{
		this.title.text = newTitle;
	}

	// Token: 0x06005CBE RID: 23742 RVA: 0x0021EB68 File Offset: 0x0021CD68
	public void SetScreen(string screenName, object content, float x)
	{
		if (!this.screenMap.ContainsKey(screenName))
		{
			global::Debug.LogError("Tried to open a screen that does exist on the manager!");
			return;
		}
		if (content == null)
		{
			global::Debug.LogError("Tried to set " + screenName + " with null content!");
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(true);
		}
		Rect rect = this.rectTransform.rect;
		this.rectTransform.offsetMin = new Vector2(x, this.rectTransform.offsetMin.y);
		this.rectTransform.offsetMax = new Vector2(x + rect.width, this.rectTransform.offsetMax.y);
		if (this.activeScreen != null)
		{
			this.activeScreen.gameObject.SetActive(false);
		}
		this.activeScreen = this.screenMap[screenName];
		this.activeScreen.gameObject.SetActive(true);
		this.SetTitle(this.activeScreen.displayName);
		this.activeScreen.SetTarget(content);
	}

	// Token: 0x04003F66 RID: 16230
	[SerializeField]
	private List<SideTargetScreen> screens;

	// Token: 0x04003F67 RID: 16231
	[SerializeField]
	private LocText title;

	// Token: 0x04003F68 RID: 16232
	[SerializeField]
	private KButton backButton;

	// Token: 0x04003F69 RID: 16233
	[SerializeField]
	private RectTransform body;

	// Token: 0x04003F6A RID: 16234
	private RectTransform rectTransform;

	// Token: 0x04003F6B RID: 16235
	private Dictionary<string, SideTargetScreen> screenMap;

	// Token: 0x04003F6C RID: 16236
	private SideTargetScreen activeScreen;

	// Token: 0x04003F6D RID: 16237
	public static SideDetailsScreen Instance;
}
