using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public abstract class KAnimControllerBase : MonoBehaviour
{
	// Token: 0x060015AD RID: 5549 RVA: 0x00070948 File Offset: 0x0006EB48
	protected KAnimControllerBase()
	{
		this.previousFrame = -1;
		this.currentFrame = -1;
		this.PlaySpeedMultiplier = 1f;
		this.synchronizer = new KAnimSynchronizer(this);
		this.layering = new KAnimLayering(this, this.fgLayer);
		this.isVisible = true;
	}

	// Token: 0x060015AE RID: 5550
	public abstract KAnim.Anim GetAnim(int index);

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060015AF RID: 5551 RVA: 0x00070A58 File Offset: 0x0006EC58
	// (set) Token: 0x060015B0 RID: 5552 RVA: 0x00070A60 File Offset: 0x0006EC60
	public string debugName { get; private set; }

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00070A69 File Offset: 0x0006EC69
	// (set) Token: 0x060015B2 RID: 5554 RVA: 0x00070A71 File Offset: 0x0006EC71
	public KAnim.Build curBuild { get; protected set; }

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x060015B3 RID: 5555 RVA: 0x00070A7C File Offset: 0x0006EC7C
	// (remove) Token: 0x060015B4 RID: 5556 RVA: 0x00070AB4 File Offset: 0x0006ECB4
	public event Action<Color32> OnOverlayColourChanged;

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00070AE9 File Offset: 0x0006ECE9
	// (set) Token: 0x060015B6 RID: 5558 RVA: 0x00070AF1 File Offset: 0x0006ECF1
	public new bool enabled
	{
		get
		{
			return this._enabled;
		}
		set
		{
			this._enabled = value;
			if (!this.hasAwakeRun)
			{
				return;
			}
			if (this._enabled)
			{
				this.Enable();
				return;
			}
			this.Disable();
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00070B18 File Offset: 0x0006ED18
	public bool HasBatchInstanceData
	{
		get
		{
			return this.batchInstanceData != null;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00070B23 File Offset: 0x0006ED23
	// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00070B2B File Offset: 0x0006ED2B
	public SymbolInstanceGpuData symbolInstanceGpuData { get; protected set; }

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060015BA RID: 5562 RVA: 0x00070B34 File Offset: 0x0006ED34
	// (set) Token: 0x060015BB RID: 5563 RVA: 0x00070B3C File Offset: 0x0006ED3C
	public SymbolOverrideInfoGpuData symbolOverrideInfoGpuData { get; protected set; }

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060015BC RID: 5564 RVA: 0x00070B45 File Offset: 0x0006ED45
	// (set) Token: 0x060015BD RID: 5565 RVA: 0x00070B58 File Offset: 0x0006ED58
	public Color32 TintColour
	{
		get
		{
			return this.batchInstanceData.GetTintColour();
		}
		set
		{
			if (this.batchInstanceData != null && this.batchInstanceData.SetTintColour(value))
			{
				this.SetDirty();
				this.SuspendUpdates(false);
				if (this.OnTintChanged != null)
				{
					this.OnTintChanged(value);
				}
			}
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060015BE RID: 5566 RVA: 0x00070BA6 File Offset: 0x0006EDA6
	// (set) Token: 0x060015BF RID: 5567 RVA: 0x00070BB8 File Offset: 0x0006EDB8
	public Color32 HighlightColour
	{
		get
		{
			return this.batchInstanceData.GetHighlightcolour();
		}
		set
		{
			if (this.batchInstanceData.SetHighlightColour(value))
			{
				this.SetDirty();
				this.SuspendUpdates(false);
				if (this.OnHighlightChanged != null)
				{
					this.OnHighlightChanged(value);
				}
			}
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00070BF3 File Offset: 0x0006EDF3
	// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00070C00 File Offset: 0x0006EE00
	public Color OverlayColour
	{
		get
		{
			return this.batchInstanceData.GetOverlayColour();
		}
		set
		{
			if (this.batchInstanceData.SetOverlayColour(value))
			{
				this.SetDirty();
				this.SuspendUpdates(false);
				if (this.OnOverlayColourChanged != null)
				{
					this.OnOverlayColourChanged(value);
				}
			}
		}
	}

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060015C2 RID: 5570 RVA: 0x00070C38 File Offset: 0x0006EE38
	// (remove) Token: 0x060015C3 RID: 5571 RVA: 0x00070C70 File Offset: 0x0006EE70
	public event KAnimControllerBase.KAnimEvent onAnimEnter;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060015C4 RID: 5572 RVA: 0x00070CA8 File Offset: 0x0006EEA8
	// (remove) Token: 0x060015C5 RID: 5573 RVA: 0x00070CE0 File Offset: 0x0006EEE0
	public event KAnimControllerBase.KAnimEvent onAnimComplete;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060015C6 RID: 5574 RVA: 0x00070D18 File Offset: 0x0006EF18
	// (remove) Token: 0x060015C7 RID: 5575 RVA: 0x00070D50 File Offset: 0x0006EF50
	public event Action<int> onLayerChanged;

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00070D85 File Offset: 0x0006EF85
	// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00070D8D File Offset: 0x0006EF8D
	public int previousFrame { get; protected set; }

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060015CA RID: 5578 RVA: 0x00070D96 File Offset: 0x0006EF96
	// (set) Token: 0x060015CB RID: 5579 RVA: 0x00070D9E File Offset: 0x0006EF9E
	public int currentFrame { get; protected set; }

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060015CC RID: 5580 RVA: 0x00070DA8 File Offset: 0x0006EFA8
	public HashedString currentAnim
	{
		get
		{
			if (this.curAnim == null)
			{
				return default(HashedString);
			}
			return this.curAnim.hash;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060015CE RID: 5582 RVA: 0x00070DDB File Offset: 0x0006EFDB
	// (set) Token: 0x060015CD RID: 5581 RVA: 0x00070DD2 File Offset: 0x0006EFD2
	public float PlaySpeedMultiplier { get; set; }

	// Token: 0x060015CF RID: 5583 RVA: 0x00070DE3 File Offset: 0x0006EFE3
	public void SetFGLayer(Grid.SceneLayer layer)
	{
		this.fgLayer = layer;
		this.GetLayering();
		if (this.layering != null)
		{
			this.layering.SetLayer(this.fgLayer);
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00070E0C File Offset: 0x0006F00C
	// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00070E14 File Offset: 0x0006F014
	public KAnim.PlayMode PlayMode
	{
		get
		{
			return this.mode;
		}
		set
		{
			this.mode = value;
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00070E1D File Offset: 0x0006F01D
	// (set) Token: 0x060015D3 RID: 5587 RVA: 0x00070E25 File Offset: 0x0006F025
	public bool FlipX
	{
		get
		{
			return this.flipX;
		}
		set
		{
			this.flipX = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00070E47 File Offset: 0x0006F047
	// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00070E4F File Offset: 0x0006F04F
	public bool FlipY
	{
		get
		{
			return this.flipY;
		}
		set
		{
			this.flipY = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00070E71 File Offset: 0x0006F071
	// (set) Token: 0x060015D7 RID: 5591 RVA: 0x00070E79 File Offset: 0x0006F079
	public Vector3 Offset
	{
		get
		{
			return this.offset;
		}
		set
		{
			this.offset = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.DeRegister();
			this.Register();
			this.RefreshVisibilityListener();
			this.SetDirty();
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00070EAD File Offset: 0x0006F0AD
	// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00070EB5 File Offset: 0x0006F0B5
	public float Rotation
	{
		get
		{
			return this.rotation;
		}
		set
		{
			this.rotation = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060015DA RID: 5594 RVA: 0x00070ED7 File Offset: 0x0006F0D7
	// (set) Token: 0x060015DB RID: 5595 RVA: 0x00070EDF File Offset: 0x0006F0DF
	public Vector3 Pivot
	{
		get
		{
			return this.pivot;
		}
		set
		{
			this.pivot = value;
			if (this.layering != null)
			{
				this.layering.Dirty();
			}
			this.SetDirty();
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060015DC RID: 5596 RVA: 0x00070F01 File Offset: 0x0006F101
	public Vector3 PositionIncludingOffset
	{
		get
		{
			return base.transform.GetPosition() + this.Offset;
		}
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00070F19 File Offset: 0x0006F119
	public KAnimBatchGroup.MaterialType GetMaterialType()
	{
		return this.materialType;
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x00070F24 File Offset: 0x0006F124
	public Vector3 GetWorldPivot()
	{
		Vector3 position = base.transform.GetPosition();
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		if (component != null)
		{
			position.x += component.offset.x;
			position.y += component.offset.y - component.size.y / 2f;
		}
		return position;
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x00070F8C File Offset: 0x0006F18C
	public KAnim.Anim GetCurrentAnim()
	{
		return this.curAnim;
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x00070F94 File Offset: 0x0006F194
	public KAnimHashedString GetBuildHash()
	{
		if (this.curBuild == null)
		{
			return KAnimBatchManager.NO_BATCH;
		}
		return this.curBuild.fileHash;
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x00070FB4 File Offset: 0x0006F1B4
	protected float GetDuration()
	{
		if (this.curAnim != null)
		{
			return (float)this.curAnim.numFrames / this.curAnim.frameRate;
		}
		return 0f;
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x00070FDC File Offset: 0x0006F1DC
	protected int GetFrameIdxFromOffset(int offset)
	{
		int num = -1;
		if (this.curAnim != null)
		{
			num = offset + this.curAnim.firstFrameIdx;
		}
		return num;
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x00071004 File Offset: 0x0006F204
	public int GetFrameIdx(float time, bool absolute)
	{
		int num = -1;
		if (this.curAnim != null)
		{
			num = this.curAnim.GetFrameIdx(this.mode, time) + (absolute ? this.curAnim.firstFrameIdx : 0);
		}
		return num;
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x00071041 File Offset: 0x0006F241
	public bool IsStopped()
	{
		return this.stopped;
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060015E5 RID: 5605 RVA: 0x00071049 File Offset: 0x0006F249
	public KAnim.Anim CurrentAnim
	{
		get
		{
			return this.curAnim;
		}
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x00071051 File Offset: 0x0006F251
	public KAnimSynchronizer GetSynchronizer()
	{
		return this.synchronizer;
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x00071059 File Offset: 0x0006F259
	public KAnimLayering GetLayering()
	{
		if (this.layering == null && this.fgLayer != Grid.SceneLayer.NoLayer)
		{
			this.layering = new KAnimLayering(this, this.fgLayer);
		}
		return this.layering;
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x00071085 File Offset: 0x0006F285
	public KAnim.PlayMode GetMode()
	{
		return this.mode;
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x0007108D File Offset: 0x0006F28D
	public static string GetModeString(KAnim.PlayMode mode)
	{
		switch (mode)
		{
		case KAnim.PlayMode.Loop:
			return "Loop";
		case KAnim.PlayMode.Once:
			return "Once";
		case KAnim.PlayMode.Paused:
			return "Paused";
		default:
			return "Unknown";
		}
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x000710BA File Offset: 0x0006F2BA
	public float GetPlaySpeed()
	{
		return this.playSpeed;
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x000710C2 File Offset: 0x0006F2C2
	public void SetElapsedTime(float value)
	{
		this.elapsedTime = value;
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000710CB File Offset: 0x0006F2CB
	public float GetElapsedTime()
	{
		return this.elapsedTime;
	}

	// Token: 0x060015ED RID: 5613
	protected abstract void SuspendUpdates(bool suspend);

	// Token: 0x060015EE RID: 5614
	protected abstract void OnStartQueuedAnim();

	// Token: 0x060015EF RID: 5615
	public abstract void SetDirty();

	// Token: 0x060015F0 RID: 5616
	protected abstract void RefreshVisibilityListener();

	// Token: 0x060015F1 RID: 5617
	protected abstract void DeRegister();

	// Token: 0x060015F2 RID: 5618
	protected abstract void Register();

	// Token: 0x060015F3 RID: 5619
	protected abstract void OnAwake();

	// Token: 0x060015F4 RID: 5620
	protected abstract void OnStart();

	// Token: 0x060015F5 RID: 5621
	protected abstract void OnStop();

	// Token: 0x060015F6 RID: 5622
	protected abstract void Enable();

	// Token: 0x060015F7 RID: 5623
	protected abstract void Disable();

	// Token: 0x060015F8 RID: 5624
	protected abstract void UpdateFrame(float t);

	// Token: 0x060015F9 RID: 5625
	public abstract Matrix2x3 GetTransformMatrix();

	// Token: 0x060015FA RID: 5626
	public abstract Matrix2x3 GetSymbolLocalTransform(HashedString symbol, out bool symbolVisible);

	// Token: 0x060015FB RID: 5627
	public abstract void UpdateHidden();

	// Token: 0x060015FC RID: 5628
	public abstract void TriggerStop();

	// Token: 0x060015FD RID: 5629 RVA: 0x000710D3 File Offset: 0x0006F2D3
	public virtual void SetLayer(int layer)
	{
		if (this.onLayerChanged != null)
		{
			this.onLayerChanged(layer);
		}
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x000710EC File Offset: 0x0006F2EC
	public Vector3 GetPivotSymbolPosition()
	{
		bool flag = false;
		Matrix4x4 symbolTransform = this.GetSymbolTransform(KAnimControllerBase.snaptoPivot, out flag);
		Vector3 position = base.transform.GetPosition();
		if (flag)
		{
			position = new Vector3(symbolTransform[0, 3], symbolTransform[1, 3], symbolTransform[2, 3]);
		}
		return position;
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x0007113B File Offset: 0x0006F33B
	public virtual Matrix4x4 GetSymbolTransform(HashedString symbol, out bool symbolVisible)
	{
		symbolVisible = false;
		return Matrix4x4.identity;
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x00071148 File Offset: 0x0006F348
	private void Awake()
	{
		this.aem = Singleton<AnimEventManager>.Instance;
		this.debugName = base.name;
		this.SetFGLayer(this.fgLayer);
		this.OnAwake();
		if (!string.IsNullOrEmpty(this.initialAnim))
		{
			this.SetDirty();
			this.Play(this.initialAnim, this.initialMode, 1f, 0f);
		}
		this.hasAwakeRun = true;
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x000711B9 File Offset: 0x0006F3B9
	private void Start()
	{
		this.OnStart();
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x000711C4 File Offset: 0x0006F3C4
	protected virtual void OnDestroy()
	{
		this.animFiles = null;
		this.curAnim = null;
		this.curBuild = null;
		this.synchronizer = null;
		this.layering = null;
		this.animQueue = null;
		this.overrideAnims = null;
		this.anims = null;
		this.synchronizer = null;
		this.layering = null;
		this.overrideAnimFiles = null;
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x0007121E File Offset: 0x0006F41E
	protected void AnimEnter(HashedString hashed_name)
	{
		if (this.onAnimEnter != null)
		{
			this.onAnimEnter(hashed_name);
		}
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x00071234 File Offset: 0x0006F434
	public void Play(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		if (!this.stopped)
		{
			this.Stop();
		}
		this.Queue(anim_name, mode, speed, time_offset);
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x00071250 File Offset: 0x0006F450
	public void Play(HashedString[] anim_names, KAnim.PlayMode mode = KAnim.PlayMode.Once)
	{
		if (!this.stopped)
		{
			this.Stop();
		}
		for (int i = 0; i < anim_names.Length - 1; i++)
		{
			this.Queue(anim_names[i], KAnim.PlayMode.Once, 1f, 0f);
		}
		global::Debug.Assert(anim_names.Length != 0, "Play was called with an empty anim array");
		this.Queue(anim_names[anim_names.Length - 1], mode, 1f, 0f);
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x000712C0 File Offset: 0x0006F4C0
	public void Queue(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		this.animQueue.Enqueue(new KAnimControllerBase.AnimData
		{
			anim = anim_name,
			mode = mode,
			speed = speed,
			timeOffset = time_offset
		});
		this.mode = ((mode == KAnim.PlayMode.Paused) ? KAnim.PlayMode.Paused : KAnim.PlayMode.Once);
		if (this.aem != null)
		{
			this.aem.SetMode(this.eventManagerHandle, this.mode);
		}
		if (this.animQueue.Count == 1 && this.stopped)
		{
			this.StartQueuedAnim();
		}
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x0007134B File Offset: 0x0006F54B
	public void QueueAndSyncTransition(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		this.SyncTransition();
		this.Queue(anim_name, mode, speed, time_offset);
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x0007135E File Offset: 0x0006F55E
	public void SyncTransition()
	{
		this.elapsedTime %= Mathf.Max(float.Epsilon, this.GetDuration());
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x0007137D File Offset: 0x0006F57D
	public void ClearQueue()
	{
		this.animQueue.Clear();
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x0007138C File Offset: 0x0006F58C
	private void Restart(HashedString anim_name, KAnim.PlayMode mode = KAnim.PlayMode.Once, float speed = 1f, float time_offset = 0f)
	{
		if (this.curBuild == null)
		{
			string[] array = new string[5];
			array[0] = "[";
			array[1] = base.gameObject.name;
			array[2] = "] Missing build while trying to play anim [";
			int num = 3;
			HashedString hashedString = anim_name;
			array[num] = hashedString.ToString();
			array[4] = "]";
			global::Debug.LogWarning(string.Concat(array), base.gameObject);
			return;
		}
		Queue<KAnimControllerBase.AnimData> queue = new Queue<KAnimControllerBase.AnimData>();
		queue.Enqueue(new KAnimControllerBase.AnimData
		{
			anim = anim_name,
			mode = mode,
			speed = speed,
			timeOffset = time_offset
		});
		while (this.animQueue.Count > 0)
		{
			queue.Enqueue(this.animQueue.Dequeue());
		}
		this.animQueue = queue;
		if (this.animQueue.Count == 1 && this.stopped)
		{
			this.StartQueuedAnim();
		}
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x0007146C File Offset: 0x0006F66C
	protected void StartQueuedAnim()
	{
		this.StopAnimEventSequence();
		this.previousFrame = -1;
		this.currentFrame = -1;
		this.SuspendUpdates(false);
		this.stopped = false;
		this.OnStartQueuedAnim();
		KAnimControllerBase.AnimData animData = this.animQueue.Dequeue();
		while (animData.mode == KAnim.PlayMode.Loop && this.animQueue.Count > 0)
		{
			animData = this.animQueue.Dequeue();
		}
		KAnimControllerBase.AnimLookupData animLookupData;
		if (this.overrideAnims == null || !this.overrideAnims.TryGetValue(animData.anim, out animLookupData))
		{
			if (!this.anims.TryGetValue(animData.anim, out animLookupData))
			{
				bool flag = true;
				if (this.showWhenMissing != null)
				{
					this.showWhenMissing.SetActive(true);
				}
				if (flag)
				{
					this.TriggerStop();
					return;
				}
			}
			else if (this.showWhenMissing != null)
			{
				this.showWhenMissing.SetActive(false);
			}
		}
		this.curAnim = this.GetAnim(animLookupData.animIndex);
		int num = 0;
		if (animData.mode == KAnim.PlayMode.Loop && this.randomiseLoopedOffset)
		{
			num = UnityEngine.Random.Range(0, this.curAnim.numFrames - 1);
		}
		this.prevAnimFrame = -1;
		this.curAnimFrameIdx = this.GetFrameIdxFromOffset(num);
		this.currentFrame = this.curAnimFrameIdx;
		this.mode = animData.mode;
		this.playSpeed = animData.speed * this.PlaySpeedMultiplier;
		this.SetElapsedTime((float)num / this.curAnim.frameRate + animData.timeOffset);
		this.synchronizer.Sync();
		this.StartAnimEventSequence();
		this.AnimEnter(animData.anim);
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000715F0 File Offset: 0x0006F7F0
	public bool GetSymbolVisiblity(KAnimHashedString symbol)
	{
		return !this.hiddenSymbols.Contains(symbol);
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x00071601 File Offset: 0x0006F801
	public void SetSymbolVisiblity(KAnimHashedString symbol, bool is_visible)
	{
		if (is_visible)
		{
			this.hiddenSymbols.Remove(symbol);
		}
		else if (!this.hiddenSymbols.Contains(symbol))
		{
			this.hiddenSymbols.Add(symbol);
		}
		if (this.curBuild != null)
		{
			this.UpdateHidden();
		}
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x00071640 File Offset: 0x0006F840
	public void AddAnimOverrides(KAnimFile kanim_file, float priority = 0f)
	{
		global::Debug.Assert(kanim_file != null);
		if (kanim_file.GetData().build != null && kanim_file.GetData().build.symbols.Length != 0)
		{
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			DebugUtil.Assert(component != null, "Anim overrides containing additional symbols require a symbol override controller.");
			component.AddBuildOverride(kanim_file.GetData(), 0);
		}
		this.overrideAnimFiles.Add(new KAnimControllerBase.OverrideAnimFileData
		{
			priority = priority,
			file = kanim_file
		});
		this.overrideAnimFiles.Sort((KAnimControllerBase.OverrideAnimFileData a, KAnimControllerBase.OverrideAnimFileData b) => b.priority.CompareTo(a.priority));
		this.RebuildOverrides(kanim_file);
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x000716F4 File Offset: 0x0006F8F4
	public void RemoveAnimOverrides(KAnimFile kanim_file)
	{
		global::Debug.Assert(kanim_file != null);
		if (kanim_file.GetData().build != null && kanim_file.GetData().build.symbols.Length != 0)
		{
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			DebugUtil.Assert(component != null, "Anim overrides containing additional symbols require a symbol override controller.");
			component.TryRemoveBuildOverride(kanim_file.GetData(), 0);
		}
		for (int i = 0; i < this.overrideAnimFiles.Count; i++)
		{
			if (this.overrideAnimFiles[i].file == kanim_file)
			{
				this.overrideAnimFiles.RemoveAt(i);
				break;
			}
		}
		this.RebuildOverrides(kanim_file);
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x00071794 File Offset: 0x0006F994
	private void RebuildOverrides(KAnimFile kanim_file)
	{
		bool flag = false;
		this.overrideAnims.Clear();
		for (int i = 0; i < this.overrideAnimFiles.Count; i++)
		{
			KAnimControllerBase.OverrideAnimFileData overrideAnimFileData = this.overrideAnimFiles[i];
			KAnimFileData data = overrideAnimFileData.file.GetData();
			for (int j = 0; j < data.animCount; j++)
			{
				KAnim.Anim anim = data.GetAnim(j);
				if (anim.animFile.hashName != data.hashName)
				{
					global::Debug.LogError(string.Format("How did we get an anim from another file? [{0}] != [{1}] for anim [{2}]", data.name, anim.animFile.name, j));
				}
				KAnimControllerBase.AnimLookupData animLookupData = default(KAnimControllerBase.AnimLookupData);
				animLookupData.animIndex = anim.index;
				HashedString hashedString = new HashedString(anim.name);
				if (!this.overrideAnims.ContainsKey(hashedString))
				{
					this.overrideAnims[hashedString] = animLookupData;
				}
				if (this.curAnim != null && this.curAnim.hash == hashedString && overrideAnimFileData.file == kanim_file)
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			this.Restart(this.curAnim.name, this.mode, this.playSpeed, 0f);
		}
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000718E4 File Offset: 0x0006FAE4
	public bool HasAnimation(HashedString anim_name)
	{
		bool flag = anim_name.IsValid;
		if (flag)
		{
			bool flag2 = this.anims.ContainsKey(anim_name);
			bool flag3 = !flag2 && this.overrideAnims.ContainsKey(anim_name);
			flag = flag2 || flag3;
		}
		return flag;
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x00071920 File Offset: 0x0006FB20
	public bool HasAnimationFile(KAnimHashedString anim_file_name)
	{
		KAnimFile kanimFile = null;
		return this.TryGetAnimationFile(anim_file_name, out kanimFile);
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x00071938 File Offset: 0x0006FB38
	public bool TryGetAnimationFile(KAnimHashedString anim_file_name, out KAnimFile match)
	{
		match = null;
		if (!anim_file_name.IsValid())
		{
			return false;
		}
		KAnimFileData kanimFileData = null;
		int num = 0;
		int num2 = this.overrideAnimFiles.Count - 1;
		int num3 = (int)((float)this.overrideAnimFiles.Count * 0.5f);
		while (num3 > 0 && match == null && num < num3)
		{
			if (this.overrideAnimFiles[num].file != null)
			{
				kanimFileData = this.overrideAnimFiles[num].file.GetData();
			}
			if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
			{
				match = this.overrideAnimFiles[num].file;
				break;
			}
			if (this.overrideAnimFiles[num2].file != null)
			{
				kanimFileData = this.overrideAnimFiles[num2].file.GetData();
			}
			if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
			{
				match = this.overrideAnimFiles[num2].file;
			}
			num++;
			num2--;
		}
		if (match == null && this.overrideAnimFiles.Count % 2 != 0)
		{
			if (this.overrideAnimFiles[num].file != null)
			{
				kanimFileData = this.overrideAnimFiles[num].file.GetData();
			}
			if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
			{
				match = this.overrideAnimFiles[num].file;
			}
		}
		kanimFileData = null;
		if (match == null && this.animFiles != null)
		{
			num = 0;
			num2 = this.animFiles.Length - 1;
			num3 = (int)((float)this.animFiles.Length * 0.5f);
			while (num3 > 0 && match == null && num < num3)
			{
				if (this.animFiles[num] != null)
				{
					kanimFileData = this.animFiles[num].GetData();
				}
				if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
				{
					match = this.animFiles[num];
					break;
				}
				if (this.animFiles[num2] != null)
				{
					kanimFileData = this.animFiles[num2].GetData();
				}
				if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
				{
					match = this.animFiles[num2];
				}
				num++;
				num2--;
			}
			if (match == null && this.animFiles.Length % 2 != 0)
			{
				if (this.animFiles[num] != null)
				{
					kanimFileData = this.animFiles[num].GetData();
				}
				if (kanimFileData != null && kanimFileData.hashName.HashValue == anim_file_name.HashValue)
				{
					match = this.animFiles[num];
				}
			}
		}
		return match != null;
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x00071C14 File Offset: 0x0006FE14
	public void AddAnims(KAnimFile anim_file)
	{
		KAnimFileData data = anim_file.GetData();
		if (data == null)
		{
			global::Debug.LogError("AddAnims() Null animfile data");
			return;
		}
		this.maxSymbols = Mathf.Max(this.maxSymbols, data.maxVisSymbolFrames);
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim = data.GetAnim(i);
			if (anim.animFile.hashName != data.hashName)
			{
				global::Debug.LogErrorFormat("How did we get an anim from another file? [{0}] != [{1}] for anim [{2}]", new object[]
				{
					data.name,
					anim.animFile.name,
					i
				});
			}
			this.anims[anim.hash] = new KAnimControllerBase.AnimLookupData
			{
				animIndex = anim.index
			};
		}
		if (this.usingNewSymbolOverrideSystem && data.buildIndex != -1 && data.build.symbols != null && data.build.symbols.Length != 0)
		{
			base.GetComponent<SymbolOverrideController>().AddBuildOverride(anim_file.GetData(), -1);
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06001615 RID: 5653 RVA: 0x00071D16 File Offset: 0x0006FF16
	// (set) Token: 0x06001616 RID: 5654 RVA: 0x00071D20 File Offset: 0x0006FF20
	public KAnimFile[] AnimFiles
	{
		get
		{
			return this.animFiles;
		}
		set
		{
			DebugUtil.AssertArgs(value.Length != 0, new object[] { "Controller has no anim files.", base.gameObject });
			DebugUtil.AssertArgs(value[0] != null, new object[] { "First anim file needs to be non-null.", base.gameObject });
			DebugUtil.AssertArgs(value[0].IsBuildLoaded, new object[] { "First anim file needs to be the build file.", base.gameObject });
			for (int i = 0; i < value.Length; i++)
			{
				DebugUtil.AssertArgs(value[i] != null, new object[] { "Anim file is null", base.gameObject });
			}
			this.animFiles = new KAnimFile[value.Length];
			for (int j = 0; j < value.Length; j++)
			{
				this.animFiles[j] = value[j];
			}
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06001617 RID: 5655 RVA: 0x00071DF1 File Offset: 0x0006FFF1
	public IReadOnlyList<KAnimControllerBase.OverrideAnimFileData> OverrideAnimFiles
	{
		get
		{
			return this.overrideAnimFiles;
		}
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x00071DFC File Offset: 0x0006FFFC
	public void Stop()
	{
		if (this.curAnim != null)
		{
			this.StopAnimEventSequence();
		}
		this.animQueue.Clear();
		this.stopped = true;
		if (this.onAnimComplete != null)
		{
			this.onAnimComplete((this.curAnim == null) ? HashedString.Invalid : this.curAnim.hash);
		}
		this.OnStop();
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x00071E5C File Offset: 0x0007005C
	public void StopAndClear()
	{
		if (!this.stopped)
		{
			this.Stop();
		}
		this.bounds.center = Vector3.zero;
		this.bounds.extents = Vector3.zero;
		if (this.OnUpdateBounds != null)
		{
			this.OnUpdateBounds(this.bounds);
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x00071EB0 File Offset: 0x000700B0
	public float GetPositionPercent()
	{
		return this.GetElapsedTime() / this.GetDuration();
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x00071EC0 File Offset: 0x000700C0
	public void SetPositionPercent(float percent)
	{
		if (this.curAnim == null)
		{
			return;
		}
		this.SetElapsedTime((float)this.curAnim.numFrames / this.curAnim.frameRate * percent);
		int frameIdx = this.curAnim.GetFrameIdx(this.mode, this.elapsedTime);
		if (this.currentFrame != frameIdx)
		{
			this.SetDirty();
			this.UpdateAnimEventSequenceTime();
			this.SuspendUpdates(false);
		}
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x00071F2C File Offset: 0x0007012C
	protected void StartAnimEventSequence()
	{
		if (!this.layering.GetIsForeground() && this.aem != null)
		{
			this.eventManagerHandle = this.aem.PlayAnim(this, this.curAnim, this.mode, this.elapsedTime, this.visibilityType == KAnimControllerBase.VisibilityType.Always);
		}
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00071F7B File Offset: 0x0007017B
	protected void UpdateAnimEventSequenceTime()
	{
		if (this.eventManagerHandle.IsValid() && this.aem != null)
		{
			this.aem.SetElapsedTime(this.eventManagerHandle, this.elapsedTime);
		}
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x00071FAC File Offset: 0x000701AC
	protected void StopAnimEventSequence()
	{
		if (this.eventManagerHandle.IsValid() && this.aem != null)
		{
			if (!this.stopped && this.mode != KAnim.PlayMode.Paused)
			{
				this.SetElapsedTime(this.aem.GetElapsedTime(this.eventManagerHandle));
			}
			this.aem.StopAnim(this.eventManagerHandle);
			this.eventManagerHandle = HandleVector<int>.InvalidHandle;
		}
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x00072012 File Offset: 0x00070212
	protected void DestroySelf()
	{
		if (this.onDestroySelf != null)
		{
			this.onDestroySelf(base.gameObject);
			return;
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x04000C12 RID: 3090
	[NonSerialized]
	public GameObject showWhenMissing;

	// Token: 0x04000C13 RID: 3091
	[SerializeField]
	public KAnimBatchGroup.MaterialType materialType;

	// Token: 0x04000C14 RID: 3092
	[SerializeField]
	public string initialAnim;

	// Token: 0x04000C15 RID: 3093
	[SerializeField]
	public KAnim.PlayMode initialMode = KAnim.PlayMode.Once;

	// Token: 0x04000C16 RID: 3094
	[SerializeField]
	protected KAnimFile[] animFiles = new KAnimFile[0];

	// Token: 0x04000C17 RID: 3095
	[SerializeField]
	protected Vector3 offset;

	// Token: 0x04000C18 RID: 3096
	[SerializeField]
	protected Vector3 pivot;

	// Token: 0x04000C19 RID: 3097
	[SerializeField]
	protected float rotation;

	// Token: 0x04000C1A RID: 3098
	[SerializeField]
	public bool destroyOnAnimComplete;

	// Token: 0x04000C1B RID: 3099
	[SerializeField]
	public bool inactiveDisable;

	// Token: 0x04000C1C RID: 3100
	[SerializeField]
	protected bool flipX;

	// Token: 0x04000C1D RID: 3101
	[SerializeField]
	protected bool flipY;

	// Token: 0x04000C1E RID: 3102
	[SerializeField]
	public bool forceUseGameTime;

	// Token: 0x04000C1F RID: 3103
	public string defaultAnim;

	// Token: 0x04000C21 RID: 3105
	protected KAnim.Anim curAnim;

	// Token: 0x04000C22 RID: 3106
	protected int curAnimFrameIdx = KAnim.Anim.Frame.InvalidFrame.idx;

	// Token: 0x04000C23 RID: 3107
	protected int prevAnimFrame = KAnim.Anim.Frame.InvalidFrame.idx;

	// Token: 0x04000C24 RID: 3108
	public bool usingNewSymbolOverrideSystem;

	// Token: 0x04000C26 RID: 3110
	protected HandleVector<int>.Handle eventManagerHandle = HandleVector<int>.InvalidHandle;

	// Token: 0x04000C27 RID: 3111
	protected List<KAnimControllerBase.OverrideAnimFileData> overrideAnimFiles = new List<KAnimControllerBase.OverrideAnimFileData>();

	// Token: 0x04000C28 RID: 3112
	protected DeepProfiler DeepProfiler = new DeepProfiler(false);

	// Token: 0x04000C29 RID: 3113
	public bool randomiseLoopedOffset;

	// Token: 0x04000C2A RID: 3114
	protected float elapsedTime;

	// Token: 0x04000C2B RID: 3115
	protected float playSpeed = 1f;

	// Token: 0x04000C2C RID: 3116
	protected KAnim.PlayMode mode = KAnim.PlayMode.Once;

	// Token: 0x04000C2D RID: 3117
	protected bool stopped = true;

	// Token: 0x04000C2E RID: 3118
	public float animHeight = 1f;

	// Token: 0x04000C2F RID: 3119
	public float animWidth = 1f;

	// Token: 0x04000C30 RID: 3120
	protected bool isVisible;

	// Token: 0x04000C31 RID: 3121
	protected Bounds bounds;

	// Token: 0x04000C32 RID: 3122
	public Action<Bounds> OnUpdateBounds;

	// Token: 0x04000C33 RID: 3123
	public Action<Color> OnTintChanged;

	// Token: 0x04000C34 RID: 3124
	public Action<Color> OnHighlightChanged;

	// Token: 0x04000C36 RID: 3126
	protected KAnimSynchronizer synchronizer;

	// Token: 0x04000C37 RID: 3127
	protected KAnimLayering layering;

	// Token: 0x04000C38 RID: 3128
	[SerializeField]
	protected bool _enabled = true;

	// Token: 0x04000C39 RID: 3129
	protected bool hasEnableRun;

	// Token: 0x04000C3A RID: 3130
	protected bool hasAwakeRun;

	// Token: 0x04000C3B RID: 3131
	protected KBatchedAnimInstanceData batchInstanceData;

	// Token: 0x04000C3E RID: 3134
	public KAnimControllerBase.VisibilityType visibilityType;

	// Token: 0x04000C42 RID: 3138
	public Action<GameObject> onDestroySelf;

	// Token: 0x04000C45 RID: 3141
	[SerializeField]
	protected List<KAnimHashedString> hiddenSymbols = new List<KAnimHashedString>();

	// Token: 0x04000C46 RID: 3142
	protected Dictionary<HashedString, KAnimControllerBase.AnimLookupData> anims = new Dictionary<HashedString, KAnimControllerBase.AnimLookupData>();

	// Token: 0x04000C47 RID: 3143
	protected Dictionary<HashedString, KAnimControllerBase.AnimLookupData> overrideAnims = new Dictionary<HashedString, KAnimControllerBase.AnimLookupData>();

	// Token: 0x04000C48 RID: 3144
	protected Queue<KAnimControllerBase.AnimData> animQueue = new Queue<KAnimControllerBase.AnimData>();

	// Token: 0x04000C49 RID: 3145
	protected int maxSymbols;

	// Token: 0x04000C4B RID: 3147
	public Grid.SceneLayer fgLayer = Grid.SceneLayer.NoLayer;

	// Token: 0x04000C4C RID: 3148
	protected AnimEventManager aem;

	// Token: 0x04000C4D RID: 3149
	private static HashedString snaptoPivot = new HashedString("snapTo_pivot");

	// Token: 0x0200103C RID: 4156
	public struct OverrideAnimFileData
	{
		// Token: 0x040056C1 RID: 22209
		public float priority;

		// Token: 0x040056C2 RID: 22210
		public KAnimFile file;
	}

	// Token: 0x0200103D RID: 4157
	public struct AnimLookupData
	{
		// Token: 0x040056C3 RID: 22211
		public int animIndex;
	}

	// Token: 0x0200103E RID: 4158
	public struct AnimData
	{
		// Token: 0x040056C4 RID: 22212
		public HashedString anim;

		// Token: 0x040056C5 RID: 22213
		public KAnim.PlayMode mode;

		// Token: 0x040056C6 RID: 22214
		public float speed;

		// Token: 0x040056C7 RID: 22215
		public float timeOffset;
	}

	// Token: 0x0200103F RID: 4159
	public enum VisibilityType
	{
		// Token: 0x040056C9 RID: 22217
		Default,
		// Token: 0x040056CA RID: 22218
		OffscreenUpdate,
		// Token: 0x040056CB RID: 22219
		Always
	}

	// Token: 0x02001040 RID: 4160
	// (Invoke) Token: 0x06007299 RID: 29337
	public delegate void KAnimEvent(HashedString name);
}
