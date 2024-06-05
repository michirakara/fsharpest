// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO
open FSharp.SystemCommandLine

[<EntryPoint>]
let main (argv: string[]) : int =
    let handler (text: string) : int =
        let sounds = [ "C"; "C#"; "D"; "D#"; "E"; "F"; "F#"; "G"; "G#"; "A"; "A#"; "B" ]

        if 'A' <= text[0] && text[0] <= 'G' then
            let offset =
                ((List.findIndex (fun s -> s = "F#") sounds + 12) - List.findIndex (fun s -> s = string text[0]) sounds)%12

            let rec modulate (cur: string, nex: string) =
                if nex.Length = 0 then
                    cur
                else
                    modulate (
                        (cur
                         + (if (List.contains (string nex[0]) sounds) then
                                sounds[(List.findIndex (fun s -> s = string nex[0]) sounds + offset) % 12]
                            else
                                string nex[0])),
                        nex[1..]
                    )

            printfn "%s" (modulate ("", text))
            0
        else
            printfn "Start with an existing sound!"
            1

    rootCommand argv {
        description "modulate everything to F#"
        inputs (Input.Argument<string>("sentence", "A sentence to modulate"))
        setHandler handler

    }
