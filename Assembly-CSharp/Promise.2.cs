using System;
using System.Collections;

// Token: 0x02000364 RID: 868
public class Promise<T> : IEnumerator
{
	// Token: 0x060011A6 RID: 4518 RVA: 0x0005DD57 File Offset: 0x0005BF57
	public Promise(Action<Action<T>> fn)
	{
		fn(delegate(T value)
		{
			this.Resolve(value);
		});
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0005DD7C File Offset: 0x0005BF7C
	public Promise()
	{
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x0005DD8F File Offset: 0x0005BF8F
	public void EnsureResolved(T value)
	{
		this.result = value;
		this.promise.EnsureResolved();
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x0005DDA3 File Offset: 0x0005BFA3
	public void Resolve(T value)
	{
		this.result = value;
		this.promise.Resolve();
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0005DDB8 File Offset: 0x0005BFB8
	public Promise<T> Then(Action<T> fn)
	{
		this.promise.Then(delegate
		{
			fn(this.result);
		});
		return this;
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0005DDF2 File Offset: 0x0005BFF2
	public Promise ThenWait(Func<Promise> fn)
	{
		return this.promise.ThenWait(fn);
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0005DE00 File Offset: 0x0005C000
	public Promise<T> ThenWait(Func<Promise<T>> fn)
	{
		return this.promise.ThenWait<T>(fn);
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060011AD RID: 4525 RVA: 0x0005DE0E File Offset: 0x0005C00E
	object IEnumerator.Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x0005DE11 File Offset: 0x0005C011
	bool IEnumerator.MoveNext()
	{
		return !this.promise.IsResolved;
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x0005DE21 File Offset: 0x0005C021
	void IEnumerator.Reset()
	{
	}

	// Token: 0x04000988 RID: 2440
	private Promise promise = new Promise();

	// Token: 0x04000989 RID: 2441
	private T result;
}
