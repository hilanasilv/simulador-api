<h1> Simulator Télos Nível 9 - APIs RESTful 🚀 </h1>

Esse projeto consiste em um Sistema de Gestão de Pizzaria, desenvolvido com ASP.NET Web API para consolidar conhecimentos em **APIs RESTful**, autenticação, autorização, testes unitários e documentação com Swagger. 

O sistema permite gerenciar sabores de pizzas, calcular o tempo de preparo de pedidos e proteger as APIs com autenticação baseada em **JWT (JSON Web Token)**.

---

## 🎯 **Objetivo**
O objetivo deste projeto é criar um sistema de gestão para uma pizzaria com funcionalidades de:
1. Cadastro de sabores de pizza.
2. Cálculo do tempo de preparo de um pedido.
3. Proteção de APIs com autenticação e autorização.
4. Documentação das APIs utilizando Swagger.
5. Garantia da qualidade do código com testes unitários.

---

## ⚙️ **Funcionalidades**
### 1. **API de Sabores de Pizza**
Permite gerenciar (CRUD) os sabores de pizza armazenados no banco de dados.

#### Atributos de uma pizza:
- `id`: Identificador único.
- `sabor`: Nome do sabor (Ex: Portuguesa).
- `tempoPreparo`: Tempo necessário para preparar o sabor (em minutos).

#### Endpoints:
| Método | Rota              | Descrição                                | Autorização       |
|--------|-------------------|------------------------------------------|-------------------|
| POST   | `/api/pizza`       | Adiciona um novo sabor                 | **Cozinheiro**    |
| GET    | `/api/pizza`       | Busca todos os sabores cadastrados     | **Garçom** e **Cozinheiro** |
| PUT    | `/api/pizza/{id}`  | Atualiza um sabor existente            | **Cozinheiro**    |
| GET    | `/api/pizza/{id}`  | Busca um sabor específico através do ID| **Garçom** e **Cozinheiro** |
| DELETE | `/api/pizza/{id}`  | Deleta um sabor                        | **Cozinheiro**    |
| GET    | `/api/pizza/sabores`| Obtém sabores de pizza por nome       | **Garçom** e **Cozinheiro** |


---

### 2. **API de Cálculo de Tempo de Duração de Pedido**
Recebe uma lista de sabores e retorna:
- O tempo total de preparo do pedido.
- Mensagem informando se algum sabor não está disponível.

#### Endpoints:
| Método | Rota                     | Descrição                          | Autorização       |
|--------|--------------------------|------------------------------------|-------------------|
| POST   | `/api/pizza/tempo-pedido` | Calcula o tempo de preparo do pedido | **Garçom** e **Cozinheiro** |

---

### 3. **Autenticação e Autorização**
- **Autenticação**: Baseada em JWT.
- **Autorização**:
  - **Cozinheiro**: Acesso completo às APIs.
  - **Garçom**: Acesso limitado às APIs de busca e cálculo de tempo de pedido.

#### Endpoints:
| Método | Rota           | Descrição                          |
|--------|----------------|------------------------------------|
| POST   | `/api/login`    | Realiza login e retorna um token JWT |

---

### 4. **Testes Unitários**
Dois testes unitários foram criados utilizando NUnit para validar o cálculo do tempo total de duração de um pedido.

---

### 5. **Documentação com Swagger**
Todas as APIs estão documentadas com Swagger, incluindo:
- Descrições detalhadas dos endpoints.
- Respostas possíveis (200, 400, 401, 403, 404.).
- Proteção por autenticação JWT.

#### Acesso:
- Após rodar a aplicação, a documentação estará disponível em `http://localhost:5000/swagger/index.html`.

## 🛠️ **Tecnologias Utilizadas**
- **ASP.NET Web API**
- **C#**
- **MySQL** como banco de dados
- **Swagger** para documentação
- **JWT** para autenticação e autorização
- **NUnit** para testes unitários

## 🚀 **Como Executar o Projeto**
### Pré-requisitos
1. .NET
2. MySQL configurado para conexão com o projeto.
3. Configuração do arquivo `appsettings.json` para o banco de dados.

### Passos
1. Clone o repositório:
   ```bash
   git clone https://github.com/hilanasilv/simulador-api

   cd PizzariaAPI
   ```
2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```

3. Configure o banco de dados:
   - Crie um banco no MySQL.
   - Execute o script SQL localizado em `Database/script-banco-de-dados.sql`.
  
4. Inicie a aplicação:
   ```bash
   dotnet run
   ```

5. Testes:
Os testes unitários garantem a precisão do cálculo do tempo de preparo. Para executá-los:
    ```bash
    dotnet test
    ```

6. Acesse o Swagger para testar as APIs em :
    `http://localhost:5000/swagger/index.html`.


# 

<p align=center>Desenvolvido por Nayla Hilana</p>


