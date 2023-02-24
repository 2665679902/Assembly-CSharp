using System;

// Token: 0x02000B98 RID: 2968
[Serializable]
public struct ButtonMenuTextOverride
{
	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x06005D59 RID: 23897 RVA: 0x00221B75 File Offset: 0x0021FD75
	public bool IsValid
	{
		get
		{
			return !string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.ToolTip);
		}
	}

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x06005D5A RID: 23898 RVA: 0x00221B9E File Offset: 0x0021FD9E
	public bool HasCancelText
	{
		get
		{
			return !string.IsNullOrEmpty(this.CancelText) && !string.IsNullOrEmpty(this.CancelToolTip);
		}
	}

	// Token: 0x04003FCF RID: 16335
	public LocString Text;

	// Token: 0x04003FD0 RID: 16336
	public LocString CancelText;

	// Token: 0x04003FD1 RID: 16337
	public LocString ToolTip;

	// Token: 0x04003FD2 RID: 16338
	public LocString CancelToolTip;
}
