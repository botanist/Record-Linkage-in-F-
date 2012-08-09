module RecordLinkage.Tests.Levenshtein.XUnit

open System
open Xunit

open RecordLinkage.EditDistance

[<Fact>]
let ``wagnerFischer identity test`` () = 
    let result = wagnerFischer "RICK" "RICK"
    Assert.Equal(0, result)

[<Fact>]
let ``wagnerFischer kitten-sitting test`` () = 
    let result = wagnerFischer "KITTEN" "SITTING"
    Assert.Equal(3, result)

[<Fact>]
let ``wagnerFischer saturday-sunday test`` () = 
    let result = wagnerFischer "SATURDAY" "SUNDAY"
    Assert.Equal(3, result)

[<Fact>]
let ``wagnerFischerLazy identity test`` () = 
    let result = wagnerFischerLazy "RICK" "RICK"
    Assert.Equal(0, result)

[<Fact>]
let ``wagnerFischerLazy kitten-sitting test`` () = 
    let result = wagnerFischerLazy "KITTEN" "SITTING"
    Assert.Equal(3, result)

[<Fact>]
let ``wagnerFischerLazy saturday-sunday test`` () = 
    let result = wagnerFischerLazy "SATURDAY" "SUNDAY"
    Assert.Equal(3, result)

[<Fact>]
let ``restrictedEditDistance identity test`` () = 
    let result = restrictedEditDistance "RICK" "RICK"
    Assert.Equal(0, result)

[<Fact>]
let ``restrictedEditDistance ca-abc test`` () = 
    let result = restrictedEditDistance "CA" "ABC"
    Assert.Equal(3, result)

