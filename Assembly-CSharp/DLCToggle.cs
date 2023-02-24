using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A7F RID: 2687
public class DLCToggle : KMonoBehaviour
{
	// Token: 0x06005232 RID: 21042 RVA: 0x001DACF4 File Offset: 0x001D8EF4
	protected override void OnPrefabInit()
	{
		this.expansion1Active = DlcManager.IsExpansion1Active();
		this.button.onClick += this.ToggleExpansion1Cicked;
		this.label.text = (this.expansion1Active ? UI.FRONTEND.MAINMENU.DLC.DEACTIVATE_EXPANSION1 : UI.FRONTEND.MAINMENU.DLC.ACTIVATE_EXPANSION1);
		this.logo.sprite = (this.expansion1Active ? GlobalResources.Instance().baseGameLogoSmall : GlobalResources.Instance().expansion1LogoSmall);
		this.logo.gameObject.SetActive(!this.expansion1Active);
	}

	// Token: 0x06005233 RID: 21043 RVA: 0x001DAD8C File Offset: 0x001D8F8C
	private void ToggleExpansion1Cicked()
	{
		Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.GetComponentInParent<Canvas>().gameObject, true).AddDefaultCancel().SetHeader(this.expansion1Active ? UI.FRONTEND.MAINMENU.DLC.DEACTIVATE_EXPANSION1 : UI.FRONTEND.MAINMENU.DLC.ACTIVATE_EXPANSION1)
			.AddSprite(this.expansion1Active ? GlobalResources.Instance().baseGameLogoSmall : GlobalResources.Instance().expansion1LogoSmall)
			.AddPlainText(this.expansion1Active ? UI.FRONTEND.MAINMENU.DLC.DEACTIVATE_EXPANSION1_DESC : UI.FRONTEND.MAINMENU.DLC.ACTIVATE_EXPANSION1_DESC)
			.AddOption(UI.CONFIRMDIALOG.OK, delegate(InfoDialogScreen screen)
			{
				DlcManager.ToggleDLC("EXPANSION1_ID");
			}, true);
	}

	// Token: 0x0400376E RID: 14190
	[SerializeField]
	private KButton button;

	// Token: 0x0400376F RID: 14191
	[SerializeField]
	private LocText label;

	// Token: 0x04003770 RID: 14192
	[SerializeField]
	private Image logo;

	// Token: 0x04003771 RID: 14193
	private bool expansion1Active;
}
