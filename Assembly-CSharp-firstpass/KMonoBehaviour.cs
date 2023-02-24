using System;
using KSerialization;
using UnityEngine;

// Token: 0x020000BA RID: 186
[SerializationConfig(MemberSerialization.OptIn)]
public class KMonoBehaviour : MonoBehaviour, IStateMachineTarget, ISaveLoadable, IUniformGridObject
{
	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001DFA3 File Offset: 0x0001C1A3
	// (set) Token: 0x060006DF RID: 1759 RVA: 0x0001DFAB File Offset: 0x0001C1AB
	public bool isSpawned { get; private set; }

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
	public new Transform transform
	{
		get
		{
			return base.transform;
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001DFBC File Offset: 0x0001C1BC
	public bool isNull
	{
		get
		{
			return this == null;
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0001DFC5 File Offset: 0x0001C1C5
	public void Awake()
	{
		if (App.IsExiting)
		{
			return;
		}
		this.InitializeComponent();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
	public void InitializeComponent()
	{
		if (this.isInitialized)
		{
			return;
		}
		if (!KMonoBehaviour.isPoolPreInit && Application.isPlaying && KMonoBehaviour.lastGameObject != base.gameObject)
		{
			KMonoBehaviour.lastGameObject = base.gameObject;
			KMonoBehaviour.lastObj = KObjectManager.Instance.GetOrCreateObject(base.gameObject);
		}
		this.obj = KMonoBehaviour.lastObj;
		this.isInitialized = true;
		MyAttributes.OnAwake(this);
		if (!KMonoBehaviour.isPoolPreInit)
		{
			try
			{
				this.OnPrefabInit();
			}
			catch (Exception ex)
			{
				string text = string.Concat(new string[]
				{
					"Error in ",
					base.name,
					".",
					base.GetType().Name,
					".OnPrefabInit"
				});
				DebugUtil.LogException(this, text, ex);
			}
		}
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0001E0AC File Offset: 0x0001C2AC
	private void OnEnable()
	{
		if (App.IsExiting)
		{
			return;
		}
		this.OnCmpEnable();
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0001E0BC File Offset: 0x0001C2BC
	private void OnDisable()
	{
		if (App.IsExiting || KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		this.OnCmpDisable();
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0001E0D3 File Offset: 0x0001C2D3
	public bool IsInitialized()
	{
		return this.isInitialized;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0001E0DC File Offset: 0x0001C2DC
	public void OnDestroy()
	{
		this.OnForcedCleanUp();
		if (App.IsExiting)
		{
			return;
		}
		if (KMonoBehaviour.isLoadingScene)
		{
			this.OnLoadLevel();
			return;
		}
		if (KObjectManager.Instance != null && !base.gameObject.activeSelf)
		{
			KObjectManager.Instance.QueueDestroy(this.obj);
		}
		this.OnCleanUp();
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0001E140 File Offset: 0x0001C340
	public void Start()
	{
		if (App.IsExiting)
		{
			return;
		}
		this.Spawn();
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0001E150 File Offset: 0x0001C350
	public void Spawn()
	{
		if (this.isSpawned)
		{
			return;
		}
		if (!this.isInitialized)
		{
			global::Debug.LogError(base.name + "." + base.GetType().Name + " is not initialized.");
			return;
		}
		this.isSpawned = true;
		if (this.autoRegisterSimRender)
		{
			SimAndRenderScheduler.instance.Add(this, this.simRenderLoadBalance);
		}
		MyAttributes.OnStart(this);
		try
		{
			this.OnSpawn();
		}
		catch (Exception ex)
		{
			string text = string.Concat(new string[]
			{
				"Error in ",
				base.name,
				".",
				base.GetType().Name,
				".OnSpawn"
			});
			DebugUtil.LogException(this, text, ex);
		}
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0001E218 File Offset: 0x0001C418
	protected virtual void OnPrefabInit()
	{
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0001E21A File Offset: 0x0001C41A
	protected virtual void OnSpawn()
	{
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0001E21C File Offset: 0x0001C41C
	protected virtual void OnCmpEnable()
	{
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0001E21E File Offset: 0x0001C41E
	protected virtual void OnCmpDisable()
	{
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0001E220 File Offset: 0x0001C420
	protected virtual void OnCleanUp()
	{
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0001E222 File Offset: 0x0001C422
	protected virtual void OnForcedCleanUp()
	{
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0001E224 File Offset: 0x0001C424
	protected virtual void OnLoadLevel()
	{
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0001E226 File Offset: 0x0001C426
	public T FindOrAdd<T>() where T : KMonoBehaviour
	{
		return this.FindOrAddComponent<T>();
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0001E22E File Offset: 0x0001C42E
	public void FindOrAdd<T>(ref T c) where T : KMonoBehaviour
	{
		c = this.FindOrAdd<T>();
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0001E23C File Offset: 0x0001C43C
	public T Require<T>() where T : Component
	{
		return this.RequireComponent<T>();
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0001E244 File Offset: 0x0001C444
	public int Subscribe(int hash, Action<object> handler)
	{
		return this.obj.GetEventSystem().Subscribe(hash, handler);
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0001E258 File Offset: 0x0001C458
	public int Subscribe(GameObject target, int hash, Action<object> handler)
	{
		return this.obj.GetEventSystem().Subscribe(target, hash, handler);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0001E26D File Offset: 0x0001C46D
	public int Subscribe<ComponentType>(int hash, EventSystem.IntraObjectHandler<ComponentType> handler) where ComponentType : Component
	{
		return this.obj.GetEventSystem().Subscribe<ComponentType>(hash, handler);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0001E281 File Offset: 0x0001C481
	public void Unsubscribe(int hash, Action<object> handler)
	{
		if (this.obj != null)
		{
			this.obj.GetEventSystem().Unsubscribe(hash, handler);
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0001E29D File Offset: 0x0001C49D
	public void Unsubscribe(int id)
	{
		this.obj.GetEventSystem().Unsubscribe(id);
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
	public void Unsubscribe(GameObject target, int hash, Action<object> handler)
	{
		this.obj.GetEventSystem().Unsubscribe(target, hash, handler);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0001E2C5 File Offset: 0x0001C4C5
	public void Unsubscribe<ComponentType>(int hash, EventSystem.IntraObjectHandler<ComponentType> handler, bool suppressWarnings = false) where ComponentType : Component
	{
		if (this.obj != null)
		{
			this.obj.GetEventSystem().Unsubscribe<ComponentType>(hash, handler, suppressWarnings);
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
	public void Trigger(int hash, object data = null)
	{
		if (this.obj != null && this.obj.hasEventSystem)
		{
			this.obj.GetEventSystem().Trigger(base.gameObject, hash, data);
		}
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0001E314 File Offset: 0x0001C514
	public static void PlaySound(string sound)
	{
		if (sound != null)
		{
			try
			{
				if (SoundListenerController.Instance == null)
				{
					KFMOD.PlayUISound(sound);
				}
				else
				{
					KFMOD.PlayOneShot(sound, SoundListenerController.Instance.transform.GetPosition(), 1f);
				}
			}
			catch
			{
				DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + sound + "]" });
			}
		}
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0001E388 File Offset: 0x0001C588
	public static void PlaySound3DAtLocation(string sound, Vector3 location)
	{
		if (SoundListenerController.Instance != null)
		{
			try
			{
				KFMOD.PlayOneShot(sound, location, 1f);
			}
			catch
			{
				DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + sound + "]" });
			}
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
	public void PlaySound3D(string asset)
	{
		try
		{
			KFMOD.PlayOneShot(asset, this.transform.GetPosition(), 1f);
		}
		catch
		{
			DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + asset + "]" });
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0001E43C File Offset: 0x0001C63C
	public virtual Vector2 PosMin()
	{
		return this.transform.GetPosition();
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0001E44E File Offset: 0x0001C64E
	public virtual Vector2 PosMax()
	{
		return this.transform.GetPosition();
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0001E46F File Offset: 0x0001C66F
	ComponentType IStateMachineTarget.GetComponent<ComponentType>()
	{
		return base.GetComponent<ComponentType>();
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0001E477 File Offset: 0x0001C677
	GameObject IStateMachineTarget.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0001E47F File Offset: 0x0001C67F
	string IStateMachineTarget.get_name()
	{
		return base.name;
	}

	// Token: 0x040005CE RID: 1486
	public static GameObject lastGameObject;

	// Token: 0x040005CF RID: 1487
	public static KObject lastObj;

	// Token: 0x040005D0 RID: 1488
	public static bool isPoolPreInit;

	// Token: 0x040005D1 RID: 1489
	public static bool isLoadingScene;

	// Token: 0x040005D2 RID: 1490
	private KObject obj;

	// Token: 0x040005D3 RID: 1491
	private bool isInitialized;

	// Token: 0x040005D4 RID: 1492
	protected bool autoRegisterSimRender = true;

	// Token: 0x040005D5 RID: 1493
	protected bool simRenderLoadBalance;
}
