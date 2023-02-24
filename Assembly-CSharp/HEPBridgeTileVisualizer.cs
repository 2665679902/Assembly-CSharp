using System;

// Token: 0x020005CC RID: 1484
public class HEPBridgeTileVisualizer : KMonoBehaviour, IHighEnergyParticleDirection
{
	// Token: 0x060024EC RID: 9452 RVA: 0x000C7CB8 File Offset: 0x000C5EB8
	protected override void OnSpawn()
	{
		base.Subscribe<HEPBridgeTileVisualizer>(-1643076535, HEPBridgeTileVisualizer.OnRotateDelegate);
		this.OnRotate();
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x000C7CD1 File Offset: 0x000C5ED1
	public void OnRotate()
	{
		Game.Instance.ForceOverlayUpdate(true);
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x060024EE RID: 9454 RVA: 0x000C7CE0 File Offset: 0x000C5EE0
	// (set) Token: 0x060024EF RID: 9455 RVA: 0x000C7D2D File Offset: 0x000C5F2D
	public EightDirection Direction
	{
		get
		{
			EightDirection eightDirection = EightDirection.Right;
			Rotatable component = base.GetComponent<Rotatable>();
			if (component != null)
			{
				switch (component.Orientation)
				{
				case Orientation.Neutral:
					eightDirection = EightDirection.Left;
					break;
				case Orientation.R90:
					eightDirection = EightDirection.Up;
					break;
				case Orientation.R180:
					eightDirection = EightDirection.Right;
					break;
				case Orientation.R270:
					eightDirection = EightDirection.Down;
					break;
				}
			}
			return eightDirection;
		}
		set
		{
		}
	}

	// Token: 0x0400154B RID: 5451
	private static readonly EventSystem.IntraObjectHandler<HEPBridgeTileVisualizer> OnRotateDelegate = new EventSystem.IntraObjectHandler<HEPBridgeTileVisualizer>(delegate(HEPBridgeTileVisualizer component, object data)
	{
		component.OnRotate();
	});
}
