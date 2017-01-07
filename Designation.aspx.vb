Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Designation

    Inherits System.Web.UI.Page

    Private Sub FillDesignation()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "select ROW_NUMBER() OVER(ORDER BY Dg.DesignationID DESC) AS Row,"
            sql &= " DesignationID,Designation,IsActive from APEX_Designation as Dg "

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvDesignation.DataSource = ds
                gvDesignation.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex
                FillDesignation()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvDesignation_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDesignation.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnDesignationID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvDesignation_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDesignation.PageIndexChanging
        Try
            gvDesignation.PageIndex = e.NewPageIndex
            FillDesignation()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class
