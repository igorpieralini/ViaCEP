# ViaCEP API

Este projeto demonstra a utilizalçao da API pública brasileira de consulta de CEP (VIA CEP). O objetivo foi demonstrar a utilização com algumas das linguagens mais utilizadas atualmente.

Via CEP é totalmente gratuita, assim podendo evitar cobranças desncessárias de certos provedores.

## Portas

| Linguagem  | Pasta        | Porta  | Framework        |
|------------|--------------|--------|------------------|
| C#         | `csharp/`    | `5000` | ASP.NET 6        |
| Node.js    | `nodejs/`    | `5001` | Express          |
| TypeScript | `typescript/`| `5002` | Express + TS     |
| Python     | `python/`    | `5003` | FastAPI          |
| Java       | `java/`      | `5004` | Spring Boot 3    |
| PHP        | `php/`       | `5005` | Slim 4           |

## Endpoint

```
GET /api/cep/{cep}
Header: X-Api-Key: {seu-token}
```

## Exemplo de resposta

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

## Features (todas as linguagens)

- Autenticação via API Key (header `X-Api-Key`)
- Cache em memória (configurável)
- Retry com backoff exponencial
- Health Check (`GET /health`)
- Swagger/OpenAPI docs
- CORS habilitado
- Comparação de token segura (timing-safe)

## Como rodar

### C# (.NET 6)
```bash
cd csharp && dotnet run
```

### Node.js
```bash
cd nodejs && npm install && npm start
```

### TypeScript
```bash
cd typescript && npm install && npm run dev
```

### Python
```bash
cd python && pip install -r requirements.txt && python -m app.main
```

### Java (Maven + JDK 17)
```bash
cd java && mvn spring-boot:run
```

### PHP (Composer + PHP 8.1+)
```bash
cd php && composer install && php -S localhost:5005 -t public
```
