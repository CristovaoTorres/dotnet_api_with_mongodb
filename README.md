
# DotNetExample_API

Bem-vindo ao projeto **DotNetExample_API**! Este é um exemplo de API RESTful construído com ASP.NET Core e MongoDB. O objetivo deste projeto é fornecer uma base para novos programadores aprenderem a integrar uma API em .NET com o MongoDB, aplicando boas práticas de arquitetura, padronização de respostas e injeção de dependências.

---

## 📝 Índice

1. [Visão Geral do Projeto](#-visão-geral-do-projeto)
2. [Configuração do MongoDB Localmente](#-configuração-do-mongodb-localmente)
3. [Como Executar o Projeto Localmente](#-como-executar-o-projeto-localmente)
4. [Estrutura do Projeto](#-estrutura-do-projeto)
5. [Padrão de Respostas da API](#-padrão-de-respostas-da-api)
6. [Uso do BaseRepository para CRUD Genérico](#-uso-do-baserepository-para-crud-genérico)
7. [Endpoints Disponíveis](#-endpoints-disponíveis)

---

## 🖥️ Visão Geral do Projeto

Este projeto é uma API para gerenciar produtos em um banco de dados MongoDB. Ele possui operações CRUD (Criar, Ler, Atualizar e Deletar) para produtos. O projeto é organizado em camadas, separando responsabilidades e implementando boas práticas de injeção de dependências.

### Principais Tecnologias Utilizadas

- **.NET 6 / .NET 8**: Framework para construção de aplicações.
- **MongoDB**: Banco de dados NoSQL orientado a documentos.
- **MongoDB.Driver**: Biblioteca oficial do MongoDB para .NET.
- **Swagger**: Para documentação interativa da API.

---

## 🛠️ Configuração do MongoDB Localmente

Para que o projeto funcione corretamente, você precisa ter o MongoDB instalado e em execução em sua máquina.

### 1. Instalando o MongoDB

1. **Baixe o MongoDB Community Server**: [https://www.mongodb.com/try/download/community](https://www.mongodb.com/try/download/community)
2. **Instale o MongoDB**:
   - **Windows**: Execute o instalador `.msi` e siga as instruções. Marque a opção para instalar como serviço para que o MongoDB inicie automaticamente.
   - **macOS e Linux**: Siga as instruções específicas do seu sistema operacional. No macOS, você pode instalar via Homebrew (`brew install mongodb-community`).

### 2. Configurando o MongoDB

Após instalar o MongoDB, ele deve ser iniciado como serviço:
   - **Windows**: O MongoDB deve iniciar automaticamente.
   - **macOS e Linux**: Abra um terminal e execute `mongod`.

### 3. Instale o MongoDB Compass (Opcional)

O MongoDB Compass é uma interface gráfica que facilita a visualização dos dados:
1. **Baixe o MongoDB Compass**: [https://www.mongodb.com/try/download/compass](https://www.mongodb.com/try/download/compass)
2. **Conecte ao MongoDB local** usando a URL padrão:
   ```plaintext
   mongodb://localhost:27017
   ```

Agora o MongoDB estará pronto para ser usado pelo projeto.

---

## 🚀 Como Executar o Projeto Localmente

Siga os passos abaixo para clonar o repositório e executar o projeto em sua máquina.

### 1. Clone o Repositório

No terminal, clone o projeto usando o comando:

```bash
git clone https://github.com/seu-usuario/DotNetExample_API.git
cd DotNetExample_API
```

### 2. Configure a Conexão com o MongoDB

Abra o arquivo `appsettings.json` no diretório raiz do projeto e insira a string de conexão para o MongoDB:

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017"
  },
  "MongoDbDatabase": "DotNetExampleDatabase"
}
```

- `MongoDb`: String de conexão com o MongoDB local.
- `MongoDbDatabase`: Nome do banco de dados que será utilizado pelo projeto.

### 3. Execute o Projeto

No terminal, execute os seguintes comandos para restaurar as dependências e iniciar o servidor:

```bash
dotnet restore
dotnet run
```

### 4. Acesse a Documentação da API (Swagger)

Com o projeto em execução, abra seu navegador e vá para o seguinte endereço para acessar a documentação interativa do Swagger:

```plaintext
https://localhost:5001/swagger
```

---

## 📂 Estrutura do Projeto

Abaixo está uma visão geral da estrutura do projeto:

```plaintext
DotNetExample_API
│
├── DotNetExample_API.CrossCutting      # Configurações de injeção de dependências
├── DotNetExample_API.Business          # Lógica de negócios (camada de serviços)
├── DotNetExample_API.Repository        # Repositório de acesso ao MongoDB
├── DotNetExample_API.Domain            # Modelos de dados (domínio)
└── Controllers                         # Endpoints da API
```

### Principais Componentes

- **Controllers**: Camada de apresentação, onde estão os endpoints da API.
- **Business**: Camada de serviços, onde estão as regras de negócio.
- **Repository**: Camada de repositório, onde são implementadas as operações com o MongoDB.
- **CrossCutting**: Configuração de injeção de dependência para registrar os serviços e repositórios.

---

## 📦 Padrão de Respostas da API

Para garantir uma resposta consistente em todos os endpoints da API, usamos o padrão `BaseResponse`. Esse padrão facilita o tratamento de erros, validações e informações adicionais. Todas as respostas são padronizadas para retornar uma estrutura unificada, que inclui:

- **Dados (Data)**: O conteúdo retornado pela operação.
- **Erros (Errors)**: Uma lista de erros, caso a operação não seja bem-sucedida.
- **Mensagens adicionais**: Informações auxiliares sobre o status da operação.

O uso do `BaseResponse` torna o consumo da API mais previsível e facilita a integração com outras aplicações.

---

## ⚙️ Uso do BaseRepository para CRUD Genérico

O projeto utiliza uma classe `BaseRepository<T>` para fornecer operações CRUD comuns para as entidades, simplificando o repositório de cada entidade específica. Essa abordagem permite que outros repositórios (como `ProductRepository`) herdem os métodos CRUD e apenas adicionem operações específicas, se necessário.

### Classe BaseRepository

A classe `BaseRepository<T>` fornece os métodos básicos de CRUD:

- `GetAllAsync()`: Obtém todos os documentos.
- `GetByIdAsync(id)`: Busca um documento pelo ID.
- `CreateAsync(entity)`: Insere um novo documento.
- `UpdateAsync(id, entity)`: Atualiza um documento existente.
- `DeleteAsync(id)`: Exclui um documento.

Essa classe é genérica, o que significa que pode ser reutilizada para várias entidades, reduzindo duplicação de código e facilitando a manutenção.

### Exemplo de Repositório Específico

`ProductRepository` é um exemplo de repositório que herda `BaseRepository<Product>`. Com essa herança, `ProductRepository` tem acesso imediato aos métodos CRUD, mas pode definir métodos adicionais específicos para `Product`, se necessário.

---

## 📋 Endpoints Disponíveis

Aqui estão os principais endpoints disponíveis na API:

### 1. Obter todos os produtos

**GET** `/api/product`

Retorna uma lista de todos os produtos cadastrados.

### 2. Obter um produto por ID

**GET** `/api/product/{id}`

Parâmetros:
- `id` (string): ID do produto.

Retorna os detalhes de um produto específico.

### 3. Criar um novo produto

**POST** `/api/product`

Body:
```json
{
  "name": "string",
  "price": 0.0,
  "quantity": 0
}
```

Cria um novo produto no banco de dados.

### 4. Atualizar um produto

**PUT** `/api/product/{id}`

Parâmetros:
- `id` (string): ID do produto.

Body:
```json
{
  "name": "string",
  "price": 0.0,
  "quantity": 0
}
```

Atualiza as informações de um produto existente.

### 5. Deletar um produto

**DELETE** `/api/product/{id}`

Parâmetros:
- `id` (string): ID do produto.

Remove um produto do banco de dados.

---

## 🤝 Contribuições

Contribuições são bem-vindas! Se você encontrar problemas ou tiver sugestões para melhorar o projeto, sinta-se à vontade para abrir uma issue ou enviar um pull request.

---

## 📄 Licença

Este projeto é licenciado sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.
