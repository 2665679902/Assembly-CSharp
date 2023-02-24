using System;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public struct SoundCuller
{
	// Token: 0x0600177E RID: 6014 RVA: 0x0007AB14 File Offset: 0x00078D14
	public static bool IsAudibleWorld(Vector2 pos)
	{
		bool flag = false;
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x0007AB48 File Offset: 0x00078D48
	public bool IsAudible(Vector2 pos)
	{
		return SoundCuller.IsAudibleWorld(pos) && this.min.LessEqual(pos) && pos.LessEqual(this.max);
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x0007AB70 File Offset: 0x00078D70
	public bool IsAudibleNoCameraScaling(Vector2 pos, float falloff_distance_sq)
	{
		return (pos.x - this.cameraPos.x) * (pos.x - this.cameraPos.x) + (pos.y - this.cameraPos.y) * (pos.y - this.cameraPos.y) < falloff_distance_sq;
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x0007ABCB File Offset: 0x00078DCB
	public bool IsAudible(Vector2 pos, float falloff_distance_sq)
	{
		if (!SoundCuller.IsAudibleWorld(pos))
		{
			return false;
		}
		pos = this.GetVerticallyScaledPosition(pos, false);
		return this.IsAudibleNoCameraScaling(pos, falloff_distance_sq);
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x0007ABF3 File Offset: 0x00078DF3
	public bool IsAudible(Vector2 pos, HashedString sound_path)
	{
		return sound_path.IsValid && this.IsAudible(pos, KFMOD.GetSoundEventDescription(sound_path).falloffDistanceSq);
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x0007AC14 File Offset: 0x00078E14
	public Vector3 GetVerticallyScaledPosition(Vector3 pos, bool objectIsSelectedAndVisible = false)
	{
		float num = 1f;
		float num2;
		if (pos.y > this.max.y)
		{
			num2 = Mathf.Abs(pos.y - this.max.y);
		}
		else if (pos.y < this.min.y)
		{
			num2 = Mathf.Abs(pos.y - this.min.y);
			num = -1f;
		}
		else
		{
			num2 = 0f;
		}
		float extraYRange = TuningData<SoundCuller.Tuning>.Get().extraYRange;
		num2 = ((num2 < extraYRange) ? num2 : extraYRange);
		float num3 = num2 * num2 / (4f * this.zoomScaler);
		num3 *= num;
		Vector3 vector = new Vector3(pos.x, pos.y + num3, 0f);
		if (objectIsSelectedAndVisible)
		{
			vector.z = pos.z;
		}
		return vector;
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x0007ACE8 File Offset: 0x00078EE8
	public static SoundCuller CreateCuller()
	{
		SoundCuller soundCuller = default(SoundCuller);
		Camera main = Camera.main;
		Vector3 vector = main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		soundCuller.min = new Vector3(vector2.x, vector2.y, 0f);
		soundCuller.max = new Vector3(vector.x, vector.y, 0f);
		soundCuller.cameraPos = main.transform.GetPosition();
		Audio audio = Audio.Get();
		float num = CameraController.Instance.OrthographicSize / (audio.listenerReferenceZ - audio.listenerMinZ);
		if (num <= 0f)
		{
			num = 2f;
		}
		else
		{
			num = 1f;
		}
		soundCuller.zoomScaler = num;
		return soundCuller;
	}

	// Token: 0x04000D0A RID: 3338
	private Vector2 min;

	// Token: 0x04000D0B RID: 3339
	private Vector2 max;

	// Token: 0x04000D0C RID: 3340
	private Vector2 cameraPos;

	// Token: 0x04000D0D RID: 3341
	private float zoomScaler;

	// Token: 0x0200105C RID: 4188
	public class Tuning : TuningData<SoundCuller.Tuning>
	{
		// Token: 0x04005757 RID: 22359
		public float extraYRange;
	}
}
