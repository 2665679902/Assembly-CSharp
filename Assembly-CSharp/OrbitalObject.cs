using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200087D RID: 2173
[AddComponentMenu("KMonoBehaviour/scripts/OrbitalObject")]
[SerializationConfig(MemberSerialization.OptIn)]
public class OrbitalObject : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06003E61 RID: 15969 RVA: 0x0015CD34 File Offset: 0x0015AF34
	public void Init(string orbit_data_name, WorldContainer orbiting_world, List<Ref<OrbitalObject>> orbiting_obj)
	{
		OrbitalData orbitalData = Db.Get().OrbitalTypeCategories.Get(orbit_data_name);
		if (orbiting_world != null)
		{
			this.orbitingWorldId = orbiting_world.id;
			this.world = orbiting_world;
			this.worldOrbitingOrigin = this.GetWorldOrigin(this.world, orbitalData);
		}
		else
		{
			this.worldOrbitingOrigin = new Vector3((float)Grid.WidthInCells * 0.5f, (float)Grid.HeightInCells * orbitalData.yGridPercent, 0f);
		}
		this.animFilename = orbitalData.animFile;
		this.initialAnim = this.GetInitialAnim(orbitalData);
		this.angle = this.GetAngle(orbitalData);
		this.timeoffset = this.GetTimeOffset(orbiting_obj);
		this.orbitalDBId = orbitalData.Id;
	}

	// Token: 0x06003E62 RID: 15970 RVA: 0x0015CDEC File Offset: 0x0015AFEC
	protected override void OnSpawn()
	{
		this.world = ClusterManager.Instance.GetWorld(this.orbitingWorldId);
		this.orbitData = Db.Get().OrbitalTypeCategories.Get(this.orbitalDBId);
		base.gameObject.SetActive(false);
		KBatchedAnimController kbatchedAnimController = base.gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = this.initialAnim;
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(this.animFilename) };
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.Always;
	}

	// Token: 0x06003E63 RID: 15971 RVA: 0x0015CE84 File Offset: 0x0015B084
	public void RenderEveryTick(float dt)
	{
		float time = GameClock.Instance.GetTime();
		bool flag;
		Vector3 vector = this.CalculateWorldPos(time, out flag);
		Vector3 vector2 = vector;
		if (this.orbitData.periodInCycles > 0f)
		{
			vector2.x = vector.x / (float)Grid.WidthInCells;
			vector2.y = vector.y / (float)Grid.HeightInCells;
			vector2.x = Camera.main.ViewportToWorldPoint(vector2).x;
			vector2.y = Camera.main.ViewportToWorldPoint(vector2).y;
		}
		bool flag2 = (!this.orbitData.rotatesBehind || !flag) && (this.world == null || ClusterManager.Instance.activeWorldId == this.world.id);
		base.gameObject.transform.SetPosition(vector2);
		if (this.orbitData.periodInCycles > 0f)
		{
			base.gameObject.transform.localScale = Vector3.one * (CameraController.Instance.baseCamera.orthographicSize / this.orbitData.distance);
		}
		else
		{
			base.gameObject.transform.localScale = Vector3.one * this.orbitData.distance;
		}
		if (base.gameObject.activeSelf != flag2)
		{
			base.gameObject.SetActive(flag2);
		}
	}

	// Token: 0x06003E64 RID: 15972 RVA: 0x0015CFEC File Offset: 0x0015B1EC
	private Vector3 CalculateWorldPos(float time, out bool behind)
	{
		Vector3 vector3;
		if (this.orbitData.periodInCycles > 0f)
		{
			float num = this.orbitData.periodInCycles * 600f;
			float num2 = ((time + (float)this.timeoffset) / num - (float)((int)((time + (float)this.timeoffset) / num))) * 2f * 3.1415927f;
			float num3 = 0.5f * this.orbitData.radiusScale * (float)this.world.WorldSize.x;
			Vector3 vector = new Vector3(Mathf.Cos(num2), 0f, Mathf.Sin(num2));
			behind = vector.z > this.orbitData.behindZ;
			Vector3 vector2 = Quaternion.Euler(this.angle, 0f, 0f) * (vector * num3);
			vector3 = this.worldOrbitingOrigin + vector2;
			vector3.z = this.orbitData.renderZ;
		}
		else
		{
			behind = false;
			vector3 = this.worldOrbitingOrigin;
			vector3.z = this.orbitData.renderZ;
		}
		return vector3;
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x0015D0FC File Offset: 0x0015B2FC
	private string GetInitialAnim(OrbitalData data)
	{
		if (data.initialAnim.IsNullOrWhiteSpace())
		{
			KAnimFileData data2 = Assets.GetAnim(data.animFile).GetData();
			int num = new KRandom().Next(0, data2.animCount - 1);
			return data2.GetAnim(num).name;
		}
		return data.initialAnim;
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x0015D154 File Offset: 0x0015B354
	private Vector3 GetWorldOrigin(WorldContainer wc, OrbitalData data)
	{
		if (wc != null)
		{
			float num = (float)wc.WorldOffset.x + (float)wc.WorldSize.x * data.xGridPercent;
			float num2 = (float)wc.WorldOffset.y + (float)wc.WorldSize.y * data.yGridPercent;
			return new Vector3(num, num2, 0f);
		}
		return new Vector3((float)Grid.WidthInCells * data.xGridPercent, (float)Grid.HeightInCells * data.yGridPercent, 0f);
	}

	// Token: 0x06003E67 RID: 15975 RVA: 0x0015D1DB File Offset: 0x0015B3DB
	private float GetAngle(OrbitalData data)
	{
		return UnityEngine.Random.Range(data.minAngle, data.maxAngle);
	}

	// Token: 0x06003E68 RID: 15976 RVA: 0x0015D1F0 File Offset: 0x0015B3F0
	private int GetTimeOffset(List<Ref<OrbitalObject>> orbiting_obj)
	{
		List<int> list = new List<int>();
		foreach (Ref<OrbitalObject> @ref in orbiting_obj)
		{
			if (@ref.Get().world == this.world)
			{
				list.Add(@ref.Get().timeoffset);
			}
		}
		int num = UnityEngine.Random.Range(0, 600);
		while (list.Contains(num))
		{
			num = UnityEngine.Random.Range(0, 600);
		}
		return num;
	}

	// Token: 0x040028D1 RID: 10449
	private WorldContainer world;

	// Token: 0x040028D2 RID: 10450
	private OrbitalData orbitData;

	// Token: 0x040028D3 RID: 10451
	[Serialize]
	private string animFilename;

	// Token: 0x040028D4 RID: 10452
	[Serialize]
	private string initialAnim;

	// Token: 0x040028D5 RID: 10453
	[Serialize]
	private Vector3 worldOrbitingOrigin;

	// Token: 0x040028D6 RID: 10454
	[Serialize]
	private int orbitingWorldId;

	// Token: 0x040028D7 RID: 10455
	[Serialize]
	private float angle;

	// Token: 0x040028D8 RID: 10456
	[Serialize]
	public int timeoffset;

	// Token: 0x040028D9 RID: 10457
	[Serialize]
	public string orbitalDBId;
}
