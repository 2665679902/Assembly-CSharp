using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Database;
using FMODUnity;
using Klei.AI;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000647 RID: 1607
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidTransferArm : StateMachineComponent<SolidTransferArm.SMInstance>, ISim1000ms, IRenderEveryTick
{
	// Token: 0x06002A9F RID: 10911 RVA: 0x000E0C14 File Offset: 0x000DEE14
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreConsumer.AddProvider(GlobalChoreProvider.Instance);
		this.choreConsumer.SetReach(this.pickupRange);
		Klei.AI.Attributes attributes = this.GetAttributes();
		if (attributes.Get(Db.Get().Attributes.CarryAmount) == null)
		{
			attributes.Add(Db.Get().Attributes.CarryAmount);
		}
		AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Attributes.CarryAmount.Id, this.max_carry_weight, base.gameObject.GetProperName(), false, false, true);
		this.GetAttributes().Add(attributeModifier);
		this.worker.usesMultiTool = false;
		this.storage.fxPrefix = Storage.FXPrefix.PickedUp;
		this.simRenderLoadBalance = false;
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x000E0CD8 File Offset: 0x000DEED8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		string text = component.name + ".arm";
		this.arm_go = new GameObject(text);
		this.arm_go.SetActive(false);
		this.arm_go.transform.parent = component.transform;
		this.looping_sounds = this.arm_go.AddComponent<LoopingSounds>();
		string sound = GlobalAssets.GetSound(this.rotateSoundName, false);
		this.rotateSound = RuntimeManager.PathToEventReference(sound);
		this.arm_go.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		this.arm_anim_ctrl = this.arm_go.AddComponent<KBatchedAnimController>();
		this.arm_anim_ctrl.AnimFiles = new KAnimFile[] { component.AnimFiles[0] };
		this.arm_anim_ctrl.initialAnim = "arm";
		this.arm_anim_ctrl.isMovable = true;
		this.arm_anim_ctrl.sceneLayer = Grid.SceneLayer.TransferArm;
		component.SetSymbolVisiblity("arm_target", false);
		bool flag;
		Vector3 vector = component.GetSymbolTransform(new HashedString("arm_target"), out flag).GetColumn(3);
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
		this.arm_go.transform.SetPosition(vector);
		this.arm_go.SetActive(true);
		this.link = new KAnimLink(component, this.arm_anim_ctrl);
		ChoreGroups choreGroups = Db.Get().ChoreGroups;
		for (int i = 0; i < choreGroups.Count; i++)
		{
			this.choreConsumer.SetPermittedByUser(choreGroups[i], true);
		}
		base.Subscribe<SolidTransferArm>(-592767678, SolidTransferArm.OnOperationalChangedDelegate);
		base.Subscribe<SolidTransferArm>(1745615042, SolidTransferArm.OnEndChoreDelegate);
		this.RotateArm(this.rotatable.GetRotatedOffset(Vector3.up), true, 0f);
		this.DropLeftovers();
		component.enabled = false;
		component.enabled = true;
		MinionGroupProber.Get().SetValidSerialNos(this, this.serial_no, this.serial_no);
		base.smi.StartSM();
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x000E0EE5 File Offset: 0x000DF0E5
	protected override void OnCleanUp()
	{
		MinionGroupProber.Get().ReleaseProber(this);
		base.OnCleanUp();
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x000E0EFC File Offset: 0x000DF0FC
	public static void BatchUpdate(List<UpdateBucketWithUpdater<ISim1000ms>.Entry> solid_transfer_arms, float time_delta)
	{
		SolidTransferArm.BatchUpdateContext batchUpdateContext = new SolidTransferArm.BatchUpdateContext(solid_transfer_arms);
		if (batchUpdateContext.solid_transfer_arms.Count == 0)
		{
			batchUpdateContext.Finish();
			return;
		}
		SolidTransferArm.cached_pickupables.Clear();
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair in Game.Instance.fetchManager.prefabIdToFetchables)
		{
			List<FetchManager.Fetchable> dataList = keyValuePair.Value.fetchables.GetDataList();
			SolidTransferArm.cached_pickupables.Capacity = Math.Max(SolidTransferArm.cached_pickupables.Capacity, SolidTransferArm.cached_pickupables.Count + dataList.Count);
			foreach (FetchManager.Fetchable fetchable in dataList)
			{
				SolidTransferArm.cached_pickupables.Add(new SolidTransferArm.CachedPickupable
				{
					pickupable = fetchable.pickupable,
					storage_cell = fetchable.pickupable.cachedCell
				});
			}
		}
		SolidTransferArm.batch_update_job.Reset(batchUpdateContext);
		int num = Math.Max(1, batchUpdateContext.solid_transfer_arms.Count / CPUBudget.coreCount);
		int num2 = Math.Min(batchUpdateContext.solid_transfer_arms.Count, CPUBudget.coreCount);
		for (int num3 = 0; num3 != num2; num3++)
		{
			int num4 = num3 * num;
			int num5 = ((num3 == num2 - 1) ? batchUpdateContext.solid_transfer_arms.Count : (num4 + num));
			SolidTransferArm.batch_update_job.Add(new SolidTransferArm.BatchUpdateTask(num4, num5));
		}
		GlobalJobManager.Run(SolidTransferArm.batch_update_job);
		for (int num6 = 0; num6 != SolidTransferArm.batch_update_job.Count; num6++)
		{
			SolidTransferArm.batch_update_job.GetWorkItem(num6).Finish();
		}
		batchUpdateContext.Finish();
		SolidTransferArm.batch_update_job.Reset(null);
		SolidTransferArm.cached_pickupables.Clear();
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x000E10F8 File Offset: 0x000DF2F8
	private void Sim()
	{
		Chore.Precondition.Context context = default(Chore.Precondition.Context);
		if (this.choreConsumer.FindNextChore(ref context))
		{
			if (context.chore is FetchChore)
			{
				this.choreDriver.SetChore(context);
				FetchChore fetchChore = context.chore as FetchChore;
				this.storage.DropUnlessMatching(fetchChore);
				this.arm_anim_ctrl.enabled = false;
				this.arm_anim_ctrl.enabled = true;
			}
			else
			{
				bool flag = false;
				string text = "I am but a lowly transfer arm. I should only acquire FetchChores: ";
				Chore chore = context.chore;
				global::Debug.Assert(flag, text + ((chore != null) ? chore.ToString() : null));
			}
		}
		this.operational.SetActive(this.choreDriver.HasChore(), false);
	}

	// Token: 0x06002AA4 RID: 10916 RVA: 0x000E11A0 File Offset: 0x000DF3A0
	public void Sim1000ms(float dt)
	{
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x000E11A4 File Offset: 0x000DF3A4
	private void UpdateArmAnim()
	{
		FetchAreaChore fetchAreaChore = this.choreDriver.GetCurrentChore() as FetchAreaChore;
		if (this.worker.workable && fetchAreaChore != null && this.rotation_complete)
		{
			this.StopRotateSound();
			this.SetArmAnim(fetchAreaChore.IsDelivering ? SolidTransferArm.ArmAnim.Drop : SolidTransferArm.ArmAnim.Pickup);
			return;
		}
		this.SetArmAnim(SolidTransferArm.ArmAnim.Idle);
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x000E1200 File Offset: 0x000DF400
	private bool AsyncUpdate(int cell, HashSet<int> workspace, GameObject game_object)
	{
		workspace.Clear();
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		for (int i = num2 - this.pickupRange; i < num2 + this.pickupRange + 1; i++)
		{
			for (int j = num - this.pickupRange; j < num + this.pickupRange + 1; j++)
			{
				int num3 = Grid.XYToCell(j, i);
				if (Grid.IsValidCell(num3) && Grid.IsPhysicallyAccessible(num, num2, j, i, true))
				{
					workspace.Add(num3);
				}
			}
		}
		bool flag = !this.reachableCells.SetEquals(workspace);
		if (flag)
		{
			this.reachableCells.Clear();
			this.reachableCells.UnionWith(workspace);
		}
		this.pickupables.Clear();
		foreach (SolidTransferArm.CachedPickupable cachedPickupable in SolidTransferArm.cached_pickupables)
		{
			if (Grid.GetCellRange(cell, cachedPickupable.storage_cell) <= this.pickupRange && this.IsPickupableRelevantToMyInterests(cachedPickupable.pickupable.KPrefabID, cachedPickupable.storage_cell) && cachedPickupable.pickupable.CouldBePickedUpByTransferArm(game_object))
			{
				this.pickupables.Add(cachedPickupable.pickupable);
			}
		}
		return flag;
	}

	// Token: 0x06002AA7 RID: 10919 RVA: 0x000E134C File Offset: 0x000DF54C
	private void IncrementSerialNo()
	{
		this.serial_no += 1;
		MinionGroupProber.Get().SetValidSerialNos(this, this.serial_no, this.serial_no);
		MinionGroupProber.Get().Occupy(this, this.serial_no, this.reachableCells);
	}

	// Token: 0x06002AA8 RID: 10920 RVA: 0x000E138B File Offset: 0x000DF58B
	public bool IsCellReachable(int cell)
	{
		return this.reachableCells.Contains(cell);
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x000E1399 File Offset: 0x000DF599
	private bool IsPickupableRelevantToMyInterests(KPrefabID prefabID, int storage_cell)
	{
		return prefabID.HasAnyTags(ref SolidTransferArm.tagBits) && this.IsCellReachable(storage_cell);
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x000E13B1 File Offset: 0x000DF5B1
	public Pickupable FindFetchTarget(Storage destination, FetchChore chore)
	{
		return FetchManager.FindFetchTarget(this.pickupables, destination, chore);
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x000E13C0 File Offset: 0x000DF5C0
	public void RenderEveryTick(float dt)
	{
		if (this.worker.workable)
		{
			Vector3 targetPoint = this.worker.workable.GetTargetPoint();
			targetPoint.z = 0f;
			Vector3 position = base.transform.GetPosition();
			position.z = 0f;
			Vector3 vector = Vector3.Normalize(targetPoint - position);
			this.RotateArm(vector, false, dt);
		}
		this.UpdateArmAnim();
	}

	// Token: 0x06002AAC RID: 10924 RVA: 0x000E1430 File Offset: 0x000DF630
	private void OnEndChore(object data)
	{
		this.DropLeftovers();
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x000E1438 File Offset: 0x000DF638
	private void DropLeftovers()
	{
		if (!this.storage.IsEmpty() && !this.choreDriver.HasChore())
		{
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x000E1478 File Offset: 0x000DF678
	private void SetArmAnim(SolidTransferArm.ArmAnim new_anim)
	{
		if (new_anim == this.arm_anim)
		{
			return;
		}
		this.arm_anim = new_anim;
		switch (this.arm_anim)
		{
		case SolidTransferArm.ArmAnim.Idle:
			this.arm_anim_ctrl.Play("arm", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		case SolidTransferArm.ArmAnim.Pickup:
			this.arm_anim_ctrl.Play("arm_pickup", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		case SolidTransferArm.ArmAnim.Drop:
			this.arm_anim_ctrl.Play("arm_drop", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		default:
			return;
		}
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x000E1512 File Offset: 0x000DF712
	private void OnOperationalChanged(object data)
	{
		if (!(bool)data)
		{
			if (this.choreDriver.HasChore())
			{
				this.choreDriver.StopChore();
			}
			this.UpdateArmAnim();
		}
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x000E153A File Offset: 0x000DF73A
	private void SetArmRotation(float rot)
	{
		this.arm_rot = rot;
		this.arm_go.transform.rotation = Quaternion.Euler(0f, 0f, this.arm_rot);
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x000E1568 File Offset: 0x000DF768
	private void RotateArm(Vector3 target_dir, bool warp, float dt)
	{
		float num = MathUtil.AngleSigned(Vector3.up, target_dir, Vector3.forward) - this.arm_rot;
		if (num < -180f)
		{
			num += 360f;
		}
		if (num > 180f)
		{
			num -= 360f;
		}
		if (!warp)
		{
			num = Mathf.Clamp(num, -this.turn_rate * dt, this.turn_rate * dt);
		}
		this.arm_rot += num;
		this.SetArmRotation(this.arm_rot);
		this.rotation_complete = Mathf.Approximately(num, 0f);
		if (!warp && !this.rotation_complete)
		{
			if (!this.rotateSoundPlaying)
			{
				this.StartRotateSound();
			}
			this.SetRotateSoundParameter(this.arm_rot);
			return;
		}
		this.StopRotateSound();
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x000E161F File Offset: 0x000DF81F
	private void StartRotateSound()
	{
		if (!this.rotateSoundPlaying)
		{
			this.looping_sounds.StartSound(this.rotateSound);
			this.rotateSoundPlaying = true;
		}
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x000E1642 File Offset: 0x000DF842
	private void SetRotateSoundParameter(float arm_rot)
	{
		if (this.rotateSoundPlaying)
		{
			this.looping_sounds.SetParameter(this.rotateSound, SolidTransferArm.HASH_ROTATION, arm_rot);
		}
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x000E1663 File Offset: 0x000DF863
	private void StopRotateSound()
	{
		if (this.rotateSoundPlaying)
		{
			this.looping_sounds.StopSound(this.rotateSound);
			this.rotateSoundPlaying = false;
		}
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x000E1685 File Offset: 0x000DF885
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x06002AB6 RID: 10934 RVA: 0x000E1687 File Offset: 0x000DF887
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x06002AB7 RID: 10935 RVA: 0x000E1689 File Offset: 0x000DF889
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x000E168B File Offset: 0x000DF88B
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x04001931 RID: 6449
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001932 RID: 6450
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001933 RID: 6451
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001934 RID: 6452
	[MyCmpAdd]
	private Worker worker;

	// Token: 0x04001935 RID: 6453
	[MyCmpAdd]
	private ChoreConsumer choreConsumer;

	// Token: 0x04001936 RID: 6454
	[MyCmpAdd]
	private ChoreDriver choreDriver;

	// Token: 0x04001937 RID: 6455
	public int pickupRange = 4;

	// Token: 0x04001938 RID: 6456
	private float max_carry_weight = 1000f;

	// Token: 0x04001939 RID: 6457
	private List<Pickupable> pickupables = new List<Pickupable>();

	// Token: 0x0400193A RID: 6458
	public static TagBits tagBits = new TagBits(STORAGEFILTERS.NOT_EDIBLE_SOLIDS.Concat(STORAGEFILTERS.FOOD).Concat(STORAGEFILTERS.PAYLOADS).ToArray<Tag>());

	// Token: 0x0400193B RID: 6459
	private KBatchedAnimController arm_anim_ctrl;

	// Token: 0x0400193C RID: 6460
	private GameObject arm_go;

	// Token: 0x0400193D RID: 6461
	private LoopingSounds looping_sounds;

	// Token: 0x0400193E RID: 6462
	private bool rotateSoundPlaying;

	// Token: 0x0400193F RID: 6463
	private string rotateSoundName = "TransferArm_rotate";

	// Token: 0x04001940 RID: 6464
	private EventReference rotateSound;

	// Token: 0x04001941 RID: 6465
	private KAnimLink link;

	// Token: 0x04001942 RID: 6466
	private float arm_rot = 45f;

	// Token: 0x04001943 RID: 6467
	private float turn_rate = 360f;

	// Token: 0x04001944 RID: 6468
	private bool rotation_complete;

	// Token: 0x04001945 RID: 6469
	private SolidTransferArm.ArmAnim arm_anim;

	// Token: 0x04001946 RID: 6470
	private HashSet<int> reachableCells = new HashSet<int>();

	// Token: 0x04001947 RID: 6471
	private static readonly EventSystem.IntraObjectHandler<SolidTransferArm> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<SolidTransferArm>(delegate(SolidTransferArm component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001948 RID: 6472
	private static readonly EventSystem.IntraObjectHandler<SolidTransferArm> OnEndChoreDelegate = new EventSystem.IntraObjectHandler<SolidTransferArm>(delegate(SolidTransferArm component, object data)
	{
		component.OnEndChore(data);
	});

	// Token: 0x04001949 RID: 6473
	private static WorkItemCollection<SolidTransferArm.BatchUpdateTask, SolidTransferArm.BatchUpdateContext> batch_update_job = new WorkItemCollection<SolidTransferArm.BatchUpdateTask, SolidTransferArm.BatchUpdateContext>();

	// Token: 0x0400194A RID: 6474
	private static List<SolidTransferArm.CachedPickupable> cached_pickupables = new List<SolidTransferArm.CachedPickupable>();

	// Token: 0x0400194B RID: 6475
	private short serial_no;

	// Token: 0x0400194C RID: 6476
	private static HashedString HASH_ROTATION = "rotation";

	// Token: 0x020012E8 RID: 4840
	private enum ArmAnim
	{
		// Token: 0x04005EFB RID: 24315
		Idle,
		// Token: 0x04005EFC RID: 24316
		Pickup,
		// Token: 0x04005EFD RID: 24317
		Drop
	}

	// Token: 0x020012E9 RID: 4841
	public class SMInstance : GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.GameInstance
	{
		// Token: 0x06007BEE RID: 31726 RVA: 0x002CD863 File Offset: 0x002CBA63
		public SMInstance(SolidTransferArm master)
			: base(master)
		{
		}
	}

	// Token: 0x020012EA RID: 4842
	public class States : GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm>
	{
		// Token: 0x06007BEF RID: 31727 RVA: 0x002CD86C File Offset: 0x002CBA6C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (SolidTransferArm.SMInstance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(SolidTransferArm.SMInstance smi)
			{
				smi.master.StopRotateSound();
			});
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (SolidTransferArm.SMInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.on.idle.PlayAnim("on").EventTransition(GameHashes.ActiveChanged, this.on.working, (SolidTransferArm.SMInstance smi) => smi.GetComponent<Operational>().IsActive);
			this.on.working.PlayAnim("working").EventTransition(GameHashes.ActiveChanged, this.on.idle, (SolidTransferArm.SMInstance smi) => !smi.GetComponent<Operational>().IsActive);
		}

		// Token: 0x04005EFE RID: 24318
		public StateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.BoolParameter transferring;

		// Token: 0x04005EFF RID: 24319
		public GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State off;

		// Token: 0x04005F00 RID: 24320
		public SolidTransferArm.States.ReadyStates on;

		// Token: 0x02001FFD RID: 8189
		public class ReadyStates : GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State
		{
			// Token: 0x04008E9C RID: 36508
			public GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State idle;

			// Token: 0x04008E9D RID: 36509
			public GameStateMachine<SolidTransferArm.States, SolidTransferArm.SMInstance, SolidTransferArm, object>.State working;
		}
	}

	// Token: 0x020012EB RID: 4843
	private class BatchUpdateContext
	{
		// Token: 0x06007BF1 RID: 31729 RVA: 0x002CD9D4 File Offset: 0x002CBBD4
		public BatchUpdateContext(List<UpdateBucketWithUpdater<ISim1000ms>.Entry> solid_transfer_arms)
		{
			this.solid_transfer_arms = ListPool<SolidTransferArm, SolidTransferArm.BatchUpdateContext>.Allocate();
			this.solid_transfer_arms.Capacity = solid_transfer_arms.Count;
			this.refreshed_reachable_cells = ListPool<bool, SolidTransferArm.BatchUpdateContext>.Allocate();
			this.refreshed_reachable_cells.Capacity = solid_transfer_arms.Count;
			this.cells = ListPool<int, SolidTransferArm.BatchUpdateContext>.Allocate();
			this.cells.Capacity = solid_transfer_arms.Count;
			this.game_objects = ListPool<GameObject, SolidTransferArm.BatchUpdateContext>.Allocate();
			this.game_objects.Capacity = solid_transfer_arms.Count;
			for (int num = 0; num != solid_transfer_arms.Count; num++)
			{
				UpdateBucketWithUpdater<ISim1000ms>.Entry entry = solid_transfer_arms[num];
				entry.lastUpdateTime = 0f;
				solid_transfer_arms[num] = entry;
				SolidTransferArm solidTransferArm = (SolidTransferArm)entry.data;
				if (solidTransferArm.operational.IsOperational)
				{
					this.solid_transfer_arms.Add(solidTransferArm);
					this.refreshed_reachable_cells.Add(false);
					this.cells.Add(Grid.PosToCell(solidTransferArm));
					this.game_objects.Add(solidTransferArm.gameObject);
				}
			}
		}

		// Token: 0x06007BF2 RID: 31730 RVA: 0x002CDAD8 File Offset: 0x002CBCD8
		public void Finish()
		{
			for (int num = 0; num != this.solid_transfer_arms.Count; num++)
			{
				if (this.refreshed_reachable_cells[num])
				{
					this.solid_transfer_arms[num].IncrementSerialNo();
				}
				this.solid_transfer_arms[num].Sim();
			}
			this.refreshed_reachable_cells.Recycle();
			this.cells.Recycle();
			this.game_objects.Recycle();
			this.solid_transfer_arms.Recycle();
		}

		// Token: 0x04005F01 RID: 24321
		public ListPool<SolidTransferArm, SolidTransferArm.BatchUpdateContext>.PooledList solid_transfer_arms;

		// Token: 0x04005F02 RID: 24322
		public ListPool<bool, SolidTransferArm.BatchUpdateContext>.PooledList refreshed_reachable_cells;

		// Token: 0x04005F03 RID: 24323
		public ListPool<int, SolidTransferArm.BatchUpdateContext>.PooledList cells;

		// Token: 0x04005F04 RID: 24324
		public ListPool<GameObject, SolidTransferArm.BatchUpdateContext>.PooledList game_objects;
	}

	// Token: 0x020012EC RID: 4844
	private struct BatchUpdateTask : IWorkItem<SolidTransferArm.BatchUpdateContext>
	{
		// Token: 0x06007BF3 RID: 31731 RVA: 0x002CDB57 File Offset: 0x002CBD57
		public BatchUpdateTask(int start, int end)
		{
			this.start = start;
			this.end = end;
			this.reachable_cells_workspace = HashSetPool<int, SolidTransferArm>.Allocate();
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x002CDB74 File Offset: 0x002CBD74
		public void Run(SolidTransferArm.BatchUpdateContext context)
		{
			for (int num = this.start; num != this.end; num++)
			{
				context.refreshed_reachable_cells[num] = context.solid_transfer_arms[num].AsyncUpdate(context.cells[num], this.reachable_cells_workspace, context.game_objects[num]);
			}
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x002CDBD2 File Offset: 0x002CBDD2
		public void Finish()
		{
			this.reachable_cells_workspace.Recycle();
		}

		// Token: 0x04005F05 RID: 24325
		private int start;

		// Token: 0x04005F06 RID: 24326
		private int end;

		// Token: 0x04005F07 RID: 24327
		private HashSetPool<int, SolidTransferArm>.PooledHashSet reachable_cells_workspace;
	}

	// Token: 0x020012ED RID: 4845
	public struct CachedPickupable
	{
		// Token: 0x04005F08 RID: 24328
		public Pickupable pickupable;

		// Token: 0x04005F09 RID: 24329
		public int storage_cell;
	}
}
