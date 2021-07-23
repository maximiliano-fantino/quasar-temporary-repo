# Api QF (Operación Fuego Quasar)
## Disclaimer
Esta API tiene como objetivo cumplir con el challenge propuesto, que solicita principalmente la implementación de dos métodos:
* GetLocation(distances ...float32) : (x,y) float32
* GetMessage(messages ...[]string) : (msg) string

Para llevar adelante la implementación de **GetLocation** tuve que investigar y me encontré con conceptos matemáticos que conocía, pero no recordaba.
De todas maneras, seguí adelante.

Implementé la **trilateración** para obtener la localización. 

Este método permite, a partir del conocimiento de 3 puntos (satélites) y 3 distancias, utilizar dicha distancia como el radio para dibujar un círculo ficticio y así obtener la ubicación de las posibles zonas de contacto.

Ayudado por la teoria encontrada [aquí](http://paulbourke.net/geometry/circlesphere) y otro tanto por los recusos casi inagotables de internet, logré llegar al objetivo.

Luego, para probar la teoría y obtener puntos reales utilicé la herramienta [desmos](https://www.desmos.com/calculator/bnk9uih1gl) que me permitió, en base a puntos y distancias, dibujar los círculos y ver sus puntos de intersección. 

Concretamente, usé la herramienta para establecer si una posicion era correcta o no, como se ve en la siguiente imagen:

![Alt text](resources/1.png?raw=true "Circulos")

### Requisitos
Para ejecutar la aplicación se necesitará:
* Visual Studio, de preferencia 2017 en adelante 
* ASP.Net Core 2.1

### Utilidades
Para realizar las pruebas se utilizaron los siguientes recursos:
* [Insomnia Rest](https://insomnia.rest/)
* [Calculadora interesecciones](https://www.desmos.com/calculator/bnk9uih1gl?lang=es)

---
### Instalación y ejecución
* Clonar el repositorio.
* #### Ejecutar desde Visual Studio
  * Abrir el archivo "\ApiQF\ApiQF.sln"
  * Ctrl + F5 para buildear y correr el programar
  * Esto puede demorar ya que debe descargar las dependecias de nuget
* #### Ejecutar desde la consola
  * Ejecutar en una consola CMD: dotnet run --project .\ApiQF\ApiQF.csproj
  
* Una vez ejecutado, dirigirse a https://localhost:44365/swagger para acceder a la documentación base de la API

### Test
* Desde una consola ejecutar el comando **dotnet test .\ApiQF.Tests\ApiQF.Tests.csproj** para ejecutar los test
* ![Alt text](resources/6.png?raw=true "Test")
---
# Api
##### Dentro de Swagger está la información de la API
![Alt text](resources/2.png?raw=true "Swagger")

##### Aquí se detallan los endpoints solicitados junto con la estructura esperada y la que será devuelta.

![Alt text](resources/3.png?raw=true "GetTopSecretSplit")
![Alt text](resources/5.png?raw=true "PostTopSecretSplit")
![Alt text](resources/4.png?raw=true "GetTopSecret")

# Nivel 2 cloud computing

Para cumplimentar con el nivel dos y hostear la app en un cloud computing libre elegí **Google App Engine Flex**

### Los endpoints son los siguientes:
* [Swagger](https://quasar-fire-320318.appspot.com/swagger)
* [Top Secret](https://quasar-fire-320318.appspot.com/api/TopSecret) [POST]
* [Top Secret Split](https://quasar-fire-320318.appspot.com/api/topsecret_split) [GET, POST]

**Limitar las peticiones para evitar cargos**

#### Adicionalmente se sube a Azure **Asp.Net Core 3.1**
* [Swagger](https://apiqf20210723113500.azurewebsites.net/swagger/)
* [Top Secret](https://apiqf20210723113500.azurewebsites.net/api/TopSecret) [POST]
* [Top Secret Split](https://apiqf20210723113500.azurewebsites.net/api/topsecret_split) [GET, POST]
