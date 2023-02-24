using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020006EE RID: 1774
[AddComponentMenu("KMonoBehaviour/scripts/SubmersionMonitor")]
public class SubmersionMonitor : KMonoBehaviour, IGameObjectEffectDescriptor, IWiltCause, ISim1000ms
{
	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06003037 RID: 12343 RVA: 0x000FEDD3 File Offset: 0x000FCFD3
	public bool Dry
	{
		get
		{
			return this.dry;
		}
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x000FEDDB File Offset: 0x000FCFDB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OnMove();
		this.CheckDry();
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnMove), "SubmersionMonitor.OnSpawn");
	}

	// Token: 0x06003039 RID: 12345 RVA: 0x000FEE14 File Offset: 0x000FD014
	private void OnMove()
	{
		this.position = Grid.PosToCell(base.gameObject);
		if (this.partitionerEntry.IsValid())
		{
			GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, this.position);
		}
		else
		{
			Vector2I vector2I = Grid.PosToXY(base.transform.GetPosition());
			Extents extents = new Extents(vector2I.x, vector2I.y, 1, 2);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("DrowningMonitor.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
		}
		this.CheckDry();
	}

	// Token: 0x0600303A RID: 12346 RVA: 0x000FEEB5 File Offset: 0x000FD0B5
	private void OnDrawGizmosSelected()
	{
	}

	// Token: 0x0600303B RID: 12347 RVA: 0x000FEEB7 File Offset: 0x000FD0B7
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnMove));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x0600303C RID: 12348 RVA: 0x000FEEEB File Offset: 0x000FD0EB
	public void Configure(float _maxStamina, float _staminaRegenRate, float _cellLiquidThreshold = 0.95f)
	{
		this.cellLiquidThreshold = _cellLiquidThreshold;
	}

	// Token: 0x0600303D RID: 12349 RVA: 0x000FEEF4 File Offset: 0x000FD0F4
	public void Sim1000ms(float dt)
	{
		this.CheckDry();
	}

	// Token: 0x0600303E RID: 12350 RVA: 0x000FEEFC File Offset: 0x000FD0FC
	private void CheckDry()
	{
		if (!this.IsCellSafe())
		{
			if (!this.dry)
			{
				this.dry = true;
				base.Trigger(-2057657673, null);
				return;
			}
		}
		else if (this.dry)
		{
			this.dry = false;
			base.Trigger(1555379996, null);
		}
	}

	// Token: 0x0600303F RID: 12351 RVA: 0x000FEF48 File Offset: 0x000FD148
	public bool IsCellSafe()
	{
		int num = Grid.PosToCell(base.gameObject);
		return Grid.IsValidCell(num) && Grid.IsSubstantialLiquid(num, this.cellLiquidThreshold);
	}

	// Token: 0x06003040 RID: 12352 RVA: 0x000FEF7C File Offset: 0x000FD17C
	private void OnLiquidChanged(object data)
	{
		this.CheckDry();
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06003041 RID: 12353 RVA: 0x000FEF84 File Offset: 0x000FD184
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.DryingOut };
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06003042 RID: 12354 RVA: 0x000FEF90 File Offset: 0x000FD190
	public string WiltStateString
	{
		get
		{
			if (this.Dry)
			{
				return Db.Get().CreatureStatusItems.DryingOut.resolveStringCallback(CREATURES.STATUSITEMS.DRYINGOUT.NAME, this);
			}
			return "";
		}
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x000FEFC4 File Offset: 0x000FD1C4
	public void SetIncapacitated(bool state)
	{
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x000FEFC6 File Offset: 0x000FD1C6
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_SUBMERSION, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_SUBMERSION, Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x04001D12 RID: 7442
	private int position;

	// Token: 0x04001D13 RID: 7443
	private bool dry;

	// Token: 0x04001D14 RID: 7444
	protected float cellLiquidThreshold = 0.2f;

	// Token: 0x04001D15 RID: 7445
	private Extents extents;

	// Token: 0x04001D16 RID: 7446
	private HandleVector<int>.Handle partitionerEntry;
}
