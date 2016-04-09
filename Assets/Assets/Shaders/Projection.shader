Shader "Unlit/ProjectionShader"
{
	Properties
	{
		 _ProjectedTexture ("Projected Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass
		{
			Cull Front
			Blend SrcAlpha OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 texcoord : TEXCOORD0;
				float3 normal : TEXCOORD1;
			};

			sampler2D _ProjectedTexture;
			float4x4 _Projector;
			float4 _Color;
			
			v2f vert (appdata i)
			{
				v2f o;
				
				o.vertex.xy = i.texcoord.xy * 2.0 - 1.0 * float2( 1, 1 );
				o.vertex.z = 0;
				o.vertex.w = i.vertex.w;
				
				o.texcoord = mul(_Projector, i.vertex);
				
				o.normal = i.normal.xyz;
				
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				clip( dot( i.normal.xyz, _Projector[2].xyz ) < 0 ? 1 : -1 );
				float4 col = tex2Dproj(_ProjectedTexture, UNITY_PROJ_COORD(i.texcoord));
							
				return float4( col.a, col.a, col.a, col.a );
			}
			ENDCG
		}
	}
}
