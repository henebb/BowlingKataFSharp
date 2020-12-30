namespace BowlingGame

type Game() =

    let mutable currentRoll = 0
    // Maximum of 21 rolls in a bowling game:
    let rolls : int array = Array.zeroCreate 21 

    member self.Roll(pins : int) =
        rolls.[currentRoll] <- pins
        currentRoll <- currentRoll + 1
    

    member self.Score() =
        let mutable score = 0
        let mutable rollIndex = 0
        // Ten frames in a bowling game
        [|1..10|]
        |> Array.iter (fun _ -> 
            match rolls.[rollIndex], rolls.[rollIndex + 1], rolls.[rollIndex + 2] with
            // Strike?
            | 10, firstRollAfterStrike, secondRollAfterStrike -> 
                score <- score + 10 + firstRollAfterStrike + secondRollAfterStrike
                rollIndex <- rollIndex + 1
            // Spare?
            | 1, 9, rollAfterSpare | 2, 8, rollAfterSpare | 3, 7, rollAfterSpare | 4, 6, rollAfterSpare 
            | 5, 5, rollAfterSpare 
            | 9, 1, rollAfterSpare | 8, 2, rollAfterSpare | 7, 3, rollAfterSpare | 6, 4, rollAfterSpare ->
                score <- score + 10 + rollAfterSpare 
                rollIndex <- rollIndex + 2
            | roll1, roll2, _ -> 
                score <- score + roll1 + roll2
                rollIndex <- rollIndex + 2
        )
        score

