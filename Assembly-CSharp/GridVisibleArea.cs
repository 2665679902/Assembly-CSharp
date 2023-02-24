using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007A1 RID: 1953
public class GridVisibleArea
{
	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06003714 RID: 14100 RVA: 0x00132C78 File Offset: 0x00130E78
	public GridArea CurrentArea
	{
		get
		{
			return this.Areas[0];
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06003715 RID: 14101 RVA: 0x00132C86 File Offset: 0x00130E86
	public GridArea PreviousArea
	{
		get
		{
			return this.Areas[1];
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06003716 RID: 14102 RVA: 0x00132C94 File Offset: 0x00130E94
	public GridArea PreviousPreviousArea
	{
		get
		{
			return this.Areas[2];
		}
	}

	// Token: 0x06003717 RID: 14103 RVA: 0x00132CA4 File Offset: 0x00130EA4
	public void Update()
	{
		this.Areas[2] = this.Areas[1];
		this.Areas[1] = this.Areas[0];
		this.Areas[0] = GridVisibleArea.GetVisibleArea();
		foreach (GridVisibleArea.Callback callback in this.Callbacks)
		{
			callback.OnUpdate();
		}
	}

	// Token: 0x06003718 RID: 14104 RVA: 0x00132D3C File Offset: 0x00130F3C
	public void AddCallback(string name, System.Action on_update)
	{
		GridVisibleArea.Callback callback = new GridVisibleArea.Callback
		{
			Name = name,
			OnUpdate = on_update
		};
		this.Callbacks.Add(callback);
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x00132D70 File Offset: 0x00130F70
	public void Run(Action<int> in_view)
	{
		if (in_view != null)
		{
			this.CurrentArea.Run(in_view);
		}
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x00132D90 File Offset: 0x00130F90
	public void Run(Action<int> outside_view, Action<int> inside_view, Action<int> inside_view_second_time)
	{
		if (outside_view != null)
		{
			this.PreviousArea.RunOnDifference(this.CurrentArea, outside_view);
		}
		if (inside_view != null)
		{
			this.CurrentArea.RunOnDifference(this.PreviousArea, inside_view);
		}
		if (inside_view_second_time != null)
		{
			this.PreviousArea.RunOnDifference(this.PreviousPreviousArea, inside_view_second_time);
		}
	}

	// Token: 0x0600371B RID: 14107 RVA: 0x00132DE8 File Offset: 0x00130FE8
	public void RunIfVisible(int cell, Action<int> action)
	{
		this.CurrentArea.RunIfInside(cell, action);
	}

	// Token: 0x0600371C RID: 14108 RVA: 0x00132E08 File Offset: 0x00131008
	public static GridArea GetVisibleArea()
	{
		GridArea gridArea = default(GridArea);
		Camera mainCamera = Game.MainCamera;
		if (mainCamera != null)
		{
			Vector3 vector = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f, mainCamera.transform.GetPosition().z));
			Vector3 vector2 = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, mainCamera.transform.GetPosition().z));
			if (CameraController.Instance != null)
			{
				Vector2I vector2I;
				Vector2I vector2I2;
				CameraController.Instance.GetWorldCamera(out vector2I, out vector2I2);
				gridArea.SetExtents(Math.Max((int)(vector2.x - 0.5f), vector2I.x), Math.Max((int)(vector2.y - 0.5f), vector2I.y), Math.Min((int)(vector.x + 1.5f), vector2I2.x + vector2I.x), Math.Min((int)(vector.y + 1.5f), vector2I2.y + vector2I.y));
			}
			else
			{
				gridArea.SetExtents(Math.Max((int)(vector2.x - 0.5f), 0), Math.Max((int)(vector2.y - 0.5f), 0), Math.Min((int)(vector.x + 1.5f), Grid.WidthInCells), Math.Min((int)(vector.y + 1.5f), Grid.HeightInCells));
			}
		}
		return gridArea;
	}

	// Token: 0x040024F6 RID: 9462
	private GridArea[] Areas = new GridArea[3];

	// Token: 0x040024F7 RID: 9463
	private List<GridVisibleArea.Callback> Callbacks = new List<GridVisibleArea.Callback>();

	// Token: 0x02001503 RID: 5379
	public struct Callback
	{
		// Token: 0x04006550 RID: 25936
		public System.Action OnUpdate;

		// Token: 0x04006551 RID: 25937
		public string Name;
	}
}
