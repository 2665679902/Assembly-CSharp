using System;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class Chore<StateMachineInstanceType> : Chore, IStateMachineTarget where StateMachineInstanceType : StateMachine.Instance
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00064A39 File Offset: 0x00062C39
	// (set) Token: 0x060012E4 RID: 4836 RVA: 0x00064A41 File Offset: 0x00062C41
	public StateMachineInstanceType smi { get; protected set; }

	// Token: 0x060012E5 RID: 4837 RVA: 0x00064A4A File Offset: 0x00062C4A
	protected override StateMachine.Instance GetSMI()
	{
		return this.smi;
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x00064A57 File Offset: 0x00062C57
	public int Subscribe(int hash, Action<object> handler)
	{
		return this.GetComponent<KPrefabID>().Subscribe(hash, handler);
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x00064A66 File Offset: 0x00062C66
	public void Unsubscribe(int hash, Action<object> handler)
	{
		this.GetComponent<KPrefabID>().Unsubscribe(hash, handler);
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x00064A75 File Offset: 0x00062C75
	public void Unsubscribe(int id)
	{
		this.GetComponent<KPrefabID>().Unsubscribe(id);
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00064A83 File Offset: 0x00062C83
	public void Trigger(int hash, object data = null)
	{
		this.GetComponent<KPrefabID>().Trigger(hash, data);
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x00064A92 File Offset: 0x00062C92
	public ComponentType GetComponent<ComponentType>()
	{
		return base.target.GetComponent<ComponentType>();
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060012EB RID: 4843 RVA: 0x00064A9F File Offset: 0x00062C9F
	public override GameObject gameObject
	{
		get
		{
			return base.target.gameObject;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060012EC RID: 4844 RVA: 0x00064AAC File Offset: 0x00062CAC
	public Transform transform
	{
		get
		{
			return base.target.gameObject.transform;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060012ED RID: 4845 RVA: 0x00064ABE File Offset: 0x00062CBE
	public string name
	{
		get
		{
			return this.gameObject.name;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060012EE RID: 4846 RVA: 0x00064ACB File Offset: 0x00062CCB
	public override bool isNull
	{
		get
		{
			return base.target.isNull;
		}
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x00064AD8 File Offset: 0x00062CD8
	public Chore(ChoreType chore_type, IStateMachineTarget target, ChoreProvider chore_provider, bool run_until_complete = true, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null, PriorityScreen.PriorityClass master_priority_class = PriorityScreen.PriorityClass.basic, int master_priority_value = 5, bool is_preemptable = false, bool allow_in_context_menu = true, int priority_mod = 0, bool add_to_daily_report = false, ReportManager.ReportType report_type = ReportManager.ReportType.WorkTime)
		: base(chore_type, target, chore_provider, run_until_complete, on_complete, on_begin, on_end, master_priority_class, master_priority_value, is_preemptable, allow_in_context_menu, priority_mod, add_to_daily_report, report_type)
	{
		target.Subscribe(1969584890, new Action<object>(this.OnTargetDestroyed));
		this.reportType = report_type;
		this.addToDailyReport = add_to_daily_report;
		if (this.addToDailyReport)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, 1f, chore_type.Name, GameUtil.GetChoreName(this, null));
		}
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x00064B51 File Offset: 0x00062D51
	public override string ResolveString(string str)
	{
		if (!base.target.isNull)
		{
			str = str.Replace("{Target}", base.target.gameObject.GetProperName());
		}
		return base.ResolveString(str);
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x00064B84 File Offset: 0x00062D84
	public override void Cleanup()
	{
		base.Cleanup();
		if (base.target != null)
		{
			base.target.Unsubscribe(1969584890, new Action<object>(this.OnTargetDestroyed));
		}
		if (this.onCleanup != null)
		{
			this.onCleanup(this);
		}
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00064BC4 File Offset: 0x00062DC4
	private void OnTargetDestroyed(object data)
	{
		base.Cancel("Target Destroyed");
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x00064BD1 File Offset: 0x00062DD1
	public override bool CanPreempt(Chore.Precondition.Context context)
	{
		return base.CanPreempt(context);
	}
}
