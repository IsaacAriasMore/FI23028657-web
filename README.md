Resumen
-Crear una aplicación para Consola.
-Implementar dos métodos: SumFor y SumIte.
-Validar ascendentemente desde 1 hasta Max con cada método hasta encontrar el último sum válido.
-Validar descendentemente desde Max hasta 1 con cada método hasta encontrar el primer sum válido.
-Mostrar resultados en la Consola.



# PP1 – SC-701: Suma de números naturales

 IsaacAriasMorera 

 FI23028657


---

Comandos `dotnet` utilizados (CLI)
```bash
1 Crear carpeta raíz de la práctica

mkdir PP1

2 Crear Solution
dotnet new sln -n PP1

3 Crear proyecto de consola (.NET 8)
dotnet new console -o PP1.App -f net8.0

4 Agregar el proyecto a la Solution
dotnet sln PP1.sln add PP1.App/PP1.App.csproj

5 Ejecutar
cd PP1.App
dotnet run



promts usados :

“Me falla dotnet new console -n PP1.app -f net.8.0. Qué está mal?

Respuesta:

El moniker correcto es net8.0 (sin punto intermedio). Comando correcto:
dotnet new console -n PP1.App -f net8.0

----

“Valida si mi estructura de carpetas está bien y si debo renombrar PP1.app.”
Respuesta (resumen):
Estructura correcta (PP1.sln, PP1.App/ con .csproj y Program.cs). Recomendación de estilo: usar PP1.App (mayúscula) por convención.



