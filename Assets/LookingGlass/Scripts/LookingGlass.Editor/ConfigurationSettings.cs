﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LookingGlass.Editor {
    [InitializeOnLoad]
    public class ConfigurationSettings : EditorWindow {

        class setting
        {
            public string label;
            public bool on;
            public bool isQualitySetting;
            public Action settingChange;
            public setting(string label, bool on, bool isQualitySetting, Action settingChange)
            {
                this.label = label;
                this.on = on;
                this.isQualitySetting = isQualitySetting;
                this.settingChange = settingChange;
            }
        }
        List<setting> settings = new List<setting> {
            new setting("Shadow Distance: 5000", true, true,
                () => { QualitySettings.shadowDistance = 5000f; }
            ),
            new setting("Shadow Projection: Close Fit", true, true,
                () => { QualitySettings.shadowProjection = ShadowProjection.CloseFit; }
            ),
            new setting("Splash Screen: off (pro/plus only)", true, false,
                () => { PlayerSettings.SplashScreen.show = false; }
            ),
            new setting("Run in Background", true, false,
                () => { PlayerSettings.runInBackground = true; }
            ),
            // No display resolution dialog after 2019.3
            // On Windows, we use SetParams to implement auto display targetting so we don't need this to be enabled by default
            // Instead, we recommend disable it on Windows
#if !UNITY_2019_3_OR_NEWER

#if UNITY_EDITOR_WIN
            new setting("Resolution Dialog: disabled", true, false,
                () => { PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled; }
            ),
#elif UNITY_EDITOR_OSX
            new setting("Resolution Dialog: enabled", true, false,
                () => { PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Enabled; }
            ),
#endif
    
#endif
       
            new setting("Use Fullscreen Window", true, false,
#if UNITY_2018_1_OR_NEWER
                () => { PlayerSettings.fullScreenMode = FullScreenMode.FullScreenWindow; }
#else
                () => { PlayerSettings.defaultIsFullScreen = true; }
#endif
            ),
#if UNITY_EDITOR_WIN            
            new setting("Window Build: 64 bit", true, false, 
                ()=>{ EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);}
            ),
#endif
            new setting("Add customize size to Game view and default to Portrait size", true, false,
                AddAllCustomSizesAndSetDefault
            )
        };

        private static void AddAllCustomSizesAndSetDefault() {
            foreach (DeviceSettings settings in DeviceSettings.GetAll()) {
                string deviceName = settings.name;
                int width = settings.screenWidth;
                int height = settings.screenHeight;

                bool sizeExists = LookingGlass.GameViewExtensions.FindSize(width, height, deviceName, out int index);
                if (!sizeExists)
                    LookingGlass.GameViewExtensions.AddCustomSize(width, height, settings.name);
            }

            DeviceSettings defaultSettings = DeviceSettings.Get(DeviceType.Portrait);
            EditorWindow[] gameViews = LookingGlass.GameViewExtensions.FindAllGameViews();
            foreach (EditorWindow gameView in gameViews) {
                LookingGlass.GameViewExtensions.SetGameViewResolution(gameView, defaultSettings.screenWidth, defaultSettings.screenHeight, defaultSettings.name);
            }
        }

        const string editorPrefName = "LookingGlass_1.2.0_";

        static ConfigurationSettings()
        {
            EditorApplication.update += CheckIfPromptedYet;
        }

        //***********/
        //* methods */
        //***********/

        static void CheckIfPromptedYet()
        {
            if (!EditorPrefs.GetBool(editorPrefName + PlayerSettings.productName, false)) {
                Init();
            }
            EditorApplication.update -= CheckIfPromptedYet;
        }

        [MenuItem("LookingGlass/Setup Player Settings", false, 53)]
        static void Init() {
            ConfigurationSettings window = EditorWindow.GetWindow<ConfigurationSettings>();
            window.Show();
        }

        void OnEnable() {
            titleContent = new GUIContent("LookingGlass Settings");
            float spacing = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            Vector2 size = new Vector2(360, 130 + spacing * settings.Count);
            maxSize = size;
            minSize = size;
        }

        void OnGUI() {
            EditorGUILayout.HelpBox(
                "It is recommended you change the following project settings " +
                "to ensure the best performance for your LookingGlass application",
                MessageType.Warning
            );

            EditorGUILayout.LabelField("Select which options to change:", EditorStyles.miniLabel);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            foreach (var s in settings) {
                EditorGUILayout.BeginHorizontal();
                s.on = EditorGUILayout.ToggleLeft(s.label, s.on);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = EditorGUIUtility.isProSkin ? Color.green : Color.Lerp(Color.green, Color.white, 0.5f);
            if (GUILayout.Button("Apply Changes")) {
                var qs = QualitySettings.names;
                int currentQuality = QualitySettings.GetQualityLevel();

                for (int i = 0; i < qs.Length; i++) {
                    QualitySettings.SetQualityLevel(i, false);
                    foreach (var setting in settings) {
                        if (setting.isQualitySetting) {
                            setting.settingChange();
                        }
                    }
                }

                foreach (var setting in settings) {
                    if (!setting.isQualitySetting) {
                        setting.settingChange();
                    }
                }

                QualitySettings.SetQualityLevel(currentQuality, true);
                EditorPrefs.SetBool(editorPrefName + PlayerSettings.productName, true);
                Debug.Log("[LookingGlass] Applied! By default, this popup will no longer appear, but you can access it by clicking LookingGlass/Setup Player Settings");
                Close();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = EditorGUIUtility.isProSkin ? Color.yellow : Color.Lerp(Color.yellow, Color.white, 0.5f);

            if (GUILayout.Button("Never display this popup again")) {
                EditorPrefs.SetBool(editorPrefName + PlayerSettings.productName, true);
                Debug.Log("[LookingGlass] Player Settings popup hidden--" +
                    "to show again, open in inspector window on LookingGlass Capture");
                Close();
            }

            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }
    }
}