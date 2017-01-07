Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex
Imports System.IO

Partial Class ViewJobInvoice
    Inherits System.Web.UI.Page
    Dim role As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            divError.Visible = False
            MessageDiv.Visible = False
            If Len(Request.QueryString("jid") > 0) Then
                If Request.QueryString("jid") <> "" Then

                    hdnJobCardID.Value = Request.QueryString("jid")
                    btnEditforFM.Visible = False
                    Dim apx As New clsApex
                    hdnBrief.Value = apx.GetBriefIDByJobCardID(hdnJobCardID.Value)

                    role = apx.GetRoleNameByUserID(getLoggedUserID())
                    If checkmax_and_min_estimateID() Then

                    Else
                        If Len(Request.QueryString("min") > 0) Then
                            If Request.QueryString("min") <> "" Then
                                hdnestimateID.Value = Request.QueryString("min")
                            End If
                        End If
                        If Len(Request.QueryString("max") > 0) Then
                            If Request.QueryString("max") <> "" Then
                                hdnmaxestimateID.Value = Request.QueryString("max")
                            End If
                        End If

                    End If
                    If Len(Request.QueryString("nid")) > 0 Then
                        If Request.QueryString("nid") <> "" Then
                            Dim capex As New clsApex

                            Dim sql As String = ""
                            sql &= " select JOBINVOICEID,RefMinTempEstimateID,RefMaxTempEstimateID,RefJobCardID,GrandTotal  from [dbo].[APEX_JobInvoice] where RefMaxTempEstimateID =" & Request.QueryString("jid")

                            Dim ds As New DataSet
                            ds = ExecuteDataSet(sql)
                            If ds.Tables(0).Rows.Count > 0 Then
                                hdnestimateID.Value = ds.Tables(0).Rows(0)("RefMinTempEstimateID")
                                hdnmaxestimateID.Value = ds.Tables(0).Rows(0)("RefMaxTempEstimateID")
                                hdnJobCardID.Value = ds.Tables(0).Rows(0)("RefJobCardID")
                            End If

                            hdnNotificationID.Value = Request.QueryString("nid")
                            capex.UpdateRecieptentSeen(hdnNotificationID.Value, getLoggedUserID())

                        End If
                    End If
                    If checkInvoice() = True Then
                        If role = "K" Or role = "H" Then
                            btnEdit.Visible = True
                            btnEditforFM.Visible = False
                            fupl_Mail.Visible = True
                            'BindJobInvoice()
                        Else
                            btnEdit.Visible = False
                            btnEditforFM.Visible = True
                            fupl_Mail.Visible = False
                        End If
                        btnsave.Visible = False
                        BindJobInvoiceafterInvoicing()
                        '
                    Else
                        BindJobInvoice()
                        btnsave.Visible = True
                        btnEdit.Visible = False
                    End If

                    If role = "K" Or role = "H" Then
                        pnlforKAM.Visible = True
                        pnlforFM.Visible = False
                        Panelfinace.Visible = False
                    Else
                        pnlforFM.Visible = True
                        pnlforKAM.Visible = False
                        Panelfinace.Visible = True
                    End If



                    If checkInvoice_FM() = True Then
                        btnEditforFM.Visible = False
                        txtpaymentReceiveAmount.Enabled = False
                        txtpaymentReceiveDate.Enabled = False
                        ddlSenttoclient.Enabled = False
                    End If

                    If CheckNotification() = True Then
                        Dim capex1 As New clsApex
                        capex1.UpdateRecieptentSeen(hdnNotificationID.Value, getLoggedUserID())
                    End If

                Else
                    CallDivError()
                End If
            Else
                CallDivError()
            End If

        End If
        pnlforKAM.Visible = True
        pnlforFM.Visible = False
    End Sub

    Private Function checkInvoice() As Boolean
        Dim result As Boolean = False

        Dim sql As String = "Select * from APEX_JobInvoice where RefJobCardID=" & hdnJobCardID.Value & " and refmintempestimateID=" & hdnestimateID.Value & " and refmaxtempestimateID=" & hdnmaxestimateID.Value & ""

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("InvoiceNo")) Then
                txtInvoicenumber.Text = ds.Tables(0).Rows(0)("InvoiceNo")
            Else
                txtInvoicenumber.Text = ""
            End If


            result = True
        End If
        Return result
    End Function
    Private Function checkmax_and_min_estimateID() As Boolean
        Dim result As Boolean = False
        Dim sql As String = ""
        sql &= " select top 1 estimateID,Subtotal,Managementfee,total,servicetax,GrandTotal from Apex_Estimate_for_invoicing where isinvoiced='N' and RefBriefID =" & hdnBrief.Value
        sql &= " select top 1 estimateID from Apex_Estimate_for_invoicing  where isinvoiced='N' and RefBriefID =" & hdnBrief.Value & " order by estimateID desc"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnestimateID.Value = ds.Tables(0).Rows(0)("estimateID")
            result = True
        End If
        If ds.Tables(1).Rows.Count > 0 Then
            hdnmaxestimateID.Value = ds.Tables(1).Rows(0)("estimateID")
            result = True
        End If


        'If ds.Tables(0).Rows.Count > 0 Then
        '    If Not IsDBNull(ds.Tables(0).Rows(0)("InvoiceNo")) Then
        '        txtInvoicenumber.Text = ds.Tables(0).Rows(0)("InvoiceNo")
        '    Else
        '        txtInvoicenumber.Text = ""
        '    End If


        '    result = True
        'End If
        Return result
    End Function
    Private Function checkInvoice_FM() As Boolean
        Dim result As Boolean = False
        'Dim sql As String = "Select * from APEX_JobInvoice where RefJobCardID=" & hdnJobCardID.Value
        Dim sql As String = "Select * from APEX_JobInvoice where RefMinTempEstimateID=" & hdnestimateID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)("Isinvoice").ToString() <> "N" Then
                result = True
            End If
        End If
        Return result
    End Function

    Private Sub BindJobInvoice()
        'Dim sql As String = "select JobCardNo,JobCardName,jc.RefClientID"
        'sql &= " ,convert(varchar(10),ActivityStartDate,105) + ' to ' + convert(varchar(10),ActivityEndDate,105) as ActivityDate "
        'sql &= " ,ct.Client,isnull(ct.Address,'N/A') as Address"
        'sql &= ",isnull(EstimatedManagentFees,0) as EstimatedManagentFees,isnull(EstimatedSubTotal,0) as EstimatedSubTotal"
        'sql &= ",(isnull(EstimatedManagentFees,0) + isnull(EstimatedSubTotal,0)) * 0.1236 as 'ServiceTax'"
        'sql &= ",isnull(EstimatedManagentFees,0) + isnull(EstimatedSubTotal,0) + (isnull(EstimatedManagentFees,0) + isnull(EstimatedSubTotal,0)) * 0.1236 as 'GrandTotal'"
        '' sql &= ",(((isnull(EstimatedSubTotal,0) + (isnull(EstimatedManagentFees,0))) - isnull(sum(PostEventTotal),0)) / (isnull(EstimatedSubTotal,0))) * 100 as ProfitPercentage "
        'sql &= " ,convert(numeric(18,2),(((isnull(EstimatedSubTotal+EstimatedManagentFees,0))-"
        'sql &= " (select Sum(isnull(actual,0)) from APEX_TempEstimate where refBriefID=(select refBriefID from apex_jobcard where jobcardID=" & hdnJobCardID.Value & ")))*100) "
        'sql &= "/ (isnull(EstimatedSubTotal+EstimatedManagentFees,0))) as ProfitPercentage  From APEX_JobCard as jc"

        'sql &= ""

        'sql &= " Inner Join APEX_Clients as ct on jc.RefClientID = ct.ClientID"
        'sql &= " Inner Join APEX_Estimate as et on jc.RefBriefID=et.RefBriefID"
        'sql &= " left outer Join APEX_PostPnL as pl on jc.JobCardID = pl.RefJobCardID"
        'sql &= " left outer Join APEX_tempPostPnLCost as tpl on pl.PostPnLID = tpl.RefPostPnLID"
        'sql &= " where JobCompleted='Y' and JobcardID=" & hdnJobCardID.Value
        'sql &= " Group by JobCardNo,JobCardName,jc.RefClientID,ActivityStartDate,ActivityEndDate,Client,ct.Address,EstimatedManagentFees,EstimatedSubTotal"
        '0 table
        Dim sql As String = ""
        sql &= " select Ponumber,JobCardNo,JobCardName,jc.RefClientID ,convert(varchar(10),ActivityStartDate,105) + ' to ' + convert(varchar(10),ActivityEndDate,105) as ActivityDate ,"
        sql &= " convert(varchar(10),ActivityStartDate,105) ActivityStartDate,convert(varchar(10),ActivityEndDate,105)ActivityEndDate,ct.Client,isnull(ct.Address,'N/A') as Address"
        sql &= ", (isnull(ATS.Actual,0)*(isnull(EstimatedManagentFees,0) / isnull(et.EstimatedSubTotal,0) ))"
        sql &= " as EstimatedManagentFees,isnull(ATS.Actual,0) as EstimatedSubTotal, isnull(tpl.posteventcost,0)Expenses, (isnull(ATS.Actual,0)) * 0.1236 as 'ServiceTax', "
        sql &= " isnull(ATS.Actual,0) + ((isnull(ATS.Actual,0)*(isnull(EstimatedManagentFees,0) / isnull(et.EstimatedSubTotal,0) )) + isnull(ATS.Actual,0)* 0.1236) GrandTotal "
        'sql &= " ,convert(numeric(18,2),(((isnull(ATS.Actual+(isnull(ATS.Actual,0)*(isnull(EstimatedManagentFees,0) / isnull(et.EstimatedSubTotal,0) )),0))- "
        'sql &= " (isnull(tpl.posteventcost,0)))*100) / (isnull(ATS.Actual+(isnull(ATS.Actual,0)*(isnull(EstimatedManagentFees,0) / isnull(et.EstimatedSubTotal,0) )),0))) as ProfitPercentage "
        sql &= "   From APEX_JobCard as jc Inner Join APEX_Clients as ct on jc.RefClientID = ct.ClientID "
        sql &= "   Inner Join APEX_Estimate as et on jc.RefBriefID=et.RefBriefID "
        sql &= "   left outer Join APEX_PostPnL as pl on jc.JobCardID = pl.RefJobCardID "
        sql &= "   left outer Join APEX_tempPostPnLCost as tpl on pl.PostPnLID = tpl.RefPostPnLID "
        sql &= "   inner join APEX_TempEstimate as ATS on et.RefBriefID= ATS.RefBriefID"
        sql &= "   where JobCompleted='Y' and JobcardID=" & hdnJobCardID.Value
        sql &= "    Group by Ponumber,JobCardNo,JobCardName,jc.RefClientID,ActivityStartDate,ActivityEndDate,Client,ct.Address,EstimatedManagentFees,EstimatedSubTotal,ATS.Actual,tpl.posteventcost"
        sql &= ""
        '1 table
        sql &= " select top 1 estimateID,Subtotal,Managementfee,total,servicetax,GrandTotal from Apex_Estimate_for_invoicing where isinvoiced='N' and RefBriefID =" & hdnBrief.Value

        'sql &= "	Select SUM(Actual) as SubTotal,((SUM(Actual)*(EstimatedManagentFees/EstimatedSubTotal*100))/100)EstimatedManagentFees,"
        'sql &= "	(((SUM(Actual)*(EstimatedManagentFees/EstimatedSubTotal*100))/100)+SUM(Actual)) as Total"
        'sql &= "	,((((SUM(Actual)*(EstimatedManagentFees/EstimatedSubTotal*100))/100)+SUM(Actual))*((EstimatedServiceTax/EstimatedTotal)))ServiceTax"
        'sql &= "	,(((SUM(Actual)*(EstimatedManagentFees/EstimatedSubTotal*100))/100)+SUM(Actual)) +"
        'sql &= "	((((SUM(Actual)*(EstimatedManagentFees/EstimatedSubTotal*100))/100)+SUM(Actual))*((EstimatedServiceTax/EstimatedTotal)))"
        'sql &= "	EstimatedGrandTotal  from APEX_TempEstimate as te   "
        'sql &= "	Join APEX_Estimate as et on et.EstimateID = te.RefEstimateID "
        'sql &= "	where et.RefBriefID =" & hdnBrief.Value 'hdnJobCardID.Value
        'sql &= "	Group By EstimatedSubTotal,EstimatedManagentFees,EstimatedTotal,EstimatedGrandTotal,EstimatedServiceTax"
        sql &= ""
        '2 table
        sql &= ""
        sql &= "    select sum(Posteventcost)Posteventcost from APEX_tempPostPnLCost"
        sql &= " where refbriefId = " & hdnBrief.Value 'hdnJobCardID.Value
        '3 table
        sql &= ""
        sql &= "select top 1 estimateID from Apex_Estimate_for_invoicing  where isinvoiced='N' and RefBriefID =" & hdnBrief.Value & " order by estimateID desc"
        sql &= ""
        '4 table
        sql &= " select Isnull(sum(EstimatedSubtotal),0.00)ET from [dbo].[APEX_JobInvoice] where refjobcardID=" & hdnJobCardID.Value
        sql &= ""

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        lblfulpl_Mail.Visible = False
        If ds.Tables(0).Rows.Count > 0 Then
            lblJobCardNo.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            lblProjectName.Text = ds.Tables(0).Rows(0)("JobCardName").ToString()
            'txtProfatibility.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ProfitPercentage").ToString()), 2)
            txtExecutionDate.Text = ds.Tables(0).Rows(0)("ActivityDate").ToString()
            'txtExpenses.Text = ds.Tables(0).Rows(0)("Expenses").ToString()
            txtExpenses.Text = ds.Tables(1).Rows(0)("SubTotal").ToString()
            hdnestimateID.Value = ds.Tables(1).Rows(0)("estimateID").ToString()
            hdnmaxestimateID.Value = ds.Tables(3).Rows(0)("estimateID").ToString()

            txtAgencyFees.Text = Math.Round(Convert.ToDouble(ds.Tables(1).Rows(0)("Managementfee").ToString()), 2)

            txtServiceTax.Text = Math.Round(Convert.ToDouble(ds.Tables(1).Rows(0)("servicetax").ToString()), 2)
            'txtServiceTax.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ServiceTax").ToString()), 2)
            'txtBillingAmount.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("GrandTotal").ToString()), 2)
            txtBillingAmount.Text = Math.Round(Convert.ToDouble(ds.Tables(1).Rows(0)("GrandTotal").ToString()), 2)
            lblClientBilling.Text = ds.Tables(0).Rows(0)("Address").ToString()
            lblClientName.Text = ds.Tables(0).Rows(0)("Client").ToString()

            lblJobCardNoFM.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            lblProjectNameFM.Text = ds.Tables(0).Rows(0)("JobCardName").ToString()
            'lblProfatibility.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ProfitPercentage").ToString()), 2)
            'lblProfatibility.Text = Math.Round(Convert.ToDouble(((txtBillingAmount.Text - txtServiceTax.Text - txtExpenses.Text) / (txtBillingAmount.Text - txtServiceTax.Text)) * 100), 2)
            'lblProfatibility.Text = Math.Round(Convert.ToDouble(((ds.Tables(1).Rows(0)("Total").ToString() - ds.Tables(2).Rows(0)("Posteventcost").ToString()) / (ds.Tables(1).Rows(0)("Total").ToString())) * 100), 2)
            If ds.Tables(4).Rows(0)("ET").ToString() = "0.00" Then
                lblProfatibility.Text = Math.Round(Convert.ToDouble(((ds.Tables(1).Rows(0)("Total").ToString() - ds.Tables(2).Rows(0)("Posteventcost").ToString()) / (ds.Tables(1).Rows(0)("Total").ToString())) * 100), 2)
                txtProfatibility.Text = Math.Round(Convert.ToDouble(((ds.Tables(1).Rows(0)("Total").ToString() - ds.Tables(2).Rows(0)("Posteventcost").ToString()) / (ds.Tables(1).Rows(0)("Total").ToString())) * 100), 2)
            Else
                Dim PF As Decimal = Math.Round(Convert.ToDouble(((ds.Tables(4).Rows(0)("ET").ToString() + Convert.ToDouble(ds.Tables(1).Rows(0)("Total").ToString())) - ds.Tables(2).Rows(0)("Posteventcost").ToString())))
                txtProfatibility.Text = Math.Round(((PF / Convert.ToDouble((ds.Tables(4).Rows(0)("ET").ToString() + Convert.ToDouble(ds.Tables(1).Rows(0)("Total").ToString())))) * 100), 2)
                lblProfatibility.Text = Math.Round(((PF / Convert.ToDouble((ds.Tables(4).Rows(0)("ET").ToString() + Convert.ToDouble(ds.Tables(1).Rows(0)("Total").ToString())))) * 100), 2)
                'Math.Round(Convert.ToDouble((ds.Tables(4).Rows(0)("ET").ToString() + Convert.ToDouble(ds.Tables(1).Rows(0)("Total").ToString()))))
                '/ ((ds.Tables(4).Rows(0)("ET").ToString()) + (ds.Tables(1).Rows(0)("Total").ToString()))) * 100), 2)
                'lblProfatibility.Text = Math.Round(Convert.ToDouble((((ds.Tables(4).Rows(0)("ET").ToString()) + (ds.Tables(1).Rows(0)("Total").ToString()) - ds.Tables(2).Rows(0)("Posteventcost").ToString()) / ((ds.Tables(4).Rows(0)("ET").ToString()) + (ds.Tables(1).Rows(0)("Total").ToString()))) * 100), 2)
            End If

            lblExecutionDate.Text = ds.Tables(0).Rows(0)("ActivityDate").ToString()
            lblExpenses.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
            lblAgencyFees.Text = ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()
            lblServiceTax.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ServiceTax").ToString()), 2)
            lblBillingAmount.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("GrandTotal").ToString()), 2)
            lblClientBillingFM.Text = ds.Tables(0).Rows(0)("Address").ToString()
            lblClientNameFM.Text = ds.Tables(0).Rows(0)("Client").ToString()
            hdnClientID.Value = ds.Tables(0).Rows(0)("RefClientID").ToString()
            txtExecutionDatefrom.Text = ds.Tables(0).Rows(0)("ActivityStartDate").ToString()
            txtExecutionDateTo.Text = ds.Tables(0).Rows(0)("ActivityEndDate").ToString()
            lblPONo.Text = ds.Tables(0).Rows(0)("Ponumber").ToString()

        End If
    End Sub

    Private Sub BindJobInvoiceafterInvoicing()
        Dim sql As String = "select JobInvoiceID,Ponumber,JobCardNo,JobCardName,jc.RefClientID"
        sql &= " ,convert(varchar(10),ActivityStartDate,105) + ' to ' + convert(varchar(10),ActivityEndDate,105) as ActivityDate "
        sql &= " ,ct.Client,isnull(ct.Address,'N/A') as Address"
        sql &= ",isnull(EstimatedManagentFees,0) as EstimatedManagentFees,isnull(EstimatedSubTotal,0) as EstimatedSubTotal"
        sql &= ",ServiceTax as 'ServiceTax'"
        sql &= ",Grandtotal as 'GrandTotal' ,ProfitPercentage as ProfitPercentage,"
        sql &= " convert(varchar(20),paymentreceivedate,105) as paymentreceivedate ,Senttoclient,Isnull(PaymentReceiveAmount,0)PaymentReceiveAmount  ,convert(varchar(10),ActivityStartDate,105) ActivityStartDate,convert(varchar(10),ActivityEndDate,105)ActivityEndDate"
        sql &= ", ProjectName, ClientBillingAddress, ClientName, Po,Filepath"
        sql &= " From APEX_JobCard as jc"
        sql &= " Inner Join APEX_Clients as ct on jc.RefClientID = ct.ClientID"

        sql &= " left outer Join APEX_JobInvoice as invoice on invoice.RefJobcardID = jc.jobcardID"
        sql &= " where JobCompleted='Y' and JobcardID=" & hdnJobCardID.Value & " and Invoice.RefMinTempEstimateID='" & hdnestimateID.Value & "' and Invoice.RefMaxTempEstimateID='" & hdnmaxestimateID.Value & "'"
        '1 table
        'sql &= " select sum(EstimatedSubtotal)ET from [dbo].[APEX_JobInvoice] where refjobcardID=" & hdnJobCardID.Value

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnjobinvoiceID.Value = ds.Tables(0).Rows(0)("JobInvoiceID").ToString()
            lblJobCardNo.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            'lblProjectName.Text = ds.Tables(0).Rows(0)("JobCardName").ToString()
            lblProjectName.Text = ds.Tables(0).Rows(0)("ProjectName").ToString()
            txtProfatibility.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ProfitPercentage").ToString()), 2)
            txtExecutionDate.Text = ds.Tables(0).Rows(0)("ActivityDate").ToString()
            txtExpenses.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
            txtAgencyFees.Text = ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()
            txtServiceTax.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ServiceTax").ToString()), 2)
            txtBillingAmount.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("GrandTotal").ToString()), 2)

            'lblClientBilling.Text = ds.Tables(0).Rows(0)("Address").ToString()
            'lblClientName.Text = ds.Tables(0).Rows(0)("Client").ToString()

            lblClientBilling.Text = ds.Tables(0).Rows(0)("ClientBillingAddress").ToString()
            lblClientName.Text = ds.Tables(0).Rows(0)("ClientName").ToString()

            lblJobCardNoFM.Text = ds.Tables(0).Rows(0)("JobCardNo").ToString()
            lblProjectNameFM.Text = ds.Tables(0).Rows(0)("JobCardName").ToString()
            lblProfatibility.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ProfitPercentage").ToString()), 2)
            lblExecutionDate.Text = ds.Tables(0).Rows(0)("ActivityDate").ToString()
            lblExpenses.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
            lblAgencyFees.Text = ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()
            lblServiceTax.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("ServiceTax").ToString()), 2)
            lblBillingAmount.Text = Math.Round(Convert.ToDouble(ds.Tables(0).Rows(0)("GrandTotal").ToString()), 2)
            lblClientBillingFM.Text = ds.Tables(0).Rows(0)("Address").ToString()
            lblClientNameFM.Text = ds.Tables(0).Rows(0)("Client").ToString()
            hdnClientID.Value = ds.Tables(0).Rows(0)("RefClientID").ToString()
            txtpaymentReceiveDate.Text = ds.Tables(0).Rows(0)("paymentreceivedate").ToString()
            txtpaymentReceiveAmount.Text = ds.Tables(0).Rows(0)("PaymentReceiveAmount").ToString()
            If ds.Tables(0).Rows(0)("Senttoclient").ToString() <> "" Then
                ddlSenttoclient.SelectedItem.Text = ds.Tables(0).Rows(0)("Senttoclient").ToString()
            End If
            txtExecutionDatefrom.Text = ds.Tables(0).Rows(0)("ActivityStartDate").ToString()
            txtExecutionDateTo.Text = ds.Tables(0).Rows(0)("ActivityEndDate").ToString()

            lblPONo.Text = ds.Tables(0).Rows(0)("Po").ToString()
            If ds.Tables(0).Rows(0)("Filepath").ToString() <> "" Then

                'lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("Approvalmail1").ToString()
                'lblfulpl_Mail.PostBackUrl = ds.Tables(0).Rows(0)("Approvalmail1").ToString()
                hdnlblfulpl_Mail.Value = ds.Tables(0).Rows(0)("Filepath").ToString()
                lblfulpl_Mail.Visible = True
            Else
                lblfulpl_Mail.Visible = False

            End If
        End If
    End Sub

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As System.EventArgs) Handles btnsave.Click
        Dim apx As New clsApex
        role = apx.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Or role = "H" Then
            saveinvoice()
            SendNotification_FMInv()
            updateEstimate_for_invoicing("Y")
            Updatejobcard("P")

        End If
    End Sub
    Private Sub updateEstimate_for_invoicing(ByVal status As String)
        Dim sql As String = "update [Apex_Estimate_for_invoicing] set isinvoiced='" & status & "' where estimateID between " & hdnestimateID.Value & " and " & hdnmaxestimateID.Value & " and RefBriefID =" & hdnBrief.Value & ""
        ExecuteNonQuery(sql)
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As System.EventArgs) Handles btnEdit.Click
        Dim apx As New clsApex
        role = apx.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Then
            updateinvoiceKAM()
        Else
            updateinvoice()

        End If
        'Dim sqlupdate As String = ""

        'sqlupdate &= "update  Jc set JobCardName='" & lblProjectName.Text & "',ActivityStartDate=Convert(datetime,'" & txtExecutionDatefrom.Text & "',105),ActivityEndDate=Convert(datetime,'" & txtExecutionDateTo.Text & "',105),Ponumber='" & lblPONo.Text & "' "
        'sqlupdate &= "from   APEX_JobCard JC  Inner join APEX_ClientContacts AC on JC.ClientContactPerson =Ac.ContactID    "
        'sqlupdate &= "Where Jc.JobCardID=" & hdnJobCardID.Value
        'sqlupdate &= " "
        'sqlupdate &= "update  AC set  AC.Client='" & lblClientName.Text & "',Ac.Address='" & lblClientBilling.Text & "'	"
        'sqlupdate &= "from   APEX_JobCard JC  Inner join APEX_ClientContacts AC on JC.ClientContactPerson =Ac.ContactID    "
        'sqlupdate &= "Where Jc.JobCardID=" & hdnJobCardID.Value
        'sqlupdate &= ""
        'sqlupdate &= ""
        'sqlupdate &= ""
        'sqlupdate &= ""
        'sqlupdate &= ""

        ''sqlupdate &= "where JobcardID = " & hdnJobCardID.Value

        'ExecuteNonQuery(sqlupdate)

    End Sub

    Protected Sub btnEditforFM_Click(sender As Object, e As EventArgs) Handles btnEditforFM.Click
        updateinvoice()
        Dim capex As New clsApex
        If hdnNotificationID.Value <> "" And IsNumeric(hdnNotificationID.Value) = True Then
            capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())
        End If
    End Sub
    Private Sub updateinvoiceKAM()
        Dim sql As String = ""
        sql = "Update APEX_JobInvoice set GrandTotal='" & Clean(txtBillingAmount.Text) & "'"
        If txtExpenses.Text <> "" Then
            sql &= " ,EstimatedSubTotal='" & Clean(txtExpenses.Text) & "' "
        End If
        If txtAgencyFees.Text <> "" Then
            sql &= " ,EstimatedManagentFees='" & Clean(txtAgencyFees.Text) & "' "
        End If
        If txtServiceTax.Text <> "" Then
            sql &= " ,ServiceTax='" & Clean(txtServiceTax.Text) & "' "
        End If
        If txtProfatibility.Text <> "" Then
            sql &= " ,ProfitPercentage='" & Clean(txtProfatibility.Text) & "' "
        End If
        sql &= ",ProjectName='" & Clean(lblProjectName.Text) & "',ClientBillingAddress='" & Clean(lblClientBilling.Text()) & "',ClientName='" & Clean(lblClientName.Text) & "',PO='" & Clean(lblPONo.Text) & "'"
        Dim Path As String = ""
        If fupl_Mail.HasFile = True Then
            Dim filename As String = fupl_Mail.FileName
            Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
            Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
            Dim encname As String = ""
            'txtUploads.Text = fname

            encname = fname & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
            fupl_Mail.SaveAs(Server.MapPath("uploads/Invoice_PO/" & Clean(encname.ToString().Replace("&", "")) & "." & fext))
            Path = "uploads/Invoice_PO/" & Clean(encname.ToString().Replace("&", "")) & "." & fext

            sql &= ",Filepath='" & Path & "'"
        End If

        sql &= " where RefJobCardID=" & hdnJobCardID.Value & " and RefMinTempEstimateID=" & hdnestimateID.Value & " RefMaxTempEstimateID=" & hdnmaxestimateID.Value & ""
        If ExecuteNonQuery(sql) > 0 Then
            MessageDiv.Visible = True
            lblMsg.Text = "Invoice Update Sucessfully"
            pnlforKAM.Visible = False
            btnCancel.Text = "Back to Home"
            btnEditforFM.Visible = False
            btnEdit.Visible = False
            btnsave.Visible = False
        End If
    End Sub

    Private Sub updateinvoice()
        Dim sql As String = ""
        Dim invoiveDate As String = "Convert(datetime,'" & txtpaymentReceiveDate.Text & "',105)"

        sql = "Update APEX_JobInvoice set Senttoclient='" & ddlSenttoclient.SelectedValue & "'"
        sql &= " ,PaymentReceiveDate= " & invoiveDate & " "
        sql &= " ,PaymentReceiveAmount='" & Clean(txtpaymentReceiveAmount.Text) & "' "
        sql &= " ,InvoiceNo='" & Clean(txtInvoicenumber.Text) & "' "
        sql &= " ,ISInvoice='Y' "
        sql &= " where JobInvoiceID=" & hdnjobinvoiceID.Value & " and RefMinTempEstimateID=" & hdnestimateID.Value & " and RefMaxTempEstimateID=" & hdnmaxestimateID.Value & ""
        'sql &= " where RefJobCardID=" & hdnJobCardID.Value
        'hdnjobinvoiceID.Value
        If ExecuteNonQuery(sql) > 0 Then
            Updatejobcard("Y")
            MessageDiv.Visible = True
            lblMsg.Text = "Invoice Generated"
            pnlforFM.Visible = False
            btnEdit.Visible = False
            btnsave.Visible = False
            btnEditforFM.Visible = False
            btnCancel.Text = "Back to Home"
        End If
    End Sub

    Private Sub Updatejobcard(ByVal status As String)
        Dim sql As String = "update APEX_JobCard set IsInvoiceing='" & status & "' where JobcardID = " & hdnJobCardID.Value
        ExecuteNonQuery(sql)
    End Sub

    Private Sub saveinvoice()
        'hdnestimateID.Value
        Dim sql As String = ""
        sql = ""
        sql = " Select count(RefJobCardID) From APEX_JobInvoice where RefMinTempEstimateID='" & Clean(hdnestimateID.Value) & "'  and RefMaxTempEstimateID='" & Clean(hdnmaxestimateID.Value) & "'"
        If ExecuteSingleResult(sql, _DataType.Numeric) > 0 Then
            Exit Sub
        End If
        Dim Path As String = ""
        If fupl_Mail.HasFile = True Then
            Dim filename As String = fupl_Mail.FileName
            Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
            Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
            Dim encname As String = ""
            'txtUploads.Text = fname

            encname = fname & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
            fupl_Mail.SaveAs(Server.MapPath("uploads/Invoice_PO/" & Clean(encname.ToString().Replace("&", "")) & "." & fext))
            Path = "uploads/Invoice_PO/" & Clean(encname.ToString().Replace("&", "")) & "." & fext

            'sql &= ",ApprovalMail='" & Path & "'"
        End If

        sql = ""
        sql &= "INSERT INTO [APEX_JobInvoice]"
        sql &= "           ([RefJobCardID]"
        sql &= "           ,[RefClientID]"
        sql &= "           ,[ProfitPercentage]"
        sql &= "           ,[ActivityDate]"
        sql &= "           ,[EstimatedSubTotal]"
        sql &= "           ,[EstimatedManagentFees]"
        sql &= "           ,[ServiceTax]"
        sql &= "           ,[GrandTotal],RefMinTempEstimateID,RefMaxTempEstimateID"
        sql &= " ,ProjectName,ClientBillingAddress,ClientName,PO,Filepath"
        sql &= "          )"
        sql &= "     VALUES"
        sql &= "           (" & hdnJobCardID.Value & ""
        sql &= "           ," & hdnClientID.Value & ""
        If txtProfatibility.Text <> "" Then
            sql &= "           ,'" & Clean(txtProfatibility.Text) & "'"
        Else
            sql &= "           ,NULL"
        End If
        If txtExecutionDate.Text <> "" Then
            sql &= "           ,getdate()"
        Else
            sql &= "           ,NULL"
        End If

        If txtExpenses.Text <> "" Then
            sql &= "           ,'" & Clean(txtExpenses.Text) & "'"
        Else
            sql &= "           ,NULL"
        End If
        If txtAgencyFees.Text <> "" Then
            sql &= "           ,'" & Clean(txtAgencyFees.Text) & "'"
        Else
            sql &= "           ,NULL"
        End If
        If txtServiceTax.Text <> "" Then
            sql &= "           ,'" & Clean(txtServiceTax.Text) & "'"
        Else
            sql &= "           ,NULL"
        End If
        If txtBillingAmount.Text <> "" Then
            sql &= "           ,'" & Clean(txtBillingAmount.Text) & "'"
        Else
            sql &= "           ,NULL"
        End If

        sql &= "  ,'" & Clean(hdnestimateID.Value) & "','" & Clean(hdnmaxestimateID.Value) & "'"
        sql &= " ,'" & Clean(lblProjectName.Text) & "','" & Clean(lblClientBilling.Text) & "','" & Clean(lblClientName.Text) & "','" & Clean(lblPONo.Text) & "','" & Clean(Path) & "'"
        sql &= " )"

        If ExecuteNonQuery(sql) > 0 Then
            'Dim sqlupdate As String = ""

            'sqlupdate &= "update  Jc set JobCardName='" & lblProjectName.Text & "',ActivityStartDate=Convert(datetime,'" & txtExecutionDatefrom.Text & "',105),ActivityEndDate=Convert(datetime,'" & txtExecutionDateTo.Text & "',105),Ponumber='" & lblPONo.Text & "' "
            'sqlupdate &= "from   APEX_JobCard JC  Inner join APEX_ClientContacts AC on JC.ClientContactPerson =Ac.ContactID   "
            'sqlupdate &= "Where Jc.JobCardID=" & hdnJobCardID.Value
            'sqlupdate &= " "
            'sqlupdate &= "update  AC set  AC.Client='" & lblClientName.Text & "',Ac.Address='" & lblClientBilling.Text & "'	"
            'sqlupdate &= "from   APEX_JobCard JC  Inner join APEX_ClientContacts AC on JC.ClientContactPerson =Ac.ContactID   "
            'sqlupdate &= "Where Jc.JobCardID=" & hdnJobCardID.Value
            'sqlupdate &= ""
            'sqlupdate &= ""
            'sqlupdate &= ""
            'sqlupdate &= ""
            'sqlupdate &= ""

            ''sqlupdate &= "where JobcardID = " & hdnJobCardID.Value

            'ExecuteNonQuery(sqlupdate)
            MessageDiv.Visible = True
            lblMsg.Text = "Job Invoice Sent to finance team"
            pnlforKAM.Visible = False
            btnEdit.Visible = False
            btnsave.Visible = False
            btnEditforFM.Visible = False
            btnCancel.Text = "Back to Home"
        End If
    End Sub

    Public Sub SendNotification_FMInv()
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
            sql &= ",SendEmail)"
            sql &= "VALUES"
            sql &= "('View Job Invoice'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>View Job Invoice'"
            sql &= ",'H'"
            sql &= "," & NotificationType.FMCreateInvoice
            'sql &= "," & hdnJobCardID.Value
            sql &= "," & hdnmaxestimateID.Value
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails_FMInv()
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Sub InsertRecieptentDetails_FMInv()
        Try
            Dim capex As New clsApex
            Dim title As String = ""
            Dim message As String = ""
            Dim notificationid As String = ""
            Dim sql As String = "Select NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.FMCreateInvoice & " and AssociateID=" & hdnJobCardID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationID")) Then
                    notificationid = ds.Tables(0).Rows(0)("NotificationID").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationTitle")) Then
                    title = ds.Tables(0).Rows(0)("NotificationTitle").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationMessage")) Then
                    message = ds.Tables(0).Rows(0)("NotificationMessage").ToString()
                End If
            End If

            Dim bheadid As String = ""
            Dim sql3 As String = " "

            'sql3 = "Select insertedBy from  APEX_Brief  where BriefID= " & hdnBriefID.Value
            'Dim ds3 As New DataSet
            'ds3 = ExecuteDataSet(sql3)
            'If ds3.Tables(0).Rows.Count > 0 Then
            '    bheadid = ds3.Tables(0).Rows(0)(0).ToString()
            'End If
            bheadid = capex.GetFinanceManagerOfJobCard(hdnJobCardID.Value)
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
            sql1 &= "(" & notificationid
            sql1 &= "," & bheadid
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",'N'"
            sql1 &= ",NULL"
            sql1 &= ",NULL"
            sql1 &= "," & getLoggedUserID() & ")"

            If ExecuteNonQuery(sql1) > 0 Then
                Dim emailid As String = ""
                Dim uid As Integer = Convert.ToInt32(bheadid)
                emailid = GetEmailIDByUserID(uid)
                If emailid <> "" Then
                    sendMail(title, message, "", emailid, "")
                End If

            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
    End Sub

    Private Function getProjectName() As String
        Dim result As String = ""
        If hdnBrief.Value <> "" Then
            Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & hdnBrief.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)("BriefName").ToString()
            End If
        End If

        Return result
    End Function

    Private Function getKAMName() As String
        Dim result As String = ""
        If hdnBrief.Value <> "" Then
            Dim sql As String = "select isnull(FirstName,'') + ' ' +isnull(LastName,'') as KAMName "
            sql &= " from APEX_UsersDetails as ud "
            sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
            sql &= " where ab.BriefID = " & hdnBrief.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = ds.Tables(0).Rows(0)("KAMName").ToString()
            End If
        End If

        Return result
    End Function

    Public Function CheckNotification() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "select * from APEX_Notifications "
        sql &= " inner join APEX_Recieptents on RefNotificationID=NotificationID"
        sql &= " where type=20 and AssociateID=" & hdnJobCardID.Value & " and IsExecuted='N' and UserID=" & getLoggedUserID()
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnNotificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
            result = True
        End If
        Return result
    End Function

    Protected Sub lblfulpl_Mail_Click(sender As Object, e As EventArgs) Handles lblfulpl_Mail.Click
        Response.ContentType = "application/vnd.ms-outlook"

        Dim str As String = hdnlblfulpl_Mail.Value
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(str) & "")
        Response.TransmitFile("~/" & str.ToString())
        'Response.TransmitFile("~/Collateral/Jobcard/65K Fund 2015422000.msg")
        Response.End()
    End Sub
    Public Shared Function GetFileName(path As String) As String

    End Function

End Class
