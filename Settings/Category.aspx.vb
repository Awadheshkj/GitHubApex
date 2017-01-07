Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Category
    Inherits System.Web.UI.Page

    Dim ds As New DataSet
    Dim sql As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillCategory()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillCategory()
        Try
            sql = "select ROW_NUMBER() OVER(ORDER BY CategoryTypeID DESC) AS Row,CategoryTypeID,CategoryType,IsActive from APEX_Category"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvCategory.DataSource = ds
                gvCategory.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvCategory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCategory.PageIndexChanging
        Try
            gvCategory.PageIndex = e.NewPageIndex
            FillCategory()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvCategory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCategory.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnCategoryTypeID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class