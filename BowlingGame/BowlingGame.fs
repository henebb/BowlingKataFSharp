namespace BowlingGame

open System
module Game =

    let private isValidRolls (rolls: int list) =
        if rolls.IsEmpty then
            false
        else if rolls.Length < 12 || rolls.Length > 21 then
            false
        else if rolls |> List.exists (fun roll -> roll < 0 || roll > 10) then
            false
        else
            let invalidRollPair =
                          rolls
                          |> List.filter (fun roll -> roll <> 10)
                          |> List.chunkBySize 2
                          |> List.exists (fun pair ->
                                if pair.Length = 2 then
                                    pair.[0] + pair.[1] > 10
                                else
                                    false
                              )
            if invalidRollPair = false && rolls.Length = 21 then
                // Check bonus frame:
                match (rolls.[18], rolls.[19], rolls.[20]) with
                | 10, 10, _ -> true
                | roll1, roll2, _ when roll1 + roll2 = 10 -> true
                | 10, roll2, 10 when roll2 <> 10 -> false
                | 0, _, _ -> false
                | _ -> true
            else if invalidRollPair = false && rolls.Length >= 19 && rolls |> List.last = 10 then
                // Unfinished game
                false
            else if invalidRollPair = false && rolls.Length = 20 && rolls.[18] + rolls.[19] = 10 then
                // Unfinished game
                false
            else
                not invalidRollPair
    
    let score (rolls: int list) =        
        if not (isValidRolls rolls) then
            None
        else
            let rolls' = 
                match rolls.Length with
                | len when len < 21 -> rolls @ List.init (21 - len) (fun _ -> 0) // Pad with 0 up to 21 rolls
                | 21 -> rolls
                | _ -> invalidArg "rolls" "Invalid rolls length"
                
            // Then frames in a bowling game.
            [0..9]
            |> List.fold (fun (frameIndexAppender, sum) frameIndex ->
                // Calculate rollIndex:
                let rollIndex = frameIndex + frameIndexAppender
                
                // Pattern match a tuple with roll plus the two next rolls:
                match rolls'.[rollIndex], rolls'.[rollIndex + 1], rolls'.[rollIndex + 2] with
                // Strike?
                | 10, firstRollAfterStrike, secondRollAfterStrike ->
                    let frameScore = sum + 10 + firstRollAfterStrike + secondRollAfterStrike
                    (frameIndexAppender, frameScore)
                // Spare?
                | roll1, roll2, rollAfterSpare when roll1 + roll2 = 10 ->
                    let frameScore = sum + 10 + rollAfterSpare
                    (frameIndexAppender + 1, frameScore)
                | roll1, roll2, _ when roll1 <= 10 ->
                    let frameScore = sum + roll1 + roll2
                    (frameIndexAppender + 1, frameScore)
                // Ignore any invalid roll (i.e. greater than 10). Will not happen
                | _ -> (rollIndex, sum)
                )
                (0, 0) // Start-state
            |> snd // Select the sum part
            |> Some // Must return an optional.

