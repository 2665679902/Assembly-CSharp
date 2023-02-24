using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AC5 RID: 2757
[AddComponentMenu("KMonoBehaviour/scripts/KCanvasScaler")]
public class KCanvasScaler : KMonoBehaviour
{
	// Token: 0x0600546A RID: 21610 RVA: 0x001EA39C File Offset: 0x001E859C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (KPlayerPrefs.HasKey(KCanvasScaler.UIScalePrefKey))
		{
			this.SetUserScale(KPlayerPrefs.GetFloat(KCanvasScaler.UIScalePrefKey) / 100f);
		}
		else
		{
			this.SetUserScale(1f);
		}
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
	}

	// Token: 0x0600546B RID: 21611 RVA: 0x001EA404 File Offset: 0x001E8604
	private void OnResize()
	{
		this.SetUserScale(this.userScale);
	}

	// Token: 0x0600546C RID: 21612 RVA: 0x001EA412 File Offset: 0x001E8612
	public void SetUserScale(float scale)
	{
		if (this.canvasScaler == null)
		{
			this.canvasScaler = base.GetComponent<CanvasScaler>();
		}
		this.userScale = scale;
		this.canvasScaler.scaleFactor = this.GetCanvasScale();
	}

	// Token: 0x0600546D RID: 21613 RVA: 0x001EA446 File Offset: 0x001E8646
	public float GetUserScale()
	{
		return this.userScale;
	}

	// Token: 0x0600546E RID: 21614 RVA: 0x001EA44E File Offset: 0x001E864E
	public float GetCanvasScale()
	{
		return this.userScale * this.ScreenRelativeScale();
	}

	// Token: 0x0600546F RID: 21615 RVA: 0x001EA460 File Offset: 0x001E8660
	private float ScreenRelativeScale()
	{
		float dpi = Screen.dpi;
		Camera camera = Camera.main;
		if (camera == null)
		{
			camera = UnityEngine.Object.FindObjectOfType<Camera>();
		}
		camera != null;
		if ((float)Screen.height <= this.scaleSteps[0].maxRes_y || (float)Screen.width / (float)Screen.height < 1.6777778f)
		{
			return this.scaleSteps[0].scale;
		}
		if ((float)Screen.height > this.scaleSteps[this.scaleSteps.Length - 1].maxRes_y)
		{
			return this.scaleSteps[this.scaleSteps.Length - 1].scale;
		}
		for (int i = 0; i < this.scaleSteps.Length; i++)
		{
			if ((float)Screen.height > this.scaleSteps[i].maxRes_y && (float)Screen.height <= this.scaleSteps[i + 1].maxRes_y)
			{
				float num = ((float)Screen.height - this.scaleSteps[i].maxRes_y) / (this.scaleSteps[i + 1].maxRes_y - this.scaleSteps[i].maxRes_y);
				return Mathf.Lerp(this.scaleSteps[i].scale, this.scaleSteps[i + 1].scale, num);
			}
		}
		return 1f;
	}

	// Token: 0x04003964 RID: 14692
	[MyCmpReq]
	private CanvasScaler canvasScaler;

	// Token: 0x04003965 RID: 14693
	public static string UIScalePrefKey = "UIScalePref";

	// Token: 0x04003966 RID: 14694
	private float userScale = 1f;

	// Token: 0x04003967 RID: 14695
	[Range(0.75f, 2f)]
	private KCanvasScaler.ScaleStep[] scaleSteps = new KCanvasScaler.ScaleStep[]
	{
		new KCanvasScaler.ScaleStep(720f, 0.86f),
		new KCanvasScaler.ScaleStep(1080f, 1f),
		new KCanvasScaler.ScaleStep(2160f, 1.33f)
	};

	// Token: 0x02001944 RID: 6468
	[Serializable]
	public struct ScaleStep
	{
		// Token: 0x06008FB5 RID: 36789 RVA: 0x00310D9E File Offset: 0x0030EF9E
		public ScaleStep(float maxRes_y, float scale)
		{
			this.maxRes_y = maxRes_y;
			this.scale = scale;
		}

		// Token: 0x040073D5 RID: 29653
		public float scale;

		// Token: 0x040073D6 RID: 29654
		public float maxRes_y;
	}
}
