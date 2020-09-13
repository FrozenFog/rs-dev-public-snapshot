uniform matrix vpmatrix;

struct VSHandler
{
    vector position : POSITION;
    vector texcoords : TEXCOORD;
    vector screenpos : TEXCOORD1;
    vector out_position : TEXCOORD2;
	vector color : COLOR;
};

VSHandler vmain(in vector position : POSITION, in vector texcoord : TEXCOORD, in vector color : COLOR)
{
	VSHandler output_data = (VSHandler)0;

    output_data.position = output_data.out_position = mul(position, vpmatrix);
    output_data.screenpos = output_data.position;
	
    output_data.screenpos.xy += float2(1.0, 1.0);
	
    output_data.screenpos.xy *= 0.5;
    output_data.screenpos.y = 1.0 - output_data.screenpos.y;
	//output_data.position = position;
	output_data.texcoords = texcoord;

	//color.rgb = float3(20.0/256.0, 0.0, 1.0);
	output_data.color = color;

	return output_data;
}
