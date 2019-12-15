using System.Collections.Generic;
using System.IO;
using UnityEngine.Rendering.PostProcessing;
using ConfigScene;
using AIProject;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ExtendedGraphics
	{
		const string SAVENAME = "CBP_ExtendedGraphics.csv";

		static List<Bloom> _bloom;
		static List<AmbientOcclusion> _ao;
		static List<ScreenSpaceReflections> _ssr;
		static List<DepthOfField> _dof;

		static readonly Dictionary<string, bool> boolState = new Dictionary<string, bool>();
		static readonly Dictionary<string, float> numState = new Dictionary<string, float>();
		static readonly Dictionary<string, string> stringState = new Dictionary<string, string>();
		static bool toggle = false;
		static string csv;

		static string SavePath => Path.Combine(Directory.GetCurrentDirectory(), "UserData", SAVENAME);

		static void Update()
		{
			if (Key.Value.IsDown())
				toggle = !toggle;

			ConfigEffector effector = Object.FindObjectOfType<ConfigEffector>();
			bool hasEffector = effector?.PostProcessLayer != null;

			if (!hasEffector)
			{
				_bloom = null;
				_ao = null;
				_ssr = null;
				_dof = null;
			}
			else if (_bloom == null || _ao == null || _ssr == null || _dof == null)
			{
				_bloom = new List<Bloom>();
				_ao = new List<AmbientOcclusion>();
				_ssr = new List<ScreenSpaceReflections>();
				_dof = new List<DepthOfField>();

				List<PostProcessVolume> list = ListPool<PostProcessVolume>.Get();
				PostProcessManager.instance.GetActiveVolumes(effector.PostProcessLayer, list, true, true);

				foreach (PostProcessVolume ppv in list)
				{
					Bloom bloom = ppv.profile.GetSetting<Bloom>();

					if (bloom)
						_bloom.Add(bloom);

					AmbientOcclusion ao = ppv.profile.GetSetting<AmbientOcclusion>();

					if (ao)
						_ao.Add(ao);

					ScreenSpaceReflections ssr = ppv.profile.GetSetting<ScreenSpaceReflections>();

					if (ssr)
						_ssr.Add(ssr);

					DepthOfField dof = ppv.profile.GetSetting<DepthOfField>();

					if (dof)
						_dof.Add(dof);
				}
			}
		}
	}
}
