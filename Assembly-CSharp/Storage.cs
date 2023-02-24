using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Storage")]
public class Storage : Workable, ISaveLoadableDetails, IGameObjectEffectDescriptor, IStorage
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00099D27 File Offset: 0x00097F27
	public bool ShouldOnlyTransferFromLowerPriority
	{
		get
		{
			return this.onlyTransferFromLowerPriority || this.allowItemRemoval;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00099D39 File Offset: 0x00097F39
	// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x00099D41 File Offset: 0x00097F41
	public bool allowUIItemRemoval { get; set; }

	// Token: 0x17000140 RID: 320
	public GameObject this[int idx]
	{
		get
		{
			return this.items[idx];
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00099D58 File Offset: 0x00097F58
	public int Count
	{
		get
		{
			return this.items.Count;
		}
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x00099D65 File Offset: 0x00097F65
	public bool ShouldShowInUI()
	{
		return this.showInUI;
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x00099D6D File Offset: 0x00097F6D
	public List<GameObject> GetItems()
	{
		return this.items;
	}

	// Token: 0x06001CE7 RID: 7399 RVA: 0x00099D75 File Offset: 0x00097F75
	public void SetDefaultStoredItemModifiers(List<Storage.StoredItemModifier> modifiers)
	{
		this.defaultStoredItemModifers = modifiers;
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00099D7E File Offset: 0x00097F7E
	public PrioritySetting masterPriority
	{
		get
		{
			if (this.prioritizable)
			{
				return this.prioritizable.GetMasterPriority();
			}
			return Chore.DefaultPrioritySetting;
		}
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x00099DA0 File Offset: 0x00097FA0
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		if (this.useGunForDelivery && worker.usesMultiTool)
		{
			Workable.AnimInfo anim = base.GetAnim(worker);
			anim.smi = new MultitoolController.Instance(this, worker, "store", Assets.GetPrefab(EffectConfigs.OreAbsorbId));
			return anim;
		}
		return base.GetAnim(worker);
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x00099DF8 File Offset: 0x00097FF8
	public override Vector3 GetTargetPoint()
	{
		Vector3 vector = base.GetTargetPoint();
		if (this.useGunForDelivery && this.gunTargetOffset != Vector2.zero)
		{
			if (this.rotatable != null)
			{
				vector += this.rotatable.GetRotatedOffset(this.gunTargetOffset);
			}
			else
			{
				vector += new Vector3(this.gunTargetOffset.x, this.gunTargetOffset.y, 0f);
			}
		}
		return vector;
	}

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06001CEB RID: 7403 RVA: 0x00099E7C File Offset: 0x0009807C
	// (remove) Token: 0x06001CEC RID: 7404 RVA: 0x00099EB4 File Offset: 0x000980B4
	public event System.Action OnStorageIncreased;

	// Token: 0x06001CED RID: 7405 RVA: 0x00099EEC File Offset: 0x000980EC
	protected override void OnPrefabInit()
	{
		if (this.useWideOffsets)
		{
			base.SetOffsetTable(OffsetGroups.InvertedWideTable);
		}
		else
		{
			base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		}
		this.showProgressBar = false;
		this.faceTargetWhenWorking = true;
		base.OnPrefabInit();
		GameUtil.SubscribeToTags<Storage>(this, Storage.OnDeadTagAddedDelegate, true);
		base.Subscribe<Storage>(1502190696, Storage.OnQueueDestroyObjectDelegate);
		base.Subscribe<Storage>(-905833192, Storage.OnCopySettingsDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Storing;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.SetupStorageStatusItems();
	}

	// Token: 0x06001CEE RID: 7406 RVA: 0x00099F94 File Offset: 0x00098194
	private void SetupStorageStatusItems()
	{
		if (Storage.capacityStatusItem == null)
		{
			Storage.capacityStatusItem = new StatusItem("StorageLocker", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Storage.capacityStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				Storage storage = (Storage)data;
				float num = storage.MassStored();
				float num2 = storage.capacityKg;
				if (num > num2 - storage.storageFullMargin && num < num2)
				{
					num = num2;
				}
				else
				{
					num = Mathf.Floor(num);
				}
				string text = Util.FormatWholeNumber(num);
				IUserControlledCapacity component = storage.GetComponent<IUserControlledCapacity>();
				if (component != null)
				{
					num2 = Mathf.Min(component.UserMaxCapacity, num2);
				}
				string text2 = Util.FormatWholeNumber(num2);
				str = str.Replace("{Stored}", text);
				str = str.Replace("{Capacity}", text2);
				if (component != null)
				{
					str = str.Replace("{Units}", component.CapacityUnits);
				}
				else
				{
					str = str.Replace("{Units}", GameUtil.GetCurrentMassUnit(false));
				}
				return str;
			};
		}
		if (this.showCapacityStatusItem)
		{
			if (this.showCapacityAsMainStatus)
			{
				base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Storage.capacityStatusItem, this);
				return;
			}
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Stored, Storage.capacityStatusItem, this);
		}
	}

	// Token: 0x06001CEF RID: 7407 RVA: 0x0009A04C File Offset: 0x0009824C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (!this.allowSettingOnlyFetchMarkedItems)
		{
			this.onlyFetchMarkedItems = false;
		}
		this.UpdateFetchCategory();
	}

	// Token: 0x06001CF0 RID: 7408 RVA: 0x0009A064 File Offset: 0x00098264
	protected override void OnSpawn()
	{
		base.SetWorkTime(this.storageWorkTime);
		foreach (GameObject gameObject in this.items)
		{
			this.ApplyStoredItemModifiers(gameObject, true, true);
			if (this.sendOnStoreOnSpawn)
			{
				gameObject.Trigger(856640610, this);
			}
		}
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.SetSymbolVisiblity("sweep", this.onlyFetchMarkedItems);
		}
		Prioritizable component2 = base.GetComponent<Prioritizable>();
		if (component2 != null)
		{
			Prioritizable prioritizable = component2;
			prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnPriorityChanged));
		}
		this.UpdateFetchCategory();
		if (this.showUnreachableStatus)
		{
			base.Subscribe<Storage>(-1432940121, Storage.OnReachableChangedDelegate);
			new ReachabilityMonitor.Instance(this).StartSM();
		}
	}

	// Token: 0x06001CF1 RID: 7409 RVA: 0x0009A15C File Offset: 0x0009835C
	public GameObject Store(GameObject go, bool hide_popups = false, bool block_events = false, bool do_disease_transfer = true, bool is_deserializing = false)
	{
		if (go == null)
		{
			return null;
		}
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		GameObject gameObject = go;
		if (!hide_popups && PopFXManager.Instance != null)
		{
			LocString locString;
			Transform transform;
			if (this.fxPrefix == Storage.FXPrefix.Delivered)
			{
				locString = UI.DELIVERED;
				transform = base.transform;
			}
			else
			{
				locString = UI.PICKEDUP;
				transform = go.transform;
			}
			string text;
			if (!Assets.IsTagCountable(go.PrefabID()))
			{
				text = string.Format(locString, GameUtil.GetFormattedMass(component.Units, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), go.GetProperName());
			}
			else
			{
				text = string.Format(locString, (int)component.Units, go.GetProperName());
			}
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, text, transform, this.storageFXOffset, 1.5f, false, false);
		}
		go.transform.parent = base.transform;
		Vector3 vector = Grid.CellToPosCCC(Grid.PosToCell(this), Grid.SceneLayer.Move);
		vector.z = go.transform.GetPosition().z;
		go.transform.SetPosition(vector);
		if (!block_events && do_disease_transfer)
		{
			this.TransferDiseaseWithObject(go);
		}
		if (!is_deserializing)
		{
			Pickupable component2 = go.GetComponent<Pickupable>();
			if (component2 != null)
			{
				if (component2 != null && component2.prevent_absorb_until_stored)
				{
					component2.prevent_absorb_until_stored = false;
				}
				foreach (GameObject gameObject2 in this.items)
				{
					if (gameObject2 != null)
					{
						Pickupable component3 = gameObject2.GetComponent<Pickupable>();
						if (component3 != null && component3.TryAbsorb(component2, hide_popups, true))
						{
							if (!block_events)
							{
								base.Trigger(-1697596308, go);
								base.Trigger(-778359855, this);
								if (this.OnStorageIncreased != null)
								{
									this.OnStorageIncreased();
								}
							}
							this.ApplyStoredItemModifiers(go, true, false);
							gameObject = gameObject2;
							go = null;
							break;
						}
					}
				}
			}
		}
		if (go != null)
		{
			this.items.Add(go);
			if (!is_deserializing)
			{
				this.ApplyStoredItemModifiers(go, true, false);
			}
			if (!block_events)
			{
				go.Trigger(856640610, this);
				base.Trigger(-1697596308, go);
				base.Trigger(-778359855, this);
				if (this.OnStorageIncreased != null)
				{
					this.OnStorageIncreased();
				}
			}
		}
		return gameObject;
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x0009A3C4 File Offset: 0x000985C4
	public PrimaryElement AddElement(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass = false, bool do_disease_transfer = true)
	{
		Element element2 = ElementLoader.FindElementByHash(element);
		if (element2.IsGas)
		{
			return this.AddGasChunk(element, mass, temperature, disease_idx, disease_count, keep_zero_mass, do_disease_transfer);
		}
		if (element2.IsLiquid)
		{
			return this.AddLiquid(element, mass, temperature, disease_idx, disease_count, keep_zero_mass, do_disease_transfer);
		}
		if (element2.IsSolid)
		{
			return this.AddOre(element, mass, temperature, disease_idx, disease_count, keep_zero_mass, do_disease_transfer);
		}
		return null;
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0009A428 File Offset: 0x00098628
	public PrimaryElement AddOre(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass = false, bool do_disease_transfer = true)
	{
		if (mass <= 0f)
		{
			return null;
		}
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, primaryElement.Mass, temperature, mass);
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			primaryElement.Mass += mass;
			primaryElement.Temperature = finalTemperature;
			primaryElement.AddDisease(disease_idx, disease_count, "Storage.AddOre");
			base.Trigger(-1697596308, primaryElement.gameObject);
		}
		else
		{
			Element element2 = ElementLoader.FindElementByHash(element);
			GameObject gameObject = element2.substance.SpawnResource(base.transform.GetPosition(), mass, temperature, disease_idx, disease_count, true, false, true);
			gameObject.GetComponent<Pickupable>().prevent_absorb_until_stored = true;
			element2.substance.ActivateSubstanceGameObject(gameObject, disease_idx, disease_count);
			this.Store(gameObject, true, false, do_disease_transfer, false);
		}
		return primaryElement;
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x0009A4F4 File Offset: 0x000986F4
	public PrimaryElement AddLiquid(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass = false, bool do_disease_transfer = true)
	{
		if (mass <= 0f)
		{
			return null;
		}
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, primaryElement.Mass, temperature, mass);
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			primaryElement.Mass += mass;
			primaryElement.Temperature = finalTemperature;
			primaryElement.AddDisease(disease_idx, disease_count, "Storage.AddLiquid");
			base.Trigger(-1697596308, primaryElement.gameObject);
		}
		else
		{
			SubstanceChunk substanceChunk = LiquidSourceManager.Instance.CreateChunk(element, mass, temperature, disease_idx, disease_count, base.transform.GetPosition());
			primaryElement = substanceChunk.GetComponent<PrimaryElement>();
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			this.Store(substanceChunk.gameObject, true, false, do_disease_transfer, false);
		}
		return primaryElement;
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x0009A5B0 File Offset: 0x000987B0
	public PrimaryElement AddGasChunk(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass, bool do_disease_transfer = true)
	{
		if (mass <= 0f)
		{
			return null;
		}
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float mass2 = primaryElement.Mass;
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, mass2, temperature, mass);
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			primaryElement.SetMassTemperature(mass2 + mass, finalTemperature);
			primaryElement.AddDisease(disease_idx, disease_count, "Storage.AddGasChunk");
			base.Trigger(-1697596308, primaryElement.gameObject);
		}
		else
		{
			SubstanceChunk substanceChunk = GasSourceManager.Instance.CreateChunk(element, mass, temperature, disease_idx, disease_count, base.transform.GetPosition());
			primaryElement = substanceChunk.GetComponent<PrimaryElement>();
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			this.Store(substanceChunk.gameObject, true, false, do_disease_transfer, false);
		}
		return primaryElement;
	}

	// Token: 0x06001CF6 RID: 7414 RVA: 0x0009A661 File Offset: 0x00098861
	public void Transfer(Storage target, bool block_events = false, bool hide_popups = false)
	{
		while (this.items.Count > 0)
		{
			this.Transfer(this.items[0], target, block_events, hide_popups);
		}
	}

	// Token: 0x06001CF7 RID: 7415 RVA: 0x0009A68C File Offset: 0x0009888C
	public float Transfer(Storage dest_storage, Tag tag, float amount, bool block_events = false, bool hide_popups = false)
	{
		GameObject gameObject = this.FindFirst(tag);
		if (gameObject != null)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (amount < component.Units)
			{
				Pickupable component2 = gameObject.GetComponent<Pickupable>();
				Pickupable pickupable = component2.Take(amount);
				dest_storage.Store(pickupable.gameObject, hide_popups, block_events, true, false);
				if (!block_events)
				{
					base.Trigger(-1697596308, component2.gameObject);
				}
			}
			else
			{
				this.Transfer(gameObject, dest_storage, block_events, hide_popups);
				amount = component.Units;
			}
			return amount;
		}
		return 0f;
	}

	// Token: 0x06001CF8 RID: 7416 RVA: 0x0009A710 File Offset: 0x00098910
	public bool Transfer(GameObject go, Storage target, bool block_events = false, bool hide_popups = false)
	{
		this.items.RemoveAll((GameObject it) => it == null);
		int count = this.items.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.items[i] == go)
			{
				this.items.RemoveAt(i);
				this.ApplyStoredItemModifiers(go, false, false);
				target.Store(go, hide_popups, block_events, true, false);
				if (!block_events)
				{
					base.Trigger(-1697596308, go);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001CF9 RID: 7417 RVA: 0x0009A7A8 File Offset: 0x000989A8
	public bool DropSome(Tag tag, float amount, bool ventGas = false, bool dumpLiquid = false, Vector3 offset = default(Vector3), bool doDiseaseTransfer = true, bool showInWorldNotification = false)
	{
		bool flag = false;
		float num = amount;
		ListPool<GameObject, Storage>.PooledList pooledList = ListPool<GameObject, Storage>.Allocate();
		this.Find(tag, pooledList);
		foreach (GameObject gameObject in pooledList)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			if (component)
			{
				Pickupable pickupable = component.Take(num);
				if (pickupable != null)
				{
					bool flag2 = false;
					if (ventGas || dumpLiquid)
					{
						Dumpable component2 = pickupable.GetComponent<Dumpable>();
						if (component2 != null)
						{
							if (ventGas && pickupable.GetComponent<PrimaryElement>().Element.IsGas)
							{
								component2.Dump(base.transform.GetPosition() + offset);
								flag2 = true;
								num -= pickupable.GetComponent<PrimaryElement>().Mass;
								base.Trigger(-1697596308, pickupable.gameObject);
								flag = true;
								if (showInWorldNotification)
								{
									PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, pickupable.GetComponent<PrimaryElement>().Element.name + " " + GameUtil.GetFormattedMass(pickupable.TotalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.transform, this.storageFXOffset, 1.5f, false, false);
								}
							}
							if (dumpLiquid && pickupable.GetComponent<PrimaryElement>().Element.IsLiquid)
							{
								component2.Dump(base.transform.GetPosition() + offset);
								flag2 = true;
								num -= pickupable.GetComponent<PrimaryElement>().Mass;
								base.Trigger(-1697596308, pickupable.gameObject);
								flag = true;
								if (showInWorldNotification)
								{
									PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, pickupable.GetComponent<PrimaryElement>().Element.name + " " + GameUtil.GetFormattedMass(pickupable.TotalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.transform, this.storageFXOffset, 1.5f, false, false);
								}
							}
						}
					}
					if (!flag2)
					{
						Vector3 vector = Grid.CellToPosCCC(Grid.PosToCell(this), Grid.SceneLayer.Ore) + offset;
						pickupable.transform.SetPosition(vector);
						KBatchedAnimController component3 = pickupable.GetComponent<KBatchedAnimController>();
						if (component3)
						{
							component3.SetSceneLayer(Grid.SceneLayer.Ore);
						}
						num -= pickupable.GetComponent<PrimaryElement>().Mass;
						this.MakeWorldActive(pickupable.gameObject);
						base.Trigger(-1697596308, pickupable.gameObject);
						flag = true;
						if (showInWorldNotification)
						{
							PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, pickupable.GetComponent<PrimaryElement>().Element.name + " " + GameUtil.GetFormattedMass(pickupable.TotalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.transform, this.storageFXOffset, 1.5f, false, false);
						}
					}
				}
			}
			if (num <= 0f)
			{
				break;
			}
		}
		pooledList.Recycle();
		return flag;
	}

	// Token: 0x06001CFA RID: 7418 RVA: 0x0009AAB4 File Offset: 0x00098CB4
	public void DropAll(Vector3 position, bool vent_gas = false, bool dump_liquid = false, Vector3 offset = default(Vector3), bool do_disease_transfer = true, List<GameObject> collect_dropped_items = null)
	{
		while (this.items.Count > 0)
		{
			GameObject gameObject = this.items[0];
			if (do_disease_transfer)
			{
				this.TransferDiseaseWithObject(gameObject);
			}
			this.items.RemoveAt(0);
			if (gameObject != null)
			{
				bool flag = false;
				if (vent_gas || dump_liquid)
				{
					Dumpable component = gameObject.GetComponent<Dumpable>();
					if (component != null)
					{
						if (vent_gas && gameObject.GetComponent<PrimaryElement>().Element.IsGas)
						{
							component.Dump(position + offset);
							flag = true;
						}
						if (dump_liquid && gameObject.GetComponent<PrimaryElement>().Element.IsLiquid)
						{
							component.Dump(position + offset);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					gameObject.transform.SetPosition(position + offset);
					KBatchedAnimController component2 = gameObject.GetComponent<KBatchedAnimController>();
					if (component2)
					{
						component2.SetSceneLayer(Grid.SceneLayer.Ore);
					}
					this.MakeWorldActive(gameObject);
					if (collect_dropped_items != null)
					{
						collect_dropped_items.Add(gameObject);
					}
				}
			}
		}
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0009ABA9 File Offset: 0x00098DA9
	public void DropAll(bool vent_gas = false, bool dump_liquid = false, Vector3 offset = default(Vector3), bool do_disease_transfer = true, List<GameObject> collect_dropped_items = null)
	{
		this.DropAll(Grid.CellToPosCCC(Grid.PosToCell(this), Grid.SceneLayer.Ore), vent_gas, dump_liquid, offset, do_disease_transfer, collect_dropped_items);
	}

	// Token: 0x06001CFC RID: 7420 RVA: 0x0009ABC8 File Offset: 0x00098DC8
	public void Drop(Tag t, List<GameObject> obj_list)
	{
		this.Find(t, obj_list);
		foreach (GameObject gameObject in obj_list)
		{
			this.Drop(gameObject, true);
		}
	}

	// Token: 0x06001CFD RID: 7421 RVA: 0x0009AC24 File Offset: 0x00098E24
	public void Drop(Tag t)
	{
		ListPool<GameObject, Storage>.PooledList pooledList = ListPool<GameObject, Storage>.Allocate();
		this.Find(t, pooledList);
		foreach (GameObject gameObject in pooledList)
		{
			this.Drop(gameObject, true);
		}
		pooledList.Recycle();
	}

	// Token: 0x06001CFE RID: 7422 RVA: 0x0009AC8C File Offset: 0x00098E8C
	public void DropUnlessMatching(FetchChore chore)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null))
			{
				KPrefabID component = this.items[i].GetComponent<KPrefabID>();
				if (!(((chore.criteria == FetchChore.MatchCriteria.MatchID && chore.tags.Contains(component.PrefabTag)) || (chore.criteria == FetchChore.MatchCriteria.MatchTags && component.HasTag(chore.tagsFirst))) & (!chore.requiredTag.IsValid || component.HasTag(chore.requiredTag)) & !component.HasAnyTags(chore.forbiddenTags)))
				{
					GameObject gameObject = this.items[i];
					this.items.RemoveAt(i);
					i--;
					this.TransferDiseaseWithObject(gameObject);
					this.MakeWorldActive(gameObject);
				}
			}
		}
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x0009AD70 File Offset: 0x00098F70
	public void DropUnlessHasTag(Tag tag)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null) && !this.items[i].GetComponent<KPrefabID>().HasTag(tag))
			{
				GameObject gameObject = this.items[i];
				this.items.RemoveAt(i);
				i--;
				this.TransferDiseaseWithObject(gameObject);
				this.MakeWorldActive(gameObject);
				Dumpable component = gameObject.GetComponent<Dumpable>();
				if (component != null)
				{
					component.Dump(base.transform.GetPosition());
				}
			}
		}
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x0009AE10 File Offset: 0x00099010
	public GameObject Drop(GameObject go, bool do_disease_transfer = true)
	{
		if (go == null)
		{
			return null;
		}
		int count = this.items.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(go != this.items[i]))
			{
				this.items[i] = this.items[count - 1];
				this.items.RemoveAt(count - 1);
				if (do_disease_transfer)
				{
					this.TransferDiseaseWithObject(go);
				}
				this.MakeWorldActive(go);
				break;
			}
		}
		return go;
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x0009AE90 File Offset: 0x00099090
	public void RenotifyAll()
	{
		this.items.RemoveAll((GameObject it) => it == null);
		foreach (GameObject gameObject in this.items)
		{
			gameObject.Trigger(856640610, this);
		}
	}

	// Token: 0x06001D02 RID: 7426 RVA: 0x0009AF14 File Offset: 0x00099114
	private void TransferDiseaseWithObject(GameObject obj)
	{
		if (obj == null || !this.doDiseaseTransfer || this.primaryElement == null)
		{
			return;
		}
		PrimaryElement component = obj.GetComponent<PrimaryElement>();
		if (component == null)
		{
			return;
		}
		SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
		invalid.idx = component.DiseaseIdx;
		invalid.count = (int)((float)component.DiseaseCount * 0.05f);
		SimUtil.DiseaseInfo invalid2 = SimUtil.DiseaseInfo.Invalid;
		invalid2.idx = this.primaryElement.DiseaseIdx;
		invalid2.count = (int)((float)this.primaryElement.DiseaseCount * 0.05f);
		component.ModifyDiseaseCount(-invalid.count, "Storage.TransferDiseaseWithObject");
		this.primaryElement.ModifyDiseaseCount(-invalid2.count, "Storage.TransferDiseaseWithObject");
		if (invalid.count > 0)
		{
			this.primaryElement.AddDisease(invalid.idx, invalid.count, "Storage.TransferDiseaseWithObject");
		}
		if (invalid2.count > 0)
		{
			component.AddDisease(invalid2.idx, invalid2.count, "Storage.TransferDiseaseWithObject");
		}
	}

	// Token: 0x06001D03 RID: 7427 RVA: 0x0009B01C File Offset: 0x0009921C
	private void MakeWorldActive(GameObject go)
	{
		go.transform.parent = null;
		go.Trigger(856640610, null);
		base.Trigger(-1697596308, go);
		this.ApplyStoredItemModifiers(go, false, false);
		if (go != null)
		{
			PrimaryElement component = go.GetComponent<PrimaryElement>();
			if (component != null && component.KeepZeroMassObject)
			{
				component.KeepZeroMassObject = false;
				if (component.Mass <= 0f)
				{
					Util.KDestroyGameObject(go);
				}
			}
		}
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x0009B094 File Offset: 0x00099294
	public List<GameObject> Find(Tag tag, List<GameObject> result)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (!(gameObject == null) && gameObject.HasTag(tag))
			{
				result.Add(gameObject);
			}
		}
		return result;
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x0009B0E0 File Offset: 0x000992E0
	public GameObject FindFirst(Tag tag)
	{
		GameObject gameObject = null;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject2 = this.items[i];
			if (!(gameObject2 == null) && gameObject2.HasTag(tag))
			{
				gameObject = gameObject2;
				break;
			}
		}
		return gameObject;
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x0009B12C File Offset: 0x0009932C
	public PrimaryElement FindFirstWithMass(Tag tag, float mass = 0f)
	{
		PrimaryElement primaryElement = null;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (!(gameObject == null) && gameObject.HasTag(tag))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.Mass > 0f && component.Mass >= mass)
				{
					primaryElement = component;
					break;
				}
			}
		}
		return primaryElement;
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x0009B194 File Offset: 0x00099394
	public HashSet<Tag> GetAllIDsInStorage()
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			hashSet.Add(gameObject.PrefabID());
		}
		return hashSet;
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x0009B1D8 File Offset: 0x000993D8
	public GameObject Find(int ID)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (ID == gameObject.PrefabID().GetHashCode())
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x0009B222 File Offset: 0x00099422
	public void ConsumeAllIgnoringDisease()
	{
		this.ConsumeAllIgnoringDisease(Tag.Invalid);
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0009B230 File Offset: 0x00099430
	public void ConsumeAllIgnoringDisease(Tag tag)
	{
		for (int i = this.items.Count - 1; i >= 0; i--)
		{
			if (!(tag != Tag.Invalid) || this.items[i].HasTag(tag))
			{
				this.ConsumeIgnoringDisease(this.items[i]);
			}
		}
	}

	// Token: 0x06001D0B RID: 7435 RVA: 0x0009B288 File Offset: 0x00099488
	public void ConsumeAndGetDisease(Tag tag, float amount, out float amount_consumed, out SimUtil.DiseaseInfo disease_info, out float aggregate_temperature)
	{
		DebugUtil.Assert(tag.IsValid);
		amount_consumed = 0f;
		disease_info = SimUtil.DiseaseInfo.Invalid;
		aggregate_temperature = 0f;
		bool flag = false;
		int num = 0;
		while (num < this.items.Count && amount > 0f)
		{
			GameObject gameObject = this.items[num];
			if (!(gameObject == null) && gameObject.HasTag(tag))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.Units > 0f)
				{
					flag = true;
					float num2 = Math.Min(component.Units, amount);
					global::Debug.Assert(num2 > 0f, "Delta amount was zero, which should be impossible.");
					aggregate_temperature = SimUtil.CalculateFinalTemperature(amount_consumed, aggregate_temperature, num2, component.Temperature);
					SimUtil.DiseaseInfo percentOfDisease = SimUtil.GetPercentOfDisease(component, num2 / component.Units);
					disease_info = SimUtil.CalculateFinalDiseaseInfo(disease_info, percentOfDisease);
					component.Units -= num2;
					component.ModifyDiseaseCount(-percentOfDisease.count, "Storage.ConsumeAndGetDisease");
					amount -= num2;
					amount_consumed += num2;
				}
				if (component.Units <= 0f && !component.KeepZeroMassObject)
				{
					if (this.deleted_objects == null)
					{
						this.deleted_objects = new List<GameObject>();
					}
					this.deleted_objects.Add(gameObject);
				}
				base.Trigger(-1697596308, gameObject);
			}
			num++;
		}
		if (!flag)
		{
			aggregate_temperature = base.GetComponent<PrimaryElement>().Temperature;
		}
		if (this.deleted_objects != null)
		{
			for (int i = 0; i < this.deleted_objects.Count; i++)
			{
				this.items.Remove(this.deleted_objects[i]);
				Util.KDestroyGameObject(this.deleted_objects[i]);
			}
			this.deleted_objects.Clear();
		}
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x0009B458 File Offset: 0x00099658
	public void ConsumeAndGetDisease(Recipe.Ingredient ingredient, out SimUtil.DiseaseInfo disease_info, out float temperature)
	{
		float num;
		this.ConsumeAndGetDisease(ingredient.tag, ingredient.amount, out num, out disease_info, out temperature);
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x0009B47C File Offset: 0x0009967C
	public void ConsumeIgnoringDisease(Tag tag, float amount)
	{
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		this.ConsumeAndGetDisease(tag, amount, out num, out diseaseInfo, out num2);
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x0009B498 File Offset: 0x00099698
	public void ConsumeIgnoringDisease(GameObject item_go)
	{
		if (this.items.Contains(item_go))
		{
			PrimaryElement component = item_go.GetComponent<PrimaryElement>();
			if (component != null && component.KeepZeroMassObject)
			{
				component.Units = 0f;
				component.ModifyDiseaseCount(-component.DiseaseCount, "consume item");
				base.Trigger(-1697596308, item_go);
				return;
			}
			this.items.Remove(item_go);
			base.Trigger(-1697596308, item_go);
			item_go.DeleteObject();
		}
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x0009B514 File Offset: 0x00099714
	public GameObject Drop(int ID)
	{
		return this.Drop(this.Find(ID), true);
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x0009B524 File Offset: 0x00099724
	private void OnDeath(object data)
	{
		this.DropAll(true, true, default(Vector3), true, null);
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x0009B544 File Offset: 0x00099744
	public bool IsFull()
	{
		return this.RemainingCapacity() <= 0f;
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0009B556 File Offset: 0x00099756
	public bool IsEmpty()
	{
		return this.items.Count == 0;
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0009B566 File Offset: 0x00099766
	public float Capacity()
	{
		return this.capacityKg;
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0009B56E File Offset: 0x0009976E
	public bool IsEndOfLife()
	{
		return this.endOfLife;
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x0009B578 File Offset: 0x00099778
	public float MassStored()
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null))
			{
				PrimaryElement component = this.items[i].GetComponent<PrimaryElement>();
				if (component != null)
				{
					num += component.Units * component.MassPerUnit;
				}
			}
		}
		return (float)Mathf.RoundToInt(num * 1000f) / 1000f;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x0009B5F4 File Offset: 0x000997F4
	public float UnitsStored()
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null))
			{
				PrimaryElement component = this.items[i].GetComponent<PrimaryElement>();
				if (component != null)
				{
					num += component.Units;
				}
			}
		}
		return (float)Mathf.RoundToInt(num * 1000f) / 1000f;
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0009B668 File Offset: 0x00099868
	public bool Has(Tag tag)
	{
		bool flag = false;
		foreach (GameObject gameObject in this.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.HasTag(tag) && component.Mass > 0f)
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x0009B6E4 File Offset: 0x000998E4
	public PrimaryElement AddToPrimaryElement(SimHashes element, float additional_mass, float temperature)
	{
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, primaryElement.Mass, temperature, additional_mass);
			primaryElement.Mass += additional_mass;
			primaryElement.Temperature = finalTemperature;
		}
		return primaryElement;
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x0009B72C File Offset: 0x0009992C
	public PrimaryElement FindPrimaryElement(SimHashes element)
	{
		PrimaryElement primaryElement = null;
		foreach (GameObject gameObject in this.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.ElementID == element)
				{
					primaryElement = component;
					break;
				}
			}
		}
		return primaryElement;
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0009B798 File Offset: 0x00099998
	public float RemainingCapacity()
	{
		return this.capacityKg - this.MassStored();
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x0009B7A7 File Offset: 0x000999A7
	public bool GetOnlyFetchMarkedItems()
	{
		return this.onlyFetchMarkedItems;
	}

	// Token: 0x06001D1C RID: 7452 RVA: 0x0009B7AF File Offset: 0x000999AF
	public void SetOnlyFetchMarkedItems(bool is_set)
	{
		if (is_set != this.onlyFetchMarkedItems)
		{
			this.onlyFetchMarkedItems = is_set;
			this.UpdateFetchCategory();
			base.Trigger(644822890, null);
			base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("sweep", is_set);
		}
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x0009B7E9 File Offset: 0x000999E9
	private void UpdateFetchCategory()
	{
		if (this.fetchCategory == Storage.FetchCategory.Building)
		{
			return;
		}
		this.fetchCategory = (this.onlyFetchMarkedItems ? Storage.FetchCategory.StorageSweepOnly : Storage.FetchCategory.GeneralStorage);
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x0009B806 File Offset: 0x00099A06
	protected override void OnCleanUp()
	{
		if (this.items.Count != 0)
		{
			global::Debug.LogWarning("Storage for [" + base.gameObject.name + "] is being destroyed but it still contains items!", base.gameObject);
		}
		base.OnCleanUp();
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x0009B840 File Offset: 0x00099A40
	private void OnQueueDestroyObject(object data)
	{
		this.endOfLife = true;
		this.DropAll(true, false, default(Vector3), true, null);
		this.OnCleanUp();
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x0009B86D File Offset: 0x00099A6D
	public void Remove(GameObject go, bool do_disease_transfer = true)
	{
		this.items.Remove(go);
		if (do_disease_transfer)
		{
			this.TransferDiseaseWithObject(go);
		}
		base.Trigger(-1697596308, go);
		this.ApplyStoredItemModifiers(go, false, false);
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x0009B89C File Offset: 0x00099A9C
	public bool ForceStore(Tag tag, float amount)
	{
		global::Debug.Assert(amount < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT);
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				gameObject.GetComponent<PrimaryElement>().Mass += amount;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x0009B904 File Offset: 0x00099B04
	public float GetAmountAvailable(Tag tag)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				num += gameObject.GetComponent<PrimaryElement>().Units;
			}
		}
		return num;
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x0009B95C File Offset: 0x00099B5C
	public float GetAmountAvailable(Tag tag, Tag[] forbiddenTags = null)
	{
		if (forbiddenTags == null)
		{
			return this.GetAmountAvailable(tag);
		}
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag) && !gameObject.HasAnyTags(forbiddenTags))
			{
				num += gameObject.GetComponent<PrimaryElement>().Units;
			}
		}
		return num;
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x0009B9C8 File Offset: 0x00099BC8
	public float GetUnitsAvailable(Tag tag)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				num += gameObject.GetComponent<PrimaryElement>().Units;
			}
		}
		return num;
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x0009BA20 File Offset: 0x00099C20
	public float GetMassAvailable(Tag tag)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				num += gameObject.GetComponent<PrimaryElement>().Mass;
			}
		}
		return num;
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x0009BA78 File Offset: 0x00099C78
	public float GetMassAvailable(SimHashes element)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.ElementID == element)
				{
					num += component.Mass;
				}
			}
		}
		return num;
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x0009BAD4 File Offset: 0x00099CD4
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (this.showDescriptor)
		{
			descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.STORAGECAPACITY, GameUtil.GetFormattedMass(this.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.STORAGECAPACITY, GameUtil.GetFormattedMass(this.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Effect, false));
		}
		return descriptors;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0009BB44 File Offset: 0x00099D44
	public static void MakeItemTemperatureInsulated(GameObject go, bool is_stored, bool is_initializing)
	{
		SimTemperatureTransfer component = go.GetComponent<SimTemperatureTransfer>();
		if (component == null)
		{
			return;
		}
		component.enabled = !is_stored;
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0009BB6C File Offset: 0x00099D6C
	public static void MakeItemInvisible(GameObject go, bool is_stored, bool is_initializing)
	{
		if (is_initializing)
		{
			return;
		}
		bool flag = !is_stored;
		KAnimControllerBase component = go.GetComponent<KAnimControllerBase>();
		if (component != null && component.enabled != flag)
		{
			component.enabled = flag;
		}
		KSelectable component2 = go.GetComponent<KSelectable>();
		if (component2 != null && component2.enabled != flag)
		{
			component2.enabled = flag;
		}
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0009BBC2 File Offset: 0x00099DC2
	public static void MakeItemSealed(GameObject go, bool is_stored, bool is_initializing)
	{
		if (go != null)
		{
			if (is_stored)
			{
				go.GetComponent<KPrefabID>().AddTag(GameTags.Sealed, false);
				return;
			}
			go.GetComponent<KPrefabID>().RemoveTag(GameTags.Sealed);
		}
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x0009BBF2 File Offset: 0x00099DF2
	public static void MakeItemPreserved(GameObject go, bool is_stored, bool is_initializing)
	{
		if (go != null)
		{
			if (is_stored)
			{
				go.GetComponent<KPrefabID>().AddTag(GameTags.Preserved, false);
				return;
			}
			go.GetComponent<KPrefabID>().RemoveTag(GameTags.Preserved);
		}
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x0009BC24 File Offset: 0x00099E24
	private void ApplyStoredItemModifiers(GameObject go, bool is_stored, bool is_initializing)
	{
		List<Storage.StoredItemModifier> list = this.defaultStoredItemModifers;
		for (int i = 0; i < list.Count; i++)
		{
			Storage.StoredItemModifier storedItemModifier = list[i];
			for (int j = 0; j < Storage.StoredItemModifierHandlers.Count; j++)
			{
				Storage.StoredItemModifierInfo storedItemModifierInfo = Storage.StoredItemModifierHandlers[j];
				if (storedItemModifierInfo.modifier == storedItemModifier)
				{
					storedItemModifierInfo.toggleState(go, is_stored, is_initializing);
					break;
				}
			}
		}
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x0009BC90 File Offset: 0x00099E90
	protected virtual void OnCopySettings(object data)
	{
		Storage component = ((GameObject)data).GetComponent<Storage>();
		if (component != null)
		{
			this.SetOnlyFetchMarkedItems(component.onlyFetchMarkedItems);
		}
	}

	// Token: 0x06001D2E RID: 7470 RVA: 0x0009BCC0 File Offset: 0x00099EC0
	private void OnPriorityChanged(PrioritySetting priority)
	{
		foreach (GameObject gameObject in this.items)
		{
			gameObject.Trigger(-1626373771, this);
		}
	}

	// Token: 0x06001D2F RID: 7471 RVA: 0x0009BD18 File Offset: 0x00099F18
	private void OnReachableChanged(object data)
	{
		bool flag = (bool)data;
		KSelectable component = base.GetComponent<KSelectable>();
		if (flag)
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.StorageUnreachable, false);
			return;
		}
		component.AddStatusItem(Db.Get().BuildingStatusItems.StorageUnreachable, this);
	}

	// Token: 0x06001D30 RID: 7472 RVA: 0x0009BD64 File Offset: 0x00099F64
	private bool ShouldSaveItem(GameObject go)
	{
		bool flag = false;
		if (go != null && go.GetComponent<SaveLoadRoot>() != null && go.GetComponent<PrimaryElement>().Mass > 0f)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x0009BDA0 File Offset: 0x00099FA0
	public void Serialize(BinaryWriter writer)
	{
		int num = 0;
		int count = this.items.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.ShouldSaveItem(this.items[i]))
			{
				num++;
			}
		}
		writer.Write(num);
		if (num == 0)
		{
			return;
		}
		if (this.items != null && this.items.Count > 0)
		{
			for (int j = 0; j < this.items.Count; j++)
			{
				GameObject gameObject = this.items[j];
				if (this.ShouldSaveItem(gameObject))
				{
					SaveLoadRoot component = gameObject.GetComponent<SaveLoadRoot>();
					if (component != null)
					{
						string name = gameObject.GetComponent<KPrefabID>().GetSaveLoadTag().Name;
						writer.WriteKleiString(name);
						component.Save(writer);
					}
					else
					{
						global::Debug.Log("Tried to save obj in storage but obj has no SaveLoadRoot", gameObject);
					}
				}
			}
		}
	}

	// Token: 0x06001D32 RID: 7474 RVA: 0x0009BE7C File Offset: 0x0009A07C
	public void Deserialize(IReader reader)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		this.ClearItems();
		int num4 = reader.ReadInt32();
		this.items = new List<GameObject>(num4);
		for (int i = 0; i < num4; i++)
		{
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
			Tag tag = TagManager.Create(reader.ReadKleiString());
			SaveLoadRoot saveLoadRoot = SaveLoadRoot.Load(tag, reader);
			num += Time.realtimeSinceStartup - realtimeSinceStartup2;
			if (saveLoadRoot != null)
			{
				KBatchedAnimController component = saveLoadRoot.GetComponent<KBatchedAnimController>();
				if (component != null)
				{
					component.enabled = false;
				}
				saveLoadRoot.SetRegistered(false);
				float realtimeSinceStartup3 = Time.realtimeSinceStartup;
				GameObject gameObject = this.Store(saveLoadRoot.gameObject, true, true, false, true);
				num2 += Time.realtimeSinceStartup - realtimeSinceStartup3;
				if (gameObject != null)
				{
					Pickupable component2 = gameObject.GetComponent<Pickupable>();
					if (component2 != null)
					{
						float realtimeSinceStartup4 = Time.realtimeSinceStartup;
						component2.OnStore(this);
						num3 += Time.realtimeSinceStartup - realtimeSinceStartup4;
					}
					Storable component3 = gameObject.GetComponent<Storable>();
					if (component3 != null)
					{
						float realtimeSinceStartup5 = Time.realtimeSinceStartup;
						component3.OnStore(this);
						num3 += Time.realtimeSinceStartup - realtimeSinceStartup5;
					}
					if (this.dropOnLoad)
					{
						this.Drop(saveLoadRoot.gameObject, true);
					}
				}
			}
			else
			{
				global::Debug.LogWarning("Tried to deserialize " + tag.ToString() + " into storage but failed", base.gameObject);
			}
		}
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
	private void ClearItems()
	{
		foreach (GameObject gameObject in this.items)
		{
			gameObject.DeleteObject();
		}
		this.items.Clear();
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x0009C054 File Offset: 0x0009A254
	public void UpdateStoredItemCachedCells()
	{
		foreach (GameObject gameObject in this.items)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			if (component != null)
			{
				component.UpdateCachedCellFromStoragePosition();
			}
		}
	}

	// Token: 0x04001045 RID: 4165
	public bool allowItemRemoval;

	// Token: 0x04001046 RID: 4166
	public bool ignoreSourcePriority;

	// Token: 0x04001047 RID: 4167
	public bool onlyTransferFromLowerPriority;

	// Token: 0x04001048 RID: 4168
	public float capacityKg = 20000f;

	// Token: 0x04001049 RID: 4169
	public bool showDescriptor;

	// Token: 0x0400104B RID: 4171
	public bool doDiseaseTransfer = true;

	// Token: 0x0400104C RID: 4172
	public List<Tag> storageFilters;

	// Token: 0x0400104D RID: 4173
	public bool useGunForDelivery = true;

	// Token: 0x0400104E RID: 4174
	public bool sendOnStoreOnSpawn;

	// Token: 0x0400104F RID: 4175
	public bool showInUI = true;

	// Token: 0x04001050 RID: 4176
	public bool allowClearable;

	// Token: 0x04001051 RID: 4177
	public bool showCapacityStatusItem;

	// Token: 0x04001052 RID: 4178
	public bool showCapacityAsMainStatus;

	// Token: 0x04001053 RID: 4179
	public bool showUnreachableStatus;

	// Token: 0x04001054 RID: 4180
	public bool showSideScreenTitleBar;

	// Token: 0x04001055 RID: 4181
	public bool useWideOffsets;

	// Token: 0x04001056 RID: 4182
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001057 RID: 4183
	public Vector2 gunTargetOffset;

	// Token: 0x04001058 RID: 4184
	public Storage.FetchCategory fetchCategory;

	// Token: 0x04001059 RID: 4185
	public int storageNetworkID = -1;

	// Token: 0x0400105A RID: 4186
	public float storageFullMargin;

	// Token: 0x0400105B RID: 4187
	public Vector3 storageFXOffset = Vector3.zero;

	// Token: 0x0400105C RID: 4188
	private static readonly EventSystem.IntraObjectHandler<Storage> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Storage>(delegate(Storage component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x0400105D RID: 4189
	public Storage.FXPrefix fxPrefix;

	// Token: 0x0400105E RID: 4190
	public List<GameObject> items = new List<GameObject>();

	// Token: 0x0400105F RID: 4191
	[MyCmpGet]
	public Prioritizable prioritizable;

	// Token: 0x04001060 RID: 4192
	[MyCmpGet]
	public Automatable automatable;

	// Token: 0x04001061 RID: 4193
	[MyCmpGet]
	protected PrimaryElement primaryElement;

	// Token: 0x04001062 RID: 4194
	public bool dropOnLoad;

	// Token: 0x04001063 RID: 4195
	protected float maxKGPerItem = float.MaxValue;

	// Token: 0x04001064 RID: 4196
	private bool endOfLife;

	// Token: 0x04001065 RID: 4197
	public bool allowSettingOnlyFetchMarkedItems = true;

	// Token: 0x04001066 RID: 4198
	[Serialize]
	private bool onlyFetchMarkedItems;

	// Token: 0x04001067 RID: 4199
	public float storageWorkTime = 1.5f;

	// Token: 0x04001068 RID: 4200
	private static readonly List<Storage.StoredItemModifierInfo> StoredItemModifierHandlers = new List<Storage.StoredItemModifierInfo>
	{
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Hide, new Action<GameObject, bool, bool>(Storage.MakeItemInvisible)),
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Insulate, new Action<GameObject, bool, bool>(Storage.MakeItemTemperatureInsulated)),
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Seal, new Action<GameObject, bool, bool>(Storage.MakeItemSealed)),
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Preserve, new Action<GameObject, bool, bool>(Storage.MakeItemPreserved))
	};

	// Token: 0x04001069 RID: 4201
	[SerializeField]
	private List<Storage.StoredItemModifier> defaultStoredItemModifers = new List<Storage.StoredItemModifier> { Storage.StoredItemModifier.Hide };

	// Token: 0x0400106A RID: 4202
	public static readonly List<Storage.StoredItemModifier> StandardSealedStorage = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Seal
	};

	// Token: 0x0400106B RID: 4203
	public static readonly List<Storage.StoredItemModifier> StandardFabricatorStorage = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Preserve
	};

	// Token: 0x0400106C RID: 4204
	public static readonly List<Storage.StoredItemModifier> StandardInsulatedStorage = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Seal,
		Storage.StoredItemModifier.Insulate
	};

	// Token: 0x0400106E RID: 4206
	private static StatusItem capacityStatusItem;

	// Token: 0x0400106F RID: 4207
	private static readonly EventSystem.IntraObjectHandler<Storage> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<Storage>(GameTags.Dead, delegate(Storage component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x04001070 RID: 4208
	private static readonly EventSystem.IntraObjectHandler<Storage> OnQueueDestroyObjectDelegate = new EventSystem.IntraObjectHandler<Storage>(delegate(Storage component, object data)
	{
		component.OnQueueDestroyObject(data);
	});

	// Token: 0x04001071 RID: 4209
	private static readonly EventSystem.IntraObjectHandler<Storage> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Storage>(delegate(Storage component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001072 RID: 4210
	private List<GameObject> deleted_objects;

	// Token: 0x02001119 RID: 4377
	public enum StoredItemModifier
	{
		// Token: 0x040059D5 RID: 22997
		Insulate,
		// Token: 0x040059D6 RID: 22998
		Hide,
		// Token: 0x040059D7 RID: 22999
		Seal,
		// Token: 0x040059D8 RID: 23000
		Preserve
	}

	// Token: 0x0200111A RID: 4378
	public enum FetchCategory
	{
		// Token: 0x040059DA RID: 23002
		Building,
		// Token: 0x040059DB RID: 23003
		GeneralStorage,
		// Token: 0x040059DC RID: 23004
		StorageSweepOnly
	}

	// Token: 0x0200111B RID: 4379
	public enum FXPrefix
	{
		// Token: 0x040059DE RID: 23006
		Delivered,
		// Token: 0x040059DF RID: 23007
		PickedUp
	}

	// Token: 0x0200111C RID: 4380
	private struct StoredItemModifierInfo
	{
		// Token: 0x0600758B RID: 30091 RVA: 0x002B67D8 File Offset: 0x002B49D8
		public StoredItemModifierInfo(Storage.StoredItemModifier modifier, Action<GameObject, bool, bool> toggle_state)
		{
			this.modifier = modifier;
			this.toggleState = toggle_state;
		}

		// Token: 0x040059E0 RID: 23008
		public Storage.StoredItemModifier modifier;

		// Token: 0x040059E1 RID: 23009
		public Action<GameObject, bool, bool> toggleState;
	}
}
