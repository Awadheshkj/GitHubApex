<%@ Page Title="" Language="VB" MasterPageFile="settingmaster.master" AutoEventWireup="false" CodeFile="Subcategory.aspx.vb" Inherits="Masters_Subcategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="Server">
    <script lang="javascript">
        function call(id) {
            location.href = "SubcategoryManager.aspx?mode=edit&lid=" + id;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">

    <div class="subMenuStrip"></div>
    <h1>Subcategory Details</h1>
   <div class="span8">
        <div class="text-right">
            <div class="control-group">
                <div class="btn-group">

                   
                <asp:Button ID="btnAdd" runat="server" Text="Add New Subcategory"  class="btn btn-primary pull-right" PostBackUrl="SubcategoryManager.aspx?mode=add" />
            </div>
          
        </div>
    </div>
         <div >
                <asp:GridView ID="gvSubcategory" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered" Width="100%" AllowPaging="true" PageSize="15">
                    <Columns>
                        <asp:TemplateField HeaderText="Row">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Row")%>
                                  <asp:HiddenField ID="hdnSubCategoryTypeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SubCategoryTypeID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <%-- <%# DataBinder.Eval(Container.DataItem, "Client")%>--%>
                                <%# DataBinder.Eval(Container.DataItem, "CategoryType")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subcategory">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "SubCategoryType")%></ItemTemplate>
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

