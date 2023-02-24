using System;
using UnityEngine;

// Token: 0x020006EF RID: 1775
[AddComponentMenu("KMonoBehaviour/scripts/TemperatureCookable")]
public class TemperatureCookable : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06003046 RID: 12358 RVA: 0x000FF001 File Offset: 0x000FD201
	public void Sim1000ms(float dt)
	{
		if (this.element.Temperature > this.cookTemperature && this.cookedID != null)
		{
			this.Cook();
		}
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x000FF024 File Offset: 0x000FD224
	private void Cook()
	{
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(this.cookedID), position);
		gameObject.SetActive(true);
		KSelectable component = base.gameObject.GetComponent<KSelectable>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == component)
		{
			SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
		}
		PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
		component2.Temperature = this.element.Temperature;
		component2.Mass = this.element.Mass;
		base.gameObject.DeleteObject();
	}

	// Token: 0x04001D17 RID: 7447
	[MyCmpReq]
	private PrimaryElement element;

	// Token: 0x04001D18 RID: 7448
	public float cookTemperature = 273150f;

	// Token: 0x04001D19 RID: 7449
	public string cookedID;
}
