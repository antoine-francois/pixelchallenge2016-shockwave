Shader "Custom/MainShaderTransparentEmissive"
{
	Properties
	{
		_MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_NormalTex ("Normal", 2D) = "bump" {}
		_M_S_AOTex ("Metalness (R) Smoothness (G) AO (B)", 2D) = "white" {}
		_Emissive ("Emissive", 2D) = "black" {}
		_EmissiveColor ("Emissive Color", Color) = (0,0,0,1)
		_EmissivePower ("Emissive Power", float) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _M_S_AOTex;
		sampler2D _Emissive;
		float4 _EmissiveColor;
		float _EmissivePower;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_NormalTex;
			float2 uv_M_S_AOTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 albedo = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 normal = tex2D (_NormalTex, IN.uv_NormalTex);
			fixed4 msao = tex2D (_M_S_AOTex, IN.uv_M_S_AOTex);
			fixed4 emissive = tex2D (_Emissive, IN.uv_MainTex);
			
			o.Albedo = albedo.rgb;
			o.Normal = UnpackNormal( normal );
			o.Emission = emissive.x * _EmissiveColor * _EmissivePower;
			o.Metallic = msao.r;
			o.Smoothness = msao.g;
			o.Occlusion = msao.b;
			o.Alpha = albedo.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
