using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020005CA RID: 1482
public class Grave : StateMachineComponent<Grave.StatesInstance>
{
	// Token: 0x060024E0 RID: 9440 RVA: 0x000C7616 File Offset: 0x000C5816
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Grave>(-1697596308, Grave.OnStorageChangedDelegate);
		this.epitaphIdx = UnityEngine.Random.Range(0, int.MaxValue);
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x000C7640 File Offset: 0x000C5840
	protected override void OnSpawn()
	{
		base.GetComponent<Storage>().SetOffsets(Grave.DELIVERY_OFFSETS);
		Storage component = base.GetComponent<Storage>();
		Storage storage = component;
		storage.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(storage.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkEvent));
		KAnimFile anim = Assets.GetAnim("anim_bury_dupe_kanim");
		int num = 0;
		KAnim.Anim anim2;
		for (;;)
		{
			anim2 = anim.GetData().GetAnim(num);
			if (anim2 == null)
			{
				goto IL_8F;
			}
			if (anim2.name == "working_pre")
			{
				break;
			}
			num++;
		}
		float num2 = (float)(anim2.numFrames - 3) / anim2.frameRate;
		component.SetWorkTime(num2);
		IL_8F:
		base.OnSpawn();
		base.smi.StartSM();
		Components.Graves.Add(this);
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x000C76F8 File Offset: 0x000C58F8
	protected override void OnCleanUp()
	{
		Components.Graves.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x000C770C File Offset: 0x000C590C
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			this.graveName = gameObject.name;
			Util.KDestroyGameObject(gameObject);
		}
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x000C773B File Offset: 0x000C593B
	private void OnWorkEvent(Workable workable, Workable.WorkableEvent evt)
	{
	}

	// Token: 0x04001541 RID: 5441
	[Serialize]
	public string graveName;

	// Token: 0x04001542 RID: 5442
	[Serialize]
	public int epitaphIdx;

	// Token: 0x04001543 RID: 5443
	[Serialize]
	public float burialTime = -1f;

	// Token: 0x04001544 RID: 5444
	private static readonly CellOffset[] DELIVERY_OFFSETS = new CellOffset[1];

	// Token: 0x04001545 RID: 5445
	private static readonly EventSystem.IntraObjectHandler<Grave> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<Grave>(delegate(Grave component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x02001213 RID: 4627
	public class StatesInstance : GameStateMachine<Grave.States, Grave.StatesInstance, Grave, object>.GameInstance
	{
		// Token: 0x060078E7 RID: 30951 RVA: 0x002C1001 File Offset: 0x002BF201
		public StatesInstance(Grave master)
			: base(master)
		{
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x002C100C File Offset: 0x002BF20C
		public void CreateFetchTask()
		{
			this.chore = new FetchChore(Db.Get().ChoreTypes.FetchCritical, base.GetComponent<Storage>(), 1f, new HashSet<Tag> { GameTags.Minion }, FetchChore.MatchCriteria.MatchID, GameTags.Corpse, null, null, true, null, null, null, Operational.State.Operational, 0);
			this.chore.allowMultifetch = false;
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x002C1069 File Offset: 0x002BF269
		public void CancelFetchTask()
		{
			this.chore.Cancel("Exit State");
			this.chore = null;
		}

		// Token: 0x04005CF3 RID: 23795
		private FetchChore chore;
	}

	// Token: 0x02001214 RID: 4628
	public class States : GameStateMachine<Grave.States, Grave.StatesInstance, Grave>
	{
		// Token: 0x060078EA RID: 30954 RVA: 0x002C1084 File Offset: 0x002BF284
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.empty.PlayAnim("open").Enter("CreateFetchTask", delegate(Grave.StatesInstance smi)
			{
				smi.CreateFetchTask();
			}).Exit("CancelFetchTask", delegate(Grave.StatesInstance smi)
			{
				smi.CancelFetchTask();
			})
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GraveEmpty, null)
				.EventTransition(GameHashes.OnStorageChange, this.full, null);
			this.full.PlayAnim("closed").ToggleMainStatusItem(Db.Get().BuildingStatusItems.Grave, null).Enter(delegate(Grave.StatesInstance smi)
			{
				if (smi.master.burialTime < 0f)
				{
					smi.master.burialTime = GameClock.Instance.GetTime();
				}
			});
		}

		// Token: 0x04005CF4 RID: 23796
		public GameStateMachine<Grave.States, Grave.StatesInstance, Grave, object>.State empty;

		// Token: 0x04005CF5 RID: 23797
		public GameStateMachine<Grave.States, Grave.StatesInstance, Grave, object>.State full;
	}
}
