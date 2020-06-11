{
    function CambiarUnidad() {
        var cmb = document.getElementById("cmbUnidades");
        var idUnidad = cmb.options[cmb.selectedIndex].value;
        if (idUnidad != "") {
            window.location.href = `PermitirEstrategias?id=${idUnidad}`
        }
    }

    var div = document.querySelector('.divSeleccionados dl dt');
    var partida = document.querySelector('.divSeleccionados dl dd');
    var select = document.getElementById('cmbUnidades');
    select.addEventListener('change',
        function () {
            div.innerHTML = "";
            partida.innerHTML = "";
            var ua = this.options[select.selectedIndex];
            $.ajax({
                type: "POST",
                url: '/PermitirEstrategias/MostrarEstrategiasByUnidad',
                data: { id: ua.value },
                success: function (response) {
                    MostrarJson(response)
                }
            })
        });

    function MostrarJson(data) {
        $.each(data, function (index, obj) {
            if (obj != null) {
                if (index === "objetivosPermitidos") {
                    for (var i = 0; i < obj.length; i++) {
                        var node = document.createElement("DT");
                        var textnode = document.createTextNode(obj[i].nombreObjetivo);
                        node.appendChild(textnode);
                        document.getElementById("dlEstrategias").appendChild(node);
                        for (var j = 0; j < obj[i].estrategiasPermitidas.length; j++) {
                            var res = obj[i].estrategiasPermitidas[j].nombreEstrategia;

                            var nodeEstrategia = document.createElement("DD");
                            var textnodeEstrategia = document.createTextNode(res);
                            nodeEstrategia.appendChild(textnodeEstrategia);
                            document.getElementById("dlEstrategias").appendChild(nodeEstrategia);
                        }
                    }
                }
                    console.log('correcto');
            }
        })
    }
}