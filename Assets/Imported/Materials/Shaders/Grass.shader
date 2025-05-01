// https://roystan.net/articles/grass-shader/
Shader "Unlit/Grass"
{
  Properties
  {
    [Header(Field Properties)]
    _BaseColor    ("Base Color", Color)         = (1.0, 1.0, 1.0, 1.0)
    _TopColor     ("Top Color", Color)          = (1.0, 1.0, 1.0, 1.0)
    _Bend         ("Bend", Range(0.0, 1.0))     = 0.2
    _Tessellation ("Tessellation", Range(1.0, 64.0)) = 1.0
    
    [Header(Wind)]
    _WindFrequency  ("Frequency", Vector) = (0.05, 0.05, 0.0, 0.0)
    _WindStrength   ("Strength", Float)   = 1.0

    [Header(Blade Properties)]
    _Width      ("Width", Float)                    = 0.05
    _WidthRand  ("Width Randomization", Float)      = 0.02
    _Height     ("Height", Float)                   = 0.5
    _HeightRand ("Height Randomization", Float)     = 0.3
    _OffsetRand ("Random Offset", Range(0.0,10.0))  = 1.0
  }

  CGINCLUDE
  // Simple noise function
  // http://answers.unity.com/answers/624136/view.html
  float rand(float3 co)
  {
    return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
  }

	// Construct a rotation matrix that rotates around the provided axis
	// https://gist.github.com/keijiro/ee439d5e7388f3aafc5296005c8c3f33
	float3x3 AngleAxis3x3(float angle, float3 axis)
	{
		float c, s;
		sincos(angle, s, c);

		float t = 1 - c;
		float x = axis.x;
		float y = axis.y;
		float z = axis.z;

		return float3x3(
				t * x * x + c, t * x * y - s * z, t * x * z + s * y,
				t * x * y + s * z, t * y * y + c, t * y * z - s * x,
				t * x * z - s * y, t * y * z + s * x, t * z * z + c
				);
	}

  // Inigo Quilez pulls through once again
  // https://www.shadertoy.com/view/Msf3WH
  float2 hash (float2 p)
  {
    p = float2( dot(p,float2(127.1,311.7)), dot(p,float2(269.5,183.3)) );
    return -1.0 + 2.0*frac(sin(p)*43758.5453123);
  }

  // Same source as above ^
  float simplex (in float2 p)
  {
    const float K1 = 0.366025404; // (sqrt(3)-1)/2;
    const float K2 = 0.211324865; // (3-sqrt(3))/6;

    float2  i = floor( p + (p.x+p.y)*K1 );
    float2  a = p - i + (i.x+i.y)*K2;
    float   m = step(a.y,a.x); 
    float2  o = float2(m,1.0-m);
    float2  b = a - o + K2;
    float2  c = a - 1.0 + 2.0*K2;
    float3  h = max( 0.5-float3(dot(a,a), dot(b,b), dot(c,c) ), 0.0 );
    float3  n = h*h*h*h*float3( dot(a,hash(i+0.0)), dot(b,hash(i+o)), dot(c,hash(i+1.0)));
    return dot(n, float3(70.0, 70.0, 70.0));
  }
  ENDCG

  SubShader
  {
    Tags { "RenderType"="Opaque" }

    Cull off

    Pass
    {
      CGPROGRAM
      #pragma fragment frag
      #pragma geometry geom
      // vert ­> tessellation

      #pragma target 4.6

      #include "UnityCG.cginc"
      #include "GrassTessellation.cginc"

      // ------------

      struct g2f
      {
        float4 pos  : SV_POSITION;
        float2 uv   : TEXCOORD0;
      };

      g2f vOut(float3 pos, float2 uv)
      {
        g2f o;
        o.pos = UnityObjectToClipPos(pos);
        o.uv  = uv;
        return o;
      }

      // ------------

      float4 _BaseColor;
      float4 _TopColor;
      float  _Bend;
      float  _OffsetRand;

      float4 _WindFrequency;
      float  _WindStrength;

      float _Width;
      float _WidthRand;
      float _Height;
      float _HeightRand;

      // ------------

      [maxvertexcount(3)]
      void geom (triangle v2g i[3] : SV_POSITION, inout TriangleStream<g2f> triStream)
      {
        float3 pos = i[0].vertex + float3(rand(i[0].vertex.xyz), 0.0, rand(i[0].vertex.zyx)) * _OffsetRand;

        // Axial vectors
        float3 vNormal    = i[0].normal;
        float4 vTangent   = i[0].tangent;
        float3 vBinormal  = cross(vNormal, vTangent) * vTangent.w;

        // Matrix to get us from tangent into local space
        float3x3 t2l = float3x3(
          vTangent.x, vBinormal.x, vNormal.x,
          vTangent.y, vBinormal.y, vNormal.y,
          vTangent.z, vBinormal.z, vNormal.z
        );

        // Rotate around the Z axis (up in tangent space)
        float3x3 rotation   = AngleAxis3x3(rand(pos) * UNITY_TWO_PI, float3(0.0, 0.0, 1.0));
        float3x3 bend       = AngleAxis3x3(rand(pos.zzx) * _Bend * UNITY_PI * 0.5, float3(-1.0, 0.0, 0.0));

        // Wind
        float2   uv         = pos.xz + _WindFrequency * _Time.y;
        float2   windSample = simplex(uv) * _WindStrength;
        float3   windOffset = normalize(float3(windSample * 2.0 - 1.0, 0.0));
        float3x3 wind       = AngleAxis3x3(UNITY_PI * windSample, windOffset);

        // Final transform
        float3x3 transform  = mul(mul(mul(t2l, wind), rotation), bend);

        // W/H resizing randomly
        float width   = rand(pos.xyz)   * _WidthRand  + _Width;
        float height  = simplex(pos.zx) * _HeightRand + _Height;

        // Calculate & append the vertices
        triStream.Append(vOut(pos + mul(transform, float3(width, 0.0, 0.0)),  float2(0.0, 0.0)));
        triStream.Append(vOut(pos + mul(transform, float3(-width, 0.0, 0.0)), float2(1.0, 0.0)));
        triStream.Append(vOut(pos + mul(transform, float3(0.0, 0.0, height)),  float2(0.5, 1.0)));
      }

			float4 frag (g2f i, float facing : VFACE) : SV_Target
      {
        // Super basic lerp color
        float4 col = lerp(_BaseColor, _TopColor, i.uv.y);
        return col;
      }
      ENDCG
    }
  }
}
