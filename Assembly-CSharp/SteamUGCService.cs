using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;
using Steamworks;
using UnityEngine;

// Token: 0x020009DE RID: 2526
public class SteamUGCService : MonoBehaviour
{
	// Token: 0x170005A5 RID: 1445
	// (get) Token: 0x06004B77 RID: 19319 RVA: 0x001A742E File Offset: 0x001A562E
	public static SteamUGCService Instance
	{
		get
		{
			return SteamUGCService.instance;
		}
	}

	// Token: 0x06004B78 RID: 19320 RVA: 0x001A7438 File Offset: 0x001A5638
	private SteamUGCService()
	{
		this.on_subscribed = Callback<RemoteStoragePublishedFileSubscribed_t>.Create(new Callback<RemoteStoragePublishedFileSubscribed_t>.DispatchDelegate(this.OnItemSubscribed));
		this.on_unsubscribed = Callback<RemoteStoragePublishedFileUnsubscribed_t>.Create(new Callback<RemoteStoragePublishedFileUnsubscribed_t>.DispatchDelegate(this.OnItemUnsubscribed));
		this.on_updated = Callback<RemoteStoragePublishedFileUpdated_t>.Create(new Callback<RemoteStoragePublishedFileUpdated_t>.DispatchDelegate(this.OnItemUpdated));
		this.on_query_completed = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(this.OnSteamUGCQueryDetailsCompleted));
		this.mods = new List<SteamUGCService.Mod>();
	}

	// Token: 0x06004B79 RID: 19321 RVA: 0x001A7520 File Offset: 0x001A5720
	public static void Initialize()
	{
		if (SteamUGCService.instance != null)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("/SteamManager");
		SteamUGCService.instance = gameObject.GetComponent<SteamUGCService>();
		if (SteamUGCService.instance == null)
		{
			SteamUGCService.instance = gameObject.AddComponent<SteamUGCService>();
		}
	}

	// Token: 0x06004B7A RID: 19322 RVA: 0x001A756C File Offset: 0x001A576C
	public void AddClient(SteamUGCService.IClient client)
	{
		this.clients.Add(client);
		ListPool<PublishedFileId_t, SteamUGCService>.PooledList pooledList = ListPool<PublishedFileId_t, SteamUGCService>.Allocate();
		foreach (SteamUGCService.Mod mod in this.mods)
		{
			pooledList.Add(mod.fileId);
		}
		client.UpdateMods(pooledList, Enumerable.Empty<PublishedFileId_t>(), Enumerable.Empty<PublishedFileId_t>(), Enumerable.Empty<SteamUGCService.Mod>());
		pooledList.Recycle();
	}

	// Token: 0x06004B7B RID: 19323 RVA: 0x001A75F4 File Offset: 0x001A57F4
	public void RemoveClient(SteamUGCService.IClient client)
	{
		this.clients.Remove(client);
	}

	// Token: 0x06004B7C RID: 19324 RVA: 0x001A7604 File Offset: 0x001A5804
	public void Awake()
	{
		global::Debug.Assert(SteamUGCService.instance == null);
		SteamUGCService.instance = this;
		uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
		if (numSubscribedItems != 0U)
		{
			PublishedFileId_t[] array = new PublishedFileId_t[numSubscribedItems];
			SteamUGC.GetSubscribedItems(array, numSubscribedItems);
			this.downloads.UnionWith(array);
		}
	}

	// Token: 0x06004B7D RID: 19325 RVA: 0x001A764C File Offset: 0x001A584C
	public bool IsSubscribed(PublishedFileId_t item)
	{
		return this.downloads.Contains(item) || this.proxies.Contains(item) || this.queries.Contains(item) || this.publishes.Any((SteamUGCDetails_t candidate) => candidate.m_nPublishedFileId == item) || this.mods.Exists((SteamUGCService.Mod candidate) => candidate.fileId == item);
	}

	// Token: 0x06004B7E RID: 19326 RVA: 0x001A76D4 File Offset: 0x001A58D4
	public SteamUGCService.Mod FindMod(PublishedFileId_t item)
	{
		return this.mods.Find((SteamUGCService.Mod candidate) => candidate.fileId == item);
	}

	// Token: 0x06004B7F RID: 19327 RVA: 0x001A7705 File Offset: 0x001A5905
	private void OnDestroy()
	{
		global::Debug.Assert(SteamUGCService.instance == this);
		SteamUGCService.instance = null;
	}

	// Token: 0x06004B80 RID: 19328 RVA: 0x001A7720 File Offset: 0x001A5920
	private Texture2D LoadPreviewImage(SteamUGCDetails_t details)
	{
		byte[] array = null;
		if (details.m_hPreviewFile != UGCHandle_t.Invalid)
		{
			SteamRemoteStorage.UGCDownload(details.m_hPreviewFile, 0U);
			array = new byte[details.m_nPreviewFileSize];
			if (SteamRemoteStorage.UGCRead(details.m_hPreviewFile, array, details.m_nPreviewFileSize, 0U, EUGCReadAction.k_EUGCRead_ContinueReadingUntilFinished) != details.m_nPreviewFileSize)
			{
				if (this.retry_counts[details.m_nPublishedFileId] % 100 == 0)
				{
					global::Debug.LogFormat("Steam: Preview image load failed", Array.Empty<object>());
				}
				array = null;
			}
		}
		if (array == null)
		{
			System.DateTime dateTime;
			array = SteamUGCService.GetBytesFromZip(details.m_nPublishedFileId, SteamUGCService.previewFileNames, out dateTime, false);
		}
		Texture2D texture2D = null;
		if (array != null)
		{
			texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(array);
		}
		else
		{
			Dictionary<PublishedFileId_t, int> dictionary = this.retry_counts;
			PublishedFileId_t nPublishedFileId = details.m_nPublishedFileId;
			dictionary[nPublishedFileId]++;
		}
		return texture2D;
	}

	// Token: 0x06004B81 RID: 19329 RVA: 0x001A77F0 File Offset: 0x001A59F0
	private void Update()
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (Game.Instance != null)
		{
			return;
		}
		this.downloads.ExceptWith(this.removals);
		this.publishes.RemoveWhere((SteamUGCDetails_t publish) => this.removals.Contains(publish.m_nPublishedFileId));
		this.previews.RemoveWhere((SteamUGCDetails_t publish) => this.removals.Contains(publish.m_nPublishedFileId));
		this.proxies.ExceptWith(this.removals);
		HashSetPool<SteamUGCService.Mod, SteamUGCService>.PooledHashSet loaded_previews = HashSetPool<SteamUGCService.Mod, SteamUGCService>.Allocate();
		HashSetPool<PublishedFileId_t, SteamUGCService>.PooledHashSet cancelled_previews = HashSetPool<PublishedFileId_t, SteamUGCService>.Allocate();
		SteamUGCService.Mod mod;
		foreach (SteamUGCDetails_t steamUGCDetails_t in this.previews)
		{
			mod = this.FindMod(steamUGCDetails_t.m_nPublishedFileId);
			DebugUtil.DevAssert(mod != null, "expect mod with pending preview to be published", null);
			mod.previewImage = this.LoadPreviewImage(steamUGCDetails_t);
			if (mod.previewImage != null)
			{
				loaded_previews.Add(mod);
			}
			else if (1000 < this.retry_counts[steamUGCDetails_t.m_nPublishedFileId])
			{
				cancelled_previews.Add(mod.fileId);
			}
		}
		this.previews.RemoveWhere((SteamUGCDetails_t publish) => loaded_previews.Any((SteamUGCService.Mod mod) => mod.fileId == publish.m_nPublishedFileId) || cancelled_previews.Contains(publish.m_nPublishedFileId));
		cancelled_previews.Recycle();
		ListPool<SteamUGCService.Mod, SteamUGCService>.PooledList pooledList = ListPool<SteamUGCService.Mod, SteamUGCService>.Allocate();
		HashSetPool<PublishedFileId_t, SteamUGCService>.PooledHashSet published = HashSetPool<PublishedFileId_t, SteamUGCService>.Allocate();
		foreach (SteamUGCDetails_t steamUGCDetails_t2 in this.publishes)
		{
			if ((SteamUGC.GetItemState(steamUGCDetails_t2.m_nPublishedFileId) & 48U) == 0U)
			{
				global::Debug.LogFormat("Steam: updating info for mod {0}", new object[] { steamUGCDetails_t2.m_rgchTitle });
				SteamUGCService.Mod mod2 = new SteamUGCService.Mod(steamUGCDetails_t2, this.LoadPreviewImage(steamUGCDetails_t2));
				pooledList.Add(mod2);
				if (steamUGCDetails_t2.m_hPreviewFile != UGCHandle_t.Invalid && mod2.previewImage == null)
				{
					this.previews.Add(steamUGCDetails_t2);
				}
				published.Add(steamUGCDetails_t2.m_nPublishedFileId);
			}
		}
		this.publishes.RemoveWhere((SteamUGCDetails_t publish) => published.Contains(publish.m_nPublishedFileId));
		published.Recycle();
		foreach (PublishedFileId_t publishedFileId_t in this.proxies)
		{
			global::Debug.LogFormat("Steam: proxy mod {0}", new object[] { publishedFileId_t });
			pooledList.Add(new SteamUGCService.Mod(publishedFileId_t));
		}
		this.proxies.Clear();
		ListPool<PublishedFileId_t, SteamUGCService>.PooledList pooledList2 = ListPool<PublishedFileId_t, SteamUGCService>.Allocate();
		ListPool<PublishedFileId_t, SteamUGCService>.PooledList pooledList3 = ListPool<PublishedFileId_t, SteamUGCService>.Allocate();
		using (List<SteamUGCService.Mod>.Enumerator enumerator3 = pooledList.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				SteamUGCService.Mod mod = enumerator3.Current;
				int num = this.mods.FindIndex((SteamUGCService.Mod candidate) => candidate.fileId == mod.fileId);
				if (num == -1)
				{
					this.mods.Add(mod);
					pooledList2.Add(mod.fileId);
				}
				else
				{
					this.mods[num] = mod;
					pooledList3.Add(mod.fileId);
				}
			}
		}
		pooledList.Recycle();
		bool flag = this.details_query == UGCQueryHandle_t.Invalid;
		if (pooledList2.Count != 0 || pooledList3.Count != 0 || (flag && this.removals.Count != 0) || loaded_previews.Count != 0)
		{
			foreach (SteamUGCService.IClient client in this.clients)
			{
				IEnumerable<PublishedFileId_t> enumerable = pooledList2;
				IEnumerable<PublishedFileId_t> enumerable2 = pooledList3;
				IEnumerable<PublishedFileId_t> enumerable3;
				if (!flag)
				{
					enumerable3 = Enumerable.Empty<PublishedFileId_t>();
				}
				else
				{
					IEnumerable<PublishedFileId_t> enumerable4 = this.removals;
					enumerable3 = enumerable4;
				}
				client.UpdateMods(enumerable, enumerable2, enumerable3, loaded_previews);
			}
		}
		pooledList2.Recycle();
		pooledList3.Recycle();
		loaded_previews.Recycle();
		if (flag)
		{
			using (HashSet<PublishedFileId_t>.Enumerator enumerator2 = this.removals.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					PublishedFileId_t removal = enumerator2.Current;
					this.mods.RemoveAll((SteamUGCService.Mod candidate) => candidate.fileId == removal);
				}
			}
			this.removals.Clear();
		}
		foreach (PublishedFileId_t publishedFileId_t2 in this.downloads)
		{
			EItemState itemState = (EItemState)SteamUGC.GetItemState(publishedFileId_t2);
			if (((itemState & EItemState.k_EItemStateInstalled) == EItemState.k_EItemStateNone || (itemState & EItemState.k_EItemStateNeedsUpdate) != EItemState.k_EItemStateNone) && (itemState & (EItemState.k_EItemStateDownloading | EItemState.k_EItemStateDownloadPending)) == EItemState.k_EItemStateNone)
			{
				SteamUGC.DownloadItem(publishedFileId_t2, false);
			}
		}
		if (this.details_query == UGCQueryHandle_t.Invalid)
		{
			this.queries.UnionWith(this.downloads);
			this.downloads.Clear();
			if (this.queries.Count != 0)
			{
				this.details_query = SteamUGC.CreateQueryUGCDetailsRequest(this.queries.ToArray<PublishedFileId_t>(), (uint)this.queries.Count);
				SteamAPICall_t steamAPICall_t = SteamUGC.SendQueryUGCRequest(this.details_query);
				this.on_query_completed.Set(steamAPICall_t, null);
			}
		}
	}

	// Token: 0x06004B82 RID: 19330 RVA: 0x001A7DA8 File Offset: 0x001A5FA8
	private void OnSteamUGCQueryDetailsCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
	{
		EResult eResult = pCallback.m_eResult;
		if (eResult != EResult.k_EResultOK)
		{
			if (eResult != EResult.k_EResultBusy)
			{
				string[] array = new string[10];
				array[0] = "Steam: [OnSteamUGCQueryDetailsCompleted] - handle: ";
				int num = 1;
				UGCQueryHandle_t ugcqueryHandle_t = pCallback.m_handle;
				array[num] = ugcqueryHandle_t.ToString();
				array[2] = " -- Result: ";
				array[3] = pCallback.m_eResult.ToString();
				array[4] = " -- NUm results: ";
				array[5] = pCallback.m_unNumResultsReturned.ToString();
				array[6] = " --Total Matching: ";
				array[7] = pCallback.m_unTotalMatchingResults.ToString();
				array[8] = " -- cached: ";
				array[9] = pCallback.m_bCachedData.ToString();
				global::Debug.Log(string.Concat(array));
				HashSet<PublishedFileId_t> hashSet = this.proxies;
				this.proxies = this.queries;
				this.queries = hashSet;
			}
			else
			{
				string[] array2 = new string[5];
				array2[0] = "Steam: [OnSteamUGCQueryDetailsCompleted] - handle: ";
				int num2 = 1;
				UGCQueryHandle_t ugcqueryHandle_t = pCallback.m_handle;
				array2[num2] = ugcqueryHandle_t.ToString();
				array2[2] = " -- Result: ";
				array2[3] = pCallback.m_eResult.ToString();
				array2[4] = " Resending";
				global::Debug.Log(string.Concat(array2));
			}
		}
		else
		{
			for (uint num3 = 0U; num3 < pCallback.m_unNumResultsReturned; num3 += 1U)
			{
				SteamUGCDetails_t steamUGCDetails_t = default(SteamUGCDetails_t);
				SteamUGC.GetQueryUGCResult(this.details_query, num3, out steamUGCDetails_t);
				if (!this.removals.Contains(steamUGCDetails_t.m_nPublishedFileId))
				{
					this.publishes.Add(steamUGCDetails_t);
					this.retry_counts[steamUGCDetails_t.m_nPublishedFileId] = 0;
				}
				this.queries.Remove(steamUGCDetails_t.m_nPublishedFileId);
			}
		}
		SteamUGC.ReleaseQueryUGCRequest(this.details_query);
		this.details_query = UGCQueryHandle_t.Invalid;
	}

	// Token: 0x06004B83 RID: 19331 RVA: 0x001A7F5A File Offset: 0x001A615A
	private void OnItemSubscribed(RemoteStoragePublishedFileSubscribed_t pCallback)
	{
		this.downloads.Add(pCallback.m_nPublishedFileId);
	}

	// Token: 0x06004B84 RID: 19332 RVA: 0x001A7F6E File Offset: 0x001A616E
	private void OnItemUpdated(RemoteStoragePublishedFileUpdated_t pCallback)
	{
		this.downloads.Add(pCallback.m_nPublishedFileId);
	}

	// Token: 0x06004B85 RID: 19333 RVA: 0x001A7F82 File Offset: 0x001A6182
	private void OnItemUnsubscribed(RemoteStoragePublishedFileUnsubscribed_t pCallback)
	{
		this.removals.Add(pCallback.m_nPublishedFileId);
	}

	// Token: 0x06004B86 RID: 19334 RVA: 0x001A7F98 File Offset: 0x001A6198
	public static byte[] GetBytesFromZip(PublishedFileId_t item, string[] filesToExtract, out System.DateTime lastModified, bool getFirstMatch = false)
	{
		byte[] array = null;
		lastModified = System.DateTime.MinValue;
		ulong num;
		string text;
		uint num2;
		SteamUGC.GetItemInstallInfo(item, out num, out text, 1024U, out num2);
		try
		{
			lastModified = File.GetLastWriteTimeUtc(text);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipFile zipFile = ZipFile.Read(text))
				{
					ZipEntry zipEntry = null;
					foreach (string text2 in filesToExtract)
					{
						if (text2.Length > 4)
						{
							if (zipFile.ContainsEntry(text2))
							{
								zipEntry = zipFile[text2];
							}
						}
						else
						{
							foreach (ZipEntry zipEntry2 in zipFile.Entries)
							{
								if (zipEntry2.FileName.EndsWith(text2))
								{
									zipEntry = zipEntry2;
									break;
								}
							}
						}
						if (zipEntry != null)
						{
							break;
						}
					}
					if (zipEntry != null)
					{
						zipEntry.Extract(memoryStream);
						memoryStream.Flush();
						array = memoryStream.ToArray();
					}
				}
			}
		}
		catch (Exception)
		{
		}
		return array;
	}

	// Token: 0x0400316A RID: 12650
	private UGCQueryHandle_t details_query = UGCQueryHandle_t.Invalid;

	// Token: 0x0400316B RID: 12651
	private Callback<RemoteStoragePublishedFileSubscribed_t> on_subscribed;

	// Token: 0x0400316C RID: 12652
	private Callback<RemoteStoragePublishedFileUpdated_t> on_updated;

	// Token: 0x0400316D RID: 12653
	private Callback<RemoteStoragePublishedFileUnsubscribed_t> on_unsubscribed;

	// Token: 0x0400316E RID: 12654
	private CallResult<SteamUGCQueryCompleted_t> on_query_completed;

	// Token: 0x0400316F RID: 12655
	private HashSet<PublishedFileId_t> downloads = new HashSet<PublishedFileId_t>();

	// Token: 0x04003170 RID: 12656
	private HashSet<PublishedFileId_t> queries = new HashSet<PublishedFileId_t>();

	// Token: 0x04003171 RID: 12657
	private HashSet<PublishedFileId_t> proxies = new HashSet<PublishedFileId_t>();

	// Token: 0x04003172 RID: 12658
	private HashSet<SteamUGCDetails_t> publishes = new HashSet<SteamUGCDetails_t>();

	// Token: 0x04003173 RID: 12659
	private HashSet<PublishedFileId_t> removals = new HashSet<PublishedFileId_t>();

	// Token: 0x04003174 RID: 12660
	private HashSet<SteamUGCDetails_t> previews = new HashSet<SteamUGCDetails_t>();

	// Token: 0x04003175 RID: 12661
	private List<SteamUGCService.Mod> mods = new List<SteamUGCService.Mod>();

	// Token: 0x04003176 RID: 12662
	private Dictionary<PublishedFileId_t, int> retry_counts = new Dictionary<PublishedFileId_t, int>();

	// Token: 0x04003177 RID: 12663
	private static readonly string[] previewFileNames = new string[] { "preview.png", "Preview.png", "PREVIEW.png", ".png", ".jpg" };

	// Token: 0x04003178 RID: 12664
	private List<SteamUGCService.IClient> clients = new List<SteamUGCService.IClient>();

	// Token: 0x04003179 RID: 12665
	private static SteamUGCService instance;

	// Token: 0x0400317A RID: 12666
	private const EItemState DOWNLOADING_MASK = EItemState.k_EItemStateDownloading | EItemState.k_EItemStateDownloadPending;

	// Token: 0x0400317B RID: 12667
	private const int RETRY_THRESHOLD = 1000;

	// Token: 0x020017DF RID: 6111
	public class Mod
	{
		// Token: 0x06008C3A RID: 35898 RVA: 0x00301D1C File Offset: 0x002FFF1C
		public Mod(SteamUGCDetails_t item, Texture2D previewImage)
		{
			this.title = item.m_rgchTitle;
			this.description = item.m_rgchDescription;
			this.fileId = item.m_nPublishedFileId;
			this.lastUpdateTime = (ulong)item.m_rtimeUpdated;
			this.tags = new List<string>(item.m_rgchTags.Split(new char[] { ',' }));
			this.previewImage = previewImage;
		}

		// Token: 0x06008C3B RID: 35899 RVA: 0x00301D8B File Offset: 0x002FFF8B
		public Mod(PublishedFileId_t id)
		{
			this.title = string.Empty;
			this.description = string.Empty;
			this.fileId = id;
			this.lastUpdateTime = 0UL;
			this.tags = new List<string>();
			this.previewImage = null;
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06008C3C RID: 35900 RVA: 0x00301DCA File Offset: 0x002FFFCA
		// (set) Token: 0x06008C3D RID: 35901 RVA: 0x00301DD2 File Offset: 0x002FFFD2
		public string title { get; private set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06008C3E RID: 35902 RVA: 0x00301DDB File Offset: 0x002FFFDB
		// (set) Token: 0x06008C3F RID: 35903 RVA: 0x00301DE3 File Offset: 0x002FFFE3
		public string description { get; private set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06008C40 RID: 35904 RVA: 0x00301DEC File Offset: 0x002FFFEC
		// (set) Token: 0x06008C41 RID: 35905 RVA: 0x00301DF4 File Offset: 0x002FFFF4
		public PublishedFileId_t fileId { get; private set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06008C42 RID: 35906 RVA: 0x00301DFD File Offset: 0x002FFFFD
		// (set) Token: 0x06008C43 RID: 35907 RVA: 0x00301E05 File Offset: 0x00300005
		public ulong lastUpdateTime { get; private set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06008C44 RID: 35908 RVA: 0x00301E0E File Offset: 0x0030000E
		// (set) Token: 0x06008C45 RID: 35909 RVA: 0x00301E16 File Offset: 0x00300016
		public List<string> tags { get; private set; }

		// Token: 0x04006E4B RID: 28235
		public Texture2D previewImage;
	}

	// Token: 0x020017E0 RID: 6112
	public interface IClient
	{
		// Token: 0x06008C46 RID: 35910
		void UpdateMods(IEnumerable<PublishedFileId_t> added, IEnumerable<PublishedFileId_t> updated, IEnumerable<PublishedFileId_t> removed, IEnumerable<SteamUGCService.Mod> loaded_previews);
	}
}
