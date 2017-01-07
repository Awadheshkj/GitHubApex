Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_SubcategoryManager
    Inherits System.Web.UI.Page

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
                            FillCategory()

                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid").ToString() <> "" Then
                                            hdnSubCategory.Value = Request.QueryString("lid").ToString()
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
            Response.Redirect("Subcategory.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            ddlCategory.SelectedIndex = 0
            txtSubcategory.Text = ""
            ChkActive.Checked = False
            lblMsg.Text = ""
            lblError.Text = ""
            divError.Visible = False
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

    Private Function CheckDuplicateSubcategory() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            sql = "select * from APEX_SubCategory where SubCategoryType='" & Clean(txtSubcategory.Text) & "'"
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

            If CheckDuplicateSubcategory() = False Then
                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_SubCategory(RefCategoryTypeID,SubCategoryType,IsActive,InsertedBy) values('" & Clean(ddlCategory.SelectedValue) & "','" & Clean(txtSubcategory.Text.Trim()) & "','" & Active & "'," & getLoggedUserID() & ")"
                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Subcategory.aspx")
                End If

            Else
                lblError.Text = "Sub Category already exists"
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

            sql = "update APEX_SubCategory  set RefCategoryTypeID='" & Clean(ddlCategory.SelectedValue) & "', SubCategoryType='" & Clean(txtSubcategory.Text.Trim()) & "',IsActive='" & Active & "',ModifiedBy='" & getLoggedUserID() & "' where SubCategoryTypeID='" & hdnSubCategory.Value & "'"
            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Subcategory.aspx")
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
    Private Sub FillCategory()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            sql &= "select CategoryTypeID,CategoryType from APEX_Category where isActive='Y' and IsDeleted='N'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlCategory.DataSource = ds.Tables(0)
                ddlCategory.DataTextField = "CategoryType"
                ddlCategory.DataValueField = "CategoryTypeID"
                ddlCategory.DataBind()

            End If
            ddlCategory.Items.Insert("0", New ListItem("Select Category", "0"))
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillCategoryInEditMode()
        Try
            Dim sql As String = ""
            Dim ds As New DataSet
            Dim Active As String = ""
            sql = "select  RefCategoryTypeID,SubCategoryType,IsActive from APEX_SubCategory where SubCategoryTypeID='" & hdnSubCategory.Value & "'"
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                ddlCategory.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("RefCategoryTypeID").ToString())
                txtSubcategory.Text = ds.Tables(0).Rows(0)("SubCategoryType").ToString()
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