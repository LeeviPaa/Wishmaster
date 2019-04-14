// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TerrainShader"
{
	Properties
	{
		_Terrainmat("Terrainmat", 2D) = "white" {}
		[NoScaleOffset]_NormalMap("NormalMap", 2D) = "bump" {}
		_MaxDist("MaxDist", Int) = 2000
		_FarTiling("FarTiling", Float) = 10
		_Distpower("Distpower", Int) = 2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform int _MaxDist;
		uniform int _Distpower;
		uniform sampler2D _NormalMap;
		uniform sampler2D _Terrainmat;
		uniform float4 _Terrainmat_ST;
		uniform float _FarTiling;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 transform4 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float temp_output_60_0 = (0.0 + (pow( min( distance( float4( _WorldSpaceCameraPos , 0.0 ) , transform4 ) , (float)_MaxDist ) , (float)_Distpower ) - 0.0) * (1.0 - 0.0) / ((float)pow( _MaxDist , _Distpower ) - 0.0));
			float2 appendResult58 = (float2(( 1.0 - temp_output_60_0 ) , temp_output_60_0));
			float2 uv_Terrainmat = i.uv_texcoord * _Terrainmat_ST.xy + _Terrainmat_ST.zw;
			float2 temp_output_2_0_g1 = uv_Terrainmat;
			float2 break6_g1 = temp_output_2_0_g1;
			float temp_output_25_0_g1 = ( pow( 0.5 , 3.0 ) * 0.1 );
			float2 appendResult8_g1 = (float2(( break6_g1.x + temp_output_25_0_g1 ) , break6_g1.y));
			float4 tex2DNode14_g1 = tex2D( _NormalMap, temp_output_2_0_g1 );
			float temp_output_4_0_g1 = 2.0;
			float3 appendResult13_g1 = (float3(1.0 , 0.0 , ( ( tex2D( _NormalMap, appendResult8_g1 ).g - tex2DNode14_g1.g ) * temp_output_4_0_g1 )));
			float2 appendResult9_g1 = (float2(break6_g1.x , ( break6_g1.y + temp_output_25_0_g1 )));
			float3 appendResult16_g1 = (float3(0.0 , 1.0 , ( ( tex2D( _NormalMap, appendResult9_g1 ).g - tex2DNode14_g1.g ) * temp_output_4_0_g1 )));
			float3 normalizeResult22_g1 = normalize( cross( appendResult13_g1 , appendResult16_g1 ) );
			float2 temp_cast_6 = (_FarTiling).xx;
			float2 uv_TexCoord70 = i.uv_texcoord * temp_cast_6;
			float2 temp_output_2_0_g2 = uv_TexCoord70;
			float2 break6_g2 = temp_output_2_0_g2;
			float temp_output_25_0_g2 = ( pow( 0.5 , 3.0 ) * 0.1 );
			float2 appendResult8_g2 = (float2(( break6_g2.x + temp_output_25_0_g2 ) , break6_g2.y));
			float4 tex2DNode14_g2 = tex2D( _NormalMap, temp_output_2_0_g2 );
			float temp_output_4_0_g2 = 2.0;
			float3 appendResult13_g2 = (float3(1.0 , 0.0 , ( ( tex2D( _NormalMap, appendResult8_g2 ).g - tex2DNode14_g2.g ) * temp_output_4_0_g2 )));
			float2 appendResult9_g2 = (float2(break6_g2.x , ( break6_g2.y + temp_output_25_0_g2 )));
			float3 appendResult16_g2 = (float3(0.0 , 1.0 , ( ( tex2D( _NormalMap, appendResult9_g2 ).g - tex2DNode14_g2.g ) * temp_output_4_0_g2 )));
			float3 normalizeResult22_g2 = normalize( cross( appendResult13_g2 , appendResult16_g2 ) );
			float2 weightedBlendVar71 = appendResult58;
			float3 weightedBlend71 = ( weightedBlendVar71.x*normalizeResult22_g1 + weightedBlendVar71.y*normalizeResult22_g2 );
			o.Normal = weightedBlend71;
			float2 temp_cast_7 = (_FarTiling).xx;
			float2 uv_TexCoord7 = i.uv_texcoord * temp_cast_7;
			float2 weightedBlendVar55 = appendResult58;
			float4 weightedBlend55 = ( weightedBlendVar55.x*tex2D( _Terrainmat, uv_Terrainmat ) + weightedBlendVar55.y*tex2D( _Terrainmat, uv_TexCoord7 ) );
			o.Albedo = weightedBlend55.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
2567;1;1527;1029;2168.921;1161.754;1.370218;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;3;-2630.744,442.6179;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;4;-2407.744,430.6179;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldSpaceCameraPos;2;-2467.744,271.6179;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DistanceOpNode;1;-2145.744,308.6179;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;61;-2103.327,511.2904;Float;False;Property;_MaxDist;MaxDist;4;0;Create;True;0;0;False;0;2000;2000;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;65;-2127.341,87.47263;Float;False;Property;_Distpower;Distpower;6;0;Create;True;0;0;False;0;2;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;59;-1950.916,306.6888;Float;False;2;0;FLOAT;0;False;1;INT;2000;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;63;-1801.65,422.56;Float;False;2;0;INT;0;False;1;INT;2;False;1;INT;0
Node;AmplifyShaderEditor.PowerNode;43;-1799.103,306.174;Float;False;2;0;FLOAT;0;False;1;INT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-1879.207,-412.0103;Float;False;Property;_FarTiling;FarTiling;5;0;Create;True;0;0;False;0;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;60;-1251.511,326.5226;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;6;-1747.461,-220.6006;Float;True;Property;_Terrainmat;Terrainmat;0;0;Create;True;0;0;False;0;8f68608f83666164fbf6e734211df771;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1296.582,-71.20657;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-1337.182,-611.0564;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;67;-1717.549,-932.0844;Float;True;Property;_NormalMap;NormalMap;1;1;[NoScaleOffset];Create;True;0;0;False;0;3c1924f221ec3db40a5b4ba7f7d1873a;None;True;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-1265.343,-281.1094;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;68;-1323.516,-853.0278;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;62;-1004.106,237.7924;Float;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;69;-968.9841,-678.0004;Float;True;NormalCreate;2;;2;e12f7ae19d416b942820e3932b56220f;0;4;1;SAMPLER2D;;False;2;FLOAT2;0,0;False;3;FLOAT;0.5;False;4;FLOAT;2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;66;-962.8977,-936.6483;Float;True;NormalCreate;2;;1;e12f7ae19d416b942820e3932b56220f;0;4;1;SAMPLER2D;;False;2;FLOAT2;0,0;False;3;FLOAT;0.5;False;4;FLOAT;2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-770.0361,300.9117;Float;False;FLOAT2;4;0;FLOAT;0.5;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-918.2391,-98.45825;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-911.6066,-310.1801;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SummedBlendNode;71;-491.2424,-696.2579;Float;False;5;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SummedBlendNode;55;-491.7407,-128.6418;Float;False;5;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;TerrainShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;1;0;2;0
WireConnection;1;1;4;0
WireConnection;59;0;1;0
WireConnection;59;1;61;0
WireConnection;63;0;61;0
WireConnection;63;1;65;0
WireConnection;43;0;59;0
WireConnection;43;1;65;0
WireConnection;60;0;43;0
WireConnection;60;2;63;0
WireConnection;7;0;56;0
WireConnection;70;0;56;0
WireConnection;51;2;6;0
WireConnection;68;2;6;0
WireConnection;62;1;60;0
WireConnection;69;1;67;0
WireConnection;69;2;70;0
WireConnection;66;1;67;0
WireConnection;66;2;68;0
WireConnection;58;0;62;0
WireConnection;58;1;60;0
WireConnection;5;0;6;0
WireConnection;5;1;7;0
WireConnection;52;0;6;0
WireConnection;52;1;51;0
WireConnection;71;0;58;0
WireConnection;71;1;66;0
WireConnection;71;2;69;0
WireConnection;55;0;58;0
WireConnection;55;1;52;0
WireConnection;55;2;5;0
WireConnection;0;0;55;0
WireConnection;0;1;71;0
ASEEND*/
//CHKSM=23B0BEA3DBC70DB5893079B407D8C425569B11E7