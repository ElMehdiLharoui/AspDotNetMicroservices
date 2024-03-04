﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.CompilerServices;

namespace Students.Options
{
    public class ConfigureSwagerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,CreateVersionInfo(description));
            }

        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "CwkSocial",
                Version = description.ApiVersion.ToString()
            };
            if(description.IsDeprecated)
            {
                info.Description = "This Api has been deprecated";
            }
            return info;
        }
    }
}
