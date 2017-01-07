<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="Designation.aspx.vb" Inherits="Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "DesignationManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">
    
        <div class="subMenuStrip"></div>
        <h1>Designation Details</h1>

        <div class="span8">
            <div class="text-right">
                <div class="control-group">
                    <div class="btn-group">


                        <asp:Button ID="btnAdd" runat="server" Text="Add New Designation" class="btn btn-primary pull-right" PostBackUrl="DesignationManager.aspx?mode=add" />
                    </div>
                   
                </div>
            </div>
             <div>
                        <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Row">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                        <asp:HiddenField ID="hdnDesignationID" runat="server" Value='<%#Bind("DesignationID")%>' />

                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Designation")%></ItemTemplate>
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

