using System;
using UnityEngine;

// Token: 0x020009E5 RID: 2533
public class Infrared : MonoBehaviour
{
	// Token: 0x06004BA9 RID: 19369 RVA: 0x001A91D6 File Offset: 0x001A73D6
	public static void DestroyInstance()
	{
		Infrared.Instance = null;
	}

	// Token: 0x06004BAA RID: 19370 RVA: 0x001A91DE File Offset: 0x001A73DE
	private void Awake()
	{
		Infrared.temperatureParametersId = Shader.PropertyToID("_TemperatureParameters");
		Infrared.Instance = this;
		this.OnResize();
		this.UpdateState();
	}

	// Token: 0x06004BAB RID: 19371 RVA: 0x001A9201 File Offset: 0x001A7401
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, this.minionTexture);
		Graphics.Blit(source, dest);
	}

	// Token: 0x06004BAC RID: 19372 RVA: 0x001A9218 File Offset: 0x001A7418
	private void OnResize()
	{
		if (this.minionTexture != null)
		{
			this.minionTexture.DestroyRenderTexture();
		}
		if (this.cameraTexture != null)
		{
			this.cameraTexture.DestroyRenderTexture();
		}
		int num = 2;
		this.minionTexture = new RenderTexture(Screen.width / num, Screen.height / num, 0, RenderTextureFormat.ARGB32);
		this.cameraTexture = new RenderTexture(Screen.width / num, Screen.height / num, 0, RenderTextureFormat.ARGB32);
		base.GetComponent<Camera>().targetTexture = this.cameraTexture;
	}

	// Token: 0x06004BAD RID: 19373 RVA: 0x001A92A0 File Offset: 0x001A74A0
	public void SetMode(Infrared.Mode mode)
	{
		Vector4 zero;
		if (mode != Infrared.Mode.Disabled)
		{
			if (mode != Infrared.Mode.Disease)
			{
				zero = new Vector4(1f, 0f, 0f, 0f);
			}
			else
			{
				zero = new Vector4(1f, 0f, 0f, 0f);
				GameComps.InfraredVisualizers.ClearOverlayColour();
			}
		}
		else
		{
			zero = Vector4.zero;
		}
		Shader.SetGlobalVector("_ColouredOverlayParameters", zero);
		this.mode = mode;
		this.UpdateState();
	}

	// Token: 0x06004BAE RID: 19374 RVA: 0x001A9318 File Offset: 0x001A7518
	private void UpdateState()
	{
		base.gameObject.SetActive(this.mode > Infrared.Mode.Disabled);
		if (base.gameObject.activeSelf)
		{
			this.Update();
		}
	}

	// Token: 0x06004BAF RID: 19375 RVA: 0x001A9344 File Offset: 0x001A7544
	private void Update()
	{
		switch (this.mode)
		{
		case Infrared.Mode.Disabled:
			break;
		case Infrared.Mode.Infrared:
			GameComps.InfraredVisualizers.UpdateTemperature();
			return;
		case Infrared.Mode.Disease:
			GameComps.DiseaseContainers.UpdateOverlayColours();
			break;
		default:
			return;
		}
	}

	// Token: 0x04003190 RID: 12688
	private RenderTexture minionTexture;

	// Token: 0x04003191 RID: 12689
	private RenderTexture cameraTexture;

	// Token: 0x04003192 RID: 12690
	private Infrared.Mode mode;

	// Token: 0x04003193 RID: 12691
	public static int temperatureParametersId;

	// Token: 0x04003194 RID: 12692
	public static Infrared Instance;

	// Token: 0x020017F0 RID: 6128
	public enum Mode
	{
		// Token: 0x04006E74 RID: 28276
		Disabled,
		// Token: 0x04006E75 RID: 28277
		Infrared,
		// Token: 0x04006E76 RID: 28278
		Disease
	}
}
