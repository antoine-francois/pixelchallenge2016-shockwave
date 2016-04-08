Shader "Unlit/ShockWaveDistort"
{
	Properties
	{
		_Displace("Displace", Range(0,1)) = 1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent+10" "RenderType"="Opaque" }

		GrabPass { "_ScreenContent" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 uvgrab : TEXCOORD0;
			};

			float _Displace;
			float _Power;
			sampler2D _ScreenContent;
			float4 _ScreenContent_TexelSize;

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float3 N = mul(_Object2World, normalize(float4(v.normal.xyz, 0.0f))).xyz;

				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif

				float4 pos = mul( UNITY_MATRIX_MVP, v.vertex - float4( v.normal.xyz, 0 ) * _Displace * dot( WorldSpaceViewDir(v.vertex), N ) * 0.001f );

				o.uvgrab.xy = (float2(pos.x, pos.y * scale) + pos.w) * 0.5f;
				o.uvgrab.zw = pos.zw;

				return o;
			}

			half4 frag (v2f i) : SV_Target
			{	
				half4 col = tex2Dproj( _ScreenContent, UNITY_PROJ_COORD(i.uvgrab));

				return col;
			}
			ENDCG
		}
	}
}
