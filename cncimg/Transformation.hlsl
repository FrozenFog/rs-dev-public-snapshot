////////////////////////////////////////////////////////////////////////////
// 
// File: transform.txt
// 
// Author: Frank Luna (C) All Rights Reserved
//
// System: AMD Athlon 1800+ XP, 512 DDR, Geforce 3, Windows XP, MSVC++ 7.0 
//
// Desc: Vertex shader that transforms a vertex by the view and 
//       projection transformation, and sets the vertex color to blue.
//          
////////////////////////////////////////////////////////////////////////////
 
//
// Globals
//
 
// Global variable to store a combined view and projection
// transformation matrix.  We initialize this variable
// from the application.
matrix ViewProjMatrix;
 
// Initialize a global blue color vector.
vector Blue = {0.0f, 0.0f, 1.0f, 1.0f};
 
//
// Structures
//
 
// Input structure describes the vertex that is input
// into the shader.  Here the input vertex contains
// a position component only.
struct VS_INPUT
{
    vector position  : POSITION;
};
 
// Output structure describes the vertex that is
// output from the shader.  Here the output
// vertex contains a position and color component.
struct VS_OUTPUT
{
    vector position : POSITION;
    vector diffuse  : COLOR;
};
 
//
// Main Entry Point, observe the main function 
// receives a copy of the input vertex through
// its parameter and returns a copy of the output
// vertex it computes.
//
 
VS_OUTPUT Main(VS_INPUT input)
{
    // zero out members of output
    VS_OUTPUT output = (VS_OUTPUT)0;
  
    // transform to view space and project
    output.position  = mul(input.position, ViewProjMatrix);
 
    // set vertex diffuse color to blue
    output.diffuse = Blue;
 
    return output;
}