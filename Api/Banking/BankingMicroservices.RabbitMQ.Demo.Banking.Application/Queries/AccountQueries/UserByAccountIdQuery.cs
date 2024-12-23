﻿using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

public sealed record UserByAccountIdQuery(int Id):IQuery<UserSearchDto>;
