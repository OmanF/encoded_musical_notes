module Composition

open MusicDefinition
open MusicEvent

let (en, qn, dqn, hn, dhn, triplet) = (1.0 / 8.0, 1.0 / 4.0, 3.0 / 8.0, 1.0 / 2.0, 3.0 / 4.0, (2.0 / 3.0) * (1.0 / 8.0)) // Notes lengths, normalized
let es = f // Utility definition. Correct according to musical theory (to some extent)

// First 28 bars of Chic Korea's "Child's song No. 6"
let baseline =
    let b1 =
        line
            [ b 2 dqn
              fs 3 dqn
              g 3 dqn
              fs 3 dqn ]

    let b2 =
        line
            [ b 2 dqn
              es 3 dqn
              fs 3 dqn
              es 3 dqn ]

    let b3 =
        line
            [ ``as`` 2 dqn // F#'s double-back-tick syntax to allow irregular variable names to be used. In this case, 'as' is an F# keyword
              fs 3 dqn
              g 3 dqn
              fs 3 dqn ]

    line
        [ times 3 b1
          times 2 b2
          times 4 b3
          times 5 b1 ]

let melody =
    line
        [ line
            [ times 3
                  (line
                      [ a 4 en
                        e 4 en
                        d 4 en
                        fs 4 en
                        cs 4 en
                        b 3 en
                        e 4 en
                        b 3 en
                        graceNote -1 (d 4 qn)
                        cs 4 en
                        b 3 en ])
              cs 4 (dhn + dhn) ]
          line
              [ d 4 dhn
                f 4 hn
                gs 4 qn
                fs 4 (hn + en)
                g 4 en
                fs 4 en
                e 4 en
                cs 4 en
                ``as`` 3 en
                a 3 dqn ]
          line
              [ ``as`` 3 en
                cs 4 en
                fs 4 en
                e 4 en
                fs 4 en
                g 4 en
                ``as`` 4 en
                cs 5 (hn + en) ]
          line
              [ d 5 en
                cs 5 en
                e 4 en
                rest en
                ``as`` 4 en
                a 4 en
                g 4 en
                d 4 qn
                c 4 en
                cs 4 en ]
          line
              [ fs 4 en
                cs 4 en
                e 4 en
                cs 4 en
                a 3 en
                ``as`` 3 en
                d 4 en
                e 4 en
                fs 4 en
                graceNote 2 (e 4 qn)
                d 4 en ]
          line
              [ graceNote 2 (d 4 qn)
                cs 4 en
                graceNote 1 (cs 4 qn)
                b 3 (en + hn)
                cs 4 en
                b 3 en ]
          line
              [ fs 4 en
                a 4 en
                b 4 (hn + qn)
                a 4 en
                fs 4 en
                e 4 qn
                d 4 en
                fs 4 en
                e 4 hn
                d 4 hn
                fs 4 qn ]
          line
              [ cs 4 triplet
                d 4 triplet
                cs 4 triplet
                b 3 ((dhn * 3.0) + hn) ] ]

let song = Par(baseline, melody)

[<EntryPoint>]
let main argv =
    perform
        { // Duration and BPM are TOTALLY arbitrarily chosen by me. I have no idea if it's correct music
          duration = 1.0
          bpm = 60 } song
    |> List.map encodeEvent
    |> List.iter (printfn "%A")

    0
