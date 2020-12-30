module Tests

open Xunit
open Xunit.Abstractions
open BowlingGame
open FsUnit.Xunit

type Tests(output : ITestOutputHelper) = 
    
    let game = Game()

    let rollMany(numThrows : int, pins : int) =
        let throws = Array.init numThrows (fun i -> pins)
        throws |> Array.iter game.Roll

    let rollSpare() =
        [|5; 5|]
        |> Array.iter game.Roll

    let rollStrike() =
        game.Roll(10)

    [<Fact>]
    let ``Gutter game`` () =
        rollMany(20, 0)
        game.Score() |> should equal 0

    [<Fact>]
    let ``All ones``() =
        rollMany(20, 1)
        game.Score() |> should equal 20

    [<Fact>]
    let ``One spare``() =
        rollSpare()
        game.Roll(3)
        rollMany(17, 0) // Rest is gutter
        game.Score() |> should equal 16

    [<Fact>]
    let ``One strike``() =
        rollStrike()
        game.Roll(3)
        game.Roll(4)
        rollMany(16, 0) // Rest is gutter
        game.Score() |> should equal 24

    [<Fact>]
    let ``A perfect game``() =
        rollMany(12, 10)
        game.Score() |> should equal 300

    [<Fact>]
    let ``Ignore roll > 10``() =
        game.Roll(11)
        rollMany(19, 0)
        game.Score() |> should equal 0
        
