Imports System.Data
Imports System.Data.SqlClient
Imports clsDatabaseHelper
Imports System.Web.Script.Serialization
Imports clsMain
Imports System.Globalization

Partial Class forum
    Inherits System.Web.UI.Page

    Protected Sub btnsubmit_Click(sender As Object, e As EventArgs) Handles btnsubmit.Click
        Try

            Dim sql As String = ""
            sql &= "	INSERT INTO [dbo].[Reg_Forum]"
            sql &= "           ([Topic]"
            sql &= "           ,[TeamName]"
            sql &= "           ,[Description],Insertedby    )"
            sql &= "     VALUES"
            sql &= "           ('" & Clean(txtTopic.Text) & "'"
            sql &= "           ,'" & Clean(txtTeamName.Text) & "'"
            sql &= "           ,'" & Clean(txtdescription.Text) & "'"
            sql &= "           ,'" & Clean(getLoggedUserName()) & "'"
            sql &= "           )"

            If ExecuteNonQuery(sql) Then
                clearall()
                bindforum()
                Dim msg = New JavaScriptSerializer().Serialize("Submit successfuly")
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "msg", "alert('" + msg + "')", True)
            End If

        Catch ex As Exception
            Dim msg = New JavaScriptSerializer().Serialize(ex.Message.ToString())
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "msg", "alert('" + msg + "')", True)
        End Try
    End Sub

    Private Sub clearall()
        txtTopic.Text = ""
        txtTeamName.Text = ""
        txtdescription.Text = ""
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bindforum()
        End If
    End Sub

    Private Sub bindforum()
        Try
            Dim sql As String = "select Distinct FID,F.*,(select Count(1) from Reg_ForumReply where REfFID=F.FID)posts from Reg_Forum F Left join Reg_ForumReply FR on F.FID=FR.RefFID order by F.insertedon "
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvforum.DataSource = ds
                gvforum.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
