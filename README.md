# ViaCEP API

API backend em **ASP.NET Core 6** para consulta de endereços pelo CEP, consumindo a API pública [ViaCEP](https://viacep.com.br). Protegida por token único via header `X-Api-Key`.

---

## Estrutura de pastas

```
ViaCEP/
├── Controllers/          → Recebe requisições HTTP e retorna respostas
├── Services/             → Regras de negócio e validações
├── Interfaces/           → Contratos (SOLID / inversão de dependência)
├── Infrastructure/       → Comunicação com a API ViaCEP externa
├── Middleware/           → Validação do token de acesso (X-Api-Key)
├── Models/
│   ├── Entities/         → Entidade de domínio (Endereco)
│   └── DTOs/             → Objetos de transferência de dados
├── Configurations/       → Configurações de DI, Swagger e settings
├── Requests/             → Arquivo .http para testar endpoints
├── appsettings.json      → Configurações da aplicação
└── Program.cs            → Ponto de entrada e pipeline HTTP
```

---

## Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

---

## Como configurar

### 1. Definir o token de acesso

Abra `appsettings.json` e troque o valor de `Token`:

```json
"ApiKey": {
  "Token": "meu-token-secreto-e-unico"
}
```

> Em produção, use variável de ambiente para não expor o token no código:
> ```
> ApiKey__Token=meu-token-secreto-e-unico
> ```

---

## Como rodar

```bash
# Clone ou abra a pasta do projeto
cd "C:\Users\igorp\Documents\ViaCEP"

# Restaurar pacotes e compilar
dotnet build

# Executar
dotnet run
```

A API ficará disponível em:
- `https://localhost:7000` (HTTPS)
- `http://localhost:5000` (HTTP)

O **Swagger UI** abre automaticamente na raiz: `https://localhost:7000`

---

## Autenticação

Todas as rotas exigem o header:

```
X-Api-Key: seu-token-aqui
```

Sem o token correto, a API retorna **401 Unauthorized**.

No Swagger UI, clique em **Authorize** e informe o token.

---

## Endpoint

### `GET /api/cep/{cep}`

Consulta o endereço de um CEP.

**Parâmetro:** CEP com 8 dígitos (com ou sem hífen).

**Exemplo de requisição:**
```
GET /api/cep/01310100
X-Api-Key: meu-token-secreto-e-unico
```

**Resposta 200 OK:**
```json
{
  "cep": "01310-100",
  "logradouro": "Avenida Paulista",
  "complemento": "de 610 a 610 - lado par",
  "bairro": "Bela Vista",
  "cidade": "São Paulo",
  "estado": "SP",
  "ibge": "3550308",
  "gia": "1004",
  "ddd": "11",
  "siafi": "7107"
}
```

**Códigos de resposta:**

| Código | Situação |
|--------|----------|
| 200 | CEP encontrado |
| 400 | CEP com formato inválido |
| 401 | Token ausente ou incorreto |
| 404 | CEP não encontrado |
| 500 | Falha na comunicação com ViaCEP |

---

## Cache

CEPs consultados ficam em cache em memória por **60 minutos** (configurável em `appsettings.json` → `ViaCep.CacheMinutes`). Requisições repetidas ao mesmo CEP são respondidas em menos de 1ms sem chamada externa.

---

## Testando com o arquivo .http

Abra [Requests/cep.http](Requests/cep.http) no VS Code (extensão **REST Client**), defina o valor da variável `@token` e execute cada requisição diretamente no editor.

---

## Configuração de exemplo

O arquivo [appsettings.Example.json](appsettings.Example.json) contém todas as opções disponíveis com comentários explicativos. Para usar:

```bash
# Copie o exemplo e edite com seus valores
copy appsettings.Example.json appsettings.json
```

Variáveis de ambiente sobrescrevem o `appsettings.json` em produção:

```bash
# Windows
set ApiKey__Token=seu-token-secreto

# Linux / macOS
export ApiKey__Token=seu-token-secreto
```

---

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
