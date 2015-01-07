namespace Site

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.JavaScript

[<JavaScript>]
module Animations =

    [<Inline "requestAnimationFrame($0)">]
    let render (frame : unit -> unit) = X<unit>

    let current = ref (fun () -> ())

    let rec frame () =
        (!current)()
        
        render frame

    let isStarted = ref false
    
    let startIfNotStarted () =
        if not !isStarted
        then
            isStarted := true
            
            render frame