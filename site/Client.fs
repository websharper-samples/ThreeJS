namespace Site

open IntelliFactory.WebSharper

[<JavaScript>]
module Client =
    let All =
        let ( !+ ) x = Samples.Set.Singleton(x)

        Samples.Set.Create [
            !+ Cube.Sample
            !+ Globe.Sample
            !+ Flamingo.Sample
        ]

    let Main = All.Show()
