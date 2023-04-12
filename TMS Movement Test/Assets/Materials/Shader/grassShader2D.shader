Shader "Unlit/grassShader2D"
{
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _WindStrength("Wind Strength", Range(0,1)) = 0.5
        _WindSpeed("Wind Speed", Range(0,10)) = 2
        _WindDirection("Wind Direction", Vector) = (1,0,0,0)
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "RenderType" = "Transparent"  }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float4 _WindDirection;
                float _WindStrength;
                float _WindSpeed;
                float _TimeOffset;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;

                    // Calculate the wind effect on the vertex position
                    float2 windOffset = sin((_Time.y + _TimeOffset) * _WindSpeed + v.vertex.x * _WindDirection.x) * _WindStrength;
                    o.vertex.xy += windOffset;

                    return o;
                }

                sampler2D _MainTex;

                float4 frag(v2f i) : SV_Target {
                    return tex2D(_MainTex, i.uv);
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}