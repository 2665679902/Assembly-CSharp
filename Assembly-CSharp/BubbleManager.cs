using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200055E RID: 1374
[AddComponentMenu("KMonoBehaviour/scripts/BubbleManager")]
public class BubbleManager : KMonoBehaviour, ISim33ms, IRenderEveryTick
{
	// Token: 0x06002113 RID: 8467 RVA: 0x000B42D5 File Offset: 0x000B24D5
	public static void DestroyInstance()
	{
		BubbleManager.instance = null;
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x000B42DD File Offset: 0x000B24DD
	protected override void OnPrefabInit()
	{
		BubbleManager.instance = this;
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x000B42E8 File Offset: 0x000B24E8
	public void SpawnBubble(Vector2 position, Vector2 velocity, SimHashes element, float mass, float temperature)
	{
		BubbleManager.Bubble bubble = new BubbleManager.Bubble
		{
			position = position,
			velocity = velocity,
			element = element,
			temperature = temperature,
			mass = mass
		};
		this.bubbles.Add(bubble);
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x000B4338 File Offset: 0x000B2538
	public void Sim33ms(float dt)
	{
		ListPool<BubbleManager.Bubble, BubbleManager>.PooledList pooledList = ListPool<BubbleManager.Bubble, BubbleManager>.Allocate();
		ListPool<BubbleManager.Bubble, BubbleManager>.PooledList pooledList2 = ListPool<BubbleManager.Bubble, BubbleManager>.Allocate();
		foreach (BubbleManager.Bubble bubble in this.bubbles)
		{
			bubble.position += bubble.velocity * dt;
			bubble.elapsedTime += dt;
			int num = Grid.PosToCell(bubble.position);
			if (!Grid.IsVisiblyInLiquid(bubble.position) || Grid.Element[num].id == bubble.element)
			{
				pooledList2.Add(bubble);
			}
			else
			{
				pooledList.Add(bubble);
			}
		}
		foreach (BubbleManager.Bubble bubble2 in pooledList2)
		{
			SimMessages.AddRemoveSubstance(Grid.PosToCell(bubble2.position), bubble2.element, CellEventLogger.Instance.FallingWaterAddToSim, bubble2.mass, bubble2.temperature, byte.MaxValue, 0, true, -1);
		}
		this.bubbles.Clear();
		this.bubbles.AddRange(pooledList);
		pooledList2.Recycle();
		pooledList.Recycle();
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x000B4490 File Offset: 0x000B2690
	public void RenderEveryTick(float dt)
	{
		ListPool<SpriteSheetAnimator.AnimInfo, BubbleManager>.PooledList pooledList = ListPool<SpriteSheetAnimator.AnimInfo, BubbleManager>.Allocate();
		SpriteSheetAnimator spriteSheetAnimator = SpriteSheetAnimManager.instance.GetSpriteSheetAnimator("liquid_splash1");
		foreach (BubbleManager.Bubble bubble in this.bubbles)
		{
			SpriteSheetAnimator.AnimInfo animInfo = new SpriteSheetAnimator.AnimInfo
			{
				frame = spriteSheetAnimator.GetFrameFromElapsedTimeLooping(bubble.elapsedTime),
				elapsedTime = bubble.elapsedTime,
				pos = new Vector3(bubble.position.x, bubble.position.y, 0f),
				rotation = Quaternion.identity,
				size = Vector2.one,
				colour = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)
			};
			pooledList.Add(animInfo);
		}
		pooledList.Recycle();
	}

	// Token: 0x04001307 RID: 4871
	public static BubbleManager instance;

	// Token: 0x04001308 RID: 4872
	private List<BubbleManager.Bubble> bubbles = new List<BubbleManager.Bubble>();

	// Token: 0x02001189 RID: 4489
	private struct Bubble
	{
		// Token: 0x04005B1E RID: 23326
		public Vector2 position;

		// Token: 0x04005B1F RID: 23327
		public Vector2 velocity;

		// Token: 0x04005B20 RID: 23328
		public float elapsedTime;

		// Token: 0x04005B21 RID: 23329
		public int frame;

		// Token: 0x04005B22 RID: 23330
		public SimHashes element;

		// Token: 0x04005B23 RID: 23331
		public float temperature;

		// Token: 0x04005B24 RID: 23332
		public float mass;
	}
}
