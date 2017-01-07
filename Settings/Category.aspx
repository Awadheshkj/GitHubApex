<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="Category.aspx.vb" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "CategoryManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">
    <h1>Category Manager</h1>
    <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">


                    <asp:Button ID="btnAdd" runat="server" Text="Add New Category" CssClass="btn btn-primary pull-right" PostBackUrl="CategoryManager.aspx?mode=add" />
                </div>
            </div>
        </div>
    </div>


    <div class="">
        <div class="GridDiv">
            <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Row">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Row")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                            <asp:HiddenField ID="hdnCategoryTypeID" runat="server" Value='<%#Bind("CategoryTypeID")%>' />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "CategoryType")%></ItemTemplate>
                    </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="Allow profit(%)">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Allowprofit")%></ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Is Active">
                        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IsActive")%></ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_footer" runat="Server">
</asp:Content>

