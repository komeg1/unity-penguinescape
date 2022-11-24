Shader "Custom/My Sprite Normalmap Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _BumpMap("Normalmap", 2D) = "bump" {}
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque"
            "IgnoreProjector" = "True"
            "RenderType" = "TransparentCutOut"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        LOD 200
        //Cull front to render BackFace
        Cull Front
        Lighting On
        ZWrite Off
        Fog { Mode Off }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha vertex:vert addshadow alphatest:_Cutoff 
        #pragma multi_compile DUMMY PIXELSNAP_ON 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        fixed4 _Color;
        float _ScaleX;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            fixed4 color : COLOR;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v, out Input o)
        {
            #if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
            v.vertex = UnityPixelSnap(v.vertex);
            #endif
            float3 normal = v.normal;

            v.normal = float3(0, 0, -1);
            v.tangent = float4(1, 0, 0, -1);

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color += _Color;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * IN.color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
        }
        ENDCG

            //Now FrontFaces
            
            Cull Back
            Lighting On
            ZWrite Off
            Fog{ Mode Off }

                CGPROGRAM
                // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Lambert alpha vertex:vert addshadow alphatest:_Cutoff 
            #pragma multi_compile DUMMY PIXELSNAP_ON 
            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            sampler2D _BumpMap;
            fixed4 _Color;
            float _ScaleX;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_BumpMap;
                fixed4 color : COLOR;
            };

            void vert(inout appdata_full v, out Input o)
            {
                #if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
                v.vertex = UnityPixelSnap(v.vertex);
                #endif
                float3 normal = v.normal;

                v.normal = float3(0, 0, -1);
                v.tangent = float4(1, 0, 0, -1);

                UNITY_INITIALIZE_OUTPUT(Input, o);
                o.color += _Color;
            }

            void surf(Input IN, inout SurfaceOutput o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            }
            ENDCG
    }
    FallBack "Diffuse"
}
