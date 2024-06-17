# Ejemplo de API con .Net Framework

1.- Autenticacion JWT con owin
2.- Inyección de dependencias con UnityContainer
3.- Conexión a SQL Server con EntityFramework (ORM)
4.- Validaciones con FluentValidations
5.- Auto mapeo de DTOs con AutoMapper
6.- Uso de Base de datos SQL Server Code First y Migraciones con EntityFramework

# Pasos para ejecutar el proyecto

1.- Clonar el repositorio
2.- Restaurar los paquetes del proyecto
3.- Ejecutar el comando Update-database en la consola de administrador de paquetes para que se apliquen las migraciones
4.- Ejecutar el proyecto

# Rutas Disponibles

# Sin Autenticación
1.- Autenticación -- api/Auth -- POST

# Necesita Autenticación JWT

# Usuarios
2.- Todos los usuarios -- api/User ------- GET
3.- Usuario por id     -- api/User/{id} -- GET
4.- Crear usuario      -- api/User ------- POST
5.- Actualizar usuario -- api/User ------- PUT
6.- Actualizar usuario -- api/User ------- PATCH

# Productos
2.- Todos los productos -- api/Products ------- GET
3.- Producto por id     -- api/Products/{id} -- GET
4.- Crear producto      -- api/Products ------- POST
5.- Actualizar producto -- api/Products ------- PUT
6.- Actualizar producto -- api/Products ------- PATCH




