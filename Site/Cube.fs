namespace Site
 
open WebSharper

[<JavaScript>]
module Cube =
    open WebSharper.ThreeJs
    open WebSharper.JQuery
    open WebSharper.JavaScript

    let Main a =
        let renderer = new THREE.WebGLRenderer(
                           WebGLRendererConfiguration(
                               Antialias = true
                           )
                       )

        renderer.SetSize(640, 360)
        renderer.SetClearColor(0xffffff)

        JQuery.Of(a :> Dom.Node).Append(renderer.DomElement) |> ignore

        let scene = new THREE.Scene()
        let cube =
            new THREE.Mesh(
                new THREE.BoxGeometry(1., 1., 1.),
                new THREE.MeshNormalMaterial(
                    MeshNormalMaterialConfiguration(
                        Wireframe = false
                    )
                )
            )

        scene.Add(cube)

        let camera = new THREE.PerspectiveCamera(45., 16./9.)

        camera.Position.Z <- 4.

        //---
        let rec frame () =
            renderer.Render(scene, camera)

            cube.Rotation.Y <- cube.Rotation.Y + (Math.PI / 180.)
            cube.Rotation.X <- cube.Rotation.X + (Math.PI / 360.)

        Animations.current := frame
        Animations.startIfNotStarted()
        //--

    let Sample =
        Samples.Build()
            .Id("Cube")
            .FileName(__SOURCE_FILE__)
            .Keywords(["basics"; "cube"])
            .Render(Main)
            .Create()
