$.extend({
    main: {
        BD: 0,
        companyid: 0,
        userid: 0,
        init: function () {
            var $this = this;
            setTimeout(function () {
                $this.fn_toggleClick();
            }, 500);
            $this.setUrlParam();
        },
        fn_toggleClick: () => {
            $('.plus-btn').click(function () {
                $('body').toggleClass('menu-open');
            });
        },
        getUrlparam: function getUrlParameter(sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                }
            }
            return false;
        },
        setUrlParam: function () {
            var $this = this;
            
            $this.BD = parseInt($this.getUrlparam("BD"));
            $this.companyid = parseInt($this.getUrlparam("companyid"));
            $this.userid = parseInt($this.getUrlparam("userid"));

            $this.BD = isNaN($this.BD) ? 0 : $this.BD;
            $this.companyid = isNaN($this.companyid) ? 0 : $this.companyid;
            $this.userid = isNaN($this.userid) ? 0 : $this.userid;

        }
    }
});
(() => $.main.init())();

kendo.culture("es-SV");