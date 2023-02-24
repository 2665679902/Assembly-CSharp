using System;

// Token: 0x0200050C RID: 1292
public class OrbitalData : Resource
{
	// Token: 0x06001EEF RID: 7919 RVA: 0x000A5630 File Offset: 0x000A3830
	public OrbitalData(string id, ResourceSet parent, string animFile = "earth_kanim", string initialAnim = "", OrbitalData.OrbitalType orbitalType = OrbitalData.OrbitalType.poi, float periodInCycles = 1f, float xGridPercent = 0.5f, float yGridPercent = 0.5f, float minAngle = -350f, float maxAngle = 350f, float radiusScale = 1.05f, bool rotatesBehind = true, float behindZ = 0.05f, float distance = 25f, float renderZ = 1f)
		: base(id, parent, null)
	{
		this.animFile = animFile;
		this.initialAnim = initialAnim;
		this.orbitalType = orbitalType;
		this.periodInCycles = periodInCycles;
		this.xGridPercent = xGridPercent;
		this.yGridPercent = yGridPercent;
		this.minAngle = minAngle;
		this.maxAngle = maxAngle;
		this.radiusScale = radiusScale;
		this.rotatesBehind = rotatesBehind;
		this.behindZ = behindZ;
		this.distance = distance;
		this.renderZ = renderZ;
	}

	// Token: 0x04001189 RID: 4489
	public string animFile;

	// Token: 0x0400118A RID: 4490
	public string initialAnim;

	// Token: 0x0400118B RID: 4491
	public float periodInCycles;

	// Token: 0x0400118C RID: 4492
	public float xGridPercent;

	// Token: 0x0400118D RID: 4493
	public float yGridPercent;

	// Token: 0x0400118E RID: 4494
	public float minAngle;

	// Token: 0x0400118F RID: 4495
	public float maxAngle;

	// Token: 0x04001190 RID: 4496
	public float radiusScale;

	// Token: 0x04001191 RID: 4497
	public bool rotatesBehind;

	// Token: 0x04001192 RID: 4498
	public float behindZ;

	// Token: 0x04001193 RID: 4499
	public float distance;

	// Token: 0x04001194 RID: 4500
	public float renderZ;

	// Token: 0x04001195 RID: 4501
	public OrbitalData.OrbitalType orbitalType;

	// Token: 0x0200114A RID: 4426
	public enum OrbitalType
	{
		// Token: 0x04005A6F RID: 23151
		world,
		// Token: 0x04005A70 RID: 23152
		poi,
		// Token: 0x04005A71 RID: 23153
		inOrbit,
		// Token: 0x04005A72 RID: 23154
		landed
	}
}
