using System;
using UnityEngine;

// Token: 0x020007D2 RID: 2002
public class PlaceTool : DragTool
{
	// Token: 0x06003976 RID: 14710 RVA: 0x0013E1E9 File Offset: 0x0013C3E9
	public static void DestroyInstance()
	{
		PlaceTool.Instance = null;
	}

	// Token: 0x06003977 RID: 14711 RVA: 0x0013E1F1 File Offset: 0x0013C3F1
	protected override void OnPrefabInit()
	{
		PlaceTool.Instance = this;
		this.tooltip = base.GetComponent<ToolTip>();
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x0013E208 File Offset: 0x0013C408
	protected override void OnActivateTool()
	{
		this.active = true;
		base.OnActivateTool();
		this.visualizer = new GameObject("PlaceToolVisualizer");
		this.visualizer.SetActive(false);
		this.visualizer.SetLayerRecursively(LayerMask.NameToLayer("Place"));
		KBatchedAnimController kbatchedAnimController = this.visualizer.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.Always;
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.SetLayer(LayerMask.NameToLayer("Place"));
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(this.source.kAnimName) };
		kbatchedAnimController.initialAnim = this.source.animName;
		this.visualizer.SetActive(true);
		this.ShowToolTip();
		base.GetComponent<PlaceToolHoverTextCard>().currentPlaceable = this.source;
		ResourceRemainingDisplayScreen.instance.ActivateDisplay(this.visualizer);
		GridCompositor.Instance.ToggleMajor(true);
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x0013E2F0 File Offset: 0x0013C4F0
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		this.active = false;
		GridCompositor.Instance.ToggleMajor(false);
		this.HideToolTip();
		ResourceRemainingDisplayScreen.instance.DeactivateDisplay();
		UnityEngine.Object.Destroy(this.visualizer);
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(this.GetDeactivateSound(), false));
		this.source = null;
		this.onPlacedCallback = null;
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x0600397A RID: 14714 RVA: 0x0013E350 File Offset: 0x0013C550
	public void Activate(Placeable source, Action<Placeable, int> onPlacedCallback)
	{
		this.source = source;
		this.onPlacedCallback = onPlacedCallback;
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600397B RID: 14715 RVA: 0x0013E36C File Offset: 0x0013C56C
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (this.visualizer == null)
		{
			return;
		}
		bool flag = false;
		string text;
		if (this.source.IsValidPlaceLocation(cell, out text))
		{
			this.onPlacedCallback(this.source, cell);
			flag = true;
		}
		if (flag)
		{
			base.DeactivateTool(null);
		}
	}

	// Token: 0x0600397C RID: 14716 RVA: 0x0013E3B8 File Offset: 0x0013C5B8
	protected override DragTool.Mode GetMode()
	{
		return DragTool.Mode.Brush;
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x0013E3BB File Offset: 0x0013C5BB
	private void ShowToolTip()
	{
		ToolTipScreen.Instance.SetToolTip(this.tooltip);
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x0013E3CD File Offset: 0x0013C5CD
	private void HideToolTip()
	{
		ToolTipScreen.Instance.ClearToolTip(this.tooltip);
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x0013E3E0 File Offset: 0x0013C5E0
	public override void OnMouseMove(Vector3 cursorPos)
	{
		cursorPos = base.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		int num = Grid.PosToCell(cursorPos);
		KBatchedAnimController component = this.visualizer.GetComponent<KBatchedAnimController>();
		string text;
		if (this.source.IsValidPlaceLocation(num, out text))
		{
			component.TintColour = Color.white;
		}
		else
		{
			component.TintColour = Color.red;
		}
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x06003980 RID: 14720 RVA: 0x0013E44C File Offset: 0x0013C64C
	public void Update()
	{
		if (this.active)
		{
			KBatchedAnimController component = this.visualizer.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				component.SetLayer(LayerMask.NameToLayer("Place"));
			}
		}
	}

	// Token: 0x06003981 RID: 14721 RVA: 0x0013E486 File Offset: 0x0013C686
	public override string GetDeactivateSound()
	{
		return "HUD_Click_Deselect";
	}

	// Token: 0x040025FA RID: 9722
	[SerializeField]
	private TextStyleSetting tooltipStyle;

	// Token: 0x040025FB RID: 9723
	private Action<Placeable, int> onPlacedCallback;

	// Token: 0x040025FC RID: 9724
	private Placeable source;

	// Token: 0x040025FD RID: 9725
	private ToolTip tooltip;

	// Token: 0x040025FE RID: 9726
	public static PlaceTool Instance;

	// Token: 0x040025FF RID: 9727
	private bool active;
}
