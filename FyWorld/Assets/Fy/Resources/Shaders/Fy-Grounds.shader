Shader "Fy/Grounds" 
{
	Properties 
	{
		_MainTex ("Main texture", 2D) = "white" {}
	}

	SubShader 
	{
		Cull Back
		Lighting Off
		ZWrite Off
		Tags { "Queue"="Transparent" }
		Blend One OneMinusSrcAlpha
		LOD 200

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;

			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color: COLOR; 
			};

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color;


				//
				o.uv = v.vertex.xy/4;
				return o;
			}

			half4 frag (v2f i) : COLOR {
				half4 texcol = tex2D(_MainTex, i.uv.xy) * i.color.a;
				return  texcol;
			}

			ENDCG
		} 
	}
}