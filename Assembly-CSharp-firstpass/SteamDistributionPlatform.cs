using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000040 RID: 64
internal class SteamDistributionPlatform : MonoBehaviour, DistributionPlatform.Implementation
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060002DD RID: 733 RVA: 0x00010099 File Offset: 0x0000E299
	public bool Initialized
	{
		get
		{
			return SteamManager.Initialized;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060002DE RID: 734 RVA: 0x000100A0 File Offset: 0x0000E2A0
	public string Name
	{
		get
		{
			return "Steam";
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060002DF RID: 735 RVA: 0x000100A7 File Offset: 0x0000E2A7
	public string Platform
	{
		get
		{
			return "Steam";
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060002E0 RID: 736 RVA: 0x000100AE File Offset: 0x0000E2AE
	public string AccountLoginEndpoint
	{
		get
		{
			return "/login/LoginViaSteam";
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060002E1 RID: 737 RVA: 0x000100B5 File Offset: 0x0000E2B5
	public string MetricsClientKey
	{
		get
		{
			return "2Ehpf6QcWdCXV8eqbbiJBkrqD6xc8waX";
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x000100BC File Offset: 0x0000E2BC
	public string MetricsUserIDField
	{
		get
		{
			return "SteamUserID";
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060002E3 RID: 739 RVA: 0x000100C3 File Offset: 0x0000E2C3
	public DistributionPlatform.User LocalUser
	{
		get
		{
			if (this.mLocalUser == null)
			{
				this.InitializeLocalUser();
			}
			return this.mLocalUser;
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x000100DC File Offset: 0x0000E2DC
	public bool IsArchiveBranch
	{
		get
		{
			string text;
			SteamApps.GetCurrentBetaName(out text, 100);
			global::Debug.Log("Checking which steam branch we're on. Got: [" + text + "]");
			return !(text == "") && !(text == "default") && !(text == "release");
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060002E5 RID: 741 RVA: 0x00010134 File Offset: 0x0000E334
	public bool IsPreviousVersionBranch
	{
		get
		{
			string text;
			SteamApps.GetCurrentBetaName(out text, 100);
			return text == "public_previous_update";
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00010156 File Offset: 0x0000E356
	public bool IsDLCStatusReady()
	{
		return true;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00010159 File Offset: 0x0000E359
	public string ApplyWordFilter(string text)
	{
		return text;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0001015C File Offset: 0x0000E35C
	public void GetAuthTicket(DistributionPlatform.AuthTicketHandler handler)
	{
		uint num = 0U;
		byte[] array = new byte[2048];
		Steamworks.SteamUser.GetAuthSessionTicket(array, array.Length, out num);
		byte[] array2 = new byte[num];
		if (0U < num)
		{
			Array.Copy(array, array2, (long)((ulong)num));
		}
		handler(array2);
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x000101A0 File Offset: 0x0000E3A0
	public bool IsDLCPurchased(string dlcID)
	{
		bool purchasedDLC = false;
		if (SteamManager.Initialized)
		{
			uint steamDlcID = this.DLCtoSteamIDMap[dlcID];
			this.GetAuthTicket(delegate(byte[] ticket)
			{
				CSteamID steamID = Steamworks.SteamUser.GetSteamID();
				Steamworks.SteamUser.BeginAuthSession(ticket, ticket.Length, steamID);
				EUserHasLicenseForAppResult euserHasLicenseForAppResult = Steamworks.SteamUser.UserHasLicenseForApp(steamID, new AppId_t(steamDlcID));
				purchasedDLC = euserHasLicenseForAppResult == EUserHasLicenseForAppResult.k_EUserHasLicenseResultHasLicense;
				Steamworks.SteamUser.EndAuthSession(steamID);
			});
		}
		else if (Application.isEditor)
		{
			purchasedDLC = true;
		}
		return purchasedDLC;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x000101FC File Offset: 0x0000E3FC
	public bool IsDLCSubscribed(string dlcID)
	{
		uint num = this.DLCtoSteamIDMap[dlcID];
		if (SteamManager.Initialized)
		{
			return SteamApps.BIsSubscribedApp(new AppId_t(num));
		}
		return Application.isEditor;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00010234 File Offset: 0x0000E434
	public void ToggleDLCSubscription(string dlcID)
	{
		global::Debug.Log("Steam: Toggling DLC " + dlcID);
		if (this.IsDLCPurchased(dlcID))
		{
			if (this.IsDLCSubscribed(dlcID))
			{
				SteamApps.UninstallDLC(new AppId_t(1452490U));
				global::Debug.Log("Switching to base game");
			}
			else
			{
				SteamApps.InstallDLC(new AppId_t(1452490U));
				global::Debug.Log("Switching to " + dlcID);
			}
			SteamApps.MarkContentCorrupt(false);
			Application.OpenURL("steam://rungameid/" + 457140U.ToString());
			App.Quit();
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x000102C8 File Offset: 0x0000E4C8
	private void InitializeLocalUser()
	{
		if (SteamManager.Initialized)
		{
			CSteamID steamID = Steamworks.SteamUser.GetSteamID();
			string personaName = SteamFriends.GetPersonaName();
			this.mLocalUser = new SteamDistributionPlatform.SteamUser(steamID, personaName);
		}
	}

	// Token: 0x040003A7 RID: 935
	private SteamDistributionPlatform.SteamUser mLocalUser;

	// Token: 0x040003A8 RID: 936
	private Dictionary<string, uint> DLCtoSteamIDMap = new Dictionary<string, uint> { { "EXPANSION1_ID", 1452490U } };

	// Token: 0x0200099E RID: 2462
	public class SteamUserId : DistributionPlatform.UserId
	{
		// Token: 0x06005332 RID: 21298 RVA: 0x0009B7F8 File Offset: 0x000999F8
		public SteamUserId(CSteamID id)
		{
			this.mSteamId = id;
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0009B807 File Offset: 0x00099A07
		public override string ToString()
		{
			return this.mSteamId.ToString();
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0009B81A File Offset: 0x00099A1A
		public override ulong ToInt64()
		{
			return this.mSteamId.m_SteamID;
		}

		// Token: 0x0400215C RID: 8540
		private CSteamID mSteamId;
	}

	// Token: 0x0200099F RID: 2463
	public class SteamUser : DistributionPlatform.User
	{
		// Token: 0x06005335 RID: 21301 RVA: 0x0009B827 File Offset: 0x00099A27
		public SteamUser(CSteamID id, string name)
		{
			this.mId = new SteamDistributionPlatform.SteamUserId(id);
			this.mName = name;
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x0009B842 File Offset: 0x00099A42
		public override DistributionPlatform.UserId Id
		{
			get
			{
				return this.mId;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06005337 RID: 21303 RVA: 0x0009B84A File Offset: 0x00099A4A
		public override string Name
		{
			get
			{
				return this.mName;
			}
		}

		// Token: 0x0400215D RID: 8541
		private SteamDistributionPlatform.SteamUserId mId;

		// Token: 0x0400215E RID: 8542
		private string mName;
	}
}
