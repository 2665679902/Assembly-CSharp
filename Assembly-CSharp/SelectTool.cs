using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x020007E1 RID: 2017
public class SelectTool : InterfaceTool
{
	// Token: 0x06003A07 RID: 14855 RVA: 0x00140D2F File Offset: 0x0013EF2F
	public static void DestroyInstance()
	{
		SelectTool.Instance = null;
	}

	// Token: 0x06003A08 RID: 14856 RVA: 0x00140D38 File Offset: 0x0013EF38
	protected override void OnPrefabInit()
	{
		this.defaultLayerMask = 1 | LayerMask.GetMask(new string[] { "World", "Pickupable", "Place", "PlaceWithDepth", "BlockSelection", "Construction", "Selection" });
		this.layerMask = this.defaultLayerMask;
		this.selectMarker = global::Util.KInstantiateUI<SelectMarker>(EntityPrefabs.Instance.SelectMarker, GameScreenManager.Instance.worldSpaceCanvas, false);
		this.selectMarker.gameObject.SetActive(false);
		this.populateHitsList = true;
		SelectTool.Instance = this;
	}

	// Token: 0x06003A09 RID: 14857 RVA: 0x00140DDA File Offset: 0x0013EFDA
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
		ToolMenu.Instance.PriorityScreen.ResetPriority();
		this.Select(null, false);
	}

	// Token: 0x06003A0A RID: 14858 RVA: 0x00140DFE File Offset: 0x0013EFFE
	public void SetLayerMask(int mask)
	{
		this.layerMask = mask;
		base.ClearHover();
		this.LateUpdate();
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x00140E13 File Offset: 0x0013F013
	public void ClearLayerMask()
	{
		this.layerMask = this.defaultLayerMask;
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x00140E21 File Offset: 0x0013F021
	public int GetDefaultLayerMask()
	{
		return this.defaultLayerMask;
	}

	// Token: 0x06003A0D RID: 14861 RVA: 0x00140E29 File Offset: 0x0013F029
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		base.ClearHover();
		this.Select(null, false);
	}

	// Token: 0x06003A0E RID: 14862 RVA: 0x00140E40 File Offset: 0x0013F040
	public void Focus(Vector3 pos, KSelectable selectable, Vector3 offset)
	{
		if (selectable != null)
		{
			pos = selectable.transform.GetPosition();
		}
		pos.z = -40f;
		pos += offset;
		WorldContainer worldFromPosition = ClusterManager.Instance.GetWorldFromPosition(pos);
		if (worldFromPosition != null)
		{
			CameraController.Instance.ActiveWorldStarWipe(worldFromPosition.id, pos, 10f, null);
			return;
		}
		DebugUtil.DevLogError("DevError: specified camera focus position has null world - possible out of bounds location");
	}

	// Token: 0x06003A0F RID: 14863 RVA: 0x00140EAF File Offset: 0x0013F0AF
	public void SelectAndFocus(Vector3 pos, KSelectable selectable, Vector3 offset)
	{
		this.Focus(pos, selectable, offset);
		this.Select(selectable, false);
	}

	// Token: 0x06003A10 RID: 14864 RVA: 0x00140EC2 File Offset: 0x0013F0C2
	public void SelectAndFocus(Vector3 pos, KSelectable selectable)
	{
		this.SelectAndFocus(pos, selectable, Vector3.zero);
	}

	// Token: 0x06003A11 RID: 14865 RVA: 0x00140ED1 File Offset: 0x0013F0D1
	public void SelectNextFrame(KSelectable new_selected, bool skipSound = false)
	{
		this.delayedNextSelection = new_selected;
		this.delayedSkipSound = skipSound;
		UIScheduler.Instance.ScheduleNextFrame("DelayedSelect", new Action<object>(this.DoSelectNextFrame), null, null);
	}

	// Token: 0x06003A12 RID: 14866 RVA: 0x00140EFF File Offset: 0x0013F0FF
	private void DoSelectNextFrame(object data)
	{
		this.Select(this.delayedNextSelection, this.delayedSkipSound);
		this.delayedNextSelection = null;
	}

	// Token: 0x06003A13 RID: 14867 RVA: 0x00140F1C File Offset: 0x0013F11C
	public void Select(KSelectable new_selected, bool skipSound = false)
	{
		if (new_selected == this.previousSelection)
		{
			return;
		}
		this.previousSelection = new_selected;
		if (this.selected != null)
		{
			this.selected.Unselect();
		}
		GameObject gameObject = null;
		if (new_selected != null && new_selected.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
		{
			SelectToolHoverTextCard component = base.GetComponent<SelectToolHoverTextCard>();
			if (component != null)
			{
				int num = component.currentSelectedSelectableIndex;
				int recentNumberOfDisplayedSelectables = component.recentNumberOfDisplayedSelectables;
				if (recentNumberOfDisplayedSelectables != 0)
				{
					num = (num + 1) % recentNumberOfDisplayedSelectables;
					if (!skipSound)
					{
						if (recentNumberOfDisplayedSelectables == 1)
						{
							KFMOD.PlayUISound(GlobalAssets.GetSound("Select_empty", false));
						}
						else
						{
							EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("Select_full", false), Vector3.zero, 1f);
							eventInstance.setParameterByName("selection", (float)num, false);
							SoundEvent.EndOneShot(eventInstance);
						}
						this.playedSoundThisFrame = true;
					}
				}
			}
			if (new_selected == this.hover)
			{
				base.ClearHover();
			}
			new_selected.Select();
			gameObject = new_selected.gameObject;
			this.selectMarker.SetTargetTransform(gameObject.transform);
			this.selectMarker.gameObject.SetActive(!new_selected.DisableSelectMarker);
		}
		else if (this.selectMarker != null)
		{
			this.selectMarker.gameObject.SetActive(false);
		}
		this.selected = ((gameObject == null) ? null : new_selected);
		Game.Instance.Trigger(-1503271301, gameObject);
	}

	// Token: 0x06003A14 RID: 14868 RVA: 0x00141088 File Offset: 0x0013F288
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		KSelectable objectUnderCursor = base.GetObjectUnderCursor<KSelectable>(true, (KSelectable s) => s.GetComponent<KSelectable>().IsSelectable, this.selected);
		this.selectedCell = Grid.PosToCell(cursor_pos);
		this.Select(objectUnderCursor, false);
		if (DevToolSimDebug.Instance != null)
		{
			DevToolSimDebug.Instance.SetCell(this.selectedCell);
		}
		if (DevToolNavGrid.Instance != null)
		{
			DevToolNavGrid.Instance.SetCell(this.selectedCell);
		}
	}

	// Token: 0x06003A15 RID: 14869 RVA: 0x00141104 File Offset: 0x0013F304
	public int GetSelectedCell()
	{
		return this.selectedCell;
	}

	// Token: 0x04002624 RID: 9764
	public KSelectable selected;

	// Token: 0x04002625 RID: 9765
	protected int cell_new;

	// Token: 0x04002626 RID: 9766
	private int selectedCell;

	// Token: 0x04002627 RID: 9767
	protected int defaultLayerMask;

	// Token: 0x04002628 RID: 9768
	public static SelectTool Instance;

	// Token: 0x04002629 RID: 9769
	private KSelectable delayedNextSelection;

	// Token: 0x0400262A RID: 9770
	private bool delayedSkipSound;

	// Token: 0x0400262B RID: 9771
	private KSelectable previousSelection;
}
