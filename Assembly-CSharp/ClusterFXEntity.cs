using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200093D RID: 2365
[SerializationConfig(MemberSerialization.OptIn)]
public class ClusterFXEntity : ClusterGridEntity
{
	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x06004549 RID: 17737 RVA: 0x001869E6 File Offset: 0x00184BE6
	public override string Name
	{
		get
		{
			return UI.SPACEDESTINATIONS.TELESCOPE_TARGET.NAME;
		}
	}

	// Token: 0x170004F8 RID: 1272
	// (get) Token: 0x0600454A RID: 17738 RVA: 0x001869F2 File Offset: 0x00184BF2
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.FX;
		}
	}

	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x0600454B RID: 17739 RVA: 0x001869F8 File Offset: 0x00184BF8
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim(this.kAnimName),
					initialAnim = this.animName,
					playMode = this.animPlayMode,
					animOffset = this.animOffset
				}
			};
		}
	}

	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x0600454C RID: 17740 RVA: 0x00186A57 File Offset: 0x00184C57
	public override bool IsVisible
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x0600454D RID: 17741 RVA: 0x00186A5A File Offset: 0x00184C5A
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x0600454E RID: 17742 RVA: 0x00186A5D File Offset: 0x00184C5D
	public void Init(AxialI location, Vector3 animOffset)
	{
		base.Location = location;
		this.animOffset = animOffset;
	}

	// Token: 0x04002E34 RID: 11828
	[SerializeField]
	public string kAnimName;

	// Token: 0x04002E35 RID: 11829
	[SerializeField]
	public string animName;

	// Token: 0x04002E36 RID: 11830
	public KAnim.PlayMode animPlayMode = KAnim.PlayMode.Once;

	// Token: 0x04002E37 RID: 11831
	public Vector3 animOffset;
}
