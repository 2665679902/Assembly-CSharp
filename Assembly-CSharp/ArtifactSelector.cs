using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200054D RID: 1357
public class ArtifactSelector : KMonoBehaviour
{
	// Token: 0x17000184 RID: 388
	// (get) Token: 0x0600205C RID: 8284 RVA: 0x000B0D67 File Offset: 0x000AEF67
	public int AnalyzedArtifactCount
	{
		get
		{
			return this.analyzedArtifactCount;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x0600205D RID: 8285 RVA: 0x000B0D6F File Offset: 0x000AEF6F
	public int AnalyzedSpaceArtifactCount
	{
		get
		{
			return this.analyzedSpaceArtifactCount;
		}
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x000B0D77 File Offset: 0x000AEF77
	public List<string> GetAnalyzedArtifactIDs()
	{
		return this.analyzedArtifatIDs;
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x000B0D80 File Offset: 0x000AEF80
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ArtifactSelector.Instance = this;
		this.placedArtifacts.Add(ArtifactType.Terrestrial, new List<string>());
		this.placedArtifacts.Add(ArtifactType.Space, new List<string>());
		this.placedArtifacts.Add(ArtifactType.Any, new List<string>());
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x000B0DCC File Offset: 0x000AEFCC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = 0;
		int num2 = 0;
		foreach (string text in this.analyzedArtifatIDs)
		{
			ArtifactType artifactType = this.GetArtifactType(text);
			if (artifactType != ArtifactType.Space)
			{
				if (artifactType == ArtifactType.Terrestrial)
				{
					num++;
				}
			}
			else
			{
				num2++;
			}
		}
		if (num > this.analyzedArtifactCount)
		{
			this.analyzedArtifactCount = num;
		}
		if (num2 > this.analyzedSpaceArtifactCount)
		{
			this.analyzedSpaceArtifactCount = num2;
		}
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x000B0E60 File Offset: 0x000AF060
	public bool RecordArtifactAnalyzed(string id)
	{
		if (this.analyzedArtifatIDs.Contains(id))
		{
			return false;
		}
		this.analyzedArtifatIDs.Add(id);
		return true;
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x000B0E7F File Offset: 0x000AF07F
	public void IncrementAnalyzedTerrestrialArtifacts()
	{
		this.analyzedArtifactCount++;
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x000B0E8F File Offset: 0x000AF08F
	public void IncrementAnalyzedSpaceArtifacts()
	{
		this.analyzedSpaceArtifactCount++;
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x000B0EA0 File Offset: 0x000AF0A0
	public string GetUniqueArtifactID(ArtifactType artifactType = ArtifactType.Any)
	{
		List<string> list = new List<string>();
		foreach (string text in ArtifactConfig.artifactItems[artifactType])
		{
			if (!this.placedArtifacts[artifactType].Contains(text))
			{
				list.Add(text);
			}
		}
		string text2 = "artifact_officemug";
		if (list.Count == 0 && artifactType != ArtifactType.Any)
		{
			foreach (string text3 in ArtifactConfig.artifactItems[ArtifactType.Any])
			{
				if (!this.placedArtifacts[ArtifactType.Any].Contains(text3))
				{
					list.Add(text3);
					artifactType = ArtifactType.Any;
				}
			}
		}
		if (list.Count != 0)
		{
			text2 = list[UnityEngine.Random.Range(0, list.Count)];
		}
		this.placedArtifacts[artifactType].Add(text2);
		return text2;
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x000B0FB4 File Offset: 0x000AF1B4
	public void ReserveArtifactID(string artifactID, ArtifactType artifactType = ArtifactType.Any)
	{
		if (this.placedArtifacts[artifactType].Contains(artifactID))
		{
			DebugUtil.Assert(true, string.Format("Tried to add {0} to placedArtifacts but it already exists in the list!", artifactID));
		}
		this.placedArtifacts[artifactType].Add(artifactID);
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x000B0FED File Offset: 0x000AF1ED
	public ArtifactType GetArtifactType(string artifactID)
	{
		if (this.placedArtifacts[ArtifactType.Terrestrial].Contains(artifactID))
		{
			return ArtifactType.Terrestrial;
		}
		if (this.placedArtifacts[ArtifactType.Space].Contains(artifactID))
		{
			return ArtifactType.Space;
		}
		return ArtifactType.Any;
	}

	// Token: 0x0400129D RID: 4765
	public static ArtifactSelector Instance;

	// Token: 0x0400129E RID: 4766
	[Serialize]
	private Dictionary<ArtifactType, List<string>> placedArtifacts = new Dictionary<ArtifactType, List<string>>();

	// Token: 0x0400129F RID: 4767
	[Serialize]
	private int analyzedArtifactCount;

	// Token: 0x040012A0 RID: 4768
	[Serialize]
	private int analyzedSpaceArtifactCount;

	// Token: 0x040012A1 RID: 4769
	[Serialize]
	private List<string> analyzedArtifatIDs = new List<string>();

	// Token: 0x040012A2 RID: 4770
	private const string DEFAULT_ARTIFACT_ID = "artifact_officemug";
}
