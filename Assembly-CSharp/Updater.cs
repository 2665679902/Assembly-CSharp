using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036A RID: 874
public readonly struct Updater : IEnumerator
{
	// Token: 0x060011C2 RID: 4546 RVA: 0x0005E261 File Offset: 0x0005C461
	public Updater(Func<float, UpdaterResult> fn)
	{
		this.fn = fn;
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0005E26A File Offset: 0x0005C46A
	public UpdaterResult Internal_Update(float deltaTime)
	{
		return this.fn(deltaTime);
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0005E278 File Offset: 0x0005C478
	object IEnumerator.Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0005E27B File Offset: 0x0005C47B
	bool IEnumerator.MoveNext()
	{
		return this.fn(Updater.GetDeltaTime()) == UpdaterResult.NotComplete;
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0005E290 File Offset: 0x0005C490
	void IEnumerator.Reset()
	{
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x0005E292 File Offset: 0x0005C492
	public static implicit operator Updater(Promise promise)
	{
		return Updater.Until(() => promise.IsResolved);
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
	public static Updater Until(Func<bool> fn)
	{
		return new Updater(delegate(float dt)
		{
			if (!fn())
			{
				return UpdaterResult.NotComplete;
			}
			return UpdaterResult.Complete;
		});
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x0005E2CE File Offset: 0x0005C4CE
	public static Updater While(Func<bool> fn)
	{
		return new Updater(delegate(float dt)
		{
			if (!fn())
			{
				return UpdaterResult.Complete;
			}
			return UpdaterResult.NotComplete;
		});
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0005E2EC File Offset: 0x0005C4EC
	public static Updater None()
	{
		return Updater.WaitFrames(0);
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x0005E2F4 File Offset: 0x0005C4F4
	public static Updater WaitOneFrame()
	{
		return Updater.WaitFrames(1);
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x0005E2FC File Offset: 0x0005C4FC
	public static Updater WaitFrames(int framesToWait)
	{
		int frame = 0;
		return new Updater(delegate(float dt)
		{
			if (framesToWait <= frame)
			{
				return UpdaterResult.Complete;
			}
			int frame2 = frame;
			frame = frame2 + 1;
			return UpdaterResult.NotComplete;
		});
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x0005E321 File Offset: 0x0005C521
	public static Updater WaitForSeconds(float secondsToWait)
	{
		float currentSeconds = 0f;
		return new Updater(delegate(float dt)
		{
			if (secondsToWait <= currentSeconds)
			{
				return UpdaterResult.Complete;
			}
			currentSeconds += dt;
			return UpdaterResult.NotComplete;
		});
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0005E34A File Offset: 0x0005C54A
	public static Updater Ease(Action<float> fn, float from, float to, float duration, Easing.EasingFn easing = null)
	{
		return Updater.GenericEase<float>(fn, new Func<float, float, float, float>(Mathf.LerpUnclamped), easing, from, to, duration);
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0005E363 File Offset: 0x0005C563
	public static Updater Ease(Action<Vector2> fn, Vector2 from, Vector2 to, float duration, Easing.EasingFn easing = null)
	{
		return Updater.GenericEase<Vector2>(fn, new Func<Vector2, Vector2, float, Vector2>(Vector2.LerpUnclamped), easing, from, to, duration);
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0005E37C File Offset: 0x0005C57C
	public static Updater Ease(Action<Vector3> fn, Vector3 from, Vector3 to, float duration, Easing.EasingFn easing = null)
	{
		return Updater.GenericEase<Vector3>(fn, new Func<Vector3, Vector3, float, Vector3>(Vector3.LerpUnclamped), easing, from, to, duration);
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0005E398 File Offset: 0x0005C598
	public static Updater GenericEase<T>(Action<T> useFn, Func<T, T, float, T> interpolateFn, Easing.EasingFn easingFn, T from, T to, float duration)
	{
		Updater.<>c__DisplayClass17_0<T> CS$<>8__locals1 = new Updater.<>c__DisplayClass17_0<T>();
		CS$<>8__locals1.useFn = useFn;
		CS$<>8__locals1.interpolateFn = interpolateFn;
		CS$<>8__locals1.from = from;
		CS$<>8__locals1.to = to;
		CS$<>8__locals1.easingFn = easingFn;
		CS$<>8__locals1.duration = duration;
		if (CS$<>8__locals1.easingFn == null)
		{
			CS$<>8__locals1.easingFn = Easing.SmoothStep;
		}
		CS$<>8__locals1.currentSeconds = 0f;
		CS$<>8__locals1.<GenericEase>g__UseKeyframeAt|0(0f);
		return new Updater(delegate(float dt)
		{
			CS$<>8__locals1.currentSeconds += dt;
			if (CS$<>8__locals1.currentSeconds < CS$<>8__locals1.duration)
			{
				base.<GenericEase>g__UseKeyframeAt|0(CS$<>8__locals1.currentSeconds / CS$<>8__locals1.duration);
				return UpdaterResult.NotComplete;
			}
			base.<GenericEase>g__UseKeyframeAt|0(1f);
			return UpdaterResult.Complete;
		});
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x0005E411 File Offset: 0x0005C611
	public static Updater Do(System.Action fn)
	{
		return new Updater(delegate(float dt)
		{
			fn();
			return UpdaterResult.Complete;
		});
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x0005E42F File Offset: 0x0005C62F
	public static Updater Do(Func<Updater> fn)
	{
		bool didInitalize = false;
		Updater target = default(Updater);
		return new Updater(delegate(float dt)
		{
			if (!didInitalize)
			{
				target = fn();
				didInitalize = true;
			}
			return target.Internal_Update(dt);
		});
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0005E460 File Offset: 0x0005C660
	public static Updater Loop(params Func<Updater>[] makeUpdaterFns)
	{
		return Updater.Internal_Loop(Option.None, makeUpdaterFns);
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x0005E472 File Offset: 0x0005C672
	public static Updater Loop(int loopCount, params Func<Updater>[] makeUpdaterFns)
	{
		return Updater.Internal_Loop(loopCount, makeUpdaterFns);
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0005E480 File Offset: 0x0005C680
	public static Updater Internal_Loop(Option<int> limitLoopCount, Func<Updater>[] makeUpdaterFns)
	{
		if (makeUpdaterFns == null || makeUpdaterFns.Length == 0)
		{
			return Updater.None();
		}
		int completedLoopCount = 0;
		int currentIndex = 0;
		Updater currentUpdater = makeUpdaterFns[currentIndex]();
		return new Updater(delegate(float dt)
		{
			if (currentUpdater.Internal_Update(dt) == UpdaterResult.Complete)
			{
				int num = currentIndex;
				currentIndex = num + 1;
				if (currentIndex >= makeUpdaterFns.Length)
				{
					currentIndex -= makeUpdaterFns.Length;
					num = completedLoopCount;
					completedLoopCount = num + 1;
					if (limitLoopCount.IsSome() && completedLoopCount >= limitLoopCount.Unwrap())
					{
						return UpdaterResult.Complete;
					}
				}
				currentUpdater = makeUpdaterFns[currentIndex]();
			}
			return UpdaterResult.NotComplete;
		});
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x0005E4EF File Offset: 0x0005C6EF
	public static Updater Parallel(params Updater[] updaters)
	{
		bool[] isCompleted = new bool[updaters.Length];
		return new Updater(delegate(float dt)
		{
			bool flag = false;
			for (int i = 0; i < updaters.Length; i++)
			{
				if (!isCompleted[i])
				{
					if (updaters[i].Internal_Update(dt) == UpdaterResult.Complete)
					{
						isCompleted[i] = true;
					}
					else
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return UpdaterResult.Complete;
			}
			return UpdaterResult.NotComplete;
		});
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0005E520 File Offset: 0x0005C720
	public static Updater Series(params Updater[] updaters)
	{
		int i = 0;
		return new Updater(delegate(float dt)
		{
			if (updaters[i].Internal_Update(dt) == UpdaterResult.Complete)
			{
				int j = i;
				i = j + 1;
			}
			if (i == updaters.Length)
			{
				return UpdaterResult.Complete;
			}
			return UpdaterResult.NotComplete;
		});
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x0005E548 File Offset: 0x0005C748
	public static Promise RunRoutine(MonoBehaviour monoBehaviour, IEnumerator coroutine)
	{
		Updater.<>c__DisplayClass25_0 CS$<>8__locals1 = new Updater.<>c__DisplayClass25_0();
		CS$<>8__locals1.coroutine = coroutine;
		CS$<>8__locals1.willComplete = new Promise();
		monoBehaviour.StartCoroutine(CS$<>8__locals1.<RunRoutine>g__Routine|0());
		return CS$<>8__locals1.willComplete;
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x0005E580 File Offset: 0x0005C780
	public static Promise Run(MonoBehaviour monoBehaviour, params Updater[] updaters)
	{
		return Updater.Run(monoBehaviour, Updater.Series(updaters));
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x0005E590 File Offset: 0x0005C790
	public static Promise Run(MonoBehaviour monoBehaviour, Updater updater)
	{
		Updater.<>c__DisplayClass27_0 CS$<>8__locals1 = new Updater.<>c__DisplayClass27_0();
		CS$<>8__locals1.updater = updater;
		CS$<>8__locals1.willComplete = new Promise();
		monoBehaviour.StartCoroutine(CS$<>8__locals1.<Run>g__Routine|0());
		return CS$<>8__locals1.willComplete;
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x0005E5C8 File Offset: 0x0005C7C8
	public static float GetDeltaTime()
	{
		return Time.unscaledDeltaTime;
	}

	// Token: 0x04000996 RID: 2454
	public readonly Func<float, UpdaterResult> fn;
}
