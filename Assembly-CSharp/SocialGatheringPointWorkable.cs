using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000929 RID: 2345
[AddComponentMenu("KMonoBehaviour/Workable/SocialGatheringPointWorkable")]
public class SocialGatheringPointWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x0600448B RID: 17547 RVA: 0x00182A85 File Offset: 0x00180C85
	private SocialGatheringPointWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x0600448C RID: 17548 RVA: 0x00182A98 File Offset: 0x00180C98
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_generic_convo_kanim") };
		this.workAnims = new HashedString[] { "idle" };
		this.faceTargetWhenWorking = true;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Socializing;
		this.synchronizeAnims = false;
		this.showProgressBar = false;
		this.resetProgressOnStop = true;
		this.lightEfficiencyBonus = false;
	}

	// Token: 0x0600448D RID: 17549 RVA: 0x00182B1E File Offset: 0x00180D1E
	public override Vector3 GetFacingTarget()
	{
		if (this.lastTalker != null)
		{
			return this.lastTalker.transform.GetPosition();
		}
		return base.GetFacingTarget();
	}

	// Token: 0x0600448E RID: 17550 RVA: 0x00182B48 File Offset: 0x00180D48
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (!worker.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation))
		{
			Effects component = worker.GetComponent<Effects>();
			if (string.IsNullOrEmpty(this.specificEffect) || component.HasEffect(this.specificEffect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600448F RID: 17551 RVA: 0x00182B98 File Offset: 0x00180D98
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<KPrefabID>().AddTag(GameTags.AlwaysConverse, false);
		worker.Subscribe(-594200555, new Action<object>(this.OnStartedTalking));
		worker.Subscribe(25860745, new Action<object>(this.OnStoppedTalking));
		this.timesConversed = 0;
	}

	// Token: 0x06004490 RID: 17552 RVA: 0x00182BF4 File Offset: 0x00180DF4
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<KPrefabID>().RemoveTag(GameTags.AlwaysConverse);
		worker.Unsubscribe(-594200555, new Action<object>(this.OnStartedTalking));
		worker.Unsubscribe(25860745, new Action<object>(this.OnStoppedTalking));
	}

	// Token: 0x06004491 RID: 17553 RVA: 0x00182C48 File Offset: 0x00180E48
	protected override void OnCompleteWork(Worker worker)
	{
		if (this.timesConversed > 0)
		{
			Effects component = worker.GetComponent<Effects>();
			if (!string.IsNullOrEmpty(this.specificEffect))
			{
				component.Add(this.specificEffect, true);
			}
		}
	}

	// Token: 0x06004492 RID: 17554 RVA: 0x00182C80 File Offset: 0x00180E80
	private void OnStartedTalking(object data)
	{
		ConversationManager.StartedTalkingEvent startedTalkingEvent = data as ConversationManager.StartedTalkingEvent;
		if (startedTalkingEvent == null)
		{
			return;
		}
		GameObject talker = startedTalkingEvent.talker;
		if (talker == base.worker.gameObject)
		{
			KBatchedAnimController component = base.worker.GetComponent<KBatchedAnimController>();
			string text = startedTalkingEvent.anim;
			text += UnityEngine.Random.Range(1, 9).ToString();
			component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("idle", KAnim.PlayMode.Loop, 1f, 0f);
		}
		else
		{
			base.worker.GetComponent<Facing>().Face(talker.transform.GetPosition());
			this.lastTalker = talker;
		}
		this.timesConversed++;
	}

	// Token: 0x06004493 RID: 17555 RVA: 0x00182D3E File Offset: 0x00180F3E
	private void OnStoppedTalking(object data)
	{
	}

	// Token: 0x06004494 RID: 17556 RVA: 0x00182D40 File Offset: 0x00180F40
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		if (!string.IsNullOrEmpty(this.specificEffect) && worker.GetComponent<Effects>().HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04002DB5 RID: 11701
	private GameObject lastTalker;

	// Token: 0x04002DB6 RID: 11702
	public int basePriority;

	// Token: 0x04002DB7 RID: 11703
	public string specificEffect;

	// Token: 0x04002DB8 RID: 11704
	public int timesConversed;
}
