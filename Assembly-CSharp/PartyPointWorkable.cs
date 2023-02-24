using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000886 RID: 2182
public class PartyPointWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06003E99 RID: 16025 RVA: 0x0015E012 File Offset: 0x0015C212
	private PartyPointWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06003E9A RID: 16026 RVA: 0x0015E024 File Offset: 0x0015C224
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_generic_convo_kanim") };
		this.workAnimPlayMode = KAnim.PlayMode.Loop;
		this.faceTargetWhenWorking = true;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Socializing;
		this.synchronizeAnims = false;
		this.showProgressBar = false;
		this.resetProgressOnStop = true;
		this.lightEfficiencyBonus = false;
		if (UnityEngine.Random.Range(0f, 100f) > 80f)
		{
			this.activity = PartyPointWorkable.ActivityType.Dance;
		}
		else
		{
			this.activity = PartyPointWorkable.ActivityType.Talk;
		}
		PartyPointWorkable.ActivityType activityType = this.activity;
		if (activityType == PartyPointWorkable.ActivityType.Talk)
		{
			this.workAnims = new HashedString[] { "idle" };
			this.workerOverrideAnims = new KAnimFile[][] { new KAnimFile[] { Assets.GetAnim("anim_generic_convo_kanim") } };
			return;
		}
		if (activityType != PartyPointWorkable.ActivityType.Dance)
		{
			return;
		}
		this.workAnims = new HashedString[] { "working_loop" };
		this.workerOverrideAnims = new KAnimFile[][]
		{
			new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_danceone_kanim") },
			new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancetwo_kanim") },
			new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancethree_kanim") }
		};
	}

	// Token: 0x06003E9B RID: 16027 RVA: 0x0015E188 File Offset: 0x0015C388
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		int num = UnityEngine.Random.Range(0, this.workerOverrideAnims.Length);
		this.overrideAnims = this.workerOverrideAnims[num];
		return base.GetAnim(worker);
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x0015E1B9 File Offset: 0x0015C3B9
	public override Vector3 GetFacingTarget()
	{
		if (this.lastTalker != null)
		{
			return this.lastTalker.transform.GetPosition();
		}
		return base.GetFacingTarget();
	}

	// Token: 0x06003E9D RID: 16029 RVA: 0x0015E1E0 File Offset: 0x0015C3E0
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		return false;
	}

	// Token: 0x06003E9E RID: 16030 RVA: 0x0015E1E4 File Offset: 0x0015C3E4
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<KPrefabID>().AddTag(GameTags.AlwaysConverse, false);
		worker.Subscribe(-594200555, new Action<object>(this.OnStartedTalking));
		worker.Subscribe(25860745, new Action<object>(this.OnStoppedTalking));
	}

	// Token: 0x06003E9F RID: 16031 RVA: 0x0015E23C File Offset: 0x0015C43C
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<KPrefabID>().RemoveTag(GameTags.AlwaysConverse);
		worker.Unsubscribe(-594200555, new Action<object>(this.OnStartedTalking));
		worker.Unsubscribe(25860745, new Action<object>(this.OnStoppedTalking));
	}

	// Token: 0x06003EA0 RID: 16032 RVA: 0x0015E290 File Offset: 0x0015C490
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.specificEffect))
		{
			component.Add(this.specificEffect, true);
		}
	}

	// Token: 0x06003EA1 RID: 16033 RVA: 0x0015E2C0 File Offset: 0x0015C4C0
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
			if (this.activity == PartyPointWorkable.ActivityType.Talk)
			{
				KBatchedAnimController component = base.worker.GetComponent<KBatchedAnimController>();
				string text = startedTalkingEvent.anim;
				text += UnityEngine.Random.Range(1, 9).ToString();
				component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
				component.Queue("idle", KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		else
		{
			if (this.activity == PartyPointWorkable.ActivityType.Talk)
			{
				base.worker.GetComponent<Facing>().Face(talker.transform.GetPosition());
			}
			this.lastTalker = talker;
		}
	}

	// Token: 0x06003EA2 RID: 16034 RVA: 0x0015E382 File Offset: 0x0015C582
	private void OnStoppedTalking(object data)
	{
	}

	// Token: 0x06003EA3 RID: 16035 RVA: 0x0015E384 File Offset: 0x0015C584
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		if (!string.IsNullOrEmpty(this.specificEffect) && worker.GetComponent<Effects>().HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x040028F7 RID: 10487
	private GameObject lastTalker;

	// Token: 0x040028F8 RID: 10488
	public int basePriority;

	// Token: 0x040028F9 RID: 10489
	public string specificEffect;

	// Token: 0x040028FA RID: 10490
	public KAnimFile[][] workerOverrideAnims;

	// Token: 0x040028FB RID: 10491
	private PartyPointWorkable.ActivityType activity;

	// Token: 0x02001641 RID: 5697
	private enum ActivityType
	{
		// Token: 0x04006944 RID: 26948
		Talk,
		// Token: 0x04006945 RID: 26949
		Dance,
		// Token: 0x04006946 RID: 26950
		LENGTH
	}
}
