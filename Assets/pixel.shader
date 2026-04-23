Shader "Hidden/RetroDitherFullscreen"
{
    Properties
    {
        // Сколько оттенков цвета будет (чем меньше, тем жестче дизеринг)
        _ColorSteps ("Color Steps", Float) = 32.0 
        _DitherStrength ("Dither Strength", Range(0, 2)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off ZTest Always

        Pass
        {
            Name "DitherPass"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            
            // Подключаем системные файлы URP для полноэкранных эффектов
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _ColorSteps;
            float _DitherStrength;

            // Классическая матрица Байера 4x4 для PS1-дизеринга
            static const float ditherMatrix[16] = {
                 0.0/16.0,  8.0/16.0,  2.0/16.0, 10.0/16.0,
                12.0/16.0,  4.0/16.0, 14.0/16.0,  6.0/16.0,
                 3.0/16.0, 11.0/16.0,  1.0/16.0,  9.0/16.0,
                15.0/16.0,  7.0/16.0, 13.0/16.0,  5.0/16.0
            };

            half4 Frag(Varyings input) : SV_Target
            {
                // Получаем координаты экрана
                float2 uv = input.texcoord;
                
                // Берем оригинальный цвет пикселя с экрана
                half4 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv);

                // Вычисляем точную позицию пикселя на экране
                float2 pixelPos = uv * _ScreenParams.xy;

                // Находим нужный индекс в матрице Байера (повторяется каждые 4 пикселя)
                int x = int(pixelPos.x) % 4;
                int y = int(pixelPos.y) % 4;
                
                // Смещаем значение дизеринга, чтобы оно балансировало вокруг нуля (-0.5 до 0.5)
                float ditherValue = ditherMatrix[y * 4 + x] - 0.5;

                // Применяем дизеринг перед квантованием (урезанием) цветов
                col.rgb += ditherValue * _DitherStrength * (1.0 / _ColorSteps);

                // Квантование (создаем эффект 8-бит / 16-бит графики)
                col.rgb = floor(col.rgb * _ColorSteps) / _ColorSteps;

                return col;
            }
            ENDHLSL
        }
    }
}