Shader "Unlit/ScrollShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Direction ("Scroll Direction", Vector)  = (1, 0, 0, 0)
        _Speed ("Scroll Speed", Float) = 1
        _Color ("Tint", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnorePojector"="True"
            "CanUseSpriteAtlas"="True"
            
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Direction;
            float _Speed;
            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex :  SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 dir = normalize(_Direction.xy);
                float2 scrolledUV = i.uv + dir * _Speed * _Time.y;

                scrolledUV = frac(scrolledUV);
                
                fixed4 col = tex2D(_MainTex, scrolledUV) * _Color;
                return col;
            }
            ENDCG
        }
    }
}
