﻿using System.Web;
using ServiceStack.Support.WebHost;
using ServiceStack.WebHost;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.WebHost.Endpoints.Support;

namespace ServiceStack
{
    public class RequestInfoFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.CatchAllHandlers.Add(ProcessRequest);
        }

        public IHttpHandler ProcessRequest(string httpMethod, string pathInfo, string filePath)
        {
            var pathParts = pathInfo.TrimStart('/').Split('/');
            return pathParts.Length == 0 ? null : GetHandlerForPathParts(pathParts);
        }

        private static IHttpHandler GetHandlerForPathParts(string[] pathParts)
        {
            var pathController = string.Intern(pathParts[0].ToLower());
            return pathController == RequestInfoHandler.RestPath
                ? new RequestInfoHandler()
                : null;
        }
    }
}