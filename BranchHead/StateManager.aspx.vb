Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_StateManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim mode As String

                Dim capex As New clsApex
                divError.Visible = False
                If Len(Request.QueryString("mode")) > 0 Then
                    If Not Request.QueryString("mode") = Nothing Then
                        If Request.QueryString("mode").ToString() <> "" Then
                            mode = Request.QueryString("mode").ToString()
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then
                                            hdnStateID.Value = Convert.ToInt32(Request.QueryString("lid").ToString())

                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillStateInEditMode()
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

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtState.Text = ""
            ChkActive.Checked = False
            lblMsg.Text = ""
            divError.Visible = False
            lblError.Text = ""
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub DataUpdation()
        Try
            Dim Active As String = ""
            Dim sql As String = ""
            Dim AffectedRows As Integer
            If ChkActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"
            End If

            sql = "update APEX_State  set State='" & Clean(txtState.Text.Trim()) & "',IsActive='" & Active & "' where StateID='" & hdnStateID.Value & "'"
            AffectedRows = ExecuteNonQuery(sql)
            If AffectedRows > 0 Then
                Response.Redirect("State.aspx")
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
    Private Sub DataInsertion()
        Try
            Dim Active As Char
            Dim sql As String = ""

            If CheckDuplicateState() = False Then

                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_State(State,IsActive) values('" & Clean(txtState.Text.Trim()) & "','" & Active & "')"

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("State.aspx")
                End If
            Else
                lblError.Text = "State already exists"
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

    Private Function CheckDuplicateState() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = ""
            Dim dupState As String = ""
            Dim ds As New DataSet

            sql = "select * from APEX_State where State='" & Clean(txtState.Text) & "'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
                lblError.Text = ""
                divError.Visible = False
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("State.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillStateInEditMode()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            Dim Active As String = ""
            sql = "select  State,IsActive from APEX_State where StateID='" & Clean(hdnStateID.Value) & "'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                txtState.Text = ds.Tables(0).Rows(0)("State").ToString()
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