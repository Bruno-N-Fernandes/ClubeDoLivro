using System;

namespace ClubeDoLivro.Function.Abstractions
{
	public interface IFormatProviders
	{
		IFormatProvider PT_BR { get; }
		IFormatProvider EN_US { get; }
		IFormatProvider Default { get; }
	}
}