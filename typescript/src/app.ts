import express from 'express';
import cors from 'cors';
import swaggerUi from 'swagger-ui-express';
import { swaggerSpec } from './config/swagger';
import { apiKeyMiddleware } from './middleware/apiKeyMiddleware';
import cepRoutes from './routes/cepRoutes';
import { config } from './config';

const app = express();

app.use(cors());
app.use(express.json());
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerSpec));
app.get('/health', (_req, res) => res.json({ status: 'Healthy' }));
app.use('/api', apiKeyMiddleware);
app.use('/api/cep', cepRoutes);

app.listen(config.port, () => {
  console.log(`[typescript] ViaCEP API rodando na porta ${config.port}`);
});
