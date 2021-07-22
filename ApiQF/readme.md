# Api QF (Operacion Fuego Quasar)
## Descripcion
Esta API tiene como objetivo cumplir con el challenge.
Este challenge solicita principalmente la implementacion de dos metodos:
* GetLocation(distances ...float32) : (x,y)float32
* GetMessage(messages ...[]string) : (msg) string

Para llevar adelante la implementacion de GetLocation tuve que investigar y me encontre con conceptos matematicos conocidos pero que tengo algo oxidados.

Sin embargo lo lleve adelante.
La forma de obtener la localizacion que implemente fue mediante la trilateracion. 

Esto es que, mediante el conocimiento de 3 puntos (satelites) y 3 distancias podria utilizar esta distancia como el radio para dibujar un circulo ficticio y con ello obtener la ubicacion de las posibles zonas de contacto.

Ayudado por la teoria encontrada aca: http://paulbourke.net/geometry/circlesphere

Y otro tanto por los recusos casi inagotables de internet logre llegar al objetivo.
Mentiria si dijiera que el calculo es mio 100%

Luego, para probar y obtner puntos reales utilice esta herramienta que me permite en base a puntos y distancia dibujar los circulos y ver sus puntos de interseccion
https://www.desmos.com/calculator/bnk9uih1gl

Esto fue usado para establecer si una posicion era correcta o no.
![Alt text](../resources/1.png?raw=true "Circulos")

### Requisitos
Para ejecutar la aplicacion se necesitara:
* Visual Studio, de preferencia 2017 en adelante 
* ASP.Net Core 2.1

### Utilidades
Para realizar las pruebas se utilizaron los siguintes recursos:
* [Insomnia Rest](https://insomnia.rest/)
* [Calculadora Interesecciones](https://www.desmos.com/calculator/bnk9uih1gl?lang=es)

---
### Instalacion y Ejecucion
* Clonar el repositorio.
* #### Ejecutar desde Visual Studio
  * Abrir el archivo "\ApiQF\ApiQF.sln"
  * Ctrl + F5 para buildear y correr el programar
  * En este punto puede que demore ya que debe descargar las dependecias de nuget
* ### Ejecutar desde la consola
  * Ejecutar en una consola CMD: dotnet run --project .\ApiQF\ApiQF.csproj
  
* Una vez ejecutado, dirigirse a https://localhost:44365/swagger para acceder a la documentacion base de la API
---
# Api
##### Dentro de Swagger podemos ver esta informacion de la API
![Alt text](../resources/2.png?raw=true "Swagger")
Aqui podemos ver que se detallan los endpoint solicitados junto con la estructura esperada y la que sera devuelta
![Alt text](../resources/3.png?raw=true "GetTopSecretSplit")
![Alt text](../resources/5.png?raw=true "PostTopSecretSplit")
![Alt text](../resources/4.png?raw=true "GetTopSecret")

