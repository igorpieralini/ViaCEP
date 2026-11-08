
# ViaCEP API

<sub>Projeto de estudo · Git Flow, CI/CD e Versionamento · API REST Multilíngue</sub>

<br/>

Projeto desenvolvido para o estudo de configuração de repositórios no GitHub, **Git Flow** e boas práticas de versionamento. Implementa um wrapper da API pública [ViaCEP](https://viacep.com.br) para consulta de endereços brasileiros por CEP.

A aplicação possui implementações em **6 linguagens**, utilizando a mesma interface REST, e serve como referência prática de CI/CD, estratégias de branches e Dependabot.

<br/>

<img width="100%" src="https://capsule-render.vercel.app/api?type=rect&color=000000&height=1" alt=""/>

<br/>

<div align="center">

### <samp>// objetivos</samp>

<br/>

<samp>Git Flow &bull; Estratégias de Branches &bull; Versionamento</samp>

<br/><br/>

<samp>CI/CD &bull; Dependabot &bull; Configuração de Repositórios</samp>

<br/><br/>

<samp>APIs REST &bull; Integração com Serviços Externos</samp>

</div>

<br/>

<img width="100%" src="https://capsule-render.vercel.app/api?type=rect&color=000000&height=1" alt=""/>

<br/>

<div align="center">

### <samp>// tecnologias</samp>

<br/>

<table align="center">
<tr>
<td align="center" width="140"><br/><img src="https://skillicons.dev/icons?i=cs" width="40" alt="C#"/><br/><br/><samp>C#</samp><br/><br/></td>
<td align="center" width="140"><br/><img src="https://skillicons.dev/icons?i=nodejs" width="40" alt="Node.js"/><br/><br/><samp>Node.js</samp><br/><br/></td>
<td align="center" width="140"><br/><img src="https://skillicons.dev/icons?i=ts" width="40" alt="TypeScript"/><br/><br/><samp>TypeScript</samp><br/><br/></td>
<td align="center" width="140"><br/><img src="https://skillicons.dev/icons?i=py" width="40" alt="Python"/><br/><br/><samp>Python</samp><br/><br/></td>
<td align="center" width="140"><br/><img src="https://skillicons.dev/icons?i=java" width="40" alt="Java"/><br/><br/><samp>Java</samp><br/><br/></td>
<td align="center" width="140"><br/><img src="https://skillicons.dev/icons?i=php" width="40" alt="PHP"/><br/><br/><samp>PHP</samp><br/><br/></td>
</tr>
</table>

<br/>

<samp>ASP.NET 6 &bull; Express &bull; FastAPI &bull; Spring Boot 3 &bull; Slim 4</samp>

</div>

<br/>

<img width="100%" src="https://capsule-render.vercel.app/api?type=rect&color=000000&height=1" alt=""/>

<br/>

<div align="center">

### <samp>// implementações</samp>

</div>

<br/>

| Linguagem | Diretório | Porta | Framework |
|:---|:---|:---:|:---|
| C# | `csharp/` | `5000` | ASP.NET 6 |
| Node.js | `nodejs/` | `5001` | Express |
| TypeScript | `typescript/` | `5002` | Express + TS |
| Python | `python/` | `5003` | FastAPI |
| Java | `java/` | `5004` | Spring Boot 3 |
| PHP | `php/` | `5005` | Slim 4 |

<br/>

<img width="100%" src="https://capsule-render.vercel.app/api?type=rect&color=000000&height=1" alt=""/>

<br/>

<div align="center">

### <samp>// endpoint</samp>

<br/>

Consulta de endereços brasileiros a partir de um CEP, utilizando autenticação por chave de API.

</div>

<br/>

```http
GET /api/cep/{cep}
X-Api-Key: {seu-token}
```

<br/>

<div align="center">

### <samp>// exemplo de resposta</samp>

</div>

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

<br/>

<img width="100%" src="https://capsule-render.vercel.app/api?type=rect&color=000000&height=1" alt=""/>

<br/>

<div align="center">

### <samp>// execução</samp>

<br/>

Cada implementação pode ser executada independentemente, utilizando as dependências e ferramentas de sua respectiva linguagem.

</div>

<br/>

**C# — .NET 6**

```bash
cd csharp && dotnet run
```

**Node.js — Express**

```bash
cd nodejs && npm install && npm start
```

**TypeScript — Express**

```bash
cd typescript && npm install && npm run dev
```

**Python — FastAPI**

```bash
cd python && pip install -r requirements.txt && python -m app.main
```

**Java — Spring Boot 3**

```bash
cd java && mvn spring-boot:run
```

**PHP — Slim 4**

```bash
cd php && composer install && php -S localhost:5005 -t public
```

<br/>

<img width="100%" src="https://capsule-render.vercel.app/api?type=rect&color=000000&height=1" alt=""/>

<br/>

<div align="center">

### <samp>// integração</samp>

<br/>

A API [ViaCEP](https://viacep.com.br) é um serviço público e gratuito para consulta de endereços brasileiros por CEP, sem necessidade de cadastro.

<br/>

<samp>API Gratuita &bull; Sem Cadastro &bull; Consulta por CEP</samp>

</div>
