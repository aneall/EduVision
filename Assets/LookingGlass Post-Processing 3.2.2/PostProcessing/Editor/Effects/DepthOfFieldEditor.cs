using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace UnityEditor.Rendering.PostProcessing
{
    [PostProcessEditor(typeof(DepthOfField))]
    internal sealed class DepthOfFieldEditor : PostProcessEffectEditor<DepthOfField>
    {
        //CUSTOM:
        SerializedParameterOverride snapToFocalPlane;
        SerializedParameterOverride intensity;

        SerializedParameterOverride m_FocusDistance;
        SerializedParameterOverride m_Aperture;
        SerializedParameterOverride m_FocalLength;
        SerializedParameterOverride m_KernelSize;

        public override void OnEnable()
        {
            snapToFocalPlane = FindParameterOverride(x => x.snapToFocalPlane);
            intensity = FindParameterOverride(x => x.intensity);

            m_FocusDistance = FindParameterOverride(x => x.focusDistance);
            m_Aperture = FindParameterOverride(x => x.aperture);
            m_FocalLength = FindParameterOverride(x => x.focalLength);
            m_KernelSize = FindParameterOverride(x => x.kernelSize);
        }

        public override void OnInspectorGUI()
        {
            if (SystemInfo.graphicsShaderLevel < 35)
                EditorGUILayout.HelpBox("Depth Of Field is only supported on the following platforms:\nDX11+, OpenGL 3.2+, OpenGL ES 3+, Metal, Vulkan, PS4/XB1 consoles.", MessageType.Warning);

            PropertyField(snapToFocalPlane);
            bool isUsingSnapToFocalPlane = snapToFocalPlane.overrideState.boolValue && snapToFocalPlane.value.boolValue;

            using (new EditorGUI.DisabledScope(!isUsingSnapToFocalPlane)) {
                PropertyField(intensity);
            }
            using (new EditorGUI.DisabledScope(isUsingSnapToFocalPlane)) {
                PropertyField(m_FocusDistance);
                PropertyField(m_Aperture);
                PropertyField(m_FocalLength);
            }

            PropertyField(m_KernelSize);
        }
    }
}
