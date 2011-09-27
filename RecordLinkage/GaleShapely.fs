module RecordLinkage.StableMarriages

open System

open RecordLinkage.JaroWinkler

//
// From: http://richardminerich.com/2011/09/record-linkage-in-f-token-matching-stable-marriages-and-the-gale-shapley-algorithm/
// And: http://richardminerich.com/2011/09/imperative-pseudocode-to-pure-functional-algorithm/
//

// a Bachelor is an identity index and an 
// ordered list of women indicies to approach.
type Bachelor = int * int list

// Some notation:
// wi = woman index (int)
// mi = man index (int)
// mi' = woman's current partner index (int)
// m = man with index and unapproached women indices (Bachelor)
// mSingle = men that are single (Bachelor list)
// wEngaged = engagements from women to men (int, Bachelor)

let funGS (comp: _ -> _ -> float) (M: _ array) (W: _ array) =
  let Windices = [ 0 .. W.Length - 1 ]
  // List of men with women in order of desire  
  let Munproposed = 
    List.init M.Length 
      (fun mi -> 
           let sortFun wi = 1.0 - (comp M.[mi] W.[wi])
           mi, Windices |> List.sortBy sortFun)
  // Recursively solve stable marriages
  let rec findMarriages mSingle wEngaged =
    match mSingle with
    // No single guys left with desired women, we're done
    | [] -> wEngaged
    // Guy is out of luck, remove from singles
    | (mi, []) :: bachelors -> findMarriages bachelors wEngaged
    // He's got options!
    | (mi, wi :: rest) :: bachelors -> 
      let m = mi, rest
      match wEngaged |> Map.tryFind wi with
      // She's single! m is now engaged!
      | None -> findMarriages bachelors (wEngaged |> Map.add wi m)
      // She's already engaged, let the best man win!
      | Some (m') -> 
        let mi', _ = m'
        if comp W.[wi] M.[mi] > comp W.[wi] M.[mi'] then 
          // Congrats mi, he is now engaged to wi
          // The previous suitor (mi') is bested 
          findMarriages 
            (m' :: bachelors) 
            (wEngaged |> Map.add wi m)
        else
          // The current bachelor (mi) lost, better luck next time
          findMarriages 
            (m :: bachelors) 
            wEngaged
  findMarriages Munproposed Map.empty
  // Before returning, remove unproposed lists from man instances  
  |> Map.map (fun wi m -> let mi, _ = m in mi)  

// By the supreme power of partial application I give you 
// Jaro-Winkler Token Alignment with Gale-Shapely in one line!

let alignJaroWinkler = funGS jaroWinkler

alignJaroWinkler [|"DUDE"; "DUDERSON"|] [|"D"; "RONNY"; "DUDERSON"|]


//
// Avert your eyes! Oh the humanity!
//

let imperativeGaleShapely (comp: _ -> _ -> float) (M: _ array) (W: _ array) =
    let aloneVal = Int32.MaxValue
    // Everyone starts single
    let Mmarriages = Array.create M.Length aloneVal
    let Wmarriages = Array.create W.Length aloneVal
    let Windices = [ 0 .. W.Length - 1 ]
    // Men rate and order women
    let Munproposed = 
        Array.init M.Length 
            (fun mi -> Windices 
                       |> List.sortBy (fun wi -> 1.0 - (comp M.[mi] W.[wi])))
    let getNextBachelorWithProspects () = 
        let mutable mi = Munproposed.Length - 1
        let mutable womens = []
        while mi >= 0 && womens = [] do
            womens <- Munproposed.[mi]
            mi <- mi - 1
        mi, womens
    let mutable keepLooking = true
    while keepLooking do
        match getNextBachelorWithProspects ()  with
        // No single men with prospects left, we're done
        | -1, _ | _, [] -> keepLooking <- false 
        // A lonely guy
        | mi, wi :: rest -> 
            match Munproposed.[mi] with
            // Mr. Lonelyheart is all out of options, we're done.
            | [] -> keepLooking <- false
            // He's got options!
            | wi :: tail ->
                Munproposed.[mi] <- tail
                if (Wmarriages.[wi] = aloneVal) then // She's single!
                    Mmarriages.[mi] <- wi; Wmarriages.[wi] <- mi
                else // She's engaged, fight for love!
                    let mi' = Wmarriages.[wi]
                    if comp W.[wi] M.[mi] > comp W.[wi] M.[mi'] then
                        Mmarriages.[mi] <- wi; Wmarriages.[wi] <- mi
                        Mmarriages.[mi'] <- aloneVal
    Wmarriages 

