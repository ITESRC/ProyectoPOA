{
    function CambiarUnidad() {
        var cmb = document.getElementById("cmbUnidades");
        var idUnidad = cmb.options[cmb.selectedIndex].value;
        if (idUnidad != "") {
            window.location.href = `PermitirEstrategias?id=${idUnidad}`
        }
    }
}