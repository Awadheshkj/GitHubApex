Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_VendorManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex
                'TWContent.InnerHtml = capex.GetActivityList()
                divError.Visible = False
                Dim mode As String = ""
                If Len(Request.QueryString("mode")) > 0 Then
                    If Not Request.QueryString("mode") = Nothing Then
                        mode = Request.QueryString("mode").ToString()
                        bindcategory()
                        If mode = "edit" Then
                            If Len(Request.QueryString("lid")) > 0 Then
                                If Not Request.QueryString("lid") = Nothing Then
                                    hdnVendor.Value = Request.QueryString("lid").ToString()
                                    btnEdit.Visible = True
                                    btnAdd.Visible = False
                                    FillVendorInEditMode()
                                Else
                                    CallDivError()
                                End If
                            Else
                                CallDivError()
                            End If
                        ElseIf mode = "add" Then

                            btnAdd.Visible = True
                            btnEdit.Visible = False
                        End If
                    Else
                        CallDivError()
                    End If
                Else
                    CallDivError()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckDuplicateVendor() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            sql = "select * from APEX_Vendor where isActive='Y' and Isdeleted='N' and VendorName='" & txtVendor.Text & "'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
                divError.Visible = False
                lblError.Text = ""
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Sub DataInsertion()
        Try
            Dim sql As String = ""
            Dim Active As Char
            Dim ds As New DataSet

            If CheckDuplicateVendor() = False Then
                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_Vendor(VendorName,RefCategoryID,IsActive,InsertedBy)"
                sql &= "values('" & Clean(txtVendor.Text.Trim()) & "'," & ddlvendorCategory.SelectedValue & " ,"
                sql &= " '" & Active & "'," & getLoggedUserID() & ")"
                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Vendor.aspx")
                End If
                divError.Visible = False
            Else
                lblError.Text = "Vendor already exists"
                divError.Visible = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub DataUpdation()
        Try
            Dim Active As String = ""
            Dim sql As String = ""

            If ChkActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"
            End If

            sql = "update APEX_Vendor  set VendorName='" & txtVendor.Text.Trim() & "', "
            sql &= "RefCategoryID=" & Clean(ddlvendorCategory.SelectedValue) & ","
            sql &= "IsActive='" & Active & "', "
            sql &= " ModifiedBy='" & getLoggedUserID() & "' where VendorID=" & hdnVendor.Value

            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Vendor.aspx")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            DataInsertion()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            DataUpdation()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Vendor.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtVendor.Text = ""
            ChkActive.Checked = False
            lblMsg.Text = ""
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillVendorInEditMode()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            Dim Active As String = ""
            sql = "select VendorName,IsActive,RefCategoryID from APEX_Vendor where VendorID=" & hdnVendor.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                txtVendor.Text = ds.Tables(0).Rows(0)("VendorName").ToString()
                Active = ds.Tables(0).Rows(0)("IsActive").ToString()
                If Not IsDBNull(ds.Tables(0).Rows(0)("RefCategoryID")) Then
                    ddlvendorCategory.SelectedValue = ds.Tables(0).Rows(0)("RefCategoryID")
                End If

                If Active = "Y" Then
                    ChkActive.Checked = True
                Else
                    ChkActive.Checked = False
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub CallDivError()
        Try
            divContent.Visible = False
            divError.Visible = True
            Dim capex As New clsApex
            lblError.Text = capex.SetErrorMessage()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub bindcategory()
        Try
            Dim sql As String = ""
            sql = "Select vendorcategoryID,vendorcategory from APEX_Vendorcategory where isActive='Y' and IsDeleted='N' order by vendorcategory desc"
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlvendorCategory.DataSource = ds.Tables(0)
                ddlvendorCategory.DataTextField = "vendorcategory"
                ddlvendorCategory.DataValueField = "vendorcategoryID"
                ddlvendorCategory.DataBind()
            End If
            ddlvendorCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class