using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlirtingApp.Application.Auth.Commands.ExchangeTokens;
using FluentAssertions;
using Xunit;

namespace FlirtingApp.Application.Tests.Auth.Commands.ExchangeTokens
{
	public class ExchangeTokensCommandValidatorTests
	{
		private ExchangeTokensCommandValidator _sut = new ExchangeTokensCommandValidator();

		[Fact]
		public void RefreshTokenLengthLessThan10_HasValidationError()
		{
			var command = new ExchangeTokensCommand
			{
				RefreshToken = "abc",
				AccessToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiODJkZTg0Y2YtZmZlZS00OGY1LTAzYmQtMDhkNzg1NjU4N2QwIiwic2VjdXJpdHlfdXNlcl9pZCI6Ijg5ZGNmNWI5LTg0ZWQtNDQ0Mi01NGJiLTA4ZDc4NTY1ODdhOCIsInJvbCI6ImFwaV9hY2Nlc3MiLCJzdWIiOiJNaW5keSIsImp0aSI6IjZlZDJjMjIzLWZiYmYtNDQ5Ny1hMTdlLTMwNWViMmQ5MWZhZCIsIm5iZiI6MTU3Njk5MzQ5NCwiZXhwIjoxNTc2OTk3MDk0LCJpYXQiOjE1NzY5OTM0OTQsImlzcyI6IkZsaXJ0aW5nQXBwQXBpIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNjcvIn0.o-L2H3oZl3oZk6MR991VE-o52IcqM835DNtqAV22Ff7YBF-Lno70-UUou0o1vgP0gkxc6sMqMvdk1P9Gx6e3tg",
				RemoteIpAddress = "192.168.1.1"
			};

			var result = _sut.Validate(command);

			result.Should().NotBeNull();
			result.Errors.Should().HaveCount(1);
			result.Errors.Any(v => v.PropertyName == nameof(ExchangeTokensCommand.RefreshToken)).Should().BeTrue();
		}

		[Fact]
		public void AccessTokenLengthLessThan100_HasValidationError()
		{
			var command = new ExchangeTokensCommand
			{
				RefreshToken = "1234567891011",
				AccessToken = "eyJhbGciOiJodHR",
				RemoteIpAddress = "192.168.1.1"
			};

			var result = _sut.Validate(command);

			result.Should().NotBeNull();
			result.Errors.Should().HaveCount(1);
			result.Errors.Any(v => v.PropertyName == nameof(ExchangeTokensCommand.AccessToken)).Should().BeTrue();
		}

		[Fact]
		public void IpAddressIsEmpty_HasValidationError()
		{
			var command = new ExchangeTokensCommand
			{
				RefreshToken = "1234567891011",
				AccessToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VyX2lkIjoiODJkZTg0Y2YtZmZlZS00OGY1LTAzYmQtMDhkNzg1NjU4N2QwIiwic2VjdXJpdHlfdXNlcl9pZCI6Ijg5ZGNmNWI5LTg0ZWQtNDQ0Mi01NGJiLTA4ZDc4NTY1ODdhOCIsInJvbCI6ImFwaV9hY2Nlc3MiLCJzdWIiOiJNaW5keSIsImp0aSI6IjZlZDJjMjIzLWZiYmYtNDQ5Ny1hMTdlLTMwNWViMmQ5MWZhZCIsIm5iZiI6MTU3Njk5MzQ5NCwiZXhwIjoxNTc2OTk3MDk0LCJpYXQiOjE1NzY5OTM0OTQsImlzcyI6IkZsaXJ0aW5nQXBwQXBpIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNjcvIn0.o-L2H3oZl3oZk6MR991VE-o52IcqM835DNtqAV22Ff7YBF-Lno70-UUou0o1vgP0gkxc6sMqMvdk1P9Gx6e3tg",
				RemoteIpAddress = ""
			};

			var result = _sut.Validate(command);

			result.Should().NotBeNull();
			result.Errors.Should().HaveCount(1);
			result.Errors.Any(v => v.PropertyName == nameof(ExchangeTokensCommand.RemoteIpAddress)).Should().BeTrue();
		}
	}
}
