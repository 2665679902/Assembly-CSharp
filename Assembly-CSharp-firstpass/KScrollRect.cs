using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000064 RID: 100
public class KScrollRect : ScrollRect
{
	// Token: 0x17000093 RID: 147
	// (get) Token: 0x0600040E RID: 1038 RVA: 0x00014374 File Offset: 0x00012574
	// (set) Token: 0x0600040F RID: 1039 RVA: 0x0001437C File Offset: 0x0001257C
	public bool isDragging { get; private set; }

	// Token: 0x06000410 RID: 1040 RVA: 0x00014388 File Offset: 0x00012588
	protected override void Awake()
	{
		base.Awake();
		base.elasticity = this.default_elasticity;
		base.inertia = this.default_intertia;
		base.decelerationRate = this.default_decelerationRate;
		base.scrollSensitivity = 1f;
		foreach (KeyValuePair<KScrollRect.SoundType, string> keyValuePair in KScrollRect.DefaultSounds)
		{
			this.currentSounds[keyValuePair.Key] = keyValuePair.Value;
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00014424 File Offset: 0x00012624
	public override void OnScroll(PointerEventData data)
	{
		if (base.vertical && this.allowVerticalScrollWheel)
		{
			this.scrollVelocity += data.scrollDelta.y * this.verticalScrollInertiaScale;
		}
		else if (base.horizontal && this.allowHorizontalScrollWheel)
		{
			this.scrollVelocity -= data.scrollDelta.y * this.horizontalScrollInertiaScale;
		}
		if (Mathf.Abs(data.scrollDelta.y) > 0.2f)
		{
			EventInstance eventInstance = KFMOD.BeginOneShot(this.currentSounds[KScrollRect.SoundType.OnMouseScroll], Vector3.zero, 1f);
			float boundsExceedAmount = this.GetBoundsExceedAmount();
			eventInstance.setParameterByName("scrollbarPosition", boundsExceedAmount, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x000144E4 File Offset: 0x000126E4
	private float GetBoundsExceedAmount()
	{
		if (base.vertical && base.verticalScrollbar != null)
		{
			float num = Mathf.Min(((base.viewport == null) ? base.gameObject.GetComponent<RectTransform>() : base.viewport.rectTransform()).rect.size.y, base.content.sizeDelta.y) / base.content.sizeDelta.y;
			float num2 = Mathf.Abs(base.verticalScrollbar.size - num);
			if (Mathf.Abs(num2) < 0.001f)
			{
				num2 = 0f;
			}
			return num2;
		}
		if (base.horizontal && base.horizontalScrollbar != null)
		{
			float num3 = Mathf.Min(((base.viewport == null) ? base.gameObject.GetComponent<RectTransform>() : base.viewport.rectTransform()).rect.size.x, base.content.sizeDelta.x) / base.content.sizeDelta.x;
			float num4 = Mathf.Abs(base.horizontalScrollbar.size - num3);
			if (Mathf.Abs(num4) < 0.001f)
			{
				num4 = 0f;
			}
			return num4;
		}
		return 0f;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00014640 File Offset: 0x00012840
	public void SetSmoothAutoScrollTarget(float normalizedVerticalPos)
	{
		this.autoScrollTargetVerticalPos = normalizedVerticalPos;
		this.autoScrolling = true;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00014650 File Offset: 0x00012850
	private void PlaySound(KScrollRect.SoundType soundType)
	{
		if (this.currentSounds.ContainsKey(soundType))
		{
			KFMOD.PlayUISound(this.currentSounds[soundType]);
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00014671 File Offset: 0x00012871
	public void SetSound(KScrollRect.SoundType soundType, string soundPath)
	{
		this.currentSounds[soundType] = soundPath;
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00014680 File Offset: 0x00012880
	public override void OnBeginDrag(PointerEventData eventData)
	{
		this.startDrag = true;
		base.OnBeginDrag(eventData);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00014690 File Offset: 0x00012890
	public override void OnEndDrag(PointerEventData eventData)
	{
		this.stopDrag = true;
		base.OnEndDrag(eventData);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x000146A0 File Offset: 0x000128A0
	public override void OnDrag(PointerEventData eventData)
	{
		if (this.allowRightMouseScroll && (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Middle))
		{
			base.content.localPosition = base.content.localPosition + new Vector3(eventData.delta.x, eventData.delta.y);
			base.normalizedPosition = new Vector2(Mathf.Clamp(base.normalizedPosition.x, 0f, 1f), Mathf.Clamp(base.normalizedPosition.y, 0f, 1f));
		}
		base.OnDrag(eventData);
		this.scrollVelocity = 0f;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00014754 File Offset: 0x00012954
	protected override void LateUpdate()
	{
		this.UpdateScrollIntertia();
		if (this.allowRightMouseScroll)
		{
			if (this.panUp)
			{
				this.keyboardScrollDelta.y = this.keyboardScrollDelta.y - this.keyboardScrollSpeed;
				this.keyboardScrollDelta.y = Mathf.Clamp(this.keyboardScrollDelta.y, -25f, 25f);
			}
			if (this.panDown)
			{
				this.keyboardScrollDelta.y = this.keyboardScrollDelta.y + this.keyboardScrollSpeed;
				this.keyboardScrollDelta.y = Mathf.Clamp(this.keyboardScrollDelta.y, -25f, 25f);
			}
			if (this.panLeft)
			{
				this.keyboardScrollDelta.x = this.keyboardScrollDelta.x + this.keyboardScrollSpeed;
				this.keyboardScrollDelta.x = Mathf.Clamp(this.keyboardScrollDelta.x, -25f, 25f);
			}
			if (this.panRight)
			{
				this.keyboardScrollDelta.x = this.keyboardScrollDelta.x - this.keyboardScrollSpeed;
				this.keyboardScrollDelta.x = Mathf.Clamp(this.keyboardScrollDelta.x, -25f, 25f);
			}
			if (this.panUp || this.panDown || this.panLeft || this.panRight)
			{
				base.content.localPosition = base.content.localPosition + this.keyboardScrollDelta;
				base.normalizedPosition = new Vector2(Mathf.Clamp(base.normalizedPosition.x, 0f, 1f), Mathf.Clamp(base.normalizedPosition.y, 0f, 1f));
			}
			else
			{
				this.keyboardScrollDelta = Vector3.zero;
			}
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			if (!this.mouseIsOver)
			{
				this.zoomInPan = (this.zoomOutPan = false);
			}
			if (!base.vertical || !base.horizontal)
			{
				if (this.zoomInPan)
				{
					this.scrollVelocity = -this.panSpeed;
				}
				if (this.zoomOutPan)
				{
					this.scrollVelocity = this.panSpeed;
				}
			}
		}
		else
		{
			this.zoomInPan = (this.zoomOutPan = false);
		}
		if (this.startDrag)
		{
			this.startDrag = false;
			this.isDragging = true;
		}
		else if (this.stopDrag)
		{
			this.stopDrag = false;
			this.isDragging = false;
		}
		if (this.autoScrolling)
		{
			base.normalizedPosition = new Vector2(base.normalizedPosition.x, Mathf.Lerp(base.normalizedPosition.y, this.autoScrollTargetVerticalPos, Time.unscaledDeltaTime * 3f));
			if (Mathf.Abs(this.autoScrollTargetVerticalPos - base.normalizedPosition.y) < 0.01f)
			{
				this.autoScrolling = false;
			}
		}
		base.LateUpdate();
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00014A09 File Offset: 0x00012C09
	public void AnalogUpdate(Vector2 analogValue)
	{
		base.content.anchoredPosition -= analogValue;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00014A24 File Offset: 0x00012C24
	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		if (this.forceContentMatchWidth)
		{
			Vector2 sizeDelta = base.content.GetComponent<RectTransform>().sizeDelta;
			sizeDelta.x = base.viewport.rectTransform().sizeDelta.x;
			base.content.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		}
		if (this.forceContentMatchHeight)
		{
			Vector2 sizeDelta2 = base.content.GetComponent<RectTransform>().sizeDelta;
			sizeDelta2.y = base.viewport.rectTransform().sizeDelta.y;
			base.content.GetComponent<RectTransform>().sizeDelta = sizeDelta2;
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00014AC4 File Offset: 0x00012CC4
	private void UpdateScrollIntertia()
	{
		this.scrollVelocity *= 1f - Mathf.Clamp(this.scrollDeceleration, 0f, 1f);
		if (Mathf.Abs(this.scrollVelocity) < 0.001f)
		{
			this.scrollVelocity = 0f;
		}
		else
		{
			Vector2 anchoredPosition = base.content.anchoredPosition;
			if (base.vertical && this.allowVerticalScrollWheel)
			{
				anchoredPosition.y -= this.scrollVelocity;
			}
			if (base.horizontal && this.allowHorizontalScrollWheel)
			{
				anchoredPosition.x -= this.scrollVelocity;
			}
			if (base.content.anchoredPosition != anchoredPosition)
			{
				base.content.anchoredPosition = anchoredPosition;
			}
		}
		if (base.vertical && this.allowVerticalScrollWheel && (base.verticalNormalizedPosition < -0.05f || base.verticalNormalizedPosition > 1.05f))
		{
			this.scrollVelocity *= 0.9f;
		}
		if (base.horizontal && this.allowHorizontalScrollWheel && (base.horizontalNormalizedPosition < -0.05f || base.horizontalNormalizedPosition > 1.05f))
		{
			this.scrollVelocity *= 0.9f;
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00014C00 File Offset: 0x00012E00
	public void OnKeyDown(KButtonEvent e)
	{
		if (KInputManager.currentControllerIsGamepad && this.mouseIsOver)
		{
			if (e.TryConsume(global::Action.ZoomIn))
			{
				this.zoomInPan = true;
				return;
			}
			if (e.TryConsume(global::Action.ZoomOut))
			{
				this.zoomOutPan = true;
				return;
			}
		}
		if (!this.allowRightMouseScroll)
		{
			return;
		}
		if (e.TryConsume(global::Action.PanLeft))
		{
			this.panLeft = true;
			return;
		}
		if (e.TryConsume(global::Action.PanRight))
		{
			this.panRight = true;
			return;
		}
		if (e.TryConsume(global::Action.PanUp))
		{
			this.panUp = true;
			return;
		}
		if (e.TryConsume(global::Action.PanDown))
		{
			this.panDown = true;
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00014C9C File Offset: 0x00012E9C
	public void OnKeyUp(KButtonEvent e)
	{
		if (KInputManager.currentControllerIsGamepad && this.mouseIsOver)
		{
			if (this.zoomInPan && e.TryConsume(global::Action.ZoomIn))
			{
				this.zoomInPan = false;
				return;
			}
			if (this.zoomOutPan && e.TryConsume(global::Action.ZoomOut))
			{
				this.zoomOutPan = false;
				return;
			}
		}
		if (!this.allowRightMouseScroll)
		{
			return;
		}
		if (this.panUp && e.TryConsume(global::Action.PanUp))
		{
			this.panUp = false;
			this.keyboardScrollDelta.y = 0f;
			return;
		}
		if (this.panDown && e.TryConsume(global::Action.PanDown))
		{
			this.panDown = false;
			this.keyboardScrollDelta.y = 0f;
			return;
		}
		if (this.panRight && e.TryConsume(global::Action.PanRight))
		{
			this.panRight = false;
			this.keyboardScrollDelta.x = 0f;
			return;
		}
		if (this.panLeft && e.TryConsume(global::Action.PanLeft))
		{
			this.panLeft = false;
			this.keyboardScrollDelta.x = 0f;
		}
	}

	// Token: 0x04000470 RID: 1136
	public static Dictionary<KScrollRect.SoundType, string> DefaultSounds = new Dictionary<KScrollRect.SoundType, string>();

	// Token: 0x04000471 RID: 1137
	private Dictionary<KScrollRect.SoundType, string> currentSounds = new Dictionary<KScrollRect.SoundType, string>();

	// Token: 0x04000472 RID: 1138
	private float scrollVelocity;

	// Token: 0x04000473 RID: 1139
	private bool default_intertia = true;

	// Token: 0x04000474 RID: 1140
	private float default_elasticity = 0.2f;

	// Token: 0x04000475 RID: 1141
	private float default_decelerationRate = 0.02f;

	// Token: 0x04000476 RID: 1142
	private float verticalScrollInertiaScale = 10f;

	// Token: 0x04000477 RID: 1143
	private float horizontalScrollInertiaScale = 5f;

	// Token: 0x04000478 RID: 1144
	private float scrollDeceleration = 0.25f;

	// Token: 0x04000479 RID: 1145
	[SerializeField]
	public bool forceContentMatchWidth;

	// Token: 0x0400047A RID: 1146
	[SerializeField]
	public bool forceContentMatchHeight;

	// Token: 0x0400047B RID: 1147
	[SerializeField]
	public bool allowHorizontalScrollWheel = true;

	// Token: 0x0400047C RID: 1148
	[SerializeField]
	public bool allowVerticalScrollWheel = true;

	// Token: 0x0400047D RID: 1149
	[SerializeField]
	public bool allowRightMouseScroll;

	// Token: 0x0400047E RID: 1150
	[SerializeField]
	public bool scrollIsHorizontalOnly;

	// Token: 0x0400047F RID: 1151
	public float panSpeed = 20f;

	// Token: 0x04000480 RID: 1152
	public bool mouseIsOver;

	// Token: 0x04000481 RID: 1153
	private bool panUp;

	// Token: 0x04000482 RID: 1154
	private bool panDown;

	// Token: 0x04000483 RID: 1155
	private bool panRight;

	// Token: 0x04000484 RID: 1156
	private bool panLeft;

	// Token: 0x04000485 RID: 1157
	private bool zoomInPan;

	// Token: 0x04000486 RID: 1158
	private bool zoomOutPan;

	// Token: 0x04000487 RID: 1159
	private Vector3 keyboardScrollDelta;

	// Token: 0x04000488 RID: 1160
	private float keyboardScrollSpeed = 1f;

	// Token: 0x04000489 RID: 1161
	private bool startDrag;

	// Token: 0x0400048A RID: 1162
	private bool stopDrag;

	// Token: 0x0400048C RID: 1164
	private bool autoScrolling;

	// Token: 0x0400048D RID: 1165
	private float autoScrollTargetVerticalPos;

	// Token: 0x020009B4 RID: 2484
	public enum SoundType
	{
		// Token: 0x040021AA RID: 8618
		OnMouseScroll
	}
}
