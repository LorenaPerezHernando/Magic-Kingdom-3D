void PseudoSubsurface_half (half3 WorldPosition, half3 WorldNormal, half3 SSRadius, half ShadowResponse, out half3 ssAmount)
{

#ifdef SHADERGRAPH_PREVIEW
	half3 color = half3(0,0,0);
	half3 atten = 1;
	half3 dir = half3 (0.707, 0, 0.707);
	
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
	Light mainLight = GetMainLight(shadowCoord);
	half3 color = mainLight.color;
	half3 atten = mainLight.shadowAttenuation;
	half3 dir = mainLight.direction;
	
#endif

    // PseudoSubsurface.hlsl  (función Custom de tu Shader Graph)

    half NdotL = dot(WorldNormal, -dir);
    half alpha = SSRadius;

// wrap básico
    half theta = max(0.0, NdotL + alpha) - alpha;
    half normalizer = (2.0 + alpha) / (2.0 * (1.0 + alpha));

// base y exponente seguros
    half denom = max((half) 1e-5, 1.0 + alpha); // evita /0
    half baseTerm = (theta + alpha) / denom;
    baseTerm = saturate(baseTerm); // clamp [0..1]
    half exponent = max((half) 1e-5, 1.0 + alpha); // evita pow(x,0)

// pow sin NaNs
    half wrapped = pow(max(baseTerm, (half) 1e-5), exponent) * normalizer;

    half shadow = lerp(1.0, atten, ShadowResponse);
    ssAmount = abs(color * shadow * wrapped);


}