<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GAFWEB.Login" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
      <meta charset="UTF-8">
     <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"> 
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
   
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   
<%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">--%>
<link rel="stylesheet" href="Content/css/font-awesome/font-awesome.css">
<link href="Content/css/maxcdn.bootstrap.min.css" rel="stylesheet">
<link href="Content/css/netdna.bootstrap.min.css" rel="stylesheet" type="text/css">

  
          <!--jQuery-->
    <script src="Content/js/jquery-3.3.1.js"></script>
        <script src="Content/js/bootstrapmaxcdn.min.js"></script>  
  

<style>
    
   body { 
  background: url(Content/images/footer-bg.png) no-repeat center center fixed; 
  -webkit-background-size: cover;
  -moz-background-size: cover;
  -o-background-size: cover;
  background-size: cover;
}

.panel-default {
opacity: 0.9;
width: 400px;
    min-width: 50px;
}
.form-group.last { margin-bottom:0px;     }
  .minh-100 {
  height: 100vh;
}
 
  #centrador{
  
  min-height:50px;
  /*background-color: red;*/
  padding: 0px;
  height:70px;
}
   #centrador2{
  
  min-height:50px;
  /*background-color: red;*/
  padding: 0px;
  height:70px;
}

#imagen{
    /*position: absolute;
    width: 100px;*/
    top: 0;
    left: 0;
    right: 10px;
    bottom: 0;
    margin:auto;
    
}
.break-word {
 -ms-word-break: break-all;
     word-break: break-all;

     // Non standard for webkit
     word-break: break-word;

-webkit-hyphens: auto;
   -moz-hyphens: auto;
    -ms-hyphens: auto;
        hyphens: auto;
}

</style>
  
    <title>Portal Promotor</title>
    <link rel="shortcut icon" href="Content/images/spreadsheet.ico" type="image/x-icon" >
</head>
<body >

     <!-- Loader Start -->
    <div class="css-loader">
        <div class="loader-inner line-scale d-flex align-items-center justify-content-center">
           
        </div>
    </div>


       <%-- <div class="container">--%>
                 <form class="form-horizontal" role="form" runat="server">
          <asp:ToolkitScriptManager AsyncPostBackTimeout="120" runat="server" ID="smGlobal"  EnablePartialRendering="True" />
   

    <div class="row justify-content-center align-items-center minh-100">
               <div class="panel panel-default" style=" background-color:#4285F4; ">
                <div class="panel-heading text-center" style="font-size:25px; color:white; background-color:#4285F4;  background-image: linear-gradient(#271cbe, #4285F4)" >
                    <%--<span class="glyphicon glyphicon-lock"></span>--%> Portal Promotor</div>
                <div class="panel-body" style="opacity: 0.77;">
                        <div class="form-group">
                        <div class="text-center">
                              &nbsp;<asp:label id="Label1" runat="server" style="background-color:#4285F4;font-size:25px;color:white;" Text="Bienvenido"/>                   
                            </div>
                        </div>

                    
                    <div class="form-group">
                        <div class="input-group margin-bottom-sm col-lg-12">
                             <span class="input-group-addon"><i class="fa fa-users fa-fw"  aria-hidden="true"></i></span>
                             <asp:TextBox ID="txtEmail" runat="server" type="text" class="form-control" placeholder="Usuario" required= "True"></asp:TextBox>
                         
                        </div>
                        </div>
                        
                        <div class="form-group">
                             <div class="input-group col-lg-12">
                             <span class="input-group-addon"><i class="fa fa-key fa-fw" aria-hidden="true"></i></span>
                              <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control" placeholder="Password" required= "True"></asp:TextBox>
                       
                         </div>
                    </div>

                    <div class="row">
                           <div class="col-lg-1">
                      </div>
                          <div class="col-lg-10">
                            <%--<div class="checkbox">
                                <label>
                                    <input type="checkbox"/>
                                    Remember me
                                </label>
                            </div>--%>
                               <asp:label id="lblmsg" runat="server" style="color: #ff0000"/>
                         
                        </div>
                              <div class="col-lg-1">
                      </div>
                           
                    </div>
                    <div class="form-group last" style="margin-top:10px;">
                        <div class="col-lg-12 text-center">
                             <asp:Button ID="btnLogin" runat="server" class="btn btn-default btn-sm" Text="Ingresar" OnClick="btnLogin_Click"/>
                        
                      
                           
                        </div>
                    </div>
                </div>
                <div class="panel-footer" style=" background-color:#4285F4; color:white;  background-image: linear-gradient(#271cbe, #4285F4)">
                  <div class="row">
                      <div class="col">
      
                          <a data-toggle="modal"  style="color:white;" data-target="#myModal">Introduzca sus datos</a>
                     
                    </div>
                      </div>
                  </div>
            </div>
        </div>
           

                    
                </form>
        
</body>
</html>