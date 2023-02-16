# Estructura de los proyectos
Hay dos proyectos, SoftApi que es donde estan todos los servicios api rest y SoftApiTest, donde estan los tests

# Pasos para levantar los servicios de Api rest
En la ruta de los dos proyectos se accede a la carpeta SoftApi, en cmd o terminal se puede acceder con cd SoftApi, después simplemente en la terminal se coloca el comando: dotnet run, para compilar y levantar el proyecto

# Explicacion Apis del proyecto
en el archivo PersonalSoft.postman_collection.json se encuentra el proyecto en postman de las apis, se encuentran 4 apis:

# Login
Para tener acceso a las demás apis es necesario loguearse con el endpoint api/User/login, dejo las credenciales con las que podran loguearse:
{
    "user_name":"bvasquez",
    "user_password": "1234"
}
este endpoint dara como respuesta un json web token, si se rechaza la conexion, en la parte de Settings, deshabilitar la opcion de: Enable SSL certificate verification

# Listar polizas, crear polizas y renovar polizas
Una vez logueados se colocara el token en la parte de Authorizacion en postman, como tipo Bearer Token en el recuadro, ya con eso se puede utilizar las tres apis menciondas

# Tests
Para ejecutar los tests sera necesario detener el proyecto de las apis y pasar a la carpeta SoftApitest, una vez ahi se pueden ejecutar los tests con el comando: dotnet test

