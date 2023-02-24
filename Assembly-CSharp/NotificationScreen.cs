using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B40 RID: 2880
public class NotificationScreen : KScreen
{
	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x06005927 RID: 22823 RVA: 0x00204642 File Offset: 0x00202842
	// (set) Token: 0x06005928 RID: 22824 RVA: 0x00204649 File Offset: 0x00202849
	public static NotificationScreen Instance { get; private set; }

	// Token: 0x06005929 RID: 22825 RVA: 0x00204651 File Offset: 0x00202851
	public static void DestroyInstance()
	{
		NotificationScreen.Instance = null;
	}

	// Token: 0x0600592A RID: 22826 RVA: 0x0020465C File Offset: 0x0020285C
	private void OnAddNotifier(Notifier notifier)
	{
		notifier.OnAdd = (Action<Notification>)Delegate.Combine(notifier.OnAdd, new Action<Notification>(this.OnAddNotification));
		notifier.OnRemove = (Action<Notification>)Delegate.Combine(notifier.OnRemove, new Action<Notification>(this.OnRemoveNotification));
	}

	// Token: 0x0600592B RID: 22827 RVA: 0x002046B0 File Offset: 0x002028B0
	private void OnRemoveNotifier(Notifier notifier)
	{
		notifier.OnAdd = (Action<Notification>)Delegate.Remove(notifier.OnAdd, new Action<Notification>(this.OnAddNotification));
		notifier.OnRemove = (Action<Notification>)Delegate.Remove(notifier.OnRemove, new Action<Notification>(this.OnRemoveNotification));
	}

	// Token: 0x0600592C RID: 22828 RVA: 0x00204701 File Offset: 0x00202901
	private void OnAddNotification(Notification notification)
	{
		this.pendingNotifications.Add(notification);
	}

	// Token: 0x0600592D RID: 22829 RVA: 0x00204710 File Offset: 0x00202910
	private void OnRemoveNotification(Notification notification)
	{
		this.dirty = true;
		this.pendingNotifications.Remove(notification);
		NotificationScreen.Entry entry = null;
		this.entriesByMessage.TryGetValue(notification.titleText, out entry);
		if (entry == null)
		{
			return;
		}
		this.notifications.Remove(notification);
		entry.Remove(notification);
		if (entry.notifications.Count == 0)
		{
			UnityEngine.Object.Destroy(entry.label);
			this.entriesByMessage[notification.titleText] = null;
			this.entries.Remove(entry);
		}
	}

	// Token: 0x0600592E RID: 22830 RVA: 0x00204798 File Offset: 0x00202998
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NotificationScreen.Instance = this;
		Components.Notifiers.OnAdd += this.OnAddNotifier;
		Components.Notifiers.OnRemove += this.OnRemoveNotifier;
		foreach (Notifier notifier in Components.Notifiers.Items)
		{
			this.OnAddNotifier(notifier);
		}
		this.MessagesPrefab.gameObject.SetActive(false);
		this.LabelPrefab.gameObject.SetActive(false);
		this.InitNotificationSounds();
	}

	// Token: 0x0600592F RID: 22831 RVA: 0x00204850 File Offset: 0x00202A50
	private void OnNewMessage(object data)
	{
		Message message = (Message)data;
		this.notifier.Add(new MessageNotification(message), "");
	}

	// Token: 0x06005930 RID: 22832 RVA: 0x0020487C File Offset: 0x00202A7C
	private void ShowMessage(MessageNotification mn)
	{
		mn.message.OnClick();
		if (mn.message.ShowDialog())
		{
			for (int i = 0; i < this.dialogPrefabs.Count; i++)
			{
				if (this.dialogPrefabs[i].CanDisplay(mn.message))
				{
					if (this.messageDialog != null)
					{
						UnityEngine.Object.Destroy(this.messageDialog.gameObject);
						this.messageDialog = null;
					}
					this.messageDialog = global::Util.KInstantiateUI<MessageDialogFrame>(ScreenPrefabs.Instance.MessageDialogFrame.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false);
					MessageDialog messageDialog = global::Util.KInstantiateUI<MessageDialog>(this.dialogPrefabs[i].gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false);
					this.messageDialog.SetMessage(messageDialog, mn.message);
					this.messageDialog.Show(true);
					break;
				}
			}
		}
		Messenger.Instance.RemoveMessage(mn.message);
		mn.Clear();
	}

	// Token: 0x06005931 RID: 22833 RVA: 0x00204988 File Offset: 0x00202B88
	public void OnClickNextMessage()
	{
		Notification notification2 = this.notifications.Find((Notification notification) => notification.Type == NotificationType.Messages);
		this.ShowMessage((MessageNotification)notification2);
	}

	// Token: 0x06005932 RID: 22834 RVA: 0x002049CC File Offset: 0x00202BCC
	protected override void OnCleanUp()
	{
		Components.Notifiers.OnAdd -= this.OnAddNotifier;
		Components.Notifiers.OnRemove -= this.OnRemoveNotifier;
	}

	// Token: 0x06005933 RID: 22835 RVA: 0x002049FC File Offset: 0x00202BFC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.initTime = KTime.Instance.UnscaledGameTime;
		LocText[] array = this.LabelPrefab.GetComponentsInChildren<LocText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = this.normalColor;
		}
		array = this.MessagesPrefab.GetComponentsInChildren<LocText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = this.normalColor;
		}
		base.Subscribe(Messenger.Instance.gameObject, 1558809273, new Action<object>(this.OnNewMessage));
		foreach (Message message in Messenger.Instance.Messages)
		{
			Notification notification = new MessageNotification(message);
			notification.playSound = false;
			this.notifier.Add(notification, "");
		}
	}

	// Token: 0x06005934 RID: 22836 RVA: 0x00204AEC File Offset: 0x00202CEC
	protected override void OnActivate()
	{
		base.OnActivate();
		this.dirty = true;
	}

	// Token: 0x06005935 RID: 22837 RVA: 0x00204AFC File Offset: 0x00202CFC
	private void AddNotification(Notification notification)
	{
		if (DebugHandler.NotificationsDisabled)
		{
			return;
		}
		this.notifications.Add(notification);
		NotificationScreen.Entry entry;
		this.entriesByMessage.TryGetValue(notification.titleText, out entry);
		if (entry == null)
		{
			HierarchyReferences hierarchyReferences;
			if (notification.Type == NotificationType.Messages)
			{
				hierarchyReferences = global::Util.KInstantiateUI<HierarchyReferences>(this.MessagesPrefab, this.MessagesFolder, false);
			}
			else
			{
				hierarchyReferences = global::Util.KInstantiateUI<HierarchyReferences>(this.LabelPrefab, this.LabelsFolder, false);
			}
			Button reference = hierarchyReferences.GetReference<Button>("DismissButton");
			reference.gameObject.SetActive(notification.showDismissButton);
			if (notification.showDismissButton)
			{
				reference.onClick.AddListener(delegate
				{
					NotificationScreen.Entry entry2;
					if (!this.entriesByMessage.TryGetValue(notification.titleText, out entry2))
					{
						return;
					}
					for (int i = entry2.notifications.Count - 1; i >= 0; i--)
					{
						Notification notification2 = entry2.notifications[i];
						MessageNotification messageNotification2 = notification2 as MessageNotification;
						if (messageNotification2 != null)
						{
							Messenger.Instance.RemoveMessage(messageNotification2.message);
						}
						notification2.Clear();
					}
				});
			}
			hierarchyReferences.GetReference<NotificationAnimator>("Animator").Begin(true);
			hierarchyReferences.gameObject.SetActive(true);
			if (notification.ToolTip != null)
			{
				ToolTip tooltip = hierarchyReferences.GetReference<ToolTip>("ToolTip");
				tooltip.OnToolTip = delegate
				{
					tooltip.ClearMultiStringTooltip();
					tooltip.AddMultiStringTooltip(notification.ToolTip(entry.notifications, notification.tooltipData), this.TooltipTextStyle);
					return "";
				};
			}
			KImage reference2 = hierarchyReferences.GetReference<KImage>("Icon");
			LocText reference3 = hierarchyReferences.GetReference<LocText>("Text");
			Button reference4 = hierarchyReferences.GetReference<Button>("MainButton");
			ColorBlock colors = reference4.colors;
			switch (notification.Type)
			{
			case NotificationType.Bad:
			case NotificationType.DuplicantThreatening:
				colors.normalColor = this.badColorBG;
				reference3.color = this.badColor;
				reference2.color = this.badColor;
				reference2.sprite = ((notification.Type == NotificationType.Bad) ? this.icon_bad : this.icon_threatening);
				goto IL_300;
			case NotificationType.Tutorial:
				colors.normalColor = this.warningColorBG;
				reference3.color = this.warningColor;
				reference2.color = this.warningColor;
				reference2.sprite = this.icon_warning;
				goto IL_300;
			case NotificationType.Messages:
			{
				colors.normalColor = this.messageColorBG;
				reference3.color = this.messageColor;
				reference2.color = this.messageColor;
				reference2.sprite = this.icon_message;
				MessageNotification messageNotification = notification as MessageNotification;
				if (messageNotification == null)
				{
					goto IL_300;
				}
				TutorialMessage tutorialMessage = messageNotification.message as TutorialMessage;
				if (tutorialMessage != null && !string.IsNullOrEmpty(tutorialMessage.videoClipId))
				{
					reference2.sprite = this.icon_video;
					goto IL_300;
				}
				goto IL_300;
			}
			case NotificationType.Event:
				colors.normalColor = this.eventColorBG;
				reference3.color = this.eventColor;
				reference2.color = this.eventColor;
				reference2.sprite = this.icon_event;
				goto IL_300;
			}
			colors.normalColor = this.normalColorBG;
			reference3.color = this.normalColor;
			reference2.color = this.normalColor;
			reference2.sprite = this.icon_normal;
			IL_300:
			reference4.colors = colors;
			reference4.onClick.AddListener(delegate
			{
				this.OnClick(entry);
			});
			string text = "";
			if (KTime.Instance.UnscaledGameTime - this.initTime > 5f && notification.playSound)
			{
				this.PlayDingSound(notification, 0);
			}
			else
			{
				text = "too early";
			}
			if (AudioDebug.Get().debugNotificationSounds)
			{
				global::Debug.Log("Notification(" + notification.titleText + "):" + text);
			}
			entry = new NotificationScreen.Entry(hierarchyReferences.gameObject);
			this.entriesByMessage[notification.titleText] = entry;
			this.entries.Add(entry);
		}
		entry.Add(notification);
		this.dirty = true;
		this.SortNotifications();
	}

	// Token: 0x06005936 RID: 22838 RVA: 0x00204EF8 File Offset: 0x002030F8
	private void SortNotifications()
	{
		this.notifications.Sort(delegate(Notification n1, Notification n2)
		{
			if (n1.Type == n2.Type)
			{
				return n1.Idx - n2.Idx;
			}
			return n1.Type - n2.Type;
		});
		foreach (Notification notification in this.notifications)
		{
			NotificationScreen.Entry entry = null;
			this.entriesByMessage.TryGetValue(notification.titleText, out entry);
			if (entry != null)
			{
				entry.label.GetComponent<RectTransform>().SetAsLastSibling();
			}
		}
	}

	// Token: 0x06005937 RID: 22839 RVA: 0x00204F98 File Offset: 0x00203198
	private void PlayDingSound(Notification notification, int count)
	{
		string text;
		if (!this.notificationSounds.TryGetValue(notification.Type, out text))
		{
			text = "Notification";
		}
		float num;
		if (!this.timeOfLastNotification.TryGetValue(text, out num))
		{
			num = 0f;
		}
		float num2 = (notification.volume_attenuation ? ((Time.time - num) / this.soundDecayTime) : 1f);
		this.timeOfLastNotification[text] = Time.time;
		string text2;
		if (count > 1)
		{
			text2 = GlobalAssets.GetSound(text + "_AddCount", true);
			if (text2 == null)
			{
				text2 = GlobalAssets.GetSound(text, false);
			}
		}
		else
		{
			text2 = GlobalAssets.GetSound(text, false);
		}
		if (notification.playSound)
		{
			EventInstance eventInstance = KFMOD.BeginOneShot(text2, Vector3.zero, 1f);
			eventInstance.setParameterByName("timeSinceLast", num2, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06005938 RID: 22840 RVA: 0x00205064 File Offset: 0x00203264
	private void Update()
	{
		int i = 0;
		while (i < this.pendingNotifications.Count)
		{
			if (this.pendingNotifications[i].IsReady())
			{
				this.AddNotification(this.pendingNotifications[i]);
				this.pendingNotifications.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < this.notifications.Count; j++)
		{
			Notification notification = this.notifications[j];
			if (notification.Type == NotificationType.Messages)
			{
				num2++;
			}
			else
			{
				num++;
			}
			if (notification.expires && KTime.Instance.UnscaledGameTime - notification.Time > this.lifetime)
			{
				this.dirty = true;
				if (notification.Notifier == null)
				{
					this.OnRemoveNotification(notification);
				}
				else
				{
					notification.Clear();
				}
			}
		}
	}

	// Token: 0x06005939 RID: 22841 RVA: 0x00205140 File Offset: 0x00203340
	private void OnClick(NotificationScreen.Entry entry)
	{
		Notification nextClickedNotification = entry.NextClickedNotification;
		base.PlaySound3D(GlobalAssets.GetSound("HUD_Click_Open", false));
		if (nextClickedNotification.customClickCallback != null)
		{
			nextClickedNotification.customClickCallback(nextClickedNotification.customClickData);
		}
		else
		{
			if (nextClickedNotification.clickFocus != null)
			{
				Vector3 position = nextClickedNotification.clickFocus.GetPosition();
				position.z = -40f;
				ClusterGridEntity component = nextClickedNotification.clickFocus.GetComponent<ClusterGridEntity>();
				KSelectable component2 = nextClickedNotification.clickFocus.GetComponent<KSelectable>();
				int myWorldId = nextClickedNotification.clickFocus.gameObject.GetMyWorldId();
				if (myWorldId != -1)
				{
					CameraController.Instance.ActiveWorldStarWipe(myWorldId, position, 10f, null);
				}
				else if (DlcManager.FeatureClusterSpaceEnabled() && component != null && component.IsVisible)
				{
					ManagementMenu.Instance.OpenClusterMap();
					ClusterMapScreen.Instance.SetTargetFocusPosition(component.Location, 0.5f);
				}
				if (component2 != null)
				{
					if (DlcManager.FeatureClusterSpaceEnabled() && component != null && component.IsVisible)
					{
						ClusterMapSelectTool.Instance.Select(component2, false);
					}
					else
					{
						SelectTool.Instance.Select(component2, false);
					}
				}
			}
			else if (nextClickedNotification.Notifier != null)
			{
				SelectTool.Instance.Select(nextClickedNotification.Notifier.GetComponent<KSelectable>(), false);
			}
			if (nextClickedNotification.Type == NotificationType.Messages)
			{
				this.ShowMessage((MessageNotification)nextClickedNotification);
			}
		}
		if (nextClickedNotification.clearOnClick)
		{
			nextClickedNotification.Clear();
		}
	}

	// Token: 0x0600593A RID: 22842 RVA: 0x002052AB File Offset: 0x002034AB
	private void PositionLocatorIcon()
	{
	}

	// Token: 0x0600593B RID: 22843 RVA: 0x002052B0 File Offset: 0x002034B0
	private void InitNotificationSounds()
	{
		this.notificationSounds[NotificationType.Good] = "Notification";
		this.notificationSounds[NotificationType.BadMinor] = "Notification";
		this.notificationSounds[NotificationType.Bad] = "Warning";
		this.notificationSounds[NotificationType.Neutral] = "Notification";
		this.notificationSounds[NotificationType.Tutorial] = "Notification";
		this.notificationSounds[NotificationType.Messages] = "Message";
		this.notificationSounds[NotificationType.DuplicantThreatening] = "Warning_DupeThreatening";
		this.notificationSounds[NotificationType.Event] = "Message";
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x0600593C RID: 22844 RVA: 0x00205345 File Offset: 0x00203545
	public Color32 BadColorBG
	{
		get
		{
			return this.badColorBG;
		}
	}

	// Token: 0x0600593D RID: 22845 RVA: 0x00205354 File Offset: 0x00203554
	public Sprite GetNotificationIcon(NotificationType type)
	{
		switch (type)
		{
		case NotificationType.Bad:
			return this.icon_bad;
		case NotificationType.Tutorial:
			return this.icon_warning;
		case NotificationType.Messages:
			return this.icon_message;
		case NotificationType.DuplicantThreatening:
			return this.icon_threatening;
		case NotificationType.Event:
			return this.icon_event;
		}
		return this.icon_normal;
	}

	// Token: 0x0600593E RID: 22846 RVA: 0x002053B4 File Offset: 0x002035B4
	public Color GetNotificationColour(NotificationType type)
	{
		switch (type)
		{
		case NotificationType.Bad:
			return this.badColor;
		case NotificationType.Tutorial:
			return this.warningColor;
		case NotificationType.Messages:
			return this.messageColor;
		case NotificationType.DuplicantThreatening:
			return this.badColor;
		case NotificationType.Event:
			return this.eventColor;
		}
		return this.normalColor;
	}

	// Token: 0x0600593F RID: 22847 RVA: 0x00205414 File Offset: 0x00203614
	public Color GetNotificationBGColour(NotificationType type)
	{
		switch (type)
		{
		case NotificationType.Bad:
			return this.badColorBG;
		case NotificationType.Tutorial:
			return this.warningColorBG;
		case NotificationType.Messages:
			return this.messageColorBG;
		case NotificationType.DuplicantThreatening:
			return this.badColorBG;
		case NotificationType.Event:
			return this.eventColorBG;
		}
		return this.normalColorBG;
	}

	// Token: 0x06005940 RID: 22848 RVA: 0x00205474 File Offset: 0x00203674
	public string GetNotificationSound(NotificationType type)
	{
		return this.notificationSounds[type];
	}

	// Token: 0x04003C34 RID: 15412
	public float lifetime;

	// Token: 0x04003C35 RID: 15413
	public bool dirty;

	// Token: 0x04003C36 RID: 15414
	public GameObject LabelPrefab;

	// Token: 0x04003C37 RID: 15415
	public GameObject LabelsFolder;

	// Token: 0x04003C38 RID: 15416
	public GameObject MessagesPrefab;

	// Token: 0x04003C39 RID: 15417
	public GameObject MessagesFolder;

	// Token: 0x04003C3A RID: 15418
	private MessageDialogFrame messageDialog;

	// Token: 0x04003C3B RID: 15419
	private float initTime;

	// Token: 0x04003C3C RID: 15420
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04003C3D RID: 15421
	[SerializeField]
	private List<MessageDialog> dialogPrefabs = new List<MessageDialog>();

	// Token: 0x04003C3E RID: 15422
	[SerializeField]
	private Color badColorBG;

	// Token: 0x04003C3F RID: 15423
	[SerializeField]
	private Color badColor = Color.red;

	// Token: 0x04003C40 RID: 15424
	[SerializeField]
	private Color normalColorBG;

	// Token: 0x04003C41 RID: 15425
	[SerializeField]
	private Color normalColor = Color.white;

	// Token: 0x04003C42 RID: 15426
	[SerializeField]
	private Color warningColorBG;

	// Token: 0x04003C43 RID: 15427
	[SerializeField]
	private Color warningColor;

	// Token: 0x04003C44 RID: 15428
	[SerializeField]
	private Color messageColorBG;

	// Token: 0x04003C45 RID: 15429
	[SerializeField]
	private Color messageColor;

	// Token: 0x04003C46 RID: 15430
	[SerializeField]
	private Color eventColorBG;

	// Token: 0x04003C47 RID: 15431
	[SerializeField]
	private Color eventColor;

	// Token: 0x04003C48 RID: 15432
	public Sprite icon_normal;

	// Token: 0x04003C49 RID: 15433
	public Sprite icon_warning;

	// Token: 0x04003C4A RID: 15434
	public Sprite icon_bad;

	// Token: 0x04003C4B RID: 15435
	public Sprite icon_threatening;

	// Token: 0x04003C4C RID: 15436
	public Sprite icon_message;

	// Token: 0x04003C4D RID: 15437
	public Sprite icon_video;

	// Token: 0x04003C4E RID: 15438
	public Sprite icon_event;

	// Token: 0x04003C4F RID: 15439
	private List<Notification> pendingNotifications = new List<Notification>();

	// Token: 0x04003C50 RID: 15440
	private List<Notification> notifications = new List<Notification>();

	// Token: 0x04003C51 RID: 15441
	public TextStyleSetting TooltipTextStyle;

	// Token: 0x04003C52 RID: 15442
	private Dictionary<NotificationType, string> notificationSounds = new Dictionary<NotificationType, string>();

	// Token: 0x04003C53 RID: 15443
	private Dictionary<string, float> timeOfLastNotification = new Dictionary<string, float>();

	// Token: 0x04003C54 RID: 15444
	private float soundDecayTime = 10f;

	// Token: 0x04003C55 RID: 15445
	private List<NotificationScreen.Entry> entries = new List<NotificationScreen.Entry>();

	// Token: 0x04003C56 RID: 15446
	private Dictionary<string, NotificationScreen.Entry> entriesByMessage = new Dictionary<string, NotificationScreen.Entry>();

	// Token: 0x020019D9 RID: 6617
	private class Entry
	{
		// Token: 0x0600917C RID: 37244 RVA: 0x00314A13 File Offset: 0x00312C13
		public Entry(GameObject label)
		{
			this.label = label;
		}

		// Token: 0x0600917D RID: 37245 RVA: 0x00314A2D File Offset: 0x00312C2D
		public void Add(Notification notification)
		{
			this.notifications.Add(notification);
			this.UpdateMessage(notification, true);
		}

		// Token: 0x0600917E RID: 37246 RVA: 0x00314A43 File Offset: 0x00312C43
		public void Remove(Notification notification)
		{
			this.notifications.Remove(notification);
			this.UpdateMessage(notification, false);
		}

		// Token: 0x0600917F RID: 37247 RVA: 0x00314A5C File Offset: 0x00312C5C
		public void UpdateMessage(Notification notification, bool playSound = true)
		{
			if (Game.IsQuitting())
			{
				return;
			}
			this.message = notification.titleText;
			if (this.notifications.Count > 1)
			{
				if (playSound && (notification.Type == NotificationType.Bad || notification.Type == NotificationType.DuplicantThreatening))
				{
					NotificationScreen.Instance.PlayDingSound(notification, this.notifications.Count);
				}
				this.message = this.message + " (" + this.notifications.Count.ToString() + ")";
			}
			if (this.label != null)
			{
				this.label.GetComponent<HierarchyReferences>().GetReference<LocText>("Text").text = this.message;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06009180 RID: 37248 RVA: 0x00314B14 File Offset: 0x00312D14
		public Notification NextClickedNotification
		{
			get
			{
				List<Notification> list = this.notifications;
				int num = this.clickIdx;
				this.clickIdx = num + 1;
				return list[num % this.notifications.Count];
			}
		}

		// Token: 0x04007589 RID: 30089
		public string message;

		// Token: 0x0400758A RID: 30090
		public int clickIdx;

		// Token: 0x0400758B RID: 30091
		public GameObject label;

		// Token: 0x0400758C RID: 30092
		public List<Notification> notifications = new List<Notification>();
	}
}
