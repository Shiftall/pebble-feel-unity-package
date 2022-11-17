Shader "PebbleFeel/PixelsOverlay"
{
    Properties{
        _PixelColor("Pixel Color", Color) = (0.0,0.0,0.0,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "DisableBatching" = "True"
            "IgnoreProjector" = "True"
        }

        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        Cull off
        ZWrite on
        AlphaToMask on

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #include "PixelsOverlay.cginc"
            ENDCG
        }
    }
    Fallback "Unlit/Transparent"
}