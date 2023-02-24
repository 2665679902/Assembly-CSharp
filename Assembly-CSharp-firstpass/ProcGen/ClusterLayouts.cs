using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004E0 RID: 1248
	public class ClusterLayouts
	{
		// Token: 0x060035CD RID: 13773 RVA: 0x00076184 File Offset: 0x00074384
		public ClusterLayout GetClusterData(string name)
		{
			ClusterLayout clusterLayout;
			if (this.clusterCache.TryGetValue(name, out clusterLayout))
			{
				return clusterLayout;
			}
			return null;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000761A4 File Offset: 0x000743A4
		public Dictionary<string, string> GetStartingBaseNames()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, ClusterLayout> keyValuePair in this.clusterCache)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value.worldPlacements[keyValuePair.Value.startWorldIndex].world);
			}
			return dictionary;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00076228 File Offset: 0x00074428
		public List<string> GetNames()
		{
			return new List<string>(this.clusterCache.Keys);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x0007623A File Offset: 0x0007443A
		public void LoadFiles(string path, string addPrefix, List<YamlIO.Error> errors)
		{
			this.UpdateClusterCache(path, addPrefix, errors);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00076248 File Offset: 0x00074448
		private void UpdateClusterCache(string path, string addPrefix, List<YamlIO.Error> errors)
		{
			ListPool<FileHandle, Worlds>.PooledList pooledList = ListPool<FileHandle, Worlds>.Allocate();
			FileSystem.GetFiles(FileSystem.Normalize(Path.Combine(path, "clusters")), "*.yaml", pooledList);
			using (List<FileHandle>.Enumerator enumerator = pooledList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FileHandle cluster_file = enumerator.Current;
					ClusterLayout clusterLayout = YamlIO.LoadFile<ClusterLayout>(cluster_file.full_path, delegate(YamlIO.Error error, bool force_log_as_warning)
					{
						error.file = cluster_file;
						errors.Add(error);
					}, null);
					if (clusterLayout == null)
					{
						DebugUtil.LogWarningArgs(new object[] { "Failed to load cluster: ", cluster_file.full_path });
					}
					else if (clusterLayout.skip != ClusterLayout.Skip.Always && (clusterLayout.skip != ClusterLayout.Skip.EditorOnly || Application.isEditor) && (clusterLayout.requiredDlcId == null || DlcManager.IsContentActive(clusterLayout.requiredDlcId)) && (clusterLayout.forbiddenDlcId == null || !DlcManager.IsContentActive(clusterLayout.forbiddenDlcId)))
					{
						string name = ClusterLayout.GetName(cluster_file.full_path, addPrefix);
						clusterLayout.filePath = name;
						this.clusterCache[name] = clusterLayout;
					}
				}
			}
			pooledList.Recycle();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x00076398 File Offset: 0x00074598
		public World GetWorldData(string clusterID, int worldID)
		{
			WorldPlacement worldPlacement = this.GetClusterData(clusterID).worldPlacements[worldID];
			return SettingsCache.worlds.GetWorldData(worldPlacement.world);
		}

		// Token: 0x040012F0 RID: 4848
		public Dictionary<string, ClusterLayout> clusterCache = new Dictionary<string, ClusterLayout>();
	}
}
