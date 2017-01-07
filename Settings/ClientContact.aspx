<%@ Page Title="" Language="VB" MasterPageFile="~/KestoneMaster.master" AutoEventWireup="false" validateRequest="false" CodeFile="ClientContact.aspx.vb" Inherits="Masters_ClientContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple"
        });
    </script>
    <script lang="javascript">
        function call(id) {
            location.href = "ClientContactManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpContent" runat="Server">
    <h3>Client Contact Details</h3>
    <hr />

     <div class="alert alert-danger" id="divError" runat="server">
          <strong>Warning: </strong>
        <asp:Label ID="lblError" runat="server"></asp:Label>

     </div>
     <div class="span8">
     
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">
                 <asp:Button ID="btnAdd" runat="server" Text="Add New Client Contact" class="btn btn-primary pull-right" PostBackUrl="ClientContactManager.aspx?mode=add" />
                    
                </div>
            </div>
        </div>
        
               
            <div class="GridDiv">
                <asp:GridView ID="gvClientContact" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Row">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <asp:HiddenField ID="hdnContactID" runat="server" Value='<%#Bind("ContactID")%>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Client">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <%# DataBinder.Eval(Container.DataItem, "Client")%>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Contact Name">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactName")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contact Designation">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactDesignation")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contact Department">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactDepartment")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Official EmailID">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ContactOfficialEmailID")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobile No.">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Mobile")%></ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Is Active">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpFooter" runat="Server">
   
</asp:Content>

