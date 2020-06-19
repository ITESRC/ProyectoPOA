{
    var divAdd = document.getElementById("popadd");
    var sectionAdd = document.getElementById("sectionAdd");
    var isopen = false;
    function OpenCancel() {
        if (!isopen) {
            divAdd.style.visibility = "visible";
            sectionAdd.style.visibility = "visible";
            isopen = true;
        }
        else {
            divAdd.style.visibility = "hidden";
            sectionAdd.style.visibility = "hidden";
            isopen = false;
        }
    }

    function Editar() {
        var ua = document.getElementById("cmbUnidades").options[select.selectedIndex];
        var estrategiasPermitidas = document.getElementsByClassName("estrategia");
        var estrategias = { idUnidad: ua.value, estrategiasPermitidas: [] };

        for (var i = 0; i < estrategiasPermitidas.length; i++) {

            var id = estrategiasPermitidas[i].value;
            var EstrategiaPermitida = estrategiasPermitidas[i].checked;
            var estrategia = { "idEstrategia": id, "permitida": EstrategiaPermitida };

            estrategias.estrategiasPermitidas[i] = estrategia;
        }

        
        $.ajax({
            type: "POST",
            url: '/PermitirEstrategias/EditarEstrategiasPermitidas',
            data: { estrategias: estrategias },
            success: function (response) {
                MostrarJson(response)
            }
        })

        OpenCancel();

    }


    var div = document.querySelector('.divSeleccionados dl dt');
    var partida = document.querySelector('.divSeleccionados dl dd');
    var select = document.getElementById('cmbUnidades');

    select.addEventListener('change',
        function () {
            div.innerHTML = "";
            partida.innerHTML = "";
            document.getElementById("dlEstrategias").innerHTML = "";
            document.getElementById("seleccionarEstrategias").innerHTML = "";
            var ua = this.options[select.selectedIndex];
            $.ajax({
                type: "POST",
                url: '/PermitirEstrategias/MostrarEstrategiasByUnidad',
                data: { id: ua.value },
                success: function (response) {
                    MostrarJson(response)
                }
            })

            $.ajax({
                type: "POST",
                url: '/PermitirEstrategias/MostrarEstrategiasParaPermitir',
                data: { id: ua.value },
                success: function (response) {
                    MostrarEstrategiasParaPermitir(response)
                }
            })
        });

    function MostrarEstrategiasParaPermitir(data) {
        $.each(data, function (index, obj) {
            if (obj != null) {
                if (index === "objetivosPermitidos") {
                    for (var i = 0; i < obj.length; i++) {
                        var node = document.createElement("DT");
                        var textnode = document.createTextNode(obj[i].nombreObjetivo);
                        node.appendChild(textnode);
                        document.getElementById("seleccionarEstrategias").appendChild(node);
                        for (var j = 0; j < obj[i].estrategiasPermitidas.length; j++) {
                            var res = obj[i].estrategiasPermitidas[j].nombreEstrategia;
                            var nodeEstrategia = document.createElement("DD");

                            var nodeCheckPermitir = document.createElement('input');
                            nodeCheckPermitir.type = "checkbox"; 
                            nodeCheckPermitir.checked = obj[i].estrategiasPermitidas[j].permitida;
                            nodeCheckPermitir.value = obj[i].estrategiasPermitidas[j].idEstrategia;
                            nodeCheckPermitir.className = "estrategia";
                            nodeEstrategia.appendChild(nodeCheckPermitir);

                            var textnodeEstrategia = document.createTextNode(res);
                            nodeEstrategia.appendChild(textnodeEstrategia);

                            document.getElementById("seleccionarEstrategias").appendChild(nodeEstrategia);
                        }
                    }
                }
            }
        })
    }

    function MostrarJson(data) {
        $.each(data, function (index, obj) {
            if (obj != null) {
                if (index === "objetivosPermitidos") {
                    document.getElementById("dlEstrategias").innerHTML = "";
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
            }
        })
    }
}