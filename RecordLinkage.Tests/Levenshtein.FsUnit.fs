module RecordLinkage.Tests.Levenshtein.FsUnit

open NUnit.Framework
open FsUnit
open System
open RecordLinkage.EditDistance

[<Test>]
let ``wagnerFischer identity test`` () = 
    let result = wagnerFischer "RICK" "RICK"
    result |> should equal 0

[<Test>]
let ``wagnerFischer kitten-sitting test`` () = 
    let result = wagnerFischer "KITTEN" "SITTING"
    result |> should equal 3

[<Test>]
let ``wagnerFischer saturday-sunday test`` () = 
    let result = wagnerFischer "SATURDAY" "SUNDAY"
    result |> should equal 3

[<Test>]
let ``wagnerFischerLazy identity test`` () = 
    let result = wagnerFischerLazy "RICK" "RICK"
    result |> should equal 0

[<Test>]
let ``wagnerFischerLazy kitten-sitting test`` () = 
    let result = wagnerFischerLazy "KITTEN" "SITTING"
    result |> should equal 3

[<Test>]
let ``wagnerFischerLazy saturday-sunday test`` () = 
    let result = wagnerFischerLazy "SATURDAY" "SUNDAY"
    result |> should equal 3

[<Test>]
let ``restrictedEditDistance identity test`` () = 
    let result = restrictedEditDistance "RICK" "RICK"
    result |> should equal 0

[<Test>]
let ``restrictedEditDistance ca-abc test`` () = 
    let result = restrictedEditDistance "CA" "ABC"
    result |> should equal 3