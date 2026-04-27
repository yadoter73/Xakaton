Shader "Custom/SpriteBillboardY"
{
    Properties
    {
        // Свойство _MainTex скрыто в инспекторе, так как SpriteRenderer сам передает сюда спрайт
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            fixed4 _Color;
            sampler2D _MainTex;

            v2f vert(appdata_t IN)
            {
                v2f OUT;

                // 1. Находим центр объекта в мире (позиция Transform'а)
                float3 centerWorld = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

                // 2. Вектор от центра спрайта к камере
                float3 forward = _WorldSpaceCameraPos - centerWorld;

                // МАГИЯ ЗДЕСЬ: Обнуляем Y. Это блокирует наклон "вперед/назад".
                // Спрайт будет вращаться только вокруг вертикальной оси.
                forward.y = 0;
                forward = normalize(forward);

                // 3. Задаем жесткий вектор "Вверх" и вычисляем вектор "Вправо"
                float3 up = float3(0, 1, 0); 
                float3 right = cross(up, forward);

                // 4. Учитываем масштаб (Scale) из Transform, чтобы спрайт можно было растягивать
                float scaleX = length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x));
                float scaleY = length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y));

                // 5. Пересобираем позицию вершин относительно камеры
                float3 newPos = centerWorld + right * (IN.vertex.x * scaleX) + up * (IN.vertex.y * scaleY);

                // 6. Переводим в экранные координаты
                OUT.vertex = mul(UNITY_MATRIX_VP, float4(newPos, 1.0));
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;

                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Читаем текстуру и умножаем на цвет из SpriteRenderer
                fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
                return c;
            }
            ENDCG
        }
    }
}