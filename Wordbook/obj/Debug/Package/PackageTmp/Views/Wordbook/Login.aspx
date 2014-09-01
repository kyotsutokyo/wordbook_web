<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Kyotsu Wordbook Login Demo</title>
    <link href="<%=ResolveClientUrl("~/Content/bootstrap.min.css")%>"rel="stylesheet">
    <link href="<%=ResolveClientUrl("~/Content/ladda-themeless.min.css")%>" rel="stylesheet">
</head>

<body>
<div class="page-header">
    </div>
<div class="container">
    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            <div class="login-panel panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">単語帳会員ログイン</h3>
                </div>
                <div class="panel-body">
                    <form role="form">
                        <fieldset>
                            <div class="form-group">
                                <input class="form-control" placeholder="メールアドレス" name="email" type="email" id="email" autofocus>
                            </div>
                            <div class="form-group">
                                <input class="form-control" placeholder="パスワード" name="password" type="password" id="password" value="">
                            </div>
                           
                            <!--<div class="checkbox">
                                <label>
                                    <input name="remember" type="checkbox" value="Remember Me">ログインしたままにする
                                </label>
                            </div>
                           -->
                            <!-- Change this to a button or input when using this as a form -->
                            <div class="form-group">
                            <a class="btn btn-lg btn-success btn-block  ladda-button " data-style="zoom-in" data-size="l" id="login"><span class="ladda-label"> ログイン </span></a>
                            </div>
                            <div class="form-group">
                            <div class="row">
                                <a class="btn btn-link pull-left" data-style="zoom-in" data-size="l" id="A1"><span class="ladda-label"> ログインできない </span></a>
                                <a class="btn btn-link pull-right" data-style="zoom-in" data-size="l" id="register"><span class="ladda-label"> 新規取得 </span></a>
                            </div>
                            </div>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Core Scripts - Include with every page -->
<script src="<%=ResolveClientUrl("~/Scripts/jquery-1.11.1.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/bootstrap.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Thrift/thrift.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Thrift/wordbook_types.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Thrift/WordbookThriftService.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/spin.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/ladda.min.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Global.js")%>"></script>
<script src="<%=ResolveClientUrl("~/Scripts/Login.js")%>"></script>
</body>
</html>
