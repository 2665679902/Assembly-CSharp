using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008C9 RID: 2249
public class MultipleRenderTarget : MonoBehaviour
{
	// Token: 0x1400001C RID: 28
	// (add) Token: 0x060040A7 RID: 16551 RVA: 0x0016A120 File Offset: 0x00168320
	// (remove) Token: 0x060040A8 RID: 16552 RVA: 0x0016A158 File Offset: 0x00168358
	public event Action<Camera> onSetupComplete;

	// Token: 0x060040A9 RID: 16553 RVA: 0x0016A18D File Offset: 0x0016838D
	private void Start()
	{
		base.StartCoroutine(this.SetupProxy());
	}

	// Token: 0x060040AA RID: 16554 RVA: 0x0016A19C File Offset: 0x0016839C
	private IEnumerator SetupProxy()
	{
		yield return null;
		Camera component = base.GetComponent<Camera>();
		Camera camera = new GameObject().AddComponent<Camera>();
		camera.CopyFrom(component);
		this.renderProxy = camera.gameObject.AddComponent<MultipleRenderTargetProxy>();
		camera.name = component.name + " MRT";
		camera.transform.parent = component.transform;
		camera.transform.SetLocalPosition(Vector3.zero);
		camera.depth = component.depth - 1f;
		component.cullingMask = 0;
		component.clearFlags = CameraClearFlags.Color;
		this.quad = new FullScreenQuad("MultipleRenderTarget", component, true);
		if (this.onSetupComplete != null)
		{
			this.onSetupComplete(camera);
		}
		yield break;
	}

	// Token: 0x060040AB RID: 16555 RVA: 0x0016A1AB File Offset: 0x001683AB
	private void OnPreCull()
	{
		if (this.renderProxy != null)
		{
			this.quad.Draw(this.renderProxy.Textures[0]);
		}
	}

	// Token: 0x060040AC RID: 16556 RVA: 0x0016A1D3 File Offset: 0x001683D3
	public void ToggleColouredOverlayView(bool enabled)
	{
		if (this.renderProxy != null)
		{
			this.renderProxy.ToggleColouredOverlayView(enabled);
		}
	}

	// Token: 0x04002B17 RID: 11031
	private MultipleRenderTargetProxy renderProxy;

	// Token: 0x04002B18 RID: 11032
	private FullScreenQuad quad;

	// Token: 0x04002B1A RID: 11034
	public bool isFrontEnd;
}
