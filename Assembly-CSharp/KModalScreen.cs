using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AC9 RID: 2761
public class KModalScreen : KScreen
{
	// Token: 0x06005497 RID: 21655 RVA: 0x001EB3F4 File Offset: 0x001E95F4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.backgroundRectTransform = KModalScreen.MakeScreenModal(this);
	}

	// Token: 0x06005498 RID: 21656 RVA: 0x001EB408 File Offset: 0x001E9608
	public static RectTransform MakeScreenModal(KScreen screen)
	{
		screen.ConsumeMouseScroll = true;
		screen.activateOnSpawn = true;
		GameObject gameObject = new GameObject("background");
		gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
		gameObject.AddComponent<CanvasRenderer>();
		Image image = gameObject.AddComponent<Image>();
		image.color = new Color32(0, 0, 0, 160);
		image.raycastTarget = true;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(screen.transform);
		KModalScreen.ResizeBackground(component);
		return component;
	}

	// Token: 0x06005499 RID: 21657 RVA: 0x001EB47C File Offset: 0x001E967C
	public static void ResizeBackground(RectTransform rectTransform)
	{
		rectTransform.SetAsFirstSibling();
		rectTransform.SetLocalPosition(Vector3.zero);
		rectTransform.localScale = Vector3.one;
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
	}

	// Token: 0x0600549A RID: 21658 RVA: 0x001EB4E8 File Offset: 0x001E96E8
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (CameraController.Instance != null)
		{
			CameraController.Instance.DisableUserCameraControl = true;
		}
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
		}
	}

	// Token: 0x0600549B RID: 21659 RVA: 0x001EB548 File Offset: 0x001E9748
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (CameraController.Instance != null)
		{
			CameraController.Instance.DisableUserCameraControl = false;
		}
		base.Trigger(476357528, null);
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (System.Action)Delegate.Remove(instance.OnResize, new System.Action(this.OnResize));
		}
	}

	// Token: 0x0600549C RID: 21660 RVA: 0x001EB5B2 File Offset: 0x001E97B2
	private void OnResize()
	{
		KModalScreen.ResizeBackground(this.backgroundRectTransform);
	}

	// Token: 0x0600549D RID: 21661 RVA: 0x001EB5BF File Offset: 0x001E97BF
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600549E RID: 21662 RVA: 0x001EB5C2 File Offset: 0x001E97C2
	public override float GetSortKey()
	{
		return 100f;
	}

	// Token: 0x0600549F RID: 21663 RVA: 0x001EB5C9 File Offset: 0x001E97C9
	protected override void OnActivate()
	{
		this.OnShow(true);
	}

	// Token: 0x060054A0 RID: 21664 RVA: 0x001EB5D2 File Offset: 0x001E97D2
	protected override void OnDeactivate()
	{
		this.OnShow(false);
	}

	// Token: 0x060054A1 RID: 21665 RVA: 0x001EB5DC File Offset: 0x001E97DC
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (this.pause && SpeedControlScreen.Instance != null)
		{
			if (show && !this.shown)
			{
				SpeedControlScreen.Instance.Pause(false, false);
			}
			else if (!show && this.shown)
			{
				SpeedControlScreen.Instance.Unpause(false);
			}
			this.shown = show;
		}
	}

	// Token: 0x060054A2 RID: 21666 RVA: 0x001EB63C File Offset: 0x001E983C
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (Game.Instance != null && (e.TryConsume(global::Action.TogglePause) || e.TryConsume(global::Action.CycleSpeed)))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		}
		if (!e.Consumed && (e.TryConsume(global::Action.Escape) || (e.TryConsume(global::Action.MouseRight) && this.canBackoutWithRightClick)))
		{
			this.Deactivate();
		}
		base.OnKeyDown(e);
		e.Consumed = true;
	}

	// Token: 0x060054A3 RID: 21667 RVA: 0x001EB6B9 File Offset: 0x001E98B9
	public override void OnKeyUp(KButtonEvent e)
	{
		base.OnKeyUp(e);
		e.Consumed = true;
	}

	// Token: 0x04003987 RID: 14727
	private bool shown;

	// Token: 0x04003988 RID: 14728
	public bool pause = true;

	// Token: 0x04003989 RID: 14729
	[Tooltip("Only used for main menu")]
	public bool canBackoutWithRightClick;

	// Token: 0x0400398A RID: 14730
	private RectTransform backgroundRectTransform;
}
