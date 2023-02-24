using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004DF RID: 1247
	public class Worlds
	{
		// Token: 0x060035C4 RID: 13764 RVA: 0x00075F3B File Offset: 0x0007413B
		public bool HasWorld(string name)
		{
			return name != null && this.worldCache.ContainsKey(name);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x00075F50 File Offset: 0x00074150
		public World GetWorldData(string name)
		{
			World world;
			if (!name.IsNullOrWhiteSpace() && this.worldCache.TryGetValue(name, out world))
			{
				return world;
			}
			return null;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x00075F78 File Offset: 0x00074178
		public List<string> GetNames()
		{
			return new List<string>(this.worldCache.Keys);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x00075F8A File Offset: 0x0007418A
		public static string GetWorldName(string path, string prefix)
		{
			return prefix + "worlds/" + Path.GetFileNameWithoutExtension(path);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x00075F9D File Offset: 0x0007419D
		public string GetIconFilename(string iconName)
		{
			if (!DlcManager.FeatureClusterSpaceEnabled())
			{
				return "Asteroid_sandstone";
			}
			return "asteroid_sandstone_start_kanim";
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x00075FB1 File Offset: 0x000741B1
		public void LoadReferencedWorlds(string path, string prefix, ISet<string> referencedWorlds, List<YamlIO.Error> errors)
		{
			this.UpdateWorldCache(path, prefix, referencedWorlds, errors);
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x00075FC0 File Offset: 0x000741C0
		private void UpdateWorldCache(string path, string prefix, ISet<string> referencedWorlds, List<YamlIO.Error> errors)
		{
			ListPool<FileHandle, Worlds>.PooledList pooledList = ListPool<FileHandle, Worlds>.Allocate();
			FileSystem.GetFiles(FileSystem.Normalize(Path.Combine(path, "worlds/")), "*.yaml", pooledList);
			YamlIO.ErrorHandler <>9__0;
			foreach (FileHandle fileHandle in pooledList)
			{
				string text = fileHandle.full_path.Substring(path.Length);
				text = text.Remove(text.LastIndexOf(".yaml"));
				string text2 = prefix + text;
				if (referencedWorlds.Contains(text2))
				{
					string full_path = fileHandle.full_path;
					YamlIO.ErrorHandler errorHandler;
					if ((errorHandler = <>9__0) == null)
					{
						errorHandler = (<>9__0 = delegate(YamlIO.Error error, bool force_log_as_warning)
						{
							errors.Add(error);
						});
					}
					World world = YamlIO.LoadFile<World>(full_path, errorHandler, null);
					if (world == null)
					{
						DebugUtil.LogWarningArgs(new object[] { "Failed to load world: ", fileHandle.full_path });
					}
					else if (world.skip != World.Skip.Always && (world.skip != World.Skip.EditorOnly || Application.isEditor))
					{
						world.filePath = Worlds.GetWorldName(fileHandle.full_path, prefix);
						this.worldCache[world.filePath] = world;
					}
				}
			}
			pooledList.Recycle();
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x00076118 File Offset: 0x00074318
		public void Validate()
		{
			foreach (KeyValuePair<string, World> keyValuePair in this.worldCache)
			{
				keyValuePair.Value.Validate();
			}
		}

		// Token: 0x040012EF RID: 4847
		public Dictionary<string, World> worldCache = new Dictionary<string, World>();
	}
}
