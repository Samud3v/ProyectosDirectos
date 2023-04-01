Shader "Custom/LimpiaShader"
{
    Properties
    {
        _MainTexOriginal ("Texture original", 2D) = "white" {}
        _MainTexModificada ("Texture modificada", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
    }

    SubShader{
        Tags {"RederType"="Opaque"}

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTexOriginal;
        sampler2D _MainTexModificada;
        sampler2D _Mask;

        struct Input
        {
            float2 uv_MainTexOriginal;
            float2 uv_MainTexModificada;
            float2 uv_Mask;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Mezcla de las dos texturas
            o.Albedo = lerp(tex2D(_MainTexOriginal, IN.uv_MainTexOriginal), tex2D(_MainTexModificada, IN.uv_MainTexModificada), 1.0 - tex2D(_Mask, IN.uv_Mask).r);

            // Establece la opacidad del objeto basado en la textura de máscara
            o.Alpha = tex2D(_Mask, IN.uv_Mask).r;
        }
        ENDCG


    }

    Fallback "Diffuse"
}
