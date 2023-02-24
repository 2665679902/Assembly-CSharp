using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

// Token: 0x0200003A RID: 58
public class KleiAccount : ThreadedHttps<KleiAccount>
{
	// Token: 0x0600027D RID: 637 RVA: 0x0000DEE2 File Offset: 0x0000C0E2
	public KleiAccount()
	{
		this.CLIENT_KEY = "ONI";
		this.LIVE_ENDPOINT = "login.kleientertainment.com" + DistributionPlatform.Inst.AccountLoginEndpoint;
		this.serviceName = "KleiAccount";
		this.ClearAuthTicket();
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000DF20 File Offset: 0x0000C120
	protected override void OnReplyRecieved(WebResponse response)
	{
		if (response == null)
		{
			KleiAccount.KleiUserID = null;
			KleiAccount.KleiToken = null;
			this.gotUserID();
			return;
		}
		Stream responseStream = response.GetResponseStream();
		StreamReader streamReader = new StreamReader(responseStream);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		responseStream.Close();
		KleiAccount.AccountReply accountReply = JsonConvert.DeserializeObject<KleiAccount.AccountReply>(text);
		if (!accountReply.Error)
		{
			Debug.Log("[Account] Got login for user " + accountReply.UserID);
			KleiAccount.KleiUserID = ((accountReply.UserID == "") ? null : accountReply.UserID);
			KleiAccount.KleiToken = ((accountReply.Token == "") ? null : accountReply.Token);
			this.gotUserID();
		}
		else
		{
			Debug.Log("[Account] Error logging in: " + text);
			this.gotUserID();
		}
		base.End();
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
	private string EncodeToAsciiHEX(byte[] data)
	{
		string text = "";
		for (int i = 0; i < data.Length; i++)
		{
			text += data[i].ToString("X2");
		}
		return text;
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000E034 File Offset: 0x0000C234
	public string PostRawData(Dictionary<string, object> data)
	{
		string text = JsonConvert.SerializeObject(data);
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		base.PutPacket(bytes, false);
		return "OK";
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000E064 File Offset: 0x0000C264
	public void AuthenticateUser(KleiAccount.GetUserIDdelegate cb, bool force = false)
	{
		if (KleiAccount.KleiUserID == null || force)
		{
			Debug.Log("[Account] Requesting auth ticket from " + DistributionPlatform.Inst.Name);
			this.gotUserID = cb;
			byte[] array = this.AuthTicket();
			if (array != null && array.Length != 0)
			{
				this.OnAuthTicketObtained(array);
				return;
			}
			if (DistributionPlatform.Initialized)
			{
				DistributionPlatform.Inst.GetAuthTicket(new DistributionPlatform.AuthTicketHandler(this.OnAuthTicketObtained));
				return;
			}
		}
		else
		{
			cb();
		}
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
	public void OnAuthTicketObtained(byte[] ticket)
	{
		if (ticket.Length != 0)
		{
			byte[] array = new byte[ticket.Length];
			Array.Copy(ticket, array, ticket.Length);
			this.SetAuthTicket(array);
			base.Start();
			Dictionary<string, object> dictionary = this.BuildLoginRequest(array);
			this.PostRawData(dictionary);
			return;
		}
		this.gotUserID();
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000E125 File Offset: 0x0000C325
	public byte[] AuthTicket()
	{
		return this.authTicket;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000E12D File Offset: 0x0000C32D
	public void SetAuthTicket(byte[] ticket)
	{
		this.authTicket = ticket;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000E136 File Offset: 0x0000C336
	public void ClearAuthTicket()
	{
		this.authTicket = null;
	}

	// Token: 0x06000286 RID: 646 RVA: 0x0000E13F File Offset: 0x0000C33F
	private Dictionary<string, object> BuildLoginRequest(byte[] ticket)
	{
		return new Dictionary<string, object>
		{
			{
				"SteamTicket",
				this.EncodeToAsciiHEX(ticket)
			},
			{ "Game", this.CLIENT_KEY },
			{ "NoEmail", true }
		};
	}

	// Token: 0x04000356 RID: 854
	public const string KleiAccountKey = "KleiAccount";

	// Token: 0x04000357 RID: 855
	private const string GameIDFieldName = "Game";

	// Token: 0x04000358 RID: 856
	private const string EmailFieldName = "NoEmail";

	// Token: 0x04000359 RID: 857
	private const string ErrorFieldName = "Error";

	// Token: 0x0400035A RID: 858
	private const string UserIDFieldName = "UserID";

	// Token: 0x0400035B RID: 859
	public static string KleiUserID;

	// Token: 0x0400035C RID: 860
	public static string KleiToken;

	// Token: 0x0400035D RID: 861
	private KleiAccount.GetUserIDdelegate gotUserID;

	// Token: 0x0400035E RID: 862
	private const string AuthTicketKey = "AUTH_TICKET";

	// Token: 0x0400035F RID: 863
	private byte[] authTicket;

	// Token: 0x04000360 RID: 864
	private const string TicketFieldName = "SteamTicket";

	// Token: 0x02000989 RID: 2441
	private struct AccountReply
	{
		// Token: 0x04002137 RID: 8503
		public string UserID;

		// Token: 0x04002138 RID: 8504
		public string Token;

		// Token: 0x04002139 RID: 8505
		public bool Error;

		// Token: 0x0400213A RID: 8506
		public string SupplementaryData;
	}

	// Token: 0x0200098A RID: 2442
	// (Invoke) Token: 0x060052FB RID: 21243
	public delegate void GetUserIDdelegate();
}
