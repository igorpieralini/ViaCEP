# ViaCEP API

Projeto focado no estudo de configuração de repositórios no GitHub e **Git Flow**,
criando um wrapper da API pública [ViaCEP](https://viacep.com.br) para consulta de endereços brasileiros por CEP. Implementado em **6 linguagens** com a mesma interface REST, servindo como referência prática de CI/CD, branch strategy, Dependabot e boas práticas de versionamento.

> ViaCEP é gratuita e não requer cadastro — ideal para substituir provedores pagos.

## Stack

| Linguagem  | Pasta         | Porta  | Framework      |
|------------|---------------|--------|----------------|
| C#         | `csharp/`     | `5000` | ASP.NET 6      |
| Node.js    | `nodejs/`     | `5001` | Express        |
| TypeScript | `typescript/` | `5002` | Express + TS   |
| Python     | `python/`     | `5003` | FastAPI        |
| Java       | `java/`       | `5004` | Spring Boot 3  |
| PHP        | `php/`        | `5005` | Slim 4         |

## Endpoint

```
GET /api/cep/{cep}
Header: X-Api-Key: {seu-token}
```

### Exemplo de resposta

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

## Como rodar

```bash
# C# (.NET 6)
cd csharp && dotnet run

# Node.js
cd nodejs && npm install && npm start

# TypeScript
cd typescript && npm install && npm run dev

# Python
cd python && pip install -r requirements.txt && python -m app.main

# Java (Maven + JDK 17)
cd java && mvn spring-boot:run

# PHP (Composer + PHP 8.1+)
cd php && composer install && php -S localhost:5005 -t public
```