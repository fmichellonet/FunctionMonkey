﻿using System;
using System.Linq.Expressions;
using AzureFromTheTrenches.Commanding.Abstractions;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.Model;

namespace FunctionMonkey.Builders
{
    internal class HttpRouteFunctionBuilder : IHttpRouteFunctionBuilder
    {
        private readonly IFunctionBuilder _decoratedBuilder;
        private readonly HttpRouteConfiguration _httpRouteConfiguration;

        public HttpRouteFunctionBuilder(IFunctionBuilder decoratedBuilder,
            HttpRouteConfiguration httpRouteConfiguration)
        {
            _decoratedBuilder = decoratedBuilder;
            _httpRouteConfiguration = httpRouteConfiguration;
        }

        public IHttpRouteFunctionBuilder HttpRoute(string routePrefix, Action<IHttpFunctionBuilder> httpFunctionBuilder)
        {
            return _decoratedBuilder.HttpRoute(routePrefix, httpFunctionBuilder);
        }

        public IFunctionBuilder ServiceBus(string connectionName, Action<IServiceBusFunctionBuilder> serviceBusFunctionBuilder)
        {
            return _decoratedBuilder.ServiceBus(connectionName, serviceBusFunctionBuilder);
        }

        public IFunctionBuilder Storage(string connectionName, Action<IStorageFunctionBuilder> storageFunctionBuilder)
        {
            return _decoratedBuilder.Storage(connectionName, storageFunctionBuilder);
        }

        public IHttpRouteFunctionBuilder OpenApiDescription(string description)
        {
            _httpRouteConfiguration.OpenApiDescription = description;
            return this;
        }

        public IHttpRouteFunctionBuilder OpenApiName(string name)
        {
            _httpRouteConfiguration.OpenApiName = name;
            return this;
        }

        public IFunctionBuilder Timer<TCommand>(string cronExpression) where TCommand : ICommand
        {
            return _decoratedBuilder.Timer<TCommand>(cronExpression);
        }

        public IFunctionBuilder Timer<TCommand, TTimerCommandFactoryType>(string cronExpression) where TCommand : ICommand where TTimerCommandFactoryType : ITimerCommandFactory<TCommand>
        {
            return _decoratedBuilder.Timer<TCommand, TTimerCommandFactoryType>(cronExpression);
        }
    }
}
