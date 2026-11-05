<%@ Page Title="" Language="C#" MasterPageFile="~/Store_Operations/Store_Operations.Master" AutoEventWireup="true" CodeBehind="Store_Operations_Dashboard.aspx.cs" Inherits="Markss_Retail_Suite.Store_Operations.Store_Operations_Dashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Store Operations | Store Operations Dashboard</title>
    <style type="text/css">
        body {
            background: #d0dbe1;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<span page-id="Store_Operations_Dashboard" id="find_page"></span>

<div class="toolbar py-5 pb-lg-5" id="kt_toolbar">
    <div id="kt_toolbar_container" class=" container-xxl  d-flex flex-stack flex-wrap">
        <div class="page-title d-flex flex-column me-3 flex-start">
            <h1 class="d-flex text-white m-0 fs-3">
                <asp:Label ID="lblMenuName" runat="server"></asp:Label>      
                <span class="h-20px border-gray-500 border-start mx-3"></span>
                <asp:Label ID="lblPageName" runat="server" Text="Store Operations Dashboard"></asp:Label>      
            </h1>
        </div>

        <div class="page-title d-flex flex-column me-3 flex-end">
            <%--<h1 class="d-flex text-white m-0 fs-3 text-uppercase">Markss Retail Suite</h1>--%>
        </div>
    </div>
</div>

<%--<div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
        <div class="content flex-row-fluid" id="kt_content">
          <div class="row">
              <div class="col-md-12 col-lg-4">
                  <div class="card mt-3  mb-2">
                    <div class="card-body">
                        <div class="scroll dragscroll scroll1" style="display:none">
                            <asp:GridView ID="grdWODetails" runat="server" AutoGenerateColumns = "false" class="display nowrap table table-hover table-striped table-bordered" cellspacing="0">
                                <Columns>
                                    <asp:BoundField DataField = "WOStatus" HeaderText = "Work Order Status" />
                                    <asp:BoundField DataField = "Count" HeaderText = "Count"  />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="wo_report"></div>
                    </div>
                  </div>
              </div>

              <div class="col-md-8">
                  <div class="mt-3 mb-2 row">
                      <div class="col-md-6 col-xl-6 col-md-6">
                          <div class="card update-card mb-7">
                              <div class="card-body">
                                  <div class="row align-items-end">
                                      <div class="col-8">
                                          <h1 class="text_count mb-3"><asp:Label ID="lblTotalWO" runat="server" Text="3"></asp:Label></h1>
                                          <h4 class="text-title m-b-0 text-uppercase">Total WO Received</h4>
                                      </div>
                                      <div class="col-4 text-end">
                                          <i class="fa fa-file fs-2 bg-c-yellow"></i>
                                      </div>
                                  </div>
                              </div>
                              <div class="card-footer" style="border-top:1px solid #fe9365">
                                  <div class="flex justify-content-between">
                                      <div>
                                          <p class="updated_on m-b-0">
                                              <i class="feather icon-clock text-gray-800 f-14 m-r-10"></i>Last Updated On : 12:10 pm
                                          </p>
                                      </div>
                                      <div>
                                          <p class=" text-theme-green updated_on m-b-0">
                                              <i class="fa fa-eye f-14 m-r-10"></i> View Details
                                          </p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="col-md-6 col-xl-6 col-md-6">
                          <div class="card update-card mb-7">
                              <div class="card-body">
                                  <div class="row align-items-end">
                                      <div class="col-8">
                                          <h1 class="text_count mb-3"><asp:Label ID="lblPendingWO" runat="server" Text="1"></asp:Label></h1>
                                          <h4 class="text-title m-b-0 text-uppercase">Pending WO</h4>
                                      </div>
                                      <div class="col-4 text-end">
                                          <i class="fa fa-question-circle fs-2 bg-c-green"></i>
                                      </div>
                                  </div>
                              </div>
                              <div class="card-footer" style="border-top:1px solid #0ac282">
                                  <div class="flex justify-content-between">
                                      <div>
                                          <p class="updated_on m-b-0">
                                              <i class="feather icon-clock text-gray-800 f-14 m-r-10"></i>Last Updated On : 12:10 pm
                                          </p>
                                      </div>
                                      <div>
                                          <p class=" text-theme-green updated_on m-b-0">
                                              <i class="fa fa-eye f-14 m-r-10"></i> View Details
                                          </p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="col-md-6 col-xl-6 col-md-6">
                          <div class="card update-card mb-7">
                              <div class="card-body">
                                  <div class="row align-items-end">
                                      <div class="col-8">
                                          <h1 class="text_count mb-3"><asp:Label ID="lblInTransitWO" runat="server" Text="1"></asp:Label></h1>
                                          <h4 class="text-title m-b-0 text-uppercase">IN - Transit WO</h4>
                                      </div>
                                      <div class="col-4 text-end">
                                          <i class="fa fa-exclamation-circle fs-2 bg-c-pink"></i>
                                      </div>
                                  </div>
                              </div>
                              <div class="card-footer" style="border-top:1px solid #fe9365">
                                  <div class="flex justify-content-between">
                                      <div>
                                          <p class="updated_on m-b-0">
                                              <i class="feather icon-clock text-gray-800 f-14 m-r-10"></i>Last Updated On : 12:10 pm
                                          </p>
                                      </div>
                                      <div>
                                          <p class=" text-theme-green updated_on m-b-0">
                                              <i class="fa fa-eye f-14 m-r-10"></i> View Details
                                          </p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="col-md-6 col-xl-6 col-md-6">
                          <div class="card update-card mb-7">
                              <div class="card-body">
                                  <div class="row align-items-end">
                                      <div class="col-8">
                                          <h1 class="text_count mb-3"><asp:Label ID="lblCompletedWO" runat="server" Text="1"></asp:Label></h1>
                                          <h4 class="text-title m-b-0 text-uppercase">Completed WO Received</h4>
                                      </div>
                                      <div class="col-4 text-end">
                                          <i class="fa fa-check-circle fs-2 bg-c-lite-green"></i>
                                      </div>
                                  </div>
                              </div>
                              <div class="card-footer" style="border-top:1px solid #01a9ac">
                                  <div class="flex justify-content-between">
                                      <div>
                                          <p class="updated_on m-b-0">
                                              <i class="feather icon-clock text-gray-800 f-14 m-r-10"></i>Last Updated On : 12:10 pm
                                          </p>
                                      </div>
                                      <div>
                                          <p class=" text-theme-green updated_on m-b-0">
                                              <i class="fa fa-eye f-14 m-r-10"></i> View Details
                                          </p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="col-md-6 col-xl-6 col-md-6">
                          <div class="card update-card mb-7">
                              <div class="card-body">
                                  <div class="row align-items-end">
                                      <div class="col-8">
                                          <h1 class="text_count mb-3"><asp:Label ID="lblSuppliers" runat="server" Text="10"></asp:Label></h1>
                                          <h4 class="text-title m-b-0 text-uppercase">Total Suppliers</h4>
                                      </div>
                                      <div class="col-4 text-end">
                                          <i class="fa fa-users fs-2 bg-c-red"></i>
                                      </div>
                                  </div>
                              </div>
                              <div class="card-footer" style="border-top:1px solid #f00">
                                  <div class="flex justify-content-between">
                                      <div>
                                          <p class="updated_on m-b-0">
                                              <i class="feather icon-clock text-gray-800 f-14 m-r-10"></i>Last Updated On : 12:10 pm
                                          </p>
                                      </div>
                                      <div>
                                          <p class=" text-theme-green updated_on m-b-0">
                                              <i class="fa fa-eye f-14 m-r-10"></i> View Details
                                          </p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>

                      <div class="col-md-6 col-xl-6 col-md-6">
                          <div class="card update-card mb-7">
                              <div class="card-body">
                                  <div class="row align-items-end">
                                      <div class="col-8">
                                          <h1 class="text_count mb-3"><asp:Label ID="lblItems" runat="server" Text="20"></asp:Label></h1>
                                          <h4 class="text-title m-b-0 text-uppercase">Total Items</h4>
                                      </div>
                                      <div class="col-4 text-end">
                                          <i class="fa fa-file-alt fs-2 bg-c-purple"></i>
                                      </div>
                                  </div>
                              </div>
                              <div class="card-footer" style="border-top:1px solid #430a47">
                                  <div class="flex justify-content-between">
                                      <div>
                                          <p class="updated_on m-b-0">
                                              <i class="feather icon-clock text-gray-800 f-14 m-r-10"></i>Last Updated On : 12:10 pm
                                          </p>
                                      </div>
                                      <div>
                                          <p class=" text-theme-green updated_on m-b-0">
                                              <i class="fa fa-eye f-14 m-r-10"></i> View Details
                                          </p>
                                      </div>
                                  </div>   
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </div>
           
        </div>
    </div>--%>

    <div id="kt_content_container" class="d-flex flex-column-fluid align-items-start  container-xxl">
    <div class="content flex-row-fluid" id="kt_content">
        <div class="card card-flush">
            <div class="card-body">
                <h3 class="text-center">STORE OPERATIONS DASHBOARD</h3>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript" src="../assets/chart_js/highcharts.js"></script>
<script type="text/javascript" src="../assets/chart_js/data.js"></script>
<script type="text/javascript" src="../assets/chart_js/exporting.js"></script>
<script type="text/javascript" src="../assets/chart_js/accessibility.js"></script>
<script type="text/javascript" src="../assets/chart_js/highcharts-3d.js"></script>
<script type="text/javascript" src="../assets/chart_js/jquery.easypiechart.min.js"></script>

<script type="text/javascript">
    
    function getRandomColor() {
        return '#' + Math.floor(Math.random() * 16777215).toString(16).padStart(6, '0');
    }

    function generateRandomColors(num) {
        const colors = [];
        for (let i = 0; i < num; i++) {
            colors.push(getRandomColor());
        }
        return colors;
    }

   <%-- Highcharts.chart('wo_report', {
        data: {
            table: '<%= grdWODetails.ClientID %>',
            startColumn: 0,
            endColumn: 1,
        },
        chart: {
            type: 'pie',
            panning: true,
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: '', 
            align: 'center'
        },
        subtitle: {
            text: 'WORK ORDER DETAILS',
            align: 'center'
        },
        legend: {
            itemWidth: 150,
            maxHeight: 50
        },
        colors: generateRandomColors(255), 
        plotOptions: {
            pie: {
                innerSize: 50,
                size: '80%',
                depth: 50,
                dataLabels: {
                    enabled: true,
                    crop: false,
                    overflow: 'none',
                    format: '{point.y}', 
                    style: {
                        fontSize: '10px',
                        color: '#000'
                    }
                },
                showInLegend: true
            }
        },
        exporting: {
            enabled: false,
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.point.name + '</b><br/>' +
                    'Count: ' + this.point.y;
            }
        }
    });--%>
</script>
</asp:Content>
