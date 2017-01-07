Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports clsApex

Partial Class Masters_ClientManager
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
                            If mode = "edit" Then
                                If Len(Request.QueryString("lid")) > 0 Then
                                    If Not Request.QueryString("lid") = Nothing Then
                                        If Request.QueryString("lid") <> "" Then
                                            hdnClientId.Value = Request.QueryString("lid").ToString()
                                            btnEdit.Visible = True
                                            btnAdd.Visible = False
                                            FillClientInEditMode()
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

    Private Function CheckDuplicateClient() As Boolean
        Dim result As Boolean = True
        Try
            Dim sql As String = ""

            Dim dupClient As String = ""
            Dim ds As New DataSet
            sql = "select * from APEX_Clients where Client='" & txtClient.Text & "'"
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

    Private Sub DataInsertion()
        Try
            Dim sql As String = ""
            Dim Active As Char
            Dim ds As New DataSet

            If CheckDuplicateClient() = False Then
                If ChkActive.Checked = True Then
                    Active = "Y"
                Else
                    Active = "N"
                End If

                sql = "insert into APEX_Clients(Client,Industry,AnnualTurnover,IsActive,InsertedBy,Address)"
                sql &= "values('" & Clean(txtClient.Text.Trim()) & "','" & Clean(txtIndustry.Text.Trim()) & "', "
                sql &= "'" & Clean(txtAnnTurn.Text.Trim()) & "','" & Active & "','" & getLoggedUserID() & "'"
                If Clean(txtAddress.Text) <> "" Then
                    sql &= ",'" & Clean(txtAddress.Text) & "')"
                Else
                    sql &= ",NULL)"
                End If

                If ExecuteNonQuery(sql) > 0 Then
                    Response.Redirect("Clients.aspx")
                End If
            Else
                lblError.Text = "Client already exists"
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

            sql = "update APEX_Clients  set Client='" & Clean(txtClient.Text.Trim()) & "',Industry='" & Clean(txtIndustry.Text.Trim()) & "', "
            sql &= "AnnualTurnover='" & Clean(txtAnnTurn.Text.Trim()) & "',IsActive='" & Active & "',"
            sql &= "Address='" & Clean(txtAddress.Text) & "',"
            sql &= " ModifiedBy='" & getLoggedUserID() & "' where ClientID=" & hdnClientId.Value

            If ExecuteNonQuery(sql) > 0 Then
                Response.Redirect("Clients.aspx")
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

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            DataUpdation()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Clients.aspx")
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtAnnTurn.Text = ""
            txtClient.Text = ""
            txtIndustry.Text = ""
            ChkActive.Checked = False
            lblMsg.Text = ""
            lblError.Text = ""
            divError.Visible = False
            txtAddress.Text = ""
            FillClientInEditMode()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillClientInEditMode()
        Try
            Dim sql As String = ""

            Dim ds As New DataSet

            Dim Active As String = ""
            sql = "select Client,Industry,AnnualTurnover,IsActive,Address from APEX_Clients where ClientID=" & hdnClientId.Value
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then

                txtClient.Text = ds.Tables(0).Rows(0)("Client").ToString()
                txtIndustry.Text = ds.Tables(0).Rows(0)("Industry").ToString()
                txtAnnTurn.Text = ds.Tables(0).Rows(0)("AnnualTurnover").ToString()
                txtAddress.Text = ds.Tables(0).Rows(0)("Address").ToString()
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
            'divContent.Visible = False
            divError.Visible = True
            Dim capex As New clsApex
            lblError.Text = capex.SetErrorMessage()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class