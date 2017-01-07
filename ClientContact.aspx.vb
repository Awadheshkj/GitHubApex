Imports clsApex
Imports clsDatabaseHelper
Imports System.Data

Partial Class Masters_ClientContact
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                Dim capex As New clsApex

                If Len(Request.QueryString("cid")) > 0 Then
                    Dim Clientid = Request.QueryString("cid")
                    If IsNumeric(Clientid) Then
                        FillClientContacts(Clientid)
                    Else
                        CallDivError()
                    End If
                Else
                    FillClientContacts()
                    divError.Visible = False
                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub FillClientContacts(Optional ByVal cid As Integer = 0)
        Try
            Dim sql As String = ""
            Dim ds As New DataSet

            sql = "select ROW_NUMBER() over(order by acc.ContactID desc) as row ,acc.ContactID,ac.Client,"
            sql &= "acc.ContactName,acc.ContactDesignation,acc.ContactDepartment,acc.ContactOfficialEmailID,acc.Mobile1 +', ' +   acc.Mobile2 as Mobile, "
            sql &= " acc.IsActive from APEX_ClientContacts as acc inner join APEX_Clients as ac "
            sql &= " on ac.ClientID=acc.RefClientID "

            If cid > 0 Then
                sql &= " where acc.RefClientID =" & cid
            End If

            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                gvClientContact.DataSource = ds
                gvClientContact.DataBind()

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvClientContact_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvClientContact.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hdnClientID As HiddenField = CType(e.Row.FindControl("hdnContactID"), HiddenField)

                e.Row.Attributes("onclick") = "javascript:call(" & hdnClientID.Value & ");"

                e.Row.Attributes.Add("onmouseover", "javascript:$(this).addClass('hover');")
                e.Row.Attributes.Add("onmouseout", "javascript:$(this).removeClass('hover');")

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Protected Sub gvClientContact_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvClientContact.PageIndexChanging
        Try
            gvClientContact.PageIndex = e.NewPageIndex
            FillClientContacts()
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
    Private Sub CallDivError()
        Try

            divError.Visible = True
            Dim capex As New clsApex

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub
End Class