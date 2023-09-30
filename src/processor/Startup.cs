// <copyright file="Startup.cs" company="Lavanya.">
// </copyright>

namespace Microsoft.Movie.Store
{
    using Azure.Search.Documents;
    using Azure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RestSharp;
    using Microsoft.Movie.Store.Workflow;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.DotNet.Scaffolding.Shared;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Movie.Store.Models;
    using Azure.Security.KeyVault.Secrets;
    using Azure.Identity;

    /// <summary>
    /// StartUp.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="environment">The hosting environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Configures the services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            string indexName = "movie-search-index";

            // Get the service endpoint and API key from the environment
            Uri endpoint = new Uri("https://movie-search-store.search.windows.net");

            // Create a client
            SearchClient searchClient = new SearchClient(endpoint, indexName, new DefaultAzureCredential());

            services.AddSingleton(searchClient);

            services.AddSingleton<ISearchWorkflow<MovieIndexRecord>, SearchWorkflow<MovieIndexRecord>>();
            _ = services.AddSwaggerGen();
            _ = services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            _ = services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            });
            _ = services.AddCors(o =>
            {
                o.AddDefaultPolicy(b =>
                {
                    b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("lastModifiedAt", "lastModifiedBy");
                });
            });
        }

        /// <summary>
        /// Configures the specified application. This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            ArgumentNullException.ThrowIfNull(lifetime);

            _ = app.UseRouting();
            _ = app.UseSwagger();
            _ = app.UseCors();
            _ = app.UseSwaggerUI();
            _ = app.UseHttpsRedirection();
            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
