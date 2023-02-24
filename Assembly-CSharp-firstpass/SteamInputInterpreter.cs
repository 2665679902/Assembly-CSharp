using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class SteamInputInterpreter
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x0600026D RID: 621 RVA: 0x0000D260 File Offset: 0x0000B460
	public int NumOfISteamInputs
	{
		get
		{
			return this.m_nInputs;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x0600026E RID: 622 RVA: 0x0000D268 File Offset: 0x0000B468
	public bool Initialized
	{
		get
		{
			return this.m_InputInitialized;
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000D270 File Offset: 0x0000B470
	public void OnEnable()
	{
		this.m_InputInitialized = DistributionPlatform.Initialized && SteamInput.Init(true);
		if (this.m_InputInitialized)
		{
			this.m_InputHandles = new InputHandle_t[16];
			this.Precache();
		}
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000D2A3 File Offset: 0x0000B4A3
	private void OnDisable()
	{
		if (this.m_InputInitialized)
		{
			SteamInput.Shutdown();
		}
		this.m_InputInitialized = false;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000D2BC File Offset: 0x0000B4BC
	public void Precache()
	{
		this.m_ActionSetNames = Enum.GetNames(typeof(SteamInputInterpreter.EActionSets));
		this.m_numActionSets = this.m_ActionSetNames.Length;
		this.m_ActionSetHandles = new InputActionSetHandle_t[this.m_numActionSets];
		for (int i = 0; i < this.m_numActionSets; i++)
		{
			this.m_ActionSetHandles[i] = SteamInput.GetActionSetHandle(this.m_ActionSetNames[i]);
		}
		this.m_MainGameActionSetAnalogActionNames = Enum.GetNames(typeof(SteamInputInterpreter.EAnalogActions_MainGameActionSet));
		this.m_numMainGameActionSetAnalogActions = this.m_MainGameActionSetAnalogActionNames.Length;
		this.m_MainGameActionSetAnalogActionHandles = new InputAnalogActionHandle_t[this.m_numMainGameActionSetAnalogActions];
		for (int j = 0; j < this.m_numMainGameActionSetAnalogActions; j++)
		{
			this.m_MainGameActionSetAnalogActionHandles[j] = SteamInput.GetAnalogActionHandle(this.m_MainGameActionSetAnalogActionNames[j]);
		}
		this.m_MainGameActionSetDigitalActionNames = Enum.GetNames(typeof(SteamInputInterpreter.EDigitalActions_MainGameActionSet));
		this.m_numMainGameActionSetDigitalActions = this.m_MainGameActionSetDigitalActionNames.Length;
		this.m_MainGameActionSetDigitalActionHandles = new InputDigitalActionHandle_t[this.m_numMainGameActionSetDigitalActions];
		for (int k = 0; k < this.m_numMainGameActionSetDigitalActions; k++)
		{
			this.m_MainGameActionSetDigitalActionHandles[k] = SteamInput.GetDigitalActionHandle(this.m_MainGameActionSetDigitalActionNames[k]);
		}
		if (this.kleiActionToSteamDigitalActionLookup.Count < 1)
		{
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.MouseLeft, SteamInputInterpreter.EDigitalActions_MainGameActionSet.affirmative_click);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.MouseRight, SteamInputInterpreter.EDigitalActions_MainGameActionSet.negative_click);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.CameraHome, SteamInputInterpreter.EDigitalActions_MainGameActionSet.camera_home);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ZoomIn, SteamInputInterpreter.EDigitalActions_MainGameActionSet.camera_zoom_in_scroll_down);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ZoomOut, SteamInputInterpreter.EDigitalActions_MainGameActionSet.camera_zoom_out_scroll_up);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.TogglePause, SteamInputInterpreter.EDigitalActions_MainGameActionSet.sim_pause);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.CycleSpeed, SteamInputInterpreter.EDigitalActions_MainGameActionSet.sim_cycle_speed);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.RotateBuilding, SteamInputInterpreter.EDigitalActions_MainGameActionSet.rotate_building);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.CopyBuilding, SteamInputInterpreter.EDigitalActions_MainGameActionSet.copy_building);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Dig, SteamInputInterpreter.EDigitalActions_MainGameActionSet.dig_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.BuildingCancel, SteamInputInterpreter.EDigitalActions_MainGameActionSet.cancel_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.BuildingDeconstruct, SteamInputInterpreter.EDigitalActions_MainGameActionSet.deconstruct_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Prioritize, SteamInputInterpreter.EDigitalActions_MainGameActionSet.priority_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Disinfect, SteamInputInterpreter.EDigitalActions_MainGameActionSet.disinfect_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Clear, SteamInputInterpreter.EDigitalActions_MainGameActionSet.sweep_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Mop, SteamInputInterpreter.EDigitalActions_MainGameActionSet.mop_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Attack, SteamInputInterpreter.EDigitalActions_MainGameActionSet.attack_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Capture, SteamInputInterpreter.EDigitalActions_MainGameActionSet.wrangle_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Harvest, SteamInputInterpreter.EDigitalActions_MainGameActionSet.harvest_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.EmptyPipe, SteamInputInterpreter.EDigitalActions_MainGameActionSet.empty_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Disconnect, SteamInputInterpreter.EDigitalActions_MainGameActionSet.disconnect_tool);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Escape, SteamInputInterpreter.EDigitalActions_MainGameActionSet.pause_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageVitals, SteamInputInterpreter.EDigitalActions_MainGameActionSet.vitals_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageConsumables, SteamInputInterpreter.EDigitalActions_MainGameActionSet.consumables_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageSchedule, SteamInputInterpreter.EDigitalActions_MainGameActionSet.schedule_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManagePriorities, SteamInputInterpreter.EDigitalActions_MainGameActionSet.priorities_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageSkills, SteamInputInterpreter.EDigitalActions_MainGameActionSet.skills_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageResearch, SteamInputInterpreter.EDigitalActions_MainGameActionSet.research_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageStarmap, SteamInputInterpreter.EDigitalActions_MainGameActionSet.starmap_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageReport, SteamInputInterpreter.EDigitalActions_MainGameActionSet.colony_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.ManageDatabase, SteamInputInterpreter.EDigitalActions_MainGameActionSet.codex_menu);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay1, SteamInputInterpreter.EDigitalActions_MainGameActionSet.oxygen_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay2, SteamInputInterpreter.EDigitalActions_MainGameActionSet.power_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay3, SteamInputInterpreter.EDigitalActions_MainGameActionSet.temperature_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay4, SteamInputInterpreter.EDigitalActions_MainGameActionSet.materials_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay5, SteamInputInterpreter.EDigitalActions_MainGameActionSet.light_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay6, SteamInputInterpreter.EDigitalActions_MainGameActionSet.plumbing_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay7, SteamInputInterpreter.EDigitalActions_MainGameActionSet.ventilation_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay8, SteamInputInterpreter.EDigitalActions_MainGameActionSet.decor_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay9, SteamInputInterpreter.EDigitalActions_MainGameActionSet.germs_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay10, SteamInputInterpreter.EDigitalActions_MainGameActionSet.farming_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay11, SteamInputInterpreter.EDigitalActions_MainGameActionSet.rooms_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay12, SteamInputInterpreter.EDigitalActions_MainGameActionSet.exosuits_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay13, SteamInputInterpreter.EDigitalActions_MainGameActionSet.automation_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay14, SteamInputInterpreter.EDigitalActions_MainGameActionSet.shipping_overlay);
			this.kleiActionToSteamDigitalActionLookup.Add(global::Action.Overlay15, SteamInputInterpreter.EDigitalActions_MainGameActionSet.radiation_overlay);
		}
		if (this.kleiActionToSteamAnalogActionLookup.Count < 1)
		{
			this.kleiActionToSteamAnalogActionLookup.Add(global::Action.AnalogCamera, SteamInputInterpreter.EAnalogActions_MainGameActionSet.Camera);
			this.kleiActionToSteamAnalogActionLookup.Add(global::Action.AnalogCursor, SteamInputInterpreter.EAnalogActions_MainGameActionSet.Cursor);
		}
		this.errorSpriteCache = Resources.LoadAll<Sprite>("Sprite Assets/ErrorSheet");
		SteamInput.RunFrame(true);
		this.m_nInputs = SteamInput.GetConnectedControllers(this.m_InputHandles);
		for (int l = 0; l < this.m_InputHandles.Length; l++)
		{
			SteamInput.ActivateActionSet(this.m_InputHandles[l], this.m_ActionSetHandles[0]);
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000D760 File Offset: 0x0000B960
	public void Reset()
	{
		SteamInput.RunFrame(true);
		this.m_nInputs = SteamInput.GetConnectedControllers(this.m_InputHandles);
		for (int i = 0; i < this.m_InputHandles.Length; i++)
		{
			SteamInput.ActivateActionSet(this.m_InputHandles[i], this.m_ActionSetHandles[0]);
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
	public bool GetSteamInputActionIsDown(global::Action action)
	{
		bool flag = false;
		for (int i = 0; i < this.m_nInputs; i++)
		{
			SteamInputInterpreter.EDigitalActions_MainGameActionSet edigitalActions_MainGameActionSet;
			if (this.kleiActionToSteamDigitalActionLookup.TryGetValue(action, out edigitalActions_MainGameActionSet))
			{
				flag |= this.controllerDatas[i].digitalDatas[(int)edigitalActions_MainGameActionSet].down;
			}
		}
		return flag;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000D804 File Offset: 0x0000BA04
	public Vector2 GetSteamCursorMovement()
	{
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.m_nInputs; i++)
		{
			float x = this.controllerDatas[i].analogDatas[1].x;
			float y = this.controllerDatas[i].analogDatas[1].y;
			if (Mathf.Abs(x) > Mathf.Epsilon || Mathf.Abs(y) > Mathf.Epsilon)
			{
				vector += new Vector2(x, -y);
			}
		}
		return vector;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000D88C File Offset: 0x0000BA8C
	public Vector2 GetSteamCameraMovement()
	{
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.m_nInputs; i++)
		{
			float x = this.controllerDatas[i].analogDatas[0].x;
			float y = this.controllerDatas[i].analogDatas[0].y;
			if (Mathf.Abs(x) > Mathf.Epsilon || Mathf.Abs(y) > Mathf.Epsilon)
			{
				vector += new Vector2(x, y);
			}
		}
		return vector;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000D914 File Offset: 0x0000BB14
	public string GetActionGlyph(global::Action action)
	{
		int num = -1;
		int num2 = 0;
		string empty = string.Empty;
		SteamInputInterpreter.EDigitalActions_MainGameActionSet edigitalActions_MainGameActionSet;
		SteamInputInterpreter.EAnalogActions_MainGameActionSet eanalogActions_MainGameActionSet;
		if (this.kleiActionToSteamDigitalActionLookup.TryGetValue(action, out edigitalActions_MainGameActionSet))
		{
			InputDigitalActionHandle_t inputDigitalActionHandle_t = this.m_MainGameActionSetDigitalActionHandles[(int)edigitalActions_MainGameActionSet];
			EInputActionOrigin[] array = new EInputActionOrigin[8];
			SteamInput.GetDigitalActionOrigins(this.m_InputHandles[this.activeControllerIndex], this.m_ActionSetHandles[0], inputDigitalActionHandle_t, array);
			int num3 = (int)array[0];
			this.GetControllerTypeForGlyphLookup(ref num2, ref empty);
			num = num3 - num2;
		}
		else if (this.kleiActionToSteamAnalogActionLookup.TryGetValue(action, out eanalogActions_MainGameActionSet))
		{
			InputAnalogActionHandle_t inputAnalogActionHandle_t = this.m_MainGameActionSetAnalogActionHandles[(int)eanalogActions_MainGameActionSet];
			EInputActionOrigin[] array2 = new EInputActionOrigin[8];
			SteamInput.GetAnalogActionOrigins(this.m_InputHandles[this.activeControllerIndex], this.m_ActionSetHandles[0], inputAnalogActionHandle_t, array2);
			int num4 = (int)array2[0];
			this.GetControllerTypeForGlyphLookup(ref num2, ref empty);
			num = num4 - num2;
		}
		return this.GetFinalGlyphString(num, empty);
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
	public Sprite GetActionSprite(global::Action action, bool ShowEmptyOnError = false)
	{
		int num = -1;
		int num2 = 0;
		string empty = string.Empty;
		bool flag = false;
		if (SteamInput.GetInputTypeForHandle(this.m_InputHandles[this.activeControllerIndex]) != this.currentControllerType)
		{
			this.currentControllerType = SteamInput.GetInputTypeForHandle(this.m_InputHandles[this.activeControllerIndex]);
			flag = true;
		}
		SteamInputInterpreter.EDigitalActions_MainGameActionSet edigitalActions_MainGameActionSet;
		SteamInputInterpreter.EAnalogActions_MainGameActionSet eanalogActions_MainGameActionSet;
		if (this.kleiActionToSteamDigitalActionLookup.TryGetValue(action, out edigitalActions_MainGameActionSet))
		{
			InputDigitalActionHandle_t inputDigitalActionHandle_t = this.m_MainGameActionSetDigitalActionHandles[(int)edigitalActions_MainGameActionSet];
			EInputActionOrigin[] array = new EInputActionOrigin[8];
			SteamInput.GetDigitalActionOrigins(this.m_InputHandles[this.activeControllerIndex], this.m_ActionSetHandles[0], inputDigitalActionHandle_t, array);
			int num3 = (int)array[0];
			this.GetControllerTypeForGlyphLookup(ref num2, ref empty);
			num = num3 - num2;
		}
		else if (this.kleiActionToSteamAnalogActionLookup.TryGetValue(action, out eanalogActions_MainGameActionSet))
		{
			InputAnalogActionHandle_t inputAnalogActionHandle_t = this.m_MainGameActionSetAnalogActionHandles[(int)eanalogActions_MainGameActionSet];
			EInputActionOrigin[] array2 = new EInputActionOrigin[8];
			SteamInput.GetAnalogActionOrigins(this.m_InputHandles[this.activeControllerIndex], this.m_ActionSetHandles[0], inputAnalogActionHandle_t, array2);
			int num4 = (int)array2[0];
			this.GetControllerTypeForGlyphLookup(ref num2, ref empty);
			num = num4 - num2;
		}
		if (num >= 0)
		{
			if (flag || this.spritesCache == null)
			{
				this.spritesCache = Resources.LoadAll<Sprite>("Sprite Assets/" + empty);
			}
			return this.spritesCache[num];
		}
		if (!ShowEmptyOnError)
		{
			return this.errorSpriteCache[0];
		}
		return this.errorSpriteCache[1];
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000DB4C File Offset: 0x0000BD4C
	private void GetControllerTypeForGlyphLookup(ref int offset, ref string spritesheetName)
	{
		this.currentControllerType = SteamInput.GetInputTypeForHandle(this.m_InputHandles[this.activeControllerIndex]);
		switch (this.currentControllerType)
		{
		case ESteamInputType.k_ESteamInputType_Unknown:
			offset = 0;
			return;
		case ESteamInputType.k_ESteamInputType_SteamController:
			spritesheetName = "SteamControllerSheet";
			offset = 1;
			return;
		case ESteamInputType.k_ESteamInputType_XBox360Controller:
			spritesheetName = "XB360Sheet";
			offset = 153;
			return;
		case ESteamInputType.k_ESteamInputType_XBoxOneController:
			spritesheetName = "XboxOneSheet";
			offset = 114;
			return;
		case ESteamInputType.k_ESteamInputType_GenericGamepad:
			offset = 0;
			return;
		case ESteamInputType.k_ESteamInputType_PS4Controller:
			spritesheetName = "PS4Sheet";
			offset = 50;
			return;
		case ESteamInputType.k_ESteamInputType_AppleMFiController:
		case ESteamInputType.k_ESteamInputType_AndroidController:
		case ESteamInputType.k_ESteamInputType_SwitchJoyConSingle:
		case ESteamInputType.k_ESteamInputType_MobileTouch:
		case ESteamInputType.k_ESteamInputType_PS3Controller:
			break;
		case ESteamInputType.k_ESteamInputType_SwitchJoyConPair:
			offset = 192;
			return;
		case ESteamInputType.k_ESteamInputType_SwitchProController:
			offset = 192;
			return;
		case ESteamInputType.k_ESteamInputType_PS5Controller:
			offset = 258;
			return;
		case ESteamInputType.k_ESteamInputType_SteamDeckController:
			spritesheetName = "SteamDeckSheet";
			offset = 333;
			break;
		default:
			return;
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000DC1F File Offset: 0x0000BE1F
	private string GetFinalGlyphString(int finalIndex, string spriteAssetSet)
	{
		if (finalIndex < 0)
		{
			return string.Empty;
		}
		return string.Concat(new string[]
		{
			"<sprite=\"",
			spriteAssetSet,
			"\" index=",
			finalIndex.ToString(),
			">"
		});
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000DC5C File Offset: 0x0000BE5C
	public void CheckForControllerChange()
	{
		SteamInput.RunFrame(true);
		int nInputs = this.m_nInputs;
		this.m_nInputs = SteamInput.GetConnectedControllers(this.m_InputHandles);
		for (int i = 0; i < this.m_nInputs; i++)
		{
			SteamInput.ActivateActionSet(this.m_InputHandles[i], this.m_ActionSetHandles[0]);
		}
		if (nInputs == 0 && this.m_nInputs != 0)
		{
			this.Precache();
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
	public void Update()
	{
		if (!this.m_InputInitialized)
		{
			return;
		}
		this.CheckForControllerChange();
		if (this.controllerDatas.Length != this.m_nInputs)
		{
			this.controllerDatas = new SteamInputInterpreter.ControllerData[this.m_nInputs];
			for (int i = 0; i < this.m_nInputs; i++)
			{
				this.controllerDatas[i].analogDatas = new SteamInputInterpreter.AnalogData[this.m_numMainGameActionSetAnalogActions];
				this.controllerDatas[i].digitalDatas = new SteamInputInterpreter.DigitalData[this.m_numMainGameActionSetDigitalActions];
			}
		}
		for (int j = 0; j < this.m_nInputs; j++)
		{
			for (int k = 0; k < this.m_numMainGameActionSetDigitalActions; k++)
			{
				bool flag = SteamInput.GetDigitalActionData(this.m_InputHandles[j], this.m_MainGameActionSetDigitalActionHandles[k]).bState > 0;
				this.controllerDatas[j].digitalDatas[k].down = flag;
				if (flag && (!KInputManager.currentControllerIsGamepad || this.activeControllerIndex != j))
				{
					this.activeControllerIndex = j;
					KInputManager.currentControllerIsGamepad = true;
					KInputManager.InputChange.Invoke();
				}
			}
			for (int l = 0; l < this.m_numMainGameActionSetAnalogActions; l++)
			{
				InputAnalogActionData_t analogActionData = SteamInput.GetAnalogActionData(this.m_InputHandles[j], this.m_MainGameActionSetAnalogActionHandles[l]);
				this.controllerDatas[j].analogDatas[l].x = analogActionData.x;
				this.controllerDatas[j].analogDatas[l].y = analogActionData.y;
				if ((Mathf.Abs(analogActionData.x) > Mathf.Epsilon || Mathf.Abs(analogActionData.y) > Mathf.Epsilon) && (!KInputManager.currentControllerIsGamepad || this.activeControllerIndex != j))
				{
					this.activeControllerIndex = j;
					KInputManager.currentControllerIsGamepad = true;
					KInputManager.InputChange.Invoke();
				}
			}
		}
	}

	// Token: 0x04000342 RID: 834
	private Vector2 m_ScrollPos;

	// Token: 0x04000343 RID: 835
	private bool m_InputInitialized;

	// Token: 0x04000344 RID: 836
	private int m_nInputs;

	// Token: 0x04000345 RID: 837
	private int activeControllerIndex;

	// Token: 0x04000346 RID: 838
	private Dictionary<global::Action, SteamInputInterpreter.EDigitalActions_MainGameActionSet> kleiActionToSteamDigitalActionLookup = new Dictionary<global::Action, SteamInputInterpreter.EDigitalActions_MainGameActionSet>();

	// Token: 0x04000347 RID: 839
	private Dictionary<global::Action, SteamInputInterpreter.EAnalogActions_MainGameActionSet> kleiActionToSteamAnalogActionLookup = new Dictionary<global::Action, SteamInputInterpreter.EAnalogActions_MainGameActionSet>();

	// Token: 0x04000348 RID: 840
	private Sprite[] spritesCache;

	// Token: 0x04000349 RID: 841
	private Sprite[] errorSpriteCache;

	// Token: 0x0400034A RID: 842
	private ESteamInputType currentControllerType;

	// Token: 0x0400034B RID: 843
	private int m_numActionSets;

	// Token: 0x0400034C RID: 844
	private int m_numMainGameActionSetAnalogActions;

	// Token: 0x0400034D RID: 845
	private int m_numMainGameActionSetDigitalActions;

	// Token: 0x0400034E RID: 846
	private string[] m_ActionSetNames;

	// Token: 0x0400034F RID: 847
	private string[] m_MainGameActionSetAnalogActionNames;

	// Token: 0x04000350 RID: 848
	private string[] m_MainGameActionSetDigitalActionNames;

	// Token: 0x04000351 RID: 849
	private InputActionSetHandle_t[] m_ActionSetHandles;

	// Token: 0x04000352 RID: 850
	private InputAnalogActionHandle_t[] m_MainGameActionSetAnalogActionHandles;

	// Token: 0x04000353 RID: 851
	private InputDigitalActionHandle_t[] m_MainGameActionSetDigitalActionHandles;

	// Token: 0x04000354 RID: 852
	private InputHandle_t[] m_InputHandles;

	// Token: 0x04000355 RID: 853
	private SteamInputInterpreter.ControllerData[] controllerDatas = new SteamInputInterpreter.ControllerData[0];

	// Token: 0x02000983 RID: 2435
	private enum EActionSets
	{
		// Token: 0x040020FD RID: 8445
		MainGameActionSet
	}

	// Token: 0x02000984 RID: 2436
	private enum EAnalogActions_MainGameActionSet
	{
		// Token: 0x040020FF RID: 8447
		Camera,
		// Token: 0x04002100 RID: 8448
		Cursor
	}

	// Token: 0x02000985 RID: 2437
	private enum EDigitalActions_MainGameActionSet
	{
		// Token: 0x04002102 RID: 8450
		affirmative_click,
		// Token: 0x04002103 RID: 8451
		negative_click,
		// Token: 0x04002104 RID: 8452
		camera_home,
		// Token: 0x04002105 RID: 8453
		camera_zoom_in_scroll_down,
		// Token: 0x04002106 RID: 8454
		camera_zoom_out_scroll_up,
		// Token: 0x04002107 RID: 8455
		sim_pause,
		// Token: 0x04002108 RID: 8456
		sim_cycle_speed,
		// Token: 0x04002109 RID: 8457
		rotate_building,
		// Token: 0x0400210A RID: 8458
		copy_building,
		// Token: 0x0400210B RID: 8459
		dig_tool,
		// Token: 0x0400210C RID: 8460
		cancel_tool,
		// Token: 0x0400210D RID: 8461
		deconstruct_tool,
		// Token: 0x0400210E RID: 8462
		priority_tool,
		// Token: 0x0400210F RID: 8463
		disinfect_tool,
		// Token: 0x04002110 RID: 8464
		sweep_tool,
		// Token: 0x04002111 RID: 8465
		mop_tool,
		// Token: 0x04002112 RID: 8466
		attack_tool,
		// Token: 0x04002113 RID: 8467
		wrangle_tool,
		// Token: 0x04002114 RID: 8468
		harvest_tool,
		// Token: 0x04002115 RID: 8469
		empty_tool,
		// Token: 0x04002116 RID: 8470
		disconnect_tool,
		// Token: 0x04002117 RID: 8471
		pause_menu,
		// Token: 0x04002118 RID: 8472
		vitals_menu,
		// Token: 0x04002119 RID: 8473
		consumables_menu,
		// Token: 0x0400211A RID: 8474
		schedule_menu,
		// Token: 0x0400211B RID: 8475
		priorities_menu,
		// Token: 0x0400211C RID: 8476
		skills_menu,
		// Token: 0x0400211D RID: 8477
		research_menu,
		// Token: 0x0400211E RID: 8478
		starmap_menu,
		// Token: 0x0400211F RID: 8479
		colony_menu,
		// Token: 0x04002120 RID: 8480
		codex_menu,
		// Token: 0x04002121 RID: 8481
		oxygen_overlay,
		// Token: 0x04002122 RID: 8482
		power_overlay,
		// Token: 0x04002123 RID: 8483
		temperature_overlay,
		// Token: 0x04002124 RID: 8484
		materials_overlay,
		// Token: 0x04002125 RID: 8485
		light_overlay,
		// Token: 0x04002126 RID: 8486
		plumbing_overlay,
		// Token: 0x04002127 RID: 8487
		ventilation_overlay,
		// Token: 0x04002128 RID: 8488
		decor_overlay,
		// Token: 0x04002129 RID: 8489
		germs_overlay,
		// Token: 0x0400212A RID: 8490
		farming_overlay,
		// Token: 0x0400212B RID: 8491
		rooms_overlay,
		// Token: 0x0400212C RID: 8492
		exosuits_overlay,
		// Token: 0x0400212D RID: 8493
		automation_overlay,
		// Token: 0x0400212E RID: 8494
		shipping_overlay,
		// Token: 0x0400212F RID: 8495
		radiation_overlay,
		// Token: 0x04002130 RID: 8496
		toggle_resources,
		// Token: 0x04002131 RID: 8497
		toggle_diagnostics
	}

	// Token: 0x02000986 RID: 2438
	private struct AnalogData
	{
		// Token: 0x04002132 RID: 8498
		public float x;

		// Token: 0x04002133 RID: 8499
		public float y;
	}

	// Token: 0x02000987 RID: 2439
	private struct DigitalData
	{
		// Token: 0x04002134 RID: 8500
		public bool down;
	}

	// Token: 0x02000988 RID: 2440
	private struct ControllerData
	{
		// Token: 0x04002135 RID: 8501
		public SteamInputInterpreter.AnalogData[] analogDatas;

		// Token: 0x04002136 RID: 8502
		public SteamInputInterpreter.DigitalData[] digitalDatas;
	}
}
