Shader "Unlit/portalShader"
{
    Properties{
        _MainTex("Main Texture", 2D) = "white" {}
        _MaskTex("Mask Texture", 2D) = "white" {}
        _TimeScale("Time Scale", Range(0, 10)) = 1
        _RotationFactor("Rotation Factor", Range(0, 50)) = 0.2
        _BandFrequency("Band Frequency", Range(0, 10)) = 1
        _BandAmplitude("Band Amplitude", Range(0, 1)) = 0.2
    }

        SubShader{
            Tags { "RenderType" = "Transparent" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                // Vertex shader inputs
                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

        // Vertex shader outputs
        struct v2f {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        // Fragment shader inputs
        sampler2D _MainTex;
        sampler2D _MaskTex;
        float _TimeScale;
        float _RotationFactor;
        float _BandFrequency;
        float _BandAmplitude;

        // Sprite shape properties
        float4 _MainTex_ST;
        float4 _MaskTex_ST;
        float4 _MainTex_TexelSize;
        float4 _MaskTex_TexelSize;

        // Vertex shader
        v2f vert(appdata v) {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;

            return o;
        }

        // Fragment shader
        fixed4 frag(v2f i) : SV_Target {
            // Get the sprite shape mask
            fixed4 maskColor = tex2D(_MaskTex, i.uv * _MaskTex_ST.xy + _MaskTex_ST.zw);

        // Discard fragments outside the mask shape
        if (maskColor.a < 0.5) {
            discard;
        }

        // Sample the main texture using polar coordinates
        float2 polarCoords = float2(length(i.uv - 0.5), atan2(i.uv.y - 0.5, i.uv.x - 0.5));
        float2 swirlCoords = polarCoords + float2(_TimeScale * _Time.y * 0.1, pow(polarCoords.x, 0.75) * 0.1);
        swirlCoords.x = pow(swirlCoords.x, 0.75);

        // Add sinusoidal function to create bands
        swirlCoords.x += _BandAmplitude * sin(_BandFrequency * swirlCoords.y + _TimeScale * _Time.y * 0.1);

        swirlCoords.y *= 4.0;
        swirlCoords.y += _TimeScale * _Time.y * 0.1 * _RotationFactor; // Add rotation effect
        float2 uv = 0.5 + swirlCoords.x * float2(cos(swirlCoords.y), sin(swirlCoords.y));
        fixed4 mainColor = tex2D(_MainTex, uv * _MainTex_ST.xy + _MainTex_ST.zw);

        return mainColor;
    }

    ENDCG
}
        }

            FallBack "Diffuse"
}
