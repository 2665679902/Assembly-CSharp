using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class CoroutineRunner : MonoBehaviour
{
	// Token: 0x0600113B RID: 4411 RVA: 0x0005CD44 File Offset: 0x0005AF44
	public Promise Run(IEnumerator routine)
	{
		return new Promise(delegate(System.Action resolve)
		{
			this.StartCoroutine(this.RunRoutine(routine, resolve));
		});
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0005CD6C File Offset: 0x0005AF6C
	public ValueTuple<Promise, System.Action> RunCancellable(IEnumerator routine)
	{
		Promise promise = new Promise();
		Coroutine coroutine = base.StartCoroutine(this.RunRoutine(routine, new System.Action(promise.Resolve)));
		System.Action action = delegate
		{
			this.StopCoroutine(coroutine);
		};
		return new ValueTuple<Promise, System.Action>(promise, action);
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0005CDBD File Offset: 0x0005AFBD
	private IEnumerator RunRoutine(IEnumerator routine, System.Action completedCallback)
	{
		yield return routine;
		completedCallback();
		yield break;
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0005CDD3 File Offset: 0x0005AFD3
	public static CoroutineRunner Create()
	{
		return new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0005CDE4 File Offset: 0x0005AFE4
	public static Promise RunOne(IEnumerator routine)
	{
		CoroutineRunner runner = CoroutineRunner.Create();
		return runner.Run(routine).Then(delegate
		{
			UnityEngine.Object.Destroy(runner.gameObject);
		});
	}
}
