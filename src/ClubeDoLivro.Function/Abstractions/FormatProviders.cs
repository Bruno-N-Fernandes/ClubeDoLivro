using System;
using System.Globalization;

namespace ClubeDoLivro.Function.Abstractions
{
	public class FormatProviders : IFormatProviders
	{
		public static readonly CultureInfo ptBR = new("pt-BR");
		public static readonly CultureInfo enUS = new("en-US");

		public IFormatProvider PT_BR => ptBR;
		public IFormatProvider EN_US => enUS;
		public IFormatProvider Default => enUS;

		public object GetFormat(Type formatType) => Default.GetFormat(formatType);
	}

}