#r @".\packages\FAKE\tools\FakeLib.dll"
open Fake

// Properties
let buildDir = "./build"


// Targets
Target "Clean" <| fun _ ->
    CleanDir buildDir

Target "Default" <| fun _ ->
    trace "Hello world !"

// Dependencies
"Clean"
  ==> "Default"

RunTargetOrDefault "Default"
