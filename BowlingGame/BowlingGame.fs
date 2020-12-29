namespace BowlingGame

type Game() =

    let mutable currentRoll = 0
    // Maximum of 21 rolls in a bowling game:
    let rolls : int array = Array.zeroCreate 21 
    
    let isSpare frameIndex =
        rolls.[frameIndex] + rolls.[frameIndex + 1] = 10
    
    let isStrike frameIndex =
        rolls.[frameIndex] = 10

    member self.Roll(pins : int) =
        rolls.[currentRoll] <- pins
        currentRoll <- currentRoll + 1
    

    member self.Score() =
        let mutable score = 0
        let mutable frameIndex = 0
        // Ten frames in a bowling game
        [|1..10|]
        |> Array.iter (fun _ -> 
            if frameIndex |> isStrike then
                score <- score + 10 + rolls.[frameIndex + 1] + rolls.[frameIndex + 2]
                frameIndex <- frameIndex + 1
            else if  frameIndex |> isSpare then
                score <- score + 10 + rolls.[frameIndex + 2] 
                frameIndex <- frameIndex + 2
            else
                score <- score + rolls.[frameIndex] + rolls.[frameIndex + 1]
                frameIndex <- frameIndex + 2
        )
        score

