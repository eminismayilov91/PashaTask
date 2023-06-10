using System;

namespace Entity.Helper
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string? StatusCode { get; set; }
        public string? Message { get; set; }
        public Exception? Exception { get; set; }

        public Response(bool status)
        {
            Status = status;
        }

        public Response(T data, bool status)
        {
            Data = data;
            Status = status;
        }

        public Response(T data, bool status, string statusCode)
        {
            Data = data;
            StatusCode = statusCode;
            Status = status;
        }

        public Response(T data, bool status, string statusCode, string message)
        {
            Data = data;
            StatusCode = statusCode;
            Status = status;
            Message = message;
        }

        public Response(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public Response(bool status, string statusCode, string message)
        {
            StatusCode = statusCode;
            Status = status;
            Message = message;
        }

        public Response(bool status, string message, Exception exception)
        {
            Status = status;
            Message = message;
            Exception = exception;
        }

        public Response(bool status, string statusCode, string message, Exception exception)
        {
            StatusCode = statusCode;
            Status = status;
            Message = message;
            Exception = exception;
        }

        public static Response<T> Succeed(T data)
        {
            return new Response<T>(data, true);
        }

        public static Response<T> Succeed(T data, string statusCode)
        {
            return new Response<T>(data, true, statusCode);
        }

        public static Response<T> Succeed(T data, string statusCode, string message)
        {
            return new Response<T>(data, true, statusCode, message);
        }

        public static Response<T> Failed()
        {
            return new Response<T>(false);
        }

        public static Response<T> Failed(string message)
        {
            return new Response<T>(false, message);
        }

        public static Response<T> Failed(string message, Exception exception)
        {
            return new Response<T>(false, message, exception);
        }
    }
}
