Imports clsMain
Imports clsApex
Imports clsDatabaseHelper
Imports System.Data

Partial Class ProjectBrief
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.Write(FillJobDetails(Request.Form("JID")))
        Response.End()
    End Sub

    Private Function FillJobDetails(ByVal JID As String) As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex

        Dim sql As String = ""

        sql &= "Select jobcardID as ProjectID, BriefName,"
        sql &= " (SELECT [ProjectType] FROM [APEX_ActivityType] Where ProjectTypeID=B.PrimaryActivityID) As PrimaryActivity"
        sql &= " , ActivityDate, ScopeOfWork, MeasurementMatrix, TargetAudience"
        sql &= " , ActivityDetails, KeyChallangesForExecution"
        sql &= " from APEX_Brief B"
        sql &= " left outer join APEX_Jobcard As jc On jc.RefBriefID = B.BriefID"
        sql &= " where jobcardID= " & JID
        sql &= ""


        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        Dim dt As New DataTable
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt).Replace(Chr(13), "").Replace(Chr(10), "")

            Return jsonstring

        End If

        Return jsonstring
    End Function


End Class
