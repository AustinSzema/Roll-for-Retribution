Shader "Unlit/SHD_SeenThroughWalls"
{
    Properties
    {
        _Color1("Color1", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (1,1,1,1)
    }
        SubShader
    {
        Blend One One

        Tags { "Queue" = "Geometry+1" }
        Pass
        {
            ZTest Greater
            Color[_Color2]
            
        }
        Pass
        {
            ZTest Less
            Color[_Color1]
        }
    }
}