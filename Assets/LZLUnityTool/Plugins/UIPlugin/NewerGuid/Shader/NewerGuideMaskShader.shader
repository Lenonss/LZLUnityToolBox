Shader "Hidden/NewerGuideMaskShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Type("镂空类型",range(0,1)) =0//[0,0.5]:circle   (0.5,1]:rengle
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        Tags
        {
            "Queue"="Transparent"
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float2 _Center;
            float _EdgeSlider;
            float _Type;
            float _Length;
            float4 _Lengths;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPosition : TEXCOORD1;
                float4 color : COLOR0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPosition = v.vertex;
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv)*i.color;
                if (_Type<=0.5f)
                {
                    col.a *= (distance(i.worldPosition.xy,_Center) > _EdgeSlider);
                }
                else
                {
                    //top down left right
                    float4 edge = float4(_Center.y + _Lengths.x,_Center.y-_Lengths.y
                        ,_Center.x-_Lengths.z,_Center.x+_Lengths.w);
                    if (i.worldPosition.y <= edge.x && i.worldPosition.y >= edge.y
                        && i.worldPosition.x >= edge.z && i.worldPosition.x <= edge.w)
                    {
                        col.a = 0;
                    }
                }
                return col;
            }
            ENDCG
        }
    }
}
