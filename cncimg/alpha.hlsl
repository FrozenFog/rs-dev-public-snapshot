sampler2D default_sampler : register(s0);
sampler2D self_sampler : register(s3);

uniform vector alpha_cof;

void amain(in float2 texcoord : TEXCOORD, in float2 screenpos : TEXCOORD1, 
out vector color : COLOR, out float depth : DEPTH)
{
    float inindex = tex2D(default_sampler, texcoord).r * (255. / 256) + (0.5 / 256);
    vector orig = tex2D(self_sampler, screenpos);
    
    if (inindex == 127.0 / 256)
        discard;
    
    orig = mul(orig, inindex / 0.5);
    orig = saturate(orig);
    orig.a = 1.0;
    vector outcolor = orig;
    
    outcolor.r = outcolor.r * alpha_cof.r;
    outcolor.g = outcolor.g * alpha_cof.g;
    outcolor.b = outcolor.b * alpha_cof.b;
    outcolor.a = outcolor.a * alpha_cof.a;
    
    color = outcolor;
    depth = 0.0;
}