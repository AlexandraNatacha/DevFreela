﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public static ResultViewModel Success()
           => new();

        public static ResultViewModel Erro(string Message)
            => new(false, Message);
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSuccess = true, string message = "") 
            : base(isSuccess, message)
        {
            Data = data;
        }

        public T? Data { get; set; }
        public static ResultViewModel<T> Success(T data)
            => new ResultViewModel<T>(data);

        public static ResultViewModel<T> Erro (string Message)
            => new (default, false, Message);
    }
}
