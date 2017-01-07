Imports Microsoft.VisualBasic
Imports clsDatabaseHelper
Imports clsMain


Public Class clsSuport
    Public Shared Function checkRound(ByVal values As String) As String
        Dim ressult As String = ""
        If values.Contains(".") Then
            Dim arr() As String = values.Split(".")
            If arr(1).Length = 1 Then
                values = values + "0"
            End If
        Else
            values = values + ".00"
        End If

        Return values
    End Function
    Public Shared Function returnRupies(ByVal str As String) As String
        Dim rupies As String = ""
        If str.Contains(".") Then
            Dim arr() As String = str.Split(".")
            Dim firststring As String = arr(0)
            Dim Secondstring As String = arr(1)
            rupies = convert(arr(0)) & " Rupees "
            '  rupies = rupies & convert(arr(1)) & " Paise "
        Else
            rupies = convert(str) & " Rupees "
        End If
        Return rupies
    End Function


    Public Shared Function convert(ByVal n As Double) As String
        Dim s As String = ""

        Dim x As Integer

        If n >= 100000 Then
            x = (n \ 100000)
            s = sngl(x) & " Lakh"
            n = n - (x * 100000)
        End If
        If n >= 1000 Then
            x = (n \ 1000)
            s = s & sngl(x) & " Thousand"
            n = n - (x * 1000)
        End If
        If n >= 100 Then
            x = (n \ 100)
            s = s & sngl(x) & " Hundred "
            n = n - (x * 100)
        End If
        If n > 10 And n < 20 Then
            s = s & betbeenTenTotwenty(x)
        Else
            x = (n \ 10)

            Select Case x
                Case 1
                    s = s & "Ten"
                Case 2
                    s = s & "Twenty"
                Case 3
                    s = s & "Thirty"
                Case 4
                    s = s & "Fourty"
                Case 5
                    s = s & "Fifty"
                Case 6
                    s = s & "Sixty"
                Case 7
                    s = s & "Seventy"
                Case 8
                    s = s & "Eighty"
                Case 9
                    s = s & "Ninety"
            End Select
            n = n - (x * 10)
        End If
        If n > 0 Then
            s = s & sngl(n)
        End If
        's = s & sngl(CInt(n))
        'If n = 0 Then s = "Zero"
        Return s
    End Function

    Public Shared Function overtentousent(ByVal x As Double) As String
        Dim s As String = ""
        Select Case x
            Case 2
                s = s & "Twenty"
            Case 3
                s = s & "Thirty"
            Case 4
                s = s & "Fourty"
            Case 5
                s = s & "Fifty"
            Case 6
                s = s & "Sixty"
            Case 7
                s = s & "Seventy"
            Case 8
                s = s & "Eighty"
            Case 9
                s = s & "Ninety"
        End Select
        Return s
    End Function
    Public Shared Function betbeenTenTotwenty(ByVal x As Double) As String
        Dim s As String = ""
        Select Case x
            Case 11
                s = s & "Eleven"
            Case 12
                s = s & "Twelve"
            Case 13
                s = s & "Thirteen"
            Case 14
                s = s & "Forty"
            Case 15
                s = s & "Fifteen"
            Case 16
                s = s & "Sixteen"
            Case 17
                s = s & "Seventeen"
            Case 18
                s = s & "Eighteen"
            Case 19
                s = s & "Nineteen"
        End Select
        Return s
    End Function
    Public Shared Function sngl(ByVal n As Integer) As String
        Dim s As String = ""
        Dim x As Integer
        If n > 10 And n < 20 Then

            s = s & betbeenTenTotwenty(n)
        ElseIf n > 10 Then
            x = (n \ 10)
            s = s & overtentousent(x)
            n = n - (x * 10)
        ElseIf n = 10 Then
            s = "Ten"
        End If
        Select Case n
            Case 1
                s = s & " One"
            Case 2
                s = s & " Two"
            Case 3
                s = s & " Three"
            Case 4
                s = s & " Four"
            Case 5
                s = s & " Five"
            Case 6
                s = s & " Six"
            Case 7
                s = s & " Seven"
            Case 8
                s = s & " Eight"
            Case 9
                s = s & " Nine"

        End Select
        sngl = s
    End Function
    Public Shared Function returnRequirevehicle(ByVal id As String, ByVal noofpassenger As String) As Integer
        Dim count As Integer = 0
        Dim resilt As String = ""
        Dim Sql As String = ""
        Sql = "Select Numberofpassenger from DAS_vehicletype  where Isdeleted='N' and  vehicleTypeID=" & id
        count = ExecuteSingleResult(Sql, _DataType.Numeric)
        If noofpassenger > count Then
            Dim i As String = noofpassenger / count
            If i.Contains(".") Then
                Dim intvalues() As String = i.Split(".")
                i = intvalues(0)
            End If
            Dim modlus As Double = noofpassenger Mod count
            If modlus > 0 Then
                i = i + 1
            End If
            resilt = i
        Else
            resilt = 1
        End If

        Return resilt
    End Function

End Class
