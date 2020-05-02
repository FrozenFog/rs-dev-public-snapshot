//mult color input from application
uniform vector remap_color;
uniform matrix vpmatrix;
uniform vector shadow_cof;
uniform vector vxl_cof;
uniform vector plain_cof;

sampler2D default_sampler : register(s0);
sampler1D palette_sampler : register(s1);

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

vector main(in vector incolor :COLOR) :COLOR
{
	vector outcolor = {0.0,0.0,0.0,0.0};

	outcolor.r = incolor.r*vxl_cof.r;
	outcolor.g = incolor.g*vxl_cof.g;
	outcolor.b = incolor.b*vxl_cof.b;
	outcolor.a = incolor.a*vxl_cof.a;

	return saturate(outcolor);
}

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

struct VSHandler
{
	vector position:POSITION;
	vector texcoords : TEXCOORD0;
	vector color : COLOR;
};

VSHandler vmain(in vector position : POSITION, in vector texcoord : TEXCOORD, in vector color : COLOR)
{
	VSHandler output_data = (VSHandler)0;

	output_data.position = mul(position, vpmatrix);
	//output_data.position = position;
	output_data.texcoords = texcoord;
	output_data.color = color;

	return output_data;
}