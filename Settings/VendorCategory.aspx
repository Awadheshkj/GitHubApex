<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="VendorCategory.aspx.vb" Inherits="VendorCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "VendorCategoryManager.aspx?mode=edit&lid=" + id;
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
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Category" class="btn btn-primary pull-right" PostBackUrl="VendorCategoryManager.aspx?mode=add" />
                </div>
            </div>
           
        </div>
         <div>
                <asp:GridView ID="gvVendor" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Row">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <asp:HiddenField ID="hdnVendorID" runat="server" Value='<%#Bind("VendorcategoryID")%>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vendor">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <%# DataBinder.Eval(Container.DataItem, "VendorCategory")%>
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

