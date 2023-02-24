using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020005AC RID: 1452
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Door")]
public class Door : Workable, ISaveLoadable, ISim200ms, INavDoor
{
	// Token: 0x060023CE RID: 9166 RVA: 0x000C1620 File Offset: 0x000BF820
	private void OnCopySettings(object data)
	{
		Door component = ((GameObject)data).GetComponent<Door>();
		if (component != null)
		{
			this.QueueStateChange(component.requestedState);
		}
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x000C164E File Offset: 0x000BF84E
	public Door()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x060023D0 RID: 9168 RVA: 0x000C167E File Offset: 0x000BF87E
	public Door.ControlState CurrentState
	{
		get
		{
			return this.controlState;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060023D1 RID: 9169 RVA: 0x000C1686 File Offset: 0x000BF886
	public Door.ControlState RequestedState
	{
		get
		{
			return this.requestedState;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x060023D2 RID: 9170 RVA: 0x000C168E File Offset: 0x000BF88E
	public bool ShouldBlockFallingSand
	{
		get
		{
			return this.rotatable.GetOrientation() != this.verticalOrientation;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000C16A6 File Offset: 0x000BF8A6
	public bool isSealed
	{
		get
		{
			return this.controller.sm.isSealed.Get(this.controller);
		}
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x000C16C4 File Offset: 0x000BF8C4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = Door.OVERRIDE_ANIMS;
		this.synchronizeAnims = false;
		base.SetWorkTime(3f);
		if (!string.IsNullOrEmpty(this.doorClosingSoundEventName))
		{
			this.doorClosingSound = GlobalAssets.GetSound(this.doorClosingSoundEventName, false);
		}
		if (!string.IsNullOrEmpty(this.doorOpeningSoundEventName))
		{
			this.doorOpeningSound = GlobalAssets.GetSound(this.doorOpeningSoundEventName, false);
		}
		base.Subscribe<Door>(-905833192, Door.OnCopySettingsDelegate);
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x000C1743 File Offset: 0x000BF943
	private Door.ControlState GetNextState(Door.ControlState wantedState)
	{
		return (wantedState + 1) % Door.ControlState.NumStates;
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x000C174A File Offset: 0x000BF94A
	private static bool DisplacesGas(Door.DoorType type)
	{
		return type != Door.DoorType.Internal;
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x000C1754 File Offset: 0x000BF954
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (base.GetComponent<KPrefabID>() != null)
		{
			this.log = new LoggerFSS("Door", 35);
		}
		if (!this.allowAutoControl && this.controlState == Door.ControlState.Auto)
		{
			this.controlState = Door.ControlState.Locked;
		}
		StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
		HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
		if (Door.DisplacesGas(this.doorType))
		{
			structureTemperatures.Bypass(handle);
		}
		this.controller = new Door.Controller.Instance(this);
		this.controller.StartSM();
		if (this.doorType == Door.DoorType.Sealed && !this.hasBeenUnsealed)
		{
			this.Seal();
		}
		this.UpdateDoorSpeed(this.operational.IsOperational);
		base.Subscribe<Door>(-592767678, Door.OnOperationalChangedDelegate);
		base.Subscribe<Door>(824508782, Door.OnOperationalChangedDelegate);
		base.Subscribe<Door>(-801688580, Door.OnLogicValueChangedDelegate);
		this.requestedState = this.CurrentState;
		this.ApplyRequestedControlState(true);
		int num = ((this.rotatable.GetOrientation() == Orientation.Neutral) ? (this.building.Def.WidthInCells * (this.building.Def.HeightInCells - 1)) : 0);
		int num2 = ((this.rotatable.GetOrientation() == Orientation.Neutral) ? this.building.Def.WidthInCells : this.building.Def.HeightInCells);
		for (int num3 = 0; num3 != num2; num3++)
		{
			int num4 = this.building.PlacementCells[num + num3];
			Grid.FakeFloor.Add(num4);
			Pathfinding.Instance.AddDirtyNavGridCell(num4);
		}
		List<int> list = new List<int>();
		foreach (int num5 in this.building.PlacementCells)
		{
			Grid.HasDoor[num5] = true;
			if (this.rotatable.IsRotated)
			{
				list.Add(Grid.CellAbove(num5));
				list.Add(Grid.CellBelow(num5));
			}
			else
			{
				list.Add(Grid.CellLeft(num5));
				list.Add(Grid.CellRight(num5));
			}
			SimMessages.SetCellProperties(num5, 8);
			if (Door.DisplacesGas(this.doorType))
			{
				Grid.RenderedByWorld[num5] = false;
			}
		}
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x000C1994 File Offset: 0x000BFB94
	protected override void OnCleanUp()
	{
		this.UpdateDoorState(true);
		List<int> list = new List<int>();
		foreach (int num in this.building.PlacementCells)
		{
			SimMessages.ClearCellProperties(num, 12);
			Grid.RenderedByWorld[num] = Grid.Element[num].substance.renderedByWorld;
			Grid.FakeFloor.Remove(num);
			if (Grid.Element[num].IsSolid)
			{
				SimMessages.ReplaceAndDisplaceElement(num, SimHashes.Vacuum, CellEventLogger.Instance.DoorOpen, 0f, -1f, byte.MaxValue, 0, -1);
			}
			Pathfinding.Instance.AddDirtyNavGridCell(num);
			if (this.rotatable.IsRotated)
			{
				list.Add(Grid.CellAbove(num));
				list.Add(Grid.CellBelow(num));
			}
			else
			{
				list.Add(Grid.CellLeft(num));
				list.Add(Grid.CellRight(num));
			}
		}
		foreach (int num2 in this.building.PlacementCells)
		{
			Grid.HasDoor[num2] = false;
			Game.Instance.SetDupePassableSolid(num2, false, Grid.Solid[num2]);
			Grid.CritterImpassable[num2] = false;
			Grid.DupeImpassable[num2] = false;
			Pathfinding.Instance.AddDirtyNavGridCell(num2);
		}
		base.OnCleanUp();
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x000C1AF0 File Offset: 0x000BFCF0
	public void Seal()
	{
		this.controller.sm.isSealed.Set(true, this.controller, false);
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x000C1B10 File Offset: 0x000BFD10
	public void OrderUnseal()
	{
		this.controller.GoTo(this.controller.sm.Sealed.awaiting_unlock);
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x000C1B34 File Offset: 0x000BFD34
	private void RefreshControlState()
	{
		switch (this.controlState)
		{
		case Door.ControlState.Auto:
			this.controller.sm.isLocked.Set(false, this.controller, false);
			break;
		case Door.ControlState.Opened:
			this.controller.sm.isLocked.Set(false, this.controller, false);
			break;
		case Door.ControlState.Locked:
			this.controller.sm.isLocked.Set(true, this.controller, false);
			break;
		}
		base.Trigger(279163026, this.controlState);
		this.SetWorldState();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.CurrentDoorControlState, this);
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x000C1C04 File Offset: 0x000BFE04
	private void OnOperationalChanged(object data)
	{
		bool isOperational = this.operational.IsOperational;
		if (isOperational != this.on)
		{
			this.UpdateDoorSpeed(isOperational);
			if (this.on && base.GetComponent<KPrefabID>().HasTag(GameTags.Transition))
			{
				this.SetActive(true);
				return;
			}
			this.SetActive(false);
		}
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x000C1C58 File Offset: 0x000BFE58
	private void UpdateDoorSpeed(bool powered)
	{
		this.on = powered;
		this.UpdateAnimAndSoundParams(powered);
		float positionPercent = this.animController.GetPositionPercent();
		this.animController.Play(this.animController.CurrentAnim.hash, this.animController.PlayMode, 1f, 0f);
		this.animController.SetPositionPercent(positionPercent);
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x000C1CBC File Offset: 0x000BFEBC
	private void UpdateAnimAndSoundParams(bool powered)
	{
		if (powered)
		{
			this.animController.PlaySpeedMultiplier = this.poweredAnimSpeed;
			if (this.doorClosingSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorClosingSound, Door.SOUND_POWERED_PARAMETER, 1f);
			}
			if (this.doorOpeningSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorOpeningSound, Door.SOUND_POWERED_PARAMETER, 1f);
				return;
			}
		}
		else
		{
			this.animController.PlaySpeedMultiplier = this.unpoweredAnimSpeed;
			if (this.doorClosingSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorClosingSound, Door.SOUND_POWERED_PARAMETER, 0f);
			}
			if (this.doorOpeningSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorOpeningSound, Door.SOUND_POWERED_PARAMETER, 0f);
			}
		}
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x000C1D7B File Offset: 0x000BFF7B
	private void SetActive(bool active)
	{
		if (this.operational.IsOperational)
		{
			this.operational.SetActive(active, false);
		}
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x000C1D98 File Offset: 0x000BFF98
	private void SetWorldState()
	{
		int[] placementCells = this.building.PlacementCells;
		bool flag = this.IsOpen();
		this.SetPassableState(flag, placementCells);
		this.SetSimState(flag, placementCells);
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x000C1DC8 File Offset: 0x000BFFC8
	private void SetPassableState(bool is_door_open, IList<int> cells)
	{
		for (int i = 0; i < cells.Count; i++)
		{
			int num = cells[i];
			switch (this.doorType)
			{
			case Door.DoorType.Pressure:
			case Door.DoorType.ManualPressure:
			case Door.DoorType.Sealed:
			{
				Grid.CritterImpassable[num] = this.controlState != Door.ControlState.Opened;
				bool flag = !is_door_open;
				bool flag2 = this.controlState != Door.ControlState.Locked;
				Game.Instance.SetDupePassableSolid(num, flag2, flag);
				if (this.controlState == Door.ControlState.Opened)
				{
					this.doorOpenLiquidRefreshHack = true;
					this.doorOpenLiquidRefreshTime = 1f;
				}
				break;
			}
			case Door.DoorType.Internal:
				Grid.CritterImpassable[num] = this.controlState != Door.ControlState.Opened;
				Grid.DupeImpassable[num] = this.controlState == Door.ControlState.Locked;
				break;
			}
			Pathfinding.Instance.AddDirtyNavGridCell(num);
		}
	}

	// Token: 0x060023E2 RID: 9186 RVA: 0x000C1EA0 File Offset: 0x000C00A0
	private void SetSimState(bool is_door_open, IList<int> cells)
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		float num = component.Mass / (float)cells.Count;
		for (int i = 0; i < cells.Count; i++)
		{
			int num2 = cells[i];
			Door.DoorType doorType = this.doorType;
			if (doorType <= Door.DoorType.ManualPressure || doorType == Door.DoorType.Sealed)
			{
				World.Instance.groundRenderer.MarkDirty(num2);
				if (is_door_open)
				{
					SimMessages.Dig(num2, Game.Instance.callbackManager.Add(new Game.CallbackInfo(new System.Action(this.OnSimDoorOpened), false)).index, true);
					if (this.ShouldBlockFallingSand)
					{
						SimMessages.ClearCellProperties(num2, 4);
					}
					else
					{
						SimMessages.SetCellProperties(num2, 4);
					}
				}
				else
				{
					HandleVector<Game.CallbackInfo>.Handle handle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new System.Action(this.OnSimDoorClosed), false));
					float num3 = component.Temperature;
					if (num3 <= 0f)
					{
						num3 = component.Temperature;
					}
					SimMessages.ReplaceAndDisplaceElement(num2, component.ElementID, CellEventLogger.Instance.DoorClose, num, num3, byte.MaxValue, 0, handle.index);
					SimMessages.SetCellProperties(num2, 4);
				}
			}
		}
	}

	// Token: 0x060023E3 RID: 9187 RVA: 0x000C1FC0 File Offset: 0x000C01C0
	private void UpdateDoorState(bool cleaningUp)
	{
		foreach (int num in this.building.PlacementCells)
		{
			if (Grid.IsValidCell(num))
			{
				Grid.Foundation[num] = !cleaningUp;
			}
		}
	}

	// Token: 0x060023E4 RID: 9188 RVA: 0x000C2004 File Offset: 0x000C0204
	public void QueueStateChange(Door.ControlState nextState)
	{
		if (this.requestedState != nextState)
		{
			this.requestedState = nextState;
		}
		else
		{
			this.requestedState = this.controlState;
		}
		if (this.requestedState == this.controlState)
		{
			if (this.changeStateChore != null)
			{
				this.changeStateChore.Cancel("Change state");
				this.changeStateChore = null;
				base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, false);
			}
			return;
		}
		if (DebugHandler.InstantBuildMode)
		{
			this.controlState = this.requestedState;
			this.RefreshControlState();
			this.OnOperationalChanged(null);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, false);
			this.Open();
			this.Close();
			return;
		}
		if (this.changeStateChore != null)
		{
			this.changeStateChore.Cancel("Change state");
		}
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, this);
		this.changeStateChore = new WorkChore<Door>(Db.Get().ChoreTypes.Toggle, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x060023E5 RID: 9189 RVA: 0x000C2124 File Offset: 0x000C0324
	private void OnSimDoorOpened()
	{
		if (this == null || !Door.DisplacesGas(this.doorType))
		{
			return;
		}
		StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
		HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
		structureTemperatures.UnBypass(handle);
		this.do_melt_check = false;
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x000C2168 File Offset: 0x000C0368
	private void OnSimDoorClosed()
	{
		if (this == null || !Door.DisplacesGas(this.doorType))
		{
			return;
		}
		StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
		HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
		structureTemperatures.Bypass(handle);
		this.do_melt_check = true;
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x000C21AB File Offset: 0x000C03AB
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.changeStateChore = null;
		this.ApplyRequestedControlState(false);
	}

	// Token: 0x060023E8 RID: 9192 RVA: 0x000C21C4 File Offset: 0x000C03C4
	public void Open()
	{
		if (this.openCount == 0 && Door.DisplacesGas(this.doorType))
		{
			StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
			HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
			if (handle.IsValid() && structureTemperatures.IsBypassed(handle))
			{
				int[] placementCells = this.building.PlacementCells;
				float num = 0f;
				int num2 = 0;
				foreach (int num3 in placementCells)
				{
					if (Grid.Mass[num3] > 0f)
					{
						num2++;
						num += Grid.Temperature[num3];
					}
				}
				if (num2 > 0)
				{
					num /= (float)placementCells.Length;
					PrimaryElement component = base.GetComponent<PrimaryElement>();
					KCrashReporter.Assert(num > 0f, "Door has calculated an invalid temperature");
					component.Temperature = num;
				}
			}
		}
		this.openCount++;
		Door.ControlState controlState = this.controlState;
		if (controlState > Door.ControlState.Opened)
		{
			return;
		}
		this.controller.sm.isOpen.Set(true, this.controller, false);
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x000C22D4 File Offset: 0x000C04D4
	public void Close()
	{
		this.openCount = Mathf.Max(0, this.openCount - 1);
		if (this.openCount == 0 && Door.DisplacesGas(this.doorType))
		{
			StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
			HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (handle.IsValid() && !structureTemperatures.IsBypassed(handle))
			{
				float temperature = structureTemperatures.GetPayload(handle).Temperature;
				component.Temperature = temperature;
			}
		}
		switch (this.controlState)
		{
		case Door.ControlState.Auto:
			if (this.openCount == 0)
			{
				this.controller.sm.isOpen.Set(false, this.controller, false);
				Game.Instance.userMenu.Refresh(base.gameObject);
			}
			break;
		case Door.ControlState.Opened:
			break;
		case Door.ControlState.Locked:
			this.controller.sm.isOpen.Set(false, this.controller, false);
			return;
		default:
			return;
		}
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x000C23C8 File Offset: 0x000C05C8
	public bool IsOpen()
	{
		return this.controller.IsInsideState(this.controller.sm.open) || this.controller.IsInsideState(this.controller.sm.closedelay) || this.controller.IsInsideState(this.controller.sm.closeblocked);
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x000C242C File Offset: 0x000C062C
	private void ApplyRequestedControlState(bool force = false)
	{
		if (this.requestedState == this.controlState && !force)
		{
			return;
		}
		this.controlState = this.requestedState;
		this.RefreshControlState();
		this.OnOperationalChanged(null);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, false);
		base.Trigger(1734268753, this);
		if (!force)
		{
			this.Open();
			this.Close();
		}
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x000C249C File Offset: 0x000C069C
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != Door.OPEN_CLOSE_PORT_ID)
		{
			return;
		}
		int newValue = logicValueChanged.newValue;
		if (this.changeStateChore != null)
		{
			this.changeStateChore.Cancel("Change state");
			this.changeStateChore = null;
		}
		this.requestedState = (LogicCircuitNetwork.IsBitActive(0, newValue) ? Door.ControlState.Opened : Door.ControlState.Locked);
		this.applyLogicChange = true;
	}

	// Token: 0x060023ED RID: 9197 RVA: 0x000C2508 File Offset: 0x000C0708
	public void Sim200ms(float dt)
	{
		if (this == null)
		{
			return;
		}
		if (this.doorOpenLiquidRefreshHack)
		{
			this.doorOpenLiquidRefreshTime -= dt;
			if (this.doorOpenLiquidRefreshTime <= 0f)
			{
				this.doorOpenLiquidRefreshHack = false;
				foreach (int num in this.building.PlacementCells)
				{
					Pathfinding.Instance.AddDirtyNavGridCell(num);
				}
			}
		}
		if (this.applyLogicChange)
		{
			this.applyLogicChange = false;
			this.ApplyRequestedControlState(false);
		}
		if (this.do_melt_check)
		{
			StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
			HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
			if (handle.IsValid() && structureTemperatures.IsBypassed(handle))
			{
				foreach (int num2 in this.building.PlacementCells)
				{
					if (!Grid.Solid[num2])
					{
						Util.KDestroyGameObject(this);
						return;
					}
				}
			}
		}
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x000C2691 File Offset: 0x000C0891
	bool INavDoor.get_isSpawned()
	{
		return base.isSpawned;
	}

	// Token: 0x04001482 RID: 5250
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001483 RID: 5251
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001484 RID: 5252
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x04001485 RID: 5253
	[MyCmpReq]
	public Building building;

	// Token: 0x04001486 RID: 5254
	[MyCmpGet]
	private EnergyConsumer consumer;

	// Token: 0x04001487 RID: 5255
	[MyCmpAdd]
	private LoopingSounds loopingSounds;

	// Token: 0x04001488 RID: 5256
	public Orientation verticalOrientation;

	// Token: 0x04001489 RID: 5257
	[SerializeField]
	public bool hasComplexUserControls;

	// Token: 0x0400148A RID: 5258
	[SerializeField]
	public float unpoweredAnimSpeed = 0.25f;

	// Token: 0x0400148B RID: 5259
	[SerializeField]
	public float poweredAnimSpeed = 1f;

	// Token: 0x0400148C RID: 5260
	[SerializeField]
	public Door.DoorType doorType;

	// Token: 0x0400148D RID: 5261
	[SerializeField]
	public bool allowAutoControl = true;

	// Token: 0x0400148E RID: 5262
	[SerializeField]
	public string doorClosingSoundEventName;

	// Token: 0x0400148F RID: 5263
	[SerializeField]
	public string doorOpeningSoundEventName;

	// Token: 0x04001490 RID: 5264
	private string doorClosingSound;

	// Token: 0x04001491 RID: 5265
	private string doorOpeningSound;

	// Token: 0x04001492 RID: 5266
	private static readonly HashedString SOUND_POWERED_PARAMETER = "doorPowered";

	// Token: 0x04001493 RID: 5267
	private static readonly HashedString SOUND_PROGRESS_PARAMETER = "doorProgress";

	// Token: 0x04001494 RID: 5268
	[Serialize]
	private bool hasBeenUnsealed;

	// Token: 0x04001495 RID: 5269
	[Serialize]
	private Door.ControlState controlState;

	// Token: 0x04001496 RID: 5270
	private bool on;

	// Token: 0x04001497 RID: 5271
	private bool do_melt_check;

	// Token: 0x04001498 RID: 5272
	private int openCount;

	// Token: 0x04001499 RID: 5273
	private Door.ControlState requestedState;

	// Token: 0x0400149A RID: 5274
	private Chore changeStateChore;

	// Token: 0x0400149B RID: 5275
	private Door.Controller.Instance controller;

	// Token: 0x0400149C RID: 5276
	private LoggerFSS log;

	// Token: 0x0400149D RID: 5277
	private const float REFRESH_HACK_DELAY = 1f;

	// Token: 0x0400149E RID: 5278
	private bool doorOpenLiquidRefreshHack;

	// Token: 0x0400149F RID: 5279
	private float doorOpenLiquidRefreshTime;

	// Token: 0x040014A0 RID: 5280
	private static readonly EventSystem.IntraObjectHandler<Door> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Door>(delegate(Door component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040014A1 RID: 5281
	public static readonly HashedString OPEN_CLOSE_PORT_ID = new HashedString("DoorOpenClose");

	// Token: 0x040014A2 RID: 5282
	private static readonly KAnimFile[] OVERRIDE_ANIMS = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };

	// Token: 0x040014A3 RID: 5283
	private static readonly EventSystem.IntraObjectHandler<Door> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Door>(delegate(Door component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x040014A4 RID: 5284
	private static readonly EventSystem.IntraObjectHandler<Door> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<Door>(delegate(Door component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040014A5 RID: 5285
	private bool applyLogicChange;

	// Token: 0x020011E1 RID: 4577
	public enum DoorType
	{
		// Token: 0x04005C4E RID: 23630
		Pressure,
		// Token: 0x04005C4F RID: 23631
		ManualPressure,
		// Token: 0x04005C50 RID: 23632
		Internal,
		// Token: 0x04005C51 RID: 23633
		Sealed
	}

	// Token: 0x020011E2 RID: 4578
	public enum ControlState
	{
		// Token: 0x04005C53 RID: 23635
		Auto,
		// Token: 0x04005C54 RID: 23636
		Opened,
		// Token: 0x04005C55 RID: 23637
		Locked,
		// Token: 0x04005C56 RID: 23638
		NumStates
	}

	// Token: 0x020011E3 RID: 4579
	public class Controller : GameStateMachine<Door.Controller, Door.Controller.Instance, Door>
	{
		// Token: 0x06007856 RID: 30806 RVA: 0x002BE3F8 File Offset: 0x002BC5F8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.closed;
			this.root.Update("RefreshIsBlocked", delegate(Door.Controller.Instance smi, float dt)
			{
				smi.RefreshIsBlocked();
			}, UpdateRate.SIM_200ms, false).ParamTransition<bool>(this.isSealed, this.Sealed.closed, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue);
			this.closeblocked.PlayAnim("open").ParamTransition<bool>(this.isOpen, this.open, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue).ParamTransition<bool>(this.isBlocked, this.closedelay, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsFalse);
			this.closedelay.PlayAnim("open").ScheduleGoTo(0.5f, this.closing).ParamTransition<bool>(this.isOpen, this.open, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue)
				.ParamTransition<bool>(this.isBlocked, this.closeblocked, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue);
			this.closing.ParamTransition<bool>(this.isBlocked, this.closeblocked, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue).ToggleTag(GameTags.Transition).ToggleLoopingSound("Closing loop", (Door.Controller.Instance smi) => smi.master.doorClosingSound, (Door.Controller.Instance smi) => !string.IsNullOrEmpty(smi.master.doorClosingSound))
				.Enter("SetParams", delegate(Door.Controller.Instance smi)
				{
					smi.master.UpdateAnimAndSoundParams(smi.master.on);
				})
				.Update(delegate(Door.Controller.Instance smi, float dt)
				{
					if (smi.master.doorClosingSound != null)
					{
						smi.master.loopingSounds.UpdateSecondParameter(smi.master.doorClosingSound, Door.SOUND_PROGRESS_PARAMETER, smi.Get<KBatchedAnimController>().GetPositionPercent());
					}
				}, UpdateRate.SIM_33ms, false)
				.Enter("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(true);
				})
				.Exit("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(false);
				})
				.PlayAnim("closing")
				.OnAnimQueueComplete(this.closed);
			this.open.PlayAnim("open").ParamTransition<bool>(this.isOpen, this.closeblocked, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsFalse).Enter("SetWorldStateOpen", delegate(Door.Controller.Instance smi)
			{
				smi.master.SetWorldState();
			});
			this.closed.PlayAnim("closed").ParamTransition<bool>(this.isOpen, this.opening, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue).ParamTransition<bool>(this.isLocked, this.locking, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue)
				.Enter("SetWorldStateClosed", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetWorldState();
				});
			this.locking.PlayAnim("locked_pre").OnAnimQueueComplete(this.locked).Enter("SetWorldStateClosed", delegate(Door.Controller.Instance smi)
			{
				smi.master.SetWorldState();
			});
			this.locked.PlayAnim("locked").ParamTransition<bool>(this.isLocked, this.unlocking, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsFalse);
			this.unlocking.PlayAnim("locked_pst").OnAnimQueueComplete(this.closed);
			this.opening.ToggleTag(GameTags.Transition).ToggleLoopingSound("Opening loop", (Door.Controller.Instance smi) => smi.master.doorOpeningSound, (Door.Controller.Instance smi) => !string.IsNullOrEmpty(smi.master.doorOpeningSound)).Enter("SetParams", delegate(Door.Controller.Instance smi)
			{
				smi.master.UpdateAnimAndSoundParams(smi.master.on);
			})
				.Update(delegate(Door.Controller.Instance smi, float dt)
				{
					if (smi.master.doorOpeningSound != null)
					{
						smi.master.loopingSounds.UpdateSecondParameter(smi.master.doorOpeningSound, Door.SOUND_PROGRESS_PARAMETER, smi.Get<KBatchedAnimController>().GetPositionPercent());
					}
				}, UpdateRate.SIM_33ms, false)
				.Enter("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(true);
				})
				.Exit("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(false);
				})
				.PlayAnim("opening")
				.OnAnimQueueComplete(this.open);
			this.Sealed.Enter(delegate(Door.Controller.Instance smi)
			{
				OccupyArea component = smi.master.GetComponent<OccupyArea>();
				for (int i = 0; i < component.OccupiedCellsOffsets.Length; i++)
				{
					Grid.PreventFogOfWarReveal[Grid.OffsetCell(Grid.PosToCell(smi.master.gameObject), component.OccupiedCellsOffsets[i])] = false;
				}
				smi.sm.isLocked.Set(true, smi, false);
				smi.master.controlState = Door.ControlState.Locked;
				smi.master.RefreshControlState();
				if (smi.master.GetComponent<Unsealable>().facingRight)
				{
					smi.master.GetComponent<KBatchedAnimController>().FlipX = true;
				}
			}).Enter("SetWorldStateClosed", delegate(Door.Controller.Instance smi)
			{
				smi.master.SetWorldState();
			}).Exit(delegate(Door.Controller.Instance smi)
			{
				smi.sm.isLocked.Set(false, smi, false);
				smi.master.GetComponent<AccessControl>().controlEnabled = true;
				smi.master.controlState = Door.ControlState.Opened;
				smi.master.RefreshControlState();
				smi.sm.isOpen.Set(true, smi, false);
				smi.sm.isLocked.Set(false, smi, false);
				smi.sm.isSealed.Set(false, smi, false);
			});
			this.Sealed.closed.PlayAnim("sealed", KAnim.PlayMode.Once);
			this.Sealed.awaiting_unlock.ToggleChore((Door.Controller.Instance smi) => this.CreateUnsealChore(smi, true), this.Sealed.chore_pst);
			this.Sealed.chore_pst.Enter(delegate(Door.Controller.Instance smi)
			{
				smi.master.hasBeenUnsealed = true;
				if (smi.master.GetComponent<Unsealable>().unsealed)
				{
					smi.GoTo(this.opening);
					FogOfWarMask.ClearMask(Grid.CellRight(Grid.PosToCell(smi.master.gameObject)));
					FogOfWarMask.ClearMask(Grid.CellLeft(Grid.PosToCell(smi.master.gameObject)));
					return;
				}
				smi.GoTo(this.Sealed.closed);
			});
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x002BE934 File Offset: 0x002BCB34
		private Chore CreateUnsealChore(Door.Controller.Instance smi, bool approach_right)
		{
			return new WorkChore<Unsealable>(Db.Get().ChoreTypes.Toggle, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04005C57 RID: 23639
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State open;

		// Token: 0x04005C58 RID: 23640
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State opening;

		// Token: 0x04005C59 RID: 23641
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closed;

		// Token: 0x04005C5A RID: 23642
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closing;

		// Token: 0x04005C5B RID: 23643
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closedelay;

		// Token: 0x04005C5C RID: 23644
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closeblocked;

		// Token: 0x04005C5D RID: 23645
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State locking;

		// Token: 0x04005C5E RID: 23646
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State locked;

		// Token: 0x04005C5F RID: 23647
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State unlocking;

		// Token: 0x04005C60 RID: 23648
		public Door.Controller.SealedStates Sealed;

		// Token: 0x04005C61 RID: 23649
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isOpen;

		// Token: 0x04005C62 RID: 23650
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isLocked;

		// Token: 0x04005C63 RID: 23651
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isBlocked;

		// Token: 0x04005C64 RID: 23652
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isSealed;

		// Token: 0x04005C65 RID: 23653
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter sealDirectionRight;

		// Token: 0x02001F9D RID: 8093
		public class SealedStates : GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State
		{
			// Token: 0x04008CC0 RID: 36032
			public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closed;

			// Token: 0x04008CC1 RID: 36033
			public Door.Controller.SealedStates.AwaitingUnlock awaiting_unlock;

			// Token: 0x04008CC2 RID: 36034
			public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State chore_pst;

			// Token: 0x02002DAB RID: 11691
			public class AwaitingUnlock : GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State
			{
				// Token: 0x0400BA46 RID: 47686
				public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State awaiting_arrival;

				// Token: 0x0400BA47 RID: 47687
				public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State unlocking;
			}
		}

		// Token: 0x02001F9E RID: 8094
		public new class Instance : GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.GameInstance
		{
			// Token: 0x06009FDC RID: 40924 RVA: 0x00340ECD File Offset: 0x0033F0CD
			public Instance(Door door)
				: base(door)
			{
			}

			// Token: 0x06009FDD RID: 40925 RVA: 0x00340ED8 File Offset: 0x0033F0D8
			public void RefreshIsBlocked()
			{
				bool flag = false;
				foreach (int num in base.master.GetComponent<Building>().PlacementCells)
				{
					if (Grid.Objects[num, 40] != null)
					{
						flag = true;
						break;
					}
				}
				base.sm.isBlocked.Set(flag, base.smi, false);
			}
		}
	}
}
