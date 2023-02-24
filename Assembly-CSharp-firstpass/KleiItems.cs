using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Klei;
using Newtonsoft.Json;

// Token: 0x0200003B RID: 59
public class KleiItems : ThreadedHttps<KleiItems>
{
	// Token: 0x06000288 RID: 648 RVA: 0x0000E17C File Offset: 0x0000C37C
	public KleiItems()
	{
		this.serviceName = "KleiItems";
		KleiItems.InventoryData.AllItems = new List<KleiItems.Item>();
		KleiItems.InventoryData.ItemsByType = new Dictionary<string, List<KleiItems.Item>>();
		KleiItems.InventoryData.HasUnopenedItem = false;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
	public static IEnumerable<KleiItems.ItemData> IterateInventory(Dictionary<string, string> item_to_permit)
	{
		if (KleiItems.InventoryData.AllItems != null)
		{
			foreach (KleiItems.Item item in KleiItems.InventoryData.AllItems)
			{
				string text;
				if (item_to_permit.TryGetValue(item.ItemType, out text))
				{
					KleiItems.ItemData itemData;
					itemData.PermitId = text;
					itemData.ItemId = item.ItemId;
					yield return itemData;
				}
			}
			List<KleiItems.Item>.Enumerator enumerator = default(List<KleiItems.Item>.Enumerator);
		}
		yield break;
		yield break;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000E1EC File Offset: 0x0000C3EC
	public static bool HasItem(string itemType)
	{
		if (KleiItems.InventoryData.ItemsByType == null)
		{
			return false;
		}
		List<KleiItems.Item> list;
		if (!KleiItems.InventoryData.ItemsByType.TryGetValue(itemType, out list))
		{
			return false;
		}
		if (list != null)
		{
			return list.Count((KleiItems.Item x) => x.IsOpened) > 0;
		}
		return false;
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000E24C File Offset: 0x0000C44C
	public static int GetOwnedItemCount(string itemType)
	{
		if (KleiItems.InventoryData.ItemsByType == null)
		{
			return 0;
		}
		List<KleiItems.Item> list;
		if (!KleiItems.InventoryData.ItemsByType.TryGetValue(itemType, out list))
		{
			return 0;
		}
		if (list == null)
		{
			return 0;
		}
		return list.Count((KleiItems.Item x) => x.IsOpened);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000E2A7 File Offset: 0x0000C4A7
	public static bool HasUnopenedItem()
	{
		return KleiItems.InventoryData.HasUnopenedItem;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000E2B3 File Offset: 0x0000C4B3
	public static void AddRequestInventoryRefresh()
	{
		ThreadedHttps<KleiItems>.Instance.AddRequest(KleiItems.Request.RequestType.GetAllItems, null);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
	public static void AddInventoryRefreshCallback(KleiItems.InventoryRefreshCallback cb)
	{
		ThreadedHttps<KleiItems>.Instance.InventoryRefreshCbs.Add(cb);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000E2D3 File Offset: 0x0000C4D3
	public static void RemoveInventoryRefreshCallback(KleiItems.InventoryRefreshCallback cb)
	{
		ThreadedHttps<KleiItems>.Instance.InventoryRefreshCbs.Remove(cb);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
	public void Update()
	{
		if (this.RequestCompleted)
		{
			KleiItems.Request activeRequest = this.ActiveRequest;
			if (!string.IsNullOrEmpty(this.Response) && activeRequest.Type == KleiItems.Request.RequestType.GetAllItems)
			{
				this.OnInventoryRecieved(this.Response);
			}
			this.RequestStarted = false;
			this.RequestCompleted = false;
			this.ActiveRequest = default(KleiItems.Request);
			this.Response = null;
		}
		if (this.WaitForReAuthentication)
		{
			return;
		}
		if (!this.RequestStarted && this.Requests.Count > 0)
		{
			KleiItems.Request request = this.Requests[0];
			this.Requests.RemoveAt(0);
			bool flag = false;
			if (request.Type == KleiItems.Request.RequestType.GetAllItems)
			{
				flag = this.RetrieveInventory();
			}
			if (flag)
			{
				this.RequestStarted = true;
				this.ActiveRequest = request;
			}
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
	private void AddRequest(KleiItems.Request.RequestType type, object data)
	{
		KleiItems.Request request;
		request.Type = type;
		request.Data = data;
		if (!this.Requests.Contains(request))
		{
			this.Requests.Add(request);
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000E3D7 File Offset: 0x0000C5D7
	private void StartHttpsRequest(string url)
	{
		this.LIVE_ENDPOINT = url;
		base.Start();
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000E3E6 File Offset: 0x0000C5E6
	private void EndHttpsRequest()
	{
		base.End();
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
	protected override void OnReplyRecieved(WebResponse response)
	{
		string text = "";
		if (response != null)
		{
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			text = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
		}
		this.Response = text;
		this.RequestCompleted = true;
		this.EndHttpsRequest();
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000E438 File Offset: 0x0000C638
	private void HandleError(KleiItems.Request req, string errorCode)
	{
		if (errorCode == "E_EXPIRED_TOKEN" || errorCode == "E_INVALID_TOKEN")
		{
			this.WaitForReAuthentication = true;
			this.AddRequest(req.Type, req.Data);
			ThreadedHttps<KleiAccount>.Instance.AuthenticateUser(new KleiAccount.GetUserIDdelegate(this.OnAuthenticateComplete), true);
		}
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000E48F File Offset: 0x0000C68F
	private void OnAuthenticateComplete()
	{
		this.WaitForReAuthentication = false;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000E498 File Offset: 0x0000C698
	private bool RetrieveInventory()
	{
		string kleiToken = KleiAccount.KleiToken;
		if (string.IsNullOrEmpty(kleiToken))
		{
			return false;
		}
		this.StartHttpsRequest(KleiItemsConfig.SERVER_URL + "clientitems/ONI/GetAllItems");
		string text = JsonConvert.SerializeObject(new Dictionary<string, object> { { "ClientToken", kleiToken } });
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		base.PutPacket(bytes, false);
		return true;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
	private void OnInventoryRecieved(string response)
	{
		KleiItems.InventoryReply inventoryReply = JsonConvert.DeserializeObject<KleiItems.InventoryReply>(response);
		if (!inventoryReply.Error)
		{
			KleiItems.InventoryData.AllItems.Clear();
			KleiItems.InventoryData.ItemsByType.Clear();
			KleiItems.InventoryData.HasUnopenedItem = false;
			if (inventoryReply.Items != null)
			{
				for (int i = 0; i < inventoryReply.Items.Length; i++)
				{
					KleiItems.InventoryReply.Item item = inventoryReply.Items[i];
					KleiItems.Item item2;
					item2.ItemId = item.ItemID;
					item2.ItemType = item.ItemType;
					item2.IsOpened = item.Context != 3;
					KleiItems.InventoryData.AllItems.Add(item2);
					List<KleiItems.Item> list;
					if (KleiItems.InventoryData.ItemsByType.TryGetValue(item2.ItemType, out list))
					{
						list.Add(item2);
					}
					else
					{
						KleiItems.InventoryData.ItemsByType[item2.ItemType] = new List<KleiItems.Item> { item2 };
					}
					KleiItems.InventoryData.HasUnopenedItem = KleiItems.InventoryData.HasUnopenedItem | !item2.IsOpened;
				}
			}
			this.SaveInventoryCache();
			using (List<KleiItems.InventoryRefreshCallback>.Enumerator enumerator = this.InventoryRefreshCbs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KleiItems.InventoryRefreshCallback inventoryRefreshCallback = enumerator.Current;
					inventoryRefreshCallback();
				}
				return;
			}
		}
		this.HandleError(this.ActiveRequest, inventoryReply.ErrorCode);
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000E668 File Offset: 0x0000C868
	public static uint hash(string s, uint seed = 0U)
	{
		uint num = seed;
		for (int i = 0; i < s.Length; i++)
		{
			num = (uint)s[i] + (num << 6) + (num << 16) - num;
		}
		return num;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000E69C File Offset: 0x0000C89C
	private string GetCachePath(string userId)
	{
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		string text = Path.Combine(Util.RootFolder(), Util.GetKleiItemUserDataFolderName());
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return Path.Combine(text, userId + ".json");
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
	private void SaveInventoryCache()
	{
		if (!DistributionPlatform.Initialized)
		{
			return;
		}
		string text = DistributionPlatform.Inst.LocalUser.Id.ToString();
		KleiItems.InventoryCache inventoryCache;
		inventoryCache.items = new SortedDictionary<ulong, KleiItems.Item>();
		foreach (KleiItems.Item item in KleiItems.InventoryData.AllItems)
		{
			inventoryCache.items[item.ItemId] = item;
		}
		inventoryCache.checksum = KleiItems.hash(text, 0U);
		foreach (KeyValuePair<ulong, KleiItems.Item> keyValuePair in inventoryCache.items)
		{
			KleiItems.Item value = keyValuePair.Value;
			inventoryCache.checksum = KleiItems.hash(value.ItemId.ToString(), inventoryCache.checksum);
			inventoryCache.checksum = KleiItems.hash(value.ItemType, inventoryCache.checksum);
			inventoryCache.checksum = KleiItems.hash(value.IsOpened.ToString(), inventoryCache.checksum);
		}
		string file_path = this.GetCachePath(text);
		string data = JsonConvert.SerializeObject(inventoryCache);
		FileUtil.DoIODialog(delegate
		{
			using (FileStream fileStream = File.Open(file_path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			{
				byte[] bytes = new ASCIIEncoding().GetBytes(data);
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}, file_path, 0);
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000E864 File Offset: 0x0000CA64
	public void LoadInventoryCache()
	{
		if (!DistributionPlatform.Inst.Initialized)
		{
			return;
		}
		string userId = DistributionPlatform.Inst.LocalUser.Id.ToString();
		string file_path = this.GetCachePath(userId);
		if (File.Exists(file_path))
		{
			FileUtil.DoIODialog(delegate
			{
				KleiItems.InventoryData.AllItems.Clear();
				KleiItems.InventoryData.ItemsByType.Clear();
				KleiItems.InventoryData.HasUnopenedItem = false;
				string text = File.ReadAllText(file_path);
				KleiItems.InventoryCache inventoryCache;
				try
				{
					inventoryCache = JsonConvert.DeserializeObject<KleiItems.InventoryCache>(text);
				}
				catch (JsonSerializationException)
				{
					return;
				}
				uint num = KleiItems.hash(userId, 0U);
				foreach (KeyValuePair<ulong, KleiItems.Item> keyValuePair in inventoryCache.items)
				{
					KleiItems.Item value = keyValuePair.Value;
					num = KleiItems.hash(value.ItemId.ToString(), num);
					num = KleiItems.hash(value.ItemType, num);
					num = KleiItems.hash(value.IsOpened.ToString(), num);
				}
				if (num != inventoryCache.checksum)
				{
					inventoryCache.items.Clear();
				}
				foreach (KeyValuePair<ulong, KleiItems.Item> keyValuePair2 in inventoryCache.items)
				{
					KleiItems.Item value2 = keyValuePair2.Value;
					KleiItems.InventoryData.AllItems.Add(value2);
					List<KleiItems.Item> list;
					if (KleiItems.InventoryData.ItemsByType.TryGetValue(value2.ItemType, out list))
					{
						list.Add(value2);
					}
					else
					{
						KleiItems.InventoryData.ItemsByType[value2.ItemType] = new List<KleiItems.Item> { value2 };
					}
					KleiItems.InventoryData.HasUnopenedItem = KleiItems.InventoryData.HasUnopenedItem | !value2.IsOpened;
				}
			}, file_path, 0);
		}
	}

	// Token: 0x04000361 RID: 865
	private static KleiItems.Inventory InventoryData;

	// Token: 0x04000362 RID: 866
	private List<KleiItems.Request> Requests = new List<KleiItems.Request>();

	// Token: 0x04000363 RID: 867
	private bool RequestStarted;

	// Token: 0x04000364 RID: 868
	private bool RequestCompleted;

	// Token: 0x04000365 RID: 869
	private KleiItems.Request ActiveRequest;

	// Token: 0x04000366 RID: 870
	private string Response;

	// Token: 0x04000367 RID: 871
	private bool WaitForReAuthentication;

	// Token: 0x04000368 RID: 872
	private List<KleiItems.InventoryRefreshCallback> InventoryRefreshCbs = new List<KleiItems.InventoryRefreshCallback>();

	// Token: 0x0200098B RID: 2443
	public struct ItemData
	{
		// Token: 0x0400213B RID: 8507
		public string PermitId;

		// Token: 0x0400213C RID: 8508
		public ulong ItemId;
	}

	// Token: 0x0200098C RID: 2444
	private struct Item
	{
		// Token: 0x0400213D RID: 8509
		public string ItemType;

		// Token: 0x0400213E RID: 8510
		public ulong ItemId;

		// Token: 0x0400213F RID: 8511
		public bool IsOpened;
	}

	// Token: 0x0200098D RID: 2445
	private struct Inventory
	{
		// Token: 0x04002140 RID: 8512
		public List<KleiItems.Item> AllItems;

		// Token: 0x04002141 RID: 8513
		public Dictionary<string, List<KleiItems.Item>> ItemsByType;

		// Token: 0x04002142 RID: 8514
		public bool HasUnopenedItem;
	}

	// Token: 0x0200098E RID: 2446
	// (Invoke) Token: 0x060052FF RID: 21247
	public delegate void InventoryRefreshCallback();

	// Token: 0x0200098F RID: 2447
	private struct Request
	{
		// Token: 0x04002143 RID: 8515
		public KleiItems.Request.RequestType Type;

		// Token: 0x04002144 RID: 8516
		public object Data;

		// Token: 0x02000B42 RID: 2882
		public enum RequestType
		{
			// Token: 0x0400268A RID: 9866
			GetAllItems
		}
	}

	// Token: 0x02000990 RID: 2448
	private struct InventoryReply
	{
		// Token: 0x04002145 RID: 8517
		public bool Error;

		// Token: 0x04002146 RID: 8518
		public string ErrorCode;

		// Token: 0x04002147 RID: 8519
		public KleiItems.InventoryReply.Item[] Items;

		// Token: 0x02000B43 RID: 2883
		public struct Item
		{
			// Token: 0x0400268B RID: 9867
			public ulong ItemID;

			// Token: 0x0400268C RID: 9868
			public string ItemType;

			// Token: 0x0400268D RID: 9869
			public int Context;
		}
	}

	// Token: 0x02000991 RID: 2449
	private struct InventoryCache
	{
		// Token: 0x04002148 RID: 8520
		public SortedDictionary<ulong, KleiItems.Item> items;

		// Token: 0x04002149 RID: 8521
		public uint checksum;
	}

	// Token: 0x02000992 RID: 2450
	private static class ItemLogger
	{
		// Token: 0x06005302 RID: 21250 RVA: 0x0009B3BD File Offset: 0x000995BD
		[Conditional("UNITY_EDITOR")]
		public static void LogInfo(string header, string payload)
		{
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x0009B3BF File Offset: 0x000995BF
		[Conditional("UNITY_EDITOR")]
		public static void LogRequest(KleiItems.Request.RequestType service, string payload)
		{
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x0009B3C1 File Offset: 0x000995C1
		[Conditional("UNITY_EDITOR")]
		public static void LogResponse(KleiItems.Request.RequestType service, string response)
		{
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0009B3C3 File Offset: 0x000995C3
		[Conditional("UNITY_EDITOR")]
		public static void LogError(KleiItems.Request.RequestType service, string errorCode)
		{
		}
	}
}
