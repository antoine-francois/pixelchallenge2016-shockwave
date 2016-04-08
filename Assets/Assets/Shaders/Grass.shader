Shader "Custom/Grass"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Length ("Length", Range(0,2)) = 1

	}
	SubShader
	{
		//Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 100

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.0
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.05
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.1
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.15
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.2
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.25
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.3
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.35
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.4
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.45
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.5
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.55
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.6
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.65
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.7
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.75
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.8
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.85
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.9
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 0.95
			#include "GrassHelper.cginc"
			ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#define CURRENT_MULTIPLIER 1.0
			#include "GrassHelper.cginc"
			ENDCG
		}
	}
	Fallback "VertexLit"
}
