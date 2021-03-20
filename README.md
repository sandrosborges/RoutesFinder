# Aplicação: RoutesFinder

#### Pré-requisitos: ####

##### .NET Core 2.2 (https://dotnet.microsoft.com/download/dotnet/2.2)

# Rota de Viagem #

Um turista deseja viajar pelo mundo pagando o menor preço possível independentemente do número de conexões necessárias.
Vamos construir um programa que facilite ao nosso turista, escolher a melhor rota para sua viagem.

Para isso precisamos inserir as rotas atravÃ©s de um arquivo de entrada.

## Input Example ##
```csv
GRU,BRC,10
BRC,SCL,5
GRU,CDG,75
GRU,SCL,20
GRU,ORL,56
ORL,CDG,5
SCL,ORL,20
```

Execução do programa
A inicializacao do teste se dará por linha de comando onde o primeiro argumento é o arquivo com a lista de rotas inicial.

**Exemplo**:
RoutesFinder.app.exe input-routes.csv

## Execução do projeto APP (console): ##

$  cd .\RoutesFinder\RoutesFinder.app\
$  dotnet  run ..\input-route.csv


Tela exibida: 

 ![image](https://user-images.githubusercontent.com/38473707/111728048-d8220e00-884a-11eb-9846-665cc658e86c.png)

1. Listar Rotas Disponiveis:

![image](https://user-images.githubusercontent.com/38473707/111728159-128bab00-884b-11eb-8d49-e2d7acacb92b.png)




## Interface Rest ##


### End points: ###

GET
​/api​/RouteFinder​/routes

GET
​/api​/RouteFinder​/routes​/best

POST
​/api​/RouteFinder​/routes


## Swagger: ##

**url**:  https://localhost:5001/swagger/index.html

**yaml**: https://github.com/sandrosborges/RoutesFinder/blob/master/RoutesFinder.api/RoutesFinder_swagger.yaml

![image](https://user-images.githubusercontent.com/38473707/111728686-27b50980-884c-11eb-81e5-bcf673f1ec31.png)


## Execução do projeto API: ##

$  cd .\RoutesFinder\RoutesFinder.api\
$  dotnet run

## Solução para busca da melhor rota: ##

1. Tendo as rotas armazenadas a partir do arquivo CSV, faço uma busca e seleciono todas cuja origem é igual a origem buscada.
Ex.: GRU-CDG
```
GRU,BRC,10
GRU,CDG,75
GRU,SCL,20
GRU,ORL,56
```

2. Percorro estas rotas selecionadas, tentando atingir o destino solicitado. Quando consigo chegar ao destino, 
3. armazeno a rota candidata se o custo for menor (ou igual) que a anterior.

```
Ex.: Rota1: GRU-BRC-SCL-ORL-CDG, custo 40.
     Rota2: GRU-CDG, custo 75
     Rota3: GRU-SCL-ORL-CDG, custo 55
 ```    
     
Neste exemplo, eu armazenei a Rota1 e descartei as rotas Rota2 e Rota3;


### Melhorias necessárias: ###

- Implementação de Logs
- Testes unitários mais abrangentes
- Exceções customizadas (custom Exceptions)
- Refactoring (inclusão de mais interfaces, tratamento de erros, parametrização dos textos na aplicação)
- Criação de presenters para API (DTO´s)
- Melhoria nas parametrizações da aplicação








