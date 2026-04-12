package com.viacep.middleware;

import jakarta.servlet.*;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.security.MessageDigest;

@Component
@Order(1)
public class ApiKeyFilter implements Filter {

    private static final Logger log = LoggerFactory.getLogger(ApiKeyFilter.class);
    private static final String HEADER_NAME = "X-Api-Key";

    @Value("${api.key}")
    private String apiKey;

    @Override
    public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
            throws IOException, ServletException {

        HttpServletRequest httpReq = (HttpServletRequest) request;
        String path = httpReq.getRequestURI();

        if (path.startsWith("/swagger") || path.startsWith("/api-docs")
                || path.startsWith("/actuator") || path.startsWith("/health")
                || path.contains("swagger-ui")) {
            chain.doFilter(request, response);
            return;
        }

        String received = httpReq.getHeader(HEADER_NAME);
        if (received == null || !MessageDigest.isEqual(received.getBytes(), apiKey.getBytes())) {
            log.warn("[java] Acesso negado: token inválido ou ausente. Path: {}", path);
            HttpServletResponse httpRes = (HttpServletResponse) response;
            httpRes.setStatus(401);
            httpRes.setContentType("application/json");
            httpRes.getWriter().write("{\"mensagem\":\"Token de acesso inválido ou ausente.\",\"statusCode\":401}");
            return;
        }

        chain.doFilter(request, response);
    }
}
