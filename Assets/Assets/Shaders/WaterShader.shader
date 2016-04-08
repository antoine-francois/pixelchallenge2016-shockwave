Shader "Custom/WaterShader"
{
	Properties
	{
		_DeepColor ("Deep Color", Color) = (1,1,1,1)
		_ShallowColor ("Shallow Color", Color) = (1,1,1,1)
		_Depth("Depth", Float) = 1
		_FoamMaskTex ("Mask (R) Foam (G)", 2D) = "black" {}
		_FoamDistance("Foam Distance", Float) = 1
		_FoamIntensity("Foam Intensity", Float) = 1
		_MaskIntensity("Mask Intensity", Range(0,1)) = 1
		_Bump ("Normal", 2D) = "bump" {}
		_Refraction("Refraction Intensity", Range(0,1)) = 1
		_Glossiness("Glossiness", Float) = 1
		_SpeedScale1("1 - Speed (xy) Scale (zw)", Vector) = (0,0,1,1)
		_SpeedScale2("2 - Speed (xy) Scale (zw)", Vector) = (0,0,1,1)
		_SpeedScale3("Foam - Speed (xy) Scale (zw)", Vector) = (0,0,1,1)
	}
	SubShader
	{
		GrabPass { "_ScreenContent" }

        Tags { "Queue" = "Geometry+10" "RenderType"="Opaque" }
				
		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0

		float4 _DeepColor;
		float4 _ShallowColor;
		float _Depth;
		sampler2D _FoamMaskTex;
		float _FoamDistance;
		float _FoamIntensity;
		float _MaskIntensity;
		sampler2D _Bump;
		float _Refraction;
		float _Glossiness;
		float4 _SpeedScale1;
		float4 _SpeedScale2;
		float4 _SpeedScale3;
		sampler2D _CameraDepthTexture;
		sampler2D _ScreenContent;

		struct Input
		{
			float2 uv_Bump;
			float2 uv_FoamMaskTex;
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			float4 bump1 = tex2D(_Bump, IN.uv_Bump * _SpeedScale1.zw + _Time.w * _SpeedScale1.xy);
			float4 bump2 = tex2D(_Bump, IN.uv_Bump * _SpeedScale2.zw + _Time.w * _SpeedScale2.xy);
			float mask = tex2D(_FoamMaskTex, IN.uv_FoamMaskTex).r;
			float foam = tex2D(_FoamMaskTex, IN.uv_FoamMaskTex * _SpeedScale3.zw + _Time.w * _SpeedScale3.xy).g;

			half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
			depth = LinearEyeDepth(depth);
			
            float waterDepth = (saturate((depth - IN.screenPos.z)/ _Depth));
            float foamDepth = 1-(saturate((depth - IN.screenPos.z)/ _FoamDistance));

			float4 color = lerp( _ShallowColor, _DeepColor, waterDepth );

			fixed3 bump = UnpackNormal((bump1 + bump2)/2.0f);
			bump = lerp(bump, fixed3(0,0,1), mask * _MaskIntensity);
			bump = lerp(bump, fixed3(0,0,1), 1-waterDepth);

			float2 grabTexcoord = IN.screenPos.xy / IN.screenPos.w + bump.xy * _Refraction;
			float4 screenContent = tex2D( _ScreenContent, grabTexcoord );

			o.Emission = lerp( screenContent, color + foam * foamDepth * _FoamIntensity, waterDepth ).xyz;
			o.Normal = bump;
			o.Metallic = 0;
			o.Smoothness = waterDepth * _Glossiness;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
