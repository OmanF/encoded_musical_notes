# Encoding musical notes

(A partial translation into F# of an Elm application, taken from a [blog post](http://blog.leifbattermann.de/2019/08/08/elm-algorithm-music/) by Leif Battermann)

Encoding musical notes into a specialized JSON structure to be passed into [WebAudioFont](https://surikov.github.io/webaudiofont/).  
The original post, targets Elm, also creates and then passes the JSON into an actual webapp using WebAudioFont, that has a button to actually play the music.  
Current implementation stops short of that, only encoding the music.

## Running the app

To run the app, which prints to the console the output JSON structure, simply, from the command-line run `dotnet run`.
