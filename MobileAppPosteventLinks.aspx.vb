Imports clsDatabaseHelper

Partial Class MobileAppFeedback
    Inherits System.Web.UI.Page

    Private Sub MobileAppFeedback_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            divError.Visible = False
            MessageDiv.Visible = False
            If Len(Request.QueryString("LoginID")) > 0 Then
                If Len(Request.QueryString("jobcardID")) > 0 Then
                    hdnloginID.Value = Request.QueryString("LoginID")
                    hdnjobcardID.Value = Request.QueryString("jobcardID")
                End If
            Else
                CallDivError()
            End If
        End If
    End Sub

    Private Sub CallDivError()
        content.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub

    'Private Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
    '    Dim sql As String = ""

    '    sql &= "INSERT INTO [dbo].[Apex_MobileFeedback]"
    '    sql &= "           ([RefLoginID]"
    '    sql &= "           ,[RefjobcardID]"
    '    sql &= "           ,[Q1]"
    '    sql &= "           ,[Q2]"
    '    sql &= "           ,[Q3]"
    '    sql &= "           ,[Q4]"
    '    sql &= "           ,[Q5]"
    '    sql &= "           ,[Q6]"
    '    sql &= "           ,[Q7]"
    '    sql &= "           ,[Q8]"
    '    sql &= "           ,[Q9]"
    '    sql &= "           ,[Q10]"
    '    sql &= "           ,[Q11]"
    '    sql &= "           )"
    '    sql &= "     VALUES"
    '    sql &= "           ('" & hdnloginID.Value & "',"
    '    sql &= "           '" & hdnjobcardID.Value & "', "
    '    sql &= "         '" & rbtQ1.SelectedValue & "',"
    '    sql &= "          '" & rbtQ2.SelectedValue & "',"
    '    sql &= "          '" & rbtQ3.SelectedValue & "',"
    '    sql &= "          '" & rbtQ4.SelectedValue & "',"
    '    sql &= "          '" & rbtQ5.SelectedValue & "',"
    '    sql &= "         '" & rbtQ6.SelectedValue & "',"
    '    sql &= "          '" & rbtQ7.SelectedValue & "',"
    '    sql &= "         '" & rbtQ8.SelectedValue & "',"
    '    sql &= "          '" & rbtQ9.SelectedValue & "',"
    '    sql &= "         '" & rbtQ10.SelectedValue & "',"
    '    sql &= "          '" & rbtQ11.SelectedValue & "'"
    '    sql &= "           )"
    '    sql &= ""


    '    If ExecuteNonQuery(sql) Then
    '        content.Visible = False
    '        MessageDiv.Visible = True
    '        lblMsg.Text = "Thanks for give your valuable feedback."
    '    End If

    'End Sub

End Class
