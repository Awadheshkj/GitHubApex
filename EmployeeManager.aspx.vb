Imports clsDatabaseHelper
Imports System.Data
Imports clsMain

Partial Class EmployeeManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            divError.Visible = False
            Dim mode As String
            Dim capex As New clsApex

            If Len(Request.QueryString("mode")) > 0 Then
                If Not Request.QueryString("mode") = Nothing Then
                    If Request.QueryString("mode") <> "" Then
                        FillState()
                        FillDesignation()
                        FillSuperiors()

                        mode = Request.QueryString("mode").ToString()
                        If mode = "edit" Then
                            If Len(Request.QueryString("lid")) > 0 Then
                                If Not Request.QueryString("lid").ToString() = Nothing Then
                                    If Request.QueryString("lid").ToString() <> "" Then
                                        hdnEmployee.Value = Request.QueryString("lid").ToString()

                                        btnEdit.Visible = True
                                        btnAdd.Visible = False
                                        FillEmployeeInEditMode()
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
    End Sub

    Private Sub FillEmployeeInEditMode()
        FillDesignation()
        FillSuperiors()
        BindState()
        BindCity()

        Dim Active As Char
        Dim ds As New DataSet
        Dim sql As String = ""

        sql = " select * from APEX_UsersDetails where  UserDetailsID=" & hdnEmployee.Value
        ds = ExecuteDataSet(sql)

        If ds.Tables(0).Rows.Count > 0 Then
            txtFname.Text = ds.Tables(0).Rows(0)("FirstName").ToString()
            txtLName.Text = ds.Tables(0).Rows(0)("LastName").ToString()
            txtMobile1.Text = ds.Tables(0).Rows(0)("Mobile1").ToString()
            txtMobile2.Text = ds.Tables(0).Rows(0)("Mobile2").ToString()
            ddlDesignation.SelectedValue = ds.Tables(0).Rows(0)("Designation").ToString()

            txtLandLine1.Text = ds.Tables(0).Rows(0)("PhoneNo1").ToString()
            txtLandLine2.Text = ds.Tables(0).Rows(0)("PhoneNo2").ToString()
            txtOfficialEmail.Text = ds.Tables(0).Rows(0)("EmailID").ToString()
            txtPersonalEmail.Text = ds.Tables(0).Rows(0)("PersonalEmailID").ToString()
            txtResAdd1.Text = ds.Tables(0).Rows(0)("ResidentAddressline1").ToString()
            txtResAdd2.Text = ds.Tables(0).Rows(0)("ResidentAddressline2").ToString()
            txtResPcode.Text = ds.Tables(0).Rows(0)("ResidentPinCode").ToString()

            ddlResState.SelectedValue = ds.Tables(0).Rows(0)("RefResidentState").ToString()
            ddlPemState.SelectedValue = ds.Tables(0).Rows(0)("RefPermanentState").ToString()



            ddlResCity.SelectedValue = ds.Tables(0).Rows(0)("RefResidentCity").ToString()
            ddlSuperior.SelectedValue = ds.Tables(0).Rows(0)("SuperiorID").ToString()

            txtPermAdd1.Text = ds.Tables(0).Rows(0)("PermanentAddressline1").ToString()
            txtPemAdd2.Text = ds.Tables(0).Rows(0)("PermanentAddressline2").ToString()



            ddlPemCity.SelectedValue = ds.Tables(0).Rows(0)("RefPermanentCity").ToString()

            txtPemPcode.Text = ds.Tables(0).Rows(0)("PermanentPinCode").ToString()
            txtempcode.Text = ds.Tables(0).Rows(0)("Empcode").ToString()
            Active = ds.Tables(0).Rows(0)("IsActive").ToString()
            If Active = "Y" Then
                chkIsActive.Checked = True
            Else
                chkIsActive.Checked = False
            End If
        End If
    End Sub
    Private Sub BindState()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select StateID,State from APEX_State where IsActive='Y' and IsDeleted='N' Order By State"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlResState.DataSource = ds
            ddlResState.DataTextField = "State"
            ddlResState.DataValueField = "StateID"
            ddlResState.DataBind()
            ddlPemState.DataSource = ds
            ddlPemState.DataTextField = "State"
            ddlPemState.DataValueField = "StateID"
            ddlPemState.DataBind()

        End If
        ddlResState.Items.Insert(0, New ListItem("Select State", "0"))
        ddlPemState.Items.Insert(0, New ListItem("Select State", "0"))

    End Sub

    Private Sub BindCity()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select CityID,City from APEX_City where IsActive='Y' and IsDeleted='N' Order By City"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlResCity.DataSource = ds
            ddlResCity.DataTextField = "City"
            ddlResCity.DataValueField = "CityID"
            ddlResCity.DataBind()
            ddlPemCity.DataSource = ds
            ddlPemCity.DataTextField = "City"
            ddlPemCity.DataValueField = "CityID"
            ddlPemCity.DataBind()

        End If
        ddlResCity.Items.Insert(0, New ListItem("Select State", "0"))
        ddlPemCity.Items.Insert(0, New ListItem("Select State", "0"))


    End Sub

    Private Sub FillState()
        ddlResState.Items.Clear()
        ddlPemState.Items.Clear()
        ddlResCity.Items.Clear()
        ddlPemCity.Items.Clear()

        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select StateID,State from APEX_State where IsActive='Y' and IsDeleted='N' Order By State"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlResState.DataSource = ds
            ddlResState.DataTextField = "State"
            ddlResState.DataValueField = "StateID"
            ddlResState.DataBind()
            ddlPemState.DataSource = ds
            ddlPemState.DataTextField = "State"
            ddlPemState.DataValueField = "StateID"
            ddlPemState.DataBind()
        End If

        ddlResState.Items.Insert(0, New ListItem("Select State", "0"))
        ddlPemState.Items.Insert(0, New ListItem("Select State", "0"))
        ddlResCity.Items.Insert(0, New ListItem("Select city", "0"))
        ddlPemCity.Items.Insert(0, New ListItem("Select city", "0"))
    End Sub

    Private Sub FillDesignation()
        ddlDesignation.Items.Clear()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select DesignationID,Designation from APEX_Designation where IsActive='Y' and IsDeleted='N' Order By Designation"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlDesignation.DataSource = ds
            ddlDesignation.DataTextField = "Designation"
            ddlDesignation.DataValueField = "DesignationID"
            ddlDesignation.DataBind()

        End If
        ddlDesignation.Items.Insert(0, New ListItem("Select Designation", "0"))
    End Sub

    Private Sub FillSuperiors()
        ddlSuperior.Items.Clear()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select UserDetailsID,FirstName + ' ' + isnull(LastName,'') as Name from APEX_UsersDetails where IsActive='Y' and IsDeleted='N' Order By FirstName"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlSuperior.DataSource = ds
            ddlSuperior.DataTextField = "Name"
            ddlSuperior.DataValueField = "UserDetailsID"
            ddlSuperior.DataBind()

        End If
        ddlSuperior.Items.Insert(0, New ListItem("Select Superior", "0"))
    End Sub


    Protected Sub ddlResState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlResState.SelectedIndexChanged
        If ddlResState.SelectedIndex > 0 Then
            Dim ds As New DataSet
            Dim sql As String = ""
            sql = "select CityID,City from APEX_City where RefStateID='" & ddlResState.SelectedItem.Value & "' Order By City"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlResCity.DataSource = ds
                ddlResCity.DataTextField = "City"
                ddlResCity.DataValueField = "CityID"
                ddlResCity.DataBind()
                ddlResCity.Items.Insert(0, New ListItem("Select City", "0"))
            Else
                ddlResCity.Items.Clear()
                ddlResCity.Items.Insert(0, New ListItem("Select City", "0"))
            End If
        Else
            ddlResCity.Items.Clear()
        End If

    End Sub

    Private Sub FillResCityInEdit()
        ddlResCity.Items.Clear()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select CityID,City from APEX_City where RefStateID='" & ddlResState.SelectedItem.Value & "' Order By City"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlResCity.DataSource = ds
            ddlResCity.DataTextField = "City"
            ddlResCity.DataValueField = "CityID"
            ddlResCity.DataBind()
        End If
        ddlResCity.Items.Insert(0, New ListItem("Select City", "0"))
    End Sub

    Private Sub FillPemCityInEdit()
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select CityID,City from APEX_City where RefStateID='" & ddlPemState.SelectedItem.Value & "' Order By City"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            ddlPemCity.DataSource = ds
            ddlPemCity.DataTextField = "City"
            ddlPemCity.DataValueField = "CityID"
            ddlPemCity.DataBind()
        End If
        ddlPemCity.Items.Insert(0, New ListItem("Select City", "0"))
    End Sub

    Protected Sub ddlPemState_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPemState.SelectedIndexChanged
        ddlPemCity.Items.Clear()
        If ddlPemState.SelectedIndex > 0 Then
            Dim ds As New DataSet
            Dim sql As String = ""
            sql = "select CityID,City from APEX_City where RefStateID=" & ddlPemState.SelectedItem.Value & " Order By City"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlPemCity.DataSource = ds.Tables(0)
                ddlPemCity.DataTextField = "City"
                ddlPemCity.DataValueField = "CityID"
                ddlPemCity.DataBind()
            End If
        Else
            ddlPemCity.Items.Clear()
        End If
        ddlPemCity.Items.Insert(0, New ListItem("Select City", "0"))
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        DataInsertion()
    End Sub

    Private Sub DataInsertion()
        If CheckExistingEmployee() = False Then
            Dim sql As String = ""
            Dim role As String = ""
            If ddlDesignation.SelectedValue = 1 Then
                role = "A"
            ElseIf ddlDesignation.SelectedValue = 2 Then
                role = "K"
            ElseIf ddlDesignation.SelectedValue = 4 Then
                role = "H"
            Else
                role = "O"

            End If

            Dim Active As Char
            If chkIsActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"
            End If

            sql = "insert into APEX_UsersDetails(FirstName,LastName,SuperiorID,Mobile1,Mobile2,Designation,PhoneNo1, "
            sql &= "PhoneNo2,EmailID,PersonalEmailID,ResidentAddressline1,ResidentAddressline2, "
            sql &= "RefResidentState,RefResidentCity,ResidentPinCode,PermanentAddressline1, "
            sql &= " PermanentAddressline2,RefPermanentState,RefPermanentCity,PermanentPinCode,IsActive,Role,InsertedBy,Empcode)"
            sql &= " values('" & Clean(txtFname.Text.Trim()) & "'"
            sql &= ",'" & Clean(txtLName.Text.Trim()) & "'"
            If ddlSuperior.SelectedIndex > 0 Then
                sql &= "," & Clean(ddlSuperior.SelectedItem.Value)
            Else
                sql &= ",NULL"
            End If

            sql &= ",'" & Clean(txtMobile1.Text.Trim()) & "'"

            If txtMobile2.Text <> "" Then
                sql &= ",'" & Clean(txtMobile2.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If

            sql &= "," & Clean(ddlDesignation.SelectedItem.Value)

            If txtLandLine1.Text <> "" Then
                sql &= ",'" & Clean(txtLandLine1.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If

            If txtLandLine2.Text <> "" Then
                sql &= ",'" & Clean(txtLandLine2.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If


            sql &= ",'" & Clean(txtOfficialEmail.Text.Trim()) & "'"

            If txtPersonalEmail.Text <> "" Then
                sql &= ",'" & Clean(txtPersonalEmail.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If

            sql &= ",'" & Clean(txtResAdd1.Text.Trim()) & "'"
            sql &= ",'" & Clean(txtResAdd2.Text.Trim()) & "'"
            sql &= "," & Clean(ddlResState.SelectedItem.Value)
            sql &= "," & Clean(ddlResCity.SelectedItem.Value)
            sql &= ",'" & Clean(txtResPcode.Text.Trim()) & "'"
            sql &= ",'" & Clean(txtPermAdd1.Text.Trim()) & "'"
            sql &= ",'" & Clean(txtPemAdd2.Text.Trim()) & "'"
            sql &= "," & Clean(ddlPemState.SelectedItem.Value)
            sql &= "," & Clean(ddlPemCity.SelectedItem.Value)
            sql &= ",'" & Clean(txtPemPcode.Text.Trim()) & "'"
            sql &= ",'" & Active & "','" & role & "'," & getLoggedUserID() & ",'" & txtempcode.Text.Trim() & "')"

            If ExecuteNonQuery(sql) > 0 Then
                insertintoLoginTable()
                Response.Redirect("Employee.aspx")
            End If
        Else
            lblError.Text = "Employee already exists"
            divError.Visible = True
        End If
    End Sub

    Private Sub DataUpdation()
        Dim sql As String = ""
        Dim Active As Char

        If chkIsActive.Checked = True Then
            Active = "Y"
        Else
            Active = "N"
        End If

        sql = "update APEX_UsersDetails set FirstName='" & Clean(txtFname.Text.Trim()) & "'"
        sql &= ",LastName='" & Clean(txtLName.Text.Trim()) & "'"

        If ddlSuperior.SelectedIndex > 0 Then
            sql &= ",SuperiorID='" & Clean(ddlSuperior.SelectedValue) & "'"
        Else
            sql &= ",SuperiorID=NULL"
        End If

        sql &= ",Mobile1='" & Clean(txtMobile1.Text.Trim()) & "'"

        If txtMobile2.Text <> "" Then
            sql &= " ,Mobile2='" & Clean(txtMobile2.Text.Trim()) & "'"
        Else
            sql &= " ,Mobile2=NULL"
        End If

        sql &= ",Designation='" & Clean(ddlDesignation.SelectedValue) & "'"

        If txtLandLine1.Text <> "" Then
            sql &= ",PhoneNo1='" & Clean(txtLandLine1.Text.Trim()) & "', "
        Else
            sql &= ",PhoneNo1=NULL, "
        End If

        If txtLandLine2.Text <> "" Then
            sql &= "PhoneNo2='" & Clean(txtLandLine2.Text.Trim()) & "'"
        Else
            sql &= "PhoneNo2=NULL"
        End If

        sql &= ",EmailID='" & Clean(txtOfficialEmail.Text.Trim()) & "'"

        If txtPersonalEmail.Text <> "" Then
            sql &= ",PersonalEmailID='" & Clean(txtPersonalEmail.Text.Trim()) & "'"
        Else
            sql &= ",PersonalEmailID=NULL"
        End If

        sql &= ",ResidentAddressline1='" & Clean(txtResAdd1.Text.Trim()) & "'"
        sql &= ",ResidentAddressline2='" & Clean(txtResAdd2.Text.Trim()) & "', "
        sql &= "RefResidentState='" & Clean(ddlResState.SelectedItem.Value) & "'"
        sql &= ",RefResidentCity='" & Clean(ddlResCity.SelectedItem.Value) & "'"
        sql &= ",ResidentPinCode='" & Clean(txtResPcode.Text.Trim()) & "'"
        sql &= ",PermanentAddressline1='" & Clean(txtPermAdd1.Text.Trim()) & "', "
        sql &= " PermanentAddressline2='" & Clean(txtPemAdd2.Text.Trim()) & "'"
        sql &= ",RefPermanentState='" & Clean(ddlPemState.SelectedItem.Value) & "'"
        sql &= ",RefPermanentCity='" & Clean(ddlPemCity.SelectedItem.Value) & "'"
        sql &= ",PermanentPinCode='" & Clean(txtPemPcode.Text.Trim()) & "',IsActive='" & Active & "',ModifiedOn=getdate(),ModifiedBy=" & getLoggedUserID() & ",Empcode='" & txtempcode.Text & "'  where UserDetailsID=" & hdnEmployee.Value

        If ExecuteNonQuery(sql) > 0 Then
            Response.Redirect("Employee.aspx")
        End If
    End Sub

    Private Function CheckExistingEmployee() As Boolean
        Dim result As Boolean = True
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "select * from APEX_UsersDetails where EmailID='" & txtOfficialEmail.Text & "'"
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = True
        Else
            result = False
            lblError.Text = ""
            divError.Visible = False
        End If
        Return result
    End Function

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        DataUpdation()
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ClearTextBox(Me)
    End Sub

    Public Sub ClearTextBox(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            ClearTextBox(ctrl)
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            End If
        Next ctrl

        ddlDesignation.SelectedIndex = 0
        ddlResState.SelectedIndex = 0
        ddlResCity.SelectedIndex = 0
        ddlPemState.SelectedIndex = 0
        ddlPemCity.SelectedIndex = 0
        chkIsActive.Checked = False
        ddlSuperior.SelectedIndex = 0
    End Sub

    Protected Sub chkAsAbove_CheckedChanged(sender As Object, e As EventArgs) Handles chkAsAbove.CheckedChanged
        If chkAsAbove.Checked = True Then
            txtPermAdd1.Text = txtResAdd1.Text.Trim()
            txtPemAdd2.Text = txtResAdd2.Text.Trim()
            ddlPemState.SelectedValue = ddlResState.SelectedValue
            txtPemPcode.Text = txtResPcode.Text.Trim()


            Dim ds As New DataSet
            Dim sql As String = ""

            sql = "select CityID,City from APEX_City where RefStateID='" & ddlPemState.SelectedValue & "' Order By City"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlPemCity.DataSource = ds.Tables(0)
                ddlPemCity.DataTextField = "City"
                ddlPemCity.DataValueField = "CityID"
                ddlPemCity.DataBind()
                ddlPemCity.Items.Insert(0, New ListItem("Select City", "0"))
            End If


            ddlPemCity.SelectedValue = ddlResCity.SelectedValue
        Else
        End If
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Employee.aspx")
    End Sub


    Private Sub CallDivError()
        'divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub

    Private Sub insertintoLoginTable()
        Dim sql As String = ""
        Dim RefudID As Integer = 0

        sql = "Select Top(1) UserDetailsID  from APEX_UsersDetails order by UserDetailsID desc "
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)

        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("UserDetailsID")) Then
                RefudID = ds.Tables(0).Rows(0)("UserDetailsID")
            End If
        End If
        Dim username As String = ""
        If txtOfficialEmail.Text <> "" Then
            Dim email As String = Clean(txtOfficialEmail.Text)
            Dim emailarray() As String = email.Split("@")
            If emailarray.Length > 0 Then
                username = emailarray(0)
            End If
        End If

        If RefudID > 0 And username <> "" Then
            sql = "insert into APEX_login(Username,Password,FirstName,LastName,DisplayName "
            sql &= ",EmailID ,RefUserdetailsID,IsActive,InsertedBy ) "

            sql &= " values('" & Clean(username) & "'"
            sql &= ",'" & Clean(username) & "'"

            If txtFname.Text <> "" Then
                sql &= ",'" & Clean(txtFname.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If
            If txtLName.Text <> "" Then
                sql &= ",'" & Clean(txtLName.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If
            If txtFname.Text <> "" Then
                sql &= ",'" & Clean(txtFname.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If

            If txtPersonalEmail.Text <> "" Then
                sql &= ",'" & Clean(txtPersonalEmail.Text.Trim()) & "'"
            Else
                sql &= ",NULL"
            End If
            sql &= "," & RefudID & ""

            sql &= ",'Y'," & getLoggedUserID() & ")"

            ExecuteNonQuery(sql)
        End If

    End Sub

End Class

