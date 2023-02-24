using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000063 RID: 99
[AddComponentMenu("KMonoBehaviour/Plugins/KScreenManager")]
public class KScreenManager : KMonoBehaviour, IInputHandler
{
	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00013EBC File Offset: 0x000120BC
	// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00013EC3 File Offset: 0x000120C3
	public static KScreenManager Instance { get; private set; }

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00013ECB File Offset: 0x000120CB
	public string handlerName
	{
		get
		{
			return base.gameObject.name;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013ED8 File Offset: 0x000120D8
	// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00013EE0 File Offset: 0x000120E0
	public KInputHandler inputHandler { get; set; }

	// Token: 0x060003F7 RID: 1015 RVA: 0x00013EE9 File Offset: 0x000120E9
	private void OnApplicationQuit()
	{
		KScreenManager.quitting = true;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00013EF1 File Offset: 0x000120F1
	public void DisableInput(bool disable)
	{
		KScreenManager.inputDisabled = disable;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00013EF9 File Offset: 0x000120F9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		KScreenManager.Instance = this;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00013F07 File Offset: 0x00012107
	protected override void OnForcedCleanUp()
	{
		KScreenManager.Instance = null;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00013F0F File Offset: 0x0001210F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.evSys = UnityEngine.EventSystems.EventSystem.current;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00013F24 File Offset: 0x00012124
	protected override void OnCmpDisable()
	{
		if (KScreenManager.quitting)
		{
			for (int i = this.screenStack.Count - 1; i >= 0; i--)
			{
				this.screenStack[i].Deactivate();
			}
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00013F61 File Offset: 0x00012161
	public GameObject ActivateScreen(GameObject screen, GameObject parent)
	{
		KScreenManager.AddExistingChild(parent, screen);
		screen.GetComponent<KScreen>().Activate();
		return screen;
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00013F77 File Offset: 0x00012177
	public KScreen InstantiateScreen(GameObject screenPrefab, GameObject parent)
	{
		return KScreenManager.AddChild(parent, screenPrefab).GetComponent<KScreen>();
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00013F85 File Offset: 0x00012185
	public KScreen StartScreen(GameObject screenPrefab, GameObject parent)
	{
		KScreen component = KScreenManager.AddChild(parent, screenPrefab).GetComponent<KScreen>();
		component.Activate();
		return component;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00013F99 File Offset: 0x00012199
	public void PushScreen(KScreen screen)
	{
		this.screenStack.Add(screen);
		this.RefreshStack();
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00013FB0 File Offset: 0x000121B0
	public void RefreshStack()
	{
		this.screenStack = (from x in this.screenStack
			where x != null
			orderby x.GetSortKey()
			select x).ToList<KScreen>();
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00014018 File Offset: 0x00012218
	public KScreen PopScreen(KScreen screen)
	{
		KScreen kscreen = null;
		int num = this.screenStack.IndexOf(screen);
		if (num >= 0)
		{
			kscreen = this.screenStack[num];
			this.screenStack.RemoveAt(num);
		}
		this.screenStack = (from x in this.screenStack
			where x != null
			orderby x.GetSortKey()
			select x).ToList<KScreen>();
		return kscreen;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000140AB File Offset: 0x000122AB
	public KScreen PopScreen()
	{
		KScreen kscreen = this.screenStack[this.screenStack.Count - 1];
		this.screenStack.RemoveAt(this.screenStack.Count - 1);
		return kscreen;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000140E0 File Offset: 0x000122E0
	public string DebugScreenStack()
	{
		string text = "";
		foreach (KScreen kscreen in this.screenStack)
		{
			if (kscreen != null)
			{
				if (!kscreen.isActiveAndEnabled)
				{
					text += "Not isActiveAndEnabled: ";
				}
				text = text + kscreen.name + "\n";
			}
			else
			{
				text += "Null screen in screenStack\n";
			}
		}
		return text;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00014170 File Offset: 0x00012370
	private void Update()
	{
		bool flag = true;
		for (int i = this.screenStack.Count - 1; i >= 0; i--)
		{
			KScreen kscreen = this.screenStack[i];
			if (kscreen != null && kscreen.isActiveAndEnabled)
			{
				kscreen.ScreenUpdate(flag);
			}
			if (flag && kscreen.IsModal())
			{
				flag = false;
			}
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000141CC File Offset: 0x000123CC
	public void OnKeyDown(KButtonEvent e)
	{
		if (KScreenManager.inputDisabled)
		{
			return;
		}
		for (int i = this.screenStack.Count - 1; i >= 0; i--)
		{
			KScreen kscreen = this.screenStack[i];
			if (!kscreen.isHiddenButActive && kscreen != null && kscreen.isActiveAndEnabled)
			{
				kscreen.OnKeyDown(e);
				if (e.Consumed)
				{
					this.lastConsumedEvent = e;
					this.lastConsumedEventScreen = kscreen;
					return;
				}
			}
		}
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00014240 File Offset: 0x00012440
	public void OnKeyUp(KButtonEvent e)
	{
		if (KScreenManager.inputDisabled)
		{
			return;
		}
		for (int i = this.screenStack.Count - 1; i >= 0; i--)
		{
			KScreen kscreen = this.screenStack[i];
			if (kscreen != null && kscreen.isActiveAndEnabled)
			{
				kscreen.OnKeyUp(e);
				if (e.Consumed)
				{
					this.lastConsumedEvent = e;
					this.lastConsumedEventScreen = kscreen;
					return;
				}
			}
		}
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x000142AC File Offset: 0x000124AC
	public void SetEventSystemEnabled(bool state)
	{
		if (this.evSys == null)
		{
			this.evSys = UnityEngine.EventSystems.EventSystem.current;
			if (this.evSys == null)
			{
				global::Debug.LogWarning("Cannot enable/disable null UI event system");
				return;
			}
		}
		if (this.evSys.enabled != state)
		{
			this.evSys.enabled = state;
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00014305 File Offset: 0x00012505
	public void SetNavigationEventsEnabled(bool state)
	{
		if (this.evSys == null)
		{
			return;
		}
		this.evSys.sendNavigationEvents = state;
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00014322 File Offset: 0x00012522
	public static GameObject AddExistingChild(GameObject parent, GameObject go)
	{
		if (go != null && parent != null)
		{
			go.transform.SetParent(parent.transform, false);
			go.layer = parent.layer;
		}
		return go;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00014355 File Offset: 0x00012555
	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		return Util.KInstantiateUI(prefab, parent, false);
	}

	// Token: 0x04000468 RID: 1128
	private static bool quitting;

	// Token: 0x04000469 RID: 1129
	private static bool inputDisabled;

	// Token: 0x0400046C RID: 1132
	private List<KScreen> screenStack = new List<KScreen>();

	// Token: 0x0400046D RID: 1133
	private UnityEngine.EventSystems.EventSystem evSys;

	// Token: 0x0400046E RID: 1134
	private KButtonEvent lastConsumedEvent;

	// Token: 0x0400046F RID: 1135
	private KScreen lastConsumedEventScreen;
}
