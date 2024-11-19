using AutoMapper;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using BookStore.Grpc.Client;
using BookStore.Grpc.Client.Grpc;
using Grpc.Core;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static BookStore.Contracts.Protos.AuthorService;
using static BookStore.Contracts.Protos.BookAuthorService;
using static BookStore.Contracts.Protos.BookService;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var mapperConfig = new MapperConfiguration(config => config.AddProfile(new GrpcMappingProfile()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazorise(options => { options.Immediate = true; }).AddBootstrap5Providers().AddFontAwesomeIcons();

builder.Services.AddGrpcClient<BookServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["Grpc:ServerUrl"]!);
    options.ChannelOptionsActions.Add(o =>
    {
        o.Credentials = ChannelCredentials.SecureSsl;
        o.ServiceProvider = builder.Services.BuildServiceProvider();
    });
}).ConfigureChannel(o =>
{
    o.HttpHandler = new GrpcWebHandler(new HttpClientHandler());
}); ;
builder.Services.AddGrpcClient<AuthorServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["Grpc:ServerUrl"]!);
    options.ChannelOptionsActions.Add(o =>
    {
        o.Credentials = ChannelCredentials.SecureSsl;
        o.ServiceProvider = builder.Services.BuildServiceProvider();
    });
}).ConfigureChannel(o =>
{
    o.HttpHandler = new GrpcWebHandler(new HttpClientHandler());
}); ;
builder.Services.AddGrpcClient<BookAuthorServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["Grpc:ServerUrl"]!);
    options.ChannelOptionsActions.Add(o =>
    {
        o.Credentials = ChannelCredentials.SecureSsl;
        o.ServiceProvider = builder.Services.BuildServiceProvider();
    });
}).ConfigureChannel(o =>
{
    o.HttpHandler = new GrpcWebHandler(new HttpClientHandler());
}); ;
builder.Services.AddSingleton<IBookStoreWrapper, BookStoreGrpcWrapper>();
await builder.Build().RunAsync();
