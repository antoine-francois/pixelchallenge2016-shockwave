#ifndef GRASS_HELPERS_INCLUDED
#define GRASS_HELPERS_INCLUDED

#include "UnityCG.cginc"
#include "AutoLight.cginc"

sampler2D _MainTex;
float4 _MainTex_ST;
float _Length;

struct vertInput
{
	float4 pos : POSITION;
	float4 nor : NORMAL;
	float4 uv : TEXCOORD0;
};

struct fragInput
{
	float4 pos : SV_POSITION;
	float3 uv_y : TEXCOORD0;
	LIGHTING_COORDS(1,2)
	UNITY_FOG_COORDS(3)
};

fragInput vert( vertInput i )
{
	fragInput o;

		// world

	float4 wPos =  mul( _Object2World, i.pos );
	float4 wNor =  normalize( mul( _Object2World, float4( i.nor.xyz, 0.0f ) ) );

		// position

	float4 pos = wPos;
	pos += wNor * _Length * CURRENT_MULTIPLIER;

		// uvs

	float2 uvs = TRANSFORM_TEX( i.uv.xy, _MainTex );

		// set

	o.pos = mul( UNITY_MATRIX_VP, pos );
	o.uv_y = float3( uvs, wPos.y );

	TRANSFER_VERTEX_TO_FRAGMENT(o);
	UNITY_TRANSFER_FOG(o,o.pos);

	return o;
}

float4 frag ( fragInput i ) : COLOR
{
	float4 o = tex2D(_MainTex, i.uv_y.xy);
	fixed atten = LIGHT_ATTENUATION(i);
	o.rgb *= max( CURRENT_MULTIPLIER * atten, .2f );
	UNITY_APPLY_FOG(i.fogCoord, o);

	return o;
}

#endif
