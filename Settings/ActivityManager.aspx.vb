Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class ActivityManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                divError.Visible = False
                Dim mode As String = ""
                Dim capex As New clsApex

                If Len(Request.QueryString("mode")) > 0 Then
                    If Not Request.QueryString("mode") = Nothing Then
                        If Request.QueryString("mode").ToString() <> "" Then
                            mode = Request.QueryString("mode").ToString()
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then

                                            hdnActivity.Value = Request.QueryString("lid").ToString()
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillActivityInEditMode()
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
                            Else
                                CallDivError()
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

    Private Sub FillActivityInEditMode()
        Try
            Dim sql As String
            Dim ds As New DataSet
            Dim Active As String = ""

            sql = "select  ProjectType,IsActive from APEX_ActivityType where ProjectTypeID=" & hdnActivity.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                txtActivity.Text = ds.Tables(0).Rows(0)("ProjectType").ToString()
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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            DataInsertion()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub DataInsertion()
        Try
            Dim Active As String = ""
            Dim sql As String
            If CheckDuplicateActivity() = False Then
                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_ActivityType(ProjectType,IsActive,Allowprofit) values('" & Clean(txtActivity.Text.Trim()) & "','" & Active & "','" & Clean(txtprofit.Text.Trim()) & "')"

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Activity.aspx")
                End If
            Else
                lblError.Text = "Activity already exists"
                divError.Visible = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub DataUpdation()
        Try
            Dim Active As String
            Dim sql As String
            If ChkActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"
            End If

            sql = "update  APEX_ActivityType set ProjectType='" & Clean(txtActivity.Text.Trim()) & "',IsActive='" & Active & "',Allowprofit='" & Clean(txtprofit.Text.Trim()) & "' where ProjectTypeID=" & hdnActivity.Value

            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Activity.aspx")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckDuplicateActivity() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String
            Dim ds As New DataSet
            sql = "select * from APEX_ActivityType where ProjectType = '" & Clean(txtActivity.Text) & "'"
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

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            DataUpdation()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Activity.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtActivity.Text = ""
            ChkActive.Checked = False
            lblError.Text = ""
            divError.Visible = False
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