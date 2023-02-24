using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C2D RID: 3117
[AddComponentMenu("KMonoBehaviour/scripts/MinMaxSlider")]
public class MinMaxSlider : KMonoBehaviour
{
	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x0600629C RID: 25244 RVA: 0x002464A5 File Offset: 0x002446A5
	// (set) Token: 0x0600629D RID: 25245 RVA: 0x002464AD File Offset: 0x002446AD
	public MinMaxSlider.Mode mode { get; private set; }

	// Token: 0x0600629E RID: 25246 RVA: 0x002464B8 File Offset: 0x002446B8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ToolTip component = base.transform.parent.gameObject.GetComponent<ToolTip>();
		if (component != null)
		{
			UnityEngine.Object.DestroyImmediate(this.toolTip);
			this.toolTip = component;
		}
		this.minSlider.value = this.currentMinValue;
		this.maxSlider.value = this.currentMaxValue;
		this.minSlider.interactable = this.interactable;
		this.maxSlider.interactable = this.interactable;
		this.minSlider.maxValue = this.maxLimit;
		this.maxSlider.maxValue = this.maxLimit;
		this.minSlider.minValue = this.minLimit;
		this.maxSlider.minValue = this.minLimit;
		this.minSlider.direction = (this.maxSlider.direction = this.direction);
		if (this.isOverPowered != null)
		{
			this.isOverPowered.enabled = false;
		}
		this.minSlider.gameObject.SetActive(false);
		if (this.mode != MinMaxSlider.Mode.Single)
		{
			this.minSlider.gameObject.SetActive(true);
		}
		if (this.extraSlider != null)
		{
			this.extraSlider.value = this.currentExtraValue;
			this.extraSlider.wholeNumbers = (this.minSlider.wholeNumbers = (this.maxSlider.wholeNumbers = this.wholeNumbers));
			this.extraSlider.direction = this.direction;
			this.extraSlider.interactable = this.interactable;
			this.extraSlider.maxValue = this.maxLimit;
			this.extraSlider.minValue = this.minLimit;
			this.extraSlider.gameObject.SetActive(false);
			if (this.mode == MinMaxSlider.Mode.Triple)
			{
				this.extraSlider.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x0600629F RID: 25247 RVA: 0x002466A8 File Offset: 0x002448A8
	public void SetIcon(Image newIcon)
	{
		this.icon = newIcon;
		this.icon.gameObject.transform.SetParent(base.transform);
		this.icon.gameObject.transform.SetAsFirstSibling();
		this.icon.rectTransform().anchoredPosition = Vector2.zero;
	}

	// Token: 0x060062A0 RID: 25248 RVA: 0x00246704 File Offset: 0x00244904
	public void SetMode(MinMaxSlider.Mode mode)
	{
		this.mode = mode;
		if (mode == MinMaxSlider.Mode.Single && this.extraSlider != null)
		{
			this.extraSlider.gameObject.SetActive(false);
			this.extraSlider.handleRect.gameObject.SetActive(false);
		}
	}

	// Token: 0x060062A1 RID: 25249 RVA: 0x00246750 File Offset: 0x00244950
	private void SetAnchor(RectTransform trans, Vector2 min, Vector2 max)
	{
		trans.anchorMin = min;
		trans.anchorMax = max;
	}

	// Token: 0x060062A2 RID: 25250 RVA: 0x00246760 File Offset: 0x00244960
	public void SetMinMaxValue(float currentMin, float currentMax, float min, float max)
	{
		this.minSlider.value = currentMin;
		this.currentMinValue = currentMin;
		this.maxSlider.value = currentMax;
		this.currentMaxValue = currentMax;
		this.minLimit = min;
		this.maxLimit = max;
		this.minSlider.minValue = this.minLimit;
		this.maxSlider.minValue = this.minLimit;
		this.minSlider.maxValue = this.maxLimit;
		this.maxSlider.maxValue = this.maxLimit;
		if (this.extraSlider != null)
		{
			this.extraSlider.minValue = this.minLimit;
			this.extraSlider.maxValue = this.maxLimit;
		}
	}

	// Token: 0x060062A3 RID: 25251 RVA: 0x0024681A File Offset: 0x00244A1A
	public void SetExtraValue(float current)
	{
		this.extraSlider.value = current;
		this.toolTip.toolTip = base.transform.parent.name + ": " + current.ToString("F2");
	}

	// Token: 0x060062A4 RID: 25252 RVA: 0x0024685C File Offset: 0x00244A5C
	public void SetMaxValue(float current, float max)
	{
		float num = current / max * 100f;
		if (this.isOverPowered != null)
		{
			this.isOverPowered.enabled = num > 100f;
		}
		this.maxSlider.value = Mathf.Min(100f, num);
		if (this.toolTip != null)
		{
			this.toolTip.toolTip = string.Concat(new string[]
			{
				base.transform.parent.name,
				": ",
				current.ToString("F2"),
				"/",
				max.ToString("F2")
			});
		}
	}

	// Token: 0x060062A5 RID: 25253 RVA: 0x00246910 File Offset: 0x00244B10
	private void Update()
	{
		if (!this.interactable)
		{
			return;
		}
		this.minSlider.value = Mathf.Clamp(this.currentMinValue, this.minLimit, this.currentMinValue);
		this.maxSlider.value = Mathf.Max(this.minSlider.value, Mathf.Clamp(this.currentMaxValue, Mathf.Max(this.minSlider.value, this.minLimit), this.maxLimit));
		if (this.direction == Slider.Direction.LeftToRight || this.direction == Slider.Direction.RightToLeft)
		{
			this.minRect.anchorMax = new Vector2(this.minSlider.value / this.maxLimit, this.minRect.anchorMax.y);
			this.maxRect.anchorMax = new Vector2(this.maxSlider.value / this.maxLimit, this.maxRect.anchorMax.y);
			this.maxRect.anchorMin = new Vector2(this.minSlider.value / this.maxLimit, this.maxRect.anchorMin.y);
			return;
		}
		this.minRect.anchorMax = new Vector2(this.minRect.anchorMin.x, this.minSlider.value / this.maxLimit);
		this.maxRect.anchorMin = new Vector2(this.maxRect.anchorMin.x, this.minSlider.value / this.maxLimit);
	}

	// Token: 0x060062A6 RID: 25254 RVA: 0x00246A9C File Offset: 0x00244C9C
	public void OnMinValueChanged(float ignoreThis)
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockRange)
		{
			this.currentMaxValue = Mathf.Min(Mathf.Max(this.minLimit, this.minSlider.value) + this.range, this.maxLimit);
			this.currentMinValue = Mathf.Max(this.minLimit, Mathf.Min(this.maxSlider.value, this.currentMaxValue - this.range));
		}
		else
		{
			this.currentMinValue = Mathf.Clamp(this.minSlider.value, this.minLimit, Mathf.Min(this.maxSlider.value, this.currentMaxValue));
		}
		if (this.onMinChange != null)
		{
			this.onMinChange(this);
		}
	}

	// Token: 0x060062A7 RID: 25255 RVA: 0x00246B60 File Offset: 0x00244D60
	public void OnMaxValueChanged(float ignoreThis)
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockRange)
		{
			this.currentMinValue = Mathf.Max(this.maxSlider.value - this.range, this.minLimit);
			this.currentMaxValue = Mathf.Max(this.minSlider.value, Mathf.Clamp(this.maxSlider.value, Mathf.Max(this.currentMinValue + this.range, this.minLimit), this.maxLimit));
		}
		else
		{
			this.currentMaxValue = Mathf.Max(this.minSlider.value, Mathf.Clamp(this.maxSlider.value, Mathf.Max(this.minSlider.value, this.minLimit), this.maxLimit));
		}
		if (this.onMaxChange != null)
		{
			this.onMaxChange(this);
		}
	}

	// Token: 0x060062A8 RID: 25256 RVA: 0x00246C40 File Offset: 0x00244E40
	public void Lock(bool shouldLock)
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockType == MinMaxSlider.LockingType.Drag)
		{
			this.lockRange = shouldLock;
			this.range = this.maxSlider.value - this.minSlider.value;
			this.mousePos = KInputManager.GetMousePos();
		}
	}

	// Token: 0x060062A9 RID: 25257 RVA: 0x00246C90 File Offset: 0x00244E90
	public void ToggleLock()
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockType == MinMaxSlider.LockingType.Toggle)
		{
			this.lockRange = !this.lockRange;
			if (this.lockRange)
			{
				this.range = this.maxSlider.value - this.minSlider.value;
			}
		}
	}

	// Token: 0x060062AA RID: 25258 RVA: 0x00246CE4 File Offset: 0x00244EE4
	public void OnDrag()
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockRange && this.lockType == MinMaxSlider.LockingType.Drag)
		{
			float num = KInputManager.GetMousePos().x - this.mousePos.x;
			if (this.direction == Slider.Direction.TopToBottom || this.direction == Slider.Direction.BottomToTop)
			{
				num = KInputManager.GetMousePos().y - this.mousePos.y;
			}
			this.currentMinValue = Mathf.Max(this.currentMinValue + num, this.minLimit);
			this.mousePos = KInputManager.GetMousePos();
		}
	}

	// Token: 0x04004453 RID: 17491
	public MinMaxSlider.LockingType lockType = MinMaxSlider.LockingType.Drag;

	// Token: 0x04004455 RID: 17493
	public bool lockRange;

	// Token: 0x04004456 RID: 17494
	public bool interactable = true;

	// Token: 0x04004457 RID: 17495
	public float minLimit;

	// Token: 0x04004458 RID: 17496
	public float maxLimit = 100f;

	// Token: 0x04004459 RID: 17497
	public float range = 50f;

	// Token: 0x0400445A RID: 17498
	public float barWidth = 10f;

	// Token: 0x0400445B RID: 17499
	public float barHeight = 100f;

	// Token: 0x0400445C RID: 17500
	public float currentMinValue = 10f;

	// Token: 0x0400445D RID: 17501
	public float currentMaxValue = 90f;

	// Token: 0x0400445E RID: 17502
	public float currentExtraValue = 50f;

	// Token: 0x0400445F RID: 17503
	public Slider.Direction direction;

	// Token: 0x04004460 RID: 17504
	public bool wholeNumbers = true;

	// Token: 0x04004461 RID: 17505
	public Action<MinMaxSlider> onMinChange;

	// Token: 0x04004462 RID: 17506
	public Action<MinMaxSlider> onMaxChange;

	// Token: 0x04004463 RID: 17507
	public Slider minSlider;

	// Token: 0x04004464 RID: 17508
	public Slider maxSlider;

	// Token: 0x04004465 RID: 17509
	public Slider extraSlider;

	// Token: 0x04004466 RID: 17510
	public RectTransform minRect;

	// Token: 0x04004467 RID: 17511
	public RectTransform maxRect;

	// Token: 0x04004468 RID: 17512
	public RectTransform bgFill;

	// Token: 0x04004469 RID: 17513
	public RectTransform mgFill;

	// Token: 0x0400446A RID: 17514
	public RectTransform fgFill;

	// Token: 0x0400446B RID: 17515
	public Text title;

	// Token: 0x0400446C RID: 17516
	[MyCmpGet]
	public ToolTip toolTip;

	// Token: 0x0400446D RID: 17517
	public Image icon;

	// Token: 0x0400446E RID: 17518
	public Image isOverPowered;

	// Token: 0x0400446F RID: 17519
	private Vector3 mousePos;

	// Token: 0x02001ABD RID: 6845
	public enum LockingType
	{
		// Token: 0x04007893 RID: 30867
		Toggle,
		// Token: 0x04007894 RID: 30868
		Drag
	}

	// Token: 0x02001ABE RID: 6846
	public enum Mode
	{
		// Token: 0x04007896 RID: 30870
		Single,
		// Token: 0x04007897 RID: 30871
		Double,
		// Token: 0x04007898 RID: 30872
		Triple
	}
}
