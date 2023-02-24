using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using KSerialization;
using UnityEngine;

// Token: 0x0200011B RID: 283
public static class Util
{
	// Token: 0x0600095C RID: 2396 RVA: 0x000252C8 File Offset: 0x000234C8
	public static void Swap<T>(ref T a, ref T b)
	{
		T t = a;
		a = b;
		b = t;
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000252F0 File Offset: 0x000234F0
	public static void Swap(IList list_or_array, in int index_a, in int index_b)
	{
		object obj = list_or_array[index_a];
		list_or_array[index_a] = list_or_array[index_b];
		list_or_array[index_b] = obj;
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x0002531F File Offset: 0x0002351F
	public static float Remap(this float value, [TupleElementNames(new string[] { "min", "max" })] ValueTuple<float, float> fromRange, [TupleElementNames(new string[] { "min", "max" })] ValueTuple<float, float> toRange)
	{
		return (value - fromRange.Item1) / (fromRange.Item2 - fromRange.Item1) * (toRange.Item2 - toRange.Item1) + toRange.Item1;
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0002534C File Offset: 0x0002354C
	public static void InitializeComponent(Component cmp)
	{
		if (cmp != null)
		{
			KMonoBehaviour kmonoBehaviour = cmp as KMonoBehaviour;
			if (kmonoBehaviour != null)
			{
				kmonoBehaviour.InitializeComponent();
			}
		}
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00025378 File Offset: 0x00023578
	public static void SpawnComponent(Component cmp)
	{
		if (cmp != null)
		{
			KMonoBehaviour kmonoBehaviour = cmp as KMonoBehaviour;
			if (kmonoBehaviour != null)
			{
				kmonoBehaviour.Spawn();
			}
		}
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x000253A4 File Offset: 0x000235A4
	public static Component FindComponent(this Component cmp, string targetName)
	{
		return cmp.gameObject.FindComponent(targetName);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x000253B2 File Offset: 0x000235B2
	public static Component FindComponent(this GameObject go, string targetName)
	{
		Component component = go.GetComponent(targetName);
		Util.InitializeComponent(component);
		return component;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x000253C1 File Offset: 0x000235C1
	public static T FindComponent<T>(this Component c) where T : Component
	{
		return c.gameObject.FindComponent<T>();
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x000253CE File Offset: 0x000235CE
	public static T FindComponent<T>(this GameObject go) where T : Component
	{
		T component = go.GetComponent<T>();
		Util.InitializeComponent(component);
		return component;
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x000253E1 File Offset: 0x000235E1
	public static T FindOrAddUnityComponent<T>(this Component cmp) where T : Component
	{
		return cmp.gameObject.FindOrAddUnityComponent<T>();
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x000253F0 File Offset: 0x000235F0
	public static T FindOrAddUnityComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0002541A File Offset: 0x0002361A
	public static Component RequireComponent(this Component cmp, string name)
	{
		return cmp.gameObject.RequireComponent(name);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00025428 File Offset: 0x00023628
	public static Component RequireComponent(this GameObject go, string name)
	{
		Component component = go.GetComponent(name);
		if (component == null)
		{
			global::Debug.LogErrorFormat(go, "{0} '{1}' requires a component of type {2}!", new object[]
			{
				go.GetType().ToString(),
				go.name,
				name
			});
			return null;
		}
		Util.InitializeComponent(component);
		return component;
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0002547C File Offset: 0x0002367C
	public static T RequireComponent<T>(this Component cmp) where T : Component
	{
		T component = cmp.gameObject.GetComponent<T>();
		if (component == null)
		{
			global::Debug.LogErrorFormat(cmp.gameObject, "{0} '{1}' requires a component of type {2} as requested by {3}!", new object[]
			{
				cmp.gameObject.GetType().ToString(),
				cmp.gameObject.name,
				typeof(T).ToString(),
				cmp.GetType().ToString()
			});
			return default(T);
		}
		Util.InitializeComponent(component);
		return component;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00025510 File Offset: 0x00023710
	public static T RequireComponent<T>(this GameObject gameObject) where T : Component
	{
		T component = gameObject.GetComponent<T>();
		if (component == null)
		{
			global::Debug.LogErrorFormat(gameObject, "{0} '{1}' requires a component of type {2}!", new object[]
			{
				gameObject.GetType().ToString(),
				gameObject.name,
				typeof(T).ToString()
			});
			return default(T);
		}
		Util.InitializeComponent(component);
		return component;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00025582 File Offset: 0x00023782
	public static void SetLayerRecursively(this GameObject go, int layer)
	{
		Util.SetLayer(go.transform, layer);
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x00025590 File Offset: 0x00023790
	public static void SetLayer(Transform t, int layer)
	{
		t.gameObject.layer = layer;
		for (int i = 0; i < t.childCount; i++)
		{
			Util.SetLayer(t.GetChild(i), layer);
		}
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x000255C7 File Offset: 0x000237C7
	public static void KDestroyGameObject(Component original)
	{
		global::Debug.Assert(original != null, "Attempted to destroy a GameObject that is already destroyed.");
		Util.KDestroyGameObject(original.gameObject);
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x000255E5 File Offset: 0x000237E5
	public static void KDestroyGameObject(GameObject original)
	{
		global::Debug.Assert(original != null, "Attempted to destroy a GameObject that is already destroyed.");
		original.DeleteObject();
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x000255FE File Offset: 0x000237FE
	public static T FindOrAddComponent<T>(this Component cmp) where T : Component
	{
		return cmp.gameObject.FindOrAddComponent<T>();
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0002560C File Offset: 0x0002380C
	public static T FindOrAddComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
			KMonoBehaviour kmonoBehaviour = t as KMonoBehaviour;
			if (kmonoBehaviour != null && !KMonoBehaviour.isPoolPreInit && !kmonoBehaviour.IsInitialized())
			{
				global::Debug.LogErrorFormat("Could not find component " + typeof(T).ToString() + " on object " + go.ToString(), Array.Empty<object>());
			}
		}
		else
		{
			Util.InitializeComponent(t);
		}
		return t;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00025698 File Offset: 0x00023898
	public static void PreInit(this GameObject go)
	{
		KMonoBehaviour.isPoolPreInit = true;
		KMonoBehaviour[] components = go.GetComponents<KMonoBehaviour>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].InitializeComponent();
		}
		KMonoBehaviour.isPoolPreInit = false;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x000256D0 File Offset: 0x000238D0
	public static T KInstantiate<T>(GameObject original, GameObject parent = null, string name = null) where T : UnityEngine.Object
	{
		GameObject gameObject = Util.KInstantiate(original, parent, null);
		if (!(gameObject == null))
		{
			return gameObject.GetComponent<T>();
		}
		return default(T);
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x000256FF File Offset: 0x000238FF
	public static GameObject KInstantiate(GameObject original, Vector3 position)
	{
		return Util.KInstantiate(original, position, Quaternion.identity, null, null, true, 0);
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00025711 File Offset: 0x00023911
	public static GameObject KInstantiate(Component original, GameObject parent = null, string name = null)
	{
		return Util.KInstantiate(original.gameObject, Vector3.zero, Quaternion.identity, parent, name, true, 0);
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0002572C File Offset: 0x0002392C
	public static GameObject KInstantiate(GameObject original, GameObject parent = null, string name = null)
	{
		return Util.KInstantiate(original, Vector3.zero, Quaternion.identity, parent, name, true, 0);
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00025744 File Offset: 0x00023944
	public static GameObject KInstantiate(GameObject original, Vector3 position, Quaternion rotation, GameObject parent = null, string name = null, bool initialize_id = true, int gameLayer = 0)
	{
		if (App.IsExiting)
		{
			return null;
		}
		GameObject gameObject = null;
		if (original == null)
		{
			DebugUtil.LogWarningArgs(new object[] { "Missing prefab" });
		}
		if (gameObject == null)
		{
			if (original.GetComponent<RectTransform>() != null && parent != null)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(original, position, rotation);
				gameObject.transform.SetParent(parent.transform, true);
			}
			else
			{
				Transform transform = null;
				if (parent != null)
				{
					transform = parent.transform;
				}
				gameObject = UnityEngine.Object.Instantiate<GameObject>(original, position, rotation, transform);
			}
			if (gameLayer != 0)
			{
				gameObject.SetLayerRecursively(gameLayer);
			}
		}
		if (name != null)
		{
			gameObject.name = name;
		}
		else
		{
			gameObject.name = original.name;
		}
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		if (component != null)
		{
			if (initialize_id)
			{
				component.InstanceID = KPrefabID.GetUniqueID();
				KPrefabIDTracker.Get().Register(component);
			}
			component.InitializeTags(true);
			KPrefabID component2 = original.GetComponent<KPrefabID>();
			component.CopyTags(component2);
			component.CopyInitFunctions(component2);
			component.RunInstantiateFn();
		}
		return gameObject;
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00025844 File Offset: 0x00023A44
	public static T KInstantiateUI<T>(GameObject original, GameObject parent = null, bool force_active = false) where T : Component
	{
		return Util.KInstantiateUI(original, parent, force_active).GetComponent<T>();
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00025854 File Offset: 0x00023A54
	public static GameObject KInstantiateUI(GameObject original, GameObject parent = null, bool force_active = false)
	{
		if (App.IsExiting)
		{
			return null;
		}
		GameObject gameObject = null;
		if (original == null)
		{
			DebugUtil.LogWarningArgs(new object[] { "Missing prefab" });
		}
		if (gameObject == null)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(original, (parent != null) ? parent.transform : null, false);
		}
		gameObject.name = original.name;
		if (force_active)
		{
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x000258C4 File Offset: 0x00023AC4
	public static GameObject NewGameObject(GameObject parent, string name)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			gameObject.transform.parent = parent.transform;
		}
		if (name != null)
		{
			gameObject.name = name;
		}
		return gameObject;
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x000258FC File Offset: 0x00023AFC
	public static T UpdateComponentRequirement<T>(this GameObject go, bool required = true) where T : Component
	{
		T t = go.GetComponent(typeof(T)) as T;
		if (!required && t != null)
		{
			UnityEngine.Object.DestroyImmediate(t, true);
			t = default(T);
		}
		else if (required && t == null)
		{
			t = go.AddComponent(typeof(T)) as T;
		}
		return t;
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00025978 File Offset: 0x00023B78
	public static string FormatTwoDecimalPlace(float value)
	{
		return string.Format("{0:0.00}", value);
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0002598A File Offset: 0x00023B8A
	public static string FormatOneDecimalPlace(float value)
	{
		return string.Format("{0:0.0}", value);
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0002599C File Offset: 0x00023B9C
	public static string FormatWholeNumber(float value)
	{
		return string.Format("{0:0}", value);
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000259AE File Offset: 0x00023BAE
	public static bool IsInputCharacterValid(char _char, bool isPath = false, bool allowNewLine = false)
	{
		return (!isPath && allowNewLine && _char == '\n') || (!Util.defaultInvalidUserInputChars.Contains(_char) && (isPath || !Util.additionalInvalidUserInputChars.Contains(_char)));
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x000259E4 File Offset: 0x00023BE4
	public static bool IsInputStringValid(string input, bool isPath = false, bool allowNewLine = false)
	{
		for (int i = 0; i < input.Length; i++)
		{
			if (!Util.IsInputCharacterValid(input[i], isPath, allowNewLine))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00025A18 File Offset: 0x00023C18
	public static void ScrubInputField(KInputTextField inputField, bool isPath = false, bool allowNewLine = false)
	{
		for (int i = inputField.text.Length - 1; i >= 0; i--)
		{
			if (i < inputField.text.Length && !Util.IsInputCharacterValid(inputField.text[i], isPath, allowNewLine))
			{
				inputField.text = inputField.text.Remove(i, 1);
			}
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00025A73 File Offset: 0x00023C73
	public static string StripTextFormatting(string original)
	{
		return Regex.Replace(original, "<[^>]*>([^<]*)<[^>]*>", "$1");
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00025A85 File Offset: 0x00023C85
	public static void Reset(Transform transform)
	{
		transform.SetLocalPosition(Vector3.zero);
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00025AAC File Offset: 0x00023CAC
	public static float GaussianRandom(float mu = 0f, float sigma = 1f)
	{
		double num = Util.random.NextDouble();
		double num2 = Util.random.NextDouble();
		double num3 = (double)(Mathf.Sqrt(-2f * Mathf.Log((float)num)) * Mathf.Sin(6.2831855f * (float)num2));
		return (float)((double)mu + (double)sigma * num3);
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00025AF9 File Offset: 0x00023CF9
	public static void Shuffle<T>(this IList<T> list)
	{
		list.ShuffleSeeded(Util.random);
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00025B08 File Offset: 0x00023D08
	public static Bounds GetBounds(GameObject go)
	{
		Bounds bounds = default(Bounds);
		bool flag = true;
		Util.GetBounds(go, ref bounds, ref flag);
		return bounds;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00025B2C File Offset: 0x00023D2C
	private static void GetBounds(GameObject go, ref Bounds bounds, ref bool first)
	{
		if (go != null)
		{
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				if (first)
				{
					bounds = component.bounds;
					first = false;
				}
				else
				{
					bounds.Encapsulate(component.bounds);
				}
			}
			for (int i = 0; i < go.transform.childCount; i++)
			{
				Util.GetBounds(go.transform.GetChild(i).gameObject, ref bounds, ref first);
			}
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00025BA1 File Offset: 0x00023DA1
	public static bool IsOnLeftSideOfScreen(Vector3 position)
	{
		return position.x < (float)Screen.width;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00025BB1 File Offset: 0x00023DB1
	public static void Write(this BinaryWriter writer, Vector2 v)
	{
		writer.WriteSingleFast(v.x);
		writer.WriteSingleFast(v.y);
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00025BCB File Offset: 0x00023DCB
	public static void Write(this BinaryWriter writer, Vector3 v)
	{
		writer.WriteSingleFast(v.x);
		writer.WriteSingleFast(v.y);
		writer.WriteSingleFast(v.z);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00025BF4 File Offset: 0x00023DF4
	public static Vector2 ReadVector2(this BinaryReader reader)
	{
		return new Vector2
		{
			x = reader.ReadSingle(),
			y = reader.ReadSingle()
		};
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00025C24 File Offset: 0x00023E24
	public static Vector3 ReadVector3(this BinaryReader reader)
	{
		return new Vector3
		{
			x = reader.ReadSingle(),
			y = reader.ReadSingle(),
			z = reader.ReadSingle()
		};
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00025C61 File Offset: 0x00023E61
	public static void Write(this BinaryWriter writer, Quaternion q)
	{
		writer.WriteSingleFast(q.x);
		writer.WriteSingleFast(q.y);
		writer.WriteSingleFast(q.z);
		writer.WriteSingleFast(q.w);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00025C94 File Offset: 0x00023E94
	public static Quaternion ReadQuaternion(this BinaryReader reader)
	{
		return new Quaternion
		{
			x = reader.ReadSingle(),
			y = reader.ReadSingle(),
			z = reader.ReadSingle(),
			w = reader.ReadSingle()
		};
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00025CE0 File Offset: 0x00023EE0
	public static Color ColorFromHex(string hex)
	{
		int num = Convert.ToInt32(hex, 16);
		float num2 = 1f;
		float num3 = 1f;
		float num4 = 1f;
		float num5 = 1f;
		if (hex.Length == 6)
		{
			num2 = (float)((num >> 16) & 255);
			num2 /= 255f;
			num3 = (float)((num >> 8) & 255);
			num3 /= 255f;
			num4 = (float)(num & 255);
			num4 /= 255f;
		}
		else if (hex.Length == 8)
		{
			num2 = (float)((num >> 24) & 255);
			num2 /= 255f;
			num3 = (float)((num >> 16) & 255);
			num3 /= 255f;
			num4 = (float)((num >> 8) & 255);
			num4 /= 255f;
			num5 = (float)(num & 255);
			num5 /= 255f;
		}
		return new Color(num2, num3, num4, num5);
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00025DB4 File Offset: 0x00023FB4
	public static string ToHexString(this Color c)
	{
		return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
		{
			(int)(c.r * 255f),
			(int)(c.g * 255f),
			(int)(c.b * 255f),
			(int)(c.a * 255f)
		});
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00025E25 File Offset: 0x00024025
	public static void Signal(this System.Action action)
	{
		if (action != null)
		{
			action();
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00025E30 File Offset: 0x00024030
	public static void Signal<T>(this Action<T> action, T parameter)
	{
		if (action != null)
		{
			action(parameter);
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00025E3C File Offset: 0x0002403C
	public static RectTransform rectTransform(this GameObject go)
	{
		return go.GetComponent<RectTransform>();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00025E44 File Offset: 0x00024044
	public static RectTransform rectTransform(this Component cmp)
	{
		return cmp.GetComponent<RectTransform>();
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00025E4C File Offset: 0x0002404C
	public static T[] Append<T>(this T[] array, T item)
	{
		T[] array2 = new T[array.Length + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		array2[array.Length] = item;
		return array2;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x00025E8C File Offset: 0x0002408C
	public static T[] Concat<T>(this T[] a1, T[] a2)
	{
		T[] array = new T[a1.Length + a2.Length];
		a1.CopyTo(array, 0);
		a2.CopyTo(array, a1.Length);
		return array;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00025EB9 File Offset: 0x000240B9
	public static string GetKleiRootPath()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Klei");
		}
		return Util.defaultRootFolder;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00025EE1 File Offset: 0x000240E1
	public static string GetTitleFolderName()
	{
		return "OxygenNotIncluded";
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00025EE8 File Offset: 0x000240E8
	public static string GetRetiredColoniesFolderName()
	{
		return "RetiredColonies";
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x00025EEF File Offset: 0x000240EF
	public static string GetKleiItemUserDataFolderName()
	{
		return "KleiItemData";
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00025EF6 File Offset: 0x000240F6
	public static string RootFolder()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			return Path.Combine(Util.GetKleiRootPath(), Util.GetTitleFolderName());
		}
		return Util.GetKleiRootPath();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00025F1D File Offset: 0x0002411D
	public static string LogFilePath()
	{
		return Util.consoleLogPath;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00025F24 File Offset: 0x00024124
	public static string LogsFolder()
	{
		return Path.GetDirectoryName(Util.consoleLogPath);
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x00025F30 File Offset: 0x00024130
	public static string CacheFolder()
	{
		return Path.Combine(Util.defaultRootFolder, "cache");
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x00025F44 File Offset: 0x00024144
	public static Transform FindTransformRecursive(Transform node, string name)
	{
		if (node.name == name)
		{
			return node;
		}
		for (int i = 0; i < node.childCount; i++)
		{
			Transform transform = Util.FindTransformRecursive(node.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00025F8C File Offset: 0x0002418C
	public static Vector3 ReadVector3(this IReader reader)
	{
		return new Vector3
		{
			x = reader.ReadSingle(),
			y = reader.ReadSingle(),
			z = reader.ReadSingle()
		};
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00025FCC File Offset: 0x000241CC
	public static Quaternion ReadQuaternion(this IReader reader)
	{
		return new Quaternion
		{
			x = reader.ReadSingle(),
			y = reader.ReadSingle(),
			z = reader.ReadSingle(),
			w = reader.ReadSingle()
		};
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00026016 File Offset: 0x00024216
	public static T GetRandom<T>(this T[] tArray)
	{
		return tArray[UnityEngine.Random.Range(0, tArray.Length)];
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00026027 File Offset: 0x00024227
	public static T GetRandom<T>(this List<T> tList)
	{
		return tList[UnityEngine.Random.Range(0, tList.Count)];
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0002603B File Offset: 0x0002423B
	public static T GetRandom<T>(this IEnumerable<T> tEnumerable)
	{
		return tEnumerable.Shuffle<T>().First<T>();
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00026048 File Offset: 0x00024248
	public static void ShuffleList(IList list_or_array, System.Random random)
	{
		for (int i = list_or_array.Count - 1; i > 0; i--)
		{
			int num = random.Next(i + 1);
			Util.Swap(list_or_array, i, num);
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0002607C File Offset: 0x0002427C
	public static void ShiftLeft<T>(this T[] array, T new_ending_value = default(T))
	{
		Array.Copy(array, 1, array, 0, array.Length - 1);
		array[array.Length - 1] = new_ending_value;
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00026098 File Offset: 0x00024298
	public static void ShiftRight<T>(this T[] array, T new_starting_value = default(T))
	{
		Array.Copy(array, 0, array, 1, array.Length - 1);
		array[0] = new_starting_value;
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x000260B0 File Offset: 0x000242B0
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable, System.Random random)
	{
		List<T> list = enumerable.ToList<T>();
		Util.ShuffleList(list, random);
		return list;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x000260BF File Offset: 0x000242BF
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
	{
		return enumerable.Shuffle(new System.Random());
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x000260CC File Offset: 0x000242CC
	public static IOrderedEnumerable<T> StableSort<T>(this IEnumerable<T> enumerable)
	{
		return enumerable.OrderBy((T t) => t);
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x000260F3 File Offset: 0x000242F3
	public static IOrderedEnumerable<T> StableSort<T>(this IEnumerable<T> enumerable, Comparer<T> comparer)
	{
		return enumerable.OrderBy((T t) => t, comparer);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0002611B File Offset: 0x0002431B
	public static IOrderedEnumerable<T> StableSort<T>(this IEnumerable<T> enumerable, Comparison<T> comparer)
	{
		return enumerable.OrderBy((T t) => t, Comparer<T>.Create(comparer));
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00026148 File Offset: 0x00024348
	public static IOrderedEnumerable<T> StableSort<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> key_selector)
	{
		return enumerable.OrderBy(key_selector);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00026151 File Offset: 0x00024351
	public static IOrderedEnumerable<T> StableSort<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> key_selector, Comparer<TKey> comparer)
	{
		return enumerable.OrderBy(key_selector, comparer);
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0002615B File Offset: 0x0002435B
	public static IOrderedEnumerable<T> StableSort<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> key_selector, Comparison<TKey> comparer)
	{
		return enumerable.OrderBy(key_selector, Comparer<TKey>.Create(comparer));
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0002616A File Offset: 0x0002436A
	public static float RandomVariance(float center, float plusminus)
	{
		return center + UnityEngine.Random.Range(-plusminus, plusminus);
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00026176 File Offset: 0x00024376
	public static bool IsNullOrWhiteSpace(this string str)
	{
		return string.IsNullOrEmpty(str) || str == " ";
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0002618D File Offset: 0x0002438D
	public static void ApplyInvariantCultureToThread(Thread thread)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			thread.CurrentCulture = CultureInfo.InvariantCulture;
		}
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x000261A2 File Offset: 0x000243A2
	public static bool IsNullOrDestroyed(this object obj)
	{
		return obj == null || (obj is UnityEngine.Object && obj as UnityEngine.Object == null);
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x000261BF File Offset: 0x000243BF
	public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> self, out TKey key, out TValue value)
	{
		key = self.Key;
		value = self.Value;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x000261DB File Offset: 0x000243DB
	public static void Deconstruct<T, U>(this Pair<T, U> self, out T first, out U second)
	{
		first = self.first;
		second = self.second;
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x000261F8 File Offset: 0x000243F8
	public static int IntPow(int x, int pow)
	{
		int num = 1;
		while (pow != 0)
		{
			if ((pow & 1) == 1)
			{
				num *= x;
			}
			x *= x;
			pow >>= 1;
		}
		return num;
	}

	// Token: 0x040006A0 RID: 1696
	private static HashSet<char> defaultInvalidUserInputChars = new HashSet<char>(Path.GetInvalidPathChars());

	// Token: 0x040006A1 RID: 1697
	private static HashSet<char> additionalInvalidUserInputChars = new HashSet<char>(new char[] { '<', '>', ':', '"', '/', '?', '*', '\\', '!', '.' });

	// Token: 0x040006A2 RID: 1698
	private static KRandom random = new KRandom();

	// Token: 0x040006A3 RID: 1699
	private static string defaultRootFolder = Application.persistentDataPath;

	// Token: 0x040006A4 RID: 1700
	private static string consoleLogPath = Application.consoleLogPath;
}
