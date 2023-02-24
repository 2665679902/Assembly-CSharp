using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200073E RID: 1854
[AddComponentMenu("KMonoBehaviour/Workable/EmptyConduitWorkable")]
public class EmptyConduitWorkable : Workable, IEmptyConduitWorkable
{
	// Token: 0x060032E8 RID: 13032 RVA: 0x001130E8 File Offset: 0x001112E8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		base.SetWorkTime(float.PositiveInfinity);
		this.faceTargetWhenWorking = true;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		base.Subscribe<EmptyConduitWorkable>(2127324410, EmptyConduitWorkable.OnEmptyConduitCancelledDelegate);
		if (EmptyConduitWorkable.emptyLiquidConduitStatusItem == null)
		{
			EmptyConduitWorkable.emptyLiquidConduitStatusItem = new StatusItem("EmptyLiquidConduit", BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.NAME, BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.TOOLTIP, "status_item_empty_pipe", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.LiquidConduits.ID, 66, true, null);
			EmptyConduitWorkable.emptyGasConduitStatusItem = new StatusItem("EmptyGasConduit", BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.NAME, BUILDINGS.PREFABS.CONDUIT.STATUS_ITEM.TOOLTIP, "status_item_empty_pipe", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.GasConduits.ID, 130, true, null);
		}
		this.requiredSkillPerk = Db.Get().SkillPerks.CanDoPlumbing.Id;
		this.shouldShowSkillPerkStatusItem = false;
	}

	// Token: 0x060032E9 RID: 13033 RVA: 0x001131DC File Offset: 0x001113DC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.elapsedTime != -1f)
		{
			this.MarkForEmptying();
		}
	}

	// Token: 0x060032EA RID: 13034 RVA: 0x001131F8 File Offset: 0x001113F8
	public void MarkForEmptying()
	{
		if (this.chore == null && this.HasContents())
		{
			StatusItem statusItem = this.GetStatusItem();
			base.GetComponent<KSelectable>().ToggleStatusItem(statusItem, true, null);
			this.CreateWorkChore();
		}
	}

	// Token: 0x060032EB RID: 13035 RVA: 0x00113234 File Offset: 0x00111434
	private bool HasContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.GetFlowManager().GetContents(num).mass > 0f;
	}

	// Token: 0x060032EC RID: 13036 RVA: 0x0011326D File Offset: 0x0011146D
	private void CancelEmptying()
	{
		this.CleanUpVisualization();
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel");
			this.chore = null;
			this.shouldShowSkillPerkStatusItem = false;
			this.UpdateStatusItem(null);
		}
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x001132A4 File Offset: 0x001114A4
	private void CleanUpVisualization()
	{
		StatusItem statusItem = this.GetStatusItem();
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.ToggleStatusItem(statusItem, false, null);
		}
		this.elapsedTime = -1f;
		if (this.chore != null)
		{
			base.GetComponent<Prioritizable>().RemoveRef();
		}
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x001132F0 File Offset: 0x001114F0
	protected override void OnCleanUp()
	{
		this.CancelEmptying();
		base.OnCleanUp();
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x001132FE File Offset: 0x001114FE
	private ConduitFlow GetFlowManager()
	{
		if (this.conduit.type != ConduitType.Gas)
		{
			return Game.Instance.liquidConduitFlow;
		}
		return Game.Instance.gasConduitFlow;
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x00113323 File Offset: 0x00111523
	private void OnEmptyConduitCancelled(object data)
	{
		this.CancelEmptying();
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x0011332C File Offset: 0x0011152C
	private StatusItem GetStatusItem()
	{
		ConduitType type = this.conduit.type;
		StatusItem statusItem;
		if (type != ConduitType.Gas)
		{
			if (type != ConduitType.Liquid)
			{
				throw new ArgumentException();
			}
			statusItem = EmptyConduitWorkable.emptyLiquidConduitStatusItem;
		}
		else
		{
			statusItem = EmptyConduitWorkable.emptyGasConduitStatusItem;
		}
		return statusItem;
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x00113368 File Offset: 0x00111568
	private void CreateWorkChore()
	{
		base.GetComponent<Prioritizable>().AddRef();
		this.chore = new WorkChore<EmptyConduitWorkable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanDoPlumbing.Id);
		this.elapsedTime = 0f;
		this.emptiedPipe = false;
		this.shouldShowSkillPerkStatusItem = true;
		this.UpdateStatusItem(null);
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x001133F8 File Offset: 0x001115F8
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.elapsedTime == -1f)
		{
			return true;
		}
		bool flag = false;
		this.elapsedTime += dt;
		if (!this.emptiedPipe)
		{
			if (this.elapsedTime > 4f)
			{
				this.EmptyContents();
				this.emptiedPipe = true;
				this.elapsedTime = 0f;
			}
		}
		else if (this.elapsedTime > 2f)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			if (this.GetFlowManager().GetContents(num).mass > 0f)
			{
				this.elapsedTime = 0f;
				this.emptiedPipe = false;
			}
			else
			{
				this.CleanUpVisualization();
				this.chore = null;
				flag = true;
				this.shouldShowSkillPerkStatusItem = false;
				this.UpdateStatusItem(null);
			}
		}
		return flag;
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x001134C1 File Offset: 0x001116C1
	public override bool InstantlyFinish(Worker worker)
	{
		worker.Work(4f);
		return true;
	}

	// Token: 0x060032F5 RID: 13045 RVA: 0x001134D0 File Offset: 0x001116D0
	public void EmptyContents()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		ConduitFlow.ConduitContents conduitContents = this.GetFlowManager().RemoveElement(num, float.PositiveInfinity);
		this.elapsedTime = 0f;
		if (conduitContents.mass > 0f && conduitContents.element != SimHashes.Vacuum)
		{
			ConduitType type = this.conduit.type;
			IChunkManager chunkManager;
			if (type != ConduitType.Gas)
			{
				if (type != ConduitType.Liquid)
				{
					throw new ArgumentException();
				}
				chunkManager = LiquidSourceManager.Instance;
			}
			else
			{
				chunkManager = GasSourceManager.Instance;
			}
			chunkManager.CreateChunk(conduitContents.element, conduitContents.mass, conduitContents.temperature, conduitContents.diseaseIdx, conduitContents.diseaseCount, Grid.CellToPosCCC(num, Grid.SceneLayer.Ore));
		}
	}

	// Token: 0x060032F6 RID: 13046 RVA: 0x00113580 File Offset: 0x00111780
	public override float GetPercentComplete()
	{
		return Mathf.Clamp01(this.elapsedTime / 4f);
	}

	// Token: 0x04001F53 RID: 8019
	[MyCmpReq]
	private Conduit conduit;

	// Token: 0x04001F54 RID: 8020
	private static StatusItem emptyLiquidConduitStatusItem;

	// Token: 0x04001F55 RID: 8021
	private static StatusItem emptyGasConduitStatusItem;

	// Token: 0x04001F56 RID: 8022
	private Chore chore;

	// Token: 0x04001F57 RID: 8023
	private const float RECHECK_PIPE_INTERVAL = 2f;

	// Token: 0x04001F58 RID: 8024
	private const float TIME_TO_EMPTY_PIPE = 4f;

	// Token: 0x04001F59 RID: 8025
	private const float NO_EMPTY_SCHEDULED = -1f;

	// Token: 0x04001F5A RID: 8026
	[Serialize]
	private float elapsedTime = -1f;

	// Token: 0x04001F5B RID: 8027
	private bool emptiedPipe = true;

	// Token: 0x04001F5C RID: 8028
	private static readonly EventSystem.IntraObjectHandler<EmptyConduitWorkable> OnEmptyConduitCancelledDelegate = new EventSystem.IntraObjectHandler<EmptyConduitWorkable>(delegate(EmptyConduitWorkable component, object data)
	{
		component.OnEmptyConduitCancelled(data);
	});
}
