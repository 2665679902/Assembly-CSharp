using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x02000526 RID: 1318
	public static class Helper
	{
		// Token: 0x06003812 RID: 14354 RVA: 0x0007F59F File Offset: 0x0007D79F
		public static int GetAllocationCount()
		{
			return Helper.s_Allocations.Count;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x0007F5AC File Offset: 0x0007D7AC
		public static bool IsOperationComplete(Result result)
		{
			int num = Helper.EOS_EResult_IsOperationComplete(result);
			bool flag = false;
			Helper.TryMarshalGet(num, out flag);
			return flag;
		}

		// Token: 0x06003814 RID: 14356
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_EResult_IsOperationComplete(Result result);

		// Token: 0x06003815 RID: 14357 RVA: 0x0007F5CA File Offset: 0x0007D7CA
		public static string ToHexString(byte[] byteArray)
		{
			return string.Join("", byteArray.Select((byte b) => string.Format("{0:X2}", b)).ToArray<string>());
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x0007F600 File Offset: 0x0007D800
		internal static bool TryMarshalGet<T>(T source, out T target)
		{
			target = source;
			return true;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x0007F60A File Offset: 0x0007D80A
		internal static bool TryMarshalGet(IntPtr source, out IntPtr target)
		{
			target = source;
			return true;
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x0007F610 File Offset: 0x0007D810
		internal static bool TryMarshalGet<T>(IntPtr source, out T target) where T : Handle
		{
			return Helper.TryConvert<T>(source, out target);
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x0007F619 File Offset: 0x0007D819
		internal static bool TryMarshalGet(int source, out bool target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x0007F622 File Offset: 0x0007D822
		internal static bool TryMarshalGet(long source, out DateTimeOffset? target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x0007F62B File Offset: 0x0007D82B
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, int arrayLength, bool isElementAllocated)
		{
			return Helper.TryFetch<T>(source, out target, arrayLength, isElementAllocated);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0007F636 File Offset: 0x0007D836
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, uint arrayLength, bool isElementAllocated)
		{
			return Helper.TryFetch<T>(source, out target, (int)arrayLength, isElementAllocated);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x0007F641 File Offset: 0x0007D841
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, int arrayLength)
		{
			return Helper.TryMarshalGet<T>(source, out target, arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x0007F65D File Offset: 0x0007D85D
		internal static bool TryMarshalGet<T>(IntPtr source, out T[] target, uint arrayLength)
		{
			return Helper.TryMarshalGet<T>(source, out target, arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x0007F679 File Offset: 0x0007D879
		internal static bool TryMarshalGet<T>(IntPtr source, out T? target) where T : struct
		{
			return Helper.TryFetch<T>(source, out target);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x0007F682 File Offset: 0x0007D882
		internal static bool TryMarshalGet(byte[] source, out string target)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0007F68C File Offset: 0x0007D88C
		internal static bool TryMarshalGet(IntPtr source, out object target)
		{
			target = null;
			BoxedData boxedData;
			if (Helper.TryFetch<BoxedData>(source, out boxedData))
			{
				target = boxedData.Data;
				return true;
			}
			return false;
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x0007F6B1 File Offset: 0x0007D8B1
		internal static bool TryMarshalGet(IntPtr source, out string target)
		{
			return Helper.TryFetch(source, out target);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x0007F6BA File Offset: 0x0007D8BA
		internal static bool TryMarshalGet<T, TEnum>(T source, out T target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<T>();
			if ((int)((object)currentEnum) == (int)((object)comparisonEnum))
			{
				target = source;
				return true;
			}
			return false;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x0007F6E9 File Offset: 0x0007D8E9
		internal static bool TryMarshalGet<T, TEnum>(T source, out T? target, TEnum currentEnum, TEnum comparisonEnum) where T : struct
		{
			target = Helper.GetDefault<T?>();
			if ((int)((object)currentEnum) == (int)((object)comparisonEnum))
			{
				target = new T?(source);
				return true;
			}
			return false;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x0007F71D File Offset: 0x0007D91D
		internal static bool TryMarshalGet<T, TEnum>(IntPtr source, out T target, TEnum currentEnum, TEnum comparisonEnum) where T : Handle
		{
			target = Helper.GetDefault<T>();
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryMarshalGet<T>(source, out target);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x0007F74B File Offset: 0x0007D94B
		internal static bool TryMarshalGet<TEnum>(IntPtr source, out string target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<string>();
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryMarshalGet(source, out target);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x0007F778 File Offset: 0x0007D978
		internal static bool TryMarshalGet<TEnum>(int source, out bool? target, TEnum currentEnum, TEnum comparisonEnum)
		{
			target = Helper.GetDefault<bool?>();
			bool flag;
			if ((int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryConvert(source, out flag))
			{
				target = new bool?(flag);
				return true;
			}
			return false;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x0007F7C4 File Offset: 0x0007D9C4
		internal static bool TryMarshalGet<TInternal, TPublic>(IntPtr source, out TPublic target) where TInternal : struct where TPublic : class, new()
		{
			target = default(TPublic);
			TInternal tinternal;
			if (Helper.TryFetch<TInternal>(source, out tinternal))
			{
				target = Helper.CopyProperties<TPublic>(tinternal);
				return true;
			}
			return false;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x0007F7F8 File Offset: 0x0007D9F8
		internal static bool TryMarshalGet<TCallbackInfoInternal, TCallbackInfo>(IntPtr callbackInfoAddress, out TCallbackInfo callbackInfo, out IntPtr clientDataAddress) where TCallbackInfoInternal : struct, ICallbackInfo where TCallbackInfo : class, new()
		{
			callbackInfo = default(TCallbackInfo);
			clientDataAddress = IntPtr.Zero;
			TCallbackInfoInternal tcallbackInfoInternal;
			if (Helper.TryFetch<TCallbackInfoInternal>(callbackInfoAddress, out tcallbackInfoInternal))
			{
				callbackInfo = Helper.CopyProperties<TCallbackInfo>(tcallbackInfoInternal);
				clientDataAddress = tcallbackInfoInternal.ClientDataAddress;
				return true;
			}
			return false;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x0007F840 File Offset: 0x0007DA40
		internal static bool TryMarshalSet<T>(ref T target, T source)
		{
			target = source;
			return true;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x0007F84A File Offset: 0x0007DA4A
		internal static bool TryMarshalSet(ref IntPtr target, Handle source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x0007F853 File Offset: 0x0007DA53
		internal static bool TryMarshalSet<T>(ref IntPtr target, T? source) where T : struct
		{
			return Helper.TryAllocate<T>(ref target, source);
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x0007F85C File Offset: 0x0007DA5C
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, bool isElementAllocated)
		{
			return Helper.TryAllocate<T>(ref target, source, isElementAllocated);
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x0007F866 File Offset: 0x0007DA66
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source)
		{
			return Helper.TryMarshalSet<T>(ref target, source, !typeof(T).IsValueType);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x0007F881 File Offset: 0x0007DA81
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out int arrayLength, bool isElementAllocated)
		{
			arrayLength = 0;
			if (Helper.TryMarshalSet<T>(ref target, source, isElementAllocated))
			{
				arrayLength = source.Length;
				return true;
			}
			return false;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x0007F898 File Offset: 0x0007DA98
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out uint arrayLength, bool isElementAllocated)
		{
			arrayLength = 0U;
			int num = 0;
			if (Helper.TryMarshalSet<T>(ref target, source, out num, isElementAllocated))
			{
				arrayLength = (uint)num;
				return true;
			}
			return false;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x0007F8BC File Offset: 0x0007DABC
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out int arrayLength)
		{
			return Helper.TryMarshalSet<T>(ref target, source, out arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x0007F8D8 File Offset: 0x0007DAD8
		internal static bool TryMarshalSet<T>(ref IntPtr target, T[] source, out uint arrayLength)
		{
			return Helper.TryMarshalSet<T>(ref target, source, out arrayLength, !typeof(T).IsValueType);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0007F8F4 File Offset: 0x0007DAF4
		internal static bool TryMarshalSet(ref long target, DateTimeOffset? source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x0007F8FD File Offset: 0x0007DAFD
		internal static bool TryMarshalSet(ref int target, bool source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x0007F906 File Offset: 0x0007DB06
		internal static bool TryMarshalSet(ref byte[] target, string source)
		{
			return Helper.TryConvert(source, out target);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x0007F90F File Offset: 0x0007DB0F
		internal static bool TryMarshalSet(ref byte[] target, string source, int length)
		{
			return Helper.TryConvert(source, out target, length);
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x0007F919 File Offset: 0x0007DB19
		internal static bool TryMarshalSet(ref IntPtr target, string source)
		{
			return Helper.TryAllocate(ref target, source);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x0007F922 File Offset: 0x0007DB22
		internal static bool TryMarshalSet<T, TEnum>(ref T target, T source, ref TEnum currentEnum, TEnum comparisonEnum, object disposable)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose(ref disposable);
				if (Helper.TryMarshalSet<T>(ref target, source))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x0007F947 File Offset: 0x0007DB47
		internal static bool TryMarshalSet<T, TEnum>(ref T target, T? source, ref TEnum currentEnum, TEnum comparisonEnum, object disposable) where T : struct
		{
			if (source != null)
			{
				Helper.TryMarshalDispose(ref disposable);
				if (Helper.TryMarshalSet<T>(ref target, source.Value))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x0007F973 File Offset: 0x0007DB73
		internal static bool TryMarshalSet<T, TEnum>(ref IntPtr target, T source, ref TEnum currentEnum, TEnum comparisonEnum, object disposable) where T : Handle
		{
			if (source != null)
			{
				Helper.TryMarshalDispose(ref disposable);
				if (Helper.TryMarshalSet(ref target, source))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x0007F9A3 File Offset: 0x0007DBA3
		internal static bool TryMarshalSet<TEnum>(ref IntPtr target, string source, ref TEnum currentEnum, TEnum comparisonEnum, object disposable)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose(ref disposable);
				if (Helper.TryMarshalSet(ref target, source))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0007F9C3 File Offset: 0x0007DBC3
		internal static bool TryMarshalSet<TEnum>(ref int target, bool? source, ref TEnum currentEnum, TEnum comparisonEnum, object disposable)
		{
			if (source != null)
			{
				Helper.TryMarshalDispose(ref disposable);
				if (Helper.TryMarshalSet(ref target, source.Value))
				{
					currentEnum = comparisonEnum;
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x0007F9F0 File Offset: 0x0007DBF0
		internal static bool TryMarshalDispose(ref object value)
		{
			IDisposable disposable = value as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x0007FA11 File Offset: 0x0007DC11
		internal static bool TryMarshalDispose<T>(ref T value) where T : IDisposable
		{
			value.Dispose();
			return true;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x0007FA20 File Offset: 0x0007DC20
		internal static bool TryMarshalDispose(ref IntPtr value)
		{
			return Helper.TryRelease(ref value);
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x0007FA28 File Offset: 0x0007DC28
		internal static bool TryMarshalDispose<TEnum>(ref IntPtr member, TEnum currentEnum, TEnum comparisonEnum)
		{
			return (int)((object)currentEnum) == (int)((object)comparisonEnum) && Helper.TryRelease(ref member);
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x0007FA4C File Offset: 0x0007DC4C
		internal static T GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0007FA64 File Offset: 0x0007DC64
		internal static T CopyProperties<T>(object value) where T : new()
		{
			object obj = new T();
			IInitializable initializable = obj as IInitializable;
			if (initializable != null)
			{
				initializable.Initialize();
			}
			Helper.CopyProperties(value, obj);
			return (T)((object)obj);
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x0007FA99 File Offset: 0x0007DC99
		internal static void AddCallback(ref IntPtr clientDataAddress, object clientData, Delegate publicDelegate, Delegate privateDelegate, params Delegate[] additionalDelegates)
		{
			Helper.TryAllocate<BoxedData>(ref clientDataAddress, new BoxedData(clientData));
			Helper.s_Callbacks.Add(clientDataAddress, new Helper.DelegateHolder(publicDelegate, privateDelegate, additionalDelegates));
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0007FAC0 File Offset: 0x0007DCC0
		internal static bool TryAssignNotificationIdToCallback(IntPtr clientDataAddress, ulong notificationId)
		{
			if (notificationId != 0UL)
			{
				Helper.DelegateHolder delegateHolder = null;
				if (Helper.s_Callbacks.TryGetValue(clientDataAddress, out delegateHolder))
				{
					delegateHolder.NotificationId = new ulong?(notificationId);
					return true;
				}
			}
			else
			{
				Helper.s_Callbacks.Remove(clientDataAddress);
				Helper.TryRelease(ref clientDataAddress);
			}
			return false;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x0007FB04 File Offset: 0x0007DD04
		internal static bool TryRemoveCallbackByNotificationId(ulong notificationId)
		{
			IEnumerable<KeyValuePair<IntPtr, Helper.DelegateHolder>> enumerable = Helper.s_Callbacks.Where(delegate(KeyValuePair<IntPtr, Helper.DelegateHolder> pair)
			{
				if (pair.Value.NotificationId != null)
				{
					ulong? notificationId2 = pair.Value.NotificationId;
					ulong notificationId3 = notificationId;
					return (notificationId2.GetValueOrDefault() == notificationId3) & (notificationId2 != null);
				}
				return false;
			});
			if (enumerable.Any<KeyValuePair<IntPtr, Helper.DelegateHolder>>())
			{
				IntPtr key = enumerable.First<KeyValuePair<IntPtr, Helper.DelegateHolder>>().Key;
				Helper.s_Callbacks.Remove(key);
				Helper.TryRelease(ref key);
				return true;
			}
			return false;
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x0007FB64 File Offset: 0x0007DD64
		internal static bool TryGetAndRemoveCallback<TCallback, TCallbackInfoInternal, TCallbackInfo>(IntPtr callbackInfoAddress, out TCallback callback, out TCallbackInfo callbackInfo) where TCallback : class where TCallbackInfoInternal : struct, ICallbackInfo where TCallbackInfo : class, new()
		{
			callback = default(TCallback);
			callbackInfo = default(TCallbackInfo);
			IntPtr zero = IntPtr.Zero;
			return Helper.TryMarshalGet<TCallbackInfoInternal, TCallbackInfo>(callbackInfoAddress, out callbackInfo, out zero) && Helper.TryGetAndRemoveCallback<TCallback>(zero, callbackInfo, out callback);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x0007FBA8 File Offset: 0x0007DDA8
		internal static bool TryGetAdditionalCallback<TDelegate, TCallbackInfoInternal, TCallbackInfo>(IntPtr callbackInfoAddress, out TDelegate callback, out TCallbackInfo callbackInfo) where TDelegate : class where TCallbackInfoInternal : struct, ICallbackInfo where TCallbackInfo : class, new()
		{
			callback = default(TDelegate);
			callbackInfo = default(TCallbackInfo);
			IntPtr zero = IntPtr.Zero;
			return Helper.TryMarshalGet<TCallbackInfoInternal, TCallbackInfo>(callbackInfoAddress, out callbackInfo, out zero) && Helper.TryGetAdditionalCallback<TDelegate>(zero, out callback);
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x0007FBE0 File Offset: 0x0007DDE0
		private static bool TryAllocate<T>(ref IntPtr target, T source)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			if (source == null)
			{
				return false;
			}
			target = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
			Marshal.StructureToPtr<T>(source, target, false);
			Helper.s_Allocations.Add(target, new Helper.Allocation(source));
			return true;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x0007FC58 File Offset: 0x0007DE58
		private static bool TryAllocate<T>(ref IntPtr target, T? source) where T : struct
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			return source != null && Helper.TryAllocate<T>(ref target, source.Value);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0007FCA8 File Offset: 0x0007DEA8
		private static bool TryAllocate(ref IntPtr target, string source)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			byte[] array;
			return source != null && Helper.TryConvert(source, out array) && Helper.TryAllocate<byte>(ref target, array, false);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x0007FCF4 File Offset: 0x0007DEF4
		private static bool TryAllocate<T>(ref IntPtr target, T[] source, bool isElementAllocated)
		{
			Helper.TryRelease(ref target);
			if (target != IntPtr.Zero)
			{
				throw new ExternalAllocationException(target, source.GetType());
			}
			if (source == null)
			{
				return false;
			}
			int num;
			if (isElementAllocated)
			{
				num = Marshal.SizeOf(typeof(IntPtr));
			}
			else
			{
				num = Marshal.SizeOf(typeof(T));
			}
			target = Marshal.AllocHGlobal(source.Length * num);
			Helper.s_Allocations.Add(target, new Helper.ArrayAllocation(source, isElementAllocated));
			for (int i = 0; i < source.Length; i++)
			{
				T t = (T)((object)source.GetValue(i));
				if (isElementAllocated)
				{
					IntPtr zero = IntPtr.Zero;
					if (typeof(T) == typeof(string))
					{
						Helper.TryAllocate(ref zero, (string)((object)t));
					}
					else if (typeof(T).BaseType == typeof(Handle))
					{
						Helper.TryConvert((Handle)((object)t), out zero);
					}
					else
					{
						Helper.TryAllocate<T>(ref zero, t);
					}
					IntPtr intPtr = new IntPtr(target.ToInt64() + (long)(i * num));
					Marshal.StructureToPtr<IntPtr>(zero, intPtr, false);
				}
				else
				{
					IntPtr intPtr2 = new IntPtr(target.ToInt64() + (long)(i * num));
					Marshal.StructureToPtr<T>(t, intPtr2, false);
				}
			}
			return true;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x0007FE44 File Offset: 0x0007E044
		private static bool TryRelease(ref IntPtr target)
		{
			if (target == IntPtr.Zero)
			{
				return false;
			}
			Helper.Allocation allocation = null;
			if (!Helper.s_Allocations.TryGetValue(target, out allocation))
			{
				return false;
			}
			if (allocation is Helper.ArrayAllocation)
			{
				Helper.ArrayAllocation arrayAllocation = allocation as Helper.ArrayAllocation;
				int num;
				if (arrayAllocation.IsElementAllocated)
				{
					num = Marshal.SizeOf(typeof(IntPtr));
				}
				else
				{
					num = Marshal.SizeOf(arrayAllocation.Data.GetType().GetElementType());
				}
				Array array = arrayAllocation.Data as Array;
				for (int i = 0; i < array.Length; i++)
				{
					if (arrayAllocation.IsElementAllocated)
					{
						IntPtr intPtr = new IntPtr(target.ToInt64() + (long)(i * num));
						intPtr = Marshal.ReadIntPtr(intPtr);
						Helper.TryRelease(ref intPtr);
					}
					else
					{
						object value = array.GetValue(i);
						if (value is IDisposable)
						{
							IDisposable disposable = value as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
			}
			if (allocation.Data is IDisposable)
			{
				IDisposable disposable2 = allocation.Data as IDisposable;
				if (disposable2 != null)
				{
					disposable2.Dispose();
				}
			}
			Marshal.FreeHGlobal(target);
			Helper.s_Allocations.Remove(target);
			target = IntPtr.Zero;
			return true;
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x0007FF70 File Offset: 0x0007E170
		private static bool TryFetch<T>(IntPtr source, out T target)
		{
			target = Helper.GetDefault<T>();
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (!Helper.s_Allocations.ContainsKey(source))
			{
				target = (T)((object)Marshal.PtrToStructure(source, typeof(T)));
				return true;
			}
			Helper.Allocation allocation = Helper.s_Allocations[source];
			if (allocation.Data.GetType() == typeof(T))
			{
				target = (T)((object)allocation.Data);
				return true;
			}
			throw new TypeAllocationException(source, allocation.Data.GetType(), typeof(T));
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x00080018 File Offset: 0x0007E218
		private static bool TryFetch<T>(IntPtr source, out T? target) where T : struct
		{
			target = Helper.GetDefault<T?>();
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (!Helper.s_Allocations.ContainsKey(source))
			{
				target = (T?)Marshal.PtrToStructure(source, typeof(T));
				return true;
			}
			Helper.Allocation allocation = Helper.s_Allocations[source];
			if (allocation.Data.GetType() == typeof(T))
			{
				target = (T?)allocation.Data;
				return true;
			}
			throw new TypeAllocationException(source, allocation.Data.GetType(), typeof(T));
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000800C0 File Offset: 0x0007E2C0
		private static bool TryFetch<T>(IntPtr source, out T[] target, int arrayLength, bool isElementAllocated)
		{
			target = null;
			if (source == IntPtr.Zero)
			{
				return false;
			}
			if (!Helper.s_Allocations.ContainsKey(source))
			{
				int num;
				if (isElementAllocated)
				{
					num = Marshal.SizeOf(typeof(IntPtr));
				}
				else
				{
					num = Marshal.SizeOf(typeof(T));
				}
				List<T> list = new List<T>();
				for (int i = 0; i < arrayLength; i++)
				{
					IntPtr intPtr = new IntPtr(source.ToInt64() + (long)(i * num));
					if (isElementAllocated)
					{
						intPtr = Marshal.ReadIntPtr(intPtr);
					}
					T t;
					Helper.TryFetch<T>(intPtr, out t);
					list.Add(t);
				}
				target = list.ToArray();
				return true;
			}
			Helper.Allocation allocation = Helper.s_Allocations[source];
			if (!(allocation.Data.GetType() == typeof(T[])))
			{
				throw new TypeAllocationException(source, allocation.Data.GetType(), typeof(T[]));
			}
			Array array = (Array)allocation.Data;
			if (array.Length == arrayLength)
			{
				target = array as T[];
				return true;
			}
			throw new ArrayAllocationException(source, array.Length, arrayLength);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000801D8 File Offset: 0x0007E3D8
		private static bool TryFetch(IntPtr source, out string target)
		{
			target = null;
			if (source == IntPtr.Zero)
			{
				return false;
			}
			int num = 0;
			while (Marshal.ReadByte(source, num) != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, num);
			target = Encoding.UTF8.GetString(array);
			return true;
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x00080228 File Offset: 0x0007E428
		private static bool TryConvert<THandle>(IntPtr source, out THandle target) where THandle : Handle
		{
			target = default(THandle);
			if (source != IntPtr.Zero)
			{
				target = Activator.CreateInstance(typeof(THandle), new object[] { source }) as THandle;
			}
			return true;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x00080278 File Offset: 0x0007E478
		private static bool TryConvert(Handle source, out IntPtr target)
		{
			target = IntPtr.Zero;
			if (source != null)
			{
				target = source.InnerHandle;
			}
			return true;
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x00080294 File Offset: 0x0007E494
		private static bool TryConvert(byte[] source, out string target)
		{
			target = null;
			if (source == null)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			while (num2 < source.Length && source[num2] != 0)
			{
				num++;
				num2++;
			}
			target = Encoding.UTF8.GetString(source.Take(num).ToArray<byte>());
			return true;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000802DD File Offset: 0x0007E4DD
		private static bool TryConvert(string source, out byte[] target, int length)
		{
			if (source == null)
			{
				source = "";
			}
			target = Encoding.UTF8.GetBytes(new string(source.Take(length).ToArray<char>()).PadRight(length, '\0'));
			return true;
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0008030E File Offset: 0x0007E50E
		private static bool TryConvert(string source, out byte[] target)
		{
			return Helper.TryConvert(source, out target, source.Length + 1);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0008031F File Offset: 0x0007E51F
		private static bool TryConvert(int source, out bool target)
		{
			target = source != 0;
			return true;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x00080328 File Offset: 0x0007E528
		private static bool TryConvert(bool source, out int target)
		{
			target = (source ? 1 : 0);
			return true;
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x00080334 File Offset: 0x0007E534
		private static bool TryConvert(DateTimeOffset? source, out long target)
		{
			target = -1L;
			if (source != null)
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				long num = (source.Value.UtcDateTime - dateTime).Ticks / 10000000L;
				target = num;
			}
			return true;
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x0008038C File Offset: 0x0007E58C
		private static bool TryConvert(long source, out DateTimeOffset? target)
		{
			target = null;
			if (source >= 0L)
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				long num = source * 10000000L;
				target = new DateTimeOffset?(new DateTimeOffset(dateTime.Ticks + num, TimeSpan.Zero));
			}
			return true;
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000803E0 File Offset: 0x0007E5E0
		private static void CopyProperties(object source, object target)
		{
			if (source == null || target == null)
			{
				return;
			}
			PropertyInfo[] properties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
			PropertyInfo[] properties2 = target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
			PropertyInfo[] array = properties;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo sourceProperty = array[i];
				PropertyInfo propertyInfo = properties2.SingleOrDefault((PropertyInfo property) => property.Name == sourceProperty.Name);
				if (!(propertyInfo == null) && !(propertyInfo.GetSetMethod(false) == null))
				{
					if (sourceProperty.PropertyType == propertyInfo.PropertyType)
					{
						propertyInfo.SetValue(target, sourceProperty.GetValue(source, null), null);
					}
					else if (propertyInfo.PropertyType.IsArray)
					{
						Array array2 = sourceProperty.GetValue(source, null) as Array;
						if (array2 != null)
						{
							Array array3 = Array.CreateInstance(propertyInfo.PropertyType.GetElementType(), array2.Length);
							for (int j = 0; j < array2.Length; j++)
							{
								object value = array2.GetValue(j);
								object obj = Activator.CreateInstance(propertyInfo.PropertyType.GetElementType());
								Helper.CopyProperties(value, obj);
								array3.SetValue(obj, j);
							}
							propertyInfo.SetValue(target, array3, null);
						}
						else
						{
							propertyInfo.SetValue(target, null, null);
						}
					}
					else
					{
						object obj2 = null;
						Type type = propertyInfo.PropertyType;
						Type underlyingType = Nullable.GetUnderlyingType(type);
						if (underlyingType != null)
						{
							type = underlyingType;
						}
						else
						{
							obj2 = Activator.CreateInstance(type);
						}
						object value2 = sourceProperty.GetValue(source, null);
						if (value2 != null)
						{
							obj2 = Activator.CreateInstance(type);
							Helper.CopyProperties(value2, obj2);
						}
						propertyInfo.SetValue(target, obj2, null);
					}
				}
			}
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000805A0 File Offset: 0x0007E7A0
		private static bool CanRemoveCallback(IntPtr clientDataAddress, object callbackInfo)
		{
			Helper.DelegateHolder delegateHolder = null;
			if (Helper.s_Callbacks.TryGetValue(clientDataAddress, out delegateHolder) && delegateHolder.NotificationId != null)
			{
				return false;
			}
			PropertyInfo propertyInfo = (from property in callbackInfo.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
				where property.PropertyType == typeof(Result)
				select property).FirstOrDefault<PropertyInfo>();
			return !(propertyInfo != null) || Helper.IsOperationComplete((Result)propertyInfo.GetValue(callbackInfo, null));
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x00080628 File Offset: 0x0007E828
		private static bool TryGetAndRemoveCallback<TCallback>(IntPtr clientDataAddress, object callbackInfo, out TCallback callback) where TCallback : class
		{
			callback = default(TCallback);
			if (clientDataAddress != IntPtr.Zero && Helper.s_Callbacks.ContainsKey(clientDataAddress))
			{
				callback = Helper.s_Callbacks[clientDataAddress].Public as TCallback;
				if (Helper.CanRemoveCallback(clientDataAddress, callbackInfo))
				{
					Helper.s_Callbacks.Remove(clientDataAddress);
					Helper.TryRelease(ref clientDataAddress);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x00080698 File Offset: 0x0007E898
		private static bool TryGetAdditionalCallback<TCallback>(IntPtr clientDataAddress, out TCallback additionalCallback) where TCallback : class
		{
			additionalCallback = default(TCallback);
			if (clientDataAddress != IntPtr.Zero && Helper.s_Callbacks.ContainsKey(clientDataAddress))
			{
				additionalCallback = Helper.s_Callbacks[clientDataAddress].Additional.FirstOrDefault((Delegate delegat) => delegat.GetType() == typeof(TCallback)) as TCallback;
				if (additionalCallback != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400143F RID: 5183
		private static Dictionary<IntPtr, Helper.Allocation> s_Allocations = new Dictionary<IntPtr, Helper.Allocation>();

		// Token: 0x04001440 RID: 5184
		private static Dictionary<IntPtr, Helper.DelegateHolder> s_Callbacks = new Dictionary<IntPtr, Helper.DelegateHolder>();

		// Token: 0x02000B2E RID: 2862
		private class Allocation
		{
			// Token: 0x17000EFC RID: 3836
			// (get) Token: 0x0600587F RID: 22655 RVA: 0x000A4854 File Offset: 0x000A2A54
			// (set) Token: 0x06005880 RID: 22656 RVA: 0x000A485C File Offset: 0x000A2A5C
			public object Data { get; private set; }

			// Token: 0x06005881 RID: 22657 RVA: 0x000A4865 File Offset: 0x000A2A65
			public Allocation(object data)
			{
				this.Data = data;
			}
		}

		// Token: 0x02000B2F RID: 2863
		private class ArrayAllocation : Helper.Allocation
		{
			// Token: 0x17000EFD RID: 3837
			// (get) Token: 0x06005882 RID: 22658 RVA: 0x000A4874 File Offset: 0x000A2A74
			// (set) Token: 0x06005883 RID: 22659 RVA: 0x000A487C File Offset: 0x000A2A7C
			public bool IsElementAllocated { get; private set; }

			// Token: 0x06005884 RID: 22660 RVA: 0x000A4885 File Offset: 0x000A2A85
			public ArrayAllocation(object data, bool isElementAllocated)
				: base(data)
			{
				this.IsElementAllocated = isElementAllocated;
			}
		}

		// Token: 0x02000B30 RID: 2864
		private class DelegateHolder
		{
			// Token: 0x17000EFE RID: 3838
			// (get) Token: 0x06005885 RID: 22661 RVA: 0x000A4895 File Offset: 0x000A2A95
			// (set) Token: 0x06005886 RID: 22662 RVA: 0x000A489D File Offset: 0x000A2A9D
			public Delegate Public { get; private set; }

			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x06005887 RID: 22663 RVA: 0x000A48A6 File Offset: 0x000A2AA6
			// (set) Token: 0x06005888 RID: 22664 RVA: 0x000A48AE File Offset: 0x000A2AAE
			public Delegate Private { get; private set; }

			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x06005889 RID: 22665 RVA: 0x000A48B7 File Offset: 0x000A2AB7
			// (set) Token: 0x0600588A RID: 22666 RVA: 0x000A48BF File Offset: 0x000A2ABF
			public Delegate[] Additional { get; private set; }

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x0600588B RID: 22667 RVA: 0x000A48C8 File Offset: 0x000A2AC8
			// (set) Token: 0x0600588C RID: 22668 RVA: 0x000A48D0 File Offset: 0x000A2AD0
			public ulong? NotificationId { get; set; }

			// Token: 0x0600588D RID: 22669 RVA: 0x000A48D9 File Offset: 0x000A2AD9
			public DelegateHolder(Delegate publicDelegate, Delegate privateDelegate, params Delegate[] additionalDelegates)
			{
				this.Public = publicDelegate;
				this.Private = privateDelegate;
				this.Additional = additionalDelegates;
			}
		}
	}
}
