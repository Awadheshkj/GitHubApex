Imports clsDatabaseHelper
Imports System.Data
Imports clsApex

Partial Class Masters_VendorContact
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                FillClientContacts()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillClientContacts()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "select ROW_NUMBER() over(order by acc.VendorContactID desc) as row ,acc.VendorContactID,av.VendorName, "
            sql &= "acc.ContactName,acc.ContactDesignation,acc.ContactDepartment,acc.ContactOfficialEmailID,acc.Mobile1 +', ' +   acc.Mobile2 as Mobile, "
            sql &= " acc.IsActive from APEX_VendorContacts as acc inner join APEX_Vendor as av "
            sql &= " on av.VendorID=acc.RefVendorID "
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvVendorContact.DataSource = ds
                gvVendorContact.DataBind()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvVendorContact_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvVendorContact.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnVendorContactID"), HiddenField)
                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"
                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvVendorContact_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvVendorContact.PageIndexChanging
        Try
            gvVendorContact.PageIndex = e.NewPageIndex
            FillClientContacts()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class