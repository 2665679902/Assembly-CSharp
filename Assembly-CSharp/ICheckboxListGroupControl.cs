using System;

// Token: 0x02000B9C RID: 2972
public interface ICheckboxListGroupControl
{
	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x06005D73 RID: 23923
	string Title { get; }

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x06005D74 RID: 23924
	string Description { get; }

	// Token: 0x06005D75 RID: 23925
	ICheckboxListGroupControl.ListGroup[] GetData();

	// Token: 0x06005D76 RID: 23926
	bool SidescreenEnabled();

	// Token: 0x06005D77 RID: 23927
	int CheckboxSideScreenSortOrder();

	// Token: 0x02001A50 RID: 6736
	public struct ListGroup
	{
		// Token: 0x060092BF RID: 37567 RVA: 0x003184CF File Offset: 0x003166CF
		public ListGroup(string title, ICheckboxListGroupControl.CheckboxItem[] checkboxItems, Func<string, string> resolveTitleCallback = null)
		{
			this.title = title;
			this.checkboxItems = checkboxItems;
			this.resolveTitleCallback = resolveTitleCallback;
		}

		// Token: 0x04007734 RID: 30516
		public Func<string, string> resolveTitleCallback;

		// Token: 0x04007735 RID: 30517
		public string title;

		// Token: 0x04007736 RID: 30518
		public ICheckboxListGroupControl.CheckboxItem[] checkboxItems;
	}

	// Token: 0x02001A51 RID: 6737
	public struct CheckboxItem
	{
		// Token: 0x04007737 RID: 30519
		public string text;

		// Token: 0x04007738 RID: 30520
		public string tooltip;

		// Token: 0x04007739 RID: 30521
		public bool isOn;

		// Token: 0x0400773A RID: 30522
		public Func<string, object, string> resolveTooltipCallback;
	}
}
