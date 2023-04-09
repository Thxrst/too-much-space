Shader "Unlit/portalShader"
{
    Properties{
        _MainTex("Main Texture", 2D) = "white" {}
        _MaskTex("Mask Texture", 2D) = "white" {}
        _TimeScale("Time Scale", Range(0, 10)) = 1
        _RotationSpeed("Rotation Speed", Range(-10, 10)) = 0
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _MaskTex;
                float4 _MainTex_ST;
                float4 _MaskTex_ST;
                float _TimeScale;
                float _RotationSpeed;

                v2f vert(appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    // Get the sprite shape mask
                    fixed4 maskColor = tex2D(_MaskTex, i.uv * _MaskTex_ST.xy + _MaskTex_ST.zw);

                // Discard fragments outside the mask shape
                if (maskColor.a < 0.5) {
                    discard;
                }

                // Rotate the main texture around its center pivot
                float2 originalUV = i.uv - 0.5;
                float2 rotatedUV = float2(
                    originalUV.x * cos(_RotationSpeed * _Time.y) - originalUV.y * sin(_RotationSpeed * _Time.y),
                    originalUV.x * sin(_RotationSpeed * _Time.y) + originalUV.y * cos(_RotationSpeed * _Time.y)
                );
                rotatedUV += 0.5;
                fixed4 mainColor = tex2D(_MainTex, rotatedUV * _MainTex_ST.xy + _MainTex_ST.zw);

                return mainColor;
            }

            ENDCG
        }
        }
            FallBack "Diffuse"
}