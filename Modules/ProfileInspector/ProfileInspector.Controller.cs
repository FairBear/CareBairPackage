using System.Collections.Generic;
using System.IO;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		const string SAVENAME = "CBP_ProfileInspector.csv";

		static Dictionary<string, ProfileEntry> entries;
		static Dictionary<string, string> save;
		static bool toggle = false;

		static string SavePath => Path.Combine(Directory.GetCurrentDirectory(), "UserData", SAVENAME);

		public static void Update()
		{
			if (Key.Value.IsDown())
				toggle = !toggle;
		}
	}
}
