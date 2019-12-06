using HarmonyLib;
using System;
using System.IO;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		static ProfileEntry Entry(object target,
								  string label,
								  string property,
								  string key,
								  Type type,
								  Action act = null)
		{
			return entries[key] = new ProfileEntry(
				target,
				label,
				property,
				key,
				new Traverse(target).Field(property).GetValue(),
				type,
				act
			);
		}

		static void Batch<T>(object target, string prefix, params string[] properties)
		{
			foreach (string property in properties)
				Entry(
					target,
					$"{prefix}\n{property}",
					property,
					$"{prefix} {property}",
					typeof(T)
				).Invoke();
		}

		static void Struct<T>(object target, Action act, string prefix, params string[] properties)
		{
			foreach (string property in properties)
				Entry(
					target,
					$"{prefix}\n{property}",
					property,
					$"{prefix} {property}",
					typeof(T),
					act
				);

			act();
		}

		static void SetValue(object target, string property, object value)
		{
			new Traverse(target).Field(property).SetValue(value);
		}
	}
}
