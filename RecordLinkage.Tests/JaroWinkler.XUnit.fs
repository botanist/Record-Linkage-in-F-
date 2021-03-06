﻿module RecordLinkage.Tests.JaroWinkler.XUnit

open System

open Xunit
open RecordLinkage.JaroWinkler

//
// From: http://richardminerich.com/2011/09/record-linkage-algorithms-in-f-jaro-winkler-distance-part-1/
//

[<Fact>]
let ``Jaro identity test`` () = 
    let result = jaro "RICK" "RICK"
    Assert.Equal<String>("1.000", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro martha test`` () =
    let result = jaro "MARTHA" "MARHTA"
    Assert.Equal<String>("0.944", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro dwayne test`` () = 
    let result = jaro "DWAYNE" "DUANE"
    Assert.Equal<String>("0.822", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro dixon test`` () =
    let result = jaro "DIXON" "DICKSONX"
    Assert.Equal<String>("0.767", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro address test`` () =
    let result = jaro "MAIN ST" "MAIN STREET"
    Assert.Equal<String>("0.888", String.Format("{0:0.000}", result))

//
// From: http://richardminerich.com/2011/09/record-linkage-algorithms-in-f-%E2%80%93-jaro-winkler-distance-part-2/
// 

[<Fact>]
let ``Jaro-Winkler identity test`` () = 
    let result = jaroWinkler "RICK" "RICK"
    Assert.Equal<String>("1.000", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro-Winkler martha test`` () = 
    let result = jaroWinkler "MARTHA" "MARHTA"
    Assert.Equal<String>("0.961", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro-Winkler dwayne test`` () = 
    let result = jaroWinkler "DWAYNE" "DUANE"
    Assert.Equal<String>("0.840", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro-Winkler dixon test`` () =
    let result = jaroWinkler "DIXON" "DICKSONX"
    Assert.Equal<String>("0.813", String.Format("{0:0.000}", result))

[<Fact>]
let ``Jaro-Winkler address test`` () =
    let result = jaroWinkler "MAIN ST" "MAIN STREET"
    Assert.Equal<String>("0.933", String.Format("{0:0.000}", result))