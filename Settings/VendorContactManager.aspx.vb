Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_VendorContactManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                divError.Visible = False
                Dim capex As New clsApex
               
                If Len(Request.QueryString("mode")) > 0 Then
                    If Request.QueryString("mode") <> Nothing Then
                        If Request.QueryString("mode") <> "" Then
                            divError.Visible = False
                            Dim mode As String
                            mode = Request.QueryString("mode").ToString()
                            FillVendor()
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Request.QueryString("lid") <> Nothing Then
                                        If Request.QueryString("lid") <> "" Then
                                            hdnVendorContactID.Value = Request.QueryString("lid").ToString()
                                            FillVendorContactInEditMode()
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                        Else
                                            CallDivError()
                                        End If
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
                Else
                    CallDivError()
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillVendorContactInEditMode()
        Try
            Dim Active As Char

            Dim ds As New DataSet
            Dim sql As String = ""
            sql = " select RefVendorID,ContactName,ContactDesignation,ContactDepartment,  "
            sql &= "ContactOfficialEmailID,ContactPersonalEmailID,Mobile1,Mobile2,Landline1,Landline2, "
            sql &= "Extension,convert(varchar(10),ContactDateOfBirth,105) as ContactDateOfBirth,ContactSpouseName,convert(varchar(10),ContactAniversary,105) as ContactAniversary,IsActive "
            sql &= " from APEX_VendorContacts  where  VendorContactID=" & hdnVendorContactID.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlVendor.SelectedIndex = ddlVendor.Items.IndexOf(ddlVendor.Items.FindByValue(ds.Tables(0).Rows(0)("RefVendorID").ToString()))
                txtContactName.Text = ds.Tables(0).Rows(0)("ContactName").ToString()
                txtContactDesignation.Text = ds.Tables(0).Rows(0)("ContactDesignation").ToString()
                txtDept.Text = ds.Tables(0).Rows(0)("ContactDepartment").ToString()
                txtOfficialEmail.Text = ds.Tables(0).Rows(0)("ContactOfficialEmailID").ToString()
                txtPersonalEmail.Text = ds.Tables(0).Rows(0)("ContactPersonalEmailID").ToString()
                txtMobile1.Text = ds.Tables(0).Rows(0)("Mobile1").ToString()

                If ds.Tables(0).Rows(0)("Mobile2").ToString() <> "" Then
                    txtMobile2.Text = ds.Tables(0).Rows(0)("Mobile2").ToString()
                Else
                    txtMobile2.Text = ""
                End If

                If ds.Tables(0).Rows(0)("Landline1").ToString() <> "" Then
                    txtLandLine1.Text = ds.Tables(0).Rows(0)("Landline1").ToString()
                Else
                    txtLandLine1.Text = ""
                End If

                If ds.Tables(0).Rows(0)("Landline2").ToString() <> "" Then
                    txtLandLine2.Text = ds.Tables(0).Rows(0)("Landline2").ToString()
                Else
                    txtLandLine2.Text = ""
                End If

                If ds.Tables(0).Rows(0)("Extension").ToString() <> "" Then
                    txtExt.Text = ds.Tables(0).Rows(0)("Extension").ToString()
                Else
                    txtExt.Text = ""
                End If

                If ds.Tables(0).Rows(0)("ContactDateOfBirth").ToString() <> "" Then
                    txtDob.Text = ds.Tables(0).Rows(0)("ContactDateOfBirth").ToString()
                Else
                    txtDob.Text = ""
                End If

                If ds.Tables(0).Rows(0)("ContactSpouseName").ToString() <> "" Then
                    txtSpouse.Text = ds.Tables(0).Rows(0)("ContactSpouseName").ToString()
                Else
                    txtSpouse.Text = ""
                End If

                txtAnniv.Text = ds.Tables(0).Rows(0)("ContactAniversary").ToString()

                Active = ds.Tables(0).Rows(0)("IsActive").ToString()
                If Active = "Y" Then
                    chkIsActive.Checked = True
                Else
                    chkIsActive.Checked = False
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillVendor()
        Try
            Dim ds As New DataSet
            Dim sql As String = ""
            sql = "select VendorID,VendorName from APEX_Vendor where IsActive='Y' and IsDeleted='N' Order By VendorName"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlVendor.DataSource = ds.Tables(0)
                ddlVendor.DataTextField = "VendorName"
                ddlVendor.DataValueField = "VendorID"
                ddlVendor.DataBind()
                ddlVendor.Items.Insert(0, New ListItem("Select Vendor", "0"))
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckExistingContact() As Boolean
        Dim result As Boolean = True
        Try
            Dim ds As New DataSet
            Dim sql As String = ""

            sql = "select *  from APEX_VendorContacts where ContactOfficialEmailID='" & txtOfficialEmail.Text & "'"
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
            Dim dob As String = "convert(datetime,'" & txtDob.Text & "',105)"
            Dim sql As String = ""
            Dim Active As Char
            If CheckExistingContact() = False Then
                If chkIsActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"

                End If

                sql = "insert into APEX_VendorContacts(ContactName,RefVendorID,ContactDesignation,ContactDepartment, "
                sql &= "ContactOfficialEmailID,ContactPersonalEmailID,Mobile1,Mobile2,Landline1,Landline2,"
                sql &= "Extension,ContactDateOfBirth,ContactSpouseName,ContactAniversary,IsActive,InsertedBY) "

                sql &= " values('" & txtContactName.Text.Trim() & "',"
                sql &= ddlVendor.SelectedValue
                sql &= ",'" & txtContactDesignation.Text.Trim() & "'"
                sql &= ",'" & txtDept.Text.Trim() & "',"
                sql &= "'" & txtOfficialEmail.Text.Trim() & "'"

                If txtPersonalEmail.Text <> "" Then
                    sql &= ",'" & txtPersonalEmail.Text.Trim() & "'"
                Else
                    sql &= ",NULL"
                End If

                sql &= ",'" & txtMobile1.Text.Trim() & "'"

                If txtMobile2.Text <> "" Then
                    sql &= ",'" & txtMobile2.Text.Trim() & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtLandLine1.Text <> "" Then
                    sql &= ",'" & txtLandLine1.Text.Trim() & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtLandLine2.Text <> "" Then
                    sql &= ",'" & txtLandLine2.Text.Trim() & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtExt.Text <> "" Then
                    sql &= ",'" & txtExt.Text.Trim() & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtDob.Text <> "" Then
                    sql &= "," & dob
                Else
                    sql &= ",NULL"
                End If
                If txtSpouse.Text <> "" Then
                    sql &= ",'" & txtSpouse.Text.Trim() & "'"
                Else
                    sql &= ",NULL"
                End If
                If txtAnniv.Text <> "" Then
                    Dim anniv As String = "convert(datetime,'" & txtAnniv.Text & "',105)"
                    sql &= "," & anniv
                Else
                    sql &= ",NULL"
                End If

                sql &= " ,'" & Active & "'," & getLoggedUserID() & ") "

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("VendorContact.aspx")
                End If
            Else
                lblError.Text = "Vendor Contact already exists"
                divError.Visible = True
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

    Private Sub DataUpdation()
        Try
            Dim sql As String = ""
            Dim Active As Char
            If chkIsActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"

            End If
            sql = "update APEX_VendorContacts set ContactName='" & txtContactName.Text.Trim() & "'"
            sql &= ",RefVendorID=" & ddlVendor.SelectedItem.Value
            sql &= ",ContactDesignation='" & txtContactDesignation.Text.Trim() & "'"
            sql &= ",ContactDepartment='" & txtDept.Text.Trim() & "'"
            sql &= " ,ContactOfficialEmailID='" & txtOfficialEmail.Text.Trim() & "'"
            If txtPersonalEmail.Text <> "" Then
                sql &= ",ContactPersonalEmailID='" & txtPersonalEmail.Text.Trim() & "'"
            Else
                sql &= ",ContactPersonalEmailID=NULL"
            End If

            sql &= ",Mobile1='" & txtMobile1.Text.Trim() & "'"

            If txtMobile2.Text <> "" Then
                sql &= ",Mobile2='" & txtMobile2.Text.Trim() & "'"
            Else
                sql &= ",Mobile2=NULL"
            End If

            If txtLandLine1.Text <> "" Then
                sql &= ",Landline1='" & txtLandLine1.Text.Trim() & "'"
            Else
                sql &= ",Landline1=NULL"
            End If

            If txtLandLine2.Text <> "" Then
                sql &= ",Landline2='" & txtLandLine2.Text.Trim() & "'"
            Else
                sql &= ",Landline2=NULL"
            End If

            If txtExt.Text <> "" Then
                sql &= ",Extension='" & txtExt.Text.Trim() & "'"
            Else
                sql &= ",Extension=NULL"
            End If

            If txtDob.Text <> "" Then
                sql &= ",ContactDateOfBirth=convert(datetime,'" & txtDob.Text & "',105)"
            Else
                sql &= ",ContactDateOfBirth=NULL"
            End If

            If txtSpouse.Text <> "" Then
                sql &= ",ContactSpouseName='" & txtSpouse.Text.Trim() & "'"
            Else
                sql &= ",ContactSpouseName=NULL"
            End If

            If txtAnniv.Text <> "" Then
                sql &= ",ContactAniversary=convert(datetime,'" & txtAnniv.Text & "',105) "
            Else
                sql &= ",ContactAniversary=NULL "
            End If

            sql &= ",IsActive='" & Active & "'"

            sql &= ",ModifiedBY=" & getLoggedUserID() & " where VendorContactID=" & hdnVendorContactID.Value


            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("VendorContact.aspx")
            End If
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

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            ClearTextBox(Me)
            ddlVendor.SelectedIndex = 0
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Public Sub ClearTextBox(ByVal root As Control)
        Try
            For Each ctrl As Control In root.Controls
                ClearTextBox(ctrl)
                If TypeOf ctrl Is TextBox Then
                    CType(ctrl, TextBox).Text = String.Empty
                End If
            Next ctrl
            chkIsActive.Checked = False
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("VendorContact.aspx")
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
End Class