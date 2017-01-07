Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Masters_Vendor
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillVendors()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillVendors()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            sql = "select ROW_NUMBER() over(order by VendorID desc) as row ,VendorID,VendorName,IsActive from APEX_Vendor"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvVendor.DataSource = ds
                gvVendor.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvVendor_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvVendor.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnVendorID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvVendor_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvVendor.PageIndexChanging
        Try
            gvVendor.PageIndex = e.NewPageIndex
            FillVendors()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class