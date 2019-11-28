Shader "Fy/Tilables" 
{
	Properties 
	{
		_MainTex ("Main texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}

	SubShader 
	{
		Cull Back
		Lighting Off
		ZWrite Off
		Tags { "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID				
			};

			v2f vert(appdata_full v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			void frag (v2f i, out half4 outDiffuse : SV_Target0) {
				UNITY_SETUP_INSTANCE_ID(i);
				outDiffuse = tex2D(_MainTex,  i.uv) * _Color;
			}

			ENDCG
		} 
	}
}