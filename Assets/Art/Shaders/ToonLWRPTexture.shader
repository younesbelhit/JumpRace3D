Shader "ToonLWRPTexture"
{
    Properties
    { 
        [Header(Color)]
        [Space(10)]
        _BaseMap("Base Map", 2D) = "white" {}

        [Header(Light)]
        [Space(10)]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        _LightIntensity ("Ambient Light Intensity", Range (0, 1)) = 1.0


        [Header(Outline)]
        [Space(10)]
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_OutlineThickness ("Outline Thickness", Range (0, 1)) = 0.01
        _OutlineLimiter ("Outline Limiter", Range (0, 1000)) = 1
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"             
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"   

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
                float3 normalOS : NORMAL;  
                float4 tangentOS : TANGENT;  
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
                float3 worldNormal : NORMAL;
            };

            // This macro declares _BaseMap as a Texture2D object.
            TEXTURE2D(_BaseMap);
            // This macro declares the sampler for the _BaseMap texture.
            SAMPLER(sampler_BaseMap);

            float4 _BaseMap_ST;
            
            CBUFFER_START(UnityPerMaterial)
                
                float4 _AmbientColor;
                float _LightIntensity;

            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);

                VertexNormalInputs normalInput = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);
                OUT.worldNormal = normalInput.normalWS;

                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);

                float3 normal = normalize(IN.worldNormal);
                float NdotL = dot(_MainLightPosition, normal);

                float lightIntensity = smoothstep(0, 0.01, NdotL);

                lightIntensity *= _MainLightColor * _LightIntensity;

                return color *  (_AmbientColor + lightIntensity );
            }
            ENDHLSL
        }

        Pass{

            Tags {"LightMode" = "UniversalForward"  "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline"  "IgnoreProjector" = "True" }

			Cull Front

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            


            float _OutlineThickness;
    
            float4 _OutlineColor;
            float _OutlineLimiter;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                float4 positionHCS : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert(appdata v) {

	            v2f o;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
	            float3 outLine = ( v.normal.xyz * _OutlineThickness * min( vertexInput.positionCS.w , 1.5f ) );
			    v.vertex.xyz += (outLine/_OutlineLimiter);

                
                vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.positionHCS = vertexInput.positionCS;
	    
	            o.color = _OutlineColor;
	            return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return i.color;
            }

            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster" 

        
    }

    
}
