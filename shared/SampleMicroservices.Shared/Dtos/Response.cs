﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SampleMicroservices.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get;  set; }

        [JsonIgnore]
        public int StatusCode { get;  set; }
        [JsonIgnore]
        public bool IsSuccessful { get;  set; }

        public List<string> Errors { get; set; }
        public static Response<T> Success(T data,int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode,IsSuccessful=true };
        }

        public static Response<T> Fail(List<string> errors,int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = false,Errors=errors };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = false, Errors = new List<string>() { error} };
        }
    }
}
