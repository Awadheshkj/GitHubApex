Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class DesignationManager
    Inherits System.Web.UI.Page

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            DataInsertion()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                Dim mode As String
                divError.Visible = False
                Dim capex As New clsApex
                If Len(Request.QueryString("mode")) > 0 Then
                    If Not Request.QueryString("mode") = Nothing Then
                        If Request.QueryString("mode").ToString() <> "" Then
                            mode = Request.QueryString("mode").ToString()
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then
                                            hdnDesignation.Value = Request.QueryString("lid").ToString()
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillDesignationInEditMode()
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
    Private Sub FillDesignationInEditMode()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            Dim Active As String = ""
            sql = "select  Designation,IsActive from APEX_Designation where DesignationID=" & hdnDesignation.Value & " Order By Designation"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                txtDesignation.Text = ds.Tables(0).Rows(0)("Designation").ToString()
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

    Private Sub DataInsertion()
        Try
            Dim Active As Char
            Dim sql As String = ""
            If CheckDuplicateDesignation() = False Then


                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_Designation(Designation,IsActive) values('" & Clean(txtDesignation.Text.Trim()) & "','" & Active & "')"
                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Designation.aspx")
                End If
            Else
                lblError.Text = "Designation already exists"
                divError.Visible = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckDuplicateDesignation() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = ""
            Dim dupDesignation As String = ""
            Dim ds As New DataSet
            sql = "select * from APEX_Designation where Designation = '" & Clean(txtDesignation.Text) & "'"
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

    Private Sub DataUpdation()
        Try
            Dim Active As String = ""
            Dim sql As String = ""

            If ChkActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"
            End If

            sql = "update APEX_Designation  set Designation='" & Clean(txtDesignation.Text.Trim()) & "',IsActive='" & Active & "' where DesignationID=" & hdnDesignation.Value
            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Designation.aspx")
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
            txtDesignation.Text = ""
            ChkActive.Checked = False
            lblError.Text = ""
            divError.Visible = False
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Designation.aspx")
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