﻿using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace WebPollyExample.Extensions
{
    public class PollyExtensions
    {
        private const int _retryCount = 3;
        protected PollyExtensions() { }

        public static AsyncRetryPolicy<HttpResponseMessage> PollyWaitAndRetry() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryAttempt, context) =>
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine($"Aguardando {timeSpan}");
                        Console.WriteLine($"Tentando pela {retryAttempt}ª vez");
                        Console.ResetColor();
                    }
                );

        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> PollyCircuitBreaker() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: _retryCount,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (result, timeSpan) =>
                    {
                        var msg = 
                            $"Circuito Aberto - onBreak \n" +
                            $"Necessário aguardar: {timeSpan} \n" +
                            $"Exception: {result.Exception.Message}";

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(msg);
                        Console.ResetColor();
                    },
                    onReset: () =>
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Circuito meio aberto - OnReset");
                        Console.ResetColor();
                    });
    }
}
