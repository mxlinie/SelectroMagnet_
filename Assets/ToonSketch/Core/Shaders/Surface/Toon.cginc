sampler2D _MainTex, _BumpMap, _SpecularTex, _RampTex;
half4 _AlbedoColor, _SpecularColor, _RimColor;
float _Cutoff, _BumpScale, _Smoothness, _RimPower;
int _RampStyle;

#ifdef _HATCH_ON
sampler2D _Hatch0, _Hatch1, _Hatch2, _Hatch3, _Hatch4, _Hatch5;
float _HatchScale, _HatchThreshold, _HatchStrength;
#endif

struct Input
{
	float2 uv_MainTex;
    float3 viewDir;
    float3 worldPos;
    float3 worldNormal;
    INTERNAL_DATA
};

struct ToonSurfaceOutput
{
    float2 UV;
    fixed3 Albedo;
    fixed Alpha;
    fixed3 Normal;
    fixed3 Bump;
    fixed3 Specular;
    half Smoothness;
    fixed3 Emission;
    fixed3 Rim;
    fixed3 Ambient;
    half Attenuation;
};

void surf (Input i, inout ToonSurfaceOutput o) {
    // Albedo
    o.UV = i.uv_MainTex;
    half4 mainColor = tex2D(_MainTex, o.UV) * _AlbedoColor;
    o.Albedo = mainColor.rgb;
    o.Alpha = mainColor.a;
    // Normals
    o.Bump = UnpackScaleNormal(tex2D(_BumpMap, o.UV), _BumpScale);
#ifdef _SPECULAR_ON
    // Specular
    o.Specular = tex2D(_SpecularTex, o.UV).rgb * _SpecularColor;
    o.Smoothness = _Smoothness;
#endif
#ifdef _RIM_LIGHT_ON
    // Rim Light
    o.Rim = _RimColor;
#endif
#ifdef _ALPHATEST_ON
    // Cutout
    clip(o.Alpha - _Cutoff);
#endif
}

struct ToonSketchLight
{
    half3 color;
    float diffuse;
    float specular;
};

ToonSketchLight ToonSketchGetLight(ToonSurfaceOutput s, half3 viewDir, UnityLight light, UnityIndirect indirect)
{
    ToonSketchLight output;
    output.diffuse = DotClamped(s.Normal, light.dir);
#ifdef _SPECULAR_ON
    half gloss = saturate(dot(viewDir, reflect(-light.dir, s.Normal)));
    output.specular = max(0, pow(gloss, lerp(10, 200, s.Smoothness)));
#else
    output.specular = 0;
#endif
    if (_RampStyle == 0)
        output.diffuse = output.diffuse * 0.5 + 0.5;
    half3 ramp = saturate(s.Ambient + tex2D(_RampTex, output.diffuse));
    output.color = (s.Albedo * (indirect.diffuse + light.color * output.diffuse))
                    + (s.Specular * (indirect.specular + light.color * output.specular));
    output.color = output.color * ramp;
    return output;
}

#ifdef _HATCH_ON
#include "Hatch.cginc"
#endif

half4 LightingToonSketch(ToonSurfaceOutput s, half3 viewDir, UnityGI gi)
{
	// Normals
    s.Normal = BlendNormals(normalize(s.Normal), s.Bump);
    // Energy Conservation
	half oneMinusReflectivity;
	s.Albedo = EnergyConservationBetweenDiffuseAndSpecular(s.Albedo, s.Specular, oneMinusReflectivity);
    // Alpha
    half outputAlpha;
	s.Albedo = PreMultiplyAlpha(s.Albedo, s.Alpha, oneMinusReflectivity, outputAlpha);
    // Rim Light
#ifdef _RIM_LIGHT_ON
    half rim = max(0, pow(1 - DotClamped(normalize(viewDir), s.Normal), _RimPower) * s.Rim);
#else
    half rim = 0;
#endif
    // Light
    half indirectLight = gi.indirect.diffuse + gi.indirect.specular;
    ToonSketchLight light = ToonSketchGetLight(s, viewDir, gi.light, gi.indirect);
    half3 color = light.color + rim;
    // Shadow
#ifdef USING_DIRECTIONAL_LIGHT
    indirectLight *= s.Attenuation;
#else
    indirectLight = s.Attenuation;
#endif
    // Output
    half4 output;
#ifdef _HATCH_ON
    output.rgb = HatchShade(s, indirectLight, Luminance(color), color);
#else
    output.rgb = color;
#endif
#if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
    output.a = outputAlpha;
#else
    UNITY_OPAQUE_ALPHA(output.a);
#endif
    return output;
}

inline void LightingToonSketch_GI(inout ToonSurfaceOutput s, UnityGIInput data, inout UnityGI gi)
{
    gi = UnityGlobalIllumination(data, 1, s.Smoothness, s.Normal);
    s.Ambient = data.ambient;
    s.Attenuation = data.atten;
}