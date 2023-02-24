using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000923 RID: 2339
[AddComponentMenu("KMonoBehaviour/Workable/Sleepable")]
public class Sleepable : Workable
{
	// Token: 0x06004456 RID: 17494 RVA: 0x00181F17 File Offset: 0x00180117
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.showProgressBar = false;
		this.workerStatusItem = null;
		this.synchronizeAnims = false;
		this.triggerWorkReactions = false;
		this.lightEfficiencyBonus = false;
	}

	// Token: 0x06004457 RID: 17495 RVA: 0x00181F4A File Offset: 0x0018014A
	protected override void OnSpawn()
	{
		Components.Sleepables.Add(this);
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x06004458 RID: 17496 RVA: 0x00181F64 File Offset: 0x00180164
	public override HashedString[] GetWorkAnims(Worker worker)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		if (base.GetComponent<Building>() != null && component != null && component.CurrentHat != null)
		{
			return Sleepable.hatWorkAnims;
		}
		return Sleepable.normalWorkAnims;
	}

	// Token: 0x06004459 RID: 17497 RVA: 0x00181FA4 File Offset: 0x001801A4
	public override HashedString[] GetWorkPstAnims(Worker worker, bool successfully_completed)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		if (base.GetComponent<Building>() != null && component != null && component.CurrentHat != null)
		{
			return Sleepable.hatWorkPstAnim;
		}
		return Sleepable.normalWorkPstAnim;
	}

	// Token: 0x0600445A RID: 17498 RVA: 0x00181FE4 File Offset: 0x001801E4
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		if (component != null)
		{
			component.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
		}
		base.Subscribe(worker.gameObject, -1142962013, new Action<object>(this.PlayPstAnim));
		if (this.operational != null)
		{
			this.operational.SetActive(true, false);
		}
		worker.Trigger(-1283701846, this);
		worker.GetComponent<Effects>().Add(this.effectName, false);
		this.isDoneSleeping = false;
	}

	// Token: 0x0600445B RID: 17499 RVA: 0x001820A0 File Offset: 0x001802A0
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.isDoneSleeping)
		{
			return Time.time > this.wakeTime;
		}
		if (this.Dreamable != null && !this.Dreamable.DreamIsDisturbed)
		{
			this.Dreamable.WorkTick(worker, dt);
		}
		if (worker.GetSMI<StaminaMonitor.Instance>().ShouldExitSleep())
		{
			this.isDoneSleeping = true;
			this.wakeTime = Time.time + UnityEngine.Random.value * 3f;
		}
		return false;
	}

	// Token: 0x0600445C RID: 17500 RVA: 0x00182118 File Offset: 0x00180318
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		if (this.operational != null)
		{
			this.operational.SetActive(false, false);
		}
		base.Unsubscribe(worker.gameObject, -1142962013, new Action<object>(this.PlayPstAnim));
		if (worker != null)
		{
			Effects component = worker.GetComponent<Effects>();
			component.Remove(this.effectName);
			if (this.wakeEffects != null)
			{
				foreach (string text in this.wakeEffects)
				{
					component.Add(text, true);
				}
			}
			if (this.stretchOnWake && UnityEngine.Random.value < 0.33f)
			{
				new EmoteChore(worker.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, Db.Get().Emotes.Minion.MorningStretch, 1, null);
			}
			if (worker.GetAmounts().Get(Db.Get().Amounts.Stamina).value < worker.GetAmounts().Get(Db.Get().Amounts.Stamina).GetMax())
			{
				worker.Trigger(1338475637, this);
			}
		}
	}

	// Token: 0x0600445D RID: 17501 RVA: 0x00182264 File Offset: 0x00180464
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x0600445E RID: 17502 RVA: 0x00182267 File Offset: 0x00180467
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Sleepables.Remove(this);
	}

	// Token: 0x0600445F RID: 17503 RVA: 0x0018227C File Offset: 0x0018047C
	private void PlayPstAnim(object data)
	{
		Worker worker = (Worker)data;
		if (worker != null && worker.workable != null)
		{
			KAnimControllerBase component = worker.workable.gameObject.GetComponent<KAnimControllerBase>();
			if (component != null)
			{
				component.Play("working_pst", KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x04002D8D RID: 11661
	private const float STRECH_CHANCE = 0.33f;

	// Token: 0x04002D8E RID: 11662
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002D8F RID: 11663
	public string effectName = "Sleep";

	// Token: 0x04002D90 RID: 11664
	public List<string> wakeEffects;

	// Token: 0x04002D91 RID: 11665
	public bool stretchOnWake = true;

	// Token: 0x04002D92 RID: 11666
	private float wakeTime;

	// Token: 0x04002D93 RID: 11667
	private bool isDoneSleeping;

	// Token: 0x04002D94 RID: 11668
	public ClinicDreamable Dreamable;

	// Token: 0x04002D95 RID: 11669
	private static readonly HashedString[] normalWorkAnims = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04002D96 RID: 11670
	private static readonly HashedString[] hatWorkAnims = new HashedString[] { "hat_pre", "working_loop" };

	// Token: 0x04002D97 RID: 11671
	private static readonly HashedString[] normalWorkPstAnim = new HashedString[] { "working_pst" };

	// Token: 0x04002D98 RID: 11672
	private static readonly HashedString[] hatWorkPstAnim = new HashedString[] { "hat_pst" };
}
