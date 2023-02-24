using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000A56 RID: 2646
public class ClusterMapScreen : KScreen
{
	// Token: 0x06005050 RID: 20560 RVA: 0x001CBED6 File Offset: 0x001CA0D6
	public static void DestroyInstance()
	{
		ClusterMapScreen.Instance = null;
	}

	// Token: 0x06005051 RID: 20561 RVA: 0x001CBEDE File Offset: 0x001CA0DE
	public ClusterMapVisualizer GetEntityVisAnim(ClusterGridEntity entity)
	{
		if (this.m_gridEntityAnims.ContainsKey(entity))
		{
			return this.m_gridEntityAnims[entity];
		}
		return null;
	}

	// Token: 0x06005052 RID: 20562 RVA: 0x001CBEFC File Offset: 0x001CA0FC
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return 20f;
	}

	// Token: 0x06005053 RID: 20563 RVA: 0x001CBF11 File Offset: 0x001CA111
	public float CurrentZoomPercentage()
	{
		return (this.m_currentZoomScale - 50f) / 100f;
	}

	// Token: 0x06005054 RID: 20564 RVA: 0x001CBF25 File Offset: 0x001CA125
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.m_selectMarker = global::Util.KInstantiateUI<SelectMarker>(this.selectMarkerPrefab, base.gameObject, false);
		this.m_selectMarker.gameObject.SetActive(false);
		ClusterMapScreen.Instance = this;
	}

	// Token: 0x06005055 RID: 20565 RVA: 0x001CBF5C File Offset: 0x001CA15C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		global::Debug.Assert(this.cellVisPrefab.rectTransform().sizeDelta == new Vector2(2f, 2f), "The radius of the cellVisPrefab hex must be 1");
		global::Debug.Assert(this.terrainVisPrefab.rectTransform().sizeDelta == new Vector2(2f, 2f), "The radius of the terrainVisPrefab hex must be 1");
		global::Debug.Assert(this.mobileVisPrefab.rectTransform().sizeDelta == new Vector2(2f, 2f), "The radius of the mobileVisPrefab hex must be 1");
		global::Debug.Assert(this.staticVisPrefab.rectTransform().sizeDelta == new Vector2(2f, 2f), "The radius of the staticVisPrefab hex must be 1");
		int num;
		int num2;
		int num3;
		int num4;
		this.GenerateGridVis(out num, out num2, out num3, out num4);
		this.Show(false);
		this.mapScrollRect.content.sizeDelta = new Vector2((float)(num2 * 4), (float)(num4 * 4));
		this.mapScrollRect.content.localScale = new Vector3(this.m_currentZoomScale, this.m_currentZoomScale, 1f);
		this.m_onDestinationChangedDelegate = new Action<object>(this.OnDestinationChanged);
		this.m_onSelectObjectDelegate = new Action<object>(this.OnSelectObject);
		base.Subscribe(1980521255, new Action<object>(this.UpdateVis));
	}

	// Token: 0x06005056 RID: 20566 RVA: 0x001CC0BC File Offset: 0x001CA2BC
	protected void MoveToNISPosition()
	{
		if (!this.movingToTargetNISPosition)
		{
			return;
		}
		Vector3 vector = new Vector3(-this.targetNISPosition.x * this.mapScrollRect.content.localScale.x, -this.targetNISPosition.y * this.mapScrollRect.content.localScale.y, this.targetNISPosition.z);
		this.m_targetZoomScale = Mathf.Lerp(this.m_targetZoomScale, this.targetNISZoom, Time.unscaledDeltaTime * 2f);
		this.mapScrollRect.content.SetLocalPosition(Vector3.Lerp(this.mapScrollRect.content.GetLocalPosition(), vector, Time.unscaledDeltaTime * 2.5f));
		float num = Vector3.Distance(this.mapScrollRect.content.GetLocalPosition(), vector);
		if (num < 100f)
		{
			ClusterMapHex component = this.m_cellVisByLocation[this.selectOnMoveNISComplete].GetComponent<ClusterMapHex>();
			if (this.m_selectedHex != component)
			{
				this.SelectHex(component);
			}
			if (num < 10f)
			{
				this.movingToTargetNISPosition = false;
			}
		}
	}

	// Token: 0x06005057 RID: 20567 RVA: 0x001CC1D6 File Offset: 0x001CA3D6
	public void SetTargetFocusPosition(AxialI targetPosition, float delayBeforeMove = 0.5f)
	{
		if (this.activeMoveToTargetRoutine != null)
		{
			base.StopCoroutine(this.activeMoveToTargetRoutine);
		}
		this.activeMoveToTargetRoutine = base.StartCoroutine(this.MoveToTargetRoutine(targetPosition, delayBeforeMove));
	}

	// Token: 0x06005058 RID: 20568 RVA: 0x001CC200 File Offset: 0x001CA400
	private IEnumerator MoveToTargetRoutine(AxialI targetPosition, float delayBeforeMove)
	{
		delayBeforeMove = Mathf.Max(delayBeforeMove, 0f);
		yield return SequenceUtil.WaitForSecondsRealtime(delayBeforeMove);
		this.targetNISPosition = AxialUtil.AxialToWorld((float)targetPosition.r, (float)targetPosition.q);
		this.targetNISZoom = 150f;
		this.movingToTargetNISPosition = true;
		this.selectOnMoveNISComplete = targetPosition;
		yield break;
	}

	// Token: 0x06005059 RID: 20569 RVA: 0x001CC220 File Offset: 0x001CA420
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!e.Consumed && (e.IsAction(global::Action.ZoomIn) || e.IsAction(global::Action.ZoomOut)))
		{
			List<RaycastResult> list = new List<RaycastResult>();
			PointerEventData pointerEventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
			pointerEventData.position = KInputManager.GetMousePos();
			UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
			if (current != null)
			{
				current.RaycastAll(pointerEventData, list);
				bool flag = false;
				foreach (RaycastResult raycastResult in list)
				{
					if (!raycastResult.gameObject.transform.IsChildOf(base.transform))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					float num;
					if (KInputManager.currentControllerIsGamepad)
					{
						num = 25f;
						num *= (float)(e.IsAction(global::Action.ZoomIn) ? 1 : (-1));
					}
					else
					{
						num = Input.mouseScrollDelta.y * 25f;
					}
					this.m_targetZoomScale = Mathf.Clamp(this.m_targetZoomScale + num, 50f, 150f);
					e.TryConsume(global::Action.ZoomIn);
					if (!e.Consumed)
					{
						e.TryConsume(global::Action.ZoomOut);
					}
				}
			}
		}
		CameraController.Instance.ChangeWorldInput(e);
		base.OnKeyDown(e);
	}

	// Token: 0x0600505A RID: 20570 RVA: 0x001CC36C File Offset: 0x001CA56C
	public bool TryHandleCancel()
	{
		if (this.m_mode == ClusterMapScreen.Mode.SelectDestination && !this.m_closeOnSelect)
		{
			this.SetMode(ClusterMapScreen.Mode.Default);
			return true;
		}
		return false;
	}

	// Token: 0x0600505B RID: 20571 RVA: 0x001CC38C File Offset: 0x001CA58C
	public void ShowInSelectDestinationMode(ClusterDestinationSelector destination_selector)
	{
		this.m_destinationSelector = destination_selector;
		if (!base.gameObject.activeSelf)
		{
			ManagementMenu.Instance.ToggleClusterMap();
			this.m_closeOnSelect = true;
		}
		ClusterGridEntity component = destination_selector.GetComponent<ClusterGridEntity>();
		this.SetSelectedEntity(component, false);
		if (this.m_selectedEntity != null)
		{
			this.m_selectedHex = this.m_cellVisByLocation[this.m_selectedEntity.Location].GetComponent<ClusterMapHex>();
		}
		else
		{
			AxialI myWorldLocation = destination_selector.GetMyWorldLocation();
			ClusterMapHex component2 = this.m_cellVisByLocation[myWorldLocation].GetComponent<ClusterMapHex>();
			this.m_selectedHex = component2;
		}
		this.SetMode(ClusterMapScreen.Mode.SelectDestination);
	}

	// Token: 0x0600505C RID: 20572 RVA: 0x001CC425 File Offset: 0x001CA625
	private void SetMode(ClusterMapScreen.Mode mode)
	{
		this.m_mode = mode;
		if (this.m_mode == ClusterMapScreen.Mode.Default)
		{
			this.m_destinationSelector = null;
		}
		this.UpdateVis(null);
	}

	// Token: 0x0600505D RID: 20573 RVA: 0x001CC444 File Offset: 0x001CA644
	public ClusterMapScreen.Mode GetMode()
	{
		return this.m_mode;
	}

	// Token: 0x0600505E RID: 20574 RVA: 0x001CC44C File Offset: 0x001CA64C
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.MoveToNISPosition();
			this.UpdateVis(null);
			if (this.m_mode == ClusterMapScreen.Mode.Default)
			{
				this.TrySelectDefault();
			}
			Game.Instance.Subscribe(-1991583975, new Action<object>(this.OnFogOfWarRevealed));
			Game.Instance.Subscribe(-1554423969, new Action<object>(this.OnNewTelescopeTarget));
			Game.Instance.Subscribe(-1298331547, new Action<object>(this.OnClusterLocationChanged));
			ClusterMapSelectTool.Instance.Activate();
			this.SetShowingNonClusterMapHud(false);
			CameraController.Instance.DisableUserCameraControl = true;
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().MENUStarmapNotPausedSnapshot);
			MusicManager.instance.PlaySong("Music_Starmap", false);
			this.UpdateTearStatus();
			return;
		}
		Game.Instance.Unsubscribe(-1554423969, new Action<object>(this.OnNewTelescopeTarget));
		Game.Instance.Unsubscribe(-1991583975, new Action<object>(this.OnFogOfWarRevealed));
		Game.Instance.Unsubscribe(-1298331547, new Action<object>(this.OnClusterLocationChanged));
		this.m_mode = ClusterMapScreen.Mode.Default;
		this.m_closeOnSelect = false;
		this.m_destinationSelector = null;
		SelectTool.Instance.Activate();
		this.SetShowingNonClusterMapHud(true);
		CameraController.Instance.DisableUserCameraControl = false;
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUStarmapNotPausedSnapshot, STOP_MODE.ALLOWFADEOUT);
		if (MusicManager.instance.SongIsPlaying("Music_Starmap"))
		{
			MusicManager.instance.StopSong("Music_Starmap", true, STOP_MODE.ALLOWFADEOUT);
		}
	}

	// Token: 0x0600505F RID: 20575 RVA: 0x001CC5D7 File Offset: 0x001CA7D7
	private void SetShowingNonClusterMapHud(bool show)
	{
		PlanScreen.Instance.gameObject.SetActive(show);
		ToolMenu.Instance.gameObject.SetActive(show);
		OverlayScreen.Instance.gameObject.SetActive(show);
	}

	// Token: 0x06005060 RID: 20576 RVA: 0x001CC60C File Offset: 0x001CA80C
	private void SetSelectedEntity(ClusterGridEntity entity, bool frameDelay = false)
	{
		if (this.m_selectedEntity != null)
		{
			this.m_selectedEntity.Unsubscribe(543433792, this.m_onDestinationChangedDelegate);
			this.m_selectedEntity.Unsubscribe(-1503271301, this.m_onSelectObjectDelegate);
		}
		this.m_selectedEntity = entity;
		if (this.m_selectedEntity != null)
		{
			this.m_selectedEntity.Subscribe(543433792, this.m_onDestinationChangedDelegate);
			this.m_selectedEntity.Subscribe(-1503271301, this.m_onSelectObjectDelegate);
		}
		KSelectable kselectable = ((this.m_selectedEntity != null) ? this.m_selectedEntity.GetComponent<KSelectable>() : null);
		if (frameDelay)
		{
			ClusterMapSelectTool.Instance.SelectNextFrame(kselectable, false);
			return;
		}
		ClusterMapSelectTool.Instance.Select(kselectable, false);
	}

	// Token: 0x06005061 RID: 20577 RVA: 0x001CC6CF File Offset: 0x001CA8CF
	private void OnDestinationChanged(object data)
	{
		this.UpdateVis(null);
	}

	// Token: 0x06005062 RID: 20578 RVA: 0x001CC6D8 File Offset: 0x001CA8D8
	private void OnSelectObject(object data)
	{
		if (this.m_selectedEntity == null)
		{
			return;
		}
		KSelectable component = this.m_selectedEntity.GetComponent<KSelectable>();
		if (component == null || component.IsSelected)
		{
			return;
		}
		this.SetSelectedEntity(null, false);
		if (this.m_mode == ClusterMapScreen.Mode.SelectDestination)
		{
			if (this.m_closeOnSelect)
			{
				ManagementMenu.Instance.CloseAll();
			}
			else
			{
				this.SetMode(ClusterMapScreen.Mode.Default);
			}
		}
		this.UpdateVis(null);
	}

	// Token: 0x06005063 RID: 20579 RVA: 0x001CC745 File Offset: 0x001CA945
	private void OnFogOfWarRevealed(object data = null)
	{
		this.UpdateVis(null);
	}

	// Token: 0x06005064 RID: 20580 RVA: 0x001CC74E File Offset: 0x001CA94E
	private void OnNewTelescopeTarget(object data = null)
	{
		this.UpdateVis(null);
	}

	// Token: 0x06005065 RID: 20581 RVA: 0x001CC757 File Offset: 0x001CA957
	private void Update()
	{
		if (KInputManager.currentControllerIsGamepad)
		{
			this.mapScrollRect.AnalogUpdate(KInputManager.steamInputInterpreter.GetSteamCameraMovement() * this.scrollSpeed);
		}
	}

	// Token: 0x06005066 RID: 20582 RVA: 0x001CC780 File Offset: 0x001CA980
	private void TrySelectDefault()
	{
		if (this.m_selectedHex != null && this.m_selectedEntity != null)
		{
			this.UpdateVis(null);
			return;
		}
		WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
		if (activeWorld == null)
		{
			return;
		}
		ClusterGridEntity component = activeWorld.GetComponent<ClusterGridEntity>();
		if (component == null)
		{
			return;
		}
		this.SelectEntity(component, false);
	}

	// Token: 0x06005067 RID: 20583 RVA: 0x001CC7E0 File Offset: 0x001CA9E0
	private void GenerateGridVis(out int minR, out int maxR, out int minQ, out int maxQ)
	{
		minR = int.MaxValue;
		maxR = int.MinValue;
		minQ = int.MaxValue;
		maxQ = int.MinValue;
		foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in ClusterGrid.Instance.cellContents)
		{
			ClusterMapVisualizer clusterMapVisualizer = UnityEngine.Object.Instantiate<ClusterMapVisualizer>(this.cellVisPrefab, Vector3.zero, Quaternion.identity, this.cellVisContainer.transform);
			clusterMapVisualizer.rectTransform().SetLocalPosition(keyValuePair.Key.ToWorld());
			clusterMapVisualizer.gameObject.SetActive(true);
			ClusterMapHex component = clusterMapVisualizer.GetComponent<ClusterMapHex>();
			component.SetLocation(keyValuePair.Key);
			this.m_cellVisByLocation.Add(keyValuePair.Key, clusterMapVisualizer);
			minR = Mathf.Min(minR, component.location.R);
			maxR = Mathf.Max(maxR, component.location.R);
			minQ = Mathf.Min(minQ, component.location.Q);
			maxQ = Mathf.Max(maxQ, component.location.Q);
		}
		this.SetupVisGameObjects();
		this.UpdateVis(null);
	}

	// Token: 0x06005068 RID: 20584 RVA: 0x001CC934 File Offset: 0x001CAB34
	public Transform GetGridEntityNameTarget(ClusterGridEntity entity)
	{
		ClusterMapVisualizer clusterMapVisualizer;
		if (this.m_currentZoomScale >= 115f && this.m_gridEntityVis.TryGetValue(entity, out clusterMapVisualizer))
		{
			return clusterMapVisualizer.nameTarget;
		}
		return null;
	}

	// Token: 0x06005069 RID: 20585 RVA: 0x001CC968 File Offset: 0x001CAB68
	public override void ScreenUpdate(bool topLevel)
	{
		float num = Mathf.Min(4f * Time.unscaledDeltaTime, 0.9f);
		this.m_currentZoomScale = Mathf.Lerp(this.m_currentZoomScale, this.m_targetZoomScale, num);
		Vector2 vector = KInputManager.GetMousePos();
		Vector3 vector2 = this.mapScrollRect.content.InverseTransformPoint(vector);
		this.mapScrollRect.content.localScale = new Vector3(this.m_currentZoomScale, this.m_currentZoomScale, 1f);
		Vector3 vector3 = this.mapScrollRect.content.InverseTransformPoint(vector);
		this.mapScrollRect.content.localPosition += (vector3 - vector2) * this.m_currentZoomScale;
		this.MoveToNISPosition();
		this.FloatyAsteroidAnimation();
	}

	// Token: 0x0600506A RID: 20586 RVA: 0x001CCA3C File Offset: 0x001CAC3C
	private void FloatyAsteroidAnimation()
	{
		float num = 0f;
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			AsteroidGridEntity component = worldContainer.GetComponent<AsteroidGridEntity>();
			if (component != null && this.m_gridEntityVis.ContainsKey(component) && ClusterMapScreen.GetRevealLevel(component) == ClusterRevealLevel.Visible)
			{
				KAnimControllerBase firstAnimController = this.m_gridEntityVis[component].GetFirstAnimController();
				float num2 = this.floatCycleOffset + this.floatCycleScale * Mathf.Sin(this.floatCycleSpeed * (num + GameClock.Instance.GetTime()));
				firstAnimController.Offset = new Vector2(0f, num2);
			}
			num += 1f;
		}
	}

	// Token: 0x0600506B RID: 20587 RVA: 0x001CCB0C File Offset: 0x001CAD0C
	private void SetupVisGameObjects()
	{
		foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in ClusterGrid.Instance.cellContents)
		{
			foreach (ClusterGridEntity clusterGridEntity in keyValuePair.Value)
			{
				ClusterGrid.Instance.GetCellRevealLevel(keyValuePair.Key);
				ClusterRevealLevel isVisibleInFOW = clusterGridEntity.IsVisibleInFOW;
				ClusterRevealLevel revealLevel = ClusterMapScreen.GetRevealLevel(clusterGridEntity);
				if (clusterGridEntity.IsVisible && revealLevel != ClusterRevealLevel.Hidden && !this.m_gridEntityVis.ContainsKey(clusterGridEntity))
				{
					ClusterMapVisualizer clusterMapVisualizer = null;
					GameObject gameObject = null;
					switch (clusterGridEntity.Layer)
					{
					case EntityLayer.Asteroid:
						clusterMapVisualizer = this.terrainVisPrefab;
						gameObject = this.terrainVisContainer;
						break;
					case EntityLayer.Craft:
						clusterMapVisualizer = this.mobileVisPrefab;
						gameObject = this.mobileVisContainer;
						break;
					case EntityLayer.POI:
						clusterMapVisualizer = this.staticVisPrefab;
						gameObject = this.POIVisContainer;
						break;
					case EntityLayer.Telescope:
						clusterMapVisualizer = this.staticVisPrefab;
						gameObject = this.telescopeVisContainer;
						break;
					case EntityLayer.Payload:
						clusterMapVisualizer = this.mobileVisPrefab;
						gameObject = this.mobileVisContainer;
						break;
					case EntityLayer.FX:
						clusterMapVisualizer = this.staticVisPrefab;
						gameObject = this.FXVisContainer;
						break;
					}
					ClusterNameDisplayScreen.Instance.AddNewEntry(clusterGridEntity);
					ClusterMapVisualizer clusterMapVisualizer2 = UnityEngine.Object.Instantiate<ClusterMapVisualizer>(clusterMapVisualizer, gameObject.transform);
					clusterMapVisualizer2.Init(clusterGridEntity, this.pathDrawer);
					clusterMapVisualizer2.gameObject.SetActive(true);
					this.m_gridEntityAnims.Add(clusterGridEntity, clusterMapVisualizer2);
					this.m_gridEntityVis.Add(clusterGridEntity, clusterMapVisualizer2);
					clusterGridEntity.positionDirty = false;
					clusterGridEntity.Subscribe(1502190696, new Action<object>(this.RemoveDeletedEntities));
				}
			}
		}
		this.RemoveDeletedEntities(null);
		foreach (KeyValuePair<ClusterGridEntity, ClusterMapVisualizer> keyValuePair2 in this.m_gridEntityVis)
		{
			ClusterGridEntity key = keyValuePair2.Key;
			if (key.Layer == EntityLayer.Asteroid)
			{
				int id = key.GetComponent<WorldContainer>().id;
				keyValuePair2.Value.alertVignette.worldID = id;
			}
		}
	}

	// Token: 0x0600506C RID: 20588 RVA: 0x001CCD90 File Offset: 0x001CAF90
	private void RemoveDeletedEntities(object obj = null)
	{
		foreach (ClusterGridEntity clusterGridEntity in this.m_gridEntityVis.Keys.Where((ClusterGridEntity x) => x == null || x.gameObject == (GameObject)obj).ToList<ClusterGridEntity>())
		{
			global::Util.KDestroyGameObject(this.m_gridEntityVis[clusterGridEntity]);
			this.m_gridEntityVis.Remove(clusterGridEntity);
			this.m_gridEntityAnims.Remove(clusterGridEntity);
		}
	}

	// Token: 0x0600506D RID: 20589 RVA: 0x001CCE30 File Offset: 0x001CB030
	private void OnClusterLocationChanged(object data)
	{
		this.UpdateVis(null);
	}

	// Token: 0x0600506E RID: 20590 RVA: 0x001CCE3C File Offset: 0x001CB03C
	public static ClusterRevealLevel GetRevealLevel(ClusterGridEntity entity)
	{
		ClusterRevealLevel cellRevealLevel = ClusterGrid.Instance.GetCellRevealLevel(entity.Location);
		ClusterRevealLevel isVisibleInFOW = entity.IsVisibleInFOW;
		if (cellRevealLevel == ClusterRevealLevel.Visible || isVisibleInFOW == ClusterRevealLevel.Visible)
		{
			return ClusterRevealLevel.Visible;
		}
		if (cellRevealLevel == ClusterRevealLevel.Peeked && isVisibleInFOW == ClusterRevealLevel.Peeked)
		{
			return ClusterRevealLevel.Peeked;
		}
		return ClusterRevealLevel.Hidden;
	}

	// Token: 0x0600506F RID: 20591 RVA: 0x001CCE78 File Offset: 0x001CB078
	private void UpdateVis(object data = null)
	{
		this.SetupVisGameObjects();
		this.UpdatePaths();
		foreach (KeyValuePair<ClusterGridEntity, ClusterMapVisualizer> keyValuePair in this.m_gridEntityAnims)
		{
			ClusterRevealLevel revealLevel = ClusterMapScreen.GetRevealLevel(keyValuePair.Key);
			keyValuePair.Value.Show(revealLevel);
			bool flag = this.m_selectedEntity == keyValuePair.Key;
			keyValuePair.Value.Select(flag);
			if (keyValuePair.Key.positionDirty)
			{
				Vector3 position = ClusterGrid.Instance.GetPosition(keyValuePair.Key);
				keyValuePair.Value.rectTransform().SetLocalPosition(position);
				keyValuePair.Key.positionDirty = false;
			}
		}
		if (this.m_selectedEntity != null && this.m_gridEntityVis.ContainsKey(this.m_selectedEntity))
		{
			ClusterMapVisualizer clusterMapVisualizer = this.m_gridEntityVis[this.m_selectedEntity];
			this.m_selectMarker.SetTargetTransform(clusterMapVisualizer.transform);
			this.m_selectMarker.gameObject.SetActive(true);
			clusterMapVisualizer.transform.SetAsLastSibling();
		}
		else
		{
			this.m_selectMarker.gameObject.SetActive(false);
		}
		foreach (KeyValuePair<AxialI, ClusterMapVisualizer> keyValuePair2 in this.m_cellVisByLocation)
		{
			ClusterMapHex component = keyValuePair2.Value.GetComponent<ClusterMapHex>();
			AxialI key = keyValuePair2.Key;
			component.SetRevealed(ClusterGrid.Instance.GetCellRevealLevel(key));
		}
		this.UpdateHexToggleStates();
		this.FloatyAsteroidAnimation();
	}

	// Token: 0x06005070 RID: 20592 RVA: 0x001CD038 File Offset: 0x001CB238
	private void OnEntityDestroyed(object obj)
	{
		this.RemoveDeletedEntities(null);
	}

	// Token: 0x06005071 RID: 20593 RVA: 0x001CD044 File Offset: 0x001CB244
	private void UpdateHexToggleStates()
	{
		bool flag = this.m_hoveredHex != null && ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_hoveredHex.location, EntityLayer.Asteroid);
		foreach (KeyValuePair<AxialI, ClusterMapVisualizer> keyValuePair in this.m_cellVisByLocation)
		{
			ClusterMapHex component = keyValuePair.Value.GetComponent<ClusterMapHex>();
			AxialI key = keyValuePair.Key;
			ClusterMapHex.ToggleState toggleState;
			if (this.m_selectedHex != null && this.m_selectedHex.location == key)
			{
				toggleState = ClusterMapHex.ToggleState.Selected;
			}
			else if (flag && this.m_hoveredHex.location.IsAdjacent(key))
			{
				toggleState = ClusterMapHex.ToggleState.OrbitHighlight;
			}
			else
			{
				toggleState = ClusterMapHex.ToggleState.Unselected;
			}
			component.UpdateToggleState(toggleState);
		}
	}

	// Token: 0x06005072 RID: 20594 RVA: 0x001CD11C File Offset: 0x001CB31C
	public void SelectEntity(ClusterGridEntity entity, bool frameDelay = false)
	{
		if (entity != null)
		{
			this.SetSelectedEntity(entity, frameDelay);
			ClusterMapHex component = this.m_cellVisByLocation[entity.Location].GetComponent<ClusterMapHex>();
			this.m_selectedHex = component;
		}
		this.UpdateVis(null);
	}

	// Token: 0x06005073 RID: 20595 RVA: 0x001CD160 File Offset: 0x001CB360
	public void SelectHex(ClusterMapHex newSelectionHex)
	{
		if (this.m_mode == ClusterMapScreen.Mode.Default)
		{
			List<ClusterGridEntity> visibleEntitiesAtCell = ClusterGrid.Instance.GetVisibleEntitiesAtCell(newSelectionHex.location);
			for (int i = visibleEntitiesAtCell.Count - 1; i >= 0; i--)
			{
				KSelectable component = visibleEntitiesAtCell[i].GetComponent<KSelectable>();
				if (component == null || !component.IsSelectable)
				{
					visibleEntitiesAtCell.RemoveAt(i);
				}
			}
			if (visibleEntitiesAtCell.Count == 0)
			{
				this.SetSelectedEntity(null, false);
			}
			else
			{
				int num = visibleEntitiesAtCell.IndexOf(this.m_selectedEntity);
				int num2 = 0;
				if (num >= 0)
				{
					num2 = (num + 1) % visibleEntitiesAtCell.Count;
				}
				this.SetSelectedEntity(visibleEntitiesAtCell[num2], false);
			}
			this.m_selectedHex = newSelectionHex;
		}
		else if (this.m_mode == ClusterMapScreen.Mode.SelectDestination)
		{
			global::Debug.Assert(this.m_destinationSelector != null, "Selected a hex in SelectDestination mode with no ClusterDestinationSelector");
			if (ClusterGrid.Instance.GetPath(this.m_selectedHex.location, newSelectionHex.location, this.m_destinationSelector) != null)
			{
				this.m_destinationSelector.SetDestination(newSelectionHex.location);
				if (this.m_closeOnSelect)
				{
					ManagementMenu.Instance.CloseAll();
				}
				else
				{
					this.SetMode(ClusterMapScreen.Mode.Default);
				}
			}
		}
		this.UpdateVis(null);
	}

	// Token: 0x06005074 RID: 20596 RVA: 0x001CD280 File Offset: 0x001CB480
	public bool HasCurrentHover()
	{
		return this.m_hoveredHex != null;
	}

	// Token: 0x06005075 RID: 20597 RVA: 0x001CD28E File Offset: 0x001CB48E
	public AxialI GetCurrentHoverLocation()
	{
		return this.m_hoveredHex.location;
	}

	// Token: 0x06005076 RID: 20598 RVA: 0x001CD29B File Offset: 0x001CB49B
	public void OnHoverHex(ClusterMapHex newHoverHex)
	{
		this.m_hoveredHex = newHoverHex;
		if (this.m_mode == ClusterMapScreen.Mode.SelectDestination)
		{
			this.UpdateVis(null);
		}
		this.UpdateHexToggleStates();
	}

	// Token: 0x06005077 RID: 20599 RVA: 0x001CD2BA File Offset: 0x001CB4BA
	public void OnUnhoverHex(ClusterMapHex unhoveredHex)
	{
		if (this.m_hoveredHex == unhoveredHex)
		{
			this.m_hoveredHex = null;
			this.UpdateHexToggleStates();
		}
	}

	// Token: 0x06005078 RID: 20600 RVA: 0x001CD2D7 File Offset: 0x001CB4D7
	public void SetLocationHighlight(AxialI location, bool highlight)
	{
		this.m_cellVisByLocation[location].GetComponent<ClusterMapHex>().ChangeState(highlight ? 1 : 0);
	}

	// Token: 0x06005079 RID: 20601 RVA: 0x001CD2F8 File Offset: 0x001CB4F8
	private void UpdatePaths()
	{
		ClusterDestinationSelector clusterDestinationSelector = ((this.m_selectedEntity != null) ? this.m_selectedEntity.GetComponent<ClusterDestinationSelector>() : null);
		if (this.m_mode != ClusterMapScreen.Mode.SelectDestination || !(this.m_hoveredHex != null))
		{
			if (this.m_previewMapPath != null)
			{
				global::Util.KDestroyGameObject(this.m_previewMapPath);
				this.m_previewMapPath = null;
			}
			return;
		}
		global::Debug.Assert(this.m_destinationSelector != null, "In SelectDestination mode without a destination selector");
		AxialI myWorldLocation = this.m_destinationSelector.GetMyWorldLocation();
		string text;
		List<AxialI> path = ClusterGrid.Instance.GetPath(myWorldLocation, this.m_hoveredHex.location, this.m_destinationSelector, out text);
		if (path != null)
		{
			if (this.m_previewMapPath == null)
			{
				this.m_previewMapPath = this.pathDrawer.AddPath();
			}
			ClusterMapVisualizer clusterMapVisualizer = this.m_gridEntityVis[this.GetSelectorGridEntity(this.m_destinationSelector)];
			this.m_previewMapPath.SetPoints(ClusterMapPathDrawer.GetDrawPathList(clusterMapVisualizer.transform.localPosition, path));
			this.m_previewMapPath.SetColor(this.rocketPreviewPathColor);
		}
		else if (this.m_previewMapPath != null)
		{
			global::Util.KDestroyGameObject(this.m_previewMapPath);
			this.m_previewMapPath = null;
		}
		int num = ((path != null) ? path.Count : (-1));
		if (this.m_selectedEntity != null)
		{
			float range = this.m_selectedEntity.GetComponent<IClusterRange>().GetRange();
			if ((float)num > range / 600f && string.IsNullOrEmpty(text))
			{
				text = string.Format(UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_OUT_OF_RANGE, range / 600f);
			}
			bool repeat = clusterDestinationSelector.GetComponent<RocketClusterDestinationSelector>().Repeat;
			this.m_hoveredHex.SetDestinationStatus(text, num, (int)range, repeat);
			return;
		}
		this.m_hoveredHex.SetDestinationStatus(text);
	}

	// Token: 0x0600507A RID: 20602 RVA: 0x001CD4C0 File Offset: 0x001CB6C0
	private ClusterGridEntity GetSelectorGridEntity(ClusterDestinationSelector selector)
	{
		ClusterGridEntity component = selector.GetComponent<ClusterGridEntity>();
		if (component != null && ClusterGrid.Instance.IsVisible(component))
		{
			return component;
		}
		ClusterGridEntity visibleEntityOfLayerAtCell = ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(selector.GetMyWorldLocation(), EntityLayer.Asteroid);
		global::Debug.Assert(component != null || visibleEntityOfLayerAtCell != null, string.Format("{0} has no grid entity and isn't located at a visible asteroid at {1}", selector, selector.GetMyWorldLocation()));
		if (visibleEntityOfLayerAtCell)
		{
			return visibleEntityOfLayerAtCell;
		}
		return component;
	}

	// Token: 0x0600507B RID: 20603 RVA: 0x001CD538 File Offset: 0x001CB738
	private void UpdateTearStatus()
	{
		ClusterPOIManager clusterPOIManager = null;
		if (ClusterManager.Instance != null)
		{
			clusterPOIManager = ClusterManager.Instance.GetComponent<ClusterPOIManager>();
		}
		if (clusterPOIManager != null)
		{
			TemporalTear temporalTear = clusterPOIManager.GetTemporalTear();
			if (temporalTear != null)
			{
				temporalTear.UpdateStatus();
			}
		}
	}

	// Token: 0x040035FA RID: 13818
	public static ClusterMapScreen Instance;

	// Token: 0x040035FB RID: 13819
	public GameObject cellVisContainer;

	// Token: 0x040035FC RID: 13820
	public GameObject terrainVisContainer;

	// Token: 0x040035FD RID: 13821
	public GameObject mobileVisContainer;

	// Token: 0x040035FE RID: 13822
	public GameObject telescopeVisContainer;

	// Token: 0x040035FF RID: 13823
	public GameObject POIVisContainer;

	// Token: 0x04003600 RID: 13824
	public GameObject FXVisContainer;

	// Token: 0x04003601 RID: 13825
	public ClusterMapVisualizer cellVisPrefab;

	// Token: 0x04003602 RID: 13826
	public ClusterMapVisualizer terrainVisPrefab;

	// Token: 0x04003603 RID: 13827
	public ClusterMapVisualizer mobileVisPrefab;

	// Token: 0x04003604 RID: 13828
	public ClusterMapVisualizer staticVisPrefab;

	// Token: 0x04003605 RID: 13829
	public Color rocketPathColor;

	// Token: 0x04003606 RID: 13830
	public Color rocketSelectedPathColor;

	// Token: 0x04003607 RID: 13831
	public Color rocketPreviewPathColor;

	// Token: 0x04003608 RID: 13832
	private ClusterMapHex m_selectedHex;

	// Token: 0x04003609 RID: 13833
	private ClusterMapHex m_hoveredHex;

	// Token: 0x0400360A RID: 13834
	private ClusterGridEntity m_selectedEntity;

	// Token: 0x0400360B RID: 13835
	public KButton closeButton;

	// Token: 0x0400360C RID: 13836
	private const float ZOOM_SCALE_MIN = 50f;

	// Token: 0x0400360D RID: 13837
	private const float ZOOM_SCALE_MAX = 150f;

	// Token: 0x0400360E RID: 13838
	private const float ZOOM_SCALE_INCREMENT = 25f;

	// Token: 0x0400360F RID: 13839
	private const float ZOOM_SCALE_SPEED = 4f;

	// Token: 0x04003610 RID: 13840
	private const float ZOOM_NAME_THRESHOLD = 115f;

	// Token: 0x04003611 RID: 13841
	private float m_currentZoomScale = 75f;

	// Token: 0x04003612 RID: 13842
	private float m_targetZoomScale = 75f;

	// Token: 0x04003613 RID: 13843
	private ClusterMapPath m_previewMapPath;

	// Token: 0x04003614 RID: 13844
	private Dictionary<ClusterGridEntity, ClusterMapVisualizer> m_gridEntityVis = new Dictionary<ClusterGridEntity, ClusterMapVisualizer>();

	// Token: 0x04003615 RID: 13845
	private Dictionary<ClusterGridEntity, ClusterMapVisualizer> m_gridEntityAnims = new Dictionary<ClusterGridEntity, ClusterMapVisualizer>();

	// Token: 0x04003616 RID: 13846
	private Dictionary<AxialI, ClusterMapVisualizer> m_cellVisByLocation = new Dictionary<AxialI, ClusterMapVisualizer>();

	// Token: 0x04003617 RID: 13847
	private Action<object> m_onDestinationChangedDelegate;

	// Token: 0x04003618 RID: 13848
	private Action<object> m_onSelectObjectDelegate;

	// Token: 0x04003619 RID: 13849
	[SerializeField]
	private KScrollRect mapScrollRect;

	// Token: 0x0400361A RID: 13850
	[SerializeField]
	private float scrollSpeed = 15f;

	// Token: 0x0400361B RID: 13851
	public GameObject selectMarkerPrefab;

	// Token: 0x0400361C RID: 13852
	public ClusterMapPathDrawer pathDrawer;

	// Token: 0x0400361D RID: 13853
	private SelectMarker m_selectMarker;

	// Token: 0x0400361E RID: 13854
	private bool movingToTargetNISPosition;

	// Token: 0x0400361F RID: 13855
	private Vector3 targetNISPosition;

	// Token: 0x04003620 RID: 13856
	private float targetNISZoom;

	// Token: 0x04003621 RID: 13857
	private AxialI selectOnMoveNISComplete;

	// Token: 0x04003622 RID: 13858
	private ClusterMapScreen.Mode m_mode;

	// Token: 0x04003623 RID: 13859
	private ClusterDestinationSelector m_destinationSelector;

	// Token: 0x04003624 RID: 13860
	private bool m_closeOnSelect;

	// Token: 0x04003625 RID: 13861
	private Coroutine activeMoveToTargetRoutine;

	// Token: 0x04003626 RID: 13862
	public float floatCycleScale = 4f;

	// Token: 0x04003627 RID: 13863
	public float floatCycleOffset = 0.75f;

	// Token: 0x04003628 RID: 13864
	public float floatCycleSpeed = 0.75f;

	// Token: 0x020018E0 RID: 6368
	public enum Mode
	{
		// Token: 0x040072A1 RID: 29345
		Default,
		// Token: 0x040072A2 RID: 29346
		SelectDestination
	}
}
