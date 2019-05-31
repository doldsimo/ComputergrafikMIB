using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;


namespace Fusee.Tutorial.Core
{
    public class FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _cubeTransform; // für die Animation von dem ersten Würfel
        private TransformComponent _quaderTransform; // für die Animation von dem Quader
        private TransformComponent _wuerfelDreiTransform; // für die Animation vom 3. Würfen
        private ShaderEffectComponent _wuerfelDreiShader;

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to light green (intensities in R, G, B, A)
            RC.ClearColor = new float4(0.2f, 0.2f, 0.2f, 1); // Hintergrundfarbe (r,g,b, Durchsichtigkeit)

            // Create a scene with a cube
            // The three components: one XForm, one Material and the Mesh
            _cubeTransform = new TransformComponent {
            
                Scale = new float3(1, 1, 1), // Skalierung
                Translation = new float3(0, 0, 0), // Position des Objektes (x,y,z)
                Rotation = new float3(0, -0.5f, 0.2f) // Drehung an den Achsen; in Gradmaß angeben
            
            };
            var cubeShader = new ShaderEffectComponent
            { 
                Effect = SimpleMeshes.MakeShaderEffect(new float3 (0, 0, 1), new float3 (1, 1, 1),  4)
                /* Erster float Wert Farbe des Würfels; Zweiter float Wert Farbe des Glanzlichtes  */
            };
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(5, 5, 5)); // Größe des Quaders

            // Assemble the cube node containing the three components
            var cubeNode = new SceneNodeContainer();
            cubeNode.Components = new List<SceneComponentContainer>();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);






            // Eigenschaften des Quaders
            
            _quaderTransform = new TransformComponent{
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 20),
    	        Rotation = new float3(0, 0, 0)
            }; 
            var quaderShader = new ShaderEffectComponent{
                 Effect = SimpleMeshes.MakeShaderEffect(new float3 (1, 1, 1), new float3 (1, 1, 1),  4)
            };
            var quaderMesh = SimpleMeshes.CreateCuboid(new float3(5, 5, 20)); 
            


            var quader = new SceneNodeContainer();
            quader.Components = new List<SceneComponentContainer>();
            quader.Components.Add(_quaderTransform);
            quader.Components.Add(quaderShader);
            quader.Components.Add(quaderMesh);




            // Eigenschaften 3. Würfel

           _wuerfelDreiTransform = new TransformComponent{
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 20),
    	        Rotation = new float3(0, 0, 0)
            }; 
            _wuerfelDreiShader = new ShaderEffectComponent{
                 Effect = SimpleMeshes.MakeShaderEffect(new float3 (0.9f, 0.2f, 0.4f), new float3 (1, 1, 1),  4)
            };
            var wuerfelDreiMesh = SimpleMeshes.CreateCuboid(new float3(5, 5, 20)); 



            var wuerfelDrei = new SceneNodeContainer();
            wuerfelDrei.Components = new List<SceneComponentContainer>();
            wuerfelDrei.Components.Add(_wuerfelDreiTransform);
            wuerfelDrei.Components.Add(_wuerfelDreiShader);
            wuerfelDrei.Components.Add(wuerfelDreiMesh);



            // Create the scene containing the cube as the only object
            // Hier werden alle Objekte der Szene Hinzugefügt
            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            _scene.Children.Add(cubeNode);
            _scene.Children.Add(quader);
            _scene.Children.Add(wuerfelDrei);





            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {

            Console.WriteLine("hallo");
            Diagnostics.Log(TimeSinceStart);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Animate the camera angle
            //_camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime ;

            // Animate the cube 1
            _cubeTransform.Translation = new float3(0, 6 * M.Sin(2 * TimeSinceStart) + 10, 0);
            _cubeTransform.Rotation = _cubeTransform.Rotation + new float3(0.05f * Time.DeltaTime, 0, 0); 
            /* muss mit Time.DeltaTime multipiziert werden, damit auf jedem System die Animation gleich schnell abgespielt wird*/
            _cubeTransform.Scale = new float3(1 * M.Sin(1 * TimeSinceStart), 1, 1 * M.Sin(4 * TimeSinceStart));
            // Animation Quader 2
            _quaderTransform.Translation = new float3(6 * M.Cos(10 * TimeSinceStart), -10, 0);
            _quaderTransform.Rotation = _quaderTransform.Rotation + new float3(0, 0.7f * Time.DeltaTime, 0.7f * Time.DeltaTime);
            
            //Animation Cube 3

            _wuerfelDreiTransform.Translation = new float3(15, 2 * M.Sin(6 * TimeSinceStart), 15 * M.Sin(1 * TimeSinceStart));
            _wuerfelDreiShader.Effect = SimpleMeshes.MakeShaderEffect(new float3 (1, M.Sin(3 * TimeSinceStart), 0), new float3 (1, 1, 1),  4);
            
            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);


            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}