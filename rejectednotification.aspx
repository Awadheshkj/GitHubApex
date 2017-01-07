<%@ Page Title="" Language="VB" MasterPageFile="~/Apex.master"  AutoEventWireup="false" CodeFile="rejectednotification.aspx.vb" Inherits="rejectednotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"  runat="Server">
    <div style="height: 20px;">
    </div>
    <div id="divError" runat="server" class="ui-state-error ui-corner-all divError">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>Warning: </strong>
            <asp:Label ID="lblError" runat="server" Text="Your Estimate has been rejected"></asp:Label>
        </p>
    </div>
    <div style="height: 20px;">
    </div>
    <div class="col-12">
        <asp:GridView runat="server" ID="gvDisplay" AutoGenerateColumns="false" Width="90%" AllowPaging="true" PageSize="15" ShowFooter="true" CssClass="table table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        <asp:HiddenField runat="server" ID="hdnEstimateID" Value='<%# Bind("EstimateID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Category")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Particulars">
                    <ItemTemplate>
                        <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Particulars")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit Price">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Days">
                    <ItemTemplate>
                        <asp:Label ID="lblDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Days")%>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Estimate">
                    <ItemTemplate>
                        <asp:Label ID="lblEstimate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Estimate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="form-group col-12">
            <span class="col-2">

                <asp:Label runat="server" ID="lbl1">Sub Total :</asp:Label></span>
            <span class="col-6">
                <asp:Label runat="server" ID="lblSubTotal"></asp:Label></span>
        </div>
        <div class="form-group col-12">
            <span class="col-2">Management Fees :(<asp:TextBox ID="txtMFeePer" Visible="false" runat="server" CssClass="form-control input-small" TabIndex="8" autocomplete="off" onkeyup="javascript:CalTotalMFeePer();"></asp:TextBox>
                <asp:Label ID="lblMFeePer" runat="server"></asp:Label>%)</span>
            <span class="col-6">
                <asp:Label ID="lblMangnmtFees" runat="server"></asp:Label>
                <asp:TextBox Visible="false" runat="server" ID="txtMangnmtFees" onkeyup="javascript:CalTotal();" MaxLength="10" CssClass="form-control" autocomplete="off"></asp:TextBox>

            </span>
        </div>
        <div class="form-group col-12">
            <span class="col-2">
                <asp:Label runat="server" ID="Label2">Total :</asp:Label></span>
            <span class="col-6">
                <asp:Label runat="server" ID="lblTotal"></asp:Label></span>
        </div>
        <div class="form-group col-12">
            <span class="col-2">Service Tax / VAT : (<asp:TextBox Visible="false" ID="txtServiceTax" runat="server" TabIndex="9" CssClass="form-control input-small" onkeyup="CalTotal1();" autocomplete="off"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtServiceTax" ForeColor="red"></asp:RequiredFieldValidator><asp:Label ID="lblServiceTaxPer" runat="server" SetFocusOnError="true"></asp:Label>
                %) </span>
            <span>
                <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
            </span>
        </div>
        <div class="form-group col-12">
            <span class="col-2">
                <asp:Label runat="server" ID="Label4">Grand Total :</asp:Label>
            </span>
            <span class="col-6">
                <asp:Label runat="server" ID="lblGrandTotal"></asp:Label></span>

        </div>

        <div class="col-12" style="text-align: center;">
            <asp:Button ID="btnback" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" TabIndex="23" />
            <asp:Button ID="btnCancelNotification" runat="server" Text="Remove Notification" CausesValidation="false" CssClass="btn btn-primary" TabIndex="22" />
            <asp:Button ID="btnEditEstimation" runat="server" Text="Edit Estimate" CssClass="btn btn-primary" TabIndex="24" />

            <asp:HiddenField runat="server" ID="hdnnotificationID" />
            <asp:HiddenField runat="server" ID="hdnBriefID" />
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
</asp:Content>

