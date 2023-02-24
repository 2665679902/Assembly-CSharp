using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x02000556 RID: 1366
[AddComponentMenu("KMonoBehaviour/scripts/MinionAssignablesProxy")]
public class MinionAssignablesProxy : KMonoBehaviour, IAssignableIdentity
{
	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060020CD RID: 8397 RVA: 0x000B2E3F File Offset: 0x000B103F
	// (set) Token: 0x060020CE RID: 8398 RVA: 0x000B2E47 File Offset: 0x000B1047
	public IAssignableIdentity target { get; private set; }

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060020CF RID: 8399 RVA: 0x000B2E50 File Offset: 0x000B1050
	public bool IsConfigured
	{
		get
		{
			return this.slotsConfigured;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060020D0 RID: 8400 RVA: 0x000B2E58 File Offset: 0x000B1058
	public int TargetInstanceID
	{
		get
		{
			return this.target_instance_id;
		}
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x000B2E60 File Offset: 0x000B1060
	public GameObject GetTargetGameObject()
	{
		if (this.target == null && this.target_instance_id != -1)
		{
			this.RestoreTargetFromInstanceID();
		}
		KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)this.target;
		if (kmonoBehaviour != null)
		{
			return kmonoBehaviour.gameObject;
		}
		return null;
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x000B2EA4 File Offset: 0x000B10A4
	public float GetArrivalTime()
	{
		if (this.GetTargetGameObject().GetComponent<MinionIdentity>() != null)
		{
			return this.GetTargetGameObject().GetComponent<MinionIdentity>().arrivalTime;
		}
		if (this.GetTargetGameObject().GetComponent<StoredMinionIdentity>() != null)
		{
			return this.GetTargetGameObject().GetComponent<StoredMinionIdentity>().arrivalTime;
		}
		global::Debug.LogError("Could not get minion arrival time");
		return -1f;
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x000B2F08 File Offset: 0x000B1108
	public int GetTotalSkillpoints()
	{
		if (this.GetTargetGameObject().GetComponent<MinionIdentity>() != null)
		{
			return this.GetTargetGameObject().GetComponent<MinionResume>().TotalSkillPointsGained;
		}
		if (this.GetTargetGameObject().GetComponent<StoredMinionIdentity>() != null)
		{
			return MinionResume.CalculateTotalSkillPointsGained(this.GetTargetGameObject().GetComponent<StoredMinionIdentity>().TotalExperienceGained);
		}
		global::Debug.LogError("Could not get minion skill points time");
		return -1;
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x000B2F70 File Offset: 0x000B1170
	public void SetTarget(IAssignableIdentity target, GameObject targetGO)
	{
		global::Debug.Assert(target != null, "target was null");
		if (targetGO == null)
		{
			global::Debug.LogWarningFormat("{0} MinionAssignablesProxy.SetTarget {1}, {2}, {3}. DESTROYING", new object[]
			{
				base.GetInstanceID(),
				this.target_instance_id,
				target,
				targetGO
			});
			Util.KDestroyGameObject(base.gameObject);
		}
		this.target = target;
		this.target_instance_id = targetGO.GetComponent<KPrefabID>().InstanceID;
		base.gameObject.name = "Minion Assignables Proxy : " + targetGO.name;
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x000B3008 File Offset: 0x000B1208
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ownables = new List<Ownables> { base.gameObject.AddOrGet<Ownables>() };
		Components.MinionAssignablesProxy.Add(this);
		base.Subscribe<MinionAssignablesProxy>(1502190696, MinionAssignablesProxy.OnQueueDestroyObjectDelegate);
		this.ConfigureAssignableSlots();
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x000B3059 File Offset: 0x000B1259
	[OnDeserialized]
	private void OnDeserialized()
	{
	}

	// Token: 0x060020D7 RID: 8407 RVA: 0x000B305C File Offset: 0x000B125C
	public void ConfigureAssignableSlots()
	{
		if (this.slotsConfigured)
		{
			return;
		}
		Ownables component = base.GetComponent<Ownables>();
		Equipment component2 = base.GetComponent<Equipment>();
		if (component2 != null)
		{
			foreach (AssignableSlot assignableSlot in Db.Get().AssignableSlots.resources)
			{
				if (assignableSlot is OwnableSlot)
				{
					OwnableSlotInstance ownableSlotInstance = new OwnableSlotInstance(component, (OwnableSlot)assignableSlot);
					component.Add(ownableSlotInstance);
				}
				else if (assignableSlot is EquipmentSlot)
				{
					EquipmentSlotInstance equipmentSlotInstance = new EquipmentSlotInstance(component2, (EquipmentSlot)assignableSlot);
					component2.Add(equipmentSlotInstance);
				}
			}
		}
		this.slotsConfigured = true;
	}

	// Token: 0x060020D8 RID: 8408 RVA: 0x000B3118 File Offset: 0x000B1318
	public void RestoreTargetFromInstanceID()
	{
		if (this.target_instance_id != -1 && this.target == null)
		{
			KPrefabID instance = KPrefabIDTracker.Get().GetInstance(this.target_instance_id);
			if (instance)
			{
				IAssignableIdentity component = instance.GetComponent<IAssignableIdentity>();
				if (component != null)
				{
					this.SetTarget(component, instance.gameObject);
					return;
				}
				global::Debug.LogWarningFormat("RestoreTargetFromInstanceID target ID {0} was found but it wasn't an IAssignableIdentity, destroying proxy object.", new object[] { this.target_instance_id });
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
			else
			{
				global::Debug.LogWarningFormat("RestoreTargetFromInstanceID target ID {0} was not found, destroying proxy object.", new object[] { this.target_instance_id });
				Util.KDestroyGameObject(base.gameObject);
			}
		}
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x000B31C0 File Offset: 0x000B13C0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RestoreTargetFromInstanceID();
		if (this.target != null)
		{
			base.Subscribe<MinionAssignablesProxy>(-1585839766, MinionAssignablesProxy.OnAssignablesChangedDelegate);
			Game.Instance.assignmentManager.AddToAssignmentGroup("public", this);
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x000B31FC File Offset: 0x000B13FC
	private void OnQueueDestroyObject(object data)
	{
		Components.MinionAssignablesProxy.Remove(this);
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x000B3209 File Offset: 0x000B1409
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.assignmentManager.RemoveFromAllGroups(this);
		base.GetComponent<Ownables>().UnassignAll();
		base.GetComponent<Equipment>().UnequipAll();
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x000B3237 File Offset: 0x000B1437
	private void OnAssignablesChanged(object data)
	{
		if (!this.target.IsNull())
		{
			((KMonoBehaviour)this.target).Trigger(-1585839766, data);
		}
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x000B325C File Offset: 0x000B145C
	private void CheckTarget()
	{
		if (this.target == null)
		{
			KPrefabID instance = KPrefabIDTracker.Get().GetInstance(this.target_instance_id);
			if (instance != null)
			{
				this.target = instance.GetComponent<IAssignableIdentity>();
				if (this.target != null)
				{
					MinionIdentity minionIdentity = this.target as MinionIdentity;
					if (minionIdentity)
					{
						minionIdentity.ValidateProxy();
						return;
					}
					StoredMinionIdentity storedMinionIdentity = this.target as StoredMinionIdentity;
					if (storedMinionIdentity)
					{
						storedMinionIdentity.ValidateProxy();
					}
				}
			}
		}
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x000B32D4 File Offset: 0x000B14D4
	public List<Ownables> GetOwners()
	{
		this.CheckTarget();
		return this.target.GetOwners();
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x000B32E7 File Offset: 0x000B14E7
	public string GetProperName()
	{
		this.CheckTarget();
		return this.target.GetProperName();
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x000B32FA File Offset: 0x000B14FA
	public Ownables GetSoleOwner()
	{
		this.CheckTarget();
		return this.target.GetSoleOwner();
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x000B330D File Offset: 0x000B150D
	public bool HasOwner(Assignables owner)
	{
		this.CheckTarget();
		return this.target.HasOwner(owner);
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x000B3321 File Offset: 0x000B1521
	public int NumOwners()
	{
		this.CheckTarget();
		return this.target.NumOwners();
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x000B3334 File Offset: 0x000B1534
	public bool IsNull()
	{
		this.CheckTarget();
		return this.target.IsNull();
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x000B3348 File Offset: 0x000B1548
	public static Ref<MinionAssignablesProxy> InitAssignableProxy(Ref<MinionAssignablesProxy> assignableProxyRef, IAssignableIdentity source)
	{
		if (assignableProxyRef == null)
		{
			assignableProxyRef = new Ref<MinionAssignablesProxy>();
		}
		GameObject gameObject = ((KMonoBehaviour)source).gameObject;
		MinionAssignablesProxy minionAssignablesProxy = assignableProxyRef.Get();
		if (minionAssignablesProxy == null)
		{
			GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab(MinionAssignablesProxyConfig.ID), Grid.SceneLayer.NoLayer, null, 0);
			minionAssignablesProxy = gameObject2.GetComponent<MinionAssignablesProxy>();
			minionAssignablesProxy.SetTarget(source, gameObject);
			gameObject2.SetActive(true);
			assignableProxyRef.Set(minionAssignablesProxy);
		}
		else
		{
			minionAssignablesProxy.SetTarget(source, gameObject);
		}
		return assignableProxyRef;
	}

	// Token: 0x040012EA RID: 4842
	public List<Ownables> ownables;

	// Token: 0x040012EC RID: 4844
	[Serialize]
	private int target_instance_id = -1;

	// Token: 0x040012ED RID: 4845
	private bool slotsConfigured;

	// Token: 0x040012EE RID: 4846
	private static readonly EventSystem.IntraObjectHandler<MinionAssignablesProxy> OnAssignablesChangedDelegate = new EventSystem.IntraObjectHandler<MinionAssignablesProxy>(delegate(MinionAssignablesProxy component, object data)
	{
		component.OnAssignablesChanged(data);
	});

	// Token: 0x040012EF RID: 4847
	private static readonly EventSystem.IntraObjectHandler<MinionAssignablesProxy> OnQueueDestroyObjectDelegate = new EventSystem.IntraObjectHandler<MinionAssignablesProxy>(delegate(MinionAssignablesProxy component, object data)
	{
		component.OnQueueDestroyObject(data);
	});
}
