# Sistema de Gestión de Stock (P3)

Proyecto web en ASP.NET Core MVC para gestionar inventario (stock). Incluye login/registro, y módulos de Productos y Categorías, conectados a SQL Server usando Entity Framework Core.

## Descripción general
Sistema web desarrollado en ASP.NET Core MVC para la administración de inventario.  
El proyecto permite gestionar productos, categorías, usuarios y movimientos de entrada y salida de stock, además de mostrar un panel principal con información resumida sobre el estado del sistema.

Este proyecto fue realizado con fines académicos y está orientado a resolver operaciones básicas de control de inventario dentro de una aplicación web con autenticación por sesión y control de acceso por roles.

## Objetivo del proyecto
Desarrollar una aplicación que facilite el registro, consulta y actualización del inventario de productos, permitiendo:
- llevar control del stock actual,
- registrar entradas y salidas,
- administrar usuarios del sistema,
- organizar productos por categorías,
- visualizar alertas de stock bajo o agotado.

## Funcionalidades implementadas

### 1. Módulo de cuenta
Permite:
- inicio de sesión,
- registro de usuarios,
- cierre de sesión,
- manejo de acceso denegado.

El inicio de sesión valida que el usuario exista, esté activo y que la contraseña coincida con la almacenada de forma segura.

### 2. Dashboard principal
El panel principal muestra un resumen general del sistema, incluyendo:
- total de productos,
- total de categorías,
- total de usuarios,
- productos sin stock,
- productos con stock bajo,
- listado de productos con bajo stock,
- últimos productos registrados.

### 3. Gestión de productos
Permite:
- crear productos,
- editar productos,
- visualizar detalles,
- eliminar productos,
- desactivar productos cuando tienen movimientos asociados.

Cada producto registra:
- código,
- nombre,
- descripción,
- categoría,
- precio,
- stock,
- stock mínimo,
- estado activo,
- fecha de creación.

### 4. Gestión de categorías
Permite:
- crear categorías,
- editar categorías,
- eliminar categorías siempre que no tengan productos asociados.

### 5. Gestión de usuarios
Permite:
- crear usuarios,
- editar usuarios,
- eliminar usuarios,
- asignar roles,
- evitar que un usuario elimine su propia cuenta desde sesión activa.

### 6. Gestión de movimientos
Permite registrar:
- entradas de inventario,
- salidas de inventario.

Cada movimiento guarda:
- producto,
- usuario,
- tipo de movimiento,
- cantidad,
- stock anterior,
- stock nuevo,
- observación,
- fecha.

## Reglas y validaciones actuales
El sistema ya incluye varias validaciones importantes:

- No permite registrar dos usuarios con el mismo nombre de usuario.
- No permite registrar dos categorías con el mismo nombre.
- No permite registrar dos productos con el mismo código.
- No permite realizar una salida si la cantidad excede el stock disponible.
- No permite eliminar una categoría si tiene productos asociados.
- No elimina físicamente un producto si tiene movimientos; en ese caso lo desactiva.
- El acceso a ciertos módulos está protegido por sesión y rol.

## Tecnologías utilizadas
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- jQuery
- DataTables
- BCrypt para manejo de contraseñas

## Estructura actual del proyecto
El repositorio contiene actualmente la siguiente estructura principal:

- `Controllers/` → Controladores del sistema
- `Filtros/` → Filtro de autenticación por sesión
- `Models/` → Entidades y modelos de vista
- `Properties/` → Configuración del proyecto
- `Views/` → Vistas MVC
- `wwwroot/` → Archivos estáticos
- `database/` → Recursos relacionados con base de datos
- `Program.cs` → Configuración principal de la aplicación
- `SistemaStock.csproj` → Archivo del proyecto

## Seguridad y acceso
El sistema trabaja actualmente con:
- autenticación basada en sesión,
- control de acceso por rol,
- contraseñas cifradas con BCrypt.

Los usuarios administradores tienen acceso a módulos de administración como categorías y usuarios, mientras que otros usuarios tienen acceso limitado según el rol almacenado en sesión.

## Base de datos
La aplicación utiliza SQL Server y se conecta mediante Entity Framework Core.  
Para poder ejecutar correctamente el proyecto, es necesario configurar la cadena de conexión en el archivo `appsettings.json` o en la configuración correspondiente del entorno.

## Requisitos para ejecutar el proyecto
- Visual Studio 2022 o superior
- .NET SDK compatible con el proyecto
- SQL Server
- Base de datos correctamente configurada
- Restauración de paquetes NuGet

## Pasos para ejecutar
1. Clonar el repositorio.
2. Abrir el proyecto en Visual Studio.
3. Verificar la cadena de conexión de la base de datos.
4. Restaurar los paquetes NuGet.
5. Compilar el proyecto.
6. Ejecutar la aplicación.

## Estado actual del proyecto
El sistema se encuentra funcional para fines académicos y cubre los módulos principales de un sistema de inventario básico.

Actualmente el repositorio muestra una estructura funcional del proyecto, aunque todavía puede mejorarse su presentación general eliminando archivos generados por compilación y reforzando la documentación del repositorio.

## Mejoras recomendadas a nivel de repositorio
Sin cambiar la lógica del sistema, se recomienda:
- mejorar `.gitignore`,
- eliminar carpetas generadas como `bin/` y `obj/`,
- agregar capturas del sistema al README,
- crear releases para las entregas,
- usar pull requests para organizar cambios,
- mantener commits con mensajes claros.

## Autores
- Eury Antonio Toribio Reyes 2024-0093
- Carlos Samuel Castillo 2024-1306  
- Darlin Adriel De Los Santos Castillo 2023-1738
- Darikson Sanchez  2024-1049
- Engel Espinosa 2023-1058

## Nota
Proyecto desarrollado con fines académicos como parte de la evaluación en el transcurso del cuatrimestre.


