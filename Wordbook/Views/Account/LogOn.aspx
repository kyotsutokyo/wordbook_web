<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Wordbook.Models.LogOnModel>" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    ログオン
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>ログオン</h2>
    <p>
        ユーザー名とパスワードを入力してください。アカウントがない場合は、 <%= Html.ActionLink("登録", "Register") %> してください。
    </p>

    <% using (Html.BeginForm()) { %>
        <%= Html.ValidationSummary(true, "ログインに失敗しました。エラーを修正し、再試行してください。") %>
        <div>
            <fieldset>
                <legend>アカウント情報</legend>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.CheckBoxFor(m => m.RememberMe) %>
                    <%= Html.LabelFor(m => m.RememberMe) %>
                </div>
                
                <p>
                    <input type="submit" value="ログオン" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
