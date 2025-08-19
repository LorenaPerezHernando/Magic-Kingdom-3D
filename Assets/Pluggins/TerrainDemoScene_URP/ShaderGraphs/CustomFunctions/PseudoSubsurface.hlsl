// PseudoSubsurface.hlsl
void PseudoSubsurface_half(half3 WorldPosition, half3 WorldNormal, half3 SSRadius, half ShadowResponse, out half3 ssAmount)
{
#ifdef SHADERGRAPH_PREVIEW
    half3 color = half3(1.0, 1.0, 1.0);
    half   att  = 1.0;
    half3  dir  = half3(0.707, 0.0, 0.707);
#else
    half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
    Light mainLight = GetMainLight(shadowCoord);
    half3 color = mainLight.color;
    half att = mainLight.shadowAttenuation; // escalar (no half3)
    half3 dir = mainLight.direction;
#endif

    // Normal y luz normalizadas
    half3 N = normalize(WorldNormal);
    half3 L = normalize(-dir);

    // Radio/alpha como escalar (usa el canal X del parámetro Vector3)
    half alpha = SSRadius.x;

    // Wrap básico
    half theta = max(0.0h, dot(N, L) + alpha) - alpha;
    half normalizer = (2.0h + alpha) / (2.0h * (1.0h + alpha));

    // Base y exponente seguros (sin negativos ni ceros)
    half denom = max((half) 1e-5, 1.0h + alpha); // evita /0
    half baseTerm = (theta + alpha) / denom;
    baseTerm = saturate(baseTerm); // [0..1]
    half exponent = max((half) 1e-5, 1.0h + alpha); // evita pow(x,0)

    // pow sin NaNs
    half wrapped = pow(max(baseTerm, (half) 1e-5), exponent) * normalizer;

    // Sombra (escalar) y salida en color
    half shadow = lerp(1.0h, att, ShadowResponse);
    ssAmount = abs(color * shadow * wrapped); // half3
}