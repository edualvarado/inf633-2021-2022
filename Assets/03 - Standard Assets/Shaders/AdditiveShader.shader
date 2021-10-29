// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Projector/Add"
{
	Properties { 
		_Tex ("Texture", 2D) = "gray" {}
	}
	 
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend One One
			Offset -1, -1
			 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			 
			struct v2f {
				float4 uvTex : TEXCOORD0;
				float4 pos : SV_POSITION;
			};
			 
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			 
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (vertex);
				o.uvTex = mul (unity_Projector, vertex);
				return o;
			}
			
			sampler2D _Tex;
			 
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 texS = tex2Dproj (_Tex, UNITY_PROJ_COORD(i.uvTex));
				return texS * fixed4(1, 0, 0, 1);
			}
			ENDCG
		}
	} 
}