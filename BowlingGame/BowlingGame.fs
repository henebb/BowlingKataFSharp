namespace BowlingGame

type Game() =

    let mutable currentRoll = 0
    // Maximum of 21 rolls in a bowling game:
    let rolls : int array = Array.zeroCreate 21 

    member self.Roll(pins : int) =
        rolls.[currentRoll] <- pins
        currentRoll <- currentRoll + 1
    

    member self.Score() =
        let mutable rollIndex = 0
        
        let score =
            [|
                // Ten frames in a bowling game:
                for _ in 1..10 do
                    // Pattern match a tuple with roll plus the two next rolls:
                    match rolls.[rollIndex], rolls.[rollIndex + 1], rolls.[rollIndex + 2] with
                    // Strike?
                    | 10, firstRollAfterStrike, secondRollAfterStrike -> 
                        rollIndex <- rollIndex + 1
                        10 + firstRollAfterStrike + secondRollAfterStrike
                    // Spare?
                    | 1, 9, rollAfterSpare | 2, 8, rollAfterSpare | 3, 7, rollAfterSpare | 4, 6, rollAfterSpare 
                    | 5, 5, rollAfterSpare 
                    | 9, 1, rollAfterSpare | 8, 2, rollAfterSpare | 7, 3, rollAfterSpare | 6, 4, rollAfterSpare ->
                        rollIndex <- rollIndex + 2
                        10 + rollAfterSpare
                    | roll1, roll2, _ -> 
                        if roll1 <= 10 then
                            rollIndex <- rollIndex + 2
                            roll1 + roll2
            |]
            |> Array.sum
        score
