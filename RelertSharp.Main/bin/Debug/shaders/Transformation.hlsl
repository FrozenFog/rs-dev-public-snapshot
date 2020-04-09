//mult color input from application
uniform vector vec;
uniform matrix vpmatrix;

sampler2D default_sampler : register(s0);

vector smain(in float2 texcoords : TEXCOORD) :COLOR
{
	vector color = tex2D(default_sampler,texcoords);

	if (color.a == 0.0f)
		discard;

	vector incolor = { 0.0,0.0,0.0,0.5f };
	vector outcolor = { 0.0,0.0,0.0,0.0 };

	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	outcolor.a = incolor.a*vec.a;

	return saturate(outcolor);
}

vector main(in vector incolor :COLOR) :COLOR
{
	vector outcolor = {0.0,0.0,0.0,0.0};

	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	outcolor.a = incolor.a*vec.a;

	return saturate(outcolor);
}

vector pmain(in float2 texcoords : TEXCOORD) : COLOR
{
	vector outcolor = { 0.0,0.0,0.0,0.0 };
	vector incolor = tex2D(default_sampler, texcoords);
	
	if (texcoords.x > 1.0f || texcoords.x < 0.0f || texcoords.y > 1.0f || texcoords.y < 0.0f)
		discard;

	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	outcolor.a = incolor.a*vec.a;

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
	vector temp = texcoord;

	output_data.position = mul(position, vpmatrix);
	output_data.texcoords = texcoord;
	output_data.color = color;

	return output_data;
}