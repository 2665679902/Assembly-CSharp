using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class Notification
{
	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00091263 File Offset: 0x0008F463
	// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0009126B File Offset: 0x0008F46B
	public NotificationType Type { get; set; }

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00091274 File Offset: 0x0008F474
	// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0009127C File Offset: 0x0008F47C
	public Notifier Notifier { get; set; }

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06001B43 RID: 6979 RVA: 0x00091285 File Offset: 0x0008F485
	// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0009128D File Offset: 0x0008F48D
	public Transform clickFocus { get; set; }

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06001B45 RID: 6981 RVA: 0x00091296 File Offset: 0x0008F496
	// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0009129E File Offset: 0x0008F49E
	public float Time { get; set; }

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000912A7 File Offset: 0x0008F4A7
	// (set) Token: 0x06001B48 RID: 6984 RVA: 0x000912AF File Offset: 0x0008F4AF
	public float GameTime { get; set; }

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06001B49 RID: 6985 RVA: 0x000912B8 File Offset: 0x0008F4B8
	// (set) Token: 0x06001B4A RID: 6986 RVA: 0x000912C0 File Offset: 0x0008F4C0
	public float Delay { get; set; }

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06001B4B RID: 6987 RVA: 0x000912C9 File Offset: 0x0008F4C9
	// (set) Token: 0x06001B4C RID: 6988 RVA: 0x000912D1 File Offset: 0x0008F4D1
	public int Idx { get; set; }

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06001B4D RID: 6989 RVA: 0x000912DA File Offset: 0x0008F4DA
	// (set) Token: 0x06001B4E RID: 6990 RVA: 0x000912E2 File Offset: 0x0008F4E2
	public Func<List<Notification>, object, string> ToolTip { get; set; }

	// Token: 0x06001B4F RID: 6991 RVA: 0x000912EB File Offset: 0x0008F4EB
	public bool IsReady()
	{
		return UnityEngine.Time.time >= this.GameTime + this.Delay;
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06001B50 RID: 6992 RVA: 0x00091304 File Offset: 0x0008F504
	// (set) Token: 0x06001B51 RID: 6993 RVA: 0x0009130C File Offset: 0x0008F50C
	public string titleText { get; private set; }

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06001B52 RID: 6994 RVA: 0x00091315 File Offset: 0x0008F515
	// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0009131D File Offset: 0x0008F51D
	public string NotifierName
	{
		get
		{
			return this.notifierName;
		}
		set
		{
			this.notifierName = value;
			this.titleText = this.ReplaceTags(this.titleText);
		}
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x00091338 File Offset: 0x0008F538
	public Notification(string title, NotificationType type, Func<List<Notification>, object, string> tooltip = null, object tooltip_data = null, bool expires = true, float delay = 0f, Notification.ClickCallback custom_click_callback = null, object custom_click_data = null, Transform click_focus = null, bool volume_attenuation = true, bool clear_on_click = false, bool show_dismiss_button = false)
	{
		this.titleText = title;
		this.Type = type;
		this.ToolTip = tooltip;
		this.tooltipData = tooltip_data;
		this.expires = expires;
		this.Delay = delay;
		this.customClickCallback = custom_click_callback;
		this.customClickData = custom_click_data;
		this.clickFocus = click_focus;
		this.volume_attenuation = volume_attenuation;
		this.clearOnClick = clear_on_click;
		this.showDismissButton = show_dismiss_button;
		int num = this.notificationIncrement;
		this.notificationIncrement = num + 1;
		this.Idx = num;
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000913D4 File Offset: 0x0008F5D4
	public void Clear()
	{
		if (this.Notifier != null)
		{
			this.Notifier.Remove(this);
		}
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x000913F0 File Offset: 0x0008F5F0
	private string ReplaceTags(string text)
	{
		DebugUtil.Assert(text != null);
		int num = text.IndexOf('{');
		int num2 = text.IndexOf('}');
		if (0 <= num && num < num2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = 0;
			while (0 <= num)
			{
				string text2 = text.Substring(num3, num - num3);
				stringBuilder.Append(text2);
				num2 = text.IndexOf('}', num);
				if (num >= num2)
				{
					break;
				}
				string text3 = text.Substring(num + 1, num2 - num - 1);
				string tagDescription = this.GetTagDescription(text3);
				stringBuilder.Append(tagDescription);
				num3 = num2 + 1;
				num = text.IndexOf('{', num2);
			}
			stringBuilder.Append(text.Substring(num3, text.Length - num3));
			return stringBuilder.ToString();
		}
		return text;
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x000914A4 File Offset: 0x0008F6A4
	private string GetTagDescription(string tag)
	{
		string text;
		if (tag == "NotifierName")
		{
			text = this.notifierName;
		}
		else
		{
			text = "UNKNOWN TAG: " + tag;
		}
		return text;
	}

	// Token: 0x04000F3A RID: 3898
	public object tooltipData;

	// Token: 0x04000F3B RID: 3899
	public bool expires = true;

	// Token: 0x04000F3C RID: 3900
	public bool playSound = true;

	// Token: 0x04000F3D RID: 3901
	public bool volume_attenuation = true;

	// Token: 0x04000F3E RID: 3902
	public Notification.ClickCallback customClickCallback;

	// Token: 0x04000F3F RID: 3903
	public bool clearOnClick;

	// Token: 0x04000F40 RID: 3904
	public bool showDismissButton;

	// Token: 0x04000F41 RID: 3905
	public object customClickData;

	// Token: 0x04000F42 RID: 3906
	private int notificationIncrement;

	// Token: 0x04000F44 RID: 3908
	private string notifierName;

	// Token: 0x020010E8 RID: 4328
	// (Invoke) Token: 0x060074DA RID: 29914
	public delegate void ClickCallback(object data);
}
