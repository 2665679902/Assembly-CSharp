using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using FMOD.Studio;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020004B7 RID: 1207
[AddComponentMenu("KMonoBehaviour/Workable/Pickupable")]
public class Pickupable : Workable, IHasSortOrder
{
	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06001B94 RID: 7060 RVA: 0x00092389 File Offset: 0x00090589
	public PrimaryElement PrimaryElement
	{
		get
		{
			return this.primaryElement;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00092391 File Offset: 0x00090591
	// (set) Token: 0x06001B96 RID: 7062 RVA: 0x00092399 File Offset: 0x00090599
	public int sortOrder
	{
		get
		{
			return this._sortOrder;
		}
		set
		{
			this._sortOrder = value;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000923A2 File Offset: 0x000905A2
	// (set) Token: 0x06001B98 RID: 7064 RVA: 0x000923AA File Offset: 0x000905AA
	public Storage storage { get; set; }

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000923B3 File Offset: 0x000905B3
	public float MinTakeAmount
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000923BA File Offset: 0x000905BA
	// (set) Token: 0x06001B9B RID: 7067 RVA: 0x000923C2 File Offset: 0x000905C2
	public bool prevent_absorb_until_stored { get; set; }

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000923CB File Offset: 0x000905CB
	// (set) Token: 0x06001B9D RID: 7069 RVA: 0x000923D3 File Offset: 0x000905D3
	public bool isKinematic { get; set; }

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000923DC File Offset: 0x000905DC
	// (set) Token: 0x06001B9F RID: 7071 RVA: 0x000923E4 File Offset: 0x000905E4
	public bool wasAbsorbed { get; private set; }

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x000923ED File Offset: 0x000905ED
	// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x000923F5 File Offset: 0x000905F5
	public int cachedCell { get; private set; }

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000923FE File Offset: 0x000905FE
	// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00092408 File Offset: 0x00090608
	public bool IsEntombed
	{
		get
		{
			return this.isEntombed;
		}
		set
		{
			if (value != this.isEntombed)
			{
				this.isEntombed = value;
				if (this.isEntombed)
				{
					base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
				}
				else
				{
					base.GetComponent<KPrefabID>().RemoveTag(GameTags.Entombed);
				}
				base.Trigger(-1089732772, null);
				this.UpdateEntombedVisualizer();
			}
		}
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x00092462 File Offset: 0x00090662
	private bool CouldBePickedUpCommon(GameObject carrier)
	{
		return this.UnreservedAmount >= this.MinTakeAmount && (this.UnreservedAmount > 0f || this.FindReservedAmount(carrier) > 0f);
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x00092494 File Offset: 0x00090694
	public bool CouldBePickedUpByMinion(GameObject carrier)
	{
		return this.CouldBePickedUpCommon(carrier) && (this.storage == null || !this.storage.automatable || !this.storage.automatable.GetAutomationOnly());
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x000924E1 File Offset: 0x000906E1
	public bool CouldBePickedUpByTransferArm(GameObject carrier)
	{
		return this.CouldBePickedUpCommon(carrier);
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x000924EC File Offset: 0x000906EC
	public float FindReservedAmount(GameObject reserver)
	{
		for (int i = 0; i < this.reservations.Count; i++)
		{
			if (this.reservations[i].reserver == reserver)
			{
				return this.reservations[i].amount;
			}
		}
		return 0f;
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0009253F File Offset: 0x0009073F
	public float UnreservedAmount
	{
		get
		{
			return this.TotalAmount - this.ReservedAmount;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x0009254E File Offset: 0x0009074E
	// (set) Token: 0x06001BAA RID: 7082 RVA: 0x00092556 File Offset: 0x00090756
	public float ReservedAmount { get; private set; }

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0009255F File Offset: 0x0009075F
	// (set) Token: 0x06001BAC RID: 7084 RVA: 0x0009256C File Offset: 0x0009076C
	public float TotalAmount
	{
		get
		{
			return this.primaryElement.Units;
		}
		set
		{
			DebugUtil.Assert(this.primaryElement != null);
			this.primaryElement.Units = value;
			if (value < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT && !this.primaryElement.KeepZeroMassObject)
			{
				base.gameObject.DeleteObject();
			}
			this.NotifyChanged(Grid.PosToCell(this));
		}
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x000925C4 File Offset: 0x000907C4
	private void RefreshReservedAmount()
	{
		this.ReservedAmount = 0f;
		for (int i = 0; i < this.reservations.Count; i++)
		{
			this.ReservedAmount += this.reservations[i].amount;
		}
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x00092610 File Offset: 0x00090810
	[Conditional("UNITY_EDITOR")]
	private void Log(string evt, string param, float value)
	{
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x00092612 File Offset: 0x00090812
	public void ClearReservations()
	{
		this.reservations.Clear();
		this.RefreshReservedAmount();
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x00092628 File Offset: 0x00090828
	[ContextMenu("Print Reservations")]
	public void PrintReservations()
	{
		foreach (Pickupable.Reservation reservation in this.reservations)
		{
			global::Debug.Log(reservation.ToString());
		}
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00092688 File Offset: 0x00090888
	public int Reserve(string context, GameObject reserver, float amount)
	{
		int num = this.nextTicketNumber;
		this.nextTicketNumber = num + 1;
		int num2 = num;
		Pickupable.Reservation reservation = new Pickupable.Reservation(reserver, amount, num2);
		this.reservations.Add(reservation);
		this.RefreshReservedAmount();
		if (this.OnReservationsChanged != null)
		{
			this.OnReservationsChanged();
		}
		return num2;
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000926D8 File Offset: 0x000908D8
	public void Unreserve(string context, int ticket)
	{
		int i = 0;
		while (i < this.reservations.Count)
		{
			if (this.reservations[i].ticket == ticket)
			{
				this.reservations.RemoveAt(i);
				this.RefreshReservedAmount();
				if (this.OnReservationsChanged != null)
				{
					this.OnReservationsChanged();
					return;
				}
				break;
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x00092738 File Offset: 0x00090938
	private Pickupable()
	{
		this.showProgressBar = false;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.shouldTransferDiseaseWithWorker = false;
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000927B0 File Offset: 0x000909B0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.log = new LoggerFSSF("Pickupable");
		this.workerStatusItem = Db.Get().DuplicantStatusItems.PickingUp;
		base.SetWorkTime(1.5f);
		this.targetWorkable = this;
		this.resetProgressOnStop = true;
		base.gameObject.layer = Game.PickupableLayer;
		Vector3 position = base.transform.GetPosition();
		this.UpdateCachedCell(Grid.PosToCell(position));
		base.Subscribe<Pickupable>(856640610, Pickupable.OnStoreDelegate);
		base.Subscribe<Pickupable>(1188683690, Pickupable.OnLandedDelegate);
		base.Subscribe<Pickupable>(1807976145, Pickupable.OnOreSizeChangedDelegate);
		base.Subscribe<Pickupable>(-1432940121, Pickupable.OnReachableChangedDelegate);
		base.Subscribe<Pickupable>(-778359855, Pickupable.RefreshStorageTagsDelegate);
		this.KPrefabID.AddTag(GameTags.Pickupable, false);
		Components.Pickupables.Add(this);
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000928A8 File Offset: 0x00090AA8
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x000928B0 File Offset: 0x00090AB0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num) && this.deleteOffGrid)
		{
			base.gameObject.DeleteObject();
			return;
		}
		this.UpdateCachedCell(num);
		new ReachabilityMonitor.Instance(this).StartSM();
		new FetchableMonitor.Instance(this).StartSM();
		base.SetWorkTime(1.5f);
		this.faceTargetWhenWorking = true;
		KSelectable component = base.GetComponent<KSelectable>();
		if (component != null)
		{
			component.SetStatusIndicatorOffset(new Vector3(0f, -0.65f, 0f));
		}
		this.OnTagsChanged(null);
		this.TryToOffsetIfBuried();
		DecorProvider component2 = base.GetComponent<DecorProvider>();
		if (component2 != null && string.IsNullOrEmpty(component2.overrideName))
		{
			component2.overrideName = UI.OVERLAYS.DECOR.CLUTTER;
		}
		this.UpdateEntombedVisualizer();
		base.Subscribe<Pickupable>(-1582839653, Pickupable.OnTagsChangedDelegate);
		this.NotifyChanged(num);
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x00092998 File Offset: 0x00090B98
	[OnDeserialized]
	public void OnDeserialize()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 28) && base.transform.position.z == 0f)
		{
			KBatchedAnimController component = base.transform.GetComponent<KBatchedAnimController>();
			component.SetSceneLayer(component.sceneLayer);
		}
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000929EC File Offset: 0x00090BEC
	public void RegisterListeners()
	{
		if (this.cleaningUp)
		{
			return;
		}
		if (this.solidPartitionerEntry.IsValid())
		{
			return;
		}
		int num = Grid.PosToCell(this);
		this.objectLayerListItem = new ObjectLayerListItem(base.gameObject, ObjectLayer.Pickupables, num);
		this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("Pickupable.RegisterSolidListener", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Pickupable.RegisterPickupable", this, num, GameScenePartitioner.Instance.pickupablesLayer, null);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "Pickupable.OnCellChange");
		Singleton<CellChangeMonitor>.Instance.MarkDirty(base.transform);
		Singleton<CellChangeMonitor>.Instance.ClearLastKnownCell(base.transform);
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00092AC0 File Offset: 0x00090CC0
	public void UnregisterListeners()
	{
		if (this.objectLayerListItem != null)
		{
			this.objectLayerListItem.Clear();
			this.objectLayerListItem = null;
		}
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x00092B23 File Offset: 0x00090D23
	private void OnSolidChanged(object data)
	{
		this.TryToOffsetIfBuried();
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00092B2C File Offset: 0x00090D2C
	public void TryToOffsetIfBuried()
	{
		if (this.KPrefabID.HasTag(GameTags.Stored) || this.KPrefabID.HasTag(GameTags.Equipped))
		{
			return;
		}
		int num = Grid.PosToCell(this);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		DeathMonitor.Instance smi = base.gameObject.GetSMI<DeathMonitor.Instance>();
		if ((smi == null || smi.IsDead()) && ((Grid.Solid[num] && Grid.Foundation[num]) || Grid.Properties[num] != 0))
		{
			for (int i = 0; i < Pickupable.displacementOffsets.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, Pickupable.displacementOffsets[i]);
				if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
				{
					Vector3 vector = Grid.CellToPosCBC(num2, Grid.SceneLayer.Move);
					KCollider2D component = base.GetComponent<KCollider2D>();
					if (component != null)
					{
						vector.y += base.transform.GetPosition().y - component.bounds.min.y;
					}
					base.transform.SetPosition(vector);
					num = num2;
					this.RemoveFaller();
					this.AddFaller(Vector2.zero);
					break;
				}
			}
		}
		this.HandleSolidCell(num);
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x00092C6C File Offset: 0x00090E6C
	private bool HandleSolidCell(int cell)
	{
		bool flag = this.IsEntombed;
		bool flag2 = false;
		if (Grid.IsValidCell(cell) && Grid.Solid[cell])
		{
			DeathMonitor.Instance smi = base.gameObject.GetSMI<DeathMonitor.Instance>();
			if (smi == null || smi.IsDead())
			{
				this.Clearable.CancelClearing();
				flag2 = true;
			}
		}
		if (flag2 != flag && !this.KPrefabID.HasTag(GameTags.Stored))
		{
			this.IsEntombed = flag2;
			base.GetComponent<KSelectable>().IsSelectable = !this.IsEntombed;
		}
		this.UpdateEntombedVisualizer();
		return this.IsEntombed;
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x00092CFC File Offset: 0x00090EFC
	private void OnCellChange()
	{
		Vector3 position = base.transform.GetPosition();
		int num = Grid.PosToCell(position);
		if (!Grid.IsValidCell(num))
		{
			Vector2 vector = new Vector2(-0.1f * (float)Grid.WidthInCells, 1.1f * (float)Grid.WidthInCells);
			Vector2 vector2 = new Vector2(-0.1f * (float)Grid.HeightInCells, 1.1f * (float)Grid.HeightInCells);
			if (this.deleteOffGrid && (position.x < vector.x || vector.y < position.x || position.y < vector2.x || vector2.y < position.y))
			{
				this.DeleteObject();
				return;
			}
		}
		else
		{
			this.ReleaseEntombedVisualizerAndAddFaller(true);
			if (this.HandleSolidCell(num))
			{
				return;
			}
			this.objectLayerListItem.Update(num);
			bool flag = false;
			if (this.absorbable && !this.KPrefabID.HasTag(GameTags.Stored))
			{
				int num2 = Grid.CellBelow(num);
				if (Grid.IsValidCell(num2) && Grid.Solid[num2])
				{
					ObjectLayerListItem objectLayerListItem = this.objectLayerListItem.nextItem;
					while (objectLayerListItem != null)
					{
						GameObject gameObject = objectLayerListItem.gameObject;
						objectLayerListItem = objectLayerListItem.nextItem;
						Pickupable component = gameObject.GetComponent<Pickupable>();
						if (component != null)
						{
							flag = component.TryAbsorb(this, false, false);
							if (flag)
							{
								break;
							}
						}
					}
				}
			}
			GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, num);
			GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, num);
			int cachedCell = this.cachedCell;
			this.UpdateCachedCell(num);
			if (!flag)
			{
				this.NotifyChanged(num);
			}
			if (Grid.IsValidCell(cachedCell) && num != cachedCell)
			{
				this.NotifyChanged(cachedCell);
			}
		}
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x00092EA8 File Offset: 0x000910A8
	private void OnTagsChanged(object data)
	{
		if (!this.KPrefabID.HasTag(GameTags.Stored) && !this.KPrefabID.HasTag(GameTags.Equipped))
		{
			this.RegisterListeners();
			this.AddFaller(Vector2.zero);
			return;
		}
		this.UnregisterListeners();
		this.RemoveFaller();
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x00092EF7 File Offset: 0x000910F7
	private void NotifyChanged(int new_cell)
	{
		GameScenePartitioner.Instance.TriggerEvent(new_cell, GameScenePartitioner.Instance.pickupablesChangedLayer, this);
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x00092F10 File Offset: 0x00091110
	public bool TryAbsorb(Pickupable other, bool hide_effects, bool allow_cross_storage = false)
	{
		if (other == null)
		{
			return false;
		}
		if (other.wasAbsorbed)
		{
			return false;
		}
		if (this.wasAbsorbed)
		{
			return false;
		}
		if (!other.CanAbsorb(this))
		{
			return false;
		}
		if (this.prevent_absorb_until_stored)
		{
			return false;
		}
		if (!allow_cross_storage && this.storage == null != (other.storage == null))
		{
			return false;
		}
		this.Absorb(other);
		if (!hide_effects && EffectPrefabs.Instance != null && !this.storage)
		{
			Vector3 position = base.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Front);
			global::Util.KInstantiate(Assets.GetPrefab(EffectConfigs.OreAbsorbId), position, Quaternion.identity, null, null, true, 0).SetActive(true);
		}
		return true;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x00092FD8 File Offset: 0x000911D8
	protected override void OnCleanUp()
	{
		this.cleaningUp = true;
		this.ReleaseEntombedVisualizerAndAddFaller(false);
		this.RemoveFaller();
		if (this.storage)
		{
			this.storage.Remove(base.gameObject, true);
		}
		this.UnregisterListeners();
		Components.Pickupables.Remove(this);
		if (this.reservations.Count > 0)
		{
			this.reservations.Clear();
			if (this.OnReservationsChanged != null)
			{
				this.OnReservationsChanged();
			}
		}
		if (Grid.IsValidCell(this.cachedCell))
		{
			this.NotifyChanged(this.cachedCell);
		}
		base.OnCleanUp();
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x00093074 File Offset: 0x00091274
	public Pickupable Take(float amount)
	{
		if (amount <= 0f)
		{
			return null;
		}
		if (this.OnTake == null)
		{
			if (this.storage != null)
			{
				this.storage.Remove(base.gameObject, true);
			}
			return this;
		}
		if (amount >= this.TotalAmount && this.storage != null && !this.primaryElement.KeepZeroMassObject)
		{
			this.storage.Remove(base.gameObject, true);
		}
		float num = Math.Min(this.TotalAmount, amount);
		if (num <= 0f)
		{
			return null;
		}
		return this.OnTake(num);
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x00093110 File Offset: 0x00091310
	private void Absorb(Pickupable pickupable)
	{
		global::Debug.Assert(!this.wasAbsorbed);
		global::Debug.Assert(!pickupable.wasAbsorbed);
		base.Trigger(-2064133523, pickupable);
		pickupable.Trigger(-1940207677, base.gameObject);
		pickupable.wasAbsorbed = true;
		KSelectable component = base.GetComponent<KSelectable>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == pickupable.GetComponent<KSelectable>())
		{
			SelectTool.Instance.Select(component, false);
		}
		pickupable.gameObject.DeleteObject();
		this.NotifyChanged(Grid.PosToCell(this));
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000931C0 File Offset: 0x000913C0
	private void RefreshStorageTags(object data = null)
	{
		bool flag = data is Storage || (data != null && (bool)data);
		if (flag && data is Storage && ((Storage)data).gameObject == base.gameObject)
		{
			return;
		}
		if (!flag)
		{
			this.KPrefabID.RemoveTag(GameTags.Stored);
			this.KPrefabID.RemoveTag(GameTags.StoredPrivate);
			return;
		}
		this.KPrefabID.AddTag(GameTags.Stored, false);
		if (this.storage == null || !this.storage.allowItemRemoval)
		{
			this.KPrefabID.AddTag(GameTags.StoredPrivate, false);
			return;
		}
		this.KPrefabID.RemoveTag(GameTags.StoredPrivate);
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x0009327C File Offset: 0x0009147C
	public void OnStore(object data)
	{
		this.storage = data as Storage;
		bool flag = data is Storage || (data != null && (bool)data);
		SaveLoadRoot component = base.GetComponent<SaveLoadRoot>();
		if (this.carryAnimOverride != null && this.lastCarrier != null)
		{
			this.lastCarrier.RemoveAnimOverrides(this.carryAnimOverride);
			this.lastCarrier = null;
		}
		KSelectable component2 = base.GetComponent<KSelectable>();
		if (component2)
		{
			component2.IsSelectable = !flag;
		}
		if (flag)
		{
			int cachedCell = this.cachedCell;
			this.RefreshStorageTags(data);
			if (this.storage != null)
			{
				if (this.carryAnimOverride != null && this.storage.GetComponent<Navigator>() != null)
				{
					this.lastCarrier = this.storage.GetComponent<KBatchedAnimController>();
					if (this.lastCarrier != null)
					{
						this.lastCarrier.AddAnimOverrides(this.carryAnimOverride, 0f);
					}
				}
				this.UpdateCachedCell(Grid.PosToCell(this.storage));
			}
			this.NotifyChanged(cachedCell);
			if (component != null)
			{
				component.SetRegistered(false);
				return;
			}
		}
		else
		{
			if (component != null)
			{
				component.SetRegistered(true);
			}
			this.RemovedFromStorage();
		}
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000933B4 File Offset: 0x000915B4
	private void RemovedFromStorage()
	{
		this.storage = null;
		this.UpdateCachedCell(Grid.PosToCell(this));
		this.RefreshStorageTags(null);
		this.AddFaller(Vector2.zero);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		component.enabled = true;
		base.gameObject.transform.rotation = Quaternion.identity;
		this.RegisterListeners();
		component.GetBatchInstanceData().ClearOverrideTransformMatrix();
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x00093418 File Offset: 0x00091618
	public void UpdateCachedCellFromStoragePosition()
	{
		global::Debug.Assert(this.storage != null, "Only call UpdateCachedCellFromStoragePosition on pickupables in storage!");
		this.UpdateCachedCell(Grid.PosToCell(this.storage));
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x00093441 File Offset: 0x00091641
	private void UpdateCachedCell(int cell)
	{
		this.cachedCell = cell;
		this.GetOffsets(this.cachedCell);
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x00093458 File Offset: 0x00091658
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		if (this.useGunforPickup && worker.usesMultiTool)
		{
			Workable.AnimInfo anim = base.GetAnim(worker);
			anim.smi = new MultitoolController.Instance(this, worker, "pickup", Assets.GetPrefab(EffectConfigs.OreAbsorbId));
			return anim;
		}
		return base.GetAnim(worker);
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000934B0 File Offset: 0x000916B0
	protected override void OnCompleteWork(Worker worker)
	{
		Storage component = worker.GetComponent<Storage>();
		Pickupable.PickupableStartWorkInfo pickupableStartWorkInfo = (Pickupable.PickupableStartWorkInfo)worker.startWorkInfo;
		float amount = pickupableStartWorkInfo.amount;
		if (!(this != null))
		{
			pickupableStartWorkInfo.setResultCb(null);
			return;
		}
		Pickupable pickupable = this.Take(amount);
		if (pickupable != null)
		{
			component.Store(pickupable.gameObject, false, false, true, false);
			worker.workCompleteData = pickupable;
			pickupableStartWorkInfo.setResultCb(pickupable.gameObject);
			return;
		}
		pickupableStartWorkInfo.setResultCb(null);
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x0009353B File Offset: 0x0009173B
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x0009353E File Offset: 0x0009173E
	public override Vector3 GetTargetPoint()
	{
		return base.transform.GetPosition();
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x0009354B File Offset: 0x0009174B
	public bool IsReachable()
	{
		return this.isReachable;
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x00093554 File Offset: 0x00091754
	private void OnReachableChanged(object data)
	{
		this.isReachable = (bool)data;
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.isReachable)
		{
			component.RemoveStatusItem(Db.Get().MiscStatusItems.PickupableUnreachable, false);
			return;
		}
		component.AddStatusItem(Db.Get().MiscStatusItems.PickupableUnreachable, this);
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x000935AB File Offset: 0x000917AB
	private void AddFaller(Vector2 initial_velocity)
	{
		if (base.GetComponent<Health>() != null)
		{
			return;
		}
		if (!GameComps.Fallers.Has(base.gameObject))
		{
			GameComps.Fallers.Add(base.gameObject, initial_velocity);
		}
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000935E0 File Offset: 0x000917E0
	private void RemoveFaller()
	{
		if (base.GetComponent<Health>() != null)
		{
			return;
		}
		if (GameComps.Fallers.Has(base.gameObject))
		{
			GameComps.Fallers.Remove(base.gameObject);
		}
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x00093614 File Offset: 0x00091814
	private void OnOreSizeChanged(object data)
	{
		Vector3 vector = Vector3.zero;
		HandleVector<int>.Handle handle = GameComps.Gravities.GetHandle(base.gameObject);
		if (handle.IsValid())
		{
			vector = GameComps.Gravities.GetData(handle).velocity;
		}
		this.RemoveFaller();
		if (!this.KPrefabID.HasTag(GameTags.Stored))
		{
			this.AddFaller(vector);
		}
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x0009367C File Offset: 0x0009187C
	private void OnLanded(object data)
	{
		if (CameraController.Instance == null)
		{
			return;
		}
		Vector3 position = base.transform.GetPosition();
		Vector2I vector2I = Grid.PosToXY(position);
		if (vector2I.x < 0 || Grid.WidthInCells <= vector2I.x || vector2I.y < 0 || Grid.HeightInCells <= vector2I.y)
		{
			this.DeleteObject();
			return;
		}
		Vector2 vector = (Vector2)data;
		if (vector.sqrMagnitude <= 0.2f || SpeedControlScreen.Instance.IsPaused)
		{
			return;
		}
		Element element = this.primaryElement.Element;
		if (element.substance != null)
		{
			string text = element.substance.GetOreBumpSound();
			if (text == null)
			{
				if (element.HasTag(GameTags.RefinedMetal))
				{
					text = "RefinedMetal";
				}
				else if (element.HasTag(GameTags.Metal))
				{
					text = "RawMetal";
				}
				else
				{
					text = "Rock";
				}
			}
			if (element.tag.ToString() == "Creature" && !base.gameObject.HasTag(GameTags.Seed))
			{
				text = "Bodyfall_rock";
			}
			else
			{
				text = "Ore_bump_" + text;
			}
			string text2 = GlobalAssets.GetSound(text, true);
			text2 = ((text2 != null) ? text2 : GlobalAssets.GetSound("Ore_bump_rock", false));
			if (CameraController.Instance.IsAudibleSound(base.transform.GetPosition(), text2))
			{
				int num = Grid.PosToCell(position);
				bool isLiquid = Grid.Element[num].IsLiquid;
				float num2 = 0f;
				if (isLiquid)
				{
					num2 = SoundUtil.GetLiquidDepth(num);
				}
				FMOD.Studio.EventInstance eventInstance = KFMOD.BeginOneShot(text2, CameraController.Instance.GetVerticallyScaledPosition(base.transform.GetPosition(), false), 1f);
				eventInstance.setParameterByName("velocity", vector.magnitude, false);
				eventInstance.setParameterByName("liquidDepth", num2, false);
				KFMOD.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x00093858 File Offset: 0x00091A58
	private void UpdateEntombedVisualizer()
	{
		if (this.IsEntombed)
		{
			if (this.entombedCell == -1)
			{
				int num = Grid.PosToCell(this);
				if (EntombedItemManager.CanEntomb(this))
				{
					SaveGame.Instance.entombedItemManager.Add(this);
				}
				if (Grid.Objects[num, 1] == null)
				{
					KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
					if (component != null && Game.Instance.GetComponent<EntombedItemVisualizer>().AddItem(num))
					{
						this.entombedCell = num;
						component.enabled = false;
						this.RemoveFaller();
						return;
					}
				}
			}
		}
		else
		{
			this.ReleaseEntombedVisualizerAndAddFaller(true);
		}
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000938E8 File Offset: 0x00091AE8
	private void ReleaseEntombedVisualizerAndAddFaller(bool add_faller_if_necessary)
	{
		if (this.entombedCell != -1)
		{
			Game.Instance.GetComponent<EntombedItemVisualizer>().RemoveItem(this.entombedCell);
			this.entombedCell = -1;
			base.GetComponent<KBatchedAnimController>().enabled = true;
			if (add_faller_if_necessary)
			{
				this.AddFaller(Vector2.zero);
			}
		}
	}

	// Token: 0x04000F60 RID: 3936
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04000F61 RID: 3937
	public const float WorkTime = 1.5f;

	// Token: 0x04000F62 RID: 3938
	[SerializeField]
	private int _sortOrder;

	// Token: 0x04000F64 RID: 3940
	[MyCmpReq]
	[NonSerialized]
	public KPrefabID KPrefabID;

	// Token: 0x04000F65 RID: 3941
	[MyCmpAdd]
	[NonSerialized]
	public Clearable Clearable;

	// Token: 0x04000F66 RID: 3942
	[MyCmpAdd]
	[NonSerialized]
	public Prioritizable prioritizable;

	// Token: 0x04000F67 RID: 3943
	public bool absorbable;

	// Token: 0x04000F69 RID: 3945
	public Func<Pickupable, bool> CanAbsorb = (Pickupable other) => false;

	// Token: 0x04000F6A RID: 3946
	public Func<float, Pickupable> OnTake;

	// Token: 0x04000F6B RID: 3947
	public System.Action OnReservationsChanged;

	// Token: 0x04000F6C RID: 3948
	public ObjectLayerListItem objectLayerListItem;

	// Token: 0x04000F6D RID: 3949
	public Workable targetWorkable;

	// Token: 0x04000F6E RID: 3950
	public KAnimFile carryAnimOverride;

	// Token: 0x04000F6F RID: 3951
	private KBatchedAnimController lastCarrier;

	// Token: 0x04000F70 RID: 3952
	public bool useGunforPickup = true;

	// Token: 0x04000F72 RID: 3954
	private static CellOffset[] displacementOffsets = new CellOffset[]
	{
		new CellOffset(0, 1),
		new CellOffset(0, -1),
		new CellOffset(1, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 1),
		new CellOffset(1, -1),
		new CellOffset(-1, 1),
		new CellOffset(-1, -1)
	};

	// Token: 0x04000F73 RID: 3955
	private bool isReachable;

	// Token: 0x04000F74 RID: 3956
	private bool isEntombed;

	// Token: 0x04000F75 RID: 3957
	private bool cleaningUp;

	// Token: 0x04000F77 RID: 3959
	public bool trackOnPickup = true;

	// Token: 0x04000F79 RID: 3961
	private int nextTicketNumber;

	// Token: 0x04000F7A RID: 3962
	[Serialize]
	public bool deleteOffGrid = true;

	// Token: 0x04000F7B RID: 3963
	private List<Pickupable.Reservation> reservations = new List<Pickupable.Reservation>();

	// Token: 0x04000F7C RID: 3964
	private HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x04000F7D RID: 3965
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000F7E RID: 3966
	private LoggerFSSF log;

	// Token: 0x04000F80 RID: 3968
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnStore(data);
	});

	// Token: 0x04000F81 RID: 3969
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnLandedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnLanded(data);
	});

	// Token: 0x04000F82 RID: 3970
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnOreSizeChangedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnOreSizeChanged(data);
	});

	// Token: 0x04000F83 RID: 3971
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x04000F84 RID: 3972
	private static readonly EventSystem.IntraObjectHandler<Pickupable> RefreshStorageTagsDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.RefreshStorageTags(data);
	});

	// Token: 0x04000F85 RID: 3973
	private static readonly EventSystem.IntraObjectHandler<Pickupable> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Pickupable>(delegate(Pickupable component, object data)
	{
		component.OnTagsChanged(data);
	});

	// Token: 0x04000F86 RID: 3974
	private int entombedCell = -1;

	// Token: 0x020010F2 RID: 4338
	private struct Reservation
	{
		// Token: 0x060074EB RID: 29931 RVA: 0x002B4819 File Offset: 0x002B2A19
		public Reservation(GameObject reserver, float amount, int ticket)
		{
			this.reserver = reserver;
			this.amount = amount;
			this.ticket = ticket;
		}

		// Token: 0x060074EC RID: 29932 RVA: 0x002B4830 File Offset: 0x002B2A30
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.reserver.name,
				", ",
				this.amount.ToString(),
				", ",
				this.ticket.ToString()
			});
		}

		// Token: 0x04005928 RID: 22824
		public GameObject reserver;

		// Token: 0x04005929 RID: 22825
		public float amount;

		// Token: 0x0400592A RID: 22826
		public int ticket;
	}

	// Token: 0x020010F3 RID: 4339
	public class PickupableStartWorkInfo : Worker.StartWorkInfo
	{
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060074ED RID: 29933 RVA: 0x002B4882 File Offset: 0x002B2A82
		// (set) Token: 0x060074EE RID: 29934 RVA: 0x002B488A File Offset: 0x002B2A8A
		public float amount { get; private set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x002B4893 File Offset: 0x002B2A93
		// (set) Token: 0x060074F0 RID: 29936 RVA: 0x002B489B File Offset: 0x002B2A9B
		public Pickupable originalPickupable { get; private set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060074F1 RID: 29937 RVA: 0x002B48A4 File Offset: 0x002B2AA4
		// (set) Token: 0x060074F2 RID: 29938 RVA: 0x002B48AC File Offset: 0x002B2AAC
		public Action<GameObject> setResultCb { get; private set; }

		// Token: 0x060074F3 RID: 29939 RVA: 0x002B48B5 File Offset: 0x002B2AB5
		public PickupableStartWorkInfo(Pickupable pickupable, float amount, Action<GameObject> set_result_cb)
			: base(pickupable.targetWorkable)
		{
			this.originalPickupable = pickupable;
			this.amount = amount;
			this.setResultCb = set_result_cb;
		}
	}
}
