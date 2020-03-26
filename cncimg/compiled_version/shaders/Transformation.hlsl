

//mult color input from application
uniform float3 vec;
uniform matrix vpmatrix;

sampler2D default_sampler;

float FarDepth():DEPTH
{
	return 1.0f;
}

float4 main(in float4 incolor :COLOR) :COLOR
{
	float4 outcolor = {0.0,0.0,0.0,1.0};
	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	return outcolor;
}

float4 pmain(in float2 texcoords : TEXCOORD) : COLOR
{
	float4 outcolor = { 0.0,0.0,0.0,0.0 };
	float4 incolor = tex2D(default_sampler, texcoords);

	outcolor.r = incolor.r*vec.r;
	outcolor.g = incolor.g*vec.g;
	outcolor.b = incolor.b*vec.b;
	outcolor.a = incolor.a;

	if (outcolor.a == 0.0f)
		discard;

	return outcolor;
}

struct VSHandler
{
	float4 position:POSITION;
	float4 texcoords:TEXCOORD;
	float4 color : COLOR;
};

VSHandler vmain(in float4 position:POSITION, in float4 texcoord : TEXCOORD, in float4 color : COLOR)
{
	VSHandler output_data = (VSHandler)0;

	output_data.position = mul(position, vpmatrix);
	output_data.texcoords = texcoord;
	output_data.color = color;

	return output_data;
}