using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200098B RID: 2443
[AddComponentMenu("KMonoBehaviour/scripts/SpaceArtifact")]
public class SpaceArtifact : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x0600485F RID: 18527 RVA: 0x001961CC File Offset: 0x001943CC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.loadCharmed && DlcManager.IsExpansion1Active())
		{
			base.gameObject.AddTag(GameTags.CharmedArtifact);
			this.SetEntombedDecor();
		}
		else
		{
			this.loadCharmed = false;
			this.SetAnalyzedDecor();
		}
		this.UpdateStatusItem();
		Components.SpaceArtifacts.Add(this);
		this.UpdateAnim();
	}

	// Token: 0x06004860 RID: 18528 RVA: 0x0019622A File Offset: 0x0019442A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.SpaceArtifacts.Remove(this);
	}

	// Token: 0x06004861 RID: 18529 RVA: 0x0019623D File Offset: 0x0019443D
	public void RemoveCharm()
	{
		base.gameObject.RemoveTag(GameTags.CharmedArtifact);
		this.UpdateStatusItem();
		this.loadCharmed = false;
		this.UpdateAnim();
		this.SetAnalyzedDecor();
	}

	// Token: 0x06004862 RID: 18530 RVA: 0x00196268 File Offset: 0x00194468
	private void SetEntombedDecor()
	{
		base.GetComponent<DecorProvider>().SetValues(DECOR.BONUS.TIER0);
	}

	// Token: 0x06004863 RID: 18531 RVA: 0x0019627A File Offset: 0x0019447A
	private void SetAnalyzedDecor()
	{
		base.GetComponent<DecorProvider>().SetValues(this.artifactTier.decorValues);
	}

	// Token: 0x06004864 RID: 18532 RVA: 0x00196294 File Offset: 0x00194494
	public void UpdateStatusItem()
	{
		if (base.gameObject.HasTag(GameTags.CharmedArtifact))
		{
			base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.ArtifactEntombed, null);
			return;
		}
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.ArtifactEntombed, false);
	}

	// Token: 0x06004865 RID: 18533 RVA: 0x001962F6 File Offset: 0x001944F6
	public void SetArtifactTier(ArtifactTier tier)
	{
		this.artifactTier = tier;
	}

	// Token: 0x06004866 RID: 18534 RVA: 0x001962FF File Offset: 0x001944FF
	public ArtifactTier GetArtifactTier()
	{
		return this.artifactTier;
	}

	// Token: 0x06004867 RID: 18535 RVA: 0x00196307 File Offset: 0x00194507
	public void SetUIAnim(string anim)
	{
		this.ui_anim = anim;
	}

	// Token: 0x06004868 RID: 18536 RVA: 0x00196310 File Offset: 0x00194510
	public string GetUIAnim()
	{
		return this.ui_anim;
	}

	// Token: 0x06004869 RID: 18537 RVA: 0x00196318 File Offset: 0x00194518
	public List<Descriptor> GetEffectDescriptions()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (base.gameObject.HasTag(GameTags.CharmedArtifact))
		{
			Descriptor descriptor = new Descriptor(STRINGS.BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.PAYLOAD_DROP_RATE.Replace("{chance}", GameUtil.GetFormattedPercent(this.artifactTier.payloadDropChance * 100f, GameUtil.TimeSlice.None)), STRINGS.BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.PAYLOAD_DROP_RATE_TOOLTIP.Replace("{chance}", GameUtil.GetFormattedPercent(this.artifactTier.payloadDropChance * 100f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		Descriptor descriptor2 = new Descriptor(string.Format("This is an artifact from space", Array.Empty<object>()), string.Format("This is the tooltip string", Array.Empty<object>()), Descriptor.DescriptorType.Information, false);
		list.Add(descriptor2);
		return list;
	}

	// Token: 0x0600486A RID: 18538 RVA: 0x001963C8 File Offset: 0x001945C8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.GetEffectDescriptions();
	}

	// Token: 0x0600486B RID: 18539 RVA: 0x001963D0 File Offset: 0x001945D0
	private void UpdateAnim()
	{
		string text;
		if (base.gameObject.HasTag(GameTags.CharmedArtifact))
		{
			text = "entombed_" + this.uniqueAnimNameFragment.Replace("idle_", "");
		}
		else
		{
			text = this.uniqueAnimNameFragment;
		}
		base.GetComponent<KBatchedAnimController>().Play(text, KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x0600486C RID: 18540 RVA: 0x00196434 File Offset: 0x00194634
	[OnDeserialized]
	public void OnDeserialize()
	{
		Pickupable component = base.GetComponent<Pickupable>();
		if (component != null)
		{
			component.deleteOffGrid = false;
		}
	}

	// Token: 0x04002F96 RID: 12182
	public const string ID = "SpaceArtifact";

	// Token: 0x04002F97 RID: 12183
	private const string charmedPrefix = "entombed_";

	// Token: 0x04002F98 RID: 12184
	private const string idlePrefix = "idle_";

	// Token: 0x04002F99 RID: 12185
	[SerializeField]
	private string ui_anim;

	// Token: 0x04002F9A RID: 12186
	[Serialize]
	private bool loadCharmed = true;

	// Token: 0x04002F9B RID: 12187
	public ArtifactTier artifactTier;

	// Token: 0x04002F9C RID: 12188
	public ArtifactType artifactType;

	// Token: 0x04002F9D RID: 12189
	public string uniqueAnimNameFragment;
}
