using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class NativeAnimBatchLoader : MonoBehaviour
{
	// Token: 0x060016B6 RID: 5814 RVA: 0x00075DE0 File Offset: 0x00073FE0
	private void Start()
	{
		if (this.generateObjects)
		{
			for (int i = 0; i < this.enableObjects.Length; i++)
			{
				if (this.enableObjects[i] != null)
				{
					this.enableObjects[i].GetComponent<KBatchedAnimController>().visibilityType = KAnimControllerBase.VisibilityType.Always;
					this.enableObjects[i].SetActive(true);
				}
			}
		}
		if (this.setTimeScale)
		{
			Time.timeScale = 1f;
		}
		if (this.destroySelf)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x00075E5C File Offset: 0x0007405C
	private void LateUpdate()
	{
		if (this.destroySelf)
		{
			return;
		}
		if (this.performUpdate)
		{
			KAnimBatchManager.Instance().UpdateActiveArea(new Vector2I(0, 0), new Vector2I(9999, 9999));
			KAnimBatchManager.Instance().UpdateDirty(Time.frameCount);
		}
		if (this.performRender)
		{
			KAnimBatchManager.Instance().Render();
		}
	}

	// Token: 0x04000C9E RID: 3230
	public bool performTimeUpdate;

	// Token: 0x04000C9F RID: 3231
	public bool performUpdate;

	// Token: 0x04000CA0 RID: 3232
	public bool performRender;

	// Token: 0x04000CA1 RID: 3233
	public bool setTimeScale;

	// Token: 0x04000CA2 RID: 3234
	public bool destroySelf;

	// Token: 0x04000CA3 RID: 3235
	public bool generateObjects;

	// Token: 0x04000CA4 RID: 3236
	public GameObject[] enableObjects;
}
