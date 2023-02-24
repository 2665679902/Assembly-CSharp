using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C10 RID: 3088
[AddComponentMenu("KMonoBehaviour/scripts/TitleBarPortrait")]
public class TitleBarPortrait : KMonoBehaviour
{
	// Token: 0x060061DB RID: 25051 RVA: 0x002429D5 File Offset: 0x00240BD5
	public void SetSaturation(bool saturated)
	{
		this.ImageObject.GetComponent<Image>().material = (saturated ? this.DefaultMaterial : this.DesatMaterial);
	}

	// Token: 0x060061DC RID: 25052 RVA: 0x002429F8 File Offset: 0x00240BF8
	public void SetPortrait(GameObject selectedTarget)
	{
		MinionIdentity component = selectedTarget.GetComponent<MinionIdentity>();
		if (component != null)
		{
			this.SetPortrait(component);
			return;
		}
		Building component2 = selectedTarget.GetComponent<Building>();
		if (component2 != null)
		{
			this.SetPortrait(component2.Def.GetUISprite("ui", false));
			return;
		}
		MeshRenderer componentInChildren = selectedTarget.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren)
		{
			this.SetPortrait(Sprite.Create((Texture2D)componentInChildren.material.mainTexture, new Rect(0f, 0f, (float)componentInChildren.material.mainTexture.width, (float)componentInChildren.material.mainTexture.height), new Vector2(0.5f, 0.5f)));
		}
	}

	// Token: 0x060061DD RID: 25053 RVA: 0x00242AB0 File Offset: 0x00240CB0
	public void SetPortrait(Sprite image)
	{
		if (this.PortraitShadow)
		{
			this.PortraitShadow.SetActive(true);
		}
		if (this.FaceObject)
		{
			this.FaceObject.SetActive(false);
		}
		if (this.ImageObject)
		{
			this.ImageObject.SetActive(true);
		}
		if (this.AnimControllerObject)
		{
			this.AnimControllerObject.SetActive(false);
		}
		if (image == null)
		{
			this.ClearPortrait();
			return;
		}
		this.ImageObject.GetComponent<Image>().sprite = image;
	}

	// Token: 0x060061DE RID: 25054 RVA: 0x00242B44 File Offset: 0x00240D44
	private void SetPortrait(MinionIdentity identity)
	{
		if (this.PortraitShadow)
		{
			this.PortraitShadow.SetActive(true);
		}
		if (this.FaceObject)
		{
			this.FaceObject.SetActive(false);
		}
		if (this.ImageObject)
		{
			this.ImageObject.SetActive(false);
		}
		CrewPortrait component = base.GetComponent<CrewPortrait>();
		if (component != null)
		{
			component.SetIdentityObject(identity, true);
			return;
		}
		if (this.AnimControllerObject)
		{
			this.AnimControllerObject.SetActive(true);
			CrewPortrait.SetPortraitData(identity, this.AnimControllerObject.GetComponent<KBatchedAnimController>(), true);
		}
	}

	// Token: 0x060061DF RID: 25055 RVA: 0x00242BE0 File Offset: 0x00240DE0
	public void ClearPortrait()
	{
		if (this.PortraitShadow)
		{
			this.PortraitShadow.SetActive(false);
		}
		if (this.FaceObject)
		{
			this.FaceObject.SetActive(false);
		}
		if (this.ImageObject)
		{
			this.ImageObject.SetActive(false);
		}
		if (this.AnimControllerObject)
		{
			this.AnimControllerObject.SetActive(false);
		}
	}

	// Token: 0x040043AD RID: 17325
	public GameObject FaceObject;

	// Token: 0x040043AE RID: 17326
	public GameObject ImageObject;

	// Token: 0x040043AF RID: 17327
	public GameObject PortraitShadow;

	// Token: 0x040043B0 RID: 17328
	public GameObject AnimControllerObject;

	// Token: 0x040043B1 RID: 17329
	public Material DefaultMaterial;

	// Token: 0x040043B2 RID: 17330
	public Material DesatMaterial;
}
