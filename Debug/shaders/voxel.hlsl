
uniform vector vxl_cof : register(c0);

vector main(in vector incolor :COLOR) :COLOR
{
	vector outcolor = { 0.0,0.0,0.0,0.0 };

	outcolor.r = incolor.r*vxl_cof.r;
	outcolor.g = incolor.g*vxl_cof.g;
	outcolor.b = incolor.b*vxl_cof.b;
	outcolor.a = incolor.a*vxl_cof.a;

	return saturate(outcolor);
}
