﻿using System;
using Klei;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A85 RID: 2693
public class DemoTimer : MonoBehaviour
{
	// Token: 0x0600527B RID: 21115 RVA: 0x001DCDF1 File Offset: 0x001DAFF1
	public static void DestroyInstance()
	{
		DemoTimer.Instance = null;
	}

	// Token: 0x0600527C RID: 21116 RVA: 0x001DCDFC File Offset: 0x001DAFFC
	private void Start()
	{
		DemoTimer.Instance = this;
		if (GenericGameSettings.instance != null)
		{
			if (GenericGameSettings.instance.demoMode)
			{
				this.duration = (float)GenericGameSettings.instance.demoTime;
				this.labelText.gameObject.SetActive(GenericGameSettings.instance.showDemoTimer);
				this.clockImage.gameObject.SetActive(GenericGameSettings.instance.showDemoTimer);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		this.duration = (float)GenericGameSettings.instance.demoTime;
		this.fadeOutScreen = Util.KInstantiateUI(this.Prefab_FadeOutScreen, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false);
		Image component = this.fadeOutScreen.GetComponent<Image>();
		component.raycastTarget = false;
		this.fadeOutColor = component.color;
		this.fadeOutColor.a = 0f;
		this.fadeOutScreen.GetComponent<Image>().color = this.fadeOutColor;
	}

	// Token: 0x0600527D RID: 21117 RVA: 0x001DCEFC File Offset: 0x001DB0FC
	private void Update()
	{
		if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.BackQuote))
		{
			this.CountdownActive = !this.CountdownActive;
			this.UpdateLabel();
		}
		if (this.demoOver || !this.CountdownActive)
		{
			return;
		}
		if (this.beginTime == -1f)
		{
			this.beginTime = Time.unscaledTime;
		}
		this.elapsed = Mathf.Clamp(0f, Time.unscaledTime - this.beginTime, this.duration);
		if (this.elapsed + 5f >= this.duration)
		{
			float num = (this.duration - this.elapsed) / 5f;
			this.fadeOutColor.a = Mathf.Min(1f, 1f - Mathf.Sqrt(num));
			this.fadeOutScreen.GetComponent<Image>().color = this.fadeOutColor;
		}
		if (this.elapsed >= this.duration)
		{
			this.EndDemo();
		}
		this.UpdateLabel();
	}

	// Token: 0x0600527E RID: 21118 RVA: 0x001DD004 File Offset: 0x001DB204
	private void UpdateLabel()
	{
		int num = Mathf.RoundToInt(this.duration - this.elapsed);
		int num2 = Mathf.FloorToInt((float)(num / 60));
		int num3 = num % 60;
		this.labelText.text = string.Concat(new string[]
		{
			UI.DEMOOVERSCREEN.TIMEREMAINING,
			" ",
			num2.ToString("00"),
			":",
			num3.ToString("00")
		});
		if (!this.CountdownActive)
		{
			this.labelText.text = UI.DEMOOVERSCREEN.TIMERINACTIVE;
		}
	}

	// Token: 0x0600527F RID: 21119 RVA: 0x001DD0A0 File Offset: 0x001DB2A0
	public void EndDemo()
	{
		if (this.demoOver)
		{
			return;
		}
		this.demoOver = true;
		Util.KInstantiateUI(this.Prefab_DemoOverScreen, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false).GetComponent<DemoOverScreen>().Show(true);
	}

	// Token: 0x040037B0 RID: 14256
	public static DemoTimer Instance;

	// Token: 0x040037B1 RID: 14257
	public LocText labelText;

	// Token: 0x040037B2 RID: 14258
	public Image clockImage;

	// Token: 0x040037B3 RID: 14259
	public GameObject Prefab_DemoOverScreen;

	// Token: 0x040037B4 RID: 14260
	public GameObject Prefab_FadeOutScreen;

	// Token: 0x040037B5 RID: 14261
	private float duration;

	// Token: 0x040037B6 RID: 14262
	private float elapsed;

	// Token: 0x040037B7 RID: 14263
	private bool demoOver;

	// Token: 0x040037B8 RID: 14264
	private float beginTime = -1f;

	// Token: 0x040037B9 RID: 14265
	public bool CountdownActive;

	// Token: 0x040037BA RID: 14266
	private GameObject fadeOutScreen;

	// Token: 0x040037BB RID: 14267
	private Color fadeOutColor;
}
