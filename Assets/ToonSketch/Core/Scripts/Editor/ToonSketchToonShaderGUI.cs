using System;
using UnityEngine;
using UnityEditor;

public class ToonSketchToonShaderGUI : ShaderGUI
{
    public enum RampStyle
    {
        Soft,
        Hard
    }

    public enum BlendMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }

    public enum CullMode
    {
        Off,
        Front,
        Back
    }

    private static class Styles
    {
        public static string titleHeadingText = "Toon Shader with Sketch Shadows";
        public static string mainHeadingText = "Main Settings";
        public static string secondaryHeadingText = "Surface Settings";
        public static string shadingHeadingText = "Shading Settings";
        public static string advancedHeadingText = "Advanced Settings";
        public static string rampStyleText = "Ramp Style";
        public static string blendModeText = "Blend Mode";
        public static string cullModeText = "Cull Mode";
        public static GUIContent albedoText = new GUIContent("Albedo", "Albedo (RGB) and Transparency (A)");
        public static GUIContent alphaCutoffText = new GUIContent("Alpha Cutoff", "Threshold for alpha cutoff");
        public static GUIContent rampText = new GUIContent("Ramp", "Ramp (RGB)");
        public static GUIContent normalText = new GUIContent("Normal Map", "Normal Map");
        public static GUIContent normalHeightText = new GUIContent("Normal Height", "Normal Height");
        public static GUIContent specularEnableText = new GUIContent("Specular Highlights?", "Enable specular highlights?");
        public static GUIContent specularText = new GUIContent("Specular", "Specular (RGB)");
        public static GUIContent smoothnessText = new GUIContent("Smoothness", "Surface smoothness for specular highlights");
        public static GUIContent rimEnableText = new GUIContent("Rim Lighting?", "Enable rim lighting?");
        public static GUIContent rimColorText = new GUIContent("Rim Color", "Rim Color (RGB)");
        public static GUIContent rimPowerText = new GUIContent("Rim Power", "Rim Power");
        public static GUIContent hatchingText = new GUIContent("Hatch Shading?", "Enable the hatch shading effect?");
        public static GUIContent hatchScaleText = new GUIContent("Hatch Scale", "Hatch line scaling");
        public static GUIContent hatch0Text = new GUIContent("Brightest Hatching", "Hatching texture for given brightness (RGB)");
        public static GUIContent hatch1Text = new GUIContent("Brighter Hatching", "Hatching texture for given brightness (RGB)");
        public static GUIContent hatch2Text = new GUIContent("Bright Hatching", "Hatching texture for given brightness (RGB)");
        public static GUIContent hatch3Text = new GUIContent("Dark Hatching", "Hatching texture for given brightness (RGB)");
        public static GUIContent hatch4Text = new GUIContent("Darker Hatching", "Hatching texture for given brightness (RGB)");
        public static GUIContent hatch5Text = new GUIContent("Darkest Hatching", "Hatching texture for given brightness (RGB)");
        public static GUIContent hatchThresholdText = new GUIContent("Hatch Threshold", "Threshold for weighting luminance values");
        public static GUIContent hatchStrengthText = new GUIContent("Hatch Strength", "Strength of the hatching lines' appearance");
        public static readonly string[] rampNames = Enum.GetNames(typeof(RampStyle));
        public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));
        public static readonly string[] cullNames = Enum.GetNames(typeof(CullMode));
    }

    MaterialProperty rampStyle = null;
    MaterialProperty blendMode = null;
    MaterialProperty cullMode = null;
    MaterialProperty albedoTexture = null;
    MaterialProperty albedoColor = null;
    MaterialProperty alphaCutoff = null;
    MaterialProperty rampTexture = null;
    MaterialProperty normalTexture = null;
    MaterialProperty normalHeight = null;
    MaterialProperty specularLighting = null;
    MaterialProperty specularTexture = null;
    MaterialProperty specularColor = null;
    MaterialProperty smoothness = null;
    MaterialProperty rimLighting = null;
    MaterialProperty rimColor = null;
    MaterialProperty rimPower = null;
    MaterialProperty hatchShading = null;
    MaterialProperty hatch0Texture = null;
    MaterialProperty hatch1Texture = null;
    MaterialProperty hatch2Texture = null;
    MaterialProperty hatch3Texture = null;
    MaterialProperty hatch4Texture = null;
    MaterialProperty hatch5Texture = null;
    MaterialProperty hatchScale = null;
    MaterialProperty hatchThreshold = null;
    MaterialProperty hatchStrength = null;

    MaterialEditor m_MaterialEditor;

    bool m_FirstTimeApply = true;

    public void FindProperties(MaterialProperty[] properties)
    {
        rampStyle = FindProperty("_RampStyle", properties);
        blendMode = FindProperty("_BlendMode", properties);
        cullMode = FindProperty("_CullMode", properties);
        albedoTexture = FindProperty("_MainTex", properties);
        albedoColor = FindProperty("_AlbedoColor", properties);
        alphaCutoff = FindProperty("_Cutoff", properties);
        rampTexture = FindProperty("_RampTex", properties);
        normalTexture = FindProperty("_BumpMap", properties);
        normalHeight = FindProperty("_BumpScale", properties);
        specularLighting = FindProperty("_Specular", properties);
        specularTexture = FindProperty("_SpecularTex", properties);
        specularColor = FindProperty("_SpecularColor", properties);
        smoothness = FindProperty("_Smoothness", properties);
        rimLighting = FindProperty("_RimLighting", properties);
        rimColor = FindProperty("_RimColor", properties);
        rimPower = FindProperty("_RimPower", properties);
        hatchShading = FindProperty("_HatchShading", properties);
        hatch0Texture = FindProperty("_Hatch0", properties);
        hatch1Texture = FindProperty("_Hatch1", properties);
        hatch2Texture = FindProperty("_Hatch2", properties);
        hatch3Texture = FindProperty("_Hatch3", properties);
        hatch4Texture = FindProperty("_Hatch4", properties);
        hatch5Texture = FindProperty("_Hatch5", properties);
        hatchScale = FindProperty("_HatchScale", properties);
        hatchThreshold = FindProperty("_HatchThreshold", properties);
        hatchStrength = FindProperty("_HatchStrength", properties);
    }

    private void GetHeading(int headingNum)
    {
        switch (headingNum)
        {
            case 0:
                ToonSketch.EditorUtils.Header(
                    Styles.titleHeadingText,
                    0
                );
                break;
            case 1:
                ToonSketch.EditorUtils.Section(
                    Styles.mainHeadingText,
                    4
                );
                break;
            case 2:
                ToonSketch.EditorUtils.Section(
                    Styles.secondaryHeadingText,
                    6
                );
                break;
            case 3:
                ToonSketch.EditorUtils.Section(
                    Styles.shadingHeadingText,
                    8
                );
                break;
            case 4:
                ToonSketch.EditorUtils.Section(
                    Styles.advancedHeadingText,
                    10
                );
                break;
        }
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties);
        m_MaterialEditor = materialEditor;
        Material material = materialEditor.target as Material;
        if (m_FirstTimeApply)
        {
            MaterialChanged(material);
            m_FirstTimeApply = false;
        }
        ShaderPropertiesGUI(material);
    }

    public void ShaderPropertiesGUI(Material material)
    {
        GetHeading(0);
        EditorGUIUtility.labelWidth = 0f;
        EditorGUI.BeginChangeCheck();
        {
            ModePopup();
            MainSettings(material);
            SecondarySettings(material);
            HatchSettings(material);
        }
        if (EditorGUI.EndChangeCheck())
        {
            foreach (var obj in blendMode.targets)
                MaterialChanged((Material)obj);
        }
        GetHeading(4);
        m_MaterialEditor.RenderQueueField();
        m_MaterialEditor.EnableInstancingField();
        m_MaterialEditor.DoubleSidedGIField();
    }

    public void ModePopup()
    {
        EditorGUI.showMixedValue = blendMode.hasMixedValue;
        var mode = (BlendMode)blendMode.floatValue;
        EditorGUI.BeginChangeCheck();
        {
            mode = (BlendMode)EditorGUILayout.Popup(Styles.blendModeText, (int)mode, Styles.blendNames);
        }
        if (EditorGUI.EndChangeCheck())
        {
            m_MaterialEditor.RegisterPropertyChangeUndo("Rendering Mode");
            blendMode.floatValue = (float)mode;
            foreach (var obj in blendMode.targets)
                MaterialChanged((Material)obj);
        }
        EditorGUI.showMixedValue = false;
        EditorGUI.showMixedValue = cullMode.hasMixedValue;
        var cull = (CullMode)cullMode.floatValue;
        EditorGUI.BeginChangeCheck();
        {
            cull = (CullMode)EditorGUILayout.Popup(Styles.cullModeText, (int)cull, Styles.cullNames);
        }
        if (EditorGUI.EndChangeCheck())
        {
            m_MaterialEditor.RegisterPropertyChangeUndo("Culling Mode");
            cullMode.floatValue = (float)cull;
            foreach (var obj in cullMode.targets)
                MaterialChanged((Material)obj);
        }
        EditorGUI.showMixedValue = false;
    }

    public void StylePopup()
    {
        EditorGUI.showMixedValue = rampStyle.hasMixedValue;
        var style = (RampStyle)rampStyle.floatValue;
        EditorGUI.BeginChangeCheck();
        {
            style = (RampStyle)EditorGUILayout.Popup(Styles.rampStyleText, (int)style, Styles.rampNames);
        }
        if (EditorGUI.EndChangeCheck())
        {
            m_MaterialEditor.RegisterPropertyChangeUndo("Ramp Style");
            rampStyle.floatValue = (float)style;
            foreach (var obj in rampStyle.targets)
                MaterialChanged((Material)obj);
        }
    }

    private void MainSettings(Material material)
    {
        GetHeading(1);
        // Albedo
        m_MaterialEditor.TexturePropertySingleLine(Styles.albedoText, albedoTexture, albedoColor);
        if (((BlendMode)material.GetFloat("_BlendMode") == BlendMode.Cutout))
        {
            m_MaterialEditor.ShaderProperty(alphaCutoff, Styles.alphaCutoffText, MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
        }
        // Ramp
        m_MaterialEditor.TexturePropertySingleLine(Styles.rampText, rampTexture);
        StylePopup();
        // Normal
        m_MaterialEditor.TexturePropertySingleLine(Styles.normalText, normalTexture, normalTexture.textureValue != null ? normalHeight : null);
        // Tiling + Offset
        EditorGUI.BeginChangeCheck();
        m_MaterialEditor.TextureScaleOffsetProperty(albedoTexture);
    }

    private void SecondarySettings(Material material)
    {
        GetHeading(2);
        // Specular
        m_MaterialEditor.ShaderProperty(specularLighting, Styles.specularEnableText);
        EditorGUILayout.Space();
        if (specularLighting != null && specularLighting.floatValue == 1)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.specularText, specularTexture, specularColor);
            m_MaterialEditor.ShaderProperty(smoothness, Styles.smoothnessText, MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
        }
        EditorGUILayout.Space();
        // Rim Lighting
        m_MaterialEditor.ShaderProperty(rimLighting, Styles.rimEnableText);
        EditorGUILayout.Space();
        if (rimLighting != null && rimLighting.floatValue == 1)
        {
            m_MaterialEditor.ShaderProperty(rimColor, Styles.rimColorText);
            m_MaterialEditor.ShaderProperty(rimPower, Styles.rimPowerText);
        }
    }

    private void HatchSettings(Material material)
    {
        GetHeading(3);
        // Hatch
        m_MaterialEditor.ShaderProperty(hatchShading, Styles.hatchingText);
        EditorGUILayout.Space();
        if (hatchShading != null && hatchShading.floatValue == 1)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.hatch0Text, hatch0Texture);
            m_MaterialEditor.TexturePropertySingleLine(Styles.hatch1Text, hatch1Texture);
            m_MaterialEditor.TexturePropertySingleLine(Styles.hatch2Text, hatch2Texture);
            m_MaterialEditor.TexturePropertySingleLine(Styles.hatch3Text, hatch3Texture);
            m_MaterialEditor.TexturePropertySingleLine(Styles.hatch4Text, hatch4Texture);
            m_MaterialEditor.TexturePropertySingleLine(Styles.hatch5Text, hatch5Texture);
            m_MaterialEditor.ShaderProperty(hatchScale, Styles.hatchScaleText);
            m_MaterialEditor.ShaderProperty(hatchThreshold, Styles.hatchThresholdText);
            m_MaterialEditor.ShaderProperty(hatchStrength, Styles.hatchStrengthText);
        }
    }

    public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
    {
        base.AssignNewShaderToMaterial(material, oldShader, newShader);
        if (oldShader == null || !oldShader.name.Contains("Legacy Shaders/"))
        {
            SetupMaterialWithBlendMode(material, (BlendMode)material.GetFloat("_BlendMode"));
            return;
        }
        BlendMode blendMode = BlendMode.Opaque;
        if (oldShader.name.Contains("/Transparent/Cutout/"))
        {
            blendMode = BlendMode.Cutout;
        }
        else if (oldShader.name.Contains("/Transparent/"))
        {
            blendMode = BlendMode.Fade;
        }
        material.SetFloat("_BlendMode", (float)blendMode);
        MaterialChanged(material);
    }

    public static void MaterialChanged(Material material)
    {
        SetupMaterialWithRampStyle(material, (RampStyle)material.GetFloat("_RampStyle"));
        SetupMaterialWithBlendMode(material, (BlendMode)material.GetFloat("_BlendMode"));
        SetupMaterialWithCullMode(material, (CullMode)material.GetFloat("_CullMode"));
    }
    
    public static void SetupMaterialWithRampStyle(Material material, RampStyle rampStyle)
    {
        switch (rampStyle)
        {
            case RampStyle.Soft:
                material.SetInt("_RampStyle", 0);
                break;
            case RampStyle.Hard:
                material.SetInt("_RampStyle", 1);
                break;
        }
    }

    public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                material.SetOverrideTag("RenderType", "");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                material.SetOverrideTag("RenderType", "TransparentCutout");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                break;
            case BlendMode.Fade:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
            case BlendMode.Transparent:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
        }
    }

    public static void SetupMaterialWithCullMode(Material material, CullMode cullMode)
    {
        switch (cullMode)
        {
            case CullMode.Off:
                material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                break;
            case CullMode.Front:
                material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Front);
                break;
            case CullMode.Back:
                material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
                break;
        }
    }
}