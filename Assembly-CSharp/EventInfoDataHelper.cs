using System;
using UnityEngine;

// Token: 0x02000A97 RID: 2711
public class EventInfoDataHelper
{
	// Token: 0x06005324 RID: 21284 RVA: 0x001E26F4 File Offset: 0x001E08F4
	public static EventInfoData GenerateStoryTraitData(string titleText, string descriptionText, string buttonText, string animFileName, EventInfoDataHelper.PopupType popupType, string buttonTooltip = null, GameObject[] minions = null, System.Action callback = null)
	{
		EventInfoData eventInfoData = new EventInfoData(titleText, descriptionText, animFileName);
		eventInfoData.minions = minions;
		if (popupType == EventInfoDataHelper.PopupType.BEGIN)
		{
			eventInfoData.showCallback = delegate
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("StoryTrait_Activation_Popup", false));
			};
		}
		if (popupType == EventInfoDataHelper.PopupType.COMPLETE)
		{
			eventInfoData.showCallback = delegate
			{
				MusicManager.instance.PlaySong("Stinger_StoryTraitUnlock", false);
			};
		}
		EventInfoData.Option option = eventInfoData.AddOption(buttonText, null);
		option.callback = callback;
		option.tooltip = buttonTooltip;
		return eventInfoData;
	}

	// Token: 0x0200191C RID: 6428
	public enum PopupType
	{
		// Token: 0x0400735B RID: 29531
		NONE = -1,
		// Token: 0x0400735C RID: 29532
		BEGIN,
		// Token: 0x0400735D RID: 29533
		NORMAL,
		// Token: 0x0400735E RID: 29534
		COMPLETE
	}
}
