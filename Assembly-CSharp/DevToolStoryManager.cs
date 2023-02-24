using System;
using System.Collections.Generic;
using ImGuiNET;

// Token: 0x0200052D RID: 1325
public class DevToolStoryManager : DevTool
{
	// Token: 0x06001FD1 RID: 8145 RVA: 0x000AD930 File Offset: 0x000ABB30
	protected override void RenderTo(DevPanel panel)
	{
		if (ImGui.CollapsingHeader("Story Instance Data", ImGuiTreeNodeFlags.DefaultOpen))
		{
			this.DrawStoryInstanceData();
		}
		ImGui.Spacing();
		if (ImGui.CollapsingHeader("Story Telemetry Data", ImGuiTreeNodeFlags.DefaultOpen))
		{
			this.DrawTelemetryData();
		}
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x000AD960 File Offset: 0x000ABB60
	private void DrawStoryInstanceData()
	{
		if (StoryManager.Instance == null)
		{
			ImGui.Text("Couldn't find StoryManager instance");
			return;
		}
		ImGui.Text(string.Format("Stories (count: {0})", StoryManager.Instance.GetStoryInstances().Count));
		string text = ((StoryManager.Instance.GetHighestCoordinate() == -2) ? "Before stories" : StoryManager.Instance.GetHighestCoordinate().ToString());
		ImGui.Text("Highest generated: " + text);
		foreach (KeyValuePair<int, StoryInstance> keyValuePair in StoryManager.Instance.GetStoryInstances())
		{
			ImGui.Text(" - " + keyValuePair.Value.storyId + ": " + keyValuePair.Value.CurrentState.ToString());
		}
		if (StoryManager.Instance.GetStoryInstances().Count == 0)
		{
			ImGui.Text(" - No stories");
		}
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x000ADA7C File Offset: 0x000ABC7C
	private void DrawTelemetryData()
	{
		ImGuiEx.DrawObjectTable<StoryManager.StoryTelemetry>("ID_telemetry", StoryManager.GetTelemetry(), null);
	}
}
