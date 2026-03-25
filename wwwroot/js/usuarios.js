$(document).ready(function () {
    $('#tablaUsuarios').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
        }
    });
});

function abrirModalCrear() {
    new bootstrap.Modal(document.getElementById('modalCrear')).show();
}

function abrirModalEditar(id, nombre, nombreUsuario, rolId, activo) {
    document.getElementById('editUsuarioId').value = id;
    document.getElementById('editNombre').value = nombre;
    document.getElementById('editNombreUsuario').value = nombreUsuario;
    document.getElementById('editRolId').value = rolId;
    document.getElementById('editActivo').checked = activo;
    new bootstrap.Modal(document.getElementById('modalEditar')).show();
}

function abrirModalEliminar(id, nombre) {
    document.getElementById('eliminarUsuarioId').value = id;
    document.getElementById('eliminarNombre').textContent = nombre;
    new bootstrap.Modal(document.getElementById('modalEliminar')).show();
}