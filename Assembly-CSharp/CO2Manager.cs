using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000678 RID: 1656
[AddComponentMenu("KMonoBehaviour/scripts/CO2Manager")]
public class CO2Manager : KMonoBehaviour, ISim33ms
{
	// Token: 0x06002CA9 RID: 11433 RVA: 0x000EA1A8 File Offset: 0x000E83A8
	public static void DestroyInstance()
	{
		CO2Manager.instance = null;
	}

	// Token: 0x06002CAA RID: 11434 RVA: 0x000EA1B0 File Offset: 0x000E83B0
	protected override void OnPrefabInit()
	{
		CO2Manager.instance = this;
		this.prefab.gameObject.SetActive(false);
		this.breathPrefab.SetActive(false);
		this.co2Pool = new GameObjectPool(new Func<GameObject>(this.InstantiateCO2), 16);
		this.breathPool = new GameObjectPool(new Func<GameObject>(this.InstantiateBreath), 16);
	}

	// Token: 0x06002CAB RID: 11435 RVA: 0x000EA212 File Offset: 0x000E8412
	private GameObject InstantiateCO2()
	{
		GameObject gameObject = GameUtil.KInstantiate(this.prefab, Grid.SceneLayer.Front, null, 0);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x06002CAC RID: 11436 RVA: 0x000EA22A File Offset: 0x000E842A
	private GameObject InstantiateBreath()
	{
		GameObject gameObject = GameUtil.KInstantiate(this.breathPrefab, Grid.SceneLayer.Front, null, 0);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x000EA244 File Offset: 0x000E8444
	public void Sim33ms(float dt)
	{
		Vector2I vector2I = default(Vector2I);
		Vector2I vector2I2 = default(Vector2I);
		Vector3 vector = this.acceleration * dt;
		int num = this.co2Items.Count;
		for (int i = 0; i < num; i++)
		{
			CO2 co = this.co2Items[i];
			co.velocity += vector;
			co.lifetimeRemaining -= dt;
			Grid.PosToXY(co.transform.GetPosition(), out vector2I);
			co.transform.SetPosition(co.transform.GetPosition() + co.velocity * dt);
			Grid.PosToXY(co.transform.GetPosition(), out vector2I2);
			int num2 = Grid.XYToCell(vector2I.x, vector2I.y);
			int j = vector2I.y;
			while (j >= vector2I2.y)
			{
				int num3 = Grid.XYToCell(vector2I.x, j);
				bool flag = !Grid.IsValidCell(num3) || co.lifetimeRemaining <= 0f;
				if (!flag)
				{
					Element element = Grid.Element[num3];
					flag = element.IsLiquid || element.IsSolid || (Grid.Properties[num3] & 1) > 0;
				}
				if (flag)
				{
					int num4 = num3;
					bool flag2 = false;
					if (num2 != num3)
					{
						num4 = num2;
						flag2 = true;
					}
					else
					{
						bool flag3 = false;
						int num5 = -1;
						int num6 = -1;
						foreach (CellOffset cellOffset in OxygenBreather.DEFAULT_BREATHABLE_OFFSETS)
						{
							int num7 = Grid.OffsetCell(num3, cellOffset);
							if (Grid.IsValidCell(num7))
							{
								Element element2 = Grid.Element[num7];
								if (element2.id == SimHashes.CarbonDioxide || element2.HasTag(GameTags.Breathable))
								{
									num5 = num7;
									flag3 = true;
									flag2 = true;
									break;
								}
								if (element2.IsGas)
								{
									num6 = num7;
									flag2 = true;
								}
							}
						}
						if (flag2)
						{
							if (flag3)
							{
								num4 = num5;
							}
							else
							{
								num4 = num6;
							}
						}
					}
					co.TriggerDestroy();
					if (flag2)
					{
						SimMessages.ModifyMass(num4, co.mass, byte.MaxValue, 0, CellEventLogger.Instance.CO2ManagerFixedUpdate, co.temperature, SimHashes.CarbonDioxide);
						num--;
						this.co2Items[i] = this.co2Items[num];
						this.co2Items.RemoveAt(num);
						break;
					}
					break;
				}
				else
				{
					num2 = num3;
					j--;
				}
			}
		}
	}

	// Token: 0x06002CAE RID: 11438 RVA: 0x000EA4C4 File Offset: 0x000E86C4
	public void SpawnCO2(Vector3 position, float mass, float temperature, bool flip)
	{
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Front);
		GameObject gameObject = this.co2Pool.GetInstance();
		gameObject.transform.SetPosition(position);
		gameObject.SetActive(true);
		CO2 component = gameObject.GetComponent<CO2>();
		component.mass = mass;
		component.temperature = temperature;
		component.velocity = Vector3.zero;
		component.lifetimeRemaining = 3f;
		KBatchedAnimController component2 = component.GetComponent<KBatchedAnimController>();
		component2.TintColour = this.tintColour;
		component2.onDestroySelf = new Action<GameObject>(this.OnDestroyCO2);
		component2.FlipX = flip;
		component.StartLoop();
		this.co2Items.Add(component);
	}

	// Token: 0x06002CAF RID: 11439 RVA: 0x000EA56C File Offset: 0x000E876C
	public void SpawnBreath(Vector3 position, float mass, float temperature, bool flip)
	{
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Front);
		this.SpawnCO2(position, mass, temperature, flip);
		GameObject gameObject = this.breathPool.GetInstance();
		gameObject.transform.SetPosition(position);
		gameObject.SetActive(true);
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		component.TintColour = this.tintColour;
		component.onDestroySelf = new Action<GameObject>(this.OnDestroyBreath);
		component.FlipX = flip;
		component.Play("breath", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002CB0 RID: 11440 RVA: 0x000EA5FB File Offset: 0x000E87FB
	private void OnDestroyCO2(GameObject co2_go)
	{
		co2_go.SetActive(false);
		this.co2Pool.ReleaseInstance(co2_go);
	}

	// Token: 0x06002CB1 RID: 11441 RVA: 0x000EA610 File Offset: 0x000E8810
	private void OnDestroyBreath(GameObject breath_go)
	{
		breath_go.SetActive(false);
		this.breathPool.ReleaseInstance(breath_go);
	}

	// Token: 0x04001A93 RID: 6803
	private const float CO2Lifetime = 3f;

	// Token: 0x04001A94 RID: 6804
	[SerializeField]
	private Vector3 acceleration;

	// Token: 0x04001A95 RID: 6805
	[SerializeField]
	private CO2 prefab;

	// Token: 0x04001A96 RID: 6806
	[SerializeField]
	private GameObject breathPrefab;

	// Token: 0x04001A97 RID: 6807
	[SerializeField]
	private Color tintColour;

	// Token: 0x04001A98 RID: 6808
	private List<CO2> co2Items = new List<CO2>();

	// Token: 0x04001A99 RID: 6809
	private GameObjectPool breathPool;

	// Token: 0x04001A9A RID: 6810
	private GameObjectPool co2Pool;

	// Token: 0x04001A9B RID: 6811
	public static CO2Manager instance;
}
