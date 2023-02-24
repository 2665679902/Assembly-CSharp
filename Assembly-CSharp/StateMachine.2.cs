using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ImGuiNET;
using KSerialization;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public class StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> : StateMachine where StateMachineInstanceType : StateMachine.Instance where MasterType : IStateMachineTarget
{
	// Token: 0x06001525 RID: 5413 RVA: 0x0006E2CC File Offset: 0x0006C4CC
	public override string[] GetStateNames()
	{
		List<string> list = new List<string>();
		foreach (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state in this.states)
		{
			list.Add(state.name);
		}
		return list.ToArray();
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x0006E330 File Offset: 0x0006C530
	public void Target(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target)
	{
		this.stateTarget = target;
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x0006E33C File Offset: 0x0006C53C
	public void BindState(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State parent_state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, string state_name)
	{
		if (parent_state != null)
		{
			state_name = parent_state.name + "." + state_name;
		}
		state.name = state_name;
		state.longName = this.name + "." + state_name;
		state.debugPushName = "PuS: " + state.longName;
		state.debugPopName = "PoS: " + state.longName;
		state.debugExecuteName = "EA: " + state.longName;
		List<StateMachine.BaseState> list;
		if (parent_state != null)
		{
			list = new List<StateMachine.BaseState>(parent_state.branch);
		}
		else
		{
			list = new List<StateMachine.BaseState>();
		}
		list.Add(state);
		state.parent = parent_state;
		state.branch = list.ToArray();
		this.maxDepth = Math.Max(state.branch.Length, this.maxDepth);
		this.states.Add(state);
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x0006E418 File Offset: 0x0006C618
	public void BindStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State parent_state, object state_machine)
	{
		foreach (FieldInfo fieldInfo in state_machine.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine.BaseState)))
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)fieldInfo.GetValue(state_machine);
				if (state != parent_state)
				{
					string name = fieldInfo.Name;
					this.BindState(parent_state, state, name);
					this.BindStates(state, state);
				}
			}
		}
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x0006E487 File Offset: 0x0006C687
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.InitializeStates(out default_state);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x0006E490 File Offset: 0x0006C690
	public override void BindStates()
	{
		this.BindStates(null, this);
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x0006E49A File Offset: 0x0006C69A
	public override Type GetStateMachineInstanceType()
	{
		return typeof(StateMachineInstanceType);
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x0006E4A8 File Offset: 0x0006C6A8
	public override StateMachine.BaseState GetState(string state_name)
	{
		foreach (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state in this.states)
		{
			if (state.name == state_name)
			{
				return state;
			}
		}
		return null;
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x0006E50C File Offset: 0x0006C70C
	public override void FreeResources()
	{
		for (int i = 0; i < this.states.Count; i++)
		{
			this.states[i].FreeResources();
		}
		this.states.Clear();
		base.FreeResources();
	}

	// Token: 0x04000BDC RID: 3036
	private List<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State> states = new List<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State>();

	// Token: 0x04000BDD RID: 3037
	public StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter masterTarget;

	// Token: 0x04000BDE RID: 3038
	[StateMachine.DoNotAutoCreate]
	protected StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter stateTarget;

	// Token: 0x02001020 RID: 4128
	public class GenericInstance : StateMachine.Instance
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600722D RID: 29229 RVA: 0x002AC80B File Offset: 0x002AAA0B
		// (set) Token: 0x0600722E RID: 29230 RVA: 0x002AC813 File Offset: 0x002AAA13
		public StateMachineType sm { get; private set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600722F RID: 29231 RVA: 0x002AC81C File Offset: 0x002AAA1C
		protected StateMachineInstanceType smi
		{
			get
			{
				return (StateMachineInstanceType)((object)this);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06007230 RID: 29232 RVA: 0x002AC824 File Offset: 0x002AAA24
		// (set) Token: 0x06007231 RID: 29233 RVA: 0x002AC82C File Offset: 0x002AAA2C
		public MasterType master { get; private set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06007232 RID: 29234 RVA: 0x002AC835 File Offset: 0x002AAA35
		// (set) Token: 0x06007233 RID: 29235 RVA: 0x002AC83D File Offset: 0x002AAA3D
		public DefType def { get; set; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06007234 RID: 29236 RVA: 0x002AC846 File Offset: 0x002AAA46
		public bool isMasterNull
		{
			get
			{
				return this.internalSm.masterTarget.IsNull((StateMachineInstanceType)((object)this));
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06007235 RID: 29237 RVA: 0x002AC85E File Offset: 0x002AAA5E
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> internalSm
		{
			get
			{
				return (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>)((object)this.sm);
			}
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x002AC870 File Offset: 0x002AAA70
		protected virtual void OnCleanUp()
		{
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06007237 RID: 29239 RVA: 0x002AC872 File Offset: 0x002AAA72
		public override float timeinstate
		{
			get
			{
				return Time.time - this.stateEnterTime;
			}
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x002AC880 File Offset: 0x002AAA80
		public override void FreeResources()
		{
			this.updateHandle.FreeResources();
			this.updateHandle = default(SchedulerHandle);
			this.controller = null;
			if (this.gotoStack != null)
			{
				this.gotoStack.Clear();
			}
			this.gotoStack = null;
			if (this.transitionStack != null)
			{
				this.transitionStack.Clear();
			}
			this.transitionStack = null;
			if (this.currentSchedulerGroup != null)
			{
				this.currentSchedulerGroup.FreeResources();
			}
			this.currentSchedulerGroup = null;
			if (this.stateStack != null)
			{
				for (int i = 0; i < this.stateStack.Length; i++)
				{
					if (this.stateStack[i].schedulerGroup != null)
					{
						this.stateStack[i].schedulerGroup.FreeResources();
					}
				}
			}
			this.stateStack = null;
			base.FreeResources();
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x002AC94C File Offset: 0x002AAB4C
		public GenericInstance(MasterType master)
			: base((StateMachine)((object)Singleton<StateMachineManager>.Instance.CreateStateMachine<StateMachineType>()), master)
		{
			this.master = master;
			this.stateStack = new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[this.stateMachine.GetMaxDepth()];
			for (int i = 0; i < this.stateStack.Length; i++)
			{
				this.stateStack[i].schedulerGroup = Singleton<StateMachineManager>.Instance.CreateSchedulerGroup();
			}
			this.sm = (StateMachineType)((object)this.stateMachine);
			this.dataTable = new object[base.GetStateMachine().dataTableSize];
			this.updateTable = new StateMachine.Instance.UpdateTableEntry[base.GetStateMachine().updateTableSize];
			this.controller = master.GetComponent<StateMachineController>();
			if (this.controller == null)
			{
				this.controller = master.gameObject.AddComponent<StateMachineController>();
			}
			this.internalSm.masterTarget.Set(master.gameObject, this.smi, false);
			this.controller.AddStateMachineInstance(this);
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x002ACA88 File Offset: 0x002AAC88
		public override IStateMachineTarget GetMaster()
		{
			return this.master;
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x002ACA98 File Offset: 0x002AAC98
		private void PushEvent(StateEvent evt)
		{
			StateEvent.Context context = evt.Subscribe(this);
			this.subscribedEvents.Push(context);
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x002ACABC File Offset: 0x002AACBC
		private void PopEvent()
		{
			StateEvent.Context context = this.subscribedEvents.Pop();
			context.stateEvent.Unsubscribe(this, context);
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x002ACAE4 File Offset: 0x002AACE4
		private bool TryEvaluateTransitions(StateMachine.BaseState state, int goto_id)
		{
			if (state.transitions == null)
			{
				return true;
			}
			bool flag = true;
			for (int i = 0; i < state.transitions.Count; i++)
			{
				StateMachine.BaseTransition baseTransition = state.transitions[i];
				if (goto_id != this.gotoId)
				{
					flag = false;
					break;
				}
				baseTransition.Evaluate(this.smi);
			}
			return flag;
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x002ACB40 File Offset: 0x002AAD40
		private void PushTransitions(StateMachine.BaseState state)
		{
			if (state.transitions == null)
			{
				return;
			}
			for (int i = 0; i < state.transitions.Count; i++)
			{
				StateMachine.BaseTransition baseTransition = state.transitions[i];
				this.PushTransition(baseTransition);
			}
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x002ACB80 File Offset: 0x002AAD80
		private void PushTransition(StateMachine.BaseTransition transition)
		{
			StateMachine.BaseTransition.Context context = transition.Register(this.smi);
			this.transitionStack.Push(context);
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x002ACBAC File Offset: 0x002AADAC
		private void PopTransition(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			StateMachine.BaseTransition.Context context = this.transitionStack.Pop();
			state.transitions[context.idx].Unregister(this.smi, context);
		}

		// Token: 0x06007241 RID: 29249 RVA: 0x002ACBE8 File Offset: 0x002AADE8
		private void PushState(StateMachine.BaseState state)
		{
			int num = this.gotoId;
			this.currentActionIdx = -1;
			if (state.events != null)
			{
				foreach (StateEvent stateEvent in state.events)
				{
					this.PushEvent(stateEvent);
				}
			}
			this.PushTransitions(state);
			if (state.updateActions != null)
			{
				for (int i = 0; i < state.updateActions.Count; i++)
				{
					StateMachine.UpdateAction updateAction = state.updateActions[i];
					int updateTableIdx = updateAction.updateTableIdx;
					int nextBucketIdx = updateAction.nextBucketIdx;
					updateAction.nextBucketIdx = (updateAction.nextBucketIdx + 1) % updateAction.buckets.Length;
					UpdateBucketWithUpdater<StateMachineInstanceType> updateBucketWithUpdater = (UpdateBucketWithUpdater<StateMachineInstanceType>)updateAction.buckets[nextBucketIdx];
					this.smi.updateTable[updateTableIdx].bucket = updateBucketWithUpdater;
					this.smi.updateTable[updateTableIdx].handle = updateBucketWithUpdater.Add(this.smi, Singleton<StateMachineUpdater>.Instance.GetFrameTime(updateAction.updateRate, updateBucketWithUpdater.frame), (UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater)updateAction.updater);
					state.updateActions[i] = updateAction;
				}
			}
			this.stateEnterTime = Time.time;
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[] array = this.stateStack;
			int stackSize = this.stackSize;
			this.stackSize = stackSize + 1;
			array[stackSize].state = state;
			this.currentSchedulerGroup = this.stateStack[this.stackSize - 1].schedulerGroup;
			if (!this.TryEvaluateTransitions(state, num))
			{
				return;
			}
			if (num != this.gotoId)
			{
				return;
			}
			this.ExecuteActions((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state, state.enterActions);
			int num2 = this.gotoId;
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x002ACDC4 File Offset: 0x002AAFC4
		private void ExecuteActions(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, List<StateMachine.Action> actions)
		{
			if (actions == null)
			{
				return;
			}
			int num = this.gotoId;
			this.currentActionIdx++;
			while (this.currentActionIdx < actions.Count && num == this.gotoId)
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback)actions[this.currentActionIdx].callback;
				try
				{
					callback(this.smi);
				}
				catch (Exception ex)
				{
					if (!StateMachine.Instance.error)
					{
						base.Error();
						string text = "(NULL).";
						IStateMachineTarget master = this.GetMaster();
						if (!master.isNull)
						{
							KPrefabID component = master.GetComponent<KPrefabID>();
							if (component != null)
							{
								text = "(" + component.PrefabTag.ToString() + ").";
							}
							else
							{
								text = "(" + base.gameObject.name + ").";
							}
						}
						string text2 = string.Concat(new string[]
						{
							"Exception in: ",
							text,
							this.stateMachine.ToString(),
							".",
							state.name,
							"."
						});
						if (this.currentActionIdx > 0 && this.currentActionIdx < actions.Count)
						{
							text2 += actions[this.currentActionIdx].name;
						}
						DebugUtil.LogException(this.controller, text2, ex);
					}
				}
				this.currentActionIdx++;
			}
			this.currentActionIdx = 2147483646;
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x002ACF58 File Offset: 0x002AB158
		private void PopState()
		{
			this.currentActionIdx = -1;
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[] array = this.stateStack;
			int num = this.stackSize - 1;
			this.stackSize = num;
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry stackEntry = array[num];
			StateMachine.BaseState state = stackEntry.state;
			int num2 = 0;
			while (state.transitions != null && num2 < state.transitions.Count)
			{
				this.PopTransition((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state);
				num2++;
			}
			if (state.events != null)
			{
				for (int i = 0; i < state.events.Count; i++)
				{
					this.PopEvent();
				}
			}
			if (state.updateActions != null)
			{
				foreach (StateMachine.UpdateAction updateAction in state.updateActions)
				{
					int updateTableIdx = updateAction.updateTableIdx;
					StateMachineUpdater.BaseUpdateBucket baseUpdateBucket = (UpdateBucketWithUpdater<StateMachineInstanceType>)this.smi.updateTable[updateTableIdx].bucket;
					this.smi.updateTable[updateTableIdx].bucket = null;
					baseUpdateBucket.Remove(this.smi.updateTable[updateTableIdx].handle);
				}
			}
			stackEntry.schedulerGroup.Reset();
			this.currentSchedulerGroup = stackEntry.schedulerGroup;
			this.ExecuteActions((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state, state.exitActions);
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x002AD0BC File Offset: 0x002AB2BC
		public override SchedulerHandle Schedule(float time, Action<object> callback, object callback_data = null)
		{
			string text = null;
			return Singleton<StateMachineManager>.Instance.Schedule(text, time, callback, callback_data, this.currentSchedulerGroup);
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x002AD0E0 File Offset: 0x002AB2E0
		public override SchedulerHandle ScheduleNextFrame(Action<object> callback, object callback_data = null)
		{
			string text = null;
			return Singleton<StateMachineManager>.Instance.ScheduleNextFrame(text, callback, callback_data, this.currentSchedulerGroup);
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x002AD102 File Offset: 0x002AB302
		public override void StartSM()
		{
			if (this.controller != null && !this.controller.HasStateMachineInstance(this))
			{
				this.controller.AddStateMachineInstance(this);
			}
			base.StartSM();
		}

		// Token: 0x06007247 RID: 29255 RVA: 0x002AD134 File Offset: 0x002AB334
		public override void StopSM(string reason)
		{
			if (StateMachine.Instance.error)
			{
				return;
			}
			if (this.controller != null)
			{
				this.controller.RemoveStateMachineInstance(this);
			}
			if (!base.IsRunning())
			{
				return;
			}
			this.gotoId++;
			while (this.stackSize > 0)
			{
				this.PopState();
			}
			if (this.master != null && this.controller != null)
			{
				this.controller.RemoveStateMachineInstance(this);
			}
			if (this.status == StateMachine.Status.Running)
			{
				base.SetStatus(StateMachine.Status.Failed);
			}
			if (this.OnStop != null)
			{
				this.OnStop(reason, this.status);
			}
			for (int i = 0; i < this.parameterContexts.Length; i++)
			{
				this.parameterContexts[i].Cleanup();
			}
			this.OnCleanUp();
		}

		// Token: 0x06007248 RID: 29256 RVA: 0x002AD202 File Offset: 0x002AB402
		private void FinishStateInProgress(StateMachine.BaseState state)
		{
			if (state.enterActions == null)
			{
				return;
			}
			this.ExecuteActions((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state, state.enterActions);
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x002AD220 File Offset: 0x002AB420
		public override void GoTo(StateMachine.BaseState base_state)
		{
			if (App.IsExiting)
			{
				return;
			}
			if (StateMachine.Instance.error)
			{
				return;
			}
			if (this.isMasterNull)
			{
				return;
			}
			if (this.smi.IsNullOrDestroyed())
			{
				return;
			}
			try
			{
				if (base.IsBreakOnGoToEnabled())
				{
					Debugger.Break();
				}
				if (base_state != null)
				{
					while (base_state.defaultState != null)
					{
						base_state = base_state.defaultState;
					}
				}
				if (this.GetCurrentState() == null)
				{
					base.SetStatus(StateMachine.Status.Running);
				}
				if (this.gotoStack.Count > 100)
				{
					string text = "Potential infinite transition loop detected in state machine: " + this.ToString() + "\nGoto stack:\n";
					foreach (StateMachine.BaseState baseState in this.gotoStack)
					{
						text = text + "\n" + baseState.name;
					}
					global::Debug.LogError(text);
					base.Error();
				}
				else
				{
					this.gotoStack.Push(base_state);
					if (base_state == null)
					{
						this.StopSM("StateMachine.GoTo(null)");
						this.gotoStack.Pop();
					}
					else
					{
						int num = this.gotoId + 1;
						this.gotoId = num;
						int num2 = num;
						StateMachine.BaseState[] branch = (base_state as StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State).branch;
						int num3 = 0;
						while (num3 < this.stackSize && num3 < branch.Length && this.stateStack[num3].state == branch[num3])
						{
							num3++;
						}
						int num4 = this.stackSize - 1;
						if (num4 >= 0 && num4 == num3 - 1)
						{
							this.FinishStateInProgress(this.stateStack[num4].state);
						}
						while (this.stackSize > num3 && num2 == this.gotoId)
						{
							this.PopState();
						}
						int num5 = num3;
						while (num5 < branch.Length && num2 == this.gotoId)
						{
							this.PushState(branch[num5]);
							num5++;
						}
						this.gotoStack.Pop();
					}
				}
			}
			catch (Exception ex)
			{
				if (!StateMachine.Instance.error)
				{
					base.Error();
					string text2 = "(Stop)";
					if (base_state != null)
					{
						text2 = base_state.name;
					}
					string text3 = "(NULL).";
					if (!this.GetMaster().isNull)
					{
						text3 = "(" + base.gameObject.name + ").";
					}
					string text4 = string.Concat(new string[]
					{
						"Exception in: ",
						text3,
						this.stateMachine.ToString(),
						".GoTo(",
						text2,
						")"
					});
					DebugUtil.LogErrorArgs(this.controller, new object[] { text4 + "\n" + ex.ToString() });
				}
			}
		}

		// Token: 0x0600724A RID: 29258 RVA: 0x002AD4EC File Offset: 0x002AB6EC
		public override StateMachine.BaseState GetCurrentState()
		{
			if (this.stackSize > 0)
			{
				return this.stateStack[this.stackSize - 1].state;
			}
			return null;
		}

		// Token: 0x04005689 RID: 22153
		private float stateEnterTime;

		// Token: 0x0400568A RID: 22154
		private int gotoId;

		// Token: 0x0400568B RID: 22155
		private int currentActionIdx = -1;

		// Token: 0x0400568C RID: 22156
		private SchedulerHandle updateHandle;

		// Token: 0x0400568D RID: 22157
		private Stack<StateMachine.BaseState> gotoStack = new Stack<StateMachine.BaseState>();

		// Token: 0x0400568E RID: 22158
		protected Stack<StateMachine.BaseTransition.Context> transitionStack = new Stack<StateMachine.BaseTransition.Context>();

		// Token: 0x04005692 RID: 22162
		protected StateMachineController controller;

		// Token: 0x04005693 RID: 22163
		private SchedulerGroup currentSchedulerGroup;

		// Token: 0x04005694 RID: 22164
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[] stateStack;

		// Token: 0x02001F55 RID: 8021
		public struct StackEntry
		{
			// Token: 0x04008B9C RID: 35740
			public StateMachine.BaseState state;

			// Token: 0x04008B9D RID: 35741
			public SchedulerGroup schedulerGroup;
		}
	}

	// Token: 0x02001021 RID: 4129
	public class State : StateMachine.BaseState
	{
		// Token: 0x04005695 RID: 22165
		protected StateMachineType sm;

		// Token: 0x02001F56 RID: 8022
		// (Invoke) Token: 0x06009E7C RID: 40572
		public delegate void Callback(StateMachineInstanceType smi);
	}

	// Token: 0x02001022 RID: 4130
	public new abstract class ParameterTransition : StateMachine.ParameterTransition
	{
		// Token: 0x0600724C RID: 29260 RVA: 0x002AD519 File Offset: 0x002AB719
		public ParameterTransition(int idx, string name, StateMachine.BaseState source_state, StateMachine.BaseState target_state)
			: base(idx, name, source_state, target_state)
		{
		}
	}

	// Token: 0x02001023 RID: 4131
	public class Transition : StateMachine.BaseTransition
	{
		// Token: 0x0600724D RID: 29261 RVA: 0x002AD526 File Offset: 0x002AB726
		public Transition(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State source_state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, int idx, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition)
			: base(idx, name, source_state, target_state)
		{
			this.condition = condition;
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x002AD53B File Offset: 0x002AB73B
		public override string ToString()
		{
			if (this.targetState != null)
			{
				return this.name + "->" + this.targetState.name;
			}
			return this.name + "->(Stop)";
		}

		// Token: 0x04005696 RID: 22166
		public StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition;

		// Token: 0x02001F57 RID: 8023
		// (Invoke) Token: 0x06009E80 RID: 40576
		public delegate bool ConditionCallback(StateMachineInstanceType smi);
	}

	// Token: 0x02001024 RID: 4132
	public abstract class Parameter<ParameterType> : StateMachine.Parameter
	{
		// Token: 0x0600724F RID: 29263 RVA: 0x002AD571 File Offset: 0x002AB771
		public Parameter()
		{
		}

		// Token: 0x06007250 RID: 29264 RVA: 0x002AD579 File Offset: 0x002AB779
		public Parameter(ParameterType default_value)
		{
			this.defaultValue = default_value;
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x002AD588 File Offset: 0x002AB788
		public ParameterType Set(ParameterType value, StateMachineInstanceType smi, bool silenceEvents = false)
		{
			((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this)).Set(value, smi, silenceEvents);
			return value;
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x002AD5A4 File Offset: 0x002AB7A4
		public ParameterType Get(StateMachineInstanceType smi)
		{
			return ((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this)).value;
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x002AD5BC File Offset: 0x002AB7BC
		public StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context GetContext(StateMachineInstanceType smi)
		{
			return (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this);
		}

		// Token: 0x04005697 RID: 22167
		public ParameterType defaultValue;

		// Token: 0x04005698 RID: 22168
		public bool isSignal;

		// Token: 0x02001F58 RID: 8024
		// (Invoke) Token: 0x06009E84 RID: 40580
		public delegate bool Callback(StateMachineInstanceType smi, ParameterType p);

		// Token: 0x02001F59 RID: 8025
		public class Transition : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ParameterTransition
		{
			// Token: 0x06009E87 RID: 40583 RVA: 0x0033EA76 File Offset: 0x0033CC76
			public Transition(int idx, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType> parameter, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Callback callback)
				: base(idx, parameter.name, null, state)
			{
				this.parameter = parameter;
				this.callback = callback;
			}

			// Token: 0x06009E88 RID: 40584 RVA: 0x0033EA98 File Offset: 0x0033CC98
			public override void Evaluate(StateMachine.Instance smi)
			{
				StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
				global::Debug.Assert(stateMachineInstanceType != null);
				if (this.parameter.isSignal && this.callback == null)
				{
					return;
				}
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)stateMachineInstanceType.GetParameterContext(this.parameter);
				if (this.callback(stateMachineInstanceType, context.value))
				{
					stateMachineInstanceType.GoTo(this.targetState);
				}
			}

			// Token: 0x06009E89 RID: 40585 RVA: 0x0033EB11 File Offset: 0x0033CD11
			private void Trigger(StateMachineInstanceType smi)
			{
				smi.GoTo(this.targetState);
			}

			// Token: 0x06009E8A RID: 40586 RVA: 0x0033EB24 File Offset: 0x0033CD24
			public override StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this.parameter);
				if (this.parameter.isSignal && this.callback == null)
				{
					StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context2 = context;
					context2.onDirty = (Action<StateMachineInstanceType>)Delegate.Combine(context2.onDirty, new Action<StateMachineInstanceType>(this.Trigger));
				}
				else
				{
					StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context3 = context;
					context3.onDirty = (Action<StateMachineInstanceType>)Delegate.Combine(context3.onDirty, new Action<StateMachineInstanceType>(this.Evaluate));
				}
				return new StateMachine.BaseTransition.Context(this);
			}

			// Token: 0x06009E8B RID: 40587 RVA: 0x0033EBA8 File Offset: 0x0033CDA8
			public override void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context transitionContext)
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this.parameter);
				if (this.parameter.isSignal && this.callback == null)
				{
					StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context2 = context;
					context2.onDirty = (Action<StateMachineInstanceType>)Delegate.Remove(context2.onDirty, new Action<StateMachineInstanceType>(this.Trigger));
					return;
				}
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context3 = context;
				context3.onDirty = (Action<StateMachineInstanceType>)Delegate.Remove(context3.onDirty, new Action<StateMachineInstanceType>(this.Evaluate));
			}

			// Token: 0x06009E8C RID: 40588 RVA: 0x0033EC22 File Offset: 0x0033CE22
			public override string ToString()
			{
				if (this.targetState != null)
				{
					return this.parameter.name + "->" + this.targetState.name;
				}
				return this.parameter.name + "->(Stop)";
			}

			// Token: 0x04008B9E RID: 35742
			private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType> parameter;

			// Token: 0x04008B9F RID: 35743
			private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Callback callback;
		}

		// Token: 0x02001F5A RID: 8026
		public new abstract class Context : StateMachine.Parameter.Context
		{
			// Token: 0x06009E8D RID: 40589 RVA: 0x0033EC62 File Offset: 0x0033CE62
			public Context(StateMachine.Parameter parameter, ParameterType default_value)
				: base(parameter)
			{
				this.value = default_value;
			}

			// Token: 0x06009E8E RID: 40590 RVA: 0x0033EC72 File Offset: 0x0033CE72
			public virtual void Set(ParameterType value, StateMachineInstanceType smi, bool silenceEvents = false)
			{
				if (!EqualityComparer<ParameterType>.Default.Equals(value, this.value))
				{
					this.value = value;
					if (!silenceEvents && this.onDirty != null)
					{
						this.onDirty(smi);
					}
				}
			}

			// Token: 0x04008BA0 RID: 35744
			public ParameterType value;

			// Token: 0x04008BA1 RID: 35745
			public Action<StateMachineInstanceType> onDirty;
		}
	}

	// Token: 0x02001025 RID: 4133
	public class BoolParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>
	{
		// Token: 0x06007254 RID: 29268 RVA: 0x002AD5CF File Offset: 0x002AB7CF
		public BoolParameter()
		{
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x002AD5D7 File Offset: 0x002AB7D7
		public BoolParameter(bool default_value)
			: base(default_value)
		{
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x002AD5E0 File Offset: 0x002AB7E0
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.BoolParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02001F5B RID: 8027
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>.Context
		{
			// Token: 0x06009E8F RID: 40591 RVA: 0x0033ECA5 File Offset: 0x0033CEA5
			public Context(StateMachine.Parameter parameter, bool default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009E90 RID: 40592 RVA: 0x0033ECAF File Offset: 0x0033CEAF
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value ? 1 : 0);
			}

			// Token: 0x06009E91 RID: 40593 RVA: 0x0033ECC4 File Offset: 0x0033CEC4
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadByte() > 0;
			}

			// Token: 0x06009E92 RID: 40594 RVA: 0x0033ECD5 File Offset: 0x0033CED5
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009E93 RID: 40595 RVA: 0x0033ECD8 File Offset: 0x0033CED8
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				bool value = this.value;
				if (ImGui.Checkbox(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x02001026 RID: 4134
	public class Vector3Parameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Vector3>
	{
		// Token: 0x06007257 RID: 29271 RVA: 0x002AD5EE File Offset: 0x002AB7EE
		public Vector3Parameter()
		{
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x002AD5F6 File Offset: 0x002AB7F6
		public Vector3Parameter(Vector3 default_value)
			: base(default_value)
		{
		}

		// Token: 0x06007259 RID: 29273 RVA: 0x002AD5FF File Offset: 0x002AB7FF
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Vector3Parameter.Context(this, this.defaultValue);
		}

		// Token: 0x02001F5C RID: 8028
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Vector3>.Context
		{
			// Token: 0x06009E94 RID: 40596 RVA: 0x0033ED10 File Offset: 0x0033CF10
			public Context(StateMachine.Parameter parameter, Vector3 default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009E95 RID: 40597 RVA: 0x0033ED1A File Offset: 0x0033CF1A
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value.x);
				writer.Write(this.value.y);
				writer.Write(this.value.z);
			}

			// Token: 0x06009E96 RID: 40598 RVA: 0x0033ED4F File Offset: 0x0033CF4F
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value.x = reader.ReadSingle();
				this.value.y = reader.ReadSingle();
				this.value.z = reader.ReadSingle();
			}

			// Token: 0x06009E97 RID: 40599 RVA: 0x0033ED84 File Offset: 0x0033CF84
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009E98 RID: 40600 RVA: 0x0033ED88 File Offset: 0x0033CF88
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				Vector3 value = this.value;
				if (ImGui.InputFloat3(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x02001027 RID: 4135
	public class EnumParameter<EnumType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<EnumType>
	{
		// Token: 0x0600725A RID: 29274 RVA: 0x002AD60D File Offset: 0x002AB80D
		public EnumParameter(EnumType default_value)
			: base(default_value)
		{
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x002AD616 File Offset: 0x002AB816
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.EnumParameter<EnumType>.Context(this, this.defaultValue);
		}

		// Token: 0x02001F5D RID: 8029
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<EnumType>.Context
		{
			// Token: 0x06009E99 RID: 40601 RVA: 0x0033EDC0 File Offset: 0x0033CFC0
			public Context(StateMachine.Parameter parameter, EnumType default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009E9A RID: 40602 RVA: 0x0033EDCA File Offset: 0x0033CFCA
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write((int)((object)this.value));
			}

			// Token: 0x06009E9B RID: 40603 RVA: 0x0033EDE2 File Offset: 0x0033CFE2
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = (EnumType)((object)reader.ReadInt32());
			}

			// Token: 0x06009E9C RID: 40604 RVA: 0x0033EDFA File Offset: 0x0033CFFA
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009E9D RID: 40605 RVA: 0x0033EDFC File Offset: 0x0033CFFC
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				string[] names = Enum.GetNames(typeof(EnumType));
				Array values = Enum.GetValues(typeof(EnumType));
				int num = Array.IndexOf(values, this.value);
				if (ImGui.Combo(this.parameter.name, ref num, names, names.Length))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set((EnumType)((object)values.GetValue(num)), stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x02001028 RID: 4136
	public class FloatParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>
	{
		// Token: 0x0600725C RID: 29276 RVA: 0x002AD624 File Offset: 0x002AB824
		public FloatParameter()
		{
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x002AD62C File Offset: 0x002AB82C
		public FloatParameter(float default_value)
			: base(default_value)
		{
		}

		// Token: 0x0600725E RID: 29278 RVA: 0x002AD638 File Offset: 0x002AB838
		public float Delta(float delta_value, StateMachineInstanceType smi)
		{
			float num = base.Get(smi);
			num += delta_value;
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x0600725F RID: 29279 RVA: 0x002AD65C File Offset: 0x002AB85C
		public float DeltaClamp(float delta_value, float min_value, float max_value, StateMachineInstanceType smi)
		{
			float num = base.Get(smi);
			num += delta_value;
			num = Mathf.Clamp(num, min_value, max_value);
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x002AD68B File Offset: 0x002AB88B
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02001F5E RID: 8030
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Context
		{
			// Token: 0x06009E9E RID: 40606 RVA: 0x0033EE6E File Offset: 0x0033D06E
			public Context(StateMachine.Parameter parameter, float default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009E9F RID: 40607 RVA: 0x0033EE78 File Offset: 0x0033D078
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value);
			}

			// Token: 0x06009EA0 RID: 40608 RVA: 0x0033EE86 File Offset: 0x0033D086
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadSingle();
			}

			// Token: 0x06009EA1 RID: 40609 RVA: 0x0033EE94 File Offset: 0x0033D094
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EA2 RID: 40610 RVA: 0x0033EE98 File Offset: 0x0033D098
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				float value = this.value;
				if (ImGui.InputFloat(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x02001029 RID: 4137
	public class IntParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>
	{
		// Token: 0x06007261 RID: 29281 RVA: 0x002AD699 File Offset: 0x002AB899
		public IntParameter()
		{
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x002AD6A1 File Offset: 0x002AB8A1
		public IntParameter(int default_value)
			: base(default_value)
		{
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x002AD6AC File Offset: 0x002AB8AC
		public int Delta(int delta_value, StateMachineInstanceType smi)
		{
			int num = base.Get(smi);
			num += delta_value;
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x002AD6D0 File Offset: 0x002AB8D0
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.IntParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02001F5F RID: 8031
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>.Context
		{
			// Token: 0x06009EA3 RID: 40611 RVA: 0x0033EED0 File Offset: 0x0033D0D0
			public Context(StateMachine.Parameter parameter, int default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009EA4 RID: 40612 RVA: 0x0033EEDA File Offset: 0x0033D0DA
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value);
			}

			// Token: 0x06009EA5 RID: 40613 RVA: 0x0033EEE8 File Offset: 0x0033D0E8
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadInt32();
			}

			// Token: 0x06009EA6 RID: 40614 RVA: 0x0033EEF6 File Offset: 0x0033D0F6
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EA7 RID: 40615 RVA: 0x0033EEF8 File Offset: 0x0033D0F8
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				int value = this.value;
				if (ImGui.InputInt(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x0200102A RID: 4138
	public class ResourceParameter<ResourceType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ResourceType> where ResourceType : Resource
	{
		// Token: 0x06007265 RID: 29285 RVA: 0x002AD6E0 File Offset: 0x002AB8E0
		public ResourceParameter()
			: base(default(ResourceType))
		{
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x002AD6FC File Offset: 0x002AB8FC
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ResourceParameter<ResourceType>.Context(this, this.defaultValue);
		}

		// Token: 0x02001F60 RID: 8032
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ResourceType>.Context
		{
			// Token: 0x06009EA8 RID: 40616 RVA: 0x0033EF30 File Offset: 0x0033D130
			public Context(StateMachine.Parameter parameter, ResourceType default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009EA9 RID: 40617 RVA: 0x0033EF3C File Offset: 0x0033D13C
			public override void Serialize(BinaryWriter writer)
			{
				string text = "";
				if (this.value != null)
				{
					if (this.value.Guid == null)
					{
						global::Debug.LogError("Cannot serialize resource with invalid guid: " + this.value.Id);
					}
					else
					{
						text = this.value.Guid.Guid;
					}
				}
				writer.WriteKleiString(text);
			}

			// Token: 0x06009EAA RID: 40618 RVA: 0x0033EFB4 File Offset: 0x0033D1B4
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				string text = reader.ReadKleiString();
				if (text != "")
				{
					ResourceGuid resourceGuid = new ResourceGuid(text, null);
					this.value = Db.Get().GetResource<ResourceType>(resourceGuid);
				}
			}

			// Token: 0x06009EAB RID: 40619 RVA: 0x0033EFEE File Offset: 0x0033D1EE
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EAC RID: 40620 RVA: 0x0033EFF0 File Offset: 0x0033D1F0
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				string text = "None";
				if (this.value != null)
				{
					text = this.value.ToString();
				}
				ImGui.LabelText(this.parameter.name, text);
			}
		}
	}

	// Token: 0x0200102B RID: 4139
	public class TagParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Tag>
	{
		// Token: 0x06007267 RID: 29287 RVA: 0x002AD70A File Offset: 0x002AB90A
		public TagParameter()
		{
		}

		// Token: 0x06007268 RID: 29288 RVA: 0x002AD712 File Offset: 0x002AB912
		public TagParameter(Tag default_value)
			: base(default_value)
		{
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x002AD71B File Offset: 0x002AB91B
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02001F61 RID: 8033
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Tag>.Context
		{
			// Token: 0x06009EAD RID: 40621 RVA: 0x0033F032 File Offset: 0x0033D232
			public Context(StateMachine.Parameter parameter, Tag default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009EAE RID: 40622 RVA: 0x0033F03C File Offset: 0x0033D23C
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value.GetHash());
			}

			// Token: 0x06009EAF RID: 40623 RVA: 0x0033F04F File Offset: 0x0033D24F
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = new Tag(reader.ReadInt32());
			}

			// Token: 0x06009EB0 RID: 40624 RVA: 0x0033F062 File Offset: 0x0033D262
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EB1 RID: 40625 RVA: 0x0033F064 File Offset: 0x0033D264
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				ImGui.LabelText(this.parameter.name, this.value.ToString());
			}
		}
	}

	// Token: 0x0200102C RID: 4140
	public class ObjectParameter<ObjectType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ObjectType> where ObjectType : class
	{
		// Token: 0x0600726A RID: 29290 RVA: 0x002AD72C File Offset: 0x002AB92C
		public ObjectParameter()
			: base(default(ObjectType))
		{
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x002AD748 File Offset: 0x002AB948
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ObjectParameter<ObjectType>.Context(this, this.defaultValue);
		}

		// Token: 0x02001F62 RID: 8034
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ObjectType>.Context
		{
			// Token: 0x06009EB2 RID: 40626 RVA: 0x0033F087 File Offset: 0x0033D287
			public Context(StateMachine.Parameter parameter, ObjectType default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009EB3 RID: 40627 RVA: 0x0033F091 File Offset: 0x0033D291
			public override void Serialize(BinaryWriter writer)
			{
				DebugUtil.DevLogError("ObjectParameter cannot be serialized");
			}

			// Token: 0x06009EB4 RID: 40628 RVA: 0x0033F09D File Offset: 0x0033D29D
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				DebugUtil.DevLogError("ObjectParameter cannot be serialized");
			}

			// Token: 0x06009EB5 RID: 40629 RVA: 0x0033F0A9 File Offset: 0x0033D2A9
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EB6 RID: 40630 RVA: 0x0033F0AC File Offset: 0x0033D2AC
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				string text = "None";
				if (this.value != null)
				{
					text = this.value.ToString();
				}
				ImGui.LabelText(this.parameter.name, text);
			}
		}
	}

	// Token: 0x0200102D RID: 4141
	public class TargetParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>
	{
		// Token: 0x0600726C RID: 29292 RVA: 0x002AD756 File Offset: 0x002AB956
		public TargetParameter()
			: base(null)
		{
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x002AD760 File Offset: 0x002AB960
		public SMT GetSMI<SMT>(StateMachineInstanceType smi) where SMT : StateMachine.Instance
		{
			GameObject gameObject = base.Get(smi);
			if (gameObject != null)
			{
				SMT smi2 = gameObject.GetSMI<SMT>();
				if (smi2 != null)
				{
					return smi2;
				}
				global::Debug.LogError(gameObject.name + " does not have state machine " + typeof(StateMachineType).Name);
			}
			return default(SMT);
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x002AD7BC File Offset: 0x002AB9BC
		public bool IsNull(StateMachineInstanceType smi)
		{
			return base.Get(smi) == null;
		}

		// Token: 0x0600726F RID: 29295 RVA: 0x002AD7CC File Offset: 0x002AB9CC
		public ComponentType Get<ComponentType>(StateMachineInstanceType smi)
		{
			GameObject gameObject = base.Get(smi);
			if (gameObject != null)
			{
				ComponentType component = gameObject.GetComponent<ComponentType>();
				if (component != null)
				{
					return component;
				}
				global::Debug.LogError(gameObject.name + " does not have component " + typeof(ComponentType).Name);
			}
			return default(ComponentType);
		}

		// Token: 0x06007270 RID: 29296 RVA: 0x002AD828 File Offset: 0x002ABA28
		public ComponentType AddOrGet<ComponentType>(StateMachineInstanceType smi) where ComponentType : Component
		{
			GameObject gameObject = base.Get(smi);
			if (gameObject != null)
			{
				ComponentType componentType = gameObject.GetComponent<ComponentType>();
				if (componentType == null)
				{
					componentType = gameObject.AddComponent<ComponentType>();
				}
				return componentType;
			}
			return default(ComponentType);
		}

		// Token: 0x06007271 RID: 29297 RVA: 0x002AD870 File Offset: 0x002ABA70
		public void Set(KMonoBehaviour value, StateMachineInstanceType smi)
		{
			GameObject gameObject = null;
			if (value != null)
			{
				gameObject = value.gameObject;
			}
			base.Set(gameObject, smi, false);
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x002AD899 File Offset: 0x002ABA99
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02001F63 RID: 8035
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>.Context
		{
			// Token: 0x06009EB7 RID: 40631 RVA: 0x0033F0EE File Offset: 0x0033D2EE
			public Context(StateMachine.Parameter parameter, GameObject default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009EB8 RID: 40632 RVA: 0x0033F0F8 File Offset: 0x0033D2F8
			public override void Serialize(BinaryWriter writer)
			{
				if (this.value != null)
				{
					int instanceID = this.value.GetComponent<KPrefabID>().InstanceID;
					writer.Write(instanceID);
					return;
				}
				writer.Write(0);
			}

			// Token: 0x06009EB9 RID: 40633 RVA: 0x0033F134 File Offset: 0x0033D334
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				try
				{
					int num = reader.ReadInt32();
					if (num != 0)
					{
						KPrefabID instance = KPrefabIDTracker.Get().GetInstance(num);
						if (instance != null)
						{
							this.value = instance.gameObject;
							this.objectDestroyedHandler = instance.Subscribe(1969584890, new Action<object>(this.OnObjectDestroyed));
						}
						this.m_smi = (StateMachineInstanceType)((object)smi);
					}
				}
				catch (Exception ex)
				{
					if (!SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 20))
					{
						global::Debug.LogWarning("Missing statemachine target params. " + ex.Message);
					}
				}
			}

			// Token: 0x06009EBA RID: 40634 RVA: 0x0033F1D8 File Offset: 0x0033D3D8
			public override void Cleanup()
			{
				base.Cleanup();
				if (this.value != null)
				{
					this.value.GetComponent<KMonoBehaviour>().Unsubscribe(this.objectDestroyedHandler);
					this.objectDestroyedHandler = 0;
				}
			}

			// Token: 0x06009EBB RID: 40635 RVA: 0x0033F20C File Offset: 0x0033D40C
			public override void Set(GameObject value, StateMachineInstanceType smi, bool silenceEvents = false)
			{
				this.m_smi = smi;
				if (this.value != null)
				{
					this.value.GetComponent<KMonoBehaviour>().Unsubscribe(this.objectDestroyedHandler);
					this.objectDestroyedHandler = 0;
				}
				if (value != null)
				{
					this.objectDestroyedHandler = value.GetComponent<KMonoBehaviour>().Subscribe(1969584890, new Action<object>(this.OnObjectDestroyed));
				}
				base.Set(value, smi, silenceEvents);
			}

			// Token: 0x06009EBC RID: 40636 RVA: 0x0033F27F File Offset: 0x0033D47F
			private void OnObjectDestroyed(object data)
			{
				this.Set(null, this.m_smi, false);
			}

			// Token: 0x06009EBD RID: 40637 RVA: 0x0033F28F File Offset: 0x0033D48F
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EBE RID: 40638 RVA: 0x0033F294 File Offset: 0x0033D494
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				if (this.value != null)
				{
					ImGui.LabelText(this.parameter.name, this.value.name);
					return;
				}
				ImGui.LabelText(this.parameter.name, "null");
			}

			// Token: 0x04008BA2 RID: 35746
			private StateMachineInstanceType m_smi;

			// Token: 0x04008BA3 RID: 35747
			private int objectDestroyedHandler;
		}
	}

	// Token: 0x0200102E RID: 4142
	public class SignalParameter
	{
	}

	// Token: 0x0200102F RID: 4143
	public class Signal : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>
	{
		// Token: 0x06007274 RID: 29300 RVA: 0x002AD8AF File Offset: 0x002ABAAF
		public Signal()
			: base(null)
		{
			this.isSignal = true;
		}

		// Token: 0x06007275 RID: 29301 RVA: 0x002AD8BF File Offset: 0x002ABABF
		public void Trigger(StateMachineInstanceType smi)
		{
			((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal.Context)smi.GetParameterContext(this)).Set(null, smi, false);
		}

		// Token: 0x06007276 RID: 29302 RVA: 0x002AD8DA File Offset: 0x002ABADA
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal.Context(this, this.defaultValue);
		}

		// Token: 0x02001F64 RID: 8036
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>.Context
		{
			// Token: 0x06009EBF RID: 40639 RVA: 0x0033F2E0 File Offset: 0x0033D4E0
			public Context(StateMachine.Parameter parameter, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x06009EC0 RID: 40640 RVA: 0x0033F2EA File Offset: 0x0033D4EA
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x06009EC1 RID: 40641 RVA: 0x0033F2EC File Offset: 0x0033D4EC
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
			}

			// Token: 0x06009EC2 RID: 40642 RVA: 0x0033F2EE File Offset: 0x0033D4EE
			public override void Set(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter value, StateMachineInstanceType smi, bool silenceEvents = false)
			{
				if (!silenceEvents && this.onDirty != null)
				{
					this.onDirty(smi);
				}
			}

			// Token: 0x06009EC3 RID: 40643 RVA: 0x0033F307 File Offset: 0x0033D507
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x06009EC4 RID: 40644 RVA: 0x0033F30C File Offset: 0x0033D50C
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				if (ImGui.Button(this.parameter.name))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(null, stateMachineInstanceType, false);
				}
			}
		}
	}
}
