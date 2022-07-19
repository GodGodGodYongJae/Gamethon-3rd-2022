Shader "Custom/fill2"
{
Properties{
		_Color("Tint Color", Color) = (1,1,1,1)
		_ShadowTex("Cookie", 2D) = "gray" {}
	}
	Subshader
	{
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
		Pass
		{
			ZWrite Off
			// Premultiplied alpha
			Blend One OneMinusSrcAlpha
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;

			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul(unity_Projector, vertex);
				return o;
			}

			// This texture contains the decal camera's output
			sampler2D _ShadowTex;
			fixed4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 texCookie = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				fixed4 outColor = _Color * texCookie;
				
				// This tweak is needed to get around gamma color space issues;
				// if you're using linear you probably don't need it
				outColor.a = sqrt(outColor.a);
				return outColor * outColor.a;
			}
			ENDCG
		}
	}
}
