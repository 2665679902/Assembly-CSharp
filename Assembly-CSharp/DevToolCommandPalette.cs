using System;
using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using UnityEngine;

// Token: 0x0200051A RID: 1306
public class DevToolCommandPalette : DevTool
{
	// Token: 0x06001F65 RID: 8037 RVA: 0x000A76D9 File Offset: 0x000A58D9
	public DevToolCommandPalette()
		: this(null)
	{
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x000A76E4 File Offset: 0x000A58E4
	public DevToolCommandPalette(List<DevToolCommandPalette.Command> commands = null)
	{
		this.drawFlags |= ImGuiWindowFlags.NoResize;
		this.drawFlags |= ImGuiWindowFlags.NoScrollbar;
		this.drawFlags |= ImGuiWindowFlags.NoScrollWithMouse;
		if (commands == null)
		{
			this.commands.allValues = DevToolCommandPaletteUtil.GenerateDefaultCommandPalette();
			return;
		}
		this.commands.allValues = commands;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x000A7773 File Offset: 0x000A5973
	public static void Init()
	{
		DevToolCommandPalette.InitWithCommands(DevToolCommandPaletteUtil.GenerateDefaultCommandPalette());
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x000A777F File Offset: 0x000A597F
	public static void InitWithCommands(List<DevToolCommandPalette.Command> commands)
	{
		DevToolManager.Instance.panels.AddPanelFor(new DevToolCommandPalette(commands));
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x000A7798 File Offset: 0x000A5998
	protected override void RenderTo(DevPanel panel)
	{
		DevToolCommandPalette.Resize(panel);
		if (this.commands.allValues == null)
		{
			ImGui.Text("No commands list given");
			return;
		}
		if (this.commands.allValues.Count == 0)
		{
			ImGui.Text("Given command list is empty, no results to show.");
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			panel.Close();
			return;
		}
		if (!ImGui.IsWindowFocused(ImGuiFocusedFlags.ChildWindows))
		{
			panel.Close();
			return;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.m_selected_index--;
			this.shouldScrollToSelectedCommandFlag = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.m_selected_index++;
			this.shouldScrollToSelectedCommandFlag = true;
		}
		if (this.commands.filteredValues.Count > 0)
		{
			while (this.m_selected_index < 0)
			{
				this.m_selected_index += this.commands.filteredValues.Count;
			}
			this.m_selected_index %= this.commands.filteredValues.Count;
		}
		else
		{
			this.m_selected_index = 0;
		}
		if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)) && this.commands.filteredValues.Count > 0)
		{
			this.SelectCommand(this.commands.filteredValues[this.m_selected_index], panel);
			return;
		}
		if (this.m_should_focus_search)
		{
			ImGui.SetKeyboardFocusHere();
		}
		if (ImGui.InputText("Filter", ref this.commands.filter, 30U) || this.m_should_focus_search)
		{
			this.commands.Refilter();
		}
		this.m_should_focus_search = false;
		ImGui.Separator();
		string text = "Up arrow & down arrow to navigate. Enter to select. ";
		if (this.commands.filteredValues.Count > 0 && this.commands.didUseFilter)
		{
			text += string.Format("Found {0} Results", this.commands.filteredValues.Count);
		}
		ImGui.Text(text);
		ImGui.Separator();
		if (ImGui.BeginChild("ID_scroll_region"))
		{
			if (this.commands.filteredValues.Count <= 0)
			{
				ImGui.Text("Couldn't find anything that matches \"" + this.commands.filter + "\", maybe it hasn't been added yet?");
			}
			else
			{
				for (int i = 0; i < this.commands.filteredValues.Count; i++)
				{
					DevToolCommandPalette.Command command = this.commands.filteredValues[i];
					bool flag = i == this.m_selected_index;
					ImGui.PushID(i);
					bool flag2;
					if (flag)
					{
						flag2 = ImGui.Selectable("> " + command.display_name, flag);
					}
					else
					{
						flag2 = ImGui.Selectable("  " + command.display_name, flag);
					}
					ImGui.PopID();
					if (this.shouldScrollToSelectedCommandFlag && flag)
					{
						this.shouldScrollToSelectedCommandFlag = false;
						ImGui.SetScrollHereY(0.5f);
					}
					if (flag2)
					{
						this.SelectCommand(command, panel);
						ImGui.EndChild();
						return;
					}
				}
			}
		}
		ImGui.EndChild();
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x000A7A7C File Offset: 0x000A5C7C
	private void SelectCommand(DevToolCommandPalette.Command command, DevPanel panel)
	{
		command.Internal_Select();
		panel.Close();
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x000A7A8C File Offset: 0x000A5C8C
	private static void Resize(DevPanel devToolPanel)
	{
		float num = 800f;
		float num2 = 400f;
		Rect rect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		Rect rect2 = new Rect
		{
			x = rect.x + rect.width / 2f - num / 2f,
			y = rect.y + rect.height / 2f - num2 / 2f,
			width = num,
			height = num2
		};
		devToolPanel.SetPosition(rect2.position, ImGuiCond.None);
		devToolPanel.SetSize(rect2.size, ImGuiCond.None);
	}

	// Token: 0x040011CC RID: 4556
	private int m_selected_index;

	// Token: 0x040011CD RID: 4557
	private StringSearchableList<DevToolCommandPalette.Command> commands = new StringSearchableList<DevToolCommandPalette.Command>(delegate(DevToolCommandPalette.Command command, in string filter)
	{
		return !StringSearchableListUtil.DoAnyTagsMatchFilter(command.tags, filter);
	});

	// Token: 0x040011CE RID: 4558
	private bool m_should_focus_search = true;

	// Token: 0x040011CF RID: 4559
	private bool shouldScrollToSelectedCommandFlag;

	// Token: 0x0200114E RID: 4430
	public class Command
	{
		// Token: 0x06007616 RID: 30230 RVA: 0x002B7278 File Offset: 0x002B5478
		public Command(string primary_tag, System.Action on_select)
			: this(new string[] { primary_tag }, on_select)
		{
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x002B728B File Offset: 0x002B548B
		public Command(string primary_tag, string tag_a, System.Action on_select)
			: this(new string[] { primary_tag, tag_a }, on_select)
		{
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x002B72A2 File Offset: 0x002B54A2
		public Command(string primary_tag, string tag_a, string tag_b, System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b }, on_select)
		{
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x002B72BE File Offset: 0x002B54BE
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c }, on_select)
		{
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x002B72DF File Offset: 0x002B54DF
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, string tag_d, System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c, tag_d }, on_select)
		{
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x002B7305 File Offset: 0x002B5505
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, string tag_d, string tag_e, System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c, tag_d, tag_e }, on_select)
		{
		}

		// Token: 0x0600761C RID: 30236 RVA: 0x002B7330 File Offset: 0x002B5530
		public Command(string primary_tag, string tag_a, string tag_b, string tag_c, string tag_d, string tag_e, string tag_f, System.Action on_select)
			: this(new string[] { primary_tag, tag_a, tag_b, tag_c, tag_d, tag_e, tag_f }, on_select)
		{
		}

		// Token: 0x0600761D RID: 30237 RVA: 0x002B7360 File Offset: 0x002B5560
		public Command(string primary_tag, string[] additional_tags, System.Action on_select)
			: this(new string[] { primary_tag }.Concat(additional_tags).ToArray<string>(), on_select)
		{
		}

		// Token: 0x0600761E RID: 30238 RVA: 0x002B7380 File Offset: 0x002B5580
		public Command(string[] tags, System.Action on_select)
		{
			this.display_name = tags[0];
			this.tags = tags.Select((string t) => t.ToLowerInvariant()).ToArray<string>();
			this.m_on_select = on_select;
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x002B73D3 File Offset: 0x002B55D3
		public void Internal_Select()
		{
			this.m_on_select();
		}

		// Token: 0x04005A7A RID: 23162
		public string display_name;

		// Token: 0x04005A7B RID: 23163
		public string[] tags;

		// Token: 0x04005A7C RID: 23164
		private System.Action m_on_select;
	}
}
