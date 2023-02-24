using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class AnimEventManager : Singleton<AnimEventManager>
{
	// Token: 0x06001582 RID: 5506 RVA: 0x0006F442 File Offset: 0x0006D642
	public void FreeResources()
	{
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x0006F444 File Offset: 0x0006D644
	public HandleVector<int>.Handle PlayAnim(KAnimControllerBase controller, KAnim.Anim anim, KAnim.PlayMode mode, float time, bool use_unscaled_time)
	{
		AnimEventManager.AnimData animData = default(AnimEventManager.AnimData);
		animData.frameRate = anim.frameRate;
		animData.totalTime = anim.totalTime;
		animData.numFrames = anim.numFrames;
		animData.useUnscaledTime = use_unscaled_time;
		AnimEventManager.EventPlayerData eventPlayerData = default(AnimEventManager.EventPlayerData);
		eventPlayerData.elapsedTime = time;
		eventPlayerData.mode = mode;
		eventPlayerData.controller = controller as KBatchedAnimController;
		eventPlayerData.currentFrame = eventPlayerData.controller.GetFrameIdx(eventPlayerData.elapsedTime, false);
		eventPlayerData.previousFrame = -1;
		eventPlayerData.events = null;
		eventPlayerData.updatingEvents = null;
		eventPlayerData.events = GameAudioSheets.Get().GetEvents(anim.id);
		if (eventPlayerData.events == null)
		{
			eventPlayerData.events = AnimEventManager.emptyEventList;
		}
		HandleVector<int>.Handle handle3;
		if (animData.useUnscaledTime)
		{
			HandleVector<int>.Handle handle = this.uiAnimData.Allocate(animData);
			HandleVector<int>.Handle handle2 = this.uiEventData.Allocate(eventPlayerData);
			handle3 = this.indirectionData.Allocate(new AnimEventManager.IndirectionData(handle, handle2, true));
		}
		else
		{
			HandleVector<int>.Handle handle4 = this.animData.Allocate(animData);
			HandleVector<int>.Handle handle5 = this.eventData.Allocate(eventPlayerData);
			handle3 = this.indirectionData.Allocate(new AnimEventManager.IndirectionData(handle4, handle5, false));
		}
		return handle3;
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x0006F578 File Offset: 0x0006D778
	public void SetMode(HandleVector<int>.Handle handle, KAnim.PlayMode mode)
	{
		if (!handle.IsValid())
		{
			return;
		}
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		KCompactedVector<AnimEventManager.EventPlayerData> kcompactedVector = (data.isUIData ? this.uiEventData : this.eventData);
		AnimEventManager.EventPlayerData data2 = kcompactedVector.GetData(data.eventDataHandle);
		data2.mode = mode;
		kcompactedVector.SetData(data.eventDataHandle, data2);
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x0006F5D4 File Offset: 0x0006D7D4
	public void StopAnim(HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return;
		}
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		KCompactedVector<AnimEventManager.AnimData> kcompactedVector = (data.isUIData ? this.uiAnimData : this.animData);
		KCompactedVector<AnimEventManager.EventPlayerData> kcompactedVector2 = (data.isUIData ? this.uiEventData : this.eventData);
		AnimEventManager.EventPlayerData data2 = kcompactedVector2.GetData(data.eventDataHandle);
		this.StopEvents(data2);
		kcompactedVector.Free(data.animDataHandle);
		kcompactedVector2.Free(data.eventDataHandle);
		this.indirectionData.Free(handle);
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x0006F660 File Offset: 0x0006D860
	public float GetElapsedTime(HandleVector<int>.Handle handle)
	{
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		return (data.isUIData ? this.uiEventData : this.eventData).GetData(data.eventDataHandle).elapsedTime;
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x0006F6A0 File Offset: 0x0006D8A0
	public void SetElapsedTime(HandleVector<int>.Handle handle, float elapsed_time)
	{
		AnimEventManager.IndirectionData data = this.indirectionData.GetData(handle);
		KCompactedVector<AnimEventManager.EventPlayerData> kcompactedVector = (data.isUIData ? this.uiEventData : this.eventData);
		AnimEventManager.EventPlayerData data2 = kcompactedVector.GetData(data.eventDataHandle);
		data2.elapsedTime = elapsed_time;
		kcompactedVector.SetData(data.eventDataHandle, data2);
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
	public void Update()
	{
		float deltaTime = Time.deltaTime;
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		this.Update(deltaTime, this.animData.GetDataList(), this.eventData.GetDataList());
		this.Update(unscaledDeltaTime, this.uiAnimData.GetDataList(), this.uiEventData.GetDataList());
		for (int i = 0; i < this.finishedCalls.Count; i++)
		{
			this.finishedCalls[i].TriggerStop();
		}
		this.finishedCalls.Clear();
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x0006F77C File Offset: 0x0006D97C
	private void Update(float dt, List<AnimEventManager.AnimData> anim_data, List<AnimEventManager.EventPlayerData> event_data)
	{
		if (dt <= 0f)
		{
			return;
		}
		for (int i = 0; i < event_data.Count; i++)
		{
			AnimEventManager.EventPlayerData eventPlayerData = event_data[i];
			if (!(eventPlayerData.controller == null) && eventPlayerData.mode != KAnim.PlayMode.Paused)
			{
				eventPlayerData.currentFrame = eventPlayerData.controller.GetFrameIdx(eventPlayerData.elapsedTime, false);
				event_data[i] = eventPlayerData;
				this.PlayEvents(eventPlayerData);
				eventPlayerData.previousFrame = eventPlayerData.currentFrame;
				eventPlayerData.elapsedTime += dt * eventPlayerData.controller.GetPlaySpeed();
				event_data[i] = eventPlayerData;
				if (eventPlayerData.updatingEvents != null)
				{
					for (int j = 0; j < eventPlayerData.updatingEvents.Count; j++)
					{
						eventPlayerData.updatingEvents[j].OnUpdate(eventPlayerData);
					}
				}
				event_data[i] = eventPlayerData;
				if (eventPlayerData.mode != KAnim.PlayMode.Loop && eventPlayerData.currentFrame >= anim_data[i].numFrames - 1)
				{
					this.StopEvents(eventPlayerData);
					this.finishedCalls.Add(eventPlayerData.controller);
				}
			}
		}
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x0006F894 File Offset: 0x0006DA94
	private void PlayEvents(AnimEventManager.EventPlayerData data)
	{
		for (int i = 0; i < data.events.Count; i++)
		{
			data.events[i].Play(data);
		}
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x0006F8CC File Offset: 0x0006DACC
	private void StopEvents(AnimEventManager.EventPlayerData data)
	{
		for (int i = 0; i < data.events.Count; i++)
		{
			data.events[i].Stop(data);
		}
		if (data.updatingEvents != null)
		{
			data.updatingEvents.Clear();
		}
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x0006F914 File Offset: 0x0006DB14
	public AnimEventManager.DevTools_DebugInfo DevTools_GetDebugInfo()
	{
		return new AnimEventManager.DevTools_DebugInfo(this, this.animData, this.eventData, this.uiAnimData, this.uiEventData);
	}

	// Token: 0x04000BF9 RID: 3065
	private static readonly List<AnimEvent> emptyEventList = new List<AnimEvent>();

	// Token: 0x04000BFA RID: 3066
	private const int INITIAL_VECTOR_SIZE = 256;

	// Token: 0x04000BFB RID: 3067
	private KCompactedVector<AnimEventManager.AnimData> animData = new KCompactedVector<AnimEventManager.AnimData>(256);

	// Token: 0x04000BFC RID: 3068
	private KCompactedVector<AnimEventManager.EventPlayerData> eventData = new KCompactedVector<AnimEventManager.EventPlayerData>(256);

	// Token: 0x04000BFD RID: 3069
	private KCompactedVector<AnimEventManager.AnimData> uiAnimData = new KCompactedVector<AnimEventManager.AnimData>(256);

	// Token: 0x04000BFE RID: 3070
	private KCompactedVector<AnimEventManager.EventPlayerData> uiEventData = new KCompactedVector<AnimEventManager.EventPlayerData>(256);

	// Token: 0x04000BFF RID: 3071
	private KCompactedVector<AnimEventManager.IndirectionData> indirectionData = new KCompactedVector<AnimEventManager.IndirectionData>(0);

	// Token: 0x04000C00 RID: 3072
	private List<KBatchedAnimController> finishedCalls = new List<KBatchedAnimController>();

	// Token: 0x02001038 RID: 4152
	public struct AnimData
	{
		// Token: 0x040056AE RID: 22190
		public float frameRate;

		// Token: 0x040056AF RID: 22191
		public float totalTime;

		// Token: 0x040056B0 RID: 22192
		public int numFrames;

		// Token: 0x040056B1 RID: 22193
		public bool useUnscaledTime;
	}

	// Token: 0x02001039 RID: 4153
	[DebuggerDisplay("{controller.name}, Anim={currentAnim}, Frame={currentFrame}, Mode={mode}")]
	public struct EventPlayerData
	{
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600728B RID: 29323 RVA: 0x002ADF33 File Offset: 0x002AC133
		// (set) Token: 0x0600728C RID: 29324 RVA: 0x002ADF3B File Offset: 0x002AC13B
		public int currentFrame { readonly get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600728D RID: 29325 RVA: 0x002ADF44 File Offset: 0x002AC144
		// (set) Token: 0x0600728E RID: 29326 RVA: 0x002ADF4C File Offset: 0x002AC14C
		public int previousFrame { readonly get; set; }

		// Token: 0x0600728F RID: 29327 RVA: 0x002ADF55 File Offset: 0x002AC155
		public ComponentType GetComponent<ComponentType>()
		{
			return this.controller.GetComponent<ComponentType>();
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06007290 RID: 29328 RVA: 0x002ADF62 File Offset: 0x002AC162
		public string name
		{
			get
			{
				return this.controller.name;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x002ADF6F File Offset: 0x002AC16F
		public float normalizedTime
		{
			get
			{
				return this.elapsedTime / this.controller.CurrentAnim.totalTime;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06007292 RID: 29330 RVA: 0x002ADF88 File Offset: 0x002AC188
		public Vector3 position
		{
			get
			{
				return this.controller.transform.GetPosition();
			}
		}

		// Token: 0x06007293 RID: 29331 RVA: 0x002ADF9A File Offset: 0x002AC19A
		public void AddUpdatingEvent(AnimEvent ev)
		{
			if (this.updatingEvents == null)
			{
				this.updatingEvents = new List<AnimEvent>();
			}
			this.updatingEvents.Add(ev);
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x002ADFBB File Offset: 0x002AC1BB
		public void SetElapsedTime(float elapsedTime)
		{
			this.elapsedTime = elapsedTime;
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x002ADFC4 File Offset: 0x002AC1C4
		public void FreeResources()
		{
			this.elapsedTime = 0f;
			this.mode = KAnim.PlayMode.Once;
			this.currentFrame = 0;
			this.previousFrame = 0;
			this.events = null;
			this.updatingEvents = null;
			this.controller = null;
		}

		// Token: 0x040056B2 RID: 22194
		public float elapsedTime;

		// Token: 0x040056B3 RID: 22195
		public KAnim.PlayMode mode;

		// Token: 0x040056B6 RID: 22198
		public List<AnimEvent> events;

		// Token: 0x040056B7 RID: 22199
		public List<AnimEvent> updatingEvents;

		// Token: 0x040056B8 RID: 22200
		public KBatchedAnimController controller;
	}

	// Token: 0x0200103A RID: 4154
	private struct IndirectionData
	{
		// Token: 0x06007296 RID: 29334 RVA: 0x002ADFFB File Offset: 0x002AC1FB
		public IndirectionData(HandleVector<int>.Handle anim_data_handle, HandleVector<int>.Handle event_data_handle, bool is_ui_data)
		{
			this.isUIData = is_ui_data;
			this.animDataHandle = anim_data_handle;
			this.eventDataHandle = event_data_handle;
		}

		// Token: 0x040056B9 RID: 22201
		public bool isUIData;

		// Token: 0x040056BA RID: 22202
		public HandleVector<int>.Handle animDataHandle;

		// Token: 0x040056BB RID: 22203
		public HandleVector<int>.Handle eventDataHandle;
	}

	// Token: 0x0200103B RID: 4155
	public readonly struct DevTools_DebugInfo
	{
		// Token: 0x06007297 RID: 29335 RVA: 0x002AE012 File Offset: 0x002AC212
		public DevTools_DebugInfo(AnimEventManager eventManager, KCompactedVector<AnimEventManager.AnimData> animData, KCompactedVector<AnimEventManager.EventPlayerData> eventData, KCompactedVector<AnimEventManager.AnimData> uiAnimData, KCompactedVector<AnimEventManager.EventPlayerData> uiEventData)
		{
			this.eventManager = eventManager;
			this.animData = animData;
			this.eventData = eventData;
			this.uiAnimData = uiAnimData;
			this.uiEventData = uiEventData;
		}

		// Token: 0x040056BC RID: 22204
		public readonly AnimEventManager eventManager;

		// Token: 0x040056BD RID: 22205
		public readonly KCompactedVector<AnimEventManager.AnimData> animData;

		// Token: 0x040056BE RID: 22206
		public readonly KCompactedVector<AnimEventManager.EventPlayerData> eventData;

		// Token: 0x040056BF RID: 22207
		public readonly KCompactedVector<AnimEventManager.AnimData> uiAnimData;

		// Token: 0x040056C0 RID: 22208
		public readonly KCompactedVector<AnimEventManager.EventPlayerData> uiEventData;
	}
}
