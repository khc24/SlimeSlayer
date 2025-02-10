// 1. Material �޴����� ���� �޴��̸�
// 2. Shader.Find�Լ����� ���� ���
Shader "MyShader/RGBShader"
{
    // �����Ϳ��� ���� �����ϴ� �뵵�� ����ϴ� 
    // ������ ����
    Properties
    { // �Ӽ��̸�     �ν����Ϳ� ��µ� �̸�    ������ Ÿ��       �ʱⰪ
        _Color      ("Color",                   Color)          = (1,1,1,1)
        _MainTex    ("Albedo (RGB)",            2D)             = "white" {}
        _R ("R", Range(0,1)) = 0
        _G ("G", Range(0,1)) = 0
        _B ("B", Range(0,1)) = 0
        _A ("A", Range(0,1)) = 0
    }
    // �ڵ��� ����
    SubShader
    {
        // �켱�� �Ǵ� �⺻ ����
        Tags
        {
            // �켱���� ����
            "Queue" = "Transparent"
            // �������� ��� ��Ȱ��ȭ
            "IgnoreProjector" = "True"
            // �׷����� Ÿ�Լ���
            "RenderType" = "Transparent"
            // ������ �����쿡�� �����ִ� ���
            "PreviewType" = "Plane"
            // ��Ʋ�� ��� ����
            "CanUseSpriteAtlas" = "True"
        }

        // �ø���� ������� �ʰڴ� ( �ո�� �޸��� ��� �׸��ڴ�. )
        Cull Off
        // ���� ��� ����
        Lighting Off

        // ���� ���� ��� ����
        // ���� ������ ����ó���� �ǰ�, ������ ������ ���̴�
        // �����Ͷ������� ���ؼ� �ȼ��� ����ó���� �˴ϴ�.
        // �ȼ��� ���� ���� ���� �ǰ�, �� ���� ���� Ȱ���ؼ�
        // ��¿� ���� �켱������ �����ǰ� �ȴ�.

        // ���� ���۸� ������� �ʰڴ�.
        ZWrite Off

        Blend  One  OneMinusSrcAlpha

        // Level Of Detail
        // �Ÿ��� ���� ��µǴ� ����� ����
        LOD 200

        // CG Program�� ������ ENDCG�� �ѽ����� ����.
        CGPROGRAM
        
        // SURFACE�� ����ϴ� �Լ��� �̸��� surf
        // ������ ����ϴ� �Լ��� �̸��� Standard 
        // �������� �̸��� LightingStandard
        // �׸��ڸ� ����ϴ� �Լ��� �̸��� fullforwardshadows 
        // ���Ŀ����� ó���ϰ�, ���ɿ� ���� ��������� �����ϰڴ�.!
        #pragma  surface surf Lambert alpha:auto

        // 3.0 ���̴������� �����.
        #pragma target 3.0

        // sampler2D : 2D �ؽ�ó Ÿ��
        // fixed(9��Ʈ), half(16��Ʈ), float (32��Ʈ)
        sampler2D _MainTex;
        half4 _Color;
        float _R;
        float _G;
        float _B;
        float _A;

        struct Input
        {
            float2 uv_MainTex;
        };

        // CG���α׷����� �ۼ��ϴ� ���α׷����
        // ǥ���� ����ϴ� �Լ��� ȣ���� ���Ŀ�
        // ������ ����ϴ� �Լ��� ȣ��˴ϴ�.
        // Albedo���� ������ �־��ָ�
        // ������ ����ϴ� �Լ����� �� ���� Ȱ���ؼ�
        // ��� ��ο� ���� ������ �����ϰ� �ȴ�.
        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            // �ؽ�ó�� �ؽ�ó�� ��ǥ���� �־��ָ� 
            // �ؽ�ó ��ǥ�� �´� �ȼ��� ������ �Լ�.
            // 0.3 * 0.3 = ��������ó���� ���� ������ ��ο�����.
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            // ���� ������� ���� ����
            o.Emission = c + float3(_R, _G, _B);
            // ���� ����� ����
            //o.Albedo = c.rgb;
            o.Alpha = c.a *_A;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
