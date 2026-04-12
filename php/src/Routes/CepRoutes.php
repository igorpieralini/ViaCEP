<?php

declare(strict_types=1);

namespace App\Routes;

use App\Service\ViaCepService;
use Psr\Http\Message\ResponseInterface as Response;
use Psr\Http\Message\ServerRequestInterface as Request;
use Slim\Routing\RouteCollectorProxy;

class CepRoutes
{
    public static function register(RouteCollectorProxy $group): void
    {
        $service = new ViaCepService();

        $group->get('/cep/{cep}', function (Request $request, Response $response, array $args) use ($service) {
            $cep = $args['cep'];

            try {
                $endereco = $service->consultarCep($cep);
                $response->getBody()->write(json_encode($endereco));
                return $response->withHeader('Content-Type', 'application/json');
            } catch (\InvalidArgumentException $e) {
                $response->getBody()->write(json_encode([
                    'mensagem' => $e->getMessage(),
                    'statusCode' => 400,
                ]));
                return $response->withStatus(400)->withHeader('Content-Type', 'application/json');
            } catch (\RuntimeException $e) {
                $statusCode = str_contains($e->getMessage(), 'não encontrado') ? 404 : 500;
                $response->getBody()->write(json_encode([
                    'mensagem' => $e->getMessage(),
                    'statusCode' => $statusCode,
                ]));
                return $response->withStatus($statusCode)->withHeader('Content-Type', 'application/json');
            } catch (\Throwable $e) {
                $response->getBody()->write(json_encode([
                    'mensagem' => 'Ocorreu um erro interno inesperado.',
                    'statusCode' => 500,
                ]));
                return $response->withStatus(500)->withHeader('Content-Type', 'application/json');
            }
        });
    }
}
