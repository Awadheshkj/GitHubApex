Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Masters_State
    Inherits System.Web.UI.Page

    Private Sub FillState()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet


            sql = "select ROW_NUMBER() OVER(ORDER BY St.StateID DESC) AS Row,"
            sql &= " StateID,State,IsActive from APEX_State as St "

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvState.DataSource = ds
                gvState.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillState()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvState_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvState.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnStateID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvState_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvState.PageIndexChanging
        Try
            gvState.PageIndex = e.NewPageIndex
            FillState()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class