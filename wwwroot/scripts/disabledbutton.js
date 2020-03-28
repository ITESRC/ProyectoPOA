$("form").submit(function () {
    console.log("Se ha submitiado");
    $(this).find(":submit").attr('disabled', 'disabled');
});