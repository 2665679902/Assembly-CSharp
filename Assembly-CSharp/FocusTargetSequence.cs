using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000357 RID: 855
public static class FocusTargetSequence
{
	// Token: 0x06001130 RID: 4400 RVA: 0x0005CABF File Offset: 0x0005ACBF
	public static void Start(MonoBehaviour coroutineRunner, FocusTargetSequence.Data sequenceData)
	{
		FocusTargetSequence.sequenceCoroutine = coroutineRunner.StartCoroutine(FocusTargetSequence.RunSequence(sequenceData));
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x0005CAD4 File Offset: 0x0005ACD4
	public static void Cancel(MonoBehaviour coroutineRunner)
	{
		if (FocusTargetSequence.sequenceCoroutine == null)
		{
			return;
		}
		coroutineRunner.StopCoroutine(FocusTargetSequence.sequenceCoroutine);
		FocusTargetSequence.sequenceCoroutine = null;
		if (FocusTargetSequence.prevSpeed >= 0)
		{
			SpeedControlScreen.Instance.SetSpeed(FocusTargetSequence.prevSpeed);
		}
		if (SpeedControlScreen.Instance.IsPaused && !FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		if (!SpeedControlScreen.Instance.IsPaused && FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		FocusTargetSequence.SetUIVisible(true);
		CameraController.Instance.SetWorldInteractive(true);
		SelectTool.Instance.Select(FocusTargetSequence.prevSelected, true);
		FocusTargetSequence.prevSelected = null;
		FocusTargetSequence.wasPaused = false;
		FocusTargetSequence.prevSpeed = -1;
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x0005CB81 File Offset: 0x0005AD81
	public static IEnumerator RunSequence(FocusTargetSequence.Data sequenceData)
	{
		SaveGame.Instance.GetComponent<UserNavigation>();
		CameraController.Instance.FadeOut(1f, 1f, null);
		FocusTargetSequence.prevSpeed = SpeedControlScreen.Instance.GetSpeed();
		SpeedControlScreen.Instance.SetSpeed(0);
		FocusTargetSequence.wasPaused = SpeedControlScreen.Instance.IsPaused;
		if (!FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		PlayerController.Instance.CancelDragging();
		CameraController.Instance.SetWorldInteractive(false);
		yield return CameraController.Instance.activeFadeRoutine;
		FocusTargetSequence.prevSelected = SelectTool.Instance.selected;
		SelectTool.Instance.Select(null, true);
		FocusTargetSequence.SetUIVisible(false);
		ClusterManager.Instance.SetActiveWorld(sequenceData.WorldId);
		ManagementMenu.Instance.CloseAll();
		CameraController.Instance.SnapTo(sequenceData.Target, sequenceData.OrthographicSize);
		if (sequenceData.PopupData != null)
		{
			EventInfoScreen.ShowPopup(sequenceData.PopupData);
		}
		CameraController.Instance.FadeIn(0f, 2f, null);
		if (sequenceData.TargetSize - sequenceData.OrthographicSize > Mathf.Epsilon)
		{
			CameraController.Instance.StartCoroutine(CameraController.Instance.DoCinematicZoom(sequenceData.TargetSize));
		}
		if (sequenceData.CanCompleteCB != null)
		{
			SpeedControlScreen.Instance.Unpause(false);
			while (!sequenceData.CanCompleteCB())
			{
				yield return SequenceUtil.WaitForNextFrame;
			}
			SpeedControlScreen.Instance.Pause(false, false);
		}
		CameraController.Instance.SetWorldInteractive(true);
		SpeedControlScreen.Instance.SetSpeed(FocusTargetSequence.prevSpeed);
		if (SpeedControlScreen.Instance.IsPaused && !FocusTargetSequence.wasPaused)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		if (sequenceData.CompleteCB != null)
		{
			sequenceData.CompleteCB();
		}
		FocusTargetSequence.SetUIVisible(true);
		SelectTool.Instance.Select(FocusTargetSequence.prevSelected, true);
		sequenceData.Clear();
		FocusTargetSequence.sequenceCoroutine = null;
		FocusTargetSequence.prevSpeed = -1;
		FocusTargetSequence.wasPaused = false;
		FocusTargetSequence.prevSelected = null;
		yield break;
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0005CB90 File Offset: 0x0005AD90
	private static void SetUIVisible(bool visible)
	{
		NotificationScreen.Instance.Show(visible);
		OverlayMenu.Instance.Show(visible);
		ManagementMenu.Instance.Show(visible);
		ToolMenu.Instance.Show(visible);
		ToolMenu.Instance.PriorityScreen.Show(visible);
		PinnedResourcesPanel.Instance.Show(visible);
		TopLeftControlScreen.Instance.Show(visible);
		global::DateTime.Instance.Show(visible);
		BuildWatermark.Instance.Show(visible);
		BuildWatermark.Instance.Show(visible);
		ColonyDiagnosticScreen.Instance.Show(visible);
		RootMenu.Instance.Show(visible);
		if (PlanScreen.Instance != null)
		{
			PlanScreen.Instance.Show(visible);
		}
		if (BuildMenu.Instance != null)
		{
			BuildMenu.Instance.Show(visible);
		}
		if (WorldSelector.Instance != null)
		{
			WorldSelector.Instance.Show(visible);
		}
	}

	// Token: 0x0400094D RID: 2381
	private static Coroutine sequenceCoroutine = null;

	// Token: 0x0400094E RID: 2382
	private static KSelectable prevSelected = null;

	// Token: 0x0400094F RID: 2383
	private static bool wasPaused = false;

	// Token: 0x04000950 RID: 2384
	private static int prevSpeed = -1;

	// Token: 0x02000F1A RID: 3866
	public struct Data
	{
		// Token: 0x06006DEF RID: 28143 RVA: 0x0029BA33 File Offset: 0x00299C33
		public void Clear()
		{
			this.PopupData = null;
			this.CompleteCB = null;
			this.CanCompleteCB = null;
		}

		// Token: 0x04005328 RID: 21288
		public int WorldId;

		// Token: 0x04005329 RID: 21289
		public float OrthographicSize;

		// Token: 0x0400532A RID: 21290
		public float TargetSize;

		// Token: 0x0400532B RID: 21291
		public Vector3 Target;

		// Token: 0x0400532C RID: 21292
		public EventInfoData PopupData;

		// Token: 0x0400532D RID: 21293
		public System.Action CompleteCB;

		// Token: 0x0400532E RID: 21294
		public Func<bool> CanCompleteCB;
	}
}
