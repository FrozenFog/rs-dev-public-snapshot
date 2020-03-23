//mult color input from application
uniform vector vec;
uniform matrix vpmatrix;

sampler2D default_sampler;

vector main(in float4 incolor :COLOR) :COLOR
{
	float4 outcolor = {0.0,0.0,0.0,0.0};
	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	outcolor.a = incolor.a*vec.a;
	return outcolor;
}

vector pmain(in float2 texcoords : TEXCOORD) : COLOR
{
	vector outcolor = { 0.0,0.0,0.0,0.0 };
	vector incolor = tex2D(default_sampler, texcoords);

	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	outcolor.a = incolor.a*vec.a;

	if (outcolor.a == 0.0f)
		discard;

	return outcolor;
}

struct VSHandler
{
	vector position:POSITION;
	vector texcoords:TEXCOORD;
	vector color : COLOR;
};

VSHandler vmain(in vector position:POSITION, in vector texcoord : TEXCOORD, in vector color : COLOR)
{
	VSHandler output_data = (VSHandler)0;

	output_data.position = mul(position, vpmatrix);
	output_data.texcoords = texcoord;
	output_data.color = color;

	return output_data;
}