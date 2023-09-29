Imports System
Imports System.Data
Imports System.Reflection

Module Program
    Sub Main(args As String())
        Dim dt As New DataTable("StudentTable")
        dt.Columns.Add("StudID", GetType(String))
        dt.Columns.Add("Subject", GetType(String))
        dt.Columns.Add("Score", GetType(Decimal))
        dt.Rows.Add(New Object() {
                "1001",
                "English",
                11.22
            })
        dt.Rows.Add(New Object() {
                "1002",
                "English",
                15.0
            })
        dt.Rows.Add(New Object() {
                "1003",
                "English",
                16.25
            })
        dt.Rows.Add(New Object() {
                "1004",
                "English",
                13.5
            })
        dt.Rows.Add(New Object() {
                "1001",
                "Math",
                18.0
            })
        dt.Rows.Add(New Object() {
                "1002",
                "Math",
                18.0
            })
        dt.Rows.Add(New Object() {
                "1003",
                "Math",
                17.0
            })
        dt.Rows.Add(New Object() {
                "1004",
                "Math",
                16.0
            })
        dt.Rows.Add(New Object() {
                "1001",
                "CompProg1",
                17.5
            })
        dt.Rows.Add(New Object() {
                "1002",
                "CompProg1",
                16.0
            })
        dt.Rows.Add(New Object() {
                "1003",
                "CompProg1",
                15.25
            })
        dt.Rows.Add(New Object() {
                "1004",
                "CompProg1",
                18.5
        })

        Dim query = (From students In dt.AsEnumerable
                     Group students By StudID = students.Field(Of String)("StudID") Into g = Group
                     Select New With {
                         Key StudID,
                         .English = g.Where(Function(c) c.Field(Of String)("Subject") = "English").Sum(Function(c) c.Field(Of Decimal)("Score")),
                         .Math = g.Where(Function(c) c.Field(Of String)("Subject") = "Math").Sum(Function(c) c.Field(Of Decimal)("Score")),
                         .CompProg1 = g.Where(Function(c) c.Field(Of String)("Subject") = "CompProg1").Sum(Function(c) c.Field(Of Decimal)("Score"))
                     }).OrderBy(Function(tkey) tkey.StudID).ToList()

        Console.WriteLine($"StudID{vbTab}English{vbTab}Math{vbTab}CompProg1")
        For index = 0 To query.Count - 1
            Console.WriteLine($"{query(index).StudID}{vbTab}{query(index).English.ToString("N")}{vbTab}{query(index).Math.ToString("N")}{vbTab}{query(index).CompProg1.ToString("N")}")
        Next

        Console.ReadLine()
    End Sub
End Module
