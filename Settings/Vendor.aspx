<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="Vendor.aspx.vb" Inherits="Masters_Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "VendorManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>Vendor Details</h1>

    <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">

                    <asp:Button ID="btnAdd" runat="server" Text="Add New Vendor" class="btn btn-primary pull-right" PostBackUrl="VendorManager.aspx?mode=add" />
                </div>
            </div>
        </div>

        <div>
            <asp:GridView ID="gvVendor" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Row">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Row")%>
                        </ItemTemplate>
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <asp:HiddenField ID="hdnVendorID" runat="server" Value='<%#Bind("VendorID")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Vendor">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <%# DataBinder.Eval(Container.DataItem, "VendorName")%>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Is Active">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>

