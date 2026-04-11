<?php

declare(strict_types=1);

require __DIR__ . '/../vendor/autoload.php';

use App\Middleware\ApiKeyMiddleware;
use App\Routes\CepRoutes;
use Slim\Factory\AppFactory;
use Slim\Middleware\ContentLengthMiddleware;

if (file_exists(__DIR__ . '/../.env')) {
    $dotenv = Dotenv\Dotenv::createImmutable(__DIR__ . '/..');
    $dotenv->load();
}

$app = AppFactory::create();

$app->addBodyParsingMiddleware();
$app->add(new ContentLengthMiddleware());
$app->addRoutingMiddleware();

$app->add(function ($request, $handler) {
    $response = $handler->handle($request);
    return $response
        ->withHeader('Access-Control-Allow-Origin', '*')
        ->withHeader('Access-Control-Allow-Headers', 'X-Api-Key, Content-Type, Accept')
        ->withHeader('Access-Control-Allow-Methods', 'GET, OPTIONS');
});

$app->addErrorMiddleware(true, true, true);

$app->get('/health', function ($request, $response) {
    $response->getBody()->write(json_encode(['status' => 'Healthy']));
    return $response->withHeader('Content-Type', 'application/json');
});

$app->group('/api', function ($group) {
    CepRoutes::register($group);
})->add(new ApiKeyMiddleware());

$app->run();
