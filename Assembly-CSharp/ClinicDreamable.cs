using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200045B RID: 1115
[AddComponentMenu("KMonoBehaviour/Workable/Clinic Dreamable")]
public class ClinicDreamable : Workable
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06001885 RID: 6277 RVA: 0x00081E5B File Offset: 0x0008005B
	// (set) Token: 0x06001886 RID: 6278 RVA: 0x00081E63 File Offset: 0x00080063
	public bool DreamIsDisturbed { get; private set; }

	// Token: 0x06001887 RID: 6279 RVA: 0x00081E6C File Offset: 0x0008006C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.resetProgressOnStop = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Dreaming;
		this.workingStatusItem = null;
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x00081E98 File Offset: 0x00080098
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (ClinicDreamable.dreamJournalPrefab == null)
		{
			ClinicDreamable.dreamJournalPrefab = Assets.GetPrefab(DreamJournalConfig.ID);
			ClinicDreamable.sleepClinic = Db.Get().effects.Get("SleepClinic");
		}
		this.equippable = base.GetComponent<Equippable>();
		global::Debug.Assert(this.equippable != null);
		EquipmentDef def = this.equippable.def;
		def.OnEquipCallBack = (Action<Equippable>)Delegate.Combine(def.OnEquipCallBack, new Action<Equippable>(this.OnEquipPajamas));
		EquipmentDef def2 = this.equippable.def;
		def2.OnUnequipCallBack = (Action<Equippable>)Delegate.Combine(def2.OnUnequipCallBack, new Action<Equippable>(this.OnUnequipPajamas));
		this.OnEquipPajamas(this.equippable);
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x00081F64 File Offset: 0x00080164
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.equippable == null)
		{
			return;
		}
		EquipmentDef def = this.equippable.def;
		def.OnEquipCallBack = (Action<Equippable>)Delegate.Remove(def.OnEquipCallBack, new Action<Equippable>(this.OnEquipPajamas));
		EquipmentDef def2 = this.equippable.def;
		def2.OnUnequipCallBack = (Action<Equippable>)Delegate.Remove(def2.OnUnequipCallBack, new Action<Equippable>(this.OnUnequipPajamas));
	}

	// Token: 0x0600188A RID: 6282 RVA: 0x00081FE0 File Offset: 0x000801E0
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.GetPercentComplete() >= 1f)
		{
			Vector3 position = this.dreamer.transform.position;
			position.y += 1f;
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			Util.KInstantiate(ClinicDreamable.dreamJournalPrefab, position, Quaternion.identity, null, null, true, 0).SetActive(true);
			this.workTimeRemaining = this.GetWorkTime();
		}
		return false;
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x00082058 File Offset: 0x00080258
	public void OnEquipPajamas(Equippable eq)
	{
		if (this.equippable == null || this.equippable != eq)
		{
			return;
		}
		MinionAssignablesProxy minionAssignablesProxy = this.equippable.assignee as MinionAssignablesProxy;
		if (minionAssignablesProxy == null)
		{
			return;
		}
		if (minionAssignablesProxy.target is StoredMinionIdentity)
		{
			return;
		}
		GameObject targetGameObject = minionAssignablesProxy.GetTargetGameObject();
		this.effects = targetGameObject.GetComponent<Effects>();
		this.dreamer = targetGameObject.GetComponent<ChoreDriver>();
		this.selectable = targetGameObject.GetComponent<KSelectable>();
		this.dreamer.Subscribe(-1283701846, new Action<object>(this.WorkerStartedSleeping));
		this.dreamer.Subscribe(-2090444759, new Action<object>(this.WorkerStoppedSleeping));
		this.effects.Add(ClinicDreamable.sleepClinic, true);
		this.selectable.AddStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Wearing, null);
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x00082140 File Offset: 0x00080340
	public void OnUnequipPajamas(Equippable eq)
	{
		if (this.dreamer == null)
		{
			return;
		}
		if (this.equippable == null || this.equippable != eq)
		{
			return;
		}
		this.dreamer.Unsubscribe(-1283701846, new Action<object>(this.WorkerStartedSleeping));
		this.dreamer.Unsubscribe(-2090444759, new Action<object>(this.WorkerStoppedSleeping));
		this.selectable.RemoveStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Wearing, false);
		this.selectable.RemoveStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Sleeping, false);
		this.effects.Remove(ClinicDreamable.sleepClinic.Id);
		this.StopDreamingThought();
		this.dreamer = null;
		this.selectable = null;
		this.effects = null;
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x0008221C File Offset: 0x0008041C
	public void WorkerStartedSleeping(object data)
	{
		SleepChore sleepChore = this.dreamer.GetCurrentChore() as SleepChore;
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context = sleepChore.smi.sm.isDisturbedByLight.GetContext(sleepChore.smi);
		context.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context2 = sleepChore.smi.sm.isDisturbedByMovement.GetContext(sleepChore.smi);
		context2.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context2.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context3 = sleepChore.smi.sm.isDisturbedByNoise.GetContext(sleepChore.smi);
		context3.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context3.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context4 = sleepChore.smi.sm.isScaredOfDark.GetContext(sleepChore.smi);
		context4.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Combine(context4.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		this.sleepable = data as Sleepable;
		this.sleepable.Dreamable = this;
		base.StartWork(this.sleepable.worker);
		this.progressBar.Retarget(this.sleepable.gameObject);
		this.selectable.AddStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Sleeping, this);
		this.StartDreamingThought();
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x0008238C File Offset: 0x0008058C
	public void WorkerStoppedSleeping(object data)
	{
		this.selectable.RemoveStatusItem(Db.Get().DuplicantStatusItems.MegaBrainTank_Pajamas_Sleeping, false);
		SleepChore sleepChore = this.dreamer.GetCurrentChore() as SleepChore;
		if (!sleepChore.IsNullOrDestroyed() && !sleepChore.smi.IsNullOrDestroyed() && !sleepChore.smi.sm.IsNullOrDestroyed())
		{
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context = sleepChore.smi.sm.isDisturbedByLight.GetContext(sleepChore.smi);
			context.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context2 = sleepChore.smi.sm.isDisturbedByMovement.GetContext(sleepChore.smi);
			context2.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context2.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context3 = sleepChore.smi.sm.isDisturbedByNoise.GetContext(sleepChore.smi);
			context3.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context3.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
			StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.Parameter<bool>.Context context4 = sleepChore.smi.sm.isScaredOfDark.GetContext(sleepChore.smi);
			context4.onDirty = (Action<SleepChore.StatesInstance>)Delegate.Remove(context4.onDirty, new Action<SleepChore.StatesInstance>(this.OnSleepDisturbed));
		}
		this.StopDreamingThought();
		this.DreamIsDisturbed = false;
		if (base.worker != null)
		{
			base.StopWork(base.worker, false);
		}
		if (this.sleepable != null)
		{
			this.sleepable.Dreamable = null;
			this.sleepable = null;
		}
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x00082530 File Offset: 0x00080730
	private void OnSleepDisturbed(SleepChore.StatesInstance smi)
	{
		SleepChore sleepChore = this.dreamer.GetCurrentChore() as SleepChore;
		bool flag = sleepChore.smi.sm.isDisturbedByLight.Get(sleepChore.smi);
		flag |= sleepChore.smi.sm.isDisturbedByMovement.Get(sleepChore.smi);
		flag |= sleepChore.smi.sm.isDisturbedByNoise.Get(sleepChore.smi);
		flag |= sleepChore.smi.sm.isScaredOfDark.Get(sleepChore.smi);
		this.DreamIsDisturbed = flag;
		if (flag)
		{
			this.StopDreamingThought();
		}
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x000825D4 File Offset: 0x000807D4
	private void StartDreamingThought()
	{
		if (this.dreamer != null && !this.HasStartedThoughts_Dreaming)
		{
			this.dreamer.GetSMI<Dreamer.Instance>().SetDream(Db.Get().Dreams.CommonDream);
			this.dreamer.GetSMI<Dreamer.Instance>().StartDreaming();
			this.HasStartedThoughts_Dreaming = true;
		}
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x0008262D File Offset: 0x0008082D
	private void StopDreamingThought()
	{
		if (this.dreamer != null && this.HasStartedThoughts_Dreaming)
		{
			this.dreamer.GetSMI<Dreamer.Instance>().StopDreaming();
			this.HasStartedThoughts_Dreaming = false;
		}
	}

	// Token: 0x04000DB2 RID: 3506
	private static GameObject dreamJournalPrefab;

	// Token: 0x04000DB3 RID: 3507
	private static Effect sleepClinic;

	// Token: 0x04000DB4 RID: 3508
	public bool HasStartedThoughts_Dreaming;

	// Token: 0x04000DB6 RID: 3510
	private ChoreDriver dreamer;

	// Token: 0x04000DB7 RID: 3511
	private Equippable equippable;

	// Token: 0x04000DB8 RID: 3512
	private Effects effects;

	// Token: 0x04000DB9 RID: 3513
	private Sleepable sleepable;

	// Token: 0x04000DBA RID: 3514
	private KSelectable selectable;

	// Token: 0x04000DBB RID: 3515
	private HashedString dreamAnimName = "portal rocket comp";
}
