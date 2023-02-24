using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200091C RID: 2332
public abstract class SimComponent : KMonoBehaviour, ISim200ms
{
	// Token: 0x170004CE RID: 1230
	// (get) Token: 0x060043DA RID: 17370 RVA: 0x0017EFD0 File Offset: 0x0017D1D0
	public bool IsSimActive
	{
		get
		{
			return this.simActive;
		}
	}

	// Token: 0x060043DB RID: 17371 RVA: 0x0017EFD8 File Offset: 0x0017D1D8
	protected virtual void OnSimRegister(HandleVector<Game.ComplexCallbackInfo<int>>.Handle cb_handle)
	{
	}

	// Token: 0x060043DC RID: 17372 RVA: 0x0017EFDA File Offset: 0x0017D1DA
	protected virtual void OnSimRegistered()
	{
	}

	// Token: 0x060043DD RID: 17373 RVA: 0x0017EFDC File Offset: 0x0017D1DC
	protected virtual void OnSimActivate()
	{
	}

	// Token: 0x060043DE RID: 17374 RVA: 0x0017EFDE File Offset: 0x0017D1DE
	protected virtual void OnSimDeactivate()
	{
	}

	// Token: 0x060043DF RID: 17375 RVA: 0x0017EFE0 File Offset: 0x0017D1E0
	protected virtual void OnSimUnregister()
	{
	}

	// Token: 0x060043E0 RID: 17376
	protected abstract Action<int> GetStaticUnregister();

	// Token: 0x060043E1 RID: 17377 RVA: 0x0017EFE2 File Offset: 0x0017D1E2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060043E2 RID: 17378 RVA: 0x0017EFEA File Offset: 0x0017D1EA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.SimRegister();
	}

	// Token: 0x060043E3 RID: 17379 RVA: 0x0017EFF8 File Offset: 0x0017D1F8
	protected override void OnCleanUp()
	{
		this.SimUnregister();
		base.OnCleanUp();
	}

	// Token: 0x060043E4 RID: 17380 RVA: 0x0017F006 File Offset: 0x0017D206
	public void SetSimActive(bool active)
	{
		this.simActive = active;
		this.dirty = true;
	}

	// Token: 0x060043E5 RID: 17381 RVA: 0x0017F016 File Offset: 0x0017D216
	public void Sim200ms(float dt)
	{
		if (!Sim.IsValidHandle(this.simHandle))
		{
			return;
		}
		this.UpdateSimState();
	}

	// Token: 0x060043E6 RID: 17382 RVA: 0x0017F02C File Offset: 0x0017D22C
	private void UpdateSimState()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		if (this.simActive)
		{
			this.OnSimActivate();
			return;
		}
		this.OnSimDeactivate();
	}

	// Token: 0x060043E7 RID: 17383 RVA: 0x0017F054 File Offset: 0x0017D254
	private void SimRegister()
	{
		if (base.isSpawned && this.simHandle == -1)
		{
			this.simHandle = -2;
			Action<int> static_unregister = this.GetStaticUnregister();
			HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle2 = Game.Instance.simComponentCallbackManager.Add(delegate(int handle, object data)
			{
				SimComponent.OnSimRegistered(this, handle, static_unregister);
			}, this, "SimComponent.SimRegister");
			this.OnSimRegister(handle2);
		}
	}

	// Token: 0x060043E8 RID: 17384 RVA: 0x0017F0BC File Offset: 0x0017D2BC
	private void SimUnregister()
	{
		if (Sim.IsValidHandle(this.simHandle))
		{
			this.OnSimUnregister();
		}
		this.simHandle = -1;
	}

	// Token: 0x060043E9 RID: 17385 RVA: 0x0017F0D8 File Offset: 0x0017D2D8
	private static void OnSimRegistered(SimComponent instance, int handle, Action<int> static_unregister)
	{
		if (instance != null)
		{
			instance.simHandle = handle;
			instance.OnSimRegistered();
			return;
		}
		static_unregister(handle);
	}

	// Token: 0x060043EA RID: 17386 RVA: 0x0017F0F8 File Offset: 0x0017D2F8
	[Conditional("ENABLE_LOGGER")]
	protected void Log(string msg)
	{
	}

	// Token: 0x04002D4D RID: 11597
	[SerializeField]
	protected int simHandle = -1;

	// Token: 0x04002D4E RID: 11598
	private bool simActive = true;

	// Token: 0x04002D4F RID: 11599
	private bool dirty = true;
}
