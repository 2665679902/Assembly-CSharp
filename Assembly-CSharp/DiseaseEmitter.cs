using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000722 RID: 1826
[AddComponentMenu("KMonoBehaviour/scripts/DiseaseEmitter")]
public class DiseaseEmitter : KMonoBehaviour
{
	// Token: 0x17000397 RID: 919
	// (get) Token: 0x060031F0 RID: 12784 RVA: 0x0010B65A File Offset: 0x0010985A
	public float EmitRate
	{
		get
		{
			return this.emitRate;
		}
	}

	// Token: 0x060031F1 RID: 12785 RVA: 0x0010B664 File Offset: 0x00109864
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.emitDiseases != null)
		{
			this.simHandles = new int[this.emitDiseases.Length];
			for (int i = 0; i < this.simHandles.Length; i++)
			{
				this.simHandles[i] = -1;
			}
		}
		this.SimRegister();
	}

	// Token: 0x060031F2 RID: 12786 RVA: 0x0010B6B4 File Offset: 0x001098B4
	protected override void OnCleanUp()
	{
		this.SimUnregister();
		base.OnCleanUp();
	}

	// Token: 0x060031F3 RID: 12787 RVA: 0x0010B6C2 File Offset: 0x001098C2
	public void SetEnable(bool enable)
	{
		if (this.enableEmitter == enable)
		{
			return;
		}
		this.enableEmitter = enable;
		if (this.enableEmitter)
		{
			this.SimRegister();
			return;
		}
		this.SimUnregister();
	}

	// Token: 0x060031F4 RID: 12788 RVA: 0x0010B6EC File Offset: 0x001098EC
	private void OnCellChanged()
	{
		if (this.simHandles == null || !this.enableEmitter)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		if (Grid.IsValidCell(num))
		{
			for (int i = 0; i < this.emitDiseases.Length; i++)
			{
				if (Sim.IsValidHandle(this.simHandles[i]))
				{
					SimMessages.ModifyDiseaseEmitter(this.simHandles[i], num, this.emitRange, this.emitDiseases[i], this.emitRate, this.emitCount);
				}
			}
		}
	}

	// Token: 0x060031F5 RID: 12789 RVA: 0x0010B764 File Offset: 0x00109964
	private void SimRegister()
	{
		if (this.simHandles == null || !this.enableEmitter)
		{
			return;
		}
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged), "DiseaseEmitter.Modify");
		for (int i = 0; i < this.simHandles.Length; i++)
		{
			if (this.simHandles[i] == -1)
			{
				this.simHandles[i] = -2;
				SimMessages.AddDiseaseEmitter(Game.Instance.simComponentCallbackManager.Add(new Action<int, object>(DiseaseEmitter.OnSimRegisteredCallback), this, "DiseaseEmitter").index);
			}
		}
	}

	// Token: 0x060031F6 RID: 12790 RVA: 0x0010B7FC File Offset: 0x001099FC
	private void SimUnregister()
	{
		if (this.simHandles == null)
		{
			return;
		}
		for (int i = 0; i < this.simHandles.Length; i++)
		{
			if (Sim.IsValidHandle(this.simHandles[i]))
			{
				SimMessages.RemoveDiseaseEmitter(-1, this.simHandles[i]);
			}
			this.simHandles[i] = -1;
		}
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged));
	}

	// Token: 0x060031F7 RID: 12791 RVA: 0x0010B867 File Offset: 0x00109A67
	private static void OnSimRegisteredCallback(int handle, object data)
	{
		((DiseaseEmitter)data).OnSimRegistered(handle);
	}

	// Token: 0x060031F8 RID: 12792 RVA: 0x0010B878 File Offset: 0x00109A78
	private void OnSimRegistered(int handle)
	{
		bool flag = false;
		if (this != null)
		{
			for (int i = 0; i < this.simHandles.Length; i++)
			{
				if (this.simHandles[i] == -2)
				{
					this.simHandles[i] = handle;
					flag = true;
					break;
				}
			}
			this.OnCellChanged();
		}
		if (!flag)
		{
			SimMessages.RemoveDiseaseEmitter(-1, handle);
		}
	}

	// Token: 0x060031F9 RID: 12793 RVA: 0x0010B8CC File Offset: 0x00109ACC
	public void SetDiseases(List<Disease> diseases)
	{
		this.emitDiseases = new byte[diseases.Count];
		for (int i = 0; i < diseases.Count; i++)
		{
			this.emitDiseases[i] = Db.Get().Diseases.GetIndex(diseases[i].id);
		}
	}

	// Token: 0x04001E52 RID: 7762
	[Serialize]
	public float emitRate = 1f;

	// Token: 0x04001E53 RID: 7763
	[Serialize]
	public byte emitRange;

	// Token: 0x04001E54 RID: 7764
	[Serialize]
	public int emitCount;

	// Token: 0x04001E55 RID: 7765
	[Serialize]
	public byte[] emitDiseases;

	// Token: 0x04001E56 RID: 7766
	public int[] simHandles;

	// Token: 0x04001E57 RID: 7767
	[Serialize]
	private bool enableEmitter;
}
