sampler2D default_sampler : register(s0);
sampler2D alphasurf_sampler : register(s1);

struct PSOut
{
    vector color : COLOR;
    float depth : DEPTH;
};

PSOut amain(in float2 texcoord : TEXCOORD, in float2 screenpos : TEXCOORD1)
{
    PSOut output = (PSOut) 0;
    
    float inindex = tex2D(default_sampler, texcoord).r * (255. / 256) + (0.5 / 256);
    float currenta = tex2D(alphasurf_sampler, screenpos).r;
    
    if (inindex == 127 / 255.0)
        discard;
    
    output.color.r = saturate(currenta * inindex / 0.5);
    output.color.a = 1.0;
    output.depth = 0.0;
    
    return output;
}