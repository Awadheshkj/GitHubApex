
Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Clients
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillClient()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillClient()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet


            sql = "select ROW_NUMBER() over (order by ClientID desc) as row ,ClientID,"
            sql &= " Client,Industry,AnnualTurnover,IsActive from APEX_Clients "

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvClient.DataSource = ds
                gvClient.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvClient_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvClient.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnClientID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvClient_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvClient.PageIndexChanging
        Try
            gvClient.PageIndex = e.NewPageIndex
            FillClient()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class