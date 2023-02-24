using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DD RID: 989
public abstract class Reactable
{
	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06001476 RID: 5238 RVA: 0x0006C22A File Offset: 0x0006A42A
	public bool IsValid
	{
		get
		{
			return this.partitionerEntry.IsValid();
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06001477 RID: 5239 RVA: 0x0006C237 File Offset: 0x0006A437
	// (set) Token: 0x06001478 RID: 5240 RVA: 0x0006C23F File Offset: 0x0006A43F
	public float creationTime { get; private set; }

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06001479 RID: 5241 RVA: 0x0006C248 File Offset: 0x0006A448
	public bool IsReacting
	{
		get
		{
			return this.reactor != null;
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x0006C258 File Offset: 0x0006A458
	public Reactable(GameObject gameObject, HashedString id, ChoreType chore_type, int range_width = 15, int range_height = 8, bool follow_transform = false, float globalCooldown = 0f, float localCooldown = 0f, float lifeSpan = float.PositiveInfinity, float max_initial_delay = 0f, ObjectLayer overrideLayer = ObjectLayer.NumLayers)
	{
		this.rangeHeight = range_height;
		this.rangeWidth = range_width;
		this.id = id;
		this.gameObject = gameObject;
		this.choreType = chore_type;
		this.globalCooldown = globalCooldown;
		this.localCooldown = localCooldown;
		this.lifeSpan = lifeSpan;
		this.initialDelay = ((max_initial_delay > 0f) ? UnityEngine.Random.Range(0f, max_initial_delay) : 0f);
		this.creationTime = GameClock.Instance.GetTime();
		ObjectLayer objectLayer = ((overrideLayer == ObjectLayer.NumLayers) ? this.reactionLayer : overrideLayer);
		ReactionMonitor.Def def = gameObject.GetDef<ReactionMonitor.Def>();
		if (overrideLayer != objectLayer && def != null)
		{
			objectLayer = def.ReactionLayer;
		}
		this.reactionLayer = objectLayer;
		this.Initialize(follow_transform);
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x0006C334 File Offset: 0x0006A534
	public void Initialize(bool followTransform)
	{
		this.UpdateLocation();
		if (followTransform)
		{
			this.transformId = Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(this.gameObject.transform, new System.Action(this.UpdateLocation), "Reactable follow transform");
		}
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x0006C36B File Offset: 0x0006A56B
	public void Begin(GameObject reactor)
	{
		this.reactor = reactor;
		this.lastTriggerTime = GameClock.Instance.GetTime();
		this.InternalBegin();
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x0006C38C File Offset: 0x0006A58C
	public void End()
	{
		this.InternalEnd();
		if (this.reactor != null)
		{
			GameObject gameObject = this.reactor;
			this.InternalEnd();
			this.reactor = null;
			if (gameObject != null)
			{
				ReactionMonitor.Instance smi = gameObject.GetSMI<ReactionMonitor.Instance>();
				if (smi != null)
				{
					smi.StopReaction();
				}
			}
		}
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x0006C3DC File Offset: 0x0006A5DC
	public bool CanBegin(GameObject reactor, Navigator.ActiveTransition transition)
	{
		float time = GameClock.Instance.GetTime();
		float num = time - this.creationTime;
		float num2 = time - this.lastTriggerTime;
		if (num < this.initialDelay || num2 < this.globalCooldown)
		{
			return false;
		}
		ChoreConsumer component = reactor.GetComponent<ChoreConsumer>();
		Chore chore = ((component != null) ? component.choreDriver.GetCurrentChore() : null);
		if (chore == null || this.choreType.priority <= chore.choreType.priority)
		{
			return false;
		}
		int num3 = 0;
		while (this.additionalPreconditions != null && num3 < this.additionalPreconditions.Count)
		{
			if (!this.additionalPreconditions[num3](reactor, transition))
			{
				return false;
			}
			num3++;
		}
		return this.InternalCanBegin(reactor, transition);
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x0006C496 File Offset: 0x0006A696
	public bool IsExpired()
	{
		return GameClock.Instance.GetTime() - this.creationTime > this.lifeSpan;
	}

	// Token: 0x06001480 RID: 5248
	public abstract bool InternalCanBegin(GameObject reactor, Navigator.ActiveTransition transition);

	// Token: 0x06001481 RID: 5249
	public abstract void Update(float dt);

	// Token: 0x06001482 RID: 5250
	protected abstract void InternalBegin();

	// Token: 0x06001483 RID: 5251
	protected abstract void InternalEnd();

	// Token: 0x06001484 RID: 5252
	protected abstract void InternalCleanup();

	// Token: 0x06001485 RID: 5253 RVA: 0x0006C4B4 File Offset: 0x0006A6B4
	public void Cleanup()
	{
		this.End();
		this.InternalCleanup();
		if (this.transformId != -1)
		{
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(this.transformId, new System.Action(this.UpdateLocation));
			this.transformId = -1;
		}
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x0006C50C File Offset: 0x0006A70C
	private void UpdateLocation()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (this.gameObject != null)
		{
			this.sourceCell = Grid.PosToCell(this.gameObject);
			Extents extents = new Extents(Grid.PosToXY(this.gameObject.transform.GetPosition()).x - this.rangeWidth / 2, Grid.PosToXY(this.gameObject.transform.GetPosition()).y - this.rangeHeight / 2, this.rangeWidth, this.rangeHeight);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("Reactable", this, extents, GameScenePartitioner.Instance.objectLayers[(int)this.reactionLayer], null);
		}
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x0006C5CD File Offset: 0x0006A7CD
	public Reactable AddPrecondition(Reactable.ReactablePrecondition precondition)
	{
		if (this.additionalPreconditions == null)
		{
			this.additionalPreconditions = new List<Reactable.ReactablePrecondition>();
		}
		this.additionalPreconditions.Add(precondition);
		return this;
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x0006C5EF File Offset: 0x0006A7EF
	public void InsertPrecondition(int index, Reactable.ReactablePrecondition precondition)
	{
		if (this.additionalPreconditions == null)
		{
			this.additionalPreconditions = new List<Reactable.ReactablePrecondition>();
		}
		index = Math.Min(index, this.additionalPreconditions.Count);
		this.additionalPreconditions.Insert(index, precondition);
	}

	// Token: 0x04000B78 RID: 2936
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000B79 RID: 2937
	protected GameObject gameObject;

	// Token: 0x04000B7A RID: 2938
	public HashedString id;

	// Token: 0x04000B7B RID: 2939
	public bool preventChoreInterruption = true;

	// Token: 0x04000B7C RID: 2940
	public int sourceCell;

	// Token: 0x04000B7D RID: 2941
	private int rangeWidth;

	// Token: 0x04000B7E RID: 2942
	private int rangeHeight;

	// Token: 0x04000B7F RID: 2943
	private int transformId = -1;

	// Token: 0x04000B80 RID: 2944
	public float globalCooldown;

	// Token: 0x04000B81 RID: 2945
	public float localCooldown;

	// Token: 0x04000B82 RID: 2946
	public float lifeSpan = float.PositiveInfinity;

	// Token: 0x04000B83 RID: 2947
	private float lastTriggerTime = -2.1474836E+09f;

	// Token: 0x04000B84 RID: 2948
	private float initialDelay;

	// Token: 0x04000B86 RID: 2950
	protected GameObject reactor;

	// Token: 0x04000B87 RID: 2951
	private ChoreType choreType;

	// Token: 0x04000B88 RID: 2952
	protected LoggerFSS log;

	// Token: 0x04000B89 RID: 2953
	private List<Reactable.ReactablePrecondition> additionalPreconditions;

	// Token: 0x04000B8A RID: 2954
	private ObjectLayer reactionLayer;

	// Token: 0x02000FFB RID: 4091
	// (Invoke) Token: 0x06007113 RID: 28947
	public delegate bool ReactablePrecondition(GameObject go, Navigator.ActiveTransition transition);
}
