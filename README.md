# CdbCalculator

## Requisitos para execução do projeto

Antes de iniciar, certifique-se de que possui as seguintes versões instaladas:

- **Node.js**: v18.19.1 ou superior
- **npm**: 10.8.2 ou superior
- **Angular**: versão 19
- **.NET**: versão 8 (SDK 8.0.406)

## Passos para configuração e execução

### 1. Instalação das dependências

1. **Clone ou extraia o projeto**.
2. **Acesse a pasta raiz do projeto pelo terminal** e execute os seguintes comandos:
   ```sh
   cd CdbCalculator.Frontend
   npm install
   ```

### 2. Configuração do Frontend

1. **Acesse a pasta do frontend**:

   ```sh
   cd CdbCalculator.Frontend
   ```

2. **Instale as dependências e inicie o servidor Angular**:

   ```sh
   npm install
   ng serve
   ```

3. **Acesse a aplicação no navegador** em [http://localhost:4200/](http://localhost:4200/).

### 3. Configuração da Solução .NET

1. **Volte para a pasta raiz e abra a solução no Visual Studio**:

   ```sh
   cd ..
   ```

2. **Abra o arquivo `CdbCalculator.sln` no Visual Studio 2022**.
3. **Faça o rebuild da solução**.

### 4. Execução do Projeto

1. **Inicie a API pelo Visual Studio**:
   - Abra a solução `CdbCalculator.sln`.
   - Pressione `F5` para iniciar a aplicação com depuração.
   - Acompanhe a execução da API diretamente pelo Visual Studio.

2. **Inicie o frontend separadamente** (caso ainda não tenha iniciado):
   ```sh
   cd CdbCalculator.Frontend
   ng serve
   ```

Se encontrar problemas durante a execução, verifique se todas as dependências foram instaladas corretamente e se o rebuild da solução foi realizado com sucesso.

