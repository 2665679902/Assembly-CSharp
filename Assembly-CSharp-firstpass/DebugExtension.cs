using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000003 RID: 3
public static class DebugExtension
{
	// Token: 0x0600001B RID: 27 RVA: 0x000023C8 File Offset: 0x000005C8
	public static void DebugPoint(Vector3 position, Color color, float scale = 1f, float duration = 0f, bool depthTest = true)
	{
		color = ((color == default(Color)) ? Color.white : color);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000023F0 File Offset: 0x000005F0
	public static void DebugPoint(Vector3 position, float scale = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugPoint(position, Color.white, scale, duration, depthTest);
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002400 File Offset: 0x00000600
	public static void DebugBounds(Bounds bounds, Color color, float duration = 0f, bool depthTest = true)
	{
		float x = bounds.extents.x;
		float y = bounds.extents.y;
		float z = bounds.extents.z;
		DebugExtension.DebugExtense(x, y, z, bounds.center, color, duration, depthTest);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002444 File Offset: 0x00000644
	public static void DebugExtense(float x, float y, float z, Vector3 center, Color color, float duration = 0f, bool depthTest = true)
	{
		center + new Vector3(x, y, z);
		center + new Vector3(x, y, -z);
		center + new Vector3(-x, y, z);
		center + new Vector3(-x, y, -z);
		center + new Vector3(x, -y, z);
		center + new Vector3(x, -y, -z);
		center + new Vector3(-x, -y, z);
		center + new Vector3(-x, -y, -z);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000024D8 File Offset: 0x000006D8
	public static void DebugAABB(AABB3 bounds, Color color, float duration = 0f, bool depthTest = true)
	{
		Vector3 range = bounds.Range;
		DebugExtension.DebugExtense(range.x, range.y, range.z, bounds.Center, color, duration, depthTest);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002510 File Offset: 0x00000710
	public static void DebugAABB(Vector3 position, AABB3 bounds, Color color, float duration = 0f, bool depthTest = true)
	{
		Vector3 vector = bounds.Range * 0.5f;
		DebugExtension.DebugExtense(vector.x, vector.y, vector.z, position, color, duration, depthTest);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000254B File Offset: 0x0000074B
	public static void DebugRect(Rect rect, Color color, float duration = 0f, bool depthTest = true)
	{
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000254D File Offset: 0x0000074D
	public static void DebugBounds(Bounds bounds, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugBounds(bounds, Color.white, duration, depthTest);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000255C File Offset: 0x0000075C
	public static void DebugLocalCube(Transform transform, Vector3 size, Color color, Vector3 center = default(Vector3), float duration = 0f, bool depthTest = true)
	{
		transform.TransformPoint(center + -size * 0.5f);
		transform.TransformPoint(center + new Vector3(size.x, -size.y, -size.z) * 0.5f);
		transform.TransformPoint(center + new Vector3(size.x, -size.y, size.z) * 0.5f);
		transform.TransformPoint(center + new Vector3(-size.x, -size.y, size.z) * 0.5f);
		transform.TransformPoint(center + new Vector3(-size.x, size.y, -size.z) * 0.5f);
		transform.TransformPoint(center + new Vector3(size.x, size.y, -size.z) * 0.5f);
		transform.TransformPoint(center + size * 0.5f);
		transform.TransformPoint(center + new Vector3(-size.x, size.y, size.z) * 0.5f);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000026BB File Offset: 0x000008BB
	public static void DebugLocalCube(Transform transform, Vector3 size, Vector3 center = default(Vector3), float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugLocalCube(transform, size, Color.white, center, duration, depthTest);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000026D0 File Offset: 0x000008D0
	public static void DebugLocalCube(Matrix4x4 space, Vector3 size, Color color, Vector3 center = default(Vector3), float duration = 0f, bool depthTest = true)
	{
		color = ((color == default(Color)) ? Color.white : color);
		space.MultiplyPoint3x4(center + -size * 0.5f);
		space.MultiplyPoint3x4(center + new Vector3(size.x, -size.y, -size.z) * 0.5f);
		space.MultiplyPoint3x4(center + new Vector3(size.x, -size.y, size.z) * 0.5f);
		space.MultiplyPoint3x4(center + new Vector3(-size.x, -size.y, size.z) * 0.5f);
		space.MultiplyPoint3x4(center + new Vector3(-size.x, size.y, -size.z) * 0.5f);
		space.MultiplyPoint3x4(center + new Vector3(size.x, size.y, -size.z) * 0.5f);
		space.MultiplyPoint3x4(center + size * 0.5f);
		space.MultiplyPoint3x4(center + new Vector3(-size.x, size.y, size.z) * 0.5f);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002852 File Offset: 0x00000A52
	public static void DebugLocalCube(Matrix4x4 space, Vector3 size, Vector3 center = default(Vector3), float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugLocalCube(space, size, Color.white, center, duration, depthTest);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002864 File Offset: 0x00000A64
	public static void DebugCircle(Vector3 position, Vector3 up, Color color, float radius = 1f, float duration = 0f, bool depthTest = true, float jumpPerSegment = 4f)
	{
		Vector3 vector = up.normalized * radius;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3 vector3 = Vector3.Cross(vector, vector2).normalized * radius;
		Matrix4x4 matrix4x = default(Matrix4x4);
		matrix4x[0] = vector3.x;
		matrix4x[1] = vector3.y;
		matrix4x[2] = vector3.z;
		matrix4x[4] = vector.x;
		matrix4x[5] = vector.y;
		matrix4x[6] = vector.z;
		matrix4x[8] = vector2.x;
		matrix4x[9] = vector2.y;
		matrix4x[10] = vector2.z;
		position + matrix4x.MultiplyPoint3x4(new Vector3(Mathf.Cos(0f), 0f, Mathf.Sin(0f)));
		Vector3 vector4 = Vector3.zero;
		color = ((color == default(Color)) ? Color.white : color);
		int num = 0;
		while ((float)num < 364f / jumpPerSegment)
		{
			vector4.x = Mathf.Cos((float)num * jumpPerSegment * 0.017453292f);
			vector4.z = Mathf.Sin((float)num * jumpPerSegment * 0.017453292f);
			vector4.y = 0f;
			vector4 = position + matrix4x.MultiplyPoint3x4(vector4);
			num++;
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000029E4 File Offset: 0x00000BE4
	public static void DebugCircle(Vector3 position, Color color, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCircle(position, Vector3.up, color, radius, duration, depthTest, 4f);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000029FB File Offset: 0x00000BFB
	public static void DebugCircle2d(Vector2 position, Color color, float radius = 1f, float duration = 0f, bool depthTest = true, float jumpPerSegment = 4f)
	{
		DebugExtension.DebugCircle(position, Vector3.forward, color, radius, duration, depthTest, jumpPerSegment);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002A14 File Offset: 0x00000C14
	public static void DebugCircle(Vector3 position, Vector3 up, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCircle(position, up, Color.white, radius, duration, depthTest, 4f);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002A2B File Offset: 0x00000C2B
	public static void DebugCircle(Vector3 position, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCircle(position, Vector3.up, Color.white, radius, duration, depthTest, 4f);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002A48 File Offset: 0x00000C48
	public static void DebugWireSphere(Vector3 position, Color color, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		float num = 10f;
		new Vector3(position.x, position.y + radius * Mathf.Sin(0f), position.z + radius * Mathf.Cos(0f));
		new Vector3(position.x + radius * Mathf.Cos(0f), position.y, position.z + radius * Mathf.Sin(0f));
		new Vector3(position.x + radius * Mathf.Cos(0f), position.y + radius * Mathf.Sin(0f), position.z);
		for (int i = 1; i < 37; i++)
		{
			Vector3 vector = new Vector3(position.x, position.y + radius * Mathf.Sin(num * (float)i * 0.017453292f), position.z + radius * Mathf.Cos(num * (float)i * 0.017453292f));
			Vector3 vector2 = new Vector3(position.x + radius * Mathf.Cos(num * (float)i * 0.017453292f), position.y, position.z + radius * Mathf.Sin(num * (float)i * 0.017453292f));
			new Vector3(position.x + radius * Mathf.Cos(num * (float)i * 0.017453292f), position.y + radius * Mathf.Sin(num * (float)i * 0.017453292f), position.z);
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002BBA File Offset: 0x00000DBA
	public static void DebugWireSphere(Vector3 position, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugWireSphere(position, Color.white, radius, duration, depthTest);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002BCC File Offset: 0x00000DCC
	public static void DebugCylinder(Vector3 start, Vector3 end, Color color, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		Vector3 vector = (end - start).normalized * radius;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3.Cross(vector, vector2).normalized * radius;
		DebugExtension.DebugCircle(start, vector, color, radius, duration, depthTest, 4f);
		DebugExtension.DebugCircle(end, -vector, color, radius, duration, depthTest, 4f);
		DebugExtension.DebugCircle((start + end) * 0.5f, vector, color, radius, duration, depthTest, 4f);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002C62 File Offset: 0x00000E62
	public static void DebugCylinder(Vector3 start, Vector3 end, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCylinder(start, end, Color.white, radius, duration, depthTest);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002C74 File Offset: 0x00000E74
	public static void DebugCone(Vector3 position, Vector3 direction, Color color, float angle = 45f, float duration = 0f, bool depthTest = true)
	{
		float magnitude = direction.magnitude;
		Vector3 vector = direction;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3.Cross(vector, vector2).normalized * magnitude;
		direction = direction.normalized;
		Vector3 vector3 = Vector3.Slerp(vector, vector2, angle / 90f);
		Plane plane = new Plane(-direction, position + vector);
		Ray ray = new Ray(position, vector3);
		float num;
		plane.Raycast(ray, out num);
		DebugExtension.DebugCircle(position + vector, direction, color, (vector - vector3.normalized * num).magnitude, duration, depthTest, 4f);
		DebugExtension.DebugCircle(position + vector * 0.5f, direction, color, (vector * 0.5f - vector3.normalized * (num * 0.5f)).magnitude, duration, depthTest, 4f);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002D77 File Offset: 0x00000F77
	public static void DebugCone(Vector3 position, Vector3 direction, float angle = 45f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCone(position, direction, Color.white, angle, duration, depthTest);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002D89 File Offset: 0x00000F89
	public static void DebugCone(Vector3 position, Color color, float angle = 45f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCone(position, Vector3.up, color, angle, duration, depthTest);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002D9B File Offset: 0x00000F9B
	public static void DebugCone(Vector3 position, float angle = 45f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCone(position, Vector3.up, Color.white, angle, duration, depthTest);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002DB0 File Offset: 0x00000FB0
	public static void DebugArrow(Vector3 position, Vector3 direction, Color color, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCone(position + direction, -direction * 0.333f, color, 15f, duration, depthTest);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002DD7 File Offset: 0x00000FD7
	public static void DebugArrow(Vector3 position, Vector3 direction, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugArrow(position, direction, Color.white, duration, depthTest);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002DE8 File Offset: 0x00000FE8
	public static void DebugCapsule(Vector3 start, Vector3 end, Color color, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		Vector3 vector = (end - start).normalized * radius;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3.Cross(vector, vector2).normalized * radius;
		float magnitude = (start - end).magnitude;
		float num = Mathf.Max(0f, magnitude * 0.5f - radius);
		Vector3 vector3 = (end + start) * 0.5f;
		start = vector3 + (start - vector3).normalized * num;
		end = vector3 + (end - vector3).normalized * num;
		DebugExtension.DebugCircle(start, vector, color, radius, duration, depthTest, 4f);
		DebugExtension.DebugCircle(end, -vector, color, radius, duration, depthTest, 4f);
		for (int i = 1; i < 26; i++)
		{
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002EE7 File Offset: 0x000010E7
	public static void DebugCapsule(Vector3 start, Vector3 end, float radius = 1f, float duration = 0f, bool depthTest = true)
	{
		DebugExtension.DebugCapsule(start, end, Color.white, radius, duration, depthTest);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002EFC File Offset: 0x000010FC
	public static void DrawPoint(Vector3 position, Color color, float scale = 1f)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale);
		Gizmos.DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale);
		Gizmos.DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale);
		Gizmos.color = color2;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002F9D File Offset: 0x0000119D
	public static void DrawPoint(Vector3 position, float scale = 1f)
	{
		DebugExtension.DrawPoint(position, Color.white, scale);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002FAC File Offset: 0x000011AC
	public static void DrawBounds(Bounds bounds, Color color)
	{
		Vector3 center = bounds.center;
		float x = bounds.extents.x;
		float y = bounds.extents.y;
		float z = bounds.extents.z;
		Vector3 vector = center + new Vector3(x, y, z);
		Vector3 vector2 = center + new Vector3(x, y, -z);
		Vector3 vector3 = center + new Vector3(-x, y, z);
		Vector3 vector4 = center + new Vector3(-x, y, -z);
		Vector3 vector5 = center + new Vector3(x, -y, z);
		Vector3 vector6 = center + new Vector3(x, -y, -z);
		Vector3 vector7 = center + new Vector3(-x, -y, z);
		Vector3 vector8 = center + new Vector3(-x, -y, -z);
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawLine(vector, vector3);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector3, vector4);
		Gizmos.DrawLine(vector2, vector4);
		Gizmos.DrawLine(vector, vector5);
		Gizmos.DrawLine(vector2, vector6);
		Gizmos.DrawLine(vector3, vector7);
		Gizmos.DrawLine(vector4, vector8);
		Gizmos.DrawLine(vector5, vector7);
		Gizmos.DrawLine(vector5, vector6);
		Gizmos.DrawLine(vector7, vector8);
		Gizmos.DrawLine(vector8, vector6);
		Gizmos.color = color2;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000030EA File Offset: 0x000012EA
	public static void DrawBounds(Bounds bounds)
	{
		DebugExtension.DrawBounds(bounds, Color.white);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000030F8 File Offset: 0x000012F8
	public static void DrawLocalCube(Transform transform, Vector3 size, Color color, Vector3 center = default(Vector3))
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Vector3 vector = transform.TransformPoint(center + -size * 0.5f);
		Vector3 vector2 = transform.TransformPoint(center + new Vector3(size.x, -size.y, -size.z) * 0.5f);
		Vector3 vector3 = transform.TransformPoint(center + new Vector3(size.x, -size.y, size.z) * 0.5f);
		Vector3 vector4 = transform.TransformPoint(center + new Vector3(-size.x, -size.y, size.z) * 0.5f);
		Vector3 vector5 = transform.TransformPoint(center + new Vector3(-size.x, size.y, -size.z) * 0.5f);
		Vector3 vector6 = transform.TransformPoint(center + new Vector3(size.x, size.y, -size.z) * 0.5f);
		Vector3 vector7 = transform.TransformPoint(center + size * 0.5f);
		Vector3 vector8 = transform.TransformPoint(center + new Vector3(-size.x, size.y, size.z) * 0.5f);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.DrawLine(vector3, vector4);
		Gizmos.DrawLine(vector4, vector);
		Gizmos.DrawLine(vector5, vector6);
		Gizmos.DrawLine(vector6, vector7);
		Gizmos.DrawLine(vector7, vector8);
		Gizmos.DrawLine(vector8, vector5);
		Gizmos.DrawLine(vector, vector5);
		Gizmos.DrawLine(vector2, vector6);
		Gizmos.DrawLine(vector3, vector7);
		Gizmos.DrawLine(vector4, vector8);
		Gizmos.color = color2;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000032CB File Offset: 0x000014CB
	public static void DrawLocalCube(Transform transform, Vector3 size, Vector3 center = default(Vector3))
	{
		DebugExtension.DrawLocalCube(transform, size, Color.white, center);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000032DC File Offset: 0x000014DC
	public static void DrawLocalCube(Matrix4x4 space, Vector3 size, Color color, Vector3 center = default(Vector3))
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Vector3 vector = space.MultiplyPoint3x4(center + -size * 0.5f);
		Vector3 vector2 = space.MultiplyPoint3x4(center + new Vector3(size.x, -size.y, -size.z) * 0.5f);
		Vector3 vector3 = space.MultiplyPoint3x4(center + new Vector3(size.x, -size.y, size.z) * 0.5f);
		Vector3 vector4 = space.MultiplyPoint3x4(center + new Vector3(-size.x, -size.y, size.z) * 0.5f);
		Vector3 vector5 = space.MultiplyPoint3x4(center + new Vector3(-size.x, size.y, -size.z) * 0.5f);
		Vector3 vector6 = space.MultiplyPoint3x4(center + new Vector3(size.x, size.y, -size.z) * 0.5f);
		Vector3 vector7 = space.MultiplyPoint3x4(center + size * 0.5f);
		Vector3 vector8 = space.MultiplyPoint3x4(center + new Vector3(-size.x, size.y, size.z) * 0.5f);
		Gizmos.DrawLine(vector, vector2);
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.DrawLine(vector3, vector4);
		Gizmos.DrawLine(vector4, vector);
		Gizmos.DrawLine(vector5, vector6);
		Gizmos.DrawLine(vector6, vector7);
		Gizmos.DrawLine(vector7, vector8);
		Gizmos.DrawLine(vector8, vector5);
		Gizmos.DrawLine(vector, vector5);
		Gizmos.DrawLine(vector2, vector6);
		Gizmos.DrawLine(vector3, vector7);
		Gizmos.DrawLine(vector4, vector8);
		Gizmos.color = color2;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000034B7 File Offset: 0x000016B7
	public static void DrawLocalCube(Matrix4x4 space, Vector3 size, Vector3 center = default(Vector3))
	{
		DebugExtension.DrawLocalCube(space, size, Color.white, center);
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000034C8 File Offset: 0x000016C8
	public static void DrawCircle(Vector3 position, Vector3 up, Color color, float radius = 1f)
	{
		up = ((up == Vector3.zero) ? Vector3.up : up).normalized * radius;
		Vector3 vector = Vector3.Slerp(up, -up, 0.5f);
		Vector3 vector2 = Vector3.Cross(up, vector).normalized * radius;
		Matrix4x4 matrix4x = default(Matrix4x4);
		matrix4x[0] = vector2.x;
		matrix4x[1] = vector2.y;
		matrix4x[2] = vector2.z;
		matrix4x[4] = up.x;
		matrix4x[5] = up.y;
		matrix4x[6] = up.z;
		matrix4x[8] = vector.x;
		matrix4x[9] = vector.y;
		matrix4x[10] = vector.z;
		Vector3 vector3 = position + matrix4x.MultiplyPoint3x4(new Vector3(Mathf.Cos(0f), 0f, Mathf.Sin(0f)));
		Vector3 vector4 = Vector3.zero;
		Color color2 = Gizmos.color;
		Gizmos.color = ((color == default(Color)) ? Color.white : color);
		for (int i = 0; i < 91; i++)
		{
			vector4.x = Mathf.Cos((float)(i * 4) * 0.017453292f);
			vector4.z = Mathf.Sin((float)(i * 4) * 0.017453292f);
			vector4.y = 0f;
			vector4 = position + matrix4x.MultiplyPoint3x4(vector4);
			Gizmos.DrawLine(vector3, vector4);
			vector3 = vector4;
		}
		Gizmos.color = color2;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003674 File Offset: 0x00001874
	public static void DrawCircleNoGizmo(Vector3 position, Vector3 up, Color color, float radius = 1f)
	{
		up = ((up == Vector3.zero) ? Vector3.up : up).normalized * radius;
		Vector3 vector = Vector3.Slerp(up, -up, 0.5f);
		Vector3 vector2 = Vector3.Cross(up, vector).normalized * radius;
		Matrix4x4 matrix4x = default(Matrix4x4);
		matrix4x[0] = vector2.x;
		matrix4x[1] = vector2.y;
		matrix4x[2] = vector2.z;
		matrix4x[4] = up.x;
		matrix4x[5] = up.y;
		matrix4x[6] = up.z;
		matrix4x[8] = vector.x;
		matrix4x[9] = vector.y;
		matrix4x[10] = vector.z;
		position + matrix4x.MultiplyPoint3x4(new Vector3(Mathf.Cos(0f), 0f, Mathf.Sin(0f)));
		Vector3 vector3 = Vector3.zero;
		for (int i = 0; i < 91; i++)
		{
			vector3.x = Mathf.Cos((float)(i * 4) * 0.017453292f);
			vector3.z = Mathf.Sin((float)(i * 4) * 0.017453292f);
			vector3.y = 0f;
			vector3 = position + matrix4x.MultiplyPoint3x4(vector3);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000037E4 File Offset: 0x000019E4
	public static void DrawCircle(Vector3 position, Color color, float radius = 1f)
	{
		DebugExtension.DrawCircle(position, Vector3.up, color, radius);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000037F3 File Offset: 0x000019F3
	public static void DrawCircleNoGizmo(Vector2 position, Color color, float radius = 1f)
	{
		DebugExtension.DrawCircleNoGizmo(position, Vector3.forward, color, radius);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003807 File Offset: 0x00001A07
	public static void DrawCircle(Vector3 position, Vector3 up, float radius = 1f)
	{
		DebugExtension.DrawCircle(position, position, Color.white, radius);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003816 File Offset: 0x00001A16
	public static void DrawCircle(Vector3 position, float radius = 1f)
	{
		DebugExtension.DrawCircle(position, Vector3.up, Color.white, radius);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000382C File Offset: 0x00001A2C
	public static void DrawCylinder(Vector3 start, Vector3 end, Color color, float radius = 1f)
	{
		Vector3 vector = (end - start).normalized * radius;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3 vector3 = Vector3.Cross(vector, vector2).normalized * radius;
		DebugExtension.DrawCircle(start, vector, color, radius);
		DebugExtension.DrawCircle(end, -vector, color, radius);
		DebugExtension.DrawCircle((start + end) * 0.5f, vector, color, radius);
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawLine(start + vector3, end + vector3);
		Gizmos.DrawLine(start - vector3, end - vector3);
		Gizmos.DrawLine(start + vector2, end + vector2);
		Gizmos.DrawLine(start - vector2, end - vector2);
		Gizmos.DrawLine(start - vector3, start + vector3);
		Gizmos.DrawLine(start - vector2, start + vector2);
		Gizmos.DrawLine(end - vector3, end + vector3);
		Gizmos.DrawLine(end - vector2, end + vector2);
		Gizmos.color = color2;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000394F File Offset: 0x00001B4F
	public static void DrawCylinder(Vector3 start, Vector3 end, float radius = 1f)
	{
		DebugExtension.DrawCylinder(start, end, Color.white, radius);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003960 File Offset: 0x00001B60
	public static void DrawCone(Vector3 position, Vector3 direction, Color color, float angle = 45f)
	{
		float magnitude = direction.magnitude;
		Vector3 vector = direction;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3 vector3 = Vector3.Cross(vector, vector2).normalized * magnitude;
		direction = direction.normalized;
		Vector3 vector4 = Vector3.Slerp(vector, vector2, angle / 90f);
		Plane plane = new Plane(-direction, position + vector);
		Ray ray = new Ray(position, vector4);
		float num;
		plane.Raycast(ray, out num);
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawRay(position, vector4.normalized * num);
		Gizmos.DrawRay(position, Vector3.Slerp(vector, -vector2, angle / 90f).normalized * num);
		Gizmos.DrawRay(position, Vector3.Slerp(vector, vector3, angle / 90f).normalized * num);
		Gizmos.DrawRay(position, Vector3.Slerp(vector, -vector3, angle / 90f).normalized * num);
		DebugExtension.DrawCircle(position + vector, direction, color, (vector - vector4.normalized * num).magnitude);
		DebugExtension.DrawCircle(position + vector * 0.5f, direction, color, (vector * 0.5f - vector4.normalized * (num * 0.5f)).magnitude);
		Gizmos.color = color2;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003AED File Offset: 0x00001CED
	public static void DrawCone(Vector3 position, Vector3 direction, float angle = 45f)
	{
		DebugExtension.DrawCone(position, direction, Color.white, angle);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003AFC File Offset: 0x00001CFC
	public static void DrawCone(Vector3 position, Color color, float angle = 45f)
	{
		DebugExtension.DrawCone(position, Vector3.up, color, angle);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003B0B File Offset: 0x00001D0B
	public static void DrawCone(Vector3 position, float angle = 45f)
	{
		DebugExtension.DrawCone(position, Vector3.up, Color.white, angle);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003B1E File Offset: 0x00001D1E
	public static void DrawArrow(Vector3 position, Vector3 direction, Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Gizmos.DrawRay(position, direction);
		DebugExtension.DrawCone(position + direction, -direction * 0.333f, color, 15f);
		Gizmos.color = color2;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003B59 File Offset: 0x00001D59
	public static void DrawArrow(Vector3 position, Vector3 direction)
	{
		DebugExtension.DrawArrow(position, direction, Color.white);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003B68 File Offset: 0x00001D68
	public static void DrawCapsule(Vector3 start, Vector3 end, Color color, float radius = 1f)
	{
		Vector3 vector = (end - start).normalized * radius;
		Vector3 vector2 = Vector3.Slerp(vector, -vector, 0.5f);
		Vector3 vector3 = Vector3.Cross(vector, vector2).normalized * radius;
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		float magnitude = (start - end).magnitude;
		float num = Mathf.Max(0f, magnitude * 0.5f - radius);
		Vector3 vector4 = (end + start) * 0.5f;
		start = vector4 + (start - vector4).normalized * num;
		end = vector4 + (end - vector4).normalized * num;
		DebugExtension.DrawCircle(start, vector, color, radius);
		DebugExtension.DrawCircle(end, -vector, color, radius);
		Gizmos.DrawLine(start + vector3, end + vector3);
		Gizmos.DrawLine(start - vector3, end - vector3);
		Gizmos.DrawLine(start + vector2, end + vector2);
		Gizmos.DrawLine(start - vector2, end - vector2);
		for (int i = 1; i < 26; i++)
		{
			Gizmos.DrawLine(Vector3.Slerp(vector3, -vector, (float)i / 25f) + start, Vector3.Slerp(vector3, -vector, (float)(i - 1) / 25f) + start);
			Gizmos.DrawLine(Vector3.Slerp(-vector3, -vector, (float)i / 25f) + start, Vector3.Slerp(-vector3, -vector, (float)(i - 1) / 25f) + start);
			Gizmos.DrawLine(Vector3.Slerp(vector2, -vector, (float)i / 25f) + start, Vector3.Slerp(vector2, -vector, (float)(i - 1) / 25f) + start);
			Gizmos.DrawLine(Vector3.Slerp(-vector2, -vector, (float)i / 25f) + start, Vector3.Slerp(-vector2, -vector, (float)(i - 1) / 25f) + start);
			Gizmos.DrawLine(Vector3.Slerp(vector3, vector, (float)i / 25f) + end, Vector3.Slerp(vector3, vector, (float)(i - 1) / 25f) + end);
			Gizmos.DrawLine(Vector3.Slerp(-vector3, vector, (float)i / 25f) + end, Vector3.Slerp(-vector3, vector, (float)(i - 1) / 25f) + end);
			Gizmos.DrawLine(Vector3.Slerp(vector2, vector, (float)i / 25f) + end, Vector3.Slerp(vector2, vector, (float)(i - 1) / 25f) + end);
			Gizmos.DrawLine(Vector3.Slerp(-vector2, vector, (float)i / 25f) + end, Vector3.Slerp(-vector2, vector, (float)(i - 1) / 25f) + end);
		}
		Gizmos.color = color2;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003EA6 File Offset: 0x000020A6
	public static void DrawCapsule(Vector3 start, Vector3 end, float radius = 1f)
	{
		DebugExtension.DrawCapsule(start, end, Color.white, radius);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003EB8 File Offset: 0x000020B8
	public static string MethodsOfObject(object obj, bool includeInfo = false)
	{
		string text = "";
		MethodInfo[] methods = obj.GetType().GetMethods();
		for (int i = 0; i < methods.Length; i++)
		{
			if (includeInfo)
			{
				string text2 = text;
				MethodInfo methodInfo = methods[i];
				text = text2 + ((methodInfo != null) ? methodInfo.ToString() : null) + "\n";
			}
			else
			{
				text = text + methods[i].Name + "\n";
			}
		}
		return text;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003F1C File Offset: 0x0000211C
	public static string MethodsOfType(Type type, bool includeInfo = false)
	{
		string text = "";
		MethodInfo[] methods = type.GetMethods();
		for (int i = 0; i < methods.Length; i++)
		{
			if (includeInfo)
			{
				string text2 = text;
				MethodInfo methodInfo = methods[i];
				text = text2 + ((methodInfo != null) ? methodInfo.ToString() : null) + "\n";
			}
			else
			{
				text = text + methods[i].Name + "\n";
			}
		}
		return text;
	}
}
