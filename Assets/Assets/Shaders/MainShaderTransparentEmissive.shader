Shader "Custom/MainShaderTransparentEmissive"
{
	Properties
	{
		_MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_EmissiveColor ("Emissive Color", Color) = (0,0,0,1)
		_EmissivePower ("Emissive Power", float) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }

		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _EmissiveColor;
			float _EmissivePower;

			struct vertInput
			{
				float4 pos : POSITION;
				float4 uv : TEXCOORD0;
			};

			struct fragInput
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			fragInput vert( vertInput i )
			{
				fragInput o;

				o.pos = mul( UNITY_MATRIX_MVP, i.pos );
				o.uv = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw * _Time.w;
				UNITY_TRANSFER_FOG(o,o.pos);

				return o;
			}

			float4 frag ( fragInput i ) : COLOR
			{
				float4 o = tex2D(_MainTex, i.uv);

				o.rgb *= _EmissiveColor * _EmissivePower;
				o.a = 1.0f;
				UNITY_APPLY_FOG(i.fogCoord, o);

				return o;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
