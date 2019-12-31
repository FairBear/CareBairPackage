using ConfigScene;
using UnityEngine;
using Manager;
using UnityEngine.Rendering.PostProcessing;

namespace CareBairPackage
{
	public static partial class ExtendedGraphics
	{
		const float MARGIN_TOP = 20f;
		const float MARGIN_BOTTOM = 10f;
		const float MARGIN_LEFT = 10f;
		const float MARGIN_RIGHT = 10f;
		const float WIDTH = 400f;
		const float HEIGHT = 400f;
		const float INNER_WIDTH = WIDTH - MARGIN_LEFT - MARGIN_RIGHT;
		const float INNER_HEIGHT = HEIGHT - MARGIN_TOP - MARGIN_BOTTOM;

		const float LABEL_WIDTH = 200f;

		static Rect rect = new Rect(Screen.width - WIDTH, (Screen.height - HEIGHT) / 2, WIDTH, HEIGHT);
		static Rect innerRect = new Rect(MARGIN_LEFT, MARGIN_TOP, INNER_WIDTH, INNER_HEIGHT);
		static Rect dragRect = new Rect(0f, 0f, WIDTH, 20f);
		static Vector2 scroll = new Vector2();

		static void OnGUI()
		{
			if (toggle)
				rect = Window.Draw(WindowID.Value, rect, Draw, "Extended Graphics");
			else
				Draw_Settings(false);
		}

		static void Draw(int id)
		{
			GUI.DragWindow(dragRect);
			GUILayout.BeginArea(innerRect);
			{
				GUILayout.BeginVertical();
				{
					scroll = GUILayout.BeginScrollView(scroll);
					{
						Field("No Fade Animations", true);
						Draw_Settings();
					}
					GUILayout.EndScrollView();

					Draw_Footer();
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();
		}

		static void Draw_Footer()
		{
			GUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Save"))
					Save();

				if (GUILayout.Button("Load"))
					Load();
			}
			GUILayout.EndHorizontal();
		}

		static void Draw_Settings(bool draw = true)
		{
			GraphicSystem data = Config.GraphicData;

			if (effector != null)
			{
				Draw_Settings_Bloom(data, draw);
				Draw_Settings_SSAO(data, draw);
				Draw_Settings_SSR(data, draw);
				Draw_Settings_DepthOfField(data, draw);
				Draw_Settings_Atmospheric(data, draw);
			}
		}

		static void Draw_Settings_Atmospheric(GraphicSystem data, bool draw = true)
		{
			if (!data.Atmospheric || Map.Instance.Simulator == null)
				return;

			EnviroSky sky = Map.Instance.Simulator.EnviroSky;
			EnviroFogSettings fog = sky.fogSettings;
			EnviroLightshafts light = sky.LightShafts;

			fog.distanceFog = Field("Distance Fog", fog.distanceFog, draw);
			fog.heightFog = Field("Height Fog", fog.heightFog, draw);
			sky.volumeLighting = Field("Volume Lighting", sky.volumeLighting, draw);
			light.sunLightShafts = Field("Sun Light Shafts", light.sunLightShafts, draw);
			light.moonLightShafts = Field("Moon Light Shafts", light.moonLightShafts, draw);
		}

		static void Draw_Settings_Bloom(GraphicSystem data, bool draw = true)
		{
			if (!data.Bloom)
				return;

			for (int i = 0; i < _bloom.Count; i++)
			{
				Bloom obj = _bloom[i];
				string name = $"[Bloom{i}] ";

				obj.enabled.value = Field($"{name}Enabled", obj.enabled.value, draw);
				obj.fastMode.value = Field($"{name}Fast Mode", obj.fastMode.value, draw);
				obj.anamorphicRatio.value = Field($"{name}Anamorphic Ratio", obj.anamorphicRatio.value, draw);
				obj.clamp.value = Field($"{name}Clamp", obj.clamp.value, draw);
				obj.diffusion.value = Field($"{name}Diffusion", obj.diffusion.value, draw);
				obj.dirtIntensity.value = Field($"{name}Dirt Intensity", obj.dirtIntensity.value, draw);
				obj.intensity.value = Field($"{name}Intensity", obj.intensity.value, draw);
				obj.softKnee.value = Field($"{name}Soft Knee", obj.softKnee.value, draw);
				obj.threshold.value = Field($"{name}Threshold", obj.threshold.value, draw);
			}
		}

		static void Draw_Settings_SSAO(GraphicSystem data, bool draw = true)
		{
			if (!data.SSAO)
				return;

			for (int i = 0; i < _ao.Count; i++)
			{
				AmbientOcclusion obj = _ao[i];
				string name = $"[Amb.Occ.{i}] ";

				obj.enabled.value = Field($"{name}Enabled", obj.enabled.value, draw);
				obj.ambientOnly.value = Field($"{name}Ambient Only", obj.ambientOnly.value, draw);
				obj.blurTolerance.value = Field($"{name}Blur Tolerance", obj.blurTolerance.value, draw);
				obj.directLightingStrength.value = Field($"{name}Direct Lighting Strength", obj.directLightingStrength.value, draw);
				obj.intensity.value = Field($"{name}Intensity", obj.intensity.value, draw);
				obj.noiseFilterTolerance.value = Field($"{name}Noise Filter Tolerance", obj.noiseFilterTolerance.value, draw);
				obj.radius.value = Field($"{name}Radius", obj.radius.value, draw);
				obj.thicknessModifier.value = Field($"{name}Thickness Modifier", obj.thicknessModifier, draw);
				obj.upsampleTolerance.value = Field($"{name}Upsample Tolerance", obj.upsampleTolerance, draw);
				obj.mode.value = Field($"{name}Mode", obj.mode.value, draw);
				obj.quality.value = Field($"{name}Quality", obj.quality.value, draw);
			}
		}

		static void Draw_Settings_SSR(GraphicSystem data, bool draw = true)
		{
			if (!data.SSR)
				return;

			for (int i = 0; i < _ssr.Count; i++)
			{
				ScreenSpaceReflections obj = _ssr[i];
				string name = $"[SSR{i}] ";

				obj.enabled.value = Field($"{name}Enabled", obj.enabled.value, draw);
				obj.distanceFade.value = Field($"{name}Distance Fade", obj.distanceFade.value, draw);
				obj.maximumIterationCount.value = Field($"{name}Max Iteration Count", obj.maximumIterationCount, draw);
				obj.maximumMarchDistance.value = Field($"{name}Max March Distance", obj.maximumMarchDistance.value, draw);
				obj.thickness.value = Field($"{name}Thickness", obj.thickness.value, draw);
				obj.vignette.value = Field($"{name}Vignette", obj.vignette.value, draw);
				obj.resolution.value = Field($"{name}Resolution", obj.resolution.value, draw);
				obj.preset.value = Field($"{name}Preset", obj.preset.value, draw);
			}
		}

		static void Draw_Settings_DepthOfField(GraphicSystem data, bool draw = true)
		{
			if (!data.DepthOfField)
				return;

			for (int i = 0; i < _dof.Count; i++)
			{
				DepthOfField obj = _dof[i];
				string name = $"[DoF{i}] ";

				obj.enabled.value = Field($"{name}Enabled", obj.enabled.value, draw);
				obj.aperture.value = Field($"{name}Aperture", obj.aperture.value, draw);
				obj.focalLength.value = Field($"{name}Focal Length", obj.focalLength.value, draw);
				obj.focusDistance.value = Field($"{name}Focus Distance", obj.focusDistance.value, draw);
				obj.kernelSize.value = Field($"{name}Kernel Size", obj.kernelSize.value, draw);
			}
		}

		static bool Field(string label, bool val, bool draw = true)
		{
			if (!boolState.ContainsKey(label))
				boolState[label] = val;

			if (!draw)
				return boolState[label];

			return boolState[label] = GUILayout.Toggle(boolState[label], label);
		}

		static int Field(string label, int val, int min, int max, string postfix = null, bool draw = true)
		{
			if (!numState.ContainsKey(label))
				numState[label] = val;

			if (!draw)
				return (int)numState[label];

			int result;

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label(
					label + $" [{postfix ?? numState[label].ToString()}]",
					GUILayout.Width(LABEL_WIDTH)
				);

				numState[label] = result = (int)GUILayout.HorizontalSlider(val, min, max);
			}
			GUILayout.EndHorizontal();

			return result;
		}

		static int Field(string label, int val, bool draw = true)
		{
			int result;

			if (!stringState.ContainsKey(label))
				stringState[label] = val.ToString();

			if (!draw)
				return int.TryParse(stringState[label], out result) ? result : val;

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label(label, GUILayout.Width(LABEL_WIDTH));

				string input = stringState[label] = GUILayout.TextField(stringState[label]);

				if (!int.TryParse(input, out result))
					result = val;
			}
			GUILayout.EndHorizontal();

			return result;
		}

		static float Field(string label, float val, bool draw = true)
		{
			float result;

			if (!stringState.ContainsKey(label))
				stringState[label] = val.ToString();

			if (!draw)
				return float.TryParse(stringState[label], out result) ? result : val;

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label(label, GUILayout.Width(LABEL_WIDTH));

				string input = stringState[label] = GUILayout.TextField(stringState[label]);

				if (!float.TryParse(input, out result))
					result = val;
			}
			GUILayout.EndHorizontal();

			return result;
		}

		static AmbientOcclusionMode Field(string label, AmbientOcclusionMode val, bool draw = true)
		{
			return (AmbientOcclusionMode)Field(label, (int)val, 0, 1, val.ToString(), draw);
		}

		static AmbientOcclusionQuality Field(string label, AmbientOcclusionQuality val, bool draw = true)
		{
			return (AmbientOcclusionQuality)Field(label, (int)val, 0, 4, val.ToString(), draw);
		}

		static ScreenSpaceReflectionPreset Field(string label,
												 ScreenSpaceReflectionPreset val,
												 bool draw = true)
		{
			return (ScreenSpaceReflectionPreset)Field(label, (int)val, 0, 7, val.ToString(), draw);
		}

		static ScreenSpaceReflectionResolution Field(string label,
													 ScreenSpaceReflectionResolution val,
													 bool draw = true)
		{
			return (ScreenSpaceReflectionResolution)Field(label, (int)val, 0, 2, val.ToString(), draw);
		}

		static KernelSize Field(string label, KernelSize val, bool draw = true)
		{
			return (KernelSize)Field(label, (int)val, 0, 3, val.ToString(), draw);
		}
	}
}
