using System;
using System.Collections.Generic;

// Token: 0x0200035C RID: 860
public class ObjectPool<T>
{
	// Token: 0x06001156 RID: 4438 RVA: 0x0005D310 File Offset: 0x0005B510
	public ObjectPool(Func<T> instantiator, int initial_count = 0)
	{
		this.instantiator = instantiator;
		this.unused = new Stack<T>(initial_count);
		for (int i = 0; i < initial_count; i++)
		{
			this.unused.Push(instantiator());
		}
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x0005D354 File Offset: 0x0005B554
	public virtual T GetInstance()
	{
		T t = default(T);
		if (this.unused.Count > 0)
		{
			t = this.unused.Pop();
		}
		else
		{
			t = this.instantiator();
		}
		return t;
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0005D392 File Offset: 0x0005B592
	public void ReleaseInstance(T instance)
	{
		if (object.Equals(instance, null))
		{
			return;
		}
		this.unused.Push(instance);
	}

	// Token: 0x04000979 RID: 2425
	protected Stack<T> unused;

	// Token: 0x0400097A RID: 2426
	protected Func<T> instantiator;
}
