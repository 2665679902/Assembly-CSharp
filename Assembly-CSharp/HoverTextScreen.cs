using System;
using UnityEngine;

// Token: 0x02000A05 RID: 2565
public class HoverTextScreen : KScreen
{
	// Token: 0x06004D02 RID: 19714 RVA: 0x001B19A3 File Offset: 0x001AFBA3
	public static void DestroyInstance()
	{
		HoverTextScreen.Instance = null;
	}

	// Token: 0x06004D03 RID: 19715 RVA: 0x001B19AB File Offset: 0x001AFBAB
	protected override void OnActivate()
	{
		base.OnActivate();
		HoverTextScreen.Instance = this;
		this.drawer = new HoverTextDrawer(this.skin.skin, base.GetComponent<RectTransform>());
	}

	// Token: 0x06004D04 RID: 19716 RVA: 0x001B19D8 File Offset: 0x001AFBD8
	public HoverTextDrawer BeginDrawing()
	{
		Vector2 zero = Vector2.zero;
		Vector2 vector = KInputManager.GetMousePos();
		RectTransform rectTransform = base.transform.parent as RectTransform;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, vector, base.transform.parent.GetComponent<Canvas>().worldCamera, out zero);
		zero.x += rectTransform.sizeDelta.x / 2f;
		zero.y -= rectTransform.sizeDelta.y / 2f;
		this.drawer.BeginDrawing(zero);
		return this.drawer;
	}

	// Token: 0x06004D05 RID: 19717 RVA: 0x001B1A70 File Offset: 0x001AFC70
	private void Update()
	{
		bool flag = PlayerController.Instance.ActiveTool.ShowHoverUI();
		this.drawer.SetEnabled(flag);
	}

	// Token: 0x06004D06 RID: 19718 RVA: 0x001B1A9C File Offset: 0x001AFC9C
	public Sprite GetSprite(string byName)
	{
		foreach (Sprite sprite in this.HoverIcons)
		{
			if (sprite != null && sprite.name == byName)
			{
				return sprite;
			}
		}
		global::Debug.LogWarning("No icon named " + byName + " was found on HoverTextScreen.prefab");
		return null;
	}

	// Token: 0x06004D07 RID: 19719 RVA: 0x001B1AF1 File Offset: 0x001AFCF1
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.drawer.Cleanup();
	}

	// Token: 0x040032C8 RID: 13000
	[SerializeField]
	private HoverTextSkin skin;

	// Token: 0x040032C9 RID: 13001
	public Sprite[] HoverIcons;

	// Token: 0x040032CA RID: 13002
	public HoverTextDrawer drawer;

	// Token: 0x040032CB RID: 13003
	public static HoverTextScreen Instance;
}
