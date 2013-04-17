<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true"
    CodeBehind="ListaCategoria.aspx.cs" Inherits="SFP.ListaCategoria" %>
    <%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td class="Titulo">
                Categoria
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdateProgress ID="UpdateProgress" AssociatedUpdatePanelID="up_btPesquisar"
                    runat="server">
                    <ProgressTemplate>
                        Aguarde...<img alt="Aguarde" src="App_Themes/Green/Image/Loading.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel ID="pnMsgPrincipal" runat="server" Visible="false">
                    <asp:Label ID="lbMsgPrincipal" runat="server" Text="" ForeColor="Red" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnTituloPesquisa" runat="server">
        <table width="100%">
            <tr>
                <td class="TituloPesquisa">
                    <div style="width: 49%; text-align: left; float: left;">
                        Pesquisa</div>
                    <div style="width: 49%; text-align: right; float: right;">
                        <asp:Image ID="imgCollapse1" runat="server" ImageUrl="App_Themes/Green/Image/cpDownDoubleReverse.png" /></div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnCaixaPesquisa" runat="server" DefaultButton="btPesquisar">
        <table class="CaixaPesquisa" width="100%">
            <tr>
                <td>
                    Descrição:
                </td>
                <td>
                    <asp:TextBox ID="txtDescricao" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="width:80%">
                    <asp:UpdatePanel ID="up_btPesquisar" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btPesquisar" runat="server" Text="Pesquisar" CssClass="BotaoPesquisar"
                                OnClick="btPesquisar_Onclick" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajax:CollapsiblePanelExtender ID="CollapsePanelSearch" runat="Server" TargetControlID="pnCaixaPesquisa"
        ExpandControlID="pnTituloPesquisa" CollapseControlID="pnTituloPesquisa" Collapsed="true"
        TextLabelID="Label1" ExpandedText="" CollapsedText="" ImageControlID="imgCollapse1"
        ExpandedImage="App_Themes/Green/Image/cpUpDoubleReverse.png" CollapsedImage="App_Themes/Green/Image/cpDownDoubleReverse.png"
        SuppressPostBack="true" />
    <asp:Panel ID="pnTituloDetalhes" runat="server">
        <table width="100%">
            <tr>
                <td class="TituloPesquisa">
                    <div style="width: 49%; text-align: left; float: left;">
                        Detalhes</div>
                    <div style="width: 49%; text-align: right; float: right;">
                        <asp:Image ID="imgCollapse2" runat="server" ImageUrl="App_Themes/Green/Image/cpDownDoubleReverse.png" /></div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnCaixaDetalhes" runat="server" DefaultButton="btPesquisar">
        <asp:UpdatePanel ID="upBotoesGestao" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btNovo" runat="server" Text="Novo" CssClass="BotaoPesquisar" OnClick="btNovo_OnClick" />
                        </td>
                        <td>
                            <asp:Button ID="btAlterar" runat="server" Text="Aterar" CssClass="BotaoPesquisar"
                                OnClick="btAlterar_OnClick" />
                        </td>
                        <td>
                            <asp:Button ID="btExcluir" runat="server" Text="Excluir" CssClass="BotaoPesquisar"
                                OnClick="btExcluir_OnClick" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvCategoria" runat="server" AutoGenerateColumns="false" Width="100%"
                                OnRowDataBound="gvCategoria_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbCategoriaSelectDeselectAll" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCategoriaSelectedDeselect" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="IdCategoria" DataField="Id" Visible="false" />
                                    <asp:BoundField HeaderText="Categoria" DataField="Description" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                </Columns>
                                <RowStyle CssClass="gvRowStyle" />
                                <HeaderStyle CssClass="gvHeaderStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajax:CollapsiblePanelExtender ID="cpePanel2" runat="Server" TargetControlID="pnCaixaDetalhes"
        ExpandControlID="pnTituloDetalhes" CollapseControlID="pnTituloDetalhes" Collapsed="False"
        TextLabelID="Label1" ExpandedText="" CollapsedText="" ImageControlID="imgCollapse2"
        ExpandedImage="App_Themes/Green/Image/cpUpDoubleReverse.png" CollapsedImage="App_Themes/Green/Image/cpDownDoubleReverse.png"
        SuppressPostBack="true" />
   <asp:Panel ID="pnPopupCategoria" runat="server">
        <asp:UpdatePanel ID="upPopupCategoria" runat="server">
            <ContentTemplate>
                <table width="400px" style="background: white;">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="TituloPesquisa">
                                        <div style="width: 49%; text-align: left; float: left;">
                                            <asp:Label ID="lbTitulo" runat="server" Text="Nova Categoria" />
                                        </div>
                                        <div style="width: 49%; text-align: right; float: right;">
                                            <asp:ImageButton ID="ibFechar" runat="server" OnClick="btFechar_OnClick" ImageUrl="App_Themes/Green/Image/close.png" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnMsgPopup" runat="server" Visible="false">
                                            <asp:Label ID="lbMsgPoup" runat="server" Text="" ForeColor="Red" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Descrição<br />
                                        <asp:TextBox ID="ptxtDescricao" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Status<br />
                                        <asp:DropDownList ID="pddlStatus" runat="server" Width="200px" >
                                            <asp:ListItem Text="Ativo" Value="0" Selected="True" ></asp:ListItem>
                                            <asp:ListItem Text="Bloqueado" Value="1" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TituloPesquisa" style="text-align: center;" colspan="2">
                                        <asp:HiddenField ID="hfIdCategoriaSelecionada" runat="server" Value="0" />
                                        <asp:Button ID="pbtGravar" runat="server" Text="Salvar" Width="100px" CssClass="BotaoPesquisar"
                                            OnClick="btGravar_OnClick" />
                                        &nbsp;
                                        <asp:Button ID="pbtFechar" runat="server" Text="Voltar" Width="100px" CssClass="BotaoPesquisar"
                                            OnClick="btFechar_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajax:ModalPopupExtender ID="popup_GestaoDeCategoria" runat="server" TargetControlID="hfHiddenModal"
        PopupControlID="pnPopupCategoria" BackgroundCssClass="ModalBackground" BehaviorID="upPopupCategoria">
    </ajax:ModalPopupExtender>
    <asp:HiddenField ID="hfHiddenModal" runat="server" />
</asp:Content>
