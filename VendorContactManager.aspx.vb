Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex
Imports System.Web.Script.Serialization

Partial Class Masters_VendorContactManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                divError.Visible = False
                Dim capex As New clsApex

                Dim role As String
                role = capex.GetRoleName(getLoggedUserName())
                If role = "F" Or role = "A" Or role = "H" Then
                    divError.Visible = False
                Else
                    CallDivError()
                End If


                If Len(Request.QueryString("mode")) > 0 Then
                    If Request.QueryString("mode") <> Nothing Then
                        If Request.QueryString("mode") <> "" Then
                            divError.Visible = False
                            Dim mode As String
                            mode = Request.QueryString("mode").ToString()
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
            sql = " SELECT [VID],[VendorNumber],[Category],[Name],[Name2],[Address],[Address2],[Phoneno],[City],[State],[Country],[PIN],[Contact], "
            sql &= "[contact2],[Email],[website],[CreditTeam],[PANNo],[ServiceTax],[TIN],[CIN],[KeyConcern],[Remarks],[InsertedOn],[Isactive],[IsDeleted]  FROM [dbo].[Apex_VendorFM] "
            sql &= "  where  VID=" & hdnVendorContactID.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                txtVNo.Text = ds.Tables(0).Rows(0)("VendorNumber")
                txtCategoryName.Text = ds.Tables(0).Rows(0)("Category")
                txtVendorName.Text = ds.Tables(0).Rows(0)("Name")
                txtvendorname2.Text = ds.Tables(0).Rows(0)("Name2")
                txtaddress.Text = ds.Tables(0).Rows(0)("Address")
                txtaddress2.Text = ds.Tables(0).Rows(0)("Address2")
                txtphoneno.Text = ds.Tables(0).Rows(0)("Phoneno")
                txtcity.Text = ds.Tables(0).Rows(0)("City")
                txtstate.Text = ds.Tables(0).Rows(0)("State")
                txtcountry.Text = ds.Tables(0).Rows(0)("Country")
                txtpin.Text = ds.Tables(0).Rows(0)("PIN")
                txtcontact.Text = ds.Tables(0).Rows(0)("Contact")
                txtcontact2.Text = ds.Tables(0).Rows(0)("Contact2")
                txtemailID.Text = ds.Tables(0).Rows(0)("Email")
                txtwebsite.Text = ds.Tables(0).Rows(0)("website")
                ddlcreditterm.SelectedValue = ds.Tables(0).Rows(0)("CreditTeam")
                txtpanno.Text = ds.Tables(0).Rows(0)("PANNo")
                txtservicetax.Text = ds.Tables(0).Rows(0)("ServiceTax")
                txttinVatLST.Text = ds.Tables(0).Rows(0)("TIN")
                txtCIN.Text = ds.Tables(0).Rows(0)("CIN")
                txtkeyconcern.Text = ds.Tables(0).Rows(0)("KeyConcern")
                txtremarks.Text = ds.Tables(0).Rows(0)("Remarks")

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

    Private Function CheckExistingContact() As Boolean
        Dim result As Boolean = True
        Try
            Dim ds As New DataSet
            Dim sql As String = ""

            sql = "select *  from Apex_VendorFM where Email='" & txtemailID.Text & "'"
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
            If CheckExistingContact() = False Then
                If chkIsActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"

                End If
                sql &= "IF EXISTS(SELECT 1 FROM Apex_VendorFM WHERE VendorNumber='" & txtVNo.Text.Trim() & "')"

                sql &= "	BEGIN"
                sql &= "		RAISERROR('This Vendor Number is already exists',16,1)"
                sql &= "	END"
                sql &= " ELSE"
                sql &= "	BEGIN "
                sql &= ""
                sql &= ""
                sql &= ""
                sql &= ""
                sql &= ""
                sql &= "	INSERT INTO [dbo].[Apex_VendorFM]"
                sql &= "           ([VendorNumber]"
                sql &= "           ,[Category]"
                sql &= "           ,[Name]"
                sql &= "           ,[Name2]"
                sql &= "           ,[Address]"
                sql &= "           ,[Address2]"
                sql &= "           ,[Phoneno]"
                sql &= "           ,[City]"
                sql &= "           ,[State]"
                sql &= "           ,[Country]"
                sql &= "           ,[PIN]"
                sql &= "           ,[Contact]"
                sql &= "           ,[contact2]"
                sql &= "           ,[Email]"
                sql &= "           ,[website]"
                sql &= "           ,[CreditTeam]"
                sql &= "           ,[PANNo]"
                sql &= "           ,[ServiceTax]"
                sql &= "           ,[TIN]"
                sql &= "           ,[CIN]"
                sql &= "           ,[KeyConcern]"
                sql &= "           ,[Remarks]"
                sql &= "           ,[InsertedBy],ISactive)"
                sql &= "     VALUES"
                sql &= "           ('" & Clean(txtVNo.Text) & "',"
                sql &= "           '" & Clean(txtCategoryName.Text) & "',"
                sql &= "           '" & Clean(txtVendorName.Text) & "', "
                sql &= "           '" & Clean(txtvendorname2.Text) & "', "
                sql &= "           '" & Clean(txtaddress.Text) & "', "
                sql &= "           '" & Clean(txtaddress2.Text) & "', "
                sql &= "           '" & Clean(txtphoneno.Text) & "', "
                sql &= "           '" & Clean(txtcity.Text) & "', "
                sql &= "           '" & Clean(txtstate.Text) & "', "
                sql &= "           '" & Clean(txtcountry.Text) & "', "
                sql &= "           '" & Clean(txtpin.Text) & "', "
                sql &= "           '" & Clean(txtcontact.Text) & "', "
                sql &= "           '" & Clean(txtcontact2.Text) & "', "
                sql &= "           '" & Clean(txtemailID.Text) & "', "
                sql &= "           '" & Clean(txtwebsite.Text) & "', "
                sql &= "           '" & Clean(ddlcreditterm.SelectedValue) & "', "
                sql &= "           '" & Clean(txtpanno.Text) & "', "
                sql &= "           '" & Clean(txtservicetax.Text) & "', "
                sql &= "           '" & Clean(txttinVatLST.Text) & "', "
                sql &= "           '" & Clean(txtCIN.Text) & "', "
                sql &= "           '" & Clean(txtkeyconcern.Text) & "',"
                sql &= "           '" & Clean(txtremarks.Text) & "', "
                sql &= "           '" & Clean(getLoggedUserID) & "','" & Active & "')"
                sql &= ""
                sql &= ""
                sql &= ""
                sql &= " END"
                sql &= ""
                sql &= ""
                sql &= ""
                sql &= ""

                'sql &= " ,'" & Active & "'," & getLoggedUserID() & ") "

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("VendorContact.aspx")
                End If
            Else
                lblError.Text = "Vendor already exists"
                divError.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            DataInsertion()
        Catch ex As Exception
            'SendExMail(ex.Message, ex.ToString())
            Dim msg = New JavaScriptSerializer().Serialize(ex.Message.ToString())
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "msg", "alert('" + msg + "')", True)
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

            sql &= "IF EXISTS(SELECT 1 FROM Apex_VendorFM WHERE VendorNumber='" & txtVNo.Text.Trim() & "' and VID<>'" & hdnVendorContactID.Value & "')"
            sql &= "	BEGIN"
            sql &= "		RAISERROR('This Vendor Number is already exists',16,1)"
            sql &= "	END"
            sql &= " ELSE"
            sql &= "	BEGIN "
            sql &= ""
            sql &= "	UPDATE [dbo].[Apex_VendorFM]"
            sql &= "	   SET [VendorNumber] = '" & Clean(txtVNo.Text) & "'"
            sql &= "      ,[Category] = '" & Clean(txtCategoryName.Text) & "'"
            sql &= "      ,[Name] = '" & Clean(txtVendorName.Text) & "'"
            sql &= "      ,[Name2] = '" & Clean(txtvendorname2.Text) & "'"
            sql &= "      ,[Address] = '" & Clean(txtaddress.Text) & "'"
            sql &= "      ,[Address2] = '" & Clean(txtaddress2.Text) & "'"
            sql &= "      ,[Phoneno] = '" & Clean(txtphoneno.Text) & "'"
            sql &= "      ,[City] = '" & Clean(txtcity.Text) & "'"
            sql &= "      ,[State] = '" & Clean(txtstate.Text) & "'"
            sql &= "      ,[Country] = '" & Clean(txtcountry.Text) & "'"
            sql &= "      ,[PIN] = '" & Clean(txtpin.Text) & "'"
            sql &= "      ,[Contact] = '" & Clean(txtcontact.Text) & "'"
            sql &= "      ,[contact2] = '" & Clean(txtcontact2.Text) & "'"
            sql &= "      ,[Email] = '" & Clean(txtemailID.Text) & "'"
            sql &= "      ,[website] = '" & Clean(txtwebsite.Text) & "'"
            sql &= "      ,[CreditTeam] = '" & Clean(ddlcreditterm.SelectedValue) & "'"
            sql &= "      ,[PANNo] = '" & Clean(txtpanno.Text) & "'"
            sql &= "      ,[ServiceTax] = '" & Clean(txtservicetax.Text) & "'"
            sql &= "      ,[TIN] = '" & Clean(txttinVatLST.Text) & "'"
            sql &= "      ,[CIN] = '" & Clean(txtCIN.Text) & "'"
            sql &= "      ,[KeyConcern] = '" & Clean(txtkeyconcern.Text) & "'"
            sql &= "      ,[Remarks] = '" & Clean(txtremarks.Text) & "'"
            sql &= "      ,[Isactive] = '" & Clean(Active) & "'"
            sql &= "       WHERE VID=" & hdnVendorContactID.Value

            sql &= " END"
            sql &= ""

           
            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("VendorContact.aspx")
            End If
        Catch ex As Exception
            'SendExMail(ex.Message, ex.ToString())
            Throw ex
        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            DataUpdation()
        Catch ex As Exception
            ' SendExMail(ex.Message, ex.ToString())
            Dim msg = New JavaScriptSerializer().Serialize(ex.Message.ToString())
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "msg", "alert('" + msg + "')", True)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            ClearTextBox(Me)
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