using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[AddComponentMenu("KMonoBehaviour/prefabs/UIRotator")]
public class UIRotator : KMonoBehaviour
{
	// Token: 0x06000029 RID: 41 RVA: 0x00002B43 File Offset: 0x00000D43
	protected override void OnPrefabInit()
	{
		this.rotationSpeed = UnityEngine.Random.Range(this.minRotationSpeed, this.maxRotationSpeed);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002B5C File Offset: 0x00000D5C
	private void Update()
	{
		base.GetComponent<RectTransform>().Rotate(0f, 0f, this.rotationSpeed * Time.unscaledDeltaTime);
	}

	// Token: 0x04000022 RID: 34
	public float minRotationSpeed = 1f;

	// Token: 0x04000023 RID: 35
	public float maxRotationSpeed = 1f;

	// Token: 0x04000024 RID: 36
	public float rotationSpeed = 1f;
}
