function MostrarJson(data) {
    $.each(data, function (index, obj) {
        if (obj != null) {
            if (index === "capitulosPermitidos") {

                for (var i = 0; i < obj.length; i++) {
                    div.innerHTML = obj[i].nombreCapitulo;
                    for (var j = 0; j < obj[i].partidasPermitidas.length; j++) {
                        var res = obj[i].partidasPermitidas[j].nombrePartida;
                        partida.append(res);
                    }
                }
                Seleccionar(data);
            }
        }
    })
}
var div = document.querySelector('.divSeleccionados dl dt');
var partida = document.querySelector('.divSeleccionados dl dd');
var select = document.getElementById('cmb');
select.addEventListener('change',
    function () {
        div.innerHTML = "";
        partida.innerHTML = "";
        var ua = this.options[select.selectedIndex];
        $.ajax({
            type: "POST",
            url: '/PermitirPartidas/Mostrar',
            data: { id: ua.value },
            success: function (response) {
                MostrarJson(response)
            }
        })
    });
function Seleccionar(objeto) {
    let container = document.querySelector('#estrategias');
    let container2 = document.querySelector('.esthtml');
    $.each(objeto, function (index, obj) {
        if (obj != null) {
            if (index === "capitulosPermitidos") {
                for (var i = 0; i < obj.length; i++) {
                    var html1 = '<optgroup label=';
                    html1 = html1 + obj[i].nombreCapitulo + ">";
                    for (var j = 0; j < obj[i].partidasPermitidas.length; j++) {
                        html1 = html1 + '<option >' + obj[i].partidasPermitidas[j].nombrePartida + '</option>';
                        html1 = html1 + '</optgroup>';
                        container.innerHTML = html1;
                        container2.innerHTML = html1;
                    }
                }
            }
        }
    })
}