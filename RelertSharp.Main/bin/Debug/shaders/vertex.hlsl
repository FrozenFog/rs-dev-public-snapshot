uniform matrix vpmatrix : register(c5);

struct VSHandler
{
	vector position:POSITION;
	vector texcoords : TEXCOORD;
	vector color : COLOR;
};

VSHandler vmain(in vector position : POSITION, in vector texcoord : TEXCOORD, in vector color : COLOR)
{
	VSHandler output_data = (VSHandler)0;

	output_data.position = mul(position, vpmatrix);
	//output_data.position = position;
	output_data.texcoords = texcoord;

	//color.rgb = float3(20.0/256.0, 0.0, 1.0);
	output_data.color = color;

	return output_data;
}
