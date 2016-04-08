Shader "Custom/WaterShader"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Depth("Depth", Float) = 1
	}
	SubShader
	{
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
				
		CGPROGRAM
		#pragma surface surf Standard alpha:auto
		#pragma target 3.0

		float4 _Color;
		float _Depth;
		sampler2D _CameraDepthTexture;
		sampler2D _ScreenContent;

		struct Input
		{
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o)
		{

			half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
			depth = LinearEyeDepth(depth);
			
            float waterDepth = 1-(saturate((depth - IN.screenPos.z)/ _Depth));

			o.Emission = _Color.xyz * waterDepth;
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Alpha = waterDepth;
		}
		ENDCG
	}
}
