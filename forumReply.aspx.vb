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
            sql &= "	INSERT INTO [Reg_ForumReply]"
            sql &= "           ([RefFID]"
            sql &= "           ,[ReplyName]"
            sql &= "           ,[Description]           )"
            sql &= "     VALUES"
            sql &= "           ('" & Clean(hdnFID.Value) & "'"
            sql &= "           ,'" & Clean(txtTeamName.Text) & "'"
            sql &= "           ,'" & Clean(txtdescription.Text) & "'"
            sql &= "           )"

            If ExecuteNonQuery(sql) Then
                clearall()
                bindforum(hdnFID.Value)
                Dim msg = New JavaScriptSerializer().Serialize("Submit successfuly")
                ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "msg", "alert('" + msg + "')", True)
            End If

        Catch ex As Exception
            Dim msg = New JavaScriptSerializer().Serialize(ex.Message.ToString())
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "msg", "alert('" + msg + "')", True)
        End Try
    End Sub

    Private Sub clearall()
        'txtTeamName.Text = ""
        txtdescription.Text = ""
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Len(Request.QueryString("fid")) > 0 Then
                bindforum(Request.QueryString("fid"))
                hdnFID.Value = Request.QueryString("fid")
                txtTeamName.Text = getLoggedUserName()
            End If

        End If
    End Sub

    Private Sub bindforum(ByVal fid As Integer)
        Try

            Dim sql As String = "select F.*,isnull(FR.RID,0)RID from Reg_Forum F Left join Reg_ForumReply FR on F.FID=FR.RefFID where FID='" & fid & "' order by F.insertedon "

            sql = ""
            sql &= "	select Topic,teamName,F.Description FDES,F.insertedon FINSERtedon,replyname,FR.description,FR.Insertedon "
            sql &= "	,(select Count(1) from Reg_ForumReply where REfFID=F.FID)posts"
            sql &= "	from Reg_Forum F "
            sql &= "	Left join Reg_ForumReply FR on F.FID=FR.RefFID"
            sql &= "	where FID= '" & fid & "'"
            sql &= ""

            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                lbltopic.Text = ds.Tables(0).Rows(0)("Topic")
                lblteamName.Text = ds.Tables(0).Rows(0)("teamName")
                lbldescription.Text = ds.Tables(0).Rows(0)("FDES")
                lblinseredon.Text = CType(ds.Tables(0).Rows(0)("FINSERtedon"), DateTime).ToString("dd/MM/yyyy hh:mm:ss tt")

                lblposts.Text = ds.Tables(0).Rows(0)("posts")
                If lblposts.Text > 0 Then
                    gvforum.DataSource = ds
                    gvforum.DataBind()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
