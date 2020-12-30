module Tests

open Xunit
open Xunit.Abstractions
open BowlingGame
open FsUnit.Xunit
open System

type Tests(output : ITestOutputHelper) = 

    [<Fact>]
    let ``Gutter game`` () =
        let rolls = Array.zeroCreate 21
        Game.score rolls |> should equal 0

    [<Fact>]
    let ``All ones``() =
        let rolls = Array.init 21 (fun _ -> 1)
        Game.score rolls |> should equal 20

    [<Fact>]
    let ``One spare``() =
        let rolls = Array.zeroCreate 21 // Gutter game init.
        rolls.[0] <- 5 // First throw
        rolls.[1] <- 5 // Second throw - spare!
        rolls.[2] <- 3 // The throw after a spare matters.
        Game.score rolls |> should equal 16

    [<Fact>]
    let ``One strike``() =
        let rolls = Array.zeroCreate 21 // Gutter game init.
        rolls.[0] <- 10 // Strike!
        rolls.[1] <- 3 // The two nextcoming throws matters
        rolls.[2] <- 4 // ...and this
        Game.score rolls |> should equal 24

    [<Fact>]
    let ``A perfect game``() =
        let rolls = Array.init 21 (fun _ -> 10) // All strikes
        Game.score rolls |> should equal 300

    [<Fact>]
    let ``Ignore roll > 10``() =
        let rolls = Array.zeroCreate 21 // Gutter game init.
        rolls.[0] <- 11 // Invalid  throw
        Game.score rolls |> should equal 0

    [<Fact>]
    let ``Must have 21 rolls``() =
        let rolls = Array.zeroCreate 20
        (fun () -> Game.score(rolls) |> ignore) |> should throw typeof<ArgumentException>
