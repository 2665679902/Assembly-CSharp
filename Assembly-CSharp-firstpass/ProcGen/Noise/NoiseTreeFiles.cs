using System;
using System.Collections.Generic;
using Klei;
using LibNoiseDotNet.Graphics.Tools.Noise;

namespace ProcGen.Noise
{
	// Token: 0x020004F2 RID: 1266
	public class NoiseTreeFiles
	{
		// Token: 0x060036A4 RID: 13988 RVA: 0x00077C9C File Offset: 0x00075E9C
		public static string GetDirectoryRel()
		{
			return "worldgen/noise/";
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x00077CA3 File Offset: 0x00075EA3
		public static string GetPathRel()
		{
			return "worldgen/" + NoiseTreeFiles.NOISE_FILE + ".yaml";
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x00077CB9 File Offset: 0x00075EB9
		public static string GetTreeFilePathRel(string filename)
		{
			return "worldgen/noise/" + filename + ".yaml";
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x00077CCB File Offset: 0x00075ECB
		// (set) Token: 0x060036A8 RID: 13992 RVA: 0x00077CD3 File Offset: 0x00075ED3
		public List<string> tree_files { get; set; }

		// Token: 0x060036A9 RID: 13993 RVA: 0x00077CDC File Offset: 0x00075EDC
		public void Clear()
		{
			this.tree_files.Clear();
			this.trees.Clear();
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00077CF4 File Offset: 0x00075EF4
		public NoiseTreeFiles()
		{
			this.trees = new Dictionary<string, Tree>();
			this.tree_files = new List<string>();
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x00077D14 File Offset: 0x00075F14
		public Tree LoadTree(string name)
		{
			if (name != null && name.Length > 0)
			{
				if (!this.trees.ContainsKey(name))
				{
					Tree tree = YamlIO.LoadFile<Tree>(SettingsCache.RewriteWorldgenPathYaml(name), null, null);
					if (tree != null)
					{
						this.trees.Add(name, tree);
					}
				}
				return this.trees[name];
			}
			return null;
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x00077D67 File Offset: 0x00075F67
		public float GetZoomForTree(string name)
		{
			if (!this.trees.ContainsKey(name))
			{
				return 1f;
			}
			return this.trees[name].settings.zoom;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x00077D93 File Offset: 0x00075F93
		public bool ShouldNormaliseTree(string name)
		{
			return this.trees.ContainsKey(name) && this.trees[name].settings.normalise;
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x00077DBC File Offset: 0x00075FBC
		public string[] GetTreeNames()
		{
			string[] array = new string[this.trees.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<string, Tree> keyValuePair in this.trees)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x00077E30 File Offset: 0x00076030
		public Tree GetTree(string name)
		{
			if (!this.trees.ContainsKey(name))
			{
				string text = SettingsCache.RewriteWorldgenPathYaml(name);
				Tree tree = YamlIO.LoadFile<Tree>(text, null, null);
				if (tree == null)
				{
					DebugUtil.LogArgs(new object[] { "NoiseArgs.GetTree failed to load " + name + " at " + text });
					return null;
				}
				this.trees.Add(name, tree);
			}
			return this.trees[name];
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x00077E9A File Offset: 0x0007609A
		public IModule3D BuildTree(string name, int globalSeed)
		{
			if (!this.trees.ContainsKey(name))
			{
				return null;
			}
			return this.trees[name].BuildFinalModule(globalSeed);
		}

		// Token: 0x0400138C RID: 5004
		public static string NOISE_FILE = "noise";

		// Token: 0x0400138E RID: 5006
		private Dictionary<string, Tree> trees;
	}
}
