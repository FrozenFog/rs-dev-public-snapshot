sampler2D default_sampler : register(s0);
texture2D source_texture : register(t0);
sampler2D alphasurf_sampler : register(s1);
uniform float2 screen_dimension : register(c0) = float2(1024, 768);

#define FXAA_PC 1
#define FXAA_HLSL_3 1
#define FXAA_QUALITY__PRESET 12
#define FXAA_GREEN_AS_LUMA 1

#include "fxaa.fxh"

vector psmain(in float2 texcoord : TEXCOORD, out float depth : DEPTH) : COLOR
{
    vector orig = FxaaPixelShader(
    texcoord + float2(0.5, 0.5) / screen_dimension,
    float4(texcoord, texcoord + float2(1.0, 1.0) / screen_dimension),
    default_sampler, default_sampler, default_sampler,
    float2(1.0, 1.0) / screen_dimension,
    float4(float2(-0.5, -0.5) / screen_dimension, float2(0.5, 0.5) / screen_dimension),
    float4(float2(-2.0, -2.0) / screen_dimension, float2(2.0, 2.0) / screen_dimension),
    float4(float2(8.0, 8.0) / screen_dimension, float2(-4.0, -4.0) / screen_dimension),
    0.75, 0.0, 0.0, 8.0, 0.25, 0.0,
    float4(1.0, -1.0, 0.25, -0.25)
    );
    
    //vector orig = tex2D(default_sampler, texcoord);
    float aval = tex2D(alphasurf_sampler, texcoord).r * (255. / 256) + (0.5 / 256);

    orig *= aval / 0.5;
    orig.a = 1.0;
    depth = 0.0;
    return saturate(orig);
}