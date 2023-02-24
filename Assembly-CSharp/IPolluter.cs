using System;
using UnityEngine;

// Token: 0x02000869 RID: 2153
public interface IPolluter
{
	// Token: 0x06003DC1 RID: 15809
	int GetRadius();

	// Token: 0x06003DC2 RID: 15810
	int GetNoise();

	// Token: 0x06003DC3 RID: 15811
	GameObject GetGameObject();

	// Token: 0x06003DC4 RID: 15812
	void SetAttributes(Vector2 pos, int dB, GameObject go, string name = null);

	// Token: 0x06003DC5 RID: 15813
	string GetName();

	// Token: 0x06003DC6 RID: 15814
	Vector2 GetPosition();

	// Token: 0x06003DC7 RID: 15815
	void Clear();

	// Token: 0x06003DC8 RID: 15816
	void SetSplat(NoiseSplat splat);
}
