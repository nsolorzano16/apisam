using System;
using System.Net;
using Newtonsoft.Json;

namespace apisam.web.HandleErrors
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }


        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DatabaseMessage { get; private set; }

        public ApiError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string dbmessage)
            : this(statusCode, statusDescription)
        {
            this.Message = GetDefaultMessageForStatusCode(statusCode);
            this.DatabaseMessage = dbmessage;
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {

                400 => "Error al realizar la petición",
                401 => "Usted no esta autorizado para esta petición",
                404 => "Recurso no encontrado",
                405 => " Se ha hecho un request con un recurso usando un método request no soportado",
                408 => " El cliente no ha enviado un request con el tiempo necesario con el que el servidor estaba preparado para esperar",
                500 => "Error en el servidor no se pudo concretar el mensaje",
                503 => "El servidor está actualmente no disponible",
                _ => "",
            };
        }
    }

    public class BadRequestError : ApiError
    {
        public BadRequestError()
            : base(400, HttpStatusCode.BadRequest.ToString())
        {
        }


        public BadRequestError(string message)
            : base(400, HttpStatusCode.BadRequest.ToString(), message)
        {
        }
    }



    public class UnathorizedError : ApiError
    {
        public UnathorizedError()
            : base(401, HttpStatusCode.BadRequest.ToString())
        {
        }


        public UnathorizedError(string message)
            : base(401, HttpStatusCode.BadRequest.ToString(), message)
        {

        }
    }

    public class NotFoundError : ApiError
    {
        public NotFoundError()
            : base(404, HttpStatusCode.NotFound.ToString())
        {
        }


        public NotFoundError(string message)
            : base(404, HttpStatusCode.NotFound.ToString(), message)
        {
        }
    }


    public class NotAllowedError : ApiError
    {
        public NotAllowedError()
            : base(405, HttpStatusCode.MethodNotAllowed.ToString())
        {
        }


        public NotAllowedError(string message)
            : base(405, HttpStatusCode.MethodNotAllowed.ToString(), message)
        {
        }
    }

    public class RequestTimeOutError : ApiError
    {
        public RequestTimeOutError()
            : base(408, HttpStatusCode.RequestTimeout.ToString())
        {
        }


        public RequestTimeOutError(string message)
            : base(408, HttpStatusCode.RequestTimeout.ToString(), message)
        {
        }
    }


    public class InternalServerError : ApiError
    {
        public InternalServerError()
            : base(500, HttpStatusCode.InternalServerError.ToString())
        {
        }


        public InternalServerError(string message)
            : base(500, HttpStatusCode.InternalServerError.ToString(), message)
        {
        }
    }




    public class ServiceUnavailableError : ApiError
    {
        public ServiceUnavailableError()
            : base(500, HttpStatusCode.ServiceUnavailable.ToString())
        {
        }


        public ServiceUnavailableError(string message)
            : base(500, HttpStatusCode.ServiceUnavailable.ToString(), message)
        {
        }
    }






}
