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

    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private float _zupackenZustand = 0;
        private TransformComponent _baseTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _foreArmTransform;
        private TransformComponent _fingerEins;
        private TransformComponent _fingerZwei;
        private bool offen = false;
        private bool schliessen = false;
        private bool vorgang = false;
        

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _bodyTransform = new TransformComponent
            {
                Rotation = new float3(0, 0.2f, 0), // zweite Komponente
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };
            _upperArmTransform = new TransformComponent
            {
                Rotation = new float3(0.8f, 0, 0), // erste Komponente
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };
            _foreArmTransform = new TransformComponent
            {
                Rotation = new float3(0.8f, 0, 0), // erste Komponente
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 8, 0)
            };
            _fingerEins = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 0.6f),
                Translation = new float3(-1.5f, 5, 0)
            };
            _fingerZwei = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 0.6f),
                Translation = new float3(+1.5f, 5, 0)
            };


            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    // GREY BASE
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // MATERIAL COMPONENT
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },
                    // RED BODY
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            new ShaderEffectComponent
                            {
                                Effect = SimpleMeshes.MakeShaderEffect(new float3(1.0f, 0.2f, 0.2f), new float3(0.7f, 0.7f, 0.7f), 5)
                            },
                            _bodyTransform,
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },
                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2)),

                        },
                        Children = new List<SceneNodeContainer>
                        {
                            // GREEN UPPER ARM
                            new SceneNodeContainer
                            {
                                Components = new List<SceneComponentContainer>
                                {
                                    _upperArmTransform,
                                },
                                Children = new List<SceneNodeContainer>
                                {
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            new ShaderEffectComponent
                                            {
                                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.2f, 1.0f, 0.2f), new float3(0.7f, 0.7f, 0.7f), 5)
                                            },
                                            new TransformComponent
                                            {
                                                Rotation = new float3(0, 0, 0),
                                                Scale = new float3(1, 1, 1),
                                                Translation = new float3(0, 4, 0)
                                            },
                                            new MaterialComponent
                                            {
                                                Diffuse = new MatChannelContainer { Color = new float3(0, 1, 0) },
                                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                            },
                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                        }
                                    },
                                    // BLUE FOREARM
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _foreArmTransform,
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new ShaderEffectComponent
                                                    {
                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0.2f, 0.2f, 1.0f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                    },
                                                    
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            },
                                            // Finger Eins
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    _fingerEins,
                                                },
                                                Children = new List<SceneNodeContainer>
                                                {
                                                    new SceneNodeContainer
                                                    {
                                                        Components = new List<SceneComponentContainer>
                                                        {
                                                            new ShaderEffectComponent
                                                            {
                                                                Effect = SimpleMeshes.MakeShaderEffect(new float3(1, 0.5f, 0.5f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                            },
                                                            
                                                            new TransformComponent
                                                            {
                                                                Rotation = new float3(0, 0, 0),
                                                                Scale = new float3(1, 1, 1),
                                                                Translation = new float3(0, 4, 0)
                                                            },
                                                            new MaterialComponent
                                                            {
                                                                Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                            },
                                                            SimpleMeshes.CreateCuboid(new float3(2, 3, 2))
                                                        }
                                                    },
                                                    
                                                }
                                            },
                                            //Finger Zwei
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    _fingerZwei
                                                },
                                                Children = new List<SceneNodeContainer>
                                                {
                                                    new SceneNodeContainer
                                                    {
                                                        Components = new List<SceneComponentContainer>
                                                        {
                                                            new ShaderEffectComponent
                                                            {
                                                                Effect = SimpleMeshes.MakeShaderEffect(new float3(1, 0.5f, 0.5f), new float3(0.7f, 0.7f, 0.7f), 5)
                                                            },
                                                            
                                                            new TransformComponent
                                                            {
                                                                Rotation = new float3(0, 0, 0),
                                                                Scale = new float3(1, 1, 1),
                                                                Translation = new float3(0, 4, 0)
                                                            },
                                                            new MaterialComponent
                                                            {
                                                                Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                            },
                                                            SimpleMeshes.CreateCuboid(new float3(2, 3, 2))
                                                        }
                                                    }
                                                }
                                            }
                                            
                                        }
                                        
                                       
                                    }

                                }
                            },
                        }
                    }
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
            
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            

            float bodyRot = _bodyTransform.Rotation.y;
            bodyRot += 3 * Keyboard.LeftRightAxis * DeltaTime; 
            _bodyTransform.Rotation = new float3(0, bodyRot, 0);

            float upperArmBewegung = _upperArmTransform.Rotation.x;
            upperArmBewegung -= 2 * Keyboard.UpDownAxis * DeltaTime;
            _upperArmTransform.Rotation = new float3(upperArmBewegung, 0, 0);

            float foreArmBewegung = _foreArmTransform.Rotation.x;
            foreArmBewegung -= 2 * Keyboard.WSAxis * DeltaTime;
            _foreArmTransform.Rotation = new float3(foreArmBewegung, 0, 0);


            // Kamerabewegung
            float camAngleXBewegung = _camAngle;
            if(Mouse.LeftButton == true){
                camAngleXBewegung = _camAngle;
                camAngleXBewegung += Mouse.Velocity.x * DeltaTime * 0.0009f;
            }
            _camAngle = camAngleXBewegung;

            // Zupacken der Finger
            if(Keyboard.GetKey(KeyCodes.A) || Keyboard.GetKey(KeyCodes.D)){
                vorgang = true;
                offen = false;
                schliessen = false;
            }
        
            if(vorgang){
                float zupackenFinger = _fingerEins.Rotation.x;
                zupackenFinger += Keyboard.ADAxis * DeltaTime * 9;
                _fingerEins.Rotation = new float3(0, 0, zupackenFinger);
                _fingerZwei.Rotation = new float3(0, 0, -zupackenFinger);
            }
            
            if(Keyboard.GetKey(KeyCodes.E)){
                offen = true;
                schliessen = false;
                vorgang = false;
            }
            if(Keyboard.GetKey(KeyCodes.R)){
                schliessen = true;
                offen = false;
                vorgang  = false;
            }

            if(offen){
                _fingerEins.Rotation = new float3(0, 0, 0.15f);
                _fingerZwei.Rotation = new float3(0, 0, -0.15f);
            }
            if(schliessen){
                _fingerZwei.Rotation = new float3(0, 0, 0.15f);
                _fingerEins.Rotation = new float3(0, 0, -0.15f);
            }

        

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);


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