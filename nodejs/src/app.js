const express = require('express');
const cors = require('cors');
const swaggerUi = require('swagger-ui-express');
const swaggerSpec = require('./config/swagger');
const { apiKeyMiddleware } = require('./middleware/apiKeyMiddleware');
const cepRoutes = require('./routes/cepRoutes');

const app = express();
const PORT = process.env.PORT || 5001;

app.use(cors());
app.use(express.json());
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerSpec));
app.get('/health', (_req, res) => res.json({ status: 'Healthy' }));
app.use('/api', apiKeyMiddleware);
app.use('/api/cep', cepRoutes);

app.listen(PORT, () => {
  console.log(`[nodejs] ViaCEP API rodando na porta ${PORT}`);
});
