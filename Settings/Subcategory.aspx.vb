Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Masters_Subcategory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillSubCategoryType()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillSubCategoryType()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "select ROW_NUMBER() OVER(ORDER BY sc.SubCategoryTypeID DESC) AS Row,"
            sql &= " ac.CategoryType,sc.SubCategoryTypeID, sc.SubCategoryType,sc.IsActive from APEX_SubCategory as "
            sql &= " sc  inner join APEX_Category as ac on ac.CategoryTypeID=sc.RefCategoryTypeID"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvSubcategory.DataSource = ds
                gvSubcategory.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvSubcategory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubcategory.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnSubCategoryTypeID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Protected Sub gvSubcategory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSubcategory.PageIndexChanging
        Try
            gvSubcategory.PageIndex = e.NewPageIndex
            FillSubCategoryType()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class