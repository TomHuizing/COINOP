Shader "Custom/GlowingLine"
{
    Properties
    {
        _Color ("Line Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (1,0,0,1)
        _LineWidth ("Line Width", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Tags { "LightMode" = "Always" }
            Cull Front
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };
            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            float _LineWidth;
            float4 _Color;
            float4 _EmissionColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = v.vertex;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _EmissionColor;
            }
            ENDCG
        }

    }
    FallBack "Unlit/Color"
}
