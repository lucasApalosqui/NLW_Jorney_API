﻿using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is JorneyException)
            {
                var jorneyException = (JorneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)jorneyException.GetStatusCode();

                var responseJson = new ResponseErrorsJson(jorneyException.GetErrorMessages());

                context.Result = new ObjectResult(responseJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrorsJson(new List<string> { "Unknow error" });
                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
