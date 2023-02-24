using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Klei;
using STRINGS;

namespace Database
{
	// Token: 0x02000C93 RID: 3219
	public class EquippableFacades : ResourceSet<EquippableFacadeResource>
	{
		// Token: 0x06006580 RID: 25984 RVA: 0x0026ABBC File Offset: 0x00268DBC
		public EquippableFacades(ResourceSet parent)
			: base("EquippableFacades", parent)
		{
			base.Initialize();
			foreach (EquippableFacades.Info info in EquippableFacades.Infos_All)
			{
				this.Add(info.id, info.name, info.defID, info.buildOverride, info.animFile);
			}
			this.Load();
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x0026AC24 File Offset: 0x00268E24
		public void Load()
		{
			ListPool<YamlIO.Error, EquippableFacadeResource>.PooledList errors = ListPool<YamlIO.Error, EquippableFacadeResource>.Allocate();
			List<FileHandle> list = new List<FileHandle>();
			DirectoryInfo directoryInfo = new DirectoryInfo(FileSystem.Normalize(Path.Combine(new string[] { Db.GetPath("", "equippablefacades") })));
			if (directoryInfo.Exists)
			{
				YamlIO.ErrorHandler <>9__0;
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					list.Clear();
					FileSystem.GetFiles(directoryInfo2.FullName, "*.yaml", list);
					foreach (FileHandle fileHandle in list)
					{
						YamlIO.ErrorHandler errorHandler;
						if ((errorHandler = <>9__0) == null)
						{
							errorHandler = (<>9__0 = delegate(YamlIO.Error error, bool force_log_as_warning)
							{
								errors.Add(error);
							});
						}
						EquippableFacadeInfo equippableFacadeInfo = YamlIO.LoadFile<EquippableFacadeInfo>(fileHandle, errorHandler, null);
						DebugUtil.DevAssert(string.Equals(directoryInfo2.Name, equippableFacadeInfo.defID, StringComparison.OrdinalIgnoreCase), "DefID mismatch!", null);
						if (equippableFacadeInfo.defID != null)
						{
							this.resources.Add(new EquippableFacadeResource(equippableFacadeInfo.id, equippableFacadeInfo.name, equippableFacadeInfo.buildoverride, equippableFacadeInfo.defID, equippableFacadeInfo.animfile));
						}
					}
				}
			}
			this.resources = this.resources.Distinct<EquippableFacadeResource>().ToList<EquippableFacadeResource>();
			errors.Recycle();
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x0026ADA0 File Offset: 0x00268FA0
		public void Add(string id, string name, string defID, string buildOverride, string animFile)
		{
			EquippableFacadeResource equippableFacadeResource = new EquippableFacadeResource(id, name, buildOverride, defID, animFile);
			this.resources.Add(equippableFacadeResource);
		}

		// Token: 0x040048DD RID: 18653
		public static EquippableFacades.Info[] Infos_Default = new EquippableFacades.Info[]
		{
			new EquippableFacades.Info("clubshirt", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.CLUBSHIRT, "CustomClothing", "body_shirt_clubshirt_kanim", "shirt_clubshirt_kanim"),
			new EquippableFacades.Info("cummerbund", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.CUMMERBUND, "CustomClothing", "body_shirt_cummerbund_kanim", "shirt_cummerbund_kanim"),
			new EquippableFacades.Info("decor_02", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.DECOR_02, "CustomClothing", "body_shirt_decor02_kanim", "shirt_decor02_kanim"),
			new EquippableFacades.Info("decor_03", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.DECOR_03, "CustomClothing", "body_shirt_decor03_kanim", "shirt_decor03_kanim"),
			new EquippableFacades.Info("decor_04", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.DECOR_04, "CustomClothing", "body_shirt_decor04_kanim", "shirt_decor04_kanim"),
			new EquippableFacades.Info("decor_05", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.DECOR_05, "CustomClothing", "body_shirt_decor05_kanim", "shirt_decor05_kanim"),
			new EquippableFacades.Info("gaudysweater", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.GAUDYSWEATER, "CustomClothing", "body_shirt_gaudysweater_kanim", "shirt_gaudysweater_kanim"),
			new EquippableFacades.Info("limone", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.LIMONE, "CustomClothing", "body_suit_limone_kanim", "suit_limone_kanim"),
			new EquippableFacades.Info("mondrian", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.MONDRIAN, "CustomClothing", "body_shirt_mondrian_kanim", "shirt_mondrian_kanim"),
			new EquippableFacades.Info("overalls", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.OVERALLS, "CustomClothing", "body_suit_overalls_kanim", "suit_overalls_kanim"),
			new EquippableFacades.Info("triangles", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.TRIANGLES, "CustomClothing", "body_shirt_triangles_kanim", "shirt_triangles_kanim"),
			new EquippableFacades.Info("workout", EQUIPMENT.PREFABS.CUSTOMCLOTHING.FACADES.WORKOUT, "CustomClothing", "body_suit_workout_kanim", "suit_workout_kanim")
		};

		// Token: 0x040048DE RID: 18654
		public static EquippableFacades.Info[] Infos_Skins = new EquippableFacades.Info[0];

		// Token: 0x040048DF RID: 18655
		public static EquippableFacades.Info[] Infos_All = EquippableFacades.Infos_Default;

		// Token: 0x02001B22 RID: 6946
		public struct Info
		{
			// Token: 0x0600958E RID: 38286 RVA: 0x0032130D File Offset: 0x0031F50D
			public Info(string id, string name, string defID, string buildOverride, string animFile)
			{
				this.id = id;
				this.name = name;
				this.defID = defID;
				this.buildOverride = buildOverride;
				this.animFile = animFile;
			}

			// Token: 0x04007A81 RID: 31361
			public string id;

			// Token: 0x04007A82 RID: 31362
			public string name;

			// Token: 0x04007A83 RID: 31363
			public string buildOverride;

			// Token: 0x04007A84 RID: 31364
			public string defID;

			// Token: 0x04007A85 RID: 31365
			public string animFile;
		}
	}
}
