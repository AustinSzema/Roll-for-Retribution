// https://catlikecoding.com/unity/tutorials/advanced-rendering/tessellation/
#if !defined(GRASS_TESSELATION_INC)
#define GRASS_TESSELATION_INC

// Do pragmas here instead of in other shader to avoid confusion
#pragma hull grassHull
#pragma domain grassDomain
#pragma vertex grassTessellationVertex

// How much to tessellate on edges & inside
struct TessellationFactors
{
  float edge[3] : SV_TessFactor;
  float inside  : SV_InsideTessFactor;
};

// Vertex inputs
struct appdata
{
  float4 vertex   : POSITION;
  float3 normal   : NORMAL;
  float4 tangent  : TANGENT;
};

// Vertex outputs
struct v2g
{
  float4 vertex   : SV_POSITION;
  float3 normal   : NORMAL;
  float4 tangent  : TANGENT;
};

// Vertex shader for tessellation
v2g grassTessellationVertex (appdata i)
{
  v2g o;
  o.vertex   = i.vertex;
  o.normal   = i.normal;
  o.tangent  = i.tangent;
  return o;
}

// Variable from Properties{} block.
// Controls amount of tessellation on edges and inside
float _Tessellation;

// Patch constants to determine amount of tessellation
// not to be confused with percentage-closer filtering (hehe :3)
TessellationFactors pcf (InputPatch<appdata, 3> patch)
{
  TessellationFactors f;
  f.edge[0] = _Tessellation;
  f.edge[1] = _Tessellation;
  f.edge[2] = _Tessellation;
  f.inside  = _Tessellation;
  return f;
}

// Hull shader
[UNITY_domain("tri")]
[UNITY_outputcontrolpoints(3)]
[UNITY_outputtopology("triangle_cw")]
[UNITY_partitioning("fractional_even")]
[UNITY_patchconstantfunc("pcf")]
appdata grassHull (InputPatch<appdata, 3> patch, uint id : SV_OutputControlPointID)
{
  return patch[id];
}

// Domain shader
[UNITY_domain("tri")]
v2g grassDomain (TessellationFactors factors, OutputPatch<appdata, 3> patch, float3 baryCoords : SV_DomainLocation)
{
  appdata data;
  #define DOMAIN_INTERPOLATE(fieldName) data.fieldName = \
    patch[0].fieldName * baryCoords.x + \
    patch[1].fieldName * baryCoords.y + \
    patch[2].fieldName * baryCoords.z;

  DOMAIN_INTERPOLATE(vertex)
  DOMAIN_INTERPOLATE(normal)
  DOMAIN_INTERPOLATE(tangent)

  return grassTessellationVertex(data);
}

#endif
