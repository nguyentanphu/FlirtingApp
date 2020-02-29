using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Domain.Common;
using MediatR;

namespace FlirtingApp.Application.Common
{
	public abstract class RequestBase<TResponse>: IRequest where TResponse: Result
	{
		[JsonIgnore] public IOutputPort<TResponse> OutputPort { get; set; } = default!;
	}
}
