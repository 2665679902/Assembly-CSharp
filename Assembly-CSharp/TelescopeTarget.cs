using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;

// Token: 0x02000966 RID: 2406
[SerializationConfig(MemberSerialization.OptIn)]
public class TelescopeTarget : ClusterGridEntity
{
	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x0600474A RID: 18250 RVA: 0x00191BF9 File Offset: 0x0018FDF9
	public override string Name
	{
		get
		{
			return UI.SPACEDESTINATIONS.TELESCOPE_TARGET.NAME;
		}
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x0600474B RID: 18251 RVA: 0x00191C05 File Offset: 0x0018FE05
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Telescope;
		}
	}

	// Token: 0x17000551 RID: 1361
	// (get) Token: 0x0600474C RID: 18252 RVA: 0x00191C08 File Offset: 0x0018FE08
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("telescope_target_kanim"),
					initialAnim = "idle"
				}
			};
		}
	}

	// Token: 0x17000552 RID: 1362
	// (get) Token: 0x0600474D RID: 18253 RVA: 0x00191C4B File Offset: 0x0018FE4B
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000553 RID: 1363
	// (get) Token: 0x0600474E RID: 18254 RVA: 0x00191C4E File Offset: 0x0018FE4E
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x0600474F RID: 18255 RVA: 0x00191C51 File Offset: 0x0018FE51
	public void Init(AxialI location)
	{
		base.Location = location;
	}

	// Token: 0x06004750 RID: 18256 RVA: 0x00191C5A File Offset: 0x0018FE5A
	public override bool ShowName()
	{
		return true;
	}

	// Token: 0x06004751 RID: 18257 RVA: 0x00191C5D File Offset: 0x0018FE5D
	public override bool ShowProgressBar()
	{
		return true;
	}

	// Token: 0x06004752 RID: 18258 RVA: 0x00191C60 File Offset: 0x0018FE60
	public override float GetProgress()
	{
		return SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().GetRevealCompleteFraction(base.Location);
	}
}
