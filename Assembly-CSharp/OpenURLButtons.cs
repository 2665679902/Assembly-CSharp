using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

// Token: 0x02000B49 RID: 2889
[AddComponentMenu("KMonoBehaviour/scripts/OpenURLButtons")]
public class OpenURLButtons : KMonoBehaviour
{
	// Token: 0x0600597F RID: 22911 RVA: 0x00206040 File Offset: 0x00204240
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		for (int i = 0; i < this.buttonData.Count; i++)
		{
			OpenURLButtons.URLButtonData data = this.buttonData[i];
			GameObject gameObject = Util.KInstantiateUI(this.buttonPrefab, base.gameObject, true);
			string text = Strings.Get(data.stringKey);
			gameObject.GetComponentInChildren<LocText>().SetText(text);
			switch (data.urlType)
			{
			case OpenURLButtons.URLButtonType.url:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenURL(data.url);
				};
				break;
			case OpenURLButtons.URLButtonType.platformUrl:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenPlatformURL(data.url);
				};
				break;
			case OpenURLButtons.URLButtonType.patchNotes:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenPatchNotes();
				};
				break;
			case OpenURLButtons.URLButtonType.feedbackScreen:
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.OpenFeedbackScreen();
				};
				break;
			}
		}
	}

	// Token: 0x06005980 RID: 22912 RVA: 0x0020614B File Offset: 0x0020434B
	public void OpenPatchNotes()
	{
		Util.KInstantiateUI(this.patchNotesScreenPrefab, FrontEndManager.Instance.gameObject, true);
	}

	// Token: 0x06005981 RID: 22913 RVA: 0x00206164 File Offset: 0x00204364
	public void OpenFeedbackScreen()
	{
		Util.KInstantiateUI(this.feedbackScreenPrefab.gameObject, FrontEndManager.Instance.gameObject, true);
	}

	// Token: 0x06005982 RID: 22914 RVA: 0x00206182 File Offset: 0x00204382
	public void OpenURL(string URL)
	{
		App.OpenWebURL(URL);
	}

	// Token: 0x06005983 RID: 22915 RVA: 0x0020618C File Offset: 0x0020438C
	public void OpenPlatformURL(string URL)
	{
		if (DistributionPlatform.Inst.Platform == "Steam" && DistributionPlatform.Inst.Initialized)
		{
			DistributionPlatform.Inst.GetAuthTicket(delegate(byte[] ticket)
			{
				string text2 = string.Concat(Array.ConvertAll<byte, string>(ticket, (byte x) => x.ToString("X2")));
				App.OpenWebURL(URL.Replace("{SteamID}", DistributionPlatform.Inst.LocalUser.Id.ToInt64().ToString()).Replace("{SteamTicket}", text2));
			});
			return;
		}
		string text = URL.Replace("{SteamID}", "").Replace("{SteamTicket}", "");
		App.OpenWebURL("https://accounts.klei.com/login?goto={gotoUrl}".Replace("{gotoUrl}", WebUtility.HtmlEncode(text)));
	}

	// Token: 0x04003C76 RID: 15478
	public GameObject buttonPrefab;

	// Token: 0x04003C77 RID: 15479
	public List<OpenURLButtons.URLButtonData> buttonData;

	// Token: 0x04003C78 RID: 15480
	[SerializeField]
	private GameObject patchNotesScreenPrefab;

	// Token: 0x04003C79 RID: 15481
	[SerializeField]
	private FeedbackScreen feedbackScreenPrefab;

	// Token: 0x020019DE RID: 6622
	public enum URLButtonType
	{
		// Token: 0x04007599 RID: 30105
		url,
		// Token: 0x0400759A RID: 30106
		platformUrl,
		// Token: 0x0400759B RID: 30107
		patchNotes,
		// Token: 0x0400759C RID: 30108
		feedbackScreen
	}

	// Token: 0x020019DF RID: 6623
	[Serializable]
	public class URLButtonData
	{
		// Token: 0x0400759D RID: 30109
		public string stringKey;

		// Token: 0x0400759E RID: 30110
		public OpenURLButtons.URLButtonType urlType;

		// Token: 0x0400759F RID: 30111
		public string url;
	}
}
