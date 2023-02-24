using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using KSerialization;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public abstract class StateMachine
{
	// Token: 0x06001515 RID: 5397 RVA: 0x0006E0AF File Offset: 0x0006C2AF
	public StateMachine()
	{
		this.name = base.GetType().FullName;
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x0006E0D4 File Offset: 0x0006C2D4
	public virtual void FreeResources()
	{
		this.name = null;
		if (this.defaultState != null)
		{
			this.defaultState.FreeResources();
		}
		this.defaultState = null;
		this.parameters = null;
	}

	// Token: 0x06001517 RID: 5399
	public abstract string[] GetStateNames();

	// Token: 0x06001518 RID: 5400
	public abstract StateMachine.BaseState GetState(string name);

	// Token: 0x06001519 RID: 5401
	public abstract void BindStates();

	// Token: 0x0600151A RID: 5402
	public abstract Type GetStateMachineInstanceType();

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600151B RID: 5403 RVA: 0x0006E0FE File Offset: 0x0006C2FE
	// (set) Token: 0x0600151C RID: 5404 RVA: 0x0006E106 File Offset: 0x0006C306
	public int version { get; protected set; }

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600151D RID: 5405 RVA: 0x0006E10F File Offset: 0x0006C30F
	// (set) Token: 0x0600151E RID: 5406 RVA: 0x0006E117 File Offset: 0x0006C317
	public StateMachine.SerializeType serializable { get; protected set; }

	// Token: 0x0600151F RID: 5407 RVA: 0x0006E120 File Offset: 0x0006C320
	public virtual void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = null;
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x0006E128 File Offset: 0x0006C328
	public void InitializeStateMachine()
	{
		this.debugSettings = StateMachineDebuggerSettings.Get().CreateEntry(base.GetType());
		StateMachine.BaseState baseState = null;
		this.InitializeStates(out baseState);
		DebugUtil.Assert(baseState != null);
		this.defaultState = baseState;
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x0006E168 File Offset: 0x0006C368
	public void CreateStates(object state_machine)
	{
		foreach (FieldInfo fieldInfo in state_machine.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			bool flag = false;
			object[] customAttributes = fieldInfo.GetCustomAttributes(false);
			for (int j = 0; j < customAttributes.Length; j++)
			{
				if (customAttributes[j].GetType() == typeof(StateMachine.DoNotAutoCreate))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine.BaseState)))
				{
					StateMachine.BaseState baseState = (StateMachine.BaseState)Activator.CreateInstance(fieldInfo.FieldType);
					this.CreateStates(baseState);
					fieldInfo.SetValue(state_machine, baseState);
				}
				else if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine.Parameter)))
				{
					StateMachine.Parameter parameter = (StateMachine.Parameter)fieldInfo.GetValue(state_machine);
					if (parameter == null)
					{
						parameter = (StateMachine.Parameter)Activator.CreateInstance(fieldInfo.FieldType);
						fieldInfo.SetValue(state_machine, parameter);
					}
					parameter.name = fieldInfo.Name;
					parameter.idx = this.parameters.Length;
					this.parameters = this.parameters.Append(parameter);
				}
				else if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine)))
				{
					fieldInfo.SetValue(state_machine, this);
				}
			}
		}
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x0006E2B1 File Offset: 0x0006C4B1
	public StateMachine.BaseState GetDefaultState()
	{
		return this.defaultState;
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x0006E2B9 File Offset: 0x0006C4B9
	public int GetMaxDepth()
	{
		return this.maxDepth;
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x0006E2C1 File Offset: 0x0006C4C1
	public override string ToString()
	{
		return this.name;
	}

	// Token: 0x04000BD2 RID: 3026
	protected string name;

	// Token: 0x04000BD3 RID: 3027
	protected int maxDepth;

	// Token: 0x04000BD4 RID: 3028
	protected StateMachine.BaseState defaultState;

	// Token: 0x04000BD5 RID: 3029
	protected StateMachine.Parameter[] parameters = new StateMachine.Parameter[0];

	// Token: 0x04000BD6 RID: 3030
	public int dataTableSize;

	// Token: 0x04000BD7 RID: 3031
	public int updateTableSize;

	// Token: 0x04000BDA RID: 3034
	public StateMachineDebuggerSettings.Entry debugSettings;

	// Token: 0x04000BDB RID: 3035
	public bool saveHistory;

	// Token: 0x02001014 RID: 4116
	public sealed class DoNotAutoCreate : Attribute
	{
	}

	// Token: 0x02001015 RID: 4117
	public enum Status
	{
		// Token: 0x04005657 RID: 22103
		Initialized,
		// Token: 0x04005658 RID: 22104
		Running,
		// Token: 0x04005659 RID: 22105
		Failed,
		// Token: 0x0400565A RID: 22106
		Success
	}

	// Token: 0x02001016 RID: 4118
	public class BaseDef
	{
		// Token: 0x060071F7 RID: 29175 RVA: 0x002AC325 File Offset: 0x002AA525
		public StateMachine.Instance CreateSMI(IStateMachineTarget master)
		{
			return Singleton<StateMachineManager>.Instance.CreateSMIFromDef(master, this);
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x002AC333 File Offset: 0x002AA533
		public Type GetStateMachineType()
		{
			return base.GetType().DeclaringType;
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x002AC340 File Offset: 0x002AA540
		public virtual void Configure(GameObject prefab)
		{
		}
	}

	// Token: 0x02001017 RID: 4119
	public class Category : Resource
	{
		// Token: 0x060071FB RID: 29179 RVA: 0x002AC34A File Offset: 0x002AA54A
		public Category(string id)
			: base(id, null, null)
		{
		}
	}

	// Token: 0x02001018 RID: 4120
	[SerializationConfig(MemberSerialization.OptIn)]
	public abstract class Instance
	{
		// Token: 0x060071FC RID: 29180
		public abstract StateMachine.BaseState GetCurrentState();

		// Token: 0x060071FD RID: 29181
		public abstract void GoTo(StateMachine.BaseState state);

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060071FE RID: 29182
		public abstract float timeinstate { get; }

		// Token: 0x060071FF RID: 29183
		public abstract IStateMachineTarget GetMaster();

		// Token: 0x06007200 RID: 29184
		public abstract void StopSM(string reason);

		// Token: 0x06007201 RID: 29185
		public abstract SchedulerHandle Schedule(float time, Action<object> callback, object callback_data = null);

		// Token: 0x06007202 RID: 29186
		public abstract SchedulerHandle ScheduleNextFrame(Action<object> callback, object callback_data = null);

		// Token: 0x06007203 RID: 29187 RVA: 0x002AC355 File Offset: 0x002AA555
		public virtual void FreeResources()
		{
			this.stateMachine = null;
			if (this.subscribedEvents != null)
			{
				this.subscribedEvents.Clear();
			}
			this.subscribedEvents = null;
			this.parameterContexts = null;
			this.dataTable = null;
			this.updateTable = null;
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x002AC38D File Offset: 0x002AA58D
		public Instance(StateMachine state_machine, IStateMachineTarget master)
		{
			this.stateMachine = state_machine;
			this.CreateParameterContexts();
			this.log = new LoggerFSSSS(this.stateMachine.name, 35);
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x002AC3C5 File Offset: 0x002AA5C5
		public bool IsRunning()
		{
			return this.GetCurrentState() != null;
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x002AC3D0 File Offset: 0x002AA5D0
		public void GoTo(string state_name)
		{
			DebugUtil.DevAssert(!KMonoBehaviour.isLoadingScene, "Using Goto while scene was loaded", null);
			StateMachine.BaseState state = this.stateMachine.GetState(state_name);
			this.GoTo(state);
		}

		// Token: 0x06007207 RID: 29191 RVA: 0x002AC404 File Offset: 0x002AA604
		public int GetStackSize()
		{
			return this.stackSize;
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x002AC40C File Offset: 0x002AA60C
		public StateMachine GetStateMachine()
		{
			return this.stateMachine;
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x002AC414 File Offset: 0x002AA614
		[Conditional("UNITY_EDITOR")]
		public void Log(string a, string b = "", string c = "", string d = "")
		{
		}

		// Token: 0x0600720A RID: 29194 RVA: 0x002AC416 File Offset: 0x002AA616
		public bool IsConsoleLoggingEnabled()
		{
			return this.enableConsoleLogging || this.stateMachine.debugSettings.enableConsoleLogging;
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x002AC432 File Offset: 0x002AA632
		public bool IsBreakOnGoToEnabled()
		{
			return this.breakOnGoTo || this.stateMachine.debugSettings.breakOnGoTo;
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x002AC44E File Offset: 0x002AA64E
		public LoggerFSSSS GetLog()
		{
			return this.log;
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x002AC456 File Offset: 0x002AA656
		public StateMachine.Parameter.Context[] GetParameterContexts()
		{
			return this.parameterContexts;
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x002AC45E File Offset: 0x002AA65E
		public StateMachine.Parameter.Context GetParameterContext(StateMachine.Parameter parameter)
		{
			return this.parameterContexts[parameter.idx];
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x002AC46D File Offset: 0x002AA66D
		public StateMachine.Status GetStatus()
		{
			return this.status;
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x002AC475 File Offset: 0x002AA675
		public void SetStatus(StateMachine.Status status)
		{
			this.status = status;
		}

		// Token: 0x06007211 RID: 29201 RVA: 0x002AC47E File Offset: 0x002AA67E
		public void Error()
		{
			if (!StateMachine.Instance.error)
			{
				this.isCrashed = true;
				StateMachine.Instance.error = true;
				RestartWarning.ShouldWarn = true;
			}
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x002AC49C File Offset: 0x002AA69C
		public override string ToString()
		{
			string text = "";
			if (this.GetCurrentState() != null)
			{
				text = this.GetCurrentState().name;
			}
			else if (this.GetStatus() != StateMachine.Status.Initialized)
			{
				text = this.GetStatus().ToString();
			}
			return this.stateMachine.ToString() + "(" + text + ")";
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x002AC500 File Offset: 0x002AA700
		public virtual void StartSM()
		{
			if (!this.IsRunning())
			{
				StateMachineController component = this.GetComponent<StateMachineController>();
				MyAttributes.OnStart(this, component);
				StateMachine.BaseState defaultState = this.stateMachine.GetDefaultState();
				DebugUtil.Assert(defaultState != null);
				if (!component.Restore(this))
				{
					this.GoTo(defaultState);
				}
			}
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x002AC548 File Offset: 0x002AA748
		public bool HasTag(Tag tag)
		{
			return this.GetComponent<KPrefabID>().HasTag(tag);
		}

		// Token: 0x06007215 RID: 29205 RVA: 0x002AC558 File Offset: 0x002AA758
		public bool IsInsideState(StateMachine.BaseState state)
		{
			StateMachine.BaseState currentState = this.GetCurrentState();
			if (currentState == null)
			{
				return false;
			}
			bool flag = state == currentState;
			int num = 0;
			while (!flag && num < currentState.branch.Length && !(flag = state == currentState.branch[num]))
			{
				num++;
			}
			return flag;
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x002AC59C File Offset: 0x002AA79C
		public void ScheduleGoTo(float time, StateMachine.BaseState state)
		{
			if (this.scheduleGoToCallback == null)
			{
				this.scheduleGoToCallback = delegate(object d)
				{
					this.GoTo((StateMachine.BaseState)d);
				};
			}
			this.Schedule(time, this.scheduleGoToCallback, state);
		}

		// Token: 0x06007217 RID: 29207 RVA: 0x002AC5C7 File Offset: 0x002AA7C7
		public void Subscribe(int hash, Action<object> handler)
		{
			this.GetMaster().Subscribe(hash, handler);
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x002AC5D7 File Offset: 0x002AA7D7
		public void Unsubscribe(int hash, Action<object> handler)
		{
			this.GetMaster().Unsubscribe(hash, handler);
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x002AC5E6 File Offset: 0x002AA7E6
		public void Trigger(int hash, object data = null)
		{
			this.GetMaster().GetComponent<KPrefabID>().Trigger(hash, data);
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x002AC5FA File Offset: 0x002AA7FA
		public ComponentType Get<ComponentType>()
		{
			return this.GetComponent<ComponentType>();
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x002AC602 File Offset: 0x002AA802
		public ComponentType GetComponent<ComponentType>()
		{
			return this.GetMaster().GetComponent<ComponentType>();
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x002AC610 File Offset: 0x002AA810
		private void CreateParameterContexts()
		{
			this.parameterContexts = new StateMachine.Parameter.Context[this.stateMachine.parameters.Length];
			for (int i = 0; i < this.stateMachine.parameters.Length; i++)
			{
				this.parameterContexts[i] = this.stateMachine.parameters[i].CreateContext();
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600721D RID: 29213 RVA: 0x002AC667 File Offset: 0x002AA867
		public GameObject gameObject
		{
			get
			{
				return this.GetMaster().gameObject;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600721E RID: 29214 RVA: 0x002AC674 File Offset: 0x002AA874
		public Transform transform
		{
			get
			{
				return this.gameObject.transform;
			}
		}

		// Token: 0x0400565B RID: 22107
		public string serializationSuffix;

		// Token: 0x0400565C RID: 22108
		protected LoggerFSSSS log;

		// Token: 0x0400565D RID: 22109
		protected StateMachine.Status status;

		// Token: 0x0400565E RID: 22110
		protected StateMachine stateMachine;

		// Token: 0x0400565F RID: 22111
		protected Stack<StateEvent.Context> subscribedEvents = new Stack<StateEvent.Context>();

		// Token: 0x04005660 RID: 22112
		protected int stackSize;

		// Token: 0x04005661 RID: 22113
		protected StateMachine.Parameter.Context[] parameterContexts;

		// Token: 0x04005662 RID: 22114
		public object[] dataTable;

		// Token: 0x04005663 RID: 22115
		public StateMachine.Instance.UpdateTableEntry[] updateTable;

		// Token: 0x04005664 RID: 22116
		private Action<object> scheduleGoToCallback;

		// Token: 0x04005665 RID: 22117
		public Action<string, StateMachine.Status> OnStop;

		// Token: 0x04005666 RID: 22118
		public bool breakOnGoTo;

		// Token: 0x04005667 RID: 22119
		public bool enableConsoleLogging;

		// Token: 0x04005668 RID: 22120
		public bool isCrashed;

		// Token: 0x04005669 RID: 22121
		public static bool error;

		// Token: 0x02001F52 RID: 8018
		public struct UpdateTableEntry
		{
			// Token: 0x04008B97 RID: 35735
			public HandleVector<int>.Handle handle;

			// Token: 0x04008B98 RID: 35736
			public StateMachineUpdater.BaseUpdateBucket bucket;
		}
	}

	// Token: 0x02001019 RID: 4121
	[DebuggerDisplay("{longName}")]
	public class BaseState
	{
		// Token: 0x06007220 RID: 29216 RVA: 0x002AC68F File Offset: 0x002AA88F
		public BaseState()
		{
			this.branch = new StateMachine.BaseState[1];
			this.branch[0] = this;
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x002AC6AC File Offset: 0x002AA8AC
		public void FreeResources()
		{
			if (this.name == null)
			{
				return;
			}
			this.name = null;
			if (this.defaultState != null)
			{
				this.defaultState.FreeResources();
			}
			this.defaultState = null;
			this.events = null;
			int num = 0;
			while (this.transitions != null && num < this.transitions.Count)
			{
				this.transitions[num].Clear();
				num++;
			}
			this.transitions = null;
			this.enterActions = null;
			this.exitActions = null;
			if (this.branch != null)
			{
				for (int i = 0; i < this.branch.Length; i++)
				{
					this.branch[i].FreeResources();
				}
			}
			this.branch = null;
			this.parent = null;
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x002AC764 File Offset: 0x002AA964
		public int GetStateCount()
		{
			return this.branch.Length;
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x002AC76E File Offset: 0x002AA96E
		public StateMachine.BaseState GetState(int idx)
		{
			return this.branch[idx];
		}

		// Token: 0x0400566A RID: 22122
		public string name;

		// Token: 0x0400566B RID: 22123
		public string longName;

		// Token: 0x0400566C RID: 22124
		public string debugPushName;

		// Token: 0x0400566D RID: 22125
		public string debugPopName;

		// Token: 0x0400566E RID: 22126
		public string debugExecuteName;

		// Token: 0x0400566F RID: 22127
		public StateMachine.BaseState defaultState;

		// Token: 0x04005670 RID: 22128
		public List<StateEvent> events;

		// Token: 0x04005671 RID: 22129
		public List<StateMachine.BaseTransition> transitions;

		// Token: 0x04005672 RID: 22130
		public List<StateMachine.UpdateAction> updateActions;

		// Token: 0x04005673 RID: 22131
		public List<StateMachine.Action> enterActions;

		// Token: 0x04005674 RID: 22132
		public List<StateMachine.Action> exitActions;

		// Token: 0x04005675 RID: 22133
		public StateMachine.BaseState[] branch;

		// Token: 0x04005676 RID: 22134
		public StateMachine.BaseState parent;
	}

	// Token: 0x0200101A RID: 4122
	public class BaseTransition
	{
		// Token: 0x06007224 RID: 29220 RVA: 0x002AC778 File Offset: 0x002AA978
		public BaseTransition(int idx, string name, StateMachine.BaseState source_state, StateMachine.BaseState target_state)
		{
			this.idx = idx;
			this.name = name;
			this.sourceState = source_state;
			this.targetState = target_state;
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x002AC79D File Offset: 0x002AA99D
		public virtual void Evaluate(StateMachine.Instance smi)
		{
		}

		// Token: 0x06007226 RID: 29222 RVA: 0x002AC79F File Offset: 0x002AA99F
		public virtual StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
		{
			return new StateMachine.BaseTransition.Context(this);
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x002AC7A7 File Offset: 0x002AA9A7
		public virtual void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context context)
		{
		}

		// Token: 0x06007228 RID: 29224 RVA: 0x002AC7A9 File Offset: 0x002AA9A9
		public void Clear()
		{
			this.name = null;
			if (this.sourceState != null)
			{
				this.sourceState.FreeResources();
			}
			this.sourceState = null;
			if (this.targetState != null)
			{
				this.targetState.FreeResources();
			}
			this.targetState = null;
		}

		// Token: 0x04005677 RID: 22135
		public int idx;

		// Token: 0x04005678 RID: 22136
		public string name;

		// Token: 0x04005679 RID: 22137
		public StateMachine.BaseState sourceState;

		// Token: 0x0400567A RID: 22138
		public StateMachine.BaseState targetState;

		// Token: 0x02001F53 RID: 8019
		public struct Context
		{
			// Token: 0x06009E74 RID: 40564 RVA: 0x0033EA50 File Offset: 0x0033CC50
			public Context(StateMachine.BaseTransition transition)
			{
				this.idx = transition.idx;
				this.handlerId = 0;
			}

			// Token: 0x04008B99 RID: 35737
			public int idx;

			// Token: 0x04008B9A RID: 35738
			public int handlerId;
		}
	}

	// Token: 0x0200101B RID: 4123
	public struct UpdateAction
	{
		// Token: 0x0400567B RID: 22139
		public int updateTableIdx;

		// Token: 0x0400567C RID: 22140
		public UpdateRate updateRate;

		// Token: 0x0400567D RID: 22141
		public int nextBucketIdx;

		// Token: 0x0400567E RID: 22142
		public StateMachineUpdater.BaseUpdateBucket[] buckets;

		// Token: 0x0400567F RID: 22143
		public object updater;
	}

	// Token: 0x0200101C RID: 4124
	public struct Action
	{
		// Token: 0x06007229 RID: 29225 RVA: 0x002AC7E6 File Offset: 0x002AA9E6
		public Action(string name, object callback)
		{
			this.name = name;
			this.callback = callback;
		}

		// Token: 0x04005680 RID: 22144
		public string name;

		// Token: 0x04005681 RID: 22145
		public object callback;
	}

	// Token: 0x0200101D RID: 4125
	public class ParameterTransition : StateMachine.BaseTransition
	{
		// Token: 0x0600722A RID: 29226 RVA: 0x002AC7F6 File Offset: 0x002AA9F6
		public ParameterTransition(int idx, string name, StateMachine.BaseState source_state, StateMachine.BaseState target_state)
			: base(idx, name, source_state, target_state)
		{
		}
	}

	// Token: 0x0200101E RID: 4126
	public abstract class Parameter
	{
		// Token: 0x0600722B RID: 29227
		public abstract StateMachine.Parameter.Context CreateContext();

		// Token: 0x04005682 RID: 22146
		public string name;

		// Token: 0x04005683 RID: 22147
		public int idx;

		// Token: 0x02001F54 RID: 8020
		public abstract class Context
		{
			// Token: 0x06009E75 RID: 40565 RVA: 0x0033EA65 File Offset: 0x0033CC65
			public Context(StateMachine.Parameter parameter)
			{
				this.parameter = parameter;
			}

			// Token: 0x06009E76 RID: 40566
			public abstract void Serialize(BinaryWriter writer);

			// Token: 0x06009E77 RID: 40567
			public abstract void Deserialize(IReader reader, StateMachine.Instance smi);

			// Token: 0x06009E78 RID: 40568 RVA: 0x0033EA74 File Offset: 0x0033CC74
			public virtual void Cleanup()
			{
			}

			// Token: 0x06009E79 RID: 40569
			public abstract void ShowEditor(StateMachine.Instance base_smi);

			// Token: 0x06009E7A RID: 40570
			public abstract void ShowDevTool(StateMachine.Instance base_smi);

			// Token: 0x04008B9B RID: 35739
			public StateMachine.Parameter parameter;
		}
	}

	// Token: 0x0200101F RID: 4127
	public enum SerializeType
	{
		// Token: 0x04005685 RID: 22149
		Never,
		// Token: 0x04005686 RID: 22150
		ParamsOnly,
		// Token: 0x04005687 RID: 22151
		CurrentStateOnly_DEPRECATED,
		// Token: 0x04005688 RID: 22152
		Both_DEPRECATED
	}
}
