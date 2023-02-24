using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000084 RID: 132
public static class CPUBudget
{
	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x00019128 File Offset: 0x00017328
	public static int coreCount
	{
		get
		{
			int overrideCoreCount = TuningData<CPUBudget.Tuning>.Get().overrideCoreCount;
			if (0 >= overrideCoreCount || overrideCoreCount >= SystemInfo.processorCount)
			{
				return SystemInfo.processorCount;
			}
			return overrideCoreCount;
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00019153 File Offset: 0x00017353
	public static float ComputeDuration(long start)
	{
		return (float)((CPUBudget.stopwatch.ElapsedTicks - start) * 1000000L / Stopwatch.Frequency) / 1000f;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00019178 File Offset: 0x00017378
	public static void AddRoot(ICPULoad root)
	{
		CPUBudget.nodes.Add(root, new CPUBudget.Node
		{
			load = root,
			children = new List<CPUBudget.Node>(),
			frameTime = root.GetEstimatedFrameTime(),
			loadBalanceThreshold = TuningData<CPUBudget.Tuning>.Get().defaultLoadBalanceThreshold
		});
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x000191CC File Offset: 0x000173CC
	public static void AddChild(ICPULoad parent, ICPULoad child, float loadBalanceThreshold)
	{
		CPUBudget.Node node = new CPUBudget.Node
		{
			load = child,
			children = new List<CPUBudget.Node>(),
			frameTime = child.GetEstimatedFrameTime(),
			loadBalanceThreshold = loadBalanceThreshold
		};
		CPUBudget.nodes.Add(child, node);
		CPUBudget.nodes[parent].children.Add(node);
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001922E File Offset: 0x0001742E
	public static void AddChild(ICPULoad parent, ICPULoad child)
	{
		CPUBudget.AddChild(parent, child, TuningData<CPUBudget.Tuning>.Get().defaultLoadBalanceThreshold);
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00019244 File Offset: 0x00017444
	public static void FinalizeChildren(ICPULoad parent)
	{
		CPUBudget.Node node = CPUBudget.nodes[parent];
		List<CPUBudget.Node> children = CPUBudget.nodes[parent].children;
		float num = 0f;
		foreach (CPUBudget.Node node2 in children)
		{
			CPUBudget.FinalizeChildren(node2.load);
			num += node2.frameTime;
		}
		for (int num2 = 0; num2 != children.Count; num2++)
		{
			CPUBudget.Node node3 = children[num2];
			node3.frameTime = node.frameTime * (node3.frameTime / num);
			children[num2] = node3;
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00019308 File Offset: 0x00017508
	public static void Start(ICPULoad cpuLoad)
	{
		CPUBudget.Node node = CPUBudget.nodes[cpuLoad];
		node.start = CPUBudget.stopwatch.ElapsedTicks;
		CPUBudget.nodes[cpuLoad] = node;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00019340 File Offset: 0x00017540
	public static void End(ICPULoad cpuLoad)
	{
		CPUBudget.Node node = CPUBudget.nodes[cpuLoad];
		float num = node.frameTime - CPUBudget.ComputeDuration(node.start);
		if (node.loadBalanceThreshold < Math.Abs(num))
		{
			CPUBudget.Balance(cpuLoad, num);
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00019384 File Offset: 0x00017584
	public static void Balance(ICPULoad cpuLoad, float frameTimeDelta)
	{
		CPUBudget.Node node = CPUBudget.nodes[cpuLoad];
		List<CPUBudget.Node> children = node.children;
		if (children.Count == 0)
		{
			if (node.load.AdjustLoad(node.frameTime, frameTimeDelta))
			{
				node.frameTime += frameTimeDelta;
				return;
			}
		}
		else
		{
			for (int num = 0; num != children.Count; num++)
			{
				CPUBudget.Node node2 = children[num];
				float num2 = node2.frameTime / node.frameTime;
				float num3 = frameTimeDelta * num2;
				CPUBudget.Balance(node2.load, num3);
				children[num] = node2;
			}
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00019410 File Offset: 0x00017610
	public static void Remove(ICPULoad cpuLoad)
	{
		foreach (CPUBudget.Node node in CPUBudget.nodes[cpuLoad].children)
		{
			CPUBudget.Remove(node.load);
		}
		CPUBudget.nodes.Remove(cpuLoad);
	}

	// Token: 0x04000528 RID: 1320
	public static Stopwatch stopwatch = Stopwatch.StartNew();

	// Token: 0x04000529 RID: 1321
	private static Dictionary<ICPULoad, CPUBudget.Node> nodes = new Dictionary<ICPULoad, CPUBudget.Node>();

	// Token: 0x020009CD RID: 2509
	private class Tuning : TuningData<CPUBudget.Tuning>
	{
		// Token: 0x040021FA RID: 8698
		public int overrideCoreCount = -1;

		// Token: 0x040021FB RID: 8699
		public float defaultLoadBalanceThreshold = 0.1f;
	}

	// Token: 0x020009CE RID: 2510
	private struct Node
	{
		// Token: 0x040021FC RID: 8700
		public ICPULoad load;

		// Token: 0x040021FD RID: 8701
		public List<CPUBudget.Node> children;

		// Token: 0x040021FE RID: 8702
		public long start;

		// Token: 0x040021FF RID: 8703
		public float frameTime;

		// Token: 0x04002200 RID: 8704
		public float loadBalanceThreshold;
	}
}
