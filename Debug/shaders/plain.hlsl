sampler2D default_sampler : register(s0);
sampler1D palette_sampler : register(s1);
sampler2D zshade_samlper : register(s2);

uniform vector plain_cof : register(c1);
uniform vector remap_color : register(c2);
uniform vector screen_dimension;
uniform float z_adjust = 0.0f;

static const float zdistance = 5000.0;

vector pmain(in float2 texcoords : TEXCOORD, in vector position : TEXCOORD2, out float outZ : DEPTH): COLOR
{
	vector outcolor = { 0.0,0.0,0.0,0.0 };

	if (texcoords.x > 1.0f || texcoords.x < 0.0f || texcoords.y > 1.0f || texcoords.y < 0.0f)
		discard;

	float inindex = tex2D(default_sampler, texcoords).r * (255. / 256) + (0.5 / 256);
    float zadjust = tex2D(zshade_samlper, texcoords).r * 255;
	vector incolor = tex1D(palette_sampler, inindex);

    if (remap_color.a == 0.0 && inindex * 256.0 >= 16.0 && inindex * 256.0 <= 31.0)
	{
		float rto = (32.0 - inindex*256.0) / 16.0;
		incolor.rgb = mul(remap_color.rgb, rto);
	}
	
    outcolor = incolor * plain_cof;

	if (outcolor.a == 0.0f)
		discard;

    float distance_adjust = (zadjust + z_adjust) * sqrt(3.0f) / zdistance;
	
    position.z += distance_adjust;
    outZ = position.z / position.w;
	
    //if (outZ)
    //    outcolor = vector(outZ, outZ, outZ, 1.0f);
	
    return saturate(outcolor);
}
