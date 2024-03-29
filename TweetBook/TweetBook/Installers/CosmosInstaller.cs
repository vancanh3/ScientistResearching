﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Domain;

namespace TweetBook.Installers
{
    public class CosmosInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            var cosmosSetting = new CosmosStoreSettings(
                Configuration["CosmosSettings:DatabaseName"],
                Configuration["CosmosSettings:AccountUri"],
                Configuration["CosmosSettings:AccountKey"],
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct,ConnectionProtocol = Protocol.Tcp});
            services.AddCosmosStore<Post>(cosmosSetting);
        }
    }
}
