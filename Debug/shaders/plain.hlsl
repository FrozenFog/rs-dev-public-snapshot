sampler2D default_sampler : register(s0);
sampler1D palette_sampler : register(s1);
sampler2D zshade_samlper : register(s2);

uniform vector plain_cof : register(c1);
uniform vector remap_color : register(c2);
uniform vector screen_dimension;
uniform float z_adjust = 0.0f;

static const float zdistance = 5000.0;
static const float Epsilon = 1e-10;
static const float pi = 3.1415926536;

float3 RGBtoHCV(in float3 RGB)
{
    // Based on work by Sam Hocevar and Emil Persson
    float4 P = (RGB.g < RGB.b) ? float4(RGB.bg, -1.0, 2.0 / 3.0) : float4(RGB.gb, 0.0, -1.0 / 3.0);
    float4 Q = (RGB.r < P.x) ? float4(P.xyw, RGB.r) : float4(RGB.r, P.yzx);
    float C = Q.x - min(Q.w, Q.y);
    float H = abs((Q.w - Q.y) / (6 * C + Epsilon) + Q.z);
    return float3(H, C, Q.x);
}

float3 RGBtoHSV(in float3 RGB)
{
    float3 HCV = RGBtoHCV(RGB);
    float S = HCV.y / (HCV.z + Epsilon);
    return float3(HCV.x, S, HCV.z);
}

float3 HUEtoRGB(in float H)
{
    float R = abs(H * 6 - 3) - 1;
    float G = 2 - abs(H * 6 - 2);
    float B = 2 - abs(H * 6 - 4);
    return saturate(float3(R, G, B));
}

float3 HSVtoRGB(in float3 HSV)
{
    float3 RGB = HUEtoRGB(HSV.x);
    return ((RGB - 1) * HSV.y + 1) * HSV.z;
}

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
        float rto = (32.0 - inindex * 256.0) / 16.0;
		//incolor.rgb = mul(remap_color.rgb, rto);
        float i = inindex * 256.0 - 16.0;
        incolor.rgb = RGBtoHSV(remap_color.rgb);
        incolor.r = incolor.r;
        incolor.g = incolor.g * sin(i * pi / 67.5 + pi / 3.6);
        incolor.b = incolor.b * cos(i * 7 * pi / 270.0 + pi / 9);
        incolor.rgb = HSVtoRGB(incolor.rgb);
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
