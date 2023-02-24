using System;
using UnityEngine;

// Token: 0x020008EE RID: 2286
public class RocketConduitStorageAccess : KMonoBehaviour, ISim200ms
{
	// Token: 0x060041CE RID: 16846 RVA: 0x00172474 File Offset: 0x00170674
	protected override void OnSpawn()
	{
		WorldContainer myWorld = this.GetMyWorld();
		this.craftModuleInterface = myWorld.GetComponent<CraftModuleInterface>();
	}

	// Token: 0x060041CF RID: 16847 RVA: 0x00172494 File Offset: 0x00170694
	public void Sim200ms(float dt)
	{
		if (this.operational != null && !this.operational.IsOperational)
		{
			return;
		}
		float num = this.storage.MassStored();
		if (num < this.targetLevel - 0.01f || num > this.targetLevel + 0.01f)
		{
			if (this.operational != null)
			{
				this.operational.SetActive(true, false);
			}
			float num2 = this.targetLevel - num;
			foreach (Ref<RocketModuleCluster> @ref in this.craftModuleInterface.ClusterModules)
			{
				CargoBayCluster component = @ref.Get().GetComponent<CargoBayCluster>();
				if (component != null && component.storageType == this.cargoType)
				{
					if (num2 > 0f && component.storage.MassStored() > 0f)
					{
						for (int i = component.storage.items.Count - 1; i >= 0; i--)
						{
							GameObject gameObject = component.storage.items[i];
							if (!(this.filterable != null) || !(this.filterable.SelectedTag != GameTags.Void) || !(gameObject.PrefabID() != this.filterable.SelectedTag))
							{
								Pickupable pickupable = gameObject.GetComponent<Pickupable>().Take(num2);
								if (pickupable != null)
								{
									num2 -= pickupable.PrimaryElement.Mass;
									this.storage.Store(pickupable.gameObject, true, false, true, false);
								}
								if (num2 <= 0f)
								{
									break;
								}
							}
						}
						if (num2 <= 0f)
						{
							break;
						}
					}
					if (num2 < 0f && component.storage.RemainingCapacity() > 0f)
					{
						Mathf.Min(-num2, component.storage.RemainingCapacity());
						for (int j = this.storage.items.Count - 1; j >= 0; j--)
						{
							Pickupable pickupable2 = this.storage.items[j].GetComponent<Pickupable>().Take(-num2);
							if (pickupable2 != null)
							{
								num2 += pickupable2.PrimaryElement.Mass;
								component.storage.Store(pickupable2.gameObject, true, false, true, false);
							}
							if (num2 >= 0f)
							{
								break;
							}
						}
						if (num2 >= 0f)
						{
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x04002BDC RID: 11228
	[SerializeField]
	public Storage storage;

	// Token: 0x04002BDD RID: 11229
	[SerializeField]
	public float targetLevel;

	// Token: 0x04002BDE RID: 11230
	[SerializeField]
	public CargoBay.CargoType cargoType;

	// Token: 0x04002BDF RID: 11231
	[MyCmpGet]
	private Filterable filterable;

	// Token: 0x04002BE0 RID: 11232
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002BE1 RID: 11233
	private const float TOLERANCE = 0.01f;

	// Token: 0x04002BE2 RID: 11234
	private CraftModuleInterface craftModuleInterface;
}
