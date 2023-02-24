using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Klei;
using Newtonsoft.Json;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007EC RID: 2028
public class KCrashReporter : MonoBehaviour
{
	// Token: 0x1400001B RID: 27
	// (add) Token: 0x06003A67 RID: 14951 RVA: 0x001435B4 File Offset: 0x001417B4
	// (remove) Token: 0x06003A68 RID: 14952 RVA: 0x001435E8 File Offset: 0x001417E8
	public static event Action<string> onCrashReported;

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x06003A69 RID: 14953 RVA: 0x0014361B File Offset: 0x0014181B
	// (set) Token: 0x06003A6A RID: 14954 RVA: 0x00143622 File Offset: 0x00141822
	public static bool hasReportedError { get; private set; }

	// Token: 0x06003A6B RID: 14955 RVA: 0x0014362C File Offset: 0x0014182C
	private void OnEnable()
	{
		KCrashReporter.dataRoot = Application.dataPath;
		Application.logMessageReceived += this.HandleLog;
		KCrashReporter.ignoreAll = true;
		string text = Path.Combine(KCrashReporter.dataRoot, "hashes.json");
		if (File.Exists(text))
		{
			StringBuilder stringBuilder = new StringBuilder();
			MD5 md = MD5.Create();
			Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(text));
			if (dictionary.Count > 0)
			{
				bool flag = true;
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					stringBuilder.Length = 0;
					using (FileStream fileStream = new FileStream(Path.Combine(KCrashReporter.dataRoot, key), FileMode.Open, FileAccess.Read))
					{
						foreach (byte b in md.ComputeHash(fileStream))
						{
							stringBuilder.AppendFormat("{0:x2}", b);
						}
						if (stringBuilder.ToString() != value)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					KCrashReporter.ignoreAll = false;
				}
			}
			else
			{
				KCrashReporter.ignoreAll = false;
			}
		}
		else
		{
			KCrashReporter.ignoreAll = false;
		}
		if (KCrashReporter.ignoreAll)
		{
			global::Debug.Log("Ignoring crash due to mismatched hashes.json entries.");
		}
		if (File.Exists("ignorekcrashreporter.txt"))
		{
			KCrashReporter.ignoreAll = true;
			global::Debug.Log("Ignoring crash due to ignorekcrashreporter.txt");
		}
		if (Application.isEditor && !GenericGameSettings.instance.enableEditorCrashReporting)
		{
			KCrashReporter.terminateOnError = false;
		}
	}

	// Token: 0x06003A6C RID: 14956 RVA: 0x001437D4 File Offset: 0x001419D4
	private void OnDisable()
	{
		Application.logMessageReceived -= this.HandleLog;
	}

	// Token: 0x06003A6D RID: 14957 RVA: 0x001437E8 File Offset: 0x001419E8
	private void HandleLog(string msg, string stack_trace, LogType type)
	{
		if ((KCrashReporter.logCount += 1U) == 10000000U)
		{
			DebugUtil.DevLogError("Turning off logging to avoid increasing the file to an unreasonable size, please review the logs as they probably contain spam");
			global::Debug.DisableLogging();
		}
		if (KCrashReporter.ignoreAll)
		{
			return;
		}
		if (Array.IndexOf<string>(KCrashReporter.IgnoreStrings, msg) != -1)
		{
			return;
		}
		if (msg != null && msg.StartsWith("<RI.Hid>"))
		{
			return;
		}
		if (msg != null && msg.StartsWith("Failed to load cursor"))
		{
			return;
		}
		if (msg != null && msg.StartsWith("Failed to save a temporary cursor"))
		{
			return;
		}
		if (type == LogType.Exception)
		{
			RestartWarning.ShouldWarn = true;
		}
		if (this.errorScreen == null && (type == LogType.Exception || type == LogType.Error))
		{
			if (KCrashReporter.terminateOnError && KCrashReporter.hasCrash)
			{
				return;
			}
			if (SpeedControlScreen.Instance != null)
			{
				SpeedControlScreen.Instance.Pause(true, true);
			}
			string text = stack_trace;
			if (string.IsNullOrEmpty(text))
			{
				text = new StackTrace(5, true).ToString();
			}
			if (App.isLoading)
			{
				if (!SceneInitializerLoader.deferred_error.IsValid)
				{
					SceneInitializerLoader.deferred_error = new SceneInitializerLoader.DeferredError
					{
						msg = msg,
						stack_trace = text
					};
					return;
				}
			}
			else
			{
				this.ShowDialog(msg, text);
			}
		}
	}

	// Token: 0x06003A6E RID: 14958 RVA: 0x00143900 File Offset: 0x00141B00
	public bool ShowDialog(string error, string stack_trace)
	{
		if (this.errorScreen != null)
		{
			return false;
		}
		GameObject gameObject = GameObject.Find(KCrashReporter.error_canvas_name);
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.name = KCrashReporter.error_canvas_name;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
			canvas.sortingOrder = 32767;
			gameObject.AddComponent<GraphicRaycaster>();
		}
		this.errorScreen = UnityEngine.Object.Instantiate<GameObject>(this.reportErrorPrefab, Vector3.zero, Quaternion.identity);
		this.errorScreen.transform.SetParent(gameObject.transform, false);
		ReportErrorDialog errorDialog = this.errorScreen.GetComponentInChildren<ReportErrorDialog>();
		string text = error + "\n\n" + stack_trace;
		KCrashReporter.hasCrash = true;
		if (Global.Instance != null && Global.Instance.modManager != null && Global.Instance.modManager.HasCrashableMods())
		{
			Exception ex = DebugUtil.RetrieveLastExceptionLogged();
			StackTrace stackTrace = ((ex != null) ? new StackTrace(ex) : new StackTrace(5, true));
			Global.Instance.modManager.SearchForModsInStackTrace(stackTrace);
			Global.Instance.modManager.SearchForModsInStackTrace(stack_trace);
			errorDialog.PopupDisableModsDialog(text, new System.Action(this.OnQuitToDesktop), (Global.Instance.modManager.IsInDevMode() || !KCrashReporter.terminateOnError) ? new System.Action(this.OnCloseErrorDialog) : null);
		}
		else
		{
			errorDialog.PopupSubmitErrorDialog(text, delegate
			{
				string text2 = null;
				if (KCrashReporter.MOST_RECENT_SAVEFILE != null)
				{
					text2 = KCrashReporter.UploadSaveFile(KCrashReporter.MOST_RECENT_SAVEFILE, stack_trace, null);
				}
				KCrashReporter.ReportError(error, stack_trace, text2, this.confirmDialogPrefab, this.errorScreen, errorDialog.UserMessage());
			}, new System.Action(this.OnQuitToDesktop), KCrashReporter.terminateOnError ? null : new System.Action(this.OnCloseErrorDialog));
		}
		return true;
	}

	// Token: 0x06003A6F RID: 14959 RVA: 0x00143AD1 File Offset: 0x00141CD1
	private void OnCloseErrorDialog()
	{
		UnityEngine.Object.Destroy(this.errorScreen);
		this.errorScreen = null;
		KCrashReporter.hasCrash = false;
		if (SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Unpause(true);
		}
	}

	// Token: 0x06003A70 RID: 14960 RVA: 0x00143B03 File Offset: 0x00141D03
	private void OnQuitToDesktop()
	{
		App.Quit();
	}

	// Token: 0x06003A71 RID: 14961 RVA: 0x00143B0C File Offset: 0x00141D0C
	private static string UploadSaveFile(string save_file, string stack_trace, Dictionary<string, string> metadata = null)
	{
		global::Debug.Log(string.Format("Save_file: {0}", save_file));
		if (KPrivacyPrefs.instance.disableDataCollection)
		{
			return "";
		}
		if (save_file != null && File.Exists(save_file))
		{
			using (WebClient webClient = new WebClient())
			{
				Encoding utf = Encoding.UTF8;
				webClient.Encoding = utf;
				byte[] array = File.ReadAllBytes(save_file);
				string text = "----" + System.DateTime.Now.Ticks.ToString("x");
				webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + text);
				string text2 = "";
				string text3;
				using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
				{
					text3 = BitConverter.ToString(sha1CryptoServiceProvider.ComputeHash(array)).Replace("-", "");
				}
				text2 += string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", text, "hash", text3);
				if (metadata != null)
				{
					string text4 = JsonConvert.SerializeObject(metadata);
					text2 += string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", text, "metadata", text4);
				}
				text2 += string.Format("--{0}\r\nContent-Disposition: form-data; name=\"save\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", new object[] { text, save_file, "application/x-spss-sav" });
				byte[] bytes = utf.GetBytes(text2);
				string text5 = string.Format("\r\n--{0}--\r\n", text);
				byte[] bytes2 = utf.GetBytes(text5);
				byte[] array2 = new byte[bytes.Length + array.Length + bytes2.Length];
				Buffer.BlockCopy(bytes, 0, array2, 0, bytes.Length);
				Buffer.BlockCopy(array, 0, array2, bytes.Length, array.Length);
				Buffer.BlockCopy(bytes2, 0, array2, bytes.Length + array.Length, bytes2.Length);
				Uri uri = new Uri("http://crashes.klei.ca/submitSave");
				try
				{
					webClient.UploadData(uri, "POST", array2);
					return text3;
				}
				catch (Exception ex)
				{
					global::Debug.Log(ex);
					return "";
				}
			}
		}
		return "";
	}

	// Token: 0x06003A72 RID: 14962 RVA: 0x00143D44 File Offset: 0x00141F44
	private static string GetUserID()
	{
		if (DistributionPlatform.Initialized)
		{
			string[] array = new string[5];
			array[0] = DistributionPlatform.Inst.Name;
			array[1] = "ID_";
			array[2] = DistributionPlatform.Inst.LocalUser.Name;
			array[3] = "_";
			int num = 4;
			DistributionPlatform.UserId id = DistributionPlatform.Inst.LocalUser.Id;
			array[num] = ((id != null) ? id.ToString() : null);
			return string.Concat(array);
		}
		return "LocalUser";
	}

	// Token: 0x06003A73 RID: 14963 RVA: 0x00143DB8 File Offset: 0x00141FB8
	private static string GetLogContents()
	{
		string text = Util.LogFilePath();
		if (File.Exists(text))
		{
			using (FileStream fileStream = File.Open(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					return streamReader.ReadToEnd();
				}
			}
		}
		return "";
	}

	// Token: 0x06003A74 RID: 14964 RVA: 0x00143E24 File Offset: 0x00142024
	public static void ReportErrorDevNotification(string notification_name, string stack_trace, string details = "")
	{
		if (KCrashReporter.previouslyReportedDevNotifications == null)
		{
			KCrashReporter.previouslyReportedDevNotifications = new HashSet<int>();
		}
		details = "DevNotification: " + notification_name + " - " + details;
		global::Debug.Log(details);
		int hashValue = new HashedString(notification_name).HashValue;
		bool hasReportedError = KCrashReporter.hasReportedError;
		if (!KCrashReporter.previouslyReportedDevNotifications.Contains(hashValue))
		{
			KCrashReporter.previouslyReportedDevNotifications.Add(hashValue);
			KCrashReporter.ReportError("DevNotification: " + notification_name, stack_trace, null, null, null, details);
		}
		KCrashReporter.hasReportedError = hasReportedError;
	}

	// Token: 0x06003A75 RID: 14965 RVA: 0x00143EA4 File Offset: 0x001420A4
	public static void ReportError(string msg, string stack_trace, string save_file_hash, ConfirmDialogScreen confirm_prefab, GameObject confirm_parent, string userMessage = "")
	{
		if (KCrashReporter.ignoreAll)
		{
			return;
		}
		global::Debug.Log("Reporting error.\n");
		if (msg != null)
		{
			global::Debug.Log(msg);
		}
		if (stack_trace != null)
		{
			global::Debug.Log(stack_trace);
		}
		KCrashReporter.hasReportedError = true;
		if (KPrivacyPrefs.instance.disableDataCollection)
		{
			return;
		}
		string text7;
		using (WebClient webClient = new WebClient())
		{
			webClient.Encoding = Encoding.UTF8;
			if (string.IsNullOrEmpty(msg))
			{
				msg = "No message";
			}
			Match match = KCrashReporter.failedToLoadModuleRegEx.Match(msg);
			if (match.Success)
			{
				string text = match.Groups[1].ToString();
				string text2 = match.Groups[2].ToString();
				string fileName = Path.GetFileName(text);
				msg = string.Concat(new string[] { "Failed to load '", fileName, "' with error '", text2, "'." });
			}
			if (string.IsNullOrEmpty(stack_trace))
			{
				string buildText = BuildWatermark.GetBuildText();
				stack_trace = string.Format("No stack trace {0}\n\n{1}", buildText, msg);
			}
			List<string> list = new List<string>();
			if (KCrashReporter.debugWasUsed)
			{
				list.Add("(Debug Used)");
			}
			if (KCrashReporter.haveActiveMods)
			{
				list.Add("(Mods Active)");
			}
			list.Add(msg);
			string[] array = new string[] { "Debug:LogError", "UnityEngine.Debug", "Output:LogError", "DebugUtil:Assert", "System.Array", "System.Collections", "KCrashReporter.Assert", "No stack trace." };
			foreach (string text3 in stack_trace.Split(new char[] { '\n' }))
			{
				if (list.Count >= 5)
				{
					break;
				}
				if (!string.IsNullOrEmpty(text3))
				{
					bool flag = false;
					foreach (string text4 in array)
					{
						if (text3.StartsWith(text4))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(text3);
					}
				}
			}
			if (userMessage == UI.CRASHSCREEN.BODY.text || userMessage.IsNullOrWhiteSpace())
			{
				userMessage = "";
			}
			else
			{
				userMessage = "[" + BuildWatermark.GetBuildText() + "]" + userMessage;
				if (!string.IsNullOrEmpty(save_file_hash))
				{
					userMessage = userMessage + "\nsave_hash: " + save_file_hash;
				}
			}
			KCrashReporter.Error error = new KCrashReporter.Error();
			error.user = KCrashReporter.GetUserID();
			error.callstack = stack_trace;
			if (KCrashReporter.disableDeduping)
			{
				error.callstack = error.callstack + "\n" + Guid.NewGuid().ToString();
			}
			error.fullstack = string.Format("{0}\n\n{1}", msg, stack_trace);
			error.build = 544519;
			error.log = KCrashReporter.GetLogContents();
			error.summaryline = string.Join("\n", list.ToArray());
			error.user_message = userMessage;
			if (!string.IsNullOrEmpty(save_file_hash))
			{
				error.save_hash = save_file_hash;
			}
			if (DistributionPlatform.Initialized)
			{
				error.steam64_verified = DistributionPlatform.Inst.LocalUser.Id.ToInt64();
			}
			string text5 = JsonConvert.SerializeObject(error);
			string text6 = "";
			Uri uri = new Uri("http://crashes.klei.ca/submitCrash");
			global::Debug.Log("Submitting crash:");
			try
			{
				webClient.UploadStringAsync(uri, text5);
			}
			catch (Exception ex)
			{
				global::Debug.Log(ex);
			}
			if (confirm_prefab != null && confirm_parent != null)
			{
				((ConfirmDialogScreen)KScreenManager.Instance.StartScreen(confirm_prefab.gameObject, confirm_parent)).PopupConfirmDialog(UI.CRASHSCREEN.REPORTEDERROR, null, null, null, null, null, null, null, null);
			}
			text7 = text6;
		}
		if (KCrashReporter.onCrashReported != null)
		{
			KCrashReporter.onCrashReported(text7);
		}
	}

	// Token: 0x06003A76 RID: 14966 RVA: 0x00144278 File Offset: 0x00142478
	public static void ReportBug(string msg, string save_file, GameObject confirmParent)
	{
		string text = "Bug Report From: " + KCrashReporter.GetUserID() + " at " + System.DateTime.Now.ToString();
		string text2 = KCrashReporter.UploadSaveFile(save_file, text, new Dictionary<string, string> { 
		{
			"user",
			KCrashReporter.GetUserID()
		} });
		KCrashReporter.ReportError(msg, text, text2, ScreenPrefabs.Instance.ConfirmDialogScreen, confirmParent, "");
	}

	// Token: 0x06003A77 RID: 14967 RVA: 0x001442DC File Offset: 0x001424DC
	public static void Assert(bool condition, string message)
	{
		if (!condition && !KCrashReporter.hasReportedError)
		{
			StackTrace stackTrace = new StackTrace(1, true);
			KCrashReporter.ReportError("ASSERT: " + message, stackTrace.ToString(), null, null, null, "");
		}
	}

	// Token: 0x06003A78 RID: 14968 RVA: 0x0014431C File Offset: 0x0014251C
	public static void ReportSimDLLCrash(string msg, string stack_trace, string dmp_filename)
	{
		if (KCrashReporter.hasReportedError)
		{
			return;
		}
		string text = null;
		string text2 = null;
		string text3 = null;
		if (dmp_filename != null)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dmp_filename);
			text2 = Path.Combine(Path.GetDirectoryName(KCrashReporter.dataRoot), dmp_filename);
			text3 = Path.Combine(Path.GetDirectoryName(KCrashReporter.dataRoot), fileNameWithoutExtension + ".sav");
			File.Move(text2, text3);
			text = KCrashReporter.UploadSaveFile(text3, stack_trace, new Dictionary<string, string> { 
			{
				"user",
				KCrashReporter.GetUserID()
			} });
		}
		KCrashReporter.ReportError(msg, stack_trace, text, null, null, "");
		if (dmp_filename != null)
		{
			File.Move(text3, text2);
		}
	}

	// Token: 0x04002654 RID: 9812
	public static string MOST_RECENT_SAVEFILE = null;

	// Token: 0x04002655 RID: 9813
	public const string CRASH_REPORTER_SERVER = "http://crashes.klei.ca";

	// Token: 0x04002656 RID: 9814
	public const uint MAX_LOGS = 10000000U;

	// Token: 0x04002658 RID: 9816
	public static bool ignoreAll = false;

	// Token: 0x04002659 RID: 9817
	public static bool debugWasUsed = false;

	// Token: 0x0400265A RID: 9818
	public static bool haveActiveMods = false;

	// Token: 0x0400265B RID: 9819
	public static uint logCount = 0U;

	// Token: 0x0400265C RID: 9820
	public static string error_canvas_name = "ErrorCanvas";

	// Token: 0x0400265D RID: 9821
	private static bool disableDeduping = false;

	// Token: 0x0400265F RID: 9823
	public static bool hasCrash = false;

	// Token: 0x04002660 RID: 9824
	private static readonly Regex failedToLoadModuleRegEx = new Regex("^Failed to load '(.*?)' with error (.*)", RegexOptions.Multiline);

	// Token: 0x04002661 RID: 9825
	[SerializeField]
	private LoadScreen loadScreenPrefab;

	// Token: 0x04002662 RID: 9826
	[SerializeField]
	private GameObject reportErrorPrefab;

	// Token: 0x04002663 RID: 9827
	[SerializeField]
	private ConfirmDialogScreen confirmDialogPrefab;

	// Token: 0x04002664 RID: 9828
	private GameObject errorScreen;

	// Token: 0x04002665 RID: 9829
	public static bool terminateOnError = true;

	// Token: 0x04002666 RID: 9830
	private static string dataRoot;

	// Token: 0x04002667 RID: 9831
	private static readonly string[] IgnoreStrings = new string[] { "Releasing render texture whose render buffer is set as Camera's target buffer with Camera.SetTargetBuffers!", "The profiler has run out of samples for this frame. This frame will be skipped. Increase the sample limit using Profiler.maxNumberOfSamplesPerFrame", "Trying to add Text (LocText) for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.", "Texture has out of range width / height", "<I> Failed to get cursor position:\r\nSuccess.\r\n" };

	// Token: 0x04002668 RID: 9832
	private static HashSet<int> previouslyReportedDevNotifications;

	// Token: 0x0200153E RID: 5438
	private class Error
	{
		// Token: 0x040065FC RID: 26108
		public string game = "simgame";

		// Token: 0x040065FD RID: 26109
		public int build = -1;

		// Token: 0x040065FE RID: 26110
		public string platform = Environment.OSVersion.ToString();

		// Token: 0x040065FF RID: 26111
		public string user = "unknown";

		// Token: 0x04006600 RID: 26112
		public ulong steam64_verified;

		// Token: 0x04006601 RID: 26113
		public string callstack = "";

		// Token: 0x04006602 RID: 26114
		public string fullstack = "";

		// Token: 0x04006603 RID: 26115
		public string log = "";

		// Token: 0x04006604 RID: 26116
		public string summaryline = "";

		// Token: 0x04006605 RID: 26117
		public string user_message = "";

		// Token: 0x04006606 RID: 26118
		public bool is_server;

		// Token: 0x04006607 RID: 26119
		public bool is_dedicated;

		// Token: 0x04006608 RID: 26120
		public string save_hash = "";
	}
}
