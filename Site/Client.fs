namespace Site

open WebSharper

[<JavaScript>]
module Client =
    let All =
        let ( !+ ) x = Samples.Set.Singleton(x)

        Samples.Set.Create [
            !+ Cube.Sample
            !+ Globe.Sample
            !+ Flamingo.Sample
            !+ Maze.Sample
        ]

    [<SPAEntryPoint>]
    let Main() =
        All.Show()
