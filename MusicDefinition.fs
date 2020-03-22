module MusicDefinition

type Duration = float

type Pitch = int

type Octave = int

type Primitive =
    | Note of Duration * Pitch
    | Rest of Duration

type Music =
    | Prim of Primitive
    | Sequ of Music * Music // 'Seq' is F# keyword, so, next best thing...
    | Par of Music * Music

let note duration pitch = Prim(Note(duration, pitch))
let rest duration = Prim(Rest duration)
let line partialMusic = Seq.foldBack (fun elem acc -> Sequ(elem, acc)) partialMusic (rest 0.0)
let times repeats (partialMusic: Music) = List.replicate repeats partialMusic |> line

let graceNote n partialMusic =
    match partialMusic with
    | Prim(Note(duration, pitch)) -> Sequ(note (duration / 8.0) (pitch + n), note ((duration / 8.0) * 7.0) pitch)
    | _ -> partialMusic

[<AutoOpen>]
module Notes =
    let c octave duration = note duration (12 * (octave + 1) + 0)
    let cs octave duration = note duration (12 * (octave + 1) + 1)
    let df octave duration = note duration (12 * (octave + 1) + 1)
    let d octave duration = note duration (12 * (octave + 1) + 2)
    let ds octave duration = note duration (12 * (octave + 1) + 3)
    let ef octave duration = note duration (12 * (octave + 1) + 3)
    let e octave duration = note duration (12 * (octave + 1) + 4)
    let f octave duration = note duration (12 * (octave + 1) + 5)
    let fs octave duration = note duration (12 * (octave + 1) + 6)
    let gf octave duration = note duration (12 * (octave + 1) + 6)
    let g octave duration = note duration (12 * (octave + 1) + 7)
    let gs octave duration = note duration (12 * (octave + 1) + 8)
    let af octave duration = note duration (12 * (octave + 1) + 8)
    let a octave duration = note duration (12 * (octave + 1) + 9)
    let ``as`` octave duration = note duration (12 * (octave + 1) + 10)
    let bf octave duration = note duration (12 * (octave + 1) + 10)
    let b octave duration = note duration (12 * (octave + 1) + 11)
