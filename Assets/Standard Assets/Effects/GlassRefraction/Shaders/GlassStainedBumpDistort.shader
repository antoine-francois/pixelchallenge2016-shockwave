Shader "FX/Glass/Stained BumpDistort"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Displace("Displace", Range(0,128)) = 1
		_BumpMap ("Normalmap", 2D) = "bump" {}
	}

Category
{
	Tags { "Queue"="Transparent" "RenderType"="Opaque" }

	SubShader
	{
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
				float2 texcoord: TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 uvgrab : TEXCOORD0;
				float2 uvbump : TEXCOORD1;
			};

			float _Displace;
			float4 _BumpMap_ST;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
				o.uvgrab.zw = o.vertex.zw;
				o.uvbump = TRANSFORM_TEX( v.texcoord, _BumpMap );
				return o;
			}

			sampler2D _ScreenContent;
			float4 _ScreenContent_TexelSize;
			sampler2D _BumpMap;
			float4 _Color;

			half4 frag (v2f i) : SV_Target
			{
				half2 bump = UnpackNormal(tex2D( _BumpMap, i.uvbump )).rg;
				float2 offset = bump * _Displace * _ScreenContent_TexelSize.xy;
				i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;
	
				half4 col = tex2Dproj( _ScreenContent, UNITY_PROJ_COORD(i.uvgrab));
				col *= _Color;
				return col;
			}
			ENDCG
		}
	}
}

}
