using System;
using UnityEngine;

// Token: 0x0200040C RID: 1036
public class BatchAnimCamera : MonoBehaviour
{
	// Token: 0x0600158F RID: 5519 RVA: 0x0006F9AA File Offset: 0x0006DBAA
	private void Awake()
	{
		this.cam = base.GetComponent<Camera>();
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x0006F9B8 File Offset: 0x0006DBB8
	private void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			base.transform.SetPosition(base.transform.GetPosition() + Vector3.right * BatchAnimCamera.pan_speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			base.transform.SetPosition(base.transform.GetPosition() + Vector3.left * BatchAnimCamera.pan_speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			base.transform.SetPosition(base.transform.GetPosition() + Vector3.up * BatchAnimCamera.pan_speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			base.transform.SetPosition(base.transform.GetPosition() + Vector3.down * BatchAnimCamera.pan_speed * Time.deltaTime);
		}
		this.ClampToBounds();
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.do_pan = true;
				this.last_pan = KInputManager.GetMousePos();
			}
			else if (Input.GetMouseButton(0) && this.do_pan)
			{
				Vector3 vector = this.cam.ScreenToViewportPoint(this.last_pan - KInputManager.GetMousePos());
				Vector3 vector2 = new Vector3(vector.x * BatchAnimCamera.pan_speed, vector.y * BatchAnimCamera.pan_speed, 0f);
				base.transform.Translate(vector2, Space.World);
				this.ClampToBounds();
				this.last_pan = KInputManager.GetMousePos();
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.do_pan = false;
		}
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			this.cam.fieldOfView = Mathf.Clamp(this.cam.fieldOfView - axis * BatchAnimCamera.zoom_speed, this.zoom_min, this.zoom_max);
		}
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x0006FBBC File Offset: 0x0006DDBC
	private void ClampToBounds()
	{
		Vector3 position = base.transform.GetPosition();
		position.x = Mathf.Clamp(base.transform.GetPosition().x, BatchAnimCamera.bounds.min.x, BatchAnimCamera.bounds.max.x);
		position.y = Mathf.Clamp(base.transform.GetPosition().y, BatchAnimCamera.bounds.min.y, BatchAnimCamera.bounds.max.y);
		position.z = Mathf.Clamp(base.transform.GetPosition().z, BatchAnimCamera.bounds.min.z, BatchAnimCamera.bounds.max.z);
		base.transform.SetPosition(position);
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x0006FC90 File Offset: 0x0006DE90
	private void OnDrawGizmosSelected()
	{
		DebugExtension.DebugBounds(BatchAnimCamera.bounds, Color.red, 0f, true);
	}

	// Token: 0x04000C01 RID: 3073
	private static readonly float pan_speed = 5f;

	// Token: 0x04000C02 RID: 3074
	private static readonly float zoom_speed = 5f;

	// Token: 0x04000C03 RID: 3075
	public static Bounds bounds = new Bounds(new Vector3(0f, 0f, -50f), new Vector3(0f, 0f, 50f));

	// Token: 0x04000C04 RID: 3076
	private float zoom_min = 1f;

	// Token: 0x04000C05 RID: 3077
	private float zoom_max = 100f;

	// Token: 0x04000C06 RID: 3078
	private Camera cam;

	// Token: 0x04000C07 RID: 3079
	private bool do_pan;

	// Token: 0x04000C08 RID: 3080
	private Vector3 last_pan;
}
