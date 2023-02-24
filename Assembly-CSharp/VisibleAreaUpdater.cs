using System;

// Token: 0x020009CB RID: 2507
public class VisibleAreaUpdater
{
	// Token: 0x06004A8B RID: 19083 RVA: 0x001A1972 File Offset: 0x0019FB72
	public VisibleAreaUpdater(Action<int> outside_view_first_time_cb, Action<int> inside_view_first_time_cb, string name)
	{
		this.OutsideViewFirstTimeCallback = outside_view_first_time_cb;
		this.InsideViewFirstTimeCallback = inside_view_first_time_cb;
		this.UpdateCallback = new Action<int>(this.InternalUpdateCell);
		this.Name = name;
	}

	// Token: 0x06004A8C RID: 19084 RVA: 0x001A19A1 File Offset: 0x0019FBA1
	public void Update()
	{
		if (CameraController.Instance != null && this.VisibleArea == null)
		{
			this.VisibleArea = CameraController.Instance.VisibleArea;
			this.VisibleArea.Run(this.InsideViewFirstTimeCallback);
		}
	}

	// Token: 0x06004A8D RID: 19085 RVA: 0x001A19D9 File Offset: 0x0019FBD9
	private void InternalUpdateCell(int cell)
	{
		this.OutsideViewFirstTimeCallback(cell);
		this.InsideViewFirstTimeCallback(cell);
	}

	// Token: 0x06004A8E RID: 19086 RVA: 0x001A19F3 File Offset: 0x0019FBF3
	public void UpdateCell(int cell)
	{
		if (this.VisibleArea != null)
		{
			this.VisibleArea.RunIfVisible(cell, this.UpdateCallback);
		}
	}

	// Token: 0x040030F2 RID: 12530
	private GridVisibleArea VisibleArea;

	// Token: 0x040030F3 RID: 12531
	private Action<int> OutsideViewFirstTimeCallback;

	// Token: 0x040030F4 RID: 12532
	private Action<int> InsideViewFirstTimeCallback;

	// Token: 0x040030F5 RID: 12533
	private Action<int> UpdateCallback;

	// Token: 0x040030F6 RID: 12534
	private string Name;
}
