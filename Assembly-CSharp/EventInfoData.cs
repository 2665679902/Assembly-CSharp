using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class EventInfoData
{
	// Token: 0x06001366 RID: 4966 RVA: 0x00066E2C File Offset: 0x0006502C
	public EventInfoData(string title, string description, HashedString animFileName)
	{
		this.title = title;
		this.description = description;
		this.animFileName = animFileName;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00066E7A File Offset: 0x0006507A
	public List<EventInfoData.Option> GetOptions()
	{
		this.FinalizeText();
		return this.options;
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x00066E88 File Offset: 0x00065088
	public EventInfoData.Option AddOption(string mainText, string description = null)
	{
		EventInfoData.Option option = new EventInfoData.Option
		{
			mainText = mainText,
			description = description
		};
		this.options.Add(option);
		this.dirty = true;
		return option;
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x00066EC0 File Offset: 0x000650C0
	public EventInfoData.Option SimpleOption(string mainText, System.Action callback)
	{
		EventInfoData.Option option = new EventInfoData.Option
		{
			mainText = mainText,
			callback = callback
		};
		this.options.Add(option);
		this.dirty = true;
		return option;
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x00066EF5 File Offset: 0x000650F5
	public EventInfoData.Option AddDefaultOption(System.Action callback = null)
	{
		return this.SimpleOption(GAMEPLAY_EVENTS.DEFAULT_OPTION_NAME, callback);
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x00066F08 File Offset: 0x00065108
	public EventInfoData.Option AddDefaultConsiderLaterOption(System.Action callback = null)
	{
		return this.SimpleOption(GAMEPLAY_EVENTS.DEFAULT_OPTION_CONSIDER_NAME, callback);
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x00066F1B File Offset: 0x0006511B
	public void SetTextParameter(string key, string value)
	{
		this.textParameters[key] = value;
		this.dirty = true;
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x00066F34 File Offset: 0x00065134
	public void FinalizeText()
	{
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		foreach (KeyValuePair<string, string> keyValuePair in this.textParameters)
		{
			string text = "{" + keyValuePair.Key + "}";
			if (this.title != null)
			{
				this.title = this.title.Replace(text, keyValuePair.Value);
			}
			if (this.description != null)
			{
				this.description = this.description.Replace(text, keyValuePair.Value);
			}
			if (this.location != null)
			{
				this.location = this.location.Replace(text, keyValuePair.Value);
			}
			if (this.whenDescription != null)
			{
				this.whenDescription = this.whenDescription.Replace(text, keyValuePair.Value);
			}
			foreach (EventInfoData.Option option in this.options)
			{
				if (option.mainText != null)
				{
					option.mainText = option.mainText.Replace(text, keyValuePair.Value);
				}
				if (option.description != null)
				{
					option.description = option.description.Replace(text, keyValuePair.Value);
				}
				if (option.tooltip != null)
				{
					option.tooltip = option.tooltip.Replace(text, keyValuePair.Value);
				}
				foreach (EventInfoData.OptionIcon optionIcon in option.informationIcons)
				{
					if (optionIcon.tooltip != null)
					{
						optionIcon.tooltip = optionIcon.tooltip.Replace(text, keyValuePair.Value);
					}
				}
				foreach (EventInfoData.OptionIcon optionIcon2 in option.consequenceIcons)
				{
					if (optionIcon2.tooltip != null)
					{
						optionIcon2.tooltip = optionIcon2.tooltip.Replace(text, keyValuePair.Value);
					}
				}
			}
		}
	}

	// Token: 0x04000A7B RID: 2683
	public string title;

	// Token: 0x04000A7C RID: 2684
	public string description;

	// Token: 0x04000A7D RID: 2685
	public string location;

	// Token: 0x04000A7E RID: 2686
	public string whenDescription;

	// Token: 0x04000A7F RID: 2687
	public Transform clickFocus;

	// Token: 0x04000A80 RID: 2688
	public GameObject[] minions;

	// Token: 0x04000A81 RID: 2689
	public GameObject artifact;

	// Token: 0x04000A82 RID: 2690
	public HashedString animFileName;

	// Token: 0x04000A83 RID: 2691
	public HashedString mainAnim = "event";

	// Token: 0x04000A84 RID: 2692
	public Dictionary<string, string> textParameters = new Dictionary<string, string>();

	// Token: 0x04000A85 RID: 2693
	public List<EventInfoData.Option> options = new List<EventInfoData.Option>();

	// Token: 0x04000A86 RID: 2694
	public System.Action showCallback;

	// Token: 0x04000A87 RID: 2695
	private bool dirty;

	// Token: 0x02000FBB RID: 4027
	public class OptionIcon
	{
		// Token: 0x06007058 RID: 28760 RVA: 0x002A6D2B File Offset: 0x002A4F2B
		public OptionIcon(Sprite sprite, EventInfoData.OptionIcon.ContainerType containerType, string tooltip, float scale = 1f)
		{
			this.sprite = sprite;
			this.containerType = containerType;
			this.tooltip = tooltip;
			this.scale = scale;
		}

		// Token: 0x0400555B RID: 21851
		public EventInfoData.OptionIcon.ContainerType containerType;

		// Token: 0x0400555C RID: 21852
		public Sprite sprite;

		// Token: 0x0400555D RID: 21853
		public string tooltip;

		// Token: 0x0400555E RID: 21854
		public float scale;

		// Token: 0x02001EDC RID: 7900
		public enum ContainerType
		{
			// Token: 0x04008A3E RID: 35390
			Neutral,
			// Token: 0x04008A3F RID: 35391
			Positive,
			// Token: 0x04008A40 RID: 35392
			Negative,
			// Token: 0x04008A41 RID: 35393
			Information
		}
	}

	// Token: 0x02000FBC RID: 4028
	public class Option
	{
		// Token: 0x06007059 RID: 28761 RVA: 0x002A6D50 File Offset: 0x002A4F50
		public void AddInformationIcon(string tooltip, float scale = 1f)
		{
			this.informationIcons.Add(new EventInfoData.OptionIcon(null, EventInfoData.OptionIcon.ContainerType.Information, tooltip, scale));
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x002A6D66 File Offset: 0x002A4F66
		public void AddPositiveIcon(Sprite sprite, string tooltip, float scale = 1f)
		{
			this.consequenceIcons.Add(new EventInfoData.OptionIcon(sprite, EventInfoData.OptionIcon.ContainerType.Positive, tooltip, scale));
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x002A6D7C File Offset: 0x002A4F7C
		public void AddNeutralIcon(Sprite sprite, string tooltip, float scale = 1f)
		{
			this.consequenceIcons.Add(new EventInfoData.OptionIcon(sprite, EventInfoData.OptionIcon.ContainerType.Neutral, tooltip, scale));
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x002A6D92 File Offset: 0x002A4F92
		public void AddNegativeIcon(Sprite sprite, string tooltip, float scale = 1f)
		{
			this.consequenceIcons.Add(new EventInfoData.OptionIcon(sprite, EventInfoData.OptionIcon.ContainerType.Negative, tooltip, scale));
		}

		// Token: 0x0400555F RID: 21855
		public string mainText;

		// Token: 0x04005560 RID: 21856
		public string description;

		// Token: 0x04005561 RID: 21857
		public string tooltip;

		// Token: 0x04005562 RID: 21858
		public System.Action callback;

		// Token: 0x04005563 RID: 21859
		public List<EventInfoData.OptionIcon> informationIcons = new List<EventInfoData.OptionIcon>();

		// Token: 0x04005564 RID: 21860
		public List<EventInfoData.OptionIcon> consequenceIcons = new List<EventInfoData.OptionIcon>();

		// Token: 0x04005565 RID: 21861
		public bool allowed = true;
	}
}
