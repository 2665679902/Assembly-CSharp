using System;
using System.Collections;

// Token: 0x02000363 RID: 867
public class Promise : IEnumerator
{
	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06001195 RID: 4501 RVA: 0x0005DB58 File Offset: 0x0005BD58
	public bool IsResolved
	{
		get
		{
			return this.m_is_resolved;
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x0005DB60 File Offset: 0x0005BD60
	public Promise(Action<System.Action> fn)
	{
		fn(delegate
		{
			this.Resolve();
		});
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0005DB7A File Offset: 0x0005BD7A
	public Promise()
	{
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0005DB82 File Offset: 0x0005BD82
	public void EnsureResolved()
	{
		if (this.IsResolved)
		{
			return;
		}
		this.Resolve();
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x0005DB93 File Offset: 0x0005BD93
	public void Resolve()
	{
		DebugUtil.Assert(!this.m_is_resolved, "Can only resolve a promise once");
		this.m_is_resolved = true;
		if (this.on_complete != null)
		{
			this.on_complete();
			this.on_complete = null;
		}
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x0005DBC9 File Offset: 0x0005BDC9
	public Promise Then(System.Action callback)
	{
		if (this.m_is_resolved)
		{
			callback();
		}
		else
		{
			this.on_complete = (System.Action)Delegate.Combine(this.on_complete, callback);
		}
		return this;
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0005DBF4 File Offset: 0x0005BDF4
	public Promise ThenWait(Func<Promise> callback)
	{
		if (this.m_is_resolved)
		{
			return callback();
		}
		return new Promise(delegate(System.Action resolve)
		{
			this.on_complete = (System.Action)Delegate.Combine(this.on_complete, new System.Action(delegate
			{
				callback().Then(resolve);
			}));
		});
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x0005DC3C File Offset: 0x0005BE3C
	public Promise<T> ThenWait<T>(Func<Promise<T>> callback)
	{
		if (this.m_is_resolved)
		{
			return callback();
		}
		return new Promise<T>(delegate(Action<T> resolve)
		{
			this.on_complete = (System.Action)Delegate.Combine(this.on_complete, new System.Action(delegate
			{
				callback().Then(resolve);
			}));
		});
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600119D RID: 4509 RVA: 0x0005DC82 File Offset: 0x0005BE82
	object IEnumerator.Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0005DC85 File Offset: 0x0005BE85
	bool IEnumerator.MoveNext()
	{
		return !this.IsResolved;
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0005DC90 File Offset: 0x0005BE90
	void IEnumerator.Reset()
	{
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0005DC92 File Offset: 0x0005BE92
	static Promise()
	{
		Promise.m_instant.Resolve();
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0005DCA8 File Offset: 0x0005BEA8
	public static Promise Instant
	{
		get
		{
			return Promise.m_instant;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0005DCAF File Offset: 0x0005BEAF
	public static Promise Fail
	{
		get
		{
			return new Promise();
		}
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
	public static Promise All(params Promise[] promises)
	{
		Promise.<>c__DisplayClass21_0 CS$<>8__locals1 = new Promise.<>c__DisplayClass21_0();
		CS$<>8__locals1.promises = promises;
		if (CS$<>8__locals1.promises == null || CS$<>8__locals1.promises.Length == 0)
		{
			return Promise.Instant;
		}
		CS$<>8__locals1.all_resolved_promise = new Promise();
		Promise[] promises2 = CS$<>8__locals1.promises;
		for (int i = 0; i < promises2.Length; i++)
		{
			promises2[i].Then(new System.Action(CS$<>8__locals1.<All>g__TryResolve|0));
		}
		return CS$<>8__locals1.all_resolved_promise;
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x0005DD24 File Offset: 0x0005BF24
	public static Promise Chain(params Func<Promise>[] make_promise_fns)
	{
		Promise.<>c__DisplayClass22_0 CS$<>8__locals1 = new Promise.<>c__DisplayClass22_0();
		CS$<>8__locals1.make_promise_fns = make_promise_fns;
		CS$<>8__locals1.all_resolve_promise = new Promise();
		CS$<>8__locals1.current_promise_fn_index = 0;
		CS$<>8__locals1.<Chain>g__TryNext|0();
		return CS$<>8__locals1.all_resolve_promise;
	}

	// Token: 0x04000985 RID: 2437
	private System.Action on_complete;

	// Token: 0x04000986 RID: 2438
	private bool m_is_resolved;

	// Token: 0x04000987 RID: 2439
	private static Promise m_instant = new Promise();
}
