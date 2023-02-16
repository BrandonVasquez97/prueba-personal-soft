# Estructura de los proyectos
Hay dos proyectos, SoftApi que es donde estan todos los servicios api rest y SoftApiTest, donde estan los tests, la base de datos esta en Azure, por lo que no se requiere montarla

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
este endpoint dara como respuesta un json web token, si se rechaza la conexion, en la parte de Settings, deshabilitar la opcion de: Enable SSL certificate verification.
Una vez logueados se colocara el token en la parte de Authorizacion en postman, como tipo Bearer Token en el recuadro, ya con eso se puede utilizar las tres apis menciondas

# Listar polizas
En listar polizas es un metodo GET porque lo que no hay body json, pero si hay un parametro que va en el endpoint:
api/Policy/GetPolicy/{id:int} o tambien api/Policy/GetPolicy/{placa:string}, donde puedes colocar un parametro ya sea el numero o id de la poliza (actualmente en la BD hay dos polizas 1 y 2) o tambien puedes colocar la placa del vehiculo

# Crear polizas
En el endpoint api/Policy/createPolicy Metodo POST puedes crear una poliza con los siguiente body:
{
    "nombre_cliente":"Hugo Perez",
    "cedula_cliente": "561531351",
    "fecha_nacimiento":"1990-01-15",
    "tipo_de_poliza": "Parcial",
    "ciudad_de_cliente":"Cali",
    "direccion_de_cliente": "Cra 15 #45-45",
    "placa":"JXP720",
    "modelo_de_carro": "Kia Rio",
    "tiene_inspeccion":"No"
}
Si ya existe la poliza, no la creara, solo respondera si la poliza esta vigente o vencida

# Renovar Polizas
Si la poliza esta vencida se puede renovar con el endpoint api/Policy/renewPolicy metodo POST y el siguiente body:
{
    "tipo_de_poliza": "Parcial",
    "ciudad_de_cliente":"Barranquilla",
    "direccion_de_cliente": "Cra 45 #80-40",
    "placa":"KFP412"
}
Si ya existe la poliza, la renovara en caso de estar vencida, si no solo respondera que la poliza esta vigente o que no existe

# Tests
Para ejecutar los tests sera necesario detener el proyecto de las apis y pasar a la carpeta SoftApitest, una vez ahi se pueden ejecutar los tests con el comando: dotnet test

