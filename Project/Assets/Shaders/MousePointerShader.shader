﻿Shader "Custom/MousePointerShader" {
    SubShader {
    	// Rendering Settings
    	Tags {"Queue" = "Overlay" }
    	Lighting Off
    	Cull Off
    	ZTest Always 
    	ZWrite Off
    	Fog { Mode Off }

        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float4 vert(float4 v:POSITION) : SV_POSITION {
                return mul (UNITY_MATRIX_MVP, v);
            }

            fixed4 frag() : SV_Target {
                return fixed4(0.1,1.0,1.0,1.0);
            }

            ENDCG
        }
    }
}
