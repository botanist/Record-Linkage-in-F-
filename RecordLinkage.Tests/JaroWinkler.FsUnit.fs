module RecordLinkage.Tests.JaroWinkler.FsUnit

open NUnit.Framework
open FsUnit
open System
open RecordLinkage.JaroWinkler

//
// From: http://richardminerich.com/2011/09/record-linkage-algorithms-in-f-jaro-winkler-distance-part-1/
//
//

[<Test>]
let ``Jaro identity test`` () =
    let result = jaro "RICK" "RICK"
    String.Format("{0:0.000}", result) |> should equal "1.000"
[<Test>]
let ``Jaro martha test`` () =
    let result = jaro "MARTHA" "MARHTA"
    String.Format("{0:0.000}", result) |> should equal "0.944"
[<Test>]
let ``Jaro dwayne test`` () =
    let result = jaro "DWAYNE" "DUANE"
    String.Format("{0:0.000}", result) |> should equal "0.822"
[<Test>]
let ``Jaro dixon test`` () =
    let result = jaro "DIXON" "DICKSONX"
    String.Format("{0:0.000}", result) |> should equal "0.767"
[<Test>]
let ``Jaro address test`` () =
    let result = jaro "MAIN ST" "MAIN STREET"
    String.Format("{0:0.000}", result) |> should equal "0.888"

//
// From: http://richardminerich.com/2011/09/record-linkage-algorithms-in-f-%E2%80%93-jaro-winkler-distance-part-2/
// 
//
[<Test>]
let ``Jaro-Winkler identity test`` () =
    let result = jaroWinkler "RICK" "RICK"
    String.Format("{0:0.000}", result) |> should equal "1.000"
[<Test>]
let ``Jaro-Winkler martha test`` () =
    let result = jaroWinkler "MARTHA" "MARHTA"
    String.Format("{0:0.000}", result) |> should equal "0.961"
[<Test>]
let ``Jaro-Winkler dwayne test`` () =
    let result = jaroWinkler "DWAYNE" "DUANE"
    String.Format("{0:0.000}", result) |> should equal "0.840"
[<Test>]
let ``Jaro-Winkler dixon test`` () =
    let result = jaroWinkler "DIXON" "DICKSONX"
    String.Format("{0:0.000}", result) |> should equal "0.813"
[<Test>]
let ``Jaro-Winkler address test`` () =
    let result = jaroWinkler "MAIN ST" "MAIN STREET"
    String.Format("{0:0.000}", result) |> should equal "0.933"