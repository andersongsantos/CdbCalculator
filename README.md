# CdbCalculator

## Requisitos para execução do projeto

Antes de iniciar, certifique-se de que possui as seguintes versões instaladas:

- **Node.js**: v18.19.1 ou superior
- **npm**: 10.8.2 ou superior
- **Angular**: versão 19
- **.NET**: versão 8
- **SDK .NET**: versão 8.0.406

## Passos para configuração e execução

### 1. Instalação das dependências

1. **Clone ou extraia o projeto**.
2. **Acesse a pasta raiz do projeto pelo terminal** e execute os seguintes comandos:
   ```sh
   npm install
   ```
   Esta pasta contém o `gulpfile` e os pacotes necessários para subir o servidor Angular e a API em ambiente de desenvolvimento. Estes arquivos devem ser ignorados em uma eventual publicação para produção.

### 2. Configuração do Frontend

1. **Acesse a pasta do frontend**:

   ```sh
   cd CdbCalculator.Frontend
   ```

2. **Instale as dependências e faça o build**:

   ```sh
   npm install
   ng build
   ```

### 3. Configuração da Solução .NET

1. **Volte para a pasta raiz e abra a solução no Visual Studio**:

   ```sh
   cd ..
   ```

2. **Abra o arquivo `CdbCalculator.sln` e faça o rebuild da solução**.

### 4. Execução do Projeto

1. **Para rodar a aplicação, execute o seguinte comando na pasta raiz**:

   ```sh
   npm start
   ```

2. **Aguarde o browser abrir automaticamente no endereço** [http://localhost:4200/](http://localhost:4200/).

   - Caso a página não carregue automaticamente, atualize-a manualmente.

### 5. Depuração e Acompanhamento no Visual Studio

Se desejar debugar e acompanhar a execução do projeto, é possível fechar a janela da API e monitorar pelo **Visual Studio 2022**, pois o projeto foi desenvolvido em **.NET 8**.

Para executar no Visual Studio:

1. **Abra o arquivo `CdbCalculator.sln` no Visual Studio 2022**.
2. **Pressione `F5` para iniciar a aplicação com depuração**.
3. **Acompanhe a execução da API diretamente pelo Visual Studio**.

---

Se encontrar problemas durante a execução, verifique se todas as dependências foram instaladas corretamente e se o rebuild da solução foi realizado com sucesso.

