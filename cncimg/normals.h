#include <windows.h>
#include <d3dx9.h>

D3DXVECTOR3 NormalTable1[16] = {
	{ 0.5495f,	-0.0002f,	-0.8355f },
	{ 0.0001f,	0.5494f,	-0.8356f },
	{ -0.5494f,	-0.0001f,	-0.8356f },
	{ 0.0001f,	-0.5495f,	-0.8355f },
	{ 0.9490f,	0.0003f,	-0.3153f },
	{ -0.0002f,	0.9490f,	-0.3153f },
	{ -0.9490f,	0.0003f,	-0.3153f },
	{ -0.0004f,	-0.9490f,	-0.3153f },
	{ 0.9508f,	-0.0003f,	0.3097f },
	{ 0.0002f,	0.9508f,	0.3097f },
	{ -0.9508f,	-0.0001f,	0.3097f },
	{ 0.0001f,	-0.9508f,	0.3097f },
	{ 0.5524f,	-0.0000f,	0.8336f },
	{ 0.0000f,	0.5524f,	0.8336f },
	{ -0.5524f,	0.0001f,	0.8336f },
	{ -0.0001f,	-0.5524f,	0.8336f },
};

D3DXVECTOR3 NormalTable2[36] = {
	{ 0.6712f,	0.1985f,	-0.7142f },
	{ 0.2696f,	0.5844f,	-0.7654f },
	{ -0.0405f,	0.0970f,	-0.9945f },
	{ -0.5724f,	-0.0919f,	-0.8148f },
	{ -0.1714f,	-0.5727f,	-0.8016f },
	{ 0.3626f,	-0.3030f,	-0.8813f },
	{ 0.8103f,	-0.3490f,	-0.4707f },
	{ 0.1040f,	0.9387f,	-0.3288f },
	{ -0.3240f,	0.5877f,	-0.7414f },
	{ -0.8009f,	0.3405f,	-0.4926f },
	{ -0.6655f,	-0.5901f,	-0.4570f },
	{ 0.3148f,	-0.8030f,	-0.5061f },
	{ 0.9726f,	0.1511f,	-0.1766f },
	{ 0.6803f,	0.6842f,	-0.2627f },
	{ -0.5201f,	0.8278f,	-0.2105f },
	{ -0.9616f,	-0.1790f,	-0.2078f },
	{ -0.2627f,	-0.9375f,	-0.2284f },
	{ 0.2197f,	-0.9713f,	0.0911f },
	{ 0.9238f,	-0.2300f,	0.3061f },
	{ -0.0825f,	0.9707f,	0.2259f },
	{ -0.5918f,	0.6968f,	0.4053f },
	{ -0.9253f,	0.3666f,	0.0971f },
	{ -0.7051f,	-0.6878f,	0.1728f },
	{ 0.7324f,	-0.6804f,	-0.0263f },
	{ 0.8552f,	0.3746f,	0.3583f },
	{ 0.4730f,	0.8365f,	0.2767f },
	{ -0.0976f,	0.6541f,	0.7501f },
	{ -0.9041f,	-0.1537f,	0.3987f },
	{ -0.2119f,	-0.8581f,	0.4677f },
	{ 0.5002f,	-0.6744f,	0.5431f },
	{ 0.5845f,	-0.1102f,	0.8038f },
	{ 0.4374f,	0.4546f,	0.7759f },
	{ -0.0424f,	0.0833f,	0.9956f },
	{ -0.5963f,	0.2201f,	0.7720f },
	{ -0.5065f,	-0.3970f,	0.7654f },
	{ 0.0706f,	-0.4785f,	0.8753f },
};

D3DXVECTOR3 NormalTable3[64] = {
	{ 0.4565f,	-0.0740f,	-0.8866f },
	{ 0.5077f,	0.3851f,	-0.7707f },
	{ 0.0954f,	0.2267f,	-0.9693f },
	{ -0.3588f,	0.5432f,	-0.7591f },
	{ -0.3613f,	0.1330f,	-0.9229f },
	{ -0.4831f,	-0.3241f,	-0.8134f },
	{ -0.0181f,	-0.1976f,	-0.9801f },
	{ 0.3211f,	-0.5015f,	-0.8034f },
	{ 0.7995f,	0.0696f,	-0.5966f },
	{ 0.3910f,	0.7713f,	-0.5022f },
	{ 0.0808f,	0.6145f,	-0.7848f },
	{ -0.7327f,	0.4114f,	-0.5420f },
	{ -0.7353f,	0.0091f,	-0.6777f },
	{ -0.8025f,	-0.3949f,	-0.4473f },
	{ -0.1341f,	-0.5892f,	-0.7968f },
	{ 0.7196f,	-0.3762f,	-0.5837f },
	{ 0.9669f,	0.1736f,	-0.1871f },
	{ 0.7608f,	0.5191f,	-0.3894f },
	{ -0.1146f,	0.8755f,	-0.4694f },
	{ -0.5324f,	0.7689f,	-0.3542f },
	{ -0.9623f,	0.0250f,	-0.2710f },
	{ -0.4674f,	-0.7220f,	-0.5102f },
	{ 0.0584f,	-0.8524f,	-0.5197f },
	{ 0.4982f,	-0.7437f,	-0.4457f },
	{ 0.9392f,	-0.2702f,	-0.2120f },
	{ 0.5839f,	0.8094f,	-0.0619f },
	{ 0.1838f,	0.9732f,	-0.1380f },
	{ -0.8844f,	0.4522f,	-0.1158f },
	{ -0.9432f,	-0.3321f,	0.0121f },
	{ -0.6984f,	-0.7066f,	-0.1138f },
	{ -0.2284f,	-0.9547f,	-0.1907f },
	{ 0.7316f,	-0.6759f,	-0.0896f },
	{ 0.9693f,	0.0468f,	0.2416f },
	{ 0.8556f,	0.5035f,	0.1199f },
	{ -0.2512f,	0.9679f,	-0.0001f },
	{ -0.6478f,	0.7567f,	0.0877f },
	{ -0.9692f,	0.1452f,	0.1991f },
	{ -0.4148f,	-0.8890f,	0.1941f },
	{ 0.2508f,	-0.9612f,	-0.1151f },
	{ 0.4786f,	-0.8426f,	0.2469f },
	{ 0.8900f,	-0.3961f,	0.2256f },
	{ 0.5241f,	0.7624f,	0.3797f },
	{ 0.1196f,	0.9455f,	0.3029f },
	{ -0.7609f,	0.4901f,	0.4254f },
	{ -0.8698f,	-0.2022f,	0.4501f },
	{ -0.7095f,	-0.6024f,	0.3657f },
	{ 0.0193f,	-0.9589f,	0.2832f },
	{ 0.6261f,	-0.5647f,	0.5377f },
	{ 0.7699f,	-0.1267f,	0.6254f },
	{ 0.7642f,	0.3507f,	0.5413f },
	{ -0.0019f,	0.7414f,	0.6711f },
	{ -0.3709f,	0.8184f,	0.4390f },
	{ -0.7139f,	0.1287f,	0.6883f },
	{ -0.2952f,	-0.7387f,	0.6060f },
	{ 0.1862f,	-0.7384f,	0.6482f },
	{ 0.3875f,	-0.3588f,	0.8492f },
	{ 0.4810f,	0.1248f,	0.8678f },
	{ 0.3918f,	0.5451f,	0.7412f },
	{ -0.0035f,	0.3656f,	0.9308f },
	{ -0.4205f,	0.4850f,	0.7668f },
	{ -0.3549f,	0.0195f,	0.9347f },
	{ -0.5478f,	-0.3592f,	0.7555f },
	{ -0.1067f,	-0.4451f,	0.8891f },
	{ 0.0868f,	-0.0593f,	0.9945f },
};

D3DXVECTOR3 NormalTable4[245] = {
	{ 0.5266f,	-0.3596f,	-0.7703f },
	{ 0.1505f,	0.4360f,	0.8873f },
	{ 0.4142f,	0.7383f,	-0.5324f },
	{ 0.0752f,	0.9162f,	-0.3935f },
	{ -0.3161f,	0.9307f,	-0.1838f },
	{ -0.7738f,	0.6233f,	-0.1125f },
	{ -0.9008f,	0.4285f,	-0.0696f },
	{ -0.9989f,	-0.0110f,	0.0447f },
	{ -0.9798f,	-0.1577f,	-0.1233f },
	{ -0.9113f,	-0.3624f,	-0.1956f },
	{ -0.6241f,	-0.7209f,	-0.3013f },
	{ -0.3102f,	-0.8093f,	-0.4988f },
	{ 0.1466f,	-0.8158f,	-0.5594f },
	{ -0.7165f,	-0.6944f,	-0.0669f },
	{ 0.5040f,	-0.1142f,	-0.8561f },
	{ 0.4555f,	0.8726f,	-0.1762f },
	{ -0.0050f,	-0.1144f,	-0.9934f },
	{ -0.1047f,	-0.3277f,	-0.9390f },
	{ 0.5604f,	0.7526f,	-0.3458f },
	{ -0.0606f,	0.8216f,	-0.5668f },
	{ -0.3023f,	0.7970f,	-0.5228f },
	{ -0.6715f,	0.6707f,	-0.3149f },
	{ -0.7784f,	-0.1284f,	0.6145f },
	{ -0.9240f,	0.2784f,	-0.2620f },
	{ -0.6998f,	-0.5505f,	-0.4553f },
	{ -0.5682f,	-0.5172f,	-0.6400f },
	{ 0.0541f,	-0.9329f,	-0.3561f },
	{ 0.7584f,	0.5729f,	-0.3109f },
	{ 0.0036f,	0.3050f,	-0.9523f },
	{ -0.0608f,	-0.9869f,	-0.1495f },
	{ 0.6352f,	0.0455f,	-0.7710f },
	{ 0.5217f,	0.2413f,	-0.8183f },
	{ 0.2694f,	0.6354f,	-0.7236f },
	{ 0.0457f,	0.6728f,	-0.7385f },
	{ -0.1805f,	0.6747f,	-0.7157f },
	{ -0.3971f,	0.6366f,	-0.6610f },
	{ -0.5520f,	0.4725f,	-0.6870f },
	{ -0.7722f,	0.0831f,	-0.6300f },
	{ -0.6698f,	-0.1195f,	-0.7328f },
	{ -0.5405f,	-0.3184f,	-0.7788f },
	{ -0.3861f,	-0.5228f,	-0.7600f },
	{ -0.2615f,	-0.6886f,	-0.6764f },
	{ -0.0194f,	-0.6961f,	-0.7177f },
	{ 0.3036f,	-0.4818f,	-0.8220f },
	{ 0.6819f,	-0.1951f,	-0.7049f },
	{ -0.2449f,	-0.1166f,	-0.9625f },
	{ 0.8008f,	-0.0230f,	-0.5985f },
	{ -0.3703f,	0.0956f,	-0.9240f },
	{ -0.3307f,	-0.3266f,	-0.8854f },
	{ -0.1632f,	-0.5276f,	-0.8337f },
	{ 0.1264f,	-0.3131f,	-0.9413f },
	{ 0.3495f,	-0.2722f,	-0.8965f },
	{ 0.2399f,	-0.0858f,	-0.9670f },
	{ 0.3908f,	0.0815f,	-0.9168f },
	{ 0.2553f,	0.2687f,	-0.9288f },
	{ 0.1462f,	0.4804f,	-0.8647f },
	{ -0.3260f,	0.4785f,	-0.8153f },
	{ -0.4697f,	-0.1125f,	-0.8756f },
	{ 0.8184f,	-0.2585f,	-0.5132f },
	{ -0.4743f,	0.2922f,	-0.8304f },
	{ 0.7789f,	0.3958f,	-0.4864f },
	{ 0.6241f,	0.3938f,	-0.6749f },
	{ 0.7409f,	0.2038f,	-0.6400f },
	{ 0.4802f,	0.5658f,	-0.6703f },
	{ 0.3809f,	0.4245f,	-0.8214f },
	{ -0.0934f,	0.5011f,	-0.8603f },
	{ -0.2365f,	0.2962f,	-0.9254f },
	{ -0.1315f,	0.0940f,	-0.9868f },
	{ -0.8236f,	0.2958f,	-0.4840f },
	{ 0.6111f,	-0.6243f,	-0.4867f },
	{ 0.0695f,	-0.5203f,	-0.8511f },
	{ 0.2265f,	-0.6649f,	-0.7118f },
	{ 0.4713f,	-0.5689f,	-0.6740f },
	{ 0.3884f,	-0.7426f,	-0.5456f },
	{ 0.7837f,	-0.4807f,	-0.3934f },
	{ 0.9624f,	0.1357f,	-0.2353f },
	{ 0.8766f,	0.1720f,	-0.4494f },
	{ 0.6334f,	0.5898f,	-0.5009f },
	{ 0.1823f,	0.8007f,	-0.5707f },
	{ 0.1770f,	0.7641f,	0.6203f },
	{ -0.5440f,	0.6755f,	-0.4977f },
	{ -0.6793f,	0.2865f,	-0.6756f },
	{ -0.5904f,	0.0914f,	-0.8019f },
	{ -0.8244f,	-0.1331f,	-0.5502f },
	{ -0.7158f,	-0.3345f,	-0.6130f },
	{ 0.1743f,	-0.8925f,	0.4160f },
	{ -0.0825f,	-0.8371f,	-0.5408f },
	{ 0.2833f,	-0.8809f,	-0.3792f },
	{ 0.6751f,	-0.4266f,	-0.6018f },
	{ 0.8437f,	-0.5123f,	-0.1602f },
	{ 0.9773f,	-0.0986f,	-0.1875f },
	{ 0.8463f,	0.5227f,	-0.1029f },
	{ 0.6771f,	0.7213f,	-0.1455f },
	{ 0.3210f,	0.8709f,	-0.3722f },
	{ -0.1790f,	0.9115f,	-0.3702f },
	{ -0.4472f,	0.8267f,	-0.3415f },
	{ -0.7032f,	0.4963f,	-0.5091f },
	{ -0.9772f,	0.0636f,	-0.2027f },
	{ -0.8782f,	-0.4129f,	0.2415f },
	{ -0.8358f,	-0.3586f,	-0.4157f },
	{ -0.4992f,	-0.6934f,	-0.5196f },
	{ -0.1888f,	-0.9238f,	-0.3332f },
	{ 0.1923f,	-0.9694f,	-0.1529f },
	{ 0.5159f,	-0.7839f,	-0.3454f },
	{ 0.9059f,	-0.3010f,	-0.2979f },
	{ 0.9911f,	-0.1277f,	0.0371f },
	{ 0.9951f,	0.0984f,	-0.0044f },
	{ 0.7601f,	0.6463f,	0.0674f },
	{ 0.2052f,	0.9596f,	-0.1926f },
	{ -0.0428f,	0.9795f,	-0.1968f },
	{ -0.4380f,	0.8989f,	0.0085f },
	{ -0.8220f,	0.4808f,	-0.3052f },
	{ -0.8999f,	0.0817f,	-0.4283f },
	{ -0.9266f,	-0.1446f,	-0.3471f },
	{ -0.7937f,	-0.5578f,	-0.2428f },
	{ -0.4313f,	-0.8478f,	-0.3086f },
	{ -0.0055f,	-0.9650f,	0.2622f },
	{ 0.5879f,	-0.8040f,	-0.0889f },
	{ 0.6995f,	-0.6677f,	-0.2548f },
	{ 0.8893f,	0.3598f,	-0.2823f },
	{ 0.7810f,	0.1970f,	0.5927f },
	{ 0.5201f,	0.5067f,	0.6876f },
	{ 0.4039f,	0.6940f,	0.5961f },
	{ -0.1550f,	0.8992f,	0.4091f },
	{ -0.6573f,	0.5372f,	0.5285f },
	{ -0.7462f,	0.3341f,	0.5758f },
	{ -0.6250f,	-0.0491f,	0.7791f },
	{ 0.3181f,	-0.2547f,	0.9132f },
	{ -0.5559f,	0.4053f,	0.7258f },
	{ -0.7944f,	0.0994f,	0.5992f },
	{ -0.6404f,	-0.6895f,	0.3385f },
	{ -0.1267f,	-0.7341f,	0.6671f },
	{ 0.1055f,	-0.7808f,	0.6158f },
	{ 0.4080f,	-0.4809f,	0.7761f },
	{ 0.6951f,	-0.5451f,	0.4686f },
	{ 0.9732f,	-0.0065f,	0.2299f },
	{ 0.9469f,	0.3175f,	-0.0508f },
	{ 0.5636f,	0.8256f,	0.0272f },
	{ 0.3258f,	0.9454f,	0.0069f },
	{ -0.1718f,	0.9851f,	-0.0078f },
	{ -0.6704f,	0.7399f,	0.0548f },
	{ -0.8230f,	0.5550f,	0.1213f },
	{ -0.9662f,	0.1179f,	0.2293f },
	{ -0.9538f,	-0.2947f,	0.0589f },
	{ -0.8644f,	-0.5027f,	-0.0100f },
	{ -0.5306f,	-0.8420f,	-0.0974f },
	{ -0.1626f,	-0.9841f,	0.0718f },
	{ 0.0814f,	-0.9960f,	0.0364f },
	{ 0.7460f,	-0.6660f,	0.0008f },
	{ 0.9421f,	-0.3293f,	-0.0641f },
	{ 0.9397f,	-0.2811f,	0.1948f },
	{ 0.7712f,	0.5507f,	0.3194f },
	{ 0.6413f,	0.7307f,	0.2340f },
	{ 0.0807f,	0.9967f,	0.0099f },
	{ -0.0467f,	0.9766f,	0.2097f },
	{ -0.5311f,	0.8210f,	0.2096f },
	{ -0.6958f,	0.6560f,	0.2924f },
	{ -0.9761f,	0.2167f,	-0.0149f },
	{ -0.9617f,	-0.1441f,	0.2333f },
	{ -0.7721f,	-0.6136f,	0.1653f },
	{ -0.4496f,	-0.8361f,	0.3144f },
	{ -0.3927f,	-0.9146f,	0.0962f },
	{ 0.3906f,	-0.9195f,	0.0449f },
	{ 0.5825f,	-0.7992f,	0.1481f },
	{ 0.8664f,	-0.4898f,	0.0969f },
	{ 0.9046f,	0.1115f,	0.4114f },
	{ 0.9535f,	0.2323f,	0.1918f },
	{ 0.4973f,	0.7708f,	0.3982f },
	{ 0.1941f,	0.9563f,	0.2186f },
	{ 0.4229f,	0.8823f,	0.2068f },
	{ -0.3738f,	0.8496f,	0.3722f },
	{ -0.5345f,	0.7140f,	0.4522f },
	{ -0.8818f,	0.2372f,	0.4076f },
	{ -0.9049f,	-0.0141f,	0.4253f },
	{ -0.7518f,	-0.5128f,	0.4145f },
	{ -0.5010f,	-0.6979f,	0.5118f },
	{ -0.2352f,	-0.9259f,	0.2956f },
	{ 0.2290f,	-0.9539f,	0.1938f },
	{ 0.7340f,	-0.6349f,	0.2411f },
	{ 0.9138f,	-0.0633f,	-0.4013f },
	{ 0.9057f,	-0.1615f,	0.3919f },
	{ 0.8589f,	0.3424f,	0.3807f },
	{ 0.6245f,	0.6076f,	0.4908f },
	{ 0.2893f,	0.8575f,	0.4255f },
	{ 0.0700f,	0.9022f,	0.4257f },
	{ -0.2862f,	0.9407f,	0.1822f },
	{ -0.5740f,	0.8051f,	-0.1493f },
	{ 0.1113f,	0.0997f,	-0.9888f },
	{ -0.3054f,	-0.9442f,	-0.1232f },
	{ -0.6012f,	-0.7896f,	0.1232f },
	{ -0.2906f,	-0.8121f,	0.5059f },
	{ -0.0649f,	-0.8772f,	0.4758f },
	{ 0.4083f,	-0.8622f,	0.2998f },
	{ 0.5661f,	-0.7256f,	0.3913f },
	{ 0.8394f,	-0.4274f,	0.3359f },
	{ 0.8189f,	-0.0413f,	0.5724f },
	{ 0.7198f,	0.4150f,	0.5565f },
	{ 0.8817f,	0.4503f,	0.1407f },
	{ 0.4018f,	-0.8982f,	-0.1782f },
	{ -0.0540f,	0.7913f,	0.6090f },
	{ -0.2938f,	0.7640f,	0.5745f },
	{ -0.4508f,	0.6103f,	0.6514f },
	{ -0.6382f,	0.1867f,	0.7469f },
	{ -0.8729f,	-0.2571f,	0.4147f },
	{ -0.5873f,	-0.5217f,	0.6188f },
	{ -0.3537f,	-0.6420f,	0.6803f },
	{ 0.0416f,	-0.6113f,	0.7903f },
	{ 0.3483f,	-0.7792f,	0.5211f },
	{ 0.4992f,	-0.6224f,	0.6028f },
	{ 0.7900f,	-0.3038f,	0.5325f },
	{ 0.6601f,	0.0607f,	0.7487f },
	{ 0.6049f,	0.2942f,	0.7400f },
	{ 0.3857f,	0.3793f,	0.8410f },
	{ 0.2397f,	0.2079f,	0.9483f },
	{ 0.0126f,	0.2585f,	0.9659f },
	{ -0.1006f,	0.4571f,	0.8837f },
	{ 0.0470f,	0.6286f,	0.7763f },
	{ -0.4304f,	-0.4454f,	0.7851f },
	{ -0.4343f,	-0.1962f,	0.8791f },
	{ -0.2566f,	-0.3369f,	0.9059f },
	{ -0.1314f,	-0.1589f,	0.9785f },
	{ 0.1024f,	-0.2088f,	0.9726f },
	{ 0.1957f,	-0.4501f,	0.8713f },
	{ 0.6273f,	-0.4231f,	0.6538f },
	{ 0.6874f,	-0.1716f,	0.7057f },
	{ 0.2759f,	-0.0213f,	0.9609f },
	{ 0.4594f,	0.1575f,	0.8742f },
	{ 0.2854f,	0.5832f,	0.7606f },
	{ -0.8122f,	0.4603f,	0.3585f },
	{ -0.1891f,	0.6412f,	0.7437f },
	{ -0.3389f,	0.4765f,	0.8113f },
	{ -0.9210f,	0.3472f,	0.1767f },
	{ 0.0406f,	0.0245f,	0.9989f },
	{ -0.7391f,	-0.3537f,	0.5732f },
	{ -0.6035f,	-0.2866f,	0.7441f },
	{ -0.1887f,	-0.5471f,	0.8156f },
	{ -0.0260f,	-0.3978f,	0.9171f },
	{ 0.2679f,	-0.6490f,	0.7120f },
	{ 0.5182f,	-0.2849f,	0.8064f },
	{ 0.4935f,	-0.0665f,	0.8672f },
	{ -0.3282f,	0.1403f,	0.9341f },
	{ -0.3282f,	0.1403f,	0.9341f },
	{ -0.3282f,	0.1403f,	0.9341f },
	{ -0.3282f,	0.1403f,	0.9341f },
	{ -0.3282f,	0.1403f,	0.9341f },
};

D3DXVECTOR3* NormalTableDirectory[] = {
	nullptr,
	NormalTable1,
	NormalTable2,
	NormalTable3,
	NormalTable4,
};
