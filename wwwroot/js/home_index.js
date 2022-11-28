$.extend({
    mp_mapa: {
        init: function () {
            var $this = this;
            setTimeout(function () {
                $this.fn_menu_urldata();
                $this.fn_menu_change_active();
            }, 500);
        },
        menuid: "mp_menu",
        menu: [
            {
                url: "mp_VerMapa/mp_VerMapa",
                text: "Mapa",
                default: true
            },
            {
                url: "mp_rpzonadelictiva/mp_rpzonadelictiva",
                text: "Reporte Zona Delictiva",
                default: false
            },
            {
                url: "mp_rpPagosRealizados/mp_rpPagosRealizados",
                text: "Reporte Pagos Realizados",
                default: false
            },
            {
                url: "mp_rpLlamadas/mp_rpLlamadas",
                text: "Reporte Llamadas",
                default: false
            }
        ],
        fn_menu_cerrar: function () {
            $('body').removeClass('menu-open');
        },
        fn_menu_urldata: function () {
            var $this = this;
            var menu_html = "<ul>";
            for (var i = 0, len = $this.menu.length; i < len; i++) {
                if ($this.menu[i]["default"]) {
                    $this.fn_menu_setPage($this.menu[i]["url"]);
                    menu_html+=
                    `<li class="active">
                        <a>${$this.menu[i]["text"]}</a>
                    </li>`;
                } else {
                    menu_html+=
                    `<li>
                        <a>${$this.menu[i]["text"]}</a>
                    </li>`;
                }
            }
            menu_html += "</ul>";
            $("#mp_menu").html(menu_html);
            //$("#" + $this.menuid + " ul li")
        },
        fn_menu_setPage: function (url) {
            var $this = this;
            $.ajax({
                url: url,
                type: "GET",
                async: true,
                success: function (data) {
                    $("#div_contenido").html(data);
                }
            });
        },
        fn_menu_change_active: function() {
            var $this = this; 
            $("#mp_menu ul li").on("click", function () {
                $("#" + $this.menuid  + " ul li").removeClass("active");
                $(this).addClass("active");
                setTimeout(function () {
                    $this.fn_menu_cerrar();
                }, 500);
                
            });
        }
    }
});

(() => $.mp_mapa.init())();