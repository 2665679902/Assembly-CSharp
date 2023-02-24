using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class GeneratedBuildings
{
	// Token: 0x0600082E RID: 2094 RVA: 0x0002FB00 File Offset: 0x0002DD00
	public static void LoadGeneratedBuildings(List<Type> types)
	{
		Type typeFromHandle = typeof(IBuildingConfig);
		List<Type> list = new List<Type>();
		foreach (Type type in types)
		{
			if (typeFromHandle.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
			{
				list.Add(type);
			}
		}
		foreach (Type type2 in list)
		{
			object obj = Activator.CreateInstance(type2);
			try
			{
				BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
			}
			catch (Exception ex)
			{
				DebugUtil.LogException(null, "Exception in RegisterBuilding for type " + type2.FullName + " from " + type2.Assembly.GetName().Name, ex);
			}
		}
		foreach (PlanScreen.PlanInfo planInfo in BUILDINGS.PLANORDER)
		{
			List<string> list2 = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				if (Assets.GetBuildingDef(keyValuePair.Key) == null)
				{
					list2.Add(keyValuePair.Key);
				}
			}
			using (List<string>.Enumerator enumerator4 = list2.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					string entry2 = enumerator4.Current;
					planInfo.buildingAndSubcategoryData.RemoveAll((KeyValuePair<string, string> match) => match.Key == entry2);
				}
			}
			List<string> list3 = new List<string>();
			using (List<string>.Enumerator enumerator4 = planInfo.data.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					string entry = enumerator4.Current;
					if (planInfo.buildingAndSubcategoryData.FindIndex((KeyValuePair<string, string> x) => x.Key == entry) == -1 && Assets.GetBuildingDef(entry) != null)
					{
						global::Debug.LogWarning("Mod: Building '" + entry + "' was not added properly to PlanInfo, use ModUtil.AddBuildingToPlanScreen instead.");
						list3.Add(entry);
					}
				}
			}
			foreach (string text in list3)
			{
				ModUtil.AddBuildingToPlanScreen(planInfo.category, text, "uncategorized");
			}
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0002FE68 File Offset: 0x0002E068
	public static void MakeBuildingAlwaysOperational(GameObject go)
	{
		BuildingDef def = go.GetComponent<BuildingComplete>().Def;
		if (def.LogicInputPorts != null || def.LogicOutputPorts != null)
		{
			global::Debug.LogWarning("Do not call MakeBuildingAlwaysOperational directly if LogicInputPorts or LogicOutputPorts are defined. Instead set BuildingDef.AlwaysOperational = true");
		}
		GeneratedBuildings.MakeBuildingAlwaysOperationalImpl(go);
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0002FEA1 File Offset: 0x0002E0A1
	public static void RemoveLoopingSounds(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<LoopingSounds>());
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0002FEAE File Offset: 0x0002E0AE
	public static void RemoveDefaultLogicPorts(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<LogicPorts>());
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0002FEBB File Offset: 0x0002E0BB
	public static void RegisterWithOverlay(HashSet<Tag> overlay_tags, string id)
	{
		overlay_tags.Add(new Tag(id));
		overlay_tags.Add(new Tag(id + "UnderConstruction"));
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0002FEE1 File Offset: 0x0002E0E1
	public static void RegisterSingleLogicInputPort(GameObject go)
	{
		LogicPorts logicPorts = go.AddOrGet<LogicPorts>();
		logicPorts.inputPortInfo = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0)).ToArray();
		logicPorts.outputPortInfo = null;
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0002FF06 File Offset: 0x0002E106
	private static void MakeBuildingAlwaysOperationalImpl(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<Operational>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<LogicPorts>());
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0002FF2C File Offset: 0x0002E12C
	public static void InitializeLogicPorts(GameObject go, BuildingDef def)
	{
		if (def.AlwaysOperational)
		{
			GeneratedBuildings.MakeBuildingAlwaysOperationalImpl(go);
		}
		if (def.LogicInputPorts != null)
		{
			go.AddOrGet<LogicPorts>().inputPortInfo = def.LogicInputPorts.ToArray();
		}
		if (def.LogicOutputPorts != null)
		{
			go.AddOrGet<LogicPorts>().outputPortInfo = def.LogicOutputPorts.ToArray();
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002FF84 File Offset: 0x0002E184
	public static void InitializeHighEnergyParticlePorts(GameObject go, BuildingDef def)
	{
		if (def.UseHighEnergyParticleInputPort || def.UseHighEnergyParticleOutputPort)
		{
			HighEnergyParticlePort highEnergyParticlePort = go.AddOrGet<HighEnergyParticlePort>();
			highEnergyParticlePort.particleInputOffset = def.HighEnergyParticleInputOffset;
			highEnergyParticlePort.particleOutputOffset = def.HighEnergyParticleOutputOffset;
			highEnergyParticlePort.particleInputEnabled = def.UseHighEnergyParticleInputPort;
			highEnergyParticlePort.particleOutputEnabled = def.UseHighEnergyParticleOutputPort;
		}
	}
}
