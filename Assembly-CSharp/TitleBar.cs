using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C0F RID: 3087
[AddComponentMenu("KMonoBehaviour/scripts/TitleBar")]
public class TitleBar : KMonoBehaviour
{
	// Token: 0x060061D5 RID: 25045 RVA: 0x0024295C File Offset: 0x00240B5C
	public void SetTitle(string Name)
	{
		this.titleText.text = Name;
	}

	// Token: 0x060061D6 RID: 25046 RVA: 0x0024296A File Offset: 0x00240B6A
	public void SetSubText(string subtext, string tooltip = "")
	{
		this.subtextText.text = subtext;
		this.subtextText.GetComponent<ToolTip>().toolTip = tooltip;
	}

	// Token: 0x060061D7 RID: 25047 RVA: 0x00242989 File Offset: 0x00240B89
	public void SetWarningActve(bool state)
	{
		this.WarningNotification.SetActive(state);
	}

	// Token: 0x060061D8 RID: 25048 RVA: 0x00242997 File Offset: 0x00240B97
	public void SetWarning(Sprite icon, string label)
	{
		this.SetWarningActve(true);
		this.NotificationIcon.sprite = icon;
		this.NotificationText.text = label;
	}

	// Token: 0x060061D9 RID: 25049 RVA: 0x002429B8 File Offset: 0x00240BB8
	public void SetPortrait(GameObject target)
	{
		this.portrait.SetPortrait(target);
	}

	// Token: 0x040043A3 RID: 17315
	public LocText titleText;

	// Token: 0x040043A4 RID: 17316
	public LocText subtextText;

	// Token: 0x040043A5 RID: 17317
	public GameObject WarningNotification;

	// Token: 0x040043A6 RID: 17318
	public Text NotificationText;

	// Token: 0x040043A7 RID: 17319
	public Image NotificationIcon;

	// Token: 0x040043A8 RID: 17320
	public Sprite techIcon;

	// Token: 0x040043A9 RID: 17321
	public Sprite materialIcon;

	// Token: 0x040043AA RID: 17322
	public TitleBarPortrait portrait;

	// Token: 0x040043AB RID: 17323
	public bool userEditable;

	// Token: 0x040043AC RID: 17324
	public bool setCameraControllerState = true;
}
