using System;
using System.Collections.Generic;
using System.Text;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface IOutputPort<in TResponse> where TResponse: ResponseBase
	{
		void Handle(TResponse model);
	}
}
