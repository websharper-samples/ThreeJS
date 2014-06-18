namespace Site
 
open IntelliFactory.WebSharper

[<JavaScript>]
module Globe =
    open IntelliFactory.WebSharper.ThreeJs
    open IntelliFactory.WebSharper.JQuery

    let Main a =
        let mouseX = ref 0.0
        let mouseY = ref 0.0
        
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
        
        light.Position.Z <- 4.

        scene.Add(light)

        let sphere =
            new THREE.Mesh(
                new THREE.SphereGeometry(200., 40, 40),
                new THREE.MeshLambertMaterial(
                    MeshLambertMaterialConfiguration(
                        Map = THREE.ImageUtils.LoadTexture("earth.jpg")
                    )
                )
            )

        scene.Add(sphere)

        let camera = new THREE.PerspectiveCamera(45., 16./9.)

        camera.Position.Z <- 500.

        Dom.Document.Current.AddEventListener("mousemove", (fun (e : Dom.Event) ->
            let ee = e :?> Dom.MouseEvent
            
            mouseX := (float ee.ClientX) - Html5.Window.Self?innerWidth / 2.
            mouseY := (float ee.ClientY) - Html5.Window.Self?innerHeight / 2.
        ), false)

        //---
        let rec frame () =
            renderer.Render(scene, camera)

            camera.Position.X <- camera.Position.X + (!mouseX - camera.Position.X) * 0.05
            camera.Position.Y <- camera.Position.Y + (-1. * !mouseY - camera.Position.Y) * 0.05

            camera.LookAt(scene.Position)

            sphere.Rotation.Y <- sphere.Rotation.Y + 0.01

        Animations.current := frame

        Animations.startIfNotStarted()
        //--

    let Sample =
        Samples.Build()
            .Id("Globe")
            .FileName(__SOURCE_FILE__)
            .Keywords(["sphere"; "texture"])
            .Render(Main)
            .Create()
