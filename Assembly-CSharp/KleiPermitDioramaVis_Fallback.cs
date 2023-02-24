using System;
using Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AE2 RID: 2786
public class KleiPermitDioramaVis_Fallback : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005544 RID: 21828 RVA: 0x001EDA85 File Offset: 0x001EBC85
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005545 RID: 21829 RVA: 0x001EDA8D File Offset: 0x001EBC8D
	public void ConfigureSetup()
	{
	}

	// Token: 0x06005546 RID: 21830 RVA: 0x001EDA8F File Offset: 0x001EBC8F
	public void ConfigureWith(PermitResource permit)
	{
		this.sprite.sprite = PermitPresentationInfo.GetUnknownSprite();
		this.editorOnlyErrorMessageParent.gameObject.SetActive(false);
	}

	// Token: 0x06005547 RID: 21831 RVA: 0x001EDAB2 File Offset: 0x001EBCB2
	public KleiPermitDioramaVis_Fallback WithError(string error)
	{
		this.error = error;
		global::Debug.Log("[KleiInventoryScreen Error] Had to use fallback vis. " + error);
		return this;
	}

	// Token: 0x040039EB RID: 14827
	[SerializeField]
	private Image sprite;

	// Token: 0x040039EC RID: 14828
	[SerializeField]
	private RectTransform editorOnlyErrorMessageParent;

	// Token: 0x040039ED RID: 14829
	[SerializeField]
	private TextMeshProUGUI editorOnlyErrorMessageText;

	// Token: 0x040039EE RID: 14830
	private Option<string> error;
}
