Imports clsApex
Imports clsDatabaseHelper
Imports clsMain
Imports System.Data

Partial Class AjaxCalls_AX_Client
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = ""
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                Dim cid As String = Request.QueryString("id").ToString()
                If callid = "1" Then
                    result = FillClientContact(cid)
                End If
                If callid = "2" Then
                    result = GetContactPersonDetails(cid)
                End If
                If callid = "3" Then
                    'GetContactPerson
                    result = GetContactPerson(cid)
                End If

            End If
            Response.Write(result)
            Response.End()
        End If
    End Sub

    Private Function FillClientContact(ByVal id As String) As String
        Dim jsonstring As String = ""
        Dim dt As New DataTable
        Dim sql As String = "Select ContactName,ContactID from APEX_ClientContacts where IsActive='Y' and IsDeleted='N' and RefClientID='" & id & "' Order By ContactName"
        'Dim sql As String = "Select ContactName,ContactID from APEX_ClientContacts where IsActive='Y' and IsDeleted='N' and RefClientID='" & id & "' Order By ContactID"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
            'Else
            '    jsonstring = "{Table : [{ContactName : Add new,ContactID :Add new}]}"
        End If
        Return jsonstring
    End Function
    Private Function GetContactPersonDetails(ByVal id As String) As String
        Dim jsonstring As String = ""
        Dim sql = "select ContactOfficialEmailID,Mobile1 from APEX_ClientContacts where ContactID=" & id
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function GetContactPerson(ByVal id As String) As String
        Dim jsonstring As String = ""
        Dim sql = "Select ContactName,VendorContactID From APEX_VendorContacts where RefVendorID=" & id & " Order By ContactName"
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

End Class

