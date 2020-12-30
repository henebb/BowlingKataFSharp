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
                    | roll1, roll2, rollAfterSpare when roll1 + roll2 = 10 ->
                        rollIndex <- rollIndex + 2
                        10 + rollAfterSpare
                    | roll1, roll2, _ when roll1 <= 10 -> 
                        rollIndex <- rollIndex + 2
                        roll1 + roll2
                    | _, _, _ ->
                        ()
            |]
            |> Array.sum
        score
