using AIProject;
using System;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class EnvironmentControl
	{
		const float MARGIN_TOP = 20f;
		const float MARGIN_BOTTOM = 10f;
		const float MARGIN_LEFT = 10f;
		const float MARGIN_RIGHT = 10f;
		const float WIDTH = 360f;
		const float HEIGHT = 260f;
		const float INNER_WIDTH = WIDTH - MARGIN_LEFT - MARGIN_RIGHT;
		const float INNER_HEIGHT = HEIGHT - MARGIN_TOP - MARGIN_BOTTOM;

		static Rect rect = new Rect(Screen.width - WIDTH, (Screen.height - HEIGHT) / 2, WIDTH, HEIGHT);
		static Rect innerRect = new Rect(MARGIN_LEFT, MARGIN_TOP, INNER_WIDTH, INNER_HEIGHT);
		static Rect dragRect = new Rect(0f, 0f, WIDTH, 20f);

		static GUIStyle selectedButtonStyle = null;
		static GUIStyle headerStyle = null;

		static string
			temp_temperature = "",
			temp_year = "",
			temp_day = "",
			temp_hour = "",
			temp_minute = "",
			temp_second = "";

		static void OnGUI()
		{
			if (!visible)
				return;

			if (selectedButtonStyle == null)
			{
				selectedButtonStyle = new GUIStyle(GUI.skin.button)
				{
					fontStyle = FontStyle.Bold,
					normal = {
						textColor = Color.red
					},
					focused = {
						textColor = Color.red
					},
					active = {
						textColor = Color.red
					},
					hover = {
						textColor = Color.red
					}
				};

				headerStyle = new GUIStyle(GUI.skin.label)
				{
					fontStyle = FontStyle.Bold
				};
			}

			rect = Window.Draw(WindowID.Value, rect, Draw, "Environment Control");
		}

		static void Draw(int id)
		{
			GUI.DragWindow(dragRect);
			GUILayout.BeginArea(innerRect);
			{
				EnvironmentSimulator sim = Manager.Map.Instance.Simulator;

				GUILayout.BeginVertical();
				{
					Draw_Weather(sim);
					Draw_Temperature(sim);
					Draw_Time(sim);
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();

			shouldUpdate = false;
		}

		static void Draw_Weather(EnvironmentSimulator sim)
		{
			Weather currWeather = sim.Weather;
			Weather[] list = (Weather[])Enum.GetValues(typeof(Weather));
			int half = list.Length / 2;

			GUILayout.Label("Weather", headerStyle);

			GUILayout.BeginHorizontal();
			for (int i = 0; i < list.Length; i++)
			{
				Weather weather = list[i];

				if (i % half == 0)
				{
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}

				if (GUILayout.Button(
						$"{weather.ToString()}",
						currWeather == weather ?
							selectedButtonStyle :
							GUI.skin.button,
						GUILayout.Width(80f)
					))
				{
					sim.RefreshWeather(weather);
					break;
				}
			}
			GUILayout.EndHorizontal();
		}

		static void Draw_Temperature(EnvironmentSimulator sim)
		{
			GUILayout.Label("Temperature", headerStyle);

			if (shouldUpdate)
				temp_temperature = ((int)sim.TemperatureValue).ToString();

			GUILayout.BeginHorizontal();
			{
				bool flag = int.TryParse(temp_temperature = GUILayout.TextField(temp_temperature), out int val);

				if (GUILayout.Button("Update", GUILayout.Width(80f)) && flag)
				{
					sim.TemperatureValue = val;
					temp_temperature = ((int)sim.TemperatureValue).ToString();
				}
			}
			GUILayout.EndHorizontal();
		}

		static string Draw_Time_Field(string label, string input, out int val, out bool flag)
		{
			GUILayout.Label(label, GUILayout.Width(50f));

			flag = int.TryParse(input = GUILayout.TextField(input, GUILayout.Width(50f)), out val);

			return input;
		}

		static void Draw_Time(EnvironmentSimulator sim)
		{
			EnviroTime time = sim.EnviroSky.GameTime;

			bool flag0, flag1, flag2, flag3, flag4;
			int year, day, hour, minute, second;

			if (shouldUpdate)
			{
				temp_year = time.Years.ToString();
				temp_day = time.Days.ToString();
				temp_hour = time.Hours.ToString();
				temp_minute = time.Minutes.ToString();
				temp_second = time.Seconds.ToString();
			}

			GUILayout.Label("Date & Time", headerStyle);

			GUILayout.BeginHorizontal();
			{
				temp_year = Draw_Time_Field("Year", temp_year, out year, out flag0);
				temp_day = Draw_Time_Field("Day", temp_day, out day, out flag1);
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			{
				temp_hour = Draw_Time_Field("Hour", temp_hour, out hour, out flag2);
				temp_minute = Draw_Time_Field("Minute", temp_minute, out minute, out flag3);
				temp_second = Draw_Time_Field("Second", temp_second, out second, out flag4);
			}
			GUILayout.EndHorizontal();

			if (GUILayout.Button("Update Date & Time") && flag0 && flag1 && flag2 && flag3 && flag4)
			{
				sim.EnviroSky.SetTime(year, day, hour, minute, second);

				temp_year = time.Years.ToString();
				temp_day = time.Days.ToString();
				temp_hour = time.Hours.ToString();
				temp_minute = time.Minutes.ToString();
				temp_second = time.Seconds.ToString();
			}
		}
	}
}
