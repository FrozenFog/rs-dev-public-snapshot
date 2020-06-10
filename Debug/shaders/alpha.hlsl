sampler2D default_sampler : register(s0);
uniform vector alpha_cof;

void amain(in float2 texcoord:TEXCOORD,out vector color:COLOR)
{
    float inindex = tex2D(default_sampler, texcoord).r * (255. / 256) + (0.5 / 256);
    
    if(inindex == 127.0 / 256)
        discard;
    
    float cvalue = inindex <= 127.0 / 256 ? 0.0 : 1.0;
    vector outcolor = { cvalue, cvalue, cvalue, abs(0.5 - inindex) / 0.5 };
    outcolor.r = outcolor.r * alpha_cof.r;
    outcolor.g = outcolor.g * alpha_cof.g;
    outcolor.b = outcolor.b * alpha_cof.b;
    outcolor.a = outcolor.a * alpha_cof.a;
    
    color = outcolor;
}