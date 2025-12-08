using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Commons.Models
{

	public class Result
	{
		public bool Succeeded { get; set; }
		public string[] Errors { get; set; } = Array.Empty<string>();
		public string? Message { get; set; }

		public static Result Success() => new() { Succeeded = true };
		public static Result Success(string message) => new() { Succeeded = true, Message = message };
		public static Result Failure(params string[] errors) => new() { Succeeded = false, Errors = errors };
	}

	public class Result<T> : Result
	{
		public T? Data { get; set; }

		public static Result<T> Success(T data) => new() { Succeeded = true, Data = data };
		public static Result<T> Success(T data, string message) => new() { Succeeded = true, Data = data, Message = message };
		public new static Result<T> Failure(params string[] errors) => new() { Succeeded = false, Errors = errors };
	}
}
