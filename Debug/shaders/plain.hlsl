sampler2D default_sampler : register(s0);
sampler1D palette_sampler : register(s1);

uniform vector plain_cof : register(c1);
uniform vector remap_color : register(c2);

vector pmain(in float2 texcoords : TEXCOORD) : COLOR
{
	vector outcolor = { 0.0,0.0,0.0,0.0 };

	if (texcoords.x > 1.0f || texcoords.x < 0.0f || texcoords.y > 1.0f || texcoords.y < 0.0f)
		discard;

	float inindex = tex2D(default_sampler, texcoords).r * (255. / 256) + (0.5 / 256);
	vector incolor = tex1D(palette_sampler, inindex);

	if (remap_color.a == 0.0 && inindex*256.0 >= 16.0 && inindex*256.0 <= 31.0)
	{
		float rto = (32.0 - inindex*256.0) / 16.0;
		incolor.rgb = mul(remap_color.rgb, rto);
	}

	outcolor.r = incolor.r*plain_cof.r;
	outcolor.g = incolor.g*plain_cof.g;
	outcolor.b = incolor.b*plain_cof.b;
	outcolor.a = incolor.a*plain_cof.a;

	if (outcolor.a == 0.0f)
	discard;

	return saturate(outcolor);
}
