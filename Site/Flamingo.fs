namespace Site
 
open IntelliFactory.WebSharper

[<JavaScript>]
module Flamingo =
    open IntelliFactory.WebSharper.ThreeJs
    open IntelliFactory.WebSharper.Html5
    open IntelliFactory.WebSharper.JQuery

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
        let light = new THREE.DirectionalLight(0xffffff)

        light.Position.Z <- 128.

        scene.Add(light)

        let flamingo =
            new THREE.Mesh(
                new THREE.Geometry(),
                new THREE.MeshNormalMaterial()
            )
        
        (new THREE.JSONLoader(false)).Load(
            "flamingo.json",
            fun (geometry, _) ->
                flamingo.Geometry <- geometry
                
                scene.Add(flamingo)
        )

        let camera = new THREE.PerspectiveCamera(45., 16./9.)

        camera.Position.Z <- 234.

        //---
        let rec frame () =
            renderer.Render(scene, camera)

            flamingo.Rotation.Y <- flamingo.Rotation.Y + 0.01

        Animations.current := frame

        Animations.startIfNotStarted()
        //---
    
    let Sample =
        Samples.Build()
            .Id("Flamingo")
            .FileName(__SOURCE_FILE__)
            .Keywords(["model"])
            .Render(Main)
            .Create()
