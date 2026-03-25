$(document).ready(function () {
    $('#tablaProductos').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
        }
    });
});

function abrirModalCrear() {
    new bootstrap.Modal(document.getElementById('modalCrear')).show();
}

function abrirModalEditar(id, codigo, nombre, descripcion, categoriaId, precio, stock, stockMinimo, activo) {
    document.getElementById('editProductoId').value = id;
    document.getElementById('editCodigo').value = codigo;
    document.getElementById('editNombre').value = nombre;
    document.getElementById('editDescripcion').value = descripcion;
    document.getElementById('editCategoriaId').value = categoriaId;
    document.getElementById('editPrecio').value = precio;
    document.getElementById('editStock').value = stock;
    document.getElementById('editStockMinimo').value = stockMinimo;
    document.getElementById('editActivo').checked = activo;
    new bootstrap.Modal(document.getElementById('modalEditar')).show();
}

function abrirModalDetalles(nombre, codigo, descripcion, categoria, precio, stock, stockMinimo, activo, fechaCreacion) {
    document.getElementById('detNombre').textContent = nombre;
    document.getElementById('detCodigo').textContent = codigo;
    document.getElementById('detDescripcion').textContent = descripcion;
    document.getElementById('detCategoria').textContent = categoria;
    document.getElementById('detPrecio').textContent = '$' + parseFloat(precio).toFixed(2);
    document.getElementById('detStock').textContent = stock;
    document.getElementById('detStockMinimo').textContent = stockMinimo;
    document.getElementById('detActivo').textContent = activo ? 'Sí' : 'No';
    document.getElementById('detFechaCreacion').textContent = fechaCreacion;
    new bootstrap.Modal(document.getElementById('modalDetalles')).show();
}

function abrirModalEliminar(id, nombre) {
    document.getElementById('eliminarProductoId').value = id;
    document.getElementById('eliminarNombre').textContent = nombre;
    new bootstrap.Modal(document.getElementById('modalEliminar')).show();
}