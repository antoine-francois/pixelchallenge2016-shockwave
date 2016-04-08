Shader "Unlit/ShockWaveIntersect"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Depth("Depth", Float) = 1
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD0;
			};

			float4 _Color;
			float _Depth;
			sampler2D _CameraDepthTexture;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
				depth = LinearEyeDepth(depth);
			
				float waterDepth = 1-(saturate((depth - i.screenPos.z)/ _Depth));

				fixed4 o = _Color;
				o.a *= waterDepth;

				return o;
			}
			ENDCG
		}
		
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Back

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
				float4 screenPos : TEXCOORD0;
				float fresnel : TEXCOORD1;
			};

			float4 _Color;
			float _Depth;
			sampler2D _CameraDepthTexture;
			
			v2f vert (appdata v)
			{
				float3 N = mul(_Object2World, normalize(float4(v.normal.xyz, 0.0f))).xyz;

				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				
				float3 viewDir = normalize( ObjSpaceViewDir ( v.vertex ) );
				o.fresnel = 1-sin(saturate ( dot ( v.normal, viewDir ) )*1.57079632f);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
				depth = LinearEyeDepth(depth);
			
				float waterDepth = 1-(saturate((depth - i.screenPos.z)/ _Depth));

				fixed4 o = _Color;
				o.a *= saturate( waterDepth + i.fresnel );

				return o;
			}
			ENDCG
		}
	}
}
