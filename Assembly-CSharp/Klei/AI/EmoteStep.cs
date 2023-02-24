using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D9B RID: 3483
	public class EmoteStep
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06006A12 RID: 27154 RVA: 0x00293597 File Offset: 0x00291797
		public int Id
		{
			get
			{
				return this.anim.HashValue;
			}
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x002935A4 File Offset: 0x002917A4
		public HandleVector<EmoteStep.Callbacks>.Handle RegisterCallbacks(Action<GameObject> startedCb, Action<GameObject> finishedCb)
		{
			if (startedCb == null && finishedCb == null)
			{
				return HandleVector<EmoteStep.Callbacks>.InvalidHandle;
			}
			EmoteStep.Callbacks callbacks = new EmoteStep.Callbacks
			{
				StartedCb = startedCb,
				FinishedCb = finishedCb
			};
			return this.callbacks.Add(callbacks);
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x002935E3 File Offset: 0x002917E3
		public void UnregisterCallbacks(HandleVector<EmoteStep.Callbacks>.Handle callbackHandle)
		{
			this.callbacks.Release(callbackHandle);
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x002935F2 File Offset: 0x002917F2
		public void UnregisterAllCallbacks()
		{
			this.callbacks = new HandleVector<EmoteStep.Callbacks>(64);
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x00293604 File Offset: 0x00291804
		public void OnStepStarted(HandleVector<EmoteStep.Callbacks>.Handle callbackHandle, GameObject parameter)
		{
			if (callbackHandle == HandleVector<EmoteStep.Callbacks>.Handle.InvalidHandle)
			{
				return;
			}
			EmoteStep.Callbacks item = this.callbacks.GetItem(callbackHandle);
			if (item.StartedCb != null)
			{
				item.StartedCb(parameter);
			}
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x00293640 File Offset: 0x00291840
		public void OnStepFinished(HandleVector<EmoteStep.Callbacks>.Handle callbackHandle, GameObject parameter)
		{
			if (callbackHandle == HandleVector<EmoteStep.Callbacks>.Handle.InvalidHandle)
			{
				return;
			}
			EmoteStep.Callbacks item = this.callbacks.GetItem(callbackHandle);
			if (item.FinishedCb != null)
			{
				item.FinishedCb(parameter);
			}
		}

		// Token: 0x04004FB2 RID: 20402
		public HashedString anim = HashedString.Invalid;

		// Token: 0x04004FB3 RID: 20403
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;

		// Token: 0x04004FB4 RID: 20404
		public float timeout = -1f;

		// Token: 0x04004FB5 RID: 20405
		private HandleVector<EmoteStep.Callbacks> callbacks = new HandleVector<EmoteStep.Callbacks>(64);

		// Token: 0x02001E6F RID: 7791
		public struct Callbacks
		{
			// Token: 0x040088AE RID: 34990
			public Action<GameObject> StartedCb;

			// Token: 0x040088AF RID: 34991
			public Action<GameObject> FinishedCb;
		}
	}
}
