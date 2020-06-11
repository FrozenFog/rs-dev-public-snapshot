sampler2D default_sampler : register(s0);
sampler2D self_sampler : register(s1);

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
    
    color = orig;
    depth = 0.0;
}