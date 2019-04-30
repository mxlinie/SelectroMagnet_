Shader "ToonSketch/Standard"
{
	Properties
	{
		_MainTex ("Albedo Texture", 2D) = "white" {}
		_AlbedoColor ("Albedo Color", Color) = (1, 1, 1, 1)
		_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
		[NoScaleOffset] _RampTex ("Ramp Texture", 2D) = "white" {}
		[NoScaleOffset] _BumpMap ("Normal Texture", 2D) = "bump" {}
		_BumpScale ("Normal Height", Float) = 1
		[Toggle(_SPECULAR_ON)] _Specular ("Specular Lighting?", Float) = 1
		[NoScaleOffset] _SpecularTex ("Specular Texture", 2D) = "black" {}
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_Smoothness ("Smoothness", Range(0, 1)) = 0
		[Toggle(_RIM_LIGHT_ON)] _RimLighting ("Rim Lighting?", Float) = 1
		_RimColor ("Rim Color", Color) = (0.25, 0.2, 0.15, 0.8)
      	_RimPower ("Rim Power", Range(0.5, 8.0)) = 5
		[Toggle(_HATCH_ON)] _HatchShading ("Hatch Shading?", Float) = 0
		[NoScaleOffset] _Hatch0("Hatch0", 2D) = "white" {}
		[NoScaleOffset] _Hatch1("Hatch1", 2D) = "white" {}
		[NoScaleOffset] _Hatch2("Hatch2", 2D) = "white" {}
		[NoScaleOffset] _Hatch3("Hatch3", 2D) = "white" {}
		[NoScaleOffset] _Hatch4("Hatch4", 2D) = "white" {}
		[NoScaleOffset] _Hatch5("Hatch5", 2D) = "white" {}
		_HatchScale ("Hatch Scale", Range(0, 20)) = 10
		_HatchThreshold ("Hatch Weight", Range(0, 1)) = 0.5
		_HatchStrength ("Hatch Strength", Range(0, 2)) = 0.8

		[HideInInspector] _RampStyle("__rampstyle", int) = 0
        [HideInInspector] _CullMode("__cullmode", int) = 2
        [HideInInspector] _Cull("__cull", int) = 2
		[HideInInspector] _BlendMode ("__blendmode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _ZWrite ("__zw", Float) = 1
	}

	CGINCLUDE
		#define UNITY_SETUP_BRDF_INPUT SpecularSetup
	ENDCG

	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		
		LOD 100

		Blend [_SrcBlend] [_DstBlend]
		ZWrite [_ZWrite]
		Cull [_Cull]
		
		CGPROGRAM
		#pragma target 3.0

		#include "UnityPBSLighting.cginc"

		#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
		#pragma shader_feature _SPECULAR_ON
		#pragma shader_feature _RIM_LIGHT_ON
		#pragma shader_feature _HATCH_ON

		#pragma surface surf ToonSketch fullforwardshadows keepalpha

		#include "Toon.cginc"
		ENDCG
		
		Pass
        {
            Name "Shadows"
			Tags
            {
				"LightMode" = "ShadowCaster"
			}
			
			ZWrite On
			ZTest LEqual

			CGPROGRAM
			#pragma target 3.0

			#include "UnityPBSLighting.cginc"

			#pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
			#pragma multi_compile_shadowcaster
			#pragma multi_compile_instancing

            #pragma vertex toonShadowVert
            #pragma fragment toonShadowFrag

			#include "Shadows.cginc"
			ENDCG
		}
	}
	FallBack "VertexLit"
	CustomEditor "ToonSketchToonShaderGUI"
}