using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class TransitionDriver
{
	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06001E07 RID: 7687 RVA: 0x000A0007 File Offset: 0x0009E207
	public Navigator.ActiveTransition GetTransition
	{
		get
		{
			return this.transition;
		}
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x000A000F File Offset: 0x0009E20F
	public TransitionDriver(Navigator navigator)
	{
		this.log = new LoggerFS("TransitionDriver", 35);
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x000A0040 File Offset: 0x0009E240
	public void BeginTransition(Navigator navigator, NavGrid.Transition transition, float defaultSpeed)
	{
		Navigator.ActiveTransition instance = TransitionDriver.TransitionPool.GetInstance();
		instance.Init(transition, defaultSpeed);
		this.BeginTransition(navigator, instance);
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x000A0068 File Offset: 0x0009E268
	private void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		bool flag = this.interruptOverrideStack.Count != 0;
		foreach (TransitionDriver.OverrideLayer overrideLayer in this.overrideLayers)
		{
			if (!flag || !(overrideLayer is TransitionDriver.InterruptOverrideLayer))
			{
				overrideLayer.BeginTransition(navigator, transition);
			}
		}
		this.navigator = navigator;
		this.transition = transition;
		this.isComplete = false;
		Grid.SceneLayer sceneLayer = navigator.sceneLayer;
		if (transition.navGridTransition.start == NavType.Tube || transition.navGridTransition.end == NavType.Tube)
		{
			sceneLayer = Grid.SceneLayer.BuildingUse;
		}
		else if (transition.navGridTransition.start == NavType.Solid && transition.navGridTransition.end == NavType.Solid)
		{
			KBatchedAnimController component = navigator.GetComponent<KBatchedAnimController>();
			sceneLayer = Grid.SceneLayer.FXFront;
			component.SetSceneLayer(sceneLayer);
		}
		else if (transition.navGridTransition.start == NavType.Solid || transition.navGridTransition.end == NavType.Solid)
		{
			navigator.GetComponent<KBatchedAnimController>().SetSceneLayer(sceneLayer);
		}
		int num = Grid.OffsetCell(Grid.PosToCell(navigator), transition.x, transition.y);
		this.targetPos = Grid.CellToPosCBC(num, sceneLayer);
		if (transition.isLooping)
		{
			KAnimControllerBase component2 = navigator.GetComponent<KAnimControllerBase>();
			component2.PlaySpeedMultiplier = transition.animSpeed;
			bool flag2 = transition.preAnim != "";
			bool flag3 = component2.CurrentAnim != null && component2.CurrentAnim.name == transition.anim;
			if (flag2 && component2.CurrentAnim != null && component2.CurrentAnim.name == transition.preAnim)
			{
				component2.ClearQueue();
				component2.Queue(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
			else if (flag3)
			{
				if (component2.PlayMode != KAnim.PlayMode.Loop)
				{
					component2.ClearQueue();
					component2.Queue(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
				}
			}
			else if (flag2)
			{
				component2.Play(transition.preAnim, KAnim.PlayMode.Once, 1f, 0f);
				component2.Queue(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
			else
			{
				component2.Play(transition.anim, KAnim.PlayMode.Loop, 1f, 0f);
			}
		}
		else if (transition.anim != null)
		{
			KAnimControllerBase component3 = navigator.GetComponent<KAnimControllerBase>();
			component3.PlaySpeedMultiplier = transition.animSpeed;
			component3.Play(transition.anim, KAnim.PlayMode.Once, 1f, 0f);
			navigator.Subscribe(-1061186183, new Action<object>(this.OnAnimComplete));
		}
		if (transition.navGridTransition.y != 0)
		{
			if (transition.navGridTransition.start == NavType.RightWall)
			{
				navigator.GetComponent<Facing>().SetFacing(transition.navGridTransition.y < 0);
			}
			else if (transition.navGridTransition.start == NavType.LeftWall)
			{
				navigator.GetComponent<Facing>().SetFacing(transition.navGridTransition.y > 0);
			}
		}
		if (transition.navGridTransition.x != 0)
		{
			if (transition.navGridTransition.start == NavType.Ceiling)
			{
				navigator.GetComponent<Facing>().SetFacing(transition.navGridTransition.x > 0);
			}
			else if (transition.navGridTransition.start != NavType.LeftWall && transition.navGridTransition.start != NavType.RightWall)
			{
				navigator.GetComponent<Facing>().SetFacing(transition.navGridTransition.x < 0);
			}
		}
		this.brain = navigator.GetComponent<Brain>();
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x000A03F4 File Offset: 0x0009E5F4
	public void UpdateTransition(float dt)
	{
		if (this.navigator == null)
		{
			return;
		}
		foreach (TransitionDriver.OverrideLayer overrideLayer in this.overrideLayers)
		{
			bool flag = this.interruptOverrideStack.Count != 0;
			bool flag2 = overrideLayer is TransitionDriver.InterruptOverrideLayer;
			if (!flag || !flag2 || this.interruptOverrideStack.Peek() == overrideLayer)
			{
				overrideLayer.UpdateTransition(this.navigator, this.transition);
			}
		}
		if (!this.isComplete && this.transition.isCompleteCB != null)
		{
			this.isComplete = this.transition.isCompleteCB();
		}
		if (this.brain != null)
		{
			bool flag3 = this.isComplete;
		}
		if (this.transition.isLooping)
		{
			float speed = this.transition.speed;
			Vector3 position = this.navigator.transform.GetPosition();
			int num = Grid.PosToCell(position);
			if (this.transition.x > 0)
			{
				position.x += dt * speed;
				if (position.x > this.targetPos.x)
				{
					this.isComplete = true;
				}
			}
			else if (this.transition.x < 0)
			{
				position.x -= dt * speed;
				if (position.x < this.targetPos.x)
				{
					this.isComplete = true;
				}
			}
			else
			{
				position.x = this.targetPos.x;
			}
			if (this.transition.y > 0)
			{
				position.y += dt * speed;
				if (position.y > this.targetPos.y)
				{
					this.isComplete = true;
				}
			}
			else if (this.transition.y < 0)
			{
				position.y -= dt * speed;
				if (position.y < this.targetPos.y)
				{
					this.isComplete = true;
				}
			}
			else
			{
				position.y = this.targetPos.y;
			}
			this.navigator.transform.SetPosition(position);
			int num2 = Grid.PosToCell(position);
			if (num2 != num)
			{
				this.navigator.Trigger(915392638, num2);
			}
		}
		if (this.isComplete)
		{
			this.isComplete = false;
			Navigator navigator = this.navigator;
			navigator.SetCurrentNavType(this.transition.end);
			navigator.transform.SetPosition(this.targetPos);
			this.EndTransition();
			navigator.AdvancePath(true);
		}
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x000A0698 File Offset: 0x0009E898
	public void EndTransition()
	{
		if (this.navigator != null)
		{
			this.interruptOverrideStack.Clear();
			foreach (TransitionDriver.OverrideLayer overrideLayer in this.overrideLayers)
			{
				overrideLayer.EndTransition(this.navigator, this.transition);
			}
			this.navigator.GetComponent<KAnimControllerBase>().PlaySpeedMultiplier = 1f;
			this.navigator.Unsubscribe(-1061186183, new Action<object>(this.OnAnimComplete));
			if (this.brain != null)
			{
				this.brain.Resume("move_handler");
			}
			if (this.navigator.animEventHandler != null)
			{
				this.navigator.animEventHandler.SetDirty();
			}
			TransitionDriver.TransitionPool.ReleaseInstance(this.transition);
			this.transition = null;
			this.navigator = null;
			this.brain = null;
		}
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x000A07A8 File Offset: 0x0009E9A8
	private void OnAnimComplete(object data)
	{
		if (this.navigator != null)
		{
			this.navigator.Unsubscribe(-1061186183, new Action<object>(this.OnAnimComplete));
		}
		this.isComplete = true;
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x000A07DB File Offset: 0x0009E9DB
	public static Navigator.ActiveTransition SwapTransitionWithEmpty(Navigator.ActiveTransition src)
	{
		Navigator.ActiveTransition instance = TransitionDriver.TransitionPool.GetInstance();
		instance.Copy(src);
		src.Copy(TransitionDriver.emptyTransition);
		return instance;
	}

	// Token: 0x040010D0 RID: 4304
	private static Navigator.ActiveTransition emptyTransition = new Navigator.ActiveTransition();

	// Token: 0x040010D1 RID: 4305
	public static ObjectPool<Navigator.ActiveTransition> TransitionPool = new ObjectPool<Navigator.ActiveTransition>(() => new Navigator.ActiveTransition(), 128);

	// Token: 0x040010D2 RID: 4306
	private Stack<TransitionDriver.InterruptOverrideLayer> interruptOverrideStack = new Stack<TransitionDriver.InterruptOverrideLayer>(8);

	// Token: 0x040010D3 RID: 4307
	private Navigator.ActiveTransition transition;

	// Token: 0x040010D4 RID: 4308
	private Navigator navigator;

	// Token: 0x040010D5 RID: 4309
	private Vector3 targetPos;

	// Token: 0x040010D6 RID: 4310
	private bool isComplete;

	// Token: 0x040010D7 RID: 4311
	private Brain brain;

	// Token: 0x040010D8 RID: 4312
	public List<TransitionDriver.OverrideLayer> overrideLayers = new List<TransitionDriver.OverrideLayer>();

	// Token: 0x040010D9 RID: 4313
	private LoggerFS log;

	// Token: 0x0200112D RID: 4397
	public class OverrideLayer
	{
		// Token: 0x060075C1 RID: 30145 RVA: 0x002B6D52 File Offset: 0x002B4F52
		public OverrideLayer(Navigator navigator)
		{
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x002B6D5A File Offset: 0x002B4F5A
		public virtual void Destroy()
		{
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x002B6D5C File Offset: 0x002B4F5C
		public virtual void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x002B6D5E File Offset: 0x002B4F5E
		public virtual void UpdateTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x002B6D60 File Offset: 0x002B4F60
		public virtual void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
		}
	}

	// Token: 0x0200112E RID: 4398
	public class InterruptOverrideLayer : TransitionDriver.OverrideLayer
	{
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060075C6 RID: 30150 RVA: 0x002B6D62 File Offset: 0x002B4F62
		protected bool InterruptInProgress
		{
			get
			{
				return this.originalTransition != null;
			}
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x002B6D6D File Offset: 0x002B4F6D
		public InterruptOverrideLayer(Navigator navigator)
			: base(navigator)
		{
			this.driver = navigator.transitionDriver;
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x002B6D82 File Offset: 0x002B4F82
		public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
			this.driver.interruptOverrideStack.Push(this);
			this.originalTransition = TransitionDriver.SwapTransitionWithEmpty(transition);
		}

		// Token: 0x060075C9 RID: 30153 RVA: 0x002B6DA4 File Offset: 0x002B4FA4
		public override void UpdateTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
			if (!this.IsOverrideComplete())
			{
				return;
			}
			this.driver.interruptOverrideStack.Pop();
			transition.Copy(this.originalTransition);
			TransitionDriver.TransitionPool.ReleaseInstance(this.originalTransition);
			this.originalTransition = null;
			this.EndTransition(navigator, transition);
			this.driver.BeginTransition(navigator, transition);
		}

		// Token: 0x060075CA RID: 30154 RVA: 0x002B6E03 File Offset: 0x002B5003
		public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
		{
			base.EndTransition(navigator, transition);
			if (this.originalTransition == null)
			{
				return;
			}
			TransitionDriver.TransitionPool.ReleaseInstance(this.originalTransition);
			this.originalTransition = null;
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x002B6E2D File Offset: 0x002B502D
		protected virtual bool IsOverrideComplete()
		{
			return this.originalTransition != null && this.driver.interruptOverrideStack.Count != 0 && this.driver.interruptOverrideStack.Peek() == this;
		}

		// Token: 0x04005A06 RID: 23046
		protected Navigator.ActiveTransition originalTransition;

		// Token: 0x04005A07 RID: 23047
		protected TransitionDriver driver;
	}
}
