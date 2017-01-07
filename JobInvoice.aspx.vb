Imports clsDatabaseHelper
Imports clsMain
Imports System.Data

Partial Class JobInvoice
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            divError.Visible = False
            If Len(Request.QueryString("jid")) > 0 Then
                If Request.QueryString("jid") <> "" Then
                    hdnJobCardID.Value = Request.QueryString("jid")
                    FillKestoneDetails()
                    FillClient()
                    FillActivity()
                    FillEstimate()
                    FillEstimateDetails()

                    If checkInvoice() = False Then
                        lblBuyer.Visible = False
                        lblBuyerDated.Visible = False
                        lblInvoiceNo.Visible = False
                        lblDated.Visible = False
                        lblSupplierReference.Visible = False
                        lblOtherReference.Visible = False

                        txtBuyer.Visible = True
                        txtBuyerDate.Visible = True
                        txtInvoiceNo.Visible = True
                        txtDated.Visible = True
                        txtSupplierRef.Visible = True
                        txtOtherRef.Visible = True
                    Else
                        FillData()
                        CloseAllControls()
                        btnSave.Visible = False
                        btnReset.Visible = False
                    End If

                    If Len(Request.QueryString("nid")) > 0 Then
                        If Request.QueryString("nid") <> "" Then
                            Dim capex As New clsApex
                            hdnNotificationID.Value = Request.QueryString("nid")
                            capex.UpdateRecieptentSeen(hdnNotificationID.Value, getLoggedUserID())
                        End If
                    End If
                Else
                    CallDivError()
                End If
            Else
                CallDivError()
            End If
        End If
    End Sub

    Private Sub FillKestoneDetails()
        Dim sql As String = "Select * from APEX_KestoneMaster"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblName.Text = ds.Tables(0).Rows(0)("Title").ToString()
            lblAddress.Text = ds.Tables(0).Rows(0)("Address").ToString()
            lblBankName.Text = ds.Tables(0).Rows(0)("BankName").ToString()
            lblBankAddress.Text = ds.Tables(0).Rows(0)("BankAddress").ToString()
            lblAccountNo.Text = ds.Tables(0).Rows(0)("AccountNo").ToString()
            lblIFSC.Text = ds.Tables(0).Rows(0)("IFSC_Code").ToString()
            lblSwiftCode.Text = ds.Tables(0).Rows(0)("Swift_Code").ToString()
            lblPAN.Text = ds.Tables(0).Rows(0)("PAN").ToString()
            lblTin.Text = ds.Tables(0).Rows(0)("TIN_No").ToString()
            lblSTaxNo.Text = ds.Tables(0).Rows(0)("ServiceTax").ToString()
            '   lblKName.Text = ds.Tables(0).Rows(0)("Title").ToString()
            '  lblKAddress.Text = ds.Tables(0).Rows(0)("Address").ToString()
            lblContact.Text = ds.Tables(0).Rows(0)("Contact").ToString()
        End If
    End Sub

    Private Sub FillClient()
        Dim sql As String = "select Client,isnull(Address,'N/A') as Address From APEX_Clients as c inner join APEX_JobCard as j on c.ClientID = j.RefClientID where JobCardID = " & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblClient.Text = ds.Tables(0).Rows(0)("Client").ToString()
            lblClientAddress.Text = ds.Tables(0).Rows(0)("Address").ToString()
        End If
    End Sub

    Private Sub FillActivity()
        Dim sql As String = "select ProjectType From APEX_ActivityType as a inner join APEX_JobCard as j on a.ProjectTypeID = j.PrimaryActivityID where JobCardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblSTaxCategory.Text = ds.Tables(0).Rows(0)("ProjectType").ToString()
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


    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtInvoiceNo.Text = ""
        txtBuyer.Text = ""
        txtBuyerDate.Text = ""
        txtDated.Text = ""
        txtInvoiceNo.Text = ""
        txtOtherRef.Text = ""
        txtSupplierRef.Text = ""
    End Sub

    Private Sub FillEstimate()
        Dim sql As String = "select Particulars,Estimate as Amount from APEX_tempEstimate as e "
        sql &= " inner join APEX_JobCard as j on e.RefBriefID = j.RefBriefID"
        sql &= " where j.JobCardID=" & hdnJobCardID.Value

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gvEstimate.DataSource = ds
            gvEstimate.DataBind()
        End If
    End Sub

    Private Sub FillEstimateDetails()
        Dim sql As String = " select EstimatedSubTotal,EstimatedManagentFees,EstimatedServiceTax,EstimatedGrandTotal,convert(numeric(18,2),((EstimatedServiceTax * 100) / (EstimatedManagentFees + EstimatedSubTotal))) as ServiceTaxPer from APEX_Estimate as e "
        sql &= " inner join APEX_JobCard as j on e.RefBriefID = j.RefBriefID"
        sql &= " where j.JobCardID=" & hdnJobCardID.Value

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblTotal.Text = ds.Tables(0).Rows(0)("EstimatedSubTotal").ToString()
            lblAgencyFee.Text = ds.Tables(0).Rows(0)("EstimatedManagentFees").ToString()
            lblServiceTaxAmount.Text = ds.Tables(0).Rows(0)("EstimatedServiceTax").ToString()
            lblTotalFee.Text = ds.Tables(0).Rows(0)("EstimatedGrandTotal").ToString()
            lblServiceTax.Text = ds.Tables(0).Rows(0)("ServiceTaxPer").ToString()
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sql As String = ""
        sql &= " INSERT INTO [APEX_JobInvoice]"
        sql &= "  ([RefJobCardID]"
        sql &= "  ,[RefKestoneMasterID]"
        sql &= "  ,[InvoiceNo]"
        sql &= "  ,[InvoiceDate]"
        sql &= "   ,[SupplierReference]"
        sql &= "  ,[OtherReference]"
        sql &= "  ,[BuyerOrderNo]"
        sql &= "  ,[BuyerDate]"
        sql &= ",InsertedBy"
        sql &= ",ServiceTaxCategory)"
        sql &= " VALUES"
        sql &= " (" & hdnJobCardID.Value
        sql &= ",1"
        sql &= ",'" & Clean(txtInvoiceNo.Text) & "'"
        sql &= ",convert(datetime,'" & txtDated.Text & "',105)"
        sql &= " ,'" & Clean(txtSupplierRef.Text) & "'"
        sql &= " ,'" & Clean(txtOtherRef.Text) & "'"
        sql &= " ,'" & Clean(txtBuyer.Text) & "'"
        sql &= " ,convert(datetime,'" & txtBuyerDate.Text & "',105)"
        sql &= " ," & getLoggedUserID()
        sql &= "  ,'" & lblSTaxCategory.Text & "')"
        If ExecuteNonQuery(sql) > 0 Then
            Dim capex As New clsApex
            capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())

            Response.Redirect("Home.aspx")
        End If
    End Sub

    Private Sub CloseAllControls()
        lblBuyer.Visible = True
        lblBuyerDated.Visible = True
        lblInvoiceNo.Visible = True
        lblDated.Visible = True
        lblSupplierReference.Visible = True
        lblOtherReference.Visible = True

        txtBuyer.Visible = False
        txtBuyerDate.Visible = False
        txtInvoiceNo.Visible = False
        txtDated.Visible = False
        txtSupplierRef.Visible = False
        txtOtherRef.Visible = False
    End Sub

    Private Sub FillData()
        Dim sql As String = "Select convert(varchar(10),BuyerDate,105) as NBuyerDate,convert(varchar(10),InvoiceDate,105) as NInvoiceDate,* from APEX_JobInvoice where IsActive='Y' and IsDeleted='N' and RefJobCardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            sql &= "  ,[BuyerDate]"
            txtBuyer.Text = ds.Tables(0).Rows(0)("BuyerOrderNo").ToString()
            txtInvoiceNo.Text = ds.Tables(0).Rows(0)("InvoiceNo").ToString()
            txtDated.Text = ds.Tables(0).Rows(0)("NInvoiceDate").ToString()
            txtSupplierRef.Text = ds.Tables(0).Rows(0)("SupplierReference").ToString()
            txtOtherRef.Text = ds.Tables(0).Rows(0)("OtherReference").ToString()
            txtBuyerDate.Text = ds.Tables(0).Rows(0)("NBuyerDate").ToString()

            lblBuyer.Text = ds.Tables(0).Rows(0)("BuyerOrderNo").ToString()
            lblInvoiceNo.Text = ds.Tables(0).Rows(0)("InvoiceNo").ToString()
            lblDated.Text = ds.Tables(0).Rows(0)("NInvoiceDate").ToString()
            lblSupplierReference.Text = ds.Tables(0).Rows(0)("SupplierReference").ToString()
            lblOtherReference.Text = ds.Tables(0).Rows(0)("OtherReference").ToString()
            lblBuyerDated.Text = ds.Tables(0).Rows(0)("NBuyerDate").ToString()
        End If
    End Sub

    Private Function checkInvoice() As Boolean
        Dim result As Boolean = False
        Dim sql As String = "Select * from APEX_JobInvoice where RefJobCardID=" & hdnJobCardID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = True
        End If
        Return result
    End Function
End Class
