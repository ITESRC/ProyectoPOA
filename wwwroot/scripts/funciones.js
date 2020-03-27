{
    let popUp = document.getElementById('pop');
    let message = document.getElementById('message');
    let options = document.getElementById('options');
    let txtId = document.getElementById('IdUnidad');
    let btnAceptar = document.getElementById('btnAceptar');

    let unidadModal = document.getElementById('unidad');

    var NombreUnidad = "";
    var IdUnidad = 0;

    function MostrarPopUp() {
        popUp.style.visibility = "visible";
        message.style.visibility = "visible";
    }
    function EsconderPopUp() {
        popUp.style.visibility = "hidden";
        message.style.visibility = "hidden";
    }

    document.querySelector('table').addEventListener('click', function (e) {

        var closestCell = e.target.closest('tr');
        activeCell = e.currentTarget.querySelector('tr.selected');

        if (!closestCell.classList.contains("Head")) {
            closestCell.classList.add('selected');
            options.style.visibility = "visible";
            txtId.value = `${IdUnidad}`;
            btnAceptar.style.display = "block";
            if (activeCell) {
                activeCell.classList.remove('selected');
                t = activeCell = e.currentTarget.querySelector('tr.selected');
                if (!t) {
                    NombreUnidad = "";
                    IdUnidad = 0;
                    unidadModal.innerText = `Seleccione una unidad.`;
                    btnAceptar.style.display = "none";
                    options.style.visibility = "hidden";
                }
            }
        }
    })

    function verModal(unidad) {
        NombreUnidad = unidad.Nombre;
        IdUnidad = unidad.Id;
        unidadModal.innerText = `¿Estás seguro de eliminar a la unidad ${NombreUnidad}?`
    }

    function EditarUnidad() {
        if (IdUnidad != 0) {
            window.location.href = `UnidadesAdministrativas/Editar/${IdUnidad}`
        }
    }
}
