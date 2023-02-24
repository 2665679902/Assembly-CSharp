using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000D06 RID: 3334
	internal static class DLLLoader
	{
		// Token: 0x0600673B RID: 26427 RVA: 0x0027D530 File Offset: 0x0027B730
		public static bool LoadUserModLoaderDLL()
		{
			try
			{
				string text = Path.Combine(Path.Combine(Application.dataPath, "Managed"), "ModLoader.dll");
				if (!File.Exists(text))
				{
					return false;
				}
				Assembly assembly = Assembly.LoadFile(text);
				if (assembly == null)
				{
					return false;
				}
				Type type = assembly.GetType("ModLoader.ModLoader");
				if (type == null)
				{
					return false;
				}
				MethodInfo method = type.GetMethod("Start");
				if (method == null)
				{
					return false;
				}
				method.Invoke(null, null);
				global::Debug.Log("Successfully started ModLoader.dll");
				return true;
			}
			catch (Exception ex)
			{
				global::Debug.Log(ex.ToString());
			}
			return false;
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x0027D5E8 File Offset: 0x0027B7E8
		public static LoadedModData LoadDLLs(Mod ownerMod, string harmonyId, string path, bool isDev)
		{
			LoadedModData loadedModData = new LoadedModData();
			LoadedModData loadedModData2;
			try
			{
				if (Testing.dll_loading == Testing.DLLLoading.Fail)
				{
					loadedModData2 = null;
				}
				else if (Testing.dll_loading == Testing.DLLLoading.UseModLoaderDLLExclusively)
				{
					loadedModData2 = null;
				}
				else
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					if (!directoryInfo.Exists)
					{
						loadedModData2 = null;
					}
					else
					{
						List<Assembly> list = new List<Assembly>();
						foreach (FileInfo fileInfo in directoryInfo.GetFiles())
						{
							if (fileInfo.Name.ToLower().EndsWith(".dll"))
							{
								global::Debug.Log(string.Format("Loading MOD dll: {0}", fileInfo.Name));
								Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
								if (assembly != null)
								{
									list.Add(assembly);
								}
							}
						}
						if (list.Count == 0)
						{
							loadedModData2 = null;
						}
						else
						{
							loadedModData.dlls = new HashSet<Assembly>();
							loadedModData.userMod2Instances = new Dictionary<Assembly, UserMod2>();
							foreach (Assembly assembly2 in list)
							{
								loadedModData.dlls.Add(assembly2);
								UserMod2 userMod = null;
								foreach (Type type in assembly2.GetTypes())
								{
									if (!(type == null) && typeof(UserMod2).IsAssignableFrom(type))
									{
										if (userMod != null)
										{
											global::Debug.LogError("Found more than one class inheriting `UserMod2` in " + assembly2.FullName + ", only one per assembly is allowed. Aborting load.");
											return null;
										}
										userMod = Activator.CreateInstance(type) as UserMod2;
									}
								}
								if (userMod == null)
								{
									if (isDev)
									{
										global::Debug.LogWarning(string.Format("{0} at {1} has no classes inheriting from UserMod, creating one...", assembly2.GetName(), path));
									}
									userMod = new UserMod2();
								}
								userMod.assembly = assembly2;
								userMod.path = path;
								userMod.mod = ownerMod;
								loadedModData.userMod2Instances[assembly2] = userMod;
							}
							loadedModData.harmony = new Harmony(harmonyId);
							if (loadedModData.harmony != null)
							{
								foreach (KeyValuePair<Assembly, UserMod2> keyValuePair in loadedModData.userMod2Instances)
								{
									keyValuePair.Value.OnLoad(loadedModData.harmony);
								}
							}
							loadedModData.patched_methods = (from method in loadedModData.harmony.GetPatchedMethods()
								where Harmony.GetPatchInfo(method).Owners.Contains(harmonyId)
								select method).ToList<MethodBase>();
							loadedModData2 = loadedModData;
						}
					}
				}
			}
			catch (Exception ex)
			{
				DebugUtil.LogException(null, string.Concat(new string[] { "Exception while loading mod ", harmonyId, " at ", path, "." }), ex);
				loadedModData2 = null;
			}
			return loadedModData2;
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x0027D8F4 File Offset: 0x0027BAF4
		public static void PostLoadDLLs(string harmonyId, LoadedModData modData, IReadOnlyList<Mod> mods)
		{
			try
			{
				foreach (KeyValuePair<Assembly, UserMod2> keyValuePair in modData.userMod2Instances)
				{
					keyValuePair.Value.OnAllModsLoaded(modData.harmony, mods);
				}
			}
			catch (Exception ex)
			{
				DebugUtil.LogException(null, "Exception while postLoading mod " + harmonyId + ".", ex);
			}
		}

		// Token: 0x04004BD2 RID: 19410
		private const string managed_path = "Managed";
	}
}
