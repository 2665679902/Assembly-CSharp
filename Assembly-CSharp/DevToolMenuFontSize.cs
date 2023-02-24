using System;
using ImGuiNET;

// Token: 0x0200051C RID: 1308
public class DevToolMenuFontSize
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000A7BF9 File Offset: 0x000A5DF9
	// (set) Token: 0x06001F6D RID: 8045 RVA: 0x000A7BF0 File Offset: 0x000A5DF0
	public bool initialized { get; private set; }

	// Token: 0x06001F6F RID: 8047 RVA: 0x000A7C04 File Offset: 0x000A5E04
	public void RefreshFontSize()
	{
		DevToolMenuFontSize.FontSizeCategory @int = (DevToolMenuFontSize.FontSizeCategory)KPlayerPrefs.GetInt("Imgui_font_size_category", 2);
		this.SetFontSizeCategory(@int);
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x000A7C24 File Offset: 0x000A5E24
	public void InitializeIfNeeded()
	{
		if (!this.initialized)
		{
			this.initialized = true;
			this.RefreshFontSize();
		}
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x000A7C3C File Offset: 0x000A5E3C
	public void DrawMenu()
	{
		if (ImGui.BeginMenu("Settings"))
		{
			bool flag = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Fabric;
			bool flag2 = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Small;
			bool flag3 = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Regular;
			bool flag4 = this.fontSizeCategory == DevToolMenuFontSize.FontSizeCategory.Large;
			if (ImGui.BeginMenu("Size"))
			{
				if (ImGui.Checkbox("Original Font", ref flag) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Fabric)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Fabric);
				}
				if (ImGui.Checkbox("Small Text", ref flag2) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Small)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Small);
				}
				if (ImGui.Checkbox("Regular Text", ref flag3) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Regular)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Regular);
				}
				if (ImGui.Checkbox("Large Text", ref flag4) && this.fontSizeCategory != DevToolMenuFontSize.FontSizeCategory.Large)
				{
					this.SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory.Large);
				}
				ImGui.EndMenu();
			}
			ImGui.EndMenu();
		}
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x000A7D10 File Offset: 0x000A5F10
	public unsafe void SetFontSizeCategory(DevToolMenuFontSize.FontSizeCategory size)
	{
		this.fontSizeCategory = size;
		KPlayerPrefs.SetInt("Imgui_font_size_category", (int)size);
		ImGuiIOPtr io = ImGui.GetIO();
		if (size < (DevToolMenuFontSize.FontSizeCategory)io.Fonts.Fonts.Size)
		{
			ImFontPtr imFontPtr = *io.Fonts.Fonts[(int)size];
			io.NativePtr->FontDefault = imFontPtr;
		}
	}

	// Token: 0x040011D0 RID: 4560
	public const string SETTINGS_KEY_FONT_SIZE_CATEGORY = "Imgui_font_size_category";

	// Token: 0x040011D1 RID: 4561
	private DevToolMenuFontSize.FontSizeCategory fontSizeCategory;

	// Token: 0x02001151 RID: 4433
	public enum FontSizeCategory
	{
		// Token: 0x04005A81 RID: 23169
		Fabric,
		// Token: 0x04005A82 RID: 23170
		Small,
		// Token: 0x04005A83 RID: 23171
		Regular,
		// Token: 0x04005A84 RID: 23172
		Large
	}
}
