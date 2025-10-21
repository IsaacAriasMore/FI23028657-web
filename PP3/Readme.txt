# Práctica Programada 3 - SC-701

## Información del estudiante
**Nombre**:Isaac Arias Morera
**Carné**: FI23028657

## Comandos utilizados
```
# Crear el solution
dotnet new sln -n PP3Solution

# Crear el proyecto Minimal API
dotnet new webapi -n PP3Project -minimal

# Agregar el proyecto al solution

dotnet sln PP3Solution.sln add PP3Project/PP3Project.csproj

# Ejecutar el proyecto

dotnet run

# Compilar el proyecto

dotnet build

```

## Recursos consultados

https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis

Prompts utilizados durante el desarrollo

Prompt: "¿Cómo defino un parámetro opcional desde Headers en Minimal API con valor por defecto?"
Respuesta: Se usa [FromHeader(Name = "xml")] bool xml = false donde el = false establece el valor predeterminado.

Prompt: "¿Por qué mi Minimal API no muestra la interfaz de Swagger al ejecutarse?"
Respuesta: Debes agregar app.UseSwagger() y app.UseSwaggerUI() en Program.cs, y asegurarte de haber agregado los servicios con builder.Services.AddEndpointsApiExplorer() y AddSwaggerGen().



### ¿Qué ventajas y desventajas observa con el Minimal API comparado con Controllers?

**Ventajas:**
 Los Minimal APIs eliminan la necesidad de crear clases de controladores, atributos de ruta y métodos de acción. Todo se define de manera más directa y concisa en el archivo Program.cs, lo que resulta en menos líneas de código para lograr la misma funcionalidad.

Puedes llegar a  crear y desplegar una API funcional en cuestión de minutos. lo cual es bueno para prototipos, pruebas de concepto o demos rápidas.

**Desventajas:**

 Cuando la API crece con muchos endpoints, tener todo en Program.cs puede volverse difícil de mantener.

Los controladores ofrecen funcionalidades como filtros de acción, model binding avanzado y mas cosas que en Minimal APIs, muchas de estas características deben implementarse manualmente.


