
# Ejemplo de API con .Net Framework

Este proyecto demuestra cómo crear una API utilizando .NET Framework, implementando varias prácticas y tecnologías modernas.

# Caracteristicas

1. Autenticacion JWT con owin
2. Inyección de dependencias con UnityContainer
3. Conexión a SQL Server con EntityFramework (ORM)
4. Validaciones con FluentValidations
5. Auto mapeo de DTOs con AutoMapper
6. Uso de Base de datos SQL Server Code First y Migraciones con EntityFramework
7. BCrypt.Net-Core para el encritado de las contraseñas

# Pasos para ejecutar el proyecto

1. Clonar el repositorio
2. Restaurar los paquetes del proyecto
3. Ejecutar el comando Update-database en la consola de administrador de paquetes para que se apliquen las migraciones
4. Ejecutar el proyecto

# Rutas Disponibles

# Sin Autenticación
1. Autenticación -- api/Auth -- POST

# Necesita Autenticación JWT

# Usuarios
1. Todos los usuarios -- api/User ------- GET
2. Usuario por id     -- api/User/{id} -- GET
3. Crear usuario      -- api/User ------- POST
4. Actualizar usuario -- api/User ------- PUT
5. Actualizar usuario -- api/User ------- PATCH

# Productos
1. Todos los productos -- api/Products ------- GET
2. Producto por id     -- api/Products/{id} -- GET
3. Crear producto      -- api/Products ------- POST
4. Actualizar producto -- api/Products ------- PUT
5. Actualizar producto -- api/Products ------- PATCH

# Notas adicionales
1. Asegúrate de configurar correctamente tu conexión a la base de datos en el archivo web.config.
2. Este proyecto utiliza migraciones de Entity Framework, por lo que cualquier cambio en el modelo de datos debe manejarse mediante migraciones.
3. Las validaciones se realizan utilizando FluentValidation, asegurando que los datos enviados a la API cumplan con los requisitos definidos.

¡Gracias por utilizar este ejemplo de API con .NET Framework!