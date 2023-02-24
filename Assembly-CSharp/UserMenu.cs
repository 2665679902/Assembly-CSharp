using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000502 RID: 1282
public class UserMenu
{
	// Token: 0x06001E44 RID: 7748 RVA: 0x000A215F File Offset: 0x000A035F
	public void Refresh(GameObject go)
	{
		Game.Instance.Trigger(1980521255, go);
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x000A2174 File Offset: 0x000A0374
	public void AddButton(GameObject go, KIconButtonMenu.ButtonInfo button, float sort_order = 1f)
	{
		if (button.onClick != null)
		{
			System.Action callback = button.onClick;
			button.onClick = delegate
			{
				callback();
				Game.Instance.Trigger(1980521255, go);
			};
		}
		this.buttons.Add(new KeyValuePair<KIconButtonMenu.ButtonInfo, float>(button, sort_order));
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x000A21C6 File Offset: 0x000A03C6
	public void AddSlider(GameObject go, UserMenu.SliderInfo slider)
	{
		this.sliders.Add(slider);
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x000A21D4 File Offset: 0x000A03D4
	public void AppendToScreen(GameObject go, UserMenuScreen screen)
	{
		this.buttons.Clear();
		this.sliders.Clear();
		go.Trigger(493375141, null);
		if (this.buttons.Count > 0)
		{
			this.buttons.Sort(delegate(KeyValuePair<KIconButtonMenu.ButtonInfo, float> x, KeyValuePair<KIconButtonMenu.ButtonInfo, float> y)
			{
				if (x.Value == y.Value)
				{
					return 0;
				}
				if (x.Value > y.Value)
				{
					return 1;
				}
				return -1;
			});
			for (int i = 0; i < this.buttons.Count; i++)
			{
				this.sortedButtons.Add(this.buttons[i].Key);
			}
			screen.AddButtons(this.sortedButtons);
			this.sortedButtons.Clear();
		}
		if (this.sliders.Count > 0)
		{
			screen.AddSliders(this.sliders);
		}
	}

	// Token: 0x040010EF RID: 4335
	public const float DECONSTRUCT_PRIORITY = 0f;

	// Token: 0x040010F0 RID: 4336
	public const float DRAWPATHS_PRIORITY = 0.1f;

	// Token: 0x040010F1 RID: 4337
	public const float FOLLOWCAM_PRIORITY = 0.3f;

	// Token: 0x040010F2 RID: 4338
	public const float SETDIRECTION_PRIORITY = 0.4f;

	// Token: 0x040010F3 RID: 4339
	public const float AUTOBOTTLE_PRIORITY = 0.4f;

	// Token: 0x040010F4 RID: 4340
	public const float AUTOREPAIR_PRIORITY = 0.5f;

	// Token: 0x040010F5 RID: 4341
	public const float DEFAULT_PRIORITY = 1f;

	// Token: 0x040010F6 RID: 4342
	public const float SUITEQUIP_PRIORITY = 2f;

	// Token: 0x040010F7 RID: 4343
	public const float AUTODISINFECT_PRIORITY = 10f;

	// Token: 0x040010F8 RID: 4344
	public const float ROCKETUSAGERESTRICTION_PRIORITY = 11f;

	// Token: 0x040010F9 RID: 4345
	private List<KeyValuePair<KIconButtonMenu.ButtonInfo, float>> buttons = new List<KeyValuePair<KIconButtonMenu.ButtonInfo, float>>();

	// Token: 0x040010FA RID: 4346
	private List<UserMenu.SliderInfo> sliders = new List<UserMenu.SliderInfo>();

	// Token: 0x040010FB RID: 4347
	private List<KIconButtonMenu.ButtonInfo> sortedButtons = new List<KIconButtonMenu.ButtonInfo>();

	// Token: 0x02001136 RID: 4406
	public class SliderInfo
	{
		// Token: 0x04005A35 RID: 23093
		public MinMaxSlider.LockingType lockType = MinMaxSlider.LockingType.Drag;

		// Token: 0x04005A36 RID: 23094
		public MinMaxSlider.Mode mode;

		// Token: 0x04005A37 RID: 23095
		public Slider.Direction direction;

		// Token: 0x04005A38 RID: 23096
		public bool interactable = true;

		// Token: 0x04005A39 RID: 23097
		public bool lockRange;

		// Token: 0x04005A3A RID: 23098
		public string toolTip;

		// Token: 0x04005A3B RID: 23099
		public string toolTipMin;

		// Token: 0x04005A3C RID: 23100
		public string toolTipMax;

		// Token: 0x04005A3D RID: 23101
		public float minLimit;

		// Token: 0x04005A3E RID: 23102
		public float maxLimit = 100f;

		// Token: 0x04005A3F RID: 23103
		public float currentMinValue = 10f;

		// Token: 0x04005A40 RID: 23104
		public float currentMaxValue = 90f;

		// Token: 0x04005A41 RID: 23105
		public GameObject sliderGO;

		// Token: 0x04005A42 RID: 23106
		public Action<MinMaxSlider> onMinChange;

		// Token: 0x04005A43 RID: 23107
		public Action<MinMaxSlider> onMaxChange;
	}
}
