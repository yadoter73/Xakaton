Shader "UI/LiquidGlass_URP"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Тонировка стекла (Tint Color)", Color) = (0.5, 0.5, 0.5, 0.5)
        _DistortionTex ("Текстура искажения (Normal Map/Noise)", 2D) = "grey" {}
        _DistortionStrength ("Сила 'жидкого' искажения", Range(0, 0.1)) = 0.015
        _BlurSize ("Сила размытия (Blur)", Range(0, 0.05)) = 0.01
    }
    
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
            "RenderPipeline"="UniversalPipeline" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        
        Cull Off 
        ZWrite Off 
        ZTest [unity_GUIZTestMode] 
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _DistortionTex;
            
            TEXTURE2D(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float _DistortionStrength;
                float _BlurSize;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                output.color = input.color;
                output.screenPos = ComputeScreenPos(output.positionCS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                float2 screenUV = input.screenPos.xy / input.screenPos.w;

                // Искажение
                float2 distortion = (tex2D(_DistortionTex, input.uv * 2.0).rg - 0.5) * 2.0;
                screenUV += distortion * _DistortionStrength;

                // Размытие
                half3 blurredColor = half3(0, 0, 0);
                float offset = _BlurSize;
                
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(-offset, -offset)).rgb;
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(0, -offset)).rgb;
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(offset, -offset)).rgb;
                
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(-offset, 0)).rgb;
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV).rgb;
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(offset, 0)).rgb;
                
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(-offset, offset)).rgb;
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(0, offset)).rgb;
                blurredColor += SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV + float2(offset, offset)).rgb;
                
                blurredColor /= 9.0;

                // Получаем цвет из компонента Image (например, если мы хотим анимировать прозрачность всего окна)
                half4 uiColor = tex2D(_MainTex, input.uv) * input.color;
                
                // Накладываем цвет тонировки на размытый фон. Чем больше _Color.a, тем сильнее цвет перекрывает фон.
                half3 finalColor = lerp(blurredColor, _Color.rgb, _Color.a);

                // Возвращаем результат. Альфа берется от компонента Image, чтобы окно могло плавно исчезать
                return half4(finalColor, uiColor.a);
            }
            ENDHLSL
        }
    }
}