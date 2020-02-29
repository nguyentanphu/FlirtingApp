using System;
using System.Collections.Generic;
using System.Text;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface IOutputPort<in TResponse> where TResponse : Result
	{
		void Handle(TResponse model);
	}
}
