using System;
using System.Collections.Generic;
using Klei;
using ProcGen;

// Token: 0x020006FE RID: 1790
public static class TemplateCache
{
	// Token: 0x17000384 RID: 900
	// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00103EE8 File Offset: 0x001020E8
	// (set) Token: 0x060030F3 RID: 12531 RVA: 0x00103EEF File Offset: 0x001020EF
	public static bool Initted { get; private set; }

	// Token: 0x060030F4 RID: 12532 RVA: 0x00103EF7 File Offset: 0x001020F7
	public static void Init()
	{
		if (TemplateCache.Initted)
		{
			return;
		}
		TemplateCache.templates = new Dictionary<string, TemplateContainer>();
		TemplateCache.Initted = true;
	}

	// Token: 0x060030F5 RID: 12533 RVA: 0x00103F11 File Offset: 0x00102111
	public static void Clear()
	{
		TemplateCache.templates = null;
		TemplateCache.Initted = false;
	}

	// Token: 0x060030F6 RID: 12534 RVA: 0x00103F20 File Offset: 0x00102120
	public static string RewriteTemplatePath(string scopePath)
	{
		string text;
		string text2;
		SettingsCache.GetDlcIdAndPath(scopePath, out text, out text2);
		return SettingsCache.GetAbsoluteContentPath(text, "templates/" + text2);
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x00103F48 File Offset: 0x00102148
	public static string RewriteTemplateYaml(string scopePath)
	{
		return TemplateCache.RewriteTemplatePath(scopePath) + ".yaml";
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x00103F5C File Offset: 0x0010215C
	public static TemplateContainer GetTemplate(string templatePath)
	{
		if (!TemplateCache.templates.ContainsKey(templatePath))
		{
			TemplateCache.templates.Add(templatePath, null);
		}
		if (TemplateCache.templates[templatePath] == null)
		{
			string text = TemplateCache.RewriteTemplateYaml(templatePath);
			TemplateContainer templateContainer = YamlIO.LoadFile<TemplateContainer>(text, null, null);
			if (templateContainer == null)
			{
				Debug.LogWarning("Missing template [" + text + "]");
			}
			templateContainer.name = templatePath;
			TemplateCache.templates[templatePath] = templateContainer;
		}
		return TemplateCache.templates[templatePath];
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x00103FD5 File Offset: 0x001021D5
	public static bool TemplateExists(string templatePath)
	{
		return FileSystem.FileExists(TemplateCache.RewriteTemplateYaml(templatePath));
	}

	// Token: 0x04001D92 RID: 7570
	private const string defaultAssetFolder = "bases";

	// Token: 0x04001D93 RID: 7571
	private static Dictionary<string, TemplateContainer> templates;
}
