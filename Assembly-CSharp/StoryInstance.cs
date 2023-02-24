using System;
using System.Collections.Generic;
using Database;
using KSerialization;

// Token: 0x02000995 RID: 2453
[SerializationConfig(MemberSerialization.OptIn)]
public class StoryInstance : ISaveLoadable
{
	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x0600488D RID: 18573 RVA: 0x00196D32 File Offset: 0x00194F32
	// (set) Token: 0x0600488E RID: 18574 RVA: 0x00196D3C File Offset: 0x00194F3C
	public StoryInstance.State CurrentState
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state == value)
			{
				return;
			}
			this.state = value;
			this.Telemetry.LogStateChange(this.state, GameClock.Instance.GetTimeInCycles());
			Action<StoryInstance.State> storyStateChanged = this.StoryStateChanged;
			if (storyStateChanged == null)
			{
				return;
			}
			storyStateChanged(this.state);
		}
	}

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x0600488F RID: 18575 RVA: 0x00196D8B File Offset: 0x00194F8B
	public StoryManager.StoryTelemetry Telemetry
	{
		get
		{
			if (this.telemetry == null)
			{
				this.telemetry = new StoryManager.StoryTelemetry();
			}
			return this.telemetry;
		}
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x06004890 RID: 18576 RVA: 0x00196DA6 File Offset: 0x00194FA6
	// (set) Token: 0x06004891 RID: 18577 RVA: 0x00196DAE File Offset: 0x00194FAE
	public EventInfoData EventInfo { get; private set; }

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x06004892 RID: 18578 RVA: 0x00196DB7 File Offset: 0x00194FB7
	// (set) Token: 0x06004893 RID: 18579 RVA: 0x00196DBF File Offset: 0x00194FBF
	public Notification Notification { get; private set; }

	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x06004894 RID: 18580 RVA: 0x00196DC8 File Offset: 0x00194FC8
	// (set) Token: 0x06004895 RID: 18581 RVA: 0x00196DD0 File Offset: 0x00194FD0
	public EventInfoDataHelper.PopupType PendingType { get; private set; } = EventInfoDataHelper.PopupType.NONE;

	// Token: 0x06004896 RID: 18582 RVA: 0x00196DD9 File Offset: 0x00194FD9
	public Story GetStory()
	{
		if (this._story == null)
		{
			this._story = Db.Get().Stories.Get(this.storyId);
		}
		return this._story;
	}

	// Token: 0x06004897 RID: 18583 RVA: 0x00196E04 File Offset: 0x00195004
	public StoryInstance()
	{
	}

	// Token: 0x06004898 RID: 18584 RVA: 0x00196E1E File Offset: 0x0019501E
	public StoryInstance(Story story, int worldId)
	{
		this._story = story;
		this.storyId = story.Id;
		this.worldId = worldId;
	}

	// Token: 0x06004899 RID: 18585 RVA: 0x00196E52 File Offset: 0x00195052
	public bool HasDisplayedPopup(EventInfoDataHelper.PopupType type)
	{
		return this.popupDisplayedStates != null && this.popupDisplayedStates.Contains(type);
	}

	// Token: 0x0600489A RID: 18586 RVA: 0x00196E6C File Offset: 0x0019506C
	public void SetPopupData(StoryManager.PopupInfo info, EventInfoData eventInfo, Notification notification = null)
	{
		this.EventInfo = eventInfo;
		this.Notification = notification;
		this.PendingType = info.PopupType;
		eventInfo.showCallback = (System.Action)Delegate.Combine(eventInfo.showCallback, new System.Action(this.OnPopupDisplayed));
		if (info.DisplayImmediate)
		{
			EventInfoScreen.ShowPopup(eventInfo);
		}
	}

	// Token: 0x0600489B RID: 18587 RVA: 0x00196EC4 File Offset: 0x001950C4
	private void OnPopupDisplayed()
	{
		if (this.popupDisplayedStates == null)
		{
			this.popupDisplayedStates = new HashSet<EventInfoDataHelper.PopupType>();
		}
		this.popupDisplayedStates.Add(this.PendingType);
		this.EventInfo = null;
		this.Notification = null;
		this.PendingType = EventInfoDataHelper.PopupType.NONE;
	}

	// Token: 0x04002FC0 RID: 12224
	public Action<StoryInstance.State> StoryStateChanged;

	// Token: 0x04002FC1 RID: 12225
	[Serialize]
	public readonly string storyId;

	// Token: 0x04002FC2 RID: 12226
	[Serialize]
	public int worldId;

	// Token: 0x04002FC3 RID: 12227
	[Serialize]
	private StoryInstance.State state;

	// Token: 0x04002FC4 RID: 12228
	[Serialize]
	private StoryManager.StoryTelemetry telemetry;

	// Token: 0x04002FC5 RID: 12229
	[Serialize]
	private HashSet<EventInfoDataHelper.PopupType> popupDisplayedStates = new HashSet<EventInfoDataHelper.PopupType>();

	// Token: 0x04002FC9 RID: 12233
	private Story _story;

	// Token: 0x0200178F RID: 6031
	public enum State
	{
		// Token: 0x04006D66 RID: 28006
		RETROFITTED = -1,
		// Token: 0x04006D67 RID: 28007
		NOT_STARTED,
		// Token: 0x04006D68 RID: 28008
		DISCOVERED,
		// Token: 0x04006D69 RID: 28009
		IN_PROGRESS,
		// Token: 0x04006D6A RID: 28010
		COMPLETE
	}
}
