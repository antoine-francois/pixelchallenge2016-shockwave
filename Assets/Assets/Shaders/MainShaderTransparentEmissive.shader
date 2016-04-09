﻿Shader "Custom/MainShaderTransparentEmissive"
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
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

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
			};

			fragInput vert( vertInput i )
			{
				fragInput o;

				o.pos = mul( UNITY_MATRIX_MVP, i.pos );
				o.uv = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw * _Time.w;

				return o;
			}

			float4 frag ( fragInput i ) : COLOR
			{
				float4 o = tex2D(_MainTex, i.uv);

				o.rgb *= _EmissiveColor * _EmissivePower;

				return o;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
