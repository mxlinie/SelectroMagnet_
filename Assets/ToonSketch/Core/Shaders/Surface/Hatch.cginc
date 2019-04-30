half3 GetSketchMask(ToonSurfaceOutput s, half luminance, half threshold, float2 scale)
{
	// Calculation
	half light = max(0, luminance * threshold);
	half bright = 0.0;
	float3 weight0 = 0.0;
	float3 weight1 = 0.0;
	if (light > 6.0)
	{
		bright = 1.0;
	}
	else if (light > 5.0)
	{
		bright = 1.0 - (6.0 - light);
		weight0.x = 1.0 - bright;
	}
	else if (light > 4.0)
	{
		weight0.x = 1.0 - (5.0 - light);
		weight0.y = 1.0 - weight0.x;
	}
	else if (light > 3.0)
	{
		weight0.y = 1.0 - (4.0 - light);
		weight0.z = 1.0 - weight0.y;
	}
	else if (light > 2.0)
	{
		weight0.z = 1.0 - (3.0 - light);
		weight1.x = 1.0 - weight0.z;
	}
	else if (light > 1.0)
	{
		weight1.x = 1.0 - (2.0 - light);
		weight1.y = 1.0 - weight1.x;
	}
	else if (light >= 0.0)
	{
		weight1.y = 1.0 - (1.0 - light);
		weight1.z = 1.0 - weight1.y;
	}
	// Textures
	half3 white = half3(1, 1, 1) * bright;
	half3 hatchTex0 = tex2D(_Hatch0, s.UV * scale) * weight0.x;
	half3 hatchTex1 = tex2D(_Hatch1, s.UV * scale) * weight0.y;
	half3 hatchTex2 = tex2D(_Hatch2, s.UV * scale) * weight0.z;
	half3 hatchTex3 = tex2D(_Hatch3, s.UV * scale) * weight1.x;
	half3 hatchTex4 = tex2D(_Hatch4, s.UV * scale) * weight1.y;
	half3 hatchTex5 = tex2D(_Hatch5, s.UV * scale) * weight1.z;
	// Composite
	half3 output = white + 
		hatchTex0 +
		hatchTex1 +
		hatchTex2 +
		hatchTex3 +
		hatchTex4 +
		hatchTex5;
	return output;
}

half3 HatchShade(ToonSurfaceOutput s, half lighting, half shading, half3 color)
{
	float2 scale = max(0.05, _HatchScale);
	half lightThreshold;
	half shadeThreshold;
	if (IsGammaSpace())
	{
		lightThreshold = lerp(25, 2.5, _HatchThreshold);
		shadeThreshold = lerp(50, 5, _HatchThreshold);
	}
	else
	{
		lightThreshold = lerp(25, 2.5, _HatchThreshold);
		shadeThreshold = lerp(250, 25, _HatchThreshold);
	}
	half3 lightHatch = GetSketchMask(s, lighting, lightThreshold, scale);
	half3 shadeHatch = GetSketchMask(s, shading, shadeThreshold, scale);
	half sketch;
#ifdef UNITY_PASS_FORWARDBASE
	sketch = saturate(lightHatch * shadeHatch);
#else
	sketch = saturate(shadeHatch);
#endif
	return color * saturate(lerp(half3(1, 1, 1), sketch, _HatchStrength));
}