using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020008D3 RID: 2259
[AddComponentMenu("KMonoBehaviour/scripts/Timelapser")]
public class Timelapser : KMonoBehaviour
{
	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x060040EF RID: 16623 RVA: 0x0016BA2B File Offset: 0x00169C2B
	public bool CapturingTimelapseScreenshot
	{
		get
		{
			return this.screenshotActive;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x060040F0 RID: 16624 RVA: 0x0016BA33 File Offset: 0x00169C33
	// (set) Token: 0x060040F1 RID: 16625 RVA: 0x0016BA3B File Offset: 0x00169C3B
	public Texture2D freezeTexture { get; private set; }

	// Token: 0x060040F2 RID: 16626 RVA: 0x0016BA44 File Offset: 0x00169C44
	protected override void OnPrefabInit()
	{
		this.RefreshRenderTextureSize(null);
		Game.Instance.Subscribe(75424175, new Action<object>(this.RefreshRenderTextureSize));
		this.freezeCamera = CameraController.Instance.timelapseFreezeCamera;
		if (this.CycleTimeToScreenshot() > 0f)
		{
			this.OnNewDay(null);
		}
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDay));
		this.OnResize();
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
		base.StartCoroutine(this.Render());
	}

	// Token: 0x060040F3 RID: 16627 RVA: 0x0016BAED File Offset: 0x00169CED
	private void OnResize()
	{
		if (this.freezeTexture != null)
		{
			UnityEngine.Object.Destroy(this.freezeTexture);
		}
		this.freezeTexture = new Texture2D(Camera.main.pixelWidth, Camera.main.pixelHeight, TextureFormat.ARGB32, false);
	}

	// Token: 0x060040F4 RID: 16628 RVA: 0x0016BB2C File Offset: 0x00169D2C
	private void RefreshRenderTextureSize(object data = null)
	{
		if (this.previewScreenshot)
		{
			this.bufferRenderTexture = new RenderTexture(this.previewScreenshotResolution.x, this.previewScreenshotResolution.y, 32, RenderTextureFormat.ARGB32);
			return;
		}
		if (this.timelapseUserEnabled)
		{
			this.bufferRenderTexture = new RenderTexture(SaveGame.Instance.TimelapseResolution.x, SaveGame.Instance.TimelapseResolution.y, 32, RenderTextureFormat.ARGB32);
		}
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x060040F5 RID: 16629 RVA: 0x0016BB9A File Offset: 0x00169D9A
	private bool timelapseUserEnabled
	{
		get
		{
			return SaveGame.Instance.TimelapseResolution.x > 0;
		}
	}

	// Token: 0x060040F6 RID: 16630 RVA: 0x0016BBB0 File Offset: 0x00169DB0
	private void OnNewDay(object data = null)
	{
		DebugUtil.LogWarningArgs(new object[]
		{
			this.worldsToScreenshot.Count == 0,
			"Timelapse.OnNewDay but worldsToScreenshot is not empty"
		});
		int cycle = GameClock.Instance.GetCycle();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer.IsDiscovered && !worldContainer.IsModuleInterior)
			{
				if (worldContainer.DiscoveryTimestamp + (float)cycle > (float)this.timelapseScreenshotCycles[this.timelapseScreenshotCycles.Length - 1])
				{
					if (worldContainer.DiscoveryTimestamp + (float)(cycle % 10) == 0f)
					{
						this.screenshotToday = true;
						this.worldsToScreenshot.Add(worldContainer.id);
					}
				}
				else
				{
					for (int i = 0; i < this.timelapseScreenshotCycles.Length; i++)
					{
						if ((int)worldContainer.DiscoveryTimestamp + cycle == this.timelapseScreenshotCycles[i])
						{
							this.screenshotToday = true;
							this.worldsToScreenshot.Add(worldContainer.id);
						}
					}
				}
			}
		}
	}

	// Token: 0x060040F7 RID: 16631 RVA: 0x0016BCD4 File Offset: 0x00169ED4
	private void Update()
	{
		if (this.screenshotToday)
		{
			if (this.CycleTimeToScreenshot() <= 0f || GameClock.Instance.GetCycle() == 0)
			{
				if (!this.timelapseUserEnabled)
				{
					this.screenshotToday = false;
					this.worldsToScreenshot.Clear();
					return;
				}
				if (!PlayerController.Instance.CanDrag())
				{
					CameraController.Instance.ForcePanningState(false);
					this.screenshotToday = false;
					this.SaveScreenshot();
					return;
				}
			}
		}
		else
		{
			this.screenshotToday = !this.screenshotPending && this.worldsToScreenshot.Count > 0;
		}
	}

	// Token: 0x060040F8 RID: 16632 RVA: 0x0016BD61 File Offset: 0x00169F61
	private float CycleTimeToScreenshot()
	{
		return 300f - GameClock.Instance.GetTime() % 600f;
	}

	// Token: 0x060040F9 RID: 16633 RVA: 0x0016BD79 File Offset: 0x00169F79
	private IEnumerator Render()
	{
		for (;;)
		{
			yield return SequenceUtil.WaitForEndOfFrame;
			if (this.screenshotPending)
			{
				int num = (this.previewScreenshot ? ClusterManager.Instance.GetStartWorld().id : this.worldsToScreenshot[0]);
				if (!this.freezeCamera.enabled)
				{
					this.freezeTexture.ReadPixels(new Rect(0f, 0f, (float)Camera.main.pixelWidth, (float)Camera.main.pixelHeight), 0, 0);
					this.freezeTexture.Apply();
					this.freezeCamera.gameObject.GetComponent<FillRenderTargetEffect>().SetFillTexture(this.freezeTexture);
					this.freezeCamera.enabled = true;
					this.screenshotActive = true;
					this.RefreshRenderTextureSize(null);
					DebugHandler.SetTimelapseMode(true, num);
					this.SetPostionAndOrtho(num);
					this.activeOverlay = OverlayScreen.Instance.mode;
					OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, false);
				}
				else
				{
					this.RenderAndPrint(num);
					if (!this.previewScreenshot)
					{
						this.worldsToScreenshot.Remove(num);
					}
					this.freezeCamera.enabled = false;
					DebugHandler.SetTimelapseMode(false, 0);
					this.screenshotPending = false;
					this.previewScreenshot = false;
					this.screenshotActive = false;
					this.debugScreenShot = false;
					this.previewSaveGamePath = "";
					OverlayScreen.Instance.ToggleOverlay(this.activeOverlay, false);
				}
			}
		}
		yield break;
	}

	// Token: 0x060040FA RID: 16634 RVA: 0x0016BD88 File Offset: 0x00169F88
	public void InitialScreenshot()
	{
		this.worldsToScreenshot.Add(ClusterManager.Instance.GetStartWorld().id);
		this.SaveScreenshot();
	}

	// Token: 0x060040FB RID: 16635 RVA: 0x0016BDAA File Offset: 0x00169FAA
	private void SaveScreenshot()
	{
		this.screenshotPending = true;
	}

	// Token: 0x060040FC RID: 16636 RVA: 0x0016BDB3 File Offset: 0x00169FB3
	public void SaveColonyPreview(string saveFileName)
	{
		this.previewSaveGamePath = saveFileName;
		this.previewScreenshot = true;
		this.SaveScreenshot();
	}

	// Token: 0x060040FD RID: 16637 RVA: 0x0016BDCC File Offset: 0x00169FCC
	private void SetPostionAndOrtho(int world_id)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(world_id);
		if (world == null)
		{
			return;
		}
		float num = 0f;
		Camera overlayCamera = CameraController.Instance.overlayCamera;
		this.camSize = overlayCamera.orthographicSize;
		this.camPosition = CameraController.Instance.transform.position;
		if (!world.IsStartWorld)
		{
			CameraController.Instance.OrthographicSize = (float)(world.WorldSize.y / 2);
			CameraController.Instance.SetPosition(new Vector3((float)(world.WorldOffset.x + world.WorldSize.x / 2), (float)(world.WorldOffset.y + world.WorldSize.y / 2), CameraController.Instance.transform.position.z));
			return;
		}
		GameObject telepad = GameUtil.GetTelepad(world_id);
		if (telepad == null)
		{
			return;
		}
		Vector3 position = telepad.transform.GetPosition();
		foreach (BuildingComplete buildingComplete in Components.BuildingCompletes.Items)
		{
			Vector3 position2 = buildingComplete.transform.GetPosition();
			float num2 = (float)this.bufferRenderTexture.width / (float)this.bufferRenderTexture.height;
			Vector3 vector = position - position2;
			num = Mathf.Max(new float[]
			{
				num,
				vector.x / num2,
				vector.y
			});
		}
		num += 10f;
		num = Mathf.Max(num, 18f);
		CameraController.Instance.OrthographicSize = num;
		CameraController.Instance.SetPosition(new Vector3(telepad.transform.position.x, telepad.transform.position.y, CameraController.Instance.transform.position.z));
	}

	// Token: 0x060040FE RID: 16638 RVA: 0x0016BFB8 File Offset: 0x0016A1B8
	private void RenderAndPrint(int world_id)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(world_id);
		if (world == null)
		{
			return;
		}
		if (world.IsStartWorld)
		{
			GameObject telepad = GameUtil.GetTelepad(world.id);
			if (telepad == null)
			{
				global::Debug.Log("No telepad present, aborting screenshot.");
				return;
			}
			Vector3 position = telepad.transform.position;
			position.z = CameraController.Instance.transform.position.z;
			CameraController.Instance.SetPosition(position);
		}
		else
		{
			CameraController.Instance.SetPosition(new Vector3((float)(world.WorldOffset.x + world.WorldSize.x / 2), (float)(world.WorldOffset.y + world.WorldSize.y / 2), CameraController.Instance.transform.position.z));
		}
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = this.bufferRenderTexture;
		CameraController.Instance.RenderForTimelapser(ref this.bufferRenderTexture);
		this.WriteToPng(this.bufferRenderTexture, world_id);
		CameraController.Instance.OrthographicSize = this.camSize;
		CameraController.Instance.SetPosition(this.camPosition);
		RenderTexture.active = active;
	}

	// Token: 0x060040FF RID: 16639 RVA: 0x0016C0E0 File Offset: 0x0016A2E0
	public void WriteToPng(RenderTexture renderTex, int world_id = -1)
	{
		Texture2D texture2D = new Texture2D(renderTex.width, renderTex.height, TextureFormat.ARGB32, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)renderTex.width, (float)renderTex.height), 0, 0);
		texture2D.Apply();
		byte[] array = texture2D.EncodeToPNG();
		UnityEngine.Object.Destroy(texture2D);
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		string text = Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName());
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string text2 = RetireColonyUtility.StripInvalidCharacters(SaveGame.Instance.BaseName);
		if (!this.previewScreenshot)
		{
			string text3 = Path.Combine(text, text2);
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			string text4 = text3;
			if (world_id >= 0)
			{
				string name = ClusterManager.Instance.GetWorld(world_id).GetComponent<ClusterGridEntity>().Name;
				text4 = Path.Combine(text4, world_id.ToString("D5"));
				if (!Directory.Exists(text4))
				{
					Directory.CreateDirectory(text4);
				}
				text4 = Path.Combine(text4, name);
			}
			else
			{
				text4 = Path.Combine(text4, text2);
			}
			DebugUtil.LogArgs(new object[] { "Saving screenshot to", text4 });
			string text5 = "0000.##";
			text4 = text4 + "_cycle_" + GameClock.Instance.GetCycle().ToString(text5);
			if (this.debugScreenShot)
			{
				text4 = string.Concat(new string[]
				{
					text4,
					"_",
					System.DateTime.Now.Day.ToString(),
					"-",
					System.DateTime.Now.Month.ToString(),
					"_",
					System.DateTime.Now.Hour.ToString(),
					"-",
					System.DateTime.Now.Minute.ToString(),
					"-",
					System.DateTime.Now.Second.ToString()
				});
			}
			File.WriteAllBytes(text4 + ".png", array);
			return;
		}
		string text6 = this.previewSaveGamePath;
		text6 = Path.ChangeExtension(text6, ".png");
		DebugUtil.LogArgs(new object[] { "Saving screenshot to", text6 });
		File.WriteAllBytes(text6, array);
	}

	// Token: 0x04002B50 RID: 11088
	private bool screenshotActive;

	// Token: 0x04002B51 RID: 11089
	private bool screenshotPending;

	// Token: 0x04002B52 RID: 11090
	private bool previewScreenshot;

	// Token: 0x04002B53 RID: 11091
	private string previewSaveGamePath = "";

	// Token: 0x04002B54 RID: 11092
	private bool screenshotToday;

	// Token: 0x04002B55 RID: 11093
	private List<int> worldsToScreenshot = new List<int>();

	// Token: 0x04002B56 RID: 11094
	private HashedString activeOverlay;

	// Token: 0x04002B57 RID: 11095
	private Camera freezeCamera;

	// Token: 0x04002B58 RID: 11096
	private RenderTexture bufferRenderTexture;

	// Token: 0x04002B5A RID: 11098
	private Vector3 camPosition;

	// Token: 0x04002B5B RID: 11099
	private float camSize;

	// Token: 0x04002B5C RID: 11100
	private bool debugScreenShot;

	// Token: 0x04002B5D RID: 11101
	private Vector2Int previewScreenshotResolution = new Vector2Int(Grid.WidthInCells * 2, Grid.HeightInCells * 2);

	// Token: 0x04002B5E RID: 11102
	private const int DEFAULT_SCREENSHOT_INTERVAL = 10;

	// Token: 0x04002B5F RID: 11103
	private int[] timelapseScreenshotCycles = new int[]
	{
		1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
		11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
		21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
		31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
		41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
		55, 60, 65, 70, 75, 80, 85, 90, 95, 100,
		110, 120, 130, 140, 150, 160, 170, 180, 190, 200,
		210, 220, 230, 240, 250, 260, 270, 280, 290, 200,
		310, 320, 330, 340, 350, 360, 370, 380, 390, 400,
		410, 420, 430, 440, 450, 460, 470, 480, 490, 500
	};
}
