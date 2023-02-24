using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000801 RID: 2049
internal class LogicEventHandler : ILogicEventReceiver, ILogicNetworkConnection, ILogicUIElement, IUniformGridObject
{
	// Token: 0x06003B3C RID: 15164 RVA: 0x001488E6 File Offset: 0x00146AE6
	public LogicEventHandler(int cell, Action<int> on_value_changed, Action<int, bool> on_connection_changed, LogicPortSpriteType sprite_type)
	{
		this.cell = cell;
		this.onValueChanged = on_value_changed;
		this.onConnectionChanged = on_connection_changed;
		this.spriteType = sprite_type;
	}

	// Token: 0x06003B3D RID: 15165 RVA: 0x0014890B File Offset: 0x00146B0B
	public void ReceiveLogicEvent(int value)
	{
		this.TriggerAudio(value);
		this.value = value;
		this.onValueChanged(value);
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x06003B3E RID: 15166 RVA: 0x00148927 File Offset: 0x00146B27
	public int Value
	{
		get
		{
			return this.value;
		}
	}

	// Token: 0x06003B3F RID: 15167 RVA: 0x0014892F File Offset: 0x00146B2F
	public int GetLogicUICell()
	{
		return this.cell;
	}

	// Token: 0x06003B40 RID: 15168 RVA: 0x00148937 File Offset: 0x00146B37
	public LogicPortSpriteType GetLogicPortSpriteType()
	{
		return this.spriteType;
	}

	// Token: 0x06003B41 RID: 15169 RVA: 0x0014893F File Offset: 0x00146B3F
	public Vector2 PosMin()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06003B42 RID: 15170 RVA: 0x00148951 File Offset: 0x00146B51
	public Vector2 PosMax()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06003B43 RID: 15171 RVA: 0x00148963 File Offset: 0x00146B63
	public int GetLogicCell()
	{
		return this.cell;
	}

	// Token: 0x06003B44 RID: 15172 RVA: 0x0014896C File Offset: 0x00146B6C
	private void TriggerAudio(int new_value)
	{
		LogicCircuitNetwork networkForCell = Game.Instance.logicCircuitManager.GetNetworkForCell(this.cell);
		SpeedControlScreen instance = SpeedControlScreen.Instance;
		if (networkForCell != null && new_value != this.value && instance != null && !instance.IsPaused)
		{
			if (KPlayerPrefs.HasKey(AudioOptionsScreen.AlwaysPlayAutomation) && KPlayerPrefs.GetInt(AudioOptionsScreen.AlwaysPlayAutomation) != 1 && OverlayScreen.Instance.GetMode() != OverlayModes.Logic.ID)
			{
				return;
			}
			string text = "Logic_Building_Toggle";
			if (!CameraController.Instance.IsAudibleSound(Grid.CellToPosCCC(this.cell, Grid.SceneLayer.BuildingFront)))
			{
				return;
			}
			LogicCircuitNetwork.LogicSoundPair logicSoundPair = new LogicCircuitNetwork.LogicSoundPair();
			Dictionary<int, LogicCircuitNetwork.LogicSoundPair> logicSoundRegister = LogicCircuitNetwork.logicSoundRegister;
			int id = networkForCell.id;
			if (!logicSoundRegister.ContainsKey(id))
			{
				logicSoundRegister.Add(id, logicSoundPair);
			}
			else
			{
				logicSoundPair.playedIndex = logicSoundRegister[id].playedIndex;
				logicSoundPair.lastPlayed = logicSoundRegister[id].lastPlayed;
			}
			if (logicSoundPair.playedIndex < 2)
			{
				logicSoundRegister[id].playedIndex = logicSoundPair.playedIndex + 1;
			}
			else
			{
				logicSoundRegister[id].playedIndex = 0;
				logicSoundRegister[id].lastPlayed = Time.time;
			}
			float num = (Time.time - logicSoundPair.lastPlayed) / 3f;
			EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound(text, false), Grid.CellToPos(this.cell), 1f);
			eventInstance.setParameterByName("logic_volumeModifer", num, false);
			eventInstance.setParameterByName("wireCount", (float)(networkForCell.WireCount % 24), false);
			eventInstance.setParameterByName("enabled", (float)new_value, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x00148B1C File Offset: 0x00146D1C
	public void OnLogicNetworkConnectionChanged(bool connected)
	{
		if (this.onConnectionChanged != null)
		{
			this.onConnectionChanged(this.cell, connected);
		}
	}

	// Token: 0x040026B7 RID: 9911
	private int cell;

	// Token: 0x040026B8 RID: 9912
	private int value;

	// Token: 0x040026B9 RID: 9913
	private Action<int> onValueChanged;

	// Token: 0x040026BA RID: 9914
	private Action<int, bool> onConnectionChanged;

	// Token: 0x040026BB RID: 9915
	private LogicPortSpriteType spriteType;
}
