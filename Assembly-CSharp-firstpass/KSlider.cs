using System;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000065 RID: 101
public class KSlider : Slider
{
	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06000421 RID: 1057 RVA: 0x00014E34 File Offset: 0x00013034
	// (remove) Token: 0x06000422 RID: 1058 RVA: 0x00014E6C File Offset: 0x0001306C
	public event System.Action onReleaseHandle;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06000423 RID: 1059 RVA: 0x00014EA4 File Offset: 0x000130A4
	// (remove) Token: 0x06000424 RID: 1060 RVA: 0x00014EDC File Offset: 0x000130DC
	public event System.Action onDrag;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x06000425 RID: 1061 RVA: 0x00014F14 File Offset: 0x00013114
	// (remove) Token: 0x06000426 RID: 1062 RVA: 0x00014F4C File Offset: 0x0001314C
	public event System.Action onPointerDown;

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06000427 RID: 1063 RVA: 0x00014F84 File Offset: 0x00013184
	// (remove) Token: 0x06000428 RID: 1064 RVA: 0x00014FBC File Offset: 0x000131BC
	public event System.Action onMove;

	// Token: 0x06000429 RID: 1065 RVA: 0x00014FF4 File Offset: 0x000131F4
	private new void Awake()
	{
		this.currentSounds = new string[KSlider.DefaultSounds.Length];
		for (int i = 0; i < KSlider.DefaultSounds.Length; i++)
		{
			this.currentSounds[i] = KSlider.DefaultSounds[i];
		}
		this.lastMoveTime = Time.unscaledTime;
		this.lastMoveValue = -1f;
		this.tooltip = base.handleRect.gameObject.GetComponent<ToolTip>();
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00015060 File Offset: 0x00013260
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		this.PlayEndSound();
		if (this.tooltip != null)
		{
			this.tooltip.enabled = true;
			this.tooltip.OnPointerEnter(eventData);
		}
		if (this.onReleaseHandle != null)
		{
			this.onReleaseHandle();
		}
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x000150B4 File Offset: 0x000132B4
	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		this.PlayStartSound();
		if (this.value != this.lastMoveValue)
		{
			this.PlayMoveSound(KSlider.MoveSource.MouseClick);
		}
		if (this.tooltip != null)
		{
			this.tooltip.enabled = false;
		}
		if (this.onPointerDown != null)
		{
			this.onPointerDown();
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00015110 File Offset: 0x00013310
	public override void OnDrag(PointerEventData eventData)
	{
		base.OnDrag(eventData);
		this.PlayMoveSound(KSlider.MoveSource.MouseDrag);
		if (this.onDrag != null)
		{
			this.onDrag();
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00015133 File Offset: 0x00013333
	public override void OnMove(AxisEventData eventData)
	{
		base.OnMove(eventData);
		this.PlayMoveSound(KSlider.MoveSource.Keyboard);
		if (this.onMove != null)
		{
			this.onMove();
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00015156 File Offset: 0x00013356
	public void ClearReleaseHandleEvent()
	{
		this.onReleaseHandle = null;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0001515F File Offset: 0x0001335F
	public void SetTooltipText(string tooltipText)
	{
		if (this.tooltip != null)
		{
			this.tooltip.SetSimpleTooltip(tooltipText);
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0001517C File Offset: 0x0001337C
	public void PlayStartSound()
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		if (!this.playSounds)
		{
			return;
		}
		string text = this.currentSounds[0];
		if (text != null && text.Length > 0)
		{
			KFMOD.PlayUISound(text);
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x000151B8 File Offset: 0x000133B8
	public void PlayMoveSound(KSlider.MoveSource moveSource)
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		if (!this.playSounds)
		{
			return;
		}
		float num = Time.unscaledTime - this.lastMoveTime;
		if (num < this.movePlayRate)
		{
			return;
		}
		if (moveSource != KSlider.MoveSource.MouseDrag)
		{
			this.playedBoundaryBump = false;
		}
		float num2 = Mathf.InverseLerp(base.minValue, base.maxValue, this.value);
		string text = null;
		if (num2 == 1f && this.lastMoveValue == 1f)
		{
			if (!this.playedBoundaryBump)
			{
				text = this.currentSounds[4];
				this.playedBoundaryBump = true;
			}
		}
		else if (num2 == 0f && this.lastMoveValue == 0f)
		{
			if (!this.playedBoundaryBump)
			{
				text = this.currentSounds[3];
				this.playedBoundaryBump = true;
			}
		}
		else if (num2 >= 0f && num2 <= 1f)
		{
			text = this.currentSounds[1];
			this.playedBoundaryBump = false;
		}
		if (text != null && text.Length > 0)
		{
			this.lastMoveTime = Time.unscaledTime;
			this.lastMoveValue = num2;
			EventInstance eventInstance = KFMOD.BeginOneShot(text, Vector3.zero, 1f);
			eventInstance.setParameterByName("sliderValue", num2, false);
			eventInstance.setParameterByName("timeSinceLast", num, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x000152E4 File Offset: 0x000134E4
	public void PlayEndSound()
	{
		if (!KInputManager.isFocused)
		{
			return;
		}
		if (!this.playSounds)
		{
			return;
		}
		string text = this.currentSounds[2];
		if (text != null && text.Length > 0)
		{
			EventInstance eventInstance = KFMOD.BeginOneShot(text, Vector3.zero, 1f);
			eventInstance.setParameterByName("sliderValue", this.value, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x04000492 RID: 1170
	public AnimationCurve sliderWeightCurve;

	// Token: 0x04000493 RID: 1171
	public static string[] DefaultSounds = new string[5];

	// Token: 0x04000494 RID: 1172
	private string[] currentSounds;

	// Token: 0x04000495 RID: 1173
	private bool playSounds = true;

	// Token: 0x04000496 RID: 1174
	private float lastMoveTime;

	// Token: 0x04000497 RID: 1175
	private float movePlayRate = 0.025f;

	// Token: 0x04000498 RID: 1176
	private float lastMoveValue;

	// Token: 0x04000499 RID: 1177
	public bool playedBoundaryBump;

	// Token: 0x0400049A RID: 1178
	private ToolTip tooltip;

	// Token: 0x020009B5 RID: 2485
	public enum SoundType
	{
		// Token: 0x040021AC RID: 8620
		Start,
		// Token: 0x040021AD RID: 8621
		Move,
		// Token: 0x040021AE RID: 8622
		End,
		// Token: 0x040021AF RID: 8623
		BoundaryLow,
		// Token: 0x040021B0 RID: 8624
		BoundaryHigh,
		// Token: 0x040021B1 RID: 8625
		Num
	}

	// Token: 0x020009B6 RID: 2486
	public enum MoveSource
	{
		// Token: 0x040021B3 RID: 8627
		Keyboard,
		// Token: 0x040021B4 RID: 8628
		MouseDrag,
		// Token: 0x040021B5 RID: 8629
		MouseClick,
		// Token: 0x040021B6 RID: 8630
		Num
	}
}
