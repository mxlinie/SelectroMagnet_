sampler2D _MainTex, _SpecularTex;
half4 _MainTex_ST;
half4 _AlbedoColor, _SpecularColor;
float _Cutoff;

#if defined(_ALPHATEST_ON) || defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
	#define _ALPHA_ENABLED 1
#endif
#if (defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)) && defined(UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS)
    #define _SHADOWS_SEMITRANSPARENT 1
sampler3D _DitherMaskLOD;
#endif

half SpecularSetup_ShadowGetOneMinusReflectivity(half2 uv)
{
    half3 specular = tex2D(_SpecularTex, uv).rgb * _SpecularColor;
    return (1 - SpecularStrength(specular));
}

#define SHADOW_JOIN2(a, b) a##b
#define SHADOW_JOIN(a, b) SHADOW_JOIN2(a,b)
#define SHADOW_ONEMINUSREFLECTIVITY SHADOW_JOIN(UNITY_SETUP_BRDF_INPUT, _ShadowGetOneMinusReflectivity)

struct ToonShadowInput
{
    float4 vertex : POSITION;
	float3 normal : NORMAL;
    float2 texcoord0 : TEXCOORD0;
};

struct ToonShadowOutput
{
    V2F_SHADOW_CASTER_NOPOS
    float2 uv0 : TEXCOORD0;
};

ToonShadowOutput toonShadowVert(ToonShadowInput v, out float4 pos : SV_POSITION)
{
    ToonShadowOutput o;
    pos = UnityApplyLinearShadowBias(UnityClipSpaceShadowCasterPos(v.vertex.xyz, v.normal));
    o.uv0 = TRANSFORM_TEX(v.texcoord0, _MainTex);
	TRANSFER_SHADOW_CASTER_NOPOS(o, pos)
    return o;
}

half4 toonShadowFrag(ToonShadowOutput i
#ifdef _SHADOWS_SEMITRANSPARENT
, UNITY_VPOS_TYPE vpos : VPOS
#endif
) : SV_Target
{
#ifdef _ALPHA_ENABLED
    half alpha = tex2D(_MainTex, TRANSFORM_TEX(i.uv0, _MainTex)).a * _AlbedoColor.a;
	#ifdef _ALPHATEST_ON
		clip(alpha - _Cutoff);
	#endif
    #if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
        #ifdef _ALPHAPREMULTIPLY_ON
            half outModifiedAlpha;
            PreMultiplyAlpha(half3(0, 0, 0), alpha, SHADOW_ONEMINUSREFLECTIVITY(i.uv0), outModifiedAlpha);
            alpha = outModifiedAlpha;
        #endif
	    #ifdef _SHADOWS_SEMITRANSPARENT
            float dither = tex3D(_DitherMaskLOD, float3(vpos.xy * 0.25, alpha * 0.9375)).a;
            clip(dither - 0.01);
        #else
            clip(alpha - _Cutoff);
        #endif
    #endif
#endif
    SHADOW_CASTER_FRAGMENT(i);
}