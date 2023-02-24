using System;
using ImGuiNET;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class DevToolPrintingPodDebug : DevTool
{
	// Token: 0x06001FA2 RID: 8098 RVA: 0x000AA285 File Offset: 0x000A8485
	protected override void RenderTo(DevPanel panel)
	{
		if (Immigration.Instance != null)
		{
			this.ShowButtons();
			return;
		}
		ImGui.Text("Game not available");
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x000AA2A8 File Offset: 0x000A84A8
	private void ShowButtons()
	{
		if (Components.Telepads.Count == 0)
		{
			ImGui.Text("No printing pods available");
			return;
		}
		ImGui.Text("Time until next print available: " + Mathf.CeilToInt(Immigration.Instance.timeBeforeSpawn).ToString() + "s");
		if (ImGui.Button("Activate now"))
		{
			Immigration.Instance.timeBeforeSpawn = 0f;
		}
		if (ImGui.Button("Shuffle Options"))
		{
			if (ImmigrantScreen.instance.Telepad == null)
			{
				ImmigrantScreen.InitializeImmigrantScreen(Components.Telepads[0]);
				return;
			}
			ImmigrantScreen.instance.DebugShuffleOptions();
		}
	}
}
