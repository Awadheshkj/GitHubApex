Imports System
Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports System.Data.SqlClient

Public Class clsApex

    Enum ClientType
        Existing = 1
        Non_Existing = 2
    End Enum

    Enum LeadsStatus
        Hot = 1
        Cold = 3
        Warm = 2
        Closed = 4
    End Enum
    Enum ChecklistStatus

        ' Pending = 1
        Running = 2
        Completed = 3

    End Enum
    Enum NotificationType
        PLA = 1
        UGN = 2
        APM = 3
        PLBA = 4
        CLTA = 5
        CLSA = 6
        PPNLA = 7
        JC = 8
        JSTMT = 9
        reject = 10
        JCInserted = 11
        TeamLead = 12
        viewJCKM = 13
        viewJKPM = 14
        PPNLApproved = 15
        ClaimApproved = 16
        ClaimRejected = 17
        ClaimExceedReq = 18
        ExceedAmount = 19
        FMCreateInvoice = 20
        PMPostPnL = 21
        viewpostpnl = 22
        approveprepnlAmt = 23
        approveprepnlAmtsuccess = 24
        PMtaskrequest = 25
        finalestimateapproval = 26
        clientapprovalpendinfromPMToKAM = 27
        clientapprovalpendinfromKAMToFM = 28
        clientapprovalpendinfromFMToKAM = 29
        clientapprovalpendinfromFMToPM = 30
        clientrejectpendinfromFMToKAM = 31
        EmployeeAdvanceFromPMtoBH = 32
        EmployeeAdvanceFromBHtoFM = 33
        EmployeeAdvanceFromFMtoPM = 34
        VendorAdvanceFromPMtoBH = 35
        VendorAdvanceFromBHtoFM = 36
        VendorAdvanceFromFMtoPM = 37
        estimateapprovalfromkamtoPM_AfterJC = 38
        estimateapprovalfromBHtoKAM_AfterJC = 39
        estimateRejectfromBHtoKAM_AfterJC = 40

        PrepnlapprovalfromkamtoPM_AfterJC = 41
        PrepnlapprovalfromBHtoKAM_AfterJC = 42
        PrepnlRejectfromBHtoKAM_AfterJC = 43

        ClaimRequestfromPMtoKam = 44
        finalestimateapprovalReject = 45

    End Enum

    Public Shared ServiceTax As Double = 12.36

    Public Function InsertStageLevel(ByVal JobCardID As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = "Insert into APEX_MappingProjectStage (RefJobCardID,StageLevel) values(" & JobCardID & ",3)"
        If ExecuteNonQuery(sql) > 0 Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Public Function UpdateStageLevel(ByVal CurrentStageLevel As String, ByVal JobCardID As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = "update APEX_MappingProjectStage set StageLevel = " & CurrentStageLevel & " where RefJobCardID=" & JobCardID
        If ExecuteNonQuery(sql) > 0 Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Public Function GetJobCardID(ByVal BriefID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select JobCardID from APEX_JobCard where RefBriefID=" & BriefID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetJobCardAmount(ByVal BriefID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select JCApprovalAmount from APEX_JobCard where RefBriefID=" & BriefID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function CheckStageLevel(ByVal CurrentStageLevel As String, ByVal JobCardID As String) As Boolean
        Dim result As Boolean

        'Dim sql As String = "Select StageLevel from APEX_MappingProjectStage where RefJobCardID = " & JobCardID
        'Dim ds As New DataSet
        'ds = ExecuteDataSet(sql)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    If Convert.ToInt32(ds.Tables(0).Rows(0)(0).ToString()) >= Convert.ToInt32(CurrentStageLevel) Then
        '        result = True
        '    Else
        '        result = False
        '    End If
        'Else
        '    result = False

        'End If
        result = True
        Return result
    End Function

    Public Function checkTaskCompleted(ByVal taskid As String) As String
        Dim result As String = ""
        Dim sql As String = "Select TaskCompleted from APEX_Task where TaskID=" & taskid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                result = "Y"
            Else
                result = "N"
            End If
        End If
        Return result
    End Function

    Public Function checkSubTaskCompleted(ByVal subtaskid As String) As String
        Dim result As String = ""
        Dim sql As String = "Select SubTaskCompleted from APEX_SubTask where SubTaskID=" & subtaskid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                result = "Y"
            Else
                result = "N"
            End If
        End If
        Return result
    End Function
    Public Function getjobStatus(ByVal jid As String) As String
        Dim status As String = ""
        Dim sql As String = ""
        sql = "Select JObCompleted=case when JobCompleted Is Null then 'N' else 'Y' end  "
        sql &= " from dbo.APEX_JobCard where JobCardID=" & jid & " and IsActive='Y'"

        Dim ds As DataSet = Nothing

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            status = ds.Tables(0).Rows(0)("JobCompleted").ToString()
        End If


        Return status
    End Function
    Public Function GetTaskIDBySubTaskID(ByVal SubTaskID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select RefTaskID from APEX_SubTask where SubTaskID=" & SubTaskID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetJobCardByTaskID(ByVal TaskID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select RefJobCardID from APEX_Task where TaskID=" & TaskID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetJobCardBySubTaskID(ByVal SubTaskID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select RefJobCardID from APEX_SubTask where SubTaskID=" & SubTaskID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetJobCardNoByJobCardID(ByVal JobCardID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select JobCardNo from APEX_JobCard where JobCardID=" & JobCardID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetStageLevel(ByVal CurrentStageLevel As String, ByVal JobCardID As String) As String
        Dim result As String = "0"

        Dim sql As String = "Select StageLevel from APEX_MappingProjectStage where RefJobCardID = " & JobCardID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        Else
            result = "0"
        End If

        Return result
    End Function

    Public Function GetBriefIDByJobCardID(ByVal JobCardID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select RefBriefID from APEX_JobCard where JobCardID=" & JobCardID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetLeadIDByJobCardID(ByVal JobCardID As String) As String
        Dim result As String = ""
        Dim sql As String = "select RefLeadID from APEX_Brief where BriefID = (select RefBriefID from APEX_JobCard where JobCardID=" & JobCardID & ")"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    'Public Function GetList(ByVal cl As String, ByVal id As String) As String
    '    Dim result As String = ""
    '    Dim clevel As String
    '    Dim capex As New clsApex
    '    Dim breifid, leadid As String
    '    clevel = capex.GetStageLevel(cl, id)
    '    breifid = capex.GetBriefIDByJobCardID(id)
    '    leadid = capex.GetLeadIDByJobCardID(id)
    '    result = "<ul>"
    '    If leadid <> "" Then
    '        result &= "<li>Lead: <a href='LeadsManager.aspx?mode=edit&lid=" & leadid & "'>View</a></li>"
    '    End If
    '    If breifid <> "" Then
    '        result &= "<li>Brief: <a href='BriefManager.aspx?mode=edit&lid=" & breifid & "'>View</a></li>"
    '        For i As Integer = 3 To clevel
    '            If i = 3 Then
    '                If clevel > i Then
    '                    result &= "<li>Estimate: <a href='Estimate.aspx?bid=" & breifid & "'>View</a></li>"
    '                Else
    '                    result &= "<li>Estimate: <a href='Estimate.aspx?bid=" & breifid & "'>Generate</a></li>"
    '                End If
    '            End If

    '            If i = 4 Then
    '                If clevel > i Then
    '                    result &= "<li>Pre P&L: <a href='PrePnlManager.aspx?bid=" & breifid & "'>View</a></li>"
    '                Else
    '                    result &= "<li>Pre P&L: <a href='PrePnlManager.aspx?bid=" & breifid & "'>Generate</a></li>"
    '                End If
    '            End If

    '            If i = 5 Then
    '                If clevel > i Then
    '                    result &= "<li>Job Card: <a href='JobCardManager.aspx?jid=" & id & "'>View</a></li>"
    '                Else
    '                    result &= "<li>Job Card: <a href='JobCardManager.aspx?jid=" & id & "'>Generate</a></li>"
    '                End If
    '            End If
    '            If i >= 6 Then
    '                result &= "<li>Task: <a href='CreateTask.aspx?jid=" & id & "'>View</a></li>"
    '            End If
    '        Next
    '    End If

    '    result &= "</ul>"
    '    Return result
    'End Function

    Public Function GetList(ByVal cl As String, ByVal id As String) As String
        Dim result As String = ""
        Dim clevel As String
        Dim capex As New clsApex
        Dim breifid, leadid As String

        Dim sqlBoxString As String = ""
        Dim dsBox As New DataSet
        Dim title As String = ""
        Dim kam As String = ""
        Dim pm As String = ""
        Dim primaryActivity As String = ""
        Dim Jobcode As String = ""
        sqlBoxString &= " select A.JobCardName,A.ProjectManagerID,A.KAMID,(B.FirstName + ' ' + isnull(B.LastName,'')) as PM, isnull(jobcardno,'N/A') as jobcode,   "
        sqlBoxString &= " (C.FirstName + ' ' + isnull(C.LastName,'')) as KamName ,activity.Projecttype from APEX_JobCard as A "
        sqlBoxString &= " inner join APEX_UsersDetails as B on A.ProjectManagerID=B.UserDetailsID "
        sqlBoxString &= " inner join APEX_UsersDetails as C on A.KAMID=C.UserDetailsID "
        sqlBoxString &= " inner join APEX_ActivityType as activity on A.primaryActivityID=activity.ProjectTypeID"

        sqlBoxString &= "  where A.JobCardID=" & id & " "
        dsBox = ExecuteDataSet(sqlBoxString)

        If dsBox.Tables(0).Rows.Count > 0 Then
            title = dsBox.Tables(0).Rows(0)("JobCardName").ToString
            kam = dsBox.Tables(0).Rows(0)("KamName").ToString
            pm = dsBox.Tables(0).Rows(0)("PM").ToString
            Jobcode = dsBox.Tables(0).Rows(0)("jobcode").ToString
            primaryActivity = dsBox.Tables(0).Rows(0)("Projecttype").ToString
        End If

        clevel = capex.GetStageLevel(cl, id)
        breifid = capex.GetBriefIDByJobCardID(id)
        leadid = capex.GetLeadIDByJobCardID(id)
        result &= "<div style='width:90%; margin:20px auto; border:1px solid black; padding:10px'><table>"
        result &= "<tr><td>Job Code :</td><td>" & Jobcode & "</td></tr>"
        result &= "<tr><td>Activity :</td><td>" & primaryActivity & "</td></tr>"
        result &= "<tr><td>Title :</td><td>" & title & "</td></tr>"
        result &= "<tr><td>KAM :</td><td>" & kam & "</td></tr>"
        result &= "<tr><td>PM :</td><td>" & pm & "</td></tr>"
        result &= "</table></div>"
        result &= "<ul style='list-style:none; margin-left:-20px'>"
        If leadid <> "" Then
            result &= "<li><a href='LeadsManager.aspx?mode=edit&lid=" & leadid & "'>View Lead</a></li>"
        End If
        If breifid <> "" Then
            result &= "<div class='divBrief'><li> <a href='BriefManager.aspx?mode=edit&bid=" & breifid & "'>View Brief</a></li></div>"
            For i As Integer = 3 To clevel
                If i = 3 Then
                    If clevel > i Then
                        result &= "<div class='divPnL'><li><a href='PrePnlManager.aspx?bid=" & breifid & "'>View Pre P&L</a></div></li>"
                    Else
                        result &= "<div class='divPnL'><li> <a href='PrePnlManager.aspx?bid=" & breifid & "'>Generate Pre P&L</a></li></div>"
                    End If
                End If

                If i = 4 Then
                    If clevel > i Then
                        result &= "<div class='divestimate'><li> <a href='Estimate.aspx?bid=" & breifid & "'>View Estimate</a></li></div>"
                    Else
                        result &= "<div class='divestimate'><li> <a href='Estimate.aspx?bid=" & breifid & "'>Generate Estimate</a></li></div>"
                    End If


                End If

                If i = 5 Then
                    If clevel > i Then
                        result &= "<div class='divjobcard'><li> <a href='JobCardManager.aspx?jid=" & id & "'>View Job Card</a></li></div>"
                    Else
                        result &= "<div class='divjobcard'><li><a href='JobCardManager.aspx?jid=" & id & "'>Generate Job Card</a></li></div>"
                    End If
                End If
                If i = 6 Then
                    result &= "<div class='divtask'><li><a href='CreateTask.aspx?jid=" & id & "'>View Task</a></li></div>"
                End If
                'If i = 8 Then
                '    result &= "<div class='divtask'><li><a href='CreateTask.aspx?jid=" & id & "'>View Task</a></li></div>"
                'End If
            Next
        End If

        result &= "</ul>"
        result = ""
        Return result
    End Function


    Public Function GetJobCardIDByLeadID(ByVal LeadID As String) As String
        Dim result As String = ""
        Dim sql As String = "select JobCardID from APEX_JobCard where RefBriefID = (Select BriefID from Apex_Brief where RefLeadID=" & LeadID & ")"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetJobCardIDByBriefID(ByVal BriefID As String) As String
        Dim result As String = ""
        Dim sql As String = "select JobCardID from APEX_JobCard where RefBriefID = " & BriefID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetBriefIDByLeadID(ByVal LeadID As String) As String
        Dim result As String = ""
        Dim sql As String = "select BriefID from APEX_Brief where RefLeadID = " & LeadID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetLeadIDByBriefID(ByVal BriefID As String) As String
        Dim result As String = ""
        Dim sql As String = "select RefLeadID from APEX_Brief where BriefID = " & BriefID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetJobCardNoByBriefID(ByVal BriefID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select JobCardNo from APEX_JobCard where RefBriefID=" & BriefID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetActivityList() As String
        Dim result As String = ""

        result &= "<ul>"
        result &= "<li><a href='Activity.aspx'>Activity</a></li>"
        result &= "<li><a href='Category.aspx'>Category</a></li>"
        result &= "<li><a href='Subcategory.aspx'>Subcategory</a></li>"
        result &= "<li><a href='State.aspx'>State</a></li>"
        result &= "<li><a href='City.aspx'>City</a></li>"
        result &= "<li><a href='Designation.aspx'>Designation</a></li>"
        result &= "<li><a href='ClientContact.aspx'>Client Contact</a></li>"
        result &= "<li><a href='VendorCategory.aspx'>Vendor Category</a></li>"
        result &= "<li><a href='Vendor.aspx'>Vendor</a></li>"
        result &= "<li><a href='VendorContact.aspx'>Vendor Contact</a></li>"
        result &= "</ul>"

        Return result
    End Function

    Public Function SetErrorMessage() As String
        Dim result As String
        result = "You are not Authorise to see this Information"
        Return result
    End Function

    Public Function GetRoleName(ByVal username As String) As String
        Dim result As String = ""
        Dim sql As String = "Select Role from APEX_UsersDetails as ud inner join APEX_Login as ln on ud.UserdetailsID = ln.RefUserDetailsID where DisplayName='" & username & "'"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Function GetPLNotificationsForKAM() As String
        Dim result As String = ""
        Dim sql As String = "Select * from APEX_Notifications as nt left outer join APEX_Recieptents as rp on nt.NotificationID = rp.RefNotificationID  where Type=" & NotificationType.PLA & " and IsExecuted <> 'Y'"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = "<div>Notification Title:" & ds.Tables(0).Rows(0)("NotificationTitle").ToString() & "</div><div>Notification Message:" & ds.Tables(0).Rows(0)("NotificationMessage").ToString() & "</div>"
        End If
        Return result
    End Function

    Public Function GetPLNotificationsForBranchHead(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql As String = "Select * from APEX_Notifications as nt left outer join APEX_Recieptents as rp on nt.NotificationID = rp.RefNotificationID  where Type=" & NotificationType.PLBA & " and IsExecuted <> 'Y'"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = "<div>Notification Title:" & ds.Tables(0).Rows(0)("NotificationTitle").ToString() & "</div><div>Notification Message:" & ds.Tables(0).Rows(0)("NotificationMessage").ToString() & "</div>"
        End If
        Return result
    End Function

    Public Function GetSubTasksID(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql As String = "select distinct SubTaskID from APEX_SubTask  as st"
        sql &= " inner join APEX_SubTaskTeam as sm on st.SubTaskID = sm.RefSubTaskID"
        sql &= " where sm.RefLeadID = " & userid & " Or sm.RefMemberID = " & userid

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                result &= ds.Tables(0).Rows(i)(0).ToString() & ","
            Next

            If result.Length > 0 Then
                result = result.Substring(0, result.Length - 1)
            End If
        End If
        Return result
    End Function

    Public Function GetTasksID(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql1 As String = ""
        Dim res As String = ""
        Dim sql As String = "select distinct TaskID from APEX_Task  as st"
        sql &= " inner join APEX_TaskTeam as sm on st.TaskID = sm.RefTaskID"
        sql &= " where sm.RefLeadID = " & userid & " Or sm.RefMemberID = " & userid
        sql &= " OR st.TaskID in ("
        Dim stask As String = GetSubTasksID(userid)

        If stask <> "" Then
            sql1 = " Select RefTaskID from APEX_SubTask Where SubTaskID in (" & stask & ")"
            Dim ds1 As DataSet
            ds1 = ExecuteDataSet(sql1)
            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                res &= ds1.Tables(0).Rows(i)(0).ToString() & ","
            Next
            If res.Length > 0 Then
                res = res.Substring(0, res.Length - 1)
            End If
        Else
            sql1 = "0"
            res = "0"
        End If


        sql &= res & ")"

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            result &= ds.Tables(0).Rows(i)(0).ToString() & ","
        Next
        If result.Length > 0 Then
            result = result.Substring(0, result.Length - 1)
        End If

        Return result
    End Function

    Public Function GetTasksIDnew(ByVal jid As String) As String
        Dim result As String = ""
        Dim sql1 As String = ""
        Dim res As String = ""
        Dim sql As String = "Select taskid from APEX_Task where refjobcardid=" & jid
        Dim ds1 As DataSet
        ds1 = ExecuteDataSet(sql)
        If ds1.Tables(0).Rows.Count > 0 Then

            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                res &= ds1.Tables(0).Rows(i)(0).ToString() & ","
            Next
            If res.Length > 0 Then
                res = res.Substring(0, res.Length - 1)
            End If
        Else
            res = "0"
        End If
        Return res
    End Function
    Public Function GetSubTasksIDNew(ByVal jid As String) As String
        Dim result As String = ""
        Dim sql As String = "select distinct SubTaskID from APEX_SubTask  as st"
        sql &= " inner join APEX_SubTaskTeam as sm on st.SubTaskID = sm.RefSubTaskID"
        sql &= " where  sm.RefMemberID = " & getLoggedUserID() & " and  st.refjobcardid=" & jid & " "

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                result &= ds.Tables(0).Rows(i)(0).ToString() & ","
            Next

            If result.Length > 0 Then
                result = result.Substring(0, result.Length - 1)
            End If
        Else
            result = "0"
        End If
        Return result
    End Function




    Public Function GetJobCardsID(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql1 As String = ""
        Dim res As String = ""

        Dim sql As String = "Select JobCardID from APEX_JobCard as jc"
        sql &= " where KAMID = " & userid & " Or ProjectManagerID = " & userid & " Or CampaignManager = " & userid
        sql &= " or JobCardID in("
        Dim tsid As String = GetTasksID(userid)
        If tsid <> "" Then
            sql1 = " Select distinct RefJobCardID from APEX_Task where TaskID in(" & tsid & ")"
            Dim ds1 As DataSet
            ds1 = ExecuteDataSet(sql1)
            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                res &= ds1.Tables(0).Rows(i)(0).ToString() & ","
            Next

            If res.Length > 0 Then
                res = res.Substring(0, res.Length - 1)
            End If

        Else
            sql1 = "0"
            res = "0"
        End If

        sql &= res & ")"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            result &= ds.Tables(0).Rows(i)(0).ToString() & ","
        Next
        If result.Length > 0 Then
            result = result.Substring(0, result.Length - 1)
        End If

        Return result
    End Function


    Public Function GetJobCardsIDnew(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql1 As String = ""
        Dim res As String = ""
        Dim capex As New clsApex

        Dim sql As String = "Select JobCardID from APEX_JobCard as jc"
        sql &= " join APEX_Brief as b on jc.refbriefID=b.BriefID"
        sql &= " where b.Insertedby = " & userid & " or b.insertedby in(" & capex.getuserdetailsID(getLoggedUserID()) & ") Or jc.ProjectManagerID = " & userid & " Or CampaignManager = " & userid
        sql &= " or JobCardID in("
        Dim tsid As String = GetTasksID(userid)
        If tsid <> "" Then
            sql1 = " Select distinct RefJobCardID from APEX_Task where TaskID in(" & tsid & ")"
            Dim ds1 As DataSet
            ds1 = ExecuteDataSet(sql1)
            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                res &= ds1.Tables(0).Rows(i)(0).ToString() & ","
            Next

            If res.Length > 0 Then
                res = res.Substring(0, res.Length - 1)
            End If

        Else
            sql1 = "0"
            res = "0"
        End If

        sql &= res & ")"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            result &= ds.Tables(0).Rows(i)(0).ToString() & ","
        Next
        If result.Length > 0 Then
            result = result.Substring(0, result.Length - 1)
        End If

        Return result
    End Function


    Public Function GetRoleNameByUserID(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql As String = "Select Role from APEX_UsersDetails where UserDetailsID=" & userid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function
    Public Function GetBHRoleNameByUserID(ByVal userid As String) As String
        Dim result As String = ""
        Dim sql As String = "Select HeadOf from APEX_UsersDetails where UserDetailsID=" & userid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Public Sub UpdateRecieptentSeen(ByVal notificationid As String, ByVal userid As String)
        Dim sql As String = "Update APEX_Recieptents set IsSeen='Y' where RefNotificationID=" & notificationid & " and UserID=" & userid
        ExecuteNonQuery(sql)
    End Sub

    Public Sub UpdateRecieptentExecuted(ByVal notificationid As String, ByVal userid As String)
        Dim i As Integer = 0
        Dim sql As String = "Update APEX_Recieptents set IsExecuted='Y' where RefNotificationID=" & notificationid & ""
        'and UserID=" & userid
        If ExecuteNonQuery(sql) > 0 Then
            i = i + 1
        Else
            i = i + 2
        End If
    End Sub

    Public Sub UpdateFMRecieptentExecuted(ByVal notificationid As String)
        Dim i As Integer = 0
        Dim sql As String = "Update APEX_Recieptents set IsExecuted='Y' where RefNotificationID=" & notificationid & " "
        If ExecuteNonQuery(sql) > 0 Then
            i = i + 1
        Else
            i = i + 2
        End If
    End Sub

    Public Function GetNotifications(ByVal userid As String, ByVal type As String) As String
        Dim result As String = ""
        Dim sql As String = "Select * from APEX_Notifications as nt left outer join APEX_Recieptents as rp on nt.NotificationID = rp.RefNotificationID  where Type=" & type & " and IsExecuted <> 'Y'"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = "<div>Notification Title:" & ds.Tables(0).Rows(0)("NotificationTitle").ToString() & "</div><div>Notification Message:" & ds.Tables(0).Rows(0)("NotificationMessage").ToString() & "</div>"
        End If
        Return result
    End Function

    Public Sub AddTaskBalance(ByVal TaskID As String, ByVal Amount As String)
        Dim sql As String = "update APEX_Task set Balance = Balance + " & Convert.ToDouble(Amount) & " where TaskID=" & TaskID
        ExecuteNonQuery(sql)
    End Sub

    Public Sub DeductTaskBalance(ByVal TaskID As String, ByVal Amount As String)
        Dim sql As String = "Update APEX_Task set Balance = Balance - " & Convert.ToDouble(Amount) & " where TaskID=" & TaskID
        ExecuteNonQuery(sql)
    End Sub

    Public Sub AddTaskBalanceNew(ByVal TaskAccountID As String, ByVal Amount As String)
        Dim sql As String = "update APEX_TaskAccount set Balance = Balance + " & Convert.ToDouble(Amount) & " where AccountID=" & TaskAccountID
        ExecuteNonQuery(sql)
    End Sub
    Public Sub DeductTaskBalanceNew(ByVal TaskAccountID As String, ByVal Amount As String)
        Dim sql As String = "Update APEX_TaskAccount set Balance = Balance - " & Convert.ToDouble(Amount) & " where AccountID=" & TaskAccountID
        ExecuteNonQuery(sql)
    End Sub

    Public Sub AddSubTaskBalance(ByVal SubTaskID As String, ByVal Amount As String)
        Dim sql As String = "update APEX_SubTask set Balance = Balance + " & Convert.ToDouble(Amount) & " where SubTaskID=" & SubTaskID
        ExecuteNonQuery(sql)
    End Sub

    Public Sub DeductSubTaskBalance(ByVal SubTaskID As String, ByVal Amount As String)
        Dim sql As String = "Update APEX_SubTask set Balance = Balance - " & Convert.ToDouble(Amount) & " where SubTaskID=" & SubTaskID
        ExecuteNonQuery(sql)
    End Sub
    Private Function getPrePnLIDbyBriefID(ByVal bid As String) As String
        Dim result As String = "Select PrePnLID  from dbo.APEX_PrePnL where RefBriefID=" & bid & "  and IsActive='Y'"
        Dim ds As DataSet = Nothing
        Dim prepnlid As String = ""
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("PrePnLID")) Then
                prepnlid = ds.Tables(0).Rows(0)("PrePnLID").ToString()
            End If
        End If

        Return result

    End Function
    Public Function FillContactInfo(ByVal contactid As String) As String()
        Dim result() As String = {"", ""}
        Dim sql = "select ContactOfficialEmailID,Mobile1 from APEX_ClientContacts where ContactID=" & contactid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result(0) = ds.Tables(0).Rows(0)(0).ToString()
            result(1) = ds.Tables(0).Rows(0)(1).ToString()
        End If
        Return result
    End Function


    Public Function GetPLIDByBriefID(ByVal BriefID As String) As String
        Dim result As String = ""
        Dim sql As String = "Select RefPrePnLID from APEX_JobCard where RefBriefID=" & BriefID
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function
    Public Function GetKeyStoneContacts(ByVal uid As String) As String
        Dim fullname As String = ""
        If uid <> "" Then
            Dim sql As String = ""
            sql = "Select (isnull(firstname,'')+' '+isnull(lastname,'')) as Fullname  from dbo.APEX_Login where RefUserDetailsID=" & uid
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("Fullname")) Then
                    fullname = ds.Tables(0).Rows(0)("Fullname")
                End If
            End If
        End If

        Return fullname
    End Function
    Public Shared Function CheckEstimated(ByVal bid As String) As Boolean
        Dim result As Boolean = False
        If bid <> "" Then
            Dim sql As String = "Select IsEstimated from APEX_JobCard where RefBriefID=" & bid
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                    result = True
                Else
                    result = False
                End If
            End If

        End If
        Return result
    End Function
    Public Shared Function CheckIsPrepnlgenerate(ByVal bid As String) As Boolean
        Dim result As Boolean = False
        If bid <> "" Then
            Dim sql As String = "Select IsPrepnl from APEX_JobCard where RefBriefID=" & bid
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)(0).ToString() = "Y" Then
                    result = True
                Else
                    result = False
                End If
            End If

        End If
        Return result
    End Function
    Public Function IsjobCardcreated(ByVal bid As String) As Boolean
        Dim flag As Boolean = False
        Dim sql As String = ""
        If bid <> "" Then
            sql = "Select JobcardNo from dbo.APEX_JobCard where REfBriefID=" & bid & " and IsActive='Y'"
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("JobcardNo")) Then
                    flag = True
                End If
            End If
        End If


        Return flag

    End Function


    Public Shared Function IsjobCardCompleted(ByVal bid As String) As Boolean
        Dim flag As Boolean = False
        Dim sql As String = ""
        If bid <> "" Then
            'sql = "Select jobcompleted from dbo.APEX_JobCard where REfBriefID=" & bid & " and IsActive='Y'"
            sql = "Select Ispostpnl from dbo.APEX_JobCard where REfBriefID=" & bid & " and IsActive='Y'"
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("Ispostpnl")) Then
                    If ds.Tables(0).Rows(0)("Ispostpnl").ToString() = "Y" Then
                        flag = True
                    End If
                End If
            End If
        End If
        Return flag
    End Function

    Public Shared Function Get_CheckListAvailable(ByVal tid As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_CheckList where RefTaskID=" & tid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Public Shared Function Status_CheckList(ByVal tid As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_CheckList where (Completed<>3 or Completed is null) and RefTaskID=" & tid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = False
        Else
            result = True
        End If
        Return result
    End Function
    Public Shared Function IsTL(ByVal taid As String) As Integer
        Dim TLID As Integer = 0
        Dim sql As String = "Select TL from APEX_taskAccount where AccountID=" & taid
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("TL")) Then
                TLID = ds.Tables(0).Rows(0)("TL")
            End If

        End If
        Return TLID
    End Function

    Public Shared Sub SendExMail(ByVal exMsg As String, ByVal ex As String)
        Dim toid As String() = {"chander@kestone.in"}
        'Dim ccid As String = "ravi@technocats.in"
        For i As Integer = 0 To 2
            If i = 0 Then
                'sendMail(exMsg.ToString(), ex.ToString(), "", toid(i), ccid)
            Else
                'sendMail(exMsg.ToString(), ex.ToString(), "", toid(i), "")
            End If
        Next
    End Sub

    Public Shared Function getTaskIDbyAccountID(ByVal AccuntID As Integer) As Integer
        Dim result As Integer = 0
        If AccuntID > 0 Then
            Dim sql As String = "Select RefjobcardID from APEX_TaskAccount where AccountID=" & AccuntID
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("RefjobcardID")) Then
                    result = ds.Tables(0).Rows(0)("RefjobcardID")
                End If
            End If
        End If
        Return result
    End Function

    Public Function GetFinanceManagerOfJobCard(ByVal jobcode As String) As String
        Dim result As String = ""
        Dim sql As String = "Select FinanceManagerID from APEX_JobCard where JobCardID=" & jobcode
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("FinanceManagerID").ToString()
        End If
        Return result
    End Function

    Public Function GetProjectManagerOfJobCard(ByVal jobcode As String) As String
        Dim result As String = ""
        Dim sql As String = "Select ProjectManagerID from APEX_JobCard where JobCardID=" & jobcode
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("ProjectManagerID").ToString()
        End If
        Return result
    End Function
    Public Shared Function GetEmailIDByUserID(ByVal Refuid As Integer) As String
        Dim email As String = ""
        Dim sql As String = ""
        If Refuid > 0 Then
            sql = "Select EmailID from Apex_usersDetails where UserDetailsID=" & Refuid & " and IsActive='Y' and IsDeleted='N' "
            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("EmailID")) Then
                    email = ds.Tables(0).Rows(0)("EmailID").ToString()
                End If
            End If
        End If
        Return email
    End Function

    Public Shared Function GetJSONString(ByVal Dt As DataTable) As String

        Dim StrDc As String() = New String(Dt.Columns.Count - 1) {}
        Dim HeadStr As String = String.Empty

        For i As Integer = 0 To Dt.Columns.Count - 1

            StrDc(i) = Dt.Columns(i).Caption

            HeadStr += """" & StrDc(i) & """ : """ & StrDc(i) & i.ToString() & "¾" & ""","
        Next

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1)

        Dim Sb As New StringBuilder()
        Sb.Append("{""" & Convert.ToString(Dt.TableName) & """ : [")

        For i As Integer = 0 To Dt.Rows.Count - 1

            Dim TempStr As String = HeadStr
            Sb.Append("{")

            For j As Integer = 0 To Dt.Columns.Count - 1

                TempStr = TempStr.Replace(Dt.Columns(j).ToString() & j.ToString() & "¾", Dt.Rows(i)(j).ToString())
            Next

            Sb.Append(TempStr & "},")
        Next

        Sb = New StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1))
        Sb.Append("]}")

        Return Sb.ToString()
    End Function

    Public Shared Function getclaimedtotal(ByVal jc As Integer) As Integer
        Dim result As Integer = 0
        If jc > 0 Then
            Dim sql As String = ""
            sql += " Select  isnull(sum(cl.Amount),'0.00') as ClaimedAmt from APEX_ClaimMaster as cm "
            sql += " join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID "
            sql += " join APEX_Task as t on ta.refTaskID=t.taskid "
            sql += " join APEX_JobCard as jc on t.RefJobcardID=jc.JobCardID "
            sql += " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID "
            sql += " where SubmittedUserID='" & getLoggedUserID() & "' and JobCardID='" & jc & "'  and IsApproved ='Y'"
            sql += ""
            sql += ""

            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("ClaimedAmt")) Then
                    result = ds.Tables(0).Rows(0)("ClaimedAmt")
                End If
            End If
        End If
        Return result
    End Function
    Public Shared Function checkpostpnlstatus(ByVal jid As Integer) As Boolean
        Dim status As Boolean = False
        Dim Sql As String = ""
        Sql &= "select isnull(IsPostPnL,'N')IsPostPnL  from apex_jobcard where JobCardID=" & jid
        Sql &= ""
        Sql &= ""
        Sql &= ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0)(0)("IsPostPnL") = "Y" Then
                status = True
            Else
                status = False
            End If
        End If
        Return status
    End Function
    Public Shared Function checkJCstatus(ByVal jid As Integer) As Boolean
        Dim status As Boolean = False
        Dim Sql As String = ""
        Sql &= "select isnull(jobcompleted,'N')jobcompleted  from apex_jobcard where JobCardID=" & jid
        Sql &= ""
        Sql &= ""
        Sql &= ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0)(0)("jobcompleted") = "Y" Then
                status = True
            Else
                status = False
            End If
        End If
        Return status
    End Function

    Public Function GetJobCardByjcID(ByVal jid As String) As String
        Dim result As String = ""
        Dim sql As String = "Select JobCardno from apex_jobcard where JobCardID=" & jid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Function GetPostLIDByBriefID(bid As String) As String
        Dim result As String = "Select PostPnLID  from dbo.APEX_PostPnL where RefBriefID=" & bid & "  and IsActive='Y'"
        Dim ds As New DataSet
        Dim prepnlid As String = ""
        ds = ExecuteDataSet(result)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("PostPnLID")) Then
                prepnlid = ds.Tables(0).Rows(0)("PostPnLID").ToString()
            End If
        End If

        Return prepnlid
    End Function
    Function getuserdetailsID(id As Integer) As String
        Dim str As String = "0"
        Dim sql As String = " exec GetHierarki_IDsByUserID " & id & ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If i = 0 Then
                    str &= "" & ds.Tables(0).Rows(i)("UserdetailsID")
                Else
                    str &= "," & ds.Tables(0).Rows(i)("UserdetailsID")
                End If

            Next

        End If
        Return str

    End Function

    Public Shared Function InsertPrePnlVSEstimateHistory(RefjobcardID As Integer, Esimatetotal As Decimal, prepnltotal As Decimal, profitperc As Decimal, Optional ReasonFromKam As String = "") As Boolean
        Dim sql As String = ""
        'and isapproved=''
        sql &= "if not exists(select top 1 refjobcardID from Apex_EstimateVSprepnlHistory where refjobcardID=" & RefjobcardID & " and profitPerc=" & profitperc & " and isapproved='' order by insertedon desc)begin"
        sql &= " Insert Into Apex_EstimateVSprepnlHistory(RefjobcardID,EstimateTotal,PrepnlTotal,ProfitPerc,ReasonFromKam)Values(" & RefjobcardID & "," & Esimatetotal & "," & prepnltotal & "," & profitperc & ",'" & ReasonFromKam & "')"
        sql &= " End"

        If ExecuteNonQuery(sql) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class