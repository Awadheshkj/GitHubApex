Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Activity
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Dim sql As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                FillActivity()
                Dim capex As New clsApex
                ' TWContent.InnerHtml = capex.GetActivityList()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillActivity()
        Try
            sql = "select ROW_NUMBER() OVER(ORDER BY ProjectTypeID DESC) AS Row,ProjectTypeID,ProjectType,IsActive,Allowprofit from APEX_ActivityType"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvActivity.DataSource = ds
                gvActivity.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvActivity_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvActivity.PageIndexChanging
        Try
            gvActivity.PageIndex = e.NewPageIndex
            FillActivity()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvActivity_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvActivity.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnActivityID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

End Class