Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_ClientContactManager
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim capex As New clsApex

                divError.Visible = False
                Dim mode As String
                FillClients()
                If Len(Request.QueryString("mode")) > 0 Then
                    If Not Request.QueryString("mode") = Nothing Then
                        If Request.QueryString("mode").ToString() <> "" Then
                            mode = Request.QueryString("mode").ToString()
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then
                                            hdnContactID.Value = Convert.ToInt32(Request.QueryString("lid").ToString())
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillClientContactInEditMode()
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
    Private Sub FillClientContactInEditMode()
        Try
            Dim Active As Char

            Dim ds As New DataSet
            Dim sql As String = ""
            sql = " select RefClientID,ContactName,ContactDesignation,ContactDepartment,  "
            sql &= "ContactOfficialEmailID,ContactPersonalEmailID,Mobile1,Mobile2,Landline1,Landline2, "
            sql &= "Extension,convert(varchar(10),ContactDateOfBirth,105) as ContactDateOfBirth,ContactSpouseName,convert(varchar(10),ContactAniversary,105) as ContactAniversary,IsActive "
            sql &= " from APEX_ClientContacts  where  ContactID=" & hdnContactID.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlClients.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("RefClientID").ToString())
                txtContactName.Text = ds.Tables(0).Rows(0)("ContactName").ToString()
                txtContactDesignation.Text = ds.Tables(0).Rows(0)("ContactDesignation").ToString()
                txtDept.Text = ds.Tables(0).Rows(0)("ContactDepartment").ToString()
                txtOfficialEmail.Text = ds.Tables(0).Rows(0)("ContactOfficialEmailID").ToString()

                If ds.Tables(0).Rows(0)("ContactPersonalEmailID").ToString() <> "" Then
                    txtPersonalEmail.Text = ds.Tables(0).Rows(0)("ContactPersonalEmailID").ToString()
                Else
                    txtPersonalEmail.Text = ""
                End If

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

                If ds.Tables(0).Rows(0)("ContactAniversary").ToString() <> "" Then
                    txtAnniv.Text = ds.Tables(0).Rows(0)("ContactAniversary").ToString()
                Else
                    txtAnniv.Text = ""
                End If
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

    Private Sub FillClients()
        Try
            Dim ds As New DataSet
            Dim sql As String = ""
            sql = "select ClientID,Client from APEX_Clients where IsActive='Y' and IsDeleted='N' Order By Client"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlClients.DataSource = ds.Tables(0)
                ddlClients.DataTextField = "Client"
                ddlClients.DataValueField = "ClientID"
                ddlClients.DataBind()
                ddlClients.Items.Insert(0, New ListItem("Select Client", "0"))
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

            sql = "select * from APEX_ClientContacts where ContactOfficialEmailID = '" & Clean(txtOfficialEmail.Text) & "'"
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
            If CheckExistingContact() = False Then
                Dim sql As String = ""
                Dim Active As Char

                If chkIsActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If
                sql = "insert into APEX_ClientContacts(ContactName,RefClientID,ContactDesignation,ContactDepartment, "
                sql &= "ContactOfficialEmailID,ContactPersonalEmailID,Mobile1,Mobile2,Landline1,Landline2,"
                sql &= "Extension,ContactDateOfBirth,ContactSpouseName,ContactAniversary,IsActive,InsertedBY) "

                sql &= " values('" & Clean(txtContactName.Text.Trim()) & "'"
                sql &= ",'" & Clean(ddlClients.SelectedValue) & "'"
                sql &= ",'" & Clean(txtContactDesignation.Text.Trim()) & "'"
                sql &= ",'" & Clean(txtDept.Text.Trim()) & "'"
                sql &= ",'" & Clean(txtOfficialEmail.Text.Trim()) & "'"

                If txtPersonalEmail.Text.Trim() <> "" Then
                    sql &= ",'" & Clean(txtPersonalEmail.Text.Trim()) & "'"
                Else
                    sql &= ",NULL"
                End If

                sql &= ",'" & Clean(txtMobile1.Text.Trim()) & "'"

                If txtMobile2.Text.Trim() <> "" Then
                    sql &= ",'" & Clean(txtMobile2.Text.Trim()) & "' "
                Else
                    sql &= ",Null"
                End If

                If txtLandLine1.Text.Trim() <> "" Then
                    sql &= ",'" & Clean(txtLandLine1.Text.Trim()) & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtLandLine2.Text.Trim() <> "" Then
                    sql &= ",'" & Clean(txtLandLine2.Text.Trim()) & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtExt.Text.Trim() <> "" Then
                    sql &= ",'" & Clean(txtExt.Text.Trim()) & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtDob.Text <> "" Then
                    sql &= ",convert(datetime,'" & txtDob.Text & "',105)"
                Else
                    sql &= ",NULL"
                End If

                If txtSpouse.Text.Trim() <> "" Then
                    sql &= ",'" & Clean(txtSpouse.Text.Trim()) & "'"
                Else
                    sql &= ",NULL"
                End If

                If txtAnniv.Text <> "" Then
                    sql &= ",convert(datetime,'" & txtAnniv.Text & "',105)"
                Else
                    sql &= ",NULL"
                End If

                sql &= ",'" & Active & "'," & getLoggedUserID() & ") "

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("ClientContact.aspx")
                End If
            Else
                divError.Attributes.Remove("alert-success")
                divError.Attributes.Add("class", "alert alert-danger")
                lblError.Text = "Contact already exists"
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

            sql = "update APEX_ClientContacts set ContactName='" & Clean(txtContactName.Text.Trim()) & "'"
            sql &= ",RefClientID='" & Clean(ddlClients.SelectedValue) & "'"
            sql &= ",ContactDesignation='" & Clean(txtContactDesignation.Text.Trim()) & "'"
            sql &= ",ContactDepartment='" & Clean(txtDept.Text.Trim()) & "'"
            sql &= " ,ContactOfficialEmailID='" & Clean(txtOfficialEmail.Text.Trim()) & "'"
            If txtPersonalEmail.Text.Trim() <> "" Then
                sql &= ",ContactPersonalEmailID='" & Clean(txtPersonalEmail.Text.Trim()) & "'"
            Else
                sql &= ",ContactPersonalEmailID=NULL"
            End If

            sql &= ",Mobile1='" & Clean(txtMobile1.Text.Trim()) & "'"

            If txtMobile2.Text.Trim() <> "" Then
                sql &= ",Mobile2='" & Clean(txtMobile2.Text.Trim()) & "'"
            Else
                sql &= ",Mobile2=NULL"
            End If

            If txtLandLine1.Text.Trim() <> "" Then
                sql &= ",Landline1='" & Clean(txtLandLine1.Text.Trim()) & "'"
            Else
                sql &= ",Landline1=NULL"
            End If

            If txtLandLine2.Text.Trim() <> "" Then
                sql &= ",Landline2='" & Clean(txtLandLine2.Text.Trim()) & "'"
            Else
                sql &= ",Landline2=NULL"
            End If

            If txtExt.Text.Trim() <> "" Then
                sql &= ",Extension='" & Clean(txtExt.Text.Trim()) & "'"
            Else
                sql &= ",Extension=NULL"
            End If

            If txtDob.Text <> "" Then
                sql &= ",ContactDateOfBirth=convert(datetime,'" & txtDob.Text & "',105)"
            Else
                sql &= ",ContactDateOfBirth=NULL"
            End If

            If txtSpouse.Text.Trim() <> "" Then
                sql &= ",ContactSpouseName='" & Clean(txtSpouse.Text.Trim()) & "'"
            Else
                sql &= ",ContactSpouseName=NULL"
            End If

            If txtAnniv.Text <> "" Then
                sql &= ",ContactAniversary=convert(datetime,'" & txtAnniv.Text & "',105)"
            Else
                sql &= ",ContactAniversary=NULL"
            End If
            sql &= ",IsActive='" & Active & "'"
            sql &= ",ModifiedBY='" & getLoggedUserID() & "' where ContactID=" & hdnContactID.Value

            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("ClientContact.aspx")

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
            ddlClients.SelectedIndex = 0
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
            lblError.Text = ""
            divError.Visible = False
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("ClientContact.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub txtOfficialEmail_TextChanged(sender As Object, e As EventArgs) Handles txtOfficialEmail.TextChanged
        Try
            CheckExistingContact()
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