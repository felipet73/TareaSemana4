
var unCliente = () => {
    var clienteid = document.getElementById("ClientesModelId").value;
    $.get(`/api/ClientesApi/${clienteid}`, (uncliente) => {
        document.getElementById("Correo").value = uncliente.email;
        document.getElementById("Cedula_RUC").value = uncliente.cedula_RUC;
        document.getElementById("Telefono").value = uncliente.telefono;
        document.getElementById("Direccion").value = uncliente.direccion;
    })
}

var Lista_Productos = () => {
    $.get(`/api/ProductosApi`, async (listaproductos) => {
        html = "";
        $.each(listaproductos, (index, producto) => {
            html += `<tr data-id="${producto.id}" data-nombre="${producto.nombre}" data-precio="${producto.precio}" >
                    <td> ${producto.nombre} </td>
                    <td> ${producto.precio} </td>
                    <td> <input id="qty_${producto.id}" type="number" min="1" value="0" /> </td>
                    <td> <button type="button" onclick="cargarproducto(this)" class="btn-success"
                    data-id="${producto.id}" data-nombre="${producto.nombre}" data-precio="${producto.precio}"
                    >+</button></td>
                </tr>
            `;
        })
        await $("#Lista_prodcutos").html(html)
    })
}

var cargarproducto = (producto) => {
    const id = producto.dataset.id;
    const nombre = producto.dataset.nombre;
    const precio = parseFloat(producto.dataset.precio)
    const cantidad = parseInt(document.getElementById(`qty_${id}`).value)

    if (cantidad <= 0 || isNaN(cantidad)) {
        alert('Ingrese una cantidad del producto valida');
        return;
    }

    const $tbody = $('#productosTable tbody');
    let fila = $tbody.find(`tr[data-id="${id}"]`);
    console.log(fila, 'valor de fila');
    if (fila.length) {
        const cantidadActual = parseInt(fila.find('input[name="Cantidad[]"]').val()) || 0;
        const nuevoCantidad = cantidadActual + cantidad;
        fila.find('input[name="Cantidad[]"]').val(nuevoCantidad);
        const nuevoMonto = nuevoCantidad * precio;
        fila.find('input[name="Monto[]"]').val(nuevoMonto.toFixed(2));
    } else {

        const monto = cantidad * precio;

        const fila = `<tr data-id="${id}" data-nombre="${nombre}">
                        <td data-nombre="nombre">${nombre}</td>
                        <td><input type="number" name="Precio[]" step="0.01" value=${precio.toFixed(2)}></td>
                        <td><input type="number" name="Cantidad[]" min="1" value=${cantidad}></td>
                        <td><input type="number" name="Monto[]" step="0.01" min="0" value=${monto.toFixed(2)}></td>
                        <td><button type="button" class="btn-remove btn btn-danger" onclick="eliminarFila(this)">X</button></td>
                    </tr>`;
        $('#productosTable tbody').append(fila);

    }

}

$(document).on("click", ".btn-remove", function () {
    $(this).closest('tr').remove();
});



var crear_venta = async () => {
    const clientId = document.getElementById("ClientesModelId").value || 0;
    const metodoPago = document.getElementById("metodoPago").value || "";
    if (clientId == 0 || !clientId) {
        alert('Seleccione un cliente');
        return;
    }

    const items = [];
    let subTotal = 0;
    const $tbody = $('#productosTable tbody tr[data-id]');
    console.log($tbody, 'tbody');
    $.each($tbody, function () {
        const $tr = $(this);
        const id = $tr.data('id');
        const nombre = $tr.data('nombre');
        console.log(id, nombre, $tr, 'valores de producto');
        const precio = parseFloat($tr.find('input[name="Precio[]"]').val());
        const cantidad = parseInt($tr.find('input[name="Cantidad[]"]').val());
        const monto = parseFloat($tr.find('input[name="Monto[]"]').val());
        console.log(id, nombre, precio, cantidad, monto, 'valores de producto');
        if (id && nombre && !isNaN(precio) && !isNaN(cantidad) && !isNaN(monto)) {
            items.push({ ProductosModelId: id, Nombre: nombre, Precio: precio, Cantidad: cantidad, Monto: parseFloat((cantidad * precio).toFixed(2)) });
            subTotal += cantidad * precio;
        }
    });
    subTotal = parseFloat(subTotal.toFixed(2));
    if (items.length === 0) {
        alert('Debe agregar al menos un producto a la venta');
        return;
    }
    const total = subTotal + (subTotal * 0.12); // Asumiendo un IVA del 12%

    const venta = {
        Fecha_venta: new Date().toISOString(),
        codigo_venta: "",
        Notas: "---",
        SubTotal: subTotal,
        Estado_Venta: "Completa",
        Descuento: 0,
        Total_Venta: total,
        MetodoPago: metodoPago,
        ClientesModelId: clientId,
        Productos_vendidos: items
    };

    try {
        const response = await fetch('/api/VentasApi', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(venta)
        })

        console.log(response, 'respuesta de crear venta');
        const createdVenta = await response.json();
        console.log(createdVenta, 'venta creada');
        alert('Venta creada exitosamente');
        await Imprimir();

    } catch (error) {
        console.log(error);
        alert("Error al guardar venta");
    }

}


var crear_compra = async () => {
    const clientId = document.getElementById("ClientesModelId").value || 0;
    const metodoPago = document.getElementById("metodoPago").value || "";
    if (clientId == 0 || !clientId) {
        alert('Seleccione un cliente');
        return;
    }

    const items = [];
    let subTotal = 0;
    const $tbody = $('#productosTable tbody tr[data-id]');
    console.log($tbody, 'tbody');
    $.each($tbody, function () {
        const $tr = $(this);
        const id = $tr.data('id');
        const nombre = $tr.data('nombre');
        console.log(id, nombre, $tr, 'valores de producto');
        const precio = parseFloat($tr.find('input[name="Precio[]"]').val());
        const cantidad = parseInt($tr.find('input[name="Cantidad[]"]').val());
        const monto = parseFloat($tr.find('input[name="Monto[]"]').val());
        console.log(id, nombre, precio, cantidad, monto, 'valores de producto');
        if (id && nombre && !isNaN(precio) && !isNaN(cantidad) && !isNaN(monto)) {
            items.push({ ProductosModelId: id, Nombre: nombre, Precio: precio, Cantidad: cantidad, Monto: parseFloat((cantidad * precio).toFixed(2)) });
            subTotal += cantidad * precio;
        }
    });
    subTotal = parseFloat(subTotal.toFixed(2));
    if (items.length === 0) {
        alert('Debe agregar al menos un producto a la venta');
        return;
    }
    const total = subTotal + (subTotal * 0.12); // Asumiendo un IVA del 12%

    const venta = {
        Fecha_venta: new Date().toISOString(),
        codigo_venta: "",
        Notas: "---",
        SubTotal: subTotal,
        Estado_Venta: "Completa",
        Descuento: 0,
        Total_Venta: total,
        MetodoPago: metodoPago,
        ClientesModelId: clientId,
        Productos_vendidos: items
    };

    try {
        const response = await fetch('/api/VentasApi', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(venta)
        })

        console.log(response, 'respuesta de crear venta');
        const createdVenta = await response.json();
        console.log(createdVenta, 'venta creada');
        alert('Venta creada exitosamente');
        await Imprimir();

    } catch (error) {
        console.log(error);
        alert("Error al guardar venta");
    }

}


var Imprimir = async () => {
    var contenido = document.getElementById("Imprimir").innerHTML;
    var contenidoOriginal = document.body.innerHTML;
    document.body.innerHTML = contenido;
    window.print();
    document.body.innerHTML = contenidoOriginal;
    //window.location.href = "/Ventas/ListaVentas";

}


//Cargar data en tabla desde JS
$(document).ready(function () {

    //Ejecutar solo para la opcion de listarClientes JS
    if (window.location.href.toLocaleLowerCase().includes('listaclientesjs'))
        $.ajax({
            url: '/api/ClientesApi',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data, 'Mi data de Clientes Get desde API Usando JS');
                var tableBody = $('#DetalleClientes');
                tableBody.empty(); // Limpiar el contenido previo
                $.each(data, function (index, cliente) {
                    var row = `<tr>
                        <td>${cliente.id}</td>
                        <td>${cliente.nombres}</td>
                        <td>${cliente.cedula_RUC}</td>
                        <td>${cliente.telefono}</td>
                        <td>${cliente.email}</td>
                        <td>
                            <a href="/Clientes/AccionClienteJS?id=${cliente.id}&accion=1" class="btn btn-outline-success">Editar</a>
                            ${cliente.isDelete ? `<a href="/Clientes/AccionClienteJS?id=${cliente.id}&accion=2" class="btn btn-outline-danger">Delete</a>` : ''}
                        </td>
                    </tr>`;
                    tableBody.append(row);
                });
            },
            error: function (xhr, status, error) {
                console.error('Error al cargar los clientes:', error);
            }
        });

    //Ejecutar solo para la opcion de Acccion Eliminar/Modificar cargar datos en Formulario JS
    if (window.location.href.toLocaleLowerCase().includes('accionclientejs') && ($('#Titulo').text() === 'Modificar Cliente' || $('#Titulo').text() === 'Eliminar Cliente'))
        $.ajax({
            url: `/api/ClientesApi/${$('#IdCliente').text()}`,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data, 'Mi data de Cliente Get desde API Usando JS');
                $('#Nombres').val(data.nombres);
                $('#Cedula_RUC').val(data.cedula_RUC);
                $('#Telefono').val(data.telefono);
                $('#Email').val(data.email);
                $('#Direccion').val(data.direccion);
                $('#isDelete').prop('checked', data.isDelete);
            },
            error: function (xhr, status, error) {
                console.error('Error al cargar los clientes:', error);
            }
        });
});

var RealizaAccion = () => {
    let accion = $('#Titulo').text();

    //Validaciones manuales con JS
    if ($('#Nombres').val() === '') {
        $('#ValidateNombres').text('Nombre no valido')
    }
    if ($('#Cedula_RUC').val() === '') {
        $('#ValidateCedula_RUC').text('Cedula/RUC no valido')
    }
    if ($('#Telefono').val() === '') {
        $('#ValidateTelefono').text('Telefono no valido')
    }
    if ($('#Email').val() === '') {
        $('#ValidateEmail').text('Email no valido')
    }
    if ($('#Direccion').val() === '') {
        $('#ValidateDireccion').text('Direccion no valida')
    }
    if ($('#Nombres').val() === '' ||
        $('#Cedula_RUC').val() === '' ||
        $('#Telefono').val() === '' ||
        $('#Email').val() === '' ||
        $('#Direccion').val() === ''
    ) {
        return;
    }

    //Obteniendo nuestro objeto cliente
    let cliente = {
        id: $('#IdCliente').text(),
        nombres: $('#Nombres').val(),
        cedula_RUC: $('#Cedula_RUC').val(),
        telefono: $('#Telefono').val(),
        email: $('#Email').val(),
        direccion: $('#Direccion').val(),
        isDelete: $('#isDelete').is(':checked')
    };
    let JSONcliente = JSON.stringify(cliente);

    //Realizando la accion dependiendo del titulo del formulario
    switch (accion) {
        case 'Nuevo Cliente':
            $.ajax({
                url: `/api/ClientesApi`,
                type: 'POST',
                data: JSONcliente,
                headers: {
                    'Content-Type': 'application/json'
                },
                success: function (result) {
                    window.location.href = '/Clientes/ListaClientesJS';
                },
                error: function (xhr, status, error) {
                    console.error('Error al agregar el cliente:', error);
                }
            });
            break;
        case 'Modificar Cliente':
            $.ajax({
                url: `/api/ClientesApi/${$('#IdCliente').text()}`,
                type: 'PUT',
                data: JSONcliente,
                headers: {
                    'Content-Type': 'application/json'
                },
                success: function (result) {
                    window.location.href = '/Clientes/ListaClientesJS';
                },
                error: function (xhr, status, error) {
                    console.error('Error al modificar el cliente:', error);
                }
            });
            break;
        case 'Eliminar Cliente':
            $.ajax({
                url: `/api/ClientesApi/${$('#IdCliente').text()}`,
                type: 'DELETE',
                success: function (result) {
                    window.location.href = '/Clientes/ListaClientesJS';
                },
                error: function (xhr, status, error) {
                    console.error('Error al eliminar el cliente:', error);
                }
            });
            break;
    }
}

//Validaciones de formulario
$(document).ready(function () {
    if (window.location.href.toLocaleLowerCase().includes('accionclientejs')) {
        $('#Nombres').on('input', function () {
            if ($(this).val() === '') {
                $('#ValidateNombres').text('Nombre no valido');
            } else {
                $('#ValidateNombres').text('');
            }
        });

        $('#Cedula_RUC').on('input', function () {
            if ($(this).val() === '') {
                $('#ValidateCedula_RUC').text('Cedula/RUC no valido');
            } else {
                $('#ValidateCedula_RUC').text('');
            }
        });

        $('#Telefono').on('input', function () {
            if ($(this).val() === '') {
                $('#ValidateTelefono').text('Telefono no valido');
            } else {
                $('#ValidateTelefono').text('');
            }
        });

        $('#Email').on('input', function () {
            if ($(this).val() === '') {
                $('#ValidateEmail').text('Email no valido');
            } else {
                $('#ValidateEmail').text('');
            }
        });

        $('#Direccion').on('input', function () {
            if ($(this).val() === '') {
                $('#ValidateDireccion').text('Direccion no valida');
            } else {
                $('#ValidateDireccion').text('');
            }
        });
    }
});



