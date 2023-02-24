using System;
using ImGuiNET;
using STRINGS;
using UnityEngine;

// Token: 0x02000532 RID: 1330
public class DevToolWarning
{
	// Token: 0x06001FEE RID: 8174 RVA: 0x000AE4AB File Offset: 0x000AC6AB
	public DevToolWarning()
	{
		this.Name = UI.FRONTEND.DEVTOOLS.TITLE;
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x000AE4C3 File Offset: 0x000AC6C3
	public void DrawMenuBar()
	{
		if (ImGui.BeginMainMenuBar())
		{
			ImGui.Checkbox(this.Name, ref this.ShouldDrawWindow);
			ImGui.EndMainMenuBar();
		}
	}

	// Token: 0x06001FF0 RID: 8176 RVA: 0x000AE4E4 File Offset: 0x000AC6E4
	public void DrawWindow(out bool isOpen)
	{
		ImGuiWindowFlags imGuiWindowFlags = ImGuiWindowFlags.None;
		isOpen = true;
		if (ImGui.Begin(this.Name + "###ID_DevToolWarning", ref isOpen, imGuiWindowFlags))
		{
			if (!isOpen)
			{
				ImGui.End();
				return;
			}
			ImGui.SetWindowSize(new Vector2(500f, 250f));
			ImGui.TextWrapped(UI.FRONTEND.DEVTOOLS.WARNING);
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Checkbox(UI.FRONTEND.DEVTOOLS.DONTSHOW, ref this.showAgain);
			if (ImGui.Button(UI.FRONTEND.DEVTOOLS.BUTTON))
			{
				if (this.showAgain)
				{
					KPlayerPrefs.SetInt("ShowDevtools", 1);
				}
				DevToolManager.Instance.UserAcceptedWarning = true;
				isOpen = false;
			}
			ImGui.End();
		}
	}

	// Token: 0x0400123F RID: 4671
	private bool showAgain;

	// Token: 0x04001240 RID: 4672
	public string Name;

	// Token: 0x04001241 RID: 4673
	public bool ShouldDrawWindow;
}
