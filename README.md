# DESAFIO ITERUP

## EXECUTANDO A API

- Dentro da pasta Desafio.Api
  ```
  dotnet build ou dotnet build --no-restore
  ```
  ```
  dotnet ./bin/Debug/netcoreapp3.1/Desafio.Api.dll \
      > Desafio.Api.log &
  ```

### Rotas
- Workflow (GET)
    ```
    https://localhost:5001/desafio/workflow
    ```

- Login (POST)

  ```
  https://localhost:5001/desafio/login
  ```

  Informar o _username_ e _password_

  > Usuário e senha padrão _desafio_

  __Para efeturar cadastro e consulta de Etapa e Resposta deve estar logado__

- Etapa

  #### Cadastrar (POST)
    ```
    https://localhost:5001/desafio/etapas
    ```
    Exemplo de body
    ```
    {
        "tipoEtapa": 1,
        "textoEtapa": "Bom dia, como vai!",
        "numProxEtapa": 2
    }
    ```
    > tipoEtapa _1 - Dialogo_ ou _0 - Pergunta_

- Resposta
  ### Cadastrar (POST)
    ```
    https://localhost:5001/desafio/etapas
    ```
    Exemplo de body
    ```
    {
        "numEtapa": 2,
        "legenda": "Sim",
        "numProxEtapa": 3
    }
    ```

## EXECUTANDO FRONT

- Dentro da pasta Desafio.Workflow

  Antes de tudo instalar as dependência
  ```
  npm install
  ```
  Depois executar o _serve_
  ```
  ng serve
  ```
- No navegador acessar
  ```
  http://localhost:4200/
  ```
