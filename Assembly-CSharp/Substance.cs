using System;
using FMODUnity;
using Klei;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x0200099C RID: 2460
[Serializable]
public class Substance
{
	// Token: 0x060048FE RID: 18686 RVA: 0x001992D8 File Offset: 0x001974D8
	public GameObject SpawnResource(Vector3 position, float mass, float temperature, byte disease_idx, int disease_count, bool prevent_merge = false, bool forceTemperature = false, bool manual_activation = false)
	{
		GameObject gameObject = null;
		PrimaryElement primaryElement = null;
		if (!prevent_merge)
		{
			int num = Grid.PosToCell(position);
			GameObject gameObject2 = Grid.Objects[num, 3];
			if (gameObject2 != null)
			{
				Pickupable component = gameObject2.GetComponent<Pickupable>();
				if (component != null)
				{
					Tag tag = GameTagExtensions.Create(this.elementID);
					for (ObjectLayerListItem objectLayerListItem = component.objectLayerListItem; objectLayerListItem != null; objectLayerListItem = objectLayerListItem.nextItem)
					{
						KPrefabID component2 = objectLayerListItem.gameObject.GetComponent<KPrefabID>();
						if (component2.PrefabTag == tag)
						{
							PrimaryElement component3 = component2.GetComponent<PrimaryElement>();
							if (component3.Mass + mass <= PrimaryElement.MAX_MASS)
							{
								gameObject = component2.gameObject;
								primaryElement = component3;
								temperature = SimUtil.CalculateFinalTemperature(primaryElement.Mass, primaryElement.Temperature, mass, temperature);
								position = gameObject.transform.GetPosition();
								break;
							}
						}
					}
				}
			}
		}
		if (gameObject == null)
		{
			gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.nameTag), Grid.SceneLayer.Ore, null, 0);
			primaryElement = gameObject.GetComponent<PrimaryElement>();
			primaryElement.Mass = mass;
		}
		else
		{
			global::Debug.Assert(primaryElement != null);
			Pickupable component4 = primaryElement.GetComponent<Pickupable>();
			if (component4 != null)
			{
				component4.TotalAmount += mass / primaryElement.MassPerUnit;
			}
			else
			{
				primaryElement.Mass += mass;
			}
		}
		primaryElement.InternalTemperature = temperature;
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
		gameObject.transform.SetPosition(position);
		if (!manual_activation)
		{
			this.ActivateSubstanceGameObject(gameObject, disease_idx, disease_count);
		}
		return gameObject;
	}

	// Token: 0x060048FF RID: 18687 RVA: 0x00199454 File Offset: 0x00197654
	public void ActivateSubstanceGameObject(GameObject obj, byte disease_idx, int disease_count)
	{
		obj.SetActive(true);
		obj.GetComponent<PrimaryElement>().AddDisease(disease_idx, disease_count, "Substances.SpawnResource");
	}

	// Token: 0x06004900 RID: 18688 RVA: 0x00199470 File Offset: 0x00197670
	private void SetTexture(MaterialPropertyBlock block, string texture_name)
	{
		Texture texture = this.material.GetTexture(texture_name);
		if (texture != null)
		{
			this.propertyBlock.SetTexture(texture_name, texture);
		}
	}

	// Token: 0x06004901 RID: 18689 RVA: 0x001994A0 File Offset: 0x001976A0
	public void RefreshPropertyBlock()
	{
		if (this.propertyBlock == null)
		{
			this.propertyBlock = new MaterialPropertyBlock();
		}
		if (this.material != null)
		{
			this.SetTexture(this.propertyBlock, "_MainTex");
			float @float = this.material.GetFloat("_WorldUVScale");
			this.propertyBlock.SetFloat("_WorldUVScale", @float);
			if (ElementLoader.FindElementByHash(this.elementID).IsSolid)
			{
				this.SetTexture(this.propertyBlock, "_MainTex2");
				this.SetTexture(this.propertyBlock, "_HeightTex2");
				this.propertyBlock.SetFloat("_Frequency", this.material.GetFloat("_Frequency"));
				this.propertyBlock.SetColor("_ShineColour", this.material.GetColor("_ShineColour"));
				this.propertyBlock.SetColor("_ColourTint", this.material.GetColor("_ColourTint"));
			}
		}
	}

	// Token: 0x06004902 RID: 18690 RVA: 0x0019959B File Offset: 0x0019779B
	internal AmbienceType GetAmbience()
	{
		if (this.audioConfig == null)
		{
			return AmbienceType.None;
		}
		return this.audioConfig.ambienceType;
	}

	// Token: 0x06004903 RID: 18691 RVA: 0x001995B2 File Offset: 0x001977B2
	internal SolidAmbienceType GetSolidAmbience()
	{
		if (this.audioConfig == null)
		{
			return SolidAmbienceType.None;
		}
		return this.audioConfig.solidAmbienceType;
	}

	// Token: 0x06004904 RID: 18692 RVA: 0x001995C9 File Offset: 0x001977C9
	internal string GetMiningSound()
	{
		if (this.audioConfig == null)
		{
			return "";
		}
		return this.audioConfig.miningSound;
	}

	// Token: 0x06004905 RID: 18693 RVA: 0x001995E4 File Offset: 0x001977E4
	internal string GetMiningBreakSound()
	{
		if (this.audioConfig == null)
		{
			return "";
		}
		return this.audioConfig.miningBreakSound;
	}

	// Token: 0x06004906 RID: 18694 RVA: 0x001995FF File Offset: 0x001977FF
	internal string GetOreBumpSound()
	{
		if (this.audioConfig == null)
		{
			return "";
		}
		return this.audioConfig.oreBumpSound;
	}

	// Token: 0x06004907 RID: 18695 RVA: 0x0019961A File Offset: 0x0019781A
	internal string GetFloorEventAudioCategory()
	{
		if (this.audioConfig == null)
		{
			return "";
		}
		return this.audioConfig.floorEventAudioCategory;
	}

	// Token: 0x06004908 RID: 18696 RVA: 0x00199635 File Offset: 0x00197835
	internal string GetCreatureChewSound()
	{
		if (this.audioConfig == null)
		{
			return "";
		}
		return this.audioConfig.creatureChewSound;
	}

	// Token: 0x04002FF6 RID: 12278
	public string name;

	// Token: 0x04002FF7 RID: 12279
	public SimHashes elementID;

	// Token: 0x04002FF8 RID: 12280
	internal Tag nameTag;

	// Token: 0x04002FF9 RID: 12281
	public Color32 colour;

	// Token: 0x04002FFA RID: 12282
	[FormerlySerializedAs("debugColour")]
	public Color32 uiColour;

	// Token: 0x04002FFB RID: 12283
	[FormerlySerializedAs("overlayColour")]
	public Color32 conduitColour = Color.white;

	// Token: 0x04002FFC RID: 12284
	[NonSerialized]
	internal bool renderedByWorld;

	// Token: 0x04002FFD RID: 12285
	[NonSerialized]
	internal int idx;

	// Token: 0x04002FFE RID: 12286
	public Material material;

	// Token: 0x04002FFF RID: 12287
	public KAnimFile anim;

	// Token: 0x04003000 RID: 12288
	[SerializeField]
	internal bool showInEditor = true;

	// Token: 0x04003001 RID: 12289
	[NonSerialized]
	internal KAnimFile[] anims;

	// Token: 0x04003002 RID: 12290
	[NonSerialized]
	internal ElementsAudio.ElementAudioConfig audioConfig;

	// Token: 0x04003003 RID: 12291
	[NonSerialized]
	internal MaterialPropertyBlock propertyBlock;

	// Token: 0x04003004 RID: 12292
	public EventReference fallingStartSound;

	// Token: 0x04003005 RID: 12293
	public EventReference fallingStopSound;
}
