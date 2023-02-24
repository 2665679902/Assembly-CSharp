using System;
using UnityEngine;

// Token: 0x020005B1 RID: 1457
public class EightDirectionController
{
	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06002412 RID: 9234 RVA: 0x000C32A2 File Offset: 0x000C14A2
	// (set) Token: 0x06002413 RID: 9235 RVA: 0x000C32AA File Offset: 0x000C14AA
	public KBatchedAnimController controller { get; private set; }

	// Token: 0x06002414 RID: 9236 RVA: 0x000C32B3 File Offset: 0x000C14B3
	public EightDirectionController(KAnimControllerBase buildingController, string targetSymbol, string defaultAnim, EightDirectionController.Offset frontBank)
	{
		this.Initialize(buildingController, targetSymbol, defaultAnim, frontBank, Grid.SceneLayer.NoLayer);
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x000C32C8 File Offset: 0x000C14C8
	private void Initialize(KAnimControllerBase buildingController, string targetSymbol, string defaultAnim, EightDirectionController.Offset frontBack, Grid.SceneLayer userSpecifiedRenderLayer)
	{
		string text = buildingController.name + ".eight_direction";
		this.gameObject = new GameObject(text);
		this.gameObject.SetActive(false);
		this.gameObject.transform.parent = buildingController.transform;
		this.gameObject.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
		this.defaultAnim = defaultAnim;
		this.controller = this.gameObject.AddOrGet<KBatchedAnimController>();
		this.controller.AnimFiles = new KAnimFile[] { buildingController.AnimFiles[0] };
		this.controller.initialAnim = defaultAnim;
		this.controller.isMovable = true;
		this.controller.sceneLayer = Grid.SceneLayer.NoLayer;
		if (EightDirectionController.Offset.UserSpecified == frontBack)
		{
			this.controller.sceneLayer = userSpecifiedRenderLayer;
		}
		buildingController.SetSymbolVisiblity(targetSymbol, false);
		bool flag;
		Vector3 vector = buildingController.GetSymbolTransform(new HashedString(targetSymbol), out flag).GetColumn(3);
		switch (frontBack)
		{
		case EightDirectionController.Offset.Infront:
			vector.z = buildingController.transform.GetPosition().z - 0.1f;
			break;
		case EightDirectionController.Offset.Behind:
			vector.z = buildingController.transform.GetPosition().z + 0.1f;
			break;
		case EightDirectionController.Offset.UserSpecified:
			vector.z = Grid.GetLayerZ(userSpecifiedRenderLayer);
			break;
		}
		this.gameObject.transform.SetPosition(vector);
		this.gameObject.SetActive(true);
		this.link = new KAnimLink(buildingController, this.controller);
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x000C3450 File Offset: 0x000C1650
	public void SetPositionPercent(float percent_full)
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.SetPositionPercent(percent_full);
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x000C346D File Offset: 0x000C166D
	public void SetSymbolTint(KAnimHashedString symbol, Color32 colour)
	{
		if (this.controller != null)
		{
			this.controller.SetSymbolTint(symbol, colour);
		}
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x000C348F File Offset: 0x000C168F
	public void SetRotation(float rot)
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.Rotation = rot;
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x000C34AC File Offset: 0x000C16AC
	public void PlayAnim(string anim, KAnim.PlayMode mode = KAnim.PlayMode.Once)
	{
		this.controller.Play(anim, mode, 1f, 0f);
	}

	// Token: 0x040014B9 RID: 5305
	public GameObject gameObject;

	// Token: 0x040014BA RID: 5306
	private string defaultAnim;

	// Token: 0x040014BB RID: 5307
	private KAnimLink link;

	// Token: 0x020011EA RID: 4586
	public enum Offset
	{
		// Token: 0x04005C6F RID: 23663
		Infront,
		// Token: 0x04005C70 RID: 23664
		Behind,
		// Token: 0x04005C71 RID: 23665
		UserSpecified
	}
}
