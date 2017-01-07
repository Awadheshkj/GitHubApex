﻿<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="VendorContact.aspx.vb" Inherits="Masters_VendorContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "VendorContactManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">
    <div class="subMenuStrip"></div>
    <h1>Vendor Contact Details</h1>
     <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">

                    
            <asp:Button ID="btnAdd" runat="server" Text="Add New Vendor Contact" class="btn btn-primary pull-right" PostBackUrl="VendorContactManager.aspx?mode=add" /></div>
                </div>
            </div>

        <div>
            <asp:GridView ID="gvVendorContact" runat="server" AutoGenerateColumns="false"  CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Row">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Row")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <asp:HiddenField ID="hdnVendorContactID" runat="server" Value='<%#Bind("VendorContactID")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <%# DataBinder.Eval(Container.DataItem, "VendorName")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contact Person">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactName")%></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactDesignation")%></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactDepartment")%></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="EmailID">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactOfficialEmailID")%></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Mobile")%></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
   
</asp:Content>