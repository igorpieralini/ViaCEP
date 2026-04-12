package com.viacep.config;

import io.swagger.v3.oas.models.Components;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Contact;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.security.SecurityRequirement;
import io.swagger.v3.oas.models.security.SecurityScheme;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SwaggerConfig {
    @Bean
    public OpenAPI customOpenAPI() {
        return new OpenAPI()
                .info(new Info()
                        .title("ViaCEP API - Java")
                        .version("1.0.0")
                        .description("API para consulta de endereços via serviço ViaCEP. Autenticação via header X-Api-Key.")
                        .contact(new Contact().name("Equipe de Desenvolvimento")))
                .addSecurityItem(new SecurityRequirement().addList("X-Api-Key"))
                .components(new Components()
                        .addSecuritySchemes("X-Api-Key",
                                new SecurityScheme()
                                        .type(SecurityScheme.Type.APIKEY)
                                        .in(SecurityScheme.In.HEADER)
                                        .name("X-Api-Key")
                                        .description("Informe o token único de acesso.")));
    }
}
