using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;
using UnityEngine;

// Token: 0x02000403 RID: 1027
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/StateMachineController")]
public class StateMachineController : KMonoBehaviour, ISaveLoadableDetails, IStateMachineControllerHack
{
	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06001538 RID: 5432 RVA: 0x0006E64A File Offset: 0x0006C84A
	public StateMachineController.CmpDef cmpdef
	{
		get
		{
			return this.defHandle.Get<StateMachineController.CmpDef>();
		}
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x0006E657 File Offset: 0x0006C857
	public IEnumerator<StateMachine.Instance> GetEnumerator()
	{
		return this.stateMachines.GetEnumerator();
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x0006E669 File Offset: 0x0006C869
	public void AddStateMachineInstance(StateMachine.Instance state_machine)
	{
		if (!this.stateMachines.Contains(state_machine))
		{
			this.stateMachines.Add(state_machine);
			MyAttributes.OnAwake(state_machine, this);
		}
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x0006E68C File Offset: 0x0006C88C
	public void RemoveStateMachineInstance(StateMachine.Instance state_machine)
	{
		if (!state_machine.GetStateMachine().saveHistory && !state_machine.GetStateMachine().debugSettings.saveHistory)
		{
			this.stateMachines.Remove(state_machine);
		}
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x0006E6BA File Offset: 0x0006C8BA
	public bool HasStateMachineInstance(StateMachine.Instance state_machine)
	{
		return this.stateMachines.Contains(state_machine);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x0006E6C8 File Offset: 0x0006C8C8
	public void AddDef(StateMachine.BaseDef def)
	{
		this.cmpdef.defs.Add(def);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0006E6DB File Offset: 0x0006C8DB
	public LoggerFSSSS GetLog()
	{
		return this.log;
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x0006E6E3 File Offset: 0x0006C8E3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.log.SetName(base.name);
		base.Subscribe<StateMachineController>(1969584890, StateMachineController.OnTargetDestroyedDelegate);
		base.Subscribe<StateMachineController>(1502190696, StateMachineController.OnTargetDestroyedDelegate);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0006E720 File Offset: 0x0006C920
	private void OnTargetDestroyed(object data)
	{
		while (this.stateMachines.Count > 0)
		{
			StateMachine.Instance instance = this.stateMachines[0];
			instance.StopSM("StateMachineController.OnCleanUp");
			this.stateMachines.Remove(instance);
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0006E764 File Offset: 0x0006C964
	protected override void OnLoadLevel()
	{
		while (this.stateMachines.Count > 0)
		{
			StateMachine.Instance instance = this.stateMachines[0];
			instance.FreeResources();
			this.stateMachines.Remove(instance);
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0006E7A4 File Offset: 0x0006C9A4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		while (this.stateMachines.Count > 0)
		{
			StateMachine.Instance instance = this.stateMachines[0];
			instance.StopSM("StateMachineController.OnCleanUp");
			this.stateMachines.Remove(instance);
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x0006E7EC File Offset: 0x0006C9EC
	public void CreateSMIS()
	{
		if (!this.defHandle.IsValid())
		{
			return;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			baseDef.CreateSMI(this);
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0006E854 File Offset: 0x0006CA54
	public void StartSMIS()
	{
		if (!this.defHandle.IsValid())
		{
			return;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			StateMachine.Instance smi = this.GetSMI(Singleton<StateMachineManager>.Instance.CreateStateMachine(baseDef.GetStateMachineType()).GetStateMachineInstanceType());
			if (smi != null && !smi.IsRunning())
			{
				smi.StartSM();
			}
		}
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0006E8E0 File Offset: 0x0006CAE0
	public void Serialize(BinaryWriter writer)
	{
		this.serializer.Serialize(this.stateMachines, writer);
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
	public void Deserialize(IReader reader)
	{
		this.serializer.Deserialize(reader);
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x0006E902 File Offset: 0x0006CB02
	public bool Restore(StateMachine.Instance smi)
	{
		return this.serializer.Restore(smi);
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x0006E910 File Offset: 0x0006CB10
	public DefType GetDef<DefType>() where DefType : StateMachine.BaseDef
	{
		if (!this.defHandle.IsValid())
		{
			return default(DefType);
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			DefType defType = baseDef as DefType;
			if (defType != null)
			{
				return defType;
			}
		}
		return default(DefType);
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0006E99C File Offset: 0x0006CB9C
	public List<DefType> GetDefs<DefType>() where DefType : StateMachine.BaseDef
	{
		List<DefType> list = new List<DefType>();
		if (!this.defHandle.IsValid())
		{
			return list;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			DefType defType = baseDef as DefType;
			if (defType != null)
			{
				list.Add(defType);
			}
		}
		return list;
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x0006EA1C File Offset: 0x0006CC1C
	public StateMachine.Instance GetSMI(Type type)
	{
		for (int i = 0; i < this.stateMachines.Count; i++)
		{
			StateMachine.Instance instance = this.stateMachines[i];
			if (type.IsAssignableFrom(instance.GetType()))
			{
				return instance;
			}
		}
		return null;
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0006EA5D File Offset: 0x0006CC5D
	public StateMachineInstanceType GetSMI<StateMachineInstanceType>() where StateMachineInstanceType : class
	{
		return this.GetSMI(typeof(StateMachineInstanceType)) as StateMachineInstanceType;
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x0006EA7C File Offset: 0x0006CC7C
	public List<StateMachineInstanceType> GetAllSMI<StateMachineInstanceType>() where StateMachineInstanceType : class
	{
		List<StateMachineInstanceType> list = new List<StateMachineInstanceType>();
		foreach (StateMachine.Instance instance in this.stateMachines)
		{
			StateMachineInstanceType stateMachineInstanceType = instance as StateMachineInstanceType;
			if (stateMachineInstanceType != null)
			{
				list.Add(stateMachineInstanceType);
			}
		}
		return list;
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x0006EAE8 File Offset: 0x0006CCE8
	public List<IGameObjectEffectDescriptor> GetDescriptors()
	{
		List<IGameObjectEffectDescriptor> list = new List<IGameObjectEffectDescriptor>();
		if (!this.defHandle.IsValid())
		{
			return list;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			if (baseDef is IGameObjectEffectDescriptor)
			{
				list.Add(baseDef as IGameObjectEffectDescriptor);
			}
		}
		return list;
	}

	// Token: 0x04000BE1 RID: 3041
	public DefHandle defHandle;

	// Token: 0x04000BE2 RID: 3042
	private List<StateMachine.Instance> stateMachines = new List<StateMachine.Instance>();

	// Token: 0x04000BE3 RID: 3043
	private LoggerFSSSS log = new LoggerFSSSS("StateMachineController", 35);

	// Token: 0x04000BE4 RID: 3044
	private StateMachineSerializer serializer = new StateMachineSerializer();

	// Token: 0x04000BE5 RID: 3045
	private static readonly EventSystem.IntraObjectHandler<StateMachineController> OnTargetDestroyedDelegate = new EventSystem.IntraObjectHandler<StateMachineController>(delegate(StateMachineController component, object data)
	{
		component.OnTargetDestroyed(data);
	});

	// Token: 0x02001030 RID: 4144
	public class CmpDef
	{
		// Token: 0x04005699 RID: 22169
		public List<StateMachine.BaseDef> defs = new List<StateMachine.BaseDef>();
	}
}
