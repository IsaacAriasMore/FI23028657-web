# Práctica Programada 3 - SC-701

##estudiante
**Nombre**:Isaac Arias Morera
**Carné**: FI23028657


## Comandos utilizados


# Crear carpeta PP4
mkdir PP4
cd PP4

# Crear el solution
dotnet new sln -n BooksManager

# Crear el proyecto de consola
dotnet new console -n BooksApp

# Agregar el proyecto al solution
dotnet sln add BooksApp/BooksApp.csproj

# Entrar a la carpeta del proyecto
cd BooksApp


Las respuestas a las siguientes preguntas:

¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?

Creo que Code First funcionaría bastante bien con bases de datos tipo NoSQL como MongoDB porque esta base de datos ya trabaja con documentos JSON que son muy parecidos a los objetos de C#. 


Database First sería más problemático porque las bases NoSQL no tienen esquemas fijos como SQL, entonces no hay mucho de dónde generar los modelos.



¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué? Por ejemplo: Pipe (|) con extensión .pipe.


Yo usaría el punto y coma (;)
Lo escogí porque es un carácter que no aparece tanto en textos normales, y además
ya se usa bastante en Europa donde escriben números decimales con coma (como 3,14). Excel también lo reconoce sin problema.
Ejemplo:
Autor;Titulo;Año

La ventaja es que funciona bien con datos que tienen comas, como los nombres en formato "Apellido, Nombre", y es fácil de procesar