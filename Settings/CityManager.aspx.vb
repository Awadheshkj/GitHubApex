Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_CityManager
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim mode As String = ""
                divError.Visible = False
                Dim capex As New clsApex

                FillState()
                If Len(Request.QueryString("mode")) > 0 Then
                    If Not Request.QueryString("mode") = Nothing Then
                        If Request.QueryString("mode").ToString() <> "" Then
                            mode = Request.QueryString("mode").ToString()
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then
                                            hdnCity.Value = Convert.ToInt32(Request.QueryString("lid").ToString())
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillCityInEditMode()
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

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("City.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            ddlState.SelectedIndex = 0
            txtCity.Text = ""
            ChkActive.Checked = False
            divError.Visible = False
            lblError.Text = ""
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

    Private Function CheckDuplicateCity() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = ""

            Dim ds As New DataSet

            sql = "select * from APEX_City where City='" & Clean(txtCity.Text.Trim()) & "'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function

    Private Sub DataInsertion()
        Try
            Dim Active As Char
            Dim sql As String = ""

            If CheckDuplicateCity() = False Then
                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_City(RefStateID,City,IsActive) values(" & Clean(ddlState.SelectedValue) & ",'" & Clean(txtCity.Text.Trim()) & "','" & Active & "')"
                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("City.aspx")
                End If

            Else
                lblError.Text = "City already exists"
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

            sql = "update APEX_City  set RefStateID=" & Clean(ddlState.SelectedValue)
            sql &= ", City='" & Clean(txtCity.Text.Trim()) & "',IsActive='" & Active & "' where CityID=" & hdnCity.Value

            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("City.aspx")
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
    Private Sub FillState()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql &= "select StateID,State from APEX_State where IsActive='Y' and IsDeleted='N'"

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlState.DataSource = ds.Tables(0)
                ddlState.DataTextField = "State"
                ddlState.DataValueField = "StateID"
                ddlState.DataBind()
                ddlState.Items.Insert("0", New ListItem("Select State", "0"))
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillCityInEditMode()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            Dim Active As String = ""
            sql = "select  RefStateID,City,IsActive from APEX_City where CityID=" & hdnCity.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(ds.Tables(0).Rows(0)("RefStateID").ToString()))
                txtCity.Text = ds.Tables(0).Rows(0)("City").ToString()
                Active = ds.Tables(0).Rows(0)("IsActive").ToString()
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
End Class