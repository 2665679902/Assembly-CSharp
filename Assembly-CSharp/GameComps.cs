using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020006A4 RID: 1700
public class GameComps : KComponents
{
	// Token: 0x06002E20 RID: 11808 RVA: 0x000F372C File Offset: 0x000F192C
	public GameComps()
	{
		foreach (FieldInfo fieldInfo in typeof(GameComps).GetFields())
		{
			object obj = Activator.CreateInstance(fieldInfo.FieldType);
			fieldInfo.SetValue(null, obj);
			base.Add<IComponentManager>(obj as IComponentManager);
			if (obj is IKComponentManager)
			{
				IKComponentManager ikcomponentManager = obj as IKComponentManager;
				GameComps.AddKComponentManager(fieldInfo.FieldType, ikcomponentManager);
			}
		}
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x000F37A0 File Offset: 0x000F19A0
	public new void Clear()
	{
		FieldInfo[] fields = typeof(GameComps).GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			IComponentManager componentManager = fields[i].GetValue(null) as IComponentManager;
			if (componentManager != null)
			{
				componentManager.Clear();
			}
		}
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x000F37E3 File Offset: 0x000F19E3
	public static void AddKComponentManager(Type kcomponent, IKComponentManager inst)
	{
		GameComps.kcomponentManagers[kcomponent] = inst;
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x000F37F1 File Offset: 0x000F19F1
	public static IKComponentManager GetKComponentManager(Type kcomponent_type)
	{
		return GameComps.kcomponentManagers[kcomponent_type];
	}

	// Token: 0x04001BC6 RID: 7110
	public static GravityComponents Gravities;

	// Token: 0x04001BC7 RID: 7111
	public static FallerComponents Fallers;

	// Token: 0x04001BC8 RID: 7112
	public static InfraredVisualizerComponents InfraredVisualizers;

	// Token: 0x04001BC9 RID: 7113
	public static ElementSplitterComponents ElementSplitters;

	// Token: 0x04001BCA RID: 7114
	public static OreSizeVisualizerComponents OreSizeVisualizers;

	// Token: 0x04001BCB RID: 7115
	public static StructureTemperatureComponents StructureTemperatures;

	// Token: 0x04001BCC RID: 7116
	public static DiseaseContainers DiseaseContainers;

	// Token: 0x04001BCD RID: 7117
	public static RequiresFoundation RequiresFoundations;

	// Token: 0x04001BCE RID: 7118
	public static WhiteBoard WhiteBoards;

	// Token: 0x04001BCF RID: 7119
	private static Dictionary<Type, IKComponentManager> kcomponentManagers = new Dictionary<Type, IKComponentManager>();
}
