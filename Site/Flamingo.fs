namespace Site
 
open WebSharper

[<JavaScript>]
module Flamingo =
    open WebSharper.ThreeJs
    open WebSharper.JQuery
    open WebSharper.UI
    open WebSharper.UI.Html
    open WebSharper.UI.Client
    open WebSharper.UI.Notation
    open WebSharper.JavaScript

    let Main a =
        let renderer = new THREE.WebGLRenderer(
                           WebGLRendererConfiguration(
                               Antialias = true
                           )
                       )

        renderer.SetSize(640, 360)
        renderer.SetClearColor(0xffffff)

        let autoRotate = Var.Create false

        Doc.Concat [
            Doc.Static renderer.DomElement
            button [
                Attr.Style "display" "block"
                on.click (fun a _ -> autoRotate := not !autoRotate)
            ] [
                text ("Auto rotate | " + if autoRotate.V then "On" else "Off")
            ]
        ]
        |> Doc.RunAppend a

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
        let controls = new THREE.TrackballControls(camera)

        camera.Position.Z <- 234.

        //---
        let rec frame () =
            renderer.Render(scene, camera)
            controls.Update()

            if !autoRotate
            then
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
