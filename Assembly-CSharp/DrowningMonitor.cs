using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006CE RID: 1742
[AddComponentMenu("KMonoBehaviour/scripts/DrowningMonitor")]
public class DrowningMonitor : KMonoBehaviour, IWiltCause, ISlicedSim1000ms
{
	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000FA2BB File Offset: 0x000F84BB
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06002F5D RID: 12125 RVA: 0x000FA2DD File Offset: 0x000F84DD
	public bool Drowning
	{
		get
		{
			return this.drowning;
		}
	}

	// Token: 0x06002F5E RID: 12126 RVA: 0x000FA2E8 File Offset: 0x000F84E8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.timeToDrown = 75f;
		if (DrowningMonitor.drowningEffect == null)
		{
			DrowningMonitor.drowningEffect = new Effect("Drowning", CREATURES.STATUSITEMS.DROWNING.NAME, CREATURES.STATUSITEMS.DROWNING.TOOLTIP, 0f, false, false, true, null, -1f, 0f, null, "");
			DrowningMonitor.drowningEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -100f, CREATURES.STATUSITEMS.DROWNING.NAME, false, false, true));
		}
		if (DrowningMonitor.saturatedEffect == null)
		{
			DrowningMonitor.saturatedEffect = new Effect("Saturated", CREATURES.STATUSITEMS.SATURATED.NAME, CREATURES.STATUSITEMS.SATURATED.TOOLTIP, 0f, false, false, true, null, -1f, 0f, null, "");
			DrowningMonitor.saturatedEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -100f, CREATURES.STATUSITEMS.SATURATED.NAME, false, false, true));
		}
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x000FA3F8 File Offset: 0x000F85F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SlicedUpdaterSim1000ms<DrowningMonitor>.instance.RegisterUpdate1000ms(this);
		this.OnMove();
		this.CheckDrowning(null);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnMove), "DrowningMonitor.OnSpawn");
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x000FA448 File Offset: 0x000F8648
	private void OnMove()
	{
		if (this.partitionerEntry.IsValid())
		{
			Extents extents = this.occupyArea.GetExtents();
			GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, extents);
		}
		else
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add("DrowningMonitor.OnSpawn", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
		}
		this.CheckDrowning(null);
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x000FA4C4 File Offset: 0x000F86C4
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnMove));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		SlicedUpdaterSim1000ms<DrowningMonitor>.instance.UnregisterUpdate1000ms(this);
		base.OnCleanUp();
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x000FA504 File Offset: 0x000F8704
	private void CheckDrowning(object data = null)
	{
		if (this.drowned)
		{
			return;
		}
		int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
		if (!this.IsCellSafe(num))
		{
			if (!this.drowning)
			{
				this.drowning = true;
				base.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.Drowning, false);
				base.Trigger(1949704522, null);
			}
			if (this.timeToDrown <= 0f && this.canDrownToDeath)
			{
				DeathMonitor.Instance smi = this.GetSMI<DeathMonitor.Instance>();
				if (smi != null)
				{
					smi.Kill(Db.Get().Deaths.Drowned);
				}
				base.Trigger(-750750377, null);
				this.drowned = true;
			}
		}
		else if (this.drowning)
		{
			this.drowning = false;
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Creatures.Drowning);
			base.Trigger(99949694, null);
		}
		if (this.livesUnderWater)
		{
			this.saturatedStatusGuid = this.selectable.ToggleStatusItem(Db.Get().CreatureStatusItems.Saturated, this.saturatedStatusGuid, this.drowning, this);
		}
		else
		{
			this.drowningStatusGuid = this.selectable.ToggleStatusItem(Db.Get().CreatureStatusItems.Drowning, this.drowningStatusGuid, this.drowning, this);
		}
		if (this.effects != null)
		{
			if (this.drowning)
			{
				if (this.livesUnderWater)
				{
					this.effects.Add(DrowningMonitor.saturatedEffect, false);
					return;
				}
				this.effects.Add(DrowningMonitor.drowningEffect, false);
				return;
			}
			else
			{
				if (this.livesUnderWater)
				{
					this.effects.Remove(DrowningMonitor.saturatedEffect);
					return;
				}
				this.effects.Remove(DrowningMonitor.drowningEffect);
			}
		}
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x000FA6AC File Offset: 0x000F88AC
	private static bool CellSafeTest(int testCell, object data)
	{
		int num = Grid.CellAbove(testCell);
		if (!Grid.IsValidCell(testCell) || !Grid.IsValidCell(num))
		{
			return false;
		}
		if (Grid.IsSubstantialLiquid(testCell, 0.95f))
		{
			return false;
		}
		if (Grid.IsLiquid(testCell))
		{
			if (Grid.Element[num].IsLiquid)
			{
				return false;
			}
			if (Grid.Element[num].IsSolid)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x000FA70A File Offset: 0x000F890A
	public bool IsCellSafe(int cell)
	{
		return this.occupyArea.TestArea(cell, this, DrowningMonitor.CellSafeTestDelegate);
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000FA71E File Offset: 0x000F891E
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.Drowning };
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000FA72A File Offset: 0x000F892A
	public string WiltStateString
	{
		get
		{
			if (this.livesUnderWater)
			{
				return "    • " + CREATURES.STATUSITEMS.SATURATED.NAME;
			}
			return "    • " + CREATURES.STATUSITEMS.DROWNING.NAME;
		}
	}

	// Token: 0x06002F67 RID: 12135 RVA: 0x000FA75D File Offset: 0x000F895D
	private void OnLiquidChanged(object data)
	{
		this.CheckDrowning(null);
	}

	// Token: 0x06002F68 RID: 12136 RVA: 0x000FA768 File Offset: 0x000F8968
	public void SlicedSim1000ms(float dt)
	{
		this.CheckDrowning(null);
		if (this.drowning)
		{
			if (!this.drowned)
			{
				this.timeToDrown -= dt;
				if (this.timeToDrown <= 0f)
				{
					this.CheckDrowning(null);
					return;
				}
			}
		}
		else
		{
			this.timeToDrown += dt * 5f;
			this.timeToDrown = Mathf.Clamp(this.timeToDrown, 0f, 75f);
		}
	}

	// Token: 0x04001C78 RID: 7288
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001C79 RID: 7289
	[MyCmpGet]
	private Effects effects;

	// Token: 0x04001C7A RID: 7290
	private OccupyArea _occupyArea;

	// Token: 0x04001C7B RID: 7291
	[Serialize]
	[SerializeField]
	private float timeToDrown;

	// Token: 0x04001C7C RID: 7292
	[Serialize]
	private bool drowned;

	// Token: 0x04001C7D RID: 7293
	private bool drowning;

	// Token: 0x04001C7E RID: 7294
	protected const float MaxDrownTime = 75f;

	// Token: 0x04001C7F RID: 7295
	protected const float RegenRate = 5f;

	// Token: 0x04001C80 RID: 7296
	protected const float CellLiquidThreshold = 0.95f;

	// Token: 0x04001C81 RID: 7297
	public bool canDrownToDeath = true;

	// Token: 0x04001C82 RID: 7298
	public bool livesUnderWater;

	// Token: 0x04001C83 RID: 7299
	private Guid drowningStatusGuid;

	// Token: 0x04001C84 RID: 7300
	private Guid saturatedStatusGuid;

	// Token: 0x04001C85 RID: 7301
	private Extents extents;

	// Token: 0x04001C86 RID: 7302
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001C87 RID: 7303
	public static Effect drowningEffect;

	// Token: 0x04001C88 RID: 7304
	public static Effect saturatedEffect;

	// Token: 0x04001C89 RID: 7305
	private static readonly Func<int, object, bool> CellSafeTestDelegate = (int testCell, object data) => DrowningMonitor.CellSafeTest(testCell, data);
}
