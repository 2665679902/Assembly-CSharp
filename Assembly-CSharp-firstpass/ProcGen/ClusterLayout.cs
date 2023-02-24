using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Klei;
using UnityEngine;

namespace ProcGen
{
	// Token: 0x020004E6 RID: 1254
	public class ClusterLayout
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x000766E7 File Offset: 0x000748E7
		// (set) Token: 0x0600360D RID: 13837 RVA: 0x000766EF File Offset: 0x000748EF
		public List<WorldPlacement> worldPlacements { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000766F8 File Offset: 0x000748F8
		// (set) Token: 0x0600360F RID: 13839 RVA: 0x00076700 File Offset: 0x00074900
		public List<SpaceMapPOIPlacement> poiPlacements { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x00076709 File Offset: 0x00074909
		// (set) Token: 0x06003611 RID: 13841 RVA: 0x00076711 File Offset: 0x00074911
		public string name { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x0007671A File Offset: 0x0007491A
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x00076722 File Offset: 0x00074922
		public string description { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x0007672B File Offset: 0x0007492B
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x00076733 File Offset: 0x00074933
		public string requiredDlcId { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x0007673C File Offset: 0x0007493C
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x00076744 File Offset: 0x00074944
		public string forbiddenDlcId { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x0007674D File Offset: 0x0007494D
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x00076755 File Offset: 0x00074955
		public int difficulty { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x0007675E File Offset: 0x0007495E
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x00076766 File Offset: 0x00074966
		public bool disableStoryTraits { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x0007676F File Offset: 0x0007496F
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x00076777 File Offset: 0x00074977
		public ClusterLayout.Skip skip { get; private set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x00076780 File Offset: 0x00074980
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x00076788 File Offset: 0x00074988
		public int clusterCategory { get; private set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x00076791 File Offset: 0x00074991
		// (set) Token: 0x06003621 RID: 13857 RVA: 0x00076799 File Offset: 0x00074999
		public int startWorldIndex { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000767A2 File Offset: 0x000749A2
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000767AA File Offset: 0x000749AA
		public int width { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000767B3 File Offset: 0x000749B3
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x000767BB File Offset: 0x000749BB
		public int height { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000767C4 File Offset: 0x000749C4
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000767CC File Offset: 0x000749CC
		public int numRings { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000767D5 File Offset: 0x000749D5
		// (set) Token: 0x06003629 RID: 13865 RVA: 0x000767DD File Offset: 0x000749DD
		public int menuOrder { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000767E6 File Offset: 0x000749E6
		// (set) Token: 0x0600362B RID: 13867 RVA: 0x000767EE File Offset: 0x000749EE
		public string coordinatePrefix { get; private set; }

		// Token: 0x0600362C RID: 13868 RVA: 0x000767F7 File Offset: 0x000749F7
		public ClusterLayout()
		{
			this.numRings = 12;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x00076807 File Offset: 0x00074A07
		public static string GetName(string path, string addPrefix)
		{
			return FileSystem.Normalize(Path.Combine(addPrefix + "clusters", Path.GetFileNameWithoutExtension(path)));
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x00076824 File Offset: 0x00074A24
		public string GetStartWorld()
		{
			return this.worldPlacements[this.startWorldIndex].world;
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x0007683C File Offset: 0x00074A3C
		public string GetCoordinatePrefix()
		{
			if (string.IsNullOrEmpty(this.coordinatePrefix))
			{
				string text = "";
				string[] array = Strings.Get(this.name).String.Split(new char[] { ' ' });
				int num = 5 - array.Length;
				bool flag = true;
				foreach (string text2 in array)
				{
					if (!flag)
					{
						text += "-";
					}
					string text3 = Regex.Replace(text2, "(a|e|i|o|u)", "");
					text += text3.Substring(0, Mathf.Min(num, text3.Length)).ToUpper();
					flag = false;
				}
				this.coordinatePrefix = text;
			}
			return this.coordinatePrefix;
		}

		// Token: 0x04001309 RID: 4873
		public const string directory = "clusters";

		// Token: 0x04001319 RID: 4889
		public string filePath;

		// Token: 0x02000B0F RID: 2831
		public enum Skip
		{
			// Token: 0x040025EB RID: 9707
			Never,
			// Token: 0x040025EC RID: 9708
			Always = 99,
			// Token: 0x040025ED RID: 9709
			EditorOnly
		}

		// Token: 0x02000B10 RID: 2832
		public enum ClusterCategory
		{
			// Token: 0x040025EF RID: 9711
			vanilla,
			// Token: 0x040025F0 RID: 9712
			spacedOutVanillaStyle,
			// Token: 0x040025F1 RID: 9713
			spacedOutStyle
		}
	}
}
