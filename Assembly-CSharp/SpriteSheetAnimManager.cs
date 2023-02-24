using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D1 RID: 1233
[AddComponentMenu("KMonoBehaviour/scripts/SpriteSheetAnimManager")]
public class SpriteSheetAnimManager : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06001C93 RID: 7315 RVA: 0x0009878C File Offset: 0x0009698C
	public static void DestroyInstance()
	{
		SpriteSheetAnimManager.instance = null;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x00098794 File Offset: 0x00096994
	protected override void OnPrefabInit()
	{
		SpriteSheetAnimManager.instance = this;
	}

	// Token: 0x06001C95 RID: 7317 RVA: 0x0009879C File Offset: 0x0009699C
	protected override void OnSpawn()
	{
		for (int i = 0; i < this.sheets.Length; i++)
		{
			int num = Hash.SDBMLower(this.sheets[i].name);
			this.nameIndexMap[num] = new SpriteSheetAnimator(this.sheets[i]);
		}
	}

	// Token: 0x06001C96 RID: 7318 RVA: 0x000987F0 File Offset: 0x000969F0
	public void Play(string name, Vector3 pos, Vector2 size, Color32 colour)
	{
		int num = Hash.SDBMLower(name);
		this.Play(num, pos, Quaternion.identity, size, colour);
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x00098814 File Offset: 0x00096A14
	public void Play(string name, Vector3 pos, Quaternion rotation, Vector2 size, Color32 colour)
	{
		int num = Hash.SDBMLower(name);
		this.Play(num, pos, rotation, size, colour);
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x00098835 File Offset: 0x00096A35
	public void Play(int name_hash, Vector3 pos, Quaternion rotation, Vector2 size, Color32 colour)
	{
		this.nameIndexMap[name_hash].Play(pos, rotation, size, colour);
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x00098853 File Offset: 0x00096A53
	public void RenderEveryTick(float dt)
	{
		this.UpdateAnims(dt);
		this.Render();
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x00098864 File Offset: 0x00096A64
	public void UpdateAnims(float dt)
	{
		foreach (KeyValuePair<int, SpriteSheetAnimator> keyValuePair in this.nameIndexMap)
		{
			keyValuePair.Value.UpdateAnims(dt);
		}
	}

	// Token: 0x06001C9B RID: 7323 RVA: 0x000988C0 File Offset: 0x00096AC0
	public void Render()
	{
		Vector3 zero = Vector3.zero;
		foreach (KeyValuePair<int, SpriteSheetAnimator> keyValuePair in this.nameIndexMap)
		{
			keyValuePair.Value.Render();
		}
	}

	// Token: 0x06001C9C RID: 7324 RVA: 0x00098920 File Offset: 0x00096B20
	public SpriteSheetAnimator GetSpriteSheetAnimator(HashedString name)
	{
		return this.nameIndexMap[name.HashValue];
	}

	// Token: 0x0400101A RID: 4122
	public const float SECONDS_PER_FRAME = 0.033333335f;

	// Token: 0x0400101B RID: 4123
	[SerializeField]
	private SpriteSheet[] sheets;

	// Token: 0x0400101C RID: 4124
	private Dictionary<int, SpriteSheetAnimator> nameIndexMap = new Dictionary<int, SpriteSheetAnimator>();

	// Token: 0x0400101D RID: 4125
	public static SpriteSheetAnimManager instance;
}
