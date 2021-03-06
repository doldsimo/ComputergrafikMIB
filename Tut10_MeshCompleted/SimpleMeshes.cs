﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace Fusee.Tutorial.Core
{
    public static class SimpleMeshes 
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    // left, bottom, front vertex
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 0  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 1  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 3  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 4  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 6  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 7  - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 9  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 10 - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 12 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 13 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 15 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 16 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 18 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 19 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 21 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 22 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 23 - belongs to back

                },
                Normals = new[]
                {
                    // left, bottom, front vertex
                    new float3(-1,  0,  0), // 0  - belongs to left
                    new float3( 0, -1,  0), // 1  - belongs to bottom
                    new float3( 0,  0, -1), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-1,  0,  0),  // 3  - belongs to left
                    new float3( 0, -1,  0),  // 4  - belongs to bottom
                    new float3( 0,  0,  1),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-1,  0,  0),  // 6  - belongs to left
                    new float3( 0,  1,  0),  // 7  - belongs to up
                    new float3( 0,  0, -1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-1,  0,  0),  // 9  - belongs to left
                    new float3( 0,  1,  0),  // 10 - belongs to up
                    new float3( 0,  0,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 1,  0,  0), // 12 - belongs to right
                    new float3( 0, -1,  0), // 13 - belongs to bottom
                    new float3( 0,  0, -1), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 1,  0,  0),  // 15 - belongs to right
                    new float3( 0, -1,  0),  // 16 - belongs to bottom
                    new float3( 0,  0,  1),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 1,  0,  0),  // 18 - belongs to right
                    new float3( 0,  1,  0),  // 19 - belongs to up
                    new float3( 0,  0, -1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 1,  0,  0),  // 21 - belongs to right
                    new float3( 0,  1,  0),  // 22 - belongs to up
                    new float3( 0,  0,  1),  // 23 - belongs to back
                },
                Triangles = new ushort[]
                {
                    0,  6,  3,     3,  6,  9,  // left
                    2, 14, 20,     2, 20,  8,  // front
                    12, 15, 18,    15, 21, 18, // right
                    5, 11, 17,    17, 11, 23,  // back
                    7, 22, 10,     7, 19, 22,  // top
                    1,  4, 16,     1, 16, 13,  // bottom 
                },
                UVs = new float2[]
                {
                    // left, bottom, front vertex
                    new float2( 1,  0), // 0  - belongs to left
                    new float2( 1,  0), // 1  - belongs to bottom
                    new float2( 0,  0), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float2( 0,  0),  // 3  - belongs to left
                    new float2( 1,  1),  // 4  - belongs to bottom
                    new float2( 1,  0),  // 5  - belongs to back

                    // left, up, front vertex
                    new float2( 1,  1),  // 6  - belongs to left
                    new float2( 0,  0),  // 7  - belongs to up
                    new float2( 0,  1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float2( 0,  1),  // 9  - belongs to left
                    new float2( 0,  1),  // 10 - belongs to up
                    new float2( 1,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float2( 0,  0), // 12 - belongs to right
                    new float2( 0,  0), // 13 - belongs to bottom
                    new float2( 1,  0), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float2( 1,  0),  // 15 - belongs to right
                    new float2( 0,  1),  // 16 - belongs to bottom
                    new float2( 0,  0),  // 17 - belongs to back

                    // right, up, front vertex
                    new float2( 0,  1),  // 18 - belongs to right
                    new float2( 1,  0),  // 19 - belongs to up
                    new float2( 1,  1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float2( 1,  1),  // 21 - belongs to right
                    new float2( 1,  1),  // 22 - belongs to up
                    new float2( 0,  1),  // 23 - belongs to back                    
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f*size)
            };
        }

        public static ShaderEffect MakeShaderEffect(float3 diffuseColor, float3 specularColor, float shininess)
        {
            MaterialComponent temp = new MaterialComponent
            {
                Diffuse = new MatChannelContainer
                {
                    Color = diffuseColor
                },
                Specular = new SpecularChannelContainer
                {
                    Color = specularColor,
                    Shininess = shininess
                }
            };
            return ShaderCodeBuilder.MakeShaderEffectFromMatComp(temp);
        }

        public static Mesh CreateCylinder(float radius, float height, int segments)
        {
            float3[] verts = new float3[4*segments+2];    // Punkte des Zylinders x,y,z
            float3[] norms = new float3[4*segments+2];    // Normalen der Punkte
            ushort[] tris  = new ushort[4 * 3 * segments];  // Dreiecke aus denen der Zylinder

            float delta = 2 * M.Pi / segments;

            // The center (store at the last position in the vertex array (index 'segments'))
            verts[4 * segments] = new float3(0, 0.5f * height, 0);
            norms[4 * segments] = float3.UnitY;

            verts[4 * segments + 1] = new float3(0, - 0.5f * height, 0);
            norms[4 * segments + 1] = -float3.UnitY;
            


            // The first and last point (first point in the list (index 0))
            verts[0] = new float3(radius, 0.5f * height, 0);  // ersten beiden Punkte des oberen zylinders nach oben versetzten
            verts[1] = new float3(radius, 0.5f * height, 0);

            verts[2] = new float3(radius, -0.5f * height, 0);
            verts[3] = new float3(radius, -0.5f * height, 0);
            
            
            
            norms[0] = float3.UnitY;
            norms[1] = float3.UnitX;
            norms[2] = float3.UnitX;
            norms[3] = -float3.UnitY;

            for (int i = 1; i < segments; i++)
            {
                // Create the current point and store it at index i
                verts[4*i + 0] = new float3(radius * M.Cos(i * delta), 0.5f * height, radius * M.Sin(i * delta));
                verts[4*i + 1] = new float3(radius * M.Cos(i * delta), 0.5f * height, radius * M.Sin(i * delta)); 
                verts[4*i + 2] = new float3(radius * M.Cos(i * delta), -0.5f * height, radius * M.Sin(i * delta)); 
                verts[4*i + 3] = new float3(radius * M.Cos(i * delta), -0.5f * height, radius * M.Sin(i * delta));  
                
                norms[4*i + 0] = float3.UnitY;
                norms[4*i + 3] = -float3.UnitY;
                norms[4*i + 1] = new float3(M.Cos(i * delta), 0, M.Sin(i * delta));
                norms[4*i + 2] = new float3(M.Cos(i * delta), 0, M.Sin(i * delta));


                //Oberes Dreiecke
                tris[12*(i-1) + 0] = (ushort) (4*segments);   
                tris[12*(i-1) + 1] = (ushort) (4*(i-1) +0);     
                tris[12*(i-1) + 2] = (ushort) ( 4*i +0);

                //Seitliche Dreiecke von unten
                tris[12*(i-1) + 3] = (ushort) (4*(i-1) + 2);      
                tris[12*(i-1) + 4] = (ushort) (4*i     + 2);     
                tris[12*(i-1) + 5] = (ushort) (4*i     + 1); 

                //Seitliche Dreiecke von oben
                tris[12*(i-1) + 6] = (ushort) (4*(i-1) + 2);      
                tris[12*(i-1) + 7] = (ushort) (4*i     + 1);     
                tris[12*(i-1) + 8] = (ushort) (4*(i-1) + 1);

                //Unteres Dreieck
                tris[12*(i-1) + 9]  = (ushort) (4*segments+1);   
                tris[12*(i-1) + 10] = (ushort) (4*i + 3);     
                tris[12*(i-1) + 11] = (ushort) (4*(i-1)  + 3);         
            }

            //Letzte Dreiecke erzeugen

            //Oberes Dreieck
            tris[12*segments -10] = (ushort) (0);   
            tris[12*segments -11] = (ushort) (4*segments - 4);     
            tris[12*segments -12] = (ushort) (4*segments);
            
            //Seitliche Dreiecke von oben
            tris[12*segments -4] = (ushort) (4*segments - 2);   
            tris[12*segments -5] = (ushort) (4*segments - 3);     
            tris[12*segments -6] = (ushort) (1);

            //Seitliche Dreiecke von unten
            tris[12*segments -7] = (ushort) (1);   
            tris[12*segments -8] = (ushort) (2);     
            tris[12*segments -9] = (ushort) (4*segments - 2);

            //Unteres Dreieck
            tris[12*segments -1] = (ushort) (4*segments + 1);   
            tris[12*segments -2] = (ushort) (4*segments - 1);     
            tris[12*segments -3] = (ushort) (3);

    
            return new Mesh
            {
                Vertices = verts,
                Normals = norms,
                Triangles = tris,
            };
        }

        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }
}
