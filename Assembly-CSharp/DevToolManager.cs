using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ImGuiNET;
using STRINGS;
using UnityEngine;

// Token: 0x0200051E RID: 1310
public class DevToolManager
{
	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000A965D File Offset: 0x000A785D
	public bool Show
	{
		get
		{
			return this.showImGui;
		}
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x000A9668 File Offset: 0x000A7868
	public DevToolManager()
	{
		DevToolManager.Instance = this;
		this.RegisterDevTool<DevToolSimDebug>("Debuggers/Sim Debug");
		this.RegisterDevTool<DevToolStateMachineDebug>("Debuggers/State Machine");
		this.RegisterDevTool<DevToolSaveGameInfo>("Debuggers/Save Game Info");
		this.RegisterDevTool<DevToolPrintingPodDebug>("Debuggers/Printing Pod Debug");
		this.RegisterDevTool<DevToolBigBaseMutations>("Debuggers/Big Base Mutation Utilities");
		this.RegisterDevTool<DevToolNavGrid>("Debuggers/Nav Grid");
		this.RegisterDevTool<DevToolResearchDebugger>("Debuggers/Research");
		this.RegisterDevTool<DevToolStatusItems>("Debuggers/StatusItems");
		this.RegisterDevTool<DevToolUI>("Debuggers/UI");
		this.RegisterDevTool<DevToolUnlockedIds>("Debuggers/UnlockedIds List");
		this.RegisterDevTool<DevToolStringsTable>("Debuggers/StringsTable");
		this.RegisterDevTool<DevToolChoreDebugger>("Debuggers/Chore");
		this.RegisterDevTool<DevToolBatchedAnimDebug>("Debuggers/Batched Anim");
		this.RegisterDevTool<DevTool_StoryTraits_Reveal>("Debuggers/Story Traits Reveal");
		this.RegisterDevTool<DevTool_StoryTrait_CritterManipulator>("Debuggers/Story Trait - Critter Manipulator");
		this.RegisterDevTool<DevToolAnimEventManager>("Debuggers/Anim Event Manager");
		this.RegisterDevTool<DevToolSceneBrowser>("Scene/Browser");
		this.RegisterDevTool<DevToolSceneInspector>("Scene/Inspector");
		this.menuNodes.AddAction("Help/" + UI.FRONTEND.DEVTOOLS.TITLE.text, delegate
		{
			this.warning.ShouldDrawWindow = true;
		});
		this.RegisterDevTool<DevToolCommandPalette>("Help/Command Palette");
		this.RegisterAdditionalDevToolsByReflection();
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x000A97C6 File Offset: 0x000A79C6
	public void Init()
	{
		this.UserAcceptedWarning = KPlayerPrefs.GetInt("ShowDevtools", 0) == 1;
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x000A97DC File Offset: 0x000A79DC
	private void RegisterDevTool<T>(string location) where T : DevTool, new()
	{
		this.menuNodes.AddAction(location, delegate
		{
			this.panels.AddPanelFor<T>();
		});
		this.dontAutomaticallyRegisterTypes.Add(typeof(T));
		this.devToolNameDict[typeof(T)] = Path.GetFileName(location);
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x000A9834 File Offset: 0x000A7A34
	private void RegisterAdditionalDevToolsByReflection()
	{
		using (List<Type>.Enumerator enumerator = ReflectionUtil.CollectTypesThatInheritOrImplement<DevTool>(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Type type = enumerator.Current;
				if (!type.IsAbstract && !this.dontAutomaticallyRegisterTypes.Contains(type) && ReflectionUtil.HasDefaultConstructor(type))
				{
					this.menuNodes.AddAction("Debuggers/" + DevToolUtil.GenerateDevToolName(type), delegate
					{
						this.panels.AddPanelFor((DevTool)Activator.CreateInstance(type));
					});
				}
			}
		}
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x000A98F0 File Offset: 0x000A7AF0
	public void UpdateShouldShowTools()
	{
		if (!DebugHandler.enabled)
		{
			this.showImGui = false;
			return;
		}
		bool flag = Input.GetKeyDown(KeyCode.BackQuote) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl));
		if (!this.toggleKeyWasDown && flag)
		{
			this.showImGui = !this.showImGui;
		}
		this.toggleKeyWasDown = flag;
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x000A9958 File Offset: 0x000A7B58
	public void UpdateTools()
	{
		if (!DebugHandler.enabled)
		{
			return;
		}
		if (this.showImGui)
		{
			if (this.warning.ShouldDrawWindow)
			{
				this.warning.DrawWindow(out this.warning.ShouldDrawWindow);
			}
			if (!this.UserAcceptedWarning)
			{
				this.warning.DrawMenuBar();
			}
			else
			{
				this.DrawMenu();
				this.panels.Render();
				if (this.showImguiState)
				{
					if (ImGui.Begin("ImGui state", ref this.showImguiState))
					{
						ImGui.Checkbox("ImGui.GetIO().WantCaptureMouse", ImGui.GetIO().WantCaptureMouse);
						ImGui.Checkbox("ImGui.GetIO().WantCaptureKeyboard", ImGui.GetIO().WantCaptureKeyboard);
					}
					ImGui.End();
				}
				if (this.showImguiDemo)
				{
					ImGui.ShowDemoWindow(ref this.showImguiDemo);
				}
			}
		}
		this.UpdateConsumingGameInputs();
		this.UpdateShortcuts();
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x000A9A2F File Offset: 0x000A7C2F
	private void UpdateShortcuts()
	{
		if (this.showImGui && this.UserAcceptedWarning)
		{
			this.<UpdateShortcuts>g__DoUpdate|24_0();
		}
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x000A9A48 File Offset: 0x000A7C48
	private void DrawMenu()
	{
		this.menuFontSize.InitializeIfNeeded();
		if (ImGui.BeginMainMenuBar())
		{
			this.menuNodes.Draw();
			this.menuFontSize.DrawMenu();
			if (ImGui.BeginMenu("IMGUI"))
			{
				ImGui.Checkbox("ImGui state", ref this.showImguiState);
				ImGui.Checkbox("ImGui Demo", ref this.showImguiDemo);
				ImGui.EndMenu();
			}
			ImGui.EndMainMenuBar();
		}
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x000A9AB8 File Offset: 0x000A7CB8
	private unsafe void UpdateConsumingGameInputs()
	{
		this.doesImGuiWantInput = false;
		if (this.showImGui)
		{
			this.doesImGuiWantInput = *ImGui.GetIO().WantCaptureMouse || *ImGui.GetIO().WantCaptureKeyboard;
			if (!this.prevDoesImGuiWantInput && this.doesImGuiWantInput)
			{
				DevToolManager.<UpdateConsumingGameInputs>g__OnInputEnterImGui|26_0();
			}
			if (this.prevDoesImGuiWantInput && !this.doesImGuiWantInput)
			{
				DevToolManager.<UpdateConsumingGameInputs>g__OnInputExitImGui|26_1();
			}
		}
		if (this.prevShowImGui && this.prevDoesImGuiWantInput && !this.showImGui)
		{
			DevToolManager.<UpdateConsumingGameInputs>g__OnInputExitImGui|26_1();
		}
		this.prevShowImGui = this.showImGui;
		this.prevDoesImGuiWantInput = this.doesImGuiWantInput;
		KInputManager.devToolFocus = this.showImGui && this.doesImGuiWantInput;
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x000A9B8C File Offset: 0x000A7D8C
	[CompilerGenerated]
	private void <UpdateShortcuts>g__DoUpdate|24_0()
	{
		if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Space))
		{
			DevToolCommandPalette.Init();
			this.showImGui = true;
		}
		if (Input.GetKeyDown(KeyCode.Comma))
		{
			DevToolUI.PingHoveredObject();
			this.showImGui = true;
		}
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x000A9BDC File Offset: 0x000A7DDC
	[CompilerGenerated]
	internal static void <UpdateConsumingGameInputs>g__OnInputEnterImGui|26_0()
	{
		UnityMouseCatcherUI.SetEnabled(true);
		GameInputManager inputManager = Global.GetInputManager();
		for (int i = 0; i < inputManager.GetControllerCount(); i++)
		{
			inputManager.GetController(i).HandleCancelInput();
		}
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x000A9C12 File Offset: 0x000A7E12
	[CompilerGenerated]
	internal static void <UpdateConsumingGameInputs>g__OnInputExitImGui|26_1()
	{
		UnityMouseCatcherUI.SetEnabled(false);
	}

	// Token: 0x040011F0 RID: 4592
	public const string SHOW_DEVTOOLS = "ShowDevtools";

	// Token: 0x040011F1 RID: 4593
	public static DevToolManager Instance;

	// Token: 0x040011F2 RID: 4594
	private bool toggleKeyWasDown;

	// Token: 0x040011F3 RID: 4595
	private bool showImGui;

	// Token: 0x040011F4 RID: 4596
	private bool prevShowImGui;

	// Token: 0x040011F5 RID: 4597
	private bool doesImGuiWantInput;

	// Token: 0x040011F6 RID: 4598
	private bool prevDoesImGuiWantInput;

	// Token: 0x040011F7 RID: 4599
	private bool showImguiState;

	// Token: 0x040011F8 RID: 4600
	private bool showImguiDemo;

	// Token: 0x040011F9 RID: 4601
	public bool UserAcceptedWarning;

	// Token: 0x040011FA RID: 4602
	private DevToolWarning warning = new DevToolWarning();

	// Token: 0x040011FB RID: 4603
	private DevToolMenuFontSize menuFontSize = new DevToolMenuFontSize();

	// Token: 0x040011FC RID: 4604
	public DevPanelList panels = new DevPanelList();

	// Token: 0x040011FD RID: 4605
	public DevToolMenuNodeList menuNodes = new DevToolMenuNodeList();

	// Token: 0x040011FE RID: 4606
	public Dictionary<Type, string> devToolNameDict = new Dictionary<Type, string>();

	// Token: 0x040011FF RID: 4607
	private HashSet<Type> dontAutomaticallyRegisterTypes = new HashSet<Type>();
}
