<?php

declare(strict_types=1);

namespace App\Service;

use GuzzleHttp\Client;
use GuzzleHttp\Exception\ConnectException;
use GuzzleHttp\Exception\RequestException;
use GuzzleHttp\HandlerStack;
use GuzzleHttp\Middleware;
use Psr\Http\Message\RequestInterface;
use Psr\Http\Message\ResponseInterface;

class ViaCepClient
{
    private Client $client;

    public function __construct()
    {
        $stack = HandlerStack::create();

        $stack->push(Middleware::retry(
            function (int $retries, RequestInterface $request, ?ResponseInterface $response = null, ?\Throwable $exception = null): bool {
                if ($retries >= 3) return false;
                if ($exception instanceof ConnectException) return true;
                if ($response && $response->getStatusCode() >= 500) return true;
                return false;
            },
            function (int $retries): int {
                return (int) (pow(2, $retries) * 1000);
            }
        ));

        $this->client = new Client([
            'base_uri' => $_ENV['VIACEP_BASE_URL'] ?? 'https://viacep.com.br/ws',
            'timeout' => (int) ($_ENV['VIACEP_TIMEOUT_SECONDS'] ?? 10),
            'handler' => $stack,
            'headers' => ['Accept' => 'application/json'],
        ]);
    }

    public function buscarCep(string $cep): ?array
    {
        try {
            $response = $this->client->get("/{$cep}/json/");
        } catch (ConnectException $e) {
            throw new \RuntimeException('Falha ao comunicar com o serviço ViaCEP.', 0, $e);
        } catch (RequestException $e) {
            if ($e->getResponse() && $e->getResponse()->getStatusCode() >= 400) {
                throw new \RuntimeException('ViaCEP retornou status inesperado: ' . $e->getResponse()->getStatusCode());
            }
            throw new \RuntimeException('Timeout ao consultar o serviço ViaCEP.', 0, $e);
        }

        return json_decode($response->getBody()->getContents(), true);
    }
}
