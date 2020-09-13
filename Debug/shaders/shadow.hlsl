sampler2D default_sampler : register(s0);
uniform vector shadow_cof : register(c4);
uniform float z_adjust = 0.0f;
static const float zdistance = 5000.0;

vector smain(in float2 texcoords : TEXCOORD, in vector position : TEXCOORD2, out float outZ : DEPTH) :COLOR
{
	float inindex = tex2D(default_sampler, texcoords).r * (255. / 256) + (0.5 / 256);

	if (inindex <= 0.5 / 256)
	discard;

	vector incolor = { 0.0,0.0,0.0,0.5f };
	vector outcolor = { 0.0,0.0,0.0,0.0 };
	
    outcolor = incolor * shadow_cof;
	
    float distance_adjust = (z_adjust) * sqrt(3.0) / zdistance;
	
    position.z += distance_adjust;
    outZ = position.z / position.w;

	return saturate(outcolor);
}