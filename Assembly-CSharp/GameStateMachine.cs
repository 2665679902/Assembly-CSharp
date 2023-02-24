using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public abstract class GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> where StateMachineType : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> where StateMachineInstanceType : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameInstance where MasterType : IStateMachineTarget
{
	// Token: 0x060014FE RID: 5374 RVA: 0x0006DD37 File Offset: 0x0006BF37
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.InitializeStates(out default_state);
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x0006DD40 File Offset: 0x0006BF40
	public static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback Not(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback transition_cb)
	{
		return (StateMachineInstanceType smi) => !transition_cb(smi);
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x0006DD59 File Offset: 0x0006BF59
	public override void BindStates()
	{
		base.BindState(null, this.root, "root");
		base.BindStates(this.root, this);
	}

	// Token: 0x04000BC1 RID: 3009
	public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State root = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State();

	// Token: 0x04000BC2 RID: 3010
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>.Callback IsFalse = (StateMachineInstanceType smi, bool p) => !p;

	// Token: 0x04000BC3 RID: 3011
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>.Callback IsTrue = (StateMachineInstanceType smi, bool p) => p;

	// Token: 0x04000BC4 RID: 3012
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsZero = (StateMachineInstanceType smi, float p) => p == 0f;

	// Token: 0x04000BC5 RID: 3013
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTZero = (StateMachineInstanceType smi, float p) => p < 0f;

	// Token: 0x04000BC6 RID: 3014
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTEZero = (StateMachineInstanceType smi, float p) => p <= 0f;

	// Token: 0x04000BC7 RID: 3015
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTZero = (StateMachineInstanceType smi, float p) => p > 0f;

	// Token: 0x04000BC8 RID: 3016
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTEZero = (StateMachineInstanceType smi, float p) => p >= 0f;

	// Token: 0x04000BC9 RID: 3017
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsOne = (StateMachineInstanceType smi, float p) => p == 1f;

	// Token: 0x04000BCA RID: 3018
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTOne = (StateMachineInstanceType smi, float p) => p < 1f;

	// Token: 0x04000BCB RID: 3019
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTEOne = (StateMachineInstanceType smi, float p) => p <= 1f;

	// Token: 0x04000BCC RID: 3020
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTOne = (StateMachineInstanceType smi, float p) => p > 1f;

	// Token: 0x04000BCD RID: 3021
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTEOne = (StateMachineInstanceType smi, float p) => p >= 1f;

	// Token: 0x04000BCE RID: 3022
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>.Callback IsNotNull = (StateMachineInstanceType smi, GameObject p) => p != null;

	// Token: 0x04000BCF RID: 3023
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>.Callback IsNull = (StateMachineInstanceType smi, GameObject p) => p == null;

	// Token: 0x02001003 RID: 4099
	public class PreLoopPostState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x04005637 RID: 22071
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State pre;

		// Token: 0x04005638 RID: 22072
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State loop;

		// Token: 0x04005639 RID: 22073
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State pst;
	}

	// Token: 0x02001004 RID: 4100
	public class WorkingState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x0400563A RID: 22074
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State waiting;

		// Token: 0x0400563B RID: 22075
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State working_pre;

		// Token: 0x0400563C RID: 22076
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State working_loop;

		// Token: 0x0400563D RID: 22077
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State working_pst;
	}

	// Token: 0x02001005 RID: 4101
	public class GameInstance : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance
	{
		// Token: 0x06007130 RID: 28976 RVA: 0x002A8A39 File Offset: 0x002A6C39
		public void Queue(string anim, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			base.smi.GetComponent<KBatchedAnimController>().Queue(anim, mode, 1f, 0f);
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x002A8A61 File Offset: 0x002A6C61
		public void Play(string anim, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			base.smi.GetComponent<KBatchedAnimController>().Play(anim, mode, 1f, 0f);
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x002A8A89 File Offset: 0x002A6C89
		public GameInstance(MasterType master, DefType def)
			: base(master)
		{
			base.def = def;
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x002A8A99 File Offset: 0x002A6C99
		public GameInstance(MasterType master)
			: base(master)
		{
		}
	}

	// Token: 0x02001006 RID: 4102
	public class TagTransitionData : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition
	{
		// Token: 0x06007134 RID: 28980 RVA: 0x002A8AA2 File Offset: 0x002A6CA2
		public TagTransitionData(string name, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State source_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, int idx, Tag[] tags, bool on_remove, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target, Func<StateMachineInstanceType, Tag[]> tags_callback = null)
			: base(name, source_state, target_state, idx, null)
		{
			this.tags = tags;
			this.onRemove = on_remove;
			this.target = target;
			this.tags_callback = tags_callback;
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x002A8AD0 File Offset: 0x002A6CD0
		public override void Evaluate(StateMachine.Instance smi)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			if (!this.onRemove)
			{
				if (!this.HasAllTags(stateMachineInstanceType))
				{
					return;
				}
			}
			else if (this.HasAnyTags(stateMachineInstanceType))
			{
				return;
			}
			this.ExecuteTransition(stateMachineInstanceType);
		}

		// Token: 0x06007136 RID: 28982 RVA: 0x002A8B1A File Offset: 0x002A6D1A
		private bool HasAllTags(StateMachineInstanceType smi)
		{
			return this.target.Get(smi).GetComponent<KPrefabID>().HasAllTags((this.tags_callback != null) ? this.tags_callback(smi) : this.tags);
		}

		// Token: 0x06007137 RID: 28983 RVA: 0x002A8B4E File Offset: 0x002A6D4E
		private bool HasAnyTags(StateMachineInstanceType smi)
		{
			return this.target.Get(smi).GetComponent<KPrefabID>().HasAnyTags((this.tags_callback != null) ? this.tags_callback(smi) : this.tags);
		}

		// Token: 0x06007138 RID: 28984 RVA: 0x002A8B82 File Offset: 0x002A6D82
		private void ExecuteTransition(StateMachineInstanceType smi)
		{
			if (this.is_executing)
			{
				return;
			}
			this.is_executing = true;
			smi.GoTo(this.targetState);
			this.is_executing = false;
		}

		// Token: 0x06007139 RID: 28985 RVA: 0x002A8BAC File Offset: 0x002A6DAC
		private void OnCallback(StateMachineInstanceType smi)
		{
			if (this.target.Get(smi) == null)
			{
				return;
			}
			if (!this.onRemove)
			{
				if (!this.HasAllTags(smi))
				{
					return;
				}
			}
			else if (this.HasAnyTags(smi))
			{
				return;
			}
			this.ExecuteTransition(smi);
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x002A8BE8 File Offset: 0x002A6DE8
		public override StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
		{
			StateMachineInstanceType smi_internal = smi as StateMachineInstanceType;
			global::Debug.Assert(smi_internal != null);
			StateMachine.BaseTransition.Context context = base.Register(smi_internal);
			context.handlerId = this.target.Get(smi_internal).Subscribe(-1582839653, delegate(object data)
			{
				this.OnCallback(smi_internal);
			});
			return context;
		}

		// Token: 0x0600713B RID: 28987 RVA: 0x002A8C68 File Offset: 0x002A6E68
		public override void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context context)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			base.Unregister(stateMachineInstanceType, context);
			if (this.target.Get(stateMachineInstanceType) != null)
			{
				this.target.Get(stateMachineInstanceType).Unsubscribe(context.handlerId);
			}
		}

		// Token: 0x0400563E RID: 22078
		private Tag[] tags;

		// Token: 0x0400563F RID: 22079
		private bool onRemove;

		// Token: 0x04005640 RID: 22080
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target;

		// Token: 0x04005641 RID: 22081
		private bool is_executing;

		// Token: 0x04005642 RID: 22082
		private Func<StateMachineInstanceType, Tag[]> tags_callback;
	}

	// Token: 0x02001007 RID: 4103
	public class EventTransitionData : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition
	{
		// Token: 0x0600713C RID: 28988 RVA: 0x002A8CC7 File Offset: 0x002A6EC7
		public EventTransitionData(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State source_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, int idx, GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target)
			: base(evt.ToString(), source_state, target_state, idx, condition)
		{
			this.evtId = evt;
			this.target = target;
			this.globalEventSystemCallback = global_event_system_callback;
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x002A8CFC File Offset: 0x002A6EFC
		public override void Evaluate(StateMachine.Instance smi)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			if (this.condition != null && this.condition(stateMachineInstanceType))
			{
				this.ExecuteTransition(stateMachineInstanceType);
			}
		}

		// Token: 0x0600713E RID: 28990 RVA: 0x002A8D40 File Offset: 0x002A6F40
		private void ExecuteTransition(StateMachineInstanceType smi)
		{
			smi.GoTo(this.targetState);
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x002A8D53 File Offset: 0x002A6F53
		private void OnCallback(StateMachineInstanceType smi)
		{
			if (this.condition == null || this.condition(smi))
			{
				this.ExecuteTransition(smi);
			}
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x002A8D74 File Offset: 0x002A6F74
		public override StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
		{
			StateMachineInstanceType smi_internal = smi as StateMachineInstanceType;
			global::Debug.Assert(smi_internal != null);
			StateMachine.BaseTransition.Context context = base.Register(smi_internal);
			Action<object> action = delegate(object d)
			{
				this.OnCallback(smi_internal);
			};
			GameObject gameObject;
			if (this.globalEventSystemCallback != null)
			{
				gameObject = this.globalEventSystemCallback(smi_internal).gameObject;
			}
			else
			{
				gameObject = this.target.Get(smi_internal);
				if (gameObject == null)
				{
					throw new InvalidOperationException("TargetParameter: " + this.target.name + " is null");
				}
			}
			context.handlerId = gameObject.Subscribe((int)this.evtId, action);
			return context;
		}

		// Token: 0x06007141 RID: 28993 RVA: 0x002A8E44 File Offset: 0x002A7044
		public override void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context context)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			base.Unregister(stateMachineInstanceType, context);
			GameObject gameObject = null;
			if (this.globalEventSystemCallback != null)
			{
				KMonoBehaviour kmonoBehaviour = this.globalEventSystemCallback(stateMachineInstanceType);
				if (kmonoBehaviour != null)
				{
					gameObject = kmonoBehaviour.gameObject;
				}
			}
			else
			{
				gameObject = this.target.Get(stateMachineInstanceType);
			}
			if (gameObject != null)
			{
				gameObject.Unsubscribe(context.handlerId);
			}
		}

		// Token: 0x04005643 RID: 22083
		private GameHashes evtId;

		// Token: 0x04005644 RID: 22084
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target;

		// Token: 0x04005645 RID: 22085
		private Func<StateMachineInstanceType, KMonoBehaviour> globalEventSystemCallback;
	}

	// Token: 0x02001008 RID: 4104
	public new class State : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x06007142 RID: 28994 RVA: 0x002A8EC4 File Offset: 0x002A70C4
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter GetStateTarget()
		{
			if (this.stateTarget != null)
			{
				return this.stateTarget;
			}
			if (this.parent != null)
			{
				return ((GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)this.parent).GetStateTarget();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter targetParameter = this.sm.stateTarget;
			if (targetParameter == null)
			{
				return this.sm.masterTarget;
			}
			return targetParameter;
		}

		// Token: 0x06007143 RID: 28995 RVA: 0x002A8F20 File Offset: 0x002A7120
		public int CreateDataTableEntry()
		{
			StateMachineType stateMachineType = this.sm;
			int dataTableSize = stateMachineType.dataTableSize;
			stateMachineType.dataTableSize = dataTableSize + 1;
			return dataTableSize;
		}

		// Token: 0x06007144 RID: 28996 RVA: 0x002A8F48 File Offset: 0x002A7148
		public int CreateUpdateTableEntry()
		{
			StateMachineType stateMachineType = this.sm;
			int updateTableSize = stateMachineType.updateTableSize;
			stateMachineType.updateTableSize = updateTableSize + 1;
			return updateTableSize;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06007145 RID: 28997 RVA: 0x002A8F70 File Offset: 0x002A7170
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State root
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x002A8F73 File Offset: 0x002A7173
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoNothing()
		{
			return this;
		}

		// Token: 0x06007147 RID: 28999 RVA: 0x002A8F78 File Offset: 0x002A7178
		private static List<StateMachine.Action> AddAction(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback, List<StateMachine.Action> actions, bool add_to_end)
		{
			if (actions == null)
			{
				actions = new List<StateMachine.Action>();
			}
			StateMachine.Action action = new StateMachine.Action(name, callback);
			if (add_to_end)
			{
				actions.Add(action);
			}
			else
			{
				actions.Insert(0, action);
			}
			return actions;
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06007148 RID: 29000 RVA: 0x002A8FAD File Offset: 0x002A71AD
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State master
		{
			get
			{
				this.stateTarget = this.sm.masterTarget;
				return this;
			}
		}

		// Token: 0x06007149 RID: 29001 RVA: 0x002A8FC6 File Offset: 0x002A71C6
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Target(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target)
		{
			this.stateTarget = target;
			return this;
		}

		// Token: 0x0600714A RID: 29002 RVA: 0x002A8FD0 File Offset: 0x002A71D0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Update(Action<StateMachineInstanceType, float> callback, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			return this.Update(this.sm.name + "." + this.name, callback, update_rate, load_balance);
		}

		// Token: 0x0600714B RID: 29003 RVA: 0x002A8FFB File Offset: 0x002A71FB
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BatchUpdate(UpdateBucketWithUpdater<StateMachineInstanceType>.BatchUpdateDelegate batch_update, UpdateRate update_rate = UpdateRate.SIM_200ms)
		{
			return this.BatchUpdate(this.sm.name + "." + this.name, batch_update, update_rate);
		}

		// Token: 0x0600714C RID: 29004 RVA: 0x002A9025 File Offset: 0x002A7225
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Enter(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.Enter("Enter", callback);
		}

		// Token: 0x0600714D RID: 29005 RVA: 0x002A9033 File Offset: 0x002A7233
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Exit(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.Exit("Exit", callback);
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x002A9044 File Offset: 0x002A7244
		private GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InternalUpdate(string name, UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater bucket_updater, UpdateRate update_rate, bool load_balance, UpdateBucketWithUpdater<StateMachineInstanceType>.BatchUpdateDelegate batch_update = null)
		{
			int num = this.CreateUpdateTableEntry();
			if (this.updateActions == null)
			{
				this.updateActions = new List<StateMachine.UpdateAction>();
			}
			StateMachine.UpdateAction updateAction = default(StateMachine.UpdateAction);
			updateAction.updateTableIdx = num;
			updateAction.updateRate = update_rate;
			updateAction.updater = bucket_updater;
			int num2 = 1;
			if (load_balance)
			{
				num2 = Singleton<StateMachineUpdater>.Instance.GetFrameCount(update_rate);
			}
			updateAction.buckets = new StateMachineUpdater.BaseUpdateBucket[num2];
			for (int i = 0; i < num2; i++)
			{
				UpdateBucketWithUpdater<StateMachineInstanceType> updateBucketWithUpdater = new UpdateBucketWithUpdater<StateMachineInstanceType>(name);
				updateBucketWithUpdater.batch_update_delegate = batch_update;
				Singleton<StateMachineUpdater>.Instance.AddBucket(update_rate, updateBucketWithUpdater);
				updateAction.buckets[i] = updateBucketWithUpdater;
			}
			this.updateActions.Add(updateAction);
			return this;
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x002A90EC File Offset: 0x002A72EC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State UpdateTransition(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State destination_state, Func<StateMachineInstanceType, float, bool> callback, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			Action<StateMachineInstanceType, float> checkCallback = delegate(StateMachineInstanceType smi, float dt)
			{
				if (callback(smi, dt))
				{
					smi.GoTo(destination_state);
				}
			};
			this.Enter(delegate(StateMachineInstanceType smi)
			{
				checkCallback(smi, 0f);
			});
			this.Update(checkCallback, update_rate, load_balance);
			return this;
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x002A9143 File Offset: 0x002A7343
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Update(string name, Action<StateMachineInstanceType, float> callback, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			return this.InternalUpdate(name, new BucketUpdater<StateMachineInstanceType>(callback), update_rate, load_balance, null);
		}

		// Token: 0x06007151 RID: 29009 RVA: 0x002A9156 File Offset: 0x002A7356
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BatchUpdate(string name, UpdateBucketWithUpdater<StateMachineInstanceType>.BatchUpdateDelegate batch_update, UpdateRate update_rate = UpdateRate.SIM_200ms)
		{
			return this.InternalUpdate(name, null, update_rate, false, batch_update);
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x002A9163 File Offset: 0x002A7363
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State FastUpdate(string name, UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater updater, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			return this.InternalUpdate(name, updater, update_rate, load_balance, null);
		}

		// Token: 0x06007153 RID: 29011 RVA: 0x002A9171 File Offset: 0x002A7371
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Enter(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			this.enterActions = GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.AddAction(name, callback, this.enterActions, true);
			return this;
		}

		// Token: 0x06007154 RID: 29012 RVA: 0x002A9188 File Offset: 0x002A7388
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Exit(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			this.exitActions = GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.AddAction(name, callback, this.exitActions, false);
			return this;
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x002A91A0 File Offset: 0x002A73A0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Toggle(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback enter_callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback exit_callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleEnter(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.dataTable[data_idx] = GameStateMachineHelper.HasToggleEnteredFlag;
				enter_callback(smi);
			});
			this.Exit("ToggleExit(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					smi.dataTable[data_idx] = null;
					exit_callback(smi);
				}
			});
			return this;
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x002A9214 File Offset: 0x002A7414
		private void Break(StateMachineInstanceType smi)
		{
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x002A9216 File Offset: 0x002A7416
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BreakOnEnter()
		{
			return this.Enter(delegate(StateMachineInstanceType smi)
			{
				this.Break(smi);
			});
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x002A922A File Offset: 0x002A742A
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BreakOnExit()
		{
			return this.Exit(delegate(StateMachineInstanceType smi)
			{
				this.Break(smi);
			});
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x002A9240 File Offset: 0x002A7440
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State AddEffect(string effect_name)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(effect_name, true);
			});
			return this;
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x002A9290 File Offset: 0x002A7490
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAnims(Func<StateMachineInstanceType, KAnimFile> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableAnims()", delegate(StateMachineInstanceType smi)
			{
				KAnimFile kanimFile = chooser_callback(smi);
				if (kanimFile == null)
				{
					return;
				}
				state_target.Get<KAnimControllerBase>(smi).AddAnimOverrides(kanimFile, 0f);
			});
			this.Exit("Disableanims()", delegate(StateMachineInstanceType smi)
			{
				KAnimFile kanimFile2 = chooser_callback(smi);
				if (kanimFile2 == null)
				{
					return;
				}
				state_target.Get<KAnimControllerBase>(smi).RemoveAnimOverrides(kanimFile2);
			});
			return this;
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x002A92E8 File Offset: 0x002A74E8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAnims(Func<StateMachineInstanceType, HashedString> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableAnims()", delegate(StateMachineInstanceType smi)
			{
				HashedString hashedString = chooser_callback(smi);
				if (hashedString == null)
				{
					return;
				}
				if (hashedString.IsValid)
				{
					KAnimFile anim = Assets.GetAnim(hashedString);
					if (anim == null)
					{
						string text = "Missing anims: ";
						HashedString hashedString2 = hashedString;
						global::Debug.LogWarning(text + hashedString2.ToString());
						return;
					}
					state_target.Get<KAnimControllerBase>(smi).AddAnimOverrides(anim, 0f);
				}
			});
			this.Exit("Disableanims()", delegate(StateMachineInstanceType smi)
			{
				HashedString hashedString3 = chooser_callback(smi);
				if (hashedString3 == null)
				{
					return;
				}
				if (hashedString3.IsValid)
				{
					KAnimFile anim2 = Assets.GetAnim(hashedString3);
					if (anim2 != null)
					{
						state_target.Get<KAnimControllerBase>(smi).RemoveAnimOverrides(anim2);
					}
				}
			});
			return this;
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x002A9340 File Offset: 0x002A7540
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAnims(string anim_file, float priority = 0f, string RequiredDlc = "")
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Toggle("ToggleAnims(" + anim_file + ")", delegate(StateMachineInstanceType smi)
			{
				if (DlcManager.IsContentActive(RequiredDlc))
				{
					KAnimFile anim = Assets.GetAnim(anim_file);
					if (anim == null)
					{
						global::Debug.LogError("Trying to add missing override anims:" + anim_file);
					}
					state_target.Get<KAnimControllerBase>(smi).AddAnimOverrides(anim, priority);
				}
			}, delegate(StateMachineInstanceType smi)
			{
				if (DlcManager.IsContentActive(RequiredDlc))
				{
					KAnimFile anim2 = Assets.GetAnim(anim_file);
					state_target.Get<KAnimControllerBase>(smi).RemoveAnimOverrides(anim2);
				}
			});
			return this;
		}

		// Token: 0x0600715D RID: 29021 RVA: 0x002A93AC File Offset: 0x002A75AC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAttributeModifier(string modifier_name, Func<StateMachineInstanceType, AttributeModifier> callback, Func<StateMachineInstanceType, bool> condition = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddAttributeModifier( " + modifier_name + " )", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					AttributeModifier attributeModifier = callback(smi);
					DebugUtil.Assert(smi.dataTable[data_idx] == null);
					smi.dataTable[data_idx] = attributeModifier;
					state_target.Get(smi).GetAttributes().Add(attributeModifier);
				}
			});
			this.Exit("RemoveAttributeModifier( " + modifier_name + " )", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					AttributeModifier attributeModifier2 = (AttributeModifier)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					GameObject gameObject = state_target.Get(smi);
					if (gameObject != null)
					{
						gameObject.GetAttributes().Remove(attributeModifier2);
					}
				}
			});
			return this;
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x002A942C File Offset: 0x002A762C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleLoopingSound(string event_name, Func<StateMachineInstanceType, bool> condition = null, bool pause_on_game_pause = true, bool enable_culling = true, bool enable_camera_scaled_position = true)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("StartLoopingSound( " + event_name + " )", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					state_target.Get(smi).GetComponent<LoopingSounds>().StartSound(event_name, pause_on_game_pause, enable_culling, enable_camera_scaled_position);
				}
			});
			this.Exit("StopLoopingSound( " + event_name + " )", delegate(StateMachineInstanceType smi)
			{
				state_target.Get(smi).GetComponent<LoopingSounds>().StopSound(event_name);
			});
			return this;
		}

		// Token: 0x0600715F RID: 29023 RVA: 0x002A94C4 File Offset: 0x002A76C4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleLoopingSound(string state_label, Func<StateMachineInstanceType, string> event_name_callback, Func<StateMachineInstanceType, bool> condition = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("StartLoopingSound( " + state_label + " )", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					string text = event_name_callback(smi);
					smi.dataTable[data_idx] = text;
					state_target.Get(smi).GetComponent<LoopingSounds>().StartSound(text);
				}
			});
			this.Exit("StopLoopingSound( " + state_label + " )", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					state_target.Get(smi).GetComponent<LoopingSounds>().StopSound((string)smi.dataTable[data_idx]);
					smi.dataTable[data_idx] = null;
				}
			});
			return this;
		}

		// Token: 0x06007160 RID: 29024 RVA: 0x002A9544 File Offset: 0x002A7744
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State RefreshUserMenuOnEnter()
		{
			this.Enter("RefreshUserMenuOnEnter()", delegate(StateMachineInstanceType smi)
			{
				UserMenu userMenu = Game.Instance.userMenu;
				MasterType master = smi.master;
				userMenu.Refresh(master.gameObject);
			});
			return this;
		}

		// Token: 0x06007161 RID: 29025 RVA: 0x002A9574 File Offset: 0x002A7774
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State WorkableStartTransition(Func<StateMachineInstanceType, Workable> get_workable_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("Enter WorkableStartTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable3 = get_workable_callback(smi);
				if (workable3 != null)
				{
					Action<Workable, Workable.WorkableEvent> action = delegate(Workable workable, Workable.WorkableEvent evt)
					{
						if (evt == Workable.WorkableEvent.WorkStarted)
						{
							smi.GoTo(target_state);
						}
					};
					smi.dataTable[data_idx] = action;
					Workable workable2 = workable3;
					workable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(workable2.OnWorkableEventCB, action);
				}
			});
			this.Exit("Exit WorkableStartTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable4 = get_workable_callback(smi);
				if (workable4 != null)
				{
					Action<Workable, Workable.WorkableEvent> action2 = (Action<Workable, Workable.WorkableEvent>)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					Workable workable5 = workable4;
					workable5.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(workable5.OnWorkableEventCB, action2);
				}
			});
			return this;
		}

		// Token: 0x06007162 RID: 29026 RVA: 0x002A95FC File Offset: 0x002A77FC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State WorkableStopTransition(Func<StateMachineInstanceType, Workable> get_workable_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("Enter WorkableStopTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable3 = get_workable_callback(smi);
				if (workable3 != null)
				{
					Action<Workable, Workable.WorkableEvent> action = delegate(Workable workable, Workable.WorkableEvent evt)
					{
						if (evt == Workable.WorkableEvent.WorkStopped)
						{
							smi.GoTo(target_state);
						}
					};
					smi.dataTable[data_idx] = action;
					Workable workable2 = workable3;
					workable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(workable2.OnWorkableEventCB, action);
				}
			});
			this.Exit("Exit WorkableStopTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable4 = get_workable_callback(smi);
				if (workable4 != null)
				{
					Action<Workable, Workable.WorkableEvent> action2 = (Action<Workable, Workable.WorkableEvent>)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					Workable workable5 = workable4;
					workable5.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(workable5.OnWorkableEventCB, action2);
				}
			});
			return this;
		}

		// Token: 0x06007163 RID: 29027 RVA: 0x002A9684 File Offset: 0x002A7884
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State WorkableCompleteTransition(Func<StateMachineInstanceType, Workable> get_workable_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("Enter WorkableCompleteTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable3 = get_workable_callback(smi);
				if (workable3 != null)
				{
					Action<Workable, Workable.WorkableEvent> action = delegate(Workable workable, Workable.WorkableEvent evt)
					{
						if (evt == Workable.WorkableEvent.WorkCompleted)
						{
							smi.GoTo(target_state);
						}
					};
					smi.dataTable[data_idx] = action;
					Workable workable2 = workable3;
					workable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(workable2.OnWorkableEventCB, action);
				}
			});
			this.Exit("Exit WorkableCompleteTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable4 = get_workable_callback(smi);
				if (workable4 != null)
				{
					Action<Workable, Workable.WorkableEvent> action2 = (Action<Workable, Workable.WorkableEvent>)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					Workable workable5 = workable4;
					workable5.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(workable5.OnWorkableEventCB, action2);
				}
			});
			return this;
		}

		// Token: 0x06007164 RID: 29028 RVA: 0x002A970C File Offset: 0x002A790C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleGravity()
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddComponent<Gravity>()", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				smi.dataTable[data_idx] = gameObject;
				GameComps.Gravities.Add(gameObject, Vector2.zero, null);
			});
			this.Exit("RemoveComponent<Gravity>()", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject2 = (GameObject)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				GameComps.Gravities.Remove(gameObject2);
			});
			return this;
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x002A9768 File Offset: 0x002A7968
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleGravity(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State landed_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.EventTransition(GameHashes.Landed, landed_state, null);
			this.Toggle("GravityComponent", delegate(StateMachineInstanceType smi)
			{
				GameComps.Gravities.Add(state_target.Get(smi), Vector2.zero, null);
			}, delegate(StateMachineInstanceType smi)
			{
				GameComps.Gravities.Remove(state_target.Get(smi));
			});
			return this;
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x002A97BC File Offset: 0x002A79BC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleThought(Func<StateMachineInstanceType, Thought> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableThought()", delegate(StateMachineInstanceType smi)
			{
				Thought thought = chooser_callback(smi);
				state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().AddThought(thought);
			});
			this.Exit("DisableThought()", delegate(StateMachineInstanceType smi)
			{
				Thought thought2 = chooser_callback(smi);
				state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().RemoveThought(thought2);
			});
			return this;
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x002A9814 File Offset: 0x002A7A14
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleThought(Thought thought, Func<StateMachineInstanceType, bool> condition_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition_callback == null || condition_callback(smi))
				{
					state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().AddThought(thought);
				}
			});
			if (condition_callback != null)
			{
				this.Update("ValidateThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi, float dt)
				{
					if (condition_callback(smi))
					{
						state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().AddThought(thought);
						return;
					}
					state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().RemoveThought(thought);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("RemoveThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().RemoveThought(thought);
			});
			return this;
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x002A98D4 File Offset: 0x002A7AD4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleExpression(Func<StateMachineInstanceType, Expression> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddExpression", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<FaceGraph>(smi).AddExpression(chooser_callback(smi));
			});
			this.Exit("RemoveExpression", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<FaceGraph>(smi).RemoveExpression(chooser_callback(smi));
			});
			return this;
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x002A992C File Offset: 0x002A7B2C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleExpression(Expression expression, Func<StateMachineInstanceType, bool> condition = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddExpression(" + expression.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					state_target.Get<FaceGraph>(smi).AddExpression(expression);
				}
			});
			if (condition != null)
			{
				this.Update("ValidateExpression(" + expression.Id + ")", delegate(StateMachineInstanceType smi, float dt)
				{
					if (condition(smi))
					{
						state_target.Get<FaceGraph>(smi).AddExpression(expression);
						return;
					}
					state_target.Get<FaceGraph>(smi).RemoveExpression(expression);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("RemoveExpression(" + expression.Id + ")", delegate(StateMachineInstanceType smi)
			{
				FaceGraph faceGraph = state_target.Get<FaceGraph>(smi);
				if (faceGraph != null)
				{
					faceGraph.RemoveExpression(expression);
				}
			});
			return this;
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x002A99EC File Offset: 0x002A7BEC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleMainStatusItem(StatusItem status_item, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddMainStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				object obj = ((callback != null) ? callback(smi) : smi);
				state_target.Get<KSelectable>(smi).SetStatusItem(Db.Get().StatusItemCategories.Main, status_item, obj);
			});
			this.Exit("RemoveMainStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null)
				{
					kselectable.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
				}
			});
			return this;
		}

		// Token: 0x0600716B RID: 29035 RVA: 0x002A9A74 File Offset: 0x002A7C74
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleMainStatusItem(Func<StateMachineInstanceType, StatusItem> status_item_cb, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddMainStatusItem(DynamicGeneration)", delegate(StateMachineInstanceType smi)
			{
				object obj = ((callback != null) ? callback(smi) : smi);
				state_target.Get<KSelectable>(smi).SetStatusItem(Db.Get().StatusItemCategories.Main, status_item_cb(smi), obj);
			});
			this.Exit("RemoveMainStatusItem(DynamicGeneration)", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null)
				{
					kselectable.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
				}
			});
			return this;
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x002A9AD4 File Offset: 0x002A7CD4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleCategoryStatusItem(StatusItemCategory category, StatusItem status_item, object data = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter(string.Concat(new string[] { "AddCategoryStatusItem(", category.Id, ", ", status_item.Id, ")" }), delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KSelectable>(smi).SetStatusItem(category, status_item, (data != null) ? data : smi);
			});
			this.Exit(string.Concat(new string[] { "RemoveCategoryStatusItem(", category.Id, ", ", status_item.Id, ")" }), delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null)
				{
					kselectable.SetStatusItem(category, null, null);
				}
			});
			return this;
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x002A9BB0 File Offset: 0x002A7DB0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(StatusItem status_item, object data = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				object obj = data;
				if (obj == null)
				{
					obj = smi;
				}
				Guid guid = state_target.Get<KSelectable>(smi).AddStatusItem(status_item, obj);
				smi.dataTable[data_idx] = guid;
			});
			this.Exit("RemoveStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null && smi.dataTable[data_idx] != null)
				{
					Guid guid2 = (Guid)smi.dataTable[data_idx];
					kselectable.RemoveStatusItem(guid2, false);
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x0600716E RID: 29038 RVA: 0x002A9C44 File Offset: 0x002A7E44
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleSnapOn(string snap_on)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("SnapOn(" + snap_on + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<SnapOn>(smi).AttachSnapOnByName(snap_on);
			});
			this.Exit("SnapOff(" + snap_on + ")", delegate(StateMachineInstanceType smi)
			{
				SnapOn snapOn = state_target.Get<SnapOn>(smi);
				if (snapOn != null)
				{
					snapOn.DetachSnapOnByName(snap_on);
				}
			});
			return this;
		}

		// Token: 0x0600716F RID: 29039 RVA: 0x002A9CBC File Offset: 0x002A7EBC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleTag(Tag tag)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddTag(" + tag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).AddTag(tag, false);
			});
			this.Exit("RemoveTag(" + tag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).RemoveTag(tag);
			});
			return this;
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x002A9D40 File Offset: 0x002A7F40
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleTag(Func<StateMachineInstanceType, Tag> behaviour_tag_cb)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddTag(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).AddTag(behaviour_tag_cb(smi), false);
			});
			this.Exit("RemoveTag(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).RemoveTag(behaviour_tag_cb(smi));
			});
			return this;
		}

		// Token: 0x06007171 RID: 29041 RVA: 0x002A9D97 File Offset: 0x002A7F97
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(StatusItem status_item, Func<StateMachineInstanceType, object> callback)
		{
			return this.ToggleStatusItem(status_item, callback, null);
		}

		// Token: 0x06007172 RID: 29042 RVA: 0x002A9DA4 File Offset: 0x002A7FA4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(StatusItem status_item, Func<StateMachineInstanceType, object> callback, StatusItemCategory category)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (category == null)
				{
					object obj = ((callback != null) ? callback(smi) : null);
					Guid guid = state_target.Get<KSelectable>(smi).AddStatusItem(status_item, obj);
					smi.dataTable[data_idx] = guid;
					return;
				}
				object obj2 = ((callback != null) ? callback(smi) : null);
				Guid guid2 = state_target.Get<KSelectable>(smi).SetStatusItem(category, status_item, obj2);
				smi.dataTable[data_idx] = guid2;
			});
			this.Exit("RemoveStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null && smi.dataTable[data_idx] != null)
				{
					if (category == null)
					{
						Guid guid3 = (Guid)smi.dataTable[data_idx];
						kselectable.RemoveStatusItem(guid3, false);
					}
					else
					{
						kselectable.SetStatusItem(category, null, null);
					}
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x002A9E40 File Offset: 0x002A8040
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(Func<StateMachineInstanceType, StatusItem> status_item_cb, Func<StateMachineInstanceType, object> data_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddStatusItem(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				StatusItem statusItem = status_item_cb(smi);
				if (statusItem != null)
				{
					object obj = ((data_callback != null) ? data_callback(smi) : null);
					Guid guid = state_target.Get<KSelectable>(smi).AddStatusItem(statusItem, obj);
					smi.dataTable[data_idx] = guid;
				}
			});
			this.Exit("RemoveStatusItem(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null && smi.dataTable[data_idx] != null)
				{
					Guid guid2 = (Guid)smi.dataTable[data_idx];
					kselectable.RemoveStatusItem(guid2, false);
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06007174 RID: 29044 RVA: 0x002A9EAC File Offset: 0x002A80AC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleFX(Func<StateMachineInstanceType, StateMachine.Instance> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("EnableFX()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance = callback(smi);
				if (instance != null)
				{
					instance.StartSM();
					smi.dataTable[data_idx] = instance;
				}
			});
			this.Exit("DisableFX()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance2 = (StateMachine.Instance)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				if (instance2 != null)
				{
					instance2.StopSM("ToggleFX.Exit");
				}
			});
			return this;
		}

		// Token: 0x06007175 RID: 29045 RVA: 0x002A9F04 File Offset: 0x002A8104
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BehaviourComplete(Func<StateMachineInstanceType, Tag> tag_cb, bool on_exit = false)
		{
			if (on_exit)
			{
				this.Exit("BehaviourComplete()", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag_cb(smi));
					smi.GoTo(null);
				});
			}
			else
			{
				this.Enter("BehaviourComplete()", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag_cb(smi));
					smi.GoTo(null);
				});
			}
			return this;
		}

		// Token: 0x06007176 RID: 29046 RVA: 0x002A9F54 File Offset: 0x002A8154
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BehaviourComplete(Tag tag, bool on_exit = false)
		{
			if (on_exit)
			{
				this.Exit("BehaviourComplete(" + tag.ToString() + ")", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag);
					smi.GoTo(null);
				});
			}
			else
			{
				this.Enter("BehaviourComplete(" + tag.ToString() + ")", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag);
					smi.GoTo(null);
				});
			}
			return this;
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x002A9FDC File Offset: 0x002A81DC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleBehaviour(Tag behaviour_tag, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback precondition, Action<StateMachineInstanceType> on_complete = null)
		{
			Func<object, bool> precondition_cb = (object obj) => precondition(obj as StateMachineInstanceType);
			this.Enter("AddPrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().AddBehaviourPrecondition(behaviour_tag, precondition_cb, smi);
				}
			});
			this.Exit("RemovePrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().RemoveBehaviourPrecondition(behaviour_tag, precondition_cb, smi);
				}
			});
			this.ToggleTag(behaviour_tag);
			if (on_complete != null)
			{
				this.EventHandler(GameHashes.BehaviourTagComplete, delegate(StateMachineInstanceType smi, object data)
				{
					if ((Tag)data == behaviour_tag)
					{
						on_complete(smi);
					}
				});
			}
			return this;
		}

		// Token: 0x06007178 RID: 29048 RVA: 0x002AA074 File Offset: 0x002A8274
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleBehaviour(Func<StateMachineInstanceType, Tag> behaviour_tag_cb, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback precondition, Action<StateMachineInstanceType> on_complete = null)
		{
			Func<object, bool> precondition_cb = (object obj) => precondition(obj as StateMachineInstanceType);
			this.Enter("AddPrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().AddBehaviourPrecondition(behaviour_tag_cb(smi), precondition_cb, smi);
				}
			});
			this.Exit("RemovePrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().RemoveBehaviourPrecondition(behaviour_tag_cb(smi), precondition_cb, smi);
				}
			});
			this.ToggleTag(behaviour_tag_cb);
			if (on_complete != null)
			{
				this.EventHandler(GameHashes.BehaviourTagComplete, delegate(StateMachineInstanceType smi, object data)
				{
					if ((Tag)data == behaviour_tag_cb(smi))
					{
						on_complete(smi);
					}
				});
			}
			return this;
		}

		// Token: 0x06007179 RID: 29049 RVA: 0x002AA10C File Offset: 0x002A830C
		public void ClearFetch(StateMachineInstanceType smi, int fetch_data_idx, int callback_data_idx)
		{
			FetchList2 fetchList = (FetchList2)smi.dataTable[fetch_data_idx];
			if (fetchList != null)
			{
				smi.dataTable[fetch_data_idx] = null;
				smi.dataTable[callback_data_idx] = null;
				fetchList.Cancel("ClearFetchListFromSM");
			}
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x002AA158 File Offset: 0x002A8358
		public void SetupFetch(Func<StateMachineInstanceType, FetchList2> create_fetchlist_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, StateMachineInstanceType smi, int fetch_data_idx, int callback_data_idx)
		{
			FetchList2 fetchList = create_fetchlist_callback(smi);
			System.Action action = delegate
			{
				this.ClearFetch(smi, fetch_data_idx, callback_data_idx);
				smi.GoTo(target_state);
			};
			fetchList.Submit(action, true);
			smi.dataTable[fetch_data_idx] = fetchList;
			smi.dataTable[callback_data_idx] = action;
		}

		// Token: 0x0600717B RID: 29051 RVA: 0x002AA1E4 File Offset: 0x002A83E4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleFetch(Func<StateMachineInstanceType, FetchList2> create_fetchlist_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleFetchEnter()", delegate(StateMachineInstanceType smi)
			{
				this.SetupFetch(create_fetchlist_callback, target_state, smi, data_idx, callback_data_idx);
			});
			this.Exit("ToggleFetchExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearFetch(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x002AA258 File Offset: 0x002A8458
		private void ClearChore(StateMachineInstanceType smi, int chore_data_idx, int callback_data_idx)
		{
			Chore chore = (Chore)smi.dataTable[chore_data_idx];
			if (chore != null)
			{
				Action<Chore> action = (Action<Chore>)smi.dataTable[callback_data_idx];
				smi.dataTable[chore_data_idx] = null;
				smi.dataTable[callback_data_idx] = null;
				Chore chore2 = chore;
				chore2.onExit = (Action<Chore>)Delegate.Remove(chore2.onExit, action);
				chore.Cancel("ClearGlobalChore");
			}
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x002AA2CC File Offset: 0x002A84CC
		private Chore SetupChore(Func<StateMachineInstanceType, Chore> create_chore_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state, StateMachineInstanceType smi, int chore_data_idx, int callback_data_idx, bool is_success_state_reentrant, bool is_failure_state_reentrant)
		{
			Chore chore = create_chore_callback(smi);
			DebugUtil.DevAssert(!chore.IsPreemptable, "ToggleChore can't be used with preemptable chores! :( (but it should...)", null);
			chore.runUntilComplete = false;
			Action<Chore> action = delegate(Chore chore_param)
			{
				bool isComplete = chore.isComplete;
				if ((isComplete & is_success_state_reentrant) || (is_failure_state_reentrant && !isComplete))
				{
					this.SetupChore(create_chore_callback, success_state, failure_state, smi, chore_data_idx, callback_data_idx, is_success_state_reentrant, is_failure_state_reentrant);
					return;
				}
				GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state = success_state;
				if (!isComplete)
				{
					state = failure_state;
				}
				this.ClearChore(smi, chore_data_idx, callback_data_idx);
				smi.GoTo(state);
			};
			Chore chore2 = chore;
			chore2.onExit = (Action<Chore>)Delegate.Combine(chore2.onExit, action);
			smi.dataTable[chore_data_idx] = chore;
			smi.dataTable[callback_data_idx] = action;
			return chore;
		}

		// Token: 0x0600717E RID: 29054 RVA: 0x002AA3C4 File Offset: 0x002A85C4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleRecurringChore(Func<StateMachineInstanceType, Chore> callback, Func<StateMachineInstanceType, bool> condition = null)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleRecurringChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					this.SetupChore(callback, this, this, smi, data_idx, callback_data_idx, true, true);
				}
			});
			this.Exit("ToggleRecurringChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x002AA438 File Offset: 0x002A8638
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleChore(Func<StateMachineInstanceType, Chore> callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				this.SetupChore(callback, target_state, target_state, smi, data_idx, callback_data_idx, false, false);
			});
			this.Exit("ToggleChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x002AA4AC File Offset: 0x002A86AC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleChore(Func<StateMachineInstanceType, Chore> callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			bool is_success_state_reentrant = success_state == this;
			bool is_failure_state_reentrant = failure_state == this;
			this.Enter("ToggleChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				this.SetupChore(callback, success_state, failure_state, smi, data_idx, callback_data_idx, is_success_state_reentrant, is_failure_state_reentrant);
			});
			this.Exit("ToggleChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x06007181 RID: 29057 RVA: 0x002AA544 File Offset: 0x002A8744
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleReactable(Func<StateMachineInstanceType, Reactable> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter(delegate(StateMachineInstanceType smi)
			{
				smi.dataTable[data_idx] = callback(smi);
			});
			this.Exit(delegate(StateMachineInstanceType smi)
			{
				Reactable reactable = (Reactable)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				if (reactable != null)
				{
					reactable.Cleanup();
				}
			});
			return this;
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x002AA594 File Offset: 0x002A8794
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State RemoveEffect(string effect_name)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("RemoveEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(effect_name);
			});
			return this;
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x002AA5E4 File Offset: 0x002A87E4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleEffect(string effect_name)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(effect_name, false);
			});
			this.Exit("RemoveEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(effect_name);
			});
			return this;
		}

		// Token: 0x06007184 RID: 29060 RVA: 0x002AA65C File Offset: 0x002A885C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleEffect(Func<StateMachineInstanceType, Effect> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(callback(smi), false);
			});
			this.Exit("RemoveEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(callback(smi));
			});
			return this;
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x002AA6B4 File Offset: 0x002A88B4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleEffect(Func<StateMachineInstanceType, string> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(callback(smi), false);
			});
			this.Exit("RemoveEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(callback(smi));
			});
			return this;
		}

		// Token: 0x06007186 RID: 29062 RVA: 0x002AA70B File Offset: 0x002A890B
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State LogOnExit(Func<StateMachineInstanceType, string> callback)
		{
			this.Enter("Log()", delegate(StateMachineInstanceType smi)
			{
			});
			return this;
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x002AA739 File Offset: 0x002A8939
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State LogOnEnter(Func<StateMachineInstanceType, string> callback)
		{
			this.Exit("Log()", delegate(StateMachineInstanceType smi)
			{
			});
			return this;
		}

		// Token: 0x06007188 RID: 29064 RVA: 0x002AA768 File Offset: 0x002A8968
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleUrge(Urge urge)
		{
			return this.ToggleUrge((StateMachineInstanceType smi) => urge);
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x002AA794 File Offset: 0x002A8994
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleUrge(Func<StateMachineInstanceType, Urge> urge_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddUrge()", delegate(StateMachineInstanceType smi)
			{
				Urge urge = urge_callback(smi);
				state_target.Get<ChoreConsumer>(smi).AddUrge(urge);
			});
			this.Exit("RemoveUrge()", delegate(StateMachineInstanceType smi)
			{
				Urge urge2 = urge_callback(smi);
				ChoreConsumer choreConsumer = state_target.Get<ChoreConsumer>(smi);
				if (choreConsumer != null)
				{
					choreConsumer.RemoveUrge(urge2);
				}
			});
			return this;
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x002AA7EB File Offset: 0x002A89EB
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnTargetLost(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			this.ParamTransition<GameObject>(parameter, target_state, (StateMachineInstanceType smi, GameObject p) => p == null);
			return this;
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x002AA818 File Offset: 0x002A8A18
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleBrain(string reason)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("StopBrain(" + reason + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Brain>(smi).Stop(reason);
			});
			this.Exit("ResetBrain(" + reason + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Brain>(smi).Reset(reason);
			});
			return this;
		}

		// Token: 0x0600718C RID: 29068 RVA: 0x002AA890 File Offset: 0x002A8A90
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TriggerOnEnter(GameHashes evt, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("Trigger(" + evt.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				object obj = ((callback != null) ? callback(smi) : null);
				gameObject.Trigger((int)evt, obj);
			});
			return this;
		}

		// Token: 0x0600718D RID: 29069 RVA: 0x002AA8F4 File Offset: 0x002A8AF4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TriggerOnExit(GameHashes evt, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Exit("Trigger(" + evt.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				if (gameObject != null)
				{
					object obj = ((callback != null) ? callback(smi) : null);
					gameObject.Trigger((int)evt, obj);
				}
			});
			return this;
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x002AA958 File Offset: 0x002A8B58
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStateMachine(Func<StateMachineInstanceType, StateMachine.Instance> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("EnableStateMachine()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance = callback(smi);
				smi.dataTable[data_idx] = instance;
				instance.StartSM();
			});
			this.Exit("DisableStateMachine()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance2 = (StateMachine.Instance)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				if (instance2 != null)
				{
					instance2.StopSM("ToggleStateMachine.Exit");
				}
			});
			return this;
		}

		// Token: 0x0600718F RID: 29071 RVA: 0x002AA9B0 File Offset: 0x002A8BB0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleComponent<ComponentType>(bool disable = false) where ComponentType : MonoBehaviour
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableComponent(" + typeof(ComponentType).Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<ComponentType>(smi).enabled = !disable;
			});
			this.Exit("DisableComponent(" + typeof(ComponentType).Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<ComponentType>(smi).enabled = disable;
			});
			return this;
		}

		// Token: 0x06007190 RID: 29072 RVA: 0x002AAA3C File Offset: 0x002A8C3C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeOperationalFlag(Operational.Flag flag, bool init_val = false)
		{
			this.Enter(string.Concat(new string[]
			{
				"InitOperationalFlag (",
				flag.Name,
				", ",
				init_val.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				smi.GetComponent<Operational>().SetFlag(flag, init_val);
			});
			return this;
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x002AAAB0 File Offset: 0x002A8CB0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleOperationalFlag(Operational.Flag flag)
		{
			this.Enter("ToggleOperationalFlag True (" + flag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.GetComponent<Operational>().SetFlag(flag, true);
			});
			this.Exit("ToggleOperationalFlag False (" + flag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.GetComponent<Operational>().SetFlag(flag, false);
			});
			return this;
		}

		// Token: 0x06007192 RID: 29074 RVA: 0x002AAB28 File Offset: 0x002A8D28
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleReserve(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter reserver, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter pickup_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter requested_amount, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter actual_amount)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter(string.Concat(new string[] { "Reserve(", pickup_target.name, ", ", requested_amount.name, ")" }), delegate(StateMachineInstanceType smi)
			{
				Pickupable pickupable = pickup_target.Get<Pickupable>(smi);
				GameObject gameObject = reserver.Get(smi);
				float num = requested_amount.Get(smi);
				float num2 = Mathf.Max(1f, Db.Get().Attributes.CarryAmount.Lookup(gameObject).GetTotalValue());
				float num3 = Math.Min(num, num2);
				num3 = Math.Min(num3, pickupable.UnreservedAmount);
				if (num3 <= 0f)
				{
					pickupable.PrintReservations();
					global::Debug.LogError(string.Concat(new string[]
					{
						num2.ToString(),
						", ",
						num.ToString(),
						", ",
						pickupable.UnreservedAmount.ToString(),
						", ",
						num3.ToString()
					}));
				}
				actual_amount.Set(num3, smi, false);
				int num4 = pickupable.Reserve("ToggleReserve", gameObject, num3);
				smi.dataTable[data_idx] = num4;
			});
			this.Exit(string.Concat(new string[] { "Unreserve(", pickup_target.name, ", ", requested_amount.name, ")" }), delegate(StateMachineInstanceType smi)
			{
				int num5 = (int)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				Pickupable pickupable2 = pickup_target.Get<Pickupable>(smi);
				if (pickupable2 != null)
				{
					pickupable2.Unreserve("ToggleReserve", num5);
				}
			});
			return this;
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x002AAC0C File Offset: 0x002A8E0C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleWork(string work_type, Action<StateMachineInstanceType> callback, Func<StateMachineInstanceType, bool> validate_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("StartWork(" + work_type + ")", delegate(StateMachineInstanceType smi)
			{
				if (validate_callback(smi))
				{
					callback(smi);
					return;
				}
				smi.GoTo(failure_state);
			});
			this.Update("Work(" + work_type + ")", delegate(StateMachineInstanceType smi, float dt)
			{
				if (validate_callback(smi))
				{
					Worker.WorkResult workResult = state_target.Get<Worker>(smi).Work(dt);
					if (workResult == Worker.WorkResult.Success)
					{
						smi.GoTo(success_state);
						return;
					}
					if (workResult == Worker.WorkResult.Failed)
					{
						smi.GoTo(failure_state);
						return;
					}
				}
				else
				{
					smi.GoTo(failure_state);
				}
			}, UpdateRate.SIM_33ms, false);
			this.Exit("StopWork()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Worker>(smi).StopWork();
			});
			return this;
		}

		// Token: 0x06007194 RID: 29076 RVA: 0x002AACAC File Offset: 0x002A8EAC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleWork<WorkableType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter source_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state, Func<StateMachineInstanceType, bool> is_valid_cb) where WorkableType : Workable
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork(typeof(WorkableType).Name, delegate(StateMachineInstanceType smi)
			{
				Workable workable = source_target.Get<WorkableType>(smi);
				state_target.Get<Worker>(smi).StartWork(new Worker.StartWorkInfo(workable));
			}, (StateMachineInstanceType smi) => source_target.Get<WorkableType>(smi) != null && (is_valid_cb == null || is_valid_cb(smi)), success_state, failure_state);
			return this;
		}

		// Token: 0x06007195 RID: 29077 RVA: 0x002AAD0C File Offset: 0x002A8F0C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoEat(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter source_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter amount, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork("Eat", delegate(StateMachineInstanceType smi)
			{
				Edible edible = source_target.Get<Edible>(smi);
				Worker worker = state_target.Get<Worker>(smi);
				float num = amount.Get(smi);
				worker.StartWork(new Edible.EdibleStartWorkInfo(edible, num));
			}, (StateMachineInstanceType smi) => source_target.Get<Edible>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x06007196 RID: 29078 RVA: 0x002AAD64 File Offset: 0x002A8F64
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoSleep(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter sleeper, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter bed, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork("Sleep", delegate(StateMachineInstanceType smi)
			{
				Worker worker = state_target.Get<Worker>(smi);
				Sleepable sleepable = bed.Get<Sleepable>(smi);
				worker.StartWork(new Worker.StartWorkInfo(sleepable));
			}, (StateMachineInstanceType smi) => bed.Get<Sleepable>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x06007197 RID: 29079 RVA: 0x002AADB4 File Offset: 0x002A8FB4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoDelivery(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter worker_param, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter storage_param, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			this.ToggleWork("Pickup", delegate(StateMachineInstanceType smi)
			{
				Worker worker = worker_param.Get<Worker>(smi);
				Storage storage = storage_param.Get<Storage>(smi);
				worker.StartWork(new Worker.StartWorkInfo(storage));
			}, (StateMachineInstanceType smi) => storage_param.Get<Storage>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x06007198 RID: 29080 RVA: 0x002AAE00 File Offset: 0x002A9000
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoPickup(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter source_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter result_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter amount, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork("Pickup", delegate(StateMachineInstanceType smi)
			{
				Pickupable pickupable = source_target.Get<Pickupable>(smi);
				Worker worker = state_target.Get<Worker>(smi);
				float num = amount.Get(smi);
				worker.StartWork(new Pickupable.PickupableStartWorkInfo(pickupable, num, delegate(GameObject result)
				{
					result_target.Set(result, smi, false);
				}));
			}, (StateMachineInstanceType smi) => source_target.Get<Pickupable>(smi) != null || result_target.Get<Pickupable>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x06007199 RID: 29081 RVA: 0x002AAE60 File Offset: 0x002A9060
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleNotification(Func<StateMachineInstanceType, Notification> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableNotification()", delegate(StateMachineInstanceType smi)
			{
				Notification notification = callback(smi);
				smi.dataTable[data_idx] = notification;
				state_target.AddOrGet<Notifier>(smi).Add(notification, "");
			});
			this.Exit("DisableNotification()", delegate(StateMachineInstanceType smi)
			{
				Notification notification2 = (Notification)smi.dataTable[data_idx];
				if (notification2 != null)
				{
					if (state_target != null)
					{
						Notifier notifier = state_target.Get<Notifier>(smi);
						if (notifier != null)
						{
							notifier.Remove(notification2);
						}
					}
					smi.dataTable[data_idx] = null;
				}
			});
			return this;
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x002AAEC4 File Offset: 0x002A90C4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoReport(ReportManager.ReportType reportType, Func<StateMachineInstanceType, float> callback, Func<StateMachineInstanceType, string> context_callback = null)
		{
			this.Enter("DoReport()", delegate(StateMachineInstanceType smi)
			{
				float num = callback(smi);
				string text = ((context_callback != null) ? context_callback(smi) : null);
				ReportManager.Instance.ReportValue(reportType, num, text, null);
			});
			return this;
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x002AAF08 File Offset: 0x002A9108
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoNotification(Func<StateMachineInstanceType, Notification> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("DoNotification()", delegate(StateMachineInstanceType smi)
			{
				Notification notification = callback(smi);
				state_target.AddOrGet<Notifier>(smi).Add(notification, "");
			});
			return this;
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x002AAF48 File Offset: 0x002A9148
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoTutorial(Tutorial.TutorialMessages msg)
		{
			this.Enter("DoTutorial()", delegate(StateMachineInstanceType smi)
			{
				Tutorial.Instance.TutorialMessage(msg, true);
			});
			return this;
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x002AAF7C File Offset: 0x002A917C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleScheduleCallback(string name, Func<StateMachineInstanceType, float> time_cb, Action<StateMachineInstanceType> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			Action<object> <>9__2;
			this.Enter("AddScheduledCallback(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				GameScheduler instance = GameScheduler.Instance;
				string name2 = name;
				float num = time_cb(smi);
				Action<object> action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate(object smi_data)
					{
						callback((StateMachineInstanceType)((object)smi_data));
					});
				}
				SchedulerHandle schedulerHandle = instance.Schedule(name2, num, action, smi, null);
				DebugUtil.Assert(smi.dataTable[data_idx] == null);
				smi.dataTable[data_idx] = schedulerHandle;
			});
			this.Exit("RemoveScheduledCallback(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					SchedulerHandle schedulerHandle2 = (SchedulerHandle)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					schedulerHandle2.ClearScheduler();
				}
			});
			return this;
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x002AB004 File Offset: 0x002A9204
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleGoTo(Func<StateMachineInstanceType, float> time_cb, StateMachine.BaseState state)
		{
			this.Enter("ScheduleGoTo(" + state.name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.ScheduleGoTo(time_cb(smi), state);
			});
			return this;
		}

		// Token: 0x0600719F RID: 29087 RVA: 0x002AB054 File Offset: 0x002A9254
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleGoTo(float time, StateMachine.BaseState state)
		{
			this.Enter(string.Concat(new string[]
			{
				"ScheduleGoTo(",
				time.ToString(),
				", ",
				state.name,
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				smi.ScheduleGoTo(time, state);
			});
			return this;
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x002AB0C8 File Offset: 0x002A92C8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleAction(string name, Func<StateMachineInstanceType, float> time_cb, Action<StateMachineInstanceType> action)
		{
			this.Enter("ScheduleAction(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.Schedule(time_cb(smi), delegate(object obj)
				{
					action(smi);
				}, null);
			});
			return this;
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x002AB110 File Offset: 0x002A9310
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleAction(string name, float time, Action<StateMachineInstanceType> action)
		{
			this.Enter(string.Concat(new string[]
			{
				"ScheduleAction(",
				time.ToString(),
				", ",
				name,
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				smi.Schedule(time, delegate(object obj)
				{
					action(smi);
				}, null);
			});
			return this;
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x002AB17C File Offset: 0x002A937C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleActionNextFrame(string name, Action<StateMachineInstanceType> action)
		{
			this.Enter("ScheduleActionNextFrame(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.ScheduleNextFrame(delegate(object obj)
				{
					action(smi);
				}, null);
			});
			return this;
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x002AB1BC File Offset: 0x002A93BC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.EventHandler(evt, global_event_system_callback, delegate(StateMachineInstanceType smi, object d)
			{
				callback(smi);
			});
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x002AB1EC File Offset: 0x002A93EC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback)
		{
			if (this.events == null)
			{
				this.events = new List<StateEvent>();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter targetParameter = this.GetStateTarget();
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent gameEvent = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent(evt, callback, targetParameter, global_event_system_callback);
			this.events.Add(gameEvent);
			return this;
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x002AB22C File Offset: 0x002A942C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.EventHandler(evt, delegate(StateMachineInstanceType smi, object d)
			{
				callback(smi);
			});
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x002AB259 File Offset: 0x002A9459
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback)
		{
			this.EventHandler(evt, null, callback);
			return this;
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x002AB268 File Offset: 0x002A9468
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandlerTransition(GameHashes evt, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, Func<StateMachineInstanceType, object, bool> callback)
		{
			return this.EventHandler(evt, delegate(StateMachineInstanceType smi, object d)
			{
				if (callback(smi, d))
				{
					smi.GoTo(state);
				}
			});
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x002AB29C File Offset: 0x002A949C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandlerTransition(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, Func<StateMachineInstanceType, object, bool> callback)
		{
			return this.EventHandler(evt, global_event_system_callback, delegate(StateMachineInstanceType smi, object d)
			{
				if (callback(smi, d))
				{
					smi.GoTo(state);
				}
			});
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x002AB2D4 File Offset: 0x002A94D4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ParamTransition<ParameterType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType> parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Callback callback)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Transition transition = new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Transition(this.transitions.Count, parameter, state, callback);
			this.transitions.Add(transition);
			return this;
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x002AB328 File Offset: 0x002A9528
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnSignal(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal signal, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, Func<StateMachineInstanceType, bool> callback)
		{
			this.ParamTransition<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>(signal, state, (StateMachineInstanceType smi, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter p) => callback(smi));
			return this;
		}

		// Token: 0x060071AB RID: 29099 RVA: 0x002AB358 File Offset: 0x002A9558
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnSignal(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal signal, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			this.ParamTransition<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>(signal, state, null);
			return this;
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x002AB368 File Offset: 0x002A9568
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EnterTransition(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition)
		{
			string text = "(Stop)";
			if (state != null)
			{
				text = state.name;
			}
			this.Enter("Transition(" + text + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition(smi))
				{
					smi.GoTo(state);
				}
			});
			return this;
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x002AB3C8 File Offset: 0x002A95C8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Transition(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition, UpdateRate update_rate = UpdateRate.SIM_200ms)
		{
			string text = "(Stop)";
			if (state != null)
			{
				text = state.name;
			}
			this.Enter("Transition(" + text + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition(smi))
				{
					smi.GoTo(state);
				}
			});
			this.FastUpdate("Transition(" + text + ")", new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.TransitionUpdater(condition, state), update_rate, false);
			return this;
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x002AB451 File Offset: 0x002A9651
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DefaultState(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State default_state)
		{
			this.defaultState = default_state;
			return this;
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x002AB45C File Offset: 0x002A965C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State GoTo(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self", null);
			string text = "(null)";
			if (state != null)
			{
				text = state.name;
			}
			this.Update("GoTo(" + text + ")", delegate(StateMachineInstanceType smi, float dt)
			{
				smi.GoTo(state);
			}, UpdateRate.SIM_200ms, false);
			return this;
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x002AB4D0 File Offset: 0x002A96D0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State StopMoving()
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target = this.GetStateTarget();
			this.Enter("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				target.Get<Navigator>(smi).Stop(false, true);
			});
			return this;
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x002AB508 File Offset: 0x002A9708
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStationaryIdling()
		{
			this.GetStateTarget();
			this.ToggleTag(GameTags.StationaryIdling);
			return this;
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x002AB520 File Offset: 0x002A9720
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnBehaviourComplete(Tag behaviour, Action<StateMachineInstanceType> cb)
		{
			this.EventHandler(GameHashes.BehaviourTagComplete, delegate(StateMachineInstanceType smi, object d)
			{
				if ((Tag)d == behaviour)
				{
					cb(smi);
				}
			});
			return this;
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x002AB55A File Offset: 0x002A975A
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo(Func<StateMachineInstanceType, int> cell_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state = null, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, bool update_cell = false)
		{
			return this.MoveTo(cell_callback, null, success_state, fail_state, update_cell);
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x002AB568 File Offset: 0x002A9768
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo(Func<StateMachineInstanceType, int> cell_callback, Func<StateMachineInstanceType, CellOffset[]> cell_offsets_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state = null, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, bool update_cell = false)
		{
			this.EventTransition(GameHashes.DestinationReached, success_state, null);
			this.EventTransition(GameHashes.NavigationFailed, fail_state, null);
			CellOffset[] default_offset = new CellOffset[1];
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("MoveTo()", delegate(StateMachineInstanceType smi)
			{
				int num = cell_callback(smi);
				Navigator navigator = state_target.Get<Navigator>(smi);
				CellOffset[] array = default_offset;
				if (cell_offsets_callback != null)
				{
					array = cell_offsets_callback(smi);
				}
				navigator.GoTo(num, array);
			});
			if (update_cell)
			{
				this.Update("MoveTo()", delegate(StateMachineInstanceType smi, float dt)
				{
					int num2 = cell_callback(smi);
					state_target.Get<Navigator>(smi).UpdateTarget(num2);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get(smi).GetComponent<Navigator>().Stop(false, true);
			});
			return this;
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x002AB610 File Offset: 0x002A9810
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo<ApproachableType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, CellOffset[] override_offsets = null, NavTactic tactic = null) where ApproachableType : IApproachable
		{
			this.EventTransition(GameHashes.DestinationReached, success_state, null);
			this.EventTransition(GameHashes.NavigationFailed, fail_state, null);
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			CellOffset[] offsets;
			this.Enter("MoveTo(" + move_parameter.name + ")", delegate(StateMachineInstanceType smi)
			{
				offsets = override_offsets;
				IApproachable approachable = move_parameter.Get<ApproachableType>(smi);
				KMonoBehaviour kmonoBehaviour = move_parameter.Get<KMonoBehaviour>(smi);
				if (kmonoBehaviour == null)
				{
					smi.GoTo(fail_state);
					return;
				}
				Navigator component = state_target.Get(smi).GetComponent<Navigator>();
				if (offsets == null)
				{
					offsets = approachable.GetOffsets();
				}
				component.GoTo(kmonoBehaviour, offsets, tactic);
			});
			this.Exit("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Navigator>(smi).Stop(false, true);
			});
			return this;
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x002AB6B4 File Offset: 0x002A98B4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Face(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter face_target, float x_offset = 0f)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("Face", delegate(StateMachineInstanceType smi)
			{
				if (face_target != null)
				{
					IApproachable approachable = face_target.Get<IApproachable>(smi);
					if (approachable != null)
					{
						float num = approachable.transform.GetPosition().x + x_offset;
						state_target.Get<Facing>(smi).Face(num);
					}
				}
			});
			return this;
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x002AB6FC File Offset: 0x002A98FC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TagTransition(Tag[] tags, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, bool on_remove = false)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData tagTransitionData = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData(tags.ToString(), this, state, this.transitions.Count, tags, on_remove, this.GetStateTarget(), null);
			this.transitions.Add(tagTransitionData);
			return this;
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x002AB760 File Offset: 0x002A9960
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TagTransition(Func<StateMachineInstanceType, Tag[]> tags_cb, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, bool on_remove = false)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData tagTransitionData = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData("DynamicTransition", this, state, this.transitions.Count, null, on_remove, this.GetStateTarget(), tags_cb);
			this.transitions.Add(tagTransitionData);
			return this;
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x002AB7C0 File Offset: 0x002A99C0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TagTransition(Tag tag, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, bool on_remove = false)
		{
			return this.TagTransition(new Tag[] { tag }, state, on_remove);
		}

		// Token: 0x060071BA RID: 29114 RVA: 0x002AB7D8 File Offset: 0x002A99D8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventTransition(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition = null)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter targetParameter = this.GetStateTarget();
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.EventTransitionData eventTransitionData = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.EventTransitionData(this, state, this.transitions.Count, evt, global_event_system_callback, condition, targetParameter);
			this.transitions.Add(eventTransitionData);
			return this;
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x002AB836 File Offset: 0x002A9A36
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventTransition(GameHashes evt, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition = null)
		{
			return this.EventTransition(evt, null, state, condition);
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x002AB842 File Offset: 0x002A9A42
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ReturnSuccess()
		{
			this.Enter("ReturnSuccess()", delegate(StateMachineInstanceType smi)
			{
				smi.SetStatus(StateMachine.Status.Success);
				smi.StopSM("GameStateMachine.ReturnSuccess()");
			});
			return this;
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x002AB870 File Offset: 0x002A9A70
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ReturnFailure()
		{
			this.Enter("ReturnFailure()", delegate(StateMachineInstanceType smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.StopSM("GameStateMachine.ReturnFailure()");
			});
			return this;
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x002AB8A0 File Offset: 0x002A9AA0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(string name, string tooltip, string icon = "", StatusItem.IconType icon_type = StatusItem.IconType.Info, NotificationType notification_type = NotificationType.Neutral, bool allow_multiples = false, HashedString render_overlay = default(HashedString), int status_overlays = 129022, Func<string, StateMachineInstanceType, string> resolve_string_callback = null, Func<string, StateMachineInstanceType, string> resolve_tooltip_callback = null, StatusItemCategory category = null)
		{
			StatusItem statusItem = new StatusItem(this.longName, name, tooltip, icon, icon_type, notification_type, allow_multiples, render_overlay, status_overlays, true, null);
			if (resolve_string_callback != null)
			{
				statusItem.resolveStringCallback = (string str, object obj) => resolve_string_callback(str, (StateMachineInstanceType)((object)obj));
			}
			if (resolve_tooltip_callback != null)
			{
				statusItem.resolveTooltipCallback = (string str, object obj) => resolve_tooltip_callback(str, (StateMachineInstanceType)((object)obj));
			}
			this.ToggleStatusItem(statusItem, (StateMachineInstanceType smi) => smi, category);
			return this;
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x002AB93C File Offset: 0x002A9B3C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(string anim)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			KAnim.PlayMode mode = KAnim.PlayMode.Once;
			this.Enter(string.Concat(new string[]
			{
				"PlayAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x002AB9C0 File Offset: 0x002A9BC0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(Func<StateMachineInstanceType, string> anim_cb, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnim(" + mode.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim_cb(smi), mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x002ABA24 File Offset: 0x002A9C24
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(string anim, KAnim.PlayMode mode)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter(string.Concat(new string[]
			{
				"PlayAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x002ABAA8 File Offset: 0x002A9CA8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(string anim, KAnim.PlayMode mode, Func<StateMachineInstanceType, string> suffix_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter(string.Concat(new string[]
			{
				"PlayAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				string text = "";
				if (suffix_callback != null)
				{
					text = suffix_callback(smi);
				}
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim + text, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x002ABB30 File Offset: 0x002A9D30
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State QueueAnim(Func<StateMachineInstanceType, string> anim_cb, bool loop = false, Func<StateMachineInstanceType, string> suffix_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			KAnim.PlayMode mode = KAnim.PlayMode.Once;
			if (loop)
			{
				mode = KAnim.PlayMode.Loop;
			}
			this.Enter("QueueAnim(" + mode.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				string text = "";
				if (suffix_callback != null)
				{
					text = suffix_callback(smi);
				}
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Queue(anim_cb(smi) + text, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x002ABBA4 File Offset: 0x002A9DA4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State QueueAnim(string anim, bool loop = false, Func<StateMachineInstanceType, string> suffix_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			KAnim.PlayMode mode = KAnim.PlayMode.Once;
			if (loop)
			{
				mode = KAnim.PlayMode.Loop;
			}
			this.Enter(string.Concat(new string[]
			{
				"QueueAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				string text = "";
				if (suffix_callback != null)
				{
					text = suffix_callback(smi);
				}
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Queue(anim + text, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x002ABC38 File Offset: 0x002A9E38
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnims(Func<StateMachineInstanceType, HashedString[]> anims_callback, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnims", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					HashedString[] array = anims_callback(smi);
					kanimControllerBase.Play(array, mode);
				}
			});
			return this;
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x002ABC80 File Offset: 0x002A9E80
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnims(Func<StateMachineInstanceType, HashedString[]> anims_callback, Func<StateMachineInstanceType, KAnim.PlayMode> mode_cb)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnims", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					HashedString[] array = anims_callback(smi);
					KAnim.PlayMode playMode = mode_cb(smi);
					kanimControllerBase.Play(array, playMode);
				}
			});
			return this;
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x002ABCC8 File Offset: 0x002A9EC8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnAnimQueueComplete(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("CheckIfAnimQueueIsEmpty", delegate(StateMachineInstanceType smi)
			{
				if (state_target.Get<KBatchedAnimController>(smi).IsStopped())
				{
					smi.GoTo(state);
				}
			});
			return this.EventTransition(GameHashes.AnimQueueComplete, state, null);
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x002ABD18 File Offset: 0x002A9F18
		internal void EventHandler()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04005646 RID: 22086
		[StateMachine.DoNotAutoCreate]
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter stateTarget;

		// Token: 0x02001EE7 RID: 7911
		private class TransitionUpdater : UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater
		{
			// Token: 0x06009D50 RID: 40272 RVA: 0x0033C0CA File Offset: 0x0033A2CA
			public TransitionUpdater(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
			{
				this.condition = condition;
				this.state = state;
			}

			// Token: 0x06009D51 RID: 40273 RVA: 0x0033C0E0 File Offset: 0x0033A2E0
			public void Update(StateMachineInstanceType smi, float dt)
			{
				if (this.condition(smi))
				{
					smi.GoTo(this.state);
				}
			}

			// Token: 0x04008A6A RID: 35434
			private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition;

			// Token: 0x04008A6B RID: 35435
			private GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state;
		}
	}

	// Token: 0x02001009 RID: 4105
	public class GameEvent : StateEvent
	{
		// Token: 0x060071CC RID: 29132 RVA: 0x002ABD39 File Offset: 0x002A9F39
		public GameEvent(GameHashes id, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback)
			: base(id.ToString())
		{
			this.id = id;
			this.target = target;
			this.callback = callback;
			this.globalEventSystemCallback = global_event_system_callback;
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x002ABD6C File Offset: 0x002A9F6C
		public override StateEvent.Context Subscribe(StateMachine.Instance smi)
		{
			StateEvent.Context context = base.Subscribe(smi);
			StateMachineInstanceType cast_smi = (StateMachineInstanceType)((object)smi);
			Action<object> action = delegate(object d)
			{
				if (StateMachine.Instance.error)
				{
					return;
				}
				this.callback(cast_smi, d);
			};
			if (this.globalEventSystemCallback != null)
			{
				KMonoBehaviour kmonoBehaviour = this.globalEventSystemCallback(cast_smi);
				context.data = kmonoBehaviour.Subscribe((int)this.id, action);
			}
			else
			{
				context.data = this.target.Get(cast_smi).Subscribe((int)this.id, action);
			}
			return context;
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x002ABDFC File Offset: 0x002A9FFC
		public override void Unsubscribe(StateMachine.Instance smi, StateEvent.Context context)
		{
			StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)smi);
			if (this.globalEventSystemCallback != null)
			{
				KMonoBehaviour kmonoBehaviour = this.globalEventSystemCallback(stateMachineInstanceType);
				if (kmonoBehaviour != null)
				{
					kmonoBehaviour.Unsubscribe(context.data);
					return;
				}
			}
			else
			{
				GameObject gameObject = this.target.Get(stateMachineInstanceType);
				if (gameObject != null)
				{
					gameObject.Unsubscribe(context.data);
				}
			}
		}

		// Token: 0x04005647 RID: 22087
		private GameHashes id;

		// Token: 0x04005648 RID: 22088
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target;

		// Token: 0x04005649 RID: 22089
		private GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback;

		// Token: 0x0400564A RID: 22090
		private Func<StateMachineInstanceType, KMonoBehaviour> globalEventSystemCallback;

		// Token: 0x02001F4C RID: 8012
		// (Invoke) Token: 0x06009E63 RID: 40547
		public delegate void Callback(StateMachineInstanceType smi, object callback_data);
	}

	// Token: 0x0200100A RID: 4106
	public class ApproachSubState<ApproachableType> : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State where ApproachableType : IApproachable
	{
		// Token: 0x060071CF RID: 29135 RVA: 0x002ABE5D File Offset: 0x002AA05D
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter mover, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null, CellOffset[] override_offsets = null, NavTactic tactic = null)
		{
			base.root.Target(mover).OnTargetLost(move_target, failure_state).MoveTo<ApproachableType>(move_target, success_state, failure_state, override_offsets, (tactic == null) ? NavigationTactics.ReduceTravelDistance : tactic);
			return this;
		}
	}

	// Token: 0x0200100B RID: 4107
	public class DebugGoToSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060071D1 RID: 29137 RVA: 0x002ABE98 File Offset: 0x002AA098
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State exit_state)
		{
			base.root.Enter("GoToCursor", delegate(StateMachineInstanceType smi)
			{
				this.GoToCursor(smi);
			}).EventHandler(GameHashes.DebugGoTo, (StateMachineInstanceType smi) => Game.Instance, delegate(StateMachineInstanceType smi)
			{
				this.GoToCursor(smi);
			}).EventTransition(GameHashes.DestinationReached, exit_state, null)
				.EventTransition(GameHashes.NavigationFailed, exit_state, null);
			return this;
		}

		// Token: 0x060071D2 RID: 29138 RVA: 0x002ABF10 File Offset: 0x002AA110
		public void GoToCursor(StateMachineInstanceType smi)
		{
			smi.GetComponent<Navigator>().GoTo(Grid.PosToCell(DebugHandler.GetMousePos()), new CellOffset[1]);
		}
	}

	// Token: 0x0200100C RID: 4108
	public class DropSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060071D6 RID: 29142 RVA: 0x002ABF50 File Offset: 0x002AA150
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter carrier, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter item, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter drop_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null)
		{
			base.root.Target(carrier).Enter("Drop", delegate(StateMachineInstanceType smi)
			{
				Storage storage = carrier.Get<Storage>(smi);
				GameObject gameObject = item.Get(smi);
				storage.Drop(gameObject, true);
				int num = Grid.CellAbove(Grid.PosToCell(drop_target.Get<Transform>(smi).GetPosition()));
				gameObject.transform.SetPosition(Grid.CellToPosCCC(num, Grid.SceneLayer.Move));
				smi.GoTo(success_state);
			});
			return this;
		}
	}

	// Token: 0x0200100D RID: 4109
	public class FetchSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060071D8 RID: 29144 RVA: 0x002ABFB4 File Offset: 0x002AA1B4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter fetcher, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter pickup_source, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter pickup_chunk, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter requested_amount, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter actual_amount, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null)
		{
			base.Target(fetcher);
			base.root.DefaultState(this.approach).ToggleReserve(fetcher, pickup_source, requested_amount, actual_amount);
			this.approach.InitializeStates(fetcher, pickup_source, this.pickup, null, null, NavigationTactics.ReduceTravelDistance).OnTargetLost(pickup_source, failure_state);
			this.pickup.DoPickup(pickup_source, pickup_chunk, actual_amount, success_state, failure_state);
			return this;
		}

		// Token: 0x0400564B RID: 22091
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ApproachSubState<Pickupable> approach;

		// Token: 0x0400564C RID: 22092
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State pickup;

		// Token: 0x0400564D RID: 22093
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success;
	}

	// Token: 0x0200100E RID: 4110
	public class HungrySubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060071DA RID: 29146 RVA: 0x002AC028 File Offset: 0x002AA228
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target, StatusItem status_item)
		{
			base.Target(target);
			base.root.DefaultState(this.satisfied);
			this.satisfied.EventTransition(GameHashes.AddUrge, this.hungry, (StateMachineInstanceType smi) => GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.HungrySubState.IsHungry(smi));
			this.hungry.EventTransition(GameHashes.RemoveUrge, this.satisfied, (StateMachineInstanceType smi) => !GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.HungrySubState.IsHungry(smi)).ToggleStatusItem(status_item, null);
			return this;
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x002AC0C3 File Offset: 0x002AA2C3
		private static bool IsHungry(StateMachineInstanceType smi)
		{
			return smi.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.Eat);
		}

		// Token: 0x0400564E RID: 22094
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State satisfied;

		// Token: 0x0400564F RID: 22095
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State hungry;
	}

	// Token: 0x0200100F RID: 4111
	public class PlantAliveSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060071DD RID: 29149 RVA: 0x002AC0EC File Offset: 0x002AA2EC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter plant, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State death_state = null)
		{
			base.root.Target(plant).TagTransition(GameTags.Uprooted, death_state, false).EventTransition(GameHashes.TooColdFatal, death_state, (StateMachineInstanceType smi) => GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.PlantAliveSubState.isLethalTemperature(plant.Get(smi)))
				.EventTransition(GameHashes.TooHotFatal, death_state, (StateMachineInstanceType smi) => GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.PlantAliveSubState.isLethalTemperature(plant.Get(smi)))
				.EventTransition(GameHashes.Drowned, death_state, null);
			return this;
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x002AC160 File Offset: 0x002AA360
		public bool ForceUpdateStatus(GameObject plant)
		{
			TemperatureVulnerable component = plant.GetComponent<TemperatureVulnerable>();
			EntombVulnerable component2 = plant.GetComponent<EntombVulnerable>();
			PressureVulnerable component3 = plant.GetComponent<PressureVulnerable>();
			return (component == null || !component.IsLethal) && (component2 == null || !component2.GetEntombed) && (component3 == null || !component3.IsLethal);
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x002AC1BC File Offset: 0x002AA3BC
		private static bool isLethalTemperature(GameObject plant)
		{
			TemperatureVulnerable component = plant.GetComponent<TemperatureVulnerable>();
			return !(component == null) && (component.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.LethalCold || component.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.LethalHot);
		}
	}
}
