using Grimoire.Bootstrapper;
using Grimoire.Core.UseCases;
using Grimoire.Core.UseCases.GetSpells;
using Grimoire.WebApi.UseCases.GetSpells;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Get the Base URI from configuration
string? baseUri = builder.Configuration["BaseUri"];

// Throw an exception if the base URI isn't set
if (string.IsNullOrWhiteSpace(baseUri))
    throw new InvalidOperationException("Base URI is not set.");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Grimoire API", Version = "v1" });

    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services
    .AddGrimoireServices()
    .AddScoped<IUseCaseInteractor<GetSpellsRequest, IResult>, GetSpellsInteractor<IResult>>()
    .AddScoped<IGetSpellsBoundary<IResult>, GetSpellsApiBoundary>(_ => 
        new GetSpellsApiBoundary(baseUri));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .MapGet(
        "/spells",
        async (string? filter, IUseCaseInteractor<GetSpellsRequest, IResult> getSpellsInteractor) =>
        {
            GetSpellsRequest request = new(filter);

            return await getSpellsInteractor.HandleAsync(request);
        })
    .WithName("GetSpells")
    .Produces<IResult>(200)
    .Produces<IResult>(404)
    .Produces<IResult>(500)
    .WithOpenApi();

app.Run();