namespace BowlingGame

open System
module Game =

    let score(rolls : int array) =
        let mutable rollIndex = 0
            
        if rolls |> Array.length <> 21 then
            raise <| ArgumentException("A game must constist of 21 rolls")

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
                    // Ignore any invalid roll (i.e. greater than 10)
                    | _, _, _ ->
                        ()
            |]
            |> Array.sum
        score
