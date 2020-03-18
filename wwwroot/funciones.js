{
let btnEditar = document.getElementById('btnEditar');
let btnEliminar = document.getElementById('btnEliminar');

document.querySelector('table').addEventListener('click', function (e) {

    var closestCell = e.target.closest('tr');
    activeCell = e.currentTarget.querySelector('tr.selected');

    if (!closestCell.classList.contains("Head")) {
        closestCell.classList.add('selected');
        btnEliminar.style = "display:block";
        btnEditar.style = "display:block";
    if (activeCell) {
        activeCell.classList.remove('selected');
        t = activeCell = e.currentTarget.querySelector('tr.selected');
        if (!t) {
            btnEliminar.style = "display:none";
            btnEditar.style = "display:none";
        }
    }
    }

})

let unidadModal = document.getElementById('unidad')
let btnAceptar = document.getElementById('btnAceptar')

var NombreUnidad = "";
var IdUnidad = 0;

function verModal(unidad) {
    NombreUnidad = unidad.Nombre;
    IdUnidad = unidad.Id;
    unidadModal.innerText = `¿Estás seguro de eliminar a la unidad ${NombreUnidad}?`
}

function EliminarUnidad() {
    window.location.href = `UnidadesAdministrativas/Eliminar/${IdUnidad}`
}

function EditarUnidad() {
    window.location.href = `UnidadesAdministrativas/Editar/${IdUnidad}`
}
}
var estatus = false;
function Enviar() {
    if (!estatus) {
        estatus = true;
        return true;
    } else {

        return false;
    }
}