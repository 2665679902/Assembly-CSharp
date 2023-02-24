using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000472 RID: 1138
	public static class ConnectionTypes
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000615C4 File Offset: 0x0005F7C4
		private static Type NullType
		{
			get
			{
				return typeof(ConnectionTypes);
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000615D0 File Offset: 0x0005F7D0
		public static Type GetType(string typeName)
		{
			return ConnectionTypes.GetTypeData(typeName).Type ?? ConnectionTypes.NullType;
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000615E8 File Offset: 0x0005F7E8
		public static TypeData GetTypeData(string typeName)
		{
			if (ConnectionTypes.types == null || ConnectionTypes.types.Count == 0)
			{
				ConnectionTypes.FetchTypes();
			}
			TypeData typeData;
			if (!ConnectionTypes.types.TryGetValue(typeName, out typeData))
			{
				Type type = Type.GetType(typeName);
				if (type == null)
				{
					typeData = ConnectionTypes.types.First<KeyValuePair<string, TypeData>>().Value;
					global::Debug.LogError("No TypeData defined for: " + typeName + " and type could not be found either");
				}
				else
				{
					typeData = ((ConnectionTypes.types.Values.Count <= 0) ? null : ConnectionTypes.types.Values.First((TypeData data) => data.isValid() && data.Type == type));
					if (typeData == null)
					{
						ConnectionTypes.types.Add(typeName, typeData = new TypeData(type));
					}
				}
			}
			return typeData;
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000616B8 File Offset: 0x0005F8B8
		public static TypeData GetTypeData(Type type)
		{
			if (ConnectionTypes.types == null || ConnectionTypes.types.Count == 0)
			{
				ConnectionTypes.FetchTypes();
			}
			TypeData typeData = ((ConnectionTypes.types.Values.Count <= 0) ? null : ConnectionTypes.types.Values.First((TypeData data) => data.isValid() && data.Type == type));
			if (typeData == null)
			{
				ConnectionTypes.types.Add(type.Name, typeData = new TypeData(type));
			}
			return typeData;
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x00061744 File Offset: 0x0005F944
		internal static void FetchTypes()
		{
			ConnectionTypes.types = new Dictionary<string, TypeData> { 
			{
				"None",
				new TypeData(typeof(object))
			} };
			foreach (Assembly assembly2 in from assembly in AppDomain.CurrentDomain.GetAssemblies()
				where assembly.FullName.Contains("Assembly")
				select assembly)
			{
				foreach (Type type in from T in assembly2.GetTypes()
					where T.IsClass && !T.IsAbstract && T.GetInterfaces().Contains(typeof(IConnectionTypeDeclaration))
					select T)
				{
					IConnectionTypeDeclaration connectionTypeDeclaration = assembly2.CreateInstance(type.FullName) as IConnectionTypeDeclaration;
					if (connectionTypeDeclaration == null)
					{
						throw new UnityException("Error with Type Declaration " + type.FullName);
					}
					ConnectionTypes.types.Add(connectionTypeDeclaration.Identifier, new TypeData(connectionTypeDeclaration));
				}
			}
		}

		// Token: 0x040010ED RID: 4333
		private static Dictionary<string, TypeData> types;
	}
}
