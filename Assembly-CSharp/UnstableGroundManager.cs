using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020009B4 RID: 2484
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/UnstableGroundManager")]
public class UnstableGroundManager : KMonoBehaviour
{
	// Token: 0x060049C2 RID: 18882 RVA: 0x0019D3D0 File Offset: 0x0019B5D0
	protected override void OnPrefabInit()
	{
		this.fallingTileOffset = new Vector3(0.5f, 0f, 0f);
		UnstableGroundManager.EffectInfo[] array = this.effects;
		for (int i = 0; i < array.Length; i++)
		{
			UnstableGroundManager.EffectInfo effectInfo = array[i];
			GameObject prefab = effectInfo.prefab;
			prefab.SetActive(false);
			UnstableGroundManager.EffectRuntimeInfo effectRuntimeInfo = default(UnstableGroundManager.EffectRuntimeInfo);
			GameObjectPool pool = new GameObjectPool(() => this.InstantiateObj(prefab), 16);
			effectRuntimeInfo.pool = pool;
			effectRuntimeInfo.releaseFunc = delegate(GameObject go)
			{
				this.ReleaseGO(go);
				pool.ReleaseInstance(go);
			};
			this.runtimeInfo[effectInfo.element] = effectRuntimeInfo;
		}
	}

	// Token: 0x060049C3 RID: 18883 RVA: 0x0019D493 File Offset: 0x0019B693
	private void ReleaseGO(GameObject go)
	{
		if (GameComps.Gravities.Has(go))
		{
			GameComps.Gravities.Remove(go);
		}
		go.SetActive(false);
	}

	// Token: 0x060049C4 RID: 18884 RVA: 0x0019D4B4 File Offset: 0x0019B6B4
	private GameObject InstantiateObj(GameObject prefab)
	{
		GameObject gameObject = GameUtil.KInstantiate(prefab, Grid.SceneLayer.BuildingBack, null, 0);
		gameObject.SetActive(false);
		gameObject.name = "UnstablePool";
		return gameObject;
	}

	// Token: 0x060049C5 RID: 18885 RVA: 0x0019D4D4 File Offset: 0x0019B6D4
	public void Spawn(int cell, Element element, float mass, float temperature, byte disease_idx, int disease_count)
	{
		Vector3 vector = Grid.CellToPosCCC(cell, Grid.SceneLayer.TileMain);
		if (float.IsNaN(temperature) || float.IsInfinity(temperature))
		{
			global::Debug.LogError("Tried to spawn unstable ground with NaN temperature");
			temperature = 293f;
		}
		KBatchedAnimController kbatchedAnimController = this.Spawn(vector, element, mass, temperature, disease_idx, disease_count);
		kbatchedAnimController.Play("start", KAnim.PlayMode.Once, 1f, 0f);
		kbatchedAnimController.Play("loop", KAnim.PlayMode.Loop, 1f, 0f);
		kbatchedAnimController.gameObject.name = "Falling " + element.name;
		GameComps.Gravities.Add(kbatchedAnimController.gameObject, Vector2.zero, null);
		this.fallingObjects.Add(kbatchedAnimController.gameObject);
		this.SpawnPuff(vector, element, mass, temperature, disease_idx, disease_count);
		Substance substance = element.substance;
		if (substance != null && !substance.fallingStartSound.IsNull && CameraController.Instance.IsAudibleSound(vector, substance.fallingStartSound))
		{
			SoundEvent.PlayOneShot(substance.fallingStartSound, vector, 1f);
		}
	}

	// Token: 0x060049C6 RID: 18886 RVA: 0x0019D5E4 File Offset: 0x0019B7E4
	private void SpawnOld(Vector3 pos, Element element, float mass, float temperature, byte disease_idx, int disease_count)
	{
		if (!element.IsUnstable)
		{
			global::Debug.LogError("Spawning falling ground with a stable element");
		}
		KBatchedAnimController kbatchedAnimController = this.Spawn(pos, element, mass, temperature, disease_idx, disease_count);
		GameComps.Gravities.Add(kbatchedAnimController.gameObject, Vector2.zero, null);
		kbatchedAnimController.Play("loop", KAnim.PlayMode.Loop, 1f, 0f);
		this.fallingObjects.Add(kbatchedAnimController.gameObject);
		kbatchedAnimController.gameObject.name = "SpawnOld " + element.name;
	}

	// Token: 0x060049C7 RID: 18887 RVA: 0x0019D674 File Offset: 0x0019B874
	private void SpawnPuff(Vector3 pos, Element element, float mass, float temperature, byte disease_idx, int disease_count)
	{
		if (!element.IsUnstable)
		{
			global::Debug.LogError("Spawning sand puff with a stable element");
		}
		KBatchedAnimController kbatchedAnimController = this.Spawn(pos, element, mass, temperature, disease_idx, disease_count);
		kbatchedAnimController.Play("sandPuff", KAnim.PlayMode.Once, 1f, 0f);
		kbatchedAnimController.gameObject.name = "Puff " + element.name;
		kbatchedAnimController.transform.SetPosition(kbatchedAnimController.transform.GetPosition() + this.spawnPuffOffset);
	}

	// Token: 0x060049C8 RID: 18888 RVA: 0x0019D6FC File Offset: 0x0019B8FC
	private KBatchedAnimController Spawn(Vector3 pos, Element element, float mass, float temperature, byte disease_idx, int disease_count)
	{
		UnstableGroundManager.EffectRuntimeInfo effectRuntimeInfo;
		if (!this.runtimeInfo.TryGetValue(element.id, out effectRuntimeInfo))
		{
			global::Debug.LogError(element.id.ToString() + " needs unstable ground info hookup!");
		}
		GameObject instance = effectRuntimeInfo.pool.GetInstance();
		instance.transform.SetPosition(pos);
		if (float.IsNaN(temperature) || float.IsInfinity(temperature))
		{
			global::Debug.LogError("Tried to spawn unstable ground with NaN temperature");
			temperature = 293f;
		}
		UnstableGround component = instance.GetComponent<UnstableGround>();
		component.element = element.id;
		component.mass = mass;
		component.temperature = temperature;
		component.diseaseIdx = disease_idx;
		component.diseaseCount = disease_count;
		instance.SetActive(true);
		KBatchedAnimController component2 = instance.GetComponent<KBatchedAnimController>();
		component2.onDestroySelf = effectRuntimeInfo.releaseFunc;
		component2.Stop();
		if (element.substance != null)
		{
			component2.TintColour = element.substance.colour;
		}
		return component2;
	}

	// Token: 0x060049C9 RID: 18889 RVA: 0x0019D7E4 File Offset: 0x0019B9E4
	public List<int> GetCellsContainingFallingAbove(Vector2I cellXY)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.fallingObjects.Count; i++)
		{
			Vector2I vector2I;
			Grid.PosToXY(this.fallingObjects[i].transform.GetPosition(), out vector2I);
			if (vector2I.x == cellXY.x && vector2I.y >= cellXY.y)
			{
				int num = Grid.PosToCell(vector2I);
				list.Add(num);
			}
		}
		for (int j = 0; j < this.pendingCells.Count; j++)
		{
			Vector2I vector2I2 = Grid.CellToXY(this.pendingCells[j]);
			if (vector2I2.x == cellXY.x && vector2I2.y >= cellXY.y)
			{
				list.Add(this.pendingCells[j]);
			}
		}
		return list;
	}

	// Token: 0x060049CA RID: 18890 RVA: 0x0019D8B9 File Offset: 0x0019BAB9
	private void RemoveFromPending(int cell)
	{
		this.pendingCells.Remove(cell);
	}

	// Token: 0x060049CB RID: 18891 RVA: 0x0019D8C8 File Offset: 0x0019BAC8
	private void Update()
	{
		if (App.isLoading)
		{
			return;
		}
		int i = 0;
		while (i < this.fallingObjects.Count)
		{
			GameObject gameObject = this.fallingObjects[i];
			if (!(gameObject == null))
			{
				Vector3 position = gameObject.transform.GetPosition();
				int cell = Grid.PosToCell(position);
				Grid.CellRight(cell);
				Grid.CellLeft(cell);
				int num = Grid.CellBelow(cell);
				Grid.CellRight(num);
				Grid.CellLeft(num);
				int cell2 = cell;
				if (!Grid.IsValidCell(num) || Grid.Element[num].IsSolid || (Grid.Properties[num] & 4) != 0)
				{
					UnstableGround component = gameObject.GetComponent<UnstableGround>();
					this.pendingCells.Add(cell2);
					HandleVector<Game.CallbackInfo>.Handle handle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(delegate
					{
						this.RemoveFromPending(cell);
					}, false));
					SimMessages.AddRemoveSubstance(cell2, component.element, CellEventLogger.Instance.UnstableGround, component.mass, component.temperature, component.diseaseIdx, component.diseaseCount, true, handle.index);
					ListPool<ScenePartitionerEntry, UnstableGroundManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, UnstableGroundManager>.Allocate();
					Vector2I vector2I = Grid.CellToXY(cell);
					vector2I.x = Mathf.Max(0, vector2I.x - 1);
					vector2I.y = Mathf.Min(Grid.HeightInCells - 1, vector2I.y + 1);
					GameScenePartitioner.Instance.GatherEntries(vector2I.x, vector2I.y, 3, 3, GameScenePartitioner.Instance.collisionLayer, pooledList);
					foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
					{
						if (scenePartitionerEntry.obj is KCollider2D)
						{
							(scenePartitionerEntry.obj as KCollider2D).gameObject.Trigger(-975551167, null);
						}
					}
					pooledList.Recycle();
					Element element = ElementLoader.FindElementByHash(component.element);
					if (element != null && element.substance != null && !element.substance.fallingStopSound.IsNull && CameraController.Instance.IsAudibleSound(position, element.substance.fallingStopSound))
					{
						SoundEvent.PlayOneShot(element.substance.fallingStopSound, position, 1f);
					}
					GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.OreAbsorbId), position + this.landEffectOffset, Grid.SceneLayer.Front, null, 0).SetActive(true);
					this.fallingObjects[i] = this.fallingObjects[this.fallingObjects.Count - 1];
					this.fallingObjects.RemoveAt(this.fallingObjects.Count - 1);
					this.ReleaseGO(gameObject);
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x060049CC RID: 18892 RVA: 0x0019DBBC File Offset: 0x0019BDBC
	[OnSerializing]
	private void OnSerializing()
	{
		if (this.fallingObjects.Count > 0)
		{
			this.serializedInfo = new List<UnstableGroundManager.SerializedInfo>();
		}
		foreach (GameObject gameObject in this.fallingObjects)
		{
			UnstableGround component = gameObject.GetComponent<UnstableGround>();
			byte diseaseIdx = component.diseaseIdx;
			int num = ((diseaseIdx != byte.MaxValue) ? Db.Get().Diseases[(int)diseaseIdx].id.HashValue : 0);
			this.serializedInfo.Add(new UnstableGroundManager.SerializedInfo
			{
				position = gameObject.transform.GetPosition(),
				element = component.element,
				mass = component.mass,
				temperature = component.temperature,
				diseaseID = num,
				diseaseCount = component.diseaseCount
			});
		}
	}

	// Token: 0x060049CD RID: 18893 RVA: 0x0019DCC0 File Offset: 0x0019BEC0
	[OnSerialized]
	private void OnSerialized()
	{
		this.serializedInfo = null;
	}

	// Token: 0x060049CE RID: 18894 RVA: 0x0019DCCC File Offset: 0x0019BECC
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.serializedInfo == null)
		{
			return;
		}
		this.fallingObjects.Clear();
		HashedString hashedString = default(HashedString);
		foreach (UnstableGroundManager.SerializedInfo serializedInfo in this.serializedInfo)
		{
			Element element = ElementLoader.FindElementByHash(serializedInfo.element);
			hashedString.HashValue = serializedInfo.diseaseID;
			byte index = Db.Get().Diseases.GetIndex(hashedString);
			int num = serializedInfo.diseaseCount;
			if (index == 255)
			{
				num = 0;
			}
			this.SpawnOld(serializedInfo.position, element, serializedInfo.mass, serializedInfo.temperature, index, num);
		}
	}

	// Token: 0x04003073 RID: 12403
	[SerializeField]
	private Vector3 spawnPuffOffset;

	// Token: 0x04003074 RID: 12404
	[SerializeField]
	private Vector3 landEffectOffset;

	// Token: 0x04003075 RID: 12405
	private Vector3 fallingTileOffset;

	// Token: 0x04003076 RID: 12406
	[SerializeField]
	private UnstableGroundManager.EffectInfo[] effects;

	// Token: 0x04003077 RID: 12407
	private List<GameObject> fallingObjects = new List<GameObject>();

	// Token: 0x04003078 RID: 12408
	private List<int> pendingCells = new List<int>();

	// Token: 0x04003079 RID: 12409
	private Dictionary<SimHashes, UnstableGroundManager.EffectRuntimeInfo> runtimeInfo = new Dictionary<SimHashes, UnstableGroundManager.EffectRuntimeInfo>();

	// Token: 0x0400307A RID: 12410
	[Serialize]
	private List<UnstableGroundManager.SerializedInfo> serializedInfo;

	// Token: 0x020017B8 RID: 6072
	[Serializable]
	private struct EffectInfo
	{
		// Token: 0x04006DD5 RID: 28117
		[HashedEnum]
		public SimHashes element;

		// Token: 0x04006DD6 RID: 28118
		public GameObject prefab;
	}

	// Token: 0x020017B9 RID: 6073
	private struct EffectRuntimeInfo
	{
		// Token: 0x04006DD7 RID: 28119
		public GameObjectPool pool;

		// Token: 0x04006DD8 RID: 28120
		public Action<GameObject> releaseFunc;
	}

	// Token: 0x020017BA RID: 6074
	private struct SerializedInfo
	{
		// Token: 0x04006DD9 RID: 28121
		public Vector3 position;

		// Token: 0x04006DDA RID: 28122
		public SimHashes element;

		// Token: 0x04006DDB RID: 28123
		public float mass;

		// Token: 0x04006DDC RID: 28124
		public float temperature;

		// Token: 0x04006DDD RID: 28125
		public int diseaseID;

		// Token: 0x04006DDE RID: 28126
		public int diseaseCount;
	}
}
