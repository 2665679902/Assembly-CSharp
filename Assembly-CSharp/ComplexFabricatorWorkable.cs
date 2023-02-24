using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200045D RID: 1117
[AddComponentMenu("KMonoBehaviour/Workable/ComplexFabricatorWorkable")]
public class ComplexFabricatorWorkable : Workable
{
	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x060018AC RID: 6316 RVA: 0x00083231 File Offset: 0x00081431
	// (set) Token: 0x060018AD RID: 6317 RVA: 0x00083239 File Offset: 0x00081439
	public StatusItem WorkerStatusItem
	{
		get
		{
			return this.workerStatusItem;
		}
		set
		{
			this.workerStatusItem = value;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x060018AE RID: 6318 RVA: 0x00083242 File Offset: 0x00081442
	// (set) Token: 0x060018AF RID: 6319 RVA: 0x0008324A File Offset: 0x0008144A
	public AttributeConverter AttributeConverter
	{
		get
		{
			return this.attributeConverter;
		}
		set
		{
			this.attributeConverter = value;
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00083253 File Offset: 0x00081453
	// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0008325B File Offset: 0x0008145B
	public float AttributeExperienceMultiplier
	{
		get
		{
			return this.attributeExperienceMultiplier;
		}
		set
		{
			this.attributeExperienceMultiplier = value;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (set) Token: 0x060018B2 RID: 6322 RVA: 0x00083264 File Offset: 0x00081464
	public string SkillExperienceSkillGroup
	{
		set
		{
			this.skillExperienceSkillGroup = value;
		}
	}

	// Token: 0x170000E4 RID: 228
	// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0008326D File Offset: 0x0008146D
	public float SkillExperienceMultiplier
	{
		set
		{
			this.skillExperienceMultiplier = value;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00083276 File Offset: 0x00081476
	public ComplexRecipe CurrentWorkingOrder
	{
		get
		{
			if (!(this.fabricator != null))
			{
				return null;
			}
			return this.fabricator.CurrentWorkingOrder;
		}
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x00083294 File Offset: 0x00081494
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x00083304 File Offset: 0x00081504
	public override string GetConversationTopic()
	{
		string conversationTopic = this.fabricator.GetConversationTopic();
		if (conversationTopic == null)
		{
			return base.GetConversationTopic();
		}
		return conversationTopic;
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x00083328 File Offset: 0x00081528
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		if (!this.operational.IsOperational)
		{
			return;
		}
		if (this.fabricator.CurrentWorkingOrder != null)
		{
			this.InstantiateVisualizer(this.fabricator.CurrentWorkingOrder);
			return;
		}
		DebugUtil.DevAssertArgs(false, new object[] { "ComplexFabricatorWorkable.OnStartWork called but CurrentMachineOrder is null", base.gameObject });
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x00083386 File Offset: 0x00081586
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.OnWorkTickActions != null)
		{
			this.OnWorkTickActions(worker, dt);
		}
		this.UpdateOrderProgress(worker, dt);
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000833B0 File Offset: 0x000815B0
	public override float GetWorkTime()
	{
		ComplexRecipe currentWorkingOrder = this.fabricator.CurrentWorkingOrder;
		if (currentWorkingOrder != null)
		{
			this.workTime = currentWorkingOrder.time;
			return this.workTime;
		}
		return -1f;
	}

	// Token: 0x060018BA RID: 6330 RVA: 0x000833E4 File Offset: 0x000815E4
	public Chore CreateWorkChore(ChoreType choreType, float order_progress)
	{
		Chore chore = new WorkChore<ComplexFabricatorWorkable>(choreType, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		this.workTimeRemaining = this.GetWorkTime() * (1f - order_progress);
		return chore;
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x0008341D File Offset: 0x0008161D
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.fabricator.CompleteWorkingOrder();
		this.DestroyVisualizer();
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x00083438 File Offset: 0x00081638
	private void InstantiateVisualizer(ComplexRecipe recipe)
	{
		if (this.visualizer != null)
		{
			this.DestroyVisualizer();
		}
		if (this.visualizerLink != null)
		{
			this.visualizerLink.Unregister();
			this.visualizerLink = null;
		}
		if (recipe.FabricationVisualizer == null)
		{
			return;
		}
		this.visualizer = Util.KInstantiate(recipe.FabricationVisualizer, null, null);
		this.visualizer.transform.parent = this.meter.meterController.transform;
		this.visualizer.transform.SetLocalPosition(new Vector3(0f, 0f, 1f));
		this.visualizer.SetActive(true);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		KBatchedAnimController component2 = this.visualizer.GetComponent<KBatchedAnimController>();
		this.visualizerLink = new KAnimLink(component, component2);
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x00083508 File Offset: 0x00081708
	private void UpdateOrderProgress(Worker worker, float dt)
	{
		float workTime = this.GetWorkTime();
		float num = Mathf.Clamp01((workTime - base.WorkTimeRemaining) / workTime);
		if (this.fabricator)
		{
			this.fabricator.OrderProgress = num;
		}
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x00083559 File Offset: 0x00081759
	private void DestroyVisualizer()
	{
		if (this.visualizer != null)
		{
			if (this.visualizerLink != null)
			{
				this.visualizerLink.Unregister();
				this.visualizerLink = null;
			}
			Util.KDestroyGameObject(this.visualizer);
			this.visualizer = null;
		}
	}

	// Token: 0x04000DC3 RID: 3523
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04000DC4 RID: 3524
	[MyCmpReq]
	private ComplexFabricator fabricator;

	// Token: 0x04000DC5 RID: 3525
	public Action<Worker, float> OnWorkTickActions;

	// Token: 0x04000DC6 RID: 3526
	public MeterController meter;

	// Token: 0x04000DC7 RID: 3527
	protected GameObject visualizer;

	// Token: 0x04000DC8 RID: 3528
	protected KAnimLink visualizerLink;
}
