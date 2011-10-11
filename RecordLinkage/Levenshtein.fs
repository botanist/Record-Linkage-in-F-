module RecordLinkage.EditDistance

//
// The Wagner–Fischer algorithm is a dynamic programming algorithm that measures the Levenshtein distance between two strings of characters.
// http://en.wikipedia.org/wiki/Wagner%E2%80%93Fischer_edit_distance
//

// int LevenshteinDistance(char s[1..m], char t[1..n])
// {
//   // for all i and j, d[i,j] will hold the Levenshtein distance between
//   // the first i characters of s and the first j characters of t;
//   // note that d has (m+1)x(n+1) values
//   declare int d[0..m, 0..n]
//  
//   for i from 0 to m
//     d[i, 0] := i // the distance of any first string to an empty second string
//   for j from 0 to n
//     d[0, j] := j // the distance of any second string to an empty first string
//  
//   for j from 1 to n
//   {
//     for i from 1 to m
//     {
//       if s[i] = t[j] then  
//         d[i, j] := d[i-1, j-1]       // no operation required
//       else
//         d[i, j] := minimum
//                    (
//                      d[i-1, j] + 1,  // a deletion
//                      d[i, j-1] + 1,  // an insertion
//                      d[i-1, j-1] + 1 // a substitution
//                    )
//     }
//   }
//  
//   return d[m,n]
// }

let inline min3 one two three = 
    if one < two && one < three then one
    elif two < three then two
    else three

let wagnerFischer (s: string) (t: string) =
    let m = s.Length
    let n = t.Length
    let d = Array2D.create (m + 1) (n + 1) 0

    for i = 0 to m do d.[i, 0] <- i
    for j = 0 to n do d.[0, j] <- j    

    for j = 1 to n do
        for i = 1 to m do
            if s.[i-1] = t.[j-1] then
                d.[i, j] <- d.[i-1, j-1]
            else
                d.[i, j] <-
                    min3
                        (d.[i-1, j  ] + 1) // a deletion
                        (d.[i  , j-1] + 1) // an insertion
                        (d.[i-1, j-1] + 1) // a substitution
    d.[m,n]


// Pretty, but exponentially slow!
let linearSpaceEditDistance (s: string) (t: string) =
    let rec innerEdit i j =
        match i, j with
        | 0, j -> j
        | i, 0 -> i
        | _ when s.[i-1] = t.[j-1] -> innerEdit (i - 1) (j - 1)
        | _ -> min3 
                ((innerEdit (i-1) j) + 1)
                ((innerEdit i (j-1)) + 1)
                ((innerEdit (i-1) (j-1)) + 1)
    innerEdit (s.Length) (t.Length) 


// The fastest in the bunch.
let wagnerFischerLazy (s: string) (t: string) =
    let m = s.Length
    let n = t.Length
    let d = Array2D.create (m+1) (n+1) -1
    let rec dist =
        function
        | i, 0 -> i
        | 0, j -> j
        | i, j when d.[i,j] <> -1 -> d.[i,j]
        | i, j ->
            let dval = 
                if s.[i-1] = t.[j-1] then dist (i-1, j-1)
                else
                    min3
                        (dist (i-1, j)   + 1) // a deletion
                        (dist (i,   j-1) + 1) // an insertion
                        (dist (i-1, j-1) + 1) // a substitution
            d.[i, j] <- dval; dval 
    dist (m, n)

//
// Damerau–Levenshtein distance
// http://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
//

//int OptimalStringAlignmentDistance(char str1[1..lenStr1], char str2[1..lenStr2])
//   // d is a table with lenStr1+1 rows and lenStr2+1 columns
//   declare int d[0..lenStr1, 0..lenStr2]
//   // i and j are used to iterate over str1 and str2
//   declare int i, j, cost
//   //for loop is inclusive, need table 1 row/column larger than string length.
//   for i from 0 to lenStr1
//       d[i, 0] := i
//   for j from 1 to lenStr2
//       d[0, j] := j
//   //Pseudo-code assumes string indices start at 1, not 0.
//   //If implemented, make sure to start comparing at 1st letter of strings.
//   for i from 1 to lenStr1
//       for j from 1 to lenStr2
//           if str1[i] = str2[j] then cost := 0
//                                else cost := 1
//           d[i, j] := minimum(
//                                d[i-1, j  ] + 1,     // deletion
//                                d[i  , j-1] + 1,     // insertion
//                                d[i-1, j-1] + cost   // substitution
//                            )
//           if(i > 1 and j > 1 and str1[i] = str2[j-1] and str1[i-1] = str2[j]) then
//               d[i, j] := minimum(
//                                d[i, j],
//                                d[i-2, j-2] + cost   // transposition
//                             )
//                                
// 
//   return d[lenStr1, lenStr2]

// the triangle inequality does not hold (or... OptimalStringAlignmentDistance)
let restrictedEditDistance (s: string) (t: string) =
    let m = s.Length
    let n = t.Length
    let d = Array2D.create (m + 1) (n + 1) 0

    for i = 0 to m do d.[i, 0] <- i
    for j = 0 to n do d.[0, j] <- j    

    for j = 1 to n do
        for i = 1 to m do
            let cost = if s.[i-1] = t.[j-1] then 0 else 1
            d.[i, j] <-
                min3
                    (d.[i-1, j  ] + 1) // a deletion
                    (d.[i  , j-1] + 1) // an insertion
                    (d.[i-1, j-1] + cost) // a substitution
            if i > 1 && j > 1 && s.[i-1] = t.[j-2] && s.[i-2] = t.[j-1] then
                d.[i, j] <- 
                    min 
                        d.[i,j] 
                        (d.[i-2, j-2] + cost) // a transposition
    d.[m,n]

