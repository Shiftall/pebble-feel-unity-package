#include "UnityCG.cginc"

struct v2f {
    float2 uv : TEXCOORD0;
};

v2f vert (
    float4 vertex : POSITION,
    float2 uv : TEXCOORD0,
    out float4 outpos : SV_POSITION
    )
{
    v2f o;
    o.uv = uv;
    outpos = UnityObjectToClipPos(vertex);
    return o;
}

// color for signal pixel
fixed4 _PixelColor;

fixed4 frag (UNITY_VPOS_TYPE pos : VPOS) : SV_Target {
    int2 s = round(_ScreenParams.xy);
    uint size = 8;
    uint halfSize = size / 2;

    pos.xy = round(pos.xy);
    uint hCenter = s.x / 2;
    uint top = s.y - (s.y * 0.03);
    
    // Finder Signals
    if (pos.x > hCenter.x - halfSize && pos.x <= hCenter + halfSize)
    {
        uint bottom = s.y * 0.03;
        // top
        if(pos.y > top - halfSize && pos.y <= top + halfSize)
        {
            return fixed4(0.0, 0.0, 0.0, 1.0);
        }
        // bottom
        if(pos.y > bottom - halfSize && pos.y <= bottom + halfSize)
        {
            return fixed4(1.0, 1.0, 1.0, 1.0);
        }
    }

    // Signal
    uint sX = hCenter + s.x * 0.04;
    if (pos.x > sX - halfSize && pos.x <= sX + halfSize)
    {
        if(pos.y > top - halfSize && pos.y <= top + halfSize)
        {
            return _PixelColor;
        }
    }
    
    clip(-1);
    return fixed4(0,0,0,0);
}