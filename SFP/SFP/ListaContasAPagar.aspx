<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true"
    CodeBehind="ListaContasAPagar.aspx.cs" Inherits="SFP.ListaContasAPagar" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress" AssociatedUpdatePanelID="up_General" DisplayAfter="1"
        runat="server">
        <ProgressTemplate>
            <table class="inProgress">
                <tr>
                    <td>
                        <img alt="Aguarde" src="App_Themes/Green/Image/Loading.gif" style="display: inline" />
                    </td>
                    <td>
                        Aguarde...
                    </td>
                </tr>
            </table>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="up_General" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="Titulo">
                        Contas a Pagar
                    </td>
                </tr>
                <tr>
                    <td>
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
                            Categoria:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="200px" />
                        </td>
                        <td>
                            Descrição:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescricao" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="up_btPesquisar" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btPesquisar" runat="server" Text="Pesquisar" CssClass="BotaoPesquisar"
                                        OnClick="btPesquisar_Onclick" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Vencimento:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataDe" runat="server" MaxLength="10" />
                            <ajax:CalendarExtender ID="txCalendarDe" runat="server" TargetControlID="txtDataDe"
                                Format="dd/MM/yyyy">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            Até:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataAte" runat="server" MaxLength="10" />
                            <ajax:CalendarExtender ID="txCalendarAte" runat="server" TargetControlID="txtDataAte"
                                Format="dd/MM/yyyy">
                            </ajax:CalendarExtender>
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
                                    <asp:Button ID="btPagar" runat="server" Text="Pagar" CssClass="BotaoPesquisar" OnClick="btPagar_OnClick" />
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
                                    <asp:GridView ID="gvListaConta" runat="server" AutoGenerateColumns="false" Width="100%"
                                        AllowSorting="true" OnRowDataBound="gvListaConta_RowDataBound" OnRowCreated="gvListaConta_OnRowCreated"
                                        OnSorting="gvListaConta_Sorting">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="cbContaSelectDeselectAll" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbContaSelectedDeselect" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Id" DataField="Id" Visible="false"
                                                SortExpression="Id" />
                                            <asp:BoundField HeaderText="Categoria" DataField="CategoryDescription" SortExpression="CategoryDescription" />
                                            <asp:BoundField HeaderText="Descrição" DataField="Description" SortExpression="Description" />
                                            <asp:BoundField HeaderText="Dt. Vencimento" DataField="MaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                                ItemStyle-HorizontalAlign="Center" SortExpression="MaturityDate" />
                                            <asp:BoundField HeaderText="Valor Total" DataField="TotalPrice" DataFormatString="{0:C}"
                                                ItemStyle-HorizontalAlign="Center" SortExpression="TotalPrice" />
                                            <asp:BoundField HeaderText="Dt. Pagamento" DataField="DatePayment" DataFormatString="{0:dd/MM/yyyy}"
                                                ItemStyle-HorizontalAlign="Center" SortExpression="DatePayment" />
                                            <asp:BoundField HeaderText="Valor Pago" DataField="PricePayment" DataFormatString="{0:C}"
                                                ItemStyle-HorizontalAlign="Center" SortExpression="PricePayment" />
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
            <asp:Panel ID="pnPopupContas" runat="server" DefaultButton="pbtGravar">
                <asp:UpdatePanel ID="upPopupContas" runat="server">
                    <ContentTemplate>
                        <table width="400px" style="background: white;">
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td class="TituloPesquisa">
                                                <div style="width: 49%; text-align: left; float: left;">
                                                    <asp:Label ID="lbTitulo" runat="server" Text="Nova Conta" />
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
                                                Categoria<br />
                                                <asp:DropDownList ID="pddlCategoria" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Descrição<br />
                                                <asp:TextBox ID="ptxtDescricao" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Data Vencimento<br />
                                                <asp:TextBox ID="ptxtDataVencimento" runat="server" MaxLength="10" Width="150px" />
                                                <ajax:CalendarExtender ID="ptxCalendarVencimento" runat="server" TargetControlID="ptxtDataVencimento"
                                                    Format="dd/MM/yyyy">
                                                </ajax:CalendarExtender>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                Valor Total<br />
                                                <asp:TextBox ID="ptxtValorTotal" runat="server" Width="150px" />
                                                        </td>
                                                        <td id="tdJaPago" runat="server" >
                                                            Já Pago
                                                            <input type="checkbox" id="ckPago" name="ckPago" onchange="setPayField(this)" />
                                                            <script type="text/javascript">
                                                                function setPayField(e) {
                                                                    if (e.checked) {
                                                                        $("#ContentPlaceHolder1_ptxtValorPago").val($("#ContentPlaceHolder1_ptxtValorTotal").val());
                                                                        $("#ContentPlaceHolder1_ptxtDataPagamento").val($("#ContentPlaceHolder1_ptxtDataVencimento").val());
                                                                    }
                                                                    else {
                                                                        $("#ContentPlaceHolder1_ptxtValorPago").val("0");
                                                                        $("#ContentPlaceHolder1_ptxtDataPagamento").val("");
                                                                    }
                                                                }
                                                            </script>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Data Pagamento<br />
                                                <asp:TextBox ID="ptxtDataPagamento" runat="server" MaxLength="10" Width="150px" />
                                                <ajax:CalendarExtender ID="ptxCalendarPagamento" runat="server" TargetControlID="ptxtDataPagamento"
                                                    Format="dd/MM/yyyy">
                                                </ajax:CalendarExtender>
                                            </td>
                                            <td>
                                                Valor Pago<br />
                                                <asp:TextBox ID="ptxtValorPago" runat="server" Width="150px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Documento<br />
                                                <asp:TextBox ID="ptxtDocumento" runat="server" Width="150px" />
                                            </td>
                                            <td>
                                                Quantidade de Parcela<br />
                                                <asp:TextBox ID="ptxtQtdParcela" runat="server" Width="150px" Text="1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Observação<br />
                                                <asp:TextBox ID="ptxtHistorico" runat="server" Width="95%" TextMode="MultiLine" Rows="2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TituloPesquisa" colspan="2" style="text-align: center;">
                                                <asp:HiddenField ID="hfIdContaSelecionada" runat="server" Value="0" />
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
            <ajax:ModalPopupExtender ID="popup_GestaoDeContas" runat="server" TargetControlID="hfHiddenModal"
                PopupControlID="pnPopupContas" BackgroundCssClass="ModalBackground" BehaviorID="upPopupContas">
            </ajax:ModalPopupExtender>
            <asp:HiddenField ID="hfHiddenModal" runat="server" />
            <asp:Panel ID="pnTituloResumo" runat="server">
                <table width="100%">
                    <tr>
                        <td class="TituloPesquisa">
                            <div style="width: 49%; text-align: left; float: left;">
                                Resumo</div>
                            <div style="width: 49%; text-align: right; float: right;">
                                <asp:Image ID="imgCollapse3" runat="server" ImageUrl="App_Themes/Green/Image/cpUpDoubleReverse.png" /></div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:UpdatePanel ID="upCaixaResumo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnCaixaResumo" runat="server">
                        <table>
                            <tr>
                                <td style="vertical-align: text-top;">
                                    <asp:GridView ID="gvTopTotal" runat="server" AutoGenerateColumns="false" Width="250px">
                                        <Columns>
                                            <asp:BoundField DataField="Resumo" HeaderText="Resumo" />
                                            <asp:BoundField DataField="TotalPrice" HeaderText="Valor Total" DataFormatString="{0:c}"
                                                ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                        <HeaderStyle CssClass="gvHeaderStyle" />
                                    </asp:GridView>
                                </td>
                                <td style="vertical-align: text-top;">
                                    <asp:GridView ID="gvTopCategoria" runat="server" AutoGenerateColumns="false" Width="250px">
                                        <Columns>
                                            <asp:BoundField DataField="Category" HeaderText="Categoria" />
                                            <asp:BoundField DataField="TotalPrice" HeaderText="Valor Total" DataFormatString="{0:c}"
                                                ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                        <HeaderStyle CssClass="gvHeaderStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajax:CollapsiblePanelExtender ID="CollapsiblePanelCaixaResumo" runat="Server" TargetControlID="pnCaixaResumo"
                        ExpandControlID="pnTituloResumo" CollapseControlID="pnTituloResumo" Collapsed="true"
                        TextLabelID="Label1" ExpandedText="" CollapsedText="" ImageControlID="imgCollapse3"
                        ExpandedImage="App_Themes/Green/Image/cpUpDoubleReverse.png" CollapsedImage="App_Themes/Green/Image/cpDownDoubleReverse.png"
                        SuppressPostBack="true" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
