using System;
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public class DevPanel
{
	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06001F29 RID: 7977 RVA: 0x000A5B9A File Offset: 0x000A3D9A
	// (set) Token: 0x06001F2A RID: 7978 RVA: 0x000A5BA2 File Offset: 0x000A3DA2
	public bool isRequestingToClose { get; private set; }

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06001F2B RID: 7979 RVA: 0x000A5BAB File Offset: 0x000A3DAB
	// (set) Token: 0x06001F2C RID: 7980 RVA: 0x000A5BB3 File Offset: 0x000A3DB3
	public Option<ValueTuple<Vector2, ImGuiCond>> nextImGuiWindowPosition { get; private set; }

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06001F2D RID: 7981 RVA: 0x000A5BBC File Offset: 0x000A3DBC
	// (set) Token: 0x06001F2E RID: 7982 RVA: 0x000A5BC4 File Offset: 0x000A3DC4
	public Option<ValueTuple<Vector2, ImGuiCond>> nextImGuiWindowSize { get; private set; }

	// Token: 0x06001F2F RID: 7983 RVA: 0x000A5BD0 File Offset: 0x000A3DD0
	public DevPanel(DevTool devTool, DevPanelList manager)
	{
		this.manager = manager;
		this.devTools = new List<DevTool>();
		this.devTools.Add(devTool);
		this.currentDevToolIndex = 0;
		this.initialDevToolType = devTool.GetType();
		manager.Internal_InitPanelId(this.initialDevToolType, out this.uniquePanelId, out this.idPostfixNumber);
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x000A5C2C File Offset: 0x000A3E2C
	public void PushValue<T>(T value) where T : class
	{
		this.PushDevTool(new DevToolObjectViewer<T>(() => value));
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x000A5C5D File Offset: 0x000A3E5D
	public void PushValue<T>(Func<T> value)
	{
		this.PushDevTool(new DevToolObjectViewer<T>(value));
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x000A5C6B File Offset: 0x000A3E6B
	public void PushDevTool<T>() where T : DevTool, new()
	{
		this.PushDevTool(new T());
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x000A5C80 File Offset: 0x000A3E80
	public void PushDevTool(DevTool devTool)
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			this.manager.AddPanelFor(devTool);
			return;
		}
		for (int i = this.devTools.Count - 1; i > this.currentDevToolIndex; i--)
		{
			this.devTools[i].Internal_Uninit();
			this.devTools.RemoveAt(i);
		}
		this.devTools.Add(devTool);
		this.currentDevToolIndex = this.devTools.Count - 1;
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x000A5D00 File Offset: 0x000A3F00
	public DevTool GetCurrentDevTool()
	{
		return this.devTools[this.currentDevToolIndex];
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x000A5D14 File Offset: 0x000A3F14
	public Option<int> TryGetDevToolIndexByOffset(int offsetFromCurrentIndex)
	{
		int num = this.currentDevToolIndex + offsetFromCurrentIndex;
		if (num < 0)
		{
			return Option.None;
		}
		if (num >= this.devTools.Count)
		{
			return Option.None;
		}
		return num;
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x000A5D58 File Offset: 0x000A3F58
	public void RenderPanel()
	{
		DevTool currentDevTool = this.GetCurrentDevTool();
		currentDevTool.Internal_TryInit();
		if (currentDevTool.isRequestingToClosePanel)
		{
			this.isRequestingToClose = true;
			return;
		}
		ImGuiWindowFlags imGuiWindowFlags;
		this.ConfigureImGuiWindowFor(currentDevTool, out imGuiWindowFlags);
		bool flag = true;
		if (ImGui.Begin(currentDevTool.Name + "###ID_" + this.uniquePanelId, ref flag, imGuiWindowFlags))
		{
			if (!flag)
			{
				this.isRequestingToClose = true;
				ImGui.End();
				return;
			}
			if (ImGui.BeginMenuBar())
			{
				this.DrawNavigation();
				ImGui.SameLine(0f, 20f);
				this.DrawMenuBarContents();
				ImGui.EndMenuBar();
			}
			currentDevTool.DoImGui(this);
			if (this.GetCurrentDevTool() != currentDevTool)
			{
				ImGui.SetScrollY(0f);
			}
		}
		ImGui.End();
		if (this.GetCurrentDevTool().isRequestingToClosePanel)
		{
			this.isRequestingToClose = true;
		}
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x000A5E18 File Offset: 0x000A4018
	private void DrawNavigation()
	{
		Option<int> option = this.TryGetDevToolIndexByOffset(-1);
		if (ImGuiEx.Button(" < ", option.IsSome()))
		{
			this.currentDevToolIndex = option.Unwrap();
		}
		if (option.IsSome())
		{
			ImGuiEx.TooltipForPrevious("Go back to " + this.devTools[option.Unwrap()].Name);
		}
		else
		{
			ImGuiEx.TooltipForPrevious("Go back");
		}
		ImGui.SameLine(0f, 5f);
		Option<int> option2 = this.TryGetDevToolIndexByOffset(1);
		if (ImGuiEx.Button(" > ", option2.IsSome()))
		{
			this.currentDevToolIndex = option2.Unwrap();
		}
		if (option2.IsSome())
		{
			ImGuiEx.TooltipForPrevious("Go forward to " + this.devTools[option2.Unwrap()].Name);
			return;
		}
		ImGuiEx.TooltipForPrevious("Go forward");
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x000A5EF9 File Offset: 0x000A40F9
	private void DrawMenuBarContents()
	{
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x000A5EFC File Offset: 0x000A40FC
	private void ConfigureImGuiWindowFor(DevTool currentDevTool, out ImGuiWindowFlags drawFlags)
	{
		drawFlags = ImGuiWindowFlags.MenuBar | currentDevTool.drawFlags;
		if (this.nextImGuiWindowPosition.HasValue)
		{
			ValueTuple<Vector2, ImGuiCond> value = this.nextImGuiWindowPosition.Value;
			Vector2 item = value.Item1;
			ImGuiCond item2 = value.Item2;
			ImGui.SetNextWindowPos(item, item2);
			this.nextImGuiWindowPosition = default(Option<ValueTuple<Vector2, ImGuiCond>>);
		}
		if (this.nextImGuiWindowSize.HasValue)
		{
			Vector2 item3 = this.nextImGuiWindowSize.Value.Item1;
			ImGui.SetNextWindowSize(item3);
			this.nextImGuiWindowSize = default(Option<ValueTuple<Vector2, ImGuiCond>>);
		}
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x000A5F93 File Offset: 0x000A4193
	public void SetPosition(Vector2 position, ImGuiCond condition = ImGuiCond.None)
	{
		this.nextImGuiWindowPosition = new ValueTuple<Vector2, ImGuiCond>(position, condition);
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x000A5FA7 File Offset: 0x000A41A7
	public void SetSize(Vector2 size, ImGuiCond condition = ImGuiCond.None)
	{
		this.nextImGuiWindowSize = new ValueTuple<Vector2, ImGuiCond>(size, condition);
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x000A5FBB File Offset: 0x000A41BB
	public void Close()
	{
		this.isRequestingToClose = true;
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x000A5FC4 File Offset: 0x000A41C4
	public void Internal_Uninit()
	{
		foreach (DevTool devTool in this.devTools)
		{
			devTool.Internal_Uninit();
		}
	}

	// Token: 0x040011B1 RID: 4529
	public readonly string uniquePanelId;

	// Token: 0x040011B2 RID: 4530
	public readonly DevPanelList manager;

	// Token: 0x040011B3 RID: 4531
	public readonly Type initialDevToolType;

	// Token: 0x040011B4 RID: 4532
	public readonly uint idPostfixNumber;

	// Token: 0x040011B5 RID: 4533
	private List<DevTool> devTools;

	// Token: 0x040011B6 RID: 4534
	private int currentDevToolIndex;
}
