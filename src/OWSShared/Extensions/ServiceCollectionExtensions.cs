﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace OWSShared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAndValidate<T>(this IServiceCollection @this, IConfiguration config) where T : class => @this
            .Configure<T>(config)
            .PostConfigure<T>(settings =>
            {
                var configErrors = settings.ValidationErrors().ToArray();
                if (configErrors.Any())
                {
                    var aggrErrors = string.Join(",", configErrors);
                    var count = configErrors.Length;
                    var configType = typeof(T).Name;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Found {count} configuration error(s) in {configType}: {aggrErrors}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });

        public static IServiceCollection ConfigureAndValidate<T>(this IServiceCollection @this, string name, IConfiguration config) where T : class => @this
            .Configure<T>(name, config)
            .PostConfigure<T>(name, settings =>
            {
                var configErrors = settings.ValidationErrors().ToArray();
                if (configErrors.Any())
                {
                    var aggrErrors = string.Join(",", configErrors);
                    var count = configErrors.Length;
                    var configType = typeof(T).Name;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Found {count} configuration error(s) in {configType}: {aggrErrors}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
    }
}
