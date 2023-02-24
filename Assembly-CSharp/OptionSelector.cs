using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C31 RID: 3121
public class OptionSelector : MonoBehaviour
{
	// Token: 0x060062BA RID: 25274 RVA: 0x00247631 File Offset: 0x00245831
	private void Start()
	{
		this.selectedItem.GetComponent<KButton>().onBtnClick += this.OnClick;
	}

	// Token: 0x060062BB RID: 25275 RVA: 0x0024764F File Offset: 0x0024584F
	public void Initialize(object id)
	{
		this.id = id;
	}

	// Token: 0x060062BC RID: 25276 RVA: 0x00247658 File Offset: 0x00245858
	private void OnClick(KKeyCode button)
	{
		if (button == KKeyCode.Mouse0)
		{
			this.OnChangePriority(this.id, 1);
			return;
		}
		if (button != KKeyCode.Mouse1)
		{
			return;
		}
		this.OnChangePriority(this.id, -1);
	}

	// Token: 0x060062BD RID: 25277 RVA: 0x00247690 File Offset: 0x00245890
	public void ConfigureItem(bool disabled, OptionSelector.DisplayOptionInfo display_info)
	{
		HierarchyReferences component = this.selectedItem.GetComponent<HierarchyReferences>();
		KImage kimage = component.GetReference("BG") as KImage;
		if (display_info.bgOptions == null)
		{
			kimage.gameObject.SetActive(false);
		}
		else
		{
			kimage.sprite = display_info.bgOptions[display_info.bgIndex];
		}
		KImage kimage2 = component.GetReference("FG") as KImage;
		if (display_info.fgOptions == null)
		{
			kimage2.gameObject.SetActive(false);
		}
		else
		{
			kimage2.sprite = display_info.fgOptions[display_info.fgIndex];
		}
		KImage kimage3 = component.GetReference("Fill") as KImage;
		if (kimage3 != null)
		{
			kimage3.enabled = !disabled;
			kimage3.color = display_info.fillColour;
		}
		KImage kimage4 = component.GetReference("Outline") as KImage;
		if (kimage4 != null)
		{
			kimage4.enabled = !disabled;
		}
	}

	// Token: 0x04004494 RID: 17556
	private object id;

	// Token: 0x04004495 RID: 17557
	public Action<object, int> OnChangePriority;

	// Token: 0x04004496 RID: 17558
	[SerializeField]
	private KImage selectedItem;

	// Token: 0x04004497 RID: 17559
	[SerializeField]
	private KImage itemTemplate;

	// Token: 0x02001ABF RID: 6847
	public class DisplayOptionInfo
	{
		// Token: 0x04007899 RID: 30873
		public IList<Sprite> bgOptions;

		// Token: 0x0400789A RID: 30874
		public IList<Sprite> fgOptions;

		// Token: 0x0400789B RID: 30875
		public int bgIndex;

		// Token: 0x0400789C RID: 30876
		public int fgIndex;

		// Token: 0x0400789D RID: 30877
		public Color32 fillColour;
	}
}
