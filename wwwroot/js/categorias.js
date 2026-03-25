$(document).ready(function () {
    $('#tablaCategorias').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
        }
    });
});

function abrirModalCrear() {
    new bootstrap.Modal(document.getElementById('modalCrear')).show();
}

function abrirModalEditar(id, nombre) {
    document.getElementById('editCategoriaId').value = id;
    document.getElementById('editNombre').value = nombre;
    new bootstrap.Modal(document.getElementById('modalEditar')).show();
}

function abrirModalEliminar(id, nombre) {
    document.getElementById('eliminarCategoriaId').value = id;
    document.getElementById('eliminarNombre').textContent = nombre;
    new bootstrap.Modal(document.getElementById('modalEliminar')).show();
}