using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200041A RID: 1050
[DebuggerDisplay("{name} visible={isVisible} suspendUpdates={suspendUpdates} moving={moving}")]
public class KBatchedAnimController : KAnimControllerBase, KAnimConverter.IAnimConverter
{
	// Token: 0x06001649 RID: 5705 RVA: 0x00072E7E File Offset: 0x0007107E
	public int GetCurrentFrameIndex()
	{
		return this.curAnimFrameIdx;
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x00072E86 File Offset: 0x00071086
	public KBatchedAnimInstanceData GetBatchInstanceData()
	{
		return this.batchInstanceData;
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600164B RID: 5707 RVA: 0x00072E8E File Offset: 0x0007108E
	// (set) Token: 0x0600164C RID: 5708 RVA: 0x00072E96 File Offset: 0x00071096
	protected bool forceRebuild
	{
		get
		{
			return this._forceRebuild;
		}
		set
		{
			this._forceRebuild = value;
		}
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x00072EA0 File Offset: 0x000710A0
	public KBatchedAnimController()
	{
		this.batchInstanceData = new KBatchedAnimInstanceData(this);
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x00072F1E File Offset: 0x0007111E
	public bool IsActive()
	{
		return base.isActiveAndEnabled && this._enabled;
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x00072F30 File Offset: 0x00071130
	public bool IsVisible()
	{
		return this.isVisible;
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x00072F38 File Offset: 0x00071138
	public void SetSymbolScale(KAnimHashedString symbol_name, float scale)
	{
		KAnim.Build.Symbol symbol = KAnimBatchManager.Instance().GetBatchGroupData(this.GetBatchGroupID(false)).GetSymbol(symbol_name);
		if (symbol == null)
		{
			return;
		}
		base.symbolInstanceGpuData.SetSymbolScale(symbol.symbolIndexInSourceBuild, scale);
		this.SuspendUpdates(false);
		this.SetDirty();
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x00072F80 File Offset: 0x00071180
	public void SetSymbolTint(KAnimHashedString symbol_name, Color color)
	{
		KAnim.Build.Symbol symbol = KAnimBatchManager.Instance().GetBatchGroupData(this.GetBatchGroupID(false)).GetSymbol(symbol_name);
		if (symbol == null)
		{
			return;
		}
		base.symbolInstanceGpuData.SetSymbolTint(symbol.symbolIndexInSourceBuild, color);
		this.SuspendUpdates(false);
		this.SetDirty();
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x00072FC8 File Offset: 0x000711C8
	public Vector2I GetCellXY()
	{
		Vector3 positionIncludingOffset = base.PositionIncludingOffset;
		if (Grid.CellSizeInMeters == 0f)
		{
			return new Vector2I((int)positionIncludingOffset.x, (int)positionIncludingOffset.y);
		}
		return Grid.PosToXY(positionIncludingOffset);
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x00073002 File Offset: 0x00071202
	public float GetZ()
	{
		return base.transform.GetPosition().z;
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x00073014 File Offset: 0x00071214
	public string GetName()
	{
		return base.name;
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0007301C File Offset: 0x0007121C
	public override KAnim.Anim GetAnim(int index)
	{
		if (!this.batchGroupID.IsValid || !(this.batchGroupID != KAnimBatchManager.NO_BATCH))
		{
			global::Debug.LogError(base.name + " batch not ready");
		}
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.batchGroupID);
		global::Debug.Assert(batchGroupData != null);
		return batchGroupData.GetAnim(index);
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x00073080 File Offset: 0x00071280
	private void Initialize()
	{
		if (this.batchGroupID.IsValid && this.batchGroupID != KAnimBatchManager.NO_BATCH)
		{
			this.DeRegister();
			this.Register();
		}
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000730BB File Offset: 0x000712BB
	private void OnMovementStateChanged(bool is_moving)
	{
		if (is_moving == this.moving)
		{
			return;
		}
		this.moving = is_moving;
		this.SetDirty();
		this.ConfigureUpdateListener();
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000730DA File Offset: 0x000712DA
	private static void OnMovementStateChanged(Transform transform, bool is_moving)
	{
		transform.GetComponent<KBatchedAnimController>().OnMovementStateChanged(is_moving);
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000730E8 File Offset: 0x000712E8
	private void SetBatchGroup(KAnimFileData kafd)
	{
		if (this.batchGroupID.IsValid && kafd != null && this.batchGroupID == kafd.batchTag)
		{
			return;
		}
		DebugUtil.Assert(!this.batchGroupID.IsValid, "Should only be setting the batch group once.");
		DebugUtil.Assert(kafd != null, "Null anim data!! For", base.name);
		base.curBuild = kafd.build;
		DebugUtil.Assert(base.curBuild != null, "Null build for anim!! ", base.name, kafd.name);
		KAnimGroupFile.Group group = KAnimGroupFile.GetGroup(base.curBuild.batchTag);
		HashedString hashedString = kafd.build.batchTag;
		if (group.renderType == KAnimBatchGroup.RendererType.DontRender || group.renderType == KAnimBatchGroup.RendererType.AnimOnly)
		{
			bool isValid = group.swapTarget.IsValid;
			string text = "Invalid swap target fro group [";
			HashedString id = group.id;
			global::Debug.Assert(isValid, text + id.ToString() + "]");
			hashedString = group.swapTarget;
		}
		this.batchGroupID = hashedString;
		base.symbolInstanceGpuData = new SymbolInstanceGpuData(KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID).maxSymbolsPerBuild);
		base.symbolOverrideInfoGpuData = new SymbolOverrideInfoGpuData(KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID).symbolFrameInstances.Count);
		if (!this.batchGroupID.IsValid || this.batchGroupID == KAnimBatchManager.NO_BATCH)
		{
			global::Debug.LogError("Batch is not ready: " + base.name);
		}
		if (this.materialType == KAnimBatchGroup.MaterialType.Default && this.batchGroupID == KAnimBatchManager.BATCH_HUMAN)
		{
			this.materialType = KAnimBatchGroup.MaterialType.Human;
		}
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x00073284 File Offset: 0x00071484
	public void LoadAnims()
	{
		if (!KAnimBatchManager.Instance().isReady)
		{
			global::Debug.LogError("KAnimBatchManager is not ready when loading anim:" + base.name);
		}
		if (this.animFiles.Length == 0)
		{
			DebugUtil.Assert(false, "KBatchedAnimController has no anim files:" + base.name);
		}
		if (!this.animFiles[0].IsBuildLoaded)
		{
			DebugUtil.LogErrorArgs(base.gameObject, new object[] { string.Format("First anim file needs to be the build file but {0} doesn't have an associated build", this.animFiles[0].GetData().name) });
		}
		this.overrideAnims.Clear();
		this.anims.Clear();
		this.SetBatchGroup(this.animFiles[0].GetData());
		for (int i = 0; i < this.animFiles.Length; i++)
		{
			base.AddAnims(this.animFiles[i]);
		}
		this.forceRebuild = true;
		if (this.layering != null)
		{
			this.layering.HideSymbols();
		}
		if (this.usingNewSymbolOverrideSystem)
		{
			DebugUtil.Assert(base.GetComponent<SymbolOverrideController>() != null);
		}
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x00073390 File Offset: 0x00071590
	public void SwapAnims(KAnimFile[] anims)
	{
		if (this.batchGroupID.IsValid)
		{
			this.DeRegister();
			this.batchGroupID = HashedString.Invalid;
		}
		base.AnimFiles = anims;
		this.LoadAnims();
		this.Register();
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000733D4 File Offset: 0x000715D4
	public void UpdateAnim(float dt)
	{
		if (this.batch != null && base.transform.hasChanged)
		{
			base.transform.hasChanged = false;
			if (this.batch != null && this.batch.group.maxGroupSize == 1 && this.lastPos.z != base.transform.GetPosition().z)
			{
				this.batch.OverrideZ(base.transform.GetPosition().z);
			}
			Vector3 positionIncludingOffset = base.PositionIncludingOffset;
			this.lastPos = positionIncludingOffset;
			if (this.visibilityType != KAnimControllerBase.VisibilityType.Always && KAnimBatchManager.ControllerToChunkXY(this) != this.lastChunkXY && this.lastChunkXY != KBatchedAnimUpdater.INVALID_CHUNK_ID)
			{
				this.DeRegister();
				this.Register();
			}
			this.SetDirty();
		}
		if (this.batchGroupID == KAnimBatchManager.NO_BATCH || !this.IsActive())
		{
			return;
		}
		if (!this.forceRebuild && (this.mode == KAnim.PlayMode.Paused || this.stopped || this.curAnim == null || (this.mode == KAnim.PlayMode.Once && this.curAnim != null && (this.elapsedTime > this.curAnim.totalTime || this.curAnim.totalTime <= 0f) && this.animQueue.Count == 0)))
		{
			this.SuspendUpdates(true);
		}
		if (!this.isVisible && !this.forceRebuild)
		{
			if (this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate && !this.stopped && this.mode != KAnim.PlayMode.Paused)
			{
				base.SetElapsedTime(this.elapsedTime + dt * this.playSpeed);
			}
			return;
		}
		this.curAnimFrameIdx = base.GetFrameIdx(this.elapsedTime, true);
		if (this.eventManagerHandle.IsValid() && this.aem != null)
		{
			float elapsedTime = this.aem.GetElapsedTime(this.eventManagerHandle);
			if ((int)((this.elapsedTime - elapsedTime) * 100f) != 0)
			{
				base.UpdateAnimEventSequenceTime();
			}
		}
		this.UpdateFrame(this.elapsedTime);
		if (!this.stopped && this.mode != KAnim.PlayMode.Paused)
		{
			base.SetElapsedTime(this.elapsedTime + dt * this.playSpeed);
		}
		this.forceRebuild = false;
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000735FC File Offset: 0x000717FC
	protected override void UpdateFrame(float t)
	{
		base.previousFrame = base.currentFrame;
		if (!this.stopped || this.forceRebuild)
		{
			if (this.curAnim != null && (this.mode == KAnim.PlayMode.Loop || this.elapsedTime <= base.GetDuration() || this.forceRebuild))
			{
				base.currentFrame = this.curAnim.GetFrameIdx(this.mode, this.elapsedTime);
				if (base.currentFrame != base.previousFrame || this.forceRebuild)
				{
					this.SetDirty();
				}
			}
			else
			{
				this.TriggerStop();
			}
			if (!this.stopped && this.mode == KAnim.PlayMode.Loop && base.currentFrame == 0)
			{
				base.AnimEnter(this.curAnim.hash);
			}
		}
		if (this.synchronizer != null)
		{
			this.synchronizer.SyncTime();
		}
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000736CC File Offset: 0x000718CC
	public override void TriggerStop()
	{
		if (this.animQueue.Count > 0)
		{
			base.StartQueuedAnim();
			return;
		}
		if (this.curAnim != null && this.mode == KAnim.PlayMode.Once)
		{
			base.currentFrame = this.curAnim.numFrames - 1;
			base.Stop();
			base.gameObject.Trigger(-1061186183, null);
			if (this.destroyOnAnimComplete)
			{
				base.DestroySelf();
			}
		}
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x00073738 File Offset: 0x00071938
	public override void UpdateHidden()
	{
		KBatchGroupData batchGroupData = KAnimBatchManager.instance.GetBatchGroupData(this.batchGroupID);
		for (int i = 0; i < batchGroupData.frameElementSymbols.Count; i++)
		{
			KAnim.Build.Symbol symbol = batchGroupData.frameElementSymbols[i];
			bool flag = !this.hiddenSymbols.Contains(symbol.hash);
			base.symbolInstanceGpuData.SetVisible(i, flag);
		}
		this.SetDirty();
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000737A1 File Offset: 0x000719A1
	public int GetMaxVisible()
	{
		return this.maxSymbols;
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06001661 RID: 5729 RVA: 0x000737A9 File Offset: 0x000719A9
	// (set) Token: 0x06001662 RID: 5730 RVA: 0x000737B1 File Offset: 0x000719B1
	public HashedString batchGroupID { get; private set; }

	// Token: 0x06001663 RID: 5731 RVA: 0x000737BC File Offset: 0x000719BC
	public HashedString GetBatchGroupID(bool isEditorWindow = false)
	{
		global::Debug.Assert(isEditorWindow || this.animFiles == null || this.animFiles.Length == 0 || (this.batchGroupID.IsValid && this.batchGroupID != KAnimBatchManager.NO_BATCH));
		return this.batchGroupID;
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x00073811 File Offset: 0x00071A11
	public int GetLayer()
	{
		return base.gameObject.layer;
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0007381E File Offset: 0x00071A1E
	public KAnimBatch GetBatch()
	{
		return this.batch;
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x00073828 File Offset: 0x00071A28
	public void SetBatch(KAnimBatch new_batch)
	{
		this.batch = new_batch;
		if (this.materialType == KAnimBatchGroup.MaterialType.UI)
		{
			KBatchedAnimCanvasRenderer kbatchedAnimCanvasRenderer = base.GetComponent<KBatchedAnimCanvasRenderer>();
			if (kbatchedAnimCanvasRenderer == null && new_batch != null)
			{
				kbatchedAnimCanvasRenderer = base.gameObject.AddComponent<KBatchedAnimCanvasRenderer>();
			}
			if (kbatchedAnimCanvasRenderer != null)
			{
				kbatchedAnimCanvasRenderer.SetBatch(this);
			}
		}
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x00073874 File Offset: 0x00071A74
	public int GetCurrentNumFrames()
	{
		if (this.curAnim == null)
		{
			return 0;
		}
		return this.curAnim.numFrames;
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x0007388B File Offset: 0x00071A8B
	public int GetFirstFrameIndex()
	{
		if (this.curAnim == null)
		{
			return -1;
		}
		return this.curAnim.firstFrameIdx;
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000738A4 File Offset: 0x00071AA4
	private Canvas GetRootCanvas()
	{
		if (this.rt == null)
		{
			return null;
		}
		RectTransform rectTransform = this.rt.parent.GetComponent<RectTransform>();
		while (rectTransform != null)
		{
			Canvas component = rectTransform.GetComponent<Canvas>();
			if (component != null && component.isRootCanvas)
			{
				return component;
			}
			rectTransform = rectTransform.parent.GetComponent<RectTransform>();
		}
		return null;
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x00073908 File Offset: 0x00071B08
	public override Matrix2x3 GetTransformMatrix()
	{
		Vector3 vector = base.PositionIncludingOffset;
		vector.z = 0f;
		Vector2 vector2 = new Vector2(this.animScale * this.animWidth, -this.animScale * this.animHeight);
		if (this.materialType == KAnimBatchGroup.MaterialType.UI)
		{
			this.rt = base.GetComponent<RectTransform>();
			if (this.rootCanvas == null)
			{
				this.rootCanvas = this.GetRootCanvas();
			}
			if (this.scaler == null && this.rootCanvas != null)
			{
				this.scaler = this.rootCanvas.GetComponent<CanvasScaler>();
			}
			if (this.rootCanvas == null)
			{
				this.screenOffset.x = (float)(Screen.width / 2);
				this.screenOffset.y = (float)(Screen.height / 2);
			}
			else
			{
				this.screenOffset.x = ((this.rootCanvas.renderMode == RenderMode.WorldSpace) ? 0f : (this.rootCanvas.rectTransform().rect.width / 2f));
				this.screenOffset.y = ((this.rootCanvas.renderMode == RenderMode.WorldSpace) ? 0f : (this.rootCanvas.rectTransform().rect.height / 2f));
			}
			float num = 1f;
			if (this.scaler != null)
			{
				num = 1f / this.scaler.scaleFactor;
			}
			vector = (this.rt.localToWorldMatrix.MultiplyPoint(this.rt.pivot) + this.offset) * num - this.screenOffset;
			float num2 = this.animWidth * this.animScale;
			float num3 = this.animHeight * this.animScale;
			if (this.setScaleFromAnim && this.curAnim != null)
			{
				num2 *= this.rt.rect.size.x / this.curAnim.unScaledSize.x;
				num3 *= this.rt.rect.size.y / this.curAnim.unScaledSize.y;
			}
			else
			{
				num2 *= this.rt.rect.size.x / this.animOverrideSize.x;
				num3 *= this.rt.rect.size.y / this.animOverrideSize.y;
			}
			vector2 = new Vector3(this.rt.lossyScale.x * num2 * num, -this.rt.lossyScale.y * num3 * num, this.rt.lossyScale.z * num);
			this.pivot = this.rt.pivot;
		}
		Matrix2x3 matrix2x = Matrix2x3.Scale(vector2);
		Matrix2x3 matrix2x2 = Matrix2x3.Scale(new Vector2(this.flipX ? (-1f) : 1f, this.flipY ? (-1f) : 1f));
		Matrix2x3 matrix2x6;
		if (this.rotation != 0f)
		{
			Matrix2x3 matrix2x3 = Matrix2x3.Translate(-this.pivot);
			Matrix2x3 matrix2x4 = Matrix2x3.Rotate(this.rotation * 0.017453292f);
			Matrix2x3 matrix2x5 = Matrix2x3.Translate(this.pivot) * matrix2x4 * matrix2x3;
			matrix2x6 = Matrix2x3.TRS(vector, base.transform.rotation, base.transform.localScale) * matrix2x5 * matrix2x * this.navMatrix * matrix2x2;
		}
		else
		{
			matrix2x6 = Matrix2x3.TRS(vector, base.transform.rotation, base.transform.localScale) * matrix2x * this.navMatrix * matrix2x2;
		}
		return matrix2x6;
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x00073D28 File Offset: 0x00071F28
	public override Matrix4x4 GetSymbolTransform(HashedString symbol, out bool symbolVisible)
	{
		if (this.curAnimFrameIdx != -1 && this.batch != null)
		{
			Matrix2x3 symbolLocalTransform = this.GetSymbolLocalTransform(symbol, out symbolVisible);
			if (symbolVisible)
			{
				return this.GetTransformMatrix() * symbolLocalTransform;
			}
		}
		symbolVisible = false;
		return default(Matrix4x4);
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x00073D78 File Offset: 0x00071F78
	public override Matrix2x3 GetSymbolLocalTransform(HashedString symbol, out bool symbolVisible)
	{
		if (this.curAnimFrameIdx != -1 && this.batch != null)
		{
			KAnim.Anim.Frame frame = this.batch.group.data.GetFrame(this.curAnimFrameIdx);
			if (frame != KAnim.Anim.Frame.InvalidFrame)
			{
				for (int i = 0; i < frame.numElements; i++)
				{
					int num = frame.firstElementIdx + i;
					if (num < this.batch.group.data.frameElements.Count)
					{
						KAnim.Anim.FrameElement frameElement = this.batch.group.data.frameElements[num];
						if (frameElement.symbol == symbol)
						{
							symbolVisible = true;
							return frameElement.transform;
						}
					}
				}
			}
		}
		symbolVisible = false;
		return Matrix2x3.identity;
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x00073E38 File Offset: 0x00072038
	public override void SetLayer(int layer)
	{
		if (layer == base.gameObject.layer)
		{
			return;
		}
		base.SetLayer(layer);
		this.DeRegister();
		base.gameObject.layer = layer;
		this.Register();
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x00073E68 File Offset: 0x00072068
	public override void SetDirty()
	{
		if (this.batch != null)
		{
			this.batch.SetDirty(this);
		}
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x00073E7E File Offset: 0x0007207E
	protected override void OnStartQueuedAnim()
	{
		this.SuspendUpdates(false);
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x00073E88 File Offset: 0x00072088
	protected override void OnAwake()
	{
		this.LoadAnims();
		if (this.visibilityType == KAnimControllerBase.VisibilityType.Default)
		{
			this.visibilityType = ((this.materialType == KAnimBatchGroup.MaterialType.UI) ? KAnimControllerBase.VisibilityType.Always : this.visibilityType);
		}
		if (this.materialType == KAnimBatchGroup.MaterialType.Default && this.batchGroupID == KAnimBatchManager.BATCH_HUMAN)
		{
			this.materialType = KAnimBatchGroup.MaterialType.Human;
		}
		this.symbolOverrideController = base.GetComponent<SymbolOverrideController>();
		this.UpdateHidden();
		this.hasEnableRun = false;
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x00073EF8 File Offset: 0x000720F8
	protected override void OnStart()
	{
		if (this.batch == null)
		{
			this.Initialize();
		}
		if (this.visibilityType == KAnimControllerBase.VisibilityType.Always || this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate)
		{
			this.ConfigureUpdateListener();
		}
		CellChangeMonitor instance = Singleton<CellChangeMonitor>.Instance;
		if (instance != null)
		{
			instance.RegisterMovementStateChanged(base.transform, new Action<Transform, bool>(KBatchedAnimController.OnMovementStateChanged));
			this.moving = instance.IsMoving(base.transform);
		}
		this.symbolOverrideController = base.GetComponent<SymbolOverrideController>();
		this.SetDirty();
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x00073F70 File Offset: 0x00072170
	protected override void OnStop()
	{
		this.SetDirty();
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x00073F78 File Offset: 0x00072178
	private void OnEnable()
	{
		if (this._enabled)
		{
			this.Enable();
		}
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x00073F88 File Offset: 0x00072188
	protected override void Enable()
	{
		if (this.hasEnableRun)
		{
			return;
		}
		this.hasEnableRun = true;
		if (this.batch == null)
		{
			this.Initialize();
		}
		this.SetDirty();
		this.SuspendUpdates(false);
		this.ConfigureVisibilityListener(true);
		if (!this.stopped && this.curAnim != null && this.mode != KAnim.PlayMode.Paused && !this.eventManagerHandle.IsValid())
		{
			base.StartAnimEventSequence();
		}
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x00073FF3 File Offset: 0x000721F3
	private void OnDisable()
	{
		this.Disable();
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x00073FFC File Offset: 0x000721FC
	protected override void Disable()
	{
		if (App.IsExiting || KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		if (!this.hasEnableRun)
		{
			return;
		}
		this.hasEnableRun = false;
		this.SuspendUpdates(true);
		if (this.batch != null)
		{
			this.DeRegister();
		}
		this.ConfigureVisibilityListener(false);
		base.StopAnimEventSequence();
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0007404C File Offset: 0x0007224C
	protected override void OnDestroy()
	{
		if (App.IsExiting)
		{
			return;
		}
		CellChangeMonitor instance = Singleton<CellChangeMonitor>.Instance;
		if (instance != null)
		{
			instance.UnregisterMovementStateChanged(base.transform, new Action<Transform, bool>(KBatchedAnimController.OnMovementStateChanged));
		}
		KBatchedAnimUpdater instance2 = Singleton<KBatchedAnimUpdater>.Instance;
		if (instance2 != null)
		{
			instance2.UpdateUnregister(this);
		}
		this.isVisible = false;
		this.DeRegister();
		this.stopped = true;
		base.StopAnimEventSequence();
		this.batchInstanceData = null;
		this.batch = null;
		base.OnDestroy();
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000740C0 File Offset: 0x000722C0
	public void SetBlendValue(float value)
	{
		this.batchInstanceData.SetBlend(value);
		this.SetDirty();
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000740D4 File Offset: 0x000722D4
	public SymbolOverrideController SetupSymbolOverriding()
	{
		if (!this.symbolOverrideController.IsNullOrDestroyed())
		{
			return this.symbolOverrideController;
		}
		this.usingNewSymbolOverrideSystem = true;
		this.symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(base.gameObject);
		return this.symbolOverrideController;
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x00074108 File Offset: 0x00072308
	public bool ApplySymbolOverrides()
	{
		this.batch.atlases.Apply(this.batch.matProperties);
		if (this.symbolOverrideController != null)
		{
			if (this.symbolOverrideControllerVersion != this.symbolOverrideController.version || this.symbolOverrideController.applySymbolOverridesEveryFrame)
			{
				this.symbolOverrideControllerVersion = this.symbolOverrideController.version;
				this.symbolOverrideController.ApplyOverrides();
			}
			this.symbolOverrideController.ApplyAtlases();
			return true;
		}
		return false;
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x00074188 File Offset: 0x00072388
	public void SetSymbolOverrides(int symbol_start_idx, int symbol_num_frames, int atlas_idx, KBatchGroupData source_data, int source_start_idx, int source_num_frames)
	{
		base.symbolOverrideInfoGpuData.SetSymbolOverrideInfo(symbol_start_idx, symbol_num_frames, atlas_idx, source_data, source_start_idx, source_num_frames);
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x0007419E File Offset: 0x0007239E
	public void SetSymbolOverride(int symbol_idx, ref KAnim.Build.SymbolFrameInstance symbol_frame_instance)
	{
		base.symbolOverrideInfoGpuData.SetSymbolOverrideInfo(symbol_idx, ref symbol_frame_instance);
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x000741B0 File Offset: 0x000723B0
	protected override void Register()
	{
		if (!this.IsActive())
		{
			return;
		}
		if (this.batch != null)
		{
			return;
		}
		if (this.batchGroupID.IsValid && this.batchGroupID != KAnimBatchManager.NO_BATCH)
		{
			this.lastChunkXY = KAnimBatchManager.ControllerToChunkXY(this);
			KAnimBatchManager.Instance().Register(this);
			this.forceRebuild = true;
			this.SetDirty();
		}
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x00074215 File Offset: 0x00072415
	protected override void DeRegister()
	{
		if (this.batch != null)
		{
			this.batch.Deregister(this);
		}
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x0007422C File Offset: 0x0007242C
	private void ConfigureUpdateListener()
	{
		if ((this.IsActive() && !this.suspendUpdates && this.isVisible) || this.moving || this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate || this.visibilityType == KAnimControllerBase.VisibilityType.Always)
		{
			Singleton<KBatchedAnimUpdater>.Instance.UpdateRegister(this);
			return;
		}
		Singleton<KBatchedAnimUpdater>.Instance.UpdateUnregister(this);
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x00074287 File Offset: 0x00072487
	protected override void SuspendUpdates(bool suspend)
	{
		this.suspendUpdates = suspend;
		this.ConfigureUpdateListener();
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x00074296 File Offset: 0x00072496
	public void SetVisiblity(bool is_visible)
	{
		if (is_visible != this.isVisible)
		{
			this.isVisible = is_visible;
			if (is_visible)
			{
				this.SuspendUpdates(false);
				this.SetDirty();
				base.UpdateAnimEventSequenceTime();
				return;
			}
			this.SuspendUpdates(true);
			this.SetDirty();
		}
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x000742CC File Offset: 0x000724CC
	private void ConfigureVisibilityListener(bool enabled)
	{
		if (this.visibilityType == KAnimControllerBase.VisibilityType.Always || this.visibilityType == KAnimControllerBase.VisibilityType.OffscreenUpdate)
		{
			return;
		}
		if (enabled)
		{
			this.RegisterVisibilityListener();
			return;
		}
		this.UnregisterVisibilityListener();
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000742F1 File Offset: 0x000724F1
	protected override void RefreshVisibilityListener()
	{
		if (!this.visibilityListenerRegistered)
		{
			return;
		}
		this.ConfigureVisibilityListener(false);
		this.ConfigureVisibilityListener(true);
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x0007430A File Offset: 0x0007250A
	private void RegisterVisibilityListener()
	{
		DebugUtil.Assert(!this.visibilityListenerRegistered);
		Singleton<KBatchedAnimUpdater>.Instance.VisibilityRegister(this);
		this.visibilityListenerRegistered = true;
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x0007432C File Offset: 0x0007252C
	private void UnregisterVisibilityListener()
	{
		DebugUtil.Assert(this.visibilityListenerRegistered);
		Singleton<KBatchedAnimUpdater>.Instance.VisibilityUnregister(this);
		this.visibilityListenerRegistered = false;
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x0007434C File Offset: 0x0007254C
	public void SetSceneLayer(Grid.SceneLayer layer)
	{
		float layerZ = Grid.GetLayerZ(layer);
		this.sceneLayer = layer;
		Vector3 position = base.transform.GetPosition();
		position.z = layerZ;
		base.transform.SetPosition(position);
		this.DeRegister();
		this.Register();
	}

	// Token: 0x04000C63 RID: 3171
	[NonSerialized]
	protected bool _forceRebuild;

	// Token: 0x04000C64 RID: 3172
	private Vector3 lastPos = Vector3.zero;

	// Token: 0x04000C65 RID: 3173
	private Vector2I lastChunkXY = KBatchedAnimUpdater.INVALID_CHUNK_ID;

	// Token: 0x04000C66 RID: 3174
	private KAnimBatch batch;

	// Token: 0x04000C67 RID: 3175
	public float animScale = 0.005f;

	// Token: 0x04000C68 RID: 3176
	private bool suspendUpdates;

	// Token: 0x04000C69 RID: 3177
	private bool visibilityListenerRegistered;

	// Token: 0x04000C6A RID: 3178
	private bool moving;

	// Token: 0x04000C6B RID: 3179
	private SymbolOverrideController symbolOverrideController;

	// Token: 0x04000C6C RID: 3180
	private int symbolOverrideControllerVersion;

	// Token: 0x04000C6D RID: 3181
	[NonSerialized]
	public KBatchedAnimUpdater.RegistrationState updateRegistrationState = KBatchedAnimUpdater.RegistrationState.Unregistered;

	// Token: 0x04000C6E RID: 3182
	public Grid.SceneLayer sceneLayer;

	// Token: 0x04000C6F RID: 3183
	private RectTransform rt;

	// Token: 0x04000C70 RID: 3184
	private Vector3 screenOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x04000C71 RID: 3185
	public Matrix2x3 navMatrix = Matrix2x3.identity;

	// Token: 0x04000C72 RID: 3186
	private CanvasScaler scaler;

	// Token: 0x04000C73 RID: 3187
	public bool setScaleFromAnim = true;

	// Token: 0x04000C74 RID: 3188
	public Vector2 animOverrideSize = Vector2.one;

	// Token: 0x04000C75 RID: 3189
	private Canvas rootCanvas;

	// Token: 0x04000C76 RID: 3190
	public bool isMovable;
}
