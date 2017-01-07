Imports clsDatabaseHelper
Imports clsMain
Imports clsApex
Imports System.Data

Partial Class forgotpassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sql As String = ""
        Dim email As String = ""
        Dim pass As String = ""
        Dim epass As String = ""
        Dim epass1 As String = ""
        Dim ds As DataSet
        Dim memID As String = ""

        email = Request.Form("F")
        sql &= "Select username,password from APEX_Login where EmailID='" & email & "'"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            epass = ds.Tables(0).Rows(0)("password")
            memID = ds.Tables(0).Rows(0)("username")
            epass1 = epass.Remove(0, 1)
            pass = Decrypt(epass1)
            If SendEmail(pass, memID, email) Then
                Response.Write(gendata("true"))
                Response.End()
            Else
                Response.Write(gendata("false"))
                Response.End()
            End If
        Else
            Response.Write(gendata("false"))
            Response.End()
        End If

    End Sub

    Private Function SendEmail(ByVal pass As String, ByVal Memid As String, ByVal MemEmail As String) As Boolean
        Dim Subject As String = "Forgot Password"
        Dim mailBody As String = ""

        mailBody &= "Dear " & Memid & ",<br /><p>Your Login details is given below:</p><p>Username: " & Memid & "<br />Password : " & pass & "<br /></p><br /><br />Regards, <br />www.kestoneapps.com"

        Dim PlainText As String = ""
        Dim ToAddress As String = MemEmail
        Dim CCAddress As String = ""
        Return sendMail(Subject, mailBody, PlainText, ToAddress, CCAddress)
    End Function

    Private Function gendata(ByVal login As String) As String
        Dim table As New DataTable
        table.TableName = "Table1"

        ' Create four typed columns in the DataTable.
        table.Columns.Add("MailSent", GetType(String))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add(login)
        Dim jsonstring As String = ""

        jsonstring = GetJSONString(table).Replace(Chr(13), "").Replace(Chr(10), "")

        Return jsonstring
    End Function
End Class
