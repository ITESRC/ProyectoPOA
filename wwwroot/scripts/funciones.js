{
    let popUp = document.getElementById('pop');
    let message = document.getElementById('message');
    let options = document.getElementById('options');
    let txtId = document.getElementById('IdUnidad');
    let btnAceptar = document.getElementById('btnAceptar');

    let unidadModal = document.getElementById('unidad');

    var NombreUnidad = "";
    var IdUnidad = 0;

    $(document).ready(function () {
        if (sessionStorage.curpos) {
            $("main").scrollTop(sessionStorage.curpos);
            sessionStorage.removeItem("curpos");
        }

        $(".cancel").click(function () {
            sessionStorage.curpos = $("main").scrollTop();
            window.location.reload();
        });

        $(".formAjax").submit(function (e) {
            e.preventDefault();

            var input =
                $(this).find("button[type=submit]")
                    .attr("disabled", true);

            var actionUrl = $(this).attr("action");
            $(".error").text("");

            sessionStorage.curpos = $("main").scrollTop();

            $.post(actionUrl, $(this).serialize(), function (res) {
                if (res == true) {
                    window.location.reload();
                } else {
                    $(".error").html(res);
                    input.attr("disabled", false);
                }
            });
        });
    });


    function showM(Id) {
        var p = document.getElementById(Id);
        p.style.visibility = 'visible';
        p.style.display = 'block';
    }

    function MostrarPopUp() {
        popUp.style.visibility = "visible";
        message.style.visibility = "visible";
    }
    function HideM(Id) {
        var p = document.getElementById(Id);
        p.style.visibility = 'hidden';
        p.style.display = 'none';
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
    {
        $(function () {
            $('table').on('click', 'tr', function (event) {
                $(this).addClass('selected').siblings().removeClass('selected');
                $('.fabs').css('visibility', 'visible');
                eliminar($('.selected th').attr('id'));
                editar($('.selected th').attr('id'));
            });
        });
    }
}
