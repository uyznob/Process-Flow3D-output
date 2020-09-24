Option Explicit
Dim scount As Integer

Sub BrowseFile()

    Dim fd As FileDialog
    Dim vrtSelectedItem As Variant
    Dim textline As String
    'vrtSelectedItem must be variant because of For Each loop
    Dim i As Integer

    'Allow the user to select multiple files
    Set fd = Application.FileDialog(msoFileDialogOpen)
    With fd
        .AllowMultiSelect = True
        .Filters.Add "Text", "*.txt", 1
        If .Show = -1 Then
            Sheets(1).name = "Data File"
            For Each vrtSelectedItem In .SelectedItems
                i = i + 1
                scount = 1
                Sheets(1).Cells(i + 1, 1) = vrtSelectedItem
                
                ActiveWorkbook.Sheets.Add After:=Worksheets(Worksheets.Count)
                WriteFile (vrtSelectedItem)
            Next vrtSelectedItem
            Sheets(1).Select
            Range("A1").Value = "Data Links as below:"
        Else
        End If
    End With
    Set fd = Nothing
    Call DivideData
    
End Sub

Sub WriteFile(vrtSelectedItem As Variant)
    Dim textline As String
    Dim j As Single
    
    Open vrtSelectedItem For Input As #1
                ActiveSheet.name = "P" & CStr(scount)
                Do Until EOF(1)
                    Line Input #1, textline
                    Cells(j + 1, 1).Value = textline
                    j = j + 1
                    'If file contains more than 1e6 rows
                    'Add new sheet
                    If j > 1000000# Then
                        ActiveWorkbook.Sheets.Add After:=Worksheets(Worksheets.Count)
                        j = 0
                        scount = scount + 1
                        ActiveSheet.name = "P" & CStr(scount)
                    End If
                Loop
                Close #1
End Sub

Sub DivideData()
    Dim i As Integer
    For i = 2 To Worksheets.Count
        Sheets(i).Select
        Columns("A:A").Select
        Selection.TextToColumns Destination:=Range("A1"), DataType:=xlFixedWidth, _
            FieldInfo:=Array(Array(0, 1), Array(16, 1), Array(32, 1), Array(48, 1)), _
            TrailingMinusNumbers:=True
    Next i
End Sub
