using System;

namespace FlirtingApp.Application.Common
{
	public class Result
	{
		public bool Success { get; private set; }
		public string Error { get; private set; }

		public bool Failure => !Success;

		protected Result(bool success, string error)
		{
			if (success && !string.IsNullOrEmpty(error))
			{
				throw new ArgumentException("Success cannot come with error", nameof(success));
			}

			if (!success && string.IsNullOrEmpty(error))
			{
				throw new ArgumentException("Failure must come with an error", nameof(success));
			}

			Success = success;
			Error = error;
		}

		public static Result Fail(string message)
		{
			return new Result(false, message);
		}

		public static Result<T> Fail<T>(string message)
		{
			return new Result<T>(default(T), false, message);
		}

		public static Result Ok()
		{
			return new Result(true, String.Empty);
		}

		public static Result<T> Ok<T>(T value)
		{
			return new Result<T>(value, true, String.Empty);
		}
	}


	public class Result<T> : Result
	{
		private T _value;

		public T Value
		{
			get
			{
				if (Failure)
				{
					throw new InvalidOperationException("Cannot get value of fail result");
				}

				return _value;
			}
			private set => _value = value;
		}

		protected internal Result(T value, bool success, string error)
			: base(success, error)
		{
			if (success && value == null)
			{
				throw new ArgumentException("Cannot create success result of null value", "success, value");
			}

			Value = value;
		}
	}
}
