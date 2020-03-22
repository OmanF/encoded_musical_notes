module MusicEvent

open MusicDefinition
open Chiron

type Time = float

type MusicEvent =
    { time: Time
      pitch: Pitch
      duration: Time }
    // Chiron's custom serializer
    static member ToJson(me: MusicEvent) =
        json {
            do! Json.write "time" me.time
            do! Json.write "pitch" me.pitch
            do! Json.write "duration" me.duration
        }

type Tempo =
    { duration: Duration
      bpm: int }

// The only merge-able attribute is the time, so, using concrete, instead of generalized, function
let rec mergeMusicEventByTime list1 list2 =
    match (list1, list2) with
    | list1, [] -> list1
    | [], list2 -> list2
    | x :: xs, y :: ys ->
        if x.time > y.time
        then x :: (mergeMusicEventByTime xs (y :: ys))
        else y :: (mergeMusicEventByTime ys (x :: xs))

let rec musicToEvents currentTime tempo music =
    let metro = (60.0 / float (tempo.bpm) * tempo.duration)

    match music with
    | Prim(Note(duration, pitch)) ->
        ([ { time = currentTime
             pitch = pitch
             duration = (duration * metro) } ], duration * metro)
    | Prim(Rest duration) -> ([], duration * metro)
    | Sequ(music1, music2) ->
        let (events1, duration1) = musicToEvents currentTime tempo music1
        let (events2, duration2) = musicToEvents (currentTime + duration1) tempo music2

        (events1 @ events2, duration1 + duration2)
    | Par(music1, music2) ->
        let (events1, duration1) = musicToEvents currentTime tempo music1
        let (events2, duration2) = musicToEvents currentTime tempo music2

        (mergeMusicEventByTime events1 events2, max duration1 duration2)

let perform tempo music = musicToEvents 0.0 tempo music |> fst

let encodeEvent (event: MusicEvent) = event |> (Json.serialize >> Json.format)
