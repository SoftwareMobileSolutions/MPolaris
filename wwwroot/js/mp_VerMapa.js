$.extend({
    mp_map: {
        lastdatageoGenerar: [],
        idmap: "map",
        idmodal: "mp_modaleventos",
        map: null,
        infowindow: null,
        geo_markers: [],
        geo_delicitivas: [],
        center: { lat: 13.698046622, lng: -89.209458191 },
        tbl_nivelDelictivo: [],
        init: () => {
            var _this = $.mp_map;
            _this.fn_initialize();
            _this.fn_getNivelDelictivo();
            _this.fn_getGeoDelictivo();
            //_this.btn_clickLocaltion();
            //_this.interval_getLocationgps;
        },
        decimalToHexColor: function (n) {
            var me = n.toString(16);
            while (me.length < 6) {
                me = "0" + me;
            }
            return "#" + me;
        },
        rad: function(x) {
            return x * Math.PI / 180;
        },
        GetDistanceBetweenPoints: function (p1, p2) {
            var $this = this;
            var R = 6378137;
            var dLat = $this.rad(p2.lat() - p1.lat());
            var dLong = $this.rad(p2.lng() - p1.lng());
            var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                Math.cos($this.rad(p1.lat())) * Math.cos($this.rad(p2.lat())) *
                Math.sin(dLong / 2) * Math.sin(dLong / 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var d = R * c;
            return d;
        },
        geo_limit_txt: function (str, len) {
            var trimmedString = str.length > len ?
                str.substring(0, len - 3) + "..." :
                str;
            return trimmedString;
        },
        map_types: {
            CommunityTilesBaseUrl: ["https://worldtiles2.waze.com/tiles/"],
            CommunityMapName: ["CommunityEstandard"],
            CommunityOpcEstandar: null,
            CommunityEstandar: null,
            CommunityMapsType: function () {
                var _this = this;
                _this.CommunityOpcEstandar = { getTileUrl: function (coord, zoom) { var tilesPerGlobe = 1 << zoom; var x = coord.x % tilesPerGlobe; if (x < 0) { x = tilesPerGlobe + x; } return _this.CommunityTilesBaseUrl[0] + zoom + "/" + x + "/" + coord.y + ".png"; }, tileSize: new google.maps.Size(256, 256), isPng: true, maxZoom: 21, name: _this.CommunityMapName[0] };
                _this.CommunityEstandar = new google.maps.ImageMapType(_this.CommunityOpcEstandar);
            }
        },
        fn_initialize: () => {
            var _this = $.mp_map;
            _this.map_types.CommunityMapsType();
            _this.map = new google.maps.Map(document.getElementById(_this.idmap), {
                zoom: 17,
                panControl: false,
                scaleControl: false,
                mapTypeControl: false,
                streetViewControl: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                fullscreenControl: false,
                mapTypeControlOptions: {
                    mapTypeIds: [google.maps.MapTypeId.ROADMAP,
                    google.maps.MapTypeId.HYBRID,
                    google.maps.MapTypeId.SATELLITE,
                    google.maps.MapTypeId.TERRAIN],
                    style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR
                },
                zoomControl: false,
                zoomControlOptions: { style: google.maps.ZoomControlStyle.SMALL },
                gestureHandling: 'greedy',
                center: _this.center
            });
            _this.map.mapTypes.set('CommunityEstandar', _this.map_types.CommunityEstandar);
            _this.map.setMapTypeId('CommunityEstandar');

            if (_this.infowindow === null) {
                _this.infowindow = new google.maps.InfoWindow();

                
            }
            _this.map.addListener("click", function () {
                if (_this.infowindow) {
                    _this.infowindow.close();
                }
            });
            _this.map.addListener("zoom_changed", function () {
                if (_this.infowindow) {
                    _this.infowindow.close();
                }
            });
            _this.map.addListener("dragstart", function () {
                if (_this.infowindow) {
                    _this.infowindow.close();
                }
            });
           
        },

        fn_getNivelDelictivo: function() {
            var $this = this;
            var arg = {
                companyid: $.main.companyid,
                BD: $.main.BD
            };
            $.get("mp_VerMapa/Get_peligrosidadDelictiva", arg, function (d) {
                $this.tbl_nivelDelictivo = d;
                var trl_html = 
                `
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <label class="control-label"><b>Nivel</b></label>
                                </td>
                                <td>
                                    <label class="control-label"><b>Color</b></label>
                                </td>
                            </tr>
                ` ;
                
                for (var i = 0, len = d.length; i < len; i++) {
                    var color = (d[i]["color"]).toString(16);
                    trl_html +=
                    `
                        <tr>
                            <td><label class="control-label">${d[i]["nombre"]}</label></td>
                            <td align="center"><button style="background-color:#${color};height: 15px"></button></td>
                        </tr>
                    `;
                }
                $("#Semaforo").html(`
                         ${trl_html}
                        </tbody>
                    </table>
                `)
            });
        },
        fn_getGeoDelictivo: function () {
            var $this = this;
            // var colorPath = (color === 0 || color === "0" || color === "" ? "#000000" : (color).ToHexColor());
            var arg = {
                companyid: $.main.companyid,
                BD: $.main.BD
            };
            $this.fn_cleanGeozonas();
            $this.fn_cleanMarkers();

            

            $.get("mp_VerMapa/Get_geodelictivas", arg, function (d) {
                var bounds = new google.maps.LatLngBounds();

                var r1 = d["r1"];
                var r2 = d["r2"];

                for (var j = r2.length; j--;) {
                    r2[j]["lat"] = parseFloat(r2[j]["lat"]);
                    r2[j]["lng"] = parseFloat(r2[j]["lng"]);
                }
                var geodetail = _.groupBy(r2, "id");
                for (var i = r1.length; i--;) {
                    r1[i]["x"] === null && (r1[i]["x"] = 0);
                    r1[i]["y"] === null && (r1[i]["y"] = 0);
                    r1[i]["valorRenta"] === null && (r1[i]["valorRenta"] = 0);
                    r1[i]["valorAlivian"] === null && (r1[i]["valorAlivian"] = 0);

                    r1[i]["x"] = parseFloat(r1[i]["x"]);
                    r1[i]["y"] = parseFloat(r1[i]["y"]);
                    r1[i]["valorRenta"] = parseFloat(r1[i]["valorRenta"]);
                    r1[i]["valorAlivian"] = parseFloat(r1[i]["valorAlivian"]);

                  //  r1[i]["color"] = (r1[i]["color"] === 0 || r1[i]["color"] === "0" || r1[i]["color"] === "" ? "#000000" : ToHexColor(r1[i]["color"]));

                    r1[i]["detalle"] = geodetail[r1[i]["id"]];


                    //Calculando el color según peligrosidad *******
                    var icono = "p100.png";
                    var geocolor = "#000000";

                    var colorPeligrosidad = String(r1[i]["peligrosidad"]).toLowerCase();
                    if (colorPeligrosidad.indexOf("muy") >= 0) {
                        if (colorPeligrosidad.indexOf("bajo") >= 0) {
                            icono = "p116.png";
                            geocolor = "#4ba425";
                        } else if (colorPeligrosidad.indexOf("alto") >= 0) {
                            icono = "p112.png";
                            geocolor = "#607d8b";
                        }
                    } else {
                        if (colorPeligrosidad.indexOf("bajo") >= 0) {
                            icono = "p100.png";
                            geocolor = "#fbe928";
                        } else if (colorPeligrosidad.indexOf("alto") >= 0) {
                            icono = "p109.png";
                            geocolor = "#ff0202";
                        } else if (colorPeligrosidad.indexOf("medio") >= 0) {
                            icono = "p107.png";
                            geocolor = "#f9ae35";
                        }
                    }
                    r1[i]["color"] = geocolor;

                    //Crear la geozonas
                    var arg = $this.fn_crearGeozonas(r1[i]);
                    
                    //Crear Puntos X Y
                    var posicionMK = $this.fn_crearMarkers(arg, icono);

                    bounds.extend(posicionMK);
                }
                $this.map.fitBounds(bounds);
                //console.log(r1, geodetail);
            });
        },
        // Geozonas
        fn_crearGeozonas: function (arg) {
            var $this = this;
            var area = 0, perimetro = 0;
            
           // console.log(arg["color"]);

            var coords = arg["detalle"];
            var Geozone = new google.maps.Polygon({
                paths: coords,
                strokeColor: arg["color"],
                strokeOpacity: 1,
                strokeWeight: 2,
                fillColor: arg["color"],
                fillOpacity: 0.25,
                map: $this.map
            });
          //  console.log(Geozone);

            area = google.maps.geometry.spherical.computeArea(Geozone.getPath());
            area = area.toFixed(2);
            var GeozonePath = Geozone.getPath();
            for (var i = 0, len = GeozonePath.length - 1; i < len; i++) {
                perimetro = perimetro + $this.GetDistanceBetweenPoints(Geozone.getPath().getAt(i), Geozone.getPath().getAt(i + 1));
            }
            perimetro = perimetro.toFixed(2);

            arg["perimetro"] = perimetro;
            arg["area"] = area;

            Geozone["arg"] = arg;
            Geozone.addListener("click", function (e) {
                var geothis = this;
                if ($this.infowindow) {
                    $this.infowindow.close();
                }
               // console.log(e);
               // var InfoPosition = new google.maps.LatLng(geothis.y, geothis.x);
                var content = $this.fn_contentinfowindow(geothis.arg);

                $this.infowindow = new google.maps.InfoWindow({ content: content });
                $this.infowindow.open($this.map, geothis);
                $this.infowindow.setPosition(e.latLng);
                


               /* google.maps.event.addDomListener($this.infowindow, 'domready', function () {
                    $this.infowindow.setPosition(InfoPosition);
                });*/

            });

            /*
            google.maps.event.addDomListener(Geozone, "click", function (event) {
                if ($this.infowindow) {
                    $this.infowindow.close();
                }
                var InfoPosition = new google.maps.LatLng(event.latLng.lat(), event.latLng.lng());
                content = $this.ContentInfoWindowRef(arg.lat, arg.lng, arg.geoname, area, perimetro);
                $this.infowindow = new google.maps.InfoWindow({ content: content });
                $this.infowindow.open(map, Geozone);
              
                
                google.maps.event.addDomListener($this.infowindow, 'domready', function () {
                    CssInfoWindow("iw-container", 0);
                    $this.infowindow.setPosition(InfoPosition);
                    google.maps.event.addDomListener($this.infowindow, 'closeclick', function () {
                        CssInfoWindowRemove();
                    });
                }); 

               
            }); 
            */
            $this.geo_delicitivas.push(Geozone);
            return arg;
        },
        fn_cleanGeozonas: function (arg) {
            var $this = this;
            for (var i = $this.geo_delicitivas.length; i--;) {
                $this.geo_delicitivas[i].setMap(null);
            }
            $this.geo_delicitivas = [];
        },
        //Marker de Geozonas
        fn_crearMarkers: function (arg, icono) {
    
            var $this = this;
            //var icon = "../img/" + icono;
            var icon = "img/" + icono;
            var posicionMK = new google.maps.LatLng(arg.y, arg.x);
           

            var mkxy = new google.maps.Marker({
                arg: arg,
                map: $this.map,
                position: posicionMK,
                icon: icon,
                visible: true,
                title: "Centro(Lat/Lng): " + arg.y + ", " + arg.x
            });

           
            mkxy.addListener("click", function () {
                var mkthis = this;
                if ($this.infowindow) {
                    $this.infowindow.close();
                }
                $this.infowindow = new google.maps.InfoWindow({ content: $this.fn_contentinfowindow(mkthis.arg) });
                $this.infowindow.open($this.map, mkthis);
            });
            
            $this.geo_markers.push(mkxy);

            return posicionMK;
        }, 
        fn_cleanMarkers: function () {
            var $this = this;
            for (var i = $this.geo_markers.length; i--;) {
                $this.geo_markers.setMap(null);
            }
            $this.geo_markers = [];
        },
        //Contenido del infowindow
        fn_contentinfowindow: function (arg) {
            //console.log(arg);
            var $this = this;
            var fPago = "";

            switch (arg.formaPago) {
                case 7: fPago = "Semanal"; break;
                case 15: fPago = "Quincenal"; break;
                case 30: fPago = "Mensual"; break;
                case 0: fPago = ""; break;
                default: fPago = "Cada " + arg.formaPago + " dias"; break;
            }

            var str = `
            <div id="iw-container">
                <div class="iw-title_">${$this.geo_limit_txt(arg.geoname, 22)}</div>
                    <div class="iw-content_">
                        <table class='info'>
                            <tr><td>Nombre   </td><td>${arg.geoname}</td></tr>
                            <tr><td>Área     </td><td>${arg.area} m²</td></tr>
                            <tr><td>Perímetro</td><td>${arg.perimetro} m</td></tr>
                            <tr><td style="background-color:${arg.color} !important;">Grupo delictivo</td><td style="background-color:${arg.color} !important;">${arg.grupodelictivo}</td></tr>
                            <tr><td>Valor PGD ($)</td><td> ${arg.valorRenta} </td></tr>
                            <tr><td>Forma de Pago</td><td>${fPago}</td></tr>
                            <tr><td>Vehiculos Asignados</td><td>${arg.mobiles}</td></tr>
                            <tr><td style="background-color:${arg.color} !important;">Eventos Anuales</td>
                                <td style="background-color:${arg.color} !important;">
                                    <div class="btn-group shadow" role="group">
                                        <button type="button" onclick="$.mp_map.fn_obtenerEventosAnuales(${arg.id});" class="btn btn-sm glass text-white p-1">${arg.canteventos}</button>
                                        <button type="button" onclick="$.mp_map.fn_obtenerEventosAnuales(${arg.id});" class="btn btn-sm glass text-white p-1">Ver</button>
                                    </div>
                                </td></tr>
                            <tr><td>Centro(Lat/Lng)</td><td>${arg.y}/${arg.x}</td></tr>
                        </table>
                    </div>
                </div>
            </div>
            `;
            return str;
        },
        fn_obtenerEventosAnuales: function (idgeo) {
            var $this = this;
            var arg = {
                companyid: $.main.companyid,
                BD: $.main.BD,
                fechaini: moment().startOf('year').format("YYYYMMDD HH:mm:ss"),
                fechafin: moment().endOf('year').format("YYYYMMDD HH:mm:ss"),
                geozoneid: idgeo
            };
            $.mp_map.lastdatageoGenerar = arg;
            $.get("mp_VerMapa/getEventosAnuales", arg, function (d) {
                //console.log(d);
                if (!d.length) {
                    Swal.fire(
                        'No hay Datos que mostrar!',
                        '',
                        'warning'
                    );
                    create_Grid_eventosAnuales([]);
                   // return false; //por si van a generar de otras fechas se omite el return false
                }
                create_Grid_eventosAnuales(d);

                $("#fecha_inirp").data("kendoDateTimePicker").value(moment().startOf('year')._d);
                $("#fecha_finrp").data("kendoDateTimePicker").value(moment().endOf('year')._d);


                $("#" + $this.idmodal).modal("show");
                setTimeout(function () {
                    $("[id='Grid_eventosAnuales']").data("kendoGrid").refresh();
                }, 500);
               
                
            });
        }
        //functiones del grid
        
    }
});

(() => $.mp_map.init())();
