using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005A RID: 90
public class KImage : Image
{
	// Token: 0x17000084 RID: 132
	// (set) Token: 0x06000396 RID: 918 RVA: 0x00012DFA File Offset: 0x00010FFA
	public KImage.ColorSelector ColorState
	{
		set
		{
			this.colorSelector = value;
			this.ApplyColorStyleSetting();
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00012E09 File Offset: 0x00011009
	protected override void Awake()
	{
		base.Awake();
		this.ColorState = this.defaultState;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00012E1D File Offset: 0x0001101D
	protected override void OnEnable()
	{
		base.OnEnable();
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00012E25 File Offset: 0x00011025
	protected override void OnDisable()
	{
		base.OnDisable();
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00012E2D File Offset: 0x0001102D
	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00012E38 File Offset: 0x00011038
	[ContextMenu("Apply Color Style Settings")]
	public void ApplyColorStyleSetting()
	{
		if (this.colorStyleSetting != null)
		{
			switch (this.colorSelector)
			{
			case KImage.ColorSelector.Active:
				this.color = this.colorStyleSetting.activeColor;
				return;
			case KImage.ColorSelector.Inactive:
				this.color = this.colorStyleSetting.inactiveColor;
				return;
			case KImage.ColorSelector.Disabled:
				this.color = this.colorStyleSetting.disabledColor;
				return;
			case KImage.ColorSelector.Hover:
				this.color = this.colorStyleSetting.hoverColor;
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x04000430 RID: 1072
	public KImage.ColorSelector defaultState = KImage.ColorSelector.Inactive;

	// Token: 0x04000431 RID: 1073
	private KImage.ColorSelector colorSelector = KImage.ColorSelector.Inactive;

	// Token: 0x04000432 RID: 1074
	public ColorStyleSetting colorStyleSetting;

	// Token: 0x04000433 RID: 1075
	public bool clearMaskOnDisable = true;

	// Token: 0x020009AD RID: 2477
	public enum ColorSelector
	{
		// Token: 0x04002196 RID: 8598
		Active,
		// Token: 0x04002197 RID: 8599
		Inactive,
		// Token: 0x04002198 RID: 8600
		Disabled,
		// Token: 0x04002199 RID: 8601
		Hover
	}
}
