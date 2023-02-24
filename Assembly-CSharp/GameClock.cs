using System;
using System.IO;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x0200077F RID: 1919
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/GameClock")]
public class GameClock : KMonoBehaviour, ISaveLoadable, ISim33ms, IRender1000ms
{
	// Token: 0x0600351B RID: 13595 RVA: 0x0011FE1E File Offset: 0x0011E01E
	public static void DestroyInstance()
	{
		GameClock.Instance = null;
	}

	// Token: 0x0600351C RID: 13596 RVA: 0x0011FE26 File Offset: 0x0011E026
	protected override void OnPrefabInit()
	{
		GameClock.Instance = this;
		this.timeSinceStartOfCycle = 50f;
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x0011FE3C File Offset: 0x0011E03C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.time != 0f)
		{
			this.cycle = (int)(this.time / 600f);
			this.timeSinceStartOfCycle = Mathf.Max(this.time - (float)this.cycle * 600f, 0f);
			this.time = 0f;
		}
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x0011FE98 File Offset: 0x0011E098
	public void Sim33ms(float dt)
	{
		this.AddTime(dt);
	}

	// Token: 0x0600351F RID: 13599 RVA: 0x0011FEA1 File Offset: 0x0011E0A1
	public void Render1000ms(float dt)
	{
		this.timePlayed += dt;
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x0011FEB1 File Offset: 0x0011E0B1
	private void LateUpdate()
	{
		this.frame++;
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x0011FEC4 File Offset: 0x0011E0C4
	private void AddTime(float dt)
	{
		this.timeSinceStartOfCycle += dt;
		bool flag = false;
		while (this.timeSinceStartOfCycle >= 600f)
		{
			this.cycle++;
			this.timeSinceStartOfCycle -= 600f;
			base.Trigger(631075836, null);
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				worldContainer.Trigger(631075836, null);
			}
			flag = true;
		}
		if (!this.isNight && this.IsNighttime())
		{
			this.isNight = true;
			base.Trigger(-722330267, null);
		}
		if (this.isNight && !this.IsNighttime())
		{
			this.isNight = false;
		}
		if (flag && SaveGame.Instance.AutoSaveCycleInterval > 0 && this.cycle % SaveGame.Instance.AutoSaveCycleInterval == 0)
		{
			this.DoAutoSave(this.cycle);
		}
		int num = Mathf.FloorToInt(this.timeSinceStartOfCycle - dt / 25f);
		int num2 = Mathf.FloorToInt(this.timeSinceStartOfCycle / 25f);
		if (num != num2)
		{
			base.Trigger(-1215042067, num2);
		}
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x00120008 File Offset: 0x0011E208
	public float GetTimeSinceStartOfReport()
	{
		if (this.IsNighttime())
		{
			return 525f - this.GetTimeSinceStartOfCycle();
		}
		return this.GetTimeSinceStartOfCycle() + 75f;
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x0012002B File Offset: 0x0011E22B
	public float GetTimeSinceStartOfCycle()
	{
		return this.timeSinceStartOfCycle;
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x00120033 File Offset: 0x0011E233
	public float GetCurrentCycleAsPercentage()
	{
		return this.timeSinceStartOfCycle / 600f;
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x00120041 File Offset: 0x0011E241
	public float GetTime()
	{
		return this.timeSinceStartOfCycle + (float)this.cycle * 600f;
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x00120057 File Offset: 0x0011E257
	public float GetTimeInCycles()
	{
		return (float)this.cycle + this.GetCurrentCycleAsPercentage();
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x00120067 File Offset: 0x0011E267
	public int GetFrame()
	{
		return this.frame;
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x0012006F File Offset: 0x0011E26F
	public int GetCycle()
	{
		return this.cycle;
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x00120077 File Offset: 0x0011E277
	public bool IsNighttime()
	{
		return GameClock.Instance.GetCurrentCycleAsPercentage() >= 0.875f;
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x0012008D File Offset: 0x0011E28D
	public float GetDaytimeDurationInPercentage()
	{
		return 0.875f;
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x00120094 File Offset: 0x0011E294
	public void SetTime(float new_time)
	{
		float num = Mathf.Max(new_time - this.GetTime(), 0f);
		this.AddTime(num);
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x001200BB File Offset: 0x0011E2BB
	public float GetTimePlayedInSeconds()
	{
		return this.timePlayed;
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x001200C4 File Offset: 0x0011E2C4
	private void DoAutoSave(int day)
	{
		if (GenericGameSettings.instance.disableAutosave)
		{
			return;
		}
		day++;
		OniMetrics.LogEvent(OniMetrics.Event.EndOfCycle, GameClock.NewCycleKey, day);
		OniMetrics.SendEvent(OniMetrics.Event.EndOfCycle, "DoAutoSave");
		string text = SaveLoader.GetActiveSaveFilePath();
		if (text == null)
		{
			text = SaveLoader.GetAutosaveFilePath();
		}
		int num = text.LastIndexOf("\\");
		if (num > 0)
		{
			int num2 = text.IndexOf(" Cycle ", num);
			if (num2 > 0)
			{
				text = text.Substring(0, num2);
			}
		}
		text = Path.ChangeExtension(text, null);
		text = text + " Cycle " + day.ToString();
		text = SaveScreen.GetValidSaveFilename(text);
		text = Path.Combine(SaveLoader.GetActiveAutoSavePath(), Path.GetFileName(text));
		string text2 = text;
		int num3 = 1;
		while (File.Exists(text))
		{
			text = text2.Replace(".sav", "");
			text = SaveScreen.GetValidSaveFilename(text2 + " (" + num3.ToString() + ")");
			num3++;
		}
		Game.Instance.StartDelayedSave(text, true, false);
	}

	// Token: 0x0400212A RID: 8490
	public static GameClock Instance;

	// Token: 0x0400212B RID: 8491
	[Serialize]
	private int frame;

	// Token: 0x0400212C RID: 8492
	[Serialize]
	private float time;

	// Token: 0x0400212D RID: 8493
	[Serialize]
	private float timeSinceStartOfCycle;

	// Token: 0x0400212E RID: 8494
	[Serialize]
	private int cycle;

	// Token: 0x0400212F RID: 8495
	[Serialize]
	private float timePlayed;

	// Token: 0x04002130 RID: 8496
	[Serialize]
	private bool isNight;

	// Token: 0x04002131 RID: 8497
	public static readonly string NewCycleKey = "NewCycle";
}
