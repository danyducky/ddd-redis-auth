using Auth.DataLayer;
using Auth.DataLayer.Entities;

Startup.Program.Run(new[]
{
    (typeof(AuthContext), typeof(IAuthEntity))
},
(services) =>
{

},
(provider) =>
{

});