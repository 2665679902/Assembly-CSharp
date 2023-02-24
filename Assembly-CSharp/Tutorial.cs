using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020004FF RID: 1279
[AddComponentMenu("KMonoBehaviour/scripts/Tutorial")]
public class Tutorial : KMonoBehaviour, IRender1000ms
{
	// Token: 0x06001E10 RID: 7696 RVA: 0x000A0824 File Offset: 0x0009EA24
	public static void ResetHiddenTutorialMessages()
	{
		if (Tutorial.Instance != null)
		{
			Tutorial.Instance.tutorialMessagesRemaining.Clear();
		}
		foreach (object obj in Enum.GetValues(typeof(Tutorial.TutorialMessages)))
		{
			Tutorial.TutorialMessages tutorialMessages = (Tutorial.TutorialMessages)obj;
			KPlayerPrefs.SetInt("HideTutorial_" + tutorialMessages.ToString(), 0);
			if (Tutorial.Instance != null)
			{
				Tutorial.Instance.tutorialMessagesRemaining.Add(tutorialMessages);
				Tutorial.Instance.hiddenTutorialMessages[tutorialMessages] = false;
			}
		}
		KPlayerPrefs.SetInt("HideTutorial_CheckState", 0);
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x000A08F4 File Offset: 0x0009EAF4
	private void LoadHiddenTutorialMessages()
	{
		foreach (object obj in Enum.GetValues(typeof(Tutorial.TutorialMessages)))
		{
			Tutorial.TutorialMessages tutorialMessages = (Tutorial.TutorialMessages)obj;
			bool flag = KPlayerPrefs.GetInt("HideTutorial_" + tutorialMessages.ToString(), 0) != 0;
			this.hiddenTutorialMessages[tutorialMessages] = flag;
		}
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x000A0980 File Offset: 0x0009EB80
	public void HideTutorialMessage(Tutorial.TutorialMessages message)
	{
		this.hiddenTutorialMessages[message] = true;
		KPlayerPrefs.SetInt("HideTutorial_" + message.ToString(), 1);
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06001E13 RID: 7699 RVA: 0x000A09AC File Offset: 0x0009EBAC
	// (set) Token: 0x06001E14 RID: 7700 RVA: 0x000A09B3 File Offset: 0x0009EBB3
	public static Tutorial Instance { get; private set; }

	// Token: 0x06001E15 RID: 7701 RVA: 0x000A09BB File Offset: 0x0009EBBB
	public static void DestroyInstance()
	{
		Tutorial.Instance = null;
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x000A09C4 File Offset: 0x0009EBC4
	private void UpdateNotifierPosition()
	{
		if (this.notifierPosition == Vector3.zero)
		{
			GameObject activeTelepad = GameUtil.GetActiveTelepad();
			if (activeTelepad != null)
			{
				this.notifierPosition = activeTelepad.transform.GetPosition();
			}
		}
		this.notifier.transform.SetPosition(this.notifierPosition);
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x000A0A1A File Offset: 0x0009EC1A
	protected override void OnPrefabInit()
	{
		Tutorial.Instance = this;
		this.LoadHiddenTutorialMessages();
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x000A0A28 File Offset: 0x0009EC28
	protected override void OnSpawn()
	{
		if (this.tutorialMessagesRemaining.Count == 0)
		{
			for (int i = 0; i <= 20; i++)
			{
				this.tutorialMessagesRemaining.Add((Tutorial.TutorialMessages)i);
			}
		}
		List<Tutorial.Item> list = new List<Tutorial.Item>();
		List<Tutorial.Item> list2 = list;
		Tutorial.Item item = new Tutorial.Item();
		item.notification = new Notification(MISC.NOTIFICATIONS.NEEDTOILET.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NEEDTOILET.TOOLTIP.text, null, true, 5f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Plumbing");
		}, null, null, true, false, false);
		item.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.ToiletExists);
		list2.Add(item);
		this.itemTree.Add(list);
		List<Tutorial.Item> list3 = new List<Tutorial.Item>();
		List<Tutorial.Item> list4 = list3;
		Tutorial.Item item2 = new Tutorial.Item();
		item2.notification = new Notification(MISC.NOTIFICATIONS.NEEDFOOD.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NEEDFOOD.TOOLTIP.text, null, true, 20f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Food");
		}, null, null, true, false, false);
		item2.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.FoodSourceExistsOnStartingWorld);
		list4.Add(item2);
		List<Tutorial.Item> list5 = list3;
		Tutorial.Item item3 = new Tutorial.Item();
		item3.notification = new Notification(MISC.NOTIFICATIONS.THERMALCOMFORT.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.THERMALCOMFORT.TOOLTIP.text, null, true, 0f, null, null, null, true, false, false);
		list5.Add(item3);
		this.itemTree.Add(list3);
		List<Tutorial.Item> list6 = new List<Tutorial.Item>();
		List<Tutorial.Item> list7 = list6;
		Tutorial.Item item4 = new Tutorial.Item();
		item4.notification = new Notification(MISC.NOTIFICATIONS.HYGENE_NEEDED.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.HYGENE_NEEDED.TOOLTIP, null, true, 20f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Medicine");
		}, null, null, true, false, false);
		item4.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.HygeneExists);
		list7.Add(item4);
		this.itemTree.Add(list6);
		List<Tutorial.Item> list8 = this.warningItems;
		Tutorial.Item item5 = new Tutorial.Item();
		item5.notification = new Notification(MISC.NOTIFICATIONS.NO_OXYGEN_GENERATOR.NAME, NotificationType.Tutorial, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NO_OXYGEN_GENERATOR.TOOLTIP, null, false, 0f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Oxygen");
		}, null, null, true, false, false);
		item5.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.OxygenGeneratorBuilt);
		item5.minTimeToNotify = 80f;
		item5.lastNotifyTime = 0f;
		list8.Add(item5);
		this.warningItems.Add(new Tutorial.Item
		{
			notification = new Notification(MISC.NOTIFICATIONS.INSUFFICIENTOXYGENLASTCYCLE.NAME, NotificationType.Tutorial, new Func<List<Notification>, object, string>(this.OnOxygenTooltip), null, false, 0f, delegate(object d)
			{
				this.ZoomToNextOxygenGenerator();
			}, null, null, true, false, false),
			hideCondition = new Tutorial.HideConditionDelegate(this.OxygenGeneratorNotBuilt),
			requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.SufficientOxygenLastCycleAndThisCycle),
			minTimeToNotify = 80f,
			lastNotifyTime = 0f
		});
		this.warningItems.Add(new Tutorial.Item
		{
			notification = new Notification(MISC.NOTIFICATIONS.UNREFRIGERATEDFOOD.NAME, NotificationType.Tutorial, new Func<List<Notification>, object, string>(this.UnrefrigeratedFoodTooltip), null, false, 0f, delegate(object d)
			{
				this.ZoomToNextUnrefrigeratedFood();
			}, null, null, true, false, false),
			requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.FoodIsRefrigerated),
			minTimeToNotify = 6f,
			lastNotifyTime = 0f
		});
		List<Tutorial.Item> list9 = this.warningItems;
		Tutorial.Item item6 = new Tutorial.Item();
		item6.notification = new Notification(MISC.NOTIFICATIONS.NO_MEDICAL_COTS.NAME, NotificationType.Bad, (List<Notification> n, object o) => MISC.NOTIFICATIONS.NO_MEDICAL_COTS.TOOLTIP, null, false, 0f, delegate(object d)
		{
			PlanScreen.Instance.OpenCategoryByName("Medicine");
		}, null, null, true, false, false);
		item6.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.CanTreatSickDuplicant);
		item6.minTimeToNotify = 10f;
		item6.lastNotifyTime = 0f;
		list9.Add(item6);
		List<Tutorial.Item> list10 = this.warningItems;
		Tutorial.Item item7 = new Tutorial.Item();
		item7.notification = new Notification(string.Format(UI.ENDOFDAYREPORT.TRAVELTIMEWARNING.WARNING_TITLE, Array.Empty<object>()), NotificationType.BadMinor, (List<Notification> n, object d) => string.Format(UI.ENDOFDAYREPORT.TRAVELTIMEWARNING.WARNING_MESSAGE, GameUtil.GetFormattedPercent(40f, GameUtil.TimeSlice.None)), null, true, 0f, delegate(object d)
		{
			ManagementMenu.Instance.OpenReports(GameClock.Instance.GetCycle());
		}, null, null, true, false, false);
		item7.requirementSatisfied = new Tutorial.RequirementSatisfiedDelegate(this.LongTravelTimes);
		item7.minTimeToNotify = 1f;
		item7.lastNotifyTime = 0f;
		list10.Add(item7);
		DiscoveredResources.Instance.OnDiscover += this.OnDiscover;
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x000A0F33 File Offset: 0x0009F133
	protected override void OnCleanUp()
	{
		DiscoveredResources.Instance.OnDiscover -= this.OnDiscover;
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x000A0F4C File Offset: 0x0009F14C
	private void OnDiscover(Tag category_tag, Tag tag)
	{
		Element element = ElementLoader.FindElementByHash(SimHashes.UraniumOre);
		if (element != null && tag == element.tag)
		{
			this.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
		}
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x000A0F80 File Offset: 0x0009F180
	public Message TutorialMessage(Tutorial.TutorialMessages tm, bool queueMessage = true)
	{
		bool flag = false;
		Message message = null;
		switch (tm)
		{
		case Tutorial.TutorialMessages.TM_Basics:
			if (DistributionPlatform.Initialized && KInputManager.currentControllerIsGamepad)
			{
				message = new TutorialMessage(Tutorial.TutorialMessages.TM_Basics, MISC.NOTIFICATIONS.BASICCONTROLS.NAME, MISC.NOTIFICATIONS.BASICCONTROLS.MESSAGEBODYALT, MISC.NOTIFICATIONS.BASICCONTROLS.TOOLTIP, null, null, null, "", null);
			}
			else
			{
				message = new TutorialMessage(Tutorial.TutorialMessages.TM_Basics, MISC.NOTIFICATIONS.BASICCONTROLS.NAME, MISC.NOTIFICATIONS.BASICCONTROLS.MESSAGEBODY, MISC.NOTIFICATIONS.BASICCONTROLS.TOOLTIP, null, null, null, "", null);
			}
			break;
		case Tutorial.TutorialMessages.TM_Welcome:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Welcome, MISC.NOTIFICATIONS.WELCOMEMESSAGE.NAME, MISC.NOTIFICATIONS.WELCOMEMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.WELCOMEMESSAGE.TOOLTIP, null, null, null, "", null);
			break;
		case Tutorial.TutorialMessages.TM_StressManagement:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_StressManagement, MISC.NOTIFICATIONS.STRESSMANAGEMENTMESSAGE.NAME, MISC.NOTIFICATIONS.STRESSMANAGEMENTMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.STRESSMANAGEMENTMESSAGE.TOOLTIP, null, null, null, "hud_stress", null);
			break;
		case Tutorial.TutorialMessages.TM_Scheduling:
			flag = true;
			break;
		case Tutorial.TutorialMessages.TM_Mopping:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Mopping, MISC.NOTIFICATIONS.MOPPINGMESSAGE.NAME, MISC.NOTIFICATIONS.MOPPINGMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.MOPPINGMESSAGE.TOOLTIP, null, null, null, "icon_action_mop", null);
			break;
		case Tutorial.TutorialMessages.TM_Locomotion:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Locomotion, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.NAME, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.TOOLTIP, "tutorials\\Locomotion", "Tute_Locomotion", VIDEOS.LOCOMOTION, "action_navigable_regions", null);
			break;
		case Tutorial.TutorialMessages.TM_Priorities:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Priorities, MISC.NOTIFICATIONS.PRIORITIESMESSAGE.NAME, MISC.NOTIFICATIONS.PRIORITIESMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.PRIORITIESMESSAGE.TOOLTIP, null, null, null, "icon_action_prioritize", null);
			break;
		case Tutorial.TutorialMessages.TM_FetchingWater:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_FetchingWater, MISC.NOTIFICATIONS.FETCHINGWATERMESSAGE.NAME, MISC.NOTIFICATIONS.FETCHINGWATERMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.FETCHINGWATERMESSAGE.TOOLTIP, null, null, null, "element_liquid", null);
			break;
		case Tutorial.TutorialMessages.TM_ThermalComfort:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_ThermalComfort, MISC.NOTIFICATIONS.THERMALCOMFORT.NAME, MISC.NOTIFICATIONS.THERMALCOMFORT.MESSAGEBODY, MISC.NOTIFICATIONS.THERMALCOMFORT.TOOLTIP, null, null, null, "temperature", null);
			break;
		case Tutorial.TutorialMessages.TM_OverheatingBuildings:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_OverheatingBuildings, MISC.NOTIFICATIONS.TUTORIAL_OVERHEATING.NAME, MISC.NOTIFICATIONS.TUTORIAL_OVERHEATING.MESSAGEBODY, MISC.NOTIFICATIONS.TUTORIAL_OVERHEATING.TOOLTIP, null, null, null, "temperature", null);
			break;
		case Tutorial.TutorialMessages.TM_LotsOfGerms:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, MISC.NOTIFICATIONS.LOTS_OF_GERMS.NAME, MISC.NOTIFICATIONS.LOTS_OF_GERMS.MESSAGEBODY, MISC.NOTIFICATIONS.LOTS_OF_GERMS.TOOLTIP, null, null, null, "overlay_disease", null);
			break;
		case Tutorial.TutorialMessages.TM_DiseaseCooking:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_DiseaseCooking, MISC.NOTIFICATIONS.DISEASE_COOKING.NAME, MISC.NOTIFICATIONS.DISEASE_COOKING.MESSAGEBODY, MISC.NOTIFICATIONS.DISEASE_COOKING.TOOLTIP, null, null, null, "icon_category_food", null);
			break;
		case Tutorial.TutorialMessages.TM_Suits:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Suits, MISC.NOTIFICATIONS.SUITS.NAME, MISC.NOTIFICATIONS.SUITS.MESSAGEBODY, MISC.NOTIFICATIONS.SUITS.TOOLTIP, null, null, null, "overlay_suit", null);
			break;
		case Tutorial.TutorialMessages.TM_Morale:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Morale, MISC.NOTIFICATIONS.MORALE.NAME, MISC.NOTIFICATIONS.MORALE.MESSAGEBODY, MISC.NOTIFICATIONS.MORALE.TOOLTIP, "tutorials\\Morale", "Tute_Morale", VIDEOS.MORALE, "icon_category_morale", null);
			break;
		case Tutorial.TutorialMessages.TM_Schedule:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, MISC.NOTIFICATIONS.SCHEDULEMESSAGE.NAME, MISC.NOTIFICATIONS.SCHEDULEMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.SCHEDULEMESSAGE.TOOLTIP, null, null, null, "OverviewUI_schedule2_icon", null);
			break;
		case Tutorial.TutorialMessages.TM_Digging:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Digging, MISC.NOTIFICATIONS.DIGGING.NAME, MISC.NOTIFICATIONS.DIGGING.MESSAGEBODY, MISC.NOTIFICATIONS.DIGGING.TOOLTIP, "tutorials\\Digging", "Tute_Digging", VIDEOS.DIGGING, "icon_action_dig", null);
			break;
		case Tutorial.TutorialMessages.TM_Power:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Power, MISC.NOTIFICATIONS.POWER.NAME, MISC.NOTIFICATIONS.POWER.MESSAGEBODY, MISC.NOTIFICATIONS.POWER.TOOLTIP, "tutorials\\Power", "Tute_Power", VIDEOS.POWER, "overlay_power", null);
			break;
		case Tutorial.TutorialMessages.TM_Insulation:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, MISC.NOTIFICATIONS.INSULATION.NAME, MISC.NOTIFICATIONS.INSULATION.MESSAGEBODY, MISC.NOTIFICATIONS.INSULATION.TOOLTIP, "tutorials\\Insulation", "Tute_Insulation", VIDEOS.INSULATION, "icon_thermal_conductivity", null);
			break;
		case Tutorial.TutorialMessages.TM_Plumbing:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Plumbing, MISC.NOTIFICATIONS.PLUMBING.NAME, MISC.NOTIFICATIONS.PLUMBING.MESSAGEBODY, MISC.NOTIFICATIONS.PLUMBING.TOOLTIP, "tutorials\\Piping", "Tute_Plumbing", VIDEOS.PLUMBING, "icon_category_plumbing", null);
			break;
		case Tutorial.TutorialMessages.TM_Radiation:
			message = new TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, MISC.NOTIFICATIONS.RADIATION.NAME, MISC.NOTIFICATIONS.RADIATION.MESSAGEBODY, MISC.NOTIFICATIONS.RADIATION.TOOLTIP, null, null, null, "icon_category_radiation", DlcManager.AVAILABLE_EXPANSION1_ONLY);
			break;
		}
		DebugUtil.AssertArgs(message != null || flag, new object[] { "No tutorial message:", tm });
		if (queueMessage)
		{
			DebugUtil.AssertArgs(!flag, new object[] { "Attempted to queue deprecated Tutorial Message", tm });
			if (!this.tutorialMessagesRemaining.Contains(tm))
			{
				return null;
			}
			if (this.hiddenTutorialMessages.ContainsKey(tm) && this.hiddenTutorialMessages[tm])
			{
				return null;
			}
			this.tutorialMessagesRemaining.Remove(tm);
			Messenger.Instance.QueueMessage(message);
		}
		return message;
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x000A14F4 File Offset: 0x0009F6F4
	private string OnOxygenTooltip(List<Notification> notifications, object data)
	{
		ReportManager.ReportEntry entry = ReportManager.Instance.YesterdaysReport.GetEntry(ReportManager.ReportType.OxygenCreated);
		return MISC.NOTIFICATIONS.INSUFFICIENTOXYGENLASTCYCLE.TOOLTIP.Replace("{EmittingRate}", GameUtil.GetFormattedMass(entry.Positive, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")).Replace("{ConsumptionRate}", GameUtil.GetFormattedMass(Mathf.Abs(entry.Negative), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x000A155C File Offset: 0x0009F75C
	private string UnrefrigeratedFoodTooltip(List<Notification> notifications, object data)
	{
		string text = MISC.NOTIFICATIONS.UNREFRIGERATEDFOOD.TOOLTIP;
		ListPool<Pickupable, Tutorial>.PooledList pooledList = ListPool<Pickupable, Tutorial>.Allocate();
		this.GetUnrefrigeratedFood(pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			text = text + "\n" + pooledList[i].GetProperName();
		}
		pooledList.Recycle();
		return text;
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x000A15B4 File Offset: 0x0009F7B4
	private string OnLowFoodTooltip(List<Notification> notifications, object data)
	{
		global::Debug.Assert(((WorldContainer)data).id == ClusterManager.Instance.activeWorldId);
		float num = RationTracker.Get().CountRations(null, ((WorldContainer)data).worldInventory, true);
		float num2 = (float)Components.LiveMinionIdentities.GetWorldItems(((WorldContainer)data).id, false).Count * -1000000f;
		return string.Format(MISC.NOTIFICATIONS.FOODLOW.TOOLTIP, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories(Mathf.Abs(num2), GameUtil.TimeSlice.None, true));
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x000A163C File Offset: 0x0009F83C
	public void DebugNotification()
	{
		NotificationType notificationType;
		string text;
		if (this.debugMessageCount % 3 == 0)
		{
			notificationType = NotificationType.Tutorial;
			text = "Warning message e.g. \"not enough oxygen\" uses Warning Color";
		}
		else if (this.debugMessageCount % 3 == 1)
		{
			notificationType = NotificationType.BadMinor;
			text = "Normal message e.g. Idle. Uses Normal Color BG";
		}
		else
		{
			notificationType = NotificationType.Bad;
			text = "Urgent important message. Uses Bad Color BG";
		}
		string text2 = "{0} ({1})";
		object obj = text;
		int num = this.debugMessageCount;
		this.debugMessageCount = num + 1;
		Notification notification = new Notification(string.Format(text2, obj, num.ToString()), notificationType, (List<Notification> n, object d) => MISC.NOTIFICATIONS.NEEDTOILET.TOOLTIP.text, null, true, 0f, null, null, null, true, false, false);
		this.notifier.Add(notification, "");
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x000A16E8 File Offset: 0x0009F8E8
	public void DebugNotificationMessage()
	{
		string text = "This is a message notification. ";
		int num = this.debugMessageCount;
		this.debugMessageCount = num + 1;
		Message message = new GenericMessage(text + num.ToString(), MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.MESSAGEBODY, MISC.NOTIFICATIONS.LOCOMOTIONMESSAGE.TOOLTIP, null);
		Messenger.Instance.QueueMessage(message);
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x000A173C File Offset: 0x0009F93C
	public void Render1000ms(float dt)
	{
		if (App.isLoading)
		{
			return;
		}
		if (Components.LiveMinionIdentities.Count == 0)
		{
			return;
		}
		if (this.itemTree.Count > 0)
		{
			List<Tutorial.Item> list = this.itemTree[0];
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Tutorial.Item item = list[i];
				if (item != null)
				{
					if (item.requirementSatisfied == null || item.requirementSatisfied())
					{
						item.notification.Clear();
						list.RemoveAt(i);
					}
					else if (item.hideCondition != null && item.hideCondition())
					{
						item.notification.Clear();
						list.RemoveAt(i);
					}
					else
					{
						this.UpdateNotifierPosition();
						this.notifier.Add(item.notification, "");
					}
				}
			}
			if (list.Count == 0)
			{
				this.itemTree.RemoveAt(0);
			}
		}
		foreach (Tutorial.Item item2 in this.warningItems)
		{
			if (item2.requirementSatisfied())
			{
				item2.notification.Clear();
				item2.lastNotifyTime = Time.time;
			}
			else if (item2.hideCondition != null && item2.hideCondition())
			{
				item2.notification.Clear();
				item2.lastNotifyTime = Time.time;
			}
			else if (item2.lastNotifyTime == 0f || Time.time - item2.lastNotifyTime > item2.minTimeToNotify)
			{
				this.notifier.Add(item2.notification, "");
				item2.lastNotifyTime = Time.time;
			}
		}
		if (GameClock.Instance.GetCycle() > 0 && !this.tutorialMessagesRemaining.Contains(Tutorial.TutorialMessages.TM_Priorities) && !this.queuedPrioritiesMessage)
		{
			this.queuedPrioritiesMessage = true;
			GameScheduler.Instance.Schedule("PrioritiesTutorial", 2f, delegate(object obj)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Priorities, true);
			}, null, null);
		}
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x000A1968 File Offset: 0x0009FB68
	private bool OxygenGeneratorBuilt()
	{
		return this.oxygenGenerators.Count > 0;
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x000A1978 File Offset: 0x0009FB78
	private bool OxygenGeneratorNotBuilt()
	{
		return this.oxygenGenerators.Count == 0;
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x000A1988 File Offset: 0x0009FB88
	private bool SufficientOxygenLastCycleAndThisCycle()
	{
		if (ReportManager.Instance.YesterdaysReport == null)
		{
			return true;
		}
		ReportManager.ReportEntry entry = ReportManager.Instance.YesterdaysReport.GetEntry(ReportManager.ReportType.OxygenCreated);
		return ReportManager.Instance.TodaysReport.GetEntry(ReportManager.ReportType.OxygenCreated).Net > 0.0001f || entry.Net > 0.0001f || (GameClock.Instance.GetCycle() < 1 && !GameClock.Instance.IsNighttime());
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x000A19FD File Offset: 0x0009FBFD
	private bool FoodIsRefrigerated()
	{
		return this.GetUnrefrigeratedFood(null) <= 0;
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x000A1A0C File Offset: 0x0009FC0C
	private int GetUnrefrigeratedFood(List<Pickupable> foods)
	{
		int num = 0;
		if (ClusterManager.Instance.activeWorld.worldInventory != null)
		{
			ICollection<Pickupable> pickupables = ClusterManager.Instance.activeWorld.worldInventory.GetPickupables(GameTags.Edible, false);
			if (pickupables == null)
			{
				return 0;
			}
			foreach (Pickupable pickupable in pickupables)
			{
				if (pickupable.storage != null && (pickupable.storage.GetComponent<RationBox>() != null || pickupable.storage.GetComponent<Refrigerator>() != null))
				{
					Rottable.Instance smi = pickupable.GetSMI<Rottable.Instance>();
					if (smi != null && Rottable.RefrigerationLevel(smi) == Rottable.RotRefrigerationLevel.Normal && Rottable.AtmosphereQuality(smi) != Rottable.RotAtmosphereQuality.Sterilizing && smi != null && smi.RotConstitutionPercentage < 0.8f)
					{
						num++;
						if (foods != null)
						{
							foods.Add(pickupable);
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x000A1B00 File Offset: 0x0009FD00
	private bool EnergySourceExists()
	{
		return Game.Instance.circuitManager.HasGenerators();
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x000A1B11 File Offset: 0x0009FD11
	private bool BedExists()
	{
		return Components.Sleepables.Count > 0;
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x000A1B20 File Offset: 0x0009FD20
	private bool EnoughFood()
	{
		int count = Components.LiveMinionIdentities.GetWorldItems(ClusterManager.Instance.activeWorldId, false).Count;
		float num = RationTracker.Get().CountRations(null, ClusterManager.Instance.activeWorld.worldInventory, true);
		float num2 = (float)count * 1000000f;
		return num / num2 >= 1f;
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x000A1B78 File Offset: 0x0009FD78
	private bool CanTreatSickDuplicant()
	{
		bool flag = Components.Clinics.Count >= 1;
		bool flag2 = false;
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			using (IEnumerator<SicknessInstance> enumerator = Components.LiveMinionIdentities[i].GetSicknesses().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Sickness.severity >= Sickness.Severity.Major)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				break;
			}
		}
		return !flag2 || flag;
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x000A1C0C File Offset: 0x0009FE0C
	private bool LongTravelTimes()
	{
		if (ReportManager.Instance.reports.Count < 3)
		{
			return true;
		}
		float num = 0f;
		float num2 = 0f;
		for (int i = ReportManager.Instance.reports.Count - 1; i >= ReportManager.Instance.reports.Count - 3; i--)
		{
			ReportManager.ReportEntry entry = ReportManager.Instance.reports[i].GetEntry(ReportManager.ReportType.TravelTime);
			num += entry.Net;
			num2 += 600f * (float)entry.contextEntries.Count;
		}
		return num / num2 <= 0.4f;
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000A1CA8 File Offset: 0x0009FEA8
	private bool FoodSourceExistsOnStartingWorld()
	{
		using (List<ComplexFabricator>.Enumerator enumerator = Components.ComplexFabricators.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetType() == typeof(MicrobeMusher))
				{
					return true;
				}
			}
		}
		return Components.PlantablePlots.GetItems(ClusterManager.Instance.GetStartWorld().id).Count > 0;
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000A1D34 File Offset: 0x0009FF34
	private bool HygeneExists()
	{
		return Components.HandSanitizers.Count > 0;
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x000A1D43 File Offset: 0x0009FF43
	private bool ToiletExists()
	{
		return Components.Toilets.Count > 0;
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x000A1D54 File Offset: 0x0009FF54
	private void ZoomToNextOxygenGenerator()
	{
		if (this.oxygenGenerators.Count == 0)
		{
			return;
		}
		this.focusedOxygenGenerator %= this.oxygenGenerators.Count;
		GameObject gameObject = this.oxygenGenerators[this.focusedOxygenGenerator];
		if (gameObject != null)
		{
			Vector3 position = gameObject.transform.GetPosition();
			CameraController.Instance.SetTargetPos(position, 8f, true);
		}
		else
		{
			DebugUtil.DevLogErrorFormat("ZoomToNextOxygenGenerator generator was null: {0}", new object[] { gameObject });
		}
		this.focusedOxygenGenerator++;
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x000A1DE4 File Offset: 0x0009FFE4
	private void ZoomToNextUnrefrigeratedFood()
	{
		ListPool<Pickupable, Tutorial>.PooledList pooledList = ListPool<Pickupable, Tutorial>.Allocate();
		int unrefrigeratedFood = this.GetUnrefrigeratedFood(pooledList);
		if (pooledList.Count == 0)
		{
			return;
		}
		this.focusedUnrefrigFood++;
		if (this.focusedUnrefrigFood >= unrefrigeratedFood)
		{
			this.focusedUnrefrigFood = 0;
		}
		Pickupable pickupable = pooledList[this.focusedUnrefrigFood];
		if (pickupable != null)
		{
			CameraController.Instance.SetTargetPos(pickupable.transform.GetPosition(), 8f, true);
		}
		pooledList.Recycle();
	}

	// Token: 0x040010DA RID: 4314
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x040010DB RID: 4315
	[Serialize]
	private SerializedList<Tutorial.TutorialMessages> tutorialMessagesRemaining = new SerializedList<Tutorial.TutorialMessages>();

	// Token: 0x040010DC RID: 4316
	private const string HIDDEN_TUTORIAL_PREF_KEY_PREFIX = "HideTutorial_";

	// Token: 0x040010DD RID: 4317
	public const string HIDDEN_TUTORIAL_PREF_BUTTON_KEY = "HideTutorial_CheckState";

	// Token: 0x040010DE RID: 4318
	private Dictionary<Tutorial.TutorialMessages, bool> hiddenTutorialMessages = new Dictionary<Tutorial.TutorialMessages, bool>();

	// Token: 0x040010DF RID: 4319
	private int debugMessageCount;

	// Token: 0x040010E0 RID: 4320
	private bool queuedPrioritiesMessage;

	// Token: 0x040010E1 RID: 4321
	private const float LOW_RATION_AMOUNT = 1f;

	// Token: 0x040010E3 RID: 4323
	private List<List<Tutorial.Item>> itemTree = new List<List<Tutorial.Item>>();

	// Token: 0x040010E4 RID: 4324
	private List<Tutorial.Item> warningItems = new List<Tutorial.Item>();

	// Token: 0x040010E5 RID: 4325
	private Vector3 notifierPosition;

	// Token: 0x040010E6 RID: 4326
	public List<GameObject> oxygenGenerators = new List<GameObject>();

	// Token: 0x040010E7 RID: 4327
	private int focusedOxygenGenerator;

	// Token: 0x040010E8 RID: 4328
	private int focusedUnrefrigFood = -1;

	// Token: 0x02001130 RID: 4400
	public enum TutorialMessages
	{
		// Token: 0x04005A0A RID: 23050
		TM_Basics,
		// Token: 0x04005A0B RID: 23051
		TM_Welcome,
		// Token: 0x04005A0C RID: 23052
		TM_StressManagement,
		// Token: 0x04005A0D RID: 23053
		TM_Scheduling,
		// Token: 0x04005A0E RID: 23054
		TM_Mopping,
		// Token: 0x04005A0F RID: 23055
		TM_Locomotion,
		// Token: 0x04005A10 RID: 23056
		TM_Priorities,
		// Token: 0x04005A11 RID: 23057
		TM_FetchingWater,
		// Token: 0x04005A12 RID: 23058
		TM_ThermalComfort,
		// Token: 0x04005A13 RID: 23059
		TM_OverheatingBuildings,
		// Token: 0x04005A14 RID: 23060
		TM_LotsOfGerms,
		// Token: 0x04005A15 RID: 23061
		TM_DiseaseCooking,
		// Token: 0x04005A16 RID: 23062
		TM_Suits,
		// Token: 0x04005A17 RID: 23063
		TM_Morale,
		// Token: 0x04005A18 RID: 23064
		TM_Schedule,
		// Token: 0x04005A19 RID: 23065
		TM_Digging,
		// Token: 0x04005A1A RID: 23066
		TM_Power,
		// Token: 0x04005A1B RID: 23067
		TM_Insulation,
		// Token: 0x04005A1C RID: 23068
		TM_Plumbing,
		// Token: 0x04005A1D RID: 23069
		TM_Radiation,
		// Token: 0x04005A1E RID: 23070
		TM_COUNT
	}

	// Token: 0x02001131 RID: 4401
	// (Invoke) Token: 0x060075D0 RID: 30160
	private delegate bool HideConditionDelegate();

	// Token: 0x02001132 RID: 4402
	// (Invoke) Token: 0x060075D4 RID: 30164
	private delegate bool RequirementSatisfiedDelegate();

	// Token: 0x02001133 RID: 4403
	private class Item
	{
		// Token: 0x04005A1F RID: 23071
		public Notification notification;

		// Token: 0x04005A20 RID: 23072
		public Tutorial.HideConditionDelegate hideCondition;

		// Token: 0x04005A21 RID: 23073
		public Tutorial.RequirementSatisfiedDelegate requirementSatisfied;

		// Token: 0x04005A22 RID: 23074
		public float minTimeToNotify;

		// Token: 0x04005A23 RID: 23075
		public float lastNotifyTime;
	}
}
