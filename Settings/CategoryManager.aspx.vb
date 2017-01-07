Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class CategoryManager
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
                                            hdnCategory.Value = Request.QueryString("lid").ToString()
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillCategoryInEditMode()
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

    Private Sub FillCategoryInEditMode()
        Try
            Dim sql As String
            Dim ds As New DataSet
            sql = "select  CategoryType,IsActive,Allowprofit from APEX_Category where CategoryTypeID=" & hdnCategory.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                txtCategory.Text = ds.Tables(0).Rows(0)("CategoryType").ToString()
                'txtprofit.Text = ds.Tables(0).Rows(0)("Allowprofit").ToString()

                If ds.Tables(0).Rows(0)("IsActive").ToString() = "Y" Then
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
            Dim Active As String
            Dim sql As String

            If CheckDuplicateCategory() = False Then
                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                'sql = "insert into APEX_Category(CategoryType,IsActive,InsertedBy,Allowprofit) values('" & Clean(txtCategory.Text.Trim()) & "','" & Active & "','" & getLoggedUserID() & "','" & txtprofit.Text.Trim() & "')"
                sql = "insert into APEX_Category(CategoryType,IsActive,InsertedBy) values('" & Clean(txtCategory.Text.Trim()) & "','" & Active & "','" & getLoggedUserID() & "')"
                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Category.aspx")
                End If
            Else
                lblError.Text = "Category already exists"
                divError.Visible = True
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub


    Private Sub DataUpdation()
        Try
            Dim sql As String

            Dim Active As String
            If ChkActive.Checked = True Then
                Active = "Y"
            Else
                Active = "N"
            End If

            'sql = "update  APEX_Category set CategoryType='" & Clean(txtCategory.Text.Trim()) & "',IsActive='" & Active & "',ModifiedBy='" & getLoggedUserID() & "',Allowprofit='" & txtprofit.Text.Trim() & "' where CategoryTypeID=" & hdnCategory.Value
            sql = "update  APEX_Category set CategoryType='" & Clean(txtCategory.Text.Trim()) & "',IsActive='" & Active & "',ModifiedBy='" & getLoggedUserID() & "' where CategoryTypeID=" & hdnCategory.Value
            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Category.aspx")
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function CheckDuplicateCategory() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String
            Dim ds As New DataSet

            sql = "select * from APEX_Category where CategoryType = '" & Clean(txtCategory.Text) & "'"
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
            Response.Redirect("Category.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtCategory.Text = ""
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