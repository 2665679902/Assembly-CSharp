using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class KInputHandler
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C844 File Offset: 0x0000AA44
	public KInputController currentController
	{
		get
		{
			return this.mController;
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000C84C File Offset: 0x0000AA4C
	public KInputHandler(IInputHandler obj, KInputController controller)
		: this(obj)
	{
		this.mController = controller;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000C85C File Offset: 0x0000AA5C
	public KInputHandler(IInputHandler obj)
	{
		this.name = obj.handlerName;
		MethodInfo method = obj.GetType().GetMethod("OnKeyDown");
		if (method != null)
		{
			Action<KButtonEvent> action = (Action<KButtonEvent>)Delegate.CreateDelegate(typeof(Action<KButtonEvent>), obj, method);
			this.mOnKeyDownDelegates.Add(action);
		}
		MethodInfo method2 = obj.GetType().GetMethod("OnKeyUp");
		if (method2 != null)
		{
			Action<KButtonEvent> action2 = (Action<KButtonEvent>)Delegate.CreateDelegate(typeof(Action<KButtonEvent>), obj, method2);
			this.mOnKeyUpDelegates.Add(action2);
		}
		MethodInfo method3 = obj.GetType().GetMethod("OnCancelInput");
		if (method3 != null)
		{
			this.mOnCancelInputDelegate = (KInputHandler.KCancelInputHandler)Delegate.CreateDelegate(typeof(KInputHandler.KCancelInputHandler), obj, method3);
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000C943 File Offset: 0x0000AB43
	public int HandleChildCount()
	{
		if (this.mChildren == null)
		{
			return 0;
		}
		return this.mChildren.Count;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000C95C File Offset: 0x0000AB5C
	public void TransferHandles(KInputHandler to)
	{
		if (this.mChildren != null && to.mChildren != null)
		{
			for (int i = 0; i < this.mChildren.Count; i++)
			{
				if (!to.mChildren.Contains(this.mChildren[i]))
				{
					to.mChildren.Add(new KInputHandler.HandlerInfo
					{
						priority = this.mChildren[i].priority,
						handler = this.mChildren[i].handler
					});
					to.mChildren.Sort((KInputHandler.HandlerInfo a, KInputHandler.HandlerInfo b) => b.priority.CompareTo(a.priority));
				}
			}
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000CA24 File Offset: 0x0000AC24
	public void SetController(KInputController controller)
	{
		this.mController = controller;
		if (this.mChildren != null)
		{
			foreach (KInputHandler.HandlerInfo handlerInfo in this.mChildren)
			{
				handlerInfo.handler.SetController(controller);
			}
			this.mChildren.Sort((KInputHandler.HandlerInfo a, KInputHandler.HandlerInfo b) => b.priority.CompareTo(a.priority));
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
	public void AddInputHandler(KInputHandler handler, int priority)
	{
		if (this.mChildren == null)
		{
			this.mChildren = new List<KInputHandler.HandlerInfo>();
		}
		handler.SetController(this.mController);
		this.mChildren.Add(new KInputHandler.HandlerInfo
		{
			priority = priority,
			handler = handler
		});
		this.mChildren.Sort((KInputHandler.HandlerInfo a, KInputHandler.HandlerInfo b) => b.priority.CompareTo(a.priority));
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000CB30 File Offset: 0x0000AD30
	public void RemoveInputHandler(KInputHandler handler)
	{
		if (this.mChildren != null)
		{
			for (int i = 0; i < this.mChildren.Count; i++)
			{
				if (this.mChildren[i].handler == handler)
				{
					this.mChildren.RemoveAt(i);
					return;
				}
			}
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000CB7C File Offset: 0x0000AD7C
	public void PushInputHandler(KInputHandler handler)
	{
		if (this.mChildren == null)
		{
			this.mChildren = new List<KInputHandler.HandlerInfo>();
		}
		handler.SetController(this.mController);
		this.mChildren.Insert(0, new KInputHandler.HandlerInfo
		{
			priority = int.MaxValue,
			handler = handler
		});
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000CBD1 File Offset: 0x0000ADD1
	public void PopInputHandler()
	{
		if (this.mChildren != null)
		{
			this.mChildren.RemoveAt(0);
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
	public void HandleCancelInput()
	{
		int num = 0;
		while (this.mChildren != null && num < this.mChildren.Count)
		{
			this.mChildren[num].handler.HandleCancelInput();
			num++;
		}
		if (this.mOnCancelInputDelegate == null)
		{
			return;
		}
		this.mOnCancelInputDelegate();
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000CC3D File Offset: 0x0000AE3D
	public void HandleEvent(KInputEvent e)
	{
		if (e.Type == InputEventType.KeyDown)
		{
			this.HandleKeyDown((KButtonEvent)e);
			return;
		}
		if (InputEventType.KeyUp == e.Type)
		{
			this.HandleKeyUp((KButtonEvent)e);
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000CC6C File Offset: 0x0000AE6C
	public void HandleKeyDown(KButtonEvent e)
	{
		this.lastConsumedEventDown = null;
		foreach (Action<KButtonEvent> action in this.mOnKeyDownDelegates)
		{
			action(e);
			if (e.Consumed)
			{
				this.lastConsumedEventDown = e;
			}
		}
		if (!e.Consumed && this.mChildren != null)
		{
			foreach (KInputHandler.HandlerInfo handlerInfo in this.mChildren)
			{
				handlerInfo.handler.HandleKeyDown(e);
				if (e.Consumed)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000CD34 File Offset: 0x0000AF34
	public void HandleKeyUp(KButtonEvent e)
	{
		this.lastConsumedEventUp = null;
		foreach (Action<KButtonEvent> action in this.mOnKeyUpDelegates)
		{
			action(e);
			if (e.Consumed)
			{
				this.lastConsumedEventUp = e;
			}
		}
		if (!e.Consumed && this.mChildren != null)
		{
			foreach (KInputHandler.HandlerInfo handlerInfo in this.mChildren)
			{
				handlerInfo.handler.HandleKeyUp(e);
				if (e.Consumed)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000CDFC File Offset: 0x0000AFFC
	public static KInputHandler GetInputHandler(IInputHandler handler)
	{
		if (handler.inputHandler == null)
		{
			handler.inputHandler = new KInputHandler(handler);
		}
		return handler.inputHandler;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000CE18 File Offset: 0x0000B018
	public static void Add(IInputHandler parent, GameObject child)
	{
		Component[] components = child.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			IInputHandler inputHandler = components[i] as IInputHandler;
			if (inputHandler != null)
			{
				KInputHandler.Add(parent, inputHandler, 0);
			}
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000CE50 File Offset: 0x0000B050
	public static void Add(IInputHandler parent, IInputHandler child, int priority = 0)
	{
		KInputHandler inputHandler = KInputHandler.GetInputHandler(parent);
		KInputHandler inputHandler2 = KInputHandler.GetInputHandler(child);
		inputHandler.AddInputHandler(inputHandler2, priority);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000CE74 File Offset: 0x0000B074
	public static void Push(IInputHandler parent, IInputHandler child)
	{
		KInputHandler inputHandler = KInputHandler.GetInputHandler(parent);
		KInputHandler inputHandler2 = KInputHandler.GetInputHandler(child);
		inputHandler.PushInputHandler(inputHandler2);
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000CE94 File Offset: 0x0000B094
	public static void Remove(IInputHandler parent, IInputHandler child)
	{
		KInputHandler inputHandler = KInputHandler.GetInputHandler(parent);
		KInputHandler inputHandler2 = KInputHandler.GetInputHandler(child);
		inputHandler.RemoveInputHandler(inputHandler2);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
	public bool IsActive(global::Action action)
	{
		return this.mController != null && this.mController.IsActive(action);
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000CECC File Offset: 0x0000B0CC
	public bool UsesController(IInputHandler handlerToCheck, KInputController conToCheck)
	{
		return handlerToCheck.inputHandler.mController == conToCheck;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000CEDF File Offset: 0x0000B0DF
	public float GetAxis(Axis axis)
	{
		if (this.mController != null)
		{
			return this.mController.GetAxis(axis);
		}
		return 0f;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000CEFB File Offset: 0x0000B0FB
	public bool IsGamepad()
	{
		return this.mController != null && this.mController.IsGamepad;
	}

	// Token: 0x04000234 RID: 564
	private List<Action<KButtonEvent>> mOnKeyDownDelegates = new List<Action<KButtonEvent>>();

	// Token: 0x04000235 RID: 565
	private List<Action<KButtonEvent>> mOnKeyUpDelegates = new List<Action<KButtonEvent>>();

	// Token: 0x04000236 RID: 566
	private KInputHandler.KCancelInputHandler mOnCancelInputDelegate;

	// Token: 0x04000237 RID: 567
	private List<KInputHandler.HandlerInfo> mChildren;

	// Token: 0x04000238 RID: 568
	private KInputController mController;

	// Token: 0x04000239 RID: 569
	private string name;

	// Token: 0x0400023A RID: 570
	private KButtonEvent lastConsumedEventDown;

	// Token: 0x0400023B RID: 571
	private KButtonEvent lastConsumedEventUp;

	// Token: 0x0200097F RID: 2431
	// (Invoke) Token: 0x060052EE RID: 21230
	public delegate void KButtonEventHandler(KButtonEvent e);

	// Token: 0x02000980 RID: 2432
	// (Invoke) Token: 0x060052F2 RID: 21234
	public delegate void KCancelInputHandler();

	// Token: 0x02000981 RID: 2433
	private struct HandlerInfo
	{
		// Token: 0x040020F6 RID: 8438
		public int priority;

		// Token: 0x040020F7 RID: 8439
		public KInputHandler handler;
	}
}
