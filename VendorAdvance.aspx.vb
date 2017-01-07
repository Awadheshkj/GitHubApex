Imports clsDatabaseHelper
Imports System.Data
Imports clsMain
Imports System.Data.SqlClient
Imports clsApex
Imports System.IO

Partial Class VendorAdvance
    Inherits System.Web.UI.Page
    Public message As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Page.IsPostBack Then
                'divError.Visible = False
                MessageDiv.Visible = False
                Dim capex As New clsApex

                If Len(Request.QueryString("nid") > 0) Then
                    If Request.QueryString("nid") <> "" Then
                        hdnNodificationID.Value = Request.QueryString("nid")
                        capex.UpdateRecieptentSeen(hdnNodificationID.Value, getLoggedUserID())

                    End If
                End If

                If Len(Request.QueryString("taid")) > 0 Then
                    If Request.QueryString("taid").ToString() <> Nothing Then
                        hdnTaskID.Value = Request.QueryString("taid").ToString()
                        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
                        fillDetails()
                        lblfulpl_Mail.Visible = False
                        lnkfileothers.Visible = False
                        pnlfinance.Visible = False

                        If role = "O" Then

                            btnReject.Visible = False
                            btnapproved.Visible = False
                            btnReset.Visible = True
                            btnSave.Visible = True
                            btnpaymentproccess.Visible = False


                            Dim sql As String = "select count(reftaskID)cnt from [dbo].[Apex_VendorAdvence] where reftaskID in(" & hdnTaskID.Value & ")"
                            Dim sql1 As String = "select finaceapproval from [dbo].[Apex_VendorAdvence] where reftaskID in(" & hdnTaskID.Value & ")"
                            If ExecuteSingleResult(sql, _DataType.Numeric) > 0 Then
                                If ExecuteSingleResult(sql1, _DataType.Alpha) = "N" Then
                                    MessageDiv.Visible = True
                                    divContent.Visible = False
                                    lblMsg.Text = "Advance approval Request has already been sent to top management. Please wait for the approval."
                                Else
                                    getadvancedetails()
                                    btnReset.Visible = False
                                    btnSave.Visible = False
                                    divContent.Visible = True
                                    MessageDiv.Visible = True
                                    lblMsg.Text = "Advance amount is approved."
                                    pnlfinance.Visible = True
                                End If
                            End If

                        ElseIf role = "H" Then
                            Dim sql As String = "select BranchHeadApproval from [dbo].[Apex_VendorAdvence] where reftaskID in(" & hdnTaskID.Value & ")"
                            Dim status As String = ""
                            status = ExecuteSingleResult(sql, _DataType.Alpha)
                            If status = "Y" Then
                                btnReject.Enabled = False
                                btnapproved.Enabled = False
                                btnapproved.Text = "Already Approved"
                                btnReject.Visible = False
                                btnapproved.Visible = True
                                pnlfinance.Visible = True
                            ElseIf status = "R" Then
                                btnReject.Enabled = False
                                btnapproved.Enabled = False
                                btnapproved.Text = "Already Rejected"
                                btnReject.Visible = False
                                btnapproved.Visible = True
                                pnlfinance.Visible = True
                            Else
                                btnReject.Visible = True
                                btnapproved.Visible = True
                            End If

                            btnReset.Visible = False
                            btnSave.Visible = False
                            btnpaymentproccess.Visible = False
                            getadvancedetails()

                        ElseIf role = "F" Then
                            pnlfinance.Visible = True
                            Dim sql As String = "select finaceapproval from [dbo].[Apex_VendorAdvence] where reftaskID in(" & hdnTaskID.Value & ")"
                            If ExecuteSingleResult(sql, _DataType.Alpha) = "Y" Then
                                btnpaymentproccess.Enabled = False
                                btnpaymentproccess.Text = "Processed"
                            Else
                                btnpaymentproccess.Visible = True
                            End If
                            btnReject.Visible = False
                            btnapproved.Visible = False
                            btnReset.Visible = False
                            btnSave.Visible = False

                            getadvancedetails()
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
    End Sub

    Private Sub getadvancedetails()
        Try

            Dim sql As String = ""
            sql &= " "
            sql &= " select EA.ID,EE.reftaskID as AccountID, ExpenseHead as title, AdvanceAmtReq as Amount, BudgetAmtAllocated as BAmount,Convert(varchar(20),EA.PaymentTobeDone,105)PaymentTobeDone,Convert(varchar(20),EA.ExpectedDateOfExpenses,105)ExpectedDateOfExpenses,AdvanceAmtRequest"
            sql &= " ,PANCardNumber"
            sql &= " ,ServiceTaxNumber"
            sql &= " ,AddressofVendor"
            sql &= " ,Vendorcompanyname"
            sql &= " ,Vendorconcernedperson"
            sql &= " ,ConcernedPhoneNumber"
            sql &= " ,Modeofpayment"
            sql &= " ,Beneficiaryname"
            sql &= " ,Taxes"
            sql &= " ,Total"
            sql &= ",PerformaInvoice,Otherdocs,PaidAmount,Remarks"
            sql &= "    from [dbo].[Apex_VendorAdvanceExpenses] EE"
            sql &= " Inner join Apex_VendorAdvence EA On EE.refEMPadvanceID=EA.ID"
            sql &= " where EE.reftaskID In(" & hdnTaskID.Value & ")"
            sql &= ""
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            gvempadvance.Enabled = False
            txtpaymenttobedone.Enabled = False
            txtexpecteddateofexpences.Enabled = False

            If ds.Tables(0).Rows.Count > 0 Then
                gvempadvance.DataSource = ds
                gvempadvance.DataBind()
                txtadvanceAmount.Text = ds.Tables(0).Rows(0)("AdvanceAmtRequest")
                txtpaymenttobedone.Text = ds.Tables(0).Rows(0)("PaymentTobeDone")
                txtexpecteddateofexpences.Text = ds.Tables(0).Rows(0)("ExpectedDateOfExpenses")
                hdnEmployeeadvanceID.Value = ds.Tables(0).Rows(0)("ID")
                txtPANCardNumber.Text = ds.Tables(0).Rows(0)("PANCardNumber")
                txtServiceTaxNumber.Text = ds.Tables(0).Rows(0)("ServiceTaxNumber")
                txtAddressofVendor.Text = ds.Tables(0).Rows(0)("AddressofVendor")
                txtVendorcompanyname.Text = ds.Tables(0).Rows(0)("Vendorcompanyname")
                txtVendorconcernedperson.Text = ds.Tables(0).Rows(0)("Vendorconcernedperson")
                txtConcernedPhoneNumber.Text = ds.Tables(0).Rows(0)("ConcernedPhoneNumber")
                txtModeofpayment.Text = ds.Tables(0).Rows(0)("Modeofpayment")
                txtBeneficiaryname.Text = ds.Tables(0).Rows(0)("Beneficiaryname")
                txtTaxes.Text = ds.Tables(0).Rows(0)("Taxes")
                txttotal.Text = ds.Tables(0).Rows(0)("Total")

                If ds.Tables(0).Rows(0)("PerformaInvoice").ToString() <> "" Then
                    hdnlblfulpl_Mail.Value = ds.Tables(0).Rows(0)("PerformaInvoice").ToString()
                    lblfulpl_Mail.Visible = True

                Else
                    lblfulpl_Mail.Visible = False

                End If

                If ds.Tables(0).Rows(0)("Otherdocs").ToString() <> "" Then
                    hdnfileothers.Value = ds.Tables(0).Rows(0)("Otherdocs").ToString()
                    lnkfileothers.Visible = True

                Else
                    lnkfileothers.Visible = False

                End If

                txtpaidamount.Text = ds.Tables(0).Rows(0)("PaidAmount").ToString()
                txtremarks.Text = ds.Tables(0).Rows(0)("Remarks").ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lblfulpl_Mail_Click(sender As Object, e As EventArgs) Handles lblfulpl_Mail.Click
        Response.ContentType = "application/vnd.ms-outlook"
        Dim str As String = hdnlblfulpl_Mail.Value
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(str) & "")
        Response.TransmitFile("~/" & str.ToString())
        'Response.TransmitFile("~/Collateral/Jobcard/65K Fund 2015422000.msg")
        Response.End()
    End Sub

    Protected Sub lnkfileothers_Click(sender As Object, e As EventArgs) Handles lnkfileothers.Click
        Response.ContentType = "application/vnd.ms-outlook"
        Dim str As String = hdnfileothers.Value
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(str) & "")
        Response.TransmitFile("~/" & str.ToString())
        'Response.TransmitFile("~/Collateral/Jobcard/65K Fund 2015422000.msg")
        Response.End()
    End Sub

    Private Sub fillDetails()

        'Dim sql As String = "Select t.title,ta.particulars,ta.Amount from APEX_TAskAccount As ta "
        'sql &= " join APEX_Task As t On ta.refTaskID=t.taskID where ta.AccountID=" & hdnTaskID.Value
        Dim sql As String = ""
        sql &= " Select ta.AccountID,City,category,ta.taskname title,ta.particulars,isnull(ta.total,0)Amount,isnull(ta.total,0)BAmount,(UD.FirstNAme + ' ' + UD.LastName)EMPName,EMPcode,JobCardName EventName,"
        sql &= " (select Insertedby  from apex_brief where briefID=JC.refbriefID)KamID,JC.ProjectManagerID,"
        sql &= " convert(varchar(20),ActivityDate,106)EventDate,CL.client,(select (FirstNAme + ' ' + LastName) from Apex_usersdetails where UserDetailsID = JC.ProjectManagerID) PMName from APEX_TAskAccount TA"
        sql &= " inner join Apex_usersdetails UD on TA.TL=UD.UserDetailsID "
        'sql &= " inner join APEX_Task as t on ta.refTaskID=t.taskID"
        sql &= " inner join apex_jobcard as JC on ta.refjobcardID=jc.jobcardID"
        ' sql &= " inner join apex_prepnl as PP on JC.RefBriefID=pp.refbriefID"
        sql &= " inner join APEX_Brief as B on jc.RefBriefID =B.BriefID "
        sql &= " inner join apex_clients as CL on B.RefClientID=CL.clientID "
        sql &= "  where ta.AccountID in(" & hdnTaskID.Value & ")"
        sql &= ""


        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        gvempadvance.DataSource = ds
        gvempadvance.DataBind()

        If ds.Tables(0).Rows.Count > 0 Then

            'If Not IsDBNull(ds.Tables(0).Rows(0)("title")) Then
            '    lblTaskTitle.Text = ds.Tables(0).Rows(0)("title")
            'End If
            'If Not IsDBNull(ds.Tables(0).Rows(0)("particulars")) Then
            '    lblparticulars.Text = ds.Tables(0).Rows(0)("particulars")
            'End If
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If Not IsDBNull(ds.Tables(0).Rows(i)("Amount")) Then
                    If i = 0 Then
                        txtbugetamountallocated.Text = CType(ds.Tables(0).Rows(i)("Amount"), Decimal)
                    Else
                        txtbugetamountallocated.Text = CType(txtbugetamountallocated.Text, Decimal) + CType(ds.Tables(0).Rows(i)("Amount"), Decimal)
                    End If

                End If
            Next
            txtadvanceAmount.Text = txtbugetamountallocated.Text
            'If Not IsDBNull(ds.Tables(0).Rows(0)("Amount")) Then
            '    txtbugetamountallocated.Text = ds.Tables(0).Rows(0)("Amount")
            'End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("PMName")) Then
                txtPmName.Text = ds.Tables(0).Rows(0)("PMName")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EMPName")) Then
                txtEmployeeName.Text = ds.Tables(0).Rows(0)("EMPName")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EMPcode")) Then
                txtempcode.Text = ds.Tables(0).Rows(0)("EMPcode")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EventName")) Then
                txtEventName.Text = ds.Tables(0).Rows(0)("EventName")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("EventDate")) Then
                txteventDate.Text = ds.Tables(0).Rows(0)("EventDate")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("client")) Then
                txtclientName.Text = ds.Tables(0).Rows(0)("client")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("category")) Then
                lblexpensehead.Text = ds.Tables(0).Rows(0)("category")
            End If
            txteventCity.Text = ds.Tables(0).Rows(0)("City")
            txtKamName.Text = getKAMName()
            hdnKamID.Value = ds.Tables(0).Rows(0)("KamID")
            hdnPMID.Value = ds.Tables(0).Rows(0)("ProjectManagerID")
        End If

        Dim jid As String = ""
        Dim capex As New clsApex
        Dim tid As String = getTaskIDbyAccountID(hdnTaskID.Value.Split(",")(0))
        hdnJobCardID.Value = tid
        Dim sql2 As String = "Select JobCardNo  from APEX_JobCard where JobCardID = " & tid
        Dim ds2 As New DataSet
        ds2 = ExecuteDataSet(sql2)
        If ds2.Tables(0).Rows.Count > 0 Then
            'lblJobCode.Text = ds2.Tables(0).Rows(0)(0).ToString()
            txtJCNo.Text = ds2.Tables(0).Rows(0)(0).ToString()
        End If
        'End If
    End Sub

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
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

    Public Shared Sub SendExMail(ByVal exMsg As String, ByVal ex As String)
        Dim toid As String() = {"chander@kestone.in"}
        'Dim ccid As String = "ravi@technocats.in"
        For i As Integer = 0 To 2
            If i = 0 Then
                sendMail(exMsg.ToString(), ex.ToString(), "", toid(i), "")
            Else
                'sendMail(exMsg.ToString(), ex.ToString(), "", toid(i), "")
            End If
        Next
    End Sub

    Private Function getKAMName() As String
        Dim bid As String = getBriefID()
        Dim result As String = ""
        Dim sql As String = "select isnull(FirstName,'') + isnull(LastName,'') as KAMName "
        sql &= " from APEX_UsersDetails as ud "
        sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
        sql &= " where ab.BriefID = " & bid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("KAMName").ToString()
        End If
        Return result
    End Function

    Private Function getBriefID() As String
        Dim bid As String = ""
        Dim jid As String = ""
        Dim tid As String = ""
        Dim capex As New clsApex

        If hdnTaskID.Value <> "" Then
            tid = getTaskIDbyAccountID(hdnTaskID.Value.Split(",")(0))
            jid = tid
            'jid = capex.GetJobCardByTaskID(tid)
            bid = capex.GetBriefIDByJobCardID(jid)
        End If

        Return bid
    End Function

    Private Function GetID() As String
        Dim IID As Integer = 0
        Try

            Dim sqla As New StringBuilder
            sqla.Append("")
            sqla.Append("")
            sqla.Append("" & getBriefID() & " ")


        Catch ex As Exception

        End Try

        Return IID

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim filename As String = filePI.FileName
        Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
        Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
        Dim encname As String = ""
        'txtUploads.Text = fname
        Dim path As String = ""
        encname = DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
        filePI.SaveAs(Server.MapPath("Uploads/VendorAdvance/PI/" & encname & "." & fext))
        path = "Uploads/VendorAdvance/PI/" & encname & "." & fext

        Dim path1 As String = ""
        If fileOther.HasFile Then
            Dim filename1 As String = fileOther.FileName
            Dim fname1 As String = filename1.Substring(0, filename1.LastIndexOf("."))
            Dim fext1 As String = filename1.Substring(filename1.LastIndexOf(".") + 1, filename1.Length - (filename1.LastIndexOf(".") + 1))
            Dim encname1 As String = ""
            'txtUploads.Text = fname

            encname1 = DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
            fileOther.SaveAs(Server.MapPath("Uploads/VendorAdvance/Otherdocs/" & encname1 & "." & fext1))
            path1 = "Uploads/VendorAdvance/Otherdocs/" & encname1 & "." & fext1
        Else
            path1 = ""
        End If


        Dim sql As String = ""
        sql &= "  INSERT INTO [dbo].[Apex_VendorAdvence]"
        sql &= "          ([RefjobcardID]"
        sql &= "          ,[RefTaskID]"
        sql &= "          ,[AdvanceAmtRequest]"
        sql &= "          ,[PaymentToBeDone]"
        sql &= "          ,[BudgetAmount]"
        sql &= "          ,[ExpectedDateOfExpenses]"
        sql &= "          ,[BranchHeadApproval]"
        sql &= " ,PANCardNumber"
        sql &= " ,ServiceTaxNumber"
        sql &= " ,AddressofVendor"
        sql &= " ,Vendorcompanyname"
        sql &= " ,Vendorconcernedperson"
        sql &= " ,ConcernedPhoneNumber"
        sql &= " ,Modeofpayment"
        sql &= " ,Beneficiaryname"
        sql &= " ,Taxes"
        sql &= " ,Total"
        sql &= "          ,[BranchHeadApprovalDate],PerformaInvoice,Otherdocs) Output Inserted.ID"
        sql &= "     VALUES"
        sql &= "          ('" & getTaskIDbyAccountID(hdnTaskID.Value.Split(",")(0)) & "'"
        sql &= "          ,'" & hdnTaskID.Value.Split(",")(0) & "'"
        sql &= "          ,'" & hdnadvanceamount.Value & "'"
        sql &= "          ,convert(datetime,'" & txtpaymenttobedone.Text & "',105)"
        sql &= "          ,'" & txtbugetamountallocated.Text & "'"
        sql &= "          ,convert(datetime,'" & txtexpecteddateofexpences.Text & "',105)"

        sql &= "          ,'P'"
        sql &= " ,'" & Clean(txtPANCardNumber.Text) & "'"
        sql &= " ,'" & Clean(txtServiceTaxNumber.Text) & "'"
        sql &= " ,'" & Clean(txtAddressofVendor.Text) & "'"
        sql &= " ,'" & Clean(txtVendorcompanyname.Text) & "'"
        sql &= " ,'" & Clean(txtVendorconcernedperson.Text) & "'"
        sql &= " ,'" & Clean(txtConcernedPhoneNumber.Text) & "'"
        sql &= " ,'" & Clean(txtModeofpayment.Text) & "'"
        sql &= " ,'" & Clean(txtBeneficiaryname.Text) & "'"
        sql &= " ,'" & Clean(hdntaxes.Value) & "'"
        sql &= " ,'" & Clean(hdntotal.Value) & "'"


        sql &= "          ,NULL,'" & path & "','" & path1 & "')"
        sql &= ""

        Dim EmpadvanceID As Integer = 0
        EmpadvanceID = ExecuteSingleResult(sql, _DataType.Numeric)
        If EmpadvanceID > 0 Then
            If SubmitEmployeeAdvanceExpences(EmpadvanceID) Then
                MessageDiv.Visible = True
                divContent.Visible = False
                lblMsg.Text = "Advance approval Request send to top management. Please wait for the approval."
                SendApprovalNotifictaion(clsApex.NotificationType.VendorAdvanceFromPMtoBH, "H", "<p>Vendor advance approval from <b>" & txtEmployeeName.Text & "</b> of amount Rs. <b>" & hdntotal.Value & "</b> for <b>" & txtEventName.Text & "</b></P>", 6)
                'Response.Write("<script>alert('Data Inserted SuccessFully')</script>")
            End If
        End If

    End Sub

    Private Function SubmitEmployeeAdvanceExpences(ByVal EmpadvanceID As Integer) As Boolean
        Try
            Dim result As Boolean = False
            Dim sql As String = ""

            For Each gvrow As GridViewRow In gvempadvance.Rows

                Dim lbltitle As Label = DirectCast(gvrow.FindControl("lbltitle"), Label)
                Dim txtadvancereq As TextBox = DirectCast(gvrow.FindControl("txtadvancereq"), TextBox)
                Dim lblbudget As TextBox = DirectCast(gvrow.FindControl("lblbudget"), TextBox)
                Dim hdntaskID As HiddenField = DirectCast(gvrow.FindControl("hdntaskID"), HiddenField)

                sql &= "	INSERT INTO [dbo].[Apex_VendorAdvanceExpenses]"
                sql &= "           ([RefEmpAdvanceID]"
                sql &= "           ,[ExpenseHead]"
                sql &= "           ,[AdvanceAmtReq]"
                sql &= "           ,[BudgetAmtAllocated]"
                sql &= "         ,RefTaskID "
                sql &= "           )"
                sql &= "    VALUES"
                sql &= "           (" & EmpadvanceID & ","
                sql &= "           '" & Clean(lbltitle.Text) & "', "
                sql &= "           '" & Clean(txtadvancereq.Text) & "', "
                sql &= "           '" & Clean(lblbudget.Text) & "'"
                sql &= "           ,'" & Clean(hdntaskID.Value) & "'"
                sql &= "	   )"
                sql &= ""
                sql &= ""
                sql &= ""

            Next
            If ExecuteNonQuery(sql) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function


    Public Sub SendApprovalNotifictaion(ByVal type As String, ByVal Role As String, ByVal message As String, ByVal desc As String)

        Try
            Dim sql As String = "INSERT INTO APEX_Notifications"
            sql &= "(NotificationTitle"
            sql &= ",NotificationMessage"
            sql &= ",Severity"
            sql &= ",Type"
            sql &= ",AssociateID"
            sql &= ",StartDate"
            sql &= ",EndDate"
            sql &= ",SendSMS"
            sql &= ",SendEmail,Link)"
            sql &= "VALUES"
            sql &= "('Request for Vendor Advance'"
            sql &= ",'<b>Project Name: </b>" & txtEventName.Text & "<br /><b>KAM: </b>" & txtKamName.Text & "<br /><hr  /> " & message & "'"
            sql &= ",'H'"
            sql &= "," & type
            sql &= "," & hdnJobCardID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N','VendorAdvance.aspx?jc=" & hdnJobCardID.Value & "&type=Task&taid=" & hdnTaskID.Value & "')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails(type, Role, desc)
            End If

        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try

    End Sub

    Private Sub InsertRecieptentDetails(ByVal type As String, ByRef Role As String, ByVal desc As String)
        Try
            Dim notificationid As String = ""

            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & type & " and AssociateID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationID")) Then
                    notificationid = ds.Tables(0).Rows(0)("NotificationID").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationTitle")) Then
                    Title = ds.Tables(0).Rows(0)("NotificationTitle").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationMessage")) Then
                    message = ds.Tables(0).Rows(0)("NotificationMessage").ToString()
                End If
            End If
            Dim bheadid As String = ""
            Dim sql3 As String = " "
            sql3 = "select UserDetailsID from APEX_UsersDetails  where Role='" & Role & "' and Designation='" & desc & "'"
            If Role = "K" Then
                sql3 &= " And userdetailsID=" & hdnKamID.Value
            End If
            If Role = "O" Then
                sql3 &= " And userdetailsID=" & hdnPMID.Value
            End If
            sql3 &= " And isactive='Y' and Isdeleted='N'"

            Dim clsApex As New clsApex
            Dim ds3 As New DataSet
            ds3 = ExecuteDataSet(sql3)
            If ds3.Tables(0).Rows.Count > 0 Then
                Dim emailid As String = ""
                For i As Integer = 0 To ds3.Tables(0).Rows.Count - 1
                    If Not IsDBNull(ds3.Tables(0).Rows(0)("UserDetailsID")) Then
                        bheadid = ds3.Tables(0).Rows(i)(0).ToString()
                        If notificationid <> "" Then
                            sendnotification(bheadid, notificationid)
                            If i = 0 Then
                                emailid = emailid + GetEmailIDByUserID(Convert.ToInt32(bheadid))
                            Else
                                emailid &= emailid + GetEmailIDByUserID(Convert.ToInt32(bheadid)) + "|"
                            End If
                        End If
                    End If
                Next

                If emailid <> "" Then

                    sendMailforEmployeeAdvance(Title, message, "", emailid, GetEmailIDByUserID(Convert.ToInt32(hdnKamID.Value)), GetEmailIDByUserID(Convert.ToInt32(hdnPMID.Value)))

                End If
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub



    Private Sub sendnotification(ByVal bheadid As String, ByVal notification As String)
        Try
            Dim sql1 As String = "INSERT INTO APEX_Recieptents"
            sql1 &= " (RefNotificationID"
            sql1 &= ",UserID"
            sql1 &= ",IsSeen"
            sql1 &= ",IsHidden"
            sql1 &= ",IsExecuted"
            sql1 &= ",SMSSentOn"
            sql1 &= ",EmailSentOn"
            sql1 &= ",InsertedBy)"
            sql1 &= "VALUES"
            sql1 &= "(" & notification
            sql1 &= "," & bheadid
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then



            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub btnapproved_Click(sender As Object, e As EventArgs) Handles btnapproved.Click
        Dim sql As String = " update Apex_VendorAdvence set BranchHeadApproval='Y',BranchheadApprovalBy='" & getLoggedUserID() & "',BranchheadApprovalDate=getdate() where ID='" & hdnEmployeeadvanceID.Value & "'"
        If ExecuteNonQuery(sql) Then
            MessageDiv.Visible = True
            divContent.Visible = False
            lblMsg.Text = "Advance approval Request send to Finance team."
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            SendApprovalNotifictaion(clsApex.NotificationType.VendorAdvanceFromBHtoFM, "F", "<p>Vendor advance of amount Rs. <b>" & txttotal.Text & "</b> for <b>" & txtEventName.Text & "</b> has been approved by " & getLoggedUserName() & " for <b>" & txtEmployeeName.Text & "</b> </P>", 3)

        End If
    End Sub

    Private Sub btnpaymentproccess_Click(sender As Object, e As EventArgs) Handles btnpaymentproccess.Click
        Dim sql As String = " update Apex_VendorAdvence set FinaceApproval='Y',financeApprovalDate=getdate(),FinanceHeadApprovalBy='" & getLoggedUserID() & "',PaidAmount='" & Clean(txtpaidamount.Text) & "',Remarks='" & Clean(txtremarks.Text) & "' where ID='" & hdnEmployeeadvanceID.Value & "'"
        If ExecuteNonQuery(sql) Then
            MessageDiv.Visible = True
            divContent.Visible = False
            lblMsg.Text = "Advance payment Done."
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            SendApprovalNotifictaion(clsApex.NotificationType.VendorAdvanceFromFMtoPM, "O", "<p>Vendor advance of amount Rs. <b>" & txtpaidamount.Text & "</b> for <b>" & txtEventName.Text & "</b> has been approved by <b>" & getLoggedUserName() & "</b></P>", 3)

        End If
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Dim sql As String = " update Apex_VendorAdvence set BranchHeadApproval='R',BranchheadApprovalBy='" & getLoggedUserID() & "',BranchheadApprovalDate=getdate() where ID='" & hdnEmployeeadvanceID.Value & "'"
        If ExecuteNonQuery(sql) Then
            MessageDiv.Visible = True
            divContent.Visible = False
            lblMsg.Text = "Advance Request has been rejected."
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnNodificationID.Value, getLoggedUserID())
            SendApprovalNotifictaion(clsApex.NotificationType.VendorAdvanceFromFMtoPM, "F", "<p>Vendor advance of amount Rs. <b>" & txttotal.Text & "</b> for <b>" & txtEventName.Text & "</b> has been rejected by " & getLoggedUserName() & "</P>", 3)

        End If
    End Sub

End Class
