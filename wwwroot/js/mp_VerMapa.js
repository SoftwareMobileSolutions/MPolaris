$.extend({
    mp_map: {
        idmap: "map",
        map: null,
        infowindow: null,
        marker: null,
        markers: [],
        geodelicitivas: [],
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
        },
        fn_getNivelDelictivo: function() {
            var $this = this;
            var arg = {
                companyid: $.main.companyid,
                BD: $.main.BD
            };
            $.get("../mp_VerMapa/Get_peligrosidadDelictiva", arg, function (d) {
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
            var arg = {
                companyid: $.main.companyid,
                BD: $.main.BD
            };
            $.get("../mp_VerMapa/Get_geodelicitvas", arg, function (d) {
                var r1 = d["r1"];
                var r2 = d["r2"];

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

                    //geodetail[r1[i]["ID"]]
                    //(16711680).toString(16);
                }

            });
        }

    }
});

(() => $.mp_map.init())();
