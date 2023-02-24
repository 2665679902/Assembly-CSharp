using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000972 RID: 2418
public class ClusterMapVisualizer : KMonoBehaviour
{
	// Token: 0x060047D0 RID: 18384 RVA: 0x0019409C File Offset: 0x0019229C
	public void Init(ClusterGridEntity entity, ClusterMapPathDrawer pathDrawer)
	{
		this.entity = entity;
		this.pathDrawer = pathDrawer;
		this.animControllers = new List<KBatchedAnimController>();
		if (this.animContainer == null)
		{
			GameObject gameObject = new GameObject("AnimContainer", new Type[] { typeof(RectTransform) });
			RectTransform component = base.GetComponent<RectTransform>();
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.SetParent(component, false);
			component2.SetLocalPosition(new Vector3(0f, 0f, 0f));
			component2.sizeDelta = component.sizeDelta;
			component2.localScale = Vector3.one;
			this.animContainer = component2;
		}
		Vector3 position = ClusterGrid.Instance.GetPosition(entity);
		this.rectTransform().SetLocalPosition(position);
		this.RefreshPathDrawing();
		entity.Subscribe(543433792, new Action<object>(this.OnClusterDestinationChanged));
	}

	// Token: 0x060047D1 RID: 18385 RVA: 0x00194172 File Offset: 0x00192372
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.doesTransitionAnimation)
		{
			new ClusterMapTravelAnimator.StatesInstance(this, this.entity).StartSM();
		}
	}

	// Token: 0x060047D2 RID: 18386 RVA: 0x00194194 File Offset: 0x00192394
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.entity != null)
		{
			if (this.entity is Clustercraft)
			{
				new ClusterMapRocketAnimator.StatesInstance(this, this.entity).StartSM();
				return;
			}
			if (this.entity is BallisticClusterGridEntity)
			{
				new ClusterMapBallisticAnimator.StatesInstance(this, this.entity).StartSM();
				return;
			}
			if (this.entity.Layer == EntityLayer.FX)
			{
				new ClusterMapFXAnimator.StatesInstance(this, this.entity).StartSM();
			}
		}
	}

	// Token: 0x060047D3 RID: 18387 RVA: 0x00194212 File Offset: 0x00192412
	protected override void OnCleanUp()
	{
		if (this.entity != null)
		{
			this.entity.Unsubscribe(543433792, new Action<object>(this.OnClusterDestinationChanged));
		}
		base.OnCleanUp();
	}

	// Token: 0x060047D4 RID: 18388 RVA: 0x00194244 File Offset: 0x00192444
	private void OnClusterDestinationChanged(object data)
	{
		this.RefreshPathDrawing();
	}

	// Token: 0x060047D5 RID: 18389 RVA: 0x0019424C File Offset: 0x0019244C
	public void Select(bool selected)
	{
		if (this.animControllers == null || this.animControllers.Count == 0)
		{
			return;
		}
		if (!selected == this.isSelected)
		{
			this.isSelected = selected;
			this.RefreshPathDrawing();
		}
		this.GetFirstAnimController().SetSymbolVisiblity("selected", selected);
	}

	// Token: 0x060047D6 RID: 18390 RVA: 0x0019429E File Offset: 0x0019249E
	public void PlayAnim(string animName, KAnim.PlayMode playMode)
	{
		this.GetFirstAnimController().Play(animName, playMode, 1f, 0f);
	}

	// Token: 0x060047D7 RID: 18391 RVA: 0x001942BC File Offset: 0x001924BC
	public KBatchedAnimController GetFirstAnimController()
	{
		return this.animControllers[0];
	}

	// Token: 0x060047D8 RID: 18392 RVA: 0x001942CC File Offset: 0x001924CC
	public void Show(ClusterRevealLevel level)
	{
		if (!this.entity.IsVisible)
		{
			level = ClusterRevealLevel.Hidden;
		}
		if (level == this.lastRevealLevel)
		{
			return;
		}
		this.lastRevealLevel = level;
		switch (level)
		{
		case ClusterRevealLevel.Hidden:
			base.gameObject.SetActive(false);
			return;
		case ClusterRevealLevel.Peeked:
		{
			this.ClearAnimControllers();
			KBatchedAnimController kbatchedAnimController = UnityEngine.Object.Instantiate<KBatchedAnimController>(this.peekControllerPrefab, this.animContainer);
			kbatchedAnimController.gameObject.SetActive(true);
			this.animControllers.Add(kbatchedAnimController);
			base.gameObject.SetActive(true);
			return;
		}
		case ClusterRevealLevel.Visible:
			this.ClearAnimControllers();
			if (this.animControllerPrefab != null && this.entity.AnimConfigs != null)
			{
				foreach (ClusterGridEntity.AnimConfig animConfig in this.entity.AnimConfigs)
				{
					KBatchedAnimController kbatchedAnimController2 = UnityEngine.Object.Instantiate<KBatchedAnimController>(this.animControllerPrefab, this.animContainer);
					kbatchedAnimController2.AnimFiles = new KAnimFile[] { animConfig.animFile };
					kbatchedAnimController2.initialMode = animConfig.playMode;
					kbatchedAnimController2.initialAnim = animConfig.initialAnim;
					kbatchedAnimController2.Offset = animConfig.animOffset;
					kbatchedAnimController2.gameObject.AddComponent<LoopingSounds>();
					if (!string.IsNullOrEmpty(animConfig.symbolSwapTarget) && !string.IsNullOrEmpty(animConfig.symbolSwapSymbol))
					{
						SymbolOverrideController component = kbatchedAnimController2.GetComponent<SymbolOverrideController>();
						KAnim.Build.Symbol symbol = kbatchedAnimController2.AnimFiles[0].GetData().build.GetSymbol(animConfig.symbolSwapSymbol);
						component.AddSymbolOverride(animConfig.symbolSwapTarget, symbol, 0);
					}
					kbatchedAnimController2.gameObject.SetActive(true);
					this.animControllers.Add(kbatchedAnimController2);
				}
			}
			base.gameObject.SetActive(true);
			return;
		default:
			return;
		}
	}

	// Token: 0x060047D9 RID: 18393 RVA: 0x0019449C File Offset: 0x0019269C
	public void RefreshPathDrawing()
	{
		if (this.entity == null)
		{
			return;
		}
		ClusterTraveler component = this.entity.GetComponent<ClusterTraveler>();
		if (component == null)
		{
			return;
		}
		List<AxialI> list = ((this.entity.IsVisible && component.IsTraveling()) ? component.CurrentPath : null);
		if (list != null && list.Count > 0)
		{
			if (this.mapPath == null)
			{
				this.mapPath = this.pathDrawer.AddPath();
			}
			this.mapPath.SetPoints(ClusterMapPathDrawer.GetDrawPathList(base.transform.GetLocalPosition(), list));
			Color color;
			if (this.isSelected)
			{
				color = ClusterMapScreen.Instance.rocketSelectedPathColor;
			}
			else if (this.entity.ShowPath())
			{
				color = ClusterMapScreen.Instance.rocketPathColor;
			}
			else
			{
				color = new Color(0f, 0f, 0f, 0f);
			}
			this.mapPath.SetColor(color);
			return;
		}
		if (this.mapPath != null)
		{
			global::Util.KDestroyGameObject(this.mapPath);
			this.mapPath = null;
		}
	}

	// Token: 0x060047DA RID: 18394 RVA: 0x001945B6 File Offset: 0x001927B6
	public void SetAnimRotation(float rotation)
	{
		this.animContainer.localRotation = Quaternion.Euler(0f, 0f, rotation);
	}

	// Token: 0x060047DB RID: 18395 RVA: 0x001945D3 File Offset: 0x001927D3
	public float GetPathAngle()
	{
		if (this.mapPath == null)
		{
			return 0f;
		}
		return this.mapPath.GetRotationForNextSegment();
	}

	// Token: 0x060047DC RID: 18396 RVA: 0x001945F4 File Offset: 0x001927F4
	private void ClearAnimControllers()
	{
		if (this.animControllers == null)
		{
			return;
		}
		foreach (KBatchedAnimController kbatchedAnimController in this.animControllers)
		{
			global::Util.KDestroyGameObject(kbatchedAnimController.gameObject);
		}
		this.animControllers.Clear();
	}

	// Token: 0x04002F67 RID: 12135
	public KBatchedAnimController animControllerPrefab;

	// Token: 0x04002F68 RID: 12136
	public KBatchedAnimController peekControllerPrefab;

	// Token: 0x04002F69 RID: 12137
	public Transform nameTarget;

	// Token: 0x04002F6A RID: 12138
	public AlertVignette alertVignette;

	// Token: 0x04002F6B RID: 12139
	public bool doesTransitionAnimation;

	// Token: 0x04002F6C RID: 12140
	[HideInInspector]
	public Transform animContainer;

	// Token: 0x04002F6D RID: 12141
	private ClusterGridEntity entity;

	// Token: 0x04002F6E RID: 12142
	private ClusterMapPathDrawer pathDrawer;

	// Token: 0x04002F6F RID: 12143
	private ClusterMapPath mapPath;

	// Token: 0x04002F70 RID: 12144
	private List<KBatchedAnimController> animControllers;

	// Token: 0x04002F71 RID: 12145
	private bool isSelected;

	// Token: 0x04002F72 RID: 12146
	private ClusterRevealLevel lastRevealLevel;

	// Token: 0x02001783 RID: 6019
	private class UpdateXPositionParameter : LoopingSoundParameterUpdater
	{
		// Token: 0x06008B27 RID: 35623 RVA: 0x002FEB6A File Offset: 0x002FCD6A
		public UpdateXPositionParameter()
			: base("Starmap_Position_X")
		{
		}

		// Token: 0x06008B28 RID: 35624 RVA: 0x002FEB88 File Offset: 0x002FCD88
		public override void Add(LoopingSoundParameterUpdater.Sound sound)
		{
			ClusterMapVisualizer.UpdateXPositionParameter.Entry entry = new ClusterMapVisualizer.UpdateXPositionParameter.Entry
			{
				transform = sound.transform,
				ev = sound.ev,
				parameterId = sound.description.GetParameterId(base.parameter)
			};
			this.entries.Add(entry);
		}

		// Token: 0x06008B29 RID: 35625 RVA: 0x002FEBE0 File Offset: 0x002FCDE0
		public override void Update(float dt)
		{
			foreach (ClusterMapVisualizer.UpdateXPositionParameter.Entry entry in this.entries)
			{
				if (!(entry.transform == null))
				{
					EventInstance ev = entry.ev;
					ev.setParameterByID(entry.parameterId, entry.transform.GetPosition().x / (float)Screen.width, false);
				}
			}
		}

		// Token: 0x06008B2A RID: 35626 RVA: 0x002FEC68 File Offset: 0x002FCE68
		public override void Remove(LoopingSoundParameterUpdater.Sound sound)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ev.handle == sound.ev.handle)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x04006D47 RID: 27975
		private List<ClusterMapVisualizer.UpdateXPositionParameter.Entry> entries = new List<ClusterMapVisualizer.UpdateXPositionParameter.Entry>();

		// Token: 0x020020CC RID: 8396
		private struct Entry
		{
			// Token: 0x0400920E RID: 37390
			public Transform transform;

			// Token: 0x0400920F RID: 37391
			public EventInstance ev;

			// Token: 0x04009210 RID: 37392
			public PARAMETER_ID parameterId;
		}
	}

	// Token: 0x02001784 RID: 6020
	private class UpdateYPositionParameter : LoopingSoundParameterUpdater
	{
		// Token: 0x06008B2B RID: 35627 RVA: 0x002FECC0 File Offset: 0x002FCEC0
		public UpdateYPositionParameter()
			: base("Starmap_Position_Y")
		{
		}

		// Token: 0x06008B2C RID: 35628 RVA: 0x002FECE0 File Offset: 0x002FCEE0
		public override void Add(LoopingSoundParameterUpdater.Sound sound)
		{
			ClusterMapVisualizer.UpdateYPositionParameter.Entry entry = new ClusterMapVisualizer.UpdateYPositionParameter.Entry
			{
				transform = sound.transform,
				ev = sound.ev,
				parameterId = sound.description.GetParameterId(base.parameter)
			};
			this.entries.Add(entry);
		}

		// Token: 0x06008B2D RID: 35629 RVA: 0x002FED38 File Offset: 0x002FCF38
		public override void Update(float dt)
		{
			foreach (ClusterMapVisualizer.UpdateYPositionParameter.Entry entry in this.entries)
			{
				if (!(entry.transform == null))
				{
					EventInstance ev = entry.ev;
					ev.setParameterByID(entry.parameterId, entry.transform.GetPosition().y / (float)Screen.height, false);
				}
			}
		}

		// Token: 0x06008B2E RID: 35630 RVA: 0x002FEDC0 File Offset: 0x002FCFC0
		public override void Remove(LoopingSoundParameterUpdater.Sound sound)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ev.handle == sound.ev.handle)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x04006D48 RID: 27976
		private List<ClusterMapVisualizer.UpdateYPositionParameter.Entry> entries = new List<ClusterMapVisualizer.UpdateYPositionParameter.Entry>();

		// Token: 0x020020CD RID: 8397
		private struct Entry
		{
			// Token: 0x04009211 RID: 37393
			public Transform transform;

			// Token: 0x04009212 RID: 37394
			public EventInstance ev;

			// Token: 0x04009213 RID: 37395
			public PARAMETER_ID parameterId;
		}
	}

	// Token: 0x02001785 RID: 6021
	private class UpdateZoomPercentageParameter : LoopingSoundParameterUpdater
	{
		// Token: 0x06008B2F RID: 35631 RVA: 0x002FEE18 File Offset: 0x002FD018
		public UpdateZoomPercentageParameter()
			: base("Starmap_Zoom_Percentage")
		{
		}

		// Token: 0x06008B30 RID: 35632 RVA: 0x002FEE38 File Offset: 0x002FD038
		public override void Add(LoopingSoundParameterUpdater.Sound sound)
		{
			ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry entry = new ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry
			{
				ev = sound.ev,
				parameterId = sound.description.GetParameterId(base.parameter)
			};
			this.entries.Add(entry);
		}

		// Token: 0x06008B31 RID: 35633 RVA: 0x002FEE84 File Offset: 0x002FD084
		public override void Update(float dt)
		{
			foreach (ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry entry in this.entries)
			{
				EventInstance ev = entry.ev;
				ev.setParameterByID(entry.parameterId, ClusterMapScreen.Instance.CurrentZoomPercentage(), false);
			}
		}

		// Token: 0x06008B32 RID: 35634 RVA: 0x002FEEF0 File Offset: 0x002FD0F0
		public override void Remove(LoopingSoundParameterUpdater.Sound sound)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ev.handle == sound.ev.handle)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x04006D49 RID: 27977
		private List<ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry> entries = new List<ClusterMapVisualizer.UpdateZoomPercentageParameter.Entry>();

		// Token: 0x020020CE RID: 8398
		private struct Entry
		{
			// Token: 0x04009214 RID: 37396
			public Transform transform;

			// Token: 0x04009215 RID: 37397
			public EventInstance ev;

			// Token: 0x04009216 RID: 37398
			public PARAMETER_ID parameterId;
		}
	}
}
