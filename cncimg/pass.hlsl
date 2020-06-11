sampler2D default_sampler : register(s0);
sampler2D alphasurf_sampler : register(s1);

vector psmain(in float2 texcoord : TEXCOORD, out float depth : DEPTH) : COLOR
{
    vector orig = tex2D(default_sampler, texcoord);
    float aval = tex2D(alphasurf_sampler, texcoord).r;

    orig *= aval / 0.5;
    orig.a = 1.0;
    depth = 0.0;
    return saturate(orig);
}
