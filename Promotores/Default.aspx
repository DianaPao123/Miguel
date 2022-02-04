<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GAFWEB._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
             <!-- Stylesheets & Fonts -->
   
    
    <link rel="stylesheet" href="Content/css/aos/aos.css" /><%--formato default --%>
    <link rel="stylesheet" href="Content/css/swiper/swiper.css" /><%--formato default -- %>
   <%-- <link rel="stylesheet" href="Content/css/lightgallery.min.css">--%>
 
           
     <!-- Hero Start -->
    <section class="hero">
        <div class="container" >
            <div class="row justify-content-center" >
                <div >
                    <div class="swiper-container hero-slider"  style="width: 615px;"><%--aqui se le da para el ancho de los datos --%>
                        <div class="swiper-wrapper">
                            <div class="swiper-slide slide-content d-flex align-items-center">
                                <div class="single-slide" >
                                    <h2 data-aos="fade-right" data-aos-delay="300">Bienvenido<br>
                                    </h2>
                                    <p data-aos="fade-right" data-aos-delay="400">Se les da la más cordial bienvenida a su nueva versión 1.0 
                                        del sistema de generación de CFDI’s<br />

                                                <asp:LinkButton ID="LinkButton1" runat="server">CFDI3.3</asp:LinkButton>     
                             
                                    </p>
   
<%--                                    <a data-aos="fade-right" data-aos-delay="500" href="#" class="btn btn-outline-primary">Facnumus
                                        </a>
                    --%>            </div>
                            </div>
                        
                        </div>

                    </div>
                </div>
            </div>
            <!-- Add Control -->
            <span class="arr-left"><i class="fa fa-angle-left"></i></span>
            <span class="arr-right"><i class="fa fa-angle-right"></i></span>
        </div>
        <div class="texture"></div>
        <div class="diag-bg"></div>
    </section>
    <!-- Hero End -->



</asp:Content>
