#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 worldPos, out float3 Direction, out float3 LightColor)	{
#if SHADERGRAPH_PREVIEW
	Direction = float3 (0.5,0.5,0);
	LightColor = 1;
#else
	Light mainLight = GetMainLight(0);
	Direction = mainLight.direction;
	LightColor = mainLight.color;
#endif
}

#endif