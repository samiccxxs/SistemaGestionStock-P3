$(document).ready(function () {
    $('#tablaMovimientos').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
        },
        order: [[0, 'desc']]
    });
});

function abrirModalEntrada() {
    new bootstrap.Modal(document.getElementById('modalEntrada')).show();
}

function abrirModalSalida() {
    new bootstrap.Modal(document.getElementById('modalSalida')).show();
}