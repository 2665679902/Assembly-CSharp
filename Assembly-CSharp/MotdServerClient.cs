using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using STRINGS;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000B33 RID: 2867
public class MotdServerClient
{
	// Token: 0x17000658 RID: 1624
	// (get) Token: 0x060058B1 RID: 22705 RVA: 0x002023A4 File Offset: 0x002005A4
	private static string MotdServerUrl
	{
		get
		{
			return "https://klei-motd.s3.amazonaws.com/oni/" + MotdServerClient.GetLocalePathSuffix();
		}
	}

	// Token: 0x17000659 RID: 1625
	// (get) Token: 0x060058B2 RID: 22706 RVA: 0x002023B5 File Offset: 0x002005B5
	private static string MotdLocalPath
	{
		get
		{
			return "motd_local/" + MotdServerClient.GetLocalePathSuffix();
		}
	}

	// Token: 0x060058B3 RID: 22707 RVA: 0x002023C6 File Offset: 0x002005C6
	private static string MotdLocalImagePath(int imageVersion)
	{
		return MotdServerClient.MotdLocalImagePath(imageVersion, Localization.GetLocale());
	}

	// Token: 0x060058B4 RID: 22708 RVA: 0x002023D3 File Offset: 0x002005D3
	private static string FallbackMotdLocalImagePath(int imageVersion)
	{
		return MotdServerClient.MotdLocalImagePath(imageVersion, null);
	}

	// Token: 0x060058B5 RID: 22709 RVA: 0x002023DC File Offset: 0x002005DC
	private static string MotdLocalImagePath(int imageVersion, Localization.Locale locale)
	{
		return "motd_local/" + MotdServerClient.GetLocalePathModifier(locale) + "image_" + imageVersion.ToString();
	}

	// Token: 0x060058B6 RID: 22710 RVA: 0x002023FA File Offset: 0x002005FA
	private static string GetLocalePathModifier()
	{
		return MotdServerClient.GetLocalePathModifier(Localization.GetLocale());
	}

	// Token: 0x060058B7 RID: 22711 RVA: 0x00202408 File Offset: 0x00200608
	private static string GetLocalePathModifier(Localization.Locale locale)
	{
		string text = "";
		if (locale != null)
		{
			Localization.Language lang = locale.Lang;
			if (lang == Localization.Language.Chinese || lang - Localization.Language.Korean <= 1)
			{
				text = locale.Code + "/";
			}
		}
		return text;
	}

	// Token: 0x060058B8 RID: 22712 RVA: 0x00202440 File Offset: 0x00200640
	private static string GetLocalePathSuffix()
	{
		return MotdServerClient.GetLocalePathModifier() + "motd.json";
	}

	// Token: 0x060058B9 RID: 22713 RVA: 0x00202454 File Offset: 0x00200654
	public void GetMotd(Action<MotdServerClient.MotdResponse, string> cb)
	{
		this.m_callback = cb;
		MotdServerClient.MotdResponse localResponse = this.GetLocalMotd(MotdServerClient.MotdLocalPath);
		this.GetWebMotd(MotdServerClient.MotdServerUrl, localResponse, delegate(MotdServerClient.MotdResponse response, string err)
		{
			MotdServerClient.MotdResponse motdResponse;
			if (err == null)
			{
				global::Debug.Assert(response.image_texture != null, "Attempting to return response with no image texture");
				motdResponse = response;
			}
			else
			{
				global::Debug.LogWarning("Could not retrieve web motd from " + MotdServerClient.MotdServerUrl + ", falling back to local - err: " + err);
				motdResponse = localResponse;
			}
			if (Localization.GetSelectedLanguageType() == Localization.SelectedLanguageType.UGC)
			{
				global::Debug.Log("Language Mod detected, MOTD strings falling back to local file");
				motdResponse.image_header_text = UI.FRONTEND.MOTD.IMAGE_HEADER;
				motdResponse.news_header_text = UI.FRONTEND.MOTD.NEWS_HEADER;
				motdResponse.news_body_text = UI.FRONTEND.MOTD.NEWS_BODY;
				motdResponse.patch_notes_summary = UI.FRONTEND.MOTD.PATCH_NOTES_SUMMARY;
				motdResponse.vanilla_update_data.update_text_override = UI.FRONTEND.MOTD.UPDATE_TEXT;
				motdResponse.expansion1_update_data.update_text_override = UI.FRONTEND.MOTD.UPDATE_TEXT_EXPANSION1;
			}
			this.doCallback(motdResponse, null);
		});
	}

	// Token: 0x060058BA RID: 22714 RVA: 0x002024A4 File Offset: 0x002006A4
	private MotdServerClient.MotdResponse GetLocalMotd(string filePath)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(filePath.Replace(".json", ""));
		this.m_localMotd = JsonConvert.DeserializeObject<MotdServerClient.MotdResponse>(textAsset.ToString());
		string text = MotdServerClient.MotdLocalImagePath(this.m_localMotd.image_version);
		this.m_localMotd.image_texture = Resources.Load<Texture2D>(text);
		if (this.m_localMotd.image_texture == null)
		{
			string text2 = MotdServerClient.FallbackMotdLocalImagePath(this.m_localMotd.image_version);
			if (text2 != text)
			{
				global::Debug.Log("Could not load " + text + ", falling back to " + text2);
				text = text2;
				this.m_localMotd.image_texture = Resources.Load<Texture2D>(text);
			}
		}
		global::Debug.Assert(this.m_localMotd.image_texture != null, "Failed to load " + text);
		return this.m_localMotd;
	}

	// Token: 0x060058BB RID: 22715 RVA: 0x00202578 File Offset: 0x00200778
	private void GetWebMotd(string url, MotdServerClient.MotdResponse localMotd, Action<MotdServerClient.MotdResponse, string> cb)
	{
		MotdServerClient.<>c__DisplayClass16_0 CS$<>8__locals1 = new MotdServerClient.<>c__DisplayClass16_0();
		CS$<>8__locals1.localMotd = localMotd;
		CS$<>8__locals1.cb = cb;
		Action<string, string> action = delegate(string response, string err)
		{
			MotdServerClient.<>c__DisplayClass16_1 CS$<>8__locals2 = new MotdServerClient.<>c__DisplayClass16_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			DebugUtil.DevAssert(CS$<>8__locals1.localMotd.image_texture != null, "Local MOTD image_texture is no longer loaded", null);
			if (CS$<>8__locals1.localMotd.image_texture == null)
			{
				CS$<>8__locals1.cb(null, "Local image_texture has been unloaded since we requested the MOTD");
				return;
			}
			if (err != null)
			{
				CS$<>8__locals1.cb(null, err);
				return;
			}
			MotdServerClient.<>c__DisplayClass16_1 CS$<>8__locals3 = CS$<>8__locals2;
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
			jsonSerializerSettings.Error = delegate(object sender, ErrorEventArgs args)
			{
				args.ErrorContext.Handled = true;
			};
			CS$<>8__locals3.responseStruct = JsonConvert.DeserializeObject<MotdServerClient.MotdResponse>(response, jsonSerializerSettings);
			if (CS$<>8__locals2.responseStruct == null)
			{
				CS$<>8__locals1.cb(null, "Invalid json from server:" + response);
				return;
			}
			if (CS$<>8__locals2.responseStruct.version <= CS$<>8__locals1.localMotd.version)
			{
				global::Debug.Log("Using local MOTD at version: " + CS$<>8__locals1.localMotd.version.ToString() + ", web version at " + CS$<>8__locals2.responseStruct.version.ToString());
				CS$<>8__locals1.cb(CS$<>8__locals1.localMotd, null);
				return;
			}
			UnityWebRequest unityWebRequest = new UnityWebRequest();
			unityWebRequest.downloadHandler = new DownloadHandlerTexture();
			SimpleNetworkCache.LoadFromCacheOrDownload("motd_image", CS$<>8__locals2.responseStruct.image_url, CS$<>8__locals2.responseStruct.image_version, unityWebRequest, delegate(UnityWebRequest wr)
			{
				string text = null;
				if (string.IsNullOrEmpty(wr.error))
				{
					global::Debug.Log("Using web MOTD at version: " + CS$<>8__locals2.responseStruct.version.ToString() + ", local version at " + CS$<>8__locals2.CS$<>8__locals1.localMotd.version.ToString());
					CS$<>8__locals2.responseStruct.image_texture = DownloadHandlerTexture.GetContent(wr);
				}
				else
				{
					text = "Failed to load image: " + CS$<>8__locals2.responseStruct.image_url + " SimpleNetworkCache - " + wr.error;
				}
				CS$<>8__locals2.CS$<>8__locals1.cb(CS$<>8__locals2.responseStruct, text);
				wr.Dispose();
			});
		};
		this.getAsyncRequest(url, action);
	}

	// Token: 0x060058BC RID: 22716 RVA: 0x002025AC File Offset: 0x002007AC
	private void getAsyncRequest(string url, Action<string, string> cb)
	{
		UnityWebRequest motdRequest = UnityWebRequest.Get(url);
		motdRequest.SetRequestHeader("Content-Type", "application/json");
		motdRequest.SendWebRequest().completed += delegate(AsyncOperation operation)
		{
			cb(motdRequest.downloadHandler.text, motdRequest.error);
			motdRequest.Dispose();
		};
	}

	// Token: 0x060058BD RID: 22717 RVA: 0x00202603 File Offset: 0x00200803
	public void UnregisterCallback()
	{
		this.m_callback = null;
	}

	// Token: 0x060058BE RID: 22718 RVA: 0x0020260C File Offset: 0x0020080C
	private void doCallback(MotdServerClient.MotdResponse response, string error)
	{
		if (this.m_callback != null)
		{
			this.m_callback(response, error);
			return;
		}
		global::Debug.Log("Motd Response receieved, but callback was unregistered");
	}

	// Token: 0x04003BEE RID: 15342
	private Action<MotdServerClient.MotdResponse, string> m_callback;

	// Token: 0x04003BEF RID: 15343
	private MotdServerClient.MotdResponse m_localMotd;

	// Token: 0x020019CD RID: 6605
	public class MotdUpdateData
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06009142 RID: 37186 RVA: 0x00314363 File Offset: 0x00312563
		// (set) Token: 0x06009143 RID: 37187 RVA: 0x0031436B File Offset: 0x0031256B
		public string last_update_time { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06009144 RID: 37188 RVA: 0x00314374 File Offset: 0x00312574
		// (set) Token: 0x06009145 RID: 37189 RVA: 0x0031437C File Offset: 0x0031257C
		public string next_update_time { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06009146 RID: 37190 RVA: 0x00314385 File Offset: 0x00312585
		// (set) Token: 0x06009147 RID: 37191 RVA: 0x0031438D File Offset: 0x0031258D
		public string update_text_override { get; set; }
	}

	// Token: 0x020019CE RID: 6606
	public class MotdResponse
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06009149 RID: 37193 RVA: 0x0031439E File Offset: 0x0031259E
		// (set) Token: 0x0600914A RID: 37194 RVA: 0x003143A6 File Offset: 0x003125A6
		public int version { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600914B RID: 37195 RVA: 0x003143AF File Offset: 0x003125AF
		// (set) Token: 0x0600914C RID: 37196 RVA: 0x003143B7 File Offset: 0x003125B7
		public string image_header_text { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600914D RID: 37197 RVA: 0x003143C0 File Offset: 0x003125C0
		// (set) Token: 0x0600914E RID: 37198 RVA: 0x003143C8 File Offset: 0x003125C8
		public int image_version { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600914F RID: 37199 RVA: 0x003143D1 File Offset: 0x003125D1
		// (set) Token: 0x06009150 RID: 37200 RVA: 0x003143D9 File Offset: 0x003125D9
		public string image_url { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06009151 RID: 37201 RVA: 0x003143E2 File Offset: 0x003125E2
		// (set) Token: 0x06009152 RID: 37202 RVA: 0x003143EA File Offset: 0x003125EA
		public string image_link_url { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06009153 RID: 37203 RVA: 0x003143F3 File Offset: 0x003125F3
		// (set) Token: 0x06009154 RID: 37204 RVA: 0x003143FB File Offset: 0x003125FB
		public string image_rail_link_url { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06009155 RID: 37205 RVA: 0x00314404 File Offset: 0x00312604
		// (set) Token: 0x06009156 RID: 37206 RVA: 0x0031440C File Offset: 0x0031260C
		public string news_header_text { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06009157 RID: 37207 RVA: 0x00314415 File Offset: 0x00312615
		// (set) Token: 0x06009158 RID: 37208 RVA: 0x0031441D File Offset: 0x0031261D
		public string news_body_text { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06009159 RID: 37209 RVA: 0x00314426 File Offset: 0x00312626
		// (set) Token: 0x0600915A RID: 37210 RVA: 0x0031442E File Offset: 0x0031262E
		public string patch_notes_summary { get; set; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600915B RID: 37211 RVA: 0x00314437 File Offset: 0x00312637
		// (set) Token: 0x0600915C RID: 37212 RVA: 0x0031443F File Offset: 0x0031263F
		public string patch_notes_link_url { get; set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600915D RID: 37213 RVA: 0x00314448 File Offset: 0x00312648
		// (set) Token: 0x0600915E RID: 37214 RVA: 0x00314450 File Offset: 0x00312650
		public string patch_notes_rail_link_url { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600915F RID: 37215 RVA: 0x00314459 File Offset: 0x00312659
		// (set) Token: 0x06009160 RID: 37216 RVA: 0x00314461 File Offset: 0x00312661
		public MotdServerClient.MotdUpdateData vanilla_update_data { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06009161 RID: 37217 RVA: 0x0031446A File Offset: 0x0031266A
		// (set) Token: 0x06009162 RID: 37218 RVA: 0x00314472 File Offset: 0x00312672
		public MotdServerClient.MotdUpdateData expansion1_update_data { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06009163 RID: 37219 RVA: 0x0031447B File Offset: 0x0031267B
		// (set) Token: 0x06009164 RID: 37220 RVA: 0x00314483 File Offset: 0x00312683
		public string latest_update_build { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06009165 RID: 37221 RVA: 0x0031448C File Offset: 0x0031268C
		// (set) Token: 0x06009166 RID: 37222 RVA: 0x00314494 File Offset: 0x00312694
		[JsonIgnore]
		public Texture2D image_texture { get; set; }
	}
}
