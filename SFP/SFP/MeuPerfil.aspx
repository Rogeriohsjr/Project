<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true"
    CodeBehind="MeuPerfil.aspx.cs" Inherits="SFP.MeuPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td class="Titulo">
                Meu Perfil
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="up_Perfil">
                    <ProgressTemplate>
                        Aguarde...<img alt="Aguarde" src="App_Themes/Green/Image/Loading.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Panel ID="pnMsgPrincipal" runat="server" Visible="false" >
                    <asp:Label ID="lbMsgPrincipal" runat="server" Text="" ForeColor="Red" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="up_Perfil" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <fieldset>
                            <legend>Dados do usuário</legend>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        Nome:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNome" runat="server" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Login:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLogin" runat="server" Width="200px" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        E-mail:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Senha:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSenha" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btGravar" runat="server" Text="Gravar" Width="100px" CssClass="BotaoPesquisar"
                                            OnClick="btGravar_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="vertical-align: text-top;">
                        <table style="width: 100%;">
                            <tr>
                                <td class="pnTituloPesquisa" align="center">
                                    <fieldset>
                                        <legend>Últimos 5 acessos</legend>
                                        <asp:GridView ID="gvAcesso" runat="server" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                                <asp:BoundField DataField="Date" HeaderText="Data" />
                                                <asp:BoundField DataField="Ip" HeaderText="Ip" />
                                            </Columns>
                                            <RowStyle CssClass="gvRowStyle" />
                                            <HeaderStyle CssClass="gvHeaderStyle" />
                                        </asp:GridView>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btGravar"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
