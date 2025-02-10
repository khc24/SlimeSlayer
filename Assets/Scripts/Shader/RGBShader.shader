// 1. Material 메뉴에서 사용될 메뉴이름
// 2. Shader.Find함수에서 사용될 경로
Shader "MyShader/RGBShader"
{
    // 에디터에서 값을 제어하는 용도로 사용하는 
    // 에디터 변수
    Properties
    { // 속성이름     인스펙터에 출력될 이름    데이터 타입       초기값
        _Color      ("Color",                   Color)          = (1,1,1,1)
        _MainTex    ("Albedo (RGB)",            2D)             = "white" {}
        _R ("R", Range(0,1)) = 0
        _G ("G", Range(0,1)) = 0
        _B ("B", Range(0,1)) = 0
        _A ("A", Range(0,1)) = 0
    }
    // 코드의 시점
    SubShader
    {
        // 우선이 되는 기본 설정
        Tags
        {
            // 우선순위 설정
            "Queue" = "Transparent"
            // 프로젝터 기능 비활성화
            "IgnoreProjector" = "True"
            // 그려지는 타입설정
            "RenderType" = "Transparent"
            // 프리뷰 윈도우에서 보여주는 양식
            "PreviewType" = "Plane"
            // 아틀라스 사용 여부
            "CanUseSpriteAtlas" = "True"
        }

        // 컬링모드 사용하지 않겠다 ( 앞면과 뒷면을 모두 그리겠다. )
        Cull Off
        // 광원 사용 여부
        Lighting Off

        // 깊이 버퍼 사용 여부
        // 정점 단위로 연산처리가 되고, 정점과 정점의 사이는
        // 레스터라이저에 의해서 픽셀로 보간처리가 됩니다.
        // 픽셀은 깊이 값을 갖게 되고, 이 깊이 값을 활용해서
        // 출력에 대한 우선순위가 지정되게 된다.

        // 깊이 버퍼를 사용하지 않겠다.
        ZWrite Off

        Blend  One  OneMinusSrcAlpha

        // Level Of Detail
        // 거리에 따라서 출력되는 방식을 지정
        LOD 200

        // CG Program의 시작점 ENDCG와 한쌍으로 사용됨.
        CGPROGRAM
        
        // SURFACE를 계산하는 함수의 이름은 surf
        // 광원을 계산하는 함수의 이름은 Standard 
        // 실질적인 이름은 LightingStandard
        // 그림자를 계산하는 함수의 이름은 fullforwardshadows 
        // 알파연산을 처리하고, 성능에 따라 지원기능을 설정하겠다.!
        #pragma  surface surf Lambert alpha:auto

        // 3.0 셰이더버전을 사용함.
        #pragma target 3.0

        // sampler2D : 2D 텍스처 타입
        // fixed(9비트), half(16비트), float (32비트)
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

        // CG프로그램에서 작성하는 프로그램방법
        // 표면을 계산하는 함수를 호출한 이후에
        // 광원을 계산하는 함수가 호출됩니다.
        // Albedo값에 색상값을 넣어주면
        // 광원을 계산하는 함수에서 그 값을 활용해서
        // 밝고 어두움에 대한 색상값을 연산하게 된다.
        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            // 텍스처와 텍스처의 좌표값을 넣어주면 
            // 텍스처 좌표에 맞는 픽셀을 얻어오는 함수.
            // 0.3 * 0.3 = 곱셈연산처리로 인해 색상이 어두워진다.
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            // 빛과 연산되지 않을 색상값
            o.Emission = c + float3(_R, _G, _B);
            // 빛과 연산될 색상값
            //o.Albedo = c.rgb;
            o.Alpha = c.a *_A;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
