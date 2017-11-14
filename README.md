# IdentityServerDemo
This repository contains a dummy project to show Identity server into Web API project for creating token and protecting resources(API).

IDENTITY SERVER
Its a framework allowing you to build your own security token service
Its a library pull it in, build it according to your requirement.
Centralized system for identity management for one or many resources.

REASON TO USE IDS

-provide flexibility to customers
-business requirement - other token services were not flexible enough - id server provides a fmwk that do all hard security stuff acc to the specs & allow you to do customiztion and provide you flexibility that you need
-centralized id mgt
-single signon
-host our own security token service over Id server & mak them both web API and web app communicate with each other via security token service in a secure way over network

HOW IT WORKS

- its based on .net core
- deliverd by nuget package
- It wires with asp .net core is that it is implemented as a middleware
- build a host around id server, grab the new git, put middleware into pipeline, and job of middleware is to implement the endpoints for those protocols openID and OAuth
- no need to implement id server into web app that we want to secure we can create a seperate  host for this becaause we need to create a sepearte security token service

IMPLEMENTATION

-Middleware ID server implemented endpoints -> the client app(that want to know who the users is) is goint to talk the protocols to id server, so its a seperate application from id server which will redirect client app to login page
- user is send to a centralized login and there id server will behave like it doesnt know who the user is so provide your credentials or code to the centralized login page
- we can connect id server on login page to another id provider so if we want to login windows users that are stored in Azure AD we can have id server sort of chain for Azure AD .
- we have users that we ant to login through google or twitter or any other 3rd party ID server will work for those and provide custom ability to make twitter as a login, so power of ID server here is the whatever customization code we have implemented according to our requirement.
- Another motivation of ID server is not only we can use whatever id store or whatever mechanism for authentication we want also we can implement additional logic during login workflow for eg. we have implemented ID server for 5 apps and before access any of app we want user to sign a EULA(end-user license aggreement). No need to write that code in every app if we have login at centralized loc for all apps we can write that addition logic centralized as well.
-ID server receives request -> send user to your local login page -> then user will be send back to authorize end point -> after login, code will commmunicate to identity of user with credentials over id server you isse a cookie -> id server read the cookie and know who the user is & generate token back to client app.

ID Server Environment

- To do all this with id server,
- need to teach id server about your app, your users and about id data that we are protecting for those users.
- need to make all this centarlized cuz if an app wants a users unique id & email and othe app want a unique id and name, so all these are id information that id server protecting about the users, we need to teach id server about these things and relationships.

SET UP ID SERVER

- Core project already have configuration for std DI, 
-configure method that configure ASP .net core pipeline with all the middlewares, so we have all middlewares
- install ID server nuget package and serve all protocols endpoints
 
 Under this repository we are not implementing client project, it has only web api & identity server host. You can test token & API access through Postman or any other API testing tool.

1. add id server into middleware pipeline into IdntityServerHost

   app.UseIdentityServer();

2. ID server need to configure into DI system of ID server host project

- 'Services' - it provides a bunch of services that are available in already configured DI
- we have extension method services.AddIdentityServer(),
that will wire up id server to the core services and we can call some extensions method to configure other things.

3. Now you need to tell Id server about your clients, application & resources

we can implement Asp .net Identity Users or legacy database for users.
Asp .net identity is a frmwk that mangers databse for ur users information and authentication
Its a library that talk to your database that holds ur users.

we need to register all clients and users with id server so that logging code will use coookie auth middleware to talk to id server.
- there will be a bunch of configuration data (client, user, resources) that id server need and it dont care where you store that data.

Id server when issue tokens need few data like emailid to generate a token for that, id server must have some interface that will provide that data & id server will query it to fetch that data.
- SO we need to stick this interface into DI so that id server will able to find our implementation of user data.
- to configure these data where ever it is need to implement it into ID server object model of client , resources and id resources
 
extension method- inmemory store()
for db - AddClientStore() // this is when fetching from DB, here we are using some static clients & resources

4. add certificate for token.

-Id server implement json token secure digitally signed,
- we need to add self signed certificate for digital signature we need to have key material
services.AddSignedCredentials("CN=sts"); //  if you have any self signed certificate
else use, DeveloperCredentials.

5. SET UP API Project

use middleware for authentication
app.UseAuthentication();

add same for configure services 

           services.AddMvcCore()
           .AddAuthorization()
           .AddJsonFormatters();
            services.AddApplicationInsightsTelemetry();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:64313/"; // who we trust
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "sampleapi"; // who we are, name of your API project that should match with scope in client into IDS host client list.
                });

put [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)] attribute for controller method that you want to secure.


6. TEST API through Postman

first check configuration of IDS host, access: 
http://localhost/.well-known/openid-configuration // WSDL for id server apis

second fetch token, access:
http://localhost/connect/token

third access api with token into header (provide request basis on client you have created, match scope & grant type carefully.)
//// RESULT: 200 OK with claims data /////////



