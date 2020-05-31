sampler2D default_sampler : register(s0);

uniform vector shadow_cof : register(c4);

vector smain(in float2 texcoords : TEXCOORD) :COLOR
{
	float inindex = tex2D(default_sampler, texcoords).r * (255. / 256) + (0.5 / 256);

	if (inindex <= 0.5 / 256)
	discard;

	vector incolor = { 0.0,0.0,0.0,0.5f };
	vector outcolor = { 0.0,0.0,0.0,0.0 };

	outcolor.r = incolor.r*shadow_cof.r;
	outcolor.g = incolor.g*shadow_cof.g;
	outcolor.b = incolor.b*shadow_cof.b;
	outcolor.a = incolor.a*shadow_cof.a;

	return saturate(outcolor);
}